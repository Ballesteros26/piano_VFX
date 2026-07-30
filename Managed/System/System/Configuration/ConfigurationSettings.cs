using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Provides runtime versions 1.0 and 1.1 support for reading configuration sections and common configuration settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016F RID: 367
	public sealed class ConfigurationSettings
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x000020EB File Offset: 0x000002EB
		private ConfigurationSettings()
		{
		}

		/// <summary>Returns the <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.NoteThe <see cref="T:System.Configuration.ConfigurationSettings" /> class provides backward compatibility only. You should use the <see cref="T:System.Configuration.ConfigurationManager" /> class or <see cref="T:System.Web.Configuration.WebConfigurationManager" /> class instead.</returns>
		/// <param name="sectionName">A configuration name and path, such as "system.net/settings".</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">Unable to retrieve the requested section.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000B15 RID: 2837 RVA: 0x0003884D File Offset: 0x00036A4D
		[Obsolete("This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.GetSection")]
		public static object GetConfig(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}

		/// <summary>Gets a read-only <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the application settings section of the configuration file.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the application settings section from the configuration file.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00039DD8 File Offset: 0x00037FD8
		[Obsolete("This property is obsolete.  Please use System.Configuration.ConfigurationManager.AppSettings")]
		public static NameValueCollection AppSettings
		{
			get
			{
				object obj = ConfigurationManager.GetSection("appSettings");
				if (obj == null)
				{
					obj = new NameValueCollection();
				}
				return (NameValueCollection)obj;
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00039E00 File Offset: 0x00038000
		internal static IConfigurationSystem ChangeConfigurationSystem(IConfigurationSystem newSystem)
		{
			if (newSystem == null)
			{
				throw new ArgumentNullException("newSystem");
			}
			object obj = ConfigurationSettings.lockobj;
			IConfigurationSystem configurationSystem2;
			lock (obj)
			{
				IConfigurationSystem configurationSystem = ConfigurationSettings.config;
				ConfigurationSettings.config = newSystem;
				configurationSystem2 = configurationSystem;
			}
			return configurationSystem2;
		}

		// Token: 0x04000F8B RID: 3979
		private static IConfigurationSystem config = DefaultConfig.GetInstance();

		// Token: 0x04000F8C RID: 3980
		private static object lockobj = new object();
	}
}
