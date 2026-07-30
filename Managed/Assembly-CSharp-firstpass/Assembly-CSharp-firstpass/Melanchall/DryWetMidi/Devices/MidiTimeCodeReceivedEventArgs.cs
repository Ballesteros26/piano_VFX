using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000F0 RID: 240
	public sealed class MidiTimeCodeReceivedEventArgs : EventArgs
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x0001972D File Offset: 0x0001792D
		internal MidiTimeCodeReceivedEventArgs(MidiTimeCodeType timeCodeType, int hours, int minutes, int seconds, int frames)
		{
			this.Format = timeCodeType;
			this.Hours = hours;
			this.Minutes = minutes;
			this.Seconds = seconds;
			this.Frames = frames;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001975A File Offset: 0x0001795A
		public MidiTimeCodeType Format { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00019762 File Offset: 0x00017962
		public int Hours { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001976A File Offset: 0x0001796A
		public int Minutes { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00019772 File Offset: 0x00017972
		public int Seconds { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001977A File Offset: 0x0001797A
		public int Frames { get; }
	}
}
