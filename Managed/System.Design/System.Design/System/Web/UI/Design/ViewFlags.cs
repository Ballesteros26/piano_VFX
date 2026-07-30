using System;

namespace System.Web.UI.Design
{
	/// <summary>Indicates which features are enabled with the <see cref="M:System.Web.UI.Design.ControlDesigner.SetViewFlags(System.Web.UI.Design.ViewFlags,System.Boolean)" /> method of a designer.</summary>
	// Token: 0x020000B6 RID: 182
	[Flags]
	public enum ViewFlags
	{
		/// <summary>Enables painting events for displaying a custom control at design time.</summary>
		// Token: 0x04000145 RID: 325
		CustomPaint = 1,
		/// <summary>Postpones all events until after the control is completely loaded.</summary>
		// Token: 0x04000146 RID: 326
		DesignTimeHtmlRequiresLoadComplete = 2,
		/// <summary>Enables template editing at design time.</summary>
		// Token: 0x04000147 RID: 327
		TemplateEditing = 4
	}
}
