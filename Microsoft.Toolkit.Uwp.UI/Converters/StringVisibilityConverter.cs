// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.Contracts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Microsoft.Toolkit.Uwp.UI.Converters
{
    /// <summary>
    /// This class converts a string value into a Visibility value (if the value is null or empty returns a collapsed value).
    /// </summary>
    public class StringVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Convert a <see cref="bool"/> value to its negation.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to negate.</param>
        /// <returns>The negation of <paramref name="value"/>.</returns>
        [Pure]
        public static Visibility Convert(string value)
        {
            return string.IsNullOrEmpty(value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.IsNullOrEmpty(value as string) ? ConverterTools.Collapsed : ConverterTools.Visible;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
