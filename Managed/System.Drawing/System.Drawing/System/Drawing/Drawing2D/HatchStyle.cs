using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the different patterns available for <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> objects.</summary>
	// Token: 0x0200013E RID: 318
	public enum HatchStyle
	{
		/// <summary>A pattern of horizontal lines.</summary>
		// Token: 0x04000AC6 RID: 2758
		Horizontal,
		/// <summary>A pattern of vertical lines.</summary>
		// Token: 0x04000AC7 RID: 2759
		Vertical,
		/// <summary>A pattern of lines on a diagonal from upper left to lower right.</summary>
		// Token: 0x04000AC8 RID: 2760
		ForwardDiagonal,
		/// <summary>A pattern of lines on a diagonal from upper right to lower left.</summary>
		// Token: 0x04000AC9 RID: 2761
		BackwardDiagonal,
		/// <summary>Specifies horizontal and vertical lines that cross.</summary>
		// Token: 0x04000ACA RID: 2762
		Cross,
		/// <summary>A pattern of crisscross diagonal lines.</summary>
		// Token: 0x04000ACB RID: 2763
		DiagonalCross,
		/// <summary>Specifies a 5-percent hatch. The ratio of foreground color to background color is 5:95.</summary>
		// Token: 0x04000ACC RID: 2764
		Percent05,
		/// <summary>Specifies a 10-percent hatch. The ratio of foreground color to background color is 10:90.</summary>
		// Token: 0x04000ACD RID: 2765
		Percent10,
		/// <summary>Specifies a 20-percent hatch. The ratio of foreground color to background color is 20:80.</summary>
		// Token: 0x04000ACE RID: 2766
		Percent20,
		/// <summary>Specifies a 25-percent hatch. The ratio of foreground color to background color is 25:75.</summary>
		// Token: 0x04000ACF RID: 2767
		Percent25,
		/// <summary>Specifies a 30-percent hatch. The ratio of foreground color to background color is 30:70.</summary>
		// Token: 0x04000AD0 RID: 2768
		Percent30,
		/// <summary>Specifies a 40-percent hatch. The ratio of foreground color to background color is 40:60.</summary>
		// Token: 0x04000AD1 RID: 2769
		Percent40,
		/// <summary>Specifies a 50-percent hatch. The ratio of foreground color to background color is 50:50.</summary>
		// Token: 0x04000AD2 RID: 2770
		Percent50,
		/// <summary>Specifies a 60-percent hatch. The ratio of foreground color to background color is 60:40.</summary>
		// Token: 0x04000AD3 RID: 2771
		Percent60,
		/// <summary>Specifies a 70-percent hatch. The ratio of foreground color to background color is 70:30.</summary>
		// Token: 0x04000AD4 RID: 2772
		Percent70,
		/// <summary>Specifies a 75-percent hatch. The ratio of foreground color to background color is 75:25.</summary>
		// Token: 0x04000AD5 RID: 2773
		Percent75,
		/// <summary>Specifies a 80-percent hatch. The ratio of foreground color to background color is 80:100.</summary>
		// Token: 0x04000AD6 RID: 2774
		Percent80,
		/// <summary>Specifies a 90-percent hatch. The ratio of foreground color to background color is 90:10.</summary>
		// Token: 0x04000AD7 RID: 2775
		Percent90,
		/// <summary>Specifies diagonal lines that slant to the right from top points to bottom points and are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, but are not antialiased.</summary>
		// Token: 0x04000AD8 RID: 2776
		LightDownwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the left from top points to bottom points and are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, but they are not antialiased.</summary>
		// Token: 0x04000AD9 RID: 2777
		LightUpwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the right from top points to bottom points, are spaced 50 percent closer together than, and are twice the width of <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />. This hatch pattern is not antialiased.</summary>
		// Token: 0x04000ADA RID: 2778
		DarkDownwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the left from top points to bottom points, are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are twice its width, but the lines are not antialiased.</summary>
		// Token: 0x04000ADB RID: 2779
		DarkUpwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the right from top points to bottom points, have the same spacing as hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, and are triple its width, but are not antialiased.</summary>
		// Token: 0x04000ADC RID: 2780
		WideDownwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the left from top points to bottom points, have the same spacing as hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are triple its width, but are not antialiased.</summary>
		// Token: 0x04000ADD RID: 2781
		WideUpwardDiagonal,
		/// <summary>Specifies vertical lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" />.</summary>
		// Token: 0x04000ADE RID: 2782
		LightVertical,
		/// <summary>Specifies horizontal lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
		// Token: 0x04000ADF RID: 2783
		LightHorizontal,
		/// <summary>Specifies vertical lines that are spaced 75 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" /> (or 25 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.LightVertical" />).</summary>
		// Token: 0x04000AE0 RID: 2784
		NarrowVertical,
		/// <summary>Specifies horizontal lines that are spaced 75 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" /> (or 25 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.LightHorizontal" />).</summary>
		// Token: 0x04000AE1 RID: 2785
		NarrowHorizontal,
		/// <summary>Specifies vertical lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" /> and are twice its width.</summary>
		// Token: 0x04000AE2 RID: 2786
		DarkVertical,
		/// <summary>Specifies horizontal lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" /> and are twice the width of <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
		// Token: 0x04000AE3 RID: 2787
		DarkHorizontal,
		/// <summary>Specifies dashed diagonal lines, that slant to the right from top points to bottom points.</summary>
		// Token: 0x04000AE4 RID: 2788
		DashedDownwardDiagonal,
		/// <summary>Specifies dashed diagonal lines, that slant to the left from top points to bottom points.</summary>
		// Token: 0x04000AE5 RID: 2789
		DashedUpwardDiagonal,
		/// <summary>Specifies dashed horizontal lines.</summary>
		// Token: 0x04000AE6 RID: 2790
		DashedHorizontal,
		/// <summary>Specifies dashed vertical lines.</summary>
		// Token: 0x04000AE7 RID: 2791
		DashedVertical,
		/// <summary>Specifies a hatch that has the appearance of confetti.</summary>
		// Token: 0x04000AE8 RID: 2792
		SmallConfetti,
		/// <summary>Specifies a hatch that has the appearance of confetti, and is composed of larger pieces than <see cref="F:System.Drawing.Drawing2D.HatchStyle.SmallConfetti" />.</summary>
		// Token: 0x04000AE9 RID: 2793
		LargeConfetti,
		/// <summary>Specifies horizontal lines that are composed of zigzags.</summary>
		// Token: 0x04000AEA RID: 2794
		ZigZag,
		/// <summary>Specifies horizontal lines that are composed of tildes.</summary>
		// Token: 0x04000AEB RID: 2795
		Wave,
		/// <summary>Specifies a hatch that has the appearance of layered bricks that slant to the left from top points to bottom points.</summary>
		// Token: 0x04000AEC RID: 2796
		DiagonalBrick,
		/// <summary>Specifies a hatch that has the appearance of horizontally layered bricks.</summary>
		// Token: 0x04000AED RID: 2797
		HorizontalBrick,
		/// <summary>Specifies a hatch that has the appearance of a woven material.</summary>
		// Token: 0x04000AEE RID: 2798
		Weave,
		/// <summary>Specifies a hatch that has the appearance of a plaid material.</summary>
		// Token: 0x04000AEF RID: 2799
		Plaid,
		/// <summary>Specifies a hatch that has the appearance of divots.</summary>
		// Token: 0x04000AF0 RID: 2800
		Divot,
		/// <summary>Specifies horizontal and vertical lines, each of which is composed of dots, that cross.</summary>
		// Token: 0x04000AF1 RID: 2801
		DottedGrid,
		/// <summary>Specifies forward diagonal and backward diagonal lines, each of which is composed of dots, that cross.</summary>
		// Token: 0x04000AF2 RID: 2802
		DottedDiamond,
		/// <summary>Specifies a hatch that has the appearance of diagonally layered shingles that slant to the right from top points to bottom points.</summary>
		// Token: 0x04000AF3 RID: 2803
		Shingle,
		/// <summary>Specifies a hatch that has the appearance of a trellis.</summary>
		// Token: 0x04000AF4 RID: 2804
		Trellis,
		/// <summary>Specifies a hatch that has the appearance of spheres laid adjacent to one another.</summary>
		// Token: 0x04000AF5 RID: 2805
		Sphere,
		/// <summary>Specifies horizontal and vertical lines that cross and are spaced 50 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Cross" />.</summary>
		// Token: 0x04000AF6 RID: 2806
		SmallGrid,
		/// <summary>Specifies a hatch that has the appearance of a checkerboard.</summary>
		// Token: 0x04000AF7 RID: 2807
		SmallCheckerBoard,
		/// <summary>Specifies a hatch that has the appearance of a checkerboard with squares that are twice the size of <see cref="F:System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard" />.</summary>
		// Token: 0x04000AF8 RID: 2808
		LargeCheckerBoard,
		/// <summary>Specifies forward diagonal and backward diagonal lines that cross but are not antialiased.</summary>
		// Token: 0x04000AF9 RID: 2809
		OutlinedDiamond,
		/// <summary>Specifies a hatch that has the appearance of a checkerboard placed diagonally.</summary>
		// Token: 0x04000AFA RID: 2810
		SolidDiamond,
		/// <summary>Specifies the hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Cross" />.</summary>
		// Token: 0x04000AFB RID: 2811
		LargeGrid = 4,
		/// <summary>Specifies hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
		// Token: 0x04000AFC RID: 2812
		Min = 0,
		/// <summary>Specifies hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.SolidDiamond" />.</summary>
		// Token: 0x04000AFD RID: 2813
		Max = 4
	}
}
