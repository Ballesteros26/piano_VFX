using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the state of a row in a data control, such as <see cref="T:System.Web.UI.WebControls.DetailsView" /> or <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
	// Token: 0x0200028F RID: 655
	[Flags]
	public enum DataControlRowState
	{
		/// <summary>Indicates that the data control row is in a normal state. The <see cref="F:System.Web.UI.WebControls.DataControlRowState.Normal" /> state is mutually exclusive with other states except the <see cref="F:System.Web.UI.WebControls.DataControlRowState.Alternate" /> state.</summary>
		// Token: 0x0400169C RID: 5788
		Normal = 0,
		/// <summary>Indicates that the data control row is an alternate row. </summary>
		// Token: 0x0400169D RID: 5789
		Alternate = 1,
		/// <summary>Indicates that the row has been selected by the user.</summary>
		// Token: 0x0400169E RID: 5790
		Selected = 2,
		/// <summary>Indicates that the row is in an edit state, often the result of clicking an edit button for the row. Typically, the <see cref="F:System.Web.UI.WebControls.DataControlRowState.Edit" /> and <see cref="F:System.Web.UI.WebControls.DataControlRowState.Insert" /> states are mutually exclusive.</summary>
		// Token: 0x0400169F RID: 5791
		Edit = 4,
		/// <summary>Indicates that the row is a new row, often the result of clicking an insert button to add a new row. Typically, the <see cref="F:System.Web.UI.WebControls.DataControlRowState.Insert" /> and <see cref="F:System.Web.UI.WebControls.DataControlRowState.Edit" /> states are mutually exclusive.</summary>
		// Token: 0x040016A0 RID: 5792
		Insert = 8
	}
}
