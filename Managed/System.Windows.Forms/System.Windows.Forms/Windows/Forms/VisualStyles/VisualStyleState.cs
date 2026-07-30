using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies how visual styles are applied to the current application.</summary>
	// Token: 0x0200062E RID: 1582
	public enum VisualStyleState
	{
		/// <summary>Visual styles are not applied to the application.</summary>
		// Token: 0x04002D5A RID: 11610
		NoneEnabled,
		/// <summary>Visual styles are applied only to the nonclient area.</summary>
		// Token: 0x04002D5B RID: 11611
		NonClientAreaEnabled,
		/// <summary>Visual styles are applied only to the client area.</summary>
		// Token: 0x04002D5C RID: 11612
		ClientAreaEnabled,
		/// <summary>Visual styles are applied to client and nonclient areas.</summary>
		// Token: 0x04002D5D RID: 11613
		ClientAndNonClientAreasEnabled
	}
}
