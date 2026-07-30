using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the style of a three-dimensional border.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000066 RID: 102
	[ComVisible(true)]
	public enum Border3DStyle
	{
		/// <summary>The border has a raised outer edge and no inner edge.</summary>
		// Token: 0x04000672 RID: 1650
		RaisedOuter = 1,
		/// <summary>The border has a sunken outer edge and no inner edge.</summary>
		// Token: 0x04000673 RID: 1651
		SunkenOuter,
		/// <summary>The border has a raised inner edge and no outer edge.</summary>
		// Token: 0x04000674 RID: 1652
		RaisedInner = 4,
		/// <summary>The border has raised inner and outer edges.</summary>
		// Token: 0x04000675 RID: 1653
		Raised,
		/// <summary>The inner and outer edges of the border have an etched appearance.</summary>
		// Token: 0x04000676 RID: 1654
		Etched,
		/// <summary>The border has a sunken inner edge and no outer edge.</summary>
		// Token: 0x04000677 RID: 1655
		SunkenInner = 8,
		/// <summary>The inner and outer edges of the border have a raised appearance.</summary>
		// Token: 0x04000678 RID: 1656
		Bump,
		/// <summary>The border has sunken inner and outer edges.</summary>
		// Token: 0x04000679 RID: 1657
		Sunken,
		/// <summary>The border is drawn outside the specified rectangle, preserving the dimensions of the rectangle for drawing.</summary>
		// Token: 0x0400067A RID: 1658
		Adjust = 8192,
		/// <summary>The border has no three-dimensional effects.</summary>
		// Token: 0x0400067B RID: 1659
		Flat = 16394
	}
}
