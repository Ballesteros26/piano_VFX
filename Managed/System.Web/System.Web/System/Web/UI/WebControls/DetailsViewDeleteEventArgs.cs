using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemDeleting" /> event. </summary>
	// Token: 0x02000388 RID: 904
	public class DetailsViewDeleteEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewDeleteEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row being deleted.</param>
		// Token: 0x06002396 RID: 9110 RVA: 0x0005CCD9 File Offset: 0x0005AED9
		public DetailsViewDeleteEventArgs(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0005CCE8 File Offset: 0x0005AEE8
		internal DetailsViewDeleteEventArgs(int index, IOrderedDictionary keys, IOrderedDictionary values)
		{
			this.rowIndex = index;
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the index of the row being deleted.</summary>
		/// <returns>The index of the row being deleted.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x0005CD05 File Offset: 0x0005AF05
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets an ordered dictionary of key field name/value pairs that contains the names and values of the key fields of the deleted items.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains an ordered dictionary of key field name/value pairs used to match the item to delete.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x0005CD0D File Offset: 0x0005AF0D
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the item to delete.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the item to delete.</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x0005CD15 File Offset: 0x0005AF15
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001976 RID: 6518
		private int rowIndex;

		// Token: 0x04001977 RID: 6519
		private IOrderedDictionary keys;

		// Token: 0x04001978 RID: 6520
		private IOrderedDictionary values;
	}
}
