using System;

namespace System.Web.UI.WebControls
{
	/// <summary>The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" /> enumeration is used by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control to identify the type of a <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> node within a node hierarchy.</summary>
	// Token: 0x0200030A RID: 778
	public enum SiteMapNodeItemType
	{
		/// <summary>The top node of the site navigation hierarchy. There can be only one root node.</summary>
		// Token: 0x0400175D RID: 5981
		Root,
		/// <summary>A parent node of the currently viewed page in the site navigation path. A parent node is any node that is found between the root node and the current node in the navigation hierarchy.</summary>
		// Token: 0x0400175E RID: 5982
		Parent,
		/// <summary>The currently viewed page in the site navigation path.</summary>
		// Token: 0x0400175F RID: 5983
		Current,
		/// <summary>A site map navigation path separator. The default separator for the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control is the "&gt;" character.</summary>
		// Token: 0x04001760 RID: 5984
		PathSeparator
	}
}
