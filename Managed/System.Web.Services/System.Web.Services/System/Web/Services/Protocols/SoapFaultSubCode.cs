using System;
using System.Xml;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the contents of the optional Subcode element of a SOAP fault when SOAP version 1.2 is used to communicate between a client and an XML Web service.</summary>
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class SoapFaultSubCode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapFaultSubcode" /> class sets the application specific error code.</summary>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> specifying the application specific error code. Sets the <see cref="P:System.Web.Services.Protocols.SoapFaultSubcode.Code" /> property. </param>
		// Token: 0x06000292 RID: 658 RVA: 0x0000C081 File Offset: 0x0000A281
		public SoapFaultSubCode(XmlQualifiedName code)
			: this(code, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapFaultSubcode" /> class setting the application specific error code and additional error information.</summary>
		/// <param name="code">An <see cref="T:System.Xml.XmlQualifiedName" /> specifying the application specific error code. Sets the <see cref="P:System.Web.Services.Protocols.SoapFaultSubcode.Code" /> property. </param>
		/// <param name="subCode">A <see cref="T:System.Web.Services.Protocols.SoapFaultSubcode" /> specifying additional application specific error information. Sets the <see cref="P:System.Web.Services.Protocols.SoapFaultSubcode.Subcode" /> property. </param>
		// Token: 0x06000293 RID: 659 RVA: 0x0000C08B File Offset: 0x0000A28B
		public SoapFaultSubCode(XmlQualifiedName code, SoapFaultSubCode subCode)
		{
			this.code = code;
			this.subCode = subCode;
		}

		/// <summary>Gets the application specific error code in the form of an XML qualified name.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> representing the application specific error code.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public XmlQualifiedName Code
		{
			get
			{
				return this.code;
			}
		}

		/// <summary>Gets additional error information contained within a child Subcode element.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapFaultSubcode" /> containing additional error information.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000C0A9 File Offset: 0x0000A2A9
		public SoapFaultSubCode SubCode
		{
			get
			{
				return this.subCode;
			}
		}

		// Token: 0x0400027D RID: 637
		private XmlQualifiedName code;

		// Token: 0x0400027E RID: 638
		private SoapFaultSubCode subCode;
	}
}
