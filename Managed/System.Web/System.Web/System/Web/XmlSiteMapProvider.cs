using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.IO;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;
using System.Xml;

namespace System.Web
{
	/// <summary>The <see cref="T:System.Web.XmlSiteMapProvider" /> class is derived from the <see cref="T:System.Web.SiteMapProvider" /> class and is the default site map provider for ASP.NET. The <see cref="T:System.Web.XmlSiteMapProvider" /> class generates site map trees from XML files with the file name extension .sitemap.</summary>
	// Token: 0x020000ED RID: 237
	public class XmlSiteMapProvider : StaticSiteMapProvider, IDisposable
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x000222BB File Offset: 0x000204BB
		private Dictionary<string, bool> ChildProvidersPresent
		{
			get
			{
				if (this._childProvidersPresent == null)
				{
					this._childProvidersPresent = new Dictionary<string, bool>();
				}
				return this._childProvidersPresent;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x000222D6 File Offset: 0x000204D6
		private List<SiteMapProvider> ChildProviders
		{
			get
			{
				if (this._childProviders == null)
				{
					this._childProviders = new List<SiteMapProvider>();
				}
				return this._childProviders;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.SiteMapNode" /> object to the collections that are maintained by the current provider.</summary>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the provider.</param>
		/// <param name="parentNode">The <see cref="T:System.Web.SiteMapNode" /> under which to add <paramref name="node" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> or <paramref name="parentNode" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Web.XmlSiteMapProvider" /> is not the provider associated with <paramref name="node" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A node with the same URL or key is already registered with the <see cref="T:System.Web.XmlSiteMapProvider" />. - or -A duplicate site map node has been encountered programmatically, such as when linking two site map providers.- or -<paramref name="node" /> is the root node of the <see cref="T:System.Web.XmlSiteMapProvider" />.</exception>
		// Token: 0x06000CA9 RID: 3241 RVA: 0x000222F4 File Offset: 0x000204F4
		protected internal override void AddNode(SiteMapNode node, SiteMapNode parentNode)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (parentNode == null)
			{
				throw new ArgumentNullException("parentNode");
			}
			SiteMapProvider provider = node.Provider;
			if (provider != this)
			{
				throw new ArgumentException("SiteMapNode '" + node + "' cannot be found in current provider, only nodes in the same provider can be added.", "node");
			}
			SiteMapProvider provider2 = parentNode.Provider;
			if (provider != provider2)
			{
				throw new ArgumentException("SiteMapNode '" + parentNode + "' cannot be found in current provider, only nodes in the same provider can be added.", "parentNode");
			}
			this.AddNodeNoCheck(node, parentNode);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00022370 File Offset: 0x00020570
		private void AddNodeNoCheck(SiteMapNode node, SiteMapNode parentNode)
		{
			base.AddNode(node, parentNode);
			SiteMapProvider provider = node.Provider;
			if (provider != this)
			{
				this.RegisterChildProvider(provider.Name, provider);
			}
		}

		/// <summary>Links a child site map provider to the current provider. </summary>
		/// <param name="providerName">The name of one of the <see cref="T:System.Web.SiteMapProvider" /> objects currently registered in the <see cref="P:System.Web.SiteMap.Providers" />.</param>
		/// <param name="parentNode">A site map node of the current site map provider under which the root node and all nodes of the child provider is added.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.SiteMapNode.Provider" /> property of the <paramref name="parentNode" /> does not reference the current provider. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parentNode" /> is null.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="providerName" /> cannot be resolved.</exception>
		/// <exception cref="T:System.InvalidOperationException">The site map file used by <paramref name="providerName" /> is already in use within the provider hierarchy. -or-The root node returned by <paramref name="providerName" /> is null.-or-The root node returned by <paramref name="providerName" /> has a URL or key that is already registered with the parent <see cref="T:System.Web.XmlSiteMapProvider" />.   </exception>
		// Token: 0x06000CAB RID: 3243 RVA: 0x000223A0 File Offset: 0x000205A0
		protected virtual void AddProvider(string providerName, SiteMapNode parentNode)
		{
			if (parentNode == null)
			{
				throw new ArgumentNullException("parentNode");
			}
			if (parentNode.Provider != this)
			{
				throw new ArgumentException("The Provider property of the parentNode does not reference the current provider.", "parentNode");
			}
			SiteMapProvider siteMapProvider = SiteMap.Providers[providerName];
			if (siteMapProvider == null)
			{
				throw new ProviderException("Provider with name [" + providerName + "] was not found.");
			}
			this.AddNode(siteMapProvider.GetRootNodeCore());
			this.RegisterChildProvider(providerName, siteMapProvider);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00022410 File Offset: 0x00020610
		private void RegisterChildProvider(string name, SiteMapProvider smp)
		{
			Dictionary<string, bool> childProvidersPresent = this.ChildProvidersPresent;
			if (childProvidersPresent.ContainsKey(name))
			{
				return;
			}
			childProvidersPresent.Add(name, true);
			this.ChildProviders.Add(smp);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00022444 File Offset: 0x00020644
		private XmlNode FindStartingNode(string virtualPath, out bool enableLocalization)
		{
			XmlElement documentElement = this.GetConfigDocument(virtualPath).DocumentElement;
			if (string.Compare("siteMap", documentElement.Name, StringComparison.Ordinal) != 0)
			{
				throw new ConfigurationErrorsException("Top element must be 'siteMap'");
			}
			XmlNode xmlNode = documentElement.Attributes["enableLocalization"];
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.Value))
			{
				enableLocalization = (bool)Convert.ChangeType(xmlNode.Value, typeof(bool));
			}
			else
			{
				enableLocalization = false;
			}
			XmlNodeList childNodes = documentElement.ChildNodes;
			XmlNode xmlNode2 = null;
			foreach (object obj in childNodes)
			{
				XmlNode xmlNode3 = (XmlNode)obj;
				if (string.Compare("siteMapNode", xmlNode3.Name, StringComparison.Ordinal) != 0)
				{
					throw new ConfigurationErrorsException("Only <siteMapNode> elements are allowed at the document top level.");
				}
				if (xmlNode2 != null)
				{
					throw new ConfigurationErrorsException("Only one <siteMapNode> element is allowed at the document top level.");
				}
				xmlNode2 = xmlNode3;
			}
			if (xmlNode2 == null)
			{
				throw new ConfigurationErrorsException("Missing <siteMapNode> element at the document top level.");
			}
			return xmlNode2;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00022548 File Offset: 0x00020748
		private XmlDocument GetConfigDocument(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentException("The siteMapFile attribute must be specified on the XmlSiteMapProvider");
			}
			string text = HostingEnvironment.MapPath(virtualPath);
			if (text == null)
			{
				throw new HttpException("Virtual path '" + virtualPath + "' cannot be mapped to physical path.");
			}
			if (string.Compare(Path.GetExtension(text), ".sitemap", RuntimeHelpers.StringComparison) != 0)
			{
				throw new InvalidOperationException(string.Format("The file {0} has an invalid extension, only .sitemap files are allowed in XmlSiteMapProvider.", string.IsNullOrEmpty(virtualPath) ? Path.GetFileName(text) : virtualPath));
			}
			if (!File.Exists(text))
			{
				throw new InvalidOperationException(string.Format("The file '{0}' required by XmlSiteMapProvider does not exist.", string.IsNullOrEmpty(virtualPath) ? Path.GetFileName(text) : virtualPath));
			}
			base.ResourceKey = Path.GetFileName(text);
			this.CreateWatcher(text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(text);
			return xmlDocument;
		}

		/// <summary>Loads the site map information from an XML file and builds it in memory.</summary>
		/// <returns>Returns the root <see cref="T:System.Web.SiteMapNode" /> of the site map navigation structure.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.XmlSiteMapProvider" /> was not initialized properly.- or -A siteMapFile is parsed for a &lt;siteMapNode&gt; that is not unique.- or -The file specified by the siteMapFile does not have the file name extension .sitemap.- or -The file specified by the siteMapFile does not exist.- or -A provider configured in the provider of a &lt;siteMapNode&gt; returns a null root node. </exception>
		/// <exception cref="T:System.ArgumentException">The siteMapFile is specified but the path lies outside the current directory structure for the application.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There is an error loading the configuration file.- or -The top element of the configuration file is not named &lt;siteMap&gt;.- or - More than one top node exists in the configuration file.- or -A child of the &lt;siteMap&gt; has a name other than &lt;siteMapNode&gt;. - or -An unexpected attribute is parsed for the &lt;siteMapNode&gt;.- or -Sub-elements are nested beneath a &lt;siteMapNode&gt; where the provider is set.- or -The roles of the &lt;siteMapNode&gt; contain characters that are not valid.- or - A url is parsed for a &lt;siteMapNode&gt; that is not unique.- or - A <see cref="T:System.Web.SiteMapNode" /> was encountered with a duplicate value for <see cref="P:System.Web.SiteMapNode.Key" />. - or -The <see cref="P:System.Web.SiteMapNode.ResourceKey" /> or <see cref="P:System.Web.SiteMapNode.Title" /> was specified on a <see cref="T:System.Web.SiteMapNode" /> or a custom attribute defined for the node contained an explicit resource expression.- or -An explicit resource expression was applied either to the <see cref="P:System.Web.SiteMapNode.Title" /> or <see cref="P:System.Web.SiteMapNode.Description" /> or to a custom attribute of a <see cref="T:System.Web.SiteMapNode" /> but the explicit information was not valid.- or -An error occurred while parsing the <see cref="P:System.Web.SiteMapNode.Url" /> of a <see cref="T:System.Web.SiteMapNode" />.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">A named provider cannot be found in the current site map providers collection. </exception>
		/// <exception cref="T:System.ArgumentNullException">A &lt;siteMapNode&gt; referencing a site map file contains an empty string for the siteMapFile.</exception>
		/// <exception cref="T:System.Web.HttpException">A siteMapFile of a &lt;siteMapNode&gt; uses a physical path.- or -An error occurred while attempting to parse the virtual path to the file specified in the siteMapFile.</exception>
		// Token: 0x06000CAF RID: 3247 RVA: 0x00022608 File Offset: 0x00020808
		public override SiteMapNode BuildSiteMap()
		{
			if (this.root != null)
			{
				return this.root;
			}
			object this_lock = this.this_lock;
			SiteMapNode siteMapNode;
			lock (this_lock)
			{
				if (this.root != null)
				{
					siteMapNode = this.root;
				}
				else
				{
					this.Clear();
					bool flag2;
					XmlNode xmlNode = this.FindStartingNode(this.fileVirtualPath, out flag2);
					base.EnableLocalization = flag2;
					this.BuildSiteMapRecursive(xmlNode, null);
					siteMapNode = this.root;
				}
			}
			return siteMapNode;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00022694 File Offset: 0x00020894
		private SiteMapNode ConvertToSiteMapNode(XmlNode xmlNode)
		{
			bool enableLocalization = base.EnableLocalization;
			string text = this.GetOptionalAttribute(xmlNode, "url");
			string optionalAttribute = this.GetOptionalAttribute(xmlNode, "title");
			string optionalAttribute2 = this.GetOptionalAttribute(xmlNode, "description");
			string optionalAttribute3 = this.GetOptionalAttribute(xmlNode, "roles");
			string optionalAttribute4 = this.GetOptionalAttribute(xmlNode, "resourceKey");
			List<string> list = new List<string>();
			if (optionalAttribute3 != null && optionalAttribute3.Length > 0)
			{
				string[] array = optionalAttribute3.Split(XmlSiteMapProvider.seperators);
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i].Trim();
					if (text2.Length > 0)
					{
						list.Add(text2);
					}
				}
			}
			text = base.MapUrl(text);
			NameValueCollection nameValueCollection = null;
			NameValueCollection nameValueCollection2 = null;
			if (enableLocalization)
			{
				this.CollectLocalizationInfo(xmlNode, ref optionalAttribute, ref optionalAttribute2, ref nameValueCollection, ref nameValueCollection2);
			}
			else
			{
				foreach (object obj in xmlNode.Attributes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					this.PutInCollection(xmlNode2.Name, xmlNode2.Value, ref nameValueCollection);
				}
			}
			string text3 = Guid.NewGuid().ToString();
			return new SiteMapNode(this, text3, text, optionalAttribute, optionalAttribute2, list.AsReadOnly(), nameValueCollection, nameValueCollection2, optionalAttribute4);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000227F4 File Offset: 0x000209F4
		private void BuildSiteMapRecursive(XmlNode xmlNode, SiteMapNode parent)
		{
			if (xmlNode.Name != "siteMapNode")
			{
				throw new ConfigurationException("incorrect element name", xmlNode);
			}
			string text = this.GetNonEmptyOptionalAttribute(xmlNode, "provider");
			if (text != null)
			{
				SiteMapProvider siteMapProvider = SiteMap.Providers[text];
				if (siteMapProvider == null)
				{
					throw new ProviderException("Provider with name [" + text + "] was not found.");
				}
				siteMapProvider.ParentProvider = this;
				SiteMapNode rootNodeCore = siteMapProvider.GetRootNodeCore();
				if (parent == null)
				{
					this.root = rootNodeCore;
					return;
				}
				this.AddNodeNoCheck(rootNodeCore, parent);
				return;
			}
			else
			{
				text = this.GetNonEmptyOptionalAttribute(xmlNode, "siteMapFile");
				if (text != null)
				{
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection.Add("siteMapFile", text);
					string optionalAttribute = this.GetOptionalAttribute(xmlNode, "description");
					if (!string.IsNullOrEmpty(optionalAttribute))
					{
						nameValueCollection.Add("description", optionalAttribute);
					}
					string text2 = base.MapUrl(text);
					XmlSiteMapProvider xmlSiteMapProvider = new XmlSiteMapProvider();
					xmlSiteMapProvider.Initialize(text2, nameValueCollection);
					SiteMapNode rootNodeCore2 = xmlSiteMapProvider.GetRootNodeCore();
					if (parent == null)
					{
						this.root = rootNodeCore2;
						return;
					}
					this.AddNodeNoCheck(rootNodeCore2, parent);
					return;
				}
				else
				{
					SiteMapNode siteMapNode = this.ConvertToSiteMapNode(xmlNode);
					if (parent == null)
					{
						this.root = siteMapNode;
					}
					else
					{
						this.AddNodeNoCheck(siteMapNode, parent);
					}
					XmlNodeList childNodes = xmlNode.ChildNodes;
					if (childNodes == null || childNodes.Count < 1)
					{
						return;
					}
					foreach (object obj in childNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						if (xmlNode2.NodeType == XmlNodeType.Element)
						{
							this.BuildSiteMapRecursive(xmlNode2, siteMapNode);
						}
					}
					return;
				}
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00022980 File Offset: 0x00020B80
		private string GetNonEmptyOptionalAttribute(XmlNode n, string name)
		{
			return HandlersUtil.ExtractAttributeValue(name, n, true);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002298A File Offset: 0x00020B8A
		private string GetOptionalAttribute(XmlNode n, string name)
		{
			return HandlersUtil.ExtractAttributeValue(name, n, true, true);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00022995 File Offset: 0x00020B95
		private void PutInCollection(string name, string value, ref NameValueCollection coll)
		{
			this.PutInCollection(name, null, value, ref coll);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x000229A1 File Offset: 0x00020BA1
		private void PutInCollection(string name, string classKey, string value, ref NameValueCollection coll)
		{
			if (coll == null)
			{
				coll = new NameValueCollection();
			}
			if (!string.IsNullOrEmpty(classKey))
			{
				coll.Add(name, classKey);
			}
			coll.Add(name, value);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000229CC File Offset: 0x00020BCC
		private bool GetAttributeLocalization(string value, out string resClass, out string resKey, out string resDefault)
		{
			resClass = null;
			resKey = null;
			resDefault = null;
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			string text = value.TrimStart(new char[] { ' ', '\t' });
			if (text.Length < 11 || string.Compare(text, 0, "$resources:", 0, 11, StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				return false;
			}
			text = text.Substring(11);
			if (text.Length == 0)
			{
				return false;
			}
			string[] array = text.Split(new char[] { ',' });
			if (array.Length < 2)
			{
				return false;
			}
			resClass = array[0].Trim();
			resKey = array[1].Trim();
			if (array.Length == 3)
			{
				resDefault = array[2];
			}
			else if (array.Length > 3)
			{
				resDefault = string.Join(",", array, 2, array.Length - 2);
			}
			return true;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00022A8C File Offset: 0x00020C8C
		private void CollectLocalizationInfo(XmlNode xmlNode, ref string title, ref string description, ref NameValueCollection attributes, ref NameValueCollection explicitResourceKeys)
		{
			string text;
			string text2;
			string text3;
			if (this.GetAttributeLocalization(title, out text, out text2, out text3))
			{
				this.PutInCollection("title", text, text2, ref explicitResourceKeys);
				title = text3;
			}
			if (this.GetAttributeLocalization(description, out text, out text2, out text3))
			{
				this.PutInCollection("description", text, text2, ref explicitResourceKeys);
				description = text3;
			}
			foreach (object obj in xmlNode.Attributes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				string text4;
				if (this.GetAttributeLocalization(xmlNode2.Value, out text, out text2, out text3))
				{
					this.PutInCollection(xmlNode2.Name, text, text2, ref explicitResourceKeys);
					text4 = text3;
				}
				else
				{
					text4 = xmlNode2.Value;
				}
				this.PutInCollection(xmlNode2.Name, text4, ref attributes);
			}
		}

		/// <summary>Removes all elements in the collections of child and parent site map nodes and site map providers that the <see cref="T:System.Web.XmlSiteMapProvider" /> object internally tracks as part of its state.</summary>
		// Token: 0x06000CB8 RID: 3256 RVA: 0x00022B6C File Offset: 0x00020D6C
		protected override void Clear()
		{
			base.Clear();
			this.root = null;
			this.ChildProviders.Clear();
			this.ChildProvidersPresent.Clear();
		}

		/// <summary>Notifies the file monitor of the Web.sitemap file that the <see cref="T:System.Web.XmlSiteMapProvider" /> object no longer requires the file to be monitored. The <see cref="M:System.Web.XmlSiteMapProvider.Dispose(System.Boolean)" /> method takes a Boolean parameter indicating whether the method is called by user code.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00022B94 File Offset: 0x00020D94
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (FileSystemWatcher fileSystemWatcher in this.watchers)
				{
					fileSystemWatcher.Dispose();
				}
				this.watchers = null;
			}
		}

		/// <summary>Notifies the file monitor of the Web.sitemap file that the <see cref="T:System.Web.XmlSiteMapProvider" /> object no longer requires the file to be monitored.</summary>
		// Token: 0x06000CBA RID: 3258 RVA: 0x00022BF0 File Offset: 0x00020DF0
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the page at the specified URL.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="rawURL" />.</returns>
		/// <param name="rawUrl">A URL that identifies the page for which to retrieve a <see cref="T:System.Web.SiteMapNode" />. </param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">A child provider linked to the current site map provider returned a node that is not valid.</exception>
		// Token: 0x06000CBB RID: 3259 RVA: 0x00022BFC File Offset: 0x00020DFC
		public override SiteMapNode FindSiteMapNode(string rawUrl)
		{
			SiteMapNode siteMapNode = base.FindSiteMapNode(rawUrl);
			if (siteMapNode != null)
			{
				return siteMapNode;
			}
			siteMapNode = this.RootNode;
			string text = base.MapUrl(rawUrl);
			if (siteMapNode != null && string.Compare(text, siteMapNode.Url, RuntimeHelpers.StringComparison) == 0)
			{
				return siteMapNode;
			}
			foreach (SiteMapProvider siteMapProvider in this.ChildProviders)
			{
				siteMapNode = siteMapProvider.FindSiteMapNode(text);
				if (siteMapNode != null)
				{
					return siteMapNode;
				}
			}
			return null;
		}

		/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object based on a specified key.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="key" />; otherwise, null, if security trimming is enabled and the node cannot be shown to the current user or the node is not found by <paramref name="key" /> in the node collection.</returns>
		/// <param name="key">A lookup key with which to search for a <see cref="T:System.Web.SiteMapNode" />.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">A child provider linked to the current site map provider returned a node that is not valid.</exception>
		// Token: 0x06000CBC RID: 3260 RVA: 0x00022C8C File Offset: 0x00020E8C
		public override SiteMapNode FindSiteMapNodeFromKey(string key)
		{
			SiteMapNode siteMapNode = base.FindSiteMapNodeFromKey(key);
			if (siteMapNode != null)
			{
				return siteMapNode;
			}
			foreach (SiteMapProvider siteMapProvider in this.ChildProviders)
			{
				siteMapNode = siteMapProvider.FindSiteMapNodeFromKey(key);
				if (siteMapNode != null)
				{
					return siteMapNode;
				}
			}
			return null;
		}

		/// <summary>Initializes the <see cref="T:System.Web.XmlSiteMapProvider" /> object. The <see cref="M:System.Web.XmlSiteMapProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" /> method does not actually build a site map, it only prepares the state of the <see cref="T:System.Web.XmlSiteMapProvider" /> to do so.</summary>
		/// <param name="name">The <see cref="T:System.Web.XmlSiteMapProvider" /> to initialize. </param>
		/// <param name="attributes">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that can contain additional attributes to help initialize <paramref name="name" />. These attributes are read from the <see cref="T:System.Web.XmlSiteMapProvider" /> configuration in the Web.config file. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.XmlSiteMapProvider" /> is initialized more than once.</exception>
		/// <exception cref="T:System.Web.HttpException">A <see cref="T:System.Web.SiteMapNode" /> used a physical path to reference a site map file.- or -An error occurred while attempting to parse the virtual path supplied for the siteMapFile attribute.</exception>
		// Token: 0x06000CBD RID: 3261 RVA: 0x00022CF8 File Offset: 0x00020EF8
		public override void Initialize(string name, NameValueCollection attributes)
		{
			if (this.initialized)
			{
				throw new InvalidOperationException("XmlSiteMapProvider cannot be initialized twice.");
			}
			this.initialized = true;
			if (attributes != null)
			{
				foreach (string text in attributes.AllKeys)
				{
					if (!(text == "siteMapFile"))
					{
						if (!(text == "description") && !(text == "securityTrimmingEnabled"))
						{
							throw new ConfigurationErrorsException(string.Concat(new string[] { "The attribute '", text, "' is unexpected in the configuration of the '", name, "' provider." }));
						}
					}
					else
					{
						this.fileVirtualPath = base.MapUrl(attributes["siteMapFile"]);
					}
				}
			}
			base.Initialize(name, (attributes != null) ? attributes : new NameValueCollection());
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00022DC8 File Offset: 0x00020FC8
		private void CreateWatcher(string file)
		{
			FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
			fileSystemWatcher.NotifyFilter |= NotifyFilters.Size;
			fileSystemWatcher.Path = Path.GetFullPath(Path.GetDirectoryName(file));
			fileSystemWatcher.Filter = Path.GetFileName(file);
			fileSystemWatcher.Changed += this.OnFileChanged;
			fileSystemWatcher.EnableRaisingEvents = true;
			if (this.watchers == null)
			{
				this.watchers = new List<FileSystemWatcher>();
			}
			this.watchers.Add(fileSystemWatcher);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.SiteMapNode" /> object from all node collections that are tracked by the provider.</summary>
		/// <param name="node">The node to remove from the node collections.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is the root node of the site map provider that owns it.- or -<paramref name="node" /> is not managed by the provider or by a provider in the chain of parent providers for this provider.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null. </exception>
		// Token: 0x06000CBF RID: 3263 RVA: 0x00022E3E File Offset: 0x0002103E
		protected override void RemoveNode(SiteMapNode node)
		{
			base.RemoveNode(node);
		}

		/// <summary>Removes a linked child site map provider from the hierarchy for the current provider.</summary>
		/// <param name="providerName">The name of one of the <see cref="T:System.Web.SiteMapProvider" /> objects currently registered in the <see cref="P:System.Web.SiteMap.Providers" />.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="providerName" /> cannot be resolved.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="providerName" /> is not a registered child provider of the current site map provider.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerName" /> is null.</exception>
		// Token: 0x06000CC0 RID: 3264 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		protected virtual void RemoveProvider(string providerName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00022E47 File Offset: 0x00021047
		private void OnFileChanged(object sender, FileSystemEventArgs args)
		{
			this.Clear();
		}

		/// <summary>Gets the root node of the site map.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the site map; otherwise, null, if security trimming is enabled and the root node is not accessible to the current user.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00022E4F File Offset: 0x0002104F
		public override SiteMapNode RootNode
		{
			get
			{
				this.BuildSiteMap();
				return this.root;
			}
		}

		/// <summary>Retrieves the top-level node of the current site map data structure.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the top-level node in the current site map data structure.</returns>
		// Token: 0x06000CC3 RID: 3267 RVA: 0x00022E5E File Offset: 0x0002105E
		protected internal override SiteMapNode GetRootNodeCore()
		{
			return this.BuildSiteMap();
		}

		// Token: 0x0400111A RID: 4378
		private static readonly char[] seperators = new char[] { ';', ',' };

		// Token: 0x0400111B RID: 4379
		private bool initialized;

		// Token: 0x0400111C RID: 4380
		private string fileVirtualPath;

		// Token: 0x0400111D RID: 4381
		private SiteMapNode root;

		// Token: 0x0400111E RID: 4382
		private List<FileSystemWatcher> watchers;

		// Token: 0x0400111F RID: 4383
		private Dictionary<string, bool> _childProvidersPresent;

		// Token: 0x04001120 RID: 4384
		private List<SiteMapProvider> _childProviders;
	}
}
