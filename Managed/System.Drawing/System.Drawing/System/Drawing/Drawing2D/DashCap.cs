using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the type of graphic shape to use on both ends of each dash in a dashed line.</summary>
	// Token: 0x0200012D RID: 301
	public enum DashCap
	{
		/// <summary>Specifies a square cap that squares off both ends of each dash.</summary>
		// Token: 0x04000A8F RID: 2703
		Flat,
		/// <summary>Specifies a circular cap that rounds off both ends of each dash.</summary>
		// Token: 0x04000A90 RID: 2704
		Round = 2,
		/// <summary>Specifies a triangular cap that points both ends of each dash.</summary>
		// Token: 0x04000A91 RID: 2705
		Triangle
	}
}
