using System;

namespace System.Configuration
{
	/// <summary>Represents a simplified configuration element used for updating elements in the configuration. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000188 RID: 392
	public sealed class SettingElement : ConfigurationElement
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0003BF94 File Offset: 0x0003A194
		static SettingElement()
		{
			SettingElement.properties.Add(SettingElement.name_prop);
			SettingElement.properties.Add(SettingElement.serialize_as_prop);
			SettingElement.properties.Add(SettingElement.value_prop);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElement" /> class.</summary>
		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		public SettingElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElement" /> class based on supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingElement" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used as the serialization scheme to store configuration settings.</param>
		// Token: 0x06000BB7 RID: 2999 RVA: 0x0003C02D File Offset: 0x0003A22D
		public SettingElement(string name, SettingsSerializeAs serializeAs)
		{
			this.Name = name;
			this.SerializeAs = serializeAs;
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingElement" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0003C043 File Offset: 0x0003A243
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x0003C055 File Offset: 0x0003A255
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[SettingElement.name_prop];
			}
			set
			{
				base[SettingElement.name_prop] = value;
			}
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Configuration.SettingElement" /> object by using a <see cref="T:System.Configuration.SettingValueElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingValueElement" /> object containing the value of the <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0003C063 File Offset: 0x0003A263
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x0003C075 File Offset: 0x0003A275
		[ConfigurationProperty("value", DefaultValue = null, Options = ConfigurationPropertyOptions.IsRequired)]
		public SettingValueElement Value
		{
			get
			{
				return (SettingValueElement)base[SettingElement.value_prop];
			}
			set
			{
				base[SettingElement.value_prop] = value;
			}
		}

		/// <summary>Gets or sets the serialization mechanism used to persist the values of the <see cref="T:System.Configuration.SettingElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0003C083 File Offset: 0x0003A283
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0003C0A4 File Offset: 0x0003A2A4
		[ConfigurationProperty("serializeAs", DefaultValue = SettingsSerializeAs.String, Options = ConfigurationPropertyOptions.IsRequired)]
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				if (base[SettingElement.serialize_as_prop] == null)
				{
					return SettingsSerializeAs.String;
				}
				return (SettingsSerializeAs)base[SettingElement.serialize_as_prop];
			}
			set
			{
				base[SettingElement.serialize_as_prop] = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0003C0B7 File Offset: 0x0003A2B7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingElement.properties;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.SettingElement" /> instance to the specified object.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.SettingElement" /> instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="settings">The object to compare with.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BBF RID: 3007 RVA: 0x0003C0C0 File Offset: 0x0003A2C0
		public override bool Equals(object settings)
		{
			SettingElement settingElement = settings as SettingElement;
			return settingElement != null && (settingElement.SerializeAs == this.SerializeAs && settingElement.Value == this.Value) && settingElement.Name == this.Name;
		}

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.SettingElement" /> current instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.SettingElement" /> current instance.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0003C108 File Offset: 0x0003A308
		public override int GetHashCode()
		{
			int num = (int)(this.SerializeAs ^ (SettingsSerializeAs)127);
			if (this.Name != null)
			{
				num += this.Name.GetHashCode() ^ 127;
			}
			if (this.Value != null)
			{
				num += this.Value.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000FD3 RID: 4051
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000FD4 RID: 4052
		private static ConfigurationProperty name_prop = new ConfigurationProperty("name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000FD5 RID: 4053
		private static ConfigurationProperty serialize_as_prop = new ConfigurationProperty("serializeAs", typeof(SettingsSerializeAs), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000FD6 RID: 4054
		private static ConfigurationProperty value_prop = new ConfigurationProperty("value", typeof(SettingValueElement), null, ConfigurationPropertyOptions.IsRequired);
	}
}
