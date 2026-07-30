using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeBinarySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E9 RID: 745
	public sealed class TypeBinarySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeBinarySchemaImporterExtension" /> class.</summary>
		// Token: 0x060021B7 RID: 8631 RVA: 0x0009D9A9 File Offset: 0x0009BBA9
		public TypeBinarySchemaImporterExtension()
			: base("binary", "System.Data.SqlTypes.SqlBinary", false)
		{
		}
	}
}
