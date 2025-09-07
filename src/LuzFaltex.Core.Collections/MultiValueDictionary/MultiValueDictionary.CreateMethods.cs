//
//  MultiValueDictionary.CreateMethods.cs
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
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>()
            where TValueCollection : ICollection<TValue>, new()
            => Factory<TValueCollection>().Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="collectionFactory">A factory method for constructing and configuring the underlying <typeparamref name="TValueCollection"/>.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(Func<TValueCollection> collectionFactory)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                CollectionFactory = collectionFactory
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(int capacity)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Capacity = capacity
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="collectionFactory">A factory method for constructing and configuring the underlying <typeparamref name="TValueCollection"/>.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(int capacity, Func<TValueCollection> collectionFactory)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Capacity = capacity,
                CollectionFactory = collectionFactory
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="comparer">The value comparer to use for <typeparamref name="TKey"/>.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEqualityComparer<TKey>? comparer)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Comparer = comparer
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="comparer">The value comparer to use for <typeparamref name="TKey"/>.</param>
        /// <param name="collectionFactory">A factory method for constructing and configuring the underlying <typeparamref name="TValueCollection"/>.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEqualityComparer<TKey>? comparer, Func<TValueCollection> collectionFactory)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Comparer = comparer,
                CollectionFactory = collectionFactory
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="comparer">The value comparer to use for <typeparamref name="TKey"/>.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(int capacity, IEqualityComparer<TKey>? comparer)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Capacity = capacity,
                Comparer = comparer
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="comparer">The value comparer to use for <typeparamref name="TKey"/>.</param>
        /// <param name="collectionFactory">A factory method for constructing and configuring the underlying <typeparamref name="TValueCollection"/>.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(int capacity, IEqualityComparer<TKey>? comparer, Func<TValueCollection> collectionFactory)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Capacity = capacity,
                Comparer = comparer,
                CollectionFactory = collectionFactory
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="values">The enumerable to contianing the items to add to the collection.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> values)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Values = values
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="values">The enumerable to contianing the items to add to the collection.</param>
        /// <param name="comparer">The value comparer to use for <typeparamref name="TKey"/>.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> values, IEqualityComparer<TKey>? comparer)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Values = values,
                Comparer = comparer
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="values">The enumerable to contianing the items to add to the collection.</param>
        /// <param name="collectionFactory">A factory method for constructing and configuring the underlying <typeparamref name="TValueCollection"/>.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> values, Func<TValueCollection> collectionFactory)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Values = values,
                CollectionFactory = collectionFactory
            }.Build();

        /// <summary>
        /// Creates a new empty MultiValueDictionary using the default <see cref="Factory{TValueCollection}()"/>.
        /// </summary>
        /// <typeparam name="TValueCollection">The type of the value collection to use.</typeparam>
        /// <param name="values">The enumerable to contianing the items to add to the collection.</param>
        /// <param name="comparer">The value comparer to use for <typeparamref name="TKey"/>.</param>
        /// <param name="collectionFactory">A factory method for constructing and configuring the underlying <typeparamref name="TValueCollection"/>.</param>
        /// <returns>A <see cref="MultiValueDictionary{TKey, TValue}"/>.</returns>
        public static MultiValueDictionary<TKey, TValue> Create<TValueCollection>(IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> values, IEqualityComparer<TKey>? comparer, Func<TValueCollection> collectionFactory)
            where TValueCollection : ICollection<TValue>, new()
            => new MultiValueDictionaryFactory<TValueCollection>
            {
                Values = values,
                Comparer = comparer,
                CollectionFactory = collectionFactory
            }.Build();
    }
}
