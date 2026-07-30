using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction to move when getting items with the <see cref="M:System.Windows.Forms.ToolStrip.GetNextItem(System.Windows.Forms.ToolStripItem,System.Windows.Forms.ArrowDirection)" /> method.</summary>
	// Token: 0x02000044 RID: 68
	public enum ArrowDirection
	{
		/// <summary>The direction is left (<see cref="F:System.Windows.Forms.Orientation.Horizontal" />).</summary>
		// Token: 0x040005D2 RID: 1490
		Left,
		/// <summary>The direction is up (<see cref="F:System.Windows.Forms.Orientation.Vertical" />).</summary>
		// Token: 0x040005D3 RID: 1491
		Up,
		/// <summary>The direction is right (<see cref="F:System.Windows.Forms.Orientation.Horizontal" />).</summary>
		// Token: 0x040005D4 RID: 1492
		Right = 16,
		/// <summary>The direction is down (<see cref="F:System.Windows.Forms.Orientation.Vertical" />).</summary>
		// Token: 0x040005D5 RID: 1493
		Down
	}
}
