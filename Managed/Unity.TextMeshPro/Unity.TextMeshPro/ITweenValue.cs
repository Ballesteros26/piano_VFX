using System;

namespace TMPro
{
	// Token: 0x02000012 RID: 18
	internal interface ITweenValue
	{
		// Token: 0x06000053 RID: 83
		void TweenValue(float floatPercentage);

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000054 RID: 84
		bool ignoreTimeScale { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000055 RID: 85
		float duration { get; }

		// Token: 0x06000056 RID: 86
		bool ValidTarget();
	}
}
