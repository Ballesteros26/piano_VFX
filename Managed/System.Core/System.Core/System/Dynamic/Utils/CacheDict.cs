using System;
using System.Threading;

namespace System.Dynamic.Utils
{
	// Token: 0x02000339 RID: 825
	internal sealed class CacheDict<TKey, TValue>
	{
		// Token: 0x06001906 RID: 6406 RVA: 0x000524E8 File Offset: 0x000506E8
		internal CacheDict(int size)
		{
			int num = CacheDict<TKey, TValue>.AlignSize(size);
			this._mask = num - 1;
			this._entries = new CacheDict<TKey, TValue>.Entry[num];
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00052517 File Offset: 0x00050717
		private static int AlignSize(int size)
		{
			size--;
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			size++;
			return size;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00052548 File Offset: 0x00050748
		internal bool TryGetValue(TKey key, out TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = hashCode & this._mask;
			CacheDict<TKey, TValue>.Entry entry = Volatile.Read<CacheDict<TKey, TValue>.Entry>(ref this._entries[num]);
			if (entry != null && entry._hash == hashCode)
			{
				TKey key2 = entry._key;
				if (key2.Equals(key))
				{
					value = entry._value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000525BC File Offset: 0x000507BC
		internal void Add(TKey key, TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = hashCode & this._mask;
			CacheDict<TKey, TValue>.Entry entry = Volatile.Read<CacheDict<TKey, TValue>.Entry>(ref this._entries[num]);
			if (entry != null && entry._hash == hashCode)
			{
				TKey key2 = entry._key;
				if (key2.Equals(key))
				{
					return;
				}
			}
			Volatile.Write<CacheDict<TKey, TValue>.Entry>(ref this._entries[num], new CacheDict<TKey, TValue>.Entry(hashCode, key, value));
		}

		// Token: 0x17000473 RID: 1139
		internal TValue this[TKey key]
		{
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x04000B45 RID: 2885
		private readonly int _mask;

		// Token: 0x04000B46 RID: 2886
		private readonly CacheDict<TKey, TValue>.Entry[] _entries;

		// Token: 0x0200033A RID: 826
		private sealed class Entry
		{
			// Token: 0x0600190B RID: 6411 RVA: 0x0005263D File Offset: 0x0005083D
			internal Entry(int hash, TKey key, TValue value)
			{
				this._hash = hash;
				this._key = key;
				this._value = value;
			}

			// Token: 0x04000B47 RID: 2887
			internal readonly int _hash;

			// Token: 0x04000B48 RID: 2888
			internal readonly TKey _key;

			// Token: 0x04000B49 RID: 2889
			internal readonly TValue _value;
		}
	}
}
