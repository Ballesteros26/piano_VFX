using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeRealSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F3 RID: 755
	public sealed class TypeRealSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeRealSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021C1 RID: 8641 RVA: 0x0009DA61 File Offset: 0x0009BC61
		public TypeRealSchemaImporterExtension()
			: base("real", "System.Data.SqlTypes.SqlSingle")
		{
		}
	}
}
