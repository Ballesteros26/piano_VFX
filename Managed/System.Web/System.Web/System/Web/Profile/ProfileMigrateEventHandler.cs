using System;

namespace System.Web.Profile
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Web.Profile.ProfileModule.MigrateAnonymous" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
	/// <param name="sender">The <see cref="T:System.Web.Profile.ProfileModule" /> that raised the <see cref="E:System.Web.Profile.ProfileModule.MigrateAnonymous" /> event.</param>
	/// <param name="e">A <see cref="T:System.Web.Profile.ProfileMigrateEventArgs" /> that contains the event data.</param>
	// Token: 0x0200050C RID: 1292
	// (Invoke) Token: 0x0600397D RID: 14717
	public delegate void ProfileMigrateEventHandler(object sender, ProfileMigrateEventArgs e);
}
