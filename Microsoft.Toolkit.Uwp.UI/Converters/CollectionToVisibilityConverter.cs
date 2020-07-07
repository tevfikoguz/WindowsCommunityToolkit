// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Microsoft.Toolkit.Uwp.UI.Converters
{
    /// <summary>
    /// This class converts a collection size to visibility.
    /// </summary>
    public class CollectionToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Convert a <see cref="bool"/> value to its negation.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to negate.</param>
        /// <returns>The negation of <paramref name="value"/>.</returns>
        [Pure]
        public static Visibility Convert(IEnumerable value)
        {
            return Any(value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Any(value as IEnumerable) ? ConverterTools.Visible : ConverterTools.Collapsed;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks whether the input source contains at least one item.
        /// </summary>
        /// <param name="source">The input source to inspect.</param>
        /// <returns>Whether or not <paramref name="source"/> has at least one item.</returns>
        [Pure]
        internal static bool Any(IEnumerable source)
        {
            if (source is null)
            {
                return false;
            }

            if (source is ICollection<object> collectionOfT)
            {
                return collectionOfT.Count != 0;
            }

            if (source is IReadOnlyCollection<object> readOnlyCollectionOfT)
            {
                return readOnlyCollectionOfT.Count != 0;
            }

            if (source is ICollection collection)
            {
                return collection.Count != 0;
            }

            IEnumerator enumerator = source.GetEnumerator();
            try
            {
                return enumerator.MoveNext();
            }
            finally
            {
                (enumerator as IDisposable)?.Dispose();
            }
        }
    }
}
