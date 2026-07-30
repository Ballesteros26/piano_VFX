using System;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000061 RID: 97
	internal sealed class XPathDocumentNavigator : XPathNavigator, IXmlLineInfo
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000B059 File Offset: 0x00009259
		public XPathDocumentNavigator(XPathNode[] pageCurrent, int idxCurrent, XPathNode[] pageParent, int idxParent)
		{
			this.pageCurrent = pageCurrent;
			this.pageParent = pageParent;
			this.idxCurrent = idxCurrent;
			this.idxParent = idxParent;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000B07E File Offset: 0x0000927E
		public XPathDocumentNavigator(XPathDocumentNavigator nav)
			: this(nav.pageCurrent, nav.idxCurrent, nav.pageParent, nav.idxParent)
		{
			this.atomizedLocalName = nav.atomizedLocalName;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B0AC File Offset: 0x000092AC
		public override string Value
		{
			get
			{
				string value = this.pageCurrent[this.idxCurrent].Value;
				if (value != null)
				{
					return value;
				}
				if (this.idxParent != 0)
				{
					return this.pageParent[this.idxParent].Value;
				}
				string text = string.Empty;
				StringBuilder stringBuilder = null;
				XPathNode[] array2;
				XPathNode[] array = (array2 = this.pageCurrent);
				int num2;
				int num = (num2 = this.idxCurrent);
				if (!XPathNodeHelper.GetNonDescendant(ref array, ref num))
				{
					array = null;
					num = 0;
				}
				while (XPathNodeHelper.GetTextFollowing(ref array2, ref num2, array, num))
				{
					if (text.Length == 0)
					{
						text = array2[num2].Value;
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(text);
						}
						stringBuilder.Append(array2[num2].Value);
					}
				}
				if (stringBuilder == null)
				{
					return text;
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000B181 File Offset: 0x00009381
		public override XPathNavigator Clone()
		{
			return new XPathDocumentNavigator(this.pageCurrent, this.idxCurrent, this.pageParent, this.idxParent);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B1A0 File Offset: 0x000093A0
		public override XPathNodeType NodeType
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].NodeType;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public override string LocalName
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].LocalName;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public override string NamespaceURI
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].NamespaceUri;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000B1E8 File Offset: 0x000093E8
		public override string Name
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].Name;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000B200 File Offset: 0x00009400
		public override string Prefix
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].Prefix;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000B218 File Offset: 0x00009418
		public override string BaseURI
		{
			get
			{
				XPathNode[] array;
				int parent;
				if (this.idxParent != 0)
				{
					array = this.pageParent;
					parent = this.idxParent;
				}
				else
				{
					array = this.pageCurrent;
					parent = this.idxCurrent;
				}
				for (;;)
				{
					XPathNodeType nodeType = array[parent].NodeType;
					if (nodeType <= XPathNodeType.Element || nodeType == XPathNodeType.ProcessingInstruction)
					{
						break;
					}
					parent = array[parent].GetParent(out array);
					if (parent == 0)
					{
						goto Block_3;
					}
				}
				return array[parent].BaseUri;
				Block_3:
				return string.Empty;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B284 File Offset: 0x00009484
		public override bool IsEmptyElement
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].AllowShortcutTag;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000B29C File Offset: 0x0000949C
		public override XmlNameTable NameTable
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].Document.NameTable;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B2BC File Offset: 0x000094BC
		public override bool MoveToFirstAttribute()
		{
			XPathNode[] array = this.pageCurrent;
			int num = this.idxCurrent;
			if (XPathNodeHelper.GetFirstAttribute(ref this.pageCurrent, ref this.idxCurrent))
			{
				this.pageParent = array;
				this.idxParent = num;
				return true;
			}
			return false;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B2FB File Offset: 0x000094FB
		public override bool MoveToNextAttribute()
		{
			return XPathNodeHelper.GetNextAttribute(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B30E File Offset: 0x0000950E
		public override bool HasAttributes
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].HasAttribute;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B328 File Offset: 0x00009528
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			XPathNode[] array = this.pageCurrent;
			int num = this.idxCurrent;
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			if (XPathNodeHelper.GetAttribute(ref this.pageCurrent, ref this.idxCurrent, this.atomizedLocalName, namespaceURI))
			{
				this.pageParent = array;
				this.idxParent = num;
				return true;
			}
			return false;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B390 File Offset: 0x00009590
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			XPathNode[] array;
			int num;
			if (namespaceScope == XPathNamespaceScope.Local)
			{
				num = XPathNodeHelper.GetLocalNamespaces(this.pageCurrent, this.idxCurrent, out array);
			}
			else
			{
				num = XPathNodeHelper.GetInScopeNamespaces(this.pageCurrent, this.idxCurrent, out array);
			}
			while (num != 0)
			{
				if (namespaceScope != XPathNamespaceScope.ExcludeXml || !array[num].IsXmlNamespaceNode)
				{
					this.pageParent = this.pageCurrent;
					this.idxParent = this.idxCurrent;
					this.pageCurrent = array;
					this.idxCurrent = num;
					return true;
				}
				num = array[num].GetSibling(out array);
			}
			return false;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B41C File Offset: 0x0000961C
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XPathNode[] array = this.pageCurrent;
			int sibling = this.idxCurrent;
			if (array[sibling].NodeType != XPathNodeType.Namespace)
			{
				return false;
			}
			for (;;)
			{
				sibling = array[sibling].GetSibling(out array);
				if (sibling == 0)
				{
					break;
				}
				if (scope != XPathNamespaceScope.ExcludeXml)
				{
					goto Block_3;
				}
				if (!array[sibling].IsXmlNamespaceNode)
				{
					goto IL_006A;
				}
			}
			return false;
			Block_3:
			XPathNode[] array2;
			if (scope == XPathNamespaceScope.Local && (array[sibling].GetParent(out array2) != this.idxParent || array2 != this.pageParent))
			{
				return false;
			}
			IL_006A:
			this.pageCurrent = array;
			this.idxCurrent = sibling;
			return true;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B4A2 File Offset: 0x000096A2
		public override bool MoveToNext()
		{
			return XPathNodeHelper.GetContentSibling(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000B4B5 File Offset: 0x000096B5
		public override bool MoveToPrevious()
		{
			return this.idxParent == 0 && XPathNodeHelper.GetPreviousContentSibling(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B4D4 File Offset: 0x000096D4
		public override bool MoveToFirstChild()
		{
			if (this.pageCurrent[this.idxCurrent].HasCollapsedText)
			{
				this.pageParent = this.pageCurrent;
				this.idxParent = this.idxCurrent;
				this.idxCurrent = this.pageCurrent[this.idxCurrent].Document.GetCollapsedTextNode(out this.pageCurrent);
				return true;
			}
			return XPathNodeHelper.GetContentChild(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000B54C File Offset: 0x0000974C
		public override bool MoveToParent()
		{
			if (this.idxParent != 0)
			{
				this.pageCurrent = this.pageParent;
				this.idxCurrent = this.idxParent;
				this.pageParent = null;
				this.idxParent = 0;
				return true;
			}
			return XPathNodeHelper.GetParent(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000B59C File Offset: 0x0000979C
		public override bool MoveTo(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				this.pageCurrent = xpathDocumentNavigator.pageCurrent;
				this.idxCurrent = xpathDocumentNavigator.idxCurrent;
				this.pageParent = xpathDocumentNavigator.pageParent;
				this.idxParent = xpathDocumentNavigator.idxParent;
				return true;
			}
			return false;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public override bool MoveToId(string id)
		{
			XPathNode[] array;
			int num = this.pageCurrent[this.idxCurrent].Document.LookupIdElement(id, out array);
			if (num != 0)
			{
				this.pageCurrent = array;
				this.idxCurrent = num;
				this.pageParent = null;
				this.idxParent = 0;
				return true;
			}
			return false;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B638 File Offset: 0x00009838
		public override bool IsSamePosition(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			return xpathDocumentNavigator != null && (this.idxCurrent == xpathDocumentNavigator.idxCurrent && this.pageCurrent == xpathDocumentNavigator.pageCurrent && this.idxParent == xpathDocumentNavigator.idxParent) && this.pageParent == xpathDocumentNavigator.pageParent;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B68B File Offset: 0x0000988B
		public override bool HasChildren
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].HasContentChild;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B6A3 File Offset: 0x000098A3
		public override void MoveToRoot()
		{
			if (this.idxParent != 0)
			{
				this.pageParent = null;
				this.idxParent = 0;
			}
			this.idxCurrent = this.pageCurrent[this.idxCurrent].GetRoot(out this.pageCurrent);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B6DD File Offset: 0x000098DD
		public override bool MoveToChild(string localName, string namespaceURI)
		{
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return XPathNodeHelper.GetElementChild(ref this.pageCurrent, ref this.idxCurrent, this.atomizedLocalName, namespaceURI);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B718 File Offset: 0x00009918
		public override bool MoveToNext(string localName, string namespaceURI)
		{
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return XPathNodeHelper.GetElementSibling(ref this.pageCurrent, ref this.idxCurrent, this.atomizedLocalName, namespaceURI);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B754 File Offset: 0x00009954
		public override bool MoveToChild(XPathNodeType type)
		{
			if (!this.pageCurrent[this.idxCurrent].HasCollapsedText)
			{
				return XPathNodeHelper.GetContentChild(ref this.pageCurrent, ref this.idxCurrent, type);
			}
			if (type != XPathNodeType.Text && type != XPathNodeType.All)
			{
				return false;
			}
			this.pageParent = this.pageCurrent;
			this.idxParent = this.idxCurrent;
			this.idxCurrent = this.pageCurrent[this.idxCurrent].Document.GetCollapsedTextNode(out this.pageCurrent);
			return true;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B7D7 File Offset: 0x000099D7
		public override bool MoveToNext(XPathNodeType type)
		{
			return XPathNodeHelper.GetContentSibling(ref this.pageCurrent, ref this.idxCurrent, type);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B7EC File Offset: 0x000099EC
		public override bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			XPathNode[] array;
			int followingEnd = this.GetFollowingEnd(end as XPathDocumentNavigator, false, out array);
			if (this.idxParent == 0)
			{
				return XPathNodeHelper.GetElementFollowing(ref this.pageCurrent, ref this.idxCurrent, array, followingEnd, this.atomizedLocalName, namespaceURI);
			}
			if (!XPathNodeHelper.GetElementFollowing(ref this.pageParent, ref this.idxParent, array, followingEnd, this.atomizedLocalName, namespaceURI))
			{
				return false;
			}
			this.pageCurrent = this.pageParent;
			this.idxCurrent = this.idxParent;
			this.pageParent = null;
			this.idxParent = 0;
			return true;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B894 File Offset: 0x00009A94
		public override bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XPathDocumentNavigator xpathDocumentNavigator = end as XPathDocumentNavigator;
			XPathNode[] array;
			int num;
			if (type == XPathNodeType.Text || type == XPathNodeType.All)
			{
				if (this.pageCurrent[this.idxCurrent].HasCollapsedText)
				{
					if (xpathDocumentNavigator != null && this.idxCurrent == xpathDocumentNavigator.idxParent && this.pageCurrent == xpathDocumentNavigator.pageParent)
					{
						return false;
					}
					this.pageParent = this.pageCurrent;
					this.idxParent = this.idxCurrent;
					this.idxCurrent = this.pageCurrent[this.idxCurrent].Document.GetCollapsedTextNode(out this.pageCurrent);
					return true;
				}
				else if (type == XPathNodeType.Text)
				{
					num = this.GetFollowingEnd(xpathDocumentNavigator, true, out array);
					XPathNode[] array2;
					int num2;
					if (this.idxParent != 0)
					{
						array2 = this.pageParent;
						num2 = this.idxParent;
					}
					else
					{
						array2 = this.pageCurrent;
						num2 = this.idxCurrent;
					}
					if (xpathDocumentNavigator != null && xpathDocumentNavigator.idxParent != 0 && num2 == num && array2 == array)
					{
						return false;
					}
					if (!XPathNodeHelper.GetTextFollowing(ref array2, ref num2, array, num))
					{
						return false;
					}
					if (array2[num2].NodeType == XPathNodeType.Element)
					{
						this.idxCurrent = array2[num2].Document.GetCollapsedTextNode(out this.pageCurrent);
						this.pageParent = array2;
						this.idxParent = num2;
					}
					else
					{
						this.pageCurrent = array2;
						this.idxCurrent = num2;
						this.pageParent = null;
						this.idxParent = 0;
					}
					return true;
				}
			}
			num = this.GetFollowingEnd(xpathDocumentNavigator, false, out array);
			if (this.idxParent == 0)
			{
				return XPathNodeHelper.GetContentFollowing(ref this.pageCurrent, ref this.idxCurrent, array, num, type);
			}
			if (!XPathNodeHelper.GetContentFollowing(ref this.pageParent, ref this.idxParent, array, num, type))
			{
				return false;
			}
			this.pageCurrent = this.pageParent;
			this.idxCurrent = this.idxParent;
			this.pageParent = null;
			this.idxParent = 0;
			return true;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000BA51 File Offset: 0x00009C51
		public override XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new XPathDocumentKindChildIterator(this, type);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000BA5A File Offset: 0x00009C5A
		public override XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			if (name == null || name.Length == 0)
			{
				return base.SelectChildren(name, namespaceURI);
			}
			return new XPathDocumentElementChildIterator(this, name, namespaceURI);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000BA78 File Offset: 0x00009C78
		public override XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			return new XPathDocumentKindDescendantIterator(this, type, matchSelf);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000BA82 File Offset: 0x00009C82
		public override XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			if (name == null || name.Length == 0)
			{
				return base.SelectDescendants(name, namespaceURI, matchSelf);
			}
			return new XPathDocumentElementDescendantIterator(this, name, namespaceURI, matchSelf);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				XPathDocument document = this.pageCurrent[this.idxCurrent].Document;
				XPathDocument document2 = xpathDocumentNavigator.pageCurrent[xpathDocumentNavigator.idxCurrent].Document;
				if (document == document2)
				{
					int num = this.GetPrimaryLocation();
					int num2 = xpathDocumentNavigator.GetPrimaryLocation();
					if (num == num2)
					{
						num = this.GetSecondaryLocation();
						num2 = xpathDocumentNavigator.GetSecondaryLocation();
						if (num == num2)
						{
							return XmlNodeOrder.Same;
						}
					}
					if (num >= num2)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BB1C File Offset: 0x00009D1C
		public override bool IsDescendant(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				XPathNode[] array;
				int num;
				if (xpathDocumentNavigator.idxParent != 0)
				{
					array = xpathDocumentNavigator.pageParent;
					num = xpathDocumentNavigator.idxParent;
				}
				else
				{
					num = xpathDocumentNavigator.pageCurrent[xpathDocumentNavigator.idxCurrent].GetParent(out array);
				}
				while (num != 0)
				{
					if (num == this.idxCurrent && array == this.pageCurrent)
					{
						return true;
					}
					num = array[num].GetParent(out array);
				}
			}
			return false;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000BB8D File Offset: 0x00009D8D
		private int GetPrimaryLocation()
		{
			if (this.idxParent == 0)
			{
				return XPathNodeHelper.GetLocation(this.pageCurrent, this.idxCurrent);
			}
			return XPathNodeHelper.GetLocation(this.pageParent, this.idxParent);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000BBBC File Offset: 0x00009DBC
		private int GetSecondaryLocation()
		{
			if (this.idxParent == 0)
			{
				return int.MinValue;
			}
			XPathNodeType nodeType = this.pageCurrent[this.idxCurrent].NodeType;
			if (nodeType == XPathNodeType.Attribute)
			{
				return XPathNodeHelper.GetLocation(this.pageCurrent, this.idxCurrent);
			}
			if (nodeType == XPathNodeType.Namespace)
			{
				return -2147483647 + XPathNodeHelper.GetLocation(this.pageCurrent, this.idxCurrent);
			}
			return int.MaxValue;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000BC28 File Offset: 0x00009E28
		internal override string UniqueId
		{
			get
			{
				char[] array = new char[16];
				int num = 0;
				array[num++] = XPathNavigator.NodeTypeLetter[(int)this.pageCurrent[this.idxCurrent].NodeType];
				int num2;
				if (this.idxParent != 0)
				{
					num2 = (this.pageParent[0].PageInfo.PageNumber - 1 << 16) | (this.idxParent - 1);
					do
					{
						array[num++] = XPathNavigator.UniqueIdTbl[num2 & 31];
						num2 >>= 5;
					}
					while (num2 != 0);
					array[num++] = '0';
				}
				num2 = (this.pageCurrent[0].PageInfo.PageNumber - 1 << 16) | (this.idxCurrent - 1);
				do
				{
					array[num++] = XPathNavigator.UniqueIdTbl[num2 & 31];
					num2 >>= 5;
				}
				while (num2 != 0);
				return new string(array, 0, num);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000BCF5 File Offset: 0x00009EF5
		public override object UnderlyingObject
		{
			get
			{
				return this.Clone();
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000BCFD File Offset: 0x00009EFD
		public bool HasLineInfo()
		{
			return this.pageCurrent[this.idxCurrent].Document.HasLineInfo;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000BD1A File Offset: 0x00009F1A
		public int LineNumber
		{
			get
			{
				if (this.idxParent != 0 && this.NodeType == XPathNodeType.Text)
				{
					return this.pageParent[this.idxParent].LineNumber;
				}
				return this.pageCurrent[this.idxCurrent].LineNumber;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000BD5A File Offset: 0x00009F5A
		public int LinePosition
		{
			get
			{
				if (this.idxParent != 0 && this.NodeType == XPathNodeType.Text)
				{
					return this.pageParent[this.idxParent].CollapsedLinePosition;
				}
				return this.pageCurrent[this.idxCurrent].LinePosition;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000BD9A File Offset: 0x00009F9A
		public int GetPositionHashCode()
		{
			return this.idxCurrent ^ this.idxParent;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public bool IsElementMatch(string localName, string namespaceURI)
		{
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return this.idxParent == 0 && this.pageCurrent[this.idxCurrent].ElementMatch(this.atomizedLocalName, namespaceURI);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000BE01 File Offset: 0x0000A001
		public bool IsContentKindMatch(XPathNodeType typ)
		{
			return ((1 << (int)this.pageCurrent[this.idxCurrent].NodeType) & XPathNavigator.GetContentKindMask(typ)) != 0;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000BE28 File Offset: 0x0000A028
		public bool IsKindMatch(XPathNodeType typ)
		{
			return ((1 << (int)this.pageCurrent[this.idxCurrent].NodeType) & XPathNavigator.GetKindMask(typ)) != 0;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000BE50 File Offset: 0x0000A050
		private int GetFollowingEnd(XPathDocumentNavigator end, bool useParentOfVirtual, out XPathNode[] pageEnd)
		{
			if (end == null || this.pageCurrent[this.idxCurrent].Document != end.pageCurrent[end.idxCurrent].Document)
			{
				pageEnd = null;
				return 0;
			}
			if (end.idxParent == 0)
			{
				pageEnd = end.pageCurrent;
				return end.idxCurrent;
			}
			pageEnd = end.pageParent;
			if (!useParentOfVirtual)
			{
				return end.idxParent + 1;
			}
			return end.idxParent;
		}

		// Token: 0x0400018B RID: 395
		private XPathNode[] pageCurrent;

		// Token: 0x0400018C RID: 396
		private XPathNode[] pageParent;

		// Token: 0x0400018D RID: 397
		private int idxCurrent;

		// Token: 0x0400018E RID: 398
		private int idxParent;

		// Token: 0x0400018F RID: 399
		private string atomizedLocalName;
	}
}
