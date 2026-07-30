using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the behaviors of a link in a <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000207 RID: 519
	public enum LinkBehavior
	{
		/// <summary>The behavior of this setting depends on the options set using the Internet Options dialog box in Control Panel or Internet Explorer.</summary>
		// Token: 0x0400116C RID: 4460
		SystemDefault,
		/// <summary>The link always displays with underlined text.</summary>
		// Token: 0x0400116D RID: 4461
		AlwaysUnderline,
		/// <summary>The link displays underlined text only when the mouse is hovered over the link text.</summary>
		// Token: 0x0400116E RID: 4462
		HoverUnderline,
		/// <summary>The link text is never underlined. The link can still be distinguished from other text by use of the <see cref="P:System.Windows.Forms.LinkLabel.LinkColor" /> property of the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		// Token: 0x0400116F RID: 4463
		NeverUnderline
	}
}
