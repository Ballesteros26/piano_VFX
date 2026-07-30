using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web.Util;

namespace System.Web.SessionState
{
	/// <summary>A collection of objects stored in session state. This class cannot be inherited.</summary>
	// Token: 0x020004A1 RID: 1185
	public sealed class SessionStateItemCollection : NameObjectCollectionBase, ISessionStateItemCollection, ICollection, IEnumerable
	{
		// Token: 0x060035C4 RID: 13764 RVA: 0x0008D5D4 File Offset: 0x0008B7D4
		private static bool IsMutable(object o)
		{
			return o != null && Type.GetTypeCode(o.GetType()) == TypeCode.Object;
		}

		/// <summary>Creates a new, empty <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> object.</summary>
		// Token: 0x060035C5 RID: 13765 RVA: 0x0000665D File Offset: 0x0000485D
		public SessionStateItemCollection()
		{
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x0008D5E9 File Offset: 0x0008B7E9
		internal SessionStateItemCollection(int capacity)
			: base(capacity)
		{
		}

		/// <summary>Gets or sets a value indicating whether the collection has been marked as changed.</summary>
		/// <returns>true if the <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> contents have been changed; otherwise, false.</returns>
		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x0008D5F2 File Offset: 0x0008B7F2
		// (set) Token: 0x060035C8 RID: 13768 RVA: 0x0008D5FA File Offset: 0x0008B7FA
		public bool Dirty
		{
			get
			{
				return this.is_dirty;
			}
			set
			{
				this.is_dirty = value;
			}
		}

		/// <summary>Gets or sets a value in the collection by numerical index.</summary>
		/// <returns>The value in the collection stored at the specified index. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="index">The numerical index of the value in the collection.</param>
		// Token: 0x170010F5 RID: 4341
		public object this[int index]
		{
			get
			{
				object obj = base.BaseGet(index);
				if (SessionStateItemCollection.IsMutable(obj))
				{
					this.is_dirty = true;
				}
				return obj;
			}
			set
			{
				base.BaseSet(index, value);
				this.is_dirty = true;
			}
		}

		/// <summary>Gets or sets a value in the collection by name.</summary>
		/// <returns>The value in the collection with the specified name. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="name">The key name of the value in the collection.</param>
		// Token: 0x170010F6 RID: 4342
		public object this[string name]
		{
			get
			{
				object obj = base.BaseGet(name);
				if (SessionStateItemCollection.IsMutable(obj))
				{
					this.is_dirty = true;
				}
				return obj;
			}
			set
			{
				base.BaseSet(name, value);
				this.is_dirty = true;
			}
		}

		/// <summary>Gets a collection of the variable names for all values stored in the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> collection that contains all the collection keys. </returns>
		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060035CD RID: 13773 RVA: 0x0008D655 File Offset: 0x0008B855
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return base.Keys;
			}
		}

		/// <summary>Removes all values and keys from the session-state collection.</summary>
		// Token: 0x060035CE RID: 13774 RVA: 0x0008D65D File Offset: 0x0008B85D
		public void Clear()
		{
			if (this.Count > 0)
			{
				base.BaseClear();
				this.is_dirty = true;
			}
		}

		/// <summary>Creates a <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> collection from a storage location that is written to using the <see cref="M:System.Web.SessionState.SessionStateItemCollection.Serialize(System.IO.BinaryWriter)" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> collection populated with the contents from a storage location that is written to using the <see cref="M:System.Web.SessionState.SessionStateItemCollection.Serialize(System.IO.BinaryWriter)" /> method.</returns>
		/// <param name="reader">The <see cref="T:System.IO.BinaryReader" /> used to read the serialized collection from a stream or encoded string.</param>
		/// <exception cref="T:System.Web.HttpException">The session state information is invalid or corrupted</exception>
		// Token: 0x060035CF RID: 13775 RVA: 0x0008D678 File Offset: 0x0008B878
		public static SessionStateItemCollection Deserialize(BinaryReader reader)
		{
			int i = reader.ReadInt32();
			SessionStateItemCollection sessionStateItemCollection = new SessionStateItemCollection(i);
			while (i > 0)
			{
				sessionStateItemCollection[reader.ReadString()] = AltSerialization.Deserialize(reader);
				i--;
			}
			return sessionStateItemCollection;
		}

		/// <summary>Writes the contents of the collection to a <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.BinaryWriter" /> used to write the serialized collection to a stream or encoded string.</param>
		// Token: 0x060035D0 RID: 13776 RVA: 0x0008D6B0 File Offset: 0x0008B8B0
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.Count);
			foreach (object obj in base.Keys)
			{
				string text = (string)obj;
				writer.Write(text);
				AltSerialization.Serialize(writer, base.BaseGet(text));
			}
		}

		/// <summary>Returns an enumerator that can be used to read all the key names in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the variable names in the session-state collection.</returns>
		// Token: 0x060035D1 RID: 13777 RVA: 0x0008D724 File Offset: 0x0008B924
		public override IEnumerator GetEnumerator()
		{
			return base.GetEnumerator();
		}

		/// <summary>Deletes an item from the collection.</summary>
		/// <param name="name">The name of the item to delete from the collection. </param>
		// Token: 0x060035D2 RID: 13778 RVA: 0x0008D72C File Offset: 0x0008B92C
		public void Remove(string name)
		{
			base.BaseRemove(name);
			this.is_dirty = true;
		}

		/// <summary>Deletes an item at a specified index from the collection.</summary>
		/// <param name="index">The index of the item to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		// Token: 0x060035D3 RID: 13779 RVA: 0x0008D73C File Offset: 0x0008B93C
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
			this.is_dirty = true;
		}

		// Token: 0x04001D78 RID: 7544
		private bool is_dirty;
	}
}
