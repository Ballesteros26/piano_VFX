using System;
using System.Security.Permissions;

namespace System.Web.Services.Protocols
{
	/// <summary>Provides a common base implementation for readers of request parameters for Web services implemented using HTTP but without SOAP.</summary>
	// Token: 0x02000042 RID: 66
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class MimeParameterReader : MimeFormatter
	{
		/// <summary>When overridden in a derived class, deserializes an HTTP request into an array of Web method parameter values.</summary>
		/// <returns>An array of <see cref="T:System.Object" /> objects that contains the deserialized HTTP request.</returns>
		/// <param name="request">An <see cref="T:System.Web.HttpRequest" /> object containing the input message for an operation.</param>
		// Token: 0x06000173 RID: 371
		public abstract object[] Read(HttpRequest request);
	}
}
