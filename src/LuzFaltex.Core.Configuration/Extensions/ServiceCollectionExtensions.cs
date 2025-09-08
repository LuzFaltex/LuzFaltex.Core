//
//  ServiceCollectionExtensions.cs
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
using LuzFaltex.Core.Configuration.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace LuzFaltex.Core.Configuration.Extensions
{
    /// <summary>
    /// Provides a set of extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Binds the specified <typeparamref name="TConfiguration"/> to the provided <paramref name="configurationSectionName"/> or the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.
        /// If the section is not found, an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the configuration model.</typeparam>
        /// <param name="builder">The host application builder which has the configuration and service provider.</param>
        /// <param name="configurationSectionName">The name of the section or, if <see langword="null"/>, the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.</param>
        /// <param name="name">The name of the configuration instance. If <see langword="null"/>, defaults to the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> for further customization.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the configuration section could not be found.</exception>
        public static OptionsBuilder<TConfiguration> AddRequiredApplicationOptions<TConfiguration>
        (
            this IHostApplicationBuilder builder,
            string? configurationSectionName = null,
            string? name = null
        )
            where TConfiguration : class, IApplicationConfiguration
            => RegisterApplicationOptions<TConfiguration, DefaultConfigurationValidation<TConfiguration>>
            (
                builder,
                builder.Configuration.GetRequiredSection(configurationSectionName ?? TConfiguration.GetFullyQualifiedSectionName()),
                name
            );

        /// <summary>
        /// Binds the specified <typeparamref name="TConfiguration"/> to the provided <paramref name="configurationSectionName"/> or the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the configuration model.</typeparam>
        /// <param name="builder">The host application builder which has the configuration and service provider.</param>
        /// <param name="configurationSectionName">The name of the section or, if <see langword="null"/>, the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.</param>
        /// <param name="name">The name of the configuration instance. If <see langword="null"/>, defaults to the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> for further customization.</returns>
        public static OptionsBuilder<TConfiguration> AddOptionalApplicationOptions<TConfiguration>
        (
            this IHostApplicationBuilder builder,
            string? configurationSectionName = null,
            string? name = null
        )
            where TConfiguration : class, IApplicationConfiguration
            => RegisterApplicationOptions<TConfiguration, DefaultConfigurationValidation<TConfiguration>>
            (
                builder,
                builder.Configuration.GetSection(configurationSectionName ?? TConfiguration.GetFullyQualifiedSectionName()),
                name
            );

        /// <summary>
        /// Binds the specified <typeparamref name="TConfiguration"/> to the provided <paramref name="configurationSectionName"/> or the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.
        /// If the section is not found, an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the configuration model.</typeparam>
        /// <typeparam name="TValidateOptions">The configuration validation type.</typeparam>
        /// <param name="builder">The host application builder which has the configuration and service provider.</param>
        /// <param name="configurationSectionName">The name of the section or, if <see langword="null"/>, the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.</param>
        /// <param name="name">The name of the configuration instance. If <see langword="null"/>, defaults to the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> for further customization.</returns>
        public static OptionsBuilder<TConfiguration> AddRequiredApplicationOptions<TConfiguration, TValidateOptions>
        (
            this IHostApplicationBuilder builder,
            string? configurationSectionName = null,
            string? name = null
        )
            where TConfiguration : class, IApplicationConfiguration
            where TValidateOptions : ConfigurationValidationBase<TConfiguration>
            => RegisterApplicationOptions<TConfiguration, TValidateOptions>
            (
                builder,
                builder.Configuration.GetRequiredSection(configurationSectionName ?? TConfiguration.GetFullyQualifiedSectionName()),
                name
            );

        /// <summary>
        /// Binds the specified <typeparamref name="TConfiguration"/> to the provided <paramref name="configurationSectionName"/> or the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.
        /// If the section is not found, an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the configuration model.</typeparam>
        /// <typeparam name="TValidateOptions">The configuration validation type.</typeparam>
        /// <param name="builder">The host application builder which has the configuration and service provider.</param>
        /// <param name="configurationSectionName">The name of the section or, if <see langword="null"/>, the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.</param>
        /// <param name="name">The name of the configuration instance. If <see langword="null"/>, defaults to the value of <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName"/>.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> for further customization.</returns>
        public static OptionsBuilder<TConfiguration> AddOptionalApplicationOptions<TConfiguration, TValidateOptions>
        (
            this IHostApplicationBuilder builder,
            string? configurationSectionName = null,
            string? name = null
        )
            where TConfiguration : class, IApplicationConfiguration
            where TValidateOptions : ConfigurationValidationBase<TConfiguration>
            => RegisterApplicationOptions<TConfiguration, TValidateOptions>
            (
                builder,
                builder.Configuration.GetSection(configurationSectionName ?? TConfiguration.GetFullyQualifiedSectionName()),
                name
            );

        /// <summary>
        /// Registers the specified <typeparamref name="TConfiguration"/> and binds the provided <paramref name="configurationSection"/>.
        /// </summary>
        /// <typeparam name="TConfiguration">The underlying type of the configuration model.</typeparam>
        /// <typeparam name="TValidateOptions">The underlying type of the validation options.</typeparam>
        /// <param name="builder">The host application builder.</param>
        /// <param name="configurationSection">The configuration section to bind against.</param>
        /// <param name="name">The name for this configuration instance. If <see langword="null"/>, defaults to <see cref="IApplicationConfiguration.GetFullyQualifiedSectionName()"/>.</param>
        /// <returns>An <see cref="OptionsBuilder{TOptions}"/> for further configuration.</returns>
        public static OptionsBuilder<TConfiguration> RegisterApplicationOptions<TConfiguration, TValidateOptions>
        (
            IHostApplicationBuilder builder,
            IConfigurationSection configurationSection,
            string? name = null
        )
            where TConfiguration : class, IApplicationConfiguration
            where TValidateOptions : ConfigurationValidationBase<TConfiguration>
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(configurationSection);
            name ??= TConfiguration.GetFullyQualifiedSectionName();

            return builder.Services.AddOptionsWithValidateOnStart<TConfiguration, TValidateOptions>(name).Bind(configurationSection);
        }

        /// <summary>
        /// Uses the environment name specified by the <see cref="ConfigurationConstants.Environment"/> setting in appsettings to override the current environment at startup.
        /// </summary>
        /// <remarks>
        /// This should only be used in environments where setting the <c>DOTNET_ENVIRONMENT</c> is not available.
        /// </remarks>
        /// <typeparam name="TApplicationBuilder">The type of the application builder.</typeparam>
        /// <param name="builder">The builder to modify.</param>
        /// <returns>The current builder, for chaining.</returns>
        public static TApplicationBuilder UseEnvironmentFromAppSettings<TApplicationBuilder>(this TApplicationBuilder builder)
            where TApplicationBuilder : IHostApplicationBuilder
        {
            string? appSettingsEnvironmentName = builder.Configuration.GetValue<string?>(ConfigurationConstants.Environment);

            if (appSettingsEnvironmentName is not null)
            {
                builder.Environment.EnvironmentName = appSettingsEnvironmentName;
            }

            return builder;
        }

#pragma warning disable SA1600, CS1591
        // TODO: Change this method to accept a parameter differentiating between scoped, singleton, and transient.
        // Also consider keyed.
        public static IServiceCollection AddScopedOptional<TInterface, TLeft, TRight>
        (
            this IServiceCollection services,
            IConfiguration configuration,
            string configurationSectionName,
            Func<IConfigurationSection, ScopedOptionalChoice> accessor
        )
            where TInterface : class
            where TLeft : class, TInterface
            where TRight : class, TInterface
        {
            IConfigurationSection section = configuration.GetRequiredSection(configurationSectionName);

            ScopedOptionalChoice choice = accessor.Invoke(section);

            return choice switch
            {
                ScopedOptionalChoice.Left => services.AddScoped<TInterface, TLeft>(),
                ScopedOptionalChoice.Right => services.AddScoped<TInterface, TRight>(),
                _ => throw new ArgumentOutOfRangeException(nameof(accessor), choice, "Enum argument out of range.")
            };
        }
    }
#pragma warning restore SA1600, CS1591
}
