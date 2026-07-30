using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000FE RID: 254
	public sealed class PlaybackCurrentTimeChangedEventArgs : EventArgs
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x0001A3B8 File Offset: 0x000185B8
		internal PlaybackCurrentTimeChangedEventArgs(IEnumerable<PlaybackCurrentTime> times)
		{
			this.Times = times;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001A3C7 File Offset: 0x000185C7
		public IEnumerable<PlaybackCurrentTime> Times { get; }
	}
}
