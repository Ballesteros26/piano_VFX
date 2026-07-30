using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the state of the user interface.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039C RID: 924
	[Flags]
	public enum UICues
	{
		/// <summary>No change was made.</summary>
		// Token: 0x04001C65 RID: 7269
		None = 0,
		/// <summary>Focus rectangles are displayed after the change.</summary>
		// Token: 0x04001C66 RID: 7270
		ShowFocus = 1,
		/// <summary>Keyboard cues are underlined after the change.</summary>
		// Token: 0x04001C67 RID: 7271
		ShowKeyboard = 2,
		/// <summary>Focus rectangles are displayed and keyboard cues are underlined after the change.</summary>
		// Token: 0x04001C68 RID: 7272
		Shown = 3,
		/// <summary>The state of the focus cues has changed.</summary>
		// Token: 0x04001C69 RID: 7273
		ChangeFocus = 4,
		/// <summary>The state of the keyboard cues has changed.</summary>
		// Token: 0x04001C6A RID: 7274
		ChangeKeyboard = 8,
		/// <summary>The state of the focus cues and keyboard cues has changed.</summary>
		// Token: 0x04001C6B RID: 7275
		Changed = 12
	}
}
