using System;

namespace NatSuite.Devices
{
	// Token: 0x02000033 RID: 51
	public interface IMediaDevice : IEquatable<IMediaDevice>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001CF RID: 463
		string uniqueID { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001D0 RID: 464
		bool running { get; }

		// Token: 0x060001D1 RID: 465
		void StopRunning();
	}
}
