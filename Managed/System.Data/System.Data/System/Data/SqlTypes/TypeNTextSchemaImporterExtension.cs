using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeNTextSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E7 RID: 743
	public sealed class TypeNTextSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeNTextSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021B5 RID: 8629 RVA: 0x0009D983 File Offset: 0x0009BB83
		public TypeNTextSchemaImporterExtension()
			: base("ntext", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
