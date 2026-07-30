using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the authentication level for COM security.</summary>
	// Token: 0x02000570 RID: 1392
	public enum ProcessModelComImpersonationLevel
	{
		/// <summary>Specifies that DCOM determines the impersonation level. This field is constant. </summary>
		// Token: 0x0400203E RID: 8254
		Default,
		/// <summary>Specifies that the client is anonymous to the server. This field is constant. </summary>
		// Token: 0x0400203F RID: 8255
		Anonymous,
		/// <summary>Specifies that the server process can impersonate the client's security context while acting on behalf of the client. This field is constant. </summary>
		// Token: 0x04002040 RID: 8256
		Delegate,
		/// <summary>Specifies that the server can obtain the client's identity. This field is constant. </summary>
		// Token: 0x04002041 RID: 8257
		Identify,
		/// <summary>Specifies that the server process can impersonate the client's security context while acting on behalf of the client. This field is constant. </summary>
		// Token: 0x04002042 RID: 8258
		Impersonate
	}
}
