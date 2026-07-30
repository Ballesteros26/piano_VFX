using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeVarBinarySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E8 RID: 744
	public sealed class TypeVarBinarySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeVarBinarySchemaImporterExtension" /> class.</summary>
		// Token: 0x060021B6 RID: 8630 RVA: 0x0009D996 File Offset: 0x0009BB96
		public TypeVarBinarySchemaImporterExtension()
			: base("varbinary", "System.Data.SqlTypes.SqlBinary", false)
		{
		}
	}
}
