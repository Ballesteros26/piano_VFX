using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	// Token: 0x0200038E RID: 910
	public class DetailsViewRowCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> class using the specified <see cref="T:System.Collections.ArrayList" /> object.</summary>
		/// <param name="rows">An <see cref="T:System.Collections.ArrayList" /> that contains the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects with which to initialize the collection.</param>
		// Token: 0x060023B8 RID: 9144 RVA: 0x0005CEB5 File Offset: 0x0005B0B5
		public DetailsViewRowCollection(ArrayList rows)
		{
			this.rows = rows;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> at the specified index in the collection.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> to retrieve from the collection.</param>
		// Token: 0x17000B54 RID: 2900
		public DetailsViewRow this[int index]
		{
			get
			{
				return (DetailsViewRow)this.rows[index];
			}
		}

		/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the copied contents.</param>
		// Token: 0x060023BA RID: 9146 RVA: 0x0005CEE2 File Offset: 0x0005B0E2
		public void CopyTo(DetailsViewRow[] array, int index)
		{
			this.rows.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that contains all <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</returns>
		// Token: 0x060023BB RID: 9147 RVA: 0x0005CEF1 File Offset: 0x0005B0F1
		public IEnumerator GetEnumerator()
		{
			return this.rows.GetEnumerator();
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object.</summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</returns>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x0005CEFE File Offset: 0x0005B0FE
		public int Count
		{
			get
			{
				return this.rows.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object is synchronized (thread safe).</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object used to synchronize access to the collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the rows in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object can be modified.</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the copied contents.</param>
		// Token: 0x060023C0 RID: 9152 RVA: 0x0005CEE2 File Offset: 0x0005B0E2
		void ICollection.CopyTo(Array array, int index)
		{
			this.rows.CopyTo(array, index);
		}

		// Token: 0x04001989 RID: 6537
		private ArrayList rows = new ArrayList();
	}
}
