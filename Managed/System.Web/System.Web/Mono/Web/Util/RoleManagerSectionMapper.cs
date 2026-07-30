using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;

namespace Mono.Web.Util
{
	// Token: 0x02000008 RID: 8
	internal class RoleManagerSectionMapper : ISectionSettingsMapper
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000236C File Offset: 0x0000056C
		public object MapSection(object _section, List<SettingsMappingWhat> whats)
		{
			RoleManagerSection roleManagerSection = _section as RoleManagerSection;
			if (roleManagerSection == null)
			{
				return _section;
			}
			foreach (SettingsMappingWhat settingsMappingWhat in whats)
			{
				List<SettingsMappingWhatContents> contents = settingsMappingWhat.Contents;
				if (contents != null && contents.Count != 0)
				{
					foreach (SettingsMappingWhatContents settingsMappingWhatContents in contents)
					{
						switch (settingsMappingWhatContents.Operation)
						{
						case SettingsMappingWhatOperation.Add:
							this.ProcessAdd(roleManagerSection, settingsMappingWhatContents);
							break;
						case SettingsMappingWhatOperation.Clear:
							this.ProcessClear(roleManagerSection, settingsMappingWhatContents);
							break;
						case SettingsMappingWhatOperation.Replace:
							this.ProcessReplace(roleManagerSection, settingsMappingWhatContents);
							break;
						case SettingsMappingWhatOperation.Remove:
							this.ProcessRemove(roleManagerSection, settingsMappingWhatContents);
							break;
						}
					}
				}
			}
			return roleManagerSection;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000245C File Offset: 0x0000065C
		private bool GetCommonAttributes(SettingsMappingWhatContents how, out string name, out string type)
		{
			string text;
			type = (text = null);
			name = text;
			Dictionary<string, string> attributes = how.Attributes;
			if (attributes == null || attributes.Count == 0)
			{
				return false;
			}
			if (!attributes.TryGetValue("name", out name))
			{
				return false;
			}
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			attributes.TryGetValue("type", out type);
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024B0 File Offset: 0x000006B0
		private void SetProviderProperties(SettingsMappingWhatContents how, ProviderSettings prov)
		{
			Dictionary<string, string> attributes = how.Attributes;
			if (attributes == null || attributes.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<string, string> keyValuePair in attributes)
			{
				string key = keyValuePair.Key;
				if (!(key == "name"))
				{
					if (key == "type")
					{
						prov.Type = keyValuePair.Value;
					}
					else
					{
						prov.Parameters[key] = keyValuePair.Value;
					}
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002550 File Offset: 0x00000750
		private void ProcessAdd(RoleManagerSection section, SettingsMappingWhatContents how)
		{
			string text;
			string text2;
			if (!this.GetCommonAttributes(how, out text, out text2))
			{
				return;
			}
			ProviderSettingsCollection providers = section.Providers;
			if (providers[text] != null)
			{
				return;
			}
			ProviderSettings providerSettings = new ProviderSettings(text, text2);
			this.SetProviderProperties(how, providerSettings);
			providers.Add(providerSettings);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002594 File Offset: 0x00000794
		private void ProcessRemove(RoleManagerSection section, SettingsMappingWhatContents how)
		{
			string text;
			string text2;
			if (!this.GetCommonAttributes(how, out text, out text2))
			{
				return;
			}
			ProviderSettingsCollection providers = section.Providers;
			ProviderSettings providerSettings = providers[text];
			if (providerSettings != null)
			{
				if (providerSettings.Type != text2)
				{
					return;
				}
				providers.Remove(text);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025D7 File Offset: 0x000007D7
		private void ProcessClear(RoleManagerSection section, SettingsMappingWhatContents how)
		{
			section.Providers.Clear();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025E4 File Offset: 0x000007E4
		private void ProcessReplace(RoleManagerSection section, SettingsMappingWhatContents how)
		{
			string text;
			string text2;
			if (!this.GetCommonAttributes(how, out text, out text2))
			{
				return;
			}
			ProviderSettings providerSettings = section.Providers[text];
			if (providerSettings != null)
			{
				this.SetProviderProperties(how, providerSettings);
			}
		}
	}
}
