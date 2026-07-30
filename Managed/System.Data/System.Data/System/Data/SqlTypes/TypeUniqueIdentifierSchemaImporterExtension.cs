using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeUniqueIdentifierSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F8 RID: 760
	public sealed class TypeUniqueIdentifierSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeUniqueIdentifierSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021C6 RID: 8646 RVA: 0x0009DABB File Offset: 0x0009BCBB
		public TypeUniqueIdentifierSchemaImporterExtension()
			: base("uniqueidentifier", "System.Data.SqlTypes.SqlGuid")
		{
		}
	}
}
