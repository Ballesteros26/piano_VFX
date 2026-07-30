using System;
using System.Runtime.CompilerServices;

namespace System.Configuration
{
	// Token: 0x02000170 RID: 368
	internal class DefaultConfig : IConfigurationSystem
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x000020EB File Offset: 0x000002EB
		private DefaultConfig()
		{
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00039E6A File Offset: 0x0003806A
		public static DefaultConfig GetInstance()
		{
			return DefaultConfig.instance;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00039E71 File Offset: 0x00038071
		[Obsolete("This method is obsolete.  Please use System.Configuration.ConfigurationManager.GetConfig")]
		public object GetConfig(string sectionName)
		{
			this.Init();
			return this.config.GetConfig(sectionName);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00039E88 File Offset: 0x00038088
		public void Init()
		{
			lock (this)
			{
				if (this.config == null)
				{
					ConfigurationData configurationData = new ConfigurationData();
					if (!configurationData.LoadString(DefaultConfig.GetBundledMachineConfig()) && !configurationData.Load(DefaultConfig.GetMachineConfigPath()))
					{
						throw new ConfigurationException("Cannot find " + DefaultConfig.GetMachineConfigPath());
					}
					string appConfigPath = DefaultConfig.GetAppConfigPath();
					if (appConfigPath == null)
					{
						this.config = configurationData;
					}
					else
					{
						ConfigurationData configurationData2 = new ConfigurationData(configurationData);
						if (configurationData2.Load(appConfigPath))
						{
							this.config = configurationData2;
						}
						else
						{
							this.config = configurationData;
						}
					}
				}
			}
		}

		// Token: 0x06000B1D RID: 2845
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		// Token: 0x06000B1E RID: 2846 RVA: 0x00039F34 File Offset: 0x00038134
		internal static string GetBundledMachineConfig()
		{
			return DefaultConfig.get_bundled_machine_config();
		}

		// Token: 0x06000B1F RID: 2847
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_machine_config_path();

		// Token: 0x06000B20 RID: 2848 RVA: 0x00039F3B File Offset: 0x0003813B
		internal static string GetMachineConfigPath()
		{
			return DefaultConfig.get_machine_config_path();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00039F44 File Offset: 0x00038144
		private static string GetAppConfigPath()
		{
			string configurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			if (configurationFile == null || configurationFile.Length == 0)
			{
				return null;
			}
			return configurationFile;
		}

		// Token: 0x04000F8D RID: 3981
		private static readonly DefaultConfig instance = new DefaultConfig();

		// Token: 0x04000F8E RID: 3982
		private ConfigurationData config;
	}
}
