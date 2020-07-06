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
    /// This class converts <see cref="Visibility"/> values into <see cref="bool"/> values.
    /// </summary>
    public class VisibilityToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Convert a <see cref="Visibility"/> value to a <see cref="bool"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Visibility"/> value to convert.</param>
        /// <returns>The <see cref="bool"/> value for <paramref name="value"/>.</returns>
        [Pure]
        public static bool Convert(Visibility value)
        {
            return value == Visibility.Visible;
        }

        /// <summary>
        /// Convert a <see cref="bool"/> value to a <see cref="Visibility"/> value.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to convert.</param>
        /// <returns>The <see cref="Visibility"/> value for <paramref name="value"/>.</returns>
        [Pure]
        public Visibility ConvertBack(bool value)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert((value as Visibility?).GetValueOrDefault());
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ConvertBack((value as bool?).GetValueOrDefault());
        }
    }
}
