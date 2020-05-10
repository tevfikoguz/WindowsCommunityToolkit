// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
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
        /// Find descendant <see cref="FrameworkElement"/> control using its name.
        /// </summary>
        /// <param name="element">Parent element.</param>
        /// <param name="name">Name of the control to find</param>
        /// <returns>Descendant control or null if not found.</returns>
        public static FrameworkElement? FindDescendantByName(this DependencyObject element, string name)
        {
            var matcher = new NameMatcher(name);

            TryFindDescendant(element, ref matcher, out DependencyObject? result);

            return (FrameworkElement?)result;
        }

        /// <summary>
        /// Find first descendant control of a specified type.
        /// </summary>
        /// <typeparam name="T">Type to search for.</typeparam>
        /// <param name="element">Parent element.</param>
        /// <returns>Descendant control or null if not found.</returns>
        public static T? FindDescendant<T>(this DependencyObject element)
            where T : DependencyObject
        {
            var matcher = default(TypeMatcher<T>);

            TryFindDescendant(element, ref matcher, out T? result);

            return result;
        }

        /// <summary>
        /// Find first descendant control of a specified type.
        /// </summary>
        /// <param name="element">Parent element.</param>
        /// <param name="type">Type of descendant.</param>
        /// <returns>Descendant control or null if not found.</returns>
        public static DependencyObject? FindDescendant(this DependencyObject element, Type type)
        {
            var matcher = new TypeMatcher(type);

            TryFindDescendant(element, ref matcher, out DependencyObject? result);

            return result;
        }

        /// <summary>
        /// Find all descendant controls of the specified type.
        /// </summary>
        /// <typeparam name="T">Type to search for.</typeparam>
        /// <param name="element">Parent element.</param>
        /// <returns>Descendant controls or empty if not found.</returns>
        public static IEnumerable<T> FindDescendants<T>(this DependencyObject element)
            where T : DependencyObject
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(element);

            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                var type = child as T;
                if (type != null)
                {
                    yield return type;
                }

                foreach (T childofChild in child.FindDescendants<T>())
                {
                    yield return childofChild;
                }
            }
        }

        /// <summary>
        /// Find visual ascendant <see cref="FrameworkElement"/> control using its name.
        /// </summary>
        /// <param name="element">Parent element.</param>
        /// <param name="name">Name of the control to find</param>
        /// <returns>Descendant control or null if not found.</returns>
        public static FrameworkElement? FindAscendantByName(this DependencyObject element, string name)
        {
            var matcher = new NameMatcher(name);

            TryFindAscendant(element, ref matcher, out DependencyObject? result);

            return (FrameworkElement?)result;
        }

        /// <summary>
        /// Find first visual ascendant control of a specified type.
        /// </summary>
        /// <typeparam name="T">Type to search for.</typeparam>
        /// <param name="element">Child element.</param>
        /// <returns>Ascendant control or null if not found.</returns>
        public static T? FindAscendant<T>(this DependencyObject element)
            where T : DependencyObject
        {
            var matcher = default(TypeMatcher<T>);

            TryFindAscendant(element, ref matcher, out T? result);

            return result;
        }

        /// <summary>
        /// Find first visual ascendant control of a specified type.
        /// </summary>
        /// <param name="element">Child element.</param>
        /// <param name="type">Type of ascendant to look for.</param>
        /// <returns>Ascendant control or null if not found.</returns>
        public static DependencyObject? FindAscendant(this DependencyObject element, Type type)
        {
            var matcher = new TypeMatcher(type);

            TryFindDescendant(element, ref matcher, out DependencyObject? result);

            return result;
        }

        /// <summary>
        /// Find all visual ascendants for the element.
        /// </summary>
        /// <param name="element">Child element.</param>
        /// <returns>A collection of parent elements or null if none found.</returns>
        public static IEnumerable<DependencyObject> FindAscendants(this DependencyObject element)
        {
            var parent = VisualTreeHelper.GetParent(element);

            while (parent != null)
            {
                yield return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
        }
    }
}
