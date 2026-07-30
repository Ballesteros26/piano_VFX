using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowDeleting" /> event.</summary>
	// Token: 0x020003A6 RID: 934
	public class GridViewDeleteEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewDeleteEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row that contains the Delete button that raised the event. </param>
		// Token: 0x06002621 RID: 9761 RVA: 0x000645EF File Offset: 0x000627EF
		public GridViewDeleteEventArgs(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000645FE File Offset: 0x000627FE
		internal GridViewDeleteEventArgs(int index, IOrderedDictionary keys, IOrderedDictionary values)
		{
			this.rowIndex = index;
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the index of the row being deleted.</summary>
		/// <returns>The zero-based index of the row being deleted.</returns>
		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x0006461B File Offset: 0x0006281B
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets a dictionary of field name/value pairs that represent the primary key of the row to delete.</summary>
		/// <returns>A dictionary that contains field name/value pairs that represent the primary key of the row to delete.</returns>
		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x00064623 File Offset: 0x00062823
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the row to delete.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains the non-key field name/value pairs of the row to delete.</returns>
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x0006462B File Offset: 0x0006282B
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001A36 RID: 6710
		private int rowIndex;

		// Token: 0x04001A37 RID: 6711
		private IOrderedDictionary keys;

		// Token: 0x04001A38 RID: 6712
		private IOrderedDictionary values;
	}
}
