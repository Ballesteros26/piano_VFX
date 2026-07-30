using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000204 RID: 516
	public class UxmlValueBounds : UxmlTypeRestriction
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000393DC File Offset: 0x000375DC
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x000393E4 File Offset: 0x000375E4
		public string min { get; set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000393ED File Offset: 0x000375ED
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x000393F5 File Offset: 0x000375F5
		public string max { get; set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x000393FE File Offset: 0x000375FE
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x00039406 File Offset: 0x00037606
		public bool excludeMin { get; set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0003940F File Offset: 0x0003760F
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x00039417 File Offset: 0x00037617
		public bool excludeMax { get; set; }

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00039420 File Offset: 0x00037620
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueBounds uxmlValueBounds = other as UxmlValueBounds;
			bool flag = uxmlValueBounds == null;
			return !flag && (this.min == uxmlValueBounds.min && this.max == uxmlValueBounds.max && this.excludeMin == uxmlValueBounds.excludeMin) && this.excludeMax == uxmlValueBounds.excludeMax;
		}
	}
}
