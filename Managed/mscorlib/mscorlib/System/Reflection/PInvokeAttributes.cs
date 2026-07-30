using System;

namespace System.Reflection
{
	// Token: 0x020002E9 RID: 745
	[Flags]
	[Serializable]
	internal enum PInvokeAttributes
	{
		// Token: 0x04001205 RID: 4613
		NoMangle = 1,
		// Token: 0x04001206 RID: 4614
		CharSetMask = 6,
		// Token: 0x04001207 RID: 4615
		CharSetNotSpec = 0,
		// Token: 0x04001208 RID: 4616
		CharSetAnsi = 2,
		// Token: 0x04001209 RID: 4617
		CharSetUnicode = 4,
		// Token: 0x0400120A RID: 4618
		CharSetAuto = 6,
		// Token: 0x0400120B RID: 4619
		BestFitUseAssem = 0,
		// Token: 0x0400120C RID: 4620
		BestFitEnabled = 16,
		// Token: 0x0400120D RID: 4621
		BestFitDisabled = 32,
		// Token: 0x0400120E RID: 4622
		BestFitMask = 48,
		// Token: 0x0400120F RID: 4623
		ThrowOnUnmappableCharUseAssem = 0,
		// Token: 0x04001210 RID: 4624
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04001211 RID: 4625
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x04001212 RID: 4626
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04001213 RID: 4627
		SupportsLastError = 64,
		// Token: 0x04001214 RID: 4628
		CallConvMask = 1792,
		// Token: 0x04001215 RID: 4629
		CallConvWinapi = 256,
		// Token: 0x04001216 RID: 4630
		CallConvCdecl = 512,
		// Token: 0x04001217 RID: 4631
		CallConvStdcall = 768,
		// Token: 0x04001218 RID: 4632
		CallConvThiscall = 1024,
		// Token: 0x04001219 RID: 4633
		CallConvFastcall = 1280,
		// Token: 0x0400121A RID: 4634
		MaxValue = 65535
	}
}
