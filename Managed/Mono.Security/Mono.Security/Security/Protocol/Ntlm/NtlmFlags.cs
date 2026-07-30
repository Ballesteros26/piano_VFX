using System;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000070 RID: 112
	[Flags]
	public enum NtlmFlags
	{
		// Token: 0x04000202 RID: 514
		NegotiateUnicode = 1,
		// Token: 0x04000203 RID: 515
		NegotiateOem = 2,
		// Token: 0x04000204 RID: 516
		RequestTarget = 4,
		// Token: 0x04000205 RID: 517
		NegotiateNtlm = 512,
		// Token: 0x04000206 RID: 518
		NegotiateDomainSupplied = 4096,
		// Token: 0x04000207 RID: 519
		NegotiateWorkstationSupplied = 8192,
		// Token: 0x04000208 RID: 520
		NegotiateAlwaysSign = 32768,
		// Token: 0x04000209 RID: 521
		NegotiateNtlm2Key = 524288,
		// Token: 0x0400020A RID: 522
		Negotiate128 = 536870912,
		// Token: 0x0400020B RID: 523
		Negotiate56 = -2147483648
	}
}
