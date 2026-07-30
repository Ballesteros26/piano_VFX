using System;
using System.Net;
using System.Web.Services.Configuration;
using System.Xml;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000081 RID: 129
	internal abstract class SoapServerProtocolHelper
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0001079E File Offset: 0x0000E99E
		protected SoapServerProtocolHelper(SoapServerProtocol protocol)
		{
			this.protocol = protocol;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000107AD File Offset: 0x0000E9AD
		protected SoapServerProtocolHelper(SoapServerProtocol protocol, string requestNamespace)
		{
			this.protocol = protocol;
			this.requestNamespace = requestNamespace;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000107C4 File Offset: 0x0000E9C4
		internal static SoapServerProtocolHelper GetHelper(SoapServerProtocol protocol, string envelopeNs)
		{
			SoapServerProtocolHelper soapServerProtocolHelper;
			if (envelopeNs == "http://schemas.xmlsoap.org/soap/envelope/")
			{
				soapServerProtocolHelper = new Soap11ServerProtocolHelper(protocol, envelopeNs);
			}
			else if (envelopeNs == "http://www.w3.org/2003/05/soap-envelope")
			{
				soapServerProtocolHelper = new Soap12ServerProtocolHelper(protocol, envelopeNs);
			}
			else
			{
				soapServerProtocolHelper = new Soap11ServerProtocolHelper(protocol, envelopeNs);
			}
			return soapServerProtocolHelper;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00010808 File Offset: 0x0000EA08
		internal HttpStatusCode SetResponseErrorCode(HttpResponse response, SoapException soapException)
		{
			if (soapException.SubCode != null && soapException.SubCode.Code == Soap12FaultCodes.UnsupportedMediaTypeFaultCode)
			{
				response.StatusCode = 415;
				soapException.ClearSubCode();
			}
			else if (SoapException.IsClientFaultCode(soapException.Code))
			{
				global::System.Web.Services.Protocols.ServerProtocol.SetHttpResponseStatusCode(response, 500);
				for (Exception ex = soapException; ex != null; ex = ex.InnerException)
				{
					if (ex is XmlException)
					{
						response.StatusCode = 400;
					}
				}
			}
			else
			{
				global::System.Web.Services.Protocols.ServerProtocol.SetHttpResponseStatusCode(response, 500);
			}
			response.StatusDescription = HttpWorkerRequest.GetStatusDescription(response.StatusCode);
			return (HttpStatusCode)response.StatusCode;
		}

		// Token: 0x06000376 RID: 886
		internal abstract void WriteFault(XmlWriter writer, SoapException soapException, HttpStatusCode statusCode);

		// Token: 0x06000377 RID: 887
		internal abstract SoapServerMethod RouteRequest();

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000378 RID: 888
		internal abstract SoapProtocolVersion Version { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000379 RID: 889
		internal abstract WebServiceProtocols Protocol { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600037A RID: 890
		internal abstract string EnvelopeNs { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600037B RID: 891
		internal abstract string EncodingNs { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600037C RID: 892
		internal abstract string HttpContentType { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000108A4 File Offset: 0x0000EAA4
		internal string RequestNamespace
		{
			get
			{
				return this.requestNamespace;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000108AC File Offset: 0x0000EAAC
		protected SoapServerProtocol ServerProtocol
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000108B4 File Offset: 0x0000EAB4
		protected SoapServerType ServerType
		{
			get
			{
				return (SoapServerType)this.protocol.ServerType;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000108C8 File Offset: 0x0000EAC8
		protected XmlQualifiedName GetRequestElement()
		{
			SoapServerMessage message = this.ServerProtocol.Message;
			long position = message.Stream.Position;
			XmlReader xmlReader = this.protocol.GetXmlReader();
			xmlReader.MoveToContent();
			this.requestNamespace = xmlReader.NamespaceURI;
			if (!xmlReader.IsStartElement("Envelope", this.requestNamespace))
			{
				throw new InvalidOperationException(Res.GetString("WebMissingEnvelopeElement"));
			}
			if (xmlReader.IsEmptyElement)
			{
				throw new InvalidOperationException(Res.GetString("WebMissingBodyElement"));
			}
			xmlReader.ReadStartElement("Envelope", this.requestNamespace);
			xmlReader.MoveToContent();
			while (!xmlReader.EOF && !xmlReader.IsStartElement("Body", this.requestNamespace))
			{
				xmlReader.Skip();
			}
			if (xmlReader.EOF)
			{
				throw new InvalidOperationException(Res.GetString("WebMissingBodyElement"));
			}
			XmlQualifiedName xmlQualifiedName;
			if (xmlReader.IsEmptyElement)
			{
				xmlQualifiedName = XmlQualifiedName.Empty;
			}
			else
			{
				xmlReader.ReadStartElement("Body", this.requestNamespace);
				xmlReader.MoveToContent();
				xmlQualifiedName = new XmlQualifiedName(xmlReader.LocalName, xmlReader.NamespaceURI);
			}
			message.Stream.Position = position;
			return xmlQualifiedName;
		}

		// Token: 0x040002FB RID: 763
		private SoapServerProtocol protocol;

		// Token: 0x040002FC RID: 764
		private string requestNamespace;
	}
}
