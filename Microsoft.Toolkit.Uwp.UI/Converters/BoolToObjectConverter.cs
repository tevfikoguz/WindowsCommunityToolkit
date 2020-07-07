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
    /// This class converts a boolean value into an other object.
    /// Can be used to convert true/false to visibility, a couple of colors, couple of images, etc.
    /// </summary>
    public class BoolToObjectConverter : DependencyObject, IValueConverter
    {
        /// <summary>
        /// Gets or sets the value to be returned when the boolean is true.
        /// </summary>
        public object TrueValue
        {
            get => GetValue(TrueValueProperty);
            set => SetValue(TrueValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the value to be returned when the boolean is false.
        /// </summary>
        public object FalseValue
        {
            get => GetValue(FalseValueProperty);
            set => SetValue(FalseValueProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="TrueValue"/> property.
        /// </summary>
        public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register(
            nameof(TrueValue),
            typeof(object),
            typeof(BoolToObjectConverter),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="FalseValue"/> property.
        /// </summary>
        public static readonly DependencyProperty FalseValueProperty = DependencyProperty.Register(
            nameof(FalseValue),
            typeof(object),
            typeof(BoolToObjectConverter),
            new PropertyMetadata(null));

        /// <summary>
        /// Convert a boolean value to an other object.
        /// </summary>
        /// <param name="value">The input <see cref="bool"/> value.</param>
        /// <param name="invert">Whether or not to invert <paramref name="value"/>.</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        [Pure]
        public object Convert(bool value, bool invert)
        {
            return (value && !invert) || (!value && invert)
                ? TrueValue
                : FalseValue;
        }

        /// <summary>
        /// Convert back the value to a boolean.
        /// </summary>
        /// <param name="value">The input <see cref="bool"/> value.</param>
        /// <param name="invert">Whether or not to invert <paramref name="value"/>.</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        /// <remarks>If the <paramref name="value"/> parameter is a reference type, <see cref="TrueValue"/> must match its reference to return true.</remarks>
        [Pure]
        public bool ConvertBack(object value, bool invert)
        {
            bool result = ReferenceEquals(value, TrueValue);

            if (invert)
            {
                result = !result;
            }

            return result;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert(
                (value as bool?).GetValueOrDefault(),
                ConverterTools.TryParseBool(parameter));
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ConvertBack(value, ConverterTools.TryParseBool(parameter));
        }
    }
}
