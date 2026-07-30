using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Indicates the hierarchical order in which navigation nodes are rendered for site-navigation controls.</summary>
	// Token: 0x020002FD RID: 765
	public enum PathDirection
	{
		/// <summary>Nodes are rendered in a hierarchical order from the top-most node to the current node, from left to right.</summary>
		// Token: 0x04001746 RID: 5958
		RootToCurrent,
		/// <summary>Nodes are rendered in a hierarchical order from the current node to the top-most node, from left to right.</summary>
		// Token: 0x04001747 RID: 5959
		CurrentToRoot
	}
}
