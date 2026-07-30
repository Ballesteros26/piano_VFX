using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeTinyIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F0 RID: 752
	public sealed class TypeTinyIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeTinyIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021BE RID: 8638 RVA: 0x0009DA2B File Offset: 0x0009BC2B
		public TypeTinyIntSchemaImporterExtension()
			: base("tinyint", "System.Data.SqlTypes.SqlByte")
		{
		}
	}
}
