using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeBigIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002ED RID: 749
	public sealed class TypeBigIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeBigIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x060021BB RID: 8635 RVA: 0x0009D9F5 File Offset: 0x0009BBF5
		public TypeBigIntSchemaImporterExtension()
			: base("bigint", "System.Data.SqlTypes.SqlInt64")
		{
		}
	}
}
