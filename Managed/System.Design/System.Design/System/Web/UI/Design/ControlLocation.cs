using System;

namespace System.Web.UI.Design
{
	/// <summary>Specifies the possible locations for adding a control in a container.</summary>
	// Token: 0x0200005C RID: 92
	public enum ControlLocation
	{
		/// <summary>Adds the control before the current selection or current control.</summary>
		// Token: 0x04000122 RID: 290
		Before,
		/// <summary>Adds the control after the current selection or current control.</summary>
		// Token: 0x04000123 RID: 291
		After,
		/// <summary>Adds the control at the start of the document.</summary>
		// Token: 0x04000124 RID: 292
		First,
		/// <summary>Adds the control at the end of the document.</summary>
		// Token: 0x04000125 RID: 293
		Last,
		/// <summary>Adds the control as the first child of the selected control, if the selected control is a container control.</summary>
		// Token: 0x04000126 RID: 294
		FirstChild,
		/// <summary>Adds the control as the last child of the selected control, if the selected control is a container control.</summary>
		// Token: 0x04000127 RID: 295
		LastChild
	}
}
