using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000E4 RID: 228
	public interface ITickGenerator : IDisposable
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600059D RID: 1437
		// (remove) Token: 0x0600059E RID: 1438
		event EventHandler TickGenerated;

		// Token: 0x0600059F RID: 1439
		void TryStart();
	}
}
