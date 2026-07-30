using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Describes the location of a point in the background specified by a visual style.</summary>
	// Token: 0x02000518 RID: 1304
	public enum HitTestCode
	{
		/// <summary>The hit test succeeded outside the control or on a transparent area.</summary>
		// Token: 0x04002B4D RID: 11085
		Nowhere,
		/// <summary>The hit test succeeded in the middle background segment.</summary>
		// Token: 0x04002B4E RID: 11086
		Client,
		/// <summary>The hit test succeeded in the left border segment.</summary>
		// Token: 0x04002B4F RID: 11087
		Left = 10,
		/// <summary>The hit test succeeded in the right border segment.</summary>
		// Token: 0x04002B50 RID: 11088
		Right,
		/// <summary>The hit test succeeded in the top border segment.</summary>
		// Token: 0x04002B51 RID: 11089
		Top,
		/// <summary>The hit test succeeded in the top and left border intersection.</summary>
		// Token: 0x04002B52 RID: 11090
		TopLeft,
		/// <summary>The hit test succeeded in the top and right border intersection.</summary>
		// Token: 0x04002B53 RID: 11091
		TopRight,
		/// <summary>The hit test succeeded in the bottom border segment.</summary>
		// Token: 0x04002B54 RID: 11092
		Bottom,
		/// <summary>The hit test succeeded in the bottom and left border intersection.</summary>
		// Token: 0x04002B55 RID: 11093
		BottomLeft,
		/// <summary>The hit test succeeded in the bottom and right border intersection.</summary>
		// Token: 0x04002B56 RID: 11094
		BottomRight
	}
}
