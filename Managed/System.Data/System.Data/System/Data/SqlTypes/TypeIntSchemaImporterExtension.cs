using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002EE RID: 750
	public sealed class TypeIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021BC RID: 8636 RVA: 0x0009DA07 File Offset: 0x0009BC07
		public TypeIntSchemaImporterExtension()
			: base("int", "System.Data.SqlTypes.SqlInt32")
		{
		}
	}
}
