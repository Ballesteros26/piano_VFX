using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x02000222 RID: 546
	internal class XmlElementList : XmlNodeList
	{
		// Token: 0x060014A3 RID: 5283 RVA: 0x000758A8 File Offset: 0x00073AA8
		private XmlElementList(XmlNode parent)
		{
			this.rootNode = parent;
			this.curInd = -1;
			this.curElem = this.rootNode;
			this.changeCount = 0;
			this.empty = false;
			this.atomized = true;
			this.matchCount = -1;
			this.listener = new WeakReference(new XmlElementListListener(parent.Document, this));
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00075908 File Offset: 0x00073B08
		~XmlElementList()
		{
			this.Dispose(false);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00075938 File Offset: 0x00073B38
		internal void ConcurrencyCheck(XmlNodeChangedEventArgs args)
		{
			if (!this.atomized)
			{
				XmlNameTable nameTable = this.rootNode.Document.NameTable;
				this.localName = nameTable.Add(this.localName);
				this.namespaceURI = nameTable.Add(this.namespaceURI);
				this.atomized = true;
			}
			if (this.IsMatch(args.Node))
			{
				this.changeCount++;
				this.curInd = -1;
				this.curElem = this.rootNode;
				if (args.Action == XmlNodeChangedAction.Insert)
				{
					this.empty = false;
				}
			}
			this.matchCount = -1;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000759D0 File Offset: 0x00073BD0
		internal XmlElementList(XmlNode parent, string name)
			: this(parent)
		{
			XmlNameTable nameTable = parent.Document.NameTable;
			this.asterisk = nameTable.Add("*");
			this.name = nameTable.Add(name);
			this.localName = null;
			this.namespaceURI = null;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00075A1C File Offset: 0x00073C1C
		internal XmlElementList(XmlNode parent, string localName, string namespaceURI)
			: this(parent)
		{
			XmlNameTable nameTable = parent.Document.NameTable;
			this.asterisk = nameTable.Add("*");
			this.localName = nameTable.Get(localName);
			this.namespaceURI = nameTable.Get(namespaceURI);
			if (this.localName == null || this.namespaceURI == null)
			{
				this.empty = true;
				this.atomized = false;
				this.localName = localName;
				this.namespaceURI = namespaceURI;
			}
			this.name = null;
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00075A9A File Offset: 0x00073C9A
		internal int ChangeCount
		{
			get
			{
				return this.changeCount;
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00075AA4 File Offset: 0x00073CA4
		private XmlNode NextElemInPreOrder(XmlNode curNode)
		{
			XmlNode xmlNode = curNode.FirstChild;
			if (xmlNode == null)
			{
				xmlNode = curNode;
				while (xmlNode != null && xmlNode != this.rootNode && xmlNode.NextSibling == null)
				{
					xmlNode = xmlNode.ParentNode;
				}
				if (xmlNode != null && xmlNode != this.rootNode)
				{
					xmlNode = xmlNode.NextSibling;
				}
			}
			if (xmlNode == this.rootNode)
			{
				xmlNode = null;
			}
			return xmlNode;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00075AFC File Offset: 0x00073CFC
		private XmlNode PrevElemInPreOrder(XmlNode curNode)
		{
			XmlNode xmlNode = curNode.PreviousSibling;
			while (xmlNode != null && xmlNode.LastChild != null)
			{
				xmlNode = xmlNode.LastChild;
			}
			if (xmlNode == null)
			{
				xmlNode = curNode.ParentNode;
			}
			if (xmlNode == this.rootNode)
			{
				xmlNode = null;
			}
			return xmlNode;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00075B3C File Offset: 0x00073D3C
		private bool IsMatch(XmlNode curNode)
		{
			if (curNode.NodeType == XmlNodeType.Element)
			{
				if (this.name != null)
				{
					if (Ref.Equal(this.name, this.asterisk) || Ref.Equal(curNode.Name, this.name))
					{
						return true;
					}
				}
				else if ((Ref.Equal(this.localName, this.asterisk) || Ref.Equal(curNode.LocalName, this.localName)) && (Ref.Equal(this.namespaceURI, this.asterisk) || curNode.NamespaceURI == this.namespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00075BD4 File Offset: 0x00073DD4
		private XmlNode GetMatchingNode(XmlNode n, bool bNext)
		{
			XmlNode xmlNode = n;
			do
			{
				if (bNext)
				{
					xmlNode = this.NextElemInPreOrder(xmlNode);
				}
				else
				{
					xmlNode = this.PrevElemInPreOrder(xmlNode);
				}
			}
			while (xmlNode != null && !this.IsMatch(xmlNode));
			return xmlNode;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00075C08 File Offset: 0x00073E08
		private XmlNode GetNthMatchingNode(XmlNode n, bool bNext, int nCount)
		{
			XmlNode xmlNode = n;
			for (int i = 0; i < nCount; i++)
			{
				xmlNode = this.GetMatchingNode(xmlNode, bNext);
				if (xmlNode == null)
				{
					return null;
				}
			}
			return xmlNode;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00075C34 File Offset: 0x00073E34
		public XmlNode GetNextNode(XmlNode n)
		{
			if (this.empty)
			{
				return null;
			}
			XmlNode xmlNode = ((n == null) ? this.rootNode : n);
			return this.GetMatchingNode(xmlNode, true);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00075C60 File Offset: 0x00073E60
		public override XmlNode Item(int index)
		{
			if (this.rootNode == null || index < 0)
			{
				return null;
			}
			if (this.empty)
			{
				return null;
			}
			if (this.curInd == index)
			{
				return this.curElem;
			}
			int num = index - this.curInd;
			bool flag = num > 0;
			if (num < 0)
			{
				num = -num;
			}
			XmlNode nthMatchingNode;
			if ((nthMatchingNode = this.GetNthMatchingNode(this.curElem, flag, num)) != null)
			{
				this.curInd = index;
				this.curElem = nthMatchingNode;
				return this.curElem;
			}
			return null;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00075CD4 File Offset: 0x00073ED4
		public override int Count
		{
			get
			{
				if (this.empty)
				{
					return 0;
				}
				if (this.matchCount < 0)
				{
					int num = 0;
					int num2 = this.changeCount;
					XmlNode matchingNode = this.rootNode;
					while ((matchingNode = this.GetMatchingNode(matchingNode, true)) != null)
					{
						num++;
					}
					if (num2 != this.changeCount)
					{
						return num;
					}
					this.matchCount = num;
				}
				return this.matchCount;
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00075D2E File Offset: 0x00073F2E
		public override IEnumerator GetEnumerator()
		{
			if (this.empty)
			{
				return new XmlEmptyElementListEnumerator(this);
			}
			return new XmlElementListEnumerator(this);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00075D45 File Offset: 0x00073F45
		protected override void PrivateDisposeNodeList()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00075D54 File Offset: 0x00073F54
		protected virtual void Dispose(bool disposing)
		{
			if (this.listener != null)
			{
				XmlElementListListener xmlElementListListener = (XmlElementListListener)this.listener.Target;
				if (xmlElementListListener != null)
				{
					xmlElementListListener.Unregister();
				}
				this.listener = null;
			}
		}

		// Token: 0x04000DC4 RID: 3524
		private string asterisk;

		// Token: 0x04000DC5 RID: 3525
		private int changeCount;

		// Token: 0x04000DC6 RID: 3526
		private string name;

		// Token: 0x04000DC7 RID: 3527
		private string localName;

		// Token: 0x04000DC8 RID: 3528
		private string namespaceURI;

		// Token: 0x04000DC9 RID: 3529
		private XmlNode rootNode;

		// Token: 0x04000DCA RID: 3530
		private int curInd;

		// Token: 0x04000DCB RID: 3531
		private XmlNode curElem;

		// Token: 0x04000DCC RID: 3532
		private bool empty;

		// Token: 0x04000DCD RID: 3533
		private bool atomized;

		// Token: 0x04000DCE RID: 3534
		private int matchCount;

		// Token: 0x04000DCF RID: 3535
		private WeakReference listener;
	}
}
