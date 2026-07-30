using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants indicating when the error icon, supplied by an <see cref="T:System.Windows.Forms.ErrorProvider" />, should blink to alert the user that an error has occurred.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015D RID: 349
	public enum ErrorBlinkStyle
	{
		/// <summary>Blinks when the icon is already displayed and a new error string is set for the control.</summary>
		// Token: 0x04000D05 RID: 3333
		BlinkIfDifferentError,
		/// <summary>Always blink when the error icon is first displayed, or when a error description string is set for the control and the error icon is already displayed.</summary>
		// Token: 0x04000D06 RID: 3334
		AlwaysBlink,
		/// <summary>Never blink the error icon.</summary>
		// Token: 0x04000D07 RID: 3335
		NeverBlink
	}
}
