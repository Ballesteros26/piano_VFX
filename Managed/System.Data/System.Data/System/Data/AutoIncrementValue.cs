using System;

namespace System.Data
{
	// Token: 0x02000060 RID: 96
	internal abstract class AutoIncrementValue
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600038D RID: 909 RVA: 0x000129EF File Offset: 0x00010BEF
		// (set) Token: 0x0600038E RID: 910 RVA: 0x000129F7 File Offset: 0x00010BF7
		internal bool Auto { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600038F RID: 911
		// (set) Token: 0x06000390 RID: 912
		internal abstract object Current { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000391 RID: 913
		// (set) Token: 0x06000392 RID: 914
		internal abstract long Seed { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000393 RID: 915
		// (set) Token: 0x06000394 RID: 916
		internal abstract long Step { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000395 RID: 917
		internal abstract Type DataType { get; }

		// Token: 0x06000396 RID: 918
		internal abstract void SetCurrent(object value, IFormatProvider formatProvider);

		// Token: 0x06000397 RID: 919
		internal abstract void SetCurrentAndIncrement(object value);

		// Token: 0x06000398 RID: 920
		internal abstract void MoveAfter();

		// Token: 0x06000399 RID: 921 RVA: 0x00012A00 File Offset: 0x00010C00
		internal AutoIncrementValue Clone()
		{
			AutoIncrementInt64 autoIncrementInt = ((this is AutoIncrementInt64) ? new AutoIncrementInt64() : new AutoIncrementBigInteger());
			autoIncrementInt.Auto = this.Auto;
			autoIncrementInt.Seed = this.Seed;
			autoIncrementInt.Step = this.Step;
			autoIncrementInt.Current = this.Current;
			return autoIncrementInt;
		}
	}
}
