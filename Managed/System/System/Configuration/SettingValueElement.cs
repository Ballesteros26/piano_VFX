using System;
using System.Reflection;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Contains the XML representing the serialized value of the setting. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018A RID: 394
	public sealed class SettingValueElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingValueElement" /> class. </summary>
		// Token: 0x06000BCA RID: 3018 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		[MonoTODO]
		public SettingValueElement()
		{
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0003C203 File Offset: 0x0003A403
		[MonoTODO]
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Configuration.SettingValueElement" /> object by using an <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> object containing the value of a <see cref="T:System.Configuration.SettingElement" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0003C20B File Offset: 0x0003A40B
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0003C213 File Offset: 0x0003A413
		public XmlNode ValueXml
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0003C21C File Offset: 0x0003A41C
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.original = new XmlDocument().ReadNode(reader);
			this.node = this.original.CloneNode(true);
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.SettingValueElement" /> instance to the specified object.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.SettingValueElement" /> instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="settingValue">The object to compare.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BCF RID: 3023 RVA: 0x00004239 File Offset: 0x00002439
		public override bool Equals(object settingValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BD0 RID: 3024 RVA: 0x00004239 File Offset: 0x00002439
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0003C241 File Offset: 0x0003A441
		protected override bool IsModified()
		{
			return this.original != this.node;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0003C254 File Offset: 0x0003A454
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.node = null;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0003C25D File Offset: 0x0003A45D
		protected override void ResetModified()
		{
			this.node = this.original;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003C26B File Offset: 0x0003A46B
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (this.node == null)
			{
				return false;
			}
			this.node.WriteTo(writer);
			return true;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0003C284 File Offset: 0x0003A484
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (parentElement != null && sourceElement.GetType() != parentElement.GetType())
			{
				throw new ConfigurationErrorsException("Can't unmerge two elements of different type");
			}
			bool flag = saveMode == ConfigurationSaveMode.Minimal || saveMode == ConfigurationSaveMode.Modified;
			foreach (object obj in sourceElement.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin != PropertyValueOrigin.Default)
				{
					PropertyInformation propertyInformation2 = base.ElementInformation.Properties[propertyInformation.Name];
					object value = propertyInformation.Value;
					if (parentElement == null || !this.HasValue(parentElement, propertyInformation.Name))
					{
						propertyInformation2.Value = value;
					}
					else if (value != null)
					{
						object item = this.GetItem(parentElement, propertyInformation.Name);
						if (!this.PropertyIsElement(propertyInformation))
						{
							if (!object.Equals(value, item) || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere))
							{
								propertyInformation2.Value = value;
							}
						}
						else
						{
							ConfigurationElement configurationElement = (ConfigurationElement)value;
							if (!flag || this.ElementIsModified(configurationElement))
							{
								if (item == null)
								{
									propertyInformation2.Value = value;
								}
								else
								{
									ConfigurationElement configurationElement2 = (ConfigurationElement)item;
									ConfigurationElement configurationElement3 = (ConfigurationElement)propertyInformation2.Value;
									this.ElementUnmerge(configurationElement3, configurationElement, configurationElement2, saveMode);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0003C3E8 File Offset: 0x0003A5E8
		private bool HasValue(ConfigurationElement element, string propName)
		{
			PropertyInformation propertyInformation = element.ElementInformation.Properties[propName];
			return propertyInformation != null && propertyInformation.ValueOrigin > PropertyValueOrigin.Default;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003C415 File Offset: 0x0003A615
		private object GetItem(ConfigurationElement element, string property)
		{
			PropertyInformation propertyInformation = base.ElementInformation.Properties[property];
			if (propertyInformation == null)
			{
				throw new InvalidOperationException("Property '" + property + "' not found in configuration element");
			}
			return propertyInformation.Value;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0003C446 File Offset: 0x0003A646
		private bool PropertyIsElement(PropertyInformation prop)
		{
			return typeof(ConfigurationElement).IsAssignableFrom(prop.Type);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0003C45D File Offset: 0x0003A65D
		private bool ElementIsModified(ConfigurationElement element)
		{
			return (bool)element.GetType().GetMethod("IsModified", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(element, new object[0]);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0003C482 File Offset: 0x0003A682
		private void ElementUnmerge(ConfigurationElement target, ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			target.GetType().GetMethod("Unmerge", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(target, new object[] { sourceElement, parentElement, saveMode });
		}

		// Token: 0x04000FD7 RID: 4055
		private XmlNode node;

		// Token: 0x04000FD8 RID: 4056
		private XmlNode original;
	}
}
