using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>Acts as the top of a two-level named hierarchy of <see cref="T:System.Web.Configuration.ProfilePropertySettingsCollection" /> collections.</summary>
	// Token: 0x020005D3 RID: 1491
	[ConfigurationCollection(typeof(ProfilePropertySettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class RootProfilePropertySettingsCollection : ProfilePropertySettingsCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> class using default settings.</summary>
		// Token: 0x0600405B RID: 16475 RVA: 0x000A9A1D File Offset: 0x000A7C1D
		public RootProfilePropertySettingsCollection()
		{
			this.groupSettings = new ProfileGroupSettingsCollection();
		}

		/// <summary>Compares the current <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object to another A <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object.</summary>
		/// <returns>true if the passed <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object is equal to the current object; otherwise, false.</returns>
		/// <param name="rootProfilePropertySettingsCollection">A <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object to compare to.</param>
		// Token: 0x0600405C RID: 16476 RVA: 0x000A9A30 File Offset: 0x000A7C30
		public override bool Equals(object rootProfilePropertySettingsCollection)
		{
			RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection2 = rootProfilePropertySettingsCollection as RootProfilePropertySettingsCollection;
			if (rootProfilePropertySettingsCollection2 == null)
			{
				return false;
			}
			if (base.GetType() != rootProfilePropertySettingsCollection2.GetType())
			{
				return false;
			}
			if (base.Count != rootProfilePropertySettingsCollection2.Count)
			{
				return false;
			}
			for (int i = 0; i < base.Count; i++)
			{
				if (!base.BaseGet(i).Equals(rootProfilePropertySettingsCollection2.BaseGet(i)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Generates a hash code for the collection.</summary>
		/// <returns>Unique integer hash code for the current object.</returns>
		// Token: 0x0600405D RID: 16477 RVA: 0x000A9A98 File Offset: 0x000A7C98
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < base.Count; i++)
			{
				num += base.BaseGet(i).GetHashCode();
			}
			return num;
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x00008B66 File Offset: 0x00006D66
		protected override bool AllowClear
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000A9AC8 File Offset: 0x000A7CC8
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (elementName == "group")
			{
				ProfileGroupSettings profileGroupSettings = new ProfileGroupSettings();
				profileGroupSettings.DoDeserialize(reader);
				this.GroupSettings.AddNewSettings(profileGroupSettings);
				return true;
			}
			return base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000A9B05 File Offset: 0x000A7D05
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.ProfileGroupSettingsCollection" /> containing a collection of <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProfileGroupSettingsCollection" /> collection.</returns>
		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x000A9B10 File Offset: 0x000A7D10
		[ConfigurationProperty("group")]
		public ProfileGroupSettingsCollection GroupSettings
		{
			get
			{
				return this.groupSettings;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x000A9B18 File Offset: 0x000A7D18
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RootProfilePropertySettingsCollection.properties;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x00008B66 File Offset: 0x00006D66
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000A8A12 File Offset: 0x000A6C12
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000A8A1A File Offset: 0x000A6C1A
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000A9B20 File Offset: 0x000A7D20
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection = (RootProfilePropertySettingsCollection)parentElement;
			if (rootProfilePropertySettingsCollection == null)
			{
				return;
			}
			this.GroupSettings.ResetInternal(rootProfilePropertySettingsCollection.GroupSettings);
		}

		// Token: 0x040022EA RID: 8938
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040022EB RID: 8939
		private ProfileGroupSettingsCollection groupSettings;
	}
}
