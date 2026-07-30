using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000033 RID: 51
	public interface ITimeControl
	{
		// Token: 0x0600026F RID: 623
		void SetTime(double time);

		// Token: 0x06000270 RID: 624
		void OnControlTimeStart();

		// Token: 0x06000271 RID: 625
		void OnControlTimeStop();
	}
}
