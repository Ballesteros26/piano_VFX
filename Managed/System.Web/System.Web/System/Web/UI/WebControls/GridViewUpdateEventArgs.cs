using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowUpdating" /> event.</summary>
	// Token: 0x020003AA RID: 938
	public class GridViewUpdateEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewUpdateEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row being updated.</param>
		// Token: 0x06002644 RID: 9796 RVA: 0x000647AC File Offset: 0x000629AC
		public GridViewUpdateEventArgs(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000647BB File Offset: 0x000629BB
		internal GridViewUpdateEventArgs(int rowIndex, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
		{
			this.rowIndex = rowIndex;
			this.keys = keys;
			this.newValues = newValues;
			this.oldValues = oldValues;
		}

		/// <summary>Gets the index of the row being updated.</summary>
		/// <returns>The index of the row being updated.</returns>
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x000647E0 File Offset: 0x000629E0
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets a dictionary of field name/value pairs that represent the primary key of the row to update.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object containing field name/value pairs that represent the primary key of the row to update.</returns>
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x000647E8 File Offset: 0x000629E8
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary containing the revised values of the non-key field name/value pairs in the row to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object containing the revised values of the non-key field name/value pairs in the row to update.</returns>
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x000647F0 File Offset: 0x000629F0
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary containing the original field name/value pairs in the row to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains the original values of the field name/value pairs in the row to update.</returns>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x000647F8 File Offset: 0x000629F8
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x04001A44 RID: 6724
		private int rowIndex;

		// Token: 0x04001A45 RID: 6725
		private IOrderedDictionary keys;

		// Token: 0x04001A46 RID: 6726
		private IOrderedDictionary newValues;

		// Token: 0x04001A47 RID: 6727
		private IOrderedDictionary oldValues;
	}
}
