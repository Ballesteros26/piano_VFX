using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.HtmlControls
{
	/// <summary>A collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects that represent the rows of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000277 RID: 631
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HtmlTableRowCollection : ICollection, IEnumerable
	{
		// Token: 0x06001A0D RID: 6669 RVA: 0x0004565D File Offset: 0x0004385D
		internal HtmlTableRowCollection(HtmlTable table)
		{
			this.cc = table.Controls;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. The default value is 0.</returns>
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x00045671 File Offset: 0x00043871
		public int Count
		{
			get
			{
				return this.cc.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases, which indicates that access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> is not synchronized (not thread safe).</returns>
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> that represents a row contained in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />.</returns>
		/// <param name="index">An ordinal index value that specifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to return. </param>
		// Token: 0x17000836 RID: 2102
		public HtmlTableRow this[int index]
		{
			get
			{
				return (HtmlTableRow)this.cc[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object to the end of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to add to the collection. </param>
		// Token: 0x06001A13 RID: 6675 RVA: 0x00045691 File Offset: 0x00043891
		public void Add(HtmlTableRow row)
		{
			this.cc.Add(row);
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		// Token: 0x06001A14 RID: 6676 RVA: 0x0004569F File Offset: 0x0004389F
		public void Clear()
		{
			this.cc.Clear();
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection to the specified <see cref="T:System.Array" /> object, starting at the specified index in the array.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
		/// <param name="index">The first index in the specified array to receive the items. </param>
		// Token: 0x06001A15 RID: 6677 RVA: 0x000456AC File Offset: 0x000438AC
		public void CopyTo(Array array, int index)
		{
			this.cc.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />.</returns>
		// Token: 0x06001A16 RID: 6678 RVA: 0x000456BB File Offset: 0x000438BB
		public IEnumerator GetEnumerator()
		{
			return this.cc.GetEnumerator();
		}

		/// <summary>Adds an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object to the specified location in the collection.</summary>
		/// <param name="index">The location in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> at which to add the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />. </param>
		/// <param name="row">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to add to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
		// Token: 0x06001A17 RID: 6679 RVA: 0x000456C8 File Offset: 0x000438C8
		public void Insert(int index, HtmlTableRow row)
		{
			this.cc.AddAt(index, row);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
		// Token: 0x06001A18 RID: 6680 RVA: 0x000456D7 File Offset: 0x000438D7
		public void Remove(HtmlTableRow row)
		{
			this.cc.Remove(row);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is outside the range of index values in the collection. </exception>
		// Token: 0x06001A19 RID: 6681 RVA: 0x000456E5 File Offset: 0x000438E5
		public void RemoveAt(int index)
		{
			this.cc.RemoveAt(index);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal HtmlTableRowCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400164A RID: 5706
		private ControlCollection cc;
	}
}
