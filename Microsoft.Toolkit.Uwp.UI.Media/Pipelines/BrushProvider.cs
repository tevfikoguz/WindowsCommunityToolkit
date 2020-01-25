﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace Microsoft.Toolkit.Uwp.UI.Media.Pipelines
{
    /// <summary>
    /// A simple container <see langword="class"/> used to store info on a custom composition effect to create
    /// </summary>
    public sealed class BrushProvider
    {
        /// <summary>
        /// Gets the name of the target <see cref="CompositionEffectSourceParameter"/>
        /// </summary>
        internal string Name { get; }

        /// <summary>
        /// Gets the stored effect initializer
        /// </summary>
        internal Func<Task<CompositionBrush>> Initializer { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrushProvider"/> class.
        /// </summary>
        /// <param name="name">The name of the target <see cref="CompositionEffectSourceParameter"/></param>
        /// <param name="initializer">The stored effect initializer</param>
        private BrushProvider(string name, Func<Task<CompositionBrush>> initializer)
        {
            this.Name = name;
            this.Initializer = initializer;
        }

        /// <summary>
        /// Creates a new instance with the info on a given <see cref="CompositionEffectSourceParameter"/> to initialize
        /// </summary>
        /// <param name="name">The target effect name</param>
        /// <param name="initializer">A <see cref="Func{TResult}"/> instance that will produce the <see cref="CompositionBrush"/> to use to initialize the effect</param>
        /// <returns>A <see cref="BrushProvider"/> instance with the input initializer</returns>
        [Pure]
        public static BrushProvider New(string name, Func<CompositionBrush> initializer) => new BrushProvider(name, () => Task.FromResult(initializer()));

        /// <summary>
        /// Creates a new instance with the info on a given <see cref="CompositionEffectSourceParameter"/> to initialize
        /// </summary>
        /// <param name="name">The target effect name</param>
        /// <param name="initializer">An asynchronous <see cref="Func{TResult}"/> instance that will produce the <see cref="CompositionBrush"/> to use to initialize the effect</param>
        /// <returns>A <see cref="BrushProvider"/> instance with the input initializer</returns>
        [Pure]
        public static BrushProvider New(string name, Func<Task<CompositionBrush>> initializer) => new BrushProvider(name, initializer);
    }
}