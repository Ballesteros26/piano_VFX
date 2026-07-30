using System;
using System.Collections;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Returns the set of captured groups in a single match.</summary>
	// Token: 0x0200014A RID: 330
	[Serializable]
	public class GroupCollection : ICollection, IEnumerable
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x00031689 File Offset: 0x0002F889
		internal GroupCollection(Match match, Hashtable caps)
		{
			this._match = match;
			this._captureMap = caps;
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Text.RegularExpressions.GroupCollection" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Text.RegularExpressions.Match" /> object to synchronize.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0003169F File Offset: 0x0002F89F
		public object SyncRoot
		{
			get
			{
				return this._match;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Text.RegularExpressions.GroupCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x000027E2 File Offset: 0x000009E2
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns the number of groups in the collection.</summary>
		/// <returns>The number of groups in the collection.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x000316A7 File Offset: 0x0002F8A7
		public int Count
		{
			get
			{
				return this._match._matchcount.Length;
			}
		}

		/// <summary>Enables access to a member of the collection by integer index.</summary>
		/// <returns>The member of the collection specified by <paramref name="groupnum" />.</returns>
		/// <param name="groupnum">The zero-based index of the collection member to be retrieved. </param>
		// Token: 0x17000197 RID: 407
		public Group this[int groupnum]
		{
			get
			{
				return this.GetGroup(groupnum);
			}
		}

		/// <summary>Enables access to a member of the collection by string index.</summary>
		/// <returns>The member of the collection specified by <paramref name="groupname" />.</returns>
		/// <param name="groupname">The name of a capturing group. </param>
		// Token: 0x17000198 RID: 408
		public Group this[string groupname]
		{
			get
			{
				if (this._match._regex == null)
				{
					return Group._emptygroup;
				}
				return this.GetGroup(this._match._regex.GroupNumberFromName(groupname));
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000316EC File Offset: 0x0002F8EC
		internal Group GetGroup(int groupnum)
		{
			if (this._captureMap != null)
			{
				object obj = this._captureMap[groupnum];
				if (obj == null)
				{
					return Group._emptygroup;
				}
				return this.GetGroupImpl((int)obj);
			}
			else
			{
				if (groupnum >= this._match._matchcount.Length || groupnum < 0)
				{
					return Group._emptygroup;
				}
				return this.GetGroupImpl(groupnum);
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0003174C File Offset: 0x0002F94C
		internal Group GetGroupImpl(int groupnum)
		{
			if (groupnum == 0)
			{
				return this._match;
			}
			if (this._groups == null)
			{
				this._groups = new Group[this._match._matchcount.Length - 1];
				for (int i = 0; i < this._groups.Length; i++)
				{
					string text = this._match._regex.GroupNameFromNumber(i + 1);
					this._groups[i] = new Group(this._match._text, this._match._matches[i + 1], this._match._matchcount[i + 1], text);
				}
			}
			return this._groups[groupnum - 1];
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the destination array where the copying is to begin. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.-or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.GroupCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		// Token: 0x060009A0 RID: 2464 RVA: 0x000317F0 File Offset: 0x0002F9F0
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], num);
				num++;
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that contains all <see cref="T:System.Text.RegularExpressions.Group" /> objects in the <see cref="T:System.Text.RegularExpressions.GroupCollection" />.</returns>
		// Token: 0x060009A1 RID: 2465 RVA: 0x00031830 File Offset: 0x0002FA30
		public IEnumerator GetEnumerator()
		{
			return new GroupEnumerator(this);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal GroupCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000EBC RID: 3772
		internal Match _match;

		// Token: 0x04000EBD RID: 3773
		internal Hashtable _captureMap;

		// Token: 0x04000EBE RID: 3774
		internal Group[] _groups;
	}
}
