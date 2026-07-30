using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x02000111 RID: 273
	internal class ServiceDescriptionSerializationWriter : XmlSerializationWriter
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0001EEBD File Offset: 0x0001D0BD
		public void Write125_definitions(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("definitions", "http://schemas.xmlsoap.org/wsdl/");
				return;
			}
			base.TopLevelElement();
			this.Write124_ServiceDescription("definitions", "http://schemas.xmlsoap.org/wsdl/", (ServiceDescription)o, true, false);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
		private void Write124_ServiceDescription(string n, string ns, ServiceDescription o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(ServiceDescription)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("ServiceDescription", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("targetNamespace", "", o.TargetNamespace);
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				ImportCollection imports = o.Imports;
				if (imports != null)
				{
					for (int k = 0; k < ((ICollection)imports).Count; k++)
					{
						this.Write4_Import("import", "http://schemas.xmlsoap.org/wsdl/", imports[k], false, false);
					}
				}
				this.Write67_Types("types", "http://schemas.xmlsoap.org/wsdl/", o.Types, false, false);
				MessageCollection messages = o.Messages;
				if (messages != null)
				{
					for (int l = 0; l < ((ICollection)messages).Count; l++)
					{
						this.Write69_Message("message", "http://schemas.xmlsoap.org/wsdl/", messages[l], false, false);
					}
				}
				PortTypeCollection portTypes = o.PortTypes;
				if (portTypes != null)
				{
					for (int m = 0; m < ((ICollection)portTypes).Count; m++)
					{
						this.Write75_PortType("portType", "http://schemas.xmlsoap.org/wsdl/", portTypes[m], false, false);
					}
				}
				BindingCollection bindings = o.Bindings;
				if (bindings != null)
				{
					for (int num = 0; num < ((ICollection)bindings).Count; num++)
					{
						this.Write117_Binding("binding", "http://schemas.xmlsoap.org/wsdl/", bindings[num], false, false);
					}
				}
				ServiceCollection services = o.Services;
				if (services != null)
				{
					for (int num2 = 0; num2 < ((ICollection)services).Count; num2++)
					{
						this.Write123_Service("service", "http://schemas.xmlsoap.org/wsdl/", services[num2], false, false);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001F198 File Offset: 0x0001D398
		private void Write123_Service(string n, string ns, Service o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Service)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("Service", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				PortCollection ports = o.Ports;
				if (ports != null)
				{
					for (int k = 0; k < ((ICollection)ports).Count; k++)
					{
						this.Write122_Port("port", "http://schemas.xmlsoap.org/wsdl/", ports[k], false, false);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001F318 File Offset: 0x0001D518
		private void Write122_Port(string n, string ns, Port o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Port)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("Port", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("binding", "", base.FromXmlQualifiedName(o.Binding));
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						object obj = extensions[j];
						if (obj is Soap12AddressBinding)
						{
							this.Write121_Soap12AddressBinding("address", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12AddressBinding)obj, false, false);
						}
						else if (obj is HttpAddressBinding)
						{
							this.Write118_HttpAddressBinding("address", "http://schemas.xmlsoap.org/wsdl/http/", (HttpAddressBinding)obj, false, false);
						}
						else if (obj is SoapAddressBinding)
						{
							this.Write119_SoapAddressBinding("address", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapAddressBinding)obj, false, false);
						}
						else if (obj is XmlElement)
						{
							XmlElement xmlElement = (XmlElement)obj;
							if (xmlElement == null && xmlElement != null)
							{
								throw base.CreateInvalidAnyTypeException(xmlElement);
							}
							base.WriteElementLiteral(xmlElement, "", null, false, true);
						}
						else if (obj != null)
						{
							throw base.CreateUnknownTypeException(obj);
						}
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
		private void Write119_SoapAddressBinding(string n, string ns, SoapAddressBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(SoapAddressBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SoapAddressBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("location", "", o.Location);
			base.WriteEndElement(o);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001F594 File Offset: 0x0001D794
		private void Write118_HttpAddressBinding(string n, string ns, HttpAddressBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(HttpAddressBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("HttpAddressBinding", "http://schemas.xmlsoap.org/wsdl/http/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("location", "", o.Location);
			base.WriteEndElement(o);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001F634 File Offset: 0x0001D834
		private void Write121_Soap12AddressBinding(string n, string ns, Soap12AddressBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Soap12AddressBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Soap12AddressBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("location", "", o.Location);
			base.WriteEndElement(o);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		private void Write117_Binding(string n, string ns, Binding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Binding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("Binding", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("type", "", base.FromXmlQualifiedName(o.Type));
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						object obj = extensions[j];
						if (obj is Soap12Binding)
						{
							this.Write84_Soap12Binding("binding", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12Binding)obj, false, false);
						}
						else if (obj is HttpBinding)
						{
							this.Write77_HttpBinding("binding", "http://schemas.xmlsoap.org/wsdl/http/", (HttpBinding)obj, false, false);
						}
						else if (obj is SoapBinding)
						{
							this.Write80_SoapBinding("binding", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapBinding)obj, false, false);
						}
						else if (obj is XmlElement)
						{
							XmlElement xmlElement = (XmlElement)obj;
							if (xmlElement == null && xmlElement != null)
							{
								throw base.CreateInvalidAnyTypeException(xmlElement);
							}
							base.WriteElementLiteral(xmlElement, "", null, false, true);
						}
						else if (obj != null)
						{
							throw base.CreateUnknownTypeException(obj);
						}
					}
				}
				OperationBindingCollection operations = o.Operations;
				if (operations != null)
				{
					for (int k = 0; k < ((ICollection)operations).Count; k++)
					{
						this.Write116_OperationBinding("operation", "http://schemas.xmlsoap.org/wsdl/", operations[k], false, false);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001F8EC File Offset: 0x0001DAEC
		private void Write116_OperationBinding(string n, string ns, OperationBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(OperationBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("OperationBinding", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						object obj = extensions[j];
						if (obj is Soap12OperationBinding)
						{
							this.Write88_Soap12OperationBinding("operation", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12OperationBinding)obj, false, false);
						}
						else if (obj is HttpOperationBinding)
						{
							this.Write85_HttpOperationBinding("operation", "http://schemas.xmlsoap.org/wsdl/http/", (HttpOperationBinding)obj, false, false);
						}
						else if (obj is SoapOperationBinding)
						{
							this.Write86_SoapOperationBinding("operation", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapOperationBinding)obj, false, false);
						}
						else if (obj is XmlElement)
						{
							XmlElement xmlElement = (XmlElement)obj;
							if (xmlElement == null && xmlElement != null)
							{
								throw base.CreateInvalidAnyTypeException(xmlElement);
							}
							base.WriteElementLiteral(xmlElement, "", null, false, true);
						}
						else if (obj != null)
						{
							throw base.CreateUnknownTypeException(obj);
						}
					}
				}
				this.Write110_InputBinding("input", "http://schemas.xmlsoap.org/wsdl/", o.Input, false, false);
				this.Write111_OutputBinding("output", "http://schemas.xmlsoap.org/wsdl/", o.Output, false, false);
				FaultBindingCollection faults = o.Faults;
				if (faults != null)
				{
					for (int k = 0; k < ((ICollection)faults).Count; k++)
					{
						this.Write115_FaultBinding("fault", "http://schemas.xmlsoap.org/wsdl/", faults[k], false, false);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001FB18 File Offset: 0x0001DD18
		private void Write115_FaultBinding(string n, string ns, FaultBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(FaultBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("FaultBinding", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						object obj = extensions[j];
						if (obj is Soap12FaultBinding)
						{
							this.Write114_Soap12FaultBinding("fault", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12FaultBinding)obj, false, false);
						}
						else if (obj is SoapFaultBinding)
						{
							this.Write112_SoapFaultBinding("fault", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapFaultBinding)obj, false, false);
						}
						else if (obj is XmlElement)
						{
							XmlElement xmlElement = (XmlElement)obj;
							if (xmlElement == null && xmlElement != null)
							{
								throw base.CreateInvalidAnyTypeException(xmlElement);
							}
							base.WriteElementLiteral(xmlElement, "", null, false, true);
						}
						else if (obj != null)
						{
							throw base.CreateUnknownTypeException(obj);
						}
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001FCB0 File Offset: 0x0001DEB0
		private void Write112_SoapFaultBinding(string n, string ns, SoapFaultBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(SoapFaultBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SoapFaultBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			if (o.Use != SoapBindingUse.Default)
			{
				base.WriteAttribute("use", "", this.Write98_SoapBindingUse(o.Use));
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("namespace", "", o.Namespace);
			if (o.Encoding != null && o.Encoding.Length != 0)
			{
				base.WriteAttribute("encodingStyle", "", o.Encoding);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		private string Write98_SoapBindingUse(SoapBindingUse v)
		{
			string text;
			if (v != SoapBindingUse.Encoded)
			{
				if (v != SoapBindingUse.Literal)
				{
					throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.SoapBindingUse");
				}
				text = "literal";
			}
			else
			{
				text = "encoded";
			}
			return text;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001FDFC File Offset: 0x0001DFFC
		private void Write114_Soap12FaultBinding(string n, string ns, Soap12FaultBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Soap12FaultBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Soap12FaultBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			if (o.Use != SoapBindingUse.Default)
			{
				base.WriteAttribute("use", "", this.Write100_SoapBindingUse(o.Use));
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("namespace", "", o.Namespace);
			if (o.Encoding != null && o.Encoding.Length != 0)
			{
				base.WriteAttribute("encodingStyle", "", o.Encoding);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001FF00 File Offset: 0x0001E100
		private string Write100_SoapBindingUse(SoapBindingUse v)
		{
			string text;
			if (v != SoapBindingUse.Encoded)
			{
				if (v != SoapBindingUse.Literal)
				{
					throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.SoapBindingUse");
				}
				text = "literal";
			}
			else
			{
				text = "encoded";
			}
			return text;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001FF48 File Offset: 0x0001E148
		private void Write111_OutputBinding(string n, string ns, OutputBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(OutputBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("OutputBinding", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						object obj = extensions[j];
						if (obj is Soap12BodyBinding)
						{
							this.Write102_Soap12BodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12BodyBinding)obj, false, false);
						}
						else if (obj is Soap12HeaderBinding)
						{
							this.Write109_Soap12HeaderBinding("header", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12HeaderBinding)obj, false, false);
						}
						else if (obj is SoapHeaderBinding)
						{
							this.Write106_SoapHeaderBinding("header", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapHeaderBinding)obj, false, false);
						}
						else if (obj is SoapBodyBinding)
						{
							this.Write99_SoapBodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapBodyBinding)obj, false, false);
						}
						else if (obj is MimeXmlBinding)
						{
							this.Write94_MimeXmlBinding("mimeXml", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeXmlBinding)obj, false, false);
						}
						else if (obj is MimeContentBinding)
						{
							this.Write93_MimeContentBinding("content", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeContentBinding)obj, false, false);
						}
						else if (obj is MimeTextBinding)
						{
							this.Write97_MimeTextBinding("text", "http://microsoft.com/wsdl/mime/textMatching/", (MimeTextBinding)obj, false, false);
						}
						else if (obj is MimeMultipartRelatedBinding)
						{
							this.Write104_MimeMultipartRelatedBinding("multipartRelated", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeMultipartRelatedBinding)obj, false, false);
						}
						else if (obj is XmlElement)
						{
							XmlElement xmlElement = (XmlElement)obj;
							if (xmlElement == null && xmlElement != null)
							{
								throw base.CreateInvalidAnyTypeException(xmlElement);
							}
							base.WriteElementLiteral(xmlElement, "", null, false, true);
						}
						else if (obj != null)
						{
							throw base.CreateUnknownTypeException(obj);
						}
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000201CC File Offset: 0x0001E3CC
		private void Write104_MimeMultipartRelatedBinding(string n, string ns, MimeMultipartRelatedBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(MimeMultipartRelatedBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MimeMultipartRelatedBinding", "http://schemas.xmlsoap.org/wsdl/mime/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			MimePartCollection parts = o.Parts;
			if (parts != null)
			{
				for (int i = 0; i < ((ICollection)parts).Count; i++)
				{
					this.Write103_MimePart("part", "http://schemas.xmlsoap.org/wsdl/mime/", parts[i], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0002028C File Offset: 0x0001E48C
		private void Write103_MimePart(string n, string ns, MimePart o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(MimePart)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MimePart", "http://schemas.xmlsoap.org/wsdl/mime/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int i = 0; i < ((ICollection)extensions).Count; i++)
				{
					object obj = extensions[i];
					if (obj is Soap12BodyBinding)
					{
						this.Write102_Soap12BodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12BodyBinding)obj, false, false);
					}
					else if (obj is SoapBodyBinding)
					{
						this.Write99_SoapBodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapBodyBinding)obj, false, false);
					}
					else if (obj is MimeContentBinding)
					{
						this.Write93_MimeContentBinding("content", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeContentBinding)obj, false, false);
					}
					else if (obj is MimeXmlBinding)
					{
						this.Write94_MimeXmlBinding("mimeXml", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeXmlBinding)obj, false, false);
					}
					else if (obj is MimeTextBinding)
					{
						this.Write97_MimeTextBinding("text", "http://microsoft.com/wsdl/mime/textMatching/", (MimeTextBinding)obj, false, false);
					}
					else if (obj is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)obj;
						if (xmlElement == null && xmlElement != null)
						{
							throw base.CreateInvalidAnyTypeException(xmlElement);
						}
						base.WriteElementLiteral(xmlElement, "", null, false, true);
					}
					else if (obj != null)
					{
						throw base.CreateUnknownTypeException(obj);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0002042C File Offset: 0x0001E62C
		private void Write97_MimeTextBinding(string n, string ns, MimeTextBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(MimeTextBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MimeTextBinding", "http://microsoft.com/wsdl/mime/textMatching/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			MimeTextMatchCollection matches = o.Matches;
			if (matches != null)
			{
				for (int i = 0; i < ((ICollection)matches).Count; i++)
				{
					this.Write96_MimeTextMatch("match", "http://microsoft.com/wsdl/mime/textMatching/", matches[i], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000204EC File Offset: 0x0001E6EC
		private void Write96_MimeTextMatch(string n, string ns, MimeTextMatch o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(MimeTextMatch)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MimeTextMatch", "http://microsoft.com/wsdl/mime/textMatching/");
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("type", "", o.Type);
			if (o.Group != 1)
			{
				base.WriteAttribute("group", "", XmlConvert.ToString(o.Group));
			}
			if (o.Capture != 0)
			{
				base.WriteAttribute("capture", "", XmlConvert.ToString(o.Capture));
			}
			if (o.RepeatsString != "1")
			{
				base.WriteAttribute("repeats", "", o.RepeatsString);
			}
			base.WriteAttribute("pattern", "", o.Pattern);
			base.WriteAttribute("ignoreCase", "", XmlConvert.ToString(o.IgnoreCase));
			MimeTextMatchCollection matches = o.Matches;
			if (matches != null)
			{
				for (int i = 0; i < ((ICollection)matches).Count; i++)
				{
					this.Write96_MimeTextMatch("match", "http://microsoft.com/wsdl/mime/textMatching/", matches[i], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00020654 File Offset: 0x0001E854
		private void Write94_MimeXmlBinding(string n, string ns, MimeXmlBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(MimeXmlBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MimeXmlBinding", "http://schemas.xmlsoap.org/wsdl/mime/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("part", "", o.Part);
			base.WriteEndElement(o);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000206F4 File Offset: 0x0001E8F4
		private void Write93_MimeContentBinding(string n, string ns, MimeContentBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(MimeContentBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MimeContentBinding", "http://schemas.xmlsoap.org/wsdl/mime/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("part", "", o.Part);
			base.WriteAttribute("type", "", o.Type);
			base.WriteEndElement(o);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000207AC File Offset: 0x0001E9AC
		private void Write99_SoapBodyBinding(string n, string ns, SoapBodyBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(SoapBodyBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SoapBodyBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			if (o.Use != SoapBindingUse.Default)
			{
				base.WriteAttribute("use", "", this.Write98_SoapBindingUse(o.Use));
			}
			if (o.Namespace != null && o.Namespace.Length != 0)
			{
				base.WriteAttribute("namespace", "", o.Namespace);
			}
			if (o.Encoding != null && o.Encoding.Length != 0)
			{
				base.WriteAttribute("encodingStyle", "", o.Encoding);
			}
			base.WriteAttribute("parts", "", o.PartsString);
			base.WriteEndElement(o);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000208C8 File Offset: 0x0001EAC8
		private void Write102_Soap12BodyBinding(string n, string ns, Soap12BodyBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Soap12BodyBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Soap12BodyBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			if (o.Use != SoapBindingUse.Default)
			{
				base.WriteAttribute("use", "", this.Write100_SoapBindingUse(o.Use));
			}
			if (o.Namespace != null && o.Namespace.Length != 0)
			{
				base.WriteAttribute("namespace", "", o.Namespace);
			}
			if (o.Encoding != null && o.Encoding.Length != 0)
			{
				base.WriteAttribute("encodingStyle", "", o.Encoding);
			}
			base.WriteAttribute("parts", "", o.PartsString);
			base.WriteEndElement(o);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000209E4 File Offset: 0x0001EBE4
		private void Write106_SoapHeaderBinding(string n, string ns, SoapHeaderBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(SoapHeaderBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SoapHeaderBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("message", "", base.FromXmlQualifiedName(o.Message));
			base.WriteAttribute("part", "", o.Part);
			if (o.Use != SoapBindingUse.Default)
			{
				base.WriteAttribute("use", "", this.Write98_SoapBindingUse(o.Use));
			}
			if (o.Encoding != null && o.Encoding.Length != 0)
			{
				base.WriteAttribute("encodingStyle", "", o.Encoding);
			}
			if (o.Namespace != null && o.Namespace.Length != 0)
			{
				base.WriteAttribute("namespace", "", o.Namespace);
			}
			this.Write105_SoapHeaderFaultBinding("headerfault", "http://schemas.xmlsoap.org/wsdl/soap/", o.Fault, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00020B34 File Offset: 0x0001ED34
		private void Write105_SoapHeaderFaultBinding(string n, string ns, SoapHeaderFaultBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(SoapHeaderFaultBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SoapHeaderFaultBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("message", "", base.FromXmlQualifiedName(o.Message));
			base.WriteAttribute("part", "", o.Part);
			if (o.Use != SoapBindingUse.Default)
			{
				base.WriteAttribute("use", "", this.Write98_SoapBindingUse(o.Use));
			}
			if (o.Encoding != null && o.Encoding.Length != 0)
			{
				base.WriteAttribute("encodingStyle", "", o.Encoding);
			}
			if (o.Namespace != null && o.Namespace.Length != 0)
			{
				base.WriteAttribute("namespace", "", o.Namespace);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00020C6C File Offset: 0x0001EE6C
		private void Write109_Soap12HeaderBinding(string n, string ns, Soap12HeaderBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Soap12HeaderBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Soap12HeaderBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("message", "", base.FromXmlQualifiedName(o.Message));
			base.WriteAttribute("part", "", o.Part);
			if (o.Use != SoapBindingUse.Default)
			{
				base.WriteAttribute("use", "", this.Write100_SoapBindingUse(o.Use));
			}
			if (o.Encoding != null && o.Encoding.Length != 0)
			{
				base.WriteAttribute("encodingStyle", "", o.Encoding);
			}
			if (o.Namespace != null && o.Namespace.Length != 0)
			{
				base.WriteAttribute("namespace", "", o.Namespace);
			}
			this.Write107_SoapHeaderFaultBinding("headerfault", "http://schemas.xmlsoap.org/wsdl/soap12/", o.Fault, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00020DBC File Offset: 0x0001EFBC
		private void Write107_SoapHeaderFaultBinding(string n, string ns, SoapHeaderFaultBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(SoapHeaderFaultBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SoapHeaderFaultBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("message", "", base.FromXmlQualifiedName(o.Message));
			base.WriteAttribute("part", "", o.Part);
			if (o.Use != SoapBindingUse.Default)
			{
				base.WriteAttribute("use", "", this.Write100_SoapBindingUse(o.Use));
			}
			if (o.Encoding != null && o.Encoding.Length != 0)
			{
				base.WriteAttribute("encodingStyle", "", o.Encoding);
			}
			if (o.Namespace != null && o.Namespace.Length != 0)
			{
				base.WriteAttribute("namespace", "", o.Namespace);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00020EF4 File Offset: 0x0001F0F4
		private void Write110_InputBinding(string n, string ns, InputBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(InputBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("InputBinding", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						object obj = extensions[j];
						if (obj is Soap12BodyBinding)
						{
							this.Write102_Soap12BodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12BodyBinding)obj, false, false);
						}
						else if (obj is Soap12HeaderBinding)
						{
							this.Write109_Soap12HeaderBinding("header", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12HeaderBinding)obj, false, false);
						}
						else if (obj is SoapBodyBinding)
						{
							this.Write99_SoapBodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapBodyBinding)obj, false, false);
						}
						else if (obj is SoapHeaderBinding)
						{
							this.Write106_SoapHeaderBinding("header", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapHeaderBinding)obj, false, false);
						}
						else if (obj is MimeTextBinding)
						{
							this.Write97_MimeTextBinding("text", "http://microsoft.com/wsdl/mime/textMatching/", (MimeTextBinding)obj, false, false);
						}
						else if (obj is HttpUrlReplacementBinding)
						{
							this.Write91_HttpUrlReplacementBinding("urlReplacement", "http://schemas.xmlsoap.org/wsdl/http/", (HttpUrlReplacementBinding)obj, false, false);
						}
						else if (obj is HttpUrlEncodedBinding)
						{
							this.Write90_HttpUrlEncodedBinding("urlEncoded", "http://schemas.xmlsoap.org/wsdl/http/", (HttpUrlEncodedBinding)obj, false, false);
						}
						else if (obj is MimeContentBinding)
						{
							this.Write93_MimeContentBinding("content", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeContentBinding)obj, false, false);
						}
						else if (obj is MimeMultipartRelatedBinding)
						{
							this.Write104_MimeMultipartRelatedBinding("multipartRelated", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeMultipartRelatedBinding)obj, false, false);
						}
						else if (obj is MimeXmlBinding)
						{
							this.Write94_MimeXmlBinding("mimeXml", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeXmlBinding)obj, false, false);
						}
						else if (obj is XmlElement)
						{
							XmlElement xmlElement = (XmlElement)obj;
							if (xmlElement == null && xmlElement != null)
							{
								throw base.CreateInvalidAnyTypeException(xmlElement);
							}
							base.WriteElementLiteral(xmlElement, "", null, false, true);
						}
						else if (obj != null)
						{
							throw base.CreateUnknownTypeException(obj);
						}
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000211C4 File Offset: 0x0001F3C4
		private void Write90_HttpUrlEncodedBinding(string n, string ns, HttpUrlEncodedBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(HttpUrlEncodedBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("HttpUrlEncodedBinding", "http://schemas.xmlsoap.org/wsdl/http/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00021250 File Offset: 0x0001F450
		private void Write91_HttpUrlReplacementBinding(string n, string ns, HttpUrlReplacementBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(HttpUrlReplacementBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("HttpUrlReplacementBinding", "http://schemas.xmlsoap.org/wsdl/http/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000212DC File Offset: 0x0001F4DC
		private void Write86_SoapOperationBinding(string n, string ns, SoapOperationBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(SoapOperationBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SoapOperationBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("soapAction", "", o.SoapAction);
			if (o.Style != SoapBindingStyle.Default)
			{
				base.WriteAttribute("style", "", this.Write79_SoapBindingStyle(o.Style));
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000213A0 File Offset: 0x0001F5A0
		private string Write79_SoapBindingStyle(SoapBindingStyle v)
		{
			string text;
			if (v != SoapBindingStyle.Document)
			{
				if (v != SoapBindingStyle.Rpc)
				{
					throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.SoapBindingStyle");
				}
				text = "rpc";
			}
			else
			{
				text = "document";
			}
			return text;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000213E8 File Offset: 0x0001F5E8
		private void Write85_HttpOperationBinding(string n, string ns, HttpOperationBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(HttpOperationBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("HttpOperationBinding", "http://schemas.xmlsoap.org/wsdl/http/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("location", "", o.Location);
			base.WriteEndElement(o);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00021488 File Offset: 0x0001F688
		private void Write88_Soap12OperationBinding(string n, string ns, Soap12OperationBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Soap12OperationBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Soap12OperationBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("soapAction", "", o.SoapAction);
			if (o.Style != SoapBindingStyle.Default)
			{
				base.WriteAttribute("style", "", this.Write82_SoapBindingStyle(o.Style));
			}
			if (o.SoapActionRequired)
			{
				base.WriteAttribute("soapActionRequired", "", XmlConvert.ToString(o.SoapActionRequired));
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00021570 File Offset: 0x0001F770
		private string Write82_SoapBindingStyle(SoapBindingStyle v)
		{
			string text;
			if (v != SoapBindingStyle.Document)
			{
				if (v != SoapBindingStyle.Rpc)
				{
					throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.SoapBindingStyle");
				}
				text = "rpc";
			}
			else
			{
				text = "document";
			}
			return text;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000215B8 File Offset: 0x0001F7B8
		private void Write80_SoapBinding(string n, string ns, SoapBinding o, bool isNullable, bool needType)
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
				base.WriteXsiType("SoapBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("transport", "", o.Transport);
			if (o.Style != SoapBindingStyle.Document)
			{
				base.WriteAttribute("style", "", this.Write79_SoapBindingStyle(o.Style));
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0002167C File Offset: 0x0001F87C
		private void Write77_HttpBinding(string n, string ns, HttpBinding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(HttpBinding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("HttpBinding", "http://schemas.xmlsoap.org/wsdl/http/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("verb", "", o.Verb);
			base.WriteEndElement(o);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0002171C File Offset: 0x0001F91C
		private void Write84_Soap12Binding(string n, string ns, Soap12Binding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Soap12Binding)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Soap12Binding", "http://schemas.xmlsoap.org/wsdl/soap12/");
			}
			if (o.Required)
			{
				base.WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
			}
			base.WriteAttribute("transport", "", o.Transport);
			if (o.Style != SoapBindingStyle.Document)
			{
				base.WriteAttribute("style", "", this.Write82_SoapBindingStyle(o.Style));
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000217E0 File Offset: 0x0001F9E0
		private void Write75_PortType(string n, string ns, PortType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(PortType)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("PortType", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				OperationCollection operations = o.Operations;
				if (operations != null)
				{
					for (int k = 0; k < ((ICollection)operations).Count; k++)
					{
						this.Write74_Operation("operation", "http://schemas.xmlsoap.org/wsdl/", operations[k], false, false);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00021960 File Offset: 0x0001FB60
		private void Write74_Operation(string n, string ns, Operation o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Operation)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("Operation", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.ParameterOrderString != null && o.ParameterOrderString.Length != 0)
			{
				base.WriteAttribute("parameterOrder", "", o.ParameterOrderString);
			}
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				OperationMessageCollection messages = o.Messages;
				if (messages != null)
				{
					for (int k = 0; k < ((ICollection)messages).Count; k++)
					{
						OperationMessage operationMessage = messages[k];
						if (operationMessage is OperationOutput)
						{
							this.Write72_OperationOutput("output", "http://schemas.xmlsoap.org/wsdl/", (OperationOutput)operationMessage, false, false);
						}
						else if (operationMessage is OperationInput)
						{
							this.Write71_OperationInput("input", "http://schemas.xmlsoap.org/wsdl/", (OperationInput)operationMessage, false, false);
						}
						else if (operationMessage != null)
						{
							throw base.CreateUnknownTypeException(operationMessage);
						}
					}
				}
				OperationFaultCollection faults = o.Faults;
				if (faults != null)
				{
					for (int l = 0; l < ((ICollection)faults).Count; l++)
					{
						this.Write73_OperationFault("fault", "http://schemas.xmlsoap.org/wsdl/", faults[l], false, false);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00021B8C File Offset: 0x0001FD8C
		private void Write73_OperationFault(string n, string ns, OperationFault o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(OperationFault)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("OperationFault", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("message", "", base.FromXmlQualifiedName(o.Message));
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		private void Write71_OperationInput(string n, string ns, OperationInput o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(OperationInput)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("OperationInput", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("message", "", base.FromXmlQualifiedName(o.Message));
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00021E44 File Offset: 0x00020044
		private void Write72_OperationOutput(string n, string ns, OperationOutput o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(OperationOutput)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("OperationOutput", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("message", "", base.FromXmlQualifiedName(o.Message));
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00021FA0 File Offset: 0x000201A0
		private void Write69_Message(string n, string ns, Message o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Message)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("Message", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				MessagePartCollection parts = o.Parts;
				if (parts != null)
				{
					for (int k = 0; k < ((ICollection)parts).Count; k++)
					{
						this.Write68_MessagePart("part", "http://schemas.xmlsoap.org/wsdl/", parts[k], false, false);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00022120 File Offset: 0x00020320
		private void Write68_MessagePart(string n, string ns, MessagePart o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(MessagePart)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("MessagePart", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("element", "", base.FromXmlQualifiedName(o.Element));
			base.WriteAttribute("type", "", base.FromXmlQualifiedName(o.Type));
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00022298 File Offset: 0x00020498
		private void Write67_Types(string n, string ns, Types o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Types)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("Types", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				XmlSchemas schemas = o.Schemas;
				if (schemas != null)
				{
					for (int k = 0; k < ((ICollection)schemas).Count; k++)
					{
						this.Write66_XmlSchema("schema", "http://www.w3.org/2001/XMLSchema", schemas[k], false, false);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00022400 File Offset: 0x00020600
		private void Write66_XmlSchema(string n, string ns, XmlSchema o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchema)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchema", "http://www.w3.org/2001/XMLSchema");
			}
			if (o.AttributeFormDefault != XmlSchemaForm.None)
			{
				base.WriteAttribute("attributeFormDefault", "", this.Write6_XmlSchemaForm(o.AttributeFormDefault));
			}
			if (o.BlockDefault != XmlSchemaDerivationMethod.None)
			{
				base.WriteAttribute("blockDefault", "", this.Write7_XmlSchemaDerivationMethod(o.BlockDefault));
			}
			if (o.FinalDefault != XmlSchemaDerivationMethod.None)
			{
				base.WriteAttribute("finalDefault", "", this.Write7_XmlSchemaDerivationMethod(o.FinalDefault));
			}
			if (o.ElementFormDefault != XmlSchemaForm.None)
			{
				base.WriteAttribute("elementFormDefault", "", this.Write6_XmlSchemaForm(o.ElementFormDefault));
			}
			base.WriteAttribute("targetNamespace", "", o.TargetNamespace);
			base.WriteAttribute("version", "", o.Version);
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			XmlSchemaObjectCollection includes = o.Includes;
			if (includes != null)
			{
				for (int j = 0; j < ((ICollection)includes).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = includes[j];
					if (xmlSchemaObject is XmlSchemaRedefine)
					{
						this.Write64_XmlSchemaRedefine("redefine", "http://www.w3.org/2001/XMLSchema", (XmlSchemaRedefine)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaImport)
					{
						this.Write13_XmlSchemaImport("import", "http://www.w3.org/2001/XMLSchema", (XmlSchemaImport)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaInclude)
					{
						this.Write12_XmlSchemaInclude("include", "http://www.w3.org/2001/XMLSchema", (XmlSchemaInclude)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int k = 0; k < ((ICollection)items).Count; k++)
				{
					XmlSchemaObject xmlSchemaObject2 = items[k];
					if (xmlSchemaObject2 is XmlSchemaElement)
					{
						this.Write52_XmlSchemaElement("element", "http://www.w3.org/2001/XMLSchema", (XmlSchemaElement)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 is XmlSchemaComplexType)
					{
						this.Write62_XmlSchemaComplexType("complexType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexType)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 is XmlSchemaSimpleType)
					{
						this.Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleType)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 is XmlSchemaAttribute)
					{
						this.Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 is XmlSchemaAttributeGroup)
					{
						this.Write40_XmlSchemaAttributeGroup("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroup)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 is XmlSchemaNotation)
					{
						this.Write65_XmlSchemaNotation("notation", "http://www.w3.org/2001/XMLSchema", (XmlSchemaNotation)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 is XmlSchemaGroup)
					{
						this.Write63_XmlSchemaGroup("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroup)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 is XmlSchemaAnnotation)
					{
						this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAnnotation)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject2);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002278C File Offset: 0x0002098C
		private void Write11_XmlSchemaAnnotation(string n, string ns, XmlSchemaAnnotation o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaAnnotation)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaAnnotation", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int j = 0; j < ((ICollection)items).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = items[j];
					if (xmlSchemaObject is XmlSchemaAppInfo)
					{
						this.Write10_XmlSchemaAppInfo("appinfo", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAppInfo)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaDocumentation)
					{
						this.Write9_XmlSchemaDocumentation("documentation", "http://www.w3.org/2001/XMLSchema", (XmlSchemaDocumentation)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000228B8 File Offset: 0x00020AB8
		private void Write9_XmlSchemaDocumentation(string n, string ns, XmlSchemaDocumentation o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaDocumentation)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaDocumentation", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("source", "", o.Source);
			base.WriteAttribute("lang", "http://www.w3.org/XML/1998/namespace", o.Language);
			XmlNode[] markup = o.Markup;
			if (markup != null)
			{
				foreach (XmlNode xmlNode in markup)
				{
					if (xmlNode is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement == null && xmlElement != null)
						{
							throw base.CreateInvalidAnyTypeException(xmlElement);
						}
						base.WriteElementLiteral(xmlElement, "", null, false, true);
					}
					else if (xmlNode != null)
					{
						xmlNode.WriteTo(base.Writer);
					}
					else if (xmlNode != null)
					{
						throw base.CreateUnknownTypeException(xmlNode);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000229BC File Offset: 0x00020BBC
		private void Write10_XmlSchemaAppInfo(string n, string ns, XmlSchemaAppInfo o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaAppInfo)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaAppInfo", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("source", "", o.Source);
			XmlNode[] markup = o.Markup;
			if (markup != null)
			{
				foreach (XmlNode xmlNode in markup)
				{
					if (xmlNode is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement == null && xmlElement != null)
						{
							throw base.CreateInvalidAnyTypeException(xmlElement);
						}
						base.WriteElementLiteral(xmlElement, "", null, false, true);
					}
					else if (xmlNode != null)
					{
						xmlNode.WriteTo(base.Writer);
					}
					else if (xmlNode != null)
					{
						throw base.CreateUnknownTypeException(xmlNode);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00022AAC File Offset: 0x00020CAC
		private void Write63_XmlSchemaGroup(string n, string ns, XmlSchemaGroup o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaGroup)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaGroup", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			if (o.Particle is XmlSchemaAll)
			{
				this.Write55_XmlSchemaAll("all", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAll)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaSequence)
			{
				this.Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)o.Particle, false, false);
			}
			else if (o.Particle != null)
			{
				throw base.CreateUnknownTypeException(o.Particle);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00022C20 File Offset: 0x00020E20
		private void Write53_XmlSchemaSequence(string n, string ns, XmlSchemaSequence o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaSequence)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaSequence", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("minOccurs", "", o.MinOccursString);
			base.WriteAttribute("maxOccurs", "", o.MaxOccursString);
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int j = 0; j < ((ICollection)items).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = items[j];
					if (xmlSchemaObject is XmlSchemaChoice)
					{
						this.Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaSequence)
					{
						this.Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaGroupRef)
					{
						this.Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaElement)
					{
						this.Write52_XmlSchemaElement("element", "http://www.w3.org/2001/XMLSchema", (XmlSchemaElement)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaAny)
					{
						this.Write46_XmlSchemaAny("any", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAny)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00022E08 File Offset: 0x00021008
		private void Write46_XmlSchemaAny(string n, string ns, XmlSchemaAny o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaAny)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaAny", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("minOccurs", "", o.MinOccursString);
			base.WriteAttribute("maxOccurs", "", o.MaxOccursString);
			base.WriteAttribute("namespace", "", o.Namespace);
			if (o.ProcessContents != XmlSchemaContentProcessing.None)
			{
				base.WriteAttribute("processContents", "", this.Write38_XmlSchemaContentProcessing(o.ProcessContents));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00022F34 File Offset: 0x00021134
		private string Write38_XmlSchemaContentProcessing(XmlSchemaContentProcessing v)
		{
			string text;
			switch (v)
			{
			case XmlSchemaContentProcessing.Skip:
				text = "skip";
				break;
			case XmlSchemaContentProcessing.Lax:
				text = "lax";
				break;
			case XmlSchemaContentProcessing.Strict:
				text = "strict";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Xml.Schema.XmlSchemaContentProcessing");
			}
			return text;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00022F90 File Offset: 0x00021190
		private void Write52_XmlSchemaElement(string n, string ns, XmlSchemaElement o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaElement)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaElement", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("minOccurs", "", o.MinOccursString);
			base.WriteAttribute("maxOccurs", "", o.MaxOccursString);
			if (o.IsAbstract)
			{
				base.WriteAttribute("abstract", "", XmlConvert.ToString(o.IsAbstract));
			}
			if (o.Block != XmlSchemaDerivationMethod.None)
			{
				base.WriteAttribute("block", "", this.Write7_XmlSchemaDerivationMethod(o.Block));
			}
			base.WriteAttribute("default", "", o.DefaultValue);
			if (o.Final != XmlSchemaDerivationMethod.None)
			{
				base.WriteAttribute("final", "", this.Write7_XmlSchemaDerivationMethod(o.Final));
			}
			base.WriteAttribute("fixed", "", o.FixedValue);
			if (o.Form != XmlSchemaForm.None)
			{
				base.WriteAttribute("form", "", this.Write6_XmlSchemaForm(o.Form));
			}
			if (o.Name != null && o.Name.Length != 0)
			{
				base.WriteAttribute("name", "", o.Name);
			}
			if (o.IsNillable)
			{
				base.WriteAttribute("nillable", "", XmlConvert.ToString(o.IsNillable));
			}
			base.WriteAttribute("ref", "", base.FromXmlQualifiedName(o.RefName));
			base.WriteAttribute("substitutionGroup", "", base.FromXmlQualifiedName(o.SubstitutionGroup));
			base.WriteAttribute("type", "", base.FromXmlQualifiedName(o.SchemaTypeName));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			if (o.SchemaType is XmlSchemaComplexType)
			{
				this.Write62_XmlSchemaComplexType("complexType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexType)o.SchemaType, false, false);
			}
			else if (o.SchemaType is XmlSchemaSimpleType)
			{
				this.Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleType)o.SchemaType, false, false);
			}
			else if (o.SchemaType != null)
			{
				throw base.CreateUnknownTypeException(o.SchemaType);
			}
			XmlSchemaObjectCollection constraints = o.Constraints;
			if (constraints != null)
			{
				for (int j = 0; j < ((ICollection)constraints).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = constraints[j];
					if (xmlSchemaObject is XmlSchemaKeyref)
					{
						this.Write51_XmlSchemaKeyref("keyref", "http://www.w3.org/2001/XMLSchema", (XmlSchemaKeyref)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaUnique)
					{
						this.Write50_XmlSchemaUnique("unique", "http://www.w3.org/2001/XMLSchema", (XmlSchemaUnique)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaKey)
					{
						this.Write49_XmlSchemaKey("key", "http://www.w3.org/2001/XMLSchema", (XmlSchemaKey)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00023300 File Offset: 0x00021500
		private void Write49_XmlSchemaKey(string n, string ns, XmlSchemaKey o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaKey)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaKey", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			this.Write47_XmlSchemaXPath("selector", "http://www.w3.org/2001/XMLSchema", o.Selector, false, false);
			XmlSchemaObjectCollection fields = o.Fields;
			if (fields != null)
			{
				for (int j = 0; j < ((ICollection)fields).Count; j++)
				{
					this.Write47_XmlSchemaXPath("field", "http://www.w3.org/2001/XMLSchema", (XmlSchemaXPath)fields[j], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00023430 File Offset: 0x00021630
		private void Write47_XmlSchemaXPath(string n, string ns, XmlSchemaXPath o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaXPath)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaXPath", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			if (o.XPath != null && o.XPath.Length != 0)
			{
				base.WriteAttribute("xpath", "", o.XPath);
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00023520 File Offset: 0x00021720
		private void Write50_XmlSchemaUnique(string n, string ns, XmlSchemaUnique o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaUnique)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaUnique", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			this.Write47_XmlSchemaXPath("selector", "http://www.w3.org/2001/XMLSchema", o.Selector, false, false);
			XmlSchemaObjectCollection fields = o.Fields;
			if (fields != null)
			{
				for (int j = 0; j < ((ICollection)fields).Count; j++)
				{
					this.Write47_XmlSchemaXPath("field", "http://www.w3.org/2001/XMLSchema", (XmlSchemaXPath)fields[j], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00023650 File Offset: 0x00021850
		private void Write51_XmlSchemaKeyref(string n, string ns, XmlSchemaKeyref o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaKeyref)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaKeyref", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("refer", "", base.FromXmlQualifiedName(o.Refer));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			this.Write47_XmlSchemaXPath("selector", "http://www.w3.org/2001/XMLSchema", o.Selector, false, false);
			XmlSchemaObjectCollection fields = o.Fields;
			if (fields != null)
			{
				for (int j = 0; j < ((ICollection)fields).Count; j++)
				{
					this.Write47_XmlSchemaXPath("field", "http://www.w3.org/2001/XMLSchema", (XmlSchemaXPath)fields[j], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002379C File Offset: 0x0002199C
		private void Write34_XmlSchemaSimpleType(string n, string ns, XmlSchemaSimpleType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleType)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaSimpleType", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.Final != XmlSchemaDerivationMethod.None)
			{
				base.WriteAttribute("final", "", this.Write7_XmlSchemaDerivationMethod(o.Final));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			if (o.Content is XmlSchemaSimpleTypeUnion)
			{
				this.Write33_XmlSchemaSimpleTypeUnion("union", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleTypeUnion)o.Content, false, false);
			}
			else if (o.Content is XmlSchemaSimpleTypeRestriction)
			{
				this.Write32_XmlSchemaSimpleTypeRestriction("restriction", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleTypeRestriction)o.Content, false, false);
			}
			else if (o.Content is XmlSchemaSimpleTypeList)
			{
				this.Write17_XmlSchemaSimpleTypeList("list", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleTypeList)o.Content, false, false);
			}
			else if (o.Content != null)
			{
				throw base.CreateUnknownTypeException(o.Content);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00023938 File Offset: 0x00021B38
		private void Write17_XmlSchemaSimpleTypeList(string n, string ns, XmlSchemaSimpleTypeList o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleTypeList)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaSimpleTypeList", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("itemType", "", base.FromXmlQualifiedName(o.ItemTypeName));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			this.Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", o.ItemType, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00023A30 File Offset: 0x00021C30
		private void Write32_XmlSchemaSimpleTypeRestriction(string n, string ns, XmlSchemaSimpleTypeRestriction o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleTypeRestriction)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaSimpleTypeRestriction", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("base", "", base.FromXmlQualifiedName(o.BaseTypeName));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			this.Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", o.BaseType, false, false);
			XmlSchemaObjectCollection facets = o.Facets;
			if (facets != null)
			{
				for (int j = 0; j < ((ICollection)facets).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = facets[j];
					if (xmlSchemaObject is XmlSchemaLengthFacet)
					{
						this.Write23_XmlSchemaLengthFacet("length", "http://www.w3.org/2001/XMLSchema", (XmlSchemaLengthFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaTotalDigitsFacet)
					{
						this.Write24_XmlSchemaTotalDigitsFacet("totalDigits", "http://www.w3.org/2001/XMLSchema", (XmlSchemaTotalDigitsFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMaxLengthFacet)
					{
						this.Write22_XmlSchemaMaxLengthFacet("maxLength", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxLengthFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaFractionDigitsFacet)
					{
						this.Write20_XmlSchemaFractionDigitsFacet("fractionDigits", "http://www.w3.org/2001/XMLSchema", (XmlSchemaFractionDigitsFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMinLengthFacet)
					{
						this.Write31_XmlSchemaMinLengthFacet("minLength", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinLengthFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMaxExclusiveFacet)
					{
						this.Write28_XmlSchemaMaxExclusiveFacet("maxExclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxExclusiveFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaWhiteSpaceFacet)
					{
						this.Write29_XmlSchemaWhiteSpaceFacet("whiteSpace", "http://www.w3.org/2001/XMLSchema", (XmlSchemaWhiteSpaceFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMinExclusiveFacet)
					{
						this.Write30_XmlSchemaMinExclusiveFacet("minExclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinExclusiveFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaPatternFacet)
					{
						this.Write25_XmlSchemaPatternFacet("pattern", "http://www.w3.org/2001/XMLSchema", (XmlSchemaPatternFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMinInclusiveFacet)
					{
						this.Write21_XmlSchemaMinInclusiveFacet("minInclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinInclusiveFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMaxInclusiveFacet)
					{
						this.Write27_XmlSchemaMaxInclusiveFacet("maxInclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxInclusiveFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaEnumerationFacet)
					{
						this.Write26_XmlSchemaEnumerationFacet("enumeration", "http://www.w3.org/2001/XMLSchema", (XmlSchemaEnumerationFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00023D30 File Offset: 0x00021F30
		private void Write26_XmlSchemaEnumerationFacet(string n, string ns, XmlSchemaEnumerationFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaEnumerationFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaEnumerationFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00023E30 File Offset: 0x00022030
		private void Write27_XmlSchemaMaxInclusiveFacet(string n, string ns, XmlSchemaMaxInclusiveFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaMaxInclusiveFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaMaxInclusiveFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00023F30 File Offset: 0x00022130
		private void Write21_XmlSchemaMinInclusiveFacet(string n, string ns, XmlSchemaMinInclusiveFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaMinInclusiveFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaMinInclusiveFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00024030 File Offset: 0x00022230
		private void Write25_XmlSchemaPatternFacet(string n, string ns, XmlSchemaPatternFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaPatternFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaPatternFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00024130 File Offset: 0x00022330
		private void Write30_XmlSchemaMinExclusiveFacet(string n, string ns, XmlSchemaMinExclusiveFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaMinExclusiveFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaMinExclusiveFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00024230 File Offset: 0x00022430
		private void Write29_XmlSchemaWhiteSpaceFacet(string n, string ns, XmlSchemaWhiteSpaceFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaWhiteSpaceFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaWhiteSpaceFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00024330 File Offset: 0x00022530
		private void Write28_XmlSchemaMaxExclusiveFacet(string n, string ns, XmlSchemaMaxExclusiveFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaMaxExclusiveFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaMaxExclusiveFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00024430 File Offset: 0x00022630
		private void Write31_XmlSchemaMinLengthFacet(string n, string ns, XmlSchemaMinLengthFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaMinLengthFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaMinLengthFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00024530 File Offset: 0x00022730
		private void Write20_XmlSchemaFractionDigitsFacet(string n, string ns, XmlSchemaFractionDigitsFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaFractionDigitsFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaFractionDigitsFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00024630 File Offset: 0x00022830
		private void Write22_XmlSchemaMaxLengthFacet(string n, string ns, XmlSchemaMaxLengthFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaMaxLengthFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaMaxLengthFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00024730 File Offset: 0x00022930
		private void Write24_XmlSchemaTotalDigitsFacet(string n, string ns, XmlSchemaTotalDigitsFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaTotalDigitsFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaTotalDigitsFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00024830 File Offset: 0x00022A30
		private void Write23_XmlSchemaLengthFacet(string n, string ns, XmlSchemaLengthFacet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaLengthFacet)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaLengthFacet", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				base.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00024930 File Offset: 0x00022B30
		private void Write33_XmlSchemaSimpleTypeUnion(string n, string ns, XmlSchemaSimpleTypeUnion o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleTypeUnion)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaSimpleTypeUnion", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			XmlQualifiedName[] memberTypes = o.MemberTypes;
			if (memberTypes != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < memberTypes.Length; j++)
				{
					XmlQualifiedName xmlQualifiedName = memberTypes[j];
					if (j != 0)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(base.FromXmlQualifiedName(xmlQualifiedName));
				}
				if (stringBuilder.Length != 0)
				{
					base.WriteAttribute("memberTypes", "", stringBuilder.ToString());
				}
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			XmlSchemaObjectCollection baseTypes = o.BaseTypes;
			if (baseTypes != null)
			{
				for (int k = 0; k < ((ICollection)baseTypes).Count; k++)
				{
					this.Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleType)baseTypes[k], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00024AA0 File Offset: 0x00022CA0
		private string Write7_XmlSchemaDerivationMethod(XmlSchemaDerivationMethod v)
		{
			switch (v)
			{
			case XmlSchemaDerivationMethod.Empty:
				return "";
			case XmlSchemaDerivationMethod.Substitution:
				return "substitution";
			case XmlSchemaDerivationMethod.Extension:
				return "extension";
			case XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension:
			case XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Restriction:
			case XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction:
			case XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction:
				break;
			case XmlSchemaDerivationMethod.Restriction:
				return "restriction";
			case XmlSchemaDerivationMethod.List:
				return "list";
			default:
				if (v == XmlSchemaDerivationMethod.Union)
				{
					return "union";
				}
				if (v == XmlSchemaDerivationMethod.All)
				{
					return "#all";
				}
				break;
			}
			return XmlSerializationWriter.FromEnum((long)v, new string[] { "", "substitution", "extension", "restriction", "list", "union", "#all" }, new long[] { 0L, 1L, 2L, 4L, 8L, 16L, 255L }, "System.Xml.Schema.XmlSchemaDerivationMethod");
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00024B84 File Offset: 0x00022D84
		private void Write62_XmlSchemaComplexType(string n, string ns, XmlSchemaComplexType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaComplexType)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaComplexType", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			if (o.Final != XmlSchemaDerivationMethod.None)
			{
				base.WriteAttribute("final", "", this.Write7_XmlSchemaDerivationMethod(o.Final));
			}
			if (o.IsAbstract)
			{
				base.WriteAttribute("abstract", "", XmlConvert.ToString(o.IsAbstract));
			}
			if (o.Block != XmlSchemaDerivationMethod.None)
			{
				base.WriteAttribute("block", "", this.Write7_XmlSchemaDerivationMethod(o.Block));
			}
			if (o.IsMixed)
			{
				base.WriteAttribute("mixed", "", XmlConvert.ToString(o.IsMixed));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			if (o.ContentModel is XmlSchemaSimpleContent)
			{
				this.Write61_XmlSchemaSimpleContent("simpleContent", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleContent)o.ContentModel, false, false);
			}
			else if (o.ContentModel is XmlSchemaComplexContent)
			{
				this.Write58_XmlSchemaComplexContent("complexContent", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexContent)o.ContentModel, false, false);
			}
			else if (o.ContentModel != null)
			{
				throw base.CreateUnknownTypeException(o.ContentModel);
			}
			if (o.Particle is XmlSchemaChoice)
			{
				this.Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaAll)
			{
				this.Write55_XmlSchemaAll("all", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAll)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaSequence)
			{
				this.Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaGroupRef)
			{
				this.Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)o.Particle, false, false);
			}
			else if (o.Particle != null)
			{
				throw base.CreateUnknownTypeException(o.Particle);
			}
			XmlSchemaObjectCollection attributes = o.Attributes;
			if (attributes != null)
			{
				for (int j = 0; j < ((ICollection)attributes).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = attributes[j];
					if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
					{
						this.Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaAttribute)
					{
						this.Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			this.Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00024EC4 File Offset: 0x000230C4
		private void Write39_XmlSchemaAnyAttribute(string n, string ns, XmlSchemaAnyAttribute o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaAnyAttribute)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaAnyAttribute", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("namespace", "", o.Namespace);
			if (o.ProcessContents != XmlSchemaContentProcessing.None)
			{
				base.WriteAttribute("processContents", "", this.Write38_XmlSchemaContentProcessing(o.ProcessContents));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00024FC4 File Offset: 0x000231C4
		private void Write36_XmlSchemaAttribute(string n, string ns, XmlSchemaAttribute o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaAttribute)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaAttribute", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("default", "", o.DefaultValue);
			base.WriteAttribute("fixed", "", o.FixedValue);
			if (o.Form != XmlSchemaForm.None)
			{
				base.WriteAttribute("form", "", this.Write6_XmlSchemaForm(o.Form));
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("ref", "", base.FromXmlQualifiedName(o.RefName));
			base.WriteAttribute("type", "", base.FromXmlQualifiedName(o.SchemaTypeName));
			if (o.Use != XmlSchemaUse.None)
			{
				base.WriteAttribute("use", "", this.Write35_XmlSchemaUse(o.Use));
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			this.Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", o.SchemaType, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00025164 File Offset: 0x00023364
		private string Write35_XmlSchemaUse(XmlSchemaUse v)
		{
			string text;
			switch (v)
			{
			case XmlSchemaUse.Optional:
				text = "optional";
				break;
			case XmlSchemaUse.Prohibited:
				text = "prohibited";
				break;
			case XmlSchemaUse.Required:
				text = "required";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Xml.Schema.XmlSchemaUse");
			}
			return text;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000251C0 File Offset: 0x000233C0
		private string Write6_XmlSchemaForm(XmlSchemaForm v)
		{
			string text;
			if (v != XmlSchemaForm.Qualified)
			{
				if (v != XmlSchemaForm.Unqualified)
				{
					throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Xml.Schema.XmlSchemaForm");
				}
				text = "unqualified";
			}
			else
			{
				text = "qualified";
			}
			return text;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00025208 File Offset: 0x00023408
		private void Write37_XmlSchemaAttributeGroupRef(string n, string ns, XmlSchemaAttributeGroupRef o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaAttributeGroupRef)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaAttributeGroupRef", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("ref", "", base.FromXmlQualifiedName(o.RefName));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000252E8 File Offset: 0x000234E8
		private void Write44_XmlSchemaGroupRef(string n, string ns, XmlSchemaGroupRef o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaGroupRef)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaGroupRef", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("minOccurs", "", o.MinOccursString);
			base.WriteAttribute("maxOccurs", "", o.MaxOccursString);
			base.WriteAttribute("ref", "", base.FromXmlQualifiedName(o.RefName));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000253F4 File Offset: 0x000235F4
		private void Write55_XmlSchemaAll(string n, string ns, XmlSchemaAll o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaAll)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaAll", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("minOccurs", "", o.MinOccursString);
			base.WriteAttribute("maxOccurs", "", o.MaxOccursString);
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int j = 0; j < ((ICollection)items).Count; j++)
				{
					this.Write52_XmlSchemaElement("element", "http://www.w3.org/2001/XMLSchema", (XmlSchemaElement)items[j], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00025524 File Offset: 0x00023724
		private void Write54_XmlSchemaChoice(string n, string ns, XmlSchemaChoice o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaChoice)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaChoice", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("minOccurs", "", o.MinOccursString);
			base.WriteAttribute("maxOccurs", "", o.MaxOccursString);
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int j = 0; j < ((ICollection)items).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = items[j];
					if (xmlSchemaObject is XmlSchemaSequence)
					{
						this.Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaChoice)
					{
						this.Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaGroupRef)
					{
						this.Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaElement)
					{
						this.Write52_XmlSchemaElement("element", "http://www.w3.org/2001/XMLSchema", (XmlSchemaElement)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaAny)
					{
						this.Write46_XmlSchemaAny("any", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAny)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0002570C File Offset: 0x0002390C
		private void Write58_XmlSchemaComplexContent(string n, string ns, XmlSchemaComplexContent o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaComplexContent)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaComplexContent", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("mixed", "", XmlConvert.ToString(o.IsMixed));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			if (o.Content is XmlSchemaComplexContentRestriction)
			{
				this.Write57_Item("restriction", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexContentRestriction)o.Content, false, false);
			}
			else if (o.Content is XmlSchemaComplexContentExtension)
			{
				this.Write56_Item("extension", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexContentExtension)o.Content, false, false);
			}
			else if (o.Content != null)
			{
				throw base.CreateUnknownTypeException(o.Content);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00025858 File Offset: 0x00023A58
		private void Write56_Item(string n, string ns, XmlSchemaComplexContentExtension o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaComplexContentExtension)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaComplexContentExtension", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("base", "", base.FromXmlQualifiedName(o.BaseTypeName));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			if (o.Particle is XmlSchemaAll)
			{
				this.Write55_XmlSchemaAll("all", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAll)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaSequence)
			{
				this.Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaGroupRef)
			{
				this.Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)o.Particle, false, false);
			}
			else if (o.Particle != null)
			{
				throw base.CreateUnknownTypeException(o.Particle);
			}
			XmlSchemaObjectCollection attributes = o.Attributes;
			if (attributes != null)
			{
				for (int j = 0; j < ((ICollection)attributes).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = attributes[j];
					if (xmlSchemaObject is XmlSchemaAttribute)
					{
						this.Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
					{
						this.Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			this.Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00025A98 File Offset: 0x00023C98
		private void Write57_Item(string n, string ns, XmlSchemaComplexContentRestriction o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaComplexContentRestriction)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaComplexContentRestriction", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("base", "", base.FromXmlQualifiedName(o.BaseTypeName));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			if (o.Particle is XmlSchemaAll)
			{
				this.Write55_XmlSchemaAll("all", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAll)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaSequence)
			{
				this.Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)o.Particle, false, false);
			}
			else if (o.Particle is XmlSchemaGroupRef)
			{
				this.Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)o.Particle, false, false);
			}
			else if (o.Particle != null)
			{
				throw base.CreateUnknownTypeException(o.Particle);
			}
			XmlSchemaObjectCollection attributes = o.Attributes;
			if (attributes != null)
			{
				for (int j = 0; j < ((ICollection)attributes).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = attributes[j];
					if (xmlSchemaObject is XmlSchemaAttribute)
					{
						this.Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
					{
						this.Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			this.Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00025CD8 File Offset: 0x00023ED8
		private void Write61_XmlSchemaSimpleContent(string n, string ns, XmlSchemaSimpleContent o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleContent)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaSimpleContent", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			if (o.Content is XmlSchemaSimpleContentExtension)
			{
				this.Write60_Item("extension", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleContentExtension)o.Content, false, false);
			}
			else if (o.Content is XmlSchemaSimpleContentRestriction)
			{
				this.Write59_Item("restriction", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleContentRestriction)o.Content, false, false);
			}
			else if (o.Content != null)
			{
				throw base.CreateUnknownTypeException(o.Content);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00025E0C File Offset: 0x0002400C
		private void Write59_Item(string n, string ns, XmlSchemaSimpleContentRestriction o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleContentRestriction)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaSimpleContentRestriction", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("base", "", base.FromXmlQualifiedName(o.BaseTypeName));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			this.Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", o.BaseType, false, false);
			XmlSchemaObjectCollection facets = o.Facets;
			if (facets != null)
			{
				for (int j = 0; j < ((ICollection)facets).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = facets[j];
					if (xmlSchemaObject is XmlSchemaMinLengthFacet)
					{
						this.Write31_XmlSchemaMinLengthFacet("minLength", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinLengthFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMaxLengthFacet)
					{
						this.Write22_XmlSchemaMaxLengthFacet("maxLength", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxLengthFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaLengthFacet)
					{
						this.Write23_XmlSchemaLengthFacet("length", "http://www.w3.org/2001/XMLSchema", (XmlSchemaLengthFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaFractionDigitsFacet)
					{
						this.Write20_XmlSchemaFractionDigitsFacet("fractionDigits", "http://www.w3.org/2001/XMLSchema", (XmlSchemaFractionDigitsFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaTotalDigitsFacet)
					{
						this.Write24_XmlSchemaTotalDigitsFacet("totalDigits", "http://www.w3.org/2001/XMLSchema", (XmlSchemaTotalDigitsFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMinExclusiveFacet)
					{
						this.Write30_XmlSchemaMinExclusiveFacet("minExclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinExclusiveFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMaxInclusiveFacet)
					{
						this.Write27_XmlSchemaMaxInclusiveFacet("maxInclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxInclusiveFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMaxExclusiveFacet)
					{
						this.Write28_XmlSchemaMaxExclusiveFacet("maxExclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxExclusiveFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaMinInclusiveFacet)
					{
						this.Write21_XmlSchemaMinInclusiveFacet("minInclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinInclusiveFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaWhiteSpaceFacet)
					{
						this.Write29_XmlSchemaWhiteSpaceFacet("whiteSpace", "http://www.w3.org/2001/XMLSchema", (XmlSchemaWhiteSpaceFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaEnumerationFacet)
					{
						this.Write26_XmlSchemaEnumerationFacet("enumeration", "http://www.w3.org/2001/XMLSchema", (XmlSchemaEnumerationFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaPatternFacet)
					{
						this.Write25_XmlSchemaPatternFacet("pattern", "http://www.w3.org/2001/XMLSchema", (XmlSchemaPatternFacet)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			XmlSchemaObjectCollection attributes = o.Attributes;
			if (attributes != null)
			{
				for (int k = 0; k < ((ICollection)attributes).Count; k++)
				{
					XmlSchemaObject xmlSchemaObject2 = attributes[k];
					if (xmlSchemaObject2 is XmlSchemaAttribute)
					{
						this.Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 is XmlSchemaAttributeGroupRef)
					{
						this.Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject2, false, false);
					}
					else if (xmlSchemaObject2 != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject2);
					}
				}
			}
			this.Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000261A8 File Offset: 0x000243A8
		private void Write60_Item(string n, string ns, XmlSchemaSimpleContentExtension o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleContentExtension)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaSimpleContentExtension", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("base", "", base.FromXmlQualifiedName(o.BaseTypeName));
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			XmlSchemaObjectCollection attributes = o.Attributes;
			if (attributes != null)
			{
				for (int j = 0; j < ((ICollection)attributes).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = attributes[j];
					if (xmlSchemaObject is XmlSchemaAttribute)
					{
						this.Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
					{
						this.Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			this.Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00026320 File Offset: 0x00024520
		private void Write65_XmlSchemaNotation(string n, string ns, XmlSchemaNotation o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaNotation)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaNotation", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			base.WriteAttribute("public", "", o.Public);
			base.WriteAttribute("system", "", o.System);
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00026428 File Offset: 0x00024628
		private void Write40_XmlSchemaAttributeGroup(string n, string ns, XmlSchemaAttributeGroup o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaAttributeGroup)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaAttributeGroup", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("name", "", o.Name);
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			XmlSchemaObjectCollection attributes = o.Attributes;
			if (attributes != null)
			{
				for (int j = 0; j < ((ICollection)attributes).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = attributes[j];
					if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
					{
						this.Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaAttribute)
					{
						this.Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			this.Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00026598 File Offset: 0x00024798
		private void Write12_XmlSchemaInclude(string n, string ns, XmlSchemaInclude o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaInclude)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaInclude", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("schemaLocation", "", o.SchemaLocation);
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00026674 File Offset: 0x00024874
		private void Write13_XmlSchemaImport(string n, string ns, XmlSchemaImport o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaImport)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaImport", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("schemaLocation", "", o.SchemaLocation);
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("namespace", "", o.Namespace);
			this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00026764 File Offset: 0x00024964
		private void Write64_XmlSchemaRedefine(string n, string ns, XmlSchemaRedefine o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(XmlSchemaRedefine)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("XmlSchemaRedefine", "http://www.w3.org/2001/XMLSchema");
			}
			base.WriteAttribute("schemaLocation", "", o.SchemaLocation);
			base.WriteAttribute("id", "", o.Id);
			XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int j = 0; j < ((ICollection)items).Count; j++)
				{
					XmlSchemaObject xmlSchemaObject = items[j];
					if (xmlSchemaObject is XmlSchemaSimpleType)
					{
						this.Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleType)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaComplexType)
					{
						this.Write62_XmlSchemaComplexType("complexType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexType)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaGroup)
					{
						this.Write63_XmlSchemaGroup("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroup)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaAttributeGroup)
					{
						this.Write40_XmlSchemaAttributeGroup("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroup)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject is XmlSchemaAnnotation)
					{
						this.Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAnnotation)xmlSchemaObject, false, false);
					}
					else if (xmlSchemaObject != null)
					{
						throw base.CreateUnknownTypeException(xmlSchemaObject);
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002691C File Offset: 0x00024B1C
		private void Write4_Import(string n, string ns, Import o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(Import)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.WriteStartElement(n, ns, o, false, o.Namespaces);
			if (needType)
			{
				base.WriteXsiType("Import", "http://schemas.xmlsoap.org/wsdl/");
			}
			XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
			if (extensibleAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in extensibleAttributes)
				{
					base.WriteXmlAttribute(xmlAttribute, o);
				}
			}
			base.WriteAttribute("namespace", "", o.Namespace);
			base.WriteAttribute("location", "", o.Location);
			if (o.DocumentationElement != null || o.DocumentationElement == null)
			{
				base.WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", false, true);
				ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
				if (extensions != null)
				{
					for (int j = 0; j < ((ICollection)extensions).Count; j++)
					{
						if (!(extensions[j] is XmlNode) && extensions[j] != null)
						{
							throw base.CreateInvalidAnyTypeException(extensions[j]);
						}
						base.WriteElementLiteral((XmlNode)extensions[j], "", null, false, true);
					}
				}
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.DocumentationElement);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0000210D File Offset: 0x0000030D
		protected override void InitCallbacks()
		{
		}
	}
}
