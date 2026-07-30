using System;

namespace System.Configuration
{
	/// <summary>Defines standard functionality for controls or libraries that store and retrieve application settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017C RID: 380
	public interface IPersistComponentSettings
	{
		/// <summary>Gets or sets a value indicating whether the control should automatically persist its application settings properties.</summary>
		/// <returns>true if the control should automatically persist its state; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000B83 RID: 2947
		// (set) Token: 0x06000B84 RID: 2948
		bool SaveSettings { get; set; }

		/// <summary>Gets or sets the value of the application settings key for the current instance of the control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current instance of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000B85 RID: 2949
		// (set) Token: 0x06000B86 RID: 2950
		string SettingsKey { get; set; }

		/// <summary>Reads the control's application settings into their corresponding properties and updates the control's state.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B87 RID: 2951
		void LoadComponentSettings();

		/// <summary>Resets the control's application settings properties to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B88 RID: 2952
		void ResetComponentSettings();

		/// <summary>Persists the control's application settings properties.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B89 RID: 2953
		void SaveComponentSettings();
	}
}
