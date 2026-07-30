using System;
using System.Collections.Specialized;
using System.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x020002C5 RID: 709
	internal static class AppSettings
	{
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00093D19 File Offset: 0x00091F19
		internal static bool? UseLegacySerializerGeneration
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings.useLegacySerializerGeneration;
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00093D28 File Offset: 0x00091F28
		private static void EnsureSettingsLoaded()
		{
			if (!AppSettings.settingsInitalized)
			{
				object obj = AppSettings.appSettingsLock;
				lock (obj)
				{
					if (!AppSettings.settingsInitalized)
					{
						NameValueCollection nameValueCollection = null;
						try
						{
							nameValueCollection = ConfigurationManager.AppSettings;
						}
						catch (ConfigurationErrorsException)
						{
						}
						finally
						{
							bool flag2;
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["System:Xml:Serialization:UseLegacySerializerGeneration"], out flag2))
							{
								AppSettings.useLegacySerializerGeneration = null;
							}
							else
							{
								AppSettings.useLegacySerializerGeneration = new bool?(flag2);
							}
							AppSettings.settingsInitalized = true;
						}
					}
				}
			}
		}

		// Token: 0x0400157C RID: 5500
		private const string UseLegacySerializerGenerationAppSettingsString = "System:Xml:Serialization:UseLegacySerializerGeneration";

		// Token: 0x0400157D RID: 5501
		private static bool? useLegacySerializerGeneration;

		// Token: 0x0400157E RID: 5502
		private static volatile bool settingsInitalized = false;

		// Token: 0x0400157F RID: 5503
		private static object appSettingsLock = new object();
	}
}
