using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000A8 RID: 168
	internal class DiscoveryDocumentSerializationWriter : XmlSerializationWriter
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x0001420F File Offset: 0x0001240F
		public void Write10_discovery(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("discovery", "http://schemas.xmlsoap.org/disco/");
				return;
			}
			base.TopLevelElement();
			this.Write9_DiscoveryDocument("discovery", "http://schemas.xmlsoap.org/disco/", (DiscoveryDocument)o, true, false);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001424C File Offset: 0x0001244C
		private void Write9_DiscoveryDocument(string n, string ns, DiscoveryDocument o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(DiscoveryDocument)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("DiscoveryDocument", "http://schemas.xmlsoap.org/disco/");
			}
			IList references = o.References;
			if (references != null)
			{
				for (int i = 0; i < references.Count; i++)
				{
					object obj = references[i];
					if (obj is SchemaReference)
					{
						this.Write7_SchemaReference("schemaRef", "http://schemas.xmlsoap.org/disco/schema/", (SchemaReference)obj, false, false);
					}
					else if (obj is ContractReference)
					{
						this.Write5_ContractReference("contractRef", "http://schemas.xmlsoap.org/disco/scl/", (ContractReference)obj, false, false);
					}
					else if (obj is DiscoveryDocumentReference)
					{
						this.Write3_DiscoveryDocumentReference("discoveryRef", "http://schemas.xmlsoap.org/disco/", (DiscoveryDocumentReference)obj, false, false);
					}
					else if (obj is SoapBinding)
					{
						this.Write8_SoapBinding("soap", "http://schemas.xmlsoap.org/disco/soap/", (SoapBinding)obj, false, false);
					}
					else if (obj != null)
					{
						throw base.CreateUnknownTypeException(obj);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00014374 File Offset: 0x00012574
		private void Write8_SoapBinding(string n, string ns, SoapBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(SoapBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SoapBinding", "http://schemas.xmlsoap.org/disco/soap/");
			}
			base.WriteAttribute("address", "", o.Address);
			base.WriteAttribute("binding", "", base.FromXmlQualifiedName(o.Binding));
			base.WriteEndElement(o);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001440C File Offset: 0x0001260C
		private void Write3_DiscoveryDocumentReference(string n, string ns, DiscoveryDocumentReference o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(DiscoveryDocumentReference)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("DiscoveryDocumentReference", "http://schemas.xmlsoap.org/disco/");
			}
			base.WriteAttribute("ref", "", o.Ref);
			base.WriteEndElement(o);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00014488 File Offset: 0x00012688
		private void Write5_ContractReference(string n, string ns, ContractReference o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(ContractReference)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ContractReference", "http://schemas.xmlsoap.org/disco/scl/");
			}
			base.WriteAttribute("ref", "", o.Ref);
			base.WriteAttribute("docRef", "", o.DocRef);
			base.WriteEndElement(o);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001451C File Offset: 0x0001271C
		private void Write7_SchemaReference(string n, string ns, SchemaReference o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(SchemaReference)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SchemaReference", "http://schemas.xmlsoap.org/disco/schema/");
			}
			base.WriteAttribute("ref", "", o.Ref);
			base.WriteAttribute("targetNamespace", "", o.TargetNamespace);
			base.WriteEndElement(o);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000210D File Offset: 0x0000030D
		protected override void InitCallbacks()
		{
		}
	}
}
