using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers that indicate the type of a refresh of the Properties window.</summary>
	// Token: 0x020002FD RID: 765
	public enum RefreshProperties
	{
		/// <summary>No refresh is necessary.</summary>
		// Token: 0x0400143E RID: 5182
		None,
		/// <summary>The properties should be requeried and the view should be refreshed.</summary>
		// Token: 0x0400143F RID: 5183
		All,
		/// <summary>The view should be refreshed.</summary>
		// Token: 0x04001440 RID: 5184
		Repaint
	}
}
