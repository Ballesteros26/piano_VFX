using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure XML serialization.</summary>
	// Token: 0x0200037D RID: 893
	public sealed class SerializationSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the object that represents the section that contains configuration elements for the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionsSection" /> that represents the schemaImporterExtenstion element in the configuration file.</returns>
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x000DD460 File Offset: 0x000DB660
		[ConfigurationProperty("schemaImporterExtensions")]
		public SchemaImporterExtensionsSection SchemaImporterExtensions
		{
			get
			{
				return (SchemaImporterExtensionsSection)base.Sections["schemaImporterExtensions"];
			}
		}

		/// <summary>Gets the object that represents the <see cref="T:System.DateTime" /> serialization configuration element.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.DateTimeSerializationSection" /> object that represents the configuration element.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x000DD477 File Offset: 0x000DB677
		[ConfigurationProperty("dateTimeSerialization")]
		public DateTimeSerializationSection DateTimeSerialization
		{
			get
			{
				return (DateTimeSerializationSection)base.Sections["dateTimeSerialization"];
			}
		}

		/// <summary>Gets the object that represents the configuration group for the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.XmlSerializerSection" /> that represents the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x000DD48E File Offset: 0x000DB68E
		public XmlSerializerSection XmlSerializer
		{
			get
			{
				return (XmlSerializerSection)base.Sections["xmlSerializer"];
			}
		}
	}
}
