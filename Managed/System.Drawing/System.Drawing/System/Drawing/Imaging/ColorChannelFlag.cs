using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies individual channels in the CMYK (cyan, magenta, yellow, black) color space. This enumeration is used by the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetOutputChannel" /> methods.</summary>
	// Token: 0x020000F3 RID: 243
	public enum ColorChannelFlag
	{
		/// <summary>The cyan color channel.</summary>
		// Token: 0x04000828 RID: 2088
		ColorChannelC,
		/// <summary>The magenta color channel.</summary>
		// Token: 0x04000829 RID: 2089
		ColorChannelM,
		/// <summary>The yellow color channel.</summary>
		// Token: 0x0400082A RID: 2090
		ColorChannelY,
		/// <summary>The black color channel.</summary>
		// Token: 0x0400082B RID: 2091
		ColorChannelK,
		/// <summary>The last selected channel should be used.</summary>
		// Token: 0x0400082C RID: 2092
		ColorChannelLast
	}
}
