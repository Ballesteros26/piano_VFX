using System;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>The exception that is thrown when a potentially malicious input string is received from the client as part of the request data. This class cannot be inherited.</summary>
	// Token: 0x020000A4 RID: 164
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class HttpRequestValidationException : HttpException
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.HttpRequestValidationException" /> class.</summary>
		// Token: 0x0600082A RID: 2090 RVA: 0x00009578 File Offset: 0x00007778
		public HttpRequestValidationException()
		{
		}

		/// <summary>Creates a new <see cref="T:System.Web.HttpRequestValidationException" /> exception with the specified error message.</summary>
		/// <param name="message">A string that describes the error.</param>
		// Token: 0x0600082B RID: 2091 RVA: 0x00009580 File Offset: 0x00007780
		public HttpRequestValidationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpRequestValidationException" /> class with a specified error message and a reference to the inner exception that is the cause of the exception.</summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If this parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x0600082C RID: 2092 RVA: 0x00009589 File Offset: 0x00007789
		public HttpRequestValidationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x000144F9 File Offset: 0x000126F9
		internal override string Description
		{
			get
			{
				return "Request validation detected a potentially dangerous input value from the client and aborted the request. This might be an attemp of using cross-site scripting to compromise the security of your site. You can disable request validation using the 'validateRequest=false' attribute in your page or setting it in your machine.config or web.config configuration files. If you disable it, you're encouraged to properly check the input values you get from the client.<br>\r\nYou can get more information on input validation <a href=\"http://www.cert.org/tech_tips/malicious_code_mitigation.html\">here</a>.";
			}
		}
	}
}
