using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeMoneySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x020002F6 RID: 758
	public sealed class TypeMoneySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeMoneySchemaImporterExtension" /> class.</summary>
		// Token: 0x060021C4 RID: 8644 RVA: 0x0009DA97 File Offset: 0x0009BC97
		public TypeMoneySchemaImporterExtension()
			: base("money", "System.Data.SqlTypes.SqlMoney")
		{
		}
	}
}
