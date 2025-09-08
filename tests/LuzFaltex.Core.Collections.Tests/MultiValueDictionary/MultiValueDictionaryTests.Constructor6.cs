//
//  MultiValueDictionaryTests.Constructor6.cs
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
using System.Linq;
using Applications.Core.Common.Collections;
using LuzFaltex.Collections.Generic.Tests;
using static Applications.Core.Common.Tests.Collections.MultiValueDictionaryTests.TestData;

#pragma warning disable SA1600 // Elements must be documented

namespace Applications.Core.Common.Tests.Collections
{
    public partial class MultiValueDictionaryTests
    {
        /// <summary>
        /// Provides tests for the following methods:
        /// <list type="bullet">
        /// <item><see cref="MultiValueDictionary{TKey, TValue}.MultiValueDictionary(IEnumerable{KeyValuePair{TKey, IReadOnlyCollection{TValue}}}, IEqualityComparer{TKey}?)"/></item>
        /// <item><see cref="MultiValueDictionary{TKey, TValue}.Create{TValueCollection}(IEnumerable{KeyValuePair{TKey, IReadOnlyCollection{TValue}}}, IEqualityComparer{TKey}?)"/></item>
        /// <item><see cref="MultiValueDictionary{TKey, TValue}.Create{TValueCollection}(IEnumerable{KeyValuePair{TKey, IReadOnlyCollection{TValue}}}, IEqualityComparer{TKey}?, Func{TValueCollection})"/></item>
        /// </list>
        /// </summary>
        public sealed class Constructor6 : TestSectionBase
        {
            private readonly TypeBuilder _typeBuilder = new();

            [Theory]
            [ClassData(typeof(TypeDataWithFactory))]
            public void EmptyIEnumerable_Constructor(Type key, Type value, bool useFactory)
                => GetGenericMethod<Constructor5>(nameof(MVD_Constructor6_EmptyIEnumerable), key, value, ListOfT(value))?.Invoke(this, [true, useFactory]);

            [Theory]
            [ClassData(typeof(TypeDataWithFactory))]
            public void NullIEnumerable_Constructor(Type key, Type value, bool useFactory)
                => GetGenericMethod<Constructor5>(nameof(MVD_Constructor6_NullIEnumerable), key, value, ListOfT(value))?.Invoke(this, [true, useFactory]);

            [Theory]
            [ClassData(typeof(TypeDataWithFactory))]
            public void NonEmptyIEnumerable_Constructor(Type key, Type value, bool useFactory)
                => GetGenericMethod<Constructor5>(nameof(MVD_Constructor6_NonEmptyIEnumerable), key, value, ListOfT(value))?.Invoke(this, [true, useFactory]);

            [Theory]
            [ClassData(typeof(TypeDataWithFactory))]
            public void ValueCopyIEnumerable_Constructor(Type key, Type value, bool useFactory)
                => GetGenericMethod<Constructor5>(nameof(MVD_Constructor6_ValueCopyIEnumerable), key, value, ListOfT(value))?.Invoke(this, [true, useFactory, false]);

            [Theory]
            [ClassData(typeof(CreateTypeDataWithFactory))]
            public void EmptyIEnumerable_Create(Type key, Type value, Type valueCollection, bool useFactory)
                => GetGenericMethod<Constructor5>(nameof(MVD_Constructor6_EmptyIEnumerable), key, value, valueCollection)?.Invoke(this, [false, useFactory]);

            [Theory]
            [ClassData(typeof(CreateTypeDataWithFactory))]
            public void NullIEnumerable_Create(Type key, Type value, Type valueCollection, bool useFactory)
                => GetGenericMethod<Constructor5>(nameof(MVD_Constructor6_NullIEnumerable), key, value, valueCollection)?.Invoke(this, [false, useFactory]);

            [Theory]
            [ClassData(typeof(CreateTypeDataWithFactory))]
            public void NonEmptyIEnumerable_Create(Type key, Type value, Type valueCollection, bool useFactory)
                => GetGenericMethod<Constructor5>(nameof(MVD_Constructor6_NonEmptyIEnumerable), key, value, valueCollection)?.Invoke(this, [false, useFactory]);

            [Theory]
            [ClassData(typeof(CreateTypeDataWithFactory))]
            public void ValueCopyIEnumerable_Create(Type key, Type value, Type valueCollection, bool useFactory)
                => GetGenericMethod<Constructor5>(nameof(MVD_Constructor6_ValueCopyIEnumerable), key, value, valueCollection)?.Invoke(this, [false, useFactory, false]);

            [Theory]
            [ClassData(typeof(TypeData))]
            public void InvalidTCollection(Type tkey, Type tvalue)
                => GetGenericMethod<Constructor2>(nameof(MVD_Constructor6_InvalidTCollection), tkey, tvalue)?.Invoke(null, []);

