using System;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000048 RID: 72
	internal interface ITweenValue
	{
		// Token: 0x060004B8 RID: 1208
		void TweenValue(float floatPercentage);

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004B9 RID: 1209
		bool ignoreTimeScale { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004BA RID: 1210
		float duration { get; }

		// Token: 0x060004BB RID: 1211
		bool ValidTarget();
	}
}
