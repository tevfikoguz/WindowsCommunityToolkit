﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

#nullable enable

namespace Microsoft.Toolkit.Diagnostics
{
    /// <summary>
    /// Helper methods to verify conditions when running code.
    /// </summary>
    [DebuggerStepThrough]
    public static partial class Guard
    {
        /// <summary>
        /// Asserts that the input value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of reference value type being tested.</typeparam>
        /// <param name="value">The input value to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is not <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNull<T>(T? value, string name)
            where T : class
        {
            if (!(value is null))
            {
                ThrowHelper.ThrowArgumentExceptionForIsNull(value, name);
            }
        }

        /// <summary>
        /// Asserts that the input value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of nullable value type being tested.</typeparam>
        /// <param name="value">The input value to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is not <see langword="null"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNull<T>(T? value, string name)
            where T : struct
        {
            if (!(value is null))
            {
                ThrowHelper.ThrowArgumentExceptionForIsNull(value, name);
            }
        }

        /// <summary>
        /// Asserts that the input value is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of reference value type being tested.</typeparam>
        /// <param name="value">The input value to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1114", Justification = "Comment for [NotNull] attribute")]
        public static void IsNotNull<T>(
#if NETSTANDARD2_1
            // On .NET Standard 2.1, we can add the [NotNull] attribute for
            // methods that throw if an argument of a nullable type is null.
            // This will let the compiler know that after a call to such
            // methods, the value of the variable passed as parameter will
            // never be null (since if that's the case, the code would have
            // thrown an exception instead of continuing the execution).
            // This makes it easy for developers to keep track of the
            // nullable state of variables, and to avoid having to use
            // the '!' operator to explicitly ignore compiler warnings
            // about variables possibly being null.
            [NotNull]
#endif
            T? value, string name)
            where T : class
        {
            if (value is null)
            {
                ThrowHelper.ThrowArgumentNullExceptionForIsNotNull<T>(name);
            }
        }

        /// <summary>
        /// Asserts that the input value is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of nullable value type being tested.</typeparam>
        /// <param name="value">The input value to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotNull<T>(
#if NETSTANDARD2_1
            [NotNull]
#endif
            T? value, string name)
            where T : struct
        {
            if (value is null)
            {
                ThrowHelper.ThrowArgumentNullExceptionForIsNotNull<T?>(name);
            }
        }

        /// <summary>
        /// Asserts that the input value is of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the input value.</typeparam>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is not of type <typeparamref name="T"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsOfType<T>(object value, string name)
        {
            if (value.GetType() != typeof(T))
            {
                ThrowHelper.ThrowArgumentExceptionForIsOfType<T>(value, name);
            }
        }

        /// <summary>
        /// Asserts that the input value is not of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the input value.</typeparam>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is of type <typeparamref name="T"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotOfType<T>(object value, string name)
        {
            if (value.GetType() == typeof(T))
            {
                ThrowHelper.ThrowArgumentExceptionForIsNotOfType<T>(value, name);
            }
        }

        /// <summary>
        /// Asserts that the input value is of a specific type.
        /// </summary>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="type">The type to look for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if the type of <paramref name="value"/> is not the same as <paramref name="type"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsOfType(object value, Type type, string name)
        {
            if (value.GetType() != type)
            {
                ThrowHelper.ThrowArgumentExceptionForIsOfType(value, type, name);
            }
        }

        /// <summary>
        /// Asserts that the input value is not of a specific type.
        /// </summary>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="type">The type to look for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if the type of <paramref name="value"/> is the same as <paramref name="type"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotOfType(object value, Type type, string name)
        {
            if (value.GetType() == type)
            {
                ThrowHelper.ThrowArgumentExceptionForIsNotOfType(value, type, name);
            }
        }

        /// <summary>
        /// Asserts that the input value can be assigned to a specified type.
        /// </summary>
        /// <typeparam name="T">The type to check the input value against.</typeparam>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> can't be assigned to type <typeparamref name="T"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsAssignableToType<T>(object value, string name)
        {
            if (!(value is T))
            {
                ThrowHelper.ThrowArgumentExceptionForIsAssignableToType<T>(value, name);
            }
        }

        /// <summary>
        /// Asserts that the input value can't be assigned to a specified type.
        /// </summary>
        /// <typeparam name="T">The type to check the input value against.</typeparam>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> can be assigned to type <typeparamref name="T"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotAssignableToType<T>(object value, string name)
        {
            if (value is T)
            {
                ThrowHelper.ThrowArgumentExceptionForIsNotAssignableToType<T>(value, name);
            }
        }

        /// <summary>
        /// Asserts that the input value can be assigned to a specified type.
        /// </summary>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="type">The type to look for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> can't be assigned to <paramref name="type"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsAssignableToType(object value, Type type, string name)
        {
            if (!type.IsInstanceOfType(value))
            {
                ThrowHelper.ThrowArgumentExceptionForIsAssignableToType(value, type, name);
            }
        }

        /// <summary>
        /// Asserts that the input value can't be assigned to a specified type.
        /// </summary>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="type">The type to look for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> can be assigned to <paramref name="type"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotAssignableToType(object value, Type type, string name)
        {
            if (type.IsInstanceOfType(value))
            {
                ThrowHelper.ThrowArgumentExceptionForIsNotAssignableToType(value, type, name);
            }
        }

        /// <summary>
        /// Asserts that the input value must be the same instance as the target value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="target">The target <typeparamref name="T"/> value to test for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is not the same instance as <paramref name="target"/>.</exception>
        /// <remarks>The method is generic to prevent using it with value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsReferenceEqualTo<T>(T value, T target, string name)
            where T : class
        {
            if (!ReferenceEquals(value, target))
            {
                ThrowHelper.ThrowArgumentExceptionForIsReferenceEqualTo<T>(name);
            }
        }

        /// <summary>
        /// Asserts that the input value must not be the same instance as the target value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="target">The target <typeparamref name="T"/> value to test for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is the same instance as <paramref name="target"/>.</exception>
        /// <remarks>The method is generic to prevent using it with value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsReferenceNotEqualTo<T>(T value, T target, string name)
            where T : class
        {
            if (ReferenceEquals(value, target))
            {
                ThrowHelper.ThrowArgumentExceptionForIsReferenceNotEqualTo<T>(name);
            }
        }

        /// <summary>
        /// Asserts that the input value must be <see langword="true"/>.
        /// </summary>
        /// <param name="value">The input <see cref="bool"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <see langword="false"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsTrue(
#if NETSTANDARD2_1
            [DoesNotReturnIf(false)]
#endif
            bool value, string name)
        {
            if (!value)
            {
                ThrowHelper.ThrowArgumentExceptionForIsTrue(name);
            }
        }

        /// <summary>
        /// Asserts that the input value must be <see langword="false"/>.
        /// </summary>
        /// <param name="value">The input <see cref="bool"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <see langword="true"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsFalse(
#if NETSTANDARD2_1
            [DoesNotReturnIf(true)]
#endif
            bool value, string name)
        {
            if (value)
            {
                ThrowHelper.ThrowArgumentExceptionForIsFalse(name);
            }
        }
    }
}
