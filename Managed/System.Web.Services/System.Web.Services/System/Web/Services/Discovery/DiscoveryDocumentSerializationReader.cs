using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000A9 RID: 169
	internal class DiscoveryDocumentSerializationReader : XmlSerializationReader
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x000145B8 File Offset: 0x000127B8
		public object Read10_discovery()
		{
			object obj = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_discovery || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				obj = this.Read9_DiscoveryDocument(true, true);
			}
			else
			{
				base.UnknownNode(null, "http://schemas.xmlsoap.org/disco/:discovery");
			}
			return obj;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00014628 File Offset: 0x00012828
		private DiscoveryDocument Read9_DiscoveryDocument(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = (checkType ? base.GetXsiType() : null);
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_DiscoveryDocument || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			DiscoveryDocument discoveryDocument = new DiscoveryDocument();
			IList references = discoveryDocument.References;
			new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(discoveryDocument);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return discoveryDocument;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id4_discoveryRef && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (references == null)
						{
							base.Reader.Skip();
						}
						else
						{
							references.Add(this.Read3_DiscoveryDocumentReference(false, true));
						}
					}
					else if (base.Reader.LocalName == this.id5_contractRef && base.Reader.NamespaceURI == this.id6_Item)
					{
						if (references == null)
						{
							base.Reader.Skip();
						}
						else
						{
							references.Add(this.Read5_ContractReference(false, true));
						}
					}
					else if (base.Reader.LocalName == this.id7_schemaRef && base.Reader.NamespaceURI == this.id8_Item)
					{
						if (references == null)
						{
							base.Reader.Skip();
						}
						else
						{
							references.Add(this.Read7_SchemaReference(false, true));
						}
					}
					else if (base.Reader.LocalName == this.id9_soap && base.Reader.NamespaceURI == this.id10_Item)
					{
						if (references == null)
						{
							base.Reader.Skip();
						}
						else
						{
							references.Add(this.Read8_SoapBinding(false, true));
						}
					}
					else
					{
						base.UnknownNode(discoveryDocument, "http://schemas.xmlsoap.org/disco/:discoveryRef, http://schemas.xmlsoap.org/disco/scl/:contractRef, http://schemas.xmlsoap.org/disco/schema/:schemaRef, http://schemas.xmlsoap.org/disco/soap/:soap");
					}
				}
				else
				{
					base.UnknownNode(discoveryDocument, "http://schemas.xmlsoap.org/disco/:discoveryRef, http://schemas.xmlsoap.org/disco/scl/:contractRef, http://schemas.xmlsoap.org/disco/schema/:schemaRef, http://schemas.xmlsoap.org/disco/soap/:soap");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return discoveryDocument;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001489C File Offset: 0x00012A9C
		private SoapBinding Read8_SoapBinding(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = (checkType ? base.GetXsiType() : null);
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id11_SoapBinding || xmlQualifiedName.Namespace != this.id10_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SoapBinding soapBinding = new SoapBinding();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id12_address && base.Reader.NamespaceURI == this.id13_Item)
				{
					soapBinding.Address = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id14_binding && base.Reader.NamespaceURI == this.id13_Item)
				{
					soapBinding.Binding = base.ToXmlQualifiedName(base.Reader.Value);
					array[1] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(soapBinding, ":address, :binding");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return soapBinding;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(soapBinding, "");
				}
				else
				{
					base.UnknownNode(soapBinding, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return soapBinding;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00014A70 File Offset: 0x00012C70
		private SchemaReference Read7_SchemaReference(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = (checkType ? base.GetXsiType() : null);
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id15_SchemaReference || xmlQualifiedName.Namespace != this.id8_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SchemaReference schemaReference = new SchemaReference();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id16_ref && base.Reader.NamespaceURI == this.id13_Item)
				{
					schemaReference.Ref = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id17_targetNamespace && base.Reader.NamespaceURI == this.id13_Item)
				{
					schemaReference.TargetNamespace = base.Reader.Value;
					array[1] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(schemaReference, ":ref, :targetNamespace");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return schemaReference;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(schemaReference, "");
				}
				else
				{
					base.UnknownNode(schemaReference, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return schemaReference;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00014C3C File Offset: 0x00012E3C
		private ContractReference Read5_ContractReference(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = (checkType ? base.GetXsiType() : null);
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id18_ContractReference || xmlQualifiedName.Namespace != this.id6_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ContractReference contractReference = new ContractReference();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id16_ref && base.Reader.NamespaceURI == this.id13_Item)
				{
					contractReference.Ref = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id19_docRef && base.Reader.NamespaceURI == this.id13_Item)
				{
					contractReference.DocRef = base.Reader.Value;
					array[1] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(contractReference, ":ref, :docRef");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return contractReference;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(contractReference, "");
				}
				else
				{
					base.UnknownNode(contractReference, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return contractReference;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00014E08 File Offset: 0x00013008
		private DiscoveryDocumentReference Read3_DiscoveryDocumentReference(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = (checkType ? base.GetXsiType() : null);
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id20_DiscoveryDocumentReference || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			DiscoveryDocumentReference discoveryDocumentReference = new DiscoveryDocumentReference();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id16_ref && base.Reader.NamespaceURI == this.id13_Item)
				{
					discoveryDocumentReference.Ref = base.Reader.Value;
					array[0] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(discoveryDocumentReference, ":ref");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return discoveryDocumentReference;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(discoveryDocumentReference, "");
				}
				else
				{
					base.UnknownNode(discoveryDocumentReference, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return discoveryDocumentReference;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000210D File Offset: 0x0000030D
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00014F8C File Offset: 0x0001318C
		protected override void InitIDs()
		{
			this.id1_discovery = base.Reader.NameTable.Add("discovery");
			this.id4_discoveryRef = base.Reader.NameTable.Add("discoveryRef");
			this.id19_docRef = base.Reader.NameTable.Add("docRef");
			this.id8_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/disco/schema/");
			this.id14_binding = base.Reader.NameTable.Add("binding");
			this.id20_DiscoveryDocumentReference = base.Reader.NameTable.Add("DiscoveryDocumentReference");
			this.id17_targetNamespace = base.Reader.NameTable.Add("targetNamespace");
			this.id5_contractRef = base.Reader.NameTable.Add("contractRef");
			this.id10_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/disco/soap/");
			this.id13_Item = base.Reader.NameTable.Add("");
			this.id7_schemaRef = base.Reader.NameTable.Add("schemaRef");
			this.id3_DiscoveryDocument = base.Reader.NameTable.Add("DiscoveryDocument");
			this.id9_soap = base.Reader.NameTable.Add("soap");
			this.id12_address = base.Reader.NameTable.Add("address");
			this.id16_ref = base.Reader.NameTable.Add("ref");
			this.id11_SoapBinding = base.Reader.NameTable.Add("SoapBinding");
			this.id18_ContractReference = base.Reader.NameTable.Add("ContractReference");
			this.id2_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/disco/");
			this.id15_SchemaReference = base.Reader.NameTable.Add("SchemaReference");
			this.id6_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/disco/scl/");
		}

		// Token: 0x04000332 RID: 818
		private string id1_discovery;

		// Token: 0x04000333 RID: 819
		private string id4_discoveryRef;

		// Token: 0x04000334 RID: 820
		private string id19_docRef;

		// Token: 0x04000335 RID: 821
		private string id8_Item;

		// Token: 0x04000336 RID: 822
		private string id14_binding;

		// Token: 0x04000337 RID: 823
		private string id20_DiscoveryDocumentReference;

		// Token: 0x04000338 RID: 824
		private string id17_targetNamespace;

		// Token: 0x04000339 RID: 825
		private string id5_contractRef;

		// Token: 0x0400033A RID: 826
		private string id10_Item;

		// Token: 0x0400033B RID: 827
		private string id13_Item;

		// Token: 0x0400033C RID: 828
		private string id7_schemaRef;

		// Token: 0x0400033D RID: 829
		private string id3_DiscoveryDocument;

		// Token: 0x0400033E RID: 830
		private string id9_soap;

		// Token: 0x0400033F RID: 831
		private string id12_address;

		// Token: 0x04000340 RID: 832
		private string id16_ref;

		// Token: 0x04000341 RID: 833
		private string id11_SoapBinding;

		// Token: 0x04000342 RID: 834
		private string id18_ContractReference;

		// Token: 0x04000343 RID: 835
		private string id2_Item;

		// Token: 0x04000344 RID: 836
		private string id15_SchemaReference;

		// Token: 0x04000345 RID: 837
		private string id6_Item;
	}
}
