using System;
using System.Configuration;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the configuration for the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> class. This class cannot be inherited.</summary>
	// Token: 0x0200037C RID: 892
	public sealed class SchemaImporterExtensionsSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionsSection" /> class.</summary>
		// Token: 0x0600243D RID: 9277 RVA: 0x000DD0AA File Offset: 0x000DB2AA
		public SchemaImporterExtensionsSection()
		{
			this.properties.Add(this.schemaImporterExtensions);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000DD0E6 File Offset: 0x000DB2E6
		private static string GetSqlTypeSchemaImporter(string typeName)
		{
			return "System.Data.SqlTypes." + typeName + ", System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000DD0F8 File Offset: 0x000DB2F8
		protected override void InitializeDefault()
		{
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterChar", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeCharSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterNChar", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeNCharSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterVarChar", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeVarCharSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterNVarChar", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeNVarCharSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterText", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeTextSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterNText", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeNTextSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterVarBinary", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeVarBinarySchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterBinary", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeBinarySchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterImage", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeVarImageSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterDecimal", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeDecimalSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterNumeric", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeNumericSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterBigInt", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeBigIntSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterInt", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeIntSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterSmallInt", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeSmallIntSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterTinyInt", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeTinyIntSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterBit", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeBitSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterFloat", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeFloatSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterReal", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeRealSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterDateTime", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeDateTimeSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterSmallDateTime", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeSmallDateTimeSchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterMoney", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeMoneySchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterSmallMoney", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeSmallMoneySchemaImporterExtension")));
			this.SchemaImporterExtensions.Add(new SchemaImporterExtensionElement("SqlTypesSchemaImporterUniqueIdentifier", SchemaImporterExtensionsSection.GetSqlTypeSchemaImporter("TypeUniqueIdentifierSchemaImporterExtension")));
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x000DD3CE File Offset: 0x000DB5CE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets the object that represents the collection of extensions.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection" /> that contains the objects that represent configuration elements.</returns>
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x000DD3D6 File Offset: 0x000DB5D6
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public SchemaImporterExtensionElementCollection SchemaImporterExtensions
		{
			get
			{
				return (SchemaImporterExtensionElementCollection)base[this.schemaImporterExtensions];
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x000DD3EC File Offset: 0x000DB5EC
		internal SchemaImporterExtensionCollection SchemaImporterExtensionsInternal
		{
			get
			{
				SchemaImporterExtensionCollection schemaImporterExtensionCollection = new SchemaImporterExtensionCollection();
				foreach (object obj in this.SchemaImporterExtensions)
				{
					SchemaImporterExtensionElement schemaImporterExtensionElement = (SchemaImporterExtensionElement)obj;
					schemaImporterExtensionCollection.Add(schemaImporterExtensionElement.Name, schemaImporterExtensionElement.Type);
				}
				return schemaImporterExtensionCollection;
			}
		}

		// Token: 0x040018B7 RID: 6327
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040018B8 RID: 6328
		private readonly ConfigurationProperty schemaImporterExtensions = new ConfigurationProperty(null, typeof(SchemaImporterExtensionElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
