using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x020001B7 RID: 439
	internal class SourceElement : ConfigurationElement
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x0003E9B8 File Offset: 0x0003CBB8
		static SourceElement()
		{
			SourceElement._properties.Add(SourceElement._propName);
			SourceElement._properties.Add(SourceElement._propSwitchName);
			SourceElement._properties.Add(SourceElement._propSwitchValue);
			SourceElement._properties.Add(SourceElement._propSwitchType);
			SourceElement._properties.Add(SourceElement._propListeners);
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0003EAA9 File Offset: 0x0003CCA9
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0003EAC9 File Offset: 0x0003CCC9
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[SourceElement._propListeners];
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0003EADB File Offset: 0x0003CCDB
		[ConfigurationProperty("name", IsRequired = true, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[SourceElement._propName];
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0003EAED File Offset: 0x0003CCED
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SourceElement._properties;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0003EAF4 File Offset: 0x0003CCF4
		[ConfigurationProperty("switchName")]
		public string SwitchName
		{
			get
			{
				return (string)base[SourceElement._propSwitchName];
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0003EB06 File Offset: 0x0003CD06
		[ConfigurationProperty("switchValue")]
		public string SwitchValue
		{
			get
			{
				return (string)base[SourceElement._propSwitchValue];
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0003EB18 File Offset: 0x0003CD18
		[ConfigurationProperty("switchType")]
		public string SwitchType
		{
			get
			{
				return (string)base[SourceElement._propSwitchType];
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0003EB2C File Offset: 0x0003CD2C
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			if (!string.IsNullOrEmpty(this.SwitchName) && !string.IsNullOrEmpty(this.SwitchValue))
			{
				throw new ConfigurationErrorsException(global::SR.GetString("'switchValue' and 'switchName' cannot both be specified on source '{0}'.", new object[] { this.Name }));
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0003EB7A File Offset: 0x0003CD7A
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0003EB8C File Offset: 0x0003CD8C
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

		// Token: 0x06000CFE RID: 3326 RVA: 0x0003EBDD File Offset: 0x0003CDDD
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0003EC04 File Offset: 0x0003CE04
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SourceElement sourceElement2 = sourceElement as SourceElement;
			if (sourceElement2 != null && sourceElement2._attributes != null)
			{
				this._attributes = sourceElement2._attributes;
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0003EC38 File Offset: 0x0003CE38
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				SourceElement._properties.Clear();
				SourceElement._properties.Add(SourceElement._propName);
				SourceElement._properties.Add(SourceElement._propSwitchName);
				SourceElement._properties.Add(SourceElement._propSwitchValue);
				SourceElement._properties.Add(SourceElement._propSwitchType);
				SourceElement._properties.Add(SourceElement._propListeners);
			}
		}

		// Token: 0x04001021 RID: 4129
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04001022 RID: 4130
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04001023 RID: 4131
		private static readonly ConfigurationProperty _propSwitchName = new ConfigurationProperty("switchName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001024 RID: 4132
		private static readonly ConfigurationProperty _propSwitchValue = new ConfigurationProperty("switchValue", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001025 RID: 4133
		private static readonly ConfigurationProperty _propSwitchType = new ConfigurationProperty("switchType", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001026 RID: 4134
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x04001027 RID: 4135
		private Hashtable _attributes;
	}
}
