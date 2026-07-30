using System;

namespace System.Collections.Generic
{
	/// <summary>Defines a key/value pair that can be set or retrieved.</summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A27 RID: 2599
	[Serializable]
	public struct KeyValuePair<TKey, TValue>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structure with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair.</param>
		/// <param name="value">The definition associated with <paramref name="key" />.</param>
		// Token: 0x06005FE0 RID: 24544 RVA: 0x0013B7AB File Offset: 0x001399AB
		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		/// <summary>Gets the key in the key/value pair.</summary>
		/// <returns>A <paramref name="TKey" /> that is the key of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />. </returns>
		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06005FE1 RID: 24545 RVA: 0x0013B7BB File Offset: 0x001399BB
		public TKey Key
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Gets the value in the key/value pair.</summary>
		/// <returns>A <paramref name="TValue" /> that is the value of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />. </returns>
		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06005FE2 RID: 24546 RVA: 0x0013B7C3 File Offset: 0x001399C3
		public TValue Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, using the string representations of the key and value.</summary>
		/// <returns>A string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, which includes the string representations of the key and value.</returns>
		// Token: 0x06005FE3 RID: 24547 RVA: 0x0013B7CB File Offset: 0x001399CB
		public override string ToString()
		{
			return KeyValuePair.PairToString(this.Key, this.Value);
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x0013B7E8 File Offset: 0x001399E8
		public void Deconstruct(out TKey key, out TValue value)
		{
			key = this.Key;
			value = this.Value;
		}

		// Token: 0x04003056 RID: 12374
		private TKey key;

		// Token: 0x04003057 RID: 12375
		private TValue value;
	}
}
