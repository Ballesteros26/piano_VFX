using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style for a section of a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	// Token: 0x02000423 RID: 1059
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableSectionStyle : Style
	{
		/// <summary>Gets or sets a value indicating whether the table section is displayed.</summary>
		/// <returns>true if the table section is displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x0007E038 File Offset: 0x0007C238
		// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x0007E061 File Offset: 0x0007C261
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				object obj = base.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}
	}
}
