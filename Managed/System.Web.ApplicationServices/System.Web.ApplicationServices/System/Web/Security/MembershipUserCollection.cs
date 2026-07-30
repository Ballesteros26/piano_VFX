using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Web.Security
{
	/// <summary>A collection of <see cref="T:System.Web.Security.MembershipUser" /> objects.</summary>
	// Token: 0x02000012 RID: 18
	[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public sealed class MembershipUserCollection : IEnumerable, ICollection
	{
		/// <summary>Creates a new, empty membership user collection.</summary>
		// Token: 0x0600003C RID: 60 RVA: 0x0000263F File Offset: 0x0000083F
		public MembershipUserCollection()
		{
			this._Indices = new Hashtable(10, StringComparer.CurrentCultureIgnoreCase);
			this._Values = new ArrayList();
		}

		/// <summary>Adds the specified membership user to the collection.</summary>
		/// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> object to add to the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Web.Security.MembershipUser.UserName" /> of the <paramref name="user" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Web.Security.MembershipUser" /> object with the same <see cref="P:System.Web.Security.MembershipUser.UserName" /> value as <paramref name="user" /> already exists in the collection.</exception>
		// Token: 0x0600003D RID: 61 RVA: 0x00002664 File Offset: 0x00000864
		public void Add(MembershipUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			int num = this._Values.Add(user);
			try
			{
				this._Indices.Add(user.UserName, num);
			}
			catch
			{
				this._Values.RemoveAt(num);
				throw;
			}
		}

		/// <summary>Removes the membership user object with the specified user name from the collection.</summary>
		/// <param name="name">The user name of the <see cref="T:System.Web.Security.MembershipUser" /> object to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600003E RID: 62 RVA: 0x000026D4 File Offset: 0x000008D4
		public void Remove(string name)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			object obj = this._Indices[name];
			if (obj == null || !(obj is int))
			{
				return;
			}
			int num = (int)obj;
			if (num >= this._Values.Count)
			{
				return;
			}
			this._Values.RemoveAt(num);
			this._Indices.Remove(name);
			ArrayList arrayList = new ArrayList();
			foreach (object obj2 in this._Indices)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				if ((int)dictionaryEntry.Value > num)
				{
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			foreach (object obj3 in arrayList)
			{
				string text = (string)obj3;
				this._Indices[text] = (int)this._Indices[text] - 1;
			}
		}

		/// <summary>Gets the membership user in the collection referenced by the specified user name.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the user specified by <paramref name="name" />.</returns>
		/// <param name="name">The <see cref="P:System.Web.Security.MembershipUser.UserName" /> of the <see cref="T:System.Web.Security.MembershipUser" /> to retrieve from the collection.</param>
		// Token: 0x17000015 RID: 21
		public MembershipUser this[string name]
		{
			get
			{
				object obj = this._Indices[name];
				if (obj == null || !(obj is int))
				{
					return null;
				}
				int num = (int)obj;
				if (num >= this._Values.Count)
				{
					return null;
				}
				return (MembershipUser)this._Values[num];
			}
		}

		/// <summary>Gets an enumerator that can iterate through the membership user collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Web.Security.MembershipUserCollection" />.</returns>
		// Token: 0x06000040 RID: 64 RVA: 0x0000285B File Offset: 0x00000A5B
		public IEnumerator GetEnumerator()
		{
			return this._Values.GetEnumerator();
		}

		/// <summary>Makes the contents of the membership user collection read-only.</summary>
		// Token: 0x06000041 RID: 65 RVA: 0x00002868 File Offset: 0x00000A68
		public void SetReadOnly()
		{
			if (this._ReadOnly)
			{
				return;
			}
			this._ReadOnly = true;
			this._Values = ArrayList.ReadOnly(this._Values);
		}

		/// <summary>Removes all membership user objects from the collection.</summary>
		// Token: 0x06000042 RID: 66 RVA: 0x0000288B File Offset: 0x00000A8B
		public void Clear()
		{
			this._Values.Clear();
			this._Indices.Clear();
		}

		/// <summary>Gets the number of membership user objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.Security.MembershipUser" /> objects in the collection.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028A3 File Offset: 0x00000AA3
		public int Count
		{
			get
			{
				return this._Values.Count;
			}
		}

		/// <summary>Gets a value indicating whether the membership user collection is thread safe.</summary>
		/// <returns>Always false because thread-safe membership user collections are not supported.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000021AF File Offset: 0x000003AF
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the synchronization root.</summary>
		/// <returns>Always this, because synchronization of membership user collections is not supported.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000233A File Offset: 0x0000053A
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the contents of the <see cref="T:System.Web.Security.MembershipUserCollection" /> object to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination for the objects copied from the <see cref="T:System.Web.Security.MembershipUserCollection" /> object. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="index" /> is greater than or equal to the length of <paramref name="array" />.-or-The number of elements in the source <see cref="T:System.Web.Security.MembershipUserCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination array. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Web.Security.MembershipUserCollection" /> cannot be cast automatically to the type of the destination array. </exception>
		// Token: 0x06000046 RID: 70 RVA: 0x000028B0 File Offset: 0x00000AB0
		void ICollection.CopyTo(Array array, int index)
		{
			this._Values.CopyTo(array, index);
		}

		/// <summary>Copies the membership user collection to a one-dimensional array.</summary>
		/// <param name="array">A one-dimensional array of type <see cref="T:System.Web.Security.MembershipUser" /> that is the destination of the elements copied from the <see cref="T:System.Web.Security.MembershipUserCollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		// Token: 0x06000047 RID: 71 RVA: 0x000028B0 File Offset: 0x00000AB0
		public void CopyTo(MembershipUser[] array, int index)
		{
			this._Values.CopyTo(array, index);
		}

		// Token: 0x04000056 RID: 86
		private Hashtable _Indices;

		// Token: 0x04000057 RID: 87
		private ArrayList _Values;

		// Token: 0x04000058 RID: 88
		private bool _ReadOnly;
	}
}
