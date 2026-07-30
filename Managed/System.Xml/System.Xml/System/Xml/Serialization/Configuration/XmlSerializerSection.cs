using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure XML serialization. </summary>
	// Token: 0x0200037E RID: 894
	public sealed class XmlSerializerSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.XmlSerializerSection" /> class. </summary>
		// Token: 0x06002447 RID: 9287 RVA: 0x000DD4A8 File Offset: 0x000DB6A8
		public XmlSerializerSection()
		{
			this.properties.Add(this.checkDeserializeAdvances);
			this.properties.Add(this.tempFilesLocation);
			this.properties.Add(this.useLegacySerializerGeneration);
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x000DD55D File Offset: 0x000DB75D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value that determines whether an additional check of progress of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is done.</summary>
		/// <returns>true if the check is made; otherwise, false. The default is true.</returns>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x000DD565 File Offset: 0x000DB765
		// (set) Token: 0x0600244A RID: 9290 RVA: 0x000DD578 File Offset: 0x000DB778
		[ConfigurationProperty("checkDeserializeAdvances", DefaultValue = false)]
		public bool CheckDeserializeAdvances
		{
			get
			{
				return (bool)base[this.checkDeserializeAdvances];
			}
			set
			{
				base[this.checkDeserializeAdvances] = value;
			}
		}

		/// <summary>Returns the location that was specified for the creation of the temporary file.</summary>
		/// <returns>The location that was specified for the creation of the temporary file.</returns>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x000DD58C File Offset: 0x000DB78C
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x000DD59F File Offset: 0x000DB79F
		[ConfigurationProperty("tempFilesLocation", DefaultValue = null)]
		public string TempFilesLocation
		{
			get
			{
				return (string)base[this.tempFilesLocation];
			}
			set
			{
				base[this.tempFilesLocation] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the specified object uses legacy serializer generation.</summary>
		/// <returns>true if the object uses legacy serializer generation; otherwise, false.</returns>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x000DD5AE File Offset: 0x000DB7AE
		// (set) Token: 0x0600244E RID: 9294 RVA: 0x000DD5C1 File Offset: 0x000DB7C1
		[ConfigurationProperty("useLegacySerializerGeneration", DefaultValue = false)]
		public bool UseLegacySerializerGeneration
		{
			get
			{
				return (bool)base[this.useLegacySerializerGeneration];
			}
			set
			{
				base[this.useLegacySerializerGeneration] = value;
			}
		}

		// Token: 0x040018B9 RID: 6329
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040018BA RID: 6330
		private readonly ConfigurationProperty checkDeserializeAdvances = new ConfigurationProperty("checkDeserializeAdvances", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x040018BB RID: 6331
		private readonly ConfigurationProperty tempFilesLocation = new ConfigurationProperty("tempFilesLocation", typeof(string), null, null, new RootedPathValidator(), ConfigurationPropertyOptions.None);

		// Token: 0x040018BC RID: 6332
		private readonly ConfigurationProperty useLegacySerializerGeneration = new ConfigurationProperty("useLegacySerializerGeneration", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
