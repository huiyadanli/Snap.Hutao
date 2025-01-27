// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.UI.Xaml.Controls;
using Snap.Hutao.Core;
using Snap.Hutao.Core.IO.Http.Proxy;
using Snap.Hutao.Core.LifeCycle;
using Snap.Hutao.Core.Setting;
using Snap.Hutao.Factory.ContentDialog;
using Snap.Hutao.Factory.Progress;
using Snap.Hutao.Service.Metadata;
using Snap.Hutao.Service.Notification;
using Snap.Hutao.Service.Update;
using Snap.Hutao.UI.Input.HotKey;
using Snap.Hutao.UI.Xaml.Behavior.Action;
using Snap.Hutao.UI.Xaml.Control.Theme;
using Snap.Hutao.UI.Xaml.View.Dialog;
using Snap.Hutao.UI.Xaml.View.Window.WebView2;
using Snap.Hutao.Web.Hutao;
using System.Globalization;
using System.IO;
using System.Text;

namespace Snap.Hutao.ViewModel;

[ConstructorGenerated]
[Injection(InjectAs.Singleton)]
[SuppressMessage("", "SA1201")]
internal sealed partial class TitleViewModel : Abstraction.ViewModel
{
    private readonly ICurrentXamlWindowReference currentXamlWindowReference;
    private readonly HttpProxyUsingSystemProxy httpProxyUsingSystemProxy;
    private readonly IContentDialogFactory contentDialogFactory;
    private readonly IMetadataService metadataService;
    private readonly IProgressFactory progressFactory;
    private readonly ILogger<TitleViewModel> logger;
    private readonly IInfoBarService infoBarService;
    private readonly IUpdateService updateService;
    private readonly ITaskContext taskContext;
    private readonly App app;

    public static string Title
    {
        get
        {
            string name = new StringBuilder()
                .Append("App")
                .AppendIf(HutaoRuntime.IsProcessElevated, "Elevated")
#if DEBUG
                .Append("Dev")
#endif
                .Append("NameAndVersion")
                .ToString();

            string? format = SH.GetString(CultureInfo.CurrentCulture, name);
            ArgumentException.ThrowIfNullOrEmpty(format);
            return string.Format(CultureInfo.CurrentCulture, format, HutaoRuntime.Version);
        }
    }

    public partial RuntimeOptions RuntimeOptions { get; }

    public partial HotKeyOptions HotKeyOptions { get; }

    public UpdateStatus? UpdateStatus { get; set => SetProperty(ref field, value); }

    public bool IsMetadataInitialized { get; set => SetProperty(ref field, value); }

    protected override async ValueTask<bool> LoadOverrideAsync()
    {
        ShowUpdateLogWindowAfterUpdate();
        NotifyIfDataFolderHasReparsePoint();
        WaitMetadataInitializationAsync().SafeForget(logger);
        await DoCheckUpdateAsync().ConfigureAwait(false);
        await CheckProxyAndLoopbackAsync().ConfigureAwait(false);
        return true;
    }

    private void ShowUpdateLogWindowAfterUpdate()
    {
        if (LocalSetting.Get(SettingKeys.AlwaysIsFirstRunAfterUpdate, false) || XamlApplicationLifetime.IsFirstRunAfterUpdate)
        {
            XamlApplicationLifetime.IsFirstRunAfterUpdate = false;
            ShowWebView2WindowAction.Show<UpdateLogContentProvider>(currentXamlWindowReference.GetXamlRoot());
        }
    }

