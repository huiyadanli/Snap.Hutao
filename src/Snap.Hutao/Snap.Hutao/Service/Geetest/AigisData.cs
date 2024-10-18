﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

namespace Snap.Hutao.Service.Geetest;

internal sealed class AigisData
{
    [JsonPropertyName("success")]
    public int Success { get; set; }

    [JsonPropertyName("gt")]
    public string GT { get; set; } = default!;

    [JsonPropertyName("challenge")]
    public string Challenge { get; set; } = default!;

    [JsonPropertyName("new_captcha")]
    public int NewCaptcha { get; set; }
}