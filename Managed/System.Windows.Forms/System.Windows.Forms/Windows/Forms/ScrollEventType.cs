using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of action used to raise the <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002CD RID: 717
	[ComVisible(true)]
	public enum ScrollEventType
	{
		/// <summary>The scroll box was moved a small distance. The user clicked the left(horizontal) or top(vertical) scroll arrow, or pressed the UP ARROW key.</summary>
		// Token: 0x040016C0 RID: 5824
		SmallDecrement,
		/// <summary>The scroll box was moved a small distance. The user clicked the right(horizontal) or bottom(vertical) scroll arrow, or pressed the DOWN ARROW key.</summary>
		// Token: 0x040016C1 RID: 5825
		SmallIncrement,
		/// <summary>The scroll box moved a large distance. The user clicked the scroll bar to the left(horizontal) or above(vertical) the scroll box, or pressed the PAGE UP key.</summary>
		// Token: 0x040016C2 RID: 5826
		LargeDecrement,
		/// <summary>The scroll box moved a large distance. The user clicked the scroll bar to the right(horizontal) or below(vertical) the scroll box, or pressed the PAGE DOWN key.</summary>
		// Token: 0x040016C3 RID: 5827
		LargeIncrement,
		/// <summary>The scroll box was moved.</summary>
		// Token: 0x040016C4 RID: 5828
		ThumbPosition,
		/// <summary>The scroll box is currently being moved.</summary>
		// Token: 0x040016C5 RID: 5829
		ThumbTrack,
		/// <summary>The scroll box was moved to the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> position.</summary>
		// Token: 0x040016C6 RID: 5830
		First,
		/// <summary>The scroll box was moved to the <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> position.</summary>
		// Token: 0x040016C7 RID: 5831
		Last,
		/// <summary>The scroll box has stopped moving.</summary>
		// Token: 0x040016C8 RID: 5832
		EndScroll
	}
}
