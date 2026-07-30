using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a set of <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> objects.</summary>
	// Token: 0x020005C8 RID: 1480
	[ConfigurationCollection(typeof(ProfileGroupSettings), AddItemName = "group", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ProfileGroupSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to the collection.</summary>
		/// <param name="group">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to add already exists in the collection, or the collection is read-only. </exception>
		// Token: 0x06003FCA RID: 16330 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(ProfileGroupSettings group)
		{
			this.BaseAdd(group);
		}

		/// <summary>Gets a string array of all the key values in the collection.</summary>
		/// <returns>A string array of all the key values in the collection.</returns>
		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x000A89D8 File Offset: 0x000A6BD8
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Name;
				}
				return array;
			}
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x000A8A12 File Offset: 0x000A6C12
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x000A8A1A File Offset: 0x000A6C1A
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> objects from the collection.</summary>
		// Token: 0x06003FCE RID: 16334 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x000A8A22 File Offset: 0x000A6C22
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProfileGroupSettings();
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to get.</param>
		// Token: 0x06003FD0 RID: 16336 RVA: 0x000A8A29 File Offset: 0x000A6C29
		public ProfileGroupSettings Get(int index)
		{
			return (ProfileGroupSettings)base.BaseGet(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name, or null if the name does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to get.</param>
		// Token: 0x06003FD1 RID: 16337 RVA: 0x000A8A37 File Offset: 0x000A6C37
		public ProfileGroupSettings Get(string name)
		{
			return (ProfileGroupSettings)base.BaseGet(name);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x000A8A45 File Offset: 0x000A6C45
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProfileGroupSettings)element).Name;
		}

		/// <summary>Returns the name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object.</param>
		// Token: 0x06003FD3 RID: 16339 RVA: 0x000A09E6 File Offset: 0x0009EBE6
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object, or -1 if the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object is not contained in the collection.</returns>
		/// <param name="group">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object in the collection.</param>
		// Token: 0x06003FD4 RID: 16340 RVA: 0x0009FDDA File Offset: 0x0009DFDA
		public int IndexOf(ProfileGroupSettings group)
		{
			return base.BaseIndexOf(group);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There is no <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003FD5 RID: 16341 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There is no <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only. </exception>
		// Token: 0x06003FD6 RID: 16342 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to the collection.</summary>
		/// <param name="group">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to add already exists in the collection, or the collection is read-only. </exception>
		// Token: 0x06003FD7 RID: 16343 RVA: 0x000A8A54 File Offset: 0x000A6C54
		public void Set(ProfileGroupSettings group)
		{
			ProfileGroupSettings profileGroupSettings = this.Get(group.Name);
			if (profileGroupSettings == null)
			{
				this.Add(group);
				return;
			}
			int num = base.BaseIndexOf(profileGroupSettings);
			this.RemoveAt(num);
			this.BaseAdd(num, group);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object in the collection.</param>
		// Token: 0x17001422 RID: 5154
		public ProfileGroupSettings this[int index]
		{
			get
			{
				return this.Get(index);
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

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name, or null if there is no object with that name.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object in the collection.</param>
		// Token: 0x17001423 RID: 5155
		public ProfileGroupSettings this[string name]
		{
			get
			{
				return (ProfileGroupSettings)base.BaseGet(name);
			}
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x000A8A99 File Offset: 0x000A6C99
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileGroupSettingsCollection.properties;
			}
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000A8AA0 File Offset: 0x000A6CA0
		internal void ResetInternal(ConfigurationElement parentElement)
		{
			this.Reset(parentElement);
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x0009F555 File Offset: 0x0009D755
		internal void AddNewSettings(ProfileGroupSettings newSettings)
		{
			base.BaseAdd(newSettings, false);
		}

		// Token: 0x040022B5 RID: 8885
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
