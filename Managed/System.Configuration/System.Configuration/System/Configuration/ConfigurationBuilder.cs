using System;
using System.Configuration.Provider;
using System.Xml;
using Unity;

namespace System.Configuration
{
	// Token: 0x0200008A RID: 138
	public abstract class ConfigurationBuilder : ProviderBase
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00003524 File Offset: 0x00001724
		protected ConfigurationBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00003533 File Offset: 0x00001733
		public virtual ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00003533 File Offset: 0x00001733
		public virtual XmlNode ProcessRawXml(XmlNode rawXml)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
