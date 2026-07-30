using System;

namespace System.Data.SqlTypes
{
	/// <summary>The TypeSmallDateTimeSchemaImporterExtension class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F5 RID: 757
	public sealed class TypeSmallDateTimeSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the TypeSmallDateTimeSchemaImporterExtension class.</summary>
		// Token: 0x060021C3 RID: 8643 RVA: 0x0009DA85 File Offset: 0x0009BC85
		public TypeSmallDateTimeSchemaImporterExtension()
			: base("smalldatetime", "System.Data.SqlTypes.SqlDateTime")
		{
		}
	}
}
