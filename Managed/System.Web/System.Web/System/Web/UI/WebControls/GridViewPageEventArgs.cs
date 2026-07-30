using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.PageIndexChanging" /> event.</summary>
	// Token: 0x020002C3 RID: 707
	public class GridViewPageEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewPageEventArgs" /> class.</summary>
		/// <param name="newPageIndex">The index of the new page to display. </param>
		// Token: 0x06001B2B RID: 6955 RVA: 0x0004600D File Offset: 0x0004420D
		public GridViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		/// <summary>Gets or sets the index of the new page to display in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The index of the new page to display in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.GridViewPageEventArgs.NewPageIndex" /> property is less than zero.</exception>
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0004601C File Offset: 0x0004421C
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x00046024 File Offset: 0x00044224
		public int NewPageIndex
		{
			get
			{
				return this._newPageIndex;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._newPageIndex = value;
			}
		}

		// Token: 0x040016E7 RID: 5863
		private int _newPageIndex;
	}
}
