using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000204 RID: 516
	internal sealed class DocumentSchemaValidator : IXmlNamespaceResolver
	{
		// Token: 0x0600125F RID: 4703 RVA: 0x0006D994 File Offset: 0x0006BB94
		public DocumentSchemaValidator(XmlDocument ownerDocument, XmlSchemaSet schemas, ValidationEventHandler eventHandler)
		{
			this.schemas = schemas;
			this.eventHandler = eventHandler;
			this.document = ownerDocument;
			this.internalEventHandler = new ValidationEventHandler(this.InternalValidationCallBack);
			this.nameTable = this.document.NameTable;
			this.nsManager = new XmlNamespaceManager(this.nameTable);
			this.nodeValueGetter = new XmlValueGetter(this.GetNodeValue);
			this.psviAugmentation = true;
			this.NsXmlNs = this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXsi = this.nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = this.nameTable.Add("type");
			this.XsiNil = this.nameTable.Add("nil");
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0006DA61 File Offset: 0x0006BC61
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x0006DA69 File Offset: 0x0006BC69
		public bool PsviAugmentation
		{
			get
			{
				return this.psviAugmentation;
			}
			set
			{
				this.psviAugmentation = value;
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0006DA74 File Offset: 0x0006BC74
		public bool Validate(XmlNode nodeToValidate)
		{
			XmlSchemaObject xmlSchemaObject = null;
			XmlSchemaValidationFlags xmlSchemaValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes;
			this.startNode = nodeToValidate;
			XmlNodeType nodeType = nodeToValidate.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.Attribute)
					{
						if (nodeToValidate.XPNodeType != XPathNodeType.Namespace)
						{
							xmlSchemaObject = nodeToValidate.SchemaInfo.SchemaAttribute;
							if (xmlSchemaObject != null)
							{
								goto IL_010E;
							}
							xmlSchemaObject = this.FindSchemaInfo(nodeToValidate as XmlAttribute);
							if (xmlSchemaObject == null)
							{
								throw new XmlSchemaValidationException("Schema information could not be found for the node passed into Validate. The node may be invalid in its current position. Navigate to the ancestor that has schema information, then call Validate again.", null, nodeToValidate);
							}
							goto IL_010E;
						}
					}
				}
				else
				{
					IXmlSchemaInfo xmlSchemaInfo = nodeToValidate.SchemaInfo;
					XmlSchemaElement schemaElement = xmlSchemaInfo.SchemaElement;
					if (schemaElement != null)
					{
						if (!schemaElement.RefName.IsEmpty)
						{
							xmlSchemaObject = this.schemas.GlobalElements[schemaElement.QualifiedName];
							goto IL_010E;
						}
						xmlSchemaObject = schemaElement;
						goto IL_010E;
					}
					else
					{
						xmlSchemaObject = xmlSchemaInfo.SchemaType;
						if (xmlSchemaObject != null)
						{
							goto IL_010E;
						}
						if (nodeToValidate.ParentNode.NodeType == XmlNodeType.Document)
						{
							nodeToValidate = nodeToValidate.ParentNode;
							goto IL_010E;
						}
						xmlSchemaObject = this.FindSchemaInfo(nodeToValidate as XmlElement);
						if (xmlSchemaObject == null)
						{
							throw new XmlSchemaValidationException("Schema information could not be found for the node passed into Validate. The node may be invalid in its current position. Navigate to the ancestor that has schema information, then call Validate again.", null, nodeToValidate);
						}
						goto IL_010E;
					}
				}
			}
			else
			{
				if (nodeType == XmlNodeType.Document)
				{
					xmlSchemaValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
					goto IL_010E;
				}
				if (nodeType == XmlNodeType.DocumentFragment)
				{
					goto IL_010E;
				}
			}
			throw new InvalidOperationException(Res.GetString("Validate method can be called only on nodes of type Document, DocumentFragment, Element, or Attribute.", null));
			IL_010E:
			this.isValid = true;
			this.CreateValidator(xmlSchemaObject, xmlSchemaValidationFlags);
			if (this.psviAugmentation)
			{
				if (this.schemaInfo == null)
				{
					this.schemaInfo = new XmlSchemaInfo();
				}
				this.attributeSchemaInfo = new XmlSchemaInfo();
			}
			this.ValidateNode(nodeToValidate);
			this.validator.EndValidation();
			return this.isValid;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0006DBDC File Offset: 0x0006BDDC
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IDictionary<string, string> namespacesInScope = this.nsManager.GetNamespacesInScope(scope);
			if (scope != XmlNamespaceScope.Local)
			{
				XmlNode xmlNode = this.startNode;
				while (xmlNode != null)
				{
					XmlNodeType nodeType = xmlNode.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.Attribute)
						{
							xmlNode = xmlNode.ParentNode;
						}
						else
						{
							xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
						}
					}
					else
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement.HasAttributes)
						{
							XmlAttributeCollection attributes = xmlElement.Attributes;
							for (int i = 0; i < attributes.Count; i++)
							{
								XmlAttribute xmlAttribute = attributes[i];
								if (Ref.Equal(xmlAttribute.NamespaceURI, this.document.strReservedXmlns))
								{
									if (xmlAttribute.Prefix.Length == 0)
									{
										if (!namespacesInScope.ContainsKey(string.Empty))
										{
											namespacesInScope.Add(string.Empty, xmlAttribute.Value);
										}
									}
									else if (!namespacesInScope.ContainsKey(xmlAttribute.LocalName))
									{
										namespacesInScope.Add(xmlAttribute.LocalName, xmlAttribute.Value);
									}
								}
							}
						}
						xmlNode = xmlNode.ParentNode;
					}
				}
			}
			return namespacesInScope;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0006DCF0 File Offset: 0x0006BEF0
		public string LookupNamespace(string prefix)
		{
			string text = this.nsManager.LookupNamespace(prefix);
			if (text == null)
			{
				text = this.startNode.GetNamespaceOfPrefixStrict(prefix);
			}
			return text;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0006DD1C File Offset: 0x0006BF1C
		public string LookupPrefix(string namespaceName)
		{
			string text = this.nsManager.LookupPrefix(namespaceName);
			if (text == null)
			{
				text = this.startNode.GetPrefixOfNamespaceStrict(namespaceName);
			}
			return text;
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0006DD47 File Offset: 0x0006BF47
		private IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				if (this.startNode == this.document)
				{
					return this.nsManager;
				}
				return this;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0006DD60 File Offset: 0x0006BF60
		private void CreateValidator(XmlSchemaObject partialValidationType, XmlSchemaValidationFlags validationFlags)
		{
			this.validator = new XmlSchemaValidator(this.nameTable, this.schemas, this.NamespaceResolver, validationFlags);
			this.validator.SourceUri = XmlConvert.ToUri(this.document.BaseURI);
			this.validator.XmlResolver = null;
			this.validator.ValidationEventHandler += this.internalEventHandler;
			this.validator.ValidationEventSender = this;
			if (partialValidationType != null)
			{
				this.validator.Initialize(partialValidationType);
				return;
			}
			this.validator.Initialize();
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0006DDEC File Offset: 0x0006BFEC
		private void ValidateNode(XmlNode node)
		{
			this.currentNode = node;
			switch (this.currentNode.NodeType)
			{
			case XmlNodeType.Element:
				this.ValidateElement();
				return;
			case XmlNodeType.Attribute:
			{
				XmlAttribute xmlAttribute = this.currentNode as XmlAttribute;
				this.validator.ValidateAttribute(xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.nodeValueGetter, this.attributeSchemaInfo);
				if (this.psviAugmentation)
				{
					xmlAttribute.XmlName = this.document.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.attributeSchemaInfo);
					return;
				}
				return;
			}
			case XmlNodeType.Text:
				this.validator.ValidateText(this.nodeValueGetter);
				return;
			case XmlNodeType.CDATA:
				this.validator.ValidateText(this.nodeValueGetter);
				return;
			case XmlNodeType.EntityReference:
			case XmlNodeType.DocumentFragment:
			{
				for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					this.ValidateNode(xmlNode);
				}
				return;
			}
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
				return;
			case XmlNodeType.Document:
			{
				XmlElement documentElement = ((XmlDocument)node).DocumentElement;
				if (documentElement == null)
				{
					throw new InvalidOperationException(Res.GetString("Invalid XML document. {0}", new object[] { Res.GetString("The document does not have a root element.") }));
				}
				this.ValidateNode(documentElement);
				return;
			}
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(this.nodeValueGetter);
				return;
			}
			throw new InvalidOperationException(Res.GetString("Unexpected XmlNodeType: '{0}'.", new string[] { this.currentNode.NodeType.ToString() }));
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0006DF7C File Offset: 0x0006C17C
		private void ValidateElement()
		{
			this.nsManager.PushScope();
			XmlElement xmlElement = this.currentNode as XmlElement;
			XmlAttributeCollection attributes = xmlElement.Attributes;
			string text = null;
			string text2 = null;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = attributes[i];
				string namespaceURI = xmlAttribute.NamespaceURI;
				string localName = xmlAttribute.LocalName;
				if (Ref.Equal(namespaceURI, this.NsXsi))
				{
					if (Ref.Equal(localName, this.XsiType))
					{
						text2 = xmlAttribute.Value;
					}
					else if (Ref.Equal(localName, this.XsiNil))
					{
						text = xmlAttribute.Value;
					}
				}
				else if (Ref.Equal(namespaceURI, this.NsXmlNs))
				{
					this.nsManager.AddNamespace((xmlAttribute.Prefix.Length == 0) ? string.Empty : xmlAttribute.LocalName, xmlAttribute.Value);
				}
			}
			this.validator.ValidateElement(xmlElement.LocalName, xmlElement.NamespaceURI, this.schemaInfo, text2, text, null, null);
			this.ValidateAttributes(xmlElement);
			this.validator.ValidateEndOfAttributes(this.schemaInfo);
			for (XmlNode xmlNode = xmlElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.ValidateNode(xmlNode);
			}
			this.currentNode = xmlElement;
			this.validator.ValidateEndElement(this.schemaInfo);
			if (this.psviAugmentation)
			{
				xmlElement.XmlName = this.document.AddXmlName(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI, this.schemaInfo);
				if (this.schemaInfo.IsDefault)
				{
					XmlText xmlText = this.document.CreateTextNode(this.schemaInfo.SchemaElement.ElementDecl.DefaultValueRaw);
					xmlElement.AppendChild(xmlText);
				}
			}
			this.nsManager.PopScope();
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0006E144 File Offset: 0x0006C344
		private void ValidateAttributes(XmlElement elementNode)
		{
			XmlAttributeCollection attributes = elementNode.Attributes;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = attributes[i];
				this.currentNode = xmlAttribute;
				if (!Ref.Equal(xmlAttribute.NamespaceURI, this.NsXmlNs))
				{
					this.validator.ValidateAttribute(xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.nodeValueGetter, this.attributeSchemaInfo);
					if (this.psviAugmentation)
					{
						xmlAttribute.XmlName = this.document.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.attributeSchemaInfo);
					}
				}
			}
			if (this.psviAugmentation)
			{
				if (this.defaultAttributes == null)
				{
					this.defaultAttributes = new ArrayList();
				}
				else
				{
					this.defaultAttributes.Clear();
				}
				this.validator.GetUnspecifiedDefaultAttributes(this.defaultAttributes);
				for (int j = 0; j < this.defaultAttributes.Count; j++)
				{
					XmlSchemaAttribute xmlSchemaAttribute = this.defaultAttributes[j] as XmlSchemaAttribute;
					XmlQualifiedName qualifiedName = xmlSchemaAttribute.QualifiedName;
					XmlAttribute xmlAttribute = this.document.CreateDefaultAttribute(this.GetDefaultPrefix(qualifiedName.Namespace), qualifiedName.Name, qualifiedName.Namespace);
					this.SetDefaultAttributeSchemaInfo(xmlSchemaAttribute);
					xmlAttribute.XmlName = this.document.AddAttrXmlName(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, this.attributeSchemaInfo);
					xmlAttribute.AppendChild(this.document.CreateTextNode(xmlSchemaAttribute.AttDef.DefaultValueRaw));
					attributes.Append(xmlAttribute);
					XmlUnspecifiedAttribute xmlUnspecifiedAttribute = xmlAttribute as XmlUnspecifiedAttribute;
					if (xmlUnspecifiedAttribute != null)
					{
						xmlUnspecifiedAttribute.SetSpecified(false);
					}
				}
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0006E2F0 File Offset: 0x0006C4F0
		private void SetDefaultAttributeSchemaInfo(XmlSchemaAttribute schemaAttribute)
		{
			this.attributeSchemaInfo.Clear();
			this.attributeSchemaInfo.IsDefault = true;
			this.attributeSchemaInfo.IsNil = false;
			this.attributeSchemaInfo.SchemaType = schemaAttribute.AttributeSchemaType;
			this.attributeSchemaInfo.SchemaAttribute = schemaAttribute;
			SchemaAttDef attDef = schemaAttribute.AttDef;
			if (attDef.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
			{
				XsdSimpleValue xsdSimpleValue = attDef.DefaultValueTyped as XsdSimpleValue;
				this.attributeSchemaInfo.MemberType = xsdSimpleValue.XmlType;
			}
			this.attributeSchemaInfo.Validity = XmlSchemaValidity.Valid;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0006E37C File Offset: 0x0006C57C
		private string GetDefaultPrefix(string attributeNS)
		{
			IEnumerable<KeyValuePair<string, string>> namespacesInScope = this.NamespaceResolver.GetNamespacesInScope(XmlNamespaceScope.All);
			string text = null;
			attributeNS = this.nameTable.Add(attributeNS);
			foreach (KeyValuePair<string, string> keyValuePair in namespacesInScope)
			{
				if (this.nameTable.Add(keyValuePair.Value) == attributeNS)
				{
					text = keyValuePair.Key;
					if (text.Length != 0)
					{
						return text;
					}
				}
			}
			return text;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0006E404 File Offset: 0x0006C604
		private object GetNodeValue()
		{
			return this.currentNode.Value;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0006E414 File Offset: 0x0006C614
		private XmlSchemaObject FindSchemaInfo(XmlElement elementToValidate)
		{
			this.isPartialTreeValid = true;
			int num = 0;
			XmlNode xmlNode = elementToValidate.ParentNode;
			IXmlSchemaInfo xmlSchemaInfo;
			do
			{
				xmlSchemaInfo = xmlNode.SchemaInfo;
				if (xmlSchemaInfo.SchemaElement != null || xmlSchemaInfo.SchemaType != null)
				{
					break;
				}
				this.CheckNodeSequenceCapacity(num);
				this.nodeSequenceToValidate[num++] = xmlNode;
				xmlNode = xmlNode.ParentNode;
			}
			while (xmlNode != null);
			if (xmlNode == null)
			{
				num--;
				this.nodeSequenceToValidate[num] = null;
				return this.GetTypeFromAncestors(elementToValidate, null, num);
			}
			this.CheckNodeSequenceCapacity(num);
			this.nodeSequenceToValidate[num++] = xmlNode;
			XmlSchemaObject xmlSchemaObject = xmlSchemaInfo.SchemaElement;
			if (xmlSchemaObject == null)
			{
				xmlSchemaObject = xmlSchemaInfo.SchemaType;
			}
			return this.GetTypeFromAncestors(elementToValidate, xmlSchemaObject, num);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0006E4B0 File Offset: 0x0006C6B0
		private void CheckNodeSequenceCapacity(int currentIndex)
		{
			if (this.nodeSequenceToValidate == null)
			{
				this.nodeSequenceToValidate = new XmlNode[4];
				return;
			}
			if (currentIndex >= this.nodeSequenceToValidate.Length - 1)
			{
				XmlNode[] array = new XmlNode[this.nodeSequenceToValidate.Length * 2];
				Array.Copy(this.nodeSequenceToValidate, 0, array, 0, this.nodeSequenceToValidate.Length);
				this.nodeSequenceToValidate = array;
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0006E50C File Offset: 0x0006C70C
		private XmlSchemaAttribute FindSchemaInfo(XmlAttribute attributeToValidate)
		{
			XmlElement ownerElement = attributeToValidate.OwnerElement;
			XmlSchemaObject xmlSchemaObject = this.FindSchemaInfo(ownerElement);
			XmlSchemaComplexType complexType = this.GetComplexType(xmlSchemaObject);
			if (complexType == null)
			{
				return null;
			}
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(attributeToValidate.LocalName, attributeToValidate.NamespaceURI);
			XmlSchemaAttribute xmlSchemaAttribute = complexType.AttributeUses[xmlQualifiedName] as XmlSchemaAttribute;
			if (xmlSchemaAttribute == null)
			{
				XmlSchemaAnyAttribute attributeWildcard = complexType.AttributeWildcard;
				if (attributeWildcard != null && attributeWildcard.NamespaceList.Allows(xmlQualifiedName))
				{
					xmlSchemaAttribute = this.schemas.GlobalAttributes[xmlQualifiedName] as XmlSchemaAttribute;
				}
			}
			return xmlSchemaAttribute;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0006E594 File Offset: 0x0006C794
		private XmlSchemaObject GetTypeFromAncestors(XmlElement elementToValidate, XmlSchemaObject ancestorType, int ancestorsCount)
		{
			this.validator = this.CreateTypeFinderValidator(ancestorType);
			this.schemaInfo = new XmlSchemaInfo();
			int num = ancestorsCount - 1;
			bool flag = this.AncestorTypeHasWildcard(ancestorType);
			for (int i = num; i >= 0; i--)
			{
				XmlNode xmlNode = this.nodeSequenceToValidate[i];
				XmlElement xmlElement = xmlNode as XmlElement;
				this.ValidateSingleElement(xmlElement, false, this.schemaInfo);
				if (!flag)
				{
					xmlElement.XmlName = this.document.AddXmlName(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI, this.schemaInfo);
					flag = this.AncestorTypeHasWildcard(this.schemaInfo.SchemaElement);
				}
				this.validator.ValidateEndOfAttributes(null);
				if (i > 0)
				{
					this.ValidateChildrenTillNextAncestor(xmlNode, this.nodeSequenceToValidate[i - 1]);
				}
				else
				{
					this.ValidateChildrenTillNextAncestor(xmlNode, elementToValidate);
				}
			}
			this.ValidateSingleElement(elementToValidate, false, this.schemaInfo);
			XmlSchemaObject xmlSchemaObject;
			if (this.schemaInfo.SchemaElement != null)
			{
				xmlSchemaObject = this.schemaInfo.SchemaElement;
			}
			else
			{
				xmlSchemaObject = this.schemaInfo.SchemaType;
			}
			if (xmlSchemaObject == null)
			{
				if (this.validator.CurrentProcessContents == XmlSchemaContentProcessing.Skip)
				{
					if (this.isPartialTreeValid)
					{
						return XmlSchemaComplexType.AnyTypeSkip;
					}
				}
				else if (this.validator.CurrentProcessContents == XmlSchemaContentProcessing.Lax)
				{
					return XmlSchemaComplexType.AnyType;
				}
			}
			return xmlSchemaObject;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0006E6D0 File Offset: 0x0006C8D0
		private bool AncestorTypeHasWildcard(XmlSchemaObject ancestorType)
		{
			XmlSchemaComplexType complexType = this.GetComplexType(ancestorType);
			return ancestorType != null && complexType.HasWildCard;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0006E6F0 File Offset: 0x0006C8F0
		private XmlSchemaComplexType GetComplexType(XmlSchemaObject schemaObject)
		{
			if (schemaObject == null)
			{
				return null;
			}
			XmlSchemaElement xmlSchemaElement = schemaObject as XmlSchemaElement;
			XmlSchemaComplexType xmlSchemaComplexType;
			if (xmlSchemaElement != null)
			{
				xmlSchemaComplexType = xmlSchemaElement.ElementSchemaType as XmlSchemaComplexType;
			}
			else
			{
				xmlSchemaComplexType = schemaObject as XmlSchemaComplexType;
			}
			return xmlSchemaComplexType;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0006E724 File Offset: 0x0006C924
		private void ValidateSingleElement(XmlElement elementNode, bool skipToEnd, XmlSchemaInfo newSchemaInfo)
		{
			this.nsManager.PushScope();
			XmlAttributeCollection attributes = elementNode.Attributes;
			string text = null;
			string text2 = null;
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = attributes[i];
				string namespaceURI = xmlAttribute.NamespaceURI;
				string localName = xmlAttribute.LocalName;
				if (Ref.Equal(namespaceURI, this.NsXsi))
				{
					if (Ref.Equal(localName, this.XsiType))
					{
						text2 = xmlAttribute.Value;
					}
					else if (Ref.Equal(localName, this.XsiNil))
					{
						text = xmlAttribute.Value;
					}
				}
				else if (Ref.Equal(namespaceURI, this.NsXmlNs))
				{
					this.nsManager.AddNamespace((xmlAttribute.Prefix.Length == 0) ? string.Empty : xmlAttribute.LocalName, xmlAttribute.Value);
				}
			}
			this.validator.ValidateElement(elementNode.LocalName, elementNode.NamespaceURI, newSchemaInfo, text2, text, null, null);
			if (skipToEnd)
			{
				this.validator.ValidateEndOfAttributes(newSchemaInfo);
				this.validator.SkipToEndElement(newSchemaInfo);
				this.nsManager.PopScope();
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0006E83C File Offset: 0x0006CA3C
		private void ValidateChildrenTillNextAncestor(XmlNode parentNode, XmlNode childToStopAt)
		{
			XmlNode xmlNode = parentNode.FirstChild;
			while (xmlNode != null && xmlNode != childToStopAt)
			{
				switch (xmlNode.NodeType)
				{
				case XmlNodeType.Element:
					this.ValidateSingleElement(xmlNode as XmlElement, true, null);
					break;
				case XmlNodeType.Attribute:
				case XmlNodeType.Entity:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentType:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
					goto IL_009C;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					this.validator.ValidateText(xmlNode.Value);
					break;
				case XmlNodeType.EntityReference:
					this.ValidateChildrenTillNextAncestor(xmlNode, childToStopAt);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.validator.ValidateWhitespace(xmlNode.Value);
					break;
				default:
					goto IL_009C;
				}
				xmlNode = xmlNode.NextSibling;
				continue;
				IL_009C:
				throw new InvalidOperationException(Res.GetString("Unexpected XmlNodeType: '{0}'.", new string[] { this.currentNode.NodeType.ToString() }));
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0006E924 File Offset: 0x0006CB24
		private XmlSchemaValidator CreateTypeFinderValidator(XmlSchemaObject partialValidationType)
		{
			XmlSchemaValidator xmlSchemaValidator = new XmlSchemaValidator(this.document.NameTable, this.document.Schemas, this.nsManager, XmlSchemaValidationFlags.None);
			xmlSchemaValidator.ValidationEventHandler += this.TypeFinderCallBack;
			if (partialValidationType != null)
			{
				xmlSchemaValidator.Initialize(partialValidationType);
			}
			else
			{
				xmlSchemaValidator.Initialize();
			}
			return xmlSchemaValidator;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0006E979 File Offset: 0x0006CB79
		private void TypeFinderCallBack(object sender, ValidationEventArgs arg)
		{
			if (arg.Severity == XmlSeverityType.Error)
			{
				this.isPartialTreeValid = false;
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0006E98C File Offset: 0x0006CB8C
		private void InternalValidationCallBack(object sender, ValidationEventArgs arg)
		{
			if (arg.Severity == XmlSeverityType.Error)
			{
				this.isValid = false;
			}
			XmlSchemaValidationException ex = arg.Exception as XmlSchemaValidationException;
			ex.SetSourceObject(this.currentNode);
			if (this.eventHandler != null)
			{
				this.eventHandler(sender, arg);
				return;
			}
			if (arg.Severity == XmlSeverityType.Error)
			{
				throw ex;
			}
		}

		// Token: 0x04000D27 RID: 3367
		private XmlSchemaValidator validator;

		// Token: 0x04000D28 RID: 3368
		private XmlSchemaSet schemas;

		// Token: 0x04000D29 RID: 3369
		private XmlNamespaceManager nsManager;

		// Token: 0x04000D2A RID: 3370
		private XmlNameTable nameTable;

		// Token: 0x04000D2B RID: 3371
		private ArrayList defaultAttributes;

		// Token: 0x04000D2C RID: 3372
		private XmlValueGetter nodeValueGetter;

		// Token: 0x04000D2D RID: 3373
		private XmlSchemaInfo attributeSchemaInfo;

		// Token: 0x04000D2E RID: 3374
		private XmlSchemaInfo schemaInfo;

		// Token: 0x04000D2F RID: 3375
		private ValidationEventHandler eventHandler;

		// Token: 0x04000D30 RID: 3376
		private ValidationEventHandler internalEventHandler;

		// Token: 0x04000D31 RID: 3377
		private XmlNode startNode;

		// Token: 0x04000D32 RID: 3378
		private XmlNode currentNode;

		// Token: 0x04000D33 RID: 3379
		private XmlDocument document;

		// Token: 0x04000D34 RID: 3380
		private XmlNode[] nodeSequenceToValidate;

		// Token: 0x04000D35 RID: 3381
		private bool isPartialTreeValid;

		// Token: 0x04000D36 RID: 3382
		private bool psviAugmentation;

		// Token: 0x04000D37 RID: 3383
		private bool isValid;

		// Token: 0x04000D38 RID: 3384
		private string NsXmlNs;

		// Token: 0x04000D39 RID: 3385
		private string NsXsi;

		// Token: 0x04000D3A RID: 3386
		private string XsiType;

		// Token: 0x04000D3B RID: 3387
		private string XsiNil;
	}
}
