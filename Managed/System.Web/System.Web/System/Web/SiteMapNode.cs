using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Resources;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web
{
	/// <summary>Represents a node in the hierarchical site map structure such as that described by the <see cref="T:System.Web.SiteMap" /> class and classes that implement the abstract <see cref="T:System.Web.SiteMapProvider" /> class.</summary>
	// Token: 0x020000D3 RID: 211
	public class SiteMapNode : IHierarchyData, INavigateUIData, ICloneable
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x00002050 File Offset: 0x00000250
		private SiteMapNode()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class, using the specified <paramref name="key" /> to identify the page that the node represents and the site map provider that manages the node.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B34 RID: 2868 RVA: 0x0001E3C4 File Offset: 0x0001C5C4
		public SiteMapNode(SiteMapProvider provider, string key)
			: this(provider, key, null, null, null, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified URL, a <paramref name="key" /> to identify the page that the node represents, and the site map provider that manages the node.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key.</param>
		/// <param name="url">The URL of the page that the node represents within the site. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B35 RID: 2869 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		public SiteMapNode(SiteMapProvider provider, string key, string url)
			: this(provider, key, url, null, null, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified URL, a <paramref name="key" /> to identify the page that the node represents, a title, and the site map provider that manages the node.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key.</param>
		/// <param name="url">The URL of the page that the node represents within the site. </param>
		/// <param name="title">A label for the node, often displayed by navigation controls. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B36 RID: 2870 RVA: 0x0001E3FC File Offset: 0x0001C5FC
		public SiteMapNode(SiteMapProvider provider, string key, string url, string title)
			: this(provider, key, url, title, null, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified URL, a <paramref name="key" /> to identify the page that the node represents, a title and description, and the site map provider that manages the node.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key.</param>
		/// <param name="url">The URL of the page that the node represents within the site. </param>
		/// <param name="title">A label for the node, often displayed by navigation controls. </param>
		/// <param name="description">A description of the page that the node represents. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B37 RID: 2871 RVA: 0x0001E41C File Offset: 0x0001C61C
		public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description)
			: this(provider, key, url, title, description, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified site map provider that manages the node, URL, title, description, roles, additional attributes, and explicit and implicit resource keys for localization.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key. </param>
		/// <param name="url">The URL of the page that the node represents within the site. </param>
		/// <param name="title">A label for the node, often displayed by navigation controls. </param>
		/// <param name="description">A description of the page that the node represents. </param>
		/// <param name="roles">An <see cref="T:System.Collections.IList" /> of roles that are allowed to view the page represented by the <see cref="T:System.Web.SiteMapNode" />. </param>
		/// <param name="attributes">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of additional attributes used to initialize the <see cref="T:System.Web.SiteMapNode" />. </param>
		/// <param name="explicitResourceKeys">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of explicit resource keys used for localization. </param>
		/// <param name="implicitResourceKey">An implicit resource key used for localization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B38 RID: 2872 RVA: 0x0001E43C File Offset: 0x0001C63C
		public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description, IList roles, NameValueCollection attributes, NameValueCollection explicitResourceKeys, string implicitResourceKey)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.provider = provider;
			this.key = key;
			this.url = url;
			this.title = title;
			this.description = description;
			this.roles = roles;
			this.attributes = attributes;
			this.resourceKeys = explicitResourceKeys;
			this.resourceKey = implicitResourceKey;
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> object that is associated with the current node.</summary>
		/// <returns>A named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> for the current node.</returns>
		/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control that the view is associated with.</param>
		/// <param name="viewName">The name of the view.</param>
		// Token: 0x06000B39 RID: 2873 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		public SiteMapDataSourceView GetDataSourceView(SiteMapDataSource owner, string viewName)
		{
			return new SiteMapDataSourceView(owner, viewName, this);
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> object that is associated with the current node.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> for the current node.</returns>
		// Token: 0x06000B3A RID: 2874 RVA: 0x0001E4BA File Offset: 0x0001C6BA
		public SiteMapHierarchicalDataSourceView GetHierarchicalDataSourceView()
		{
			return new SiteMapHierarchicalDataSourceView(this);
		}

		/// <summary>Gets a value indicating whether the specified site map node can be viewed by the user in the specified context.</summary>
		/// <returns>true if any one of the following conditions is met: the security trimming is enabled and the current user is a member of at least one of the roles allowing access to view the site map node; the current user is authorized specifically for the requested node's URL in the authorization element for the current application and the URL is located within the directory structure for the application; the current thread has an associated <see cref="T:System.Security.Principal.WindowsIdentity" /> that has file access to the requested node's URL and the URL is located within the directory structure for the application; or security trimming is not enabled and therefore any user is allowed to view the site map node; otherwise, false.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> that contains user information.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified context is null.</exception>
		// Token: 0x06000B3B RID: 2875 RVA: 0x0001E4C2 File Offset: 0x0001C6C2
		public virtual bool IsAccessibleToUser(HttpContext context)
		{
			return this.provider.IsAccessibleToUser(context, this);
		}

		/// <summary>Converts the value of this instance of the <see cref="T:System.Web.SiteMapNode" /> class to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this <see cref="T:System.Web.SiteMapNode" />.</returns>
		// Token: 0x06000B3C RID: 2876 RVA: 0x0001E4D1 File Offset: 0x0001C6D1
		public override string ToString()
		{
			return this.Title;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Web.SiteMapNode" /> has any child nodes.</summary>
		/// <returns>true if the node has children; otherwise, false.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		public virtual bool HasChildNodes
		{
			get
			{
				SiteMapNodeCollection siteMapNodeCollection = this.ChildNodes;
				return siteMapNodeCollection != null && siteMapNodeCollection.Count > 0;
			}
		}

		/// <summary>Retrieves a read-only collection of all <see cref="T:System.Web.SiteMapNode" /> objects that are descendants of the calling node, regardless of the degree of separation.</summary>
		/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that represents all the descendants of a <see cref="T:System.Web.SiteMapNode" /> within the scope of the current provider.</returns>
		// Token: 0x06000B3E RID: 2878 RVA: 0x0001E500 File Offset: 0x0001C700
		public SiteMapNodeCollection GetAllNodes()
		{
			SiteMapNodeCollection siteMapNodeCollection = new SiteMapNodeCollection();
			this.GetAllNodesRecursive(siteMapNodeCollection);
			return SiteMapNodeCollection.ReadOnly(siteMapNodeCollection);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0001E520 File Offset: 0x0001C720
		private void GetAllNodesRecursive(SiteMapNodeCollection c)
		{
			SiteMapNodeCollection siteMapNodeCollection = this.ChildNodes;
			if (siteMapNodeCollection != null && siteMapNodeCollection.Count > 0)
			{
				c.AddRange(siteMapNodeCollection);
				foreach (object obj in siteMapNodeCollection)
				{
					((SiteMapNode)obj).GetAllNodesRecursive(c);
				}
			}
		}

		/// <summary>Gets a value indicating whether the current site map node is a child or a direct descendant of the specified node.</summary>
		/// <returns>true if the current node is a child or descendant of the specified node; otherwise, false.</returns>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to check if the current node is a child or descendant of.</param>
		// Token: 0x06000B40 RID: 2880 RVA: 0x0001E58C File Offset: 0x0001C78C
		public virtual bool IsDescendantOf(SiteMapNode node)
		{
			for (SiteMapNode siteMapNode = this.ParentNode; siteMapNode != null; siteMapNode = siteMapNode.ParentNode)
			{
				if (siteMapNode == node)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets the next <see cref="T:System.Web.SiteMapNode" /> node on the same hierarchical level as the current one, relative to the <see cref="P:System.Web.SiteMapNode.ParentNode" /> property (if one exists).</summary>
		/// <returns>The next <see cref="T:System.Web.SiteMapNode" />, serially, after the current one, under the parent node; otherwise, null, if no parent exists, there is no node that follows this one, or security trimming is enabled and the user cannot view the parent or next sibling nodes.</returns>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
		public virtual SiteMapNode NextSibling
		{
			get
			{
				IList siblingNodes = this.SiblingNodes;
				if (siblingNodes == null)
				{
					return null;
				}
				int num = siblingNodes.IndexOf(this);
				if (num >= 0 && num < siblingNodes.Count - 1)
				{
					return (SiteMapNode)siblingNodes[num + 1];
				}
				return null;
			}
		}

		/// <summary>Gets the previous <see cref="T:System.Web.SiteMapNode" /> object on the same level as the current one, relative to the <see cref="P:System.Web.SiteMapNode.ParentNode" /> object (if one exists).</summary>
		/// <returns>The previous <see cref="T:System.Web.SiteMapNode" />, serially, before the current one, under the parent node; otherwise, null, if no parent exists, there is no node before this one, or security trimming is enabled and the user cannot view the parent or previous sibling nodes.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
		public virtual SiteMapNode PreviousSibling
		{
			get
			{
				IList siblingNodes = this.SiblingNodes;
				if (siblingNodes == null)
				{
					return null;
				}
				int num = siblingNodes.IndexOf(this);
				if (num > 0 && num < siblingNodes.Count)
				{
					return (SiteMapNode)siblingNodes[num - 1];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.SiteMapNode" /> object that is the parent of the current node.</summary>
		/// <returns>The parent <see cref="T:System.Web.SiteMapNode" />; otherwise, null, if security trimming is enabled and the user cannot view the parent node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0001E634 File Offset: 0x0001C834
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0001E67E File Offset: 0x0001C87E
		public virtual SiteMapNode ParentNode
		{
			get
			{
				if (this.parent != null)
				{
					return this.parent;
				}
				SiteMapProvider parentProvider = this.provider;
				for (;;)
				{
					this.parent = parentProvider.GetParentNode(this);
					if (this.parent != null)
					{
						break;
					}
					parentProvider = parentProvider.ParentProvider;
					if (parentProvider == null)
					{
						goto Block_3;
					}
				}
				return this.parent;
				Block_3:
				return null;
			}
			set
			{
				this.CheckWritable();
				this.parent = value;
			}
		}

		/// <summary>Gets or sets all the child nodes of the current <see cref="T:System.Web.SiteMapNode" /> object from the associated <see cref="T:System.Web.SiteMapProvider" /> provider.</summary>
		/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> of child nodes, if any exist for the current node; otherwise, null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0001E690 File Offset: 0x0001C890
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0001E712 File Offset: 0x0001C912
		public virtual SiteMapNodeCollection ChildNodes
		{
			get
			{
				if (this.provider.SecurityTrimmingEnabled)
				{
					IPrincipal principal = HttpContext.Current.User;
					if ((this.user == null && this.user != principal) || (this.user != null && this.user != principal))
					{
						this.user = principal;
						this.childNodes = this.provider.GetChildNodes(this);
					}
				}
				else if (this.childNodes == null)
				{
					this.childNodes = this.provider.GetChildNodes(this);
				}
				return this.childNodes;
			}
			set
			{
				this.CheckWritable();
				this.user = null;
				this.childNodes = value;
			}
		}

		/// <summary>Gets the root node of the root provider in a site map provider hierarchy. If no provider hierarchy exists, the <see cref="P:System.Web.SiteMapNode.RootNode" /> property gets the root node of the current provider. </summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the site navigation structure.</returns>
		/// <exception cref="T:System.InvalidOperationException">The root node cannot be retrieved from the root provider.</exception>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0001E728 File Offset: 0x0001C928
		public virtual SiteMapNode RootNode
		{
			get
			{
				return this.provider.RootProvider.RootNode;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0001E73A File Offset: 0x0001C93A
		private SiteMapNodeCollection SiblingNodes
		{
			get
			{
				if (this.ParentNode != null)
				{
					return this.ParentNode.ChildNodes;
				}
				return null;
			}
		}

		/// <summary>Retrieves a localized string based on a <see cref="T:System.Web.SiteMapNode" /> attribute to localize, a default string to return if no resource is found, and a Boolean value indicating whether to throw an exception if no resource is found. </summary>
		/// <returns>A string representing the localized attribute.</returns>
		/// <param name="attributeName">The <see cref="T:System.Web.SiteMapNode" /> attribute to localize. </param>
		/// <param name="defaultValue">The default value to return if a matching resource is not found.</param>
		/// <param name="throwIfNotFound">true to throw an <see cref="T:System.InvalidOperationException" />, if an explicit resource is defined for <paramref name="attributeName" />, <paramref name="defaultValue" /> is null, and a localized value is not found; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">A matching resource object was not found and <paramref name="throwIfNotFound" /> is true. </exception>
		// Token: 0x06000B49 RID: 2889 RVA: 0x0001E754 File Offset: 0x0001C954
		protected string GetExplicitResourceString(string attributeName, string defaultValue, bool throwIfNotFound)
		{
			if (attributeName == null)
			{
				throw new ArgumentNullException("attributeName");
			}
			if (this.resourceKeys != null)
			{
				string[] values = this.resourceKeys.GetValues(attributeName);
				if (values != null && values.Length == 2)
				{
					try
					{
						object globalResourceObject = HttpContext.GetGlobalResourceObject(values[0], values[1]);
						if (globalResourceObject is string)
						{
							return (string)globalResourceObject;
						}
					}
					catch (MissingManifestResourceException)
					{
					}
					if (throwIfNotFound && defaultValue == null)
					{
						throw new InvalidOperationException(string.Format("The resource object with classname '{0}' and key '{1}' was not found.", values[0], values[1]));
					}
					return defaultValue;
				}
			}
			return defaultValue;
		}

		/// <summary>Gets a localized string based on the attribute name and <see cref="P:System.Web.SiteMapProvider.ResourceKey" /> property that is specified by the <see cref="T:System.Web.SiteMapProvider" /> by which the <see cref="T:System.Web.SiteMapNode" /> is tracked.</summary>
		/// <returns>A string representing the localized attribute. The default is null.</returns>
		/// <param name="attributeName">The <see cref="T:System.Web.SiteMapNode" /> attribute to localize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeName" /> is null. </exception>
		// Token: 0x06000B4A RID: 2890 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		protected string GetImplicitResourceString(string attributeName)
		{
			if (attributeName == null)
			{
				throw new ArgumentNullException("attributeName");
			}
			string text = this.ResourceKey;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			try
			{
				object globalResourceObject = HttpContext.GetGlobalResourceObject(this.provider.ResourceKey, text + "." + attributeName);
				if (globalResourceObject is string)
				{
					return (string)globalResourceObject;
				}
			}
			catch (MissingManifestResourceException)
			{
			}
			return null;
		}

		/// <summary>Gets or sets a custom attribute from the <see cref="P:System.Web.SiteMapNode.Attributes" /> collection or a resource string based on the specified key.</summary>
		/// <returns>A custom attribute or resource string identified by <paramref name="key" />; otherwise, null.</returns>
		/// <param name="key">A string that identifies the attribute or resource string to retrieve.</param>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003F8 RID: 1016
		public virtual string this[string key]
		{
			get
			{
				if (this.provider.EnableLocalization)
				{
					string text = this.GetImplicitResourceString(key);
					if (text == null)
					{
						text = this.GetExplicitResourceString(key, null, true);
					}
					if (text != null)
					{
						return text;
					}
				}
				if (this.attributes != null)
				{
					return this.attributes[key];
				}
				return null;
			}
			set
			{
				this.CheckWritable();
				if (this.attributes == null)
				{
					this.attributes = new NameValueCollection();
				}
				this.attributes[key] = value;
			}
		}

		/// <summary>Creates a new node that is a copy of the current node. For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new node that is a copy of the current node.</returns>
		// Token: 0x06000B4D RID: 2893 RVA: 0x0001E8C6 File Offset: 0x0001CAC6
		object ICloneable.Clone()
		{
			return this.Clone(false);
		}

		/// <summary>Creates a new node that is a copy of the current node.</summary>
		/// <returns>A new node that is a copy of the current node.</returns>
		// Token: 0x06000B4E RID: 2894 RVA: 0x0001E8C6 File Offset: 0x0001CAC6
		public virtual SiteMapNode Clone()
		{
			return this.Clone(false);
		}

		/// <summary>Creates a new copy that is a copy of the current node, optionally cloning all parent and ancestor nodes of the current node.</summary>
		/// <returns>A new node that is a copy of the current node.</returns>
		/// <param name="cloneParentNodes">true to clone all parent and ancestor nodes of the current node; otherwise, false.</param>
		// Token: 0x06000B4F RID: 2895 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
		public virtual SiteMapNode Clone(bool cloneParentNodes)
		{
			SiteMapNode siteMapNode = new SiteMapNode();
			siteMapNode.provider = this.provider;
			siteMapNode.key = this.key;
			siteMapNode.url = this.url;
			siteMapNode.title = this.title;
			siteMapNode.description = this.description;
			if (this.roles != null)
			{
				siteMapNode.roles = new ArrayList(this.roles);
			}
			if (this.attributes != null)
			{
				siteMapNode.attributes = new NameValueCollection(this.attributes);
			}
			if (cloneParentNodes && this.ParentNode != null)
			{
				siteMapNode.parent = this.ParentNode.Clone(true);
			}
			return siteMapNode;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Web.SiteMapNode" /> is identical to the specified object. </summary>
		/// <returns>true if <paramref name="obj" /> is both a <see cref="T:System.Web.SiteMapNode" /> and identical to the current <see cref="T:System.Web.SiteMapNode" />; otherwise, false. </returns>
		/// <param name="obj">An object to compare to the current <see cref="T:System.Web.SiteMapNode" />. </param>
		// Token: 0x06000B50 RID: 2896 RVA: 0x0001E970 File Offset: 0x0001CB70
		public override bool Equals(object obj)
		{
			SiteMapNode siteMapNode = obj as SiteMapNode;
			if (siteMapNode == null)
			{
				return false;
			}
			if (siteMapNode.key != this.key || siteMapNode.url != this.url || siteMapNode.title != this.title || siteMapNode.description != this.description)
			{
				return false;
			}
			if (this.roles == null || siteMapNode.roles == null)
			{
				if (this.roles != siteMapNode.roles)
				{
					return false;
				}
			}
			else
			{
				if (this.roles.Count != siteMapNode.roles.Count)
				{
					return false;
				}
				foreach (object obj2 in this.roles)
				{
					if (!siteMapNode.roles.Contains(obj2))
					{
						return false;
					}
				}
			}
			if (this.attributes == null || siteMapNode.attributes == null)
			{
				if (this.attributes != siteMapNode.attributes)
				{
					return false;
				}
			}
			else
			{
				if (this.attributes.Count != siteMapNode.attributes.Count)
				{
					return false;
				}
				foreach (object obj3 in this.attributes)
				{
					string text = (string)obj3;
					if (this.attributes[text] != siteMapNode.attributes[text])
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Returns the hash code of the <see cref="T:System.Web.SiteMapNode" /> object. </summary>
		/// <returns>A 32-bit signed integer representing the hash code.</returns>
		// Token: 0x06000B51 RID: 2897 RVA: 0x0001EB10 File Offset: 0x0001CD10
		public override int GetHashCode()
		{
			return (this.key + this.url + this.title + this.description).GetHashCode();
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0001EB34 File Offset: 0x0001CD34
		private void CheckWritable()
		{
			if (this.readOnly)
			{
				throw new InvalidOperationException("Can't modify read-only node");
			}
		}

		/// <summary>Gets or sets a collection of additional attributes beyond the strongly typed properties that are defined for the <see cref="T:System.Web.SiteMapNode" /> class.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of additional attributes for the <see cref="T:System.Web.SiteMapNode" /> beyond <see cref="P:System.Web.SiteMapNode.Title" />, <see cref="P:System.Web.SiteMapNode.Description" />, <see cref="P:System.Web.SiteMapNode.Url" />, and <see cref="P:System.Web.SiteMapNode.Roles" />; otherwise, null, if no attributes exist.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0001EB49 File Offset: 0x0001CD49
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0001EB51 File Offset: 0x0001CD51
		protected NameValueCollection Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.CheckWritable();
				this.attributes = value;
			}
		}

		/// <summary>Gets or sets a description for the <see cref="T:System.Web.SiteMapNode" />. </summary>
		/// <returns>A string that represents a description of the node; otherwise, <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0001EB60 File Offset: 0x0001CD60
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0001EBB1 File Offset: 0x0001CDB1
		[Localizable(true)]
		public virtual string Description
		{
			get
			{
				string text;
				if (this.provider.EnableLocalization)
				{
					text = this.GetImplicitResourceString("description");
					if (text == null)
					{
						text = this.GetExplicitResourceString("description", this.description, true);
					}
				}
				else
				{
					text = this.description;
				}
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.CheckWritable();
				this.description = value;
			}
		}

		/// <summary>Gets or sets the title of the <see cref="T:System.Web.SiteMapNode" /> object. </summary>
		/// <returns>A string that represents the title of the node. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001EBC0 File Offset: 0x0001CDC0
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0001EC11 File Offset: 0x0001CE11
		[Localizable(true)]
		public virtual string Title
		{
			get
			{
				string text;
				if (this.provider.EnableLocalization)
				{
					text = this.GetImplicitResourceString("title");
					if (text == null)
					{
						text = this.GetExplicitResourceString("title", this.title, true);
					}
				}
				else
				{
					text = this.title;
				}
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.CheckWritable();
				this.title = value;
			}
		}

		/// <summary>Gets or sets the URL of the page that the <see cref="T:System.Web.SiteMapNode" /> object represents.</summary>
		/// <returns>The URL of the page that the node represents. The default is <see cref="F:System.String.Empty" />. </returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0001EC20 File Offset: 0x0001CE20
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0001EC36 File Offset: 0x0001CE36
		public virtual string Url
		{
			get
			{
				if (this.url == null)
				{
					return "";
				}
				return this.url;
			}
			set
			{
				this.CheckWritable();
				this.url = value;
			}
		}

		/// <summary>Gets or sets a collection of roles that are associated with the <see cref="T:System.Web.SiteMapNode" /> object, used during security trimming. </summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of roles.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0001EC45 File Offset: 0x0001CE45
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0001EC4D File Offset: 0x0001CE4D
		public IList Roles
		{
			get
			{
				return this.roles;
			}
			set
			{
				this.CheckWritable();
				this.roles = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the site map node can be modified.</summary>
		/// <returns>true if the site map node can be modified; otherwise, false.</returns>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0001EC5C File Offset: 0x0001CE5C
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0001EC64 File Offset: 0x0001CE64
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		/// <summary>Gets or sets the resource key that is used to localize the <see cref="T:System.Web.SiteMapNode" />.</summary>
		/// <returns>A string containing the resource key name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0001EC6D File Offset: 0x0001CE6D
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0001EC75 File Offset: 0x0001CE75
		public string ResourceKey
		{
			get
			{
				return this.resourceKey;
			}
			set
			{
				if (this.ReadOnly)
				{
					throw new InvalidOperationException("The node is read-only.");
				}
				this.resourceKey = value;
			}
		}

		/// <summary>Gets a string representing a lookup key for a site map node.</summary>
		/// <returns>A string representing a lookup key.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0001EC91 File Offset: 0x0001CE91
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.SiteMapProvider" /> provider that the <see cref="T:System.Web.SiteMapNode" /> object is tracked by.</summary>
		/// <returns>The <see cref="T:System.Web.SiteMapProvider" /> that the <see cref="T:System.Web.SiteMapNode" /> is tracked by. </returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0001EC99 File Offset: 0x0001CE99
		public SiteMapProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		/// <summary>Retrieves the hierarchical children data items of the current item. For a description of this member, see <see cref="M:System.Web.UI.IHierarchyData.GetChildren" />.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> that represents the immediate children of the current item in the hierarchy.</returns>
		// Token: 0x06000B63 RID: 2915 RVA: 0x0001ECA1 File Offset: 0x0001CEA1
		IHierarchicalEnumerable IHierarchyData.GetChildren()
		{
			return this.ChildNodes;
		}

		/// <summary>Retrieves the hierarchical parent of the current item. For a description of this member, see <see cref="M:System.Web.UI.IHierarchyData.GetParent" />.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> that represents the parent of the current item in the hierarchy.</returns>
		// Token: 0x06000B64 RID: 2916 RVA: 0x0001ECA9 File Offset: 0x0001CEA9
		IHierarchyData IHierarchyData.GetParent()
		{
			return this.ParentNode;
		}

		/// <summary>Gets a value that indicates whether the current <see cref="T:System.Web.SiteMapNode" /> object has any child nodes. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.HasChildren" />.</summary>
		/// <returns>true if the node has child nodes; otherwise, false.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0001ECB1 File Offset: 0x0001CEB1
		bool IHierarchyData.HasChildren
		{
			get
			{
				return this.HasChildNodes;
			}
		}

		/// <summary>Gets the hierarchical data item. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.Item" />.</summary>
		/// <returns>An hierarchical data node object.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00002058 File Offset: 0x00000258
		object IHierarchyData.Item
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the path of the hierarchical data item. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.Path" />.</summary>
		/// <returns>The path of the data item.</returns>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0001ECB9 File Offset: 0x0001CEB9
		string IHierarchyData.Path
		{
			get
			{
				return this.Url;
			}
		}

		/// <summary>Gets a string that represents the type name of the hierarchical data item. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.Type" />.</summary>
		/// <returns>The string named "SiteMapNode".</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0001ECC1 File Offset: 0x0001CEC1
		string IHierarchyData.Type
		{
			get
			{
				return "SiteMapNode";
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.SiteMapNode.Title" /> property of the site map node. For a description of this member, see <see cref="P:System.Web.UI.INavigateUIData.Name" />.</summary>
		/// <returns>Text that is displayed for a node of a navigation control; otherwise, <see cref="F:System.String.Empty" /> if no <see cref="P:System.Web.SiteMapNode.Title" /> is set for the node.</returns>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0001E4D1 File Offset: 0x0001C6D1
		string INavigateUIData.Name
		{
			get
			{
				return this.Title;
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.SiteMapNode.Url" /> property of the site map node. For a description of this member, see <see cref="P:System.Web.UI.INavigateUIData.NavigateUrl" />.</summary>
		/// <returns>The URL to navigate to when the node is clicked; otherwise, <see cref="F:System.String.Empty" /> if no <see cref="P:System.Web.SiteMapNode.Url" /> is set for the node.</returns>
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0001ECB9 File Offset: 0x0001CEB9
		string INavigateUIData.NavigateUrl
		{
			get
			{
				return this.Url;
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.SiteMapNode.Title" /> property of the site map node. For a description of this member, see <see cref="P:System.Web.UI.INavigateUIData.Value" />.</summary>
		/// <returns>A value that is not displayed; otherwise, <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0001E4D1 File Offset: 0x0001C6D1
		string INavigateUIData.Value
		{
			get
			{
				return this.Title;
			}
		}

		// Token: 0x04001096 RID: 4246
		private SiteMapProvider provider;

		// Token: 0x04001097 RID: 4247
		private string key;

		// Token: 0x04001098 RID: 4248
		private string url;

		// Token: 0x04001099 RID: 4249
		private string title;

		// Token: 0x0400109A RID: 4250
		private string description;

		// Token: 0x0400109B RID: 4251
		private IList roles;

		// Token: 0x0400109C RID: 4252
		private NameValueCollection attributes;

		// Token: 0x0400109D RID: 4253
		private NameValueCollection resourceKeys;

		// Token: 0x0400109E RID: 4254
		private bool readOnly;

		// Token: 0x0400109F RID: 4255
		private string resourceKey;

		// Token: 0x040010A0 RID: 4256
		private SiteMapNode parent;

		// Token: 0x040010A1 RID: 4257
		private SiteMapNodeCollection childNodes;

		// Token: 0x040010A2 RID: 4258
		private IPrincipal user;
	}
}
