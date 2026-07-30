using System;

namespace System.Collections
{
	/// <summary>Defines a dictionary key/value pair that can be set or retrieved.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020009AA RID: 2474
	[Serializable]
	public struct DictionaryEntry
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Collections.DictionaryEntry" /> type with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair. </param>
		/// <param name="value">The definition associated with <paramref name="key" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null and the .NET Framework version is 1.0 or 1.1. </exception>
		// Token: 0x06005AA2 RID: 23202 RVA: 0x0012C8BB File Offset: 0x0012AABB
		public DictionaryEntry(object key, object value)
		{
			this._key = key;
			this._value = value;
		}

		/// <summary>Gets or sets the key in the key/value pair.</summary>
		/// <returns>The key in the key/value pair.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06005AA3 RID: 23203 RVA: 0x0012C8CB File Offset: 0x0012AACB
		// (set) Token: 0x06005AA4 RID: 23204 RVA: 0x0012C8D3 File Offset: 0x0012AAD3
		public object Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Gets or sets the value in the key/value pair.</summary>
		/// <returns>The value in the key/value pair.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06005AA5 RID: 23205 RVA: 0x0012C8DC File Offset: 0x0012AADC
		// (set) Token: 0x06005AA6 RID: 23206 RVA: 0x0012C8E4 File Offset: 0x0012AAE4
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x0012C8ED File Offset: 0x0012AAED
		public void Deconstruct(out object key, out object value)
		{
			key = this.Key;
			value = this.Value;
		}

		// Token: 0x04002EFC RID: 12028
		private object _key;

		// Token: 0x04002EFD RID: 12029
		private object _value;
	}
}
