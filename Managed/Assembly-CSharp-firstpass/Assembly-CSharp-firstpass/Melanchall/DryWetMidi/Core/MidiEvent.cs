using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200012B RID: 299
	public abstract class MidiEvent
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		public MidiEvent(MidiEventType eventType)
		{
			this.EventType = eventType;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001E3D7 File Offset: 0x0001C5D7
		public MidiEventType EventType { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001E3DF File Offset: 0x0001C5DF
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0001E3E7 File Offset: 0x0001C5E7
		public long DeltaTime
		{
			get
			{
				return this._deltaTime;
			}
			set
			{
				ThrowIfArgument.IsNegative("value", value, "Delta-time is negative.");
				this._deltaTime = value;
			}
		}

		// Token: 0x060007C9 RID: 1993
		internal abstract void Read(MidiReader reader, ReadingSettings settings, int size);

		// Token: 0x060007CA RID: 1994
		internal abstract void Write(MidiWriter writer, WritingSettings settings);

		// Token: 0x060007CB RID: 1995
		internal abstract int GetSize(WritingSettings settings);

		// Token: 0x060007CC RID: 1996
		protected abstract MidiEvent CloneEvent();

		// Token: 0x060007CD RID: 1997 RVA: 0x0001E400 File Offset: 0x0001C600
		public MidiEvent Clone()
		{
			MidiEvent midiEvent = this.CloneEvent();
			midiEvent.DeltaTime = this.DeltaTime;
			return midiEvent;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001E414 File Offset: 0x0001C614
		public static bool Equals(MidiEvent midiEvent1, MidiEvent midiEvent2)
		{
			string text;
			return MidiEvent.Equals(midiEvent1, midiEvent2, out text);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001E42A File Offset: 0x0001C62A
		public static bool Equals(MidiEvent midiEvent1, MidiEvent midiEvent2, out string message)
		{
			return MidiEvent.Equals(midiEvent1, midiEvent2, null, out message);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001E435 File Offset: 0x0001C635
		public static bool Equals(MidiEvent midiEvent1, MidiEvent midiEvent2, MidiEventEqualityCheckSettings settings, out string message)
		{
			return MidiEventEquality.Equals(midiEvent1, midiEvent2, settings ?? new MidiEventEqualityCheckSettings(), out message);
		}

		// Token: 0x04000851 RID: 2129
		public const int UnknownContentSize = -1;

		// Token: 0x04000852 RID: 2130
		private long _deltaTime;
	}
}
