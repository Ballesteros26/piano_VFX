using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200067E RID: 1662
	internal struct Win32LengthFlagsUnion
	{
		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000C37C4 File Offset: 0x000C19C4
		public bool IsDnsEligible
		{
			get
			{
				return (this.Flags & 1U) > 0U;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000C37D1 File Offset: 0x000C19D1
		public bool IsTransient
		{
			get
			{
				return (this.Flags & 2U) > 0U;
			}
		}

		// Token: 0x040029FD RID: 10749
		private const int IP_ADAPTER_ADDRESS_DNS_ELIGIBLE = 1;

		// Token: 0x040029FE RID: 10750
		private const int IP_ADAPTER_ADDRESS_TRANSIENT = 2;

		// Token: 0x040029FF RID: 10751
		public uint Length;

		// Token: 0x04002A00 RID: 10752
		public uint Flags;
	}
}
