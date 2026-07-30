using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a control should be docked by default when added through a designer.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200014C RID: 332
	public enum DockingBehavior
	{
		/// <summary>Do not prompt the user for the desired docking behavior.</summary>
		// Token: 0x04000CAB RID: 3243
		Never,
		/// <summary>Prompt the user for the desired docking behavior.</summary>
		// Token: 0x04000CAC RID: 3244
		Ask,
		/// <summary>Set the control's <see cref="P:System.Windows.Forms.Control.Dock" /> property to <see cref="F:System.Windows.Forms.DockStyle.Fill" />  when it is dropped into a container with no other child controls.</summary>
		// Token: 0x04000CAD RID: 3245
		AutoDock
	}
}
