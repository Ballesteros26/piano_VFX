using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000276 RID: 630
	internal struct StyleValidationResult
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x000532C4 File Offset: 0x000514C4
		public bool success
		{
			get
			{
				return this.status == StyleValidationStatus.Ok;
			}
		}

		// Token: 0x04000940 RID: 2368
		public StyleValidationStatus status;

		// Token: 0x04000941 RID: 2369
		public string message;

		// Token: 0x04000942 RID: 2370
		public string errorValue;

		// Token: 0x04000943 RID: 2371
		public string hint;
	}
}
