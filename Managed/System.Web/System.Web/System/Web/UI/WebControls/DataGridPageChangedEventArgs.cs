using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.PageIndexChanged" /> event of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000295 RID: 661
	public class DataGridPageChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridPageChangedEventArgs" /> class.</summary>
		/// <param name="commandSource">The source of the command. </param>
		/// <param name="newPageIndex">The index of the page selected by the user from the page selection element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. </param>
		// Token: 0x06001A93 RID: 6803 RVA: 0x00045E31 File Offset: 0x00044031
		public DataGridPageChangedEventArgs(object commandSource, int newPageIndex)
		{
			this.commandSource = commandSource;
			this.newPageIndex = newPageIndex;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>The source of the command.</returns>
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x00045E47 File Offset: 0x00044047
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		/// <summary>Gets the index of the page selected by the user in the page selection element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The index of the page selected by the user in the page selection element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00045E4F File Offset: 0x0004404F
		public int NewPageIndex
		{
			get
			{
				return this.newPageIndex;
			}
		}

		// Token: 0x040016AB RID: 5803
		private object commandSource;

		// Token: 0x040016AC RID: 5804
		private int newPageIndex;
	}
}
