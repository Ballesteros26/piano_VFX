using System;

namespace System.Net
{
	/// <summary>Selects the authentication scheme for an <see cref="T:System.Net.HttpListener" /> instance.</summary>
	/// <returns>One of the <see cref="T:System.Net.AuthenticationSchemes" /> values that indicates the method of authentication to use for the specified client request.</returns>
	/// <param name="httpRequest">The <see cref="T:System.Net.HttpListenerRequest" /> instance for which to select an authentication scheme.</param>
	// Token: 0x0200041A RID: 1050
	// (Invoke) Token: 0x06001FF2 RID: 8178
	public delegate AuthenticationSchemes AuthenticationSchemeSelector(HttpListenerRequest httpRequest);
}
