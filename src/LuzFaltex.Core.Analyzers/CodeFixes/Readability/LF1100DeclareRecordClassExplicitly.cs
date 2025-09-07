//
//  LF1100DeclareRecordClassExplicitly.cs
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LuzFaltex.Core.Analyzers.Diagnostics.ReadabilityRules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LuzFaltex.Core.Analyzers.CodeFixes.Readability
{
    /// <summary>
    /// Provides a code fix provider which adds the <see langword="class"/> keyword to implicitly declared record classes.
    /// </summary>
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(LF1100DeclareRecordClassExplicitly))]
    internal sealed class LF1100DeclareRecordClassExplicitly : CodeFixProvider
    {
        private const string Title = "Add explicit class keyword to record declaration.";

        /// <inheritdoc/>
        public override ImmutableArray<string> FixableDiagnosticIds => [LF1100ImplicitRecordClassDeclaration.DiagnosticId];

        /// <inheritdoc/>
        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync();

            if (root is null)
            {
                return;
            }

            var node = root.FindNode(context.Span);
            var declaration = (RecordDeclarationSyntax)node;

            var diagnostic = context.Diagnostics.First();

            var action = CodeAction.Create(Title, token => AddClassKeyword(context.Document, root, declaration, token), diagnostic.Id);

            context.RegisterCodeFix(action, diagnostic);

            static Task<Document> AddClassKeyword(Document oldDocument, SyntaxNode oldRoot, RecordDeclarationSyntax oldNode, CancellationToken cancellationToken = default)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var token = SyntaxFactory.Token(SyntaxKind.ClassKeyword);
                var newNode = oldNode.WithClassOrStructKeyword(token);
                var newRoot = oldRoot.ReplaceNode(oldNode, newNode);
                var newDocument = oldDocument.WithSyntaxRoot(newRoot);
                return Task.FromResult(newDocument);
            }
        }

        /// <inheritdoc/>
        public override FixAllProvider? GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }
    }
}
