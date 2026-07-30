using System;

namespace System.Web.Profile
{
	/// <summary>Provides data for the <see cref="E:System.Web.Profile.ProfileModule.ProfileAutoSaving" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
	// Token: 0x02000507 RID: 1287
	public sealed class ProfileAutoSaveEventArgs : EventArgs
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileAutoSaveEventArgs" /> class.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
		// Token: 0x06003943 RID: 14659 RVA: 0x0009A04D File Offset: 0x0009824D
		public ProfileAutoSaveEventArgs(HttpContext context)
		{
			this.context = context;
			this.continueWithProfileAutoSave = true;
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x0009A063 File Offset: 0x00098263
		public HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Profile.ProfileModule" /> will automatically save the user profile.</summary>
		/// <returns>true if the <see cref="T:System.Web.Profile.ProfileModule" /> will automatically save the user profile; otherwise, false. The default is true.</returns>
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x0009A06B File Offset: 0x0009826B
		// (set) Token: 0x06003946 RID: 14662 RVA: 0x0009A073 File Offset: 0x00098273
		public bool ContinueWithProfileAutoSave
		{
			get
			{
				return this.continueWithProfileAutoSave;
			}
			set
			{
				this.continueWithProfileAutoSave = value;
			}
		}

		// Token: 0x04001F21 RID: 7969
		private HttpContext context;

		// Token: 0x04001F22 RID: 7970
		private bool continueWithProfileAutoSave;
	}
}
