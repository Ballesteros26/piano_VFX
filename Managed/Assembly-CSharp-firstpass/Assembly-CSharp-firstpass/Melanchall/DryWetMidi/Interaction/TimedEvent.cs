using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000CC RID: 204
	public sealed class TimedEvent : ITimedObject
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x000172B0 File Offset: 0x000154B0
		public TimedEvent(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			this.Event = midiEvent;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000172CA File Offset: 0x000154CA
		public TimedEvent(MidiEvent midiEvent, long time)
			: this(midiEvent)
		{
			this.Time = time;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x000172DA File Offset: 0x000154DA
		public MidiEvent Event { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000172E2 File Offset: 0x000154E2
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x000172EA File Offset: 0x000154EA
		public long Time
		{
			get
			{
				return this._time;
			}
			set
			{
				ThrowIfTimeArgument.IsNegative("value", value);
				this._time = value;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000172FE File Offset: 0x000154FE
		public TimedEvent Clone()
		{
			return new TimedEvent(this.Event.Clone(), this.Time);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00017316 File Offset: 0x00015516
		public override string ToString()
		{
			return string.Format("Event at {0}: {1}", this.Time, this.Event);
		}

		// Token: 0x04000722 RID: 1826
		private long _time;
	}
}
