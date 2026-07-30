using System;
using System.Collections;

namespace System.Web.Profile
{
	/// <summary>A collection of <see cref="T:System.Web.Profile.ProfileInfo" /> objects.</summary>
	// Token: 0x02000505 RID: 1285
	[Serializable]
	public sealed class ProfileInfoCollection : IEnumerable, ICollection
	{
		/// <summary>Creates a new, empty <see cref="T:System.Web.Profile.ProfileInfoCollection" />.</summary>
		// Token: 0x06003933 RID: 14643 RVA: 0x00099DD3 File Offset: 0x00097FD3
		public ProfileInfoCollection()
		{
			this._Hashtable = new Hashtable(10, StringComparer.CurrentCultureIgnoreCase);
			this._ArrayList = new ArrayList();
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Profile.ProfileInfo" /> object to the collection.</summary>
		/// <param name="profileInfo">A <see cref="T:System.Web.Profile.ProfileInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Web.Profile.ProfileInfo" /> object with the same <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> value as <paramref name="profileInfo" /> already exists in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="profileInfo" /> is null.-or-The <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> property of <paramref name="profileInfo" /> is null.</exception>
		// Token: 0x06003934 RID: 14644 RVA: 0x00099DF8 File Offset: 0x00097FF8
		public void Add(ProfileInfo profileInfo)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			if (profileInfo == null || profileInfo.UserName == null)
			{
				throw new ArgumentNullException("profileInfo");
			}
			this._Hashtable.Add(profileInfo.UserName, this._CurPos);
			this._ArrayList.Add(profileInfo);
			this._CurPos++;
		}

		/// <summary>Removes the <see cref="T:System.Web.Profile.ProfileInfo" /> object with the specified user name from the collection.</summary>
		/// <param name="name">The <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> of the <see cref="T:System.Web.Profile.ProfileInfo" /> object to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003935 RID: 14645 RVA: 0x00099E60 File Offset: 0x00098060
		public void Remove(string name)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			object obj = this._Hashtable[name];
			if (obj == null)
			{
				return;
			}
			this._Hashtable.Remove(name);
			this._ArrayList[(int)obj] = null;
			this._NumBlanks++;
		}

		/// <summary>Gets the <see cref="T:System.Web.Profile.ProfileInfo" /> object in the collection, referenced by the specified <see cref="P:System.Web.Profile.ProfileInfo.UserName" />.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfo" /> object for the specified user name. If name is not found in the collection, null is returned.</returns>
		/// <param name="name">The <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> of the <see cref="T:System.Web.Profile.ProfileInfo" /> object to retrieve from the collection.</param>
		// Token: 0x170011C7 RID: 4551
		public ProfileInfo this[string name]
		{
			get
			{
				object obj = this._Hashtable[name];
				if (obj == null)
				{
					return null;
				}
				return this._ArrayList[(int)obj] as ProfileInfo;
			}
		}

		/// <summary>Gets an enumerator that can iterate through the <see cref="T:System.Web.Profile.ProfileInfoCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Web.Profile.ProfileInfoCollection" />.</returns>
		// Token: 0x06003937 RID: 14647 RVA: 0x00099EED File Offset: 0x000980ED
		public IEnumerator GetEnumerator()
		{
			this.DoCompact();
			return this._ArrayList.GetEnumerator();
		}

		/// <summary>Makes the contents of the <see cref="T:System.Web.Profile.ProfileInfoCollection" /> read-only.</summary>
		// Token: 0x06003938 RID: 14648 RVA: 0x00099F00 File Offset: 0x00098100
		public void SetReadOnly()
		{
			if (this._ReadOnly)
			{
				return;
			}
			this._ReadOnly = true;
		}

		/// <summary>Removes all <see cref="T:System.Web.Profile.ProfileInfo" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003939 RID: 14649 RVA: 0x00099F12 File Offset: 0x00098112
		public void Clear()
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			this._Hashtable.Clear();
			this._ArrayList.Clear();
			this._CurPos = 0;
			this._NumBlanks = 0;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.Profile.ProfileInfo" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.Profile.ProfileInfo" /> objects in the collection.</returns>
		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x0600393A RID: 14650 RVA: 0x00099F46 File Offset: 0x00098146
		public int Count
		{
			get
			{
				return this._Hashtable.Count;
			}
		}

		/// <summary>Gets a value indicating whether the profile info collection is thread safe.</summary>
		/// <returns>Always false, because thread-safe profile info collections are not supported.</returns>
		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the synchronization root.</summary>
		/// <returns>Always this (Me in Visual Basic), because synchronization of <see cref="T:System.Web.Profile.ProfileInfoCollection" /> objects is not supported.</returns>
		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x0600393C RID: 14652 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the <see cref="T:System.Web.Profile.ProfileInfoCollection" /> to a one-dimensional array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.Profile.ProfileInfoCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		// Token: 0x0600393D RID: 14653 RVA: 0x00099F53 File Offset: 0x00098153
		public void CopyTo(Array array, int index)
		{
			this.DoCompact();
			this._ArrayList.CopyTo(array, index);
		}

		/// <summary>Copies the <see cref="T:System.Web.Profile.ProfileInfoCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Profile.ProfileInfo" />.</summary>
		/// <param name="array">A one-dimensional array of type <see cref="T:System.Web.Profile.ProfileInfo" /> that is the destination of the elements copied from the <see cref="T:System.Web.Profile.ProfileInfoCollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600393E RID: 14654 RVA: 0x00099F53 File Offset: 0x00098153
		public void CopyTo(ProfileInfo[] array, int index)
		{
			this.DoCompact();
			this._ArrayList.CopyTo(array, index);
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x00099F68 File Offset: 0x00098168
		private void DoCompact()
		{
			if (this._NumBlanks < 1)
			{
				return;
			}
			ArrayList arrayList = new ArrayList(this._CurPos - this._NumBlanks);
			int num = -1;
			for (int i = 0; i < this._CurPos; i++)
			{
				if (this._ArrayList[i] != null)
				{
					arrayList.Add(this._ArrayList[i]);
				}
				else if (num == -1)
				{
					num = i;
				}
			}
			this._NumBlanks = 0;
			this._ArrayList = arrayList;
			this._CurPos = this._ArrayList.Count;
			for (int j = num; j < this._CurPos; j++)
			{
				ProfileInfo profileInfo = this._ArrayList[j] as ProfileInfo;
				this._Hashtable[profileInfo.UserName] = j;
			}
		}

		// Token: 0x04001F1B RID: 7963
		private Hashtable _Hashtable;

		// Token: 0x04001F1C RID: 7964
		private ArrayList _ArrayList;

		// Token: 0x04001F1D RID: 7965
		private bool _ReadOnly;

		// Token: 0x04001F1E RID: 7966
		private int _CurPos;

		// Token: 0x04001F1F RID: 7967
		private int _NumBlanks;
	}
}
