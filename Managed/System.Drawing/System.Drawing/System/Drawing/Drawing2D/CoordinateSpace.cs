using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the system to use when evaluating coordinates.</summary>
	// Token: 0x02000135 RID: 309
	public enum CoordinateSpace
	{
		/// <summary>Specifies that coordinates are in the world coordinate context. World coordinates are used in a nonphysical environment, such as a modeling environment.</summary>
		// Token: 0x04000AAE RID: 2734
		World,
		/// <summary>Specifies that coordinates are in the page coordinate context. Their units are defined by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, and must be one of the elements of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration.</summary>
		// Token: 0x04000AAF RID: 2735
		Page,
		/// <summary>Specifies that coordinates are in the device coordinate context. On a computer screen the device coordinates are usually measured in pixels.</summary>
		// Token: 0x04000AB0 RID: 2736
		Device
	}
}
