using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers that indicate the display modes used by <see cref="T:System.ComponentModel.Design.ByteViewer" />.</summary>
	// Token: 0x02000122 RID: 290
	public enum DisplayMode
	{
		/// <summary>An ANSI format display.</summary>
		// Token: 0x040001EE RID: 494
		Ansi = 2,
		/// <summary>A display mode that automatically selects a display mode. In this mode, the bytes are examined to determine if they are hexadecimal or printable. If the bytes are in hexadecimal format, the <see cref="F:System.ComponentModel.Design.DisplayMode.Hexdump" /> mode is selected. If the characters match a printable character set, a test is run to automatically select either the <see cref="F:System.ComponentModel.Design.DisplayMode.Ansi" /> or <see cref="F:System.ComponentModel.Design.DisplayMode.Unicode" /> display mode.</summary>
		// Token: 0x040001EF RID: 495
		Auto = 4,
		/// <summary>A hexadecimal format display.</summary>
		// Token: 0x040001F0 RID: 496
		Hexdump = 1,
		/// <summary>A Unicode format display.</summary>
		// Token: 0x040001F1 RID: 497
		Unicode = 3
	}
}
