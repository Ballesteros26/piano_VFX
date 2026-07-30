using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000461 RID: 1121
	internal class CachedTransportContext : TransportContext
	{
		// Token: 0x06002102 RID: 8450 RVA: 0x0007FFB1 File Offset: 0x0007E1B1
		internal CachedTransportContext(ChannelBinding binding)
		{
			this.binding = binding;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0007FFC0 File Offset: 0x0007E1C0
		public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (kind != ChannelBindingKind.Endpoint)
			{
				return null;
			}
			return this.binding;
		}

		// Token: 0x04001DF9 RID: 7673
		private ChannelBinding binding;
	}
}
