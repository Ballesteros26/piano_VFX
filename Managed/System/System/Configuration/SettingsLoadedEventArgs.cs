using System;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
	// Token: 0x02000191 RID: 401
	public class SettingsLoadedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> class. </summary>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object from which settings are loaded.</param>
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0003C8BD File Offset: 0x0003AABD
		public SettingsLoadedEventArgs(SettingsProvider provider)
		{
			this.provider = provider;
		}

		/// <summary>Gets the settings provider used to store configuration settings.</summary>
		/// <returns>A settings provider.</returns>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0003C8CC File Offset: 0x0003AACC
		public SettingsProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x04000FE2 RID: 4066
		private SettingsProvider provider;
	}
}
