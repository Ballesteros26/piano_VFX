using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Defines a blend pattern for a <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> object. This class cannot be inherited.</summary>
	// Token: 0x0200012F RID: 303
	public sealed class Blend
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Blend" /> class.</summary>
		// Token: 0x06000DDA RID: 3546 RVA: 0x0001E64D File Offset: 0x0001C84D
		public Blend()
		{
			this.Factors = new float[1];
			this.Positions = new float[1];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Blend" /> class with the specified number of factors and positions.</summary>
		/// <param name="count">The number of elements in the <see cref="P:System.Drawing.Drawing2D.Blend.Factors" /> and <see cref="P:System.Drawing.Drawing2D.Blend.Positions" /> arrays. </param>
		// Token: 0x06000DDB RID: 3547 RVA: 0x0001E66D File Offset: 0x0001C86D
		public Blend(int count)
		{
			this.Factors = new float[count];
			this.Positions = new float[count];
		}

		/// <summary>Gets or sets an array of blend factors for the gradient.</summary>
		/// <returns>An array of blend factors that specify the percentages of the starting color and the ending color to be used at the corresponding position.</returns>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0001E68D File Offset: 0x0001C88D
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x0001E695 File Offset: 0x0001C895
		public float[] Factors { get; set; }

		/// <summary>Gets or sets an array of blend positions for the gradient.</summary>
		/// <returns>An array of blend positions that specify the percentages of distance along the gradient line.</returns>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0001E69E File Offset: 0x0001C89E
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0001E6A6 File Offset: 0x0001C8A6
		public float[] Positions { get; set; }
	}
}
