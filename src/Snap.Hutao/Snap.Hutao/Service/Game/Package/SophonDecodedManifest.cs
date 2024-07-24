﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Web.Hoyolab.Takumi.Downloader.Proto;

namespace Snap.Hutao.Service.Game.Package;

internal sealed class SophonDecodedManifest
{
    public SophonDecodedManifest(string urlPrefix, SophonManifestProto sophonManifestProto)
    {
        UrlPrefix = urlPrefix;
        ManifestProto = sophonManifestProto;
    }

    public string UrlPrefix { get; }

    public SophonManifestProto ManifestProto { get; }
}