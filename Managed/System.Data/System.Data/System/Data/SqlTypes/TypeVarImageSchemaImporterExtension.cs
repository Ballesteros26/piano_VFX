using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeVarImageSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002EA RID: 746
	public sealed class TypeVarImageSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeVarImageSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021B8 RID: 8632 RVA: 0x0009D9BC File Offset: 0x0009BBBC
		public TypeVarImageSchemaImporterExtension()
			: base("image", "System.Data.SqlTypes.SqlBinary", false)
		{
		}
	}
}
