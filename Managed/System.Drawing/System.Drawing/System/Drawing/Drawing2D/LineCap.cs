using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the available cap styles with which a <see cref="T:System.Drawing.Pen" /> object can end a line.</summary>
	// Token: 0x02000140 RID: 320
	public enum LineCap
	{
		/// <summary>Specifies a flat line cap.</summary>
		// Token: 0x04000B09 RID: 2825
		Flat,
		/// <summary>Specifies a square line cap.</summary>
		// Token: 0x04000B0A RID: 2826
		Square,
		/// <summary>Specifies a round line cap.</summary>
		// Token: 0x04000B0B RID: 2827
		Round,
		/// <summary>Specifies a triangular line cap.</summary>
		// Token: 0x04000B0C RID: 2828
		Triangle,
		/// <summary>Specifies no anchor.</summary>
		// Token: 0x04000B0D RID: 2829
		NoAnchor = 16,
		/// <summary>Specifies a square anchor line cap.</summary>
		// Token: 0x04000B0E RID: 2830
		SquareAnchor,
		/// <summary>Specifies a round anchor cap.</summary>
		// Token: 0x04000B0F RID: 2831
		RoundAnchor,
		/// <summary>Specifies a diamond anchor cap.</summary>
		// Token: 0x04000B10 RID: 2832
		DiamondAnchor,
		/// <summary>Specifies an arrow-shaped anchor cap.</summary>
		// Token: 0x04000B11 RID: 2833
		ArrowAnchor,
		/// <summary>Specifies a custom line cap.</summary>
		// Token: 0x04000B12 RID: 2834
		Custom = 255,
		/// <summary>Specifies a mask used to check whether a line cap is an anchor cap.</summary>
		// Token: 0x04000B13 RID: 2835
		AnchorMask = 240
	}
}
