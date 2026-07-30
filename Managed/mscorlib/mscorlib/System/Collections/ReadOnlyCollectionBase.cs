using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Provides the abstract base class for a strongly typed non-generic read-only collection.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020009E0 RID: 2528
	[ComVisible(true)]
	[Serializable]
	public abstract class ReadOnlyCollectionBase : ICollection, IEnumerable
	{
		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> representing the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance itself.</returns>
		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06005D5C RID: 23900 RVA: 0x001347A0 File Offset: 0x001329A0
		protected ArrayList InnerList
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.Retrieving the value of this property is an O(1) operation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06005D5D RID: 23901 RVA: 0x001347BB File Offset: 0x001329BB
		public virtual int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06005D5E RID: 23902 RVA: 0x001347C8 File Offset: 0x001329C8
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object.</returns>
		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06005D5F RID: 23903 RVA: 0x001347D5 File Offset: 0x001329D5
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ReadOnlyCollectionBase" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ReadOnlyCollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ReadOnlyCollectionBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ReadOnlyCollectionBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06005D60 RID: 23904 RVA: 0x000E8E65 File Offset: 0x000E7065
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D61 RID: 23905 RVA: 0x001347E2 File Offset: 0x001329E2
		public virtual IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x04002F8A RID: 12170
		private ArrayList list;
	}
}
