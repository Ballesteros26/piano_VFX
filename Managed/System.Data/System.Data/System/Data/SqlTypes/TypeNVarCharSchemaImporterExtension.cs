using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeNVarCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E5 RID: 741
	public sealed class TypeNVarCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeNVarCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021B3 RID: 8627 RVA: 0x0009D95D File Offset: 0x0009BB5D
		public TypeNVarCharSchemaImporterExtension()
			: base("nvarchar", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
