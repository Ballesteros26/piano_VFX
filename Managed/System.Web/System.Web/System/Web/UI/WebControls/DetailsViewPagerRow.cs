using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the pager row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	// Token: 0x0200038C RID: 908
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewPagerRow : DetailsViewRow, INamingContainer, INonBindingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewPagerRow" /> class.</summary>
		/// <param name="rowIndex">The index of the row in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
		// Token: 0x060023B0 RID: 9136 RVA: 0x0005CE28 File Offset: 0x0005B028
		[global::System.MonoTODO("why this class exists at all?")]
		public DetailsViewPagerRow(int rowIndex, DataControlRowType rowType, DataControlRowState rowState)
			: base(rowIndex, rowType, rowState)
		{
		}
	}
}
