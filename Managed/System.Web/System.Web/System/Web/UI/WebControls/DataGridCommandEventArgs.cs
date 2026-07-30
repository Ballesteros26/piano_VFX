using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.CancelCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.DeleteCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.EditCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCommand" />, and <see cref="E:System.Web.UI.WebControls.DataGrid.UpdateCommand" /> events of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000291 RID: 657
	public class DataGridCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> class.</summary>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" />. </param>
		/// <param name="commandSource">The source of the command. </param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06001A86 RID: 6790 RVA: 0x00045DF3 File Offset: 0x00043FF3
		public DataGridCommandEventArgs(DataGridItem item, object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>The source of the command.</returns>
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00045E0A File Offset: 0x0004400A
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		/// <summary>Gets the item containing the command source in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00045E12 File Offset: 0x00044012
		public DataGridItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x040016A8 RID: 5800
		private DataGridItem item;

		// Token: 0x040016A9 RID: 5801
		private object commandSource;
	}
}
