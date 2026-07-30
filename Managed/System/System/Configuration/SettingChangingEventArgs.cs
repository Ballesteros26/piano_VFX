using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000186 RID: 390
	public class SettingChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingChangingEventArgs" /> class.</summary>
		/// <param name="settingName">A <see cref="T:System.String" /> containing the name of the application setting.</param>
		/// <param name="settingClass">A <see cref="T:System.String" /> containing a category description of the setting. Often this parameter is set to the application settings group name.</param>
		/// <param name="settingKey">A <see cref="T:System.String" /> containing the application settings key.</param>
		/// <param name="newValue">An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</param>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		// Token: 0x06000BAC RID: 2988 RVA: 0x0003BF4A File Offset: 0x0003A14A
		public SettingChangingEventArgs(string settingName, string settingClass, string settingKey, object newValue, bool cancel)
			: base(cancel)
		{
			this.settingName = settingName;
			this.settingClass = settingClass;
			this.settingKey = settingKey;
			this.newValue = newValue;
		}

		/// <summary>Gets the name of the application setting associated with the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application setting. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0003BF71 File Offset: 0x0003A171
		public string SettingName
		{
			get
			{
				return this.settingName;
			}
		}

		/// <summary>Gets the application settings property category.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a category description of the setting. Typically, this parameter is set to the application settings group name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0003BF79 File Offset: 0x0003A179
		public string SettingClass
		{
			get
			{
				return this.settingClass;
			}
		}

		/// <summary>Gets the application settings key associated with the property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the application settings key.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0003BF81 File Offset: 0x0003A181
		public string SettingKey
		{
			get
			{
				return this.settingKey;
			}
		}

		/// <summary>Gets the new value being assigned to the application settings property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0003BF89 File Offset: 0x0003A189
		public object NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x04000FCF RID: 4047
		private string settingName;

		// Token: 0x04000FD0 RID: 4048
		private string settingClass;

		// Token: 0x04000FD1 RID: 4049
		private string settingKey;

		// Token: 0x04000FD2 RID: 4050
		private object newValue;
	}
}
