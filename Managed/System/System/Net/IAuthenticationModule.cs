using System;

namespace System.Net
{
	/// <summary>Provides the base authentication interface for Web client authentication modules.</summary>
	// Token: 0x0200042D RID: 1069
	public interface IAuthenticationModule
	{
		/// <summary>Returns an instance of the <see cref="T:System.Net.Authorization" /> class in respose to an authentication challenge from a server.</summary>
		/// <returns>An <see cref="T:System.Net.Authorization" /> instance containing the authorization message for the request, or null if the challenge cannot be handled.</returns>
		/// <param name="challenge">The authentication challenge sent by the server. </param>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> instance associated with the challenge. </param>
		/// <param name="credentials">The credentials associated with the challenge. </param>
		// Token: 0x06002045 RID: 8261
		Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials);

		/// <summary>Returns an instance of the <see cref="T:System.Net.Authorization" /> class for an authentication request to a server.</summary>
		/// <returns>An <see cref="T:System.Net.Authorization" /> instance containing the authorization message for the request.</returns>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> instance associated with the authentication request. </param>
		/// <param name="credentials">The credentials associated with the authentication request. </param>
		// Token: 0x06002046 RID: 8262
		Authorization PreAuthenticate(WebRequest request, ICredentials credentials);

		/// <summary>Gets a value indicating whether the authentication module supports preauthentication.</summary>
		/// <returns>true if the authorization module supports preauthentication; otherwise false.</returns>
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002047 RID: 8263
		bool CanPreAuthenticate { get; }

		/// <summary>Gets the authentication type provided by this authentication module.</summary>
		/// <returns>A string indicating the authentication type provided by this authentication module.</returns>
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002048 RID: 8264
		string AuthenticationType { get; }
	}
}