            private void MVD_Constructor6_EmptyIEnumerable<TKey, TValue, TValueCollection>(bool useConstructor, bool useFactory)
                where TKey : notnull
                where TValueCollection : ICollection<TValue>, new()
                => MVD_Constructor6_IEnumerable<TKey, TValue, TValueCollection>(useConstructor, useFactory, [], false);

            private static void MVD_Constructor6_NullIEnumerable<TKey, TValue, TValueCollection>(bool useConstructor, bool useFactory)
                where TKey : notnull
                where TValueCollection : ICollection<TValue>, new()
            {
                IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>>? enumerable = null;

                if (useConstructor)
                {
                    Assert.Throws<ArgumentNullException>(() => _ = new MultiValueDictionary<TKey, TValue>(enumerable!, null));
                }
                else
                {
                    Assert.Throws<NullReferenceException>(() =>
                    {
                        if (useFactory)
                        {
                            _ = MultiValueDictionary<TKey, TValue>.Create<TValueCollection>(enumerable!, null, () => []);
                        }
                        else
                        {
                            _ = MultiValueDictionary<TKey, TValue>.Create<TValueCollection>(enumerable!, comparer: null);
                        }
                    });
                }
            }

            private void MVD_Constructor6_NonEmptyIEnumerable<TKey, TValue, TValueCollection>(bool useConstructor, bool useFactory)
                where TKey : notnull
                where TValueCollection : ICollection<TValue>, new()
                => MVD_Constructor6_IEnumerable<TKey, TValue, TValueCollection>(useConstructor, useFactory, Helpers.Multi<TKey, TValue, TValueCollection>(_typeBuilder)[1], false);

            private void MVD_Constructor6_IEnumerable<TKey, TValue, TValueCollection>(bool useConstructor, bool useFactory, MultiValueDictionary<TKey, TValue> enumerable, bool areEqual)
                where TKey : notnull
                where TValueCollection : ICollection<TValue>, new()
            {
                MultiValueDictionary<TKey, TValue> mvd;

                if (useConstructor)
                {
                    mvd = new MultiValueDictionary<TKey, TValue>(enumerable, null);
                }
                else if (useFactory)
                {
                    mvd = MultiValueDictionary<TKey, TValue>.Create<TValueCollection>(enumerable, null, () => []);
                }
                else
                {
                    mvd = MultiValueDictionary<TKey, TValue>.Create<TValueCollection>(enumerable, comparer: null);
                }

                CompareMVDs(enumerable, mvd);

                TKey key = _typeBuilder.GetNext(mvd.Keys.ToArray());
                TValue value = _typeBuilder.GetNext<TValue>();

                mvd.Add(key, value);

                CompareEnumerables(enumerable, mvd, areEqual);
                Assert.Equal(Helpers.PairsCount(enumerable) + 1, Helpers.PairsCount(mvd));
            }

            private void MVD_Constructor6_ValueCopyIEnumerable<TKey, TValue, TValueCollection>(bool useConstructor, bool useFactory, bool areEqual)
                where TKey : notnull
                where TValueCollection : ICollection<TValue>, new()
            {
                MultiValueDictionary<TKey, TValue> enumerable = Helpers.Multi<TKey, TValue, TValueCollection>(_typeBuilder)[1];
                MultiValueDictionary<TKey, TValue> mvd;

                if (useConstructor)
                {
                    mvd = new MultiValueDictionary<TKey, TValue>(enumerable, null);
                }
                else if (useFactory)
                {
                    mvd = MultiValueDictionary<TKey, TValue>.Create<TValueCollection>(enumerable, null, () => []);
                }
                else
                {
                    mvd = MultiValueDictionary<TKey, TValue>.Create<TValueCollection>(enumerable, comparer: null);
                }

                CompareMVDs(enumerable, mvd);

                TKey key = _typeBuilder.GetNext(enumerable.Keys.ToArray());
                TValue value = _typeBuilder.GetNext<TValue>();

                enumerable.Add(key, value);

                CompareEnumerables(enumerable, mvd, areEqual);
                Assert.Equal(Helpers.PairsCount(mvd) + 1, Helpers.PairsCount(enumerable));
            }

            private static void MVD_Constructor6_InvalidTCollection<TKey, TValue>()
            where TKey : notnull
            {
                Assert.Throws<InvalidOperationException>(() => MultiValueDictionary<TKey, TValue>.Create([], null, () => new DummyReadOnlyCollection<TValue>()));
                Assert.Throws<InvalidOperationException>(() => MultiValueDictionary<TKey, TValue>.Create<DummyReadOnlyCollection<TValue>>([], (IEqualityComparer<TKey>?)null));
            }
        }
    }
}
