using System;
using System.Globalization;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x02000375 RID: 885
	internal static class ConfigurationStrings
	{
		// Token: 0x06002417 RID: 9239 RVA: 0x000DCD41 File Offset: 0x000DAF41
		private static string GetSectionPath(string sectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", "system.xml.serialization", sectionName);
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x000DCD58 File Offset: 0x000DAF58
		internal static string SchemaImporterExtensionsSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("schemaImporterExtensions");
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000DCD64 File Offset: 0x000DAF64
		internal static string DateTimeSerializationSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("dateTimeSerialization");
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000DCD70 File Offset: 0x000DAF70
		internal static string XmlSerializerSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("xmlSerializer");
			}
		}

		// Token: 0x0400188B RID: 6283
		internal const string Name = "name";

		// Token: 0x0400188C RID: 6284
		internal const string SchemaImporterExtensionsSectionName = "schemaImporterExtensions";

		// Token: 0x0400188D RID: 6285
		internal const string DateTimeSerializationSectionName = "dateTimeSerialization";

		// Token: 0x0400188E RID: 6286
		internal const string XmlSerializerSectionName = "xmlSerializer";

		// Token: 0x0400188F RID: 6287
		internal const string SectionGroupName = "system.xml.serialization";

		// Token: 0x04001890 RID: 6288
		internal const string SqlTypesSchemaImporterChar = "SqlTypesSchemaImporterChar";

		// Token: 0x04001891 RID: 6289
		internal const string SqlTypesSchemaImporterNChar = "SqlTypesSchemaImporterNChar";

		// Token: 0x04001892 RID: 6290
		internal const string SqlTypesSchemaImporterVarChar = "SqlTypesSchemaImporterVarChar";

		// Token: 0x04001893 RID: 6291
		internal const string SqlTypesSchemaImporterNVarChar = "SqlTypesSchemaImporterNVarChar";

		// Token: 0x04001894 RID: 6292
		internal const string SqlTypesSchemaImporterText = "SqlTypesSchemaImporterText";

		// Token: 0x04001895 RID: 6293
		internal const string SqlTypesSchemaImporterNText = "SqlTypesSchemaImporterNText";

		// Token: 0x04001896 RID: 6294
		internal const string SqlTypesSchemaImporterVarBinary = "SqlTypesSchemaImporterVarBinary";

		// Token: 0x04001897 RID: 6295
		internal const string SqlTypesSchemaImporterBinary = "SqlTypesSchemaImporterBinary";

		// Token: 0x04001898 RID: 6296
		internal const string SqlTypesSchemaImporterImage = "SqlTypesSchemaImporterImage";

		// Token: 0x04001899 RID: 6297
		internal const string SqlTypesSchemaImporterDecimal = "SqlTypesSchemaImporterDecimal";

		// Token: 0x0400189A RID: 6298
		internal const string SqlTypesSchemaImporterNumeric = "SqlTypesSchemaImporterNumeric";

		// Token: 0x0400189B RID: 6299
		internal const string SqlTypesSchemaImporterBigInt = "SqlTypesSchemaImporterBigInt";

		// Token: 0x0400189C RID: 6300
		internal const string SqlTypesSchemaImporterInt = "SqlTypesSchemaImporterInt";

		// Token: 0x0400189D RID: 6301
		internal const string SqlTypesSchemaImporterSmallInt = "SqlTypesSchemaImporterSmallInt";

		// Token: 0x0400189E RID: 6302
		internal const string SqlTypesSchemaImporterTinyInt = "SqlTypesSchemaImporterTinyInt";

		// Token: 0x0400189F RID: 6303
		internal const string SqlTypesSchemaImporterBit = "SqlTypesSchemaImporterBit";

		// Token: 0x040018A0 RID: 6304
		internal const string SqlTypesSchemaImporterFloat = "SqlTypesSchemaImporterFloat";

		// Token: 0x040018A1 RID: 6305
		internal const string SqlTypesSchemaImporterReal = "SqlTypesSchemaImporterReal";

		// Token: 0x040018A2 RID: 6306
		internal const string SqlTypesSchemaImporterDateTime = "SqlTypesSchemaImporterDateTime";

		// Token: 0x040018A3 RID: 6307
		internal const string SqlTypesSchemaImporterSmallDateTime = "SqlTypesSchemaImporterSmallDateTime";

		// Token: 0x040018A4 RID: 6308
		internal const string SqlTypesSchemaImporterMoney = "SqlTypesSchemaImporterMoney";

		// Token: 0x040018A5 RID: 6309
		internal const string SqlTypesSchemaImporterSmallMoney = "SqlTypesSchemaImporterSmallMoney";

		// Token: 0x040018A6 RID: 6310
		internal const string SqlTypesSchemaImporterUniqueIdentifier = "SqlTypesSchemaImporterUniqueIdentifier";

		// Token: 0x040018A7 RID: 6311
		internal const string Type = "type";

		// Token: 0x040018A8 RID: 6312
		internal const string Mode = "mode";

		// Token: 0x040018A9 RID: 6313
		internal const string CheckDeserializeAdvances = "checkDeserializeAdvances";

		// Token: 0x040018AA RID: 6314
		internal const string TempFilesLocation = "tempFilesLocation";

		// Token: 0x040018AB RID: 6315
		internal const string UseLegacySerializerGeneration = "useLegacySerializerGeneration";
	}
}
