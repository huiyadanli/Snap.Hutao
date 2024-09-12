﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Model.Intrinsic;
using Snap.Hutao.Model.Primitive;

namespace Snap.Hutao.Service.Metadata.ContextAbstraction;

internal interface IMetadataDictionaryLevelWeaponGrowCurveSource
{
    Dictionary<Level, Dictionary<GrowCurveType, float>> LevelDictionaryWeaponGrowCurveMap { get; set; }
}