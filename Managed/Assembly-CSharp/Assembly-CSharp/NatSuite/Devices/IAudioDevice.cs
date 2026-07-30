using System;

namespace NatSuite.Devices
{
	// Token: 0x02000034 RID: 52
	public interface IAudioDevice : IMediaDevice, IEquatable<IMediaDevice>
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001D2 RID: 466
		int sampleRate { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001D3 RID: 467
		int channelCount { get; }

		// Token: 0x060001D4 RID: 468
		void StartRunning(SampleBufferDelegate @delegate);
	}
}
