//
//  ConfigurationConstants.cs
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

namespace LuzFaltex.Core.Configuration
{
    /// <summary>
    /// Provides a set of well-known constants.
    /// </summary>
    public static class ConfigurationConstants
    {
        /// <summary>
        /// Gets the name of the <see cref="ApplicationConfiguration"/> configuration section.
        /// </summary>
        public static readonly string ApplicationConfiguration = nameof(ApplicationConfiguration);

        /// <summary>
        /// Gets the name of the <see cref="ConnectionStrings"/> configuration section.
        /// </summary>
        public static readonly string ConnectionStrings = nameof(ConnectionStrings);

        /// <summary>
        /// Gets the name of the <see cref="Directories"/> configuration section.
        /// </summary>
        public static readonly string Directories = nameof(Directories);

        /// <summary>
        /// Gets the name of the <see cref="Environment"/> configuration section.
        /// </summary>
        public static readonly string Environment = nameof(Environment);
    }
}
