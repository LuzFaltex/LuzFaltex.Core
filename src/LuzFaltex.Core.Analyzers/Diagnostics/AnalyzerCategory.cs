//
//  AnalyzerCategory.cs
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

namespace LuzFaltex.Core.Analyzers.Diagnostics
{
    /// <summary>
    /// Class defining the analyzer category contents.
    /// </summary>
    internal static class AnalyzerCategory
    {
        private const string RootNamespace = "LuzFaltex.Core.CSharp";

        /// <summary>
        /// Category definition for documentation rules.
        /// </summary>
        public const string DocumentationRules = $"{RootNamespace}.{nameof(DocumentationRules)}";

        /// <summary>
        /// Category definition for layout rules.
        /// </summary>
        public const string LayoutRules = $"{RootNamespace}.{nameof(LayoutRules)}";

        /// <summary>
        /// Category definition for Maintainability rules.
        /// </summary>
        public const string MaintainabilityRules = $"{RootNamespace}.{nameof(MaintainabilityRules)}";

        /// <summary>
        /// Category definition for Naming rules.
        /// </summary>
        public const string NamingRules = $"{RootNamespace}.{nameof(NamingRules)}";

        /// <summary>
        /// Category definition for Ordering rules.
        /// </summary>
        public const string OrderingRules = $"{RootNamespace}.{nameof(OrderingRules)}";

        /// <summary>
        /// Category definition for Readability rules.
        /// </summary>
        public const string ReadabilityRules = $"{RootNamespace}.{nameof(ReadabilityRules)}";

        /// <summary>
        /// Category definition for Spacing rules.
        /// </summary>
        public const string SpacingRules = $"{RootNamespace}.{nameof(SpacingRules)}";

        /// <summary>
        /// Category definition for Special rules.
        /// </summary>
        public const string SpecialRules = $"{RootNamespace}.{nameof(SpecialRules)}";
    }
}
