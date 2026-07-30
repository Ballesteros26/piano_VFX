using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides a wrapper type definition for configuration sections that are not handled by the <see cref="N:System.Configuration" /> types.</summary>
	// Token: 0x02000044 RID: 68
	public sealed class IgnoreSection : ConfigurationSection
	{
		// Token: 0x06000247 RID: 583 RVA: 0x000023BB File Offset: 0x000005BB
		protected internal override bool IsModified()
		{
			return false;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007A54 File Offset: 0x00005C54
		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			this.xml = xmlReader.ReadOuterXml();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007726 File Offset: 0x00005926
		[MonoTODO]
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			base.Reset(parentSection);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000772F File Offset: 0x0000592F
		[MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007A62 File Offset: 0x00005C62
		protected internal override string SerializeSection(ConfigurationElement parentSection, string name, ConfigurationSaveMode saveMode)
		{
			return this.xml;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00007A6A File Offset: 0x00005C6A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IgnoreSection.properties;
			}
		}

		// Token: 0x040000EB RID: 235
		private string xml;

		// Token: 0x040000EC RID: 236
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
