using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QBForge.Extensions.Linq
{
	internal static class LinqExtensions
	{
		public static IReadOnlyDictionary<TKey, TValue> AsOrderedReadOnlyDictionary<TKey, TValue>(this ICollection<TValue> collectionToAttach, Func<TValue, TKey> getItemKey, IEqualityComparer<TKey>? keyComparer = null, bool buildIndex = true)
		{
			return buildIndex
				? (IReadOnlyDictionary<TKey, TValue>)new ReadOnlyDictionaryAdapter<TKey, TValue>(collectionToAttach, getItemKey, keyComparer)
				: (IReadOnlyDictionary<TKey, TValue>)new OrderedReadOnlyDictionary<TKey, TValue>(collectionToAttach, getItemKey, keyComparer);
		}

		private sealed class ReadOnlyDictionaryAdapter<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
		{
			private readonly ICollection<TValue> _col;
			private readonly Func<TValue, TKey> _getItemKey;
			private readonly IEqualityComparer<TKey> _keyComparer;

			public int Count => _col.Count;
			public IEnumerable<TKey> Keys => _col.Select(item => _getItemKey(item));
			public IEnumerable<TValue> Values => _col;

			public ReadOnlyDictionaryAdapter(ICollection<TValue> collectionToAttach, Func<TValue, TKey> getItemKey, IEqualityComparer<TKey>? keyComparer = null)
			{
				_col = collectionToAttach;
				_getItemKey = getItemKey;
				_keyComparer = _keyComparer ?? EqualityComparer<TKey>.Default;
			}

			public TValue this[TKey key]
			{
				get
				{
					foreach (var item in _col)
					{
						if (_keyComparer.Equals(key, _getItemKey(item)))
						{
							return item;
						}
					}

					throw new KeyNotFoundException();
				}
			}

			public bool ContainsKey(TKey key)
			{
				foreach (var item in _col)
				{
					if (_keyComparer.Equals(key, _getItemKey(item)))
					{
						return true;
					}
				}

				return false;
			}

			public bool TryGetValue(TKey key, out TValue value)
			{
				foreach (var item in _col)
				{
					if (_keyComparer.Equals(key, _getItemKey(item)))
					{
						value = item;
						return true;
					}
				}

				value = default(TValue)!;
				return false;
			}

			public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
			{
				return _col.Select(item => new KeyValuePair<TKey, TValue>(_getItemKey(item), item)).GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		private sealed class OrderedReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
		{
			private readonly ICollection<TValue> _col;
			private readonly Func<TValue, TKey> _getItemKey;
			private readonly Dictionary<TKey, TValue> _index;

			public int Count => _col.Count;
			public IEnumerable<TKey> Keys => _col.Select(item => _getItemKey(item));
			public IEnumerable<TValue> Values => _col;

			public OrderedReadOnlyDictionary(ICollection<TValue> collectionToAttach, Func<TValue, TKey> getItemKey, IEqualityComparer<TKey>? keyComparer = null)
			{
				_col = collectionToAttach;
				_getItemKey = getItemKey;
				_index = new Dictionary<TKey, TValue>(_col.Count, keyComparer ?? EqualityComparer<TKey>.Default);

				foreach (var item in _col)
				{
					_index.Add(_getItemKey(item), item);
				}
			}

			public TValue this[TKey key] => _index[key];

			public bool ContainsKey(TKey key) => _index.ContainsKey(key);

			public bool TryGetValue(TKey key, out TValue value) => _index.TryGetValue(key, out value);

			public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
				=> _col.Select(item => new KeyValuePair<TKey, TValue>(_getItemKey(item), item)).GetEnumerator();

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}