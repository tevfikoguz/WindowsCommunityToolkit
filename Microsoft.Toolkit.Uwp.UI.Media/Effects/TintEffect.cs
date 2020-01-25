﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Toolkit.Uwp.UI.Media.Effects.Interfaces;
using Windows.UI;

namespace FluentExtensions.UI.Brushes.Brushes.Effects
{
    /// <summary>
    /// A tint effect with a customizable opacity
    /// </summary>
    public sealed class TintEffect : IPipelineEffect
    {
        /// <summary>
        /// Gets or sets the tint color to use
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the opacity of the tint effect
        /// </summary>
        public double Opacity { get; set; }
    }
}