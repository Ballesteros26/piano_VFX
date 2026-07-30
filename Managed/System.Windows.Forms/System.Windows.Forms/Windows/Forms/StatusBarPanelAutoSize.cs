using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a <see cref="T:System.Windows.Forms.StatusBarPanel" /> on a <see cref="T:System.Windows.Forms.StatusBar" /> control behaves when the control resizes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002EB RID: 747
	public enum StatusBarPanelAutoSize
	{
		/// <summary>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> does not change size when the <see cref="T:System.Windows.Forms.StatusBar" /> control resizes.</summary>
		// Token: 0x04001802 RID: 6146
		None = 1,
		/// <summary>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> shares the available space on the <see cref="T:System.Windows.Forms.StatusBar" /> (the space not taken up by other panels whose <see cref="P:System.Windows.Forms.StatusBarPanel.AutoSize" /> property is set to <see cref="F:System.Windows.Forms.StatusBarPanelAutoSize.None" /> or <see cref="F:System.Windows.Forms.StatusBarPanelAutoSize.Contents" />) with other panels that have their <see cref="P:System.Windows.Forms.StatusBarPanel.AutoSize" /> property set to <see cref="F:System.Windows.Forms.StatusBarPanelAutoSize.Spring" />.</summary>
		// Token: 0x04001803 RID: 6147
		Spring,
		/// <summary>The width of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> is determined by its contents.</summary>
		// Token: 0x04001804 RID: 6148
		Contents
	}
}
