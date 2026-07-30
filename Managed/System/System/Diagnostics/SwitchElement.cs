using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x020001BE RID: 446
	internal class SwitchElement : ConfigurationElement
	{
		// Token: 0x06000D2F RID: 3375 RVA: 0x0003F4A0 File Offset: 0x0003D6A0
		static SwitchElement()
		{
			SwitchElement._properties.Add(SwitchElement._propName);
			SwitchElement._properties.Add(SwitchElement._propValue);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0003F50F File Offset: 0x0003D70F
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0003F52F File Offset: 0x0003D72F
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[SwitchElement._propName];
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0003F541 File Offset: 0x0003D741
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SwitchElement._properties;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0003F548 File Offset: 0x0003D748
		[ConfigurationProperty("value", IsRequired = true)]
		public string Value
		{
			get
			{
				return (string)base[SwitchElement._propValue];
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003F55A File Offset: 0x0003D75A
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003F56C File Offset: 0x0003D76C
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string text2 = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(text2, text);
					}
				}
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003F5BD File Offset: 0x0003D7BD
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0003F5E4 File Offset: 0x0003D7E4
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SwitchElement switchElement = sourceElement as SwitchElement;
			if (switchElement != null && switchElement._attributes != null)
			{
				this._attributes = switchElement._attributes;
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003F618 File Offset: 0x0003D818
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				SwitchElement._properties.Clear();
				SwitchElement._properties.Add(SwitchElement._propName);
				SwitchElement._properties.Add(SwitchElement._propValue);
			}
		}

		// Token: 0x04001041 RID: 4161
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04001042 RID: 4162
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001043 RID: 4163
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04001044 RID: 4164
		private Hashtable _attributes;
	}
}
