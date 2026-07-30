using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace System.Web
{
	/// <summary>Provides a common base class for all site map data providers, and a way for developers to implement custom site map data providers that can be used with the ASP.NET site map infrastructure as persistent stores for <see cref="T:System.Web.SiteMap" /> objects. </summary>
	// Token: 0x020000D5 RID: 213
	public abstract class SiteMapProvider : ProviderBase
	{
		/// <summary>Occurs when the <see cref="P:System.Web.SiteMapProvider.CurrentNode" /> property is called. </summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000B9D RID: 2973 RVA: 0x0001EF83 File Offset: 0x0001D183
		// (remove) Token: 0x06000B9E RID: 2974 RVA: 0x0001EF96 File Offset: 0x0001D196
		public event SiteMapResolveEventHandler SiteMapResolve
		{
			add
			{
				this.events.AddHandler(SiteMapProvider.siteMapResolveEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(SiteMapProvider.siteMapResolveEvent, value);
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.SiteMapNode" /> object to the node collection that is maintained by the site map provider.</summary>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the node collection maintained by the provider. </param>
		// Token: 0x06000B9F RID: 2975 RVA: 0x0001EFA9 File Offset: 0x0001D1A9
		protected virtual void AddNode(SiteMapNode node)
		{
			this.AddNode(node, null);
		}

		/// <summary>Adds a <see cref="T:System.Web.SiteMapNode" /> object to the node collection that is maintained by the site map provider and specifies the parent <see cref="T:System.Web.SiteMapNode" /> object. </summary>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the node collection maintained by the provider.</param>
		/// <param name="parentNode">The <see cref="T:System.Web.SiteMapNode" /> that is the parent of <paramref name="node" />.</param>
		/// <exception cref="T:System.NotImplementedException">In all cases.</exception>
		// Token: 0x06000BA0 RID: 2976 RVA: 0x00003A1F File Offset: 0x00001C1F
		protected internal virtual void AddNode(SiteMapNode node, SiteMapNode parentNode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the currently requested page using the specified <see cref="T:System.Web.HttpContext" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the currently requested page; otherwise, null, if no corresponding <see cref="T:System.Web.SiteMapNode" /> can be found in the <see cref="T:System.Web.SiteMapNode" /> or if the page context is null. </returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> used to match node information with the URL of the requested page.</param>
		// Token: 0x06000BA1 RID: 2977 RVA: 0x0001EFB4 File Offset: 0x0001D1B4
		public virtual SiteMapNode FindSiteMapNode(HttpContext context)
		{
			if (context == null)
			{
				return null;
			}
			HttpRequest request = context.Request;
			if (request == null)
			{
				return null;
			}
			SiteMapNode siteMapNode = this.FindSiteMapNode(request.RawUrl);
			if (siteMapNode == null)
			{
				siteMapNode = this.FindSiteMapNode(request.Path);
			}
			return siteMapNode;
		}

		/// <summary>When overridden in a derived class, retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the page at the specified URL.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="rawURL" />; otherwise, null, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user.</returns>
		/// <param name="rawUrl">A URL that identifies the page for which to retrieve a <see cref="T:System.Web.SiteMapNode" />. </param>
		// Token: 0x06000BA2 RID: 2978
		public abstract SiteMapNode FindSiteMapNode(string rawUrl);

		/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object based on a specified key.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="key" />; otherwise, null, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user. The default is null.</returns>
		/// <param name="key">A lookup key with which a <see cref="T:System.Web.SiteMapNode" /> is created.</param>
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0001EFF0 File Offset: 0x0001D1F0
		public virtual SiteMapNode FindSiteMapNodeFromKey(string key)
		{
			return this.FindSiteMapNode(key);
		}

		/// <summary>When overridden in a derived class, retrieves the child nodes of a specific <see cref="T:System.Web.SiteMapNode" />.</summary>
		/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the immediate child nodes of the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, null or an empty collection, if no child nodes exist.</returns>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve all child nodes. </param>
		// Token: 0x06000BA4 RID: 2980
		public abstract SiteMapNodeCollection GetChildNodes(SiteMapNode node);

		/// <summary>Provides an optimized lookup method for site map providers when retrieving the node for the currently requested page and fetching the parent and ancestor site map nodes for the current page.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the currently requested page; otherwise, null, if the <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
		/// <param name="upLevel">The number of ancestor site map node generations to get. A value of -1 indicates that all ancestors might be retrieved and cached by the provider.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="upLevel" /> is less than -1.</exception>
		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001EFF9 File Offset: 0x0001D1F9
		public virtual SiteMapNode GetCurrentNodeAndHintAncestorNodes(int upLevel)
		{
			if (upLevel < -1)
			{
				throw new ArgumentOutOfRangeException("upLevel");
			}
			return this.CurrentNode;
		}

		/// <summary>Provides an optimized lookup method for site map providers when retrieving the node for the currently requested page and fetching the site map nodes in the proximity of the current node.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the currently requested page; otherwise, null, if the <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
		/// <param name="upLevel">The number of ancestor <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no ancestor nodes are retrieved and -1 indicates that all ancestors might be retrieved and cached by the provider.</param>
		/// <param name="downLevel">The number of child <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no descendant nodes are retrieved and a -1 indicates that all descendant nodes might be retrieved and cached by the provider.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="upLevel" /> or <paramref name="downLevel" /> is less than -1.</exception>
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001F010 File Offset: 0x0001D210
		public virtual SiteMapNode GetCurrentNodeAndHintNeighborhoodNodes(int upLevel, int downLevel)
		{
			if (upLevel < -1)
			{
				throw new ArgumentOutOfRangeException("upLevel");
			}
			if (downLevel < -1)
			{
				throw new ArgumentOutOfRangeException("downLevel");
			}
			return this.CurrentNode;
		}

		/// <summary>When overridden in a derived class, retrieves the parent node of a specific <see cref="T:System.Web.SiteMapNode" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the parent of <paramref name="node" />; otherwise, null, if the <see cref="T:System.Web.SiteMapNode" /> has no parent or security trimming is enabled and the parent node is not accessible to the current user.Note<see cref="M:System.Web.SiteMapProvider.GetParentNode(System.Web.SiteMapNode)" /> might also return null if the parent node belongs to a different provider. In this case, use the <see cref="P:System.Web.SiteMapNode.ParentNode" /> property of <paramref name="node" /> instead.</returns>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve the parent node. </param>
		// Token: 0x06000BA7 RID: 2983
		public abstract SiteMapNode GetParentNode(SiteMapNode node);

		/// <summary>Provides an optimized lookup method for site map providers when retrieving an ancestor node for the currently requested page and fetching the descendant nodes for the ancestor.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents an ancestor <see cref="T:System.Web.SiteMapNode" /> of the currently requested page; otherwise, null, if the current or ancestor <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
		/// <param name="walkupLevels">The number of ancestor node levels to traverse when retrieving the requested ancestor node. </param>
		/// <param name="relativeDepthFromWalkup">The number of descendant node levels to retrieve from the target ancestor node. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="walkupLevels" /> or <paramref name="relativeDepthFromWalkup" /> is less than 0.</exception>
		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001F038 File Offset: 0x0001D238
		public virtual SiteMapNode GetParentNodeRelativeToCurrentNodeAndHintDownFromParent(int walkupLevels, int relativeDepthFromWalkup)
		{
			if (walkupLevels < 0)
			{
				throw new ArgumentOutOfRangeException("walkupLevels");
			}
			if (relativeDepthFromWalkup < 0)
			{
				throw new ArgumentOutOfRangeException("relativeDepthFromWalkup");
			}
			SiteMapNode siteMapNode = this.GetCurrentNodeAndHintAncestorNodes(walkupLevels);
			int num = 0;
			while (num < walkupLevels && siteMapNode != null)
			{
				siteMapNode = this.GetParentNode(siteMapNode);
				num++;
			}
			if (siteMapNode == null)
			{
				return null;
			}
			this.HintNeighborhoodNodes(siteMapNode, 0, relativeDepthFromWalkup);
			return siteMapNode;
		}

		/// <summary>Provides an optimized lookup method for site map providers when retrieving an ancestor node for the specified <see cref="T:System.Web.SiteMapNode" /> object and fetching its child nodes.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents an ancestor of <paramref name="node" />; otherwise, null, if the current or ancestor <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> that acts as a reference point for <paramref name="walkupLevels" /> and <paramref name="relativeDepthFromWalkup" />. </param>
		/// <param name="walkupLevels">The number of ancestor node levels to traverse when retrieving the requested ancestor node.</param>
		/// <param name="relativeDepthFromWalkup">The number of descendant node levels to retrieve from the target ancestor node.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for <paramref name="walkupLevels" /> or <paramref name="relativeDepthFromWalkup" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		// Token: 0x06000BA9 RID: 2985 RVA: 0x0001F094 File Offset: 0x0001D294
		public virtual SiteMapNode GetParentNodeRelativeToNodeAndHintDownFromParent(SiteMapNode node, int walkupLevels, int relativeDepthFromWalkup)
		{
			if (walkupLevels < 0)
			{
				throw new ArgumentOutOfRangeException("walkupLevels");
			}
			if (relativeDepthFromWalkup < 0)
			{
				throw new ArgumentOutOfRangeException("relativeDepthFromWalkup");
			}
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.HintAncestorNodes(node, walkupLevels);
			int num = 0;
			while (num < walkupLevels && node != null)
			{
				node = this.GetParentNode(node);
				num++;
			}
			if (node == null)
			{
				return null;
			}
			this.HintNeighborhoodNodes(node, 0, relativeDepthFromWalkup);
			return node;
		}

		/// <summary>When overridden in a derived class, retrieves the root node of all the nodes that are currently managed by the current provider. </summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the set of nodes that the current provider manages. </returns>
		// Token: 0x06000BAA RID: 2986
		protected internal abstract SiteMapNode GetRootNodeCore();

		/// <summary>Retrieves the root node of all the nodes that are currently managed by the specified site map provider.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the set of nodes that is managed by <paramref name="provider" />.</returns>
		/// <param name="provider">The provider that calls the <see cref="M:System.Web.SiteMapProvider.GetRootNodeCore" />.</param>
		// Token: 0x06000BAB RID: 2987 RVA: 0x0001F0FC File Offset: 0x0001D2FC
		protected static SiteMapNode GetRootNodeCoreFromProvider(SiteMapProvider provider)
		{
			return provider.GetRootNodeCore();
		}

		/// <summary>Provides a method that site map providers can override to perform an optimized retrieval of one or more levels of parent and ancestor nodes, relative to the specified <see cref="T:System.Web.SiteMapNode" /> object. </summary>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> that acts as a reference point for <paramref name="upLevel" />.</param>
		/// <param name="upLevel">The number of ancestor <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no ancestor nodes are retrieved and -1 indicates that all ancestors might be retrieved and cached.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="upLevel" /> is less than -1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		// Token: 0x06000BAC RID: 2988 RVA: 0x0001F104 File Offset: 0x0001D304
		public virtual void HintAncestorNodes(SiteMapNode node, int upLevel)
		{
			if (upLevel < -1)
			{
				throw new ArgumentOutOfRangeException("upLevel");
			}
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
		}

		/// <summary>Provides a method that site map providers can override to perform an optimized retrieval of nodes found in the proximity of the specified node. </summary>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> that acts as a reference point for <paramref name="upLevel" />.</param>
		/// <param name="upLevel">The number of ancestor <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no ancestor nodes are retrieved and -1 indicates that all ancestors (and their descendant nodes to the level of <paramref name="node" />) might be retrieved and cached.</param>
		/// <param name="downLevel">The number of descendant <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no descendant nodes are retrieved and -1 indicates that all descendant nodes might be retrieved and cached.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="upLevel" /> or <paramref name="downLevel" /> is less than -1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		// Token: 0x06000BAD RID: 2989 RVA: 0x0001F123 File Offset: 0x0001D323
		public virtual void HintNeighborhoodNodes(SiteMapNode node, int upLevel, int downLevel)
		{
			if (upLevel < -1)
			{
				throw new ArgumentOutOfRangeException("upLevel");
			}
			if (downLevel < -1)
			{
				throw new ArgumentOutOfRangeException("downLevel");
			}
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Web.SiteMapNode" /> object from the node collection that is maintained by the site map provider.</summary>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to remove from the node collection maintained by the provider.</param>
		/// <exception cref="T:System.NotImplementedException">In all cases.</exception>
		// Token: 0x06000BAE RID: 2990 RVA: 0x00003A1F File Offset: 0x00001C1F
		protected virtual void RemoveNode(SiteMapNode node)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes the <see cref="T:System.Web.SiteMapProvider" /> implementation, including any resources that are needed to load site map data from persistent storage.</summary>
		/// <param name="name">The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of the provider to initialize. </param>
		/// <param name="attributes">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that can contain additional attributes to help initialize the provider. These attributes are read from the site map provider configuration in the Web.config file. </param>
		// Token: 0x06000BAF RID: 2991 RVA: 0x0001F151 File Offset: 0x0001D351
		public override void Initialize(string name, NameValueCollection attributes)
		{
			base.Initialize(name, attributes);
			if (attributes["securityTrimmingEnabled"] != null)
			{
				this.securityTrimming = (bool)Convert.ChangeType(attributes["securityTrimmingEnabled"], typeof(bool));
			}
		}

		/// <summary>Retrieves a Boolean value indicating whether the specified <see cref="T:System.Web.SiteMapNode" /> object can be viewed by the user in the specified context.</summary>
		/// <returns>true if security trimming is enabled and <paramref name="node" /> can be viewed by the user or security trimming is not enabled; otherwise, false.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> that contains user information.</param>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> that is requested by the user.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.- or -<paramref name="node" /> is null.</exception>
		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001F190 File Offset: 0x0001D390
		[global::System.MonoTODO("need to implement cases 2 and 3")]
		public virtual bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (!this.SecurityTrimmingEnabled)
			{
				return true;
			}
			IList roles = node.Roles;
			if (roles != null && roles.Count > 0)
			{
				foreach (object obj in roles)
				{
					string text = (string)obj;
					if (text == "*" || context.User.IsInRole(text))
					{
						return true;
					}
				}
			}
			string text2 = node.Url;
			if (!string.IsNullOrEmpty(text2))
			{
				if (VirtualPathUtility.IsAppRelative(text2) || !VirtualPathUtility.IsAbsolute(text2))
				{
					text2 = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(HttpRuntime.AppDomainAppVirtualPath), text2);
				}
				AuthorizationSection authorizationSection = (AuthorizationSection)WebConfigurationManager.GetSection("system.web/authorization", text2);
				if (authorizationSection != null)
				{
					return authorizationSection.IsValidUser(context.User, context.Request.HttpMethod);
				}
			}
			return false;
		}

		/// <summary>Gets the <see cref="T:System.Web.SiteMapNode" /> object that represents the currently requested page.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the currently requested page; otherwise, null, if the <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0001F29C File Offset: 0x0001D49C
		public virtual SiteMapNode CurrentNode
		{
			get
			{
				if (HttpContext.Current == null)
				{
					return null;
				}
				SiteMapNode siteMapNode = this.ResolveSiteMapNode(HttpContext.Current);
				if (siteMapNode != null)
				{
					return siteMapNode;
				}
				return this.FindSiteMapNode(HttpContext.Current);
			}
		}

		/// <summary>Gets or sets the parent <see cref="T:System.Web.SiteMapProvider" /> object of the current provider.</summary>
		/// <returns>The parent provider of the current <see cref="T:System.Web.SiteMapProvider" />.</returns>
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0001F2CE File Offset: 0x0001D4CE
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x0001F2D6 File Offset: 0x0001D4D6
		public virtual SiteMapProvider ParentProvider
		{
			get
			{
				return this.parentProvider;
			}
			set
			{
				this.parentProvider = value;
			}
		}

		/// <summary>Gets the root <see cref="T:System.Web.SiteMapProvider" /> object in the current provider hierarchy.</summary>
		/// <returns>An <see cref="T:System.Web.SiteMapProvider" /> that is the top-level site map provider in the provider hierarchy that the current provider belongs to.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">There is a circular reference to the current site map provider. </exception>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0001F2E0 File Offset: 0x0001D4E0
		public virtual SiteMapProvider RootProvider
		{
			get
			{
				object obj = this.this_lock;
				lock (obj)
				{
					if (this.rootProviderCache == null)
					{
						SiteMapProvider siteMapProvider = this;
						while (siteMapProvider.ParentProvider != null)
						{
							siteMapProvider = siteMapProvider.ParentProvider;
						}
						this.rootProviderCache = siteMapProvider;
					}
				}
				return this.rootProviderCache;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.SiteMapProvider.SiteMapResolve" /> event. </summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> provided by the event handler delegate that is registered to handle the event or, if more than one delegate is registered to handle the event, the return value of the last delegate in the delegate chain; otherwise, null. </returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for which the site map currently exists. </param>
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001F344 File Offset: 0x0001D544
		protected SiteMapNode ResolveSiteMapNode(HttpContext context)
		{
			SiteMapResolveEventHandler siteMapResolveEventHandler = this.events[SiteMapProvider.siteMapResolveEvent] as SiteMapResolveEventHandler;
			if (siteMapResolveEventHandler != null)
			{
				object obj = this.resolveLock;
				lock (obj)
				{
					if (this.resolving)
					{
						return null;
					}
					this.resolving = true;
					SiteMapResolveEventArgs siteMapResolveEventArgs = new SiteMapResolveEventArgs(context, this);
					SiteMapNode siteMapNode = siteMapResolveEventHandler(this, siteMapResolveEventArgs);
					this.resolving = false;
					return siteMapNode;
				}
			}
			return null;
		}

		/// <summary>Gets or sets a Boolean value indicating whether localized values of <see cref="T:System.Web.SiteMapNode" /> attributes are returned.</summary>
		/// <returns>true if a localized value of the <see cref="T:System.Web.SiteMapNode" /> attributes are returned; otherwise, false. The default is false.</returns>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x0001F3D0 File Offset: 0x0001D5D0
		public bool EnableLocalization
		{
			get
			{
				return this.enableLocalization;
			}
			set
			{
				this.enableLocalization = value;
			}
		}

		/// <summary>Gets a Boolean value indicating whether a site map provider filters site map nodes based on a user's role.</summary>
		/// <returns>true if the provider is configured to filter nodes based on role; otherwise, false.</returns>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0001F3D9 File Offset: 0x0001D5D9
		public bool SecurityTrimmingEnabled
		{
			get
			{
				return this.securityTrimming;
			}
		}

		/// <summary>Get or sets the resource key that is used for localizing <see cref="T:System.Web.SiteMapNode" /> attributes. </summary>
		/// <returns>A string containing the resource key name.</returns>
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0001F3E1 File Offset: 0x0001D5E1
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0001F3E9 File Offset: 0x0001D5E9
		public string ResourceKey
		{
			get
			{
				return this.resourceKey;
			}
			set
			{
				this.resourceKey = value;
			}
		}

		/// <summary>Gets the root <see cref="T:System.Web.SiteMapNode" /> object of the site map data that the current provider represents.</summary>
		/// <returns>The root <see cref="T:System.Web.SiteMapNode" /> of the current site map data provider. The default implementation performs security trimming on the returned node.</returns>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0001F3F2 File Offset: 0x0001D5F2
		public virtual SiteMapNode RootNode
		{
			get
			{
				return SiteMapProvider.ReturnNodeIfAccessible(this.GetRootNodeCore());
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0001F3FF File Offset: 0x0001D5FF
		internal static SiteMapNode ReturnNodeIfAccessible(SiteMapNode node)
		{
			if (node.IsAccessibleToUser(HttpContext.Current))
			{
				return node;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x040010A5 RID: 4261
		private static readonly object siteMapResolveEvent = new object();

		// Token: 0x040010A6 RID: 4262
		internal object this_lock = new object();

		// Token: 0x040010A7 RID: 4263
		private bool enableLocalization;

		// Token: 0x040010A8 RID: 4264
		private SiteMapProvider parentProvider;

		// Token: 0x040010A9 RID: 4265
		private SiteMapProvider rootProviderCache;

		// Token: 0x040010AA RID: 4266
		private bool securityTrimming;

		// Token: 0x040010AB RID: 4267
		private object resolveLock = new object();

		// Token: 0x040010AC RID: 4268
		private bool resolving;

		// Token: 0x040010AD RID: 4269
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x040010AE RID: 4270
		private string resourceKey;
	}
}
