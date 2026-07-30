using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeVarCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E4 RID: 740
	public sealed class TypeVarCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeVarCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021B2 RID: 8626 RVA: 0x0009D94A File Offset: 0x0009BB4A
		public TypeVarCharSchemaImporterExtension()
			: base("varchar", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
