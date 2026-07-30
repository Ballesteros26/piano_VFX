using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.PageIndexChanging" /> event.</summary>
	// Token: 0x020002A8 RID: 680
	public class DetailsViewPageEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewPageEventArgs" /> class.</summary>
		/// <param name="newPageIndex">The index of the new page to display.</param>
		// Token: 0x06001AD2 RID: 6866 RVA: 0x00045F1E File Offset: 0x0004411E
		public DetailsViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		/// <summary>Gets or sets the index of the new page to display in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The index of the new page to display in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x00045F2D File Offset: 0x0004412D
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x00045F35 File Offset: 0x00044135
		public int NewPageIndex
		{
			get
			{
				return this._newPageIndex;
			}
			set
			{
				this._newPageIndex = value;
			}
		}

		// Token: 0x040016C0 RID: 5824
		private int _newPageIndex;
	}
}
