using System;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	public interface ISubsystem
	{
		// Token: 0x06000032 RID: 50
		void Start();

		// Token: 0x06000033 RID: 51
		void Stop();

		// Token: 0x06000034 RID: 52
		void Destroy();

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53
		bool running { get; }
	}
}
