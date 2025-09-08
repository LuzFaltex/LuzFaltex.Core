//
//  OptionsBuilderExtensions.cs
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
using System.IO;
using System.Linq;
using System.Reflection;
using LuzFaltex.Core.Configuration.Attributes;
using LuzFaltex.Core.Configuration.Models;
using Microsoft.Extensions.Options;

namespace LuzFaltex.Core.Configuration.Extensions
{
    /// <summary>
    /// Provides a set of extensions for <see cref="OptionsBuilder{TOptions}"/>.
    /// </summary>
    public static class OptionsBuilderExtensions
    {
        /// <summary>
        /// Scans the provided <typeparamref name="TFolderConfiguration"/> for public properties or fields annotated with <see cref="PathRootAttribute"/> and updates them to have a fully qualified path.
        /// If the path is fully qualified, <see cref="Path.GetFullPath(string)"/> is called to process navigation marks (e.g. <c>..</c>).
        /// </summary>
        /// <typeparam name="TFolderConfiguration">The <see cref="FolderConfigurationBase"/> implementation.</typeparam>
        /// <param name="optionsBuilder">The builder to process.</param>
        /// <returns>The <paramref name="optionsBuilder"/>, for chaining.</returns>
        public static OptionsBuilder<TFolderConfiguration> EnsurePathsAreFullyQualified<TFolderConfiguration>(this OptionsBuilder<TFolderConfiguration> optionsBuilder)
            where TFolderConfiguration : FolderConfigurationBase
            => optionsBuilder.Configure(ProcessPaths);

        private static void ProcessPaths<TFolderConfiguration>(TFolderConfiguration folders)
            where TFolderConfiguration : FolderConfigurationBase
        {
            var properties = typeof(TFolderConfiguration).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var fields = typeof(TFolderConfiguration).GetFields(BindingFlags.Instance | BindingFlags.Public);
            MemberInfo[] members = [.. properties.Cast<MemberInfo>(), .. fields.Cast<MemberInfo>()];

            foreach (var member in members)
            {
                // We only care about annotated members.
                if (member.GetCustomAttribute<PathRootAttribute>() is not { } rootAttribute)
                {
                    continue;
                }

                // Search all members for the root folder path.
                MemberInfo[] matchingMembers = typeof(TFolderConfiguration).GetMember(rootAttribute.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

                string rootPath = string.Empty;
                MemberInfo? rootInfo = matchingMembers.FirstOrDefault();

                if (rootInfo is PropertyInfo rootPathProperty)
                {
                    rootPath = (string?)rootPathProperty.GetValue(folders) ?? string.Empty;
                }
                else if (rootInfo is FieldInfo rootPathField)
                {
                    rootPath = rootPathField is { IsLiteral: true, IsInitOnly: true }
                        ? (string?)rootPathField.GetRawConstantValue() ?? string.Empty
                        : (string?)rootPathField.GetValue(folders) ?? string.Empty;
                }
                else
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(rootPath) || !Path.IsPathRooted(rootPath))
                {
                    continue;
                }

                if (member is PropertyInfo p)
                {
                    if (!EnsureValidProperty(p, out MethodInfo? propertySetMethod))
                    {
                        continue;
                    }

                    string? value = (string?)p.GetValue(folders);
                    SetValue(rootPath, value, folders, propertySetMethod.Invoke);
                }
                else if (member is FieldInfo f)
                {
                    if (!EnsureValidField(f, out Func<object?, object?[]?, object?>? fieldSetMethod))
                    {
                        continue;
                    }

                    string? value = (string?)f.GetValue(folders);
                    SetValue(rootPath, value, folders, fieldSetMethod);
                }
            }
        }

        private static bool EnsureValidProperty(PropertyInfo property, [NotNullWhen(true)] out MethodInfo? setMethod)
        {
            setMethod = null;

            if (property.PropertyType != typeof(string))
            {
                return false;
            }

            // If the property doesn't have a setter, skip.
            if (property.GetSetMethod(nonPublic: true) is not { } setter)
            {
                return false;
            }

            setMethod = setter;
            return true;
        }

        private static bool EnsureValidField(FieldInfo field, [NotNullWhen(true)] out Func<object?, object?[]?, object?>? setAction)
        {
            setAction = null;

            if (field.FieldType != typeof(string))
            {
                return false;
            }

            if (field.IsInitOnly)
            {
                return false;
            }

            setAction = (instance, args) =>
            {
                field.SetValue(instance, args);
                return null;
            };
            return true;
        }

        private static void SetValue<TInstance>(string rootPath, string? value, TInstance instance, Func<object?, object?[]?, object?> setAction)
        {
            if (string.IsNullOrWhiteSpace(value) || value.IndexOfAny(Path.GetInvalidPathChars()) > -1)
            {
                return;
            }

            if (Path.IsPathFullyQualified(value))
            {
                // Process any navigation operations.
                // C:\Foo\Bar\..\Bizz\ evaluates to C:\Foo\Bizz\
                setAction.Invoke(instance, [Path.GetFullPath(value)]);
                return;
            }

            setAction.Invoke(instance, [Path.GetFullPath(value, rootPath)]);
        }
    }
}
