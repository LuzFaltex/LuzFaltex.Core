//
//  TestHelpers.cs
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

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace LuzFaltex.Core.Analyzers.Tests
{
    /// <summary>
    /// Provides a set of test helpers.
    /// </summary>
    public static class TestHelpers
    {
        /// <summary>
        /// Gets the name of inline C# code syntax.
        /// </summary>
        public const string CSharpTest = "c#-test";

        /// <summary>
        /// Gets an implicit record class declaration for testing.
        /// </summary>
        [StringSyntax(CSharpTest)]
        public const string ImplicitRecordClassDeclaration = "public sealed record [|Target|] { public required int Id { get; init; } }";

        /// <summary>
        /// Gets an explicit record class declaration for testing.
        /// </summary>
        [StringSyntax(CSharpTest)]
        public const string ExplicitRecordClassDeclaration = "public sealed record class Target { public required int Id { get; init; } }";

        /// <summary>
        /// Gets the <see cref="ReferenceAssemblies"/> to use for tests.
        /// </summary>
        public static ReferenceAssemblies ReferenceAssemblies
#if NET8_0
            => ReferenceAssemblies.Net.Net80;
#elif NET9_0
            => ReferenceAssemblies.Net.Net90;
#endif

        /// <summary>
        /// Creates a test for the specified <typeparamref name="TAnalyzer"/>.
        /// </summary>
        /// <typeparam name="TAnalyzer">The analyzer to test.</typeparam>
        /// <param name="inputSource">The input source code.</param>
        /// <returns>A test for the analyzer.</returns>
        public static CSharpAnalyzerTest<TAnalyzer, DefaultVerifier> CreateAnalyzerTest<TAnalyzer>
        (
            [StringSyntax(CSharpTest)] string inputSource
        )
            where TAnalyzer : DiagnosticAnalyzer, new()
        {
            return new CSharpAnalyzerTest<TAnalyzer, DefaultVerifier>
            {
                TestState =
                {
                    Sources = { inputSource },
                    ReferenceAssemblies = ReferenceAssemblies
                }
            };
        }
    }
}
