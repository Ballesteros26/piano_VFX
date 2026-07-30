using System;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
	// Token: 0x02000062 RID: 98
	[ConfigurationCollection(typeof(ProviderSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ProviderSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Configuration.ProviderSettings" /> object to the collection.</summary>
		/// <param name="provider">The <see cref="T:System.Configuration.ProviderSettings" /> object to add.</param>
		// Token: 0x0600032D RID: 813 RVA: 0x000075CC File Offset: 0x000057CC
		public void Add(ProviderSettings provider)
		{
			this.BaseAdd(provider);
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x0600032E RID: 814 RVA: 0x000075D5 File Offset: 0x000057D5
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008F20 File Offset: 0x00007120
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderSettings();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008F27 File Offset: 0x00007127
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProviderSettings)element).Name;
		}

		/// <summary>Removes an element from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object to remove.</param>
		// Token: 0x06000331 RID: 817 RVA: 0x000075F4 File Offset: 0x000057F4
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Gets or sets a value at the specified index in the <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection.</summary>
		/// <returns>The specified <see cref="T:System.Configuration.ProviderSettings" />.</returns>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ProviderSettings" /> to return.</param>
		// Token: 0x170000EF RID: 239
		public ProviderSettings this[int index]
		{
			get
			{
				return (ProviderSettings)base.BaseGet(index);
			}
			set
			{
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets an item from the collection. </summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettings" /> object contained in the collection.</returns>
		/// <param name="key">A string reference to the <see cref="T:System.Configuration.ProviderSettings" /> object within the collection.</param>
		// Token: 0x170000F0 RID: 240
		public ProviderSettings this[string key]
		{
			get
			{
				return (ProviderSettings)base.BaseGet(key);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00008F5A File Offset: 0x0000715A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProviderSettingsCollection.props;
			}
		}

		// Token: 0x0400012B RID: 299
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
