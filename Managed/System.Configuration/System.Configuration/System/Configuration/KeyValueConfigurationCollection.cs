using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.KeyValueConfigurationElement" /> objects. </summary>
	// Token: 0x0200004F RID: 79
	[ConfigurationCollection(typeof(KeyValueConfigurationElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class KeyValueConfigurationCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the collection based on the supplied parameters.</summary>
		/// <param name="keyValue">A <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		// Token: 0x060002AA RID: 682 RVA: 0x0000836D File Offset: 0x0000656D
		public void Add(KeyValueConfigurationElement keyValue)
		{
			keyValue.Init();
			this.BaseAdd(keyValue);
		}

		/// <summary>Adds a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the collection based on the supplied parameters.</summary>
		/// <param name="key">A string specifying the key.</param>
		/// <param name="value">A string specifying the value.</param>
		// Token: 0x060002AB RID: 683 RVA: 0x0000837C File Offset: 0x0000657C
		public void Add(string key, string value)
		{
			this.Add(new KeyValueConfigurationElement(key, value));
		}

		/// <summary>Clears the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection.</summary>
		// Token: 0x060002AC RID: 684 RVA: 0x000075D5 File Offset: 0x000057D5
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object from the collection.</summary>
		/// <param name="key">A string specifying the <paramref name="key" />.</param>
		// Token: 0x060002AD RID: 685 RVA: 0x000075F4 File Offset: 0x000057F4
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		/// <summary>Gets the keys to all items contained in the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection.</summary>
		/// <returns>A string array.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000838C File Offset: 0x0000658C
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				int num = 0;
				foreach (object obj in this)
				{
					KeyValueConfigurationElement keyValueConfigurationElement = (KeyValueConfigurationElement)obj;
					array[num++] = keyValueConfigurationElement.Key;
				}
				return array;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object based on the supplied parameter.</summary>
		/// <returns>A configuration element, or null if the key does not exist in the collection.</returns>
		/// <param name="key">The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> contained in the collection.</param>
		// Token: 0x170000BF RID: 191
		public KeyValueConfigurationElement this[string key]
		{
			get
			{
				return (KeyValueConfigurationElement)base.BaseGet(key);
			}
		}

		/// <summary>When overridden in a derived class, the <see cref="M:System.Configuration.KeyValueConfigurationCollection.CreateNewElement" /> method creates a new <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>A newly created <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		// Token: 0x060002B0 RID: 688 RVA: 0x00008406 File Offset: 0x00006606
		protected override ConfigurationElement CreateNewElement()
		{
			return new KeyValueConfigurationElement();
		}

		/// <summary>Gets the element key for a specified configuration element when overridden in a derived class.</summary>
		/// <returns>An object that acts as the key for the specified <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Configuration.KeyValueConfigurationElement" /> to which the key should be returned.</param>
		// Token: 0x060002B1 RID: 689 RVA: 0x0000840D File Offset: 0x0000660D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((KeyValueConfigurationElement)element).Key;
		}

		/// <summary>Gets a collection of configuration properties.</summary>
		/// <returns>A collection of configuration properties.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000841A File Offset: 0x0000661A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection();
				}
				return this.properties;
			}
		}

		/// <summary>Gets a value indicating whether an attempt to add a duplicate <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection will cause an exception to be thrown.</summary>
		/// <returns>true if an attempt to add a duplicate <see cref="T:System.Configuration.KeyValueConfigurationElement" /> to the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> will cause an exception to be thrown; otherwise, false. </returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000023BB File Offset: 0x000005BB
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040000FF RID: 255
		private ConfigurationPropertyCollection properties;
	}
}
