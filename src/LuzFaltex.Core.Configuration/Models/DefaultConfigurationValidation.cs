//
//  DefaultConfigurationValidation.cs
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

using System.Collections.Generic;
using Remora.Results;

namespace LuzFaltex.Core.Configuration.Models
{
    /// <summary>
    /// Provides a configuration validation that does nothing. Useful for when you do not need to provide validations.
    /// </summary>
    /// <typeparam name="TOptions">The type of options this configuration validates. Must be provided for DI purposes, even if it is unused.</typeparam>
    public sealed class DefaultConfigurationValidation<TOptions> : ConfigurationValidationBase<TOptions>
        where TOptions : class, IApplicationConfiguration
    {
        /// <inheritdoc/>
        public override IEnumerable<Result> PerformCustomValidations(string? name, TOptions configuration)
        {
            // Body intentionally left blank.
            return [];
        }
    }
}
