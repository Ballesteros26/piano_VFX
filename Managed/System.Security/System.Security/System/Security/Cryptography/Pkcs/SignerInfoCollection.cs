using System;
using System.Collections;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface. </summary>
	// Token: 0x02000033 RID: 51
	public sealed class SignerInfoCollection : ICollection, IEnumerable
	{
		// Token: 0x06000123 RID: 291 RVA: 0x000049D6 File Offset: 0x00002BD6
		internal SignerInfoCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>An int value that represents the number of items in the collection.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000049E9 File Offset: 0x00002BE9
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns false, which means the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of false, which means the collection is not thread safe.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object  at the specified index.</returns>
		/// <param name="index">An int value that represents the index in the collection. The index is zero based.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x17000056 RID: 86
		public SignerInfo this[int index]
		{
			get
			{
				return (SignerInfo)this._list[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004A09 File Offset: 0x00002C09
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004A16 File Offset: 0x00002C16
		internal void Add(SignerInfo signer)
		{
			this._list.Add(signer);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x06000129 RID: 297 RVA: 0x00004A25 File Offset: 0x00002C25
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.SignerInfo[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x0600012A RID: 298 RVA: 0x00004A5A File Offset: 0x00002C5A
		public void CopyTo(SignerInfo[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x0600012B RID: 299 RVA: 0x00004A8C File Offset: 0x00002C8C
		public SignerInfoEnumerator GetEnumerator()
		{
			return new SignerInfoEnumerator(this._list);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.System#Collections#IEnumerable#GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x0600012C RID: 300 RVA: 0x00004A8C File Offset: 0x00002C8C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SignerInfoEnumerator(this._list);
		}

		// Token: 0x040000F6 RID: 246
		private ArrayList _list;
	}
}
