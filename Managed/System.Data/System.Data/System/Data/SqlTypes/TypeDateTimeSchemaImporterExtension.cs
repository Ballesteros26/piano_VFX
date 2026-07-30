using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeDateTimeSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F4 RID: 756
	public sealed class TypeDateTimeSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeDateTimeSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021C2 RID: 8642 RVA: 0x0009DA73 File Offset: 0x0009BC73
		public TypeDateTimeSchemaImporterExtension()
			: base("datetime", "System.Data.SqlTypes.SqlDateTime")
		{
		}
	}
}
