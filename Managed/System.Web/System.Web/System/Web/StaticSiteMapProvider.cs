using System;
using System.Collections.Generic;
using System.Web.Util;

namespace System.Web
{
	/// <summary>Serves as a partial implementation of the abstract <see cref="T:System.Web.SiteMapProvider" /> class and serves as a base class for the <see cref="T:System.Web.XmlSiteMapProvider" /> class, which is the default site map provider in ASP.NET. </summary>
	// Token: 0x020000DA RID: 218
	public abstract class StaticSiteMapProvider : SiteMapProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.StaticSiteMapProvider" /> class. </summary>
		// Token: 0x06000BCF RID: 3023 RVA: 0x0001F704 File Offset: 0x0001D904
		protected StaticSiteMapProvider()
		{
			this.keyToNode = new Dictionary<string, SiteMapNode>();
			this.nodeToParent = new Dictionary<SiteMapNode, SiteMapNode>();
			this.nodeToChildren = new Dictionary<SiteMapNode, SiteMapNodeCollection>();
			this.urlToNode = new Dictionary<string, SiteMapNode>(StringComparer.InvariantCultureIgnoreCase);
		}

		/// <summary>Adds a <see cref="T:System.Web.SiteMapNode" /> to the collections that are maintained by the site map provider and establishes a parent/child relationship between the <see cref="T:System.Web.SiteMapNode" /> objects.</summary>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the site map provider. </param>
		/// <param name="parentNode">The <see cref="T:System.Web.SiteMapNode" /> under which to add <paramref name="node" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.SiteMapNode.Url" /> or <see cref="P:System.Web.SiteMapNode.Key" /> is already registered with the <see cref="T:System.Web.StaticSiteMapProvider" />. A site map node must be made up of pages with unique URLs or keys. </exception>
		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001F740 File Offset: 0x0001D940
		protected internal override void AddNode(SiteMapNode node, SiteMapNode parentNode)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			object this_lock = this.this_lock;
			lock (this_lock)
			{
				string key = node.Key;
				if (this.FindSiteMapNodeFromKey(key) != null && node.Provider == this)
				{
					throw new InvalidOperationException(string.Format("A node with key '{0}' already exists.", key));
				}
				string url = node.Url;
				if (!string.IsNullOrEmpty(url))
				{
					string text = this.MapUrl(url);
					SiteMapNode siteMapNode = this.FindSiteMapNode(text);
					if (siteMapNode != null && string.Compare(siteMapNode.Url, text, RuntimeHelpers.StringComparison) == 0)
					{
						throw new InvalidOperationException(string.Format("Multiple nodes with the same URL '{0}' were found. StaticSiteMapProvider requires that sitemap nodes have unique URLs.", node.Url));
					}
					this.urlToNode.Add(text, node);
				}
				this.keyToNode.Add(key, node);
				if (node != this.RootNode)
				{
					if (parentNode == null)
					{
						parentNode = this.RootNode;
					}
					this.nodeToParent.Add(node, parentNode);
					SiteMapNodeCollection siteMapNodeCollection;
					if (!this.nodeToChildren.TryGetValue(parentNode, out siteMapNodeCollection))
					{
						this.nodeToChildren.Add(parentNode, siteMapNodeCollection = new SiteMapNodeCollection());
					}
					siteMapNodeCollection.Add(node);
				}
			}
		}

		/// <summary>Removes all elements in the collections of child and parent site map nodes that the <see cref="T:System.Web.StaticSiteMapProvider" /> tracks as part of its state.</summary>
		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001F870 File Offset: 0x0001DA70
		protected virtual void Clear()
		{
			object this_lock = this.this_lock;
			lock (this_lock)
			{
				this.urlToNode.Clear();
				this.nodeToChildren.Clear();
				this.nodeToParent.Clear();
				this.keyToNode.Clear();
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the page at the specified URL.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="rawURL" />; otherwise, null, if no corresponding site map node is found.</returns>
		/// <param name="rawUrl">A URL that identifies the page for which to retrieve a <see cref="T:System.Web.SiteMapNode" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawURL" /> is null. </exception>
		// Token: 0x06000BD2 RID: 3026 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
		public override SiteMapNode FindSiteMapNode(string rawUrl)
		{
			if (rawUrl == null)
			{
				throw new ArgumentNullException("rawUrl");
			}
			if (rawUrl == string.Empty)
			{
				return null;
			}
			this.BuildSiteMap();
			if (VirtualPathUtility.IsAppRelative(rawUrl))
			{
				rawUrl = VirtualPathUtility.ToAbsolute(rawUrl, HttpRuntime.AppDomainAppVirtualPath, false);
			}
			SiteMapNode siteMapNode;
			if (!this.urlToNode.TryGetValue(rawUrl, out siteMapNode))
			{
				return null;
			}
			return this.CheckAccessibility(siteMapNode);
		}

		/// <summary>Retrieves the child site map nodes of a specific <see cref="T:System.Web.SiteMapNode" /> object.</summary>
		/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the child site map nodes of <paramref name="node" />. If security trimming is enabled, the collection contains only site map nodes that the user is permitted to see.</returns>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve all child site map nodes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null. </exception>
		// Token: 0x06000BD3 RID: 3027 RVA: 0x0001F938 File Offset: 0x0001DB38
		public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.BuildSiteMap();
			SiteMapNodeCollection siteMapNodeCollection;
			if (!this.nodeToChildren.TryGetValue(node, out siteMapNodeCollection))
			{
				return SiteMapNodeCollection.EmptyCollection;
			}
			SiteMapNodeCollection siteMapNodeCollection2 = null;
			for (int i = 0; i < siteMapNodeCollection.Count; i++)
			{
				if (!this.IsAccessibleToUser(HttpContext.Current, siteMapNodeCollection[i]))
				{
					if (siteMapNodeCollection2 == null)
					{
						siteMapNodeCollection2 = new SiteMapNodeCollection();
						for (int j = 0; j < i; j++)
						{
							siteMapNodeCollection2.Add(siteMapNodeCollection[j]);
						}
					}
				}
				else if (siteMapNodeCollection2 != null)
				{
					siteMapNodeCollection2.Add(siteMapNodeCollection[i]);
				}
			}
			if (siteMapNodeCollection2 == null)
			{
				return SiteMapNodeCollection.ReadOnly(siteMapNodeCollection);
			}
			if (siteMapNodeCollection2.Count > 0)
			{
				return SiteMapNodeCollection.ReadOnly(siteMapNodeCollection2);
			}
			return SiteMapNodeCollection.EmptyCollection;
		}

		/// <summary>Retrieves the parent site map node of a specific <see cref="T:System.Web.SiteMapNode" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the parent of the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, null, if no parent site map node exists or the user is not permitted to see the parent site map node.</returns>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve the parent site map node. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null. </exception>
		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001F9EC File Offset: 0x0001DBEC
		public override SiteMapNode GetParentNode(SiteMapNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.BuildSiteMap();
			SiteMapNode siteMapNode;
			this.nodeToParent.TryGetValue(node, out siteMapNode);
			return this.CheckAccessibility(siteMapNode);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.SiteMapNode" /> object from all site map node collections that are tracked by the site map provider.</summary>
		/// <param name="node">The site map node to remove from the site map node collections. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null. </exception>
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0001FA24 File Offset: 0x0001DC24
		protected override void RemoveNode(SiteMapNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			string key = node.Key;
			object this_lock = this.this_lock;
			lock (this_lock)
			{
				if (this.keyToNode.ContainsKey(key))
				{
					this.keyToNode.Remove(key);
				}
				string text = node.Url;
				if (!string.IsNullOrEmpty(text))
				{
					text = this.MapUrl(text);
					if (this.urlToNode.ContainsKey(text))
					{
						this.urlToNode.Remove(text);
					}
				}
				if (node != this.RootNode)
				{
					SiteMapNode siteMapNode;
					if (this.nodeToParent.TryGetValue(node, out siteMapNode))
					{
						this.nodeToParent.Remove(node);
						if (this.nodeToChildren.ContainsKey(siteMapNode))
						{
							this.nodeToChildren[siteMapNode].Remove(node);
						}
					}
				}
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object based on a specified key.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="key" />; otherwise, null, if security trimming is enabled and the site map node cannot be shown to the current user or the site map node is not found in the site map node collection by <paramref name="key" />. </returns>
		/// <param name="key">A lookup key with which a <see cref="T:System.Web.SiteMapNode" /> is created.</param>
		// Token: 0x06000BD6 RID: 3030 RVA: 0x0001FB0C File Offset: 0x0001DD0C
		public override SiteMapNode FindSiteMapNodeFromKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SiteMapNode siteMapNode;
			this.keyToNode.TryGetValue(key, out siteMapNode);
			return this.CheckAccessibility(siteMapNode);
		}

		/// <summary>When overridden in a derived class, loads the site map information from persistent storage and builds it in memory.</summary>
		/// <returns>The root <see cref="T:System.Web.SiteMapNode" /> of the site map navigation structure.</returns>
		// Token: 0x06000BD7 RID: 3031
		public abstract SiteMapNode BuildSiteMap();

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0001FB3D File Offset: 0x0001DD3D
		private SiteMapNode CheckAccessibility(SiteMapNode node)
		{
			if (node == null || !this.IsAccessibleToUser(HttpContext.Current, node))
			{
				return null;
			}
			return node;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0001FB54 File Offset: 0x0001DD54
		internal string MapUrl(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return url;
			}
			string text = HttpRuntime.AppDomainAppVirtualPath;
			if (string.IsNullOrEmpty(text))
			{
				text = "/";
			}
			if (VirtualPathUtility.IsAppRelative(url))
			{
				return VirtualPathUtility.ToAbsolute(url, text, true);
			}
			return VirtualPathUtility.ToAbsolute(UrlUtils.Combine(text, url), text, true);
		}

		// Token: 0x040010B1 RID: 4273
		private Dictionary<string, SiteMapNode> keyToNode;

		// Token: 0x040010B2 RID: 4274
		private Dictionary<SiteMapNode, SiteMapNode> nodeToParent;

		// Token: 0x040010B3 RID: 4275
		private Dictionary<SiteMapNode, SiteMapNodeCollection> nodeToChildren;

		// Token: 0x040010B4 RID: 4276
		private Dictionary<string, SiteMapNode> urlToNode;
	}
}
