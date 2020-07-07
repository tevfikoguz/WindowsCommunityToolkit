// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
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
            return ConverterTools.Any(value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ConverterTools.Any(value as IEnumerable) ? ConverterTools.Visible : ConverterTools.Collapsed;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
