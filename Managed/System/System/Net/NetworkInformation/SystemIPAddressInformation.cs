using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200061A RID: 1562
	internal class SystemIPAddressInformation : IPAddressInformation
	{
		// Token: 0x060031D5 RID: 12757 RVA: 0x000BE368 File Offset: 0x000BC568
		public SystemIPAddressInformation(IPAddress address, bool isDnsEligible, bool isTransient)
		{
			this.address = address;
			this.dnsEligible = isDnsEligible;
			this.transient = isTransient;
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x000BE38C File Offset: 0x000BC58C
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x000BE394 File Offset: 0x000BC594
		public override bool IsTransient
		{
			get
			{
				return this.transient;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x000BE39C File Offset: 0x000BC59C
		public override bool IsDnsEligible
		{
			get
			{
				return this.dnsEligible;
			}
		}

		// Token: 0x0400281C RID: 10268
		private IPAddress address;

		// Token: 0x0400281D RID: 10269
		internal bool transient;

		// Token: 0x0400281E RID: 10270
		internal bool dnsEligible = true;
	}
}
