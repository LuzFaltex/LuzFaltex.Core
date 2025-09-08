//
//  TestSectionBase.cs
//
//  Author:
//       LuzFaltex Contributors <support@luzfaltex.com>
//
//  Copyright (c) LuzFaltex, LLC.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LuzFaltex.Core.Collections.Tests
{
    /// <summary>
    /// Describes a test section.
    /// </summary>
    public abstract class TestSectionBase
    {
        /// <summary>
        /// Gets the binding flags for all methods.
        /// </summary>
        public const BindingFlags Everything = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;

        /// <summary>
        /// A generic method to retrieve the specified method and genericize it.
        /// </summary>
        /// <typeparam name="TClass">The type of the wrapping class.</typeparam>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="types">The collection of type parameters.</param>
        /// <returns>The <see cref="MethodInfo"/>, if found; otherwise, <see langword="null"/>.</returns>
        public virtual MethodInfo? GetGenericMethod<TClass>([ConstantExpected] string methodName, params Type[] types)
            => typeof(TClass).GetMethod(methodName, Everything)?.MakeGenericMethod(types);
    }
}
