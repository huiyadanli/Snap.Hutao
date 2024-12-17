// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Service.Achievement;
using Snap.Hutao.Service.Metadata;
using Snap.Hutao.Service.Metadata.ContextAbstraction;
using Snap.Hutao.UI.Xaml.View.Page;
using System.Collections.Immutable;

namespace Snap.Hutao.ViewModel.Achievement;

[ConstructorGenerated(CallBaseConstructor = true)]
[Injection(InjectAs.Transient)]
internal sealed partial class AchievementViewModelSlim : Abstraction.ViewModelSlim<AchievementPage>
{
    public ImmutableArray<AchievementStatistics>? StatisticsList { get; set => SetProperty(ref field, value); }

    protected override async Task LoadAsync()
    {
        using (IServiceScope scope = ServiceProvider.CreateScope())
        {
            ITaskContext taskContext = scope.ServiceProvider.GetRequiredService<ITaskContext>();
            IMetadataService metadataService = scope.ServiceProvider.GetRequiredService<IMetadataService>();

            if (await metadataService.InitializeAsync().ConfigureAwait(false))
            {
                AchievementServiceMetadataContext context = await metadataService
                    .GetContextAsync<AchievementServiceMetadataContext>()
                    .ConfigureAwait(false);
                ImmutableArray<AchievementStatistics> array = await scope.ServiceProvider
                    .GetRequiredService<IAchievementStatisticsService>()
                    .GetAchievementStatisticsAsync(context)
                    .ConfigureAwait(false);

                await taskContext.SwitchToMainThreadAsync();
                StatisticsList = array;
                IsInitialized = true;
            }
        }
    }
}