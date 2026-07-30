using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the button style within a toolbar.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200033A RID: 826
	public enum ToolBarButtonStyle
	{
		/// <summary>A standard, three-dimensional button.</summary>
		// Token: 0x04001A0D RID: 6669
		PushButton = 1,
		/// <summary>A toggle button that appears sunken when clicked and retains the sunken appearance until clicked again.</summary>
		// Token: 0x04001A0E RID: 6670
		ToggleButton,
		/// <summary>A space or line between toolbar buttons. The appearance depends on the value of the <see cref="P:System.Windows.Forms.ToolBar.Appearance" /> property.</summary>
		// Token: 0x04001A0F RID: 6671
		Separator,
		/// <summary>A drop-down control that displays a menu or other window when clicked.</summary>
		// Token: 0x04001A10 RID: 6672
		DropDownButton
	}
}
