// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

#nullable enable

namespace Microsoft.Toolkit.Uwp.UI.Extensions
{
    /// <summary>
    /// Defines a collection of extensions methods for UI.
    /// </summary>
    public static partial class VisualTree
    {
        /// <summary>
        /// An <see cref="IMatcher{T}"/> implementation looking for a specific name.
        /// </summary>
        private readonly struct NameMatcher : IMatcher<DependencyObject>
        {
            private readonly string name;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public NameMatcher(string name)
            {
                this.name = name;
            }

            /// <inheritdoc/>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool TryMatch(DependencyObject value, out DependencyObject? result)
            {
                if ((value as FrameworkElement)?.Name.Equals(this.name) == true)
                {
                    result = value;

                    return true;
                }

                result = null;

                return false;
            }
        }

        /// <summary>
        /// An <see cref="IMatcher{T}"/> implementation looking for a specific type.
        /// </summary>
        private readonly struct TypeMatcher : IMatcher<DependencyObject>
        {
            private readonly Type type;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TypeMatcher(Type type)
            {
                this.type = type;
            }

            /// <inheritdoc/>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool TryMatch(DependencyObject value, out DependencyObject? result)
            {
                if (value.GetType() == this.type)
                {
                    result = value;

                    return true;
                }

                result = null;

                return false;
            }
        }

        /// <summary>
        /// An <see cref="IMatcher{T}"/> implementation looking for a specific type.
        /// </summary>
        /// <typeparam name="T">The type of elements to look for.</typeparam>
        private readonly struct TypeMatcher<T> : IMatcher<T>
            where T : DependencyObject
        {
            /// <inheritdoc/>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool TryMatch(DependencyObject value, out T? result)
            {
                if (value.GetType() == typeof(T))
                {
                    result = (T)value;

                    return true;
                }

                result = null;

                return false;
            }
        }

        /// <summary>
        /// An interface for a matcher used to explore a visual tree.
        /// </summary>
        /// <typeparam name="T">The target type of element to look for.</typeparam>
        private interface IMatcher<T>
            where T : DependencyObject
        {
            /// <summary>
            /// Tries to match the current element.
            /// </summary>
            /// <param name="value">The input <see cref="DependencyObject"/> value.</param>
            /// <param name="result">The resulting <typeparamref name="T"/> value, if matching.</param>
            /// <returns>Whether or not <paramref name="value"/> matched the search criteria.</returns>
            bool TryMatch(DependencyObject value, out T? result);
        }
    }
}
