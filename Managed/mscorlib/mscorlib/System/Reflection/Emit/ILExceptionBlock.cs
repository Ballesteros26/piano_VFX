using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200035E RID: 862
	internal struct ILExceptionBlock
	{
		// Token: 0x060026E7 RID: 9959 RVA: 0x00002194 File Offset: 0x00000394
		internal void Debug()
		{
		}

		// Token: 0x0400142A RID: 5162
		public const int CATCH = 0;

		// Token: 0x0400142B RID: 5163
		public const int FILTER = 1;

		// Token: 0x0400142C RID: 5164
		public const int FINALLY = 2;

		// Token: 0x0400142D RID: 5165
		public const int FAULT = 4;

		// Token: 0x0400142E RID: 5166
		public const int FILTER_START = -1;

		// Token: 0x0400142F RID: 5167
		internal Type extype;

		// Token: 0x04001430 RID: 5168
		internal int type;

		// Token: 0x04001431 RID: 5169
		internal int start;

		// Token: 0x04001432 RID: 5170
		internal int len;

		// Token: 0x04001433 RID: 5171
		internal int filter_offset;
	}
}
