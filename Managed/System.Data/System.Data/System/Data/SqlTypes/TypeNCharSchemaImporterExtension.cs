using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeNCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E3 RID: 739
	public sealed class TypeNCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeNCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021B1 RID: 8625 RVA: 0x0009D937 File Offset: 0x0009BB37
		public TypeNCharSchemaImporterExtension()
			: base("nchar", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
