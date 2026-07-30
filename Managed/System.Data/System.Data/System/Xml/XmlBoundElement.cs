using System;
using System.Data;
using System.Threading;

namespace System.Xml
{
	// Token: 0x02000034 RID: 52
	internal sealed class XmlBoundElement : XmlElement
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000958B File Offset: 0x0000778B
		internal XmlBoundElement(string prefix, string localName, string namespaceURI, XmlDocument doc)
			: base(prefix, localName, namespaceURI, doc)
		{
			this._state = ElementState.None;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000959F File Offset: 0x0000779F
		public override XmlAttributeCollection Attributes
		{
			get
			{
				this.AutoFoliate();
				return base.Attributes;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000095AD File Offset: 0x000077AD
		public override bool HasAttributes
		{
			get
			{
				return this.Attributes.Count > 0;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000095BD File Offset: 0x000077BD
		public override XmlNode FirstChild
		{
			get
			{
				this.AutoFoliate();
				return base.FirstChild;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000095CB File Offset: 0x000077CB
		internal XmlNode SafeFirstChild
		{
			get
			{
				return base.FirstChild;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000095D3 File Offset: 0x000077D3
		public override XmlNode LastChild
		{
			get
			{
				this.AutoFoliate();
				return base.LastChild;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000095E4 File Offset: 0x000077E4
		public override XmlNode PreviousSibling
		{
			get
			{
				XmlNode previousSibling = base.PreviousSibling;
				if (previousSibling == null)
				{
					XmlBoundElement xmlBoundElement = this.ParentNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						xmlBoundElement.AutoFoliate();
						return base.PreviousSibling;
					}
				}
				return previousSibling;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009618 File Offset: 0x00007818
		internal XmlNode SafePreviousSibling
		{
			get
			{
				return base.PreviousSibling;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00009620 File Offset: 0x00007820
		public override XmlNode NextSibling
		{
			get
			{
				XmlNode nextSibling = base.NextSibling;
				if (nextSibling == null)
				{
					XmlBoundElement xmlBoundElement = this.ParentNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						xmlBoundElement.AutoFoliate();
						return base.NextSibling;
					}
				}
				return nextSibling;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00009654 File Offset: 0x00007854
		internal XmlNode SafeNextSibling
		{
			get
			{
				return base.NextSibling;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000965C File Offset: 0x0000785C
		public override bool HasChildNodes
		{
			get
			{
				this.AutoFoliate();
				return base.HasChildNodes;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000966A File Offset: 0x0000786A
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertBefore(newChild, refChild);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000967A File Offset: 0x0000787A
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertAfter(newChild, refChild);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000968A File Offset: 0x0000788A
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			this.AutoFoliate();
			return base.ReplaceChild(newChild, oldChild);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000969A File Offset: 0x0000789A
		public override XmlNode AppendChild(XmlNode newChild)
		{
			this.AutoFoliate();
			return base.AppendChild(newChild);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000096AC File Offset: 0x000078AC
		internal void RemoveAllChildren()
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				this.RemoveChild(xmlNode);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000096D4 File Offset: 0x000078D4
		// (set) Token: 0x0600018A RID: 394 RVA: 0x000096DC File Offset: 0x000078DC
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAllChildren();
				XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
				bool ignoreXmlEvents = xmlDataDocument.IgnoreXmlEvents;
				bool ignoreDataSetEvents = xmlDataDocument.IgnoreDataSetEvents;
				xmlDataDocument.IgnoreXmlEvents = true;
				xmlDataDocument.IgnoreDataSetEvents = true;
				base.InnerXml = value;
				xmlDataDocument.SyncTree(this);
				xmlDataDocument.IgnoreDataSetEvents = ignoreDataSetEvents;
				xmlDataDocument.IgnoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00009731 File Offset: 0x00007931
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00009739 File Offset: 0x00007939
		internal DataRow Row
		{
			get
			{
				return this._row;
			}
			set
			{
				this._row = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00009742 File Offset: 0x00007942
		internal bool IsFoliated
		{
			get
			{
				while (this._state == ElementState.Foliating || this._state == ElementState.Defoliating)
				{
					Thread.Sleep(0);
				}
				return this._state != ElementState.Defoliated;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000976A File Offset: 0x0000796A
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00009772 File Offset: 0x00007972
		internal ElementState ElementState
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000977C File Offset: 0x0000797C
		internal void Foliate(ElementState newState)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, newState);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000097A0 File Offset: 0x000079A0
		private void AutoFoliate()
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, xmlDataDocument.AutoFoliationState);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000097CC File Offset: 0x000079CC
		public override XmlNode CloneNode(bool deep)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			ElementState autoFoliationState = xmlDataDocument.AutoFoliationState;
			xmlDataDocument.AutoFoliationState = ElementState.WeakFoliation;
			XmlElement xmlElement;
			try
			{
				this.Foliate(ElementState.WeakFoliation);
				xmlElement = (XmlElement)base.CloneNode(deep);
			}
			finally
			{
				xmlDataDocument.AutoFoliationState = autoFoliationState;
			}
			return xmlElement;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009824 File Offset: 0x00007A24
		public override void WriteContentTo(XmlWriter w)
		{
			DataPointer dataPointer = new DataPointer((XmlDataDocument)this.OwnerDocument, this);
			try
			{
				dataPointer.AddPointer();
				XmlBoundElement.WriteBoundElementContentTo(dataPointer, w);
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000986C File Offset: 0x00007A6C
		public override void WriteTo(XmlWriter w)
		{
			DataPointer dataPointer = new DataPointer((XmlDataDocument)this.OwnerDocument, this);
			try
			{
				dataPointer.AddPointer();
				this.WriteRootBoundElementTo(dataPointer, w);
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000098B4 File Offset: 0x00007AB4
		private void WriteRootBoundElementTo(DataPointer dp, XmlWriter w)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			w.WriteStartElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			int attributeCount = dp.AttributeCount;
			bool flag = false;
			if (attributeCount > 0)
			{
				for (int i = 0; i < attributeCount; i++)
				{
					dp.MoveToAttribute(i);
					if (dp.Prefix == "xmlns" && dp.LocalName == "xsi")
					{
						flag = true;
					}
					XmlBoundElement.WriteTo(dp, w);
					dp.MoveToOwnerElement();
				}
			}
			if (!flag && xmlDataDocument._bLoadFromDataSet && xmlDataDocument._bHasXSINIL)
			{
				w.WriteAttributeString("xmlns", "xsi", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2001/XMLSchema-instance");
			}
			XmlBoundElement.WriteBoundElementContentTo(dp, w);
			if (dp.IsEmptyElement)
			{
				w.WriteEndElement();
				return;
			}
			w.WriteFullEndElement();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009984 File Offset: 0x00007B84
		private static void WriteBoundElementTo(DataPointer dp, XmlWriter w)
		{
			w.WriteStartElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			int attributeCount = dp.AttributeCount;
			if (attributeCount > 0)
			{
				for (int i = 0; i < attributeCount; i++)
				{
					dp.MoveToAttribute(i);
					XmlBoundElement.WriteTo(dp, w);
					dp.MoveToOwnerElement();
				}
			}
			XmlBoundElement.WriteBoundElementContentTo(dp, w);
			if (dp.IsEmptyElement)
			{
				w.WriteEndElement();
				return;
			}
			w.WriteFullEndElement();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000099F2 File Offset: 0x00007BF2
		private static void WriteBoundElementContentTo(DataPointer dp, XmlWriter w)
		{
			if (!dp.IsEmptyElement && dp.MoveToFirstChild())
			{
				do
				{
					XmlBoundElement.WriteTo(dp, w);
				}
				while (dp.MoveToNextSibling());
				dp.MoveToParent();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009A1C File Offset: 0x00007C1C
		private static void WriteTo(DataPointer dp, XmlWriter w)
		{
			switch (dp.NodeType)
			{
			case XmlNodeType.Element:
				XmlBoundElement.WriteBoundElementTo(dp, w);
				return;
			case XmlNodeType.Attribute:
				if (!dp.IsDefault)
				{
					w.WriteStartAttribute(dp.Prefix, dp.LocalName, dp.NamespaceURI);
					if (dp.MoveToFirstChild())
					{
						do
						{
							XmlBoundElement.WriteTo(dp, w);
						}
						while (dp.MoveToNextSibling());
						dp.MoveToParent();
					}
					w.WriteEndAttribute();
					return;
				}
				break;
			case XmlNodeType.Text:
				w.WriteString(dp.Value);
				return;
			default:
				if (dp.GetNode() != null)
				{
					dp.GetNode().WriteTo(w);
				}
				break;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009AB4 File Offset: 0x00007CB4
		public override XmlNodeList GetElementsByTagName(string name)
		{
			XmlNodeList elementsByTagName = base.GetElementsByTagName(name);
			int count = elementsByTagName.Count;
			return elementsByTagName;
		}

		// Token: 0x0400042F RID: 1071
		private DataRow _row;

		// Token: 0x04000430 RID: 1072
		private ElementState _state;
	}
}
