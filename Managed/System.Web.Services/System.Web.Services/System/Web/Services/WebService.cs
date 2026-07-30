using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Services.Protocols;
using System.Web.SessionState;

namespace System.Web.Services
{
	/// <summary>Defines the optional base class for XML Web services, which provides direct access to common ASP.NET objects, such as application and session state.</summary>
	// Token: 0x02000011 RID: 17
	public class WebService : MarshalByValueComponent
	{
		/// <summary>Gets the application object for the current HTTP request.</summary>
		/// <returns>An <see cref="T:System.Web.HttpApplicationState" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlThread, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Web.AspNetHostingPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Level="Minimal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026F5 File Offset: 0x000008F5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("The ASP.NET application object for the current request.")]
		public HttpApplicationState Application
		{
			[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
			get
			{
				return this.Context.Application;
			}
		}

		/// <summary>Gets the ASP.NET <see cref="T:System.Web.HttpContext" /> for the current request, which encapsulates all HTTP-specific context used by the HTTP server to process Web requests.</summary>
		/// <returns>The ASP.NET <see cref="T:System.Web.HttpContext" /> for the current request.</returns>
		/// <exception cref="T:System.Exception">
		///   <paramref name="Context" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Web.AspNetHostingPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Level="Minimal" />
		/// </PermissionSet>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002702 File Offset: 0x00000902
		[Browsable(false)]
		[WebServicesDescription("WebServiceContext")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpContext Context
		{
			[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
			get
			{
				PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
				if (this.context == null)
				{
					this.context = HttpContext.Current;
				}
				if (this.context == null)
				{
					throw new InvalidOperationException(Res.GetString("WebMissingHelpContext"));
				}
				return this.context;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.SessionState.HttpSessionState" /> instance for the current request.</summary>
		/// <returns>An <see cref="T:System.Web.SessionState.HttpSessionState" /> representing the ASP.NET session state object for the current session.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Web.AspNetHostingPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Level="Minimal" />
		/// </PermissionSet>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000273A File Offset: 0x0000093A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebServicesDescription("WebServiceSession")]
		[Browsable(false)]
		public HttpSessionState Session
		{
			[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
			get
			{
				return this.Context.Session;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpServerUtility" /> for the current request.</summary>
		/// <returns>An <see cref="T:System.Web.HttpServerUtility" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Web.AspNetHostingPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Level="Minimal" />
		/// </PermissionSet>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002747 File Offset: 0x00000947
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebServicesDescription("WebServiceServer")]
		public HttpServerUtility Server
		{
			[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
			get
			{
				return this.Context.Server;
			}
		}

		/// <summary>Gets the ASP.NET server <see cref="P:System.Web.HttpContext.User" /> object. Can be used to authenticate whether a user is authorized to execute the request.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.IPrincipal" /> representing the ASP.NET server <see cref="P:System.Web.HttpContext.User" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Web.AspNetHostingPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Level="Minimal" />
		/// </PermissionSet>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002754 File Offset: 0x00000954
		[Browsable(false)]
		[WebServicesDescription("WebServiceUser")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPrincipal User
		{
			[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
			get
			{
				return this.Context.User;
			}
		}

		/// <summary>Gets the version of the SOAP protocol used to make the SOAP request to the XML Web service.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Default" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Web.AspNetHostingPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Level="Minimal" />
		/// </PermissionSet>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002764 File Offset: 0x00000964
		[ComVisible(false)]
		[WebServicesDescription("WebServiceSoapVersion")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SoapProtocolVersion SoapVersion
		{
			[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
			get
			{
				object obj = this.Context.Items[WebService.SoapVersionContextSlot];
				if (obj != null && obj is SoapProtocolVersion)
				{
					return (SoapProtocolVersion)obj;
				}
				return SoapProtocolVersion.Default;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000279A File Offset: 0x0000099A
		internal void SetContext(HttpContext context)
		{
			this.context = context;
		}

		// Token: 0x04000076 RID: 118
		private HttpContext context;

		// Token: 0x04000077 RID: 119
		internal static readonly string SoapVersionContextSlot = "WebServiceSoapVersion";
	}
}
