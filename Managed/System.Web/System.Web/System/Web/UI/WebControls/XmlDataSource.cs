using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Caching;
using System.Xml;
using System.Xml.Xsl;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an XML data source to data-bound controls.</summary>
	// Token: 0x02000453 RID: 1107
	[Designer("System.Web.UI.Design.WebControls.XmlDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[WebSysDescription("Connect to an XML file.")]
	[ToolboxBitmap("")]
	[DefaultEvent("Transforming")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[DefaultProperty("DataFile")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class XmlDataSource : HierarchicalDataSourceControl, IDataSource, IListSource
	{
		/// <summary>For a description of this member, see <see cref="E:System.Web.UI.IDataSource.DataSourceChanged" />.</summary>
		// Token: 0x140000FC RID: 252
		// (add) Token: 0x06003351 RID: 13137 RVA: 0x000790F8 File Offset: 0x000772F8
		// (remove) Token: 0x06003352 RID: 13138 RVA: 0x00079101 File Offset: 0x00077301
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

		/// <summary>Occurs before the style sheet that is defined by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Transform" /> property or identified by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.TransformFile" /> property is applied to XML data.</summary>
		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06003353 RID: 13139 RVA: 0x00089984 File Offset: 0x00087B84
		// (remove) Token: 0x06003354 RID: 13140 RVA: 0x00089997 File Offset: 0x00087B97
		public event EventHandler Transforming
		{
			add
			{
				base.Events.AddHandler(XmlDataSource.EventTransforming, value);
			}
			remove
			{
				base.Events.RemoveHandler(XmlDataSource.EventTransforming, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.XmlDataSource.Transforming" /> event before the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control performs an XSLT transformation on its XML data.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003355 RID: 13141 RVA: 0x000899AC File Offset: 0x00087BAC
		protected virtual void OnTransforming(EventArgs e)
		{
			EventHandler eventHandler = base.Events[XmlDataSource.EventTransforming] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Loads the XML data into memory, either directly from the underlying data storage or from the cache, and returns it in the form of an <see cref="T:System.Xml.XmlDataDocument" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlDataDocument" /> object that represents the XML specified in the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property or in the file identified by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property, with any transformations and <see cref="P:System.Web.UI.WebControls.XmlDataSource.XPath" /> queries applied.</returns>
		/// <exception cref="T:System.InvalidOperationException">A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control does not have the correct permissions for the Web resource.</exception>
		/// <exception cref="T:System.NotSupportedException">A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, it is not an HTTP-based URL. - or -A design-time relative path was not mapped correctly by the designer before using the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.- or -Both caching and client impersonation are enabled. The <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control does not support caching when client impersonation is enabled.</exception>
		/// <exception cref="T:System.Web.HttpException">Access is denied to the path specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property.</exception>
		// Token: 0x06003356 RID: 13142 RVA: 0x000899DC File Offset: 0x00087BDC
		public XmlDocument GetXmlDocument()
		{
			if (this._documentNeedsUpdate)
			{
				this.UpdateXml();
			}
			if (this.xmlDocument == null && this.EnableCaching)
			{
				this.xmlDocument = this.GetXmlDocumentFromCache();
			}
			if (this.xmlDocument == null)
			{
				this.xmlDocument = this.LoadXmlDocument();
				this.UpdateCache();
			}
			return this.xmlDocument;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x00089A34 File Offset: 0x00087C34
		[global::System.MonoTODO("schema")]
		private XmlDocument LoadXmlDocument()
		{
			XmlDocument xmlDocument = this.LoadFileOrData(this.DataFile, this.Data);
			if (string.IsNullOrEmpty(this.TransformFile) && string.IsNullOrEmpty(this.Transform))
			{
				return xmlDocument;
			}
			XslTransform xslTransform = new XslTransform();
			XmlDocument xmlDocument2 = this.LoadFileOrData(this.TransformFile, this.Transform);
			xslTransform.Load(xmlDocument2);
			this.OnTransforming(EventArgs.Empty);
			XmlDocument xmlDocument3 = new XmlDocument();
			xmlDocument3.Load(xslTransform.Transform(xmlDocument, this.TransformArgumentList));
			return xmlDocument3;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x00089AB4 File Offset: 0x00087CB4
		private XmlDocument LoadFileOrData(string filename, string data)
		{
			XmlDocument xmlDocument = new XmlDocument();
			if (!string.IsNullOrEmpty(filename))
			{
				Uri uri;
				if (Uri.TryCreate(filename, UriKind.Absolute, out uri))
				{
					xmlDocument.Load(filename);
				}
				else
				{
					xmlDocument.Load(base.MapPathSecure(filename));
				}
			}
			else if (!string.IsNullOrEmpty(data))
			{
				xmlDocument.LoadXml(data);
			}
			return xmlDocument;
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x00089B02 File Offset: 0x00087D02
		private XmlDocument GetXmlDocumentFromCache()
		{
			if (this.DataCache != null)
			{
				return (XmlDocument)this.DataCache[this.GetDataKey()];
			}
			return null;
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x00089B24 File Offset: 0x00087D24
		private string GetDataKey()
		{
			if (string.IsNullOrEmpty(this.DataFile) && !string.IsNullOrEmpty(this.Data))
			{
				string cacheKeyContext = this.CacheKeyContext;
				if (!string.IsNullOrEmpty(cacheKeyContext))
				{
					return cacheKeyContext;
				}
			}
			Page page = this.Page;
			string text = ((page != null) ? page.ToString() : "NullPage");
			return string.Concat(new string[] { this.TemplateSourceDirectory, "_", text, "_", this.ID });
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x00089BA5 File Offset: 0x00087DA5
		private Cache DataCache
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.InternalCache;
				}
				return null;
			}
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x00089BBC File Offset: 0x00087DBC
		private void UpdateCache()
		{
			if (!this.EnableCaching)
			{
				return;
			}
			if (this.DataCache == null)
			{
				return;
			}
			string dataKey = this.GetDataKey();
			if (this.DataCache[dataKey] != null)
			{
				this.DataCache.Remove(dataKey);
			}
			DateTime dateTime = Cache.NoAbsoluteExpiration;
			TimeSpan noSlidingExpiration = Cache.NoSlidingExpiration;
			if (this.CacheDuration > 0)
			{
				if (this.CacheExpirationPolicy == DataSourceCacheExpiry.Absolute)
				{
					dateTime = DateTime.Now.AddSeconds((double)this.CacheDuration);
				}
				else
				{
					noSlidingExpiration = new TimeSpan((long)this.CacheDuration * 10000L);
				}
			}
			CacheDependency cacheDependency;
			if (this.CacheKeyDependency.Length > 0)
			{
				cacheDependency = new CacheDependency(new string[0], new string[] { this.CacheKeyDependency });
			}
			else
			{
				cacheDependency = new CacheDependency(new string[0], new string[0]);
			}
			this.DataCache.Add(dataKey, this.xmlDocument, cacheDependency, dateTime, noSlidingExpiration, CacheItemPriority.Normal, null);
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x00089C9E File Offset: 0x00087E9E
		private void UpdateXml()
		{
			this.xmlDocument = this.LoadXmlDocument();
			this.UpdateCache();
			this._documentNeedsUpdate = false;
		}

		/// <summary>Saves the XML data currently held in memory by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control to disk if the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property is set.</summary>
		/// <exception cref="T:System.InvalidOperationException">XML data was loaded using the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property instead of the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property. - or -A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control does not have the correct permissions for the Web resource.</exception>
		/// <exception cref="T:System.NotSupportedException">A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, it is not an HTTP-based URL. - or -A design-time relative path was not mapped correctly by the designer before using the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</exception>
		/// <exception cref="T:System.Web.HttpException">Access is denied to the path specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property.</exception>
		// Token: 0x0600335E RID: 13150 RVA: 0x00089CB9 File Offset: 0x00087EB9
		public void Save()
		{
			if (!this.CanBeSaved)
			{
				throw new InvalidOperationException();
			}
			if (this.xmlDocument != null)
			{
				this.xmlDocument.Save(base.MapPathSecure(this.DataFile));
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x00089CE8 File Offset: 0x00087EE8
		private bool CanBeSaved
		{
			get
			{
				return this.Transform == string.Empty && this.TransformFile == string.Empty && this.DataFile != string.Empty;
			}
		}

		/// <summary>Gets the data source view object for the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control. The <paramref name="viewPath" /> parameter can be an XPath expression.</summary>
		/// <returns>Returns an <see cref="T:System.Web.UI.WebControls.XmlHierarchicalDataSourceView" /> object that represents a single view of the data starting with the data node identified by <paramref name="viewPath" />.</returns>
		/// <param name="viewPath">An XPath expression that identifies a node from which the current hierarchical view is built. </param>
		// Token: 0x06003360 RID: 13152 RVA: 0x00089D20 File Offset: 0x00087F20
		protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
		{
			XmlNode xmlNode = this.GetXmlDocument();
			XmlNodeList xmlNodeList = null;
			if (!string.IsNullOrEmpty(viewPath))
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode(viewPath);
				if (xmlNode2 != null)
				{
					xmlNodeList = xmlNode2.ChildNodes;
				}
			}
			else if (!string.IsNullOrEmpty(this.XPath))
			{
				xmlNodeList = xmlNode.SelectNodes(this.XPath);
			}
			else
			{
				xmlNodeList = xmlNode.ChildNodes;
			}
			return new XmlHierarchicalDataSourceView(xmlNodeList);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
		/// <returns>An object implementing <see cref="T:System.Collections.IList" /> that can be bound to a data source.</returns>
		// Token: 0x06003361 RID: 13153 RVA: 0x00032A76 File Offset: 0x00030C76
		IList IListSource.GetList()
		{
			return ListSourceHelper.GetList(this);
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</summary>
		/// <returns>true if the collection is a collection of <see cref="T:System.Collections.IList" /> objects; otherwise, false.</returns>
		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x00032AE0 File Offset: 0x00030CE0
		bool IListSource.ContainsListCollection
		{
			get
			{
				return ListSourceHelper.ContainsListCollection(this);
			}
		}

		/// <summary>Gets the named data source view associated with the data source control.</summary>
		/// <returns>Returns the named <see cref="T:System.Web.UI.WebControls.XmlDataSourceView" /> object associated with the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</returns>
		/// <param name="viewName">The name of the view to retrieve. If <see cref="F:System.String.Empty" /> is specified, the default view for the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control is retrieved. </param>
		// Token: 0x06003363 RID: 13155 RVA: 0x00089D7B File Offset: 0x00087F7B
		DataSourceView IDataSource.GetView(string viewName)
		{
			if (string.IsNullOrEmpty(viewName))
			{
				viewName = "DefaultView";
			}
			return new XmlDataSourceView(this, viewName);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IDataSource.GetViewNames" />.</summary>
		/// <returns>An object implementing <see cref="T:System.Collections.ICollection" /> containing names representing the list of view objects associated with the <see cref="T:System.Web.UI.IDataSource" /> object.</returns>
		// Token: 0x06003364 RID: 13156 RVA: 0x00089D93 File Offset: 0x00087F93
		ICollection IDataSource.GetViewNames()
		{
			return XmlDataSource.emptyNames;
		}

		/// <summary>Gets or sets the length of time, in seconds, that the data source control caches data it has retrieved.</summary>
		/// <returns>The number of seconds that the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control caches the results of a data retrieval operation. The default value is 0.</returns>
		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x00089D9A File Offset: 0x00087F9A
		// (set) Token: 0x06003366 RID: 13158 RVA: 0x00089DA2 File Offset: 0x00087FA2
		[DefaultValue(0)]
		[TypeConverter(typeof(DataSourceCacheDurationConverter))]
		public virtual int CacheDuration
		{
			get
			{
				return this._cacheDuration;
			}
			set
			{
				this._cacheDuration = value;
			}
		}

		/// <summary>Gets or sets the cache expiration policy that is combined with the cache duration to describe the caching behavior of the cache that the data source control uses.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.DataSourceCacheExpiry" /> values. The default cache expiration policy setting is <see cref="F:System.Web.UI.DataSourceCacheExpiry.Absolute" />.</returns>
		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x00089DAB File Offset: 0x00087FAB
		// (set) Token: 0x06003368 RID: 13160 RVA: 0x00089DB3 File Offset: 0x00087FB3
		[DefaultValue(DataSourceCacheExpiry.Absolute)]
		public virtual DataSourceCacheExpiry CacheExpirationPolicy
		{
			get
			{
				return this._cacheExpirationPolicy;
			}
			set
			{
				this._cacheExpirationPolicy = value;
			}
		}

		/// <summary>Gets or sets a user-defined key dependency that is linked to all data cache objects created by the data source control. All cache objects explicitly expire when the key expires.</summary>
		/// <returns>A key that identifies all cache objects created by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</returns>
		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x00089DBC File Offset: 0x00087FBC
		// (set) Token: 0x0600336A RID: 13162 RVA: 0x00089DC4 File Offset: 0x00087FC4
		[DefaultValue("")]
		public virtual string CacheKeyDependency
		{
			get
			{
				return this._cacheKeyDependency;
			}
			set
			{
				this._cacheKeyDependency = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control has data caching enabled.</summary>
		/// <returns>true if data caching is enabled for the data source control; otherwise, false. The default value is true.</returns>
		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x00089DCD File Offset: 0x00087FCD
		// (set) Token: 0x0600336C RID: 13164 RVA: 0x00089DD5 File Offset: 0x00087FD5
		[DefaultValue(true)]
		public virtual bool EnableCaching
		{
			get
			{
				return this._enableCaching;
			}
			set
			{
				this._enableCaching = value;
			}
		}

		/// <summary>Gets or sets a block of XML data that the data source control binds to.</summary>
		/// <returns>A string of inline XML data that the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control binds to. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x00089DDE File Offset: 0x00087FDE
		// (set) Token: 0x0600336E RID: 13166 RVA: 0x00089DE6 File Offset: 0x00087FE6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("Inline XML data.")]
		[WebCategory("Data")]
		[DefaultValue("")]
		[TypeConverter(typeof(MultilineStringConverter))]
		public virtual string Data
		{
			get
			{
				return this._data;
			}
			set
			{
				if (this._data != value)
				{
					this._data = value;
					this._documentNeedsUpdate = true;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Specifies the file name of an XML file that the data source binds to.</summary>
		/// <returns>The absolute physical path or relative path of the XML file that contains data that the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control represents. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x00089E0F File Offset: 0x0008800F
		// (set) Token: 0x06003370 RID: 13168 RVA: 0x00089E17 File Offset: 0x00088017
		[global::System.MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
		[Editor("System.Web.UI.Design.XmlDataFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string DataFile
		{
			get
			{
				return this._dataFile;
			}
			set
			{
				if (this._dataFile != value)
				{
					this._dataFile = value;
					this._documentNeedsUpdate = true;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Provides a list of XSLT arguments that are used with the style sheet defined by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Transform" /> or <see cref="P:System.Web.UI.WebControls.XmlDataSource.TransformFile" /> properties to perform a transformation on the XML data.</summary>
		/// <returns>An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> object that contains XSLT parameters and objects to be applied to XML data when it is loaded by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control. The default value is null.</returns>
		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x00089E40 File Offset: 0x00088040
		// (set) Token: 0x06003372 RID: 13170 RVA: 0x00089E48 File Offset: 0x00088048
		[Browsable(false)]
		public virtual XsltArgumentList TransformArgumentList
		{
			get
			{
				return this.transformArgumentList;
			}
			set
			{
				this.transformArgumentList = value;
			}
		}

		/// <summary>Gets or sets a block of Extensible Stylesheet Language (XSL) data that defines an XSLT transformation to be performed on the XML data managed by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
		/// <returns>A string of inline XSL that defines an XML transformation to be performed on the data contained in the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> or <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> properties. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x00089E51 File Offset: 0x00088051
		// (set) Token: 0x06003374 RID: 13172 RVA: 0x00089E59 File Offset: 0x00088059
		[TypeConverter(typeof(MultilineStringConverter))]
		[DefaultValue("")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual string Transform
		{
			get
			{
				return this._transform;
			}
			set
			{
				if (this._transform != value)
				{
					this._transform = value;
					this._documentNeedsUpdate = true;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Specifies the file name of an Extensible Stylesheet Language (XSL) file (.xsl) that defines an XSLT transformation to be performed on the XML data managed by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
		/// <returns>The absolute physical path or relative path of the XSL style sheet file that defines an XML transformation to be performed on the data contained in the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> or <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> properties. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x00089E82 File Offset: 0x00088082
		// (set) Token: 0x06003376 RID: 13174 RVA: 0x00089E8A File Offset: 0x0008808A
		[global::System.MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XslTransformFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string TransformFile
		{
			get
			{
				return this._transformFile;
			}
			set
			{
				if (this._transformFile != value)
				{
					this._transformFile = value;
					this._documentNeedsUpdate = true;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Specifies an XPath expression to be applied to the XML data contained by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property or by the XML file indicated by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property.</summary>
		/// <returns>A string that represents an XPath expression that can be used to filter the data contained by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property or by the XML file indicated by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x00089EB3 File Offset: 0x000880B3
		// (set) Token: 0x06003378 RID: 13176 RVA: 0x00089EBB File Offset: 0x000880BB
		[DefaultValue("")]
		public virtual string XPath
		{
			get
			{
				return this._xpath;
			}
			set
			{
				if (this._xpath != value)
				{
					this._xpath = value;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the value of the cache key for the data source control from view state, or adds the cache key to view state.</summary>
		/// <returns>The value of the cache key, or an empty string if the cache key is not in view state.</returns>
		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x00089EDD File Offset: 0x000880DD
		// (set) Token: 0x0600337A RID: 13178 RVA: 0x00089EF4 File Offset: 0x000880F4
		[DefaultValue("")]
		public virtual string CacheKeyContext
		{
			get
			{
				return this.ViewState.GetString("CacheKeyContext", string.Empty);
			}
			set
			{
				this.ViewState["CacheKeyContext"] = value;
			}
		}

		// Token: 0x04001CC9 RID: 7369
		private string _data = string.Empty;

		// Token: 0x04001CCA RID: 7370
		private string _transform = string.Empty;

		// Token: 0x04001CCB RID: 7371
		private string _xpath = string.Empty;

		// Token: 0x04001CCC RID: 7372
		private string _dataFile = string.Empty;

		// Token: 0x04001CCD RID: 7373
		private string _transformFile = string.Empty;

		// Token: 0x04001CCE RID: 7374
		private string _cacheKeyDependency = string.Empty;

		// Token: 0x04001CCF RID: 7375
		private bool _enableCaching = true;

		// Token: 0x04001CD0 RID: 7376
		private int _cacheDuration;

		// Token: 0x04001CD1 RID: 7377
		private bool _documentNeedsUpdate;

		// Token: 0x04001CD2 RID: 7378
		private DataSourceCacheExpiry _cacheExpirationPolicy;

		// Token: 0x04001CD3 RID: 7379
		private static readonly string[] emptyNames = new string[] { "DefaultView" };

		// Token: 0x04001CD4 RID: 7380
		private static object EventTransforming = new object();

		// Token: 0x04001CD5 RID: 7381
		private XmlDocument xmlDocument;

		// Token: 0x04001CD6 RID: 7382
		private XsltArgumentList transformArgumentList;
	}
}
