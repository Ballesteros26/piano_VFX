using System;

namespace System.Web.Profile
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Web.Profile.ProfileModule.ProfileAutoSaving" /> event of a <see cref="T:System.Web.Profile.ProfileModule" />. </summary>
	/// <param name="sender">The <see cref="T:System.Web.Profile.ProfileModule" /> that raised the <see cref="E:System.Web.Profile.ProfileModule.ProfileAutoSaving" /> event.</param>
	/// <param name="e">A <see cref="T:System.Web.Profile.ProfileAutoSaveEventArgs" /> that contains the event data.</param>
	// Token: 0x02000508 RID: 1288
	// (Invoke) Token: 0x06003948 RID: 14664
	public delegate void ProfileAutoSaveEventHandler(object sender, ProfileAutoSaveEventArgs e);
}
