using System;

namespace System.Configuration
{
	/// <summary>Represents a configuration element that contains a key/value pair. </summary>
	// Token: 0x02000050 RID: 80
	public class KeyValueConfigurationElement : ConfigurationElement
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00008438 File Offset: 0x00006638
		static KeyValueConfigurationElement()
		{
			KeyValueConfigurationElement.properties.Add(KeyValueConfigurationElement.keyProp);
			KeyValueConfigurationElement.properties.Add(KeyValueConfigurationElement.valueProp);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000067D7 File Offset: 0x000049D7
		internal KeyValueConfigurationElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> class based on the supplied parameters.</summary>
		/// <param name="key">The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		/// <param name="value">The value of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		// Token: 0x060002B7 RID: 695 RVA: 0x000084AA File Offset: 0x000066AA
		public KeyValueConfigurationElement(string key, string value)
		{
			base[KeyValueConfigurationElement.keyProp] = key;
			base[KeyValueConfigurationElement.valueProp] = value;
		}

		/// <summary>Gets the key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000084CA File Offset: 0x000066CA
		[ConfigurationProperty("key", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		public string Key
		{
			get
			{
				return (string)base[KeyValueConfigurationElement.keyProp];
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000084DC File Offset: 0x000066DC
		// (set) Token: 0x060002BA RID: 698 RVA: 0x000084EE File Offset: 0x000066EE
		[ConfigurationProperty("value", DefaultValue = "")]
		public string Value
		{
			get
			{
				return (string)base[KeyValueConfigurationElement.valueProp];
			}
			set
			{
				base[KeyValueConfigurationElement.valueProp] = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to its initial state.</summary>
		// Token: 0x060002BB RID: 699 RVA: 0x000023B9 File Offset: 0x000005B9
		[MonoTODO]
		protected internal override void Init()
		{
		}

		/// <summary>Gets the collection of properties. </summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> of properties for the element.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000084FC File Offset: 0x000066FC
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return KeyValueConfigurationElement.properties;
			}
		}

		// Token: 0x04000100 RID: 256
		private static ConfigurationProperty keyProp = new ConfigurationProperty("key", typeof(string), "", ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000101 RID: 257
		private static ConfigurationProperty valueProp = new ConfigurationProperty("value", typeof(string), "");

		// Token: 0x04000102 RID: 258
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
