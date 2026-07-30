using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x0200020F RID: 527
	internal sealed class DocumentXmlWriter : XmlRawWriter, IXmlNamespaceResolver
	{
		// Token: 0x060012F8 RID: 4856 RVA: 0x00070DE4 File Offset: 0x0006EFE4
		public DocumentXmlWriter(DocumentXmlWriterType type, XmlNode start, XmlDocument document)
		{
			this.type = type;
			this.start = start;
			this.document = document;
			this.state = this.StartState();
			this.fragment = new List<XmlNode>();
			this.settings = new XmlWriterSettings();
			this.settings.ReadOnly = false;
			this.settings.CheckCharacters = false;
			this.settings.CloseOutput = false;
			this.settings.ConformanceLevel = ((this.state == DocumentXmlWriter.State.Prolog) ? ConformanceLevel.Document : ConformanceLevel.Fragment);
			this.settings.ReadOnly = true;
		}

		// Token: 0x17000348 RID: 840
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x00070E76 File Offset: 0x0006F076
		public XmlNamespaceManager NamespaceManager
		{
			set
			{
				this.namespaceManager = value;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00070E7F File Offset: 0x0006F07F
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00070E87 File Offset: 0x0006F087
		internal void SetSettings(XmlWriterSettings value)
		{
			this.settings = value;
		}

		// Token: 0x1700034A RID: 842
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x00070E90 File Offset: 0x0006F090
		public DocumentXPathNavigator Navigator
		{
			set
			{
				this.navigator = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x00070E99 File Offset: 0x0006F099
		public XmlNode EndNode
		{
			set
			{
				this.end = value;
			}
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00070EA4 File Offset: 0x0006F0A4
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteXmlDeclaration);
			if (standalone != XmlStandalone.Omit)
			{
				XmlNode xmlNode = this.document.CreateXmlDeclaration("1.0", string.Empty, (standalone == XmlStandalone.Yes) ? "yes" : "no");
				this.AddChild(xmlNode, this.write);
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00070EF0 File Offset: 0x0006F0F0
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteXmlDeclaration);
			string text;
			string text2;
			string text3;
			XmlLoader.ParseXmlDeclarationValue(xmldecl, out text, out text2, out text3);
			XmlNode xmlNode = this.document.CreateXmlDeclaration(text, text2, text3);
			this.AddChild(xmlNode, this.write);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00070F2C File Offset: 0x0006F12C
		public override void WriteStartDocument()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartDocument);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00070F2C File Offset: 0x0006F12C
		public override void WriteStartDocument(bool standalone)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartDocument);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00070F35 File Offset: 0x0006F135
		public override void WriteEndDocument()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndDocument);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00070F40 File Offset: 0x0006F140
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteDocType);
			XmlNode xmlNode = this.document.CreateDocumentType(name, pubid, sysid, subset);
			this.AddChild(xmlNode, this.write);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00070F74 File Offset: 0x0006F174
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartElement);
			XmlNode xmlNode = this.document.CreateElement(prefix, localName, ns);
			this.AddChild(xmlNode, this.write);
			this.write = xmlNode;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00070FAB File Offset: 0x0006F1AB
		public override void WriteEndElement()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndElement);
			if (this.write == null)
			{
				throw new InvalidOperationException();
			}
			this.write = this.write.ParentNode;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00070FD3 File Offset: 0x0006F1D3
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.WriteEndElement();
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00070FDC File Offset: 0x0006F1DC
		public override void WriteFullEndElement()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteFullEndElement);
			XmlElement xmlElement = this.write as XmlElement;
			if (xmlElement == null)
			{
				throw new InvalidOperationException();
			}
			xmlElement.IsEmpty = false;
			this.write = xmlElement.ParentNode;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00071018 File Offset: 0x0006F218
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.WriteFullEndElement();
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void StartElementContent()
		{
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00071020 File Offset: 0x0006F220
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartAttribute);
			XmlAttribute xmlAttribute = this.document.CreateAttribute(prefix, localName, ns);
			this.AddAttribute(xmlAttribute, this.write);
			this.write = xmlAttribute;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00071058 File Offset: 0x0006F258
		public override void WriteEndAttribute()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndAttribute);
			XmlAttribute xmlAttribute = this.write as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException();
			}
			if (!xmlAttribute.HasChildNodes)
			{
				XmlNode xmlNode = this.document.CreateTextNode(string.Empty);
				this.AddChild(xmlNode, xmlAttribute);
			}
			this.write = xmlAttribute.OwnerElement;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00020501 File Offset: 0x0001E701
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.WriteStartNamespaceDeclaration(prefix);
			this.WriteString(ns);
			this.WriteEndNamespaceDeclaration();
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000710B0 File Offset: 0x0006F2B0
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartNamespaceDeclaration);
			XmlAttribute xmlAttribute;
			if (prefix.Length == 0)
			{
				xmlAttribute = this.document.CreateAttribute(prefix, this.document.strXmlns, this.document.strReservedXmlns);
			}
			else
			{
				xmlAttribute = this.document.CreateAttribute(this.document.strXmlns, prefix, this.document.strReservedXmlns);
			}
			this.AddAttribute(xmlAttribute, this.write);
			this.write = xmlAttribute;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0007112C File Offset: 0x0006F32C
		internal override void WriteEndNamespaceDeclaration()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndNamespaceDeclaration);
			XmlAttribute xmlAttribute = this.write as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException();
			}
			if (!xmlAttribute.HasChildNodes)
			{
				XmlNode xmlNode = this.document.CreateTextNode(string.Empty);
				this.AddChild(xmlNode, xmlAttribute);
			}
			this.write = xmlAttribute.OwnerElement;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00071184 File Offset: 0x0006F384
		public override void WriteCData(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteCData);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode xmlNode = this.document.CreateCDataSection(text);
			this.AddChild(xmlNode, this.write);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000711BC File Offset: 0x0006F3BC
		public override void WriteComment(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteComment);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode xmlNode = this.document.CreateComment(text);
			this.AddChild(xmlNode, this.write);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000711F4 File Offset: 0x0006F3F4
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteProcessingInstruction);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode xmlNode = this.document.CreateProcessingInstruction(name, text);
			this.AddChild(xmlNode, this.write);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0007122C File Offset: 0x0006F42C
		public override void WriteEntityRef(string name)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEntityRef);
			XmlNode xmlNode = this.document.CreateEntityReference(name);
			this.AddChild(xmlNode, this.write);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0007125B File Offset: 0x0006F45B
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(new string(ch, 1));
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0007126C File Offset: 0x0006F46C
		public override void WriteWhitespace(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteWhitespace);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			if (this.document.PreserveWhitespace)
			{
				XmlNode xmlNode = this.document.CreateWhitespace(text);
				this.AddChild(xmlNode, this.write);
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000712B0 File Offset: 0x0006F4B0
		public override void WriteString(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteString);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode xmlNode = this.document.CreateTextNode(text);
			this.AddChild(xmlNode, this.write);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000712E6 File Offset: 0x0006F4E6
		public override void WriteSurrogateCharEntity(char lowCh, char highCh)
		{
			this.WriteString(new string(new char[] { highCh, lowCh }));
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00028CF6 File Offset: 0x00026EF6
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00002F50 File Offset: 0x00001150
		public override void Close()
		{
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00071304 File Offset: 0x0006F504
		internal override void Close(WriteState currentState)
		{
			if (currentState == WriteState.Error)
			{
				return;
			}
			try
			{
				switch (this.type)
				{
				case DocumentXmlWriterType.InsertSiblingAfter:
				{
					XmlNode xmlNode = this.start.ParentNode;
					if (xmlNode == null)
					{
						throw new InvalidOperationException(Res.GetString("The current position of the navigator is missing a valid parent."));
					}
					for (int i = this.fragment.Count - 1; i >= 0; i--)
					{
						xmlNode.InsertAfter(this.fragment[i], this.start);
					}
					break;
				}
				case DocumentXmlWriterType.InsertSiblingBefore:
				{
					XmlNode xmlNode = this.start.ParentNode;
					if (xmlNode == null)
					{
						throw new InvalidOperationException(Res.GetString("The current position of the navigator is missing a valid parent."));
					}
					for (int j = 0; j < this.fragment.Count; j++)
					{
						xmlNode.InsertBefore(this.fragment[j], this.start);
					}
					break;
				}
				case DocumentXmlWriterType.PrependChild:
				{
					for (int k = this.fragment.Count - 1; k >= 0; k--)
					{
						this.start.PrependChild(this.fragment[k]);
					}
					break;
				}
				case DocumentXmlWriterType.AppendChild:
				{
					for (int l = 0; l < this.fragment.Count; l++)
					{
						this.start.AppendChild(this.fragment[l]);
					}
					break;
				}
				case DocumentXmlWriterType.AppendAttribute:
					this.CloseWithAppendAttribute();
					break;
				case DocumentXmlWriterType.ReplaceToFollowingSibling:
					if (this.fragment.Count == 0)
					{
						throw new InvalidOperationException(Res.GetString("No content generated as the result of the operation."));
					}
					this.CloseWithReplaceToFollowingSibling();
					break;
				}
			}
			finally
			{
				this.fragment.Clear();
			}
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000714AC File Offset: 0x0006F6AC
		private void CloseWithAppendAttribute()
		{
			XmlAttributeCollection attributes = (this.start as XmlElement).Attributes;
			for (int i = 0; i < this.fragment.Count; i++)
			{
				XmlAttribute xmlAttribute = this.fragment[i] as XmlAttribute;
				int num = attributes.FindNodeOffsetNS(xmlAttribute);
				if (num != -1 && ((XmlAttribute)attributes.nodes[num]).Specified)
				{
					throw new XmlException("'{0}' is a duplicate attribute name.", (xmlAttribute.Prefix.Length == 0) ? xmlAttribute.LocalName : (xmlAttribute.Prefix + ":" + xmlAttribute.LocalName));
				}
			}
			for (int j = 0; j < this.fragment.Count; j++)
			{
				XmlAttribute xmlAttribute2 = this.fragment[j] as XmlAttribute;
				attributes.Append(xmlAttribute2);
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00071584 File Offset: 0x0006F784
		private void CloseWithReplaceToFollowingSibling()
		{
			XmlNode parentNode = this.start.ParentNode;
			if (parentNode == null)
			{
				throw new InvalidOperationException(Res.GetString("The current position of the navigator is missing a valid parent."));
			}
			if (this.start != this.end)
			{
				if (!DocumentXPathNavigator.IsFollowingSibling(this.start, this.end))
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
				}
				if (this.start.IsReadOnly)
				{
					throw new InvalidOperationException(Res.GetString("This node is read-only. It cannot be modified."));
				}
				DocumentXPathNavigator.DeleteToFollowingSibling(this.start.NextSibling, this.end);
			}
			XmlNode xmlNode = this.fragment[0];
			parentNode.ReplaceChild(xmlNode, this.start);
			for (int i = this.fragment.Count - 1; i >= 1; i--)
			{
				parentNode.InsertAfter(this.fragment[i], xmlNode);
			}
			this.navigator.ResetPosition(xmlNode);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00002F50 File Offset: 0x00001150
		public override void Flush()
		{
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00071667 File Offset: 0x0006F867
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.namespaceManager.GetNamespacesInScope(scope);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00071675 File Offset: 0x0006F875
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.namespaceManager.LookupNamespace(prefix);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00071683 File Offset: 0x0006F883
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.namespaceManager.LookupPrefix(namespaceName);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00071691 File Offset: 0x0006F891
		private void AddAttribute(XmlAttribute attr, XmlNode parent)
		{
			if (parent == null)
			{
				this.fragment.Add(attr);
				return;
			}
			XmlElement xmlElement = parent as XmlElement;
			if (xmlElement == null)
			{
				throw new InvalidOperationException();
			}
			xmlElement.Attributes.Append(attr);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000716BE File Offset: 0x0006F8BE
		private void AddChild(XmlNode node, XmlNode parent)
		{
			if (parent == null)
			{
				this.fragment.Add(node);
				return;
			}
			parent.AppendChild(node);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x000716D8 File Offset: 0x0006F8D8
		private DocumentXmlWriter.State StartState()
		{
			XmlNodeType xmlNodeType = XmlNodeType.None;
			switch (this.type)
			{
			case DocumentXmlWriterType.InsertSiblingAfter:
			case DocumentXmlWriterType.InsertSiblingBefore:
			{
				XmlNode parentNode = this.start.ParentNode;
				if (parentNode != null)
				{
					xmlNodeType = parentNode.NodeType;
				}
				if (xmlNodeType == XmlNodeType.Document)
				{
					return DocumentXmlWriter.State.Prolog;
				}
				if (xmlNodeType == XmlNodeType.DocumentFragment)
				{
					return DocumentXmlWriter.State.Fragment;
				}
				break;
			}
			case DocumentXmlWriterType.PrependChild:
			case DocumentXmlWriterType.AppendChild:
				xmlNodeType = this.start.NodeType;
				if (xmlNodeType == XmlNodeType.Document)
				{
					return DocumentXmlWriter.State.Prolog;
				}
				if (xmlNodeType == XmlNodeType.DocumentFragment)
				{
					return DocumentXmlWriter.State.Fragment;
				}
				break;
			case DocumentXmlWriterType.AppendAttribute:
				return DocumentXmlWriter.State.Attribute;
			}
			return DocumentXmlWriter.State.Content;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0007174F File Offset: 0x0006F94F
		private void VerifyState(DocumentXmlWriter.Method method)
		{
			this.state = DocumentXmlWriter.changeState[(int)(method * DocumentXmlWriter.Method.WriteEndElement + (int)this.state)];
			if (this.state == DocumentXmlWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
		}

		// Token: 0x04000D4E RID: 3406
		private DocumentXmlWriterType type;

		// Token: 0x04000D4F RID: 3407
		private XmlNode start;

		// Token: 0x04000D50 RID: 3408
		private XmlDocument document;

		// Token: 0x04000D51 RID: 3409
		private XmlNamespaceManager namespaceManager;

		// Token: 0x04000D52 RID: 3410
		private DocumentXmlWriter.State state;

		// Token: 0x04000D53 RID: 3411
		private XmlNode write;

		// Token: 0x04000D54 RID: 3412
		private List<XmlNode> fragment;

		// Token: 0x04000D55 RID: 3413
		private XmlWriterSettings settings;

		// Token: 0x04000D56 RID: 3414
		private DocumentXPathNavigator navigator;

		// Token: 0x04000D57 RID: 3415
		private XmlNode end;

		// Token: 0x04000D58 RID: 3416
		private static DocumentXmlWriter.State[] changeState = new DocumentXmlWriter.State[]
		{
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content
		};

		// Token: 0x02000210 RID: 528
		private enum State
		{
			// Token: 0x04000D5A RID: 3418
			Error,
			// Token: 0x04000D5B RID: 3419
			Attribute,
			// Token: 0x04000D5C RID: 3420
			Prolog,
			// Token: 0x04000D5D RID: 3421
			Fragment,
			// Token: 0x04000D5E RID: 3422
			Content,
			// Token: 0x04000D5F RID: 3423
			Last
		}

		// Token: 0x02000211 RID: 529
		private enum Method
		{
			// Token: 0x04000D61 RID: 3425
			WriteXmlDeclaration,
			// Token: 0x04000D62 RID: 3426
			WriteStartDocument,
			// Token: 0x04000D63 RID: 3427
			WriteEndDocument,
			// Token: 0x04000D64 RID: 3428
			WriteDocType,
			// Token: 0x04000D65 RID: 3429
			WriteStartElement,
			// Token: 0x04000D66 RID: 3430
			WriteEndElement,
			// Token: 0x04000D67 RID: 3431
			WriteFullEndElement,
			// Token: 0x04000D68 RID: 3432
			WriteStartAttribute,
			// Token: 0x04000D69 RID: 3433
			WriteEndAttribute,
			// Token: 0x04000D6A RID: 3434
			WriteStartNamespaceDeclaration,
			// Token: 0x04000D6B RID: 3435
			WriteEndNamespaceDeclaration,
			// Token: 0x04000D6C RID: 3436
			WriteCData,
			// Token: 0x04000D6D RID: 3437
			WriteComment,
			// Token: 0x04000D6E RID: 3438
			WriteProcessingInstruction,
			// Token: 0x04000D6F RID: 3439
			WriteEntityRef,
			// Token: 0x04000D70 RID: 3440
			WriteWhitespace,
			// Token: 0x04000D71 RID: 3441
			WriteString
		}
	}
}
