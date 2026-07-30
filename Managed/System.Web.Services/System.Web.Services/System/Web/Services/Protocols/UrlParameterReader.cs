using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Reads incoming request parameters for Web services implemented using HTTP with name-value pairs encoded in the URL's query string rather than as a SOAP message.</summary>
	// Token: 0x02000086 RID: 134
	public class UrlParameterReader : ValueCollectionParameterReader
	{
		/// <summary>Reads name/value pairs encoded in the query string of an HTTP request into Web method parameter values.</summary>
		/// <returns>An array of name/value pairs.</returns>
		/// <param name="request">A <see cref="T:System.Net.WebResponse" /> objectcontaining HTML URL-encoded name/value pairs.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000397 RID: 919 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		public override object[] Read(HttpRequest request)
		{
			return base.Read(request.QueryString);
		}
	}
}
