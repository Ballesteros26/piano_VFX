using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>A collection of <see cref="T:System.Web.Configuration.RuleSettings" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005D5 RID: 1493
	[ConfigurationCollection(typeof(RuleSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class RuleSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.RuleSettings" /> object to the collection.</summary>
		/// <param name="ruleSettings">A <see cref="T:System.Web.Configuration.RuleSettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.RuleSettings" /> object to add already exists in the collection or the collection is read-only.</exception>
		// Token: 0x0600407E RID: 16510 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(RuleSettings ruleSettings)
		{
			this.BaseAdd(ruleSettings);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.RuleSettings" /> objects from the collection.</summary>
		// Token: 0x0600407F RID: 16511 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns true if the collection contains a <see cref="T:System.Web.Configuration.RuleSettings" /> object with the specified name.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Web.Configuration.RuleSettings" /> object with the specified name; otherwise, false.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		// Token: 0x06004080 RID: 16512 RVA: 0x000A1AE9 File Offset: 0x0009FCE9
		public bool Contains(string name)
		{
			return base.BaseGet(name) != null;
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000A9EF4 File Offset: 0x000A80F4
		protected override ConfigurationElement CreateNewElement()
		{
			return new RuleSettings();
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000A9EFB File Offset: 0x000A80FB
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((RuleSettings)element).Name;
		}

		/// <summary>Finds the index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection with the specified name.</summary>
		/// <returns>The index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection with the specified name.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		// Token: 0x06004083 RID: 16515 RVA: 0x000A9F08 File Offset: 0x000A8108
		public int IndexOf(string name)
		{
			RuleSettings ruleSettings = (RuleSettings)base.BaseGet(name);
			if (ruleSettings == null)
			{
				return -1;
			}
			return base.BaseIndexOf(ruleSettings);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.RuleSettings" /> object to the specified index point in the collection.</summary>
		/// <param name="index">A valid index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		/// <param name="eventSettings">The <see cref="T:System.Web.Configuration.RuleSettings" /> object to insert into the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.RuleSettings" /> object to add already exists in the collection, the index is invalid, or the collection is read only.</exception>
		// Token: 0x06004084 RID: 16516 RVA: 0x000A1B32 File Offset: 0x0009FD32
		[global::System.MonoTODO("why did they use 'Insert' and not 'Add' as other collections do?")]
		public void Insert(int index, RuleSettings eventSettings)
		{
			this.BaseAdd(index, eventSettings);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.RuleSettings" /> object from the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.RuleSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06004085 RID: 16517 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified index location from the collection.</summary>
		/// <param name="index">A valid index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06004086 RID: 16518 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.RuleSettings" /> object based on the specified key in the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.RuleSettings" /> object.</returns>
		/// <param name="key">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object contained in the collection.</param>
		// Token: 0x17001460 RID: 5216
		public RuleSettings this[string key]
		{
			get
			{
				return (RuleSettings)base.BaseGet(key);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified numeric index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Index is out of range.</exception>
		// Token: 0x17001461 RID: 5217
		public RuleSettings this[int index]
		{
			get
			{
				return (RuleSettings)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x000A9F4A File Offset: 0x000A814A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RuleSettingsCollection.properties;
			}
		}

		// Token: 0x040022F5 RID: 8949
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
