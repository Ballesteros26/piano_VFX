using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides a collection of <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200059B RID: 1435
	[ConfigurationCollection(typeof(EventMappingSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class EventMappingSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to the collection.</summary>
		/// <param name="eventMappingSettings">An <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003CD0 RID: 15568 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(EventMappingSettings eventMappingSettings)
		{
			this.BaseAdd(eventMappingSettings);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects from the collection.</summary>
		// Token: 0x06003CD1 RID: 15569 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Indicates whether the collection contains an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified name.</summary>
		/// <returns>true if the collection contains an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified name; otherwise, false.</returns>
		/// <param name="name">The name of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		// Token: 0x06003CD2 RID: 15570 RVA: 0x000A1AE9 File Offset: 0x0009FCE9
		public bool Contains(string name)
		{
			return base.BaseGet(name) != null;
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000A1AF5 File Offset: 0x0009FCF5
		protected override ConfigurationElement CreateNewElement()
		{
			return new EventMappingSettings();
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000A1AFC File Offset: 0x0009FCFC
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((EventMappingSettings)element).Name;
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.EventMappingSettings" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Configuration.EventMappingSettings" /> object, or -1 if the object is not found in the collection.</returns>
		/// <param name="name">The name of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		// Token: 0x06003CD5 RID: 15573 RVA: 0x000A1B0C File Offset: 0x0009FD0C
		public int IndexOf(string name)
		{
			EventMappingSettings eventMappingSettings = (EventMappingSettings)base.BaseGet(name);
			if (eventMappingSettings == null)
			{
				return -1;
			}
			return base.BaseIndexOf(eventMappingSettings);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to the specified index point in the collection.</summary>
		/// <param name="index">A valid index of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		/// <param name="eventMappingSettings">The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to insert into the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to add already exists in the collection, the index is invalid, or the collection is read-only.</exception>
		// Token: 0x06003CD6 RID: 15574 RVA: 0x000A1B32 File Offset: 0x0009FD32
		[global::System.MonoTODO("why did they use 'Insert' and not 'Add' as other collections do?")]
		public void Insert(int index, EventMappingSettings eventMappingSettings)
		{
			this.BaseAdd(index, eventMappingSettings);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003CD7 RID: 15575 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object from the collection.</summary>
		/// <param name="index">The index of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003CD8 RID: 15576 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">A valid index of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		// Token: 0x170012BC RID: 4796
		public EventMappingSettings this[int index]
		{
			get
			{
				return (EventMappingSettings)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object based on the specified key in the collection.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.EventMappingSettings" /> object.</returns>
		/// <param name="key">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object contained in the collection.</param>
		// Token: 0x170012BD RID: 4797
		public EventMappingSettings this[string key]
		{
			get
			{
				return (EventMappingSettings)base.BaseGet(key);
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x000A1B58 File Offset: 0x0009FD58
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return EventMappingSettingsCollection.properties;
			}
		}

		// Token: 0x040020E0 RID: 8416
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
