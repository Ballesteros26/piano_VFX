using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000025 RID: 37
	internal class CreateMatchRequest : Request
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000057BF File Offset: 0x000039BF
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000057C7 File Offset: 0x000039C7
		public string name { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000057D0 File Offset: 0x000039D0
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000057D8 File Offset: 0x000039D8
		public uint size { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000057E1 File Offset: 0x000039E1
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000057E9 File Offset: 0x000039E9
		public string publicAddress { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000057F2 File Offset: 0x000039F2
		// (set) Token: 0x060001AF RID: 431 RVA: 0x000057FA File Offset: 0x000039FA
		public string privateAddress { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005803 File Offset: 0x00003A03
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000580B File Offset: 0x00003A0B
		public int eloScore { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005814 File Offset: 0x00003A14
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000581C File Offset: 0x00003A1C
		public bool advertise { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005825 File Offset: 0x00003A25
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000582D File Offset: 0x00003A2D
		public string password { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00005836 File Offset: 0x00003A36
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000583E File Offset: 0x00003A3E
		public Dictionary<string, long> matchAttributes { get; set; }

		// Token: 0x060001B8 RID: 440 RVA: 0x00005848 File Offset: 0x00003A48
		public override string ToString()
		{
			return UnityString.Format("[{0}]-name:{1},size:{2},publicAddress:{3},privateAddress:{4},eloScore:{5},advertise:{6},HasPassword:{7},matchAttributes.Count:{8}", new object[]
			{
				base.ToString(),
				this.name,
				this.size,
				this.publicAddress,
				this.privateAddress,
				this.eloScore,
				this.advertise,
				string.IsNullOrEmpty(this.password) ? "NO" : "YES",
				(this.matchAttributes == null) ? 0 : this.matchAttributes.Count
			});
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000058F4 File Offset: 0x00003AF4
		public override bool IsValid()
		{
			return base.IsValid() && this.size >= 2U && (this.matchAttributes == null || this.matchAttributes.Count <= 10);
		}
	}
}
