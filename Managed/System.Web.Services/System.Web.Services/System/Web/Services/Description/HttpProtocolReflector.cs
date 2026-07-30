using System;
using System.Reflection;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;

namespace System.Web.Services.Description
{
	// Token: 0x020000CA RID: 202
	internal abstract class HttpProtocolReflector : ProtocolReflector
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x000185F0 File Offset: 0x000167F0
		protected HttpProtocolReflector()
		{
			Type[] mimeReflectorTypes = WebServicesSection.Current.MimeReflectorTypes;
			this.reflectors = new MimeReflector[mimeReflectorTypes.Length];
			for (int i = 0; i < this.reflectors.Length; i++)
			{
				MimeReflector mimeReflector = (MimeReflector)Activator.CreateInstance(mimeReflectorTypes[i]);
				mimeReflector.ReflectionContext = this;
				this.reflectors[i] = mimeReflector;
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00018650 File Offset: 0x00016850
		protected bool ReflectMimeParameters()
		{
			bool flag = false;
			for (int i = 0; i < this.reflectors.Length; i++)
			{
				if (this.reflectors[i].ReflectParameters())
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00018684 File Offset: 0x00016884
		protected bool ReflectMimeReturn()
		{
			if (base.Method.ReturnType == typeof(void))
			{
				Message outputMessage = base.OutputMessage;
				return true;
			}
			bool flag = false;
			for (int i = 0; i < this.reflectors.Length; i++)
			{
				if (this.reflectors[i].ReflectReturn())
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000186DF File Offset: 0x000168DF
		protected bool ReflectUrlParameters()
		{
			if (!HttpServerProtocol.AreUrlParametersSupported(base.Method))
			{
				return false;
			}
			this.ReflectStringParametersMessage();
			base.OperationBinding.Input.Extensions.Add(new HttpUrlEncodedBinding());
			return true;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00018714 File Offset: 0x00016914
		internal void ReflectStringParametersMessage()
		{
			Message inputMessage = base.InputMessage;
			foreach (ParameterInfo parameterInfo in base.Method.InParameters)
			{
				MessagePart messagePart = new MessagePart();
				messagePart.Name = XmlConvert.EncodeLocalName(parameterInfo.Name);
				if (parameterInfo.ParameterType.IsArray)
				{
					string text = base.DefaultNamespace;
					if (text.EndsWith("/", StringComparison.Ordinal))
					{
						text += "AbstractTypes";
					}
					else
					{
						text += "/AbstractTypes";
					}
					string text2 = "StringArray";
					if (!base.ServiceDescription.Types.Schemas.Contains(text))
					{
						XmlSchema xmlSchema = new XmlSchema();
						xmlSchema.TargetNamespace = text;
						base.ServiceDescription.Types.Schemas.Add(xmlSchema);
						XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
						xmlSchemaElement.Name = "String";
						xmlSchemaElement.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
						xmlSchemaElement.MinOccurs = 0m;
						xmlSchemaElement.MaxOccurs = decimal.MaxValue;
						XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
						xmlSchemaSequence.Items.Add(xmlSchemaElement);
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = new XmlSchemaComplexContentRestriction();
						xmlSchemaComplexContentRestriction.BaseTypeName = new XmlQualifiedName("Array", "http://schemas.xmlsoap.org/soap/encoding/");
						xmlSchemaComplexContentRestriction.Particle = xmlSchemaSequence;
						XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
						xmlSchemaImport.Namespace = xmlSchemaComplexContentRestriction.BaseTypeName.Namespace;
						XmlSchemaComplexContent xmlSchemaComplexContent = new XmlSchemaComplexContent();
						xmlSchemaComplexContent.Content = xmlSchemaComplexContentRestriction;
						XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
						xmlSchemaComplexType.Name = text2;
						xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent;
						xmlSchema.Items.Add(xmlSchemaComplexType);
						xmlSchema.Includes.Add(xmlSchemaImport);
					}
					messagePart.Type = new XmlQualifiedName(text2, text);
				}
				else
				{
					messagePart.Type = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
				}
				inputMessage.Parts.Add(messagePart);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00018910 File Offset: 0x00016B10
		internal string MethodUrl
		{
			get
			{
				string text = base.Method.MethodAttribute.MessageName;
				if (text.Length == 0)
				{
					text = base.Method.Name;
				}
				return "/" + text;
			}
		}

		// Token: 0x04000383 RID: 899
		private MimeReflector[] reflectors;
	}
}
