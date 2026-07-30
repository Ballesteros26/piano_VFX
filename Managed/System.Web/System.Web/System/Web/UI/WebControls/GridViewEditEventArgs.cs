using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowEditing" /> event.</summary>
	// Token: 0x020002C1 RID: 705
	public class GridViewEditEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewEditEventArgs" /> class.</summary>
		/// <param name="newEditIndex">The index of the row to edit. </param>
		// Token: 0x06001B24 RID: 6948 RVA: 0x00045FED File Offset: 0x000441ED
		public GridViewEditEventArgs(int newEditIndex)
		{
			this._newEditIndex = newEditIndex;
		}

		/// <summary>Gets or sets the index of the row being edited.</summary>
		/// <returns>The index of the row being edited.</returns>
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00045FFC File Offset: 0x000441FC
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x00046004 File Offset: 0x00044204
		public int NewEditIndex
		{
			get
			{
				return this._newEditIndex;
			}
			set
			{
				this._newEditIndex = value;
			}
		}

		// Token: 0x040016E6 RID: 5862
		private int _newEditIndex;
	}
}
