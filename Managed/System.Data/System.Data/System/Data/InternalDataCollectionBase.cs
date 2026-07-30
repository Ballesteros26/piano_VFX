using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	/// <summary>Provides the base functionality for creating collections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000050 RID: 80
	public class InternalDataCollectionBase : ICollection, IEnumerable
	{
		/// <summary>Gets the total number of elements in a collection.</summary>
		/// <returns>The total number of elements in a collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.InternalDataCollectionBase" /> to a one-dimensional <see cref="T:System.Array" />, starting at the specified <see cref="T:System.Data.InternalDataCollectionBase" /> index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> to copy the current <see cref="T:System.Data.InternalDataCollectionBase" /> object's elements into. </param>
		/// <param name="index">The destination <see cref="T:System.Array" /> index to start copying into. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000290 RID: 656 RVA: 0x0000ECC1 File Offset: 0x0000CEC1
		public virtual void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000291 RID: 657 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public virtual IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.InternalDataCollectionBase" /> is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000061D5 File Offset: 0x000043D5
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.InternalDataCollectionBase" /> is synchonized.</summary>
		/// <returns>true if the collection is synchronized; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000061D5 File Offset: 0x000043D5
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000ECDD File Offset: 0x0000CEDD
		internal int NamesEqual(string s1, string s2, bool fCaseSensitive, CultureInfo locale)
		{
			if (fCaseSensitive)
			{
				if (string.Compare(s1, s2, false, locale) != 0)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (locale.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) != 0)
				{
					return 0;
				}
				if (string.Compare(s1, s2, false, locale) != 0)
				{
					return -1;
				}
				return 1;
			}
		}

		/// <summary>Gets an object that can be used to synchronize the collection.</summary>
		/// <returns>The <see cref="T:System.object" /> used to synchronize the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00005D82 File Offset: 0x00003F82
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the items of the collection as a list.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the collection.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00004526 File Offset: 0x00002726
		protected virtual ArrayList List
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040004E6 RID: 1254
		internal static readonly CollectionChangeEventArgs s_refreshEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
	}
}
