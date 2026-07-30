using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies that <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />, <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />, or neither panel is fixed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000186 RID: 390
	public enum FixedPanel
	{
		/// <summary>Specifies that neither <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />, <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is fixed. A <see cref="E:System.Windows.Forms.Control.Resize" /> event affects both panels.</summary>
		// Token: 0x04000E33 RID: 3635
		None,
		/// <summary>Specifies that <see cref="P:System.Windows.Forms.SplitContainer.Panel1" /> is fixed. A <see cref="E:System.Windows.Forms.Control.Resize" /> event affects only <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />.</summary>
		// Token: 0x04000E34 RID: 3636
		Panel1,
		/// <summary>Specifies that <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is fixed. A <see cref="E:System.Windows.Forms.Control.Resize" /> event affects only <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />.</summary>
		// Token: 0x04000E35 RID: 3637
		Panel2
	}
}
