using System;

namespace System.Configuration
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
	/// <param name="sender">The source of the event, typically the settings class.</param>
	/// <param name="e">A <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> object that contains the event data.</param>
	// Token: 0x02000192 RID: 402
	// (Invoke) Token: 0x06000BF7 RID: 3063
	public delegate void SettingsLoadedEventHandler(object sender, SettingsLoadedEventArgs e);
}
