using System;

namespace System.Windows.Forms
{
	/// <summary>Determines how a control validates its data when it loses user input focus.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004C RID: 76
	public enum AutoValidate
	{
		/// <summary>The control inherits its <see cref="T:System.Windows.Forms.AutoValidate" /> behavior from its container (such as a form or another control). If there is no container control, it defaults to <see cref="F:System.Windows.Forms.AutoValidate.EnablePreventFocusChange" />.</summary>
		// Token: 0x040005FA RID: 1530
		Inherit = -1,
		/// <summary>Implicit validation will not occur. Setting this value will not interfere with explicit calls to <see cref="M:System.Windows.Forms.ContainerControl.Validate" /> or <see cref="M:System.Windows.Forms.ContainerControl.ValidateChildren" />.</summary>
		// Token: 0x040005FB RID: 1531
		Disable,
		/// <summary>Implicit validation occurs when the control loses focus.</summary>
		// Token: 0x040005FC RID: 1532
		EnablePreventFocusChange,
		/// <summary>Implicit validation occurs, but if validation fails, focus will still change to the new control. If validation fails, the <see cref="E:System.Windows.Forms.Control.Validated" /> event will not fire.</summary>
		// Token: 0x040005FD RID: 1533
		EnableAllowFocusChange
	}
}
