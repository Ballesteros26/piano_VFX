using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000270 RID: 624
	internal struct MatchResult
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0005225C File Offset: 0x0005045C
		public bool success
		{
			get
			{
				return this.errorCode == MatchResultErrorCode.None;
			}
		}

		// Token: 0x04000925 RID: 2341
		public MatchResultErrorCode errorCode;

		// Token: 0x04000926 RID: 2342
		public string errorValue;
	}
}
