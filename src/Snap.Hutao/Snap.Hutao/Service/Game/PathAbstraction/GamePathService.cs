// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Service.Game.Locator;
using System.Collections.Immutable;

namespace Snap.Hutao.Service.Game.PathAbstraction;

[ConstructorGenerated]
[Injection(InjectAs.Singleton, typeof(IGamePathService))]
internal sealed partial class GamePathService : IGamePathService
{
    private readonly IGameLocatorFactory gameLocatorFactory;
    private readonly LaunchOptions launchOptions;

    public async ValueTask<ValueResult<bool, string>> SilentGetGamePathAsync()
    {
        // Found in setting
        if (!string.IsNullOrEmpty(launchOptions.GamePath))
        {
            return new(true, launchOptions.GamePath);
        }

        // Try to locate by unity log
        if (await gameLocatorFactory.LocateSingleAsync(GameLocationSourceKind.UnityLog).ConfigureAwait(false) is (true, { } path1))
        {
            launchOptions.UpdateGamePath(path1);
            return new(true, launchOptions.GamePath);
        }

        // Try to locate by registry
        if (await gameLocatorFactory.LocateSingleAsync(GameLocationSourceKind.Registry).ConfigureAwait(false) is (true, { } path2))
        {
            launchOptions.UpdateGamePath(path2);
            return new(true, launchOptions.GamePath);
        }

        return new(false, SH.ServiceGamePathLocateFailed);
    }

    public async ValueTask SilentLocateAllGamePathAsync()
    {
        HashSet<string> paths = [];
        foreach (string path in await gameLocatorFactory.LocateMultipleAsync(GameLocationSourceKind.UnityLog).ConfigureAwait(false))
        {
            paths.Add(path);
        }

        foreach (string path in await gameLocatorFactory.LocateMultipleAsync(GameLocationSourceKind.Registry).ConfigureAwait(false))
        {
            paths.Add(path);
        }

        using (await launchOptions.GamePathLock.WriterLockAsync().ConfigureAwait(false))
        {
            foreach (GamePathEntry entry in launchOptions.GamePathEntries)
            {
                paths.Remove(entry.Path);
            }

            ImmutableArray<GamePathEntry>.Builder builder = launchOptions.GamePathEntries.ToBuilder();
            builder.AddRange(paths.Select(GamePathEntry.Create));

            // Since all path we add are not in original list, we can skip calling PerformGamePathEntrySynchronization
            launchOptions.GamePathEntries = builder.ToImmutable();
        }
    }
}