using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies a value indicating whether the text appears from right to left, such as when using Hebrew or Arabic fonts.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002C0 RID: 704
	public enum RightToLeft
	{
		/// <summary>The text reads from left to right. This is the default.</summary>
		// Token: 0x04001671 RID: 5745
		No,
		/// <summary>The text reads from right to left.</summary>
		// Token: 0x04001672 RID: 5746
		Yes,
		/// <summary>The direction the text read is inherited from the parent control.</summary>
		// Token: 0x04001673 RID: 5747
		Inherit
	}
}
