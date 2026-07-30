using System;

namespace System.Reflection
{
	// Token: 0x020002E7 RID: 743
	[Serializable]
	internal enum CorElementType : byte
	{
		// Token: 0x040011D0 RID: 4560
		End,
		// Token: 0x040011D1 RID: 4561
		Void,
		// Token: 0x040011D2 RID: 4562
		Boolean,
		// Token: 0x040011D3 RID: 4563
		Char,
		// Token: 0x040011D4 RID: 4564
		I1,
		// Token: 0x040011D5 RID: 4565
		U1,
		// Token: 0x040011D6 RID: 4566
		I2,
		// Token: 0x040011D7 RID: 4567
		U2,
		// Token: 0x040011D8 RID: 4568
		I4,
		// Token: 0x040011D9 RID: 4569
		U4,
		// Token: 0x040011DA RID: 4570
		I8,
		// Token: 0x040011DB RID: 4571
		U8,
		// Token: 0x040011DC RID: 4572
		R4,
		// Token: 0x040011DD RID: 4573
		R8,
		// Token: 0x040011DE RID: 4574
		String,
		// Token: 0x040011DF RID: 4575
		Ptr,
		// Token: 0x040011E0 RID: 4576
		ByRef,
		// Token: 0x040011E1 RID: 4577
		ValueType,
		// Token: 0x040011E2 RID: 4578
		Class,
		// Token: 0x040011E3 RID: 4579
		Var,
		// Token: 0x040011E4 RID: 4580
		Array,
		// Token: 0x040011E5 RID: 4581
		GenericInst,
		// Token: 0x040011E6 RID: 4582
		TypedByRef,
		// Token: 0x040011E7 RID: 4583
		I = 24,
		// Token: 0x040011E8 RID: 4584
		U,
		// Token: 0x040011E9 RID: 4585
		FnPtr = 27,
		// Token: 0x040011EA RID: 4586
		Object,
		// Token: 0x040011EB RID: 4587
		SzArray,
		// Token: 0x040011EC RID: 4588
		MVar,
		// Token: 0x040011ED RID: 4589
		CModReqd,
		// Token: 0x040011EE RID: 4590
		CModOpt,
		// Token: 0x040011EF RID: 4591
		Internal,
		// Token: 0x040011F0 RID: 4592
		Max,
		// Token: 0x040011F1 RID: 4593
		Modifier = 64,
		// Token: 0x040011F2 RID: 4594
		Sentinel,
		// Token: 0x040011F3 RID: 4595
		Pinned = 69
	}
}
