using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeBitSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F1 RID: 753
	public sealed class TypeBitSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeBitSchemaImporterExtension" /> class. </summary>
		// Token: 0x060021BF RID: 8639 RVA: 0x0009DA3D File Offset: 0x0009BC3D
		public TypeBitSchemaImporterExtension()
			: base("bit", "System.Data.SqlTypes.SqlBoolean")
		{
		}
	}
}
