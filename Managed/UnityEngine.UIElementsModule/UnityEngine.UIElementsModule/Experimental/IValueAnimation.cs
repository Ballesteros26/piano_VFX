using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000287 RID: 647
	public interface IValueAnimation
	{
		// Token: 0x0600132F RID: 4911
		void Start();

		// Token: 0x06001330 RID: 4912
		void Stop();

		// Token: 0x06001331 RID: 4913
		void Recycle();

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001332 RID: 4914
		bool isRunning { get; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001333 RID: 4915
		// (set) Token: 0x06001334 RID: 4916
		int durationMs { get; set; }
	}
}
