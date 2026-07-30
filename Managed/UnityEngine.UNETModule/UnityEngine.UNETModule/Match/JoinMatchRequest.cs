using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000027 RID: 39
	internal class JoinMatchRequest : Request
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000059DE File Offset: 0x00003BDE
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000059E6 File Offset: 0x00003BE6
		public NetworkID networkId { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000059EF File Offset: 0x00003BEF
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000059F7 File Offset: 0x00003BF7
		public string publicAddress { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005A00 File Offset: 0x00003C00
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00005A08 File Offset: 0x00003C08
		public string privateAddress { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005A11 File Offset: 0x00003C11
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00005A19 File Offset: 0x00003C19
		public int eloScore { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00005A22 File Offset: 0x00003C22
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00005A2A File Offset: 0x00003C2A
		public string password { get; set; }

		// Token: 0x060001C7 RID: 455 RVA: 0x00005A34 File Offset: 0x00003C34
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1},publicAddress:{2},privateAddress:{3},eloScore:{4},HasPassword:{5}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X"),
				this.publicAddress,
				this.privateAddress,
				this.eloScore,
				string.IsNullOrEmpty(this.password) ? "NO" : "YES"
			});
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005AB4 File Offset: 0x00003CB4
		public override bool IsValid()
		{
			return base.IsValid() && this.networkId != NetworkID.Invalid;
		}
	}
}