    private async ValueTask DoCheckUpdateAsync()
    {
        IProgress<UpdateStatus> progress = progressFactory.CreateForMainThread<UpdateStatus>(status => UpdateStatus = status);
        CheckUpdateResult checkUpdateResult = await updateService.CheckUpdateAsync(progress).ConfigureAwait(false);

        if (currentXamlWindowReference.Window is null)
        {
            return;
        }

        if (checkUpdateResult.Kind is CheckUpdateResultKind.NeedDownload)
        {
            UpdatePackageDownloadConfirmDialog dialog = await contentDialogFactory
                .CreateInstanceAsync<UpdatePackageDownloadConfirmDialog>()
                .ConfigureAwait(false);

            await taskContext.SwitchToMainThreadAsync();

            dialog.Title = SH.FormatViewTitileUpdatePackageDownloadTitle(UpdateStatus?.Version);
            dialog.Mirrors = checkUpdateResult.PackageInformation?.Mirrors;
            dialog.SelectedItem = dialog.Mirrors?.FirstOrDefault();

            (bool isOk, HutaoPackageMirror? mirror) = await dialog.GetSelectedMirrorAsync().ConfigureAwait(false);

            if (isOk && mirror is not null)
            {
                ArgumentNullException.ThrowIfNull(checkUpdateResult.PackageInformation);
                HutaoSelectedMirrorInformation mirrorInformation = new()
                {
                    Mirror = mirror,
                    Validation = checkUpdateResult.PackageInformation.Validation,
                    Version = checkUpdateResult.PackageInformation.Version,
                };

                // This method will set CheckUpdateResult.Kind to NeedInstall if download success
                if (!await DownloadPackageAsync(progress, mirrorInformation, checkUpdateResult).ConfigureAwait(false))
                {
                    infoBarService.Warning(SH.ViewTitileUpdatePackageDownloadFailedMessage);
                    return;
                }
            }
        }

        if (currentXamlWindowReference.Window is null)
        {
            return;
        }

        if (checkUpdateResult.Kind is CheckUpdateResultKind.NeedInstall)
        {
            ContentDialogResult installUpdateUserConsentResult = await contentDialogFactory
                .CreateForConfirmCancelAsync(
                    SH.FormatViewTitileUpdatePackageReadyTitle(UpdateStatus?.Version),
                    SH.ViewTitileUpdatePackageReadyContent,
                    ContentDialogButton.Primary)
                .ConfigureAwait(false);

            if (installUpdateUserConsentResult is ContentDialogResult.Primary)
            {
                LaunchUpdaterResult launchUpdaterResult = updateService.LaunchUpdater();
                if (launchUpdaterResult.IsSuccess)
                {
                    ContentDialog contentDialog = await contentDialogFactory
                        .CreateForIndeterminateProgressAsync(SH.ViewTitleUpdatePackageInstallingContent)
                        .ConfigureAwait(false);
                    using (await contentDialogFactory.BlockAsync(contentDialog).ConfigureAwait(false))
                    {
                        if (launchUpdaterResult.Process is { } updater)
                        {
                            await updater.WaitForExitAsync().ConfigureAwait(false);
                        }
                    }
                }
            }
        }

        await taskContext.SwitchToMainThreadAsync();
        UpdateStatus = null;
    }

    private async ValueTask<bool> DownloadPackageAsync(IProgress<UpdateStatus> progress, HutaoSelectedMirrorInformation mirrorInformation, CheckUpdateResult checkUpdateResult)
    {
        bool downloadSuccess = true;
        try
        {
            if (await updateService.DownloadUpdateAsync(mirrorInformation, progress).ConfigureAwait(false))
            {
                checkUpdateResult.Kind = CheckUpdateResultKind.NeedInstall;
            }
            else
            {
                downloadSuccess = false;
            }
        }
        catch
        {
            downloadSuccess = false;
        }

        return downloadSuccess;
    }

    private void NotifyIfDataFolderHasReparsePoint()
    {
        if (new DirectoryInfo(HutaoRuntime.DataFolder).Attributes.HasFlag(FileAttributes.ReparsePoint))
        {
            infoBarService.Warning(SH.FormatViewModelTitleDataFolderHasReparsepoint(HutaoRuntime.DataFolder));
        }
    }

    private async ValueTask CheckProxyAndLoopbackAsync()
    {
        if (!httpProxyUsingSystemProxy.IsUsingProxyAndNotWorking)
        {
            return;
        }

        ContentDialogResult result = await contentDialogFactory
            .CreateForConfirmCancelAsync(SH.ViewDialogFeedbackEnableLoopbackTitle, SH.ViewDialogFeedbackEnableLoopbackDescription)
            .ConfigureAwait(false);

        if (result is ContentDialogResult.Primary)
        {
            await taskContext.SwitchToMainThreadAsync();
            httpProxyUsingSystemProxy.EnableLoopback();
        }
    }

    private async ValueTask WaitMetadataInitializationAsync()
    {
        try
        {
            await metadataService.InitializeAsync().ConfigureAwait(false);
        }
        finally
        {
            await taskContext.SwitchToMainThreadAsync();
            IsMetadataInitialized = true;
        }
    }
}

internal sealed partial class TitleViewModel
{
    public static bool IsDebug
    {
        get =>
#if DEBUG
            true;
#else
            false;
#endif
    }

#if DEBUG
    [Command("InvertAppThemeCommand")]
    private void InvertAppTheme()
    {
        WinUI.FrameworkTheming.FrameworkTheming.SetTheme(ThemeHelper.ApplicationToFrameworkInvert(app.RequestedTheme));
    }
#endif
}