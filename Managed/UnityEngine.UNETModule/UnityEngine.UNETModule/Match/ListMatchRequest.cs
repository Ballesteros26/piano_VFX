using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200002C RID: 44
	internal class ListMatchRequest : Request
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005CEA File Offset: 0x00003EEA
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00005CF2 File Offset: 0x00003EF2
		public int pageSize { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005CFB File Offset: 0x00003EFB
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00005D03 File Offset: 0x00003F03
		public int pageNum { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005D0C File Offset: 0x00003F0C
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00005D14 File Offset: 0x00003F14
		public string nameFilter { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00005D1D File Offset: 0x00003F1D
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00005D25 File Offset: 0x00003F25
		public bool filterOutPrivateMatches { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00005D2E File Offset: 0x00003F2E
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00005D36 File Offset: 0x00003F36
		public int eloScore { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00005D3F File Offset: 0x00003F3F
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00005D47 File Offset: 0x00003F47
		public Dictionary<string, long> matchAttributeFilterLessThan { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00005D50 File Offset: 0x00003F50
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00005D58 File Offset: 0x00003F58
		public Dictionary<string, long> matchAttributeFilterEqualTo { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00005D61 File Offset: 0x00003F61
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00005D69 File Offset: 0x00003F69
		public Dictionary<string, long> matchAttributeFilterGreaterThan { get; set; }

		// Token: 0x060001EA RID: 490 RVA: 0x00005D74 File Offset: 0x00003F74
		public override string ToString()
		{
			return UnityString.Format("[{0}]-pageSize:{1},pageNum:{2},nameFilter:{3}, filterOutPrivateMatches:{4}, eloScore:{5}, matchAttributeFilterLessThan.Count:{6}, matchAttributeFilterEqualTo.Count:{7}, matchAttributeFilterGreaterThan.Count:{8}", new object[]
			{
				base.ToString(),
				this.pageSize,
				this.pageNum,
				this.nameFilter,
				this.filterOutPrivateMatches,
				this.eloScore,
				(this.matchAttributeFilterLessThan == null) ? 0 : this.matchAttributeFilterLessThan.Count,
				(this.matchAttributeFilterEqualTo == null) ? 0 : this.matchAttributeFilterEqualTo.Count,
				(this.matchAttributeFilterGreaterThan == null) ? 0 : this.matchAttributeFilterGreaterThan.Count
			});
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005E3C File Offset: 0x0000403C
		public override bool IsValid()
		{
			int num = ((this.matchAttributeFilterLessThan == null) ? 0 : this.matchAttributeFilterLessThan.Count);
			num += ((this.matchAttributeFilterEqualTo == null) ? 0 : this.matchAttributeFilterEqualTo.Count);
			num += ((this.matchAttributeFilterGreaterThan == null) ? 0 : this.matchAttributeFilterGreaterThan.Count);
			return base.IsValid() && this.pageSize >= 1 && this.pageSize <= 1000 && num <= 10;
		}

		// Token: 0x040000C8 RID: 200
		[Obsolete("This bool is deprecated in favor of filterOutPrivateMatches")]
		public bool includePasswordMatches;
	}
}
