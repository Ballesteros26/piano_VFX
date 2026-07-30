using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeFloatSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F2 RID: 754
	public sealed class TypeFloatSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeFloatSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021C0 RID: 8640 RVA: 0x0009DA4F File Offset: 0x0009BC4F
		public TypeFloatSchemaImporterExtension()
			: base("float", "System.Data.SqlTypes.SqlDouble")
		{
		}
	}
}
