using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200002A RID: 42
	internal class DropConnectionRequest : Request
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00005BFE File Offset: 0x00003DFE
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00005C06 File Offset: 0x00003E06
		public NetworkID networkId { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00005C0F File Offset: 0x00003E0F
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00005C17 File Offset: 0x00003E17
		public NodeID nodeId { get; set; }

		// Token: 0x060001D5 RID: 469 RVA: 0x00005C20 File Offset: 0x00003E20
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1},nodeId:0x{2}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X"),
				this.nodeId.ToString("X")
			});
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005C7C File Offset: 0x00003E7C
		public override bool IsValid()
		{
			return base.IsValid() && this.networkId != NetworkID.Invalid && this.nodeId > NodeID.Invalid;
		}
	}
}
