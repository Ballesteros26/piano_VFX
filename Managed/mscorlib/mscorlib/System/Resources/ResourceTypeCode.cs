using System;

namespace System.Resources
{
	// Token: 0x020002AD RID: 685
	[Serializable]
	internal enum ResourceTypeCode
	{
		// Token: 0x040010FF RID: 4351
		Null,
		// Token: 0x04001100 RID: 4352
		String,
		// Token: 0x04001101 RID: 4353
		Boolean,
		// Token: 0x04001102 RID: 4354
		Char,
		// Token: 0x04001103 RID: 4355
		Byte,
		// Token: 0x04001104 RID: 4356
		SByte,
		// Token: 0x04001105 RID: 4357
		Int16,
		// Token: 0x04001106 RID: 4358
		UInt16,
		// Token: 0x04001107 RID: 4359
		Int32,
		// Token: 0x04001108 RID: 4360
		UInt32,
		// Token: 0x04001109 RID: 4361
		Int64,
		// Token: 0x0400110A RID: 4362
		UInt64,
		// Token: 0x0400110B RID: 4363
		Single,
		// Token: 0x0400110C RID: 4364
		Double,
		// Token: 0x0400110D RID: 4365
		Decimal,
		// Token: 0x0400110E RID: 4366
		DateTime,
		// Token: 0x0400110F RID: 4367
		TimeSpan,
		// Token: 0x04001110 RID: 4368
		LastPrimitive = 16,
		// Token: 0x04001111 RID: 4369
		ByteArray = 32,
		// Token: 0x04001112 RID: 4370
		Stream,
		// Token: 0x04001113 RID: 4371
		StartOfUserTypes = 64
	}
}
