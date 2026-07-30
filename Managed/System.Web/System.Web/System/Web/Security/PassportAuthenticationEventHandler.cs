using System;

namespace System.Web.Security
{
	/// <summary>Represents the method that handles the PassportAuthentication_OnAuthenticate event of a <see cref="T:System.Web.Security.PassportAuthenticationModule" />. This class is deprecated.</summary>
	/// <param name="sender">The object that raised the event. </param>
	/// <param name="e">A <see cref="T:System.Web.Security.PassportAuthenticationEventArgs" /> object that contains the event data. </param>
	// Token: 0x020004AF RID: 1199
	// (Invoke) Token: 0x06003632 RID: 13874
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public delegate void PassportAuthenticationEventHandler(object sender, PassportAuthenticationEventArgs e);
}
