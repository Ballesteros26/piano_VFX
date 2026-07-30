using System;

namespace System.Web.Profile
{
	/// <summary>Provides data for the <see cref="E:System.Web.Profile.ProfileModule.MigrateAnonymous" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
	// Token: 0x0200050B RID: 1291
	public sealed class ProfileMigrateEventArgs : EventArgs
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileMigrateEventArgs" /> class.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
		/// <param name="anonymousId">The anonymous identifier being migrated from.</param>
		// Token: 0x06003979 RID: 14713 RVA: 0x0009AB0C File Offset: 0x00098D0C
		public ProfileMigrateEventArgs(HttpContext context, string anonymousId)
		{
			this.context = context;
			this.anonymousId = anonymousId;
		}

		/// <summary>Gets the anonymous identifier for the anonymous profile from which to migrate profile property values.</summary>
		/// <returns>The anonymous identifier for the anonymous profile from which to migrate profile property values.</returns>
		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x0009AB22 File Offset: 0x00098D22
		public string AnonymousID
		{
			get
			{
				return this.anonymousId;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x0009AB2A File Offset: 0x00098D2A
		public HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x04001F2D RID: 7981
		private HttpContext context;

		// Token: 0x04001F2E RID: 7982
		private string anonymousId;
	}
}
