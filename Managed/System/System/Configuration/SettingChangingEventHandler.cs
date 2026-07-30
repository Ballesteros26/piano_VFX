using System;

namespace System.Configuration
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event. </summary>
	/// <param name="sender">The source of the event, typically an application settings wrapper class derived from the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class.</param>
	/// <param name="e">A <see cref="T:System.Configuration.SettingChangingEventArgs" /> containing the data for the event.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000187 RID: 391
	// (Invoke) Token: 0x06000BB2 RID: 2994
	public delegate void SettingChangingEventHandler(object sender, SettingChangingEventArgs e);
}
