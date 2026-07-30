using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles configuration settings for XML serialization of <see cref="T:System.DateTime" /> instances.</summary>
	// Token: 0x02000376 RID: 886
	public sealed class DateTimeSerializationSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.DateTimeSerializationSection" /> class.</summary>
		// Token: 0x0600241B RID: 9243 RVA: 0x000DCD7C File Offset: 0x000DAF7C
		public DateTimeSerializationSection()
		{
			this.properties.Add(this.mode);
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x000DCDDC File Offset: 0x000DAFDC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value that determines the serialization format.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Serialization.Configuration.DateTimeSerializationSection.DateTimeSerializationMode" /> values.</returns>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x000DCDE4 File Offset: 0x000DAFE4
		// (set) Token: 0x0600241E RID: 9246 RVA: 0x000DCDF7 File Offset: 0x000DAFF7
		[ConfigurationProperty("mode", DefaultValue = DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip)]
		public DateTimeSerializationSection.DateTimeSerializationMode Mode
		{
			get
			{
				return (DateTimeSerializationSection.DateTimeSerializationMode)base[this.mode];
			}
			set
			{
				base[this.mode] = value;
			}
		}

		// Token: 0x040018AC RID: 6316
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040018AD RID: 6317
		private readonly ConfigurationProperty mode = new ConfigurationProperty("mode", typeof(DateTimeSerializationSection.DateTimeSerializationMode), DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip, new EnumConverter(typeof(DateTimeSerializationSection.DateTimeSerializationMode)), null, ConfigurationPropertyOptions.None);

		/// <summary>Determines XML serialization format of <see cref="T:System.DateTime" /> objects.</summary>
		// Token: 0x02000377 RID: 887
		public enum DateTimeSerializationMode
		{
			/// <summary>Same as Roundtrip.</summary>
			// Token: 0x040018AF RID: 6319
			Default,
			/// <summary>The serializer examines individual <see cref="T:System.DateTime" />  instances to determine the serialization format: UTC, local, or unspecified.</summary>
			// Token: 0x040018B0 RID: 6320
			Roundtrip,
			/// <summary>The serializer formats all <see cref="T:System.DateTime" /> objects as local time. This is for version 1.0 and 1.1 compatibility.</summary>
			// Token: 0x040018B1 RID: 6321
			Local
		}
	}
}
