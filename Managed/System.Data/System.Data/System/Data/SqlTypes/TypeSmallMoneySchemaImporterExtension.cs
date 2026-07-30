using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeSmallMoneySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F7 RID: 759
	public sealed class TypeSmallMoneySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeSmallMoneySchemaImporterExtension" /> class.</summary>
		// Token: 0x060021C5 RID: 8645 RVA: 0x0009DAA9 File Offset: 0x0009BCA9
		public TypeSmallMoneySchemaImporterExtension()
			: base("smallmoney", "System.Data.SqlTypes.SqlMoney")
		{
		}
	}
}
