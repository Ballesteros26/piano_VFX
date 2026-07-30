using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Xml.Schema;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200060F RID: 1551
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlQueryRuntime
	{
		// Token: 0x06003C96 RID: 15510 RVA: 0x0015162C File Offset: 0x0014F82C
		internal XmlQueryRuntime(XmlQueryStaticData data, object defaultDataSource, XmlResolver dataSources, XsltArgumentList argList, XmlSequenceWriter seqWrt)
		{
			string[] names = data.Names;
			Int32Pair[] array = data.Filters;
			WhitespaceRuleLookup whitespaceRuleLookup = ((data.WhitespaceRules != null && data.WhitespaceRules.Count != 0) ? new WhitespaceRuleLookup(data.WhitespaceRules) : null);
			this.ctxt = new XmlQueryContext(this, defaultDataSource, dataSources, argList, whitespaceRuleLookup);
			this.xsltLib = null;
			this.earlyInfo = data.EarlyBound;
			this.earlyObjects = ((this.earlyInfo != null) ? new object[this.earlyInfo.Length] : null);
			this.globalNames = data.GlobalNames;
			this.globalValues = ((this.globalNames != null) ? new object[this.globalNames.Length] : null);
			this.nameTableQuery = this.ctxt.QueryNameTable;
			this.atomizedNames = null;
			if (names != null)
			{
				XmlNameTable defaultNameTable = this.ctxt.DefaultNameTable;
				this.atomizedNames = new string[names.Length];
				if (defaultNameTable != this.nameTableQuery && defaultNameTable != null)
				{
					for (int i = 0; i < names.Length; i++)
					{
						string text = defaultNameTable.Get(names[i]);
						this.atomizedNames[i] = this.nameTableQuery.Add(text ?? names[i]);
					}
				}
				else
				{
					for (int i = 0; i < names.Length; i++)
					{
						this.atomizedNames[i] = this.nameTableQuery.Add(names[i]);
					}
				}
			}
			this.filters = null;
			if (array != null)
			{
				this.filters = new XmlNavigatorFilter[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.filters[i] = XmlNavNameFilter.Create(this.atomizedNames[array[i].Left], this.atomizedNames[array[i].Right]);
				}
			}
			this.prefixMappingsList = data.PrefixMappingsList;
			this.types = data.Types;
			this.collations = data.Collations;
			this.docOrderCmp = new DocumentOrderComparer();
			this.indexes = null;
			this.stkOutput = new Stack<XmlQueryOutput>(16);
			this.output = new XmlQueryOutput(this, seqWrt);
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x0015182A File Offset: 0x0014FA2A
		public string[] DebugGetGlobalNames()
		{
			return this.globalNames;
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x00151834 File Offset: 0x0014FA34
		public IList DebugGetGlobalValue(string name)
		{
			for (int i = 0; i < this.globalNames.Length; i++)
			{
				if (this.globalNames[i] == name)
				{
					return (IList)this.globalValues[i];
				}
			}
			return null;
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x00151874 File Offset: 0x0014FA74
		public void DebugSetGlobalValue(string name, object value)
		{
			for (int i = 0; i < this.globalNames.Length; i++)
			{
				if (this.globalNames[i] == name)
				{
					this.globalValues[i] = (IList<XPathItem>)XmlAnyListConverter.ItemList.ChangeType(value, typeof(XPathItem[]), null);
					return;
				}
			}
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x001518C8 File Offset: 0x0014FAC8
		public object DebugGetXsltValue(IList seq)
		{
			if (seq != null && seq.Count == 1)
			{
				XPathItem xpathItem = seq[0] as XPathItem;
				if (xpathItem != null && !xpathItem.IsNode)
				{
					return xpathItem.TypedValue;
				}
				if (xpathItem is RtfNavigator)
				{
					return ((RtfNavigator)xpathItem).ToNavigator();
				}
			}
			return seq;
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x00151915 File Offset: 0x0014FB15
		public XmlQueryContext ExternalContext
		{
			get
			{
				return this.ctxt;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003C9C RID: 15516 RVA: 0x0015191D File Offset: 0x0014FB1D
		public XsltLibrary XsltFunctions
		{
			get
			{
				if (this.xsltLib == null)
				{
					this.xsltLib = new XsltLibrary(this);
				}
				return this.xsltLib;
			}
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x0015193C File Offset: 0x0014FB3C
		public object GetEarlyBoundObject(int index)
		{
			object obj = this.earlyObjects[index];
			if (obj == null)
			{
				obj = this.earlyInfo[index].CreateObject();
				this.earlyObjects[index] = obj;
			}
			return obj;
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x00151970 File Offset: 0x0014FB70
		public bool EarlyBoundFunctionExists(string name, string namespaceUri)
		{
			if (this.earlyInfo == null)
			{
				return false;
			}
			for (int i = 0; i < this.earlyInfo.Length; i++)
			{
				if (namespaceUri == this.earlyInfo[i].NamespaceUri)
				{
					return new XmlExtensionFunction(name, namespaceUri, -1, this.earlyInfo[i].EarlyBoundType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public).CanBind();
				}
			}
			return false;
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x001519CD File Offset: 0x0014FBCD
		public bool IsGlobalComputed(int index)
		{
			return this.globalValues[index] != null;
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x001519DA File Offset: 0x0014FBDA
		public object GetGlobalValue(int index)
		{
			return this.globalValues[index];
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x001519E4 File Offset: 0x0014FBE4
		public void SetGlobalValue(int index, object value)
		{
			this.globalValues[index] = value;
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x001519EF File Offset: 0x0014FBEF
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTableQuery;
			}
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x001519F7 File Offset: 0x0014FBF7
		public string GetAtomizedName(int index)
		{
			return this.atomizedNames[index];
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x00151A01 File Offset: 0x0014FC01
		public XmlNavigatorFilter GetNameFilter(int index)
		{
			return this.filters[index];
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x00151A0B File Offset: 0x0014FC0B
		public XmlNavigatorFilter GetTypeFilter(XPathNodeType nodeType)
		{
			if (nodeType == XPathNodeType.All)
			{
				return XmlNavNeverFilter.Create();
			}
			if (nodeType == XPathNodeType.Attribute)
			{
				return XmlNavAttrFilter.Create();
			}
			return XmlNavTypeFilter.Create(nodeType);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x00151A28 File Offset: 0x0014FC28
		public XmlQualifiedName ParseTagName(string tagName, int indexPrefixMappings)
		{
			string text;
			string text2;
			string text3;
			this.ParseTagName(tagName, indexPrefixMappings, out text, out text2, out text3);
			return new XmlQualifiedName(text2, text3);
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x00151A4C File Offset: 0x0014FC4C
		public XmlQualifiedName ParseTagName(string tagName, string ns)
		{
			string text;
			string text2;
			ValidateNames.ParseQNameThrow(tagName, out text, out text2);
			return new XmlQualifiedName(text2, ns);
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x00151A6C File Offset: 0x0014FC6C
		internal void ParseTagName(string tagName, int idxPrefixMappings, out string prefix, out string localName, out string ns)
		{
			ValidateNames.ParseQNameThrow(tagName, out prefix, out localName);
			ns = null;
			foreach (StringPair stringPair in this.prefixMappingsList[idxPrefixMappings])
			{
				if (prefix == stringPair.Left)
				{
					ns = stringPair.Right;
					break;
				}
			}
			if (ns != null)
			{
				return;
			}
			if (prefix.Length == 0)
			{
				ns = "";
				return;
			}
			if (prefix.Equals("xml"))
			{
				ns = "http://www.w3.org/XML/1998/namespace";
				return;
			}
			if (prefix.Equals("xmlns"))
			{
				ns = "http://www.w3.org/2000/xmlns/";
				return;
			}
			throw new XslTransformException("Prefix '{0}' is not defined.", new string[] { prefix });
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x00151B1C File Offset: 0x0014FD1C
		public bool IsQNameEqual(XPathNavigator n1, XPathNavigator n2)
		{
			if (n1.NameTable == n2.NameTable)
			{
				return n1.LocalName == n2.LocalName && n1.NamespaceURI == n2.NamespaceURI;
			}
			return n1.LocalName == n2.LocalName && n1.NamespaceURI == n2.NamespaceURI;
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x00151B7C File Offset: 0x0014FD7C
		public bool IsQNameEqual(XPathNavigator navigator, int indexLocalName, int indexNamespaceUri)
		{
			if (navigator.NameTable == this.nameTableQuery)
			{
				return this.GetAtomizedName(indexLocalName) == navigator.LocalName && this.GetAtomizedName(indexNamespaceUri) == navigator.NamespaceURI;
			}
			return this.GetAtomizedName(indexLocalName) == navigator.LocalName && this.GetAtomizedName(indexNamespaceUri) == navigator.NamespaceURI;
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x00151BE0 File Offset: 0x0014FDE0
		internal XmlQueryType[] XmlTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x00151BE8 File Offset: 0x0014FDE8
		internal XmlQueryType GetXmlType(int idxType)
		{
			return this.types[idxType];
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x00151BF2 File Offset: 0x0014FDF2
		public object ChangeTypeXsltArgument(int indexType, object value, Type destinationType)
		{
			return this.ChangeTypeXsltArgument(this.GetXmlType(indexType), value, destinationType);
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x00151C04 File Offset: 0x0014FE04
		internal object ChangeTypeXsltArgument(XmlQueryType xmlType, object value, Type destinationType)
		{
			XmlTypeCode typeCode = xmlType.TypeCode;
			if (typeCode <= XmlTypeCode.Node)
			{
				if (typeCode != XmlTypeCode.Item)
				{
					if (typeCode == XmlTypeCode.Node)
					{
						if (destinationType == XsltConvert.XPathNodeIteratorType)
						{
							value = new XPathArrayIterator((IList)value);
						}
						else if (destinationType == XsltConvert.XPathNavigatorArrayType)
						{
							IList<XPathNavigator> list = (IList<XPathNavigator>)value;
							XPathNavigator[] array = new XPathNavigator[list.Count];
							for (int i = 0; i < list.Count; i++)
							{
								array[i] = list[i];
							}
							value = array;
						}
					}
				}
				else
				{
					if (destinationType != XsltConvert.ObjectType)
					{
						throw new XslTransformException("Extension function parameters or return values which have Clr type '{0}' are not supported.", new string[] { destinationType.Name });
					}
					IList<XPathItem> list2 = (IList<XPathItem>)value;
					if (list2.Count == 1)
					{
						XPathItem xpathItem = list2[0];
						if (xpathItem.IsNode)
						{
							RtfNavigator rtfNavigator = xpathItem as RtfNavigator;
							if (rtfNavigator != null)
							{
								value = rtfNavigator.ToNavigator();
							}
							else
							{
								value = new XPathArrayIterator((IList)value);
							}
						}
						else
						{
							value = xpathItem.TypedValue;
						}
					}
					else
					{
						value = new XPathArrayIterator((IList)value);
					}
				}
			}
			else if (typeCode != XmlTypeCode.String)
			{
				if (typeCode == XmlTypeCode.Double)
				{
					if (destinationType != XsltConvert.DoubleType)
					{
						value = Convert.ChangeType(value, destinationType, CultureInfo.InvariantCulture);
					}
				}
			}
			else if (destinationType == XsltConvert.DateTimeType)
			{
				value = XsltConvert.ToDateTime((string)value);
			}
			return value;
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x00151D76 File Offset: 0x0014FF76
		public object ChangeTypeXsltResult(int indexType, object value)
		{
			return this.ChangeTypeXsltResult(this.GetXmlType(indexType), value);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x00151D88 File Offset: 0x0014FF88
		internal object ChangeTypeXsltResult(XmlQueryType xmlType, object value)
		{
			if (value == null)
			{
				throw new XslTransformException("Extension functions cannot return null values.", new string[] { string.Empty });
			}
			XmlTypeCode xmlTypeCode = xmlType.TypeCode;
			if (xmlTypeCode <= XmlTypeCode.Node)
			{
				if (xmlTypeCode != XmlTypeCode.Item)
				{
					if (xmlTypeCode == XmlTypeCode.Node)
					{
						if (!xmlType.IsSingleton)
						{
							XPathArrayIterator xpathArrayIterator = value as XPathArrayIterator;
							if (xpathArrayIterator != null && xpathArrayIterator.AsList is XmlQueryNodeSequence)
							{
								value = xpathArrayIterator.AsList as XmlQueryNodeSequence;
							}
							else
							{
								XmlQueryNodeSequence xmlQueryNodeSequence = new XmlQueryNodeSequence();
								IList list = value as IList;
								if (list != null)
								{
									for (int i = 0; i < list.Count; i++)
									{
										xmlQueryNodeSequence.Add(XmlQueryRuntime.EnsureNavigator(list[i]));
									}
								}
								else
								{
									foreach (object obj in ((IEnumerable)value))
									{
										xmlQueryNodeSequence.Add(XmlQueryRuntime.EnsureNavigator(obj));
									}
								}
								value = xmlQueryNodeSequence;
							}
							value = ((XmlQueryNodeSequence)value).DocOrderDistinct(this.docOrderCmp);
						}
					}
				}
				else
				{
					Type type = value.GetType();
					xmlTypeCode = XsltConvert.InferXsltType(type).TypeCode;
					if (xmlTypeCode != XmlTypeCode.Item)
					{
						if (xmlTypeCode != XmlTypeCode.Node)
						{
							switch (xmlTypeCode)
							{
							case XmlTypeCode.String:
								if (type == XsltConvert.DateTimeType)
								{
									value = new XmlQueryItemSequence(new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), XsltConvert.ToString((DateTime)value)));
								}
								else
								{
									value = new XmlQueryItemSequence(new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), value));
								}
								break;
							case XmlTypeCode.Boolean:
								value = new XmlQueryItemSequence(new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean), value));
								break;
							case XmlTypeCode.Double:
								value = new XmlQueryItemSequence(new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Double), ((IConvertible)value).ToDouble(null)));
								break;
							}
						}
						else
						{
							value = this.ChangeTypeXsltResult(XmlQueryTypeFactory.NodeS, value);
						}
					}
					else if (value is XPathNodeIterator)
					{
						value = this.ChangeTypeXsltResult(XmlQueryTypeFactory.NodeS, value);
					}
					else
					{
						IXPathNavigable ixpathNavigable = value as IXPathNavigable;
						if (ixpathNavigable == null)
						{
							throw new XslTransformException("Extension function parameters or return values which have Clr type '{0}' are not supported.", new string[] { type.Name });
						}
						if (value is XPathNavigator)
						{
							value = new XmlQueryNodeSequence((XPathNavigator)value);
						}
						else
						{
							value = new XmlQueryNodeSequence(ixpathNavigable.CreateNavigator());
						}
					}
				}
			}
			else if (xmlTypeCode != XmlTypeCode.String)
			{
				if (xmlTypeCode == XmlTypeCode.Double)
				{
					if (value.GetType() != XsltConvert.DoubleType)
					{
						value = ((IConvertible)value).ToDouble(null);
					}
				}
			}
			else if (value.GetType() == XsltConvert.DateTimeType)
			{
				value = XsltConvert.ToString((DateTime)value);
			}
			return value;
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x00152050 File Offset: 0x00150250
		private static XPathNavigator EnsureNavigator(object value)
		{
			XPathNavigator xpathNavigator = value as XPathNavigator;
			if (xpathNavigator == null)
			{
				throw new XslTransformException("Extension functions cannot return null values.", new string[] { string.Empty });
			}
			return xpathNavigator;
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x00152084 File Offset: 0x00150284
		public bool MatchesXmlType(IList<XPathItem> seq, int indexType)
		{
			XmlQueryType xmlQueryType = this.GetXmlType(indexType);
			int count = seq.Count;
			XmlQueryCardinality xmlQueryCardinality;
			if (count != 0)
			{
				if (count != 1)
				{
					xmlQueryCardinality = XmlQueryCardinality.More;
				}
				else
				{
					xmlQueryCardinality = XmlQueryCardinality.One;
				}
			}
			else
			{
				xmlQueryCardinality = XmlQueryCardinality.Zero;
			}
			if (!(xmlQueryCardinality <= xmlQueryType.Cardinality))
			{
				return false;
			}
			xmlQueryType = xmlQueryType.Prime;
			for (int i = 0; i < seq.Count; i++)
			{
				if (!this.CreateXmlType(seq[0]).IsSubtypeOf(xmlQueryType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x001520FF File Offset: 0x001502FF
		public bool MatchesXmlType(XPathItem item, int indexType)
		{
			return this.CreateXmlType(item).IsSubtypeOf(this.GetXmlType(indexType));
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x00152114 File Offset: 0x00150314
		public bool MatchesXmlType(IList<XPathItem> seq, XmlTypeCode code)
		{
			return seq.Count == 1 && this.MatchesXmlType(seq[0], code);
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x00152130 File Offset: 0x00150330
		public bool MatchesXmlType(XPathItem item, XmlTypeCode code)
		{
			if (code > XmlTypeCode.AnyAtomicType)
			{
				return !item.IsNode && item.XmlType.TypeCode == code;
			}
			if (code == XmlTypeCode.Item)
			{
				return true;
			}
			if (code == XmlTypeCode.Node)
			{
				return item.IsNode;
			}
			if (code == XmlTypeCode.AnyAtomicType)
			{
				return !item.IsNode;
			}
			if (!item.IsNode)
			{
				return false;
			}
			switch (((XPathNavigator)item).NodeType)
			{
			case XPathNodeType.Root:
				return code == XmlTypeCode.Document;
			case XPathNodeType.Element:
				return code == XmlTypeCode.Element;
			case XPathNodeType.Attribute:
				return code == XmlTypeCode.Attribute;
			case XPathNodeType.Namespace:
				return code == XmlTypeCode.Namespace;
			case XPathNodeType.Text:
				return code == XmlTypeCode.Text;
			case XPathNodeType.SignificantWhitespace:
				return code == XmlTypeCode.Text;
			case XPathNodeType.Whitespace:
				return code == XmlTypeCode.Text;
			case XPathNodeType.ProcessingInstruction:
				return code == XmlTypeCode.ProcessingInstruction;
			case XPathNodeType.Comment:
				return code == XmlTypeCode.Comment;
			default:
				return false;
			}
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x001521F0 File Offset: 0x001503F0
		private XmlQueryType CreateXmlType(XPathItem item)
		{
			if (!item.IsNode)
			{
				return XmlQueryTypeFactory.Type((XmlSchemaSimpleType)item.XmlType, true);
			}
			if (item is RtfNavigator)
			{
				return XmlQueryTypeFactory.Node;
			}
			XPathNavigator xpathNavigator = (XPathNavigator)item;
			XPathNodeType nodeType = xpathNavigator.NodeType;
			if (nodeType > XPathNodeType.Element)
			{
				if (nodeType != XPathNodeType.Attribute)
				{
					return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.AnyType, false);
				}
				if (xpathNavigator.XmlType == null)
				{
					return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.New(xpathNavigator.LocalName, xpathNavigator.NamespaceURI), DatatypeImplementation.UntypedAtomicType, false);
				}
				return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.New(xpathNavigator.LocalName, xpathNavigator.NamespaceURI), xpathNavigator.XmlType, false);
			}
			else
			{
				if (xpathNavigator.XmlType == null)
				{
					return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.New(xpathNavigator.LocalName, xpathNavigator.NamespaceURI), XmlSchemaComplexType.UntypedAnyType, false);
				}
				return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.New(xpathNavigator.LocalName, xpathNavigator.NamespaceURI), xpathNavigator.XmlType, xpathNavigator.SchemaInfo.SchemaElement.IsNillable);
			}
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x00152306 File Offset: 0x00150506
		public XmlCollation GetCollation(int index)
		{
			return this.collations[index];
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x00152310 File Offset: 0x00150510
		public XmlCollation CreateCollation(string collation)
		{
			return XmlCollation.Create(collation);
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x00152318 File Offset: 0x00150518
		public int ComparePosition(XPathNavigator navigatorThis, XPathNavigator navigatorThat)
		{
			return this.docOrderCmp.Compare(navigatorThis, navigatorThat);
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x00152328 File Offset: 0x00150528
		public IList<XPathNavigator> DocOrderDistinct(IList<XPathNavigator> seq)
		{
			if (seq.Count <= 1)
			{
				return seq;
			}
			XmlQueryNodeSequence xmlQueryNodeSequence = (XmlQueryNodeSequence)seq;
			if (xmlQueryNodeSequence == null)
			{
				xmlQueryNodeSequence = new XmlQueryNodeSequence(seq);
			}
			return xmlQueryNodeSequence.DocOrderDistinct(this.docOrderCmp);
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x00152360 File Offset: 0x00150560
		public string GenerateId(XPathNavigator navigator)
		{
			return "ID" + this.docOrderCmp.GetDocumentIndex(navigator).ToString(CultureInfo.InvariantCulture) + navigator.UniqueId;
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x00152398 File Offset: 0x00150598
		public bool FindIndex(XPathNavigator context, int indexId, out XmlILIndex index)
		{
			XPathNavigator xpathNavigator = context.Clone();
			xpathNavigator.MoveToRoot();
			if (this.indexes != null && indexId < this.indexes.Length)
			{
				ArrayList arrayList = this.indexes[indexId];
				if (arrayList != null)
				{
					for (int i = 0; i < arrayList.Count; i += 2)
					{
						if (((XPathNavigator)arrayList[i]).IsSamePosition(xpathNavigator))
						{
							index = (XmlILIndex)arrayList[i + 1];
							return true;
						}
					}
				}
			}
			index = new XmlILIndex();
			return false;
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x00152410 File Offset: 0x00150610
		public void AddNewIndex(XPathNavigator context, int indexId, XmlILIndex index)
		{
			XPathNavigator xpathNavigator = context.Clone();
			xpathNavigator.MoveToRoot();
			if (this.indexes == null)
			{
				this.indexes = new ArrayList[indexId + 4];
			}
			else if (indexId >= this.indexes.Length)
			{
				ArrayList[] array = new ArrayList[indexId + 4];
				Array.Copy(this.indexes, 0, array, 0, this.indexes.Length);
				this.indexes = array;
			}
			ArrayList arrayList = this.indexes[indexId];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				this.indexes[indexId] = arrayList;
			}
			arrayList.Add(xpathNavigator);
			arrayList.Add(index);
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x0015249E File Offset: 0x0015069E
		public XmlQueryOutput Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x001524A8 File Offset: 0x001506A8
		public void StartSequenceConstruction(out XmlQueryOutput output)
		{
			this.stkOutput.Push(this.output);
			output = (this.output = new XmlQueryOutput(this, new XmlCachedSequenceWriter()));
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x001524DC File Offset: 0x001506DC
		public IList<XPathItem> EndSequenceConstruction(out XmlQueryOutput output)
		{
			IList<XPathItem> resultSequence = ((XmlCachedSequenceWriter)this.output.SequenceWriter).ResultSequence;
			output = (this.output = this.stkOutput.Pop());
			return resultSequence;
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x00152514 File Offset: 0x00150714
		public void StartRtfConstruction(string baseUri, out XmlQueryOutput output)
		{
			this.stkOutput.Push(this.output);
			output = (this.output = new XmlQueryOutput(this, new XmlEventCache(baseUri, true)));
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x0015254C File Offset: 0x0015074C
		public XPathNavigator EndRtfConstruction(out XmlQueryOutput output)
		{
			XmlEventCache xmlEventCache = (XmlEventCache)this.output.Writer;
			output = (this.output = this.stkOutput.Pop());
			xmlEventCache.EndEvents();
			return new RtfTreeNavigator(xmlEventCache, this.nameTableQuery);
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x00152590 File Offset: 0x00150790
		public XPathNavigator TextRtfConstruction(string text, string baseUri)
		{
			return new RtfTextNavigator(text, baseUri);
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x00152599 File Offset: 0x00150799
		public void SendMessage(string message)
		{
			this.ctxt.OnXsltMessageEncountered(message);
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x001525A7 File Offset: 0x001507A7
		public void ThrowException(string text)
		{
			throw new XslTransformException(text);
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x001525AF File Offset: 0x001507AF
		internal static XPathNavigator SyncToNavigator(XPathNavigator navigatorThis, XPathNavigator navigatorThat)
		{
			if (navigatorThis == null || !navigatorThis.MoveTo(navigatorThat))
			{
				return navigatorThat.Clone();
			}
			return navigatorThis;
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x001525C8 File Offset: 0x001507C8
		public static int OnCurrentNodeChanged(XPathNavigator currentNode)
		{
			IXmlLineInfo xmlLineInfo = currentNode as IXmlLineInfo;
			if (xmlLineInfo != null && (currentNode.NodeType != XPathNodeType.Namespace || !XmlQueryRuntime.IsInheritedNamespace(currentNode)))
			{
				XmlQueryRuntime.OnCurrentNodeChanged2(currentNode.BaseURI, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
			}
			return 0;
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x00152608 File Offset: 0x00150808
		private static bool IsInheritedNamespace(XPathNavigator node)
		{
			XPathNavigator xpathNavigator = node.Clone();
			if (xpathNavigator.MoveToParent() && xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
			{
				while (xpathNavigator.LocalName != node.LocalName)
				{
					if (!xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.Local))
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x00002F50 File Offset: 0x00001150
		private static void OnCurrentNodeChanged2(string baseUri, int lineNumber, int linePosition)
		{
		}

		// Token: 0x0400279F RID: 10143
		private XmlQueryContext ctxt;

		// Token: 0x040027A0 RID: 10144
		private XsltLibrary xsltLib;

		// Token: 0x040027A1 RID: 10145
		private EarlyBoundInfo[] earlyInfo;

		// Token: 0x040027A2 RID: 10146
		private object[] earlyObjects;

		// Token: 0x040027A3 RID: 10147
		private string[] globalNames;

		// Token: 0x040027A4 RID: 10148
		private object[] globalValues;

		// Token: 0x040027A5 RID: 10149
		private XmlNameTable nameTableQuery;

		// Token: 0x040027A6 RID: 10150
		private string[] atomizedNames;

		// Token: 0x040027A7 RID: 10151
		private XmlNavigatorFilter[] filters;

		// Token: 0x040027A8 RID: 10152
		private StringPair[][] prefixMappingsList;

		// Token: 0x040027A9 RID: 10153
		private XmlQueryType[] types;

		// Token: 0x040027AA RID: 10154
		private XmlCollation[] collations;

		// Token: 0x040027AB RID: 10155
		private DocumentOrderComparer docOrderCmp;

		// Token: 0x040027AC RID: 10156
		private ArrayList[] indexes;

		// Token: 0x040027AD RID: 10157
		private XmlQueryOutput output;

		// Token: 0x040027AE RID: 10158
		private Stack<XmlQueryOutput> stkOutput;

		// Token: 0x040027AF RID: 10159
		internal const BindingFlags EarlyBoundFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x040027B0 RID: 10160
		internal const BindingFlags LateBoundFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}
