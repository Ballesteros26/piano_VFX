using System;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Specifies the types of objects that can be retrieved from a data-store schema. This class cannot be inherited.</summary>
	// Token: 0x0200016B RID: 363
	public sealed class DesignerDataSchemaClass
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x00002352 File Offset: 0x00000552
		private DesignerDataSchemaClass()
		{
		}

		/// <summary>Indicates that stored procedures should be returned from the data-store schema.</summary>
		// Token: 0x0400028D RID: 653
		public static readonly DesignerDataSchemaClass StoredProcedures = new DesignerDataSchemaClass();

		/// <summary>Indicates that tables should be returned from the data-store schema.</summary>
		// Token: 0x0400028E RID: 654
		public static readonly DesignerDataSchemaClass Tables = new DesignerDataSchemaClass();

		/// <summary>Indicates that data views should be returned from the data-store schema.</summary>
		// Token: 0x0400028F RID: 655
		public static readonly DesignerDataSchemaClass Views = new DesignerDataSchemaClass();
	}
}
