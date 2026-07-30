using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a data source control that Web server controls and other controls can use to bind to hierarchical site map data.</summary>
	// Token: 0x02000408 RID: 1032
	[ToolboxBitmap("")]
	[Designer("System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class SiteMapDataSource : HierarchicalDataSourceControl, IDataSource, IListSource
	{
		/// <summary>Retrieves a collection of named views for the data source control.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> objects associated with the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" />. Because the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> supports only one named view, the <see cref="M:System.Web.UI.WebControls.SiteMapDataSource.GetViewNames" /> method returns an <see cref="T:System.Collections.ICollection" /> with one <see cref="F:System.String.Empty" /> element.</returns>
		// Token: 0x06002DBD RID: 11709 RVA: 0x000790D0 File Offset: 0x000772D0
		public virtual ICollection GetViewNames()
		{
			return SiteMapDataSource.emptyNames;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of data source controls that can be used as sources of lists of data.</returns>
		// Token: 0x06002DBE RID: 11710 RVA: 0x000790D7 File Offset: 0x000772D7
		IList IListSource.GetList()
		{
			return this.GetList();
		}

		/// <summary>Gets a value that indicates whether the collection is a collection of <see cref="T:System.Collections.IList" /> objects.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> is associated with one or more <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> objects; otherwise, false.</returns>
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x000790DF File Offset: 0x000772DF
		bool IListSource.ContainsListCollection
		{
			get
			{
				return this.ContainsListCollection;
			}
		}

		/// <summary>Retrieves a list of data source controls that can be used as sources of lists of data.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of data source controls that can be used as sources of lists of data.</returns>
		// Token: 0x06002DC0 RID: 11712 RVA: 0x00032A76 File Offset: 0x00030C76
		public virtual IList GetList()
		{
			return ListSourceHelper.GetList(this);
		}

		/// <summary>Gets a value indicating whether the data source control contains a collection of data source view objects.</summary>
		/// <returns>true if the data source control contains a collection of data source view objects; otherwise, false.</returns>
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x00032AE0 File Offset: 0x00030CE0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool ContainsListCollection
		{
			get
			{
				return ListSourceHelper.ContainsListCollection(this);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IDataSource.GetView(System.String)" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object on the site map data, according to the starting node identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> property or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" /> is false.</returns>
		/// <param name="viewName">The URL of the root node of the view. </param>
		/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.SiteMapProvider" /> is configured or available for the site. </exception>
		// Token: 0x06002DC2 RID: 11714 RVA: 0x000790E7 File Offset: 0x000772E7
		DataSourceView IDataSource.GetView(string viewName)
		{
			return this.GetView(viewName);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IDataSource.GetViewNames" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> objects associated with the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" />. Because the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> supports only one named view, the <see cref="M:System.Web.UI.WebControls.SiteMapDataSource.GetViewNames" /> returns a collection containing one element set to <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x06002DC3 RID: 11715 RVA: 0x000790F0 File Offset: 0x000772F0
		ICollection IDataSource.GetViewNames()
		{
			return this.GetViewNames();
		}

		/// <summary>Occurs when a data source control has changed in some way that affects data-bound controls.</summary>
		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06002DC4 RID: 11716 RVA: 0x000790F8 File Offset: 0x000772F8
		// (remove) Token: 0x06002DC5 RID: 11717 RVA: 0x00079101 File Offset: 0x00077301
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
				((IHierarchicalDataSource)this).DataSourceChanged += value;
			}
			remove
			{
				((IHierarchicalDataSource)this).DataSourceChanged -= value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Web.SiteMapProvider" /> object that is associated with the data source control.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapProvider" /> that is associated with the data source control; otherwise, if no provider is explicitly set, the default site map provider.</returns>
		/// <exception cref="T:System.Web.HttpException">The provider named by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.SiteMapProvider" /> is not available.- or -No default provider is configured for the site.</exception>
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x0007910C File Offset: 0x0007730C
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x0007918B File Offset: 0x0007738B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SiteMapProvider Provider
		{
			get
			{
				if (this.provider == null)
				{
					if (this.SiteMapProvider.Length == 0)
					{
						this.provider = SiteMap.Provider;
						if (this.provider == null)
						{
							throw new HttpException("There is no default provider configured for the site.");
						}
					}
					else
					{
						this.provider = SiteMap.Providers[this.SiteMapProvider];
						if (this.provider == null)
						{
							throw new HttpException("SiteMap provider '" + this.SiteMapProvider + "' not found.");
						}
					}
				}
				return this.provider;
			}
			set
			{
				this.provider = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the site map provider that the data source binds to.</summary>
		/// <returns>The name of the site map provider that the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> binds to. By default, the value is <see cref="F:System.String.Empty" />, and the default site map provider for the site is used.</returns>
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x0007919F File Offset: 0x0007739F
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x000791B6 File Offset: 0x000773B6
		[DefaultValue("")]
		public virtual string SiteMapProvider
		{
			get
			{
				return this.ViewState.GetString("SiteMapProvider", "");
			}
			set
			{
				this.ViewState["SiteMapProvider"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a node in the site map that the data source then uses as a reference point to retrieve nodes from a hierarchical site map.</summary>
		/// <returns>The URL of a node in the site map. The <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> retrieves the identified <see cref="T:System.Web.SiteMapNode" /> and any child nodes from the site map. The default is an <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000791D4 File Offset: 0x000773D4
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x000791EB File Offset: 0x000773EB
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string StartingNodeUrl
		{
			get
			{
				return this.ViewState.GetString("StartingNodeUrl", "");
			}
			set
			{
				this.ViewState["StartingNodeUrl"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a positive or negative integer offset from the starting node that determines the root hierarchy that is exposed by the data source control.</summary>
		/// <returns>The default is 0, which indicates that the root hierarchy exposed by the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> is the same as the starting node.</returns>
		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x00079209 File Offset: 0x00077409
		// (set) Token: 0x06002DCD RID: 11725 RVA: 0x0007921C File Offset: 0x0007741C
		[DefaultValue(0)]
		public virtual int StartingNodeOffset
		{
			get
			{
				return this.ViewState.GetInt("StartingNodeOffset", 0);
			}
			set
			{
				this.ViewState["StartingNodeOffset"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the site map node tree is retrieved using the node that represents the current page.</summary>
		/// <returns>true if the node tree is retrieved relative to the current page; otherwise, false. The default is false. </returns>
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x0007923F File Offset: 0x0007743F
		// (set) Token: 0x06002DCF RID: 11727 RVA: 0x00079252 File Offset: 0x00077452
		[DefaultValue(false)]
		public virtual bool StartFromCurrentNode
		{
			get
			{
				return this.ViewState.GetBool("StartFromCurrentNode", false);
			}
			set
			{
				this.ViewState["StartFromCurrentNode"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the starting node is retrieved and displayed. </summary>
		/// <returns>true if the starting node is displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x00079275 File Offset: 0x00077475
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x00079288 File Offset: 0x00077488
		[DefaultValue(true)]
		public virtual bool ShowStartingNode
		{
			get
			{
				return this.ViewState.GetBool("ShowStartingNode", true);
			}
			set
			{
				this.ViewState["ShowStartingNode"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Retrieves a named view on the site map data of the site map provider according to the starting node and other properties of the data source.</summary>
		/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object on the site map data, according to the starting node that is identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> property or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" /> is false.</returns>
		/// <param name="viewName">The name of the data source view to retrieve.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.Provider" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartFromCurrentNode" /> is true but the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set but the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> cannot resolve a node for the specified URL.</exception>
		// Token: 0x06002DD2 RID: 11730 RVA: 0x000792AC File Offset: 0x000774AC
		public virtual DataSourceView GetView(string viewName)
		{
			SiteMapNode startNode = this.GetStartNode(viewName);
			if (startNode == null)
			{
				return new SiteMapDataSourceView(this, viewName, SiteMapNodeCollection.EmptyList);
			}
			if (this.ShowStartingNode)
			{
				return new SiteMapDataSourceView(this, viewName, startNode);
			}
			return new SiteMapDataSourceView(this, viewName, startNode.ChildNodes);
		}

		/// <summary>Retrieves a single view on the site map data for the <see cref="T:System.Web.SiteMapProvider" /> object according to the starting node and other properties of the data source.</summary>
		/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object on the site map data, starting with the node that is identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" /> is false.</returns>
		/// <param name="viewPath">The URL of the starting node, specified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" />. </param>
		/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.SiteMapProvider" /> is configured or available for the site. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartFromCurrentNode" /> is true but the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set but the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> cannot resolve a node for the specified URL.</exception>
		// Token: 0x06002DD3 RID: 11731 RVA: 0x000792F0 File Offset: 0x000774F0
		protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
		{
			SiteMapNode startNode = this.GetStartNode(viewPath);
			if (startNode == null)
			{
				return new SiteMapHierarchicalDataSourceView(SiteMapNodeCollection.EmptyList);
			}
			if (this.ShowStartingNode || startNode == null)
			{
				return new SiteMapHierarchicalDataSourceView(startNode);
			}
			return new SiteMapHierarchicalDataSourceView(startNode.ChildNodes);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x00079330 File Offset: 0x00077530
		[global::System.MonoTODO("handle StartNodeOffsets > 0")]
		private SiteMapNode GetStartNode(string viewPath)
		{
			if (viewPath != null && viewPath.Length != 0)
			{
				string text = this.MapUrl(this.StartingNodeUrl);
				return this.Provider.FindSiteMapNode(text);
			}
			SiteMapNode siteMapNode;
			if (this.StartFromCurrentNode)
			{
				if (this.StartingNodeUrl.Length != 0)
				{
					throw new InvalidOperationException("StartingNodeUrl can't be set if StartFromCurrentNode is set to true.");
				}
				siteMapNode = SiteMap.CurrentNode;
			}
			else if (this.StartingNodeUrl.Length != 0)
			{
				string text2 = this.MapUrl(this.StartingNodeUrl);
				SiteMapNode siteMapNode2 = this.Provider.FindSiteMapNode(text2);
				if (siteMapNode2 == null)
				{
					throw new ArgumentException("Can't find a site map node for the url: " + this.StartingNodeUrl);
				}
				siteMapNode = siteMapNode2;
			}
			else
			{
				siteMapNode = this.Provider.RootNode;
			}
			if (siteMapNode == null)
			{
				return null;
			}
			if (this.StartingNodeOffset < 0)
			{
				for (int i = this.StartingNodeOffset; i < 0; i++)
				{
					if (siteMapNode.ParentNode == null)
					{
						break;
					}
					siteMapNode = siteMapNode.ParentNode;
				}
			}
			else if (this.StartingNodeOffset > 0)
			{
				List<SiteMapNode> list = new List<SiteMapNode>();
				SiteMapNode siteMapNode3 = this.Provider.CurrentNode;
				while (siteMapNode3 != null && siteMapNode3 != siteMapNode)
				{
					list.Insert(0, siteMapNode3);
					siteMapNode3 = siteMapNode3.ParentNode;
				}
				if (siteMapNode3 == siteMapNode && this.StartingNodeOffset <= list.Count)
				{
					siteMapNode = list[this.StartingNodeOffset - 1];
				}
			}
			return siteMapNode;
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x00079468 File Offset: 0x00077668
		private string MapUrl(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return string.Empty;
			}
			if (UrlUtils.IsRelativeUrl(url))
			{
				return UrlUtils.Combine(HttpRuntime.AppDomainAppVirtualPath, url);
			}
			return UrlUtils.ResolveVirtualPathFromAppAbsolute(url);
		}

		// Token: 0x04001B85 RID: 7045
		private static string[] emptyNames = new string[] { "DefaultView" };

		// Token: 0x04001B86 RID: 7046
		private SiteMapProvider provider;
	}
}
