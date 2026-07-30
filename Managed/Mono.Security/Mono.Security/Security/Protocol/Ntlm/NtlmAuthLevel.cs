using System;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x0200006F RID: 111
	public enum NtlmAuthLevel
	{
		// Token: 0x040001FD RID: 509
		LM_and_NTLM,
		// Token: 0x040001FE RID: 510
		LM_and_NTLM_and_try_NTLMv2_Session,
		// Token: 0x040001FF RID: 511
		NTLM_only,
		// Token: 0x04000200 RID: 512
		NTLMv2_only
	}
}
