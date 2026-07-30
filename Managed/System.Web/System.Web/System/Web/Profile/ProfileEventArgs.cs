using System;

namespace System.Web.Profile
{
	/// <summary>Provides data for the <see cref="E:System.Web.Profile.ProfileModule.Personalize" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
	// Token: 0x02000502 RID: 1282
	public sealed class ProfileEventArgs : EventArgs
	{
		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x00099D10 File Offset: 0x00097F10
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Gets or sets the user profile for the current request.</summary>
		/// <returns>The user profile to use for the current request. The default is null.</returns>
		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x00099D18 File Offset: 0x00097F18
		// (set) Token: 0x06003926 RID: 14630 RVA: 0x00099D20 File Offset: 0x00097F20
		public ProfileBase Profile
		{
			get
			{
				return this._Profile;
			}
			set
			{
				this._Profile = value;
			}
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileEventArgs" /> class.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
		// Token: 0x06003927 RID: 14631 RVA: 0x00099D29 File Offset: 0x00097F29
		public ProfileEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x04001F14 RID: 7956
		private HttpContext _Context;

		// Token: 0x04001F15 RID: 7957
		private ProfileBase _Profile;
	}
}
