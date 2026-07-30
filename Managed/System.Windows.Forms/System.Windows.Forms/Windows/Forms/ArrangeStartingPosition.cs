using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the starting position that the system uses to arrange minimized windows.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000043 RID: 67
	[Flags]
	public enum ArrangeStartingPosition
	{
		/// <summary>Starts at the lower-left corner of the screen, which is the default position.</summary>
		// Token: 0x040005CC RID: 1484
		BottomLeft = 0,
		/// <summary>Starts at the lower-right corner of the screen.</summary>
		// Token: 0x040005CD RID: 1485
		BottomRight = 1,
		/// <summary>Starts at the upper-left corner of the screen.</summary>
		// Token: 0x040005CE RID: 1486
		TopLeft = 2,
		/// <summary>Starts at the upper-right corner of the screen.</summary>
		// Token: 0x040005CF RID: 1487
		TopRight = 3,
		/// <summary>Hides minimized windows by moving them off the visible area of the screen.</summary>
		// Token: 0x040005D0 RID: 1488
		Hide = 8
	}
}
