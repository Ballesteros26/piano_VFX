using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the style that a <see cref="T:System.Windows.Forms.ProgressBar" /> uses to indicate the progress of an operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200029C RID: 668
	public enum ProgressBarStyle
	{
		/// <summary>Indicates progress by increasing the number of segmented blocks in a <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
		// Token: 0x040015A4 RID: 5540
		Blocks,
		/// <summary>Indicates progress by increasing the size of a smooth, continuous bar in a <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
		// Token: 0x040015A5 RID: 5541
		Continuous,
		/// <summary>Indicates progress by continuously scrolling a block across a <see cref="T:System.Windows.Forms.ProgressBar" /> in a marquee fashion.</summary>
		// Token: 0x040015A6 RID: 5542
		Marquee
	}
}
