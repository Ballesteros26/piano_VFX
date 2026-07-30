using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x020002BD RID: 701
	internal class XPathNavigatorReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x060019FA RID: 6650 RVA: 0x00092A86 File Offset: 0x00090C86
		internal static XmlNodeType ToXmlNodeType(XPathNodeType typ)
		{
			return XPathNavigatorReader.convertFromXPathNodeType[(int)typ];
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00092A8F File Offset: 0x00090C8F
		internal object UnderlyingObject
		{
			get
			{
				return this.nav.UnderlyingObject;
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00092A9C File Offset: 0x00090C9C
		public static XPathNavigatorReader Create(XPathNavigator navToRead)
		{
			XPathNavigator xpathNavigator = navToRead.Clone();
			IXmlLineInfo xmlLineInfo = xpathNavigator as IXmlLineInfo;
			IXmlSchemaInfo xmlSchemaInfo = xpathNavigator as IXmlSchemaInfo;
			if (xmlSchemaInfo == null)
			{
				return new XPathNavigatorReader(xpathNavigator, xmlLineInfo, xmlSchemaInfo);
			}
			return new XPathNavigatorReaderWithSI(xpathNavigator, xmlLineInfo, xmlSchemaInfo);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00092AD4 File Offset: 0x00090CD4
		protected XPathNavigatorReader(XPathNavigator navToRead, IXmlLineInfo xli, IXmlSchemaInfo xsi)
		{
			this.navToRead = navToRead;
			this.lineInfo = xli;
			this.schemaInfo = xsi;
			this.nav = XmlEmptyNavigator.Singleton;
			this.state = XPathNavigatorReader.State.Initial;
			this.depth = 0;
			this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00092B2B File Offset: 0x00090D2B
		protected bool IsReading
		{
			get
			{
				return this.state > XPathNavigatorReader.State.Initial && this.state < XPathNavigatorReader.State.EOF;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00092B41 File Offset: 0x00090D41
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return XPathNavigator.GetNamespaces(this);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x00092B49 File Offset: 0x00090D49
		public override XmlNameTable NameTable
		{
			get
			{
				return this.navToRead.NameTable;
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00092B56 File Offset: 0x00090D56
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.nav.GetNamespacesInScope(scope);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00092B64 File Offset: 0x00090D64
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.nav.LookupNamespace(prefix);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00092B72 File Offset: 0x00090D72
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.nav.LookupPrefix(namespaceName);
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00092B80 File Offset: 0x00090D80
		public override XmlReaderSettings Settings
		{
			get
			{
				return new XmlReaderSettings
				{
					NameTable = this.NameTable,
					ConformanceLevel = ConformanceLevel.Fragment,
					CheckCharacters = false,
					ReadOnly = true
				};
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x00092BA8 File Offset: 0x00090DA8
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (this.nodeType == XmlNodeType.Text)
				{
					return null;
				}
				return this.nav.SchemaInfo;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x00092BC0 File Offset: 0x00090DC0
		public override Type ValueType
		{
			get
			{
				return this.nav.ValueType;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00092BCD File Offset: 0x00090DCD
		public override XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00092BD5 File Offset: 0x00090DD5
		public override string NamespaceURI
		{
			get
			{
				if (this.nav.NodeType == XPathNodeType.Namespace)
				{
					return this.NameTable.Add("http://www.w3.org/2000/xmlns/");
				}
				if (this.NodeType == XmlNodeType.Text)
				{
					return string.Empty;
				}
				return this.nav.NamespaceURI;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00092C10 File Offset: 0x00090E10
		public override string LocalName
		{
			get
			{
				if (this.nav.NodeType == XPathNodeType.Namespace && this.nav.LocalName.Length == 0)
				{
					return this.NameTable.Add("xmlns");
				}
				if (this.NodeType == XmlNodeType.Text)
				{
					return string.Empty;
				}
				return this.nav.LocalName;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x00092C68 File Offset: 0x00090E68
		public override string Prefix
		{
			get
			{
				if (this.nav.NodeType == XPathNodeType.Namespace && this.nav.LocalName.Length != 0)
				{
					return this.NameTable.Add("xmlns");
				}
				if (this.NodeType == XmlNodeType.Text)
				{
					return string.Empty;
				}
				return this.nav.Prefix;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x00092CC0 File Offset: 0x00090EC0
		public override string BaseURI
		{
			get
			{
				if (this.state == XPathNavigatorReader.State.Initial)
				{
					return this.navToRead.BaseURI;
				}
				return this.nav.BaseURI;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x00092CE1 File Offset: 0x00090EE1
		public override bool IsEmptyElement
		{
			get
			{
				return this.nav.IsEmptyElement;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x00092CF0 File Offset: 0x00090EF0
		public override XmlSpace XmlSpace
		{
			get
			{
				XPathNavigator xpathNavigator = this.nav.Clone();
				for (;;)
				{
					if (xpathNavigator.MoveToAttribute("space", "http://www.w3.org/XML/1998/namespace"))
					{
						string text = XmlConvert.TrimString(xpathNavigator.Value);
						if (text == "default")
						{
							break;
						}
						if (text == "preserve")
						{
							return XmlSpace.Preserve;
						}
						xpathNavigator.MoveToParent();
					}
					if (!xpathNavigator.MoveToParent())
					{
						return XmlSpace.None;
					}
				}
				return XmlSpace.Default;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x00092D57 File Offset: 0x00090F57
		public override string XmlLang
		{
			get
			{
				return this.nav.XmlLang;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x00092D64 File Offset: 0x00090F64
		public override bool HasValue
		{
			get
			{
				return this.nodeType != XmlNodeType.Element && this.nodeType != XmlNodeType.Document && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.None;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00092D8E File Offset: 0x00090F8E
		public override string Value
		{
			get
			{
				if (this.nodeType != XmlNodeType.Element && this.nodeType != XmlNodeType.Document && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.None)
				{
					return this.nav.Value;
				}
				return string.Empty;
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00092DC8 File Offset: 0x00090FC8
		private XPathNavigator GetElemNav()
		{
			switch (this.state)
			{
			case XPathNavigatorReader.State.Content:
				return this.nav.Clone();
			case XPathNavigatorReader.State.Attribute:
			case XPathNavigatorReader.State.AttrVal:
			{
				XPathNavigator xpathNavigator = this.nav.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator;
				}
				break;
			}
			case XPathNavigatorReader.State.InReadBinary:
			{
				this.state = this.savedState;
				XPathNavigator elemNav = this.GetElemNav();
				this.state = XPathNavigatorReader.State.InReadBinary;
				return elemNav;
			}
			}
			return null;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00092E38 File Offset: 0x00091038
		private XPathNavigator GetElemNav(out int depth)
		{
			XPathNavigator xpathNavigator = null;
			switch (this.state)
			{
			case XPathNavigatorReader.State.Content:
				if (this.nodeType == XmlNodeType.Element)
				{
					xpathNavigator = this.nav.Clone();
				}
				depth = this.depth;
				return xpathNavigator;
			case XPathNavigatorReader.State.Attribute:
				xpathNavigator = this.nav.Clone();
				xpathNavigator.MoveToParent();
				depth = this.depth - 1;
				return xpathNavigator;
			case XPathNavigatorReader.State.AttrVal:
				xpathNavigator = this.nav.Clone();
				xpathNavigator.MoveToParent();
				depth = this.depth - 2;
				return xpathNavigator;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				xpathNavigator = this.GetElemNav(out depth);
				this.state = XPathNavigatorReader.State.InReadBinary;
				return xpathNavigator;
			}
			depth = this.depth;
			return xpathNavigator;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00092EEF File Offset: 0x000910EF
		private void MoveToAttr(XPathNavigator nav, int depth)
		{
			this.nav.MoveTo(nav);
			this.depth = depth;
			this.nodeType = XmlNodeType.Attribute;
			this.state = XPathNavigatorReader.State.Attribute;
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x00092F14 File Offset: 0x00091114
		public override int AttributeCount
		{
			get
			{
				if (this.attrCount < 0)
				{
					XPathNavigator elemNav = this.GetElemNav();
					int num = 0;
					if (elemNav != null)
					{
						if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
						{
							do
							{
								num++;
							}
							while (elemNav.MoveToNextNamespace(XPathNamespaceScope.Local));
							elemNav.MoveToParent();
						}
						if (elemNav.MoveToFirstAttribute())
						{
							do
							{
								num++;
							}
							while (elemNav.MoveToNextAttribute());
						}
					}
					this.attrCount = num;
				}
				return this.attrCount;
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00092F74 File Offset: 0x00091174
		public override string GetAttribute(string name)
		{
			XPathNavigator xpathNavigator = this.nav;
			XPathNodeType xpathNodeType = xpathNavigator.NodeType;
			if (xpathNodeType != XPathNodeType.Element)
			{
				if (xpathNodeType != XPathNodeType.Attribute)
				{
					return null;
				}
				xpathNavigator = xpathNavigator.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return null;
				}
			}
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			if (text.Length == 0)
			{
				if (text2 == "xmlns")
				{
					return xpathNavigator.GetNamespace(string.Empty);
				}
				if (xpathNavigator == this.nav)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
				if (xpathNavigator.MoveToAttribute(text2, string.Empty))
				{
					return xpathNavigator.Value;
				}
			}
			else
			{
				if (text == "xmlns")
				{
					return xpathNavigator.GetNamespace(text2);
				}
				if (xpathNavigator == this.nav)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
				if (xpathNavigator.MoveToFirstAttribute())
				{
					while (!(xpathNavigator.LocalName == text2) || !(xpathNavigator.Prefix == text))
					{
						if (!xpathNavigator.MoveToNextAttribute())
						{
							goto IL_00D1;
						}
					}
					return xpathNavigator.Value;
				}
			}
			IL_00D1:
			return null;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00093054 File Offset: 0x00091254
		public override string GetAttribute(string localName, string namespaceURI)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			XPathNavigator xpathNavigator = this.nav;
			XPathNodeType xpathNodeType = xpathNavigator.NodeType;
			if (xpathNodeType != XPathNodeType.Element)
			{
				if (xpathNodeType != XPathNodeType.Attribute)
				{
					return null;
				}
				xpathNavigator = xpathNavigator.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return null;
				}
			}
			if (namespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				if (localName == "xmlns")
				{
					localName = string.Empty;
				}
				return xpathNavigator.GetNamespace(localName);
			}
			if (namespaceURI == null)
			{
				namespaceURI = string.Empty;
			}
			if (xpathNavigator == this.nav)
			{
				xpathNavigator = xpathNavigator.Clone();
			}
			if (xpathNavigator.MoveToAttribute(localName, namespaceURI))
			{
				return xpathNavigator.Value;
			}
			return null;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000930F0 File Offset: 0x000912F0
		private static string GetNamespaceByIndex(XPathNavigator nav, int index, out int count)
		{
			string value = nav.Value;
			string text = null;
			if (nav.MoveToNextNamespace(XPathNamespaceScope.Local))
			{
				text = XPathNavigatorReader.GetNamespaceByIndex(nav, index, out count);
			}
			else
			{
				count = 0;
			}
			if (count == index)
			{
				text = value;
			}
			count++;
			return text;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0009312C File Offset: 0x0009132C
		public override string GetAttribute(int index)
		{
			if (index >= 0)
			{
				XPathNavigator elemNav = this.GetElemNav();
				if (elemNav != null)
				{
					if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						int num;
						string namespaceByIndex = XPathNavigatorReader.GetNamespaceByIndex(elemNav, index, out num);
						if (namespaceByIndex != null)
						{
							return namespaceByIndex;
						}
						index -= num;
						elemNav.MoveToParent();
					}
					if (elemNav.MoveToFirstAttribute())
					{
						while (index != 0)
						{
							index--;
							if (!elemNav.MoveToNextAttribute())
							{
								goto IL_0051;
							}
						}
						return elemNav.Value;
					}
				}
			}
			IL_0051:
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00093194 File Offset: 0x00091394
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			int num = this.depth;
			XPathNavigator elemNav = this.GetElemNav(out num);
			if (elemNav != null)
			{
				if (namespaceName == "http://www.w3.org/2000/xmlns/")
				{
					if (localName == "xmlns")
					{
						localName = string.Empty;
					}
					if (!elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						return false;
					}
					while (!(elemNav.LocalName == localName))
					{
						if (!elemNav.MoveToNextNamespace(XPathNamespaceScope.Local))
						{
							return false;
						}
					}
				}
				else
				{
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
					if (!elemNav.MoveToAttribute(localName, namespaceName))
					{
						return false;
					}
				}
				if (this.state == XPathNavigatorReader.State.InReadBinary)
				{
					this.readBinaryHelper.Finish();
					this.state = this.savedState;
				}
				this.MoveToAttr(elemNav, num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00093248 File Offset: 0x00091448
		public override bool MoveToFirstAttribute()
		{
			int num;
			XPathNavigator elemNav = this.GetElemNav(out num);
			if (elemNav != null)
			{
				if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					while (elemNav.MoveToNextNamespace(XPathNamespaceScope.Local))
					{
					}
				}
				else if (!elemNav.MoveToFirstAttribute())
				{
					return false;
				}
				if (this.state == XPathNavigatorReader.State.InReadBinary)
				{
					this.readBinaryHelper.Finish();
					this.state = this.savedState;
				}
				this.MoveToAttr(elemNav, num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000932AC File Offset: 0x000914AC
		public override bool MoveToNextAttribute()
		{
			switch (this.state)
			{
			case XPathNavigatorReader.State.Content:
				return this.MoveToFirstAttribute();
			case XPathNavigatorReader.State.Attribute:
			{
				if (XPathNodeType.Attribute == this.nav.NodeType)
				{
					return this.nav.MoveToNextAttribute();
				}
				XPathNavigator xpathNavigator = this.nav.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return false;
				}
				if (!xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					return false;
				}
				if (!xpathNavigator.IsSamePosition(this.nav))
				{
					XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
					while (xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.Local))
					{
						if (xpathNavigator.IsSamePosition(this.nav))
						{
							this.nav.MoveTo(xpathNavigator2);
							return true;
						}
						xpathNavigator2.MoveTo(xpathNavigator);
					}
					return false;
				}
				xpathNavigator.MoveToParent();
				if (!xpathNavigator.MoveToFirstAttribute())
				{
					return false;
				}
				this.nav.MoveTo(xpathNavigator);
				return true;
			}
			case XPathNavigatorReader.State.AttrVal:
				this.depth--;
				this.state = XPathNavigatorReader.State.Attribute;
				if (!this.MoveToNextAttribute())
				{
					this.depth++;
					this.state = XPathNavigatorReader.State.AttrVal;
					return false;
				}
				this.nodeType = XmlNodeType.Attribute;
				return true;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				if (!this.MoveToNextAttribute())
				{
					this.state = XPathNavigatorReader.State.InReadBinary;
					return false;
				}
				this.readBinaryHelper.Finish();
				return true;
			}
			return false;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000933F4 File Offset: 0x000915F4
		public override bool MoveToAttribute(string name)
		{
			int num;
			XPathNavigator elemNav = this.GetElemNav(out num);
			if (elemNav == null)
			{
				return false;
			}
			string text;
			string empty;
			ValidateNames.SplitQName(name, out text, out empty);
			bool flag;
			if ((flag = text.Length == 0 && empty == "xmlns") || text == "xmlns")
			{
				if (flag)
				{
					empty = string.Empty;
				}
				if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					while (!(elemNav.LocalName == empty))
					{
						if (!elemNav.MoveToNextNamespace(XPathNamespaceScope.Local))
						{
							return false;
						}
					}
					goto IL_00B5;
				}
			}
			else if (text.Length == 0)
			{
				if (elemNav.MoveToAttribute(empty, string.Empty))
				{
					goto IL_00B5;
				}
			}
			else if (elemNav.MoveToFirstAttribute())
			{
				while (!(elemNav.LocalName == empty) || !(elemNav.Prefix == text))
				{
					if (!elemNav.MoveToNextAttribute())
					{
						return false;
					}
				}
				goto IL_00B5;
			}
			return false;
			IL_00B5:
			if (this.state == XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper.Finish();
				this.state = this.savedState;
			}
			this.MoveToAttr(elemNav, num + 1);
			return true;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000934E4 File Offset: 0x000916E4
		public override bool MoveToElement()
		{
			XPathNavigatorReader.State state = this.state;
			if (state - XPathNavigatorReader.State.Attribute > 1)
			{
				if (state == XPathNavigatorReader.State.InReadBinary)
				{
					this.state = this.savedState;
					if (!this.MoveToElement())
					{
						this.state = XPathNavigatorReader.State.InReadBinary;
						return false;
					}
					this.readBinaryHelper.Finish();
				}
				return false;
			}
			if (!this.nav.MoveToParent())
			{
				return false;
			}
			this.depth--;
			if (this.state == XPathNavigatorReader.State.AttrVal)
			{
				this.depth--;
			}
			this.state = XPathNavigatorReader.State.Content;
			this.nodeType = XmlNodeType.Element;
			return true;
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00093571 File Offset: 0x00091771
		public override bool EOF
		{
			get
			{
				return this.state == XPathNavigatorReader.State.EOF;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0009357C File Offset: 0x0009177C
		public override ReadState ReadState
		{
			get
			{
				switch (this.state)
				{
				case XPathNavigatorReader.State.Initial:
					return ReadState.Initial;
				case XPathNavigatorReader.State.Content:
				case XPathNavigatorReader.State.EndElement:
				case XPathNavigatorReader.State.Attribute:
				case XPathNavigatorReader.State.AttrVal:
				case XPathNavigatorReader.State.InReadBinary:
					return ReadState.Interactive;
				case XPathNavigatorReader.State.EOF:
					return ReadState.EndOfFile;
				case XPathNavigatorReader.State.Closed:
					return ReadState.Closed;
				default:
					return ReadState.Error;
				}
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void ResolveEntity()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000935C4 File Offset: 0x000917C4
		public override bool ReadAttributeValue()
		{
			if (this.state == XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper.Finish();
				this.state = this.savedState;
			}
			if (this.state == XPathNavigatorReader.State.Attribute)
			{
				this.state = XPathNavigatorReader.State.AttrVal;
				this.nodeType = XmlNodeType.Text;
				this.depth++;
				return true;
			}
			return false;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00003242 File Offset: 0x00001442
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0009361C File Offset: 0x0009181C
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int num = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return num;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00093688 File Offset: 0x00091888
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int num = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return num;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x000936F4 File Offset: 0x000918F4
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int num = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return num;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00093760 File Offset: 0x00091960
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int num = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return num;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00092B64 File Offset: 0x00090D64
		public override string LookupNamespace(string prefix)
		{
			return this.nav.LookupNamespace(prefix);
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x000937CC File Offset: 0x000919CC
		public override int Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000937D4 File Offset: 0x000919D4
		public override bool Read()
		{
			this.attrCount = -1;
			switch (this.state)
			{
			case XPathNavigatorReader.State.Initial:
				this.nav = this.navToRead;
				this.state = XPathNavigatorReader.State.Content;
				if (this.nav.NodeType == XPathNodeType.Root)
				{
					if (!this.nav.MoveToFirstChild())
					{
						this.SetEOF();
						return false;
					}
					this.readEntireDocument = true;
				}
				else if (XPathNodeType.Attribute == this.nav.NodeType)
				{
					this.state = XPathNavigatorReader.State.Attribute;
				}
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				return true;
			case XPathNavigatorReader.State.Content:
				break;
			case XPathNavigatorReader.State.EndElement:
				goto IL_0114;
			case XPathNavigatorReader.State.Attribute:
			case XPathNavigatorReader.State.AttrVal:
				if (!this.nav.MoveToParent())
				{
					this.SetEOF();
					return false;
				}
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				this.depth--;
				if (this.state == XPathNavigatorReader.State.AttrVal)
				{
					this.depth--;
				}
				break;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				this.readBinaryHelper.Finish();
				return this.Read();
			case XPathNavigatorReader.State.EOF:
			case XPathNavigatorReader.State.Closed:
			case XPathNavigatorReader.State.Error:
				return false;
			default:
				return true;
			}
			if (this.nav.MoveToFirstChild())
			{
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				this.depth++;
				this.state = XPathNavigatorReader.State.Content;
				return true;
			}
			if (this.nodeType == XmlNodeType.Element && !this.nav.IsEmptyElement)
			{
				this.nodeType = XmlNodeType.EndElement;
				this.state = XPathNavigatorReader.State.EndElement;
				return true;
			}
			IL_0114:
			if (this.depth == 0 && !this.readEntireDocument)
			{
				this.SetEOF();
				return false;
			}
			if (this.nav.MoveToNext())
			{
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				this.state = XPathNavigatorReader.State.Content;
			}
			else
			{
				if (this.depth <= 0 || !this.nav.MoveToParent())
				{
					this.SetEOF();
					return false;
				}
				this.nodeType = XmlNodeType.EndElement;
				this.state = XPathNavigatorReader.State.EndElement;
				this.depth--;
			}
			return true;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000939F0 File Offset: 0x00091BF0
		public override void Close()
		{
			this.nav = XmlEmptyNavigator.Singleton;
			this.nodeType = XmlNodeType.None;
			this.state = XPathNavigatorReader.State.Closed;
			this.depth = 0;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00093A12 File Offset: 0x00091C12
		private void SetEOF()
		{
			this.nav = XmlEmptyNavigator.Singleton;
			this.nodeType = XmlNodeType.None;
			this.state = XPathNavigatorReader.State.EOF;
			this.depth = 0;
		}

		// Token: 0x04001554 RID: 5460
		private XPathNavigator nav;

		// Token: 0x04001555 RID: 5461
		private XPathNavigator navToRead;

		// Token: 0x04001556 RID: 5462
		private int depth;

		// Token: 0x04001557 RID: 5463
		private XPathNavigatorReader.State state;

		// Token: 0x04001558 RID: 5464
		private XmlNodeType nodeType;

		// Token: 0x04001559 RID: 5465
		private int attrCount;

		// Token: 0x0400155A RID: 5466
		private bool readEntireDocument;

		// Token: 0x0400155B RID: 5467
		protected IXmlLineInfo lineInfo;

		// Token: 0x0400155C RID: 5468
		protected IXmlSchemaInfo schemaInfo;

		// Token: 0x0400155D RID: 5469
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x0400155E RID: 5470
		private XPathNavigatorReader.State savedState;

		// Token: 0x0400155F RID: 5471
		internal const string space = "space";

		// Token: 0x04001560 RID: 5472
		internal static XmlNodeType[] convertFromXPathNodeType = new XmlNodeType[]
		{
			XmlNodeType.Document,
			XmlNodeType.Element,
			XmlNodeType.Attribute,
			XmlNodeType.Attribute,
			XmlNodeType.Text,
			XmlNodeType.SignificantWhitespace,
			XmlNodeType.Whitespace,
			XmlNodeType.ProcessingInstruction,
			XmlNodeType.Comment,
			XmlNodeType.None
		};

		// Token: 0x020002BE RID: 702
		private enum State
		{
			// Token: 0x04001562 RID: 5474
			Initial,
			// Token: 0x04001563 RID: 5475
			Content,
			// Token: 0x04001564 RID: 5476
			EndElement,
			// Token: 0x04001565 RID: 5477
			Attribute,
			// Token: 0x04001566 RID: 5478
			AttrVal,
			// Token: 0x04001567 RID: 5479
			InReadBinary,
			// Token: 0x04001568 RID: 5480
			EOF,
			// Token: 0x04001569 RID: 5481
			Closed,
			// Token: 0x0400156A RID: 5482
			Error
		}
	}
}
