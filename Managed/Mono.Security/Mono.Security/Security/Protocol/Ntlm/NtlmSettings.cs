using System;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000071 RID: 113
	public static class NtlmSettings
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001623E File Offset: 0x0001443E
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00016245 File Offset: 0x00014445
		public static NtlmAuthLevel DefaultAuthLevel
		{
			get
			{
				return NtlmSettings.defaultAuthLevel;
			}
			set
			{
				NtlmSettings.defaultAuthLevel = value;
			}
		}

		// Token: 0x0400020C RID: 524
		private static NtlmAuthLevel defaultAuthLevel = NtlmAuthLevel.LM_and_NTLM_and_try_NTLMv2_Session;
	}
}
