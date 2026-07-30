using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the type of fill a <see cref="T:System.Drawing.Pen" /> object uses to fill lines.</summary>
	// Token: 0x02000147 RID: 327
	public enum PenType
	{
		/// <summary>Specifies a solid fill.</summary>
		// Token: 0x04000B33 RID: 2867
		SolidColor,
		/// <summary>Specifies a hatch fill.</summary>
		// Token: 0x04000B34 RID: 2868
		HatchFill,
		/// <summary>Specifies a bitmap texture fill.</summary>
		// Token: 0x04000B35 RID: 2869
		TextureFill,
		/// <summary>Specifies a path gradient fill.</summary>
		// Token: 0x04000B36 RID: 2870
		PathGradient,
		/// <summary>Specifies a linear gradient fill.</summary>
		// Token: 0x04000B37 RID: 2871
		LinearGradient
	}
}
