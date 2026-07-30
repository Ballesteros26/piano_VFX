using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x0200008D RID: 141
	public sealed class Chord : ILengthedObject, ITimedObject, IMusicalObject
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002D5 RID: 725 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		// (remove) Token: 0x060002D6 RID: 726 RVA: 0x0000FF20 File Offset: 0x0000E120
		public event NotesCollectionChangedEventHandler NotesCollectionChanged;

		// Token: 0x060002D7 RID: 727 RVA: 0x0000FF55 File Offset: 0x0000E155
		public Chord()
			: this(Enumerable.Empty<Note>())
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000FF62 File Offset: 0x0000E162
		public Chord(IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			this.Notes = new NotesCollection(notes);
			this.Notes.CollectionChanged += this.OnNotesCollectionChanged;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000FF98 File Offset: 0x0000E198
		public Chord(params Note[] notes)
			: this(notes)
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000FFA1 File Offset: 0x0000E1A1
		public Chord(IEnumerable<Note> notes, long time)
			: this(notes)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			this.Time = time;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000FFBC File Offset: 0x0000E1BC
		public NotesCollection Notes { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public long Time
		{
			get
			{
				Note note = this.Notes.FirstOrDefault<Note>();
				if (note == null)
				{
					return 0L;
				}
				return note.Time;
			}
			set
			{
				ThrowIfTimeArgument.IsNegative("value", value);
				long time = this.Time;
				foreach (Note note in this.Notes)
				{
					long num = note.Time - time;
					note.Time = value + num;
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00010048 File Offset: 0x0000E248
		// (set) Token: 0x060002DF RID: 735 RVA: 0x000100D4 File Offset: 0x0000E2D4
		public long Length
		{
			get
			{
				if (!this.Notes.Any<Note>())
				{
					return 0L;
				}
				long num = long.MaxValue;
				long num2 = long.MinValue;
				foreach (Note note in this.Notes)
				{
					long time = note.Time;
					num = Math.Min(time, num);
					num2 = Math.Max(time + note.Length, num2);
				}
				return num2 - num;
			}
			set
			{
				long num = value - this.Length;
				foreach (Note note in this.Notes)
				{
					note.Length += num;
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00010130 File Offset: 0x0000E330
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0001013D File Offset: 0x0000E33D
		public FourBitNumber Channel
		{
			get
			{
				return this.GetNotesProperty<FourBitNumber>(Chord.ChannelPropertySelector);
			}
			set
			{
				this.SetNotesProperty<FourBitNumber>(Chord.ChannelPropertySelector, value);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0001014B File Offset: 0x0000E34B
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00010158 File Offset: 0x0000E358
		public SevenBitNumber Velocity
		{
			get
			{
				return this.GetNotesProperty<SevenBitNumber>(Chord.VelocityPropertySelector);
			}
			set
			{
				this.SetNotesProperty<SevenBitNumber>(Chord.VelocityPropertySelector, value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00010166 File Offset: 0x0000E366
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00010173 File Offset: 0x0000E373
		public SevenBitNumber OffVelocity
		{
			get
			{
				return this.GetNotesProperty<SevenBitNumber>(Chord.OffVelocityPropertySelector);
			}
			set
			{
				this.SetNotesProperty<SevenBitNumber>(Chord.OffVelocityPropertySelector, value);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00010181 File Offset: 0x0000E381
		public Chord Clone()
		{
			return new Chord(this.Notes.Select((Note note) => note.Clone()));
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public SplittedLengthedObject<Chord> Split(long time)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			long time2 = this.Time;
			long num = time2 + this.Length;
			if (time <= time2)
			{
				return new SplittedLengthedObject<Chord>(null, this.Clone());
			}
			if (time >= num)
			{
				return new SplittedLengthedObject<Chord>(this.Clone(), null);
			}
			SplittedLengthedObject<Note>[] array = this.Notes.Select((Note n) => n.Split(time)).ToArray<SplittedLengthedObject<Note>>();
			Chord chord = new Chord(from p in array
				select p.LeftPart into p
				where p != null
				select p);
			Chord chord2 = new Chord(from p in array
				select p.RightPart into p
				where p != null
				select p);
			return new SplittedLengthedObject<Chord>(chord, chord2);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000102DC File Offset: 0x0000E4DC
		private void OnNotesCollectionChanged(NotesCollection collection, NotesCollectionChangedEventArgs args)
		{
			NotesCollectionChangedEventHandler notesCollectionChanged = this.NotesCollectionChanged;
			if (notesCollectionChanged == null)
			{
				return;
			}
			notesCollectionChanged(collection, args);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000102F0 File Offset: 0x0000E4F0
		private TValue GetNotesProperty<TValue>(Expression<Func<Note, TValue>> propertySelector)
		{
			if (!this.Notes.Any<Note>())
			{
				throw new InvalidOperationException("Chord doesn't contain notes.");
			}
			PropertyInfo propertyInfo = Chord.GetPropertyInfo<TValue>(propertySelector);
			TValue[] array = this.Notes.Select((Note n) => (TValue)((object)propertyInfo.GetValue(n))).Distinct<TValue>().ToArray<TValue>();
			if (array.Length > 1)
			{
				throw new InvalidOperationException("Chord's notes have different values of the " + propertyInfo.Name + " property.");
			}
			return array.First<TValue>();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00010374 File Offset: 0x0000E574
		private void SetNotesProperty<TValue>(Expression<Func<Note, TValue>> propertySelector, TValue value)
		{
			PropertyInfo propertyInfo = Chord.GetPropertyInfo<TValue>(propertySelector);
			foreach (Note note in this.Notes)
			{
				propertyInfo.SetValue(note, value);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000103D0 File Offset: 0x0000E5D0
		private static PropertyInfo GetPropertyInfo<TValue>(Expression<Func<Note, TValue>> propertySelector)
		{
			MemberExpression memberExpression = propertySelector.Body as MemberExpression;
			return ((memberExpression != null) ? memberExpression.Member : null) as PropertyInfo;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000103F0 File Offset: 0x0000E5F0
		public override string ToString()
		{
			NotesCollection notes = this.Notes;
			if (!notes.Any<Note>())
			{
				return "Empty notes collection";
			}
			return string.Join<Note>(" ", notes.OrderBy((Note n) => n.NoteNumber));
		}

		// Token: 0x04000660 RID: 1632
		private static readonly Expression<Func<Note, FourBitNumber>> ChannelPropertySelector = (Note n) => n.Channel;

		// Token: 0x04000661 RID: 1633
		private static readonly Expression<Func<Note, SevenBitNumber>> VelocityPropertySelector = (Note n) => n.Velocity;

		// Token: 0x04000662 RID: 1634
		private static readonly Expression<Func<Note, SevenBitNumber>> OffVelocityPropertySelector = (Note n) => n.OffVelocity;
	}
}
