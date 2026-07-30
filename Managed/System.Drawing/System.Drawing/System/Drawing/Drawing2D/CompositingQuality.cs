using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the quality level to use during compositing.</summary>
	// Token: 0x02000134 RID: 308
	public enum CompositingQuality
	{
		/// <summary>Invalid quality.</summary>
		// Token: 0x04000AA7 RID: 2727
		Invalid = -1,
		/// <summary>Default quality.</summary>
		// Token: 0x04000AA8 RID: 2728
		Default,
		/// <summary>High speed, low quality.</summary>
		// Token: 0x04000AA9 RID: 2729
		HighSpeed,
		/// <summary>High quality, low speed compositing.</summary>
		// Token: 0x04000AAA RID: 2730
		HighQuality,
		/// <summary>Gamma correction is used.</summary>
		// Token: 0x04000AAB RID: 2731
		GammaCorrected,
		/// <summary>Assume linear values.</summary>
		// Token: 0x04000AAC RID: 2732
		AssumeLinear
	}
}
