using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.ProfileSettings" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005CE RID: 1486
	[ConfigurationCollection(typeof(ProfileSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ProfileSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.ProfileSettings" /> object to the collection.</summary>
		/// <param name="profilesSettings">A <see cref="T:System.Web.Configuration.ProfileSettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileSettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06004029 RID: 16425 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(ProfileSettings profilesSettings)
		{
			this.BaseAdd(profilesSettings);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.ProfileSettings" /> objects from the collection.</summary>
		// Token: 0x0600402A RID: 16426 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Indicates whether the collection contains a <see cref="T:System.Web.Configuration.ProfileSettings" /> object with the specified name.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Web.Configuration.ProfileSettings" /> object with the specified <paramref name="name" />; otherwise, false.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		// Token: 0x0600402B RID: 16427 RVA: 0x000A1AE9 File Offset: 0x0009FCE9
		public bool Contains(string name)
		{
			return base.BaseGet(name) != null;
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000A9381 File Offset: 0x000A7581
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProfileSettings();
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x000A9388 File Offset: 0x000A7588
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProfileSettings)element).Name;
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.ProfileSettings" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Configuration.ProfileSettings" /> object, or -1 if the object is not found in the collection.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		// Token: 0x0600402E RID: 16430 RVA: 0x000A9398 File Offset: 0x000A7598
		public int IndexOf(string name)
		{
			ProfileSettings profileSettings = (ProfileSettings)base.BaseGet(name);
			if (profileSettings == null)
			{
				return -1;
			}
			return base.BaseIndexOf(profileSettings);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		/// <param name="authorizationSettings">A <see cref="T:System.Web.Configuration.ProfileSettings" /> object to insert into the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileSettings" /> object to add already exists in the collection, the index is invalid, or the collection is read-only.</exception>
		// Token: 0x0600402F RID: 16431 RVA: 0x000A1B32 File Offset: 0x0009FD32
		[global::System.MonoTODO("why did they use 'Insert' and not 'Add' as other collections do?")]
		public void Insert(int index, ProfileSettings authorizationSettings)
		{
			this.BaseAdd(index, authorizationSettings);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object from the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.ProfileSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06004030 RID: 16432 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06004031 RID: 16433 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ProfileSettings" /> object based on the specified key in the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProfileSettings" /> object.</returns>
		/// <param name="key">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object contained in the collection.</param>
		// Token: 0x17001441 RID: 5185
		public ProfileSettings this[string key]
		{
			get
			{
				return (ProfileSettings)base.BaseGet(key);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified numeric index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		// Token: 0x17001442 RID: 5186
		public ProfileSettings this[int index]
		{
			get
			{
				return (ProfileSettings)base.BaseGet(index);
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

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x000A93DA File Offset: 0x000A75DA
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileSettingsCollection.properties;
			}
		}

		// Token: 0x040022CD RID: 8909
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
