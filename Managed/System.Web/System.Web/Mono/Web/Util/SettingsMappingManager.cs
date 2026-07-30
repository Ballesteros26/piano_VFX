using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.Configuration;
using System.Xml.XPath;

namespace Mono.Web.Util
{
	// Token: 0x0200000B RID: 11
	public class SettingsMappingManager
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027B0 File Offset: 0x000009B0
		internal static bool IsRunningOnWindows
		{
			get
			{
				return SettingsMappingManager._runningOnWindows;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000027B7 File Offset: 0x000009B7
		public static SettingsMappingPlatform Platform
		{
			get
			{
				return SettingsMappingManager._myPlatform;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000027BE File Offset: 0x000009BE
		public bool HasMappings
		{
			get
			{
				return this._mappers != null && this._mappers.Count > 0;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027D8 File Offset: 0x000009D8
		static SettingsMappingManager()
		{
			PlatformID platform = Environment.OSVersion.Platform;
			SettingsMappingManager._runningOnWindows = platform != (PlatformID)128 && platform != PlatformID.Unix && platform != PlatformID.MacOSX;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002830 File Offset: 0x00000A30
		public static void Init()
		{
			if (SettingsMappingManager._instance != null)
			{
				return;
			}
			if (Environment.GetEnvironmentVariable("MONO_ASPNET_INHIBIT_SETTINGSMAP") != null)
			{
				return;
			}
			NameValueCollection appSettings = WebConfigurationManager.AppSettings;
			if (appSettings != null && string.Compare(appSettings["MonoAspnetInhibitSettingsMap"], "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return;
			}
			if (SettingsMappingManager.IsRunningOnWindows)
			{
				SettingsMappingManager._myPlatform = SettingsMappingPlatform.Windows;
			}
			else
			{
				SettingsMappingManager._myPlatform = SettingsMappingPlatform.Unix;
			}
			SettingsMappingManager settingsMappingManager = new SettingsMappingManager();
			settingsMappingManager.LoadMappings();
			if (settingsMappingManager.HasMappings)
			{
				SettingsMappingManager._instance = settingsMappingManager;
				SettingsMappingManager._mappedSections = new Dictionary<object, object>();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028B0 File Offset: 0x00000AB0
		private void LoadMappings()
		{
			if (File.Exists(SettingsMappingManager._mappingFile))
			{
				this.LoadMappings(SettingsMappingManager._mappingFile);
			}
			string text = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "settings.map.config");
			if (File.Exists(text))
			{
				this.LoadMappings(text);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002900 File Offset: 0x00000B00
		private void LoadMappings(string mappingFilePath)
		{
			XPathNavigator xpathNavigator;
			try
			{
				xpathNavigator = new XPathDocument(mappingFilePath).CreateNavigator();
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Error loading mapping settings", ex);
			}
			XPathNodeIterator xpathNodeIterator;
			if (this._mappers == null)
			{
				this._mappers = new Dictionary<Type, SettingsMapping>();
			}
			else
			{
				xpathNodeIterator = xpathNavigator.Select("//settingsMap/clear");
				if (xpathNodeIterator.MoveNext())
				{
					this._mappers.Clear();
				}
			}
			xpathNodeIterator = xpathNavigator.Select("//settingsMap/map[string-length (@sectionType) > 0 and string-length (@mapperType) > 0 and string-length (@platform) > 0]");
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
				SettingsMapping settingsMapping = new SettingsMapping(xpathNavigator2);
				if (SettingsMappingManager._myPlatform == settingsMapping.Platform)
				{
					if (!this._mappers.ContainsKey(settingsMapping.SectionType))
					{
						this._mappers.Add(settingsMapping.SectionType, settingsMapping);
					}
					else
					{
						this._mappers[settingsMapping.SectionType] = settingsMapping;
					}
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029D4 File Offset: 0x00000BD4
		public static object MapSection(object input)
		{
			if (SettingsMappingManager._instance == null || input == null)
			{
				return input;
			}
			object obj;
			if (SettingsMappingManager._mappedSections.TryGetValue(input, out obj))
			{
				return obj;
			}
			object obj2 = SettingsMappingManager._instance.MapSection(input, input.GetType());
			object obj3 = SettingsMappingManager.mapperLock;
			lock (obj3)
			{
				if (obj2 != null && !SettingsMappingManager._mappedSections.ContainsKey(obj2))
				{
					SettingsMappingManager._mappedSections.Add(obj2, obj2);
				}
			}
			return obj2;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A5C File Offset: 0x00000C5C
		private object MapSection(object input, Type type)
		{
			if (this._mappers == null || this._mappers.Count == 0 || !this._mappers.ContainsKey(type))
			{
				return input;
			}
			SettingsMapping settingsMapping;
			if (!this._mappers.TryGetValue(type, out settingsMapping))
			{
				return input;
			}
			if (settingsMapping == null)
			{
				return input;
			}
			return settingsMapping.MapSection(input, type);
		}

		// Token: 0x04000D31 RID: 3377
		private const string settingsMapFileName = "settings.map";

		// Token: 0x04000D32 RID: 3378
		private const string localSettingsMapFileName = "settings.map.config";

		// Token: 0x04000D33 RID: 3379
		private static object mapperLock = new object();

		// Token: 0x04000D34 RID: 3380
		private static SettingsMappingManager _instance;

		// Token: 0x04000D35 RID: 3381
		private static string _mappingFile = Path.Combine(Path.GetDirectoryName(RuntimeEnvironment.SystemConfigurationFile), "settings.map");

		// Token: 0x04000D36 RID: 3382
		private Dictionary<Type, SettingsMapping> _mappers;

		// Token: 0x04000D37 RID: 3383
		private static Dictionary<object, object> _mappedSections;

		// Token: 0x04000D38 RID: 3384
		private static SettingsMappingPlatform _myPlatform;

		// Token: 0x04000D39 RID: 3385
		private static bool _runningOnWindows;
	}
}
