using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the style of dashed lines drawn with a <see cref="T:System.Drawing.Pen" /> object.</summary>
	// Token: 0x02000138 RID: 312
	public enum DashStyle
	{
		/// <summary>Specifies a solid line.</summary>
		// Token: 0x04000AB7 RID: 2743
		Solid,
		/// <summary>Specifies a line consisting of dashes.</summary>
		// Token: 0x04000AB8 RID: 2744
		Dash,
		/// <summary>Specifies a line consisting of dots.</summary>
		// Token: 0x04000AB9 RID: 2745
		Dot,
		/// <summary>Specifies a line consisting of a repeating pattern of dash-dot.</summary>
		// Token: 0x04000ABA RID: 2746
		DashDot,
		/// <summary>Specifies a line consisting of a repeating pattern of dash-dot-dot.</summary>
		// Token: 0x04000ABB RID: 2747
		DashDotDot,
		/// <summary>Specifies a user-defined custom dash style.</summary>
		// Token: 0x04000ABC RID: 2748
		Custom
	}
}
