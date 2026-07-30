using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x02000410 RID: 1040
	[Serializable]
	internal class EraInfo
	{
		// Token: 0x0600317A RID: 12666 RVA: 0x000B2130 File Offset: 0x000B0330
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000B217C File Offset: 0x000B037C
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear, string eraName, string abbrevEraName, string englishEraName)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
			this.eraName = eraName;
			this.abbrevEraName = abbrevEraName;
			this.englishEraName = englishEraName;
		}

		// Token: 0x04001A0D RID: 6669
		internal int era;

		// Token: 0x04001A0E RID: 6670
		internal long ticks;

		// Token: 0x04001A0F RID: 6671
		internal int yearOffset;

		// Token: 0x04001A10 RID: 6672
		internal int minEraYear;

		// Token: 0x04001A11 RID: 6673
		internal int maxEraYear;

		// Token: 0x04001A12 RID: 6674
		[OptionalField(VersionAdded = 4)]
		internal string eraName;

		// Token: 0x04001A13 RID: 6675
		[OptionalField(VersionAdded = 4)]
		internal string abbrevEraName;

		// Token: 0x04001A14 RID: 6676
		[OptionalField(VersionAdded = 4)]
		internal string englishEraName;
	}
}
