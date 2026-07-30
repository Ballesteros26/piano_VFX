using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the level of authentication for DCOM security.</summary>
	// Token: 0x0200056F RID: 1391
	public enum ProcessModelComAuthenticationLevel
	{
		/// <summary>Specifies no authentication. This field is constant. </summary>
		// Token: 0x04002036 RID: 8246
		None,
		/// <summary>Specifies that DCOM authenticates the credentials of the client. This field is constant.</summary>
		// Token: 0x04002037 RID: 8247
		Call,
		/// <summary>Specifies that DCOM authenticates the credentials of the client. This field is constant.</summary>
		// Token: 0x04002038 RID: 8248
		Connect,
		/// <summary>Specifies that DCOM determines the authentication level. This field is constant. </summary>
		// Token: 0x04002039 RID: 8249
		Default,
		/// <summary>Specifies that DCOM verifies that all data received is from the expected client. This field is constant. </summary>
		// Token: 0x0400203A RID: 8250
		Pkt,
		/// <summary>Specifies that DCOM authenticates and verifies the data transferred. This field is constant. </summary>
		// Token: 0x0400203B RID: 8251
		PktIntegrity,
		/// <summary>Specifies that DCOM authenticates all previous levels and does encryption. This field is constant. </summary>
		// Token: 0x0400203C RID: 8252
		PktPrivacy
	}
}
