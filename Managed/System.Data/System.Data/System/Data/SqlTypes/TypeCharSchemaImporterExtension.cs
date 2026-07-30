using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E2 RID: 738
	public sealed class TypeCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021B0 RID: 8624 RVA: 0x0009D924 File Offset: 0x0009BB24
		public TypeCharSchemaImporterExtension()
			: base("char", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
