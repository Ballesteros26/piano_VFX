using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x0200009B RID: 155
	public sealed class Note : ILengthedObject, ITimedObject, IMusicalObject
	{
		// Token: 0x06000339 RID: 825 RVA: 0x000111BD File Offset: 0x0000F3BD
		public Note(NoteName noteName, int octave)
			: this(noteName, octave, 0L)
		{
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000111C9 File Offset: 0x0000F3C9
		public Note(NoteName noteName, int octave, long length)
			: this(noteName, octave, length, 0L)
		{
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000111D6 File Offset: 0x0000F3D6
		public Note(NoteName noteName, int octave, long length, long time)
			: this(NoteUtilities.GetNoteNumber(noteName, octave))
		{
			this.Length = length;
			this.Time = time;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000111F4 File Offset: 0x0000F3F4
		public Note(SevenBitNumber noteNumber)
			: this(noteNumber, 0L)
		{
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000111FF File Offset: 0x0000F3FF
		public Note(SevenBitNumber noteNumber, long length)
			: this(noteNumber, length, 0L)
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001120C File Offset: 0x0000F40C
		public Note(SevenBitNumber noteNumber, long length, long time)
		{
			this.Velocity = Note.DefaultVelocity;
			this.TimedNoteOnEvent = new TimedEvent(new NoteOnEvent());
			this.TimedNoteOffEvent = new TimedEvent(new NoteOffEvent());
			base..ctor();
			this.UnderlyingNote = Note.Get(noteNumber);
			this.Length = length;
			this.Time = time;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00011264 File Offset: 0x0000F464
		internal Note(TimedEvent timedNoteOnEvent, TimedEvent timedNoteOffEvent)
		{
			this.Velocity = Note.DefaultVelocity;
			this.TimedNoteOnEvent = new TimedEvent(new NoteOnEvent());
			this.TimedNoteOffEvent = new TimedEvent(new NoteOffEvent());
			base..ctor();
			NoteOnEvent noteOnEvent = (NoteOnEvent)timedNoteOnEvent.Event;
			NoteOffEvent noteOffEvent = (NoteOffEvent)timedNoteOffEvent.Event;
			this.TimedNoteOnEvent = timedNoteOnEvent;
			this.TimedNoteOffEvent = timedNoteOffEvent;
			this.UnderlyingNote = Note.Get(noteOnEvent.NoteNumber);
			this.Velocity = noteOnEvent.Velocity;
			this.OffVelocity = noteOffEvent.Velocity;
			this.Channel = noteOnEvent.Channel;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000340 RID: 832 RVA: 0x000112FD File Offset: 0x0000F4FD
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0001130A File Offset: 0x0000F50A
		public long Time
		{
			get
			{
				return this.TimedNoteOnEvent.Time;
			}
			set
			{
				ThrowIfTimeArgument.IsNegative("value", value);
				this.TimedNoteOffEvent.Time = value + this.Length;
				this.TimedNoteOnEvent.Time = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00011336 File Offset: 0x0000F536
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0001134F File Offset: 0x0000F54F
		public long Length
		{
			get
			{
				return this.TimedNoteOffEvent.Time - this.TimedNoteOnEvent.Time;
			}
			set
			{
				ThrowIfLengthArgument.IsNegative("value", value);
				this.TimedNoteOffEvent.Time = this.TimedNoteOnEvent.Time + value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00011374 File Offset: 0x0000F574
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00011381 File Offset: 0x0000F581
		public SevenBitNumber NoteNumber
		{
			get
			{
				return this.UnderlyingNote.NoteNumber;
			}
			set
			{
				this.UnderlyingNote = Note.Get(value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0001138F File Offset: 0x0000F58F
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00011397 File Offset: 0x0000F597
		public SevenBitNumber Velocity { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000113A0 File Offset: 0x0000F5A0
		// (set) Token: 0x06000349 RID: 841 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public SevenBitNumber OffVelocity { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000113B1 File Offset: 0x0000F5B1
		// (set) Token: 0x0600034B RID: 843 RVA: 0x000113B9 File Offset: 0x0000F5B9
		public FourBitNumber Channel { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000113C2 File Offset: 0x0000F5C2
		public NoteName NoteName
		{
			get
			{
				return this.UnderlyingNote.NoteName;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000113CF File Offset: 0x0000F5CF
		public int Octave
		{
			get
			{
				return this.UnderlyingNote.Octave;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000113DC File Offset: 0x0000F5DC
		internal TimedEvent TimedNoteOnEvent { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000113E4 File Offset: 0x0000F5E4
		internal TimedEvent TimedNoteOffEvent { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000113EC File Offset: 0x0000F5EC
		// (set) Token: 0x06000351 RID: 849 RVA: 0x000113F4 File Offset: 0x0000F5F4
		internal Note UnderlyingNote { get; private set; }

		// Token: 0x06000352 RID: 850 RVA: 0x000113FD File Offset: 0x0000F5FD
		public TimedEvent GetTimedNoteOnEvent()
		{
			return new TimedEvent(new NoteOnEvent(this.NoteNumber, this.Velocity)
			{
				Channel = this.Channel
			}, this.Time);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00011427 File Offset: 0x0000F627
		public TimedEvent GetTimedNoteOffEvent()
		{
			return new TimedEvent(new NoteOffEvent(this.NoteNumber, this.OffVelocity)
			{
				Channel = this.Channel
			}, this.Time + this.Length);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011458 File Offset: 0x0000F658
		public void SetNoteNameAndOctave(NoteName noteName, int octave)
		{
			this.UnderlyingNote = Note.Get(noteName, octave);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00011467 File Offset: 0x0000F667
		public Note Clone()
		{
			return new Note(this.NoteNumber, this.Length, this.Time)
			{
				Channel = this.Channel,
				Velocity = this.Velocity,
				OffVelocity = this.OffVelocity
			};
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000114A4 File Offset: 0x0000F6A4
		public SplittedLengthedObject<Note> Split(long time)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			long time2 = this.Time;
			long num = time2 + this.Length;
			if (time <= time2)
			{
				return new SplittedLengthedObject<Note>(null, this.Clone());
			}
			if (time >= num)
			{
				return new SplittedLengthedObject<Note>(this.Clone(), null);
			}
			Note note = this.Clone();
			note.Length = time - time2;
			Note note2 = this.Clone();
			note2.Time = time;
			note2.Length = num - time;
			return new SplittedLengthedObject<Note>(note, note2);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001151A File Offset: 0x0000F71A
		public override string ToString()
		{
			return this.UnderlyingNote.ToString();
		}

		// Token: 0x04000673 RID: 1651
		public static readonly SevenBitNumber DefaultVelocity = (SevenBitNumber)100;
	}
}
