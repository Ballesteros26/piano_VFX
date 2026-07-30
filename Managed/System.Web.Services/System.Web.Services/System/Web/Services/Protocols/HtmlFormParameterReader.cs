using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Reads incoming request parameters for Web services implemented using HTTP, with name-value pairs encoded like an HTML form rather than as a SOAP message.</summary>
	// Token: 0x0200002D RID: 45
	public class HtmlFormParameterReader : ValueCollectionParameterReader
	{
		/// <summary>Reads name-value pairs into Web method parameter values.</summary>
		/// <returns>An array of objects contain the name-value pairs.</returns>
		/// <param name="request">An <see cref="T:System.Web.HttpRequest" /> object containing HTML name-value pairs encoded in the body of an HTTP request.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000101 RID: 257 RVA: 0x00004D1F File Offset: 0x00002F1F
		public override object[] Read(HttpRequest request)
		{
			if (!ContentType.MatchesBase(request.ContentType, "application/x-www-form-urlencoded"))
			{
				return null;
			}
			return base.Read(request.Form);
		}

		// Token: 0x040001E6 RID: 486
		internal const string MimeType = "application/x-www-form-urlencoded";
	}
}
