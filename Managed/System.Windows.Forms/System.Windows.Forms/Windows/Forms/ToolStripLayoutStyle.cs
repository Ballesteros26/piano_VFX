using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the possible alignments with which the items of a <see cref="T:System.Windows.Forms.ToolStrip" /> can be displayed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000364 RID: 868
	public enum ToolStripLayoutStyle
	{
		/// <summary>Specifies that items are laid out automatically.</summary>
		// Token: 0x04001B12 RID: 6930
		StackWithOverflow,
		/// <summary>Specifies that items are laid out horizontally and overflow as necessary.</summary>
		// Token: 0x04001B13 RID: 6931
		HorizontalStackWithOverflow,
		/// <summary>Specifies that items are laid out vertically, are centered within the control, and overflow as necessary.</summary>
		// Token: 0x04001B14 RID: 6932
		VerticalStackWithOverflow,
		/// <summary>Specifies that items flow horizontally or vertically as necessary.</summary>
		// Token: 0x04001B15 RID: 6933
		Flow,
		/// <summary>Specifies that items are laid out flush left.</summary>
		// Token: 0x04001B16 RID: 6934
		Table
	}
}
