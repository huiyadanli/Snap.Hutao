﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.UI;
using Snap.Hutao.Control;
using Snap.Hutao.Model.Intrinsic;
using Snap.Hutao.Win32;
using Windows.UI;

namespace Snap.Hutao.Model.Metadata.Converter;

/// <summary>
/// 品质颜色转换器
/// </summary>
[HighQuality]
internal sealed class QualityColorConverter : ValueConverter<QualityType, Color>
{
    private static readonly Dictionary<string, QualityType> LocalizedNameToQualityType = new()
    {
        [SH.ModelIntrinsicItemQualityWhite] = QualityType.QUALITY_WHITE,
        [SH.ModelIntrinsicItemQualityGreen] = QualityType.QUALITY_GREEN,
        [SH.ModelIntrinsicItemQualityBlue] = QualityType.QUALITY_BLUE,
        [SH.ModelIntrinsicItemQualityPurple] = QualityType.QUALITY_PURPLE,
        [SH.ModelIntrinsicItemQualityOrange] = QualityType.QUALITY_ORANGE,
        [SH.ModelIntrinsicItemQualityRed] = QualityType.QUALITY_ORANGE_SP,
    };

    public static Color QualityNameToColor(string qualityName)
    {
        return QualityToColor(LocalizedNameToQualityType.GetValueOrDefault(qualityName));
    }

    public static Color QualityToColor(QualityType quality)
    {
        return quality switch
        {
            QualityType.QUALITY_WHITE => StructMarshal.Color(0xFF72778B),
            QualityType.QUALITY_GREEN => StructMarshal.Color(0xFF2A8F72),
            QualityType.QUALITY_BLUE => StructMarshal.Color(0xFF5180CB),
            QualityType.QUALITY_PURPLE => StructMarshal.Color(0xFFA156E0),
            QualityType.QUALITY_ORANGE or QualityType.QUALITY_ORANGE_SP => StructMarshal.Color(0xFFBC6932),
            _ => Colors.Transparent,
        };
    }

    /// <inheritdoc/>
    public override Color Convert(QualityType from)
    {
        return QualityToColor(from);
    }
}