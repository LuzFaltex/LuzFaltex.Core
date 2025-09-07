//
//  LF1100ImplicitRecordClassDeclaration.cs
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

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace LuzFaltex.Core.Analyzers.Diagnostics.ReadabilityRules
{
    /// <summary>
    /// Provides an analyzer which flags implicit record class declarations.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class LF1100ImplicitRecordClassDeclaration : DiagnosticAnalyzer
    {
        /// <summary>
        /// Gets this analyzer's diagnostic id.
        /// </summary>
        public const string DiagnosticId = "LF1100";

        private static readonly DiagnosticDescriptor Rule = new
        (
            id: DiagnosticId,
            title: new LocalizableResourceString(nameof(ReadabilityResources.LF1100Title), ReadabilityResources.ResourceManager, typeof(ReadabilityResources)),
            messageFormat: new LocalizableResourceString(nameof(ReadabilityResources.LF1100MessageFormat), ReadabilityResources.ResourceManager, typeof(ReadabilityResources)),
            category: AnalyzerCategory.ReadabilityRules,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: new LocalizableResourceString(nameof(ReadabilityResources.LF1100Description), ReadabilityResources.ResourceManager, typeof(ReadabilityResources))
        );

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            // Do not use analyzer for generated code.
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

            // Allow processing multiple sites concurrently.
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.RecordDeclaration);

            static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
            {
                if (context.Compilation is not CSharpCompilation { LanguageVersion: >= LanguageVersion.CSharp10 })
                {
                    return;
                }

                // Target record but not record class
                if (context is { Node: RecordDeclarationSyntax { ClassOrStructKeyword.RawKind: (int)SyntaxKind.None } recordDeclaration, ContainingSymbol: INamedTypeSymbol })
                {
                    var location = recordDeclaration.Identifier.GetLocation();
                    var className = recordDeclaration.Identifier.ValueText;
                    var diagnostic = Diagnostic.Create(Rule, location, className);

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
