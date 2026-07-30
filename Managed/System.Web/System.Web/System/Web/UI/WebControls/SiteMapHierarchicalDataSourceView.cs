using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a data view on a site map node or collection of nodes for a <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control.</summary>
	// Token: 0x0200040A RID: 1034
	public class SiteMapHierarchicalDataSourceView : HierarchicalDataSourceView
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> class, adding the specified node to the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection that the data source view maintains.</summary>
		/// <param name="node">A <see cref="T:System.Web.SiteMapNode" /> that the data source view represents.</param>
		// Token: 0x06002DDD RID: 11741 RVA: 0x000794E8 File Offset: 0x000776E8
		public SiteMapHierarchicalDataSourceView(SiteMapNode node)
			: this(new SiteMapNodeCollection(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> class, setting the specified collection to the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection that the data source view maintains.</summary>
		/// <param name="collection">A <see cref="T:System.Web.SiteMapNodeCollection" /> that the data source view represents.</param>
		// Token: 0x06002DDE RID: 11742 RVA: 0x000794F6 File Offset: 0x000776F6
		public SiteMapHierarchicalDataSourceView(SiteMapNodeCollection collection)
		{
			this.collection = collection;
		}

		/// <summary>Gets the collection of the <see cref="T:System.Web.SiteMapNode" /> objects that represents the site navigation structure for the current user.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection of site map nodes.</returns>
		// Token: 0x06002DDF RID: 11743 RVA: 0x00079505 File Offset: 0x00077705
		public override IHierarchicalEnumerable Select()
		{
			return this.collection;
		}

		// Token: 0x04001B88 RID: 7048
		private SiteMapNodeCollection collection;
	}
}
