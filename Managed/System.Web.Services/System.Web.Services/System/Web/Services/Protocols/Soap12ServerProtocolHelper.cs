using System;
using System.Net;
using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Xml;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000059 RID: 89
	internal class Soap12ServerProtocolHelper : SoapServerProtocolHelper
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00009355 File Offset: 0x00007555
		internal Soap12ServerProtocolHelper(SoapServerProtocol protocol)
			: base(protocol)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000935E File Offset: 0x0000755E
		internal Soap12ServerProtocolHelper(SoapServerProtocol protocol, string requestNamespace)
			: base(protocol, requestNamespace)
		{
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009860 File Offset: 0x00007A60
		internal override SoapProtocolVersion Version
		{
			get
			{
				return SoapProtocolVersion.Soap12;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00009863 File Offset: 0x00007A63
		internal override WebServiceProtocols Protocol
		{
			get
			{
				return WebServiceProtocols.HttpSoap12;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009867 File Offset: 0x00007A67
		internal override string EnvelopeNs
		{
			get
			{
				return "http://www.w3.org/2003/05/soap-envelope";
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000986E File Offset: 0x00007A6E
		internal override string EncodingNs
		{
			get
			{
				return "http://www.w3.org/2003/05/soap-encoding";
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009875 File Offset: 0x00007A75
		internal override string HttpContentType
		{
			get
			{
				return "application/soap+xml";
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000987C File Offset: 0x00007A7C
		internal override SoapServerMethod RouteRequest()
		{
			string text = ContentType.GetAction(base.ServerProtocol.Request.ContentType);
			SoapServerMethod soapServerMethod = null;
			bool flag = false;
			bool flag2 = false;
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "RouteRequest", Array.Empty<object>()) : null);
			if (text != null && text.Length > 0)
			{
				text = HttpUtility.UrlDecode(text);
				if (Tracing.On)
				{
					Tracing.Enter("RouteRequest", traceMethod, new TraceMethod(base.ServerType, "GetMethod", new object[] { text }), Tracing.Details(base.ServerProtocol.Request));
				}
				soapServerMethod = base.ServerType.GetMethod(text);
				if (Tracing.On)
				{
					Tracing.Exit("RouteRequest", traceMethod);
				}
				if (soapServerMethod != null && base.ServerType.GetDuplicateMethod(text) != null)
				{
					soapServerMethod = null;
					flag = true;
				}
			}
			XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Empty;
			if (soapServerMethod == null)
			{
				xmlQualifiedName = base.GetRequestElement();
				if (Tracing.On)
				{
					Tracing.Enter("RouteRequest", traceMethod, new TraceMethod(base.ServerType, "GetMethod", new object[] { xmlQualifiedName }), Tracing.Details(base.ServerProtocol.Request));
				}
				soapServerMethod = base.ServerType.GetMethod(xmlQualifiedName);
				if (Tracing.On)
				{
					Tracing.Exit("RouteRequest", traceMethod);
				}
				if (soapServerMethod != null && base.ServerType.GetDuplicateMethod(xmlQualifiedName) != null)
				{
					soapServerMethod = null;
					flag2 = true;
				}
			}
			if (soapServerMethod != null)
			{
				return soapServerMethod;
			}
			if (text == null || text.Length == 0)
			{
				throw new SoapException(Res.GetString("UnableToHandleRequestActionRequired0"), Soap12FaultCodes.SenderFaultCode);
			}
			if (!flag)
			{
				throw new SoapException(Res.GetString("UnableToHandleRequestActionNotRecognized1", new object[] { text }), Soap12FaultCodes.SenderFaultCode);
			}
			if (flag2)
			{
				throw new SoapException(Res.GetString("UnableToHandleRequest0"), Soap12FaultCodes.ReceiverFaultCode);
			}
			throw new SoapException(Res.GetString("TheRequestElementXmlnsWasNotRecognized2", new object[] { xmlQualifiedName.Name, xmlQualifiedName.Namespace }), Soap12FaultCodes.SenderFaultCode);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009A60 File Offset: 0x00007C60
		internal override void WriteFault(XmlWriter writer, SoapException soapException, HttpStatusCode statusCode)
		{
			if (statusCode != HttpStatusCode.InternalServerError)
			{
				return;
			}
			if (soapException == null)
			{
				return;
			}
			writer.WriteStartDocument();
			writer.WriteStartElement("soap", "Envelope", "http://www.w3.org/2003/05/soap-envelope");
			writer.WriteAttributeString("xmlns", "soap", null, "http://www.w3.org/2003/05/soap-envelope");
			writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
			if (base.ServerProtocol.ServerMethod != null)
			{
				SoapHeaderHandling.WriteHeaders(writer, base.ServerProtocol.ServerMethod.outHeaderSerializer, base.ServerProtocol.Message.Headers, base.ServerProtocol.ServerMethod.outHeaderMappings, SoapHeaderDirection.Fault, base.ServerProtocol.ServerMethod.use == SoapBindingUse.Encoded, base.ServerType.serviceNamespace, base.ServerType.serviceDefaultIsEncoded, "http://www.w3.org/2003/05/soap-envelope");
			}
			else
			{
				SoapHeaderHandling.WriteUnknownHeaders(writer, base.ServerProtocol.Message.Headers, "http://www.w3.org/2003/05/soap-envelope");
			}
			writer.WriteStartElement("Body", "http://www.w3.org/2003/05/soap-envelope");
			writer.WriteStartElement("Fault", "http://www.w3.org/2003/05/soap-envelope");
			writer.WriteStartElement("Code", "http://www.w3.org/2003/05/soap-envelope");
			Soap12ServerProtocolHelper.WriteFaultCodeValue(writer, Soap12ServerProtocolHelper.TranslateFaultCode(soapException.Code), soapException.SubCode);
			writer.WriteEndElement();
			writer.WriteStartElement("Reason", "http://www.w3.org/2003/05/soap-envelope");
			writer.WriteStartElement("Text", "http://www.w3.org/2003/05/soap-envelope");
			writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", Res.GetString("XmlLang"));
			writer.WriteString(base.ServerProtocol.GenerateFaultString(soapException));
			writer.WriteEndElement();
			writer.WriteEndElement();
			string actor = soapException.Actor;
			if (actor.Length > 0)
			{
				writer.WriteElementString("Node", "http://www.w3.org/2003/05/soap-envelope", actor);
			}
			string role = soapException.Role;
			if (role.Length > 0)
			{
				writer.WriteElementString("Role", "http://www.w3.org/2003/05/soap-envelope", role);
			}
			if (!(soapException is SoapHeaderException))
			{
				if (soapException.Detail == null)
				{
					writer.WriteStartElement("Detail", "http://www.w3.org/2003/05/soap-envelope");
					writer.WriteEndElement();
				}
				else
				{
					soapException.Detail.WriteTo(writer);
				}
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.Flush();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009CA0 File Offset: 0x00007EA0
		private static void WriteFaultCodeValue(XmlWriter writer, XmlQualifiedName code, SoapFaultSubCode subcode)
		{
			if (code == null)
			{
				return;
			}
			writer.WriteStartElement("Value", "http://www.w3.org/2003/05/soap-envelope");
			if (code.Namespace != null && code.Namespace.Length > 0 && writer.LookupPrefix(code.Namespace) == null)
			{
				writer.WriteAttributeString("xmlns", "q0", null, code.Namespace);
			}
			writer.WriteQualifiedName(code.Name, code.Namespace);
			writer.WriteEndElement();
			if (subcode != null)
			{
				writer.WriteStartElement("Subcode", "http://www.w3.org/2003/05/soap-envelope");
				Soap12ServerProtocolHelper.WriteFaultCodeValue(writer, subcode.Code, subcode.SubCode);
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009D48 File Offset: 0x00007F48
		private static XmlQualifiedName TranslateFaultCode(XmlQualifiedName code)
		{
			if (code.Namespace == "http://schemas.xmlsoap.org/soap/envelope/")
			{
				if (code.Name == "Server")
				{
					return Soap12FaultCodes.ReceiverFaultCode;
				}
				if (code.Name == "Client")
				{
					return Soap12FaultCodes.SenderFaultCode;
				}
				if (code.Name == "MustUnderstand")
				{
					return Soap12FaultCodes.MustUnderstandFaultCode;
				}
				if (code.Name == "VersionMismatch")
				{
					return Soap12FaultCodes.VersionMismatchFaultCode;
				}
			}
			return code;
		}
	}
}
