using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemDeleting" /> event.</summary>
	// Token: 0x0200039D RID: 925
	public class FormViewDeleteEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewDeleteEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row being deleted.</param>
		// Token: 0x06002507 RID: 9479 RVA: 0x00060997 File Offset: 0x0005EB97
		public FormViewDeleteEventArgs(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000609A6 File Offset: 0x0005EBA6
		internal FormViewDeleteEventArgs(int index, IOrderedDictionary keys, IOrderedDictionary values)
			: this(index)
		{
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the index of the record being deleted from the data source.</summary>
		/// <returns>The index of the record being deleted from the data source.</returns>
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x000609BD File Offset: 0x0005EBBD
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets an ordered dictionary of key field name/value pairs for the record to delete.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains the key field name/value pairs for the record to delete.</returns>
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x000609C5 File Offset: 0x0005EBC5
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the item to delete.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains the non-key field name/value pairs for the item to delete.</returns>
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x000609CD File Offset: 0x0005EBCD
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040019DC RID: 6620
		private int rowIndex;

		// Token: 0x040019DD RID: 6621
		private IOrderedDictionary keys;

		// Token: 0x040019DE RID: 6622
		private IOrderedDictionary values;
	}
}
