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
    /// This class converts a collection size into an other object.
    /// Can be used to convert to bind a visibility, a color or an image to the size of the collection.
    /// </summary>
    public class CollectionToObjectConverter : DependencyObject, IValueConverter
    {
        /// <summary>
        /// Gets or sets the value to be returned when the object is either <see langword="null"/> or empty.
        /// </summary>
        public object EmptyValue
        {
            get => GetValue(EmptyValueProperty);
            set => SetValue(EmptyValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the value to be returned when the object is neither <see langword="null"/> nor empty.
        /// </summary>
        public object NotEmptyValue
        {
            get => GetValue(NotEmptyValueProperty);
            set => SetValue(NotEmptyValueProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="EmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty EmptyValueProperty = DependencyProperty.Register(
            nameof(EmptyValue),
            typeof(object),
            typeof(EmptyObjectToObjectConverter),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="NotEmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty NotEmptyValueProperty = DependencyProperty.Register(
            nameof(NotEmptyValue),
            typeof(object),
            typeof(EmptyObjectToObjectConverter),
            new PropertyMetadata(null));

        /// <summary>
        /// Convert an <see cref="IEnumerable"/> value to an other object.
        /// </summary>
        /// <param name="source">The input <see cref="IEnumerable"/> value.</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        [Pure]
        public object Convert(IEnumerable source)
        {
            return ConverterTools.Any(source) ? NotEmptyValue : EmptyValue;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert(value as IEnumerable);
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}