using System;
using NatSuite.Devices.Internal;

namespace NatSuite.Devices
{
	// Token: 0x0200002F RID: 47
	[Doc("AudioDevice")]
	public abstract class AudioDevice : IAudioDevice, IMediaDevice, IEquatable<IMediaDevice>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600019A RID: 410
		[Doc("UniqueID")]
		public abstract string uniqueID { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600019B RID: 411
		[Doc("Name")]
		public abstract string name { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600019C RID: 412
		[Doc("EchoCancellation")]
		public abstract bool echoCancellation { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600019D RID: 413
		// (set) Token: 0x0600019E RID: 414
		[Doc("SampleRate")]
		public abstract int sampleRate { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600019F RID: 415
		// (set) Token: 0x060001A0 RID: 416
		[Doc("ChannelCount")]
		public abstract int channelCount { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060001A1 RID: 417
		[Doc("Running")]
		public abstract bool running { get; }

		// Token: 0x060001A2 RID: 418
		[Doc("StartRecording")]
		public abstract void StartRunning(SampleBufferDelegate @delegate);

		// Token: 0x060001A3 RID: 419
		[Doc("StopRunning")]
		public abstract void StopRunning();

		// Token: 0x060001A4 RID: 420 RVA: 0x0001300F File Offset: 0x0001120F
		public bool Equals(IMediaDevice other)
		{
			return other != null && other is AudioDevice && other.uniqueID == this.uniqueID;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0001302F File Offset: 0x0001122F
		public override string ToString()
		{
			return "microphone:" + this.uniqueID;
		}
	}
}
