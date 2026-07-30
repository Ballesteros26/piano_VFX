using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants defining which information to display.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200025B RID: 603
	public enum MessageBoxIcon
	{
		/// <summary>The message box contain no symbols.</summary>
		// Token: 0x040013B3 RID: 5043
		None,
		/// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
		// Token: 0x040013B4 RID: 5044
		Error = 16,
		/// <summary>The message box contains a symbol consisting of a white X in a circle with a red background.</summary>
		// Token: 0x040013B5 RID: 5045
		Hand = 16,
		/// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
		// Token: 0x040013B6 RID: 5046
		Stop = 16,
		/// <summary>The message box contains a symbol consisting of a question mark in a circle. The question-mark message icon is no longer recommended because it does not clearly represent a specific type of message and because the phrasing of a message as a question could apply to any message type. In addition, users can confuse the message symbol question mark with Help information. Therefore, do not use this question mark message symbol in your message boxes. The system continues to support its inclusion only for backward compatibility.</summary>
		// Token: 0x040013B7 RID: 5047
		Question = 32,
		/// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
		// Token: 0x040013B8 RID: 5048
		Exclamation = 48,
		/// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
		// Token: 0x040013B9 RID: 5049
		Warning = 48,
		/// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
		// Token: 0x040013BA RID: 5050
		Asterisk = 64,
		/// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
		// Token: 0x040013BB RID: 5051
		Information = 64
	}
}
