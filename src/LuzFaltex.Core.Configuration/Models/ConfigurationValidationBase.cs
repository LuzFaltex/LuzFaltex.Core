//
//  ConfigurationValidationBase.cs
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
using Microsoft.Extensions.Options;
using Remora.Results;

namespace LuzFaltex.Core.Configuration.Models
{
    /// <summary>
    /// Provides base functionality for <see cref="IValidateOptions{TOptions}"/> instances.
    /// </summary>
    /// <typeparam name="TConfiguration">The underlying configuration model.</typeparam>
    public abstract class ConfigurationValidationBase<TConfiguration> : IValidateOptions<TConfiguration>
        where TConfiguration : class, IApplicationConfiguration
    {
        /// <inheritdoc/>
        public ValidateOptionsResult Validate(string? name, TConfiguration options)
        {
            // 1. Perform validations based on attributes.
            // 2. Perform custom validations.
            // 3. Return error count.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates the provided model. Yield return any validation errors.
        /// </summary>
        /// <inheritdoc cref="IValidateOptions{TOptions}.Validate(string?, TOptions)"/>
        public virtual IEnumerable<Result> PerformCustomValidations(string? name, TConfiguration configuration) => [];
    }
}
