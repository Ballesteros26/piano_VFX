using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Defines arrays of colors and positions used for interpolating color blending in a multicolor gradient. This class cannot be inherited.</summary>
	// Token: 0x02000131 RID: 305
	public sealed class ColorBlend
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> class.</summary>
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0001E6AF File Offset: 0x0001C8AF
		public ColorBlend()
		{
			this.Colors = new Color[1];
			this.Positions = new float[1];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> class with the specified number of colors and positions.</summary>
		/// <param name="count">The number of colors and positions in this <see cref="T:System.Drawing.Drawing2D.ColorBlend" />. </param>
		// Token: 0x06000DE1 RID: 3553 RVA: 0x0001E6CF File Offset: 0x0001C8CF
		public ColorBlend(int count)
		{
			this.Colors = new Color[count];
			this.Positions = new float[count];
		}

		/// <summary>Gets or sets an array of colors that represents the colors to use at corresponding positions along a gradient.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Color" /> structures that represents the colors to use at corresponding positions along a gradient.</returns>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0001E6EF File Offset: 0x0001C8EF
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0001E6F7 File Offset: 0x0001C8F7
		public Color[] Colors { get; set; }

		/// <summary>Gets or sets the positions along a gradient line.</summary>
		/// <returns>An array of values that specify percentages of distance along the gradient line.</returns>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0001E700 File Offset: 0x0001C900
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0001E708 File Offset: 0x0001C908
		public float[] Positions { get; set; }
	}
}
