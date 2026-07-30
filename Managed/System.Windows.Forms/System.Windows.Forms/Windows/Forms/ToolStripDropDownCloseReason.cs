using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the reason that a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed.</summary>
	// Token: 0x0200034A RID: 842
	public enum ToolStripDropDownCloseReason
	{
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because another application has received the focus.</summary>
		// Token: 0x04001A71 RID: 6769
		AppFocusChange,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because an application was launched.</summary>
		// Token: 0x04001A72 RID: 6770
		AppClicked,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because one of its items was clicked.</summary>
		// Token: 0x04001A73 RID: 6771
		ItemClicked,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because of keyboard activity, such as the ESC key being pressed.</summary>
		// Token: 0x04001A74 RID: 6772
		Keyboard,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because the <see cref="M:System.Windows.Forms.ToolStripDropDown.Close" /> method was called.</summary>
		// Token: 0x04001A75 RID: 6773
		CloseCalled
	}
}
