using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection that contains the key field of each record in a data source. This class cannot be inherited.</summary>
	// Token: 0x02000381 RID: 897
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataKeyCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> class.</summary>
		/// <param name="keys">A <see cref="T:System.Collections.ArrayList" /> that contains key fields from the data source. </param>
		// Token: 0x06002251 RID: 8785 RVA: 0x00058668 File Offset: 0x00056868
		public DataKeyCollection(ArrayList keys)
		{
			this.list = keys;
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x00058677 File Offset: 0x00056877
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether items in the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> can be modified.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the key field at the specified index in the collection.</summary>
		/// <returns>The key field at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the key field to retrieve from the collection. </param>
		// Token: 0x17000AD2 RID: 2770
		public object this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		/// <summary>Gets the object used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" /> object.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> object to receive the copied contents. </param>
		// Token: 0x06002257 RID: 8791 RVA: 0x00058692 File Offset: 0x00056892
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Creates a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all key fields in the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all key fields in the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />.</returns>
		// Token: 0x06002258 RID: 8792 RVA: 0x000586A1 File Offset: 0x000568A1
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x04001915 RID: 6421
		private ArrayList list;
	}
}
