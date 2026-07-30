using System;
using System.Collections;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface. </summary>
	// Token: 0x0200002E RID: 46
	public sealed class RecipientInfoCollection : ICollection, IEnumerable
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00004319 File Offset: 0x00002519
		internal RecipientInfoCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>An int value that represents the number of items in the collection.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000432C File Offset: 0x0000252C
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns false, which means the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of false, which means the collection is not thread safe.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004339 File Offset: 0x00002539
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object at the specified index.</returns>
		/// <param name="index">An int value that represents the index in the collection. The index is zero based.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x17000044 RID: 68
		public RecipientInfo this[int index]
		{
			get
			{
				return (RecipientInfo)this._list[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" />  object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004359 File Offset: 0x00002559
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004366 File Offset: 0x00002566
		internal int Add(RecipientInfo ri)
		{
			return this._list.Add(ri);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which  the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x060000F2 RID: 242 RVA: 0x00004374 File Offset: 0x00002574
		public void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.RecipientInfo[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x060000F3 RID: 243 RVA: 0x00004374 File Offset: 0x00002574
		public void CopyTo(RecipientInfo[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x060000F4 RID: 244 RVA: 0x00004383 File Offset: 0x00002583
		public RecipientInfoEnumerator GetEnumerator()
		{
			return new RecipientInfoEnumerator(this._list);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.System#Collections#IEnumerable#GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x060000F5 RID: 245 RVA: 0x00004383 File Offset: 0x00002583
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new RecipientInfoEnumerator(this._list);
		}

		// Token: 0x040000E3 RID: 227
		private ArrayList _list;
	}
}
