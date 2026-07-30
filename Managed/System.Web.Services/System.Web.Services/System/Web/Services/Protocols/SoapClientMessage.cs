using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the data in a SOAP request sent or a SOAP response received by an XML Web service client at a specific <see cref="T:System.Web.Services.Protocols.SoapMessageStage" />. This class cannot be inherited.</summary>
	// Token: 0x0200005A RID: 90
	public sealed class SoapClientMessage : SoapMessage
	{
		// Token: 0x0600020B RID: 523 RVA: 0x00009DC8 File Offset: 0x00007FC8
		internal SoapClientMessage(SoapHttpClientProtocol protocol, SoapClientMethod method, string url)
		{
			this.method = method;
			this.protocol = protocol;
			this.url = url;
		}

		/// <summary>Gets a value indicating whether the client waits for the server to finish processing an XML Web service method.</summary>
		/// <returns>true if the client does not wait for the server to completely process a method.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009DE5 File Offset: 0x00007FE5
		public override bool OneWay
		{
			get
			{
				return this.method.oneWay;
			}
		}

		/// <summary>Gets an instance of the client proxy class, which derives from <see cref="T:System.Web.Services.Protocols.SoapHttpClientProtocol" />.</summary>
		/// <returns>An instance of the client proxy class.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009DF2 File Offset: 0x00007FF2
		public SoapHttpClientProtocol Client
		{
			get
			{
				return this.protocol;
			}
		}

		/// <summary>Gets a representation of the method prototype of the XML Web service method for which the SOAP request is intended.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> representing the XML Web service method for which the SOAP request is intended.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00009DFA File Offset: 0x00007FFA
		public override LogicalMethodInfo MethodInfo
		{
			get
			{
				return this.method.methodInfo;
			}
		}

		/// <summary>Gets the URL of the XML Web service.</summary>
		/// <returns>The URL of the XML Web service.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00009E07 File Offset: 0x00008007
		public override string Url
		{
			get
			{
				return this.url;
			}
		}

		/// <summary>Gets the SOAPAction HTTP request header field for the SOAP request or SOAP response.</summary>
		/// <returns>The SOAPAction HTTP request header field for the SOAP request or SOAP response.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00009E0F File Offset: 0x0000800F
		public override string Action
		{
			get
			{
				return this.method.action;
			}
		}

		/// <summary>Gets the version of the SOAP protocol used to communicate with the XML Web service.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Default" />.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00009E1C File Offset: 0x0000801C
		[ComVisible(false)]
		public override SoapProtocolVersion SoapVersion
		{
			get
			{
				if (this.protocol.SoapVersion != SoapProtocolVersion.Default)
				{
					return this.protocol.SoapVersion;
				}
				return SoapProtocolVersion.Soap11;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00009E38 File Offset: 0x00008038
		internal SoapClientMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009E40 File Offset: 0x00008040
		protected override void EnsureOutStage()
		{
			base.EnsureStage(SoapMessageStage.AfterDeserialize);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009E49 File Offset: 0x00008049
		protected override void EnsureInStage()
		{
			base.EnsureStage(SoapMessageStage.BeforeSerialize);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00003846 File Offset: 0x00001A46
		internal SoapClientMessage()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000239 RID: 569
		private SoapClientMethod method;

		// Token: 0x0400023A RID: 570
		private SoapHttpClientProtocol protocol;

		// Token: 0x0400023B RID: 571
		private string url;

		// Token: 0x0400023C RID: 572
		internal SoapExtension[] initializedExtensions;
	}
}
