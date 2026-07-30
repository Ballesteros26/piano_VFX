using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000066 RID: 102
	internal class SectionGroupInfo : ConfigInfo
	{
		// Token: 0x0600034D RID: 845 RVA: 0x000091D7 File Offset: 0x000073D7
		public SectionGroupInfo()
		{
			this.Type = typeof(ConfigurationSectionGroup);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000091EF File Offset: 0x000073EF
		public SectionGroupInfo(string groupName, string typeName)
		{
			this.Name = groupName;
			this.TypeName = typeName;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00009208 File Offset: 0x00007408
		public void AddChild(ConfigInfo data)
		{
			this.modified = true;
			data.Parent = this;
			if (data is SectionInfo)
			{
				if (this.sections == null)
				{
					this.sections = new ConfigInfoCollection();
				}
				this.sections[data.Name] = data;
				return;
			}
			if (this.groups == null)
			{
				this.groups = new ConfigInfoCollection();
			}
			this.groups[data.Name] = data;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00009276 File Offset: 0x00007476
		public void Clear()
		{
			this.modified = true;
			if (this.sections != null)
			{
				this.sections.Clear();
			}
			if (this.groups != null)
			{
				this.groups.Clear();
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000092A5 File Offset: 0x000074A5
		public bool HasChild(string name)
		{
			return (this.sections != null && this.sections[name] != null) || (this.groups != null && this.groups[name] != null);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000092D8 File Offset: 0x000074D8
		public void RemoveChild(string name)
		{
			this.modified = true;
			if (this.sections != null)
			{
				this.sections.Remove(name);
			}
			if (this.groups != null)
			{
				this.groups.Remove(name);
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00009309 File Offset: 0x00007509
		public SectionInfo GetChildSection(string name)
		{
			if (this.sections != null)
			{
				return this.sections[name] as SectionInfo;
			}
			return null;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00009326 File Offset: 0x00007526
		public SectionGroupInfo GetChildGroup(string name)
		{
			if (this.groups != null)
			{
				return this.groups[name] as SectionGroupInfo;
			}
			return null;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00009343 File Offset: 0x00007543
		public ConfigInfoCollection Sections
		{
			get
			{
				if (this.sections == null)
				{
					return SectionGroupInfo.emptyList;
				}
				return this.sections;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00009359 File Offset: 0x00007559
		public ConfigInfoCollection Groups
		{
			get
			{
				if (this.groups == null)
				{
					return SectionGroupInfo.emptyList;
				}
				return this.groups;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009370 File Offset: 0x00007570
		public override bool HasDataContent(Configuration config)
		{
			foreach (ConfigInfoCollection configInfoCollection in new object[] { this.Sections, this.Groups })
			{
				foreach (object obj in configInfoCollection)
				{
					string text = (string)obj;
					if (configInfoCollection[text].HasDataContent(config))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000940C File Offset: 0x0000760C
		public override bool HasConfigContent(Configuration cfg)
		{
			if (base.StreamName == cfg.FileName)
			{
				return true;
			}
			foreach (ConfigInfoCollection configInfoCollection in new object[] { this.Sections, this.Groups })
			{
				foreach (object obj in configInfoCollection)
				{
					string text = (string)obj;
					if (configInfoCollection[text].HasConfigContent(cfg))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000094BC File Offset: 0x000076BC
		public override void ReadConfig(Configuration cfg, string streamName, XmlReader reader)
		{
			base.StreamName = streamName;
			this.ConfigHost = cfg.ConfigHost;
			if (reader.LocalName != "configSections")
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.Name == "name")
					{
						this.Name = reader.Value;
					}
					else if (reader.Name == "type")
					{
						this.TypeName = reader.Value;
						this.Type = null;
					}
					else
					{
						base.ThrowException("Unrecognized attribute", reader);
					}
				}
				if (this.Name == null)
				{
					base.ThrowException("sectionGroup must have a 'name' attribute", reader);
				}
				if (this.Name == "location")
				{
					base.ThrowException("location is a reserved section name", reader);
				}
			}
			if (this.TypeName == null)
			{
				this.TypeName = "System.Configuration.ConfigurationSectionGroup";
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			reader.MoveToContent();
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
				}
				else
				{
					string localName = reader.LocalName;
					ConfigInfo configInfo = null;
					if (localName == "remove")
					{
						this.ReadRemoveSection(reader);
					}
					else if (localName == "clear")
					{
						if (reader.HasAttributes)
						{
							base.ThrowException("Unrecognized attribute.", reader);
						}
						this.Clear();
						reader.Skip();
					}
					else
					{
						if (localName == "section")
						{
							configInfo = new SectionInfo();
						}
						else if (localName == "sectionGroup")
						{
							configInfo = new SectionGroupInfo();
						}
						else
						{
							base.ThrowException("Unrecognized element: " + reader.Name, reader);
						}
						configInfo.ReadConfig(cfg, streamName, reader);
						ConfigInfo configInfo2 = this.Groups[configInfo.Name];
						if (configInfo2 == null)
						{
							configInfo2 = this.Sections[configInfo.Name];
						}
						if (configInfo2 != null)
						{
							if (configInfo2.GetType() != configInfo.GetType())
							{
								base.ThrowException("A section or section group named '" + configInfo.Name + "' already exists", reader);
							}
							configInfo2.Merge(configInfo);
							configInfo2.StreamName = streamName;
						}
						else
						{
							this.AddChild(configInfo);
						}
					}
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000096EC File Offset: 0x000078EC
		public override void WriteConfig(Configuration cfg, XmlWriter writer, ConfigurationSaveMode mode)
		{
			if (this.Name != null)
			{
				writer.WriteStartElement("sectionGroup");
				writer.WriteAttributeString("name", this.Name);
				if (this.TypeName != null && this.TypeName != "" && this.TypeName != "System.Configuration.ConfigurationSectionGroup")
				{
					writer.WriteAttributeString("type", this.TypeName);
				}
			}
			else
			{
				writer.WriteStartElement("configSections");
			}
			foreach (ConfigInfoCollection configInfoCollection in new object[] { this.Sections, this.Groups })
			{
				foreach (object obj in configInfoCollection)
				{
					string text = (string)obj;
					ConfigInfo configInfo = configInfoCollection[text];
					if (configInfo.HasConfigContent(cfg))
					{
						configInfo.WriteConfig(cfg, writer, mode);
					}
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00009800 File Offset: 0x00007A00
		private void ReadRemoveSection(XmlReader reader)
		{
			if (!reader.MoveToNextAttribute() || reader.Name != "name")
			{
				base.ThrowException("Unrecognized attribute.", reader);
			}
			string value = reader.Value;
			if (string.IsNullOrEmpty(value))
			{
				base.ThrowException("Empty name to remove", reader);
			}
			reader.MoveToElement();
			if (!this.HasChild(value))
			{
				base.ThrowException("No factory for " + value, reader);
			}
			this.RemoveChild(value);
			reader.Skip();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000987D File Offset: 0x00007A7D
		public void ReadRootData(XmlReader reader, Configuration config, bool overrideAllowed)
		{
			reader.MoveToContent();
			this.ReadContent(reader, config, overrideAllowed, true);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00009890 File Offset: 0x00007A90
		public override void ReadData(Configuration config, XmlReader reader, bool overrideAllowed)
		{
			reader.MoveToContent();
			if (!reader.IsEmptyElement)
			{
				reader.ReadStartElement();
				this.ReadContent(reader, config, overrideAllowed, false);
				reader.MoveToContent();
				reader.ReadEndElement();
				return;
			}
			reader.Read();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000098C8 File Offset: 0x00007AC8
		private void ReadContent(XmlReader reader, Configuration config, bool overrideAllowed, bool root)
		{
			while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.None)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
				}
				else if (reader.LocalName == "dllmap")
				{
					reader.Skip();
				}
				else if (reader.LocalName == "location")
				{
					if (!root)
					{
						base.ThrowException("<location> elements are only allowed in <configuration> elements.", reader);
					}
					string attribute = reader.GetAttribute("allowOverride");
					bool flag = attribute == null || attribute.Length == 0 || bool.Parse(attribute);
					string attribute2 = reader.GetAttribute("path");
					if (attribute2 != null && attribute2.Length > 0)
					{
						string text = reader.ReadOuterXml();
						string[] array = attribute2.Split(new char[] { ',' });
						for (int i = 0; i < array.Length; i++)
						{
							string text2 = array[i].Trim();
							if (config.Locations.Find(text2) != null)
							{
								base.ThrowException("Sections must only appear once per config file.", reader);
							}
							ConfigurationLocation configurationLocation = new ConfigurationLocation(text2, text, config, flag);
							config.Locations.Add(configurationLocation);
						}
					}
					else
					{
						this.ReadData(config, reader, flag);
					}
				}
				else
				{
					ConfigInfo configInfo = this.GetConfigInfo(reader, this);
					if (configInfo != null)
					{
						configInfo.ReadData(config, reader, overrideAllowed);
					}
					else
					{
						base.ThrowException("Unrecognized configuration section <" + reader.LocalName + ">", reader);
					}
				}
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009A30 File Offset: 0x00007C30
		private ConfigInfo GetConfigInfo(XmlReader reader, SectionGroupInfo current)
		{
			ConfigInfo configInfo = null;
			if (current.sections != null)
			{
				configInfo = current.sections[reader.LocalName];
			}
			if (configInfo != null)
			{
				return configInfo;
			}
			if (current.groups != null)
			{
				configInfo = current.groups[reader.LocalName];
			}
			if (configInfo != null)
			{
				return configInfo;
			}
			if (current.groups == null)
			{
				return null;
			}
			foreach (object obj in current.groups.AllKeys)
			{
				string text = (string)obj;
				configInfo = this.GetConfigInfo(reader, (SectionGroupInfo)current.groups[text]);
				if (configInfo != null)
				{
					return configInfo;
				}
			}
			return null;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009AF8 File Offset: 0x00007CF8
		internal override void Merge(ConfigInfo newData)
		{
			SectionGroupInfo sectionGroupInfo = newData as SectionGroupInfo;
			if (sectionGroupInfo == null)
			{
				return;
			}
			if (sectionGroupInfo.sections != null && sectionGroupInfo.sections.Count > 0)
			{
				foreach (object obj in sectionGroupInfo.sections.AllKeys)
				{
					string text = (string)obj;
					if (this.sections[text] == null)
					{
						this.sections.Add(text, sectionGroupInfo.sections[text]);
					}
				}
			}
			if (sectionGroupInfo.groups != null && sectionGroupInfo.sections != null && sectionGroupInfo.sections.Count > 0)
			{
				foreach (object obj2 in sectionGroupInfo.groups.AllKeys)
				{
					string text2 = (string)obj2;
					if (this.groups[text2] == null)
					{
						this.groups.Add(text2, sectionGroupInfo.groups[text2]);
					}
				}
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009C24 File Offset: 0x00007E24
		public void WriteRootData(XmlWriter writer, Configuration config, ConfigurationSaveMode mode)
		{
			this.WriteContent(writer, config, mode, false);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009C30 File Offset: 0x00007E30
		public override void WriteData(Configuration config, XmlWriter writer, ConfigurationSaveMode mode)
		{
			writer.WriteStartElement(this.Name);
			this.WriteContent(writer, config, mode, true);
			writer.WriteEndElement();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00009C50 File Offset: 0x00007E50
		public void WriteContent(XmlWriter writer, Configuration config, ConfigurationSaveMode mode, bool writeElem)
		{
			foreach (ConfigInfoCollection configInfoCollection in new object[] { this.Sections, this.Groups })
			{
				foreach (object obj in configInfoCollection)
				{
					string text = (string)obj;
					ConfigInfo configInfo = configInfoCollection[text];
					if (configInfo.HasDataContent(config))
					{
						configInfo.WriteData(config, writer, mode);
					}
				}
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00009CF0 File Offset: 0x00007EF0
		internal override bool HasValues(Configuration config, ConfigurationSaveMode mode)
		{
			if (this.modified && mode == ConfigurationSaveMode.Modified)
			{
				return true;
			}
			foreach (ConfigInfoCollection configInfoCollection in new object[] { this.Sections, this.Groups })
			{
				foreach (object obj in configInfoCollection)
				{
					string text = (string)obj;
					if (configInfoCollection[text].HasValues(config, mode))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00009D98 File Offset: 0x00007F98
		internal override void ResetModified(Configuration config)
		{
			this.modified = false;
			foreach (ConfigInfoCollection configInfoCollection in new object[] { this.Sections, this.Groups })
			{
				foreach (object obj in configInfoCollection)
				{
					string text = (string)obj;
					configInfoCollection[text].ResetModified(config);
				}
			}
		}

		// Token: 0x04000134 RID: 308
		private bool modified;

		// Token: 0x04000135 RID: 309
		private ConfigInfoCollection sections;

		// Token: 0x04000136 RID: 310
		private ConfigInfoCollection groups;

		// Token: 0x04000137 RID: 311
		private static ConfigInfoCollection emptyList = new ConfigInfoCollection();
	}
}
