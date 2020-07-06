// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.Contracts;
using Windows.UI.Xaml.Data;

namespace Microsoft.Toolkit.Uwp.UI.Converters
{
    /// <summary>
    /// Value converter that applies NOT operator to a <see cref="bool"/> value.
    /// </summary>
    public class BoolNegationConverter : IValueConverter
    {
        /// <summary>
        /// Convert a <see cref="bool"/> value to its negation.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to negate.</param>
        /// <returns>The negation of <paramref name="value"/>.</returns>
        [Pure]
        public static bool Convert(bool value)
        {
            return !value;
        }

        /// <inheritdoc cref="Convert(bool)"/>.
        [Pure]
        public bool ConvertBack(bool value)
        {
            // Same as above in this case, but we need
            // an instance method to enable x:Bind back.
            return !value;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value as bool?).GetValueOrDefault() ? ConverterTools.False : ConverterTools.True;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value as bool?).GetValueOrDefault() ? ConverterTools.False : ConverterTools.True;
        }
    }
}
