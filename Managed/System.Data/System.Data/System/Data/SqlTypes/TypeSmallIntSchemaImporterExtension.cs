using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeSmallIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002EF RID: 751
	public sealed class TypeSmallIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeSmallIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021BD RID: 8637 RVA: 0x0009DA19 File Offset: 0x0009BC19
		public TypeSmallIntSchemaImporterExtension()
			: base("smallint", "System.Data.SqlTypes.SqlInt16")
		{
		}
	}
}
