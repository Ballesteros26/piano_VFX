using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200014D RID: 333
	public sealed class SetTempoEvent : MetaEvent
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x0001F1D3 File Offset: 0x0001D3D3
		public SetTempoEvent()
			: base(MidiEventType.SetTempo)
		{
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001F1E9 File Offset: 0x0001D3E9
		public SetTempoEvent(long microsecondsPerQuarterNote)
			: this()
		{
			this.MicrosecondsPerQuarterNote = microsecondsPerQuarterNote;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0001F200 File Offset: 0x0001D400
		public long MicrosecondsPerQuarterNote
		{
			get
			{
				return this._microsecondsPerBeat;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Value of microseconds per quarter note is zero or negative.");
				this._microsecondsPerBeat = value;
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001F219 File Offset: 0x0001D419
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			this.MicrosecondsPerQuarterNote = (long)((ulong)reader.Read3ByteDword());
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001F228 File Offset: 0x0001D428
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.Write3ByteDword((uint)this.MicrosecondsPerQuarterNote);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001F237 File Offset: 0x0001D437
		protected override int GetContentSize(WritingSettings settings)
		{
			return 3;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001F23A File Offset: 0x0001D43A
		protected override MidiEvent CloneEvent()
		{
			return new SetTempoEvent(this.MicrosecondsPerQuarterNote);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001F247 File Offset: 0x0001D447
		public override string ToString()
		{
			return string.Format("Set Tempo ({0})", this.MicrosecondsPerQuarterNote);
		}

		// Token: 0x040008A8 RID: 2216
		public const long DefaultTempo = 500000L;

		// Token: 0x040008A9 RID: 2217
		private long _microsecondsPerBeat = 500000L;
	}
}
