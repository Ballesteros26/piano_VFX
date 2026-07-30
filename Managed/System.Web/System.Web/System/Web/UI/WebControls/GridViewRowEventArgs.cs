using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowCreated" /> and <see cref="E:System.Web.UI.WebControls.GridView.RowDataBound" /> events.</summary>
	// Token: 0x020002C5 RID: 709
	public class GridViewRowEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewRowEventArgs" /> class.</summary>
		/// <param name="row">A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the row being created or data-bound. </param>
		// Token: 0x06001B32 RID: 6962 RVA: 0x0004603C File Offset: 0x0004423C
		public GridViewRowEventArgs(GridViewRow row)
		{
			this._row = row;
		}

		/// <summary>Gets the row being created or data-bound.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the row being created or data-bound.</returns>
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0004604B File Offset: 0x0004424B
		public GridViewRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x040016E8 RID: 5864
		private GridViewRow _row;
	}
}
