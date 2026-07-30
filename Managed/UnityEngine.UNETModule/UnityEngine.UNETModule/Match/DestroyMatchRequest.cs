using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000029 RID: 41
	internal class DestroyMatchRequest : Request
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00005B7E File Offset: 0x00003D7E
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00005B86 File Offset: 0x00003D86
		public NetworkID networkId { get; set; }

		// Token: 0x060001CE RID: 462 RVA: 0x00005B90 File Offset: 0x00003D90
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X")
			});
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public override bool IsValid()
		{
			return base.IsValid() && this.networkId != NetworkID.Invalid;
		}
	}
}
