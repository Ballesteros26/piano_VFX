using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the image to draw when drawing a menu with the <see cref="M:System.Windows.Forms.ControlPaint.DrawMenuGlyph(System.Drawing.Graphics,System.Drawing.Rectangle,System.Windows.Forms.MenuGlyph)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200024F RID: 591
	public enum MenuGlyph
	{
		/// <summary>Draws a submenu arrow.</summary>
		// Token: 0x04001357 RID: 4951
		Arrow,
		/// <summary>The minimum value available by this enumeration (equal to the <see cref="F:System.Windows.Forms.MenuGlyph.Arrow" /> value).</summary>
		// Token: 0x04001358 RID: 4952
		Min = 0,
		/// <summary>Draws a menu check mark.</summary>
		// Token: 0x04001359 RID: 4953
		Checkmark,
		/// <summary>Draws a menu bullet.</summary>
		// Token: 0x0400135A RID: 4954
		Bullet,
		/// <summary>The maximum value available by this enumeration (equal to the <see cref="F:System.Windows.Forms.MenuGlyph.Bullet" /> value).</summary>
		// Token: 0x0400135B RID: 4955
		Max = 2
	}
}
