using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the data in a SOAP request received or a SOAP response sent by an XML Web service method at a specific <see cref="T:System.Web.Services.Protocols.SoapMessageStage" />. This class cannot be inherited.</summary>
	// Token: 0x0200007B RID: 123
	public sealed class SoapServerMessage : SoapMessage
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0000E52A File Offset: 0x0000C72A
		internal SoapServerMessage(SoapServerProtocol protocol)
		{
			this.protocol = protocol;
		}

		/// <summary>Gets a value indicating whether the client waits for the server to finish processing an XML Web service method.</summary>
		/// <returns>true if the client waits for the server to completely process a method; otherwise, false.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000E539 File Offset: 0x0000C739
		public override bool OneWay
		{
			get
			{
				return this.protocol.ServerMethod.oneWay;
			}
		}

		/// <summary>Gets the base URL of the XML Web service.</summary>
		/// <returns>The base URL of the XML Web service.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000E54B File Offset: 0x0000C74B
		public override string Url
		{
			get
			{
				return RuntimeUtils.EscapeUri(this.protocol.Request.Url);
			}
		}

		/// <summary>Gets the SOAPAction HTTP request header field for the SOAP request or SOAP response.</summary>
		/// <returns>The SOAPAction HTTP request header field for the SOAP request or SOAP response.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000E562 File Offset: 0x0000C762
		public override string Action
		{
			get
			{
				return this.protocol.ServerMethod.action;
			}
		}

		/// <summary>Gets the version of the SOAP protocol used to communicate with the XML Web service.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Default" />.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000E574 File Offset: 0x0000C774
		[ComVisible(false)]
		public override SoapProtocolVersion SoapVersion
		{
			get
			{
				return this.protocol.Version;
			}
		}

		/// <summary>Gets the instance of the class handling the method invocation on the Web server.</summary>
		/// <returns>The instance of the class implementing the XML Web service.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> is not <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterDeserialize" /> or <see cref="F:System.Web.Services.Protocols.SoapMessageStage.BeforeSerialize" />. </exception>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000E581 File Offset: 0x0000C781
		public object Server
		{
			get
			{
				base.EnsureStage((SoapMessageStage)9);
				return this.protocol.Target;
			}
		}

		/// <summary>Gets a representation of the method prototype for the XML Web service method for which the SOAP request is intended.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> representing the XML Web service method for which the SOAP request is intended.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000E596 File Offset: 0x0000C796
		public override LogicalMethodInfo MethodInfo
		{
			get
			{
				return this.protocol.MethodInfo;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009E49 File Offset: 0x00008049
		protected override void EnsureOutStage()
		{
			base.EnsureStage(SoapMessageStage.BeforeSerialize);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009E40 File Offset: 0x00008040
		protected override void EnsureInStage()
		{
			base.EnsureStage(SoapMessageStage.AfterDeserialize);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00003846 File Offset: 0x00001A46
		internal SoapServerMessage()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040002D5 RID: 725
		private SoapServerProtocol protocol;

		// Token: 0x040002D6 RID: 726
		internal SoapExtension[] highPriConfigExtensions;

		// Token: 0x040002D7 RID: 727
		internal SoapExtension[] otherExtensions;

		// Token: 0x040002D8 RID: 728
		internal SoapExtension[] allExtensions;
	}
}
