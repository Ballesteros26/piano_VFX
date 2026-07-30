using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200060B RID: 1547
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlQueryContext
	{
		// Token: 0x06003C2C RID: 15404 RVA: 0x00150088 File Offset: 0x0014E288
		internal XmlQueryContext(XmlQueryRuntime runtime, object defaultDataSource, XmlResolver dataSources, XsltArgumentList argList, WhitespaceRuleLookup wsRules)
		{
			this.runtime = runtime;
			this.dataSources = dataSources;
			this.dataSourceCache = new Hashtable();
			this.argList = argList;
			this.wsRules = wsRules;
			if (defaultDataSource is XmlReader)
			{
				this.readerSettings = new QueryReaderSettings((XmlReader)defaultDataSource);
			}
			else
			{
				this.readerSettings = new QueryReaderSettings(new NameTable());
			}
			if (defaultDataSource is string)
			{
				this.defaultDataSource = this.GetDataSource(defaultDataSource as string, null);
				if (this.defaultDataSource == null)
				{
					throw new XslTransformException("Data source '{0}' cannot be located.", new string[] { defaultDataSource as string });
				}
			}
			else if (defaultDataSource != null)
			{
				this.defaultDataSource = this.ConstructDocument(defaultDataSource, null, null);
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x0015013E File Offset: 0x0014E33E
		public XmlNameTable QueryNameTable
		{
			get
			{
				return this.readerSettings.NameTable;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x0015014B File Offset: 0x0014E34B
		public XmlNameTable DefaultNameTable
		{
			get
			{
				if (this.defaultDataSource == null)
				{
					return null;
				}
				return this.defaultDataSource.NameTable;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x00150162 File Offset: 0x0014E362
		public XPathNavigator DefaultDataSource
		{
			get
			{
				if (this.defaultDataSource == null)
				{
					throw new XslTransformException("Query requires a default data source, but no default was supplied to the query engine.", new string[] { string.Empty });
				}
				return this.defaultDataSource;
			}
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x0015018C File Offset: 0x0014E38C
		public XPathNavigator GetDataSource(string uriRelative, string uriBase)
		{
			XPathNavigator xpathNavigator = null;
			try
			{
				Uri uri = ((uriBase != null) ? this.dataSources.ResolveUri(null, uriBase) : null);
				Uri uri2 = this.dataSources.ResolveUri(uri, uriRelative);
				if (uri2 != null)
				{
					xpathNavigator = this.dataSourceCache[uri2] as XPathNavigator;
				}
				if (xpathNavigator == null)
				{
					object entity = this.dataSources.GetEntity(uri2, null, null);
					if (entity != null)
					{
						xpathNavigator = this.ConstructDocument(entity, uriRelative, uri2);
						this.dataSourceCache.Add(uri2, xpathNavigator);
					}
				}
			}
			catch (XslTransformException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (!XmlException.IsCatchableException(ex))
				{
					throw;
				}
				throw new XslTransformException(ex, "An error occurred while loading document '{0}'. See InnerException for a complete description of the error.", new string[] { uriRelative });
			}
			return xpathNavigator;
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x00150248 File Offset: 0x0014E448
		private XPathNavigator ConstructDocument(object dataSource, string uriRelative, Uri uriResolved)
		{
			Stream stream = dataSource as Stream;
			if (stream != null)
			{
				XmlReader xmlReader = this.readerSettings.CreateReader(stream, (uriResolved != null) ? uriResolved.ToString() : null);
				try
				{
					return new XPathDocument(WhitespaceRuleReader.CreateReader(xmlReader, this.wsRules), XmlSpace.Preserve).CreateNavigator();
				}
				finally
				{
					xmlReader.Close();
				}
			}
			if (dataSource is XmlReader)
			{
				return new XPathDocument(WhitespaceRuleReader.CreateReader(dataSource as XmlReader, this.wsRules), XmlSpace.Preserve).CreateNavigator();
			}
			if (!(dataSource is IXPathNavigable))
			{
				throw new XslTransformException("Cannot query the data source object referenced by URI '{0}', because the provided XmlResolver returned an object of type '{1}'. Only Stream, XmlReader, and IXPathNavigable data source objects are currently supported.", new string[]
				{
					uriRelative,
					dataSource.GetType().ToString()
				});
			}
			if (this.wsRules != null)
			{
				throw new XslTransformException("White space cannot be stripped from input documents that have already been loaded. Provide the input document as an XmlReader instead.", new string[] { string.Empty });
			}
			return (dataSource as IXPathNavigable).CreateNavigator();
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x00150330 File Offset: 0x0014E530
		public object GetParameter(string localName, string namespaceUri)
		{
			if (this.argList == null)
			{
				return null;
			}
			return this.argList.GetParam(localName, namespaceUri);
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x00150349 File Offset: 0x0014E549
		public object GetLateBoundObject(string namespaceUri)
		{
			if (this.argList == null)
			{
				return null;
			}
			return this.argList.GetExtensionObject(namespaceUri);
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x00150364 File Offset: 0x0014E564
		public bool LateBoundFunctionExists(string name, string namespaceUri)
		{
			if (this.argList == null)
			{
				return false;
			}
			object extensionObject = this.argList.GetExtensionObject(namespaceUri);
			return extensionObject != null && new XmlExtensionFunction(name, namespaceUri, -1, extensionObject.GetType(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public).CanBind();
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x001503A4 File Offset: 0x0014E5A4
		public IList<XPathItem> InvokeXsltLateBoundFunction(string name, string namespaceUri, IList<XPathItem>[] args)
		{
			object obj = ((this.argList != null) ? this.argList.GetExtensionObject(namespaceUri) : null);
			if (obj == null)
			{
				throw new XslTransformException("Cannot find a script or an extension object associated with namespace '{0}'.", new string[] { namespaceUri });
			}
			if (this.extFuncsLate == null)
			{
				this.extFuncsLate = new XmlExtensionFunctionTable();
			}
			XmlExtensionFunction xmlExtensionFunction = this.extFuncsLate.Bind(name, namespaceUri, args.Length, obj.GetType(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			object[] array = new object[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				XmlQueryType xmlArgumentType = xmlExtensionFunction.GetXmlArgumentType(i);
				XmlTypeCode typeCode = xmlArgumentType.TypeCode;
				if (typeCode != XmlTypeCode.Item)
				{
					if (typeCode != XmlTypeCode.Node)
					{
						switch (typeCode)
						{
						case XmlTypeCode.String:
							array[i] = XsltConvert.ToString(args[i]);
							break;
						case XmlTypeCode.Boolean:
							array[i] = XsltConvert.ToBoolean(args[i]);
							break;
						case XmlTypeCode.Double:
							array[i] = XsltConvert.ToDouble(args[i]);
							break;
						}
					}
					else if (xmlArgumentType.IsSingleton)
					{
						array[i] = XsltConvert.ToNode(args[i]);
					}
					else
					{
						array[i] = XsltConvert.ToNodeSet(args[i]);
					}
				}
				else
				{
					array[i] = args[i];
				}
				Type clrArgumentType = xmlExtensionFunction.GetClrArgumentType(i);
				if (xmlArgumentType.TypeCode == XmlTypeCode.Item || !clrArgumentType.IsAssignableFrom(array[i].GetType()))
				{
					array[i] = this.runtime.ChangeTypeXsltArgument(xmlArgumentType, array[i], clrArgumentType);
				}
			}
			object obj2 = xmlExtensionFunction.Invoke(obj, array);
			if (obj2 == null && xmlExtensionFunction.ClrReturnType == XsltConvert.VoidType)
			{
				return XmlQueryNodeSequence.Empty;
			}
			return (IList<XPathItem>)this.runtime.ChangeTypeXsltResult(XmlQueryTypeFactory.ItemS, obj2);
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x0015054C File Offset: 0x0014E74C
		public void OnXsltMessageEncountered(string message)
		{
			XsltMessageEncounteredEventHandler xsltMessageEncounteredEventHandler = ((this.argList != null) ? this.argList.xsltMessageEncountered : null);
			if (xsltMessageEncounteredEventHandler != null)
			{
				xsltMessageEncounteredEventHandler(this, new XmlILQueryEventArgs(message));
				return;
			}
			Console.WriteLine(message);
		}

		// Token: 0x0400277F RID: 10111
		private XmlQueryRuntime runtime;

		// Token: 0x04002780 RID: 10112
		private XPathNavigator defaultDataSource;

		// Token: 0x04002781 RID: 10113
		private XmlResolver dataSources;

		// Token: 0x04002782 RID: 10114
		private Hashtable dataSourceCache;

		// Token: 0x04002783 RID: 10115
		private XsltArgumentList argList;

		// Token: 0x04002784 RID: 10116
		private XmlExtensionFunctionTable extFuncsLate;

		// Token: 0x04002785 RID: 10117
		private WhitespaceRuleLookup wsRules;

		// Token: 0x04002786 RID: 10118
		private QueryReaderSettings readerSettings;
	}
}
