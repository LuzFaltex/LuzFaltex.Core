//
//  IApplicationConfiguration.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuzFaltex.Core.Configuration.Models
{
    /// <summary>
    /// A marker interface for types which represent a configuration section.
    /// </summary>
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// Gets the name of this configuration section.
        /// </summary>
        static abstract string ConfigurationSectionName { get; }

        /// <summary>
        /// Gets a fully-qualified, colon-delimited path to the configuration section.
        /// </summary>
        /// <returns>The full path to the configuration section.</returns>
        static abstract string GetFullyQualifiedSectionName();
    }
}
