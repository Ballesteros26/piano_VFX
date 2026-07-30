using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Encapsulates basic validation rules that indicate whether a piece of custom data is considered personalizable in either <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> or <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope.</summary>
	// Token: 0x020006DB RID: 1755
	public class PersonalizationDictionary : ICollection, IEnumerable, IDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class. </summary>
		// Token: 0x06004A5B RID: 19035 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PersonalizationDictionary()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class using the provided parameter. </summary>
		/// <param name="initialSize">The estimated number of entries to be stored in the dictionary.</param>
		// Token: 0x06004A5C RID: 19036 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PersonalizationDictionary(int initialSize)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the number of entries in the dictionary.</summary>
		/// <returns>The number of entries in the dictionary.</returns>
		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x000CA770 File Offset: 0x000C8970
		public virtual int Count
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets whether the personalization dictionary is of a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x000CA78C File Offset: 0x000C898C
		public virtual bool IsFixedSize
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets whether the personalization dictionary is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x000CA7A8 File Offset: 0x000C89A8
		public virtual bool IsReadOnly
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets whether the personalization dictionary is synchronized.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x000CA7C4 File Offset: 0x000C89C4
		public virtual bool IsSynchronized
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets an entry in the personalization dictionary. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> representing custom personalization information identified by the <paramref name="key" /> parameter.</returns>
		/// <param name="key">The key of the entry to be retrieved or changed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is either an empty string (""), or trimming <paramref name="key" /> results in an empty string.</exception>
		// Token: 0x170016F7 RID: 5879
		public virtual PersonalizationEntry this[string key]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to an <see cref="T:System.Collections.ICollection" /> object containing the keys for the personalization dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys for the personalization dictionary.</returns>
		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06004A63 RID: 19043 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ICollection Keys
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the personalization dictionary.</summary>
		/// <returns>An object that can be used to synchronize access to the personalization dictionary.</returns>
		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual object SyncRoot
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x0000E80B File Offset: 0x0000CA0B
		object IDictionary.get_Item(object key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IDictionary.set_Item(object key, object value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to an <see cref="T:System.Collections.ICollection" /> object containing the values in the personalization dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values for the personalization dictionary.</returns>
		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ICollection Values
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Adds personalization entries to the personalization dictionary.</summary>
		/// <param name="key">The unique identifier for a piece of state information.</param>
		/// <param name="value">A piece of state information to be added to the personalization dictionary. This value can be null.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is a duplicate of a key already in the dictionary.- or -<paramref name="key" /> is either an empty string ("") or trimming <paramref name="key" /> results in an empty string.- or -<paramref name="value" /> is not a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.- or - <paramref name="value" /> is null.</exception>
		// Token: 0x06004A68 RID: 19048 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void Add(string key, PersonalizationEntry value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all custom state information from the current <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> object.</summary>
		// Token: 0x06004A69 RID: 19049 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value indicating whether a piece of custom state information with the same key is contained in the personalization dictionary.</summary>
		/// <returns>true if the provided key matches a key in the personalization dictionary; otherwise, false.</returns>
		/// <param name="key">A key value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is either an empty string (""), or trimming <paramref name="key" /> results in an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004A6A RID: 19050 RVA: 0x000CA7E0 File Offset: 0x000C89E0
		public virtual bool Contains(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the personalization dictionary state entries as <see cref="T:System.Collections.DictionaryEntry" /> instances into the specified array.</summary>
		/// <param name="array">The array the <see cref="T:System.Collections.DictionaryEntry" /> instances are copied into.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.- or -<paramref name="array" /> is the wrong size based on the <paramref name="index" /> parameter.- or - <paramref name="array" /> has insufficient capacity to hold the values contained in the personalization dictionary.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x06004A6B RID: 19051 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void CopyTo(DictionaryEntry[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns an enumerator that can be used to iterate through the entries in the personalization dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the personalization dictionary.</returns>
		// Token: 0x06004A6C RID: 19052 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes a custom state entry based on the provided key.</summary>
		/// <param name="key">The key of the entry to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is either an empty string (""), or trimming <paramref name="key" /> results in an empty string.</exception>
		// Token: 0x06004A6D RID: 19053 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void Remove(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> of <see cref="T:System.Collections.DictionaryEntry" /> items to copy into a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" />.</param>
		/// <param name="index">The starting point in a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> at which to insert <paramref name="array" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not an array of <see cref="T:System.Collections.DictionaryEntry" /> items.</exception>
		// Token: 0x06004A6E RID: 19054 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void ICollection.CopyTo(Array array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.IDictionary.Add(System.Object,System.Object)" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class. </summary>
		/// <param name="key">The <see cref="T:System.String" /> to use as the key for an item in the personalization dictionary. </param>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> to add to the personalization dictionary. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is not a <see cref="T:System.String" /> object.- or -<paramref name="value" /> is not a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> object.</exception>
		// Token: 0x06004A6F RID: 19055 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IDictionary.Add(object key, object value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.IDictionary.Contains(System.Object)" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class.</summary>
		/// <returns>true if <paramref name="key" /> exists in the personalization dictionary; otherwise false.</returns>
		/// <param name="key">A <see cref="T:System.String" /> object that identifies a particular <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> to check for existence in the personalization dictionary.  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is not a <see cref="T:System.String" />.</exception>
		// Token: 0x06004A70 RID: 19056 RVA: 0x000CA7FC File Offset: 0x000C89FC
		bool IDictionary.Contains(object key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IDictionary.Remove(System.Object)" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that identifies a particular <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> object to remove from the personalization dictionary. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is not a <see cref="T:System.String" />.</exception>
		// Token: 0x06004A71 RID: 19057 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IDictionary.Remove(object key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.IEnumerator" /> to enumerate through the items in a personalization dictionary.</returns>
		// Token: 0x06004A72 RID: 19058 RVA: 0x0000E80B File Offset: 0x0000CA0B
		IEnumerator IEnumerable.GetEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
