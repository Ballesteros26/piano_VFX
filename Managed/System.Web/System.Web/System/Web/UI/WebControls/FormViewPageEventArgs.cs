using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.PageIndexChanging" /> event.</summary>
	// Token: 0x020002B6 RID: 694
	public class FormViewPageEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewPageEventArgs" /> class.</summary>
		/// <param name="newPageIndex">The index of the new page to display.</param>
		// Token: 0x06001AFD RID: 6909 RVA: 0x00045F6E File Offset: 0x0004416E
		public FormViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		/// <summary>Gets or sets the index of the new page to display in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The index of the new page to display in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x00045F7D File Offset: 0x0004417D
		// (set) Token: 0x06001AFF RID: 6911 RVA: 0x00045F85 File Offset: 0x00044185
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

		// Token: 0x040016DC RID: 5852
		private int _newPageIndex;
	}
}
