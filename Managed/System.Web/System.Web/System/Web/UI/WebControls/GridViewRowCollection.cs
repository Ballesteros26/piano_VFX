using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.GridViewRow" /> objects in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	// Token: 0x020003A9 RID: 937
	public class GridViewRowCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> class using the specified <see cref="T:System.Collections.ArrayList" /> object.</summary>
		/// <param name="rows">An <see cref="T:System.Collections.ArrayList" /> object that contains the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> objects with which to initialize the collection.</param>
		// Token: 0x0600263B RID: 9787 RVA: 0x00064756 File Offset: 0x00062956
		public GridViewRowCollection(ArrayList rows)
		{
			this.rows = rows;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object at the specified index in the collection.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object to retrieve from the collection.</param>
		// Token: 0x17000C32 RID: 3122
		public GridViewRow this[int index]
		{
			get
			{
				return (GridViewRow)this.rows[index];
			}
		}

		/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" /> object.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object.</param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> object to receive the copied contents.</param>
		// Token: 0x0600263D RID: 9789 RVA: 0x00064783 File Offset: 0x00062983
		public void CopyTo(GridViewRow[] array, int index)
		{
			this.rows.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that contains all <see cref="T:System.Web.UI.WebControls.GridViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.GridViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" />.</returns>
		// Token: 0x0600263E RID: 9790 RVA: 0x00064792 File Offset: 0x00062992
		public IEnumerator GetEnumerator()
		{
			return this.rows.GetEnumerator();
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object.</summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object.</returns>
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x0006479F File Offset: 0x0006299F
		public int Count
		{
			get
			{
				return this.rows.Count;
			}
		}

		/// <summary>Gets a value indicating whether the rows in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object can be modified.</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object is synchronized (thread-safe). </summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" /> interface. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in a <see cref="T:System.Array" /> object at which copying begins.</param>
		// Token: 0x06002643 RID: 9795 RVA: 0x00064783 File Offset: 0x00062983
		void ICollection.CopyTo(Array array, int index)
		{
			this.rows.CopyTo(array, index);
		}

		// Token: 0x04001A43 RID: 6723
		private ArrayList rows = new ArrayList();
	}
}
