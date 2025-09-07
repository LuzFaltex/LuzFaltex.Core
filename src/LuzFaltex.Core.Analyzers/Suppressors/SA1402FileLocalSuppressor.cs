//
//  SA1402FileLocalSuppressor.cs
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
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace LuzFaltex.Core.Analyzers.Suppressors
{
    /// <summary>
    /// Suppresses instances of SA1402 when file-local types are used.
    /// </summary>
    /// <seealso href="https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3803"/>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class SA1402FileLocalSuppressor : DiagnosticSuppressor
    {
        /// <summary>
        /// Suppresor id for SA1402.
        /// </summary>
        public const string SuppressorId = "LFSA1402S";

        /// <summary>
        /// <list type="table">
        /// <item>
        ///     <term>TypeName</term>
        ///     <description>SA1402FileMayOnlyContainASingleType</description>
        /// </item>
        /// <item>
        ///     <term>CheckId</term>
        ///     <description>SA1402</description>
        /// </item>
        /// <item>
        ///     <term>Category</term>
        ///     <description>Maintainability Rules</description>
        /// </item>
        /// <item>
        ///     <term>Cause</term>
        ///     <description>A C# code file contains more than one unique type.</description>
        /// </item>
        /// </list>
        /// </summary>
        public const string SuppressedDiagnosticId = "SA1402";

        /// <summary>
        /// Gets the supporession descriptor for this instance.
        /// </summary>
        internal static readonly SuppressionDescriptor Suppressor = new
        (
            SuppressorId,
            SuppressedDiagnosticId,
            new LocalizableResourceString(nameof(SuppressorResources.LFSA1402Justification), SuppressorResources.ResourceManager, typeof(SuppressorResources))
        );

        /// <inheritdoc/>
        public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions => [Suppressor];

        /// <inheritdoc/>
        public override void ReportSuppressions(SuppressionAnalysisContext context)
        {
            foreach (var diagnostic in context.ReportedDiagnostics)
            {
                if (diagnostic.Location.SourceTree is not { } tree)
                {
                    continue;
                }

                var root = tree.GetRoot(context.CancellationToken);
                var node = root.FindNode(diagnostic.Location.SourceSpan);

                if (IsFileLocalType(node))
                {
                    context.ReportSuppression(Suppression.Create(Suppressor, diagnostic));
                }
            }

            static bool IsFileLocalType(SyntaxNode node)
            {
                SyntaxTokenList modifiers = node switch
                {
                    BaseTypeDeclarationSyntax x => x.Modifiers,
                    DelegateDeclarationSyntax x => x.Modifiers,
                    _ => default
                };

                return modifiers.Any(SyntaxKind.FileKeyword);
            }
        }
    }
}
