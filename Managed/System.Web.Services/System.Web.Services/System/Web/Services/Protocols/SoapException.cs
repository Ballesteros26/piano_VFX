using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web.Services.Configuration;
using System.Xml;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the exception that is thrown when an XML Web service method is called over SOAP and an exception occurs.</summary>
	// Token: 0x02000061 RID: 97
	[Serializable]
	public class SoapException : SystemException
	{
		/// <summary>Returns a value that indicates whether the SOAP fault code is equivalent to the Server SOAP fault code regardless of the version of the SOAP protocol used.</summary>
		/// <returns>true if <paramref name="code" /> is equivalent to the Server SOAP fault code; otherwise, false.</returns>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that contains a SOAP fault code. </param>
		// Token: 0x06000253 RID: 595 RVA: 0x0000B8E9 File Offset: 0x00009AE9
		public static bool IsServerFaultCode(XmlQualifiedName code)
		{
			return code == SoapException.ServerFaultCode || code == Soap12FaultCodes.ReceiverFaultCode;
		}

		/// <summary>Returns a value that indicates whether the SOAP fault code is equivalent to the Client SOAP fault code regardless of the version of the SOAP protocol used.</summary>
		/// <returns>true if <paramref name="code" /> is equivalent to the Client SOAP fault code; otherwise, false.</returns>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that contains a SOAP fault code. </param>
		// Token: 0x06000254 RID: 596 RVA: 0x0000B905 File Offset: 0x00009B05
		public static bool IsClientFaultCode(XmlQualifiedName code)
		{
			return code == SoapException.ClientFaultCode || code == Soap12FaultCodes.SenderFaultCode;
		}

		/// <summary>Returns a value that indicates whether the SOAP fault code is equivalent to the VersionMismatch SOAP fault code regardless of the version of the SOAP protocol used.</summary>
		/// <returns>true if <paramref name="code" /> is equivalent to the VersionMismatch SOAP fault code; otherwise, false.</returns>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that contains a SOAP fault code. </param>
		// Token: 0x06000255 RID: 597 RVA: 0x0000B921 File Offset: 0x00009B21
		public static bool IsVersionMismatchFaultCode(XmlQualifiedName code)
		{
			return code == SoapException.VersionMismatchFaultCode || code == Soap12FaultCodes.VersionMismatchFaultCode;
		}

		/// <summary>Returns a value that indicates whether the SOAP fault code is equivalent to MustUnderstand regardless of the version of the SOAP protocol used.</summary>
		/// <returns>true if <paramref name="code" /> is equivalent to the MustUnderstand SOAP fault code; otherwise, false.</returns>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that contains a SOAP fault code. </param>
		// Token: 0x06000256 RID: 598 RVA: 0x0000B93D File Offset: 0x00009B3D
		public static bool IsMustUnderstandFaultCode(XmlQualifiedName code)
		{
			return code == SoapException.MustUnderstandFaultCode || code == Soap12FaultCodes.MustUnderstandFaultCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class.</summary>
		// Token: 0x06000257 RID: 599 RVA: 0x0000B959 File Offset: 0x00009B59
		public SoapException()
			: base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with the specified exception message, exception code, and URI that identifies the piece of code that caused the exception.</summary>
		/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property. </param>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property. </param>
		/// <param name="actor">A URI that identifies the code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property. </param>
		// Token: 0x06000258 RID: 600 RVA: 0x0000B96D File Offset: 0x00009B6D
		public SoapException(string message, XmlQualifiedName code, string actor)
			: base(message)
		{
			this.code = code;
			this.actor = actor;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with the specified exception message, exception code, URI that identifies the code that caused the exception, and reference to the root cause of the exception.</summary>
		/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property. </param>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property. </param>
		/// <param name="actor">A URI that identifies the piece of code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property. </param>
		/// <param name="innerException">An exception that is the root cause of the exception. This parameter sets the <see cref="P:System.Exception.InnerException" /> property. </param>
		// Token: 0x06000259 RID: 601 RVA: 0x0000B98F File Offset: 0x00009B8F
		public SoapException(string message, XmlQualifiedName code, string actor, Exception innerException)
			: base(message, innerException)
		{
			this.code = code;
			this.actor = actor;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with the specified exception message and exception code.</summary>
		/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property. </param>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property. </param>
		// Token: 0x0600025A RID: 602 RVA: 0x0000B9B3 File Offset: 0x00009BB3
		public SoapException(string message, XmlQualifiedName code)
			: base(message)
		{
			this.code = code;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with the specified exception message, exception code, and reference to the root cause of the exception.</summary>
		/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property. </param>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property. </param>
		/// <param name="innerException">An exception that is the root cause of the exception. This parameter sets the <see cref="P:System.Exception.InnerException" /> property. </param>
		// Token: 0x0600025B RID: 603 RVA: 0x0000B9CE File Offset: 0x00009BCE
		public SoapException(string message, XmlQualifiedName code, Exception innerException)
			: base(message, innerException)
		{
			this.code = code;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with the specified exception message, exception code, URI that identifies the piece of code that caused the exception, and application specific exception information.</summary>
		/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property. </param>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property. </param>
		/// <param name="actor">A URI that identifies the piece of code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property. </param>
		/// <param name="detail">An <see cref="T:System.Xml.XmlNode" /> that contains application specific exception information. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Detail" /> property. </param>
		// Token: 0x0600025C RID: 604 RVA: 0x0000B9EA File Offset: 0x00009BEA
		public SoapException(string message, XmlQualifiedName code, string actor, XmlNode detail)
			: base(message)
		{
			this.code = code;
			this.actor = actor;
			this.detail = detail;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with the specified exception message, exception code, URI that identifies the piece of code that caused the exception, application-specific exception information, and reference to the root cause of the exception.</summary>
		/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property. </param>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property. </param>
		/// <param name="actor">A URI that identifies the piece of code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property. </param>
		/// <param name="detail">An <see cref="T:System.Xml.XmlNode" /> that contains application specific exception information. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Detail" /> property. </param>
		/// <param name="innerException">An exception that is the root cause of the exception. This parameter sets the <see cref="P:System.Exception.InnerException" /> property. </param>
		// Token: 0x0600025D RID: 605 RVA: 0x0000BA14 File Offset: 0x00009C14
		public SoapException(string message, XmlQualifiedName code, string actor, XmlNode detail, Exception innerException)
			: base(message, innerException)
		{
			this.code = code;
			this.actor = actor;
			this.detail = detail;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with the specified exception message, exception code, and subcode.</summary>
		/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property.</param>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property.</param>
		/// <param name="subCode">An optional subcode for the SOAP fault. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.SubCode" /> property.</param>
		// Token: 0x0600025E RID: 606 RVA: 0x0000BA40 File Offset: 0x00009C40
		public SoapException(string message, XmlQualifiedName code, SoapFaultSubCode subCode)
			: base(message)
		{
			this.code = code;
			this.subCode = subCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with the specified exception message, exception code, URI that identifies the piece of code that caused the exception, application-specific exception information, and reference to the root cause of the exception.</summary>
		/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property.</param>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property.</param>
		/// <param name="actor">A URI that identifies the piece of code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property.</param>
		/// <param name="role">A URI that represents the XML Web service's function in processing the SOAP message. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Role" /> property.</param>
		/// <param name="detail">An <see cref="T:System.Xml.XmlNode" /> that contains application specific exception information. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Detail" /> property.</param>
		/// <param name="subCode">An optional subcode for the SOAP fault. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.SubCode" /> property.</param>
		/// <param name="innerException">An exception that is the root cause of the exception. This parameter sets the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x0600025F RID: 607 RVA: 0x0000BA62 File Offset: 0x00009C62
		public SoapException(string message, XmlQualifiedName code, string actor, string role, XmlNode detail, SoapFaultSubCode subCode, Exception innerException)
			: base(message, innerException)
		{
			this.code = code;
			this.actor = actor;
			this.role = role;
			this.detail = detail;
			this.subCode = subCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with the specified exception message, exception code, URI that identifies the piece of code that caused the exception, URI that represents the XML Web service's function in processing the SOAP message, the human language associated with the exception, the application-specific exception information, the subcode for the SOAP fault and reference to the root cause of the exception.</summary>
		/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property.</param>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property.</param>
		/// <param name="actor">A URI that identifies the piece of code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property.</param>
		/// <param name="role">A URI that represents the XML Web service's function in processing the SOAP message. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Role" /> property.</param>
		/// <param name="lang">A human language associated with the exception. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Lang" /> property.</param>
		/// <param name="detail">An <see cref="T:System.Xml.XmlNode" /> that contains application specific exception information. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Detail" /> property.</param>
		/// <param name="subCode">An optional subcode for the SOAP fault. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.SubCode" /> property.</param>
		/// <param name="innerException">An exception that is the root cause of the exception. This parameter sets the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x06000260 RID: 608 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		public SoapException(string message, XmlQualifiedName code, string actor, string role, string lang, XmlNode detail, SoapFaultSubCode subCode, Exception innerException)
			: base(message, innerException)
		{
			this.code = code;
			this.actor = actor;
			this.role = role;
			this.detail = detail;
			this.lang = lang;
			this.subCode = subCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The T:System.Runtime.Serialization.StreamingContext  that contains contextual information about the source or destination.</param>
		// Token: 0x06000261 RID: 609 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		protected SoapException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IDictionary data = base.Data;
			this.code = (XmlQualifiedName)data["code"];
			this.actor = (string)data["actor"];
			this.role = (string)data["role"];
			this.subCode = (SoapFaultSubCode)data["subCode"];
			this.lang = (string)data["lang"];
		}

		/// <summary>Gets the piece of code that caused the exception.</summary>
		/// <returns>The piece of code that caused the exception.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000BB85 File Offset: 0x00009D85
		public string Actor
		{
			get
			{
				if (this.actor != null)
				{
					return this.actor;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the type of SOAP fault code.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the SOAP fault code that occurred.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000BB9B File Offset: 0x00009D9B
		public XmlQualifiedName Code
		{
			get
			{
				return this.code;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlNode" /> that represents the application-specific error information details.</summary>
		/// <returns>The application-specific error information.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000BBA3 File Offset: 0x00009DA3
		public XmlNode Detail
		{
			get
			{
				return this.detail;
			}
		}

		/// <summary>Gets the human language associated with the exception.</summary>
		/// <returns>A <see cref="T:System.String" /> value that identifies the human language associated with the exception.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000BBAB File Offset: 0x00009DAB
		[ComVisible(false)]
		public string Lang
		{
			get
			{
				if (this.lang != null)
				{
					return this.lang;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a URI that represents the piece of code that caused the exception.</summary>
		/// <returns>The piece of code that caused the exception.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000BB85 File Offset: 0x00009D85
		[ComVisible(false)]
		public string Node
		{
			get
			{
				if (this.actor != null)
				{
					return this.actor;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a URI that represents the XML Web service's function in processing the SOAP message.</summary>
		/// <returns>The role of the XML Web service throwing the <see cref="T:System.Web.Services.Protocols.SoapException" />. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000BBC1 File Offset: 0x00009DC1
		[ComVisible(false)]
		public string Role
		{
			get
			{
				if (this.role != null)
				{
					return this.role;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the optional error information contained in the subcode XML element of a SOAP fault.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapFaultSubcode" /> that represents the contents of the subcode XML element of a SOAP fault.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000BBD7 File Offset: 0x00009DD7
		[ComVisible(false)]
		public SoapFaultSubCode SubCode
		{
			get
			{
				return this.subCode;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BBDF File Offset: 0x00009DDF
		internal void ClearSubCode()
		{
			if (this.subCode != null)
			{
				this.subCode = this.subCode.SubCode;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization." /><see cref="SerializationInfo" /> with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x0600026A RID: 618 RVA: 0x0000BBFC File Offset: 0x00009DFC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			IDictionary data = this.Data;
			data["code"] = this.Code;
			data["actor"] = this.Actor;
			data["role"] = this.Role;
			data["subCode"] = this.SubCode;
			data["lang"] = this.Lang;
			base.GetObjectData(info, context);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BC6B File Offset: 0x00009E6B
		private static SoapException CreateSuppressedException(SoapProtocolVersion soapVersion, string message, Exception innerException)
		{
			return new SoapException(Res.GetString("WebSuppressedExceptionMessage"), (soapVersion == SoapProtocolVersion.Soap12) ? new XmlQualifiedName("Receiver", "http://www.w3.org/2003/05/soap-envelope") : new XmlQualifiedName("Server", "http://schemas.xmlsoap.org/soap/envelope/"));
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		internal static SoapException Create(SoapProtocolVersion soapVersion, string message, XmlQualifiedName code, string actor, string role, XmlNode detail, SoapFaultSubCode subCode, Exception innerException)
		{
			if (WebServicesSection.Current.Diagnostics.SuppressReturningExceptions)
			{
				return SoapException.CreateSuppressedException(soapVersion, Res.GetString("WebSuppressedExceptionMessage"), innerException);
			}
			return new SoapException(message, code, actor, role, detail, subCode, innerException);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000BCD6 File Offset: 0x00009ED6
		internal static SoapException Create(SoapProtocolVersion soapVersion, string message, XmlQualifiedName code, Exception innerException)
		{
			if (WebServicesSection.Current.Diagnostics.SuppressReturningExceptions)
			{
				return SoapException.CreateSuppressedException(soapVersion, Res.GetString("WebSuppressedExceptionMessage"), innerException);
			}
			return new SoapException(message, code, innerException);
		}

		// Token: 0x04000264 RID: 612
		private XmlQualifiedName code = XmlQualifiedName.Empty;

		// Token: 0x04000265 RID: 613
		private string actor;

		// Token: 0x04000266 RID: 614
		private string role;

		// Token: 0x04000267 RID: 615
		private XmlNode detail;

		// Token: 0x04000268 RID: 616
		private SoapFaultSubCode subCode;

		// Token: 0x04000269 RID: 617
		private string lang;

		/// <summary>Specifies that a SOAP fault code that represents an error occurred during the processing of a client call on the server, where the problem is not due to the message contents.</summary>
		// Token: 0x0400026A RID: 618
		public static readonly XmlQualifiedName ServerFaultCode = new XmlQualifiedName("Server", "http://schemas.xmlsoap.org/soap/envelope/");

		/// <summary>Specifies a SOAP fault code that represents a client call that is not formatted correctly or does not contain the appropriate information.</summary>
		// Token: 0x0400026B RID: 619
		public static readonly XmlQualifiedName ClientFaultCode = new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/");

		/// <summary>A SOAP fault code that represents an invalid namespace for a SOAP envelope was found during the processing of the SOAP message.</summary>
		// Token: 0x0400026C RID: 620
		public static readonly XmlQualifiedName VersionMismatchFaultCode = new XmlQualifiedName("VersionMismatch", "http://schemas.xmlsoap.org/soap/envelope/");

		/// <summary>A SOAP Fault Code that represents a SOAP element marked with the MustUnderstand attribute was not processed.</summary>
		// Token: 0x0400026D RID: 621
		public static readonly XmlQualifiedName MustUnderstandFaultCode = new XmlQualifiedName("MustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/");

		/// <summary>Gets an <see cref="T:System.Xml.XmlQualifiedName" /> that represents the <see cref="P:System.Web.Services.Protocols.SoapException.Detail" /> element of a SOAP Fault code.</summary>
		// Token: 0x0400026E RID: 622
		public static readonly XmlQualifiedName DetailElementName = new XmlQualifiedName("detail", "");
	}
}
