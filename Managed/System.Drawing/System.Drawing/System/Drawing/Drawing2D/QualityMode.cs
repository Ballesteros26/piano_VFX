using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the overall quality when rendering GDI+ objects.</summary>
	// Token: 0x02000149 RID: 329
	public enum QualityMode
	{
		/// <summary>Specifies an invalid mode.</summary>
		// Token: 0x04000B40 RID: 2880
		Invalid = -1,
		/// <summary>Specifies the default mode.</summary>
		// Token: 0x04000B41 RID: 2881
		Default,
		/// <summary>Specifies low quality, high speed rendering.</summary>
		// Token: 0x04000B42 RID: 2882
		Low,
		/// <summary>Specifies high quality, low speed rendering.</summary>
		// Token: 0x04000B43 RID: 2883
		High
	}
}
