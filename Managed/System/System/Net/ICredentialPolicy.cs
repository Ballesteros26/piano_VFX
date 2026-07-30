using System;

namespace System.Net
{
	/// <summary>Defines the credential policy to be used for resource requests that are made using <see cref="T:System.Net.WebRequest" /> and its derived classes.</summary>
	// Token: 0x02000535 RID: 1333
	public interface ICredentialPolicy
	{
		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether the client's credentials are sent with a resource request made using an instance of the <see cref="T:System.Net.WebRequest" /> class.</summary>
		/// <returns>true if the credentials are sent with the request; otherwise, false.</returns>
		/// <param name="challengeUri">The <see cref="T:System.Uri" /> that will receive the request. For more information, see the Remarks section.</param>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> that represents the resource being requested.</param>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that will be sent with the request if this method returns true. </param>
		/// <param name="authenticationModule">The <see cref="T:System.Net.IAuthenticationModule" /> that will conduct the authentication, if authentication is required.</param>
		// Token: 0x06002951 RID: 10577
		bool ShouldSendCredential(Uri challengeUri, WebRequest request, NetworkCredential credential, IAuthenticationModule authenticationModule);
	}
}
