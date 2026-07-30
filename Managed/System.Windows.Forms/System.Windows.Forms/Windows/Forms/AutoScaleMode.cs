using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the different types of automatic scaling modes supported by Windows Forms.</summary>
	// Token: 0x0200004A RID: 74
	public enum AutoScaleMode
	{
		/// <summary>Automatic scaling is disabled.</summary>
		// Token: 0x040005F2 RID: 1522
		None,
		/// <summary>Controls scale relative to the dimensions of the font the classes are using, which is typically the system font.</summary>
		// Token: 0x040005F3 RID: 1523
		Font,
		/// <summary>Controls scale relative to the display resolution. Common resolutions are 96 and 120 DPI.</summary>
		// Token: 0x040005F4 RID: 1524
		Dpi,
		/// <summary>Controls scale according to the classes' parent's scaling mode. If there is no parent, automatic scaling is disabled.</summary>
		// Token: 0x040005F5 RID: 1525
		Inherit
	}
}
