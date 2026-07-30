using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.DataGrid.ItemDataBound" /> events of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000293 RID: 659
	public class DataGridItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridItemEventArgs" /> class.</summary>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that represents an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" />. </param>
		// Token: 0x06001A8D RID: 6797 RVA: 0x00045E1A File Offset: 0x0004401A
		public DataGridItemEventArgs(DataGridItem item)
		{
			this.item = item;
		}

		/// <summary>Gets the referenced item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when the event is raised.</summary>
		/// <returns>The referenced item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when the event is raised.</returns>
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x00045E29 File Offset: 0x00044029
		public DataGridItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x040016AA RID: 5802
		private DataGridItem item;
	}
}
