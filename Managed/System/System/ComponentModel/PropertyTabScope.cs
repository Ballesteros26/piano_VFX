using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers that indicate the persistence scope of a tab in the Properties window.</summary>
	// Token: 0x020002FC RID: 764
	public enum PropertyTabScope
	{
		/// <summary>This tab is added to the Properties window and cannot be removed.</summary>
		// Token: 0x04001439 RID: 5177
		Static,
		/// <summary>This tab is added to the Properties window and can only be removed explicitly by a parent component.</summary>
		// Token: 0x0400143A RID: 5178
		Global,
		/// <summary>This tab is specific to the current document. This tab is added to the Properties window and is removed when the currently selected document changes.</summary>
		// Token: 0x0400143B RID: 5179
		Document,
		/// <summary>This tab is specific to the current component. This tab is added to the Properties window for the current component only and is removed when the component is no longer selected.</summary>
		// Token: 0x0400143C RID: 5180
		Component
	}
}
