//
//  SectionNameHelpers.cs
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

using LuzFaltex.Core.Configuration.Models;

namespace LuzFaltex.Core.Configuration.Helpers
{
    /// <summary>
    /// Provides a set of helpers for retrieving the section name.
    /// </summary>
    public static class SectionNameHelpers
    {
        /// <summary>
        /// Gets the fully qualified section name with the specified <paramref name="parents"/>.
        /// </summary>
        /// <param name="sectionName">The configuration section name, typically <see cref="IApplicationConfiguration.ConfigurationSectionName"/>.</param>
        /// <param name="parents">An array containing the ordered list of parent objects.</param>
        /// <returns>A string with the value <c>parent1:parent2:...:<paramref name="sectionName"/></c>.</returns>
        public static string GetConfigurationSectionName(string sectionName, params string[] parents)
            => string.Join(':', [.. parents, sectionName]);
    }
}
