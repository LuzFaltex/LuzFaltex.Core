//
//  LF1100ImplicitRecordClassDeclarationTests.cs
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

using System.Threading.Tasks;
using LuzFaltex.Core.Analyzers.Diagnostics.ReadabilityRules;

namespace LuzFaltex.Core.Analyzers.Tests.AnalyzerTests
{
    /// <summary>
    /// Provides a set of tests for <see cref="LF1100ImplicitRecordClassDeclaration"/>.
    /// </summary>
    public sealed class LF1100ImplicitRecordClassDeclarationTests
    {
        /// <summary>
        /// Warns if the class declaration of a record class is defined implicitly.
        /// </summary>
        /// <returns>A task represnting this operation.</returns>
        [Fact]
        public Task RecordClassWithoutClassDeclarationWarns()
            => TestHelpers.CreateAnalyzerTest<LF1100ImplicitRecordClassDeclaration>(TestHelpers.ImplicitRecordClassDeclaration).RunAsync();

        /// <summary>
        /// Does not warn if the class declaration of a record class is defined explicitly.
        /// </summary>
        /// <returns>A task represnting this operation.</returns>
        [Fact]
        public Task RecordClassWithClassDeclarationDoesNotWarn()
            => TestHelpers.CreateAnalyzerTest<LF1100ImplicitRecordClassDeclaration>(TestHelpers.ExplicitRecordClassDeclaration).RunAsync();
    }
}
