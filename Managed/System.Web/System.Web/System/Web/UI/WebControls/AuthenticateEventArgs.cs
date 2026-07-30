using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Login.Authenticate" /> event.</summary>
	// Token: 0x0200027D RID: 637
	public class AuthenticateEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /> class with the <see cref="P:System.Web.UI.WebControls.AuthenticateEventArgs.Authenticated" /> property set to false.</summary>
		// Token: 0x06001A60 RID: 6752 RVA: 0x00045D2F File Offset: 0x00043F2F
		public AuthenticateEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /> class.</summary>
		/// <param name="authenticated">true if the user is authenticated; otherwise, false. </param>
		// Token: 0x06001A61 RID: 6753 RVA: 0x00045D38 File Offset: 0x00043F38
		public AuthenticateEventArgs(bool authenticated)
		{
			this._authenticated = authenticated;
		}

		/// <summary>Gets or sets a value indicating whether a user's authentication attempt succeeded.</summary>
		/// <returns>true if the authentication attempt succeeded; otherwise, false.</returns>
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00045D47 File Offset: 0x00043F47
		// (set) Token: 0x06001A63 RID: 6755 RVA: 0x00045D4F File Offset: 0x00043F4F
		public bool Authenticated
		{
			get
			{
				return this._authenticated;
			}
			set
			{
				this._authenticated = value;
			}
		}

		// Token: 0x0400164F RID: 5711
		private bool _authenticated;
	}
}
