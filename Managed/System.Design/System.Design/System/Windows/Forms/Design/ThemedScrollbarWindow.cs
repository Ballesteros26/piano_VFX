using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Represents a window and a value that indicates how its scrollbars should be themed when displayed in the Visual Studio designer. </summary>
	// Token: 0x02000179 RID: 377
	public struct ThemedScrollbarWindow
	{
		/// <summary>The window handle.</summary>
		// Token: 0x0400029F RID: 671
		public IntPtr Handle;

		/// <summary>A value that indicates how the window scrollbars should be themed when displayed in the Visual Studio designer.</summary>
		// Token: 0x040002A0 RID: 672
		public ThemedScrollbarMode Mode;
	}
}
