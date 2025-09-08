//
//  PathRootAttribute.cs
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

namespace LuzFaltex.Core.Configuration.Attributes
{
    /// <summary>
    /// Denotes the root path of a potentially relative configuration option.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class PathRootAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the property or field containing the root path.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathRootAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the property or field containing the root path.</param>
        public PathRootAttribute(string name)
        {
#if NET8_0_OR_GREATER
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
#else
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value must not be null, empty, or comprised entirely of white space characters.", nameof(name));
            }
#endif
            Name = name;
        }
    }
}
