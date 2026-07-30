using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowCancelingEdit" /> event.</summary>
	// Token: 0x020002BB RID: 699
	public class GridViewCancelEditEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewCancelEditEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row containing the Cancel button that raised the event. </param>
		// Token: 0x06001B0C RID: 6924 RVA: 0x00045F8E File Offset: 0x0004418E
		public GridViewCancelEditEventArgs(int rowIndex)
		{
			this._rowIndex = rowIndex;
		}

		/// <summary>Gets the index of the row containing the Cancel button that raised the event.</summary>
		/// <returns>The zero-based index of the row containing the Cancel button that raised the event.</returns>
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00045F9D File Offset: 0x0004419D
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x040016E2 RID: 5858
		private int _rowIndex;
	}
}
