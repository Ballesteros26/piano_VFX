using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;

namespace Mono.Web.Util
{
	// Token: 0x02000007 RID: 7
	internal class MembershipSectionMapper : ISectionSettingsMapper
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020C0 File Offset: 0x000002C0
		public object MapSection(object _section, List<SettingsMappingWhat> whats)
		{
			MembershipSection membershipSection = _section as MembershipSection;
			if (membershipSection == null)
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
							this.ProcessAdd(membershipSection, settingsMappingWhatContents);
							break;
						case SettingsMappingWhatOperation.Clear:
							this.ProcessClear(membershipSection, settingsMappingWhatContents);
							break;
						case SettingsMappingWhatOperation.Replace:
							this.ProcessReplace(membershipSection, settingsMappingWhatContents);
							break;
						case SettingsMappingWhatOperation.Remove:
							this.ProcessRemove(membershipSection, settingsMappingWhatContents);
							break;
						}
					}
				}
			}
			return membershipSection;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B0 File Offset: 0x000003B0
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

		// Token: 0x0600000F RID: 15 RVA: 0x00002204 File Offset: 0x00000404
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

		// Token: 0x06000010 RID: 16 RVA: 0x000022A4 File Offset: 0x000004A4
		private void ProcessAdd(MembershipSection section, SettingsMappingWhatContents how)
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

		// Token: 0x06000011 RID: 17 RVA: 0x000022E8 File Offset: 0x000004E8
		private void ProcessRemove(MembershipSection section, SettingsMappingWhatContents how)
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

		// Token: 0x06000012 RID: 18 RVA: 0x0000232B File Offset: 0x0000052B
		private void ProcessClear(MembershipSection section, SettingsMappingWhatContents how)
		{
			section.Providers.Clear();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002338 File Offset: 0x00000538
		private void ProcessReplace(MembershipSection section, SettingsMappingWhatContents how)
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
