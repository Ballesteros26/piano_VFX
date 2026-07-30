using System;

namespace System.Configuration
{
	/// <summary>Provides an interface for defining an alternate application settings provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017D RID: 381
	public interface ISettingsProviderService
	{
		/// <summary>Returns the settings provider compatible with the specified settings property.</summary>
		/// <returns>If found, the <see cref="T:System.Configuration.SettingsProvider" /> that can persist the specified settings property; otherwise, null.</returns>
		/// <param name="property">The <see cref="T:System.Configuration.SettingsProperty" /> that requires serialization.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B8A RID: 2954
		SettingsProvider GetSettingsProvider(SettingsProperty property);
	}
}
