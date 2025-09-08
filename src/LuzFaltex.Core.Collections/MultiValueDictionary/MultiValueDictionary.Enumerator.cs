//
//  MultiValueDictionary.Enumerator.cs
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
using System.Collections;
using System.Collections.Generic;

namespace LuzFaltex.Core.Collections
{
    public partial class MultiValueDictionary<TKey, TValue> where TKey : notnull
    {
        /// <summary>
        /// An enumerator class for a <see cref="MultiValueDictionary{TKey, TValue}"/>.
        /// </summary>
        private sealed class Enumerator
            : IEnumerator<KeyValuePair<TKey, IReadOnlyCollection<TValue>>>,
              IEnumerator,
              IDisposable
        {
            private enum EnumerationState
            {
                BeforeFirst,
                During,
                AfterLast
            }

            private readonly MultiValueDictionary<TKey, TValue> _multiValueDictionary;
            private readonly int _version;

            private Dictionary<TKey, InnerCollectionView>.Enumerator _enumerator;
            private EnumerationState _state;

            /// <inheritdoc/>
            public KeyValuePair<TKey, IReadOnlyCollection<TValue>> Current { get; private set; }

            /// <inheritdoc/>
            object IEnumerator.Current => _state switch
            {
                EnumerationState.BeforeFirst => throw new InvalidOperationException("Enumeration not yet started."),
                EnumerationState.AfterLast => throw new InvalidOperationException("Enumeration has completed."),
                _ => Current
            };

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> class.
            /// </summary>
            /// <param name="multiValueDictionary">The MultiValueDictionary to iterate.</param>
            internal Enumerator(MultiValueDictionary<TKey, TValue> multiValueDictionary)
            {
                _multiValueDictionary = multiValueDictionary;
                _version = multiValueDictionary._version;
                _enumerator = multiValueDictionary._dictionary.GetEnumerator();
                _state = EnumerationState.BeforeFirst;
                Current = default;
            }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                if (_version != _multiValueDictionary._version)
                {
                    throw new InvalidOperationException("Version mismatch!");
                }

                if (_enumerator.MoveNext())
                {
                    Current = new KeyValuePair<TKey, IReadOnlyCollection<TValue>>(_enumerator.Current.Key, _enumerator.Current.Value);
                    _state = EnumerationState.During;
                    return true;
                }

                Current = default;
                _state = EnumerationState.AfterLast;
                return false;
            }

            /// <inheritdoc/>
            public void Reset()
            {
                if (_version != _multiValueDictionary._version)
                {
                    throw new InvalidOperationException("Version mismatch!");
                }

                _enumerator.Dispose();
                _enumerator = _multiValueDictionary._dictionary.GetEnumerator();
                Current = default;
                _state = EnumerationState.BeforeFirst;
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                _enumerator.Dispose();
            }
        }
    }
}
