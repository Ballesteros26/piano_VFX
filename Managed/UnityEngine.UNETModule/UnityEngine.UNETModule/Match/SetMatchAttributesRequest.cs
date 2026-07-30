using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000031 RID: 49
	internal class SetMatchAttributesRequest : Request
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00006056 File Offset: 0x00004256
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000605E File Offset: 0x0000425E
		public NetworkID networkId { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006067 File Offset: 0x00004267
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000606F File Offset: 0x0000426F
		public bool isListed { get; set; }

		// Token: 0x060001F9 RID: 505 RVA: 0x00006078 File Offset: 0x00004278
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:{1},isListed:{2}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X"),
				this.isListed
			});
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000060CC File Offset: 0x000042CC
		public override bool IsValid()
		{
			return base.IsValid() && this.networkId != NetworkID.Invalid;
		}
	}
}
