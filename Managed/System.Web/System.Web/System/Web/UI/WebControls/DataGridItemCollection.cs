using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in a <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	// Token: 0x0200037C RID: 892
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataGridItemCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> class.</summary>
		/// <param name="items">A <see cref="T:System.Collections.ArrayList" /> that contains the items with which to initialize the collection. </param>
		// Token: 0x0600221C RID: 8732 RVA: 0x00057AEE File Offset: 0x00055CEE
		public DataGridItemCollection(ArrayList items)
		{
			this.array = items;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the collection.</returns>
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x00057AFD File Offset: 0x00055CFD
		public int Count
		{
			get
			{
				return this.array.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection can be modified.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x00057B0A File Offset: 0x00055D0A
		public bool IsReadOnly
		{
			get
			{
				return this.array.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x00057B17 File Offset: 0x00055D17
		public bool IsSynchronized
		{
			get
			{
				return this.array.IsSynchronized;
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x00057B24 File Offset: 0x00055D24
		public object SyncRoot
		{
			get
			{
				return this.array.SyncRoot;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DataGridItem" /> at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> to retrieve from the collection. </param>
		// Token: 0x17000ABC RID: 2748
		public DataGridItem this[int index]
		{
			get
			{
				return (DataGridItem)this.array[index];
			}
		}

		/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" />. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
		// Token: 0x06002222 RID: 8738 RVA: 0x00057B44 File Offset: 0x00055D44
		public void CopyTo(Array array, int index)
		{
			if (!(array is DataGridItem[]))
			{
				throw new InvalidCastException("Target array must be DataGridItem[]");
			}
			if (index + this.array.Count > array.Length)
			{
				throw new IndexOutOfRangeException("Target array not large enough to hold copied array.");
			}
			this.array.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" />.</returns>
		// Token: 0x06002223 RID: 8739 RVA: 0x00057B91 File Offset: 0x00055D91
		public IEnumerator GetEnumerator()
		{
			return this.array.GetEnumerator();
		}

		// Token: 0x04001907 RID: 6407
		private ArrayList array;
	}
}
