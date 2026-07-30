using System;

namespace System.Data
{
	// Token: 0x020000ED RID: 237
	internal struct IndexField
	{
		// Token: 0x06000C74 RID: 3188 RVA: 0x0003A4BE File Offset: 0x000386BE
		internal IndexField(DataColumn column, bool isDescending)
		{
			this.Column = column;
			this.IsDescending = isDescending;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0003A4CE File Offset: 0x000386CE
		public static bool operator ==(IndexField if1, IndexField if2)
		{
			return if1.Column == if2.Column && if1.IsDescending == if2.IsDescending;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0003A4EE File Offset: 0x000386EE
		public static bool operator !=(IndexField if1, IndexField if2)
		{
			return !(if1 == if2);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0003A4FA File Offset: 0x000386FA
		public override bool Equals(object obj)
		{
			return obj is IndexField && this == (IndexField)obj;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0003A518 File Offset: 0x00038718
		public override int GetHashCode()
		{
			return this.Column.GetHashCode() ^ this.IsDescending.GetHashCode();
		}

		// Token: 0x04000854 RID: 2132
		public readonly DataColumn Column;

		// Token: 0x04000855 RID: 2133
		public readonly bool IsDescending;
	}
}
