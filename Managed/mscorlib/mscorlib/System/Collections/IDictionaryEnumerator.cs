using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Enumerates the elements of a nongeneric dictionary.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020009CE RID: 2510
	[ComVisible(true)]
	public interface IDictionaryEnumerator : IEnumerator
	{
		/// <summary>Gets the key of the current dictionary entry.</summary>
		/// <returns>The key of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06005CF5 RID: 23797
		object Key { get; }

		/// <summary>Gets the value of the current dictionary entry.</summary>
		/// <returns>The value of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06005CF6 RID: 23798
		object Value { get; }

		/// <summary>Gets both the key and the value of the current dictionary entry.</summary>
		/// <returns>A <see cref="T:System.Collections.DictionaryEntry" /> containing both the key and the value of the current dictionary entry.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06005CF7 RID: 23799
		DictionaryEntry Entry { get; }
	}
}
