using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the bounds of the control to use when defining a control's size and position.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000068 RID: 104
	[Flags]
	public enum BoundsSpecified
	{
		/// <summary>No bounds are specified.</summary>
		// Token: 0x04000681 RID: 1665
		None = 0,
		/// <summary>The left edge of the control is defined.</summary>
		// Token: 0x04000682 RID: 1666
		X = 1,
		/// <summary>The top edge of the control is defined.</summary>
		// Token: 0x04000683 RID: 1667
		Y = 2,
		/// <summary>Both X and Y coordinates of the control are defined.</summary>
		// Token: 0x04000684 RID: 1668
		Location = 3,
		/// <summary>The width of the control is defined.</summary>
		// Token: 0x04000685 RID: 1669
		Width = 4,
		/// <summary>The height of the control is defined.</summary>
		// Token: 0x04000686 RID: 1670
		Height = 8,
		/// <summary>Both <see cref="P:System.Windows.Forms.Control.Width" /> and <see cref="P:System.Windows.Forms.Control.Height" /> property values of the control are defined.</summary>
		// Token: 0x04000687 RID: 1671
		Size = 12,
		/// <summary>Both <see cref="P:System.Windows.Forms.Control.Location" /> and <see cref="P:System.Windows.Forms.Control.Size" /> property values are defined.</summary>
		// Token: 0x04000688 RID: 1672
		All = 15
	}
}
