using System;

namespace System.Reflection
{
	// Token: 0x020002E8 RID: 744
	[Flags]
	[Serializable]
	internal enum MdSigCallingConvention : byte
	{
		// Token: 0x040011F5 RID: 4597
		CallConvMask = 15,
		// Token: 0x040011F6 RID: 4598
		Default = 0,
		// Token: 0x040011F7 RID: 4599
		C = 1,
		// Token: 0x040011F8 RID: 4600
		StdCall = 2,
		// Token: 0x040011F9 RID: 4601
		ThisCall = 3,
		// Token: 0x040011FA RID: 4602
		FastCall = 4,
		// Token: 0x040011FB RID: 4603
		Vararg = 5,
		// Token: 0x040011FC RID: 4604
		Field = 6,
		// Token: 0x040011FD RID: 4605
		LocalSig = 7,
		// Token: 0x040011FE RID: 4606
		Property = 8,
		// Token: 0x040011FF RID: 4607
		Unmgd = 9,
		// Token: 0x04001200 RID: 4608
		GenericInst = 10,
		// Token: 0x04001201 RID: 4609
		Generic = 16,
		// Token: 0x04001202 RID: 4610
		HasThis = 32,
		// Token: 0x04001203 RID: 4611
		ExplicitThis = 64
	}
}
