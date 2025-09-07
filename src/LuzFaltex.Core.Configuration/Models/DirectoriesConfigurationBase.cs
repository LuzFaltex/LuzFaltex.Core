//
//  DirectoriesConfigurationBase.cs
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
    /// Represents a configuration section named <see cref="ConfigurationConstants.Directories"/> used for setting directory paths.
    /// </summary>
    public abstract class DirectoriesConfigurationBase : IApplicationConfiguration
    {
        /// <inheritdoc/>
        public static string ConfigurationSectionName => ConfigurationConstants.Directories;

        /// <inheritdoc/>
        public static string GetFullyQualifiedSectionName() => ConfigurationSectionName;
    }
}
