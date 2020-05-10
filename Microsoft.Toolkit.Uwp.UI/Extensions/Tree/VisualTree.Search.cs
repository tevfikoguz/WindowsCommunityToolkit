// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#nullable enable

namespace Microsoft.Toolkit.Uwp.UI.Extensions
{
    /// <summary>
    /// Defines a collection of extensions methods for UI.
    /// </summary>
    public static partial class VisualTree
    {
        /// <summary>
        /// Performs an ascendant search looking for a target element.
        /// </summary>
        /// <typeparam name="TMatcher">The <see cref="IMatcher{T}"/> type to use.</typeparam>
        /// <typeparam name="TResult">The resulting type to look for.</typeparam>
        /// <param name="element">The initial element to start the search from.</param>
        /// <param name="matcher">The current <typeparamref name="TMatcher"/> value to use.</param>
        /// <param name="result">The resulting value, if found.</param>
        /// <returns>Whether or not a match has been found.</returns>
        private static bool TryFindAscendant<TMatcher, TResult>(
            this DependencyObject element,
            ref TMatcher matcher,
            out TResult? result)
            where TMatcher : struct, IMatcher<TResult>
            where TResult : DependencyObject
        {
            DependencyObject? parent = VisualTreeHelper.GetParent(element);

            if (parent is null)
            {
                result = null;

                return false;
            }

            if (matcher.TryMatch(parent, out result))
            {
                return true;
            }

            return TryFindAscendant(parent, ref matcher, out result);
        }

        /// <summary>
        /// Performs a pre-order search looking for a target element.
        /// </summary>
        /// <typeparam name="TMatcher">The <see cref="IMatcher{T}"/> type to use.</typeparam>
        /// <typeparam name="TResult">The resulting type to look for.</typeparam>
        /// <param name="element">The initial element to start the search from.</param>
        /// <param name="matcher">The current <typeparamref name="TMatcher"/> value to use.</param>
        /// <param name="result">The resulting value, if found.</param>
        /// <returns>Whether or not a match has been found.</returns>
        private static bool TryFindDescendant<TMatcher, TResult>(
            this DependencyObject element,
            ref TMatcher matcher,
            out TResult? result)
            where TMatcher : struct, IMatcher<TResult>
            where TResult : DependencyObject
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(element);

            for (var i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);

                if (matcher.TryMatch(element, out result))
                {
                    return true;
                }

                if (TryFindDescendant(child, ref matcher, out result))
                {
                    return true;
                }
            }

            result = null;

            return false;
        }
    }
}
