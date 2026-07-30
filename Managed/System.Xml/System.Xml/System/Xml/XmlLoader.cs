using System;
using System.Collections;
using System.Globalization;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200022B RID: 555
	internal class XmlLoader
	{
		// Token: 0x060014F3 RID: 5363 RVA: 0x00076384 File Offset: 0x00074584
		internal void Load(XmlDocument doc, XmlReader reader, bool preserveWhitespace)
		{
			this.doc = doc;
			if (reader.GetType() == typeof(XmlTextReader))
			{
				this.reader = ((XmlTextReader)reader).Impl;
			}
			else
			{
				this.reader = reader;
			}
			this.preserveWhitespace = preserveWhitespace;
			if (doc == null)
			{
				throw new ArgumentException(Res.GetString("The document to be loaded could not be found."));
			}
			if (reader == null)
			{
				throw new ArgumentException(Res.GetString("There is no reader from which to load the document."));
			}
			doc.SetBaseURI(reader.BaseURI);
			if (reader.Settings != null && reader.Settings.ValidationType == ValidationType.Schema)
			{
				doc.Schemas = reader.Settings.Schemas;
			}
			if (this.reader.ReadState != ReadState.Interactive && !this.reader.Read())
			{
				return;
			}
			this.LoadDocSequence(doc);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0007644C File Offset: 0x0007464C
		private void LoadDocSequence(XmlDocument parentDoc)
		{
			XmlNode xmlNode;
			while ((xmlNode = this.LoadNode(true)) != null)
			{
				parentDoc.AppendChildForLoad(xmlNode, parentDoc);
				if (!this.reader.Read())
				{
					return;
				}
			}
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00076480 File Offset: 0x00074680
		internal XmlNode ReadCurrentNode(XmlDocument doc, XmlReader reader)
		{
			this.doc = doc;
			this.reader = reader;
			this.preserveWhitespace = true;
			if (doc == null)
			{
				throw new ArgumentException(Res.GetString("The document to be loaded could not be found."));
			}
			if (reader == null)
			{
				throw new ArgumentException(Res.GetString("There is no reader from which to load the document."));
			}
			if (reader.ReadState == ReadState.Initial)
			{
				reader.Read();
			}
			if (reader.ReadState == ReadState.Interactive)
			{
				XmlNode xmlNode = this.LoadNode(true);
				if (xmlNode.NodeType != XmlNodeType.Attribute)
				{
					reader.Read();
				}
				return xmlNode;
			}
			return null;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000764FC File Offset: 0x000746FC
		private XmlNode LoadNode(bool skipOverWhitespace)
		{
			XmlReader xmlReader = this.reader;
			XmlNode xmlNode = null;
			for (;;)
			{
				XmlNode xmlNode2;
				switch (xmlReader.NodeType)
				{
				case XmlNodeType.Element:
				{
					bool isEmptyElement = xmlReader.IsEmptyElement;
					XmlElement xmlElement = this.doc.CreateElement(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
					xmlElement.IsEmpty = isEmptyElement;
					if (xmlReader.MoveToFirstAttribute())
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						do
						{
							XmlAttribute xmlAttribute = this.LoadAttributeNode();
							attributes.Append(xmlAttribute);
						}
						while (xmlReader.MoveToNextAttribute());
						xmlReader.MoveToElement();
					}
					if (!isEmptyElement)
					{
						if (xmlNode != null)
						{
							xmlNode.AppendChildForLoad(xmlElement, this.doc);
						}
						xmlNode = xmlElement;
						goto IL_025B;
					}
					IXmlSchemaInfo xmlSchemaInfo = xmlReader.SchemaInfo;
					if (xmlSchemaInfo != null)
					{
						xmlElement.XmlName = this.doc.AddXmlName(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI, xmlSchemaInfo);
					}
					xmlNode2 = xmlElement;
					goto IL_0244;
				}
				case XmlNodeType.Attribute:
					xmlNode2 = this.LoadAttributeNode();
					goto IL_0244;
				case XmlNodeType.Text:
					xmlNode2 = this.doc.CreateTextNode(xmlReader.Value);
					goto IL_0244;
				case XmlNodeType.CDATA:
					xmlNode2 = this.doc.CreateCDataSection(xmlReader.Value);
					goto IL_0244;
				case XmlNodeType.EntityReference:
					xmlNode2 = this.LoadEntityReferenceNode(false);
					goto IL_0244;
				case XmlNodeType.ProcessingInstruction:
					xmlNode2 = this.doc.CreateProcessingInstruction(xmlReader.Name, xmlReader.Value);
					goto IL_0244;
				case XmlNodeType.Comment:
					xmlNode2 = this.doc.CreateComment(xmlReader.Value);
					goto IL_0244;
				case XmlNodeType.DocumentType:
					xmlNode2 = this.LoadDocumentTypeNode();
					goto IL_0244;
				case XmlNodeType.Whitespace:
					if (this.preserveWhitespace)
					{
						xmlNode2 = this.doc.CreateWhitespace(xmlReader.Value);
						goto IL_0244;
					}
					if (xmlNode == null && !skipOverWhitespace)
					{
						goto Block_13;
					}
					goto IL_025B;
				case XmlNodeType.SignificantWhitespace:
					xmlNode2 = this.doc.CreateSignificantWhitespace(xmlReader.Value);
					goto IL_0244;
				case XmlNodeType.EndElement:
				{
					if (xmlNode == null)
					{
						goto Block_7;
					}
					IXmlSchemaInfo xmlSchemaInfo = xmlReader.SchemaInfo;
					if (xmlSchemaInfo != null)
					{
						XmlElement xmlElement = xmlNode as XmlElement;
						if (xmlElement != null)
						{
							xmlElement.XmlName = this.doc.AddXmlName(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI, xmlSchemaInfo);
						}
					}
					if (xmlNode.ParentNode == null)
					{
						return xmlNode;
					}
					xmlNode = xmlNode.ParentNode;
					goto IL_025B;
				}
				case XmlNodeType.EndEntity:
					goto IL_0178;
				case XmlNodeType.XmlDeclaration:
					xmlNode2 = this.LoadDeclarationNode();
					goto IL_0244;
				}
				break;
				IL_025B:
				if (!xmlReader.Read())
				{
					goto Block_15;
				}
				continue;
				IL_0244:
				if (xmlNode != null)
				{
					xmlNode.AppendChildForLoad(xmlNode2, this.doc);
					goto IL_025B;
				}
				return xmlNode2;
			}
			goto IL_0238;
			Block_7:
			return null;
			IL_0178:
			return null;
			Block_13:
			return null;
			IL_0238:
			throw XmlLoader.UnexpectedNodeType(xmlReader.NodeType);
			Block_15:
			if (xmlNode != null)
			{
				while (xmlNode.ParentNode != null)
				{
					xmlNode = xmlNode.ParentNode;
				}
			}
			return xmlNode;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00076784 File Offset: 0x00074984
		private XmlAttribute LoadAttributeNode()
		{
			XmlReader xmlReader = this.reader;
			if (xmlReader.IsDefault)
			{
				return this.LoadDefaultAttribute();
			}
			XmlAttribute xmlAttribute = this.doc.CreateAttribute(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
			IXmlSchemaInfo schemaInfo = xmlReader.SchemaInfo;
			if (schemaInfo != null)
			{
				xmlAttribute.XmlName = this.doc.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, schemaInfo);
			}
			while (xmlReader.ReadAttributeValue())
			{
				XmlNodeType nodeType = xmlReader.NodeType;
				XmlNode xmlNode;
				if (nodeType != XmlNodeType.Text)
				{
					if (nodeType != XmlNodeType.EntityReference)
					{
						throw XmlLoader.UnexpectedNodeType(xmlReader.NodeType);
					}
					xmlNode = this.doc.CreateEntityReference(xmlReader.LocalName);
					if (xmlReader.CanResolveEntity)
					{
						xmlReader.ResolveEntity();
						this.LoadAttributeValue(xmlNode, false);
						if (xmlNode.FirstChild == null)
						{
							xmlNode.AppendChildForLoad(this.doc.CreateTextNode(string.Empty), this.doc);
						}
					}
				}
				else
				{
					xmlNode = this.doc.CreateTextNode(xmlReader.Value);
				}
				xmlAttribute.AppendChildForLoad(xmlNode, this.doc);
			}
			return xmlAttribute;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00076898 File Offset: 0x00074A98
		private XmlAttribute LoadDefaultAttribute()
		{
			XmlReader xmlReader = this.reader;
			XmlAttribute xmlAttribute = this.doc.CreateDefaultAttribute(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
			IXmlSchemaInfo schemaInfo = xmlReader.SchemaInfo;
			if (schemaInfo != null)
			{
				xmlAttribute.XmlName = this.doc.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, schemaInfo);
			}
			this.LoadAttributeValue(xmlAttribute, false);
			XmlUnspecifiedAttribute xmlUnspecifiedAttribute = xmlAttribute as XmlUnspecifiedAttribute;
			if (xmlUnspecifiedAttribute != null)
			{
				xmlUnspecifiedAttribute.SetSpecified(false);
			}
			return xmlAttribute;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00076914 File Offset: 0x00074B14
		private void LoadAttributeValue(XmlNode parent, bool direct)
		{
			XmlReader xmlReader = this.reader;
			while (xmlReader.ReadAttributeValue())
			{
				XmlNodeType nodeType = xmlReader.NodeType;
				XmlNode xmlNode;
				if (nodeType != XmlNodeType.Text)
				{
					if (nodeType != XmlNodeType.EntityReference)
					{
						if (nodeType != XmlNodeType.EndEntity)
						{
							throw XmlLoader.UnexpectedNodeType(xmlReader.NodeType);
						}
						return;
					}
					else
					{
						xmlNode = (direct ? new XmlEntityReference(this.reader.LocalName, this.doc) : this.doc.CreateEntityReference(this.reader.LocalName));
						if (xmlReader.CanResolveEntity)
						{
							xmlReader.ResolveEntity();
							this.LoadAttributeValue(xmlNode, direct);
							if (xmlNode.FirstChild == null)
							{
								xmlNode.AppendChildForLoad(direct ? new XmlText(string.Empty) : this.doc.CreateTextNode(string.Empty), this.doc);
							}
						}
					}
				}
				else
				{
					xmlNode = (direct ? new XmlText(xmlReader.Value, this.doc) : this.doc.CreateTextNode(xmlReader.Value));
				}
				parent.AppendChildForLoad(xmlNode, this.doc);
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00076A18 File Offset: 0x00074C18
		private XmlEntityReference LoadEntityReferenceNode(bool direct)
		{
			XmlEntityReference xmlEntityReference = (direct ? new XmlEntityReference(this.reader.Name, this.doc) : this.doc.CreateEntityReference(this.reader.Name));
			if (this.reader.CanResolveEntity)
			{
				this.reader.ResolveEntity();
				while (this.reader.Read() && this.reader.NodeType != XmlNodeType.EndEntity)
				{
					XmlNode xmlNode = (direct ? this.LoadNodeDirect() : this.LoadNode(false));
					if (xmlNode != null)
					{
						xmlEntityReference.AppendChildForLoad(xmlNode, this.doc);
					}
				}
				if (xmlEntityReference.LastChild == null)
				{
					xmlEntityReference.AppendChildForLoad(this.doc.CreateTextNode(string.Empty), this.doc);
				}
			}
			return xmlEntityReference;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00076AD8 File Offset: 0x00074CD8
		private XmlDeclaration LoadDeclarationNode()
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			while (this.reader.MoveToNextAttribute())
			{
				string name = this.reader.Name;
				if (!(name == "version"))
				{
					if (!(name == "encoding"))
					{
						if (name == "standalone")
						{
							text3 = this.reader.Value;
						}
					}
					else
					{
						text2 = this.reader.Value;
					}
				}
				else
				{
					text = this.reader.Value;
				}
			}
			if (text == null)
			{
				XmlLoader.ParseXmlDeclarationValue(this.reader.Value, out text, out text2, out text3);
			}
			return this.doc.CreateXmlDeclaration(text, text2, text3);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00076B80 File Offset: 0x00074D80
		private XmlDocumentType LoadDocumentTypeNode()
		{
			string text = null;
			string text2 = null;
			string value = this.reader.Value;
			string localName = this.reader.LocalName;
			while (this.reader.MoveToNextAttribute())
			{
				string name = this.reader.Name;
				if (!(name == "PUBLIC"))
				{
					if (name == "SYSTEM")
					{
						text2 = this.reader.Value;
					}
				}
				else
				{
					text = this.reader.Value;
				}
			}
			XmlDocumentType xmlDocumentType = this.doc.CreateDocumentType(localName, text, text2, value);
			IDtdInfo dtdInfo = this.reader.DtdInfo;
			if (dtdInfo != null)
			{
				this.LoadDocumentType(dtdInfo, xmlDocumentType);
			}
			else
			{
				this.ParseDocumentType(xmlDocumentType);
			}
			return xmlDocumentType;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00076C38 File Offset: 0x00074E38
		private XmlNode LoadNodeDirect()
		{
			XmlReader xmlReader = this.reader;
			XmlNode xmlNode = null;
			for (;;)
			{
				XmlNode xmlNode2;
				switch (xmlReader.NodeType)
				{
				case XmlNodeType.Element:
				{
					bool isEmptyElement = this.reader.IsEmptyElement;
					XmlElement xmlElement = new XmlElement(this.reader.Prefix, this.reader.LocalName, this.reader.NamespaceURI, this.doc);
					xmlElement.IsEmpty = isEmptyElement;
					if (this.reader.MoveToFirstAttribute())
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						do
						{
							XmlAttribute xmlAttribute = this.LoadAttributeNodeDirect();
							attributes.Append(xmlAttribute);
						}
						while (xmlReader.MoveToNextAttribute());
					}
					if (!isEmptyElement)
					{
						xmlNode.AppendChildForLoad(xmlElement, this.doc);
						xmlNode = xmlElement;
						goto IL_01FD;
					}
					xmlNode2 = xmlElement;
					goto IL_01E8;
				}
				case XmlNodeType.Attribute:
					xmlNode2 = this.LoadAttributeNodeDirect();
					goto IL_01E8;
				case XmlNodeType.Text:
					xmlNode2 = new XmlText(this.reader.Value, this.doc);
					goto IL_01E8;
				case XmlNodeType.CDATA:
					xmlNode2 = new XmlCDataSection(this.reader.Value, this.doc);
					goto IL_01E8;
				case XmlNodeType.EntityReference:
					xmlNode2 = this.LoadEntityReferenceNode(true);
					goto IL_01E8;
				case XmlNodeType.ProcessingInstruction:
					xmlNode2 = new XmlProcessingInstruction(this.reader.Name, this.reader.Value, this.doc);
					goto IL_01E8;
				case XmlNodeType.Comment:
					xmlNode2 = new XmlComment(this.reader.Value, this.doc);
					goto IL_01E8;
				case XmlNodeType.Whitespace:
					if (this.preserveWhitespace)
					{
						xmlNode2 = new XmlWhitespace(this.reader.Value, this.doc);
						goto IL_01E8;
					}
					goto IL_01FD;
				case XmlNodeType.SignificantWhitespace:
					xmlNode2 = new XmlSignificantWhitespace(this.reader.Value, this.doc);
					goto IL_01E8;
				case XmlNodeType.EndElement:
					if (xmlNode.ParentNode == null)
					{
						return xmlNode;
					}
					xmlNode = xmlNode.ParentNode;
					goto IL_01FD;
				case XmlNodeType.EndEntity:
					goto IL_01FD;
				}
				break;
				IL_01FD:
				if (!xmlReader.Read())
				{
					goto Block_7;
				}
				continue;
				IL_01E8:
				if (xmlNode != null)
				{
					xmlNode.AppendChildForLoad(xmlNode2, this.doc);
					goto IL_01FD;
				}
				return xmlNode2;
			}
			throw XmlLoader.UnexpectedNodeType(this.reader.NodeType);
			Block_7:
			return null;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00076E50 File Offset: 0x00075050
		private XmlAttribute LoadAttributeNodeDirect()
		{
			XmlReader xmlReader = this.reader;
			if (xmlReader.IsDefault)
			{
				XmlUnspecifiedAttribute xmlUnspecifiedAttribute = new XmlUnspecifiedAttribute(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI, this.doc);
				this.LoadAttributeValue(xmlUnspecifiedAttribute, true);
				xmlUnspecifiedAttribute.SetSpecified(false);
				return xmlUnspecifiedAttribute;
			}
			XmlAttribute xmlAttribute = new XmlAttribute(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI, this.doc);
			this.LoadAttributeValue(xmlAttribute, true);
			return xmlAttribute;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00076EC4 File Offset: 0x000750C4
		internal void ParseDocumentType(XmlDocumentType dtNode)
		{
			XmlDocument ownerDocument = dtNode.OwnerDocument;
			if (ownerDocument.HasSetResolver)
			{
				this.ParseDocumentType(dtNode, true, ownerDocument.GetResolver());
				return;
			}
			this.ParseDocumentType(dtNode, false, null);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00076EF8 File Offset: 0x000750F8
		private void ParseDocumentType(XmlDocumentType dtNode, bool bUseResolver, XmlResolver resolver)
		{
			this.doc = dtNode.OwnerDocument;
			XmlParserContext xmlParserContext = new XmlParserContext(null, new XmlNamespaceManager(this.doc.NameTable), null, null, null, null, this.doc.BaseURI, string.Empty, XmlSpace.None);
			XmlTextReaderImpl xmlTextReaderImpl = new XmlTextReaderImpl("", XmlNodeType.Element, xmlParserContext);
			xmlTextReaderImpl.Namespaces = dtNode.ParseWithNamespaces;
			if (bUseResolver)
			{
				xmlTextReaderImpl.XmlResolver = resolver;
			}
			IDtdParser dtdParser = DtdParser.Create();
			XmlTextReaderImpl.DtdParserProxy dtdParserProxy = new XmlTextReaderImpl.DtdParserProxy(xmlTextReaderImpl);
			IDtdInfo dtdInfo = dtdParser.ParseFreeFloatingDtd(this.doc.BaseURI, dtNode.Name, dtNode.PublicId, dtNode.SystemId, dtNode.InternalSubset, dtdParserProxy);
			this.LoadDocumentType(dtdInfo, dtNode);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00076FA0 File Offset: 0x000751A0
		private void LoadDocumentType(IDtdInfo dtdInfo, XmlDocumentType dtNode)
		{
			SchemaInfo schemaInfo = dtdInfo as SchemaInfo;
			if (schemaInfo == null)
			{
				throw new XmlException("An internal error has occurred.", string.Empty);
			}
			dtNode.DtdSchemaInfo = schemaInfo;
			if (schemaInfo != null)
			{
				this.doc.DtdSchemaInfo = schemaInfo;
				if (schemaInfo.Notations != null)
				{
					foreach (SchemaNotation schemaNotation in schemaInfo.Notations.Values)
					{
						dtNode.Notations.SetNamedItem(new XmlNotation(schemaNotation.Name.Name, schemaNotation.Pubid, schemaNotation.SystemLiteral, this.doc));
					}
				}
				if (schemaInfo.GeneralEntities != null)
				{
					foreach (SchemaEntity schemaEntity in schemaInfo.GeneralEntities.Values)
					{
						XmlEntity xmlEntity = new XmlEntity(schemaEntity.Name.Name, schemaEntity.Text, schemaEntity.Pubid, schemaEntity.Url, schemaEntity.NData.IsEmpty ? null : schemaEntity.NData.Name, this.doc);
						xmlEntity.SetBaseURI(schemaEntity.DeclaredURI);
						dtNode.Entities.SetNamedItem(xmlEntity);
					}
				}
				if (schemaInfo.ParameterEntities != null)
				{
					foreach (SchemaEntity schemaEntity2 in schemaInfo.ParameterEntities.Values)
					{
						XmlEntity xmlEntity2 = new XmlEntity(schemaEntity2.Name.Name, schemaEntity2.Text, schemaEntity2.Pubid, schemaEntity2.Url, schemaEntity2.NData.IsEmpty ? null : schemaEntity2.NData.Name, this.doc);
						xmlEntity2.SetBaseURI(schemaEntity2.DeclaredURI);
						dtNode.Entities.SetNamedItem(xmlEntity2);
					}
				}
				this.doc.Entities = dtNode.Entities;
				IDictionaryEnumerator dictionaryEnumerator = schemaInfo.ElementDecls.GetEnumerator();
				if (dictionaryEnumerator != null)
				{
					dictionaryEnumerator.Reset();
					while (dictionaryEnumerator.MoveNext())
					{
						SchemaElementDecl schemaElementDecl = (SchemaElementDecl)dictionaryEnumerator.Value;
						if (schemaElementDecl.AttDefs != null)
						{
							IDictionaryEnumerator dictionaryEnumerator2 = schemaElementDecl.AttDefs.GetEnumerator();
							while (dictionaryEnumerator2.MoveNext())
							{
								SchemaAttDef schemaAttDef = (SchemaAttDef)dictionaryEnumerator2.Value;
								if (schemaAttDef.Datatype.TokenizedType == XmlTokenizedType.ID)
								{
									this.doc.AddIdInfo(this.doc.AddXmlName(schemaElementDecl.Prefix, schemaElementDecl.Name.Name, string.Empty, null), this.doc.AddAttrXmlName(schemaAttDef.Prefix, schemaAttDef.Name.Name, string.Empty, null));
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x000772AC File Offset: 0x000754AC
		private XmlParserContext GetContext(XmlNode node)
		{
			string text = null;
			XmlSpace xmlSpace = XmlSpace.None;
			XmlDocumentType documentType = this.doc.DocumentType;
			string baseURI = this.doc.BaseURI;
			Hashtable hashtable = new Hashtable();
			XmlNameTable nameTable = this.doc.NameTable;
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(nameTable);
			bool flag = false;
			while (node != null && node != this.doc)
			{
				if (node is XmlElement && ((XmlElement)node).HasAttributes)
				{
					xmlNamespaceManager.PushScope();
					foreach (object obj in ((XmlElement)node).Attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj;
						if (xmlAttribute.Prefix == this.doc.strXmlns && !hashtable.Contains(xmlAttribute.LocalName))
						{
							hashtable.Add(xmlAttribute.LocalName, xmlAttribute.LocalName);
							xmlNamespaceManager.AddNamespace(xmlAttribute.LocalName, xmlAttribute.Value);
						}
						else if (!flag && xmlAttribute.Prefix.Length == 0 && xmlAttribute.LocalName == this.doc.strXmlns)
						{
							xmlNamespaceManager.AddNamespace(string.Empty, xmlAttribute.Value);
							flag = true;
						}
						else if (xmlSpace == XmlSpace.None && xmlAttribute.Prefix == this.doc.strXml && xmlAttribute.LocalName == this.doc.strSpace)
						{
							if (xmlAttribute.Value == "default")
							{
								xmlSpace = XmlSpace.Default;
							}
							else if (xmlAttribute.Value == "preserve")
							{
								xmlSpace = XmlSpace.Preserve;
							}
						}
						else if (text == null && xmlAttribute.Prefix == this.doc.strXml && xmlAttribute.LocalName == this.doc.strLang)
						{
							text = xmlAttribute.Value;
						}
					}
				}
				node = node.ParentNode;
			}
			return new XmlParserContext(nameTable, xmlNamespaceManager, (documentType == null) ? null : documentType.Name, (documentType == null) ? null : documentType.PublicId, (documentType == null) ? null : documentType.SystemId, (documentType == null) ? null : documentType.InternalSubset, baseURI, text, xmlSpace);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00077514 File Offset: 0x00075714
		internal XmlNamespaceManager ParsePartialContent(XmlNode parentNode, string innerxmltext, XmlNodeType nt)
		{
			this.doc = parentNode.OwnerDocument;
			XmlParserContext context = this.GetContext(parentNode);
			this.reader = this.CreateInnerXmlReader(innerxmltext, nt, context, this.doc);
			try
			{
				this.preserveWhitespace = true;
				bool isLoading = this.doc.IsLoading;
				this.doc.IsLoading = true;
				if (nt == XmlNodeType.Entity)
				{
					while (this.reader.Read())
					{
						XmlNode xmlNode;
						if ((xmlNode = this.LoadNodeDirect()) == null)
						{
							break;
						}
						parentNode.AppendChildForLoad(xmlNode, this.doc);
					}
				}
				else
				{
					XmlNode xmlNode2;
					while (this.reader.Read() && (xmlNode2 = this.LoadNode(true)) != null)
					{
						parentNode.AppendChildForLoad(xmlNode2, this.doc);
					}
				}
				this.doc.IsLoading = isLoading;
			}
			finally
			{
				this.reader.Close();
			}
			return context.NamespaceManager;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000775F4 File Offset: 0x000757F4
		internal void LoadInnerXmlElement(XmlElement node, string innerxmltext)
		{
			XmlNamespaceManager xmlNamespaceManager = this.ParsePartialContent(node, innerxmltext, XmlNodeType.Element);
			if (node.ChildNodes.Count > 0)
			{
				this.RemoveDuplicateNamespace(node, xmlNamespaceManager, false);
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00077622 File Offset: 0x00075822
		internal void LoadInnerXmlAttribute(XmlAttribute node, string innerxmltext)
		{
			this.ParsePartialContent(node, innerxmltext, XmlNodeType.Attribute);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00077630 File Offset: 0x00075830
		private void RemoveDuplicateNamespace(XmlElement elem, XmlNamespaceManager mgr, bool fCheckElemAttrs)
		{
			mgr.PushScope();
			XmlAttributeCollection attributes = elem.Attributes;
			int count = attributes.Count;
			if (fCheckElemAttrs && count > 0)
			{
				for (int i = count - 1; i >= 0; i--)
				{
					XmlAttribute xmlAttribute = attributes[i];
					if (xmlAttribute.Prefix == this.doc.strXmlns)
					{
						string text = mgr.LookupNamespace(xmlAttribute.LocalName);
						if (text != null)
						{
							if (xmlAttribute.Value == text)
							{
								elem.Attributes.RemoveNodeAt(i);
							}
						}
						else
						{
							mgr.AddNamespace(xmlAttribute.LocalName, xmlAttribute.Value);
						}
					}
					else if (xmlAttribute.Prefix.Length == 0 && xmlAttribute.LocalName == this.doc.strXmlns)
					{
						string defaultNamespace = mgr.DefaultNamespace;
						if (defaultNamespace != null)
						{
							if (xmlAttribute.Value == defaultNamespace)
							{
								elem.Attributes.RemoveNodeAt(i);
							}
						}
						else
						{
							mgr.AddNamespace(xmlAttribute.LocalName, xmlAttribute.Value);
						}
					}
				}
			}
			for (XmlNode xmlNode = elem.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					this.RemoveDuplicateNamespace(xmlElement, mgr, true);
				}
			}
			mgr.PopScope();
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00077775 File Offset: 0x00075975
		private string EntitizeName(string name)
		{
			return "&" + name + ";";
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00077787 File Offset: 0x00075987
		internal void ExpandEntity(XmlEntity ent)
		{
			this.ParsePartialContent(ent, this.EntitizeName(ent.Name), XmlNodeType.Entity);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000777A0 File Offset: 0x000759A0
		internal void ExpandEntityReference(XmlEntityReference eref)
		{
			this.doc = eref.OwnerDocument;
			bool isLoading = this.doc.IsLoading;
			this.doc.IsLoading = true;
			string name = eref.Name;
			if (name == "lt")
			{
				eref.AppendChildForLoad(this.doc.CreateTextNode("<"), this.doc);
				this.doc.IsLoading = isLoading;
				return;
			}
			if (name == "gt")
			{
				eref.AppendChildForLoad(this.doc.CreateTextNode(">"), this.doc);
				this.doc.IsLoading = isLoading;
				return;
			}
			if (name == "amp")
			{
				eref.AppendChildForLoad(this.doc.CreateTextNode("&"), this.doc);
				this.doc.IsLoading = isLoading;
				return;
			}
			if (name == "apos")
			{
				eref.AppendChildForLoad(this.doc.CreateTextNode("'"), this.doc);
				this.doc.IsLoading = isLoading;
				return;
			}
			if (!(name == "quot"))
			{
				using (IEnumerator enumerator = this.doc.Entities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (Ref.Equal(((XmlEntity)enumerator.Current).Name, eref.Name))
						{
							this.ParsePartialContent(eref, this.EntitizeName(eref.Name), XmlNodeType.EntityReference);
							return;
						}
					}
				}
				if (!this.doc.ActualLoadingStatus)
				{
					eref.AppendChildForLoad(this.doc.CreateTextNode(""), this.doc);
					this.doc.IsLoading = isLoading;
					return;
				}
				this.doc.IsLoading = isLoading;
				throw new XmlException("Reference to undeclared parameter entity '{0}'.", eref.Name);
			}
			eref.AppendChildForLoad(this.doc.CreateTextNode("\""), this.doc);
			this.doc.IsLoading = isLoading;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000779BC File Offset: 0x00075BBC
		private XmlReader CreateInnerXmlReader(string xmlFragment, XmlNodeType nt, XmlParserContext context, XmlDocument doc)
		{
			XmlNodeType xmlNodeType = nt;
			if (xmlNodeType == XmlNodeType.Entity || xmlNodeType == XmlNodeType.EntityReference)
			{
				xmlNodeType = XmlNodeType.Element;
			}
			XmlTextReaderImpl xmlTextReaderImpl = new XmlTextReaderImpl(xmlFragment, xmlNodeType, context);
			xmlTextReaderImpl.XmlValidatingReaderCompatibilityMode = true;
			if (doc.HasSetResolver)
			{
				xmlTextReaderImpl.XmlResolver = doc.GetResolver();
			}
			if (!doc.ActualLoadingStatus)
			{
				xmlTextReaderImpl.DisableUndeclaredEntityCheck = true;
			}
			XmlDocumentType documentType = doc.DocumentType;
			if (documentType != null)
			{
				xmlTextReaderImpl.Namespaces = documentType.ParseWithNamespaces;
				if (documentType.DtdSchemaInfo != null)
				{
					xmlTextReaderImpl.SetDtdInfo(documentType.DtdSchemaInfo);
				}
				else
				{
					IDtdParser dtdParser = DtdParser.Create();
					XmlTextReaderImpl.DtdParserProxy dtdParserProxy = new XmlTextReaderImpl.DtdParserProxy(xmlTextReaderImpl);
					IDtdInfo dtdInfo = dtdParser.ParseFreeFloatingDtd(context.BaseURI, context.DocTypeName, context.PublicId, context.SystemId, context.InternalSubset, dtdParserProxy);
					documentType.DtdSchemaInfo = dtdInfo as SchemaInfo;
					xmlTextReaderImpl.SetDtdInfo(dtdInfo);
				}
			}
			if (nt == XmlNodeType.Entity || nt == XmlNodeType.EntityReference)
			{
				xmlTextReaderImpl.Read();
				xmlTextReaderImpl.ResolveEntity();
			}
			return xmlTextReaderImpl;
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00077A98 File Offset: 0x00075C98
		internal static void ParseXmlDeclarationValue(string strValue, out string version, out string encoding, out string standalone)
		{
			version = null;
			encoding = null;
			standalone = null;
			XmlTextReaderImpl xmlTextReaderImpl = new XmlTextReaderImpl(strValue, null);
			try
			{
				xmlTextReaderImpl.Read();
				if (xmlTextReaderImpl.MoveToAttribute("version"))
				{
					version = xmlTextReaderImpl.Value;
				}
				if (xmlTextReaderImpl.MoveToAttribute("encoding"))
				{
					encoding = xmlTextReaderImpl.Value;
				}
				if (xmlTextReaderImpl.MoveToAttribute("standalone"))
				{
					standalone = xmlTextReaderImpl.Value;
				}
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00077B18 File Offset: 0x00075D18
		internal static Exception UnexpectedNodeType(XmlNodeType nodetype)
		{
			return new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Res.GetString("Unexpected XmlNodeType: '{0}'."), nodetype.ToString()));
		}

		// Token: 0x04000DE6 RID: 3558
		private XmlDocument doc;

		// Token: 0x04000DE7 RID: 3559
		private XmlReader reader;

		// Token: 0x04000DE8 RID: 3560
		private bool preserveWhitespace;
	}
}
