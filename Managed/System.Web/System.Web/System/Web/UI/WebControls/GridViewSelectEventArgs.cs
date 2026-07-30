using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.SelectedIndexChanging" /> event.</summary>
	// Token: 0x020002C7 RID: 711
	public class GridViewSelectEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewSelectEventArgs" /> class.</summary>
		/// <param name="newSelectedIndex">The index of the new row to select in the <see cref="T:System.Web.UI.WebControls.GridView" /> control. </param>
		// Token: 0x06001B38 RID: 6968 RVA: 0x00046053 File Offset: 0x00044253
		public GridViewSelectEventArgs(int newSelectedIndex)
		{
			this._newSelectedIndex = newSelectedIndex;
		}

		/// <summary>Gets or sets the index of the new row to select in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The index of the new row to select in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00046062 File Offset: 0x00044262
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x0004606A File Offset: 0x0004426A
		public int NewSelectedIndex
		{
			get
			{
				return this._newSelectedIndex;
			}
			set
			{
				this._newSelectedIndex = value;
			}
		}

		// Token: 0x040016E9 RID: 5865
		private int _newSelectedIndex;
	}
}
