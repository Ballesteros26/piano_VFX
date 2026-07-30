using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeDecimalSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002EB RID: 747
	public sealed class TypeDecimalSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeDecimalSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021B9 RID: 8633 RVA: 0x0009D9CF File Offset: 0x0009BBCF
		public TypeDecimalSchemaImporterExtension()
			: base("decimal", "System.Data.SqlTypes.SqlDecimal", false)
		{
		}
	}
}
