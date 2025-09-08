//
//  OptionsMonitorExtensions.cs
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
using Microsoft.Extensions.Options;

namespace LuzFaltex.Core.Configuration.Extensions
{
    /// <summary>
    /// Provides a set of extensions for <see cref="IOptionsMonitor{T}"/>.
    /// </summary>
    public static class OptionsMonitorExtensions
    {
        /// <summary>
        /// Returns a configured <typeparamref name="TConfiguration"/> instance using the fully qualified configuration section name.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the configuration instance to retrieve.</typeparam>
        /// <param name="optionsMonitor">The options monitor.</param>
        /// <returns>The <typeparamref name="TConfiguration"/> that matches the configuration section name.</returns>
        public static TConfiguration Get<TConfiguration>(this IOptionsMonitor<TConfiguration> optionsMonitor)
            where TConfiguration : class, IApplicationConfiguration
            => optionsMonitor.Get(TConfiguration.GetFullyQualifiedSectionName());
    }
}
