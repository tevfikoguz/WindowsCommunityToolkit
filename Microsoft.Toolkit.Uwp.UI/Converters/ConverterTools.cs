// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace Microsoft.Toolkit.Uwp.UI.Converters
{
    /// <summary>
    /// Static class used to provide internal tools
    /// </summary>
    internal static class ConverterTools
    {
        /// <summary>
        /// The reusable boxed <see langword="true"/> value.
        /// </summary>
        public static readonly object True = true;

        /// <summary>
        /// The reusable boxed <see langword="false"/> value.
        /// </summary>
        public static readonly object False = false;

        /// <summary>
        /// The reusable boxed <see cref="Visibility.Visible"/> value.
        /// </summary>
        public static readonly object Visible = Visibility.Visible;

        /// <summary>
        /// The reusable boxed <see cref="Visibility.Collapsed"/> value.
        /// </summary>
        public static readonly object Collapsed = Visibility.Collapsed;

        /// <summary>
        /// Checks whether the input source contains at least one item.
        /// </summary>
        /// <param name="source">The input source to inspect.</param>
        /// <returns>Whether or not <paramref name="source"/> has at least one item.</returns>
        [Pure]
        public static bool Any(IEnumerable source)
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

        /// <summary>
        /// Helper method to safely cast an object to a boolean
        /// </summary>
        /// <param name="parameter">Parameter to cast to a boolean</param>
        /// <returns>Bool value or false if cast failed</returns>
        internal static bool TryParseBool(object parameter)
        {
            var parsed = false;

            if (parameter != null)
            {
                bool.TryParse(parameter.ToString(), out parsed);
            }

            return parsed;
        }

        /// <summary>
        /// Helper method to convert a value from a source type to a target type.
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="targetType">The target type</param>
        /// <returns>The converted value</returns>
        internal static object Convert(object value, Type targetType)
        {
            if (targetType.IsInstanceOfType(value))
            {
                return value;
            }

            return XamlBindingHelper.ConvertValue(targetType, value);
        }
    }
}
