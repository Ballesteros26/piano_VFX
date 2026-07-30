using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000203 RID: 515
	public class UxmlValueMatches : UxmlTypeRestriction
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0003938A File Offset: 0x0003758A
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x00039392 File Offset: 0x00037592
		public string regex { get; set; }

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003939C File Offset: 0x0003759C
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueMatches uxmlValueMatches = other as UxmlValueMatches;
			bool flag = uxmlValueMatches == null;
			return !flag && this.regex == uxmlValueMatches.regex;
		}
	}
}
