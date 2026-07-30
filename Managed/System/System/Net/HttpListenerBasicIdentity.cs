using System;
using System.Security.Principal;

namespace System.Net
{
	/// <summary>Holds the user name and password from a basic authentication request.</summary>
	// Token: 0x02000521 RID: 1313
	public class HttpListenerBasicIdentity : GenericIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerBasicIdentity" /> class using the specified user name and password.</summary>
		/// <param name="username">The user name.</param>
		/// <param name="password">The password.</param>
		// Token: 0x060027EE RID: 10222 RVA: 0x0009A295 File Offset: 0x00098495
		public HttpListenerBasicIdentity(string username, string password)
			: base(username, "Basic")
		{
			this.password = password;
		}

		/// <summary>Indicates the password from a basic authentication attempt.</summary>
		/// <returns>A <see cref="T:System.String" /> that holds the password.</returns>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x0009A2AA File Offset: 0x000984AA
		public virtual string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x040021B6 RID: 8630
		private string password;
	}
}
