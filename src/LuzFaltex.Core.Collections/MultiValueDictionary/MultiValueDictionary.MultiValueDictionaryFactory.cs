//
//  MultiValueDictionary.MultiValueDictionaryFactory.cs
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

namespace LuzFaltex.Core.Collections
{
    public partial class MultiValueDictionary<TKey, TValue> where TKey : notnull
    {
        /// <summary>
        /// Gets a factory method for this instance.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection.</typeparam>
        public sealed class MultiValueDictionaryFactory<TValueCollection>
            where TValueCollection : ICollection<TValue>, new()
        {
            /// <summary>
            /// Gets or sets the initial capacity.
            /// </summary>
            /// <remarks>If the value is negative, the default capacity will be used.</remarks>
            public int Capacity { get; set; } = -1;

            /// <summary>
            /// Gets or sets the equality comparer to use to compare <typeparamref name="TKey"/>s.
            /// </summary>
            public IEqualityComparer<TKey>? Comparer { get; set; } = default;

            /// <summary>
            /// Gets or sets the values which will be added to the internal dictionary.
            /// </summary>
            public IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> Values { get; set; } = [];

            /// <summary>
            /// Gets or sets the collection factory.
            /// </summary>
            public Func<TValueCollection> CollectionFactory { get; set; } = () => [];

            /// <summary>
            /// Creates a new <see cref="MultiValueDictionary{TKey, TValue}"/> with the provided values.
            /// </summary>
            /// <returns>The newly constructed <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
            /// <exception cref="InvalidOperationException">Thrown if the provided <typeparamref name="TValueCollection"/> is readonly.</exception>
            public MultiValueDictionary<TKey, TValue> Build()
            {
                MultiValueDictionary<TKey, TValue> mvd = Capacity < 0
                    ? new MultiValueDictionary<TKey, TValue>(Comparer) { _newCollectionFactory = (Func<ICollection<TValue>>)(object)CollectionFactory }
                    : new MultiValueDictionary<TKey, TValue>(Capacity, Comparer) { _newCollectionFactory = (Func<ICollection<TValue>>)(object)CollectionFactory };

                if (CollectionFactory().IsReadOnly)
                {
                    throw new InvalidOperationException("Collection type must not be readonly.");
                }

                foreach (KeyValuePair<TKey, IReadOnlyCollection<TValue>> item in Values)
                {
                    mvd.AddRange(item.Key, item.Value);
                }

                return mvd;
            }
        }
    }
}
