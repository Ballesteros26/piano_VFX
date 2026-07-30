using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of scroll arrow to draw on a scroll bar.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002CB RID: 715
	public enum ScrollButton
	{
		/// <summary>A minimum-scroll arrow.</summary>
		// Token: 0x040016B5 RID: 5813
		Min,
		/// <summary>An up-scroll arrow.</summary>
		// Token: 0x040016B6 RID: 5814
		Up = 0,
		/// <summary>A down-scroll arrow.</summary>
		// Token: 0x040016B7 RID: 5815
		Down,
		/// <summary>A left-scroll arrow.</summary>
		// Token: 0x040016B8 RID: 5816
		Left,
		/// <summary>A right-scroll arrow.</summary>
		// Token: 0x040016B9 RID: 5817
		Right,
		/// <summary>A maximum-scroll arrow.</summary>
		// Token: 0x040016BA RID: 5818
		Max = 3
	}
}
