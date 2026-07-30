using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000087 RID: 135
	public sealed class Octave
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		private Octave(int octave)
		{
			this.Number = octave;
			this._notes = (from NoteName n in Enum.GetValues(typeof(NoteName))
				where NoteUtilities.IsNoteValid(n, octave)
				select n).ToDictionary((NoteName n) => n, (NoteName n) => Note.Get(n, octave));
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000EC63 File Offset: 0x0000CE63
		public int Number { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000EC6B File Offset: 0x0000CE6B
		public Note C
		{
			get
			{
				return this.GetNote(NoteName.C);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000EC74 File Offset: 0x0000CE74
		public Note CSharp
		{
			get
			{
				return this.GetNote(NoteName.CSharp);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000EC7D File Offset: 0x0000CE7D
		public Note D
		{
			get
			{
				return this.GetNote(NoteName.D);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000EC86 File Offset: 0x0000CE86
		public Note DSharp
		{
			get
			{
				return this.GetNote(NoteName.DSharp);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000EC8F File Offset: 0x0000CE8F
		public Note E
		{
			get
			{
				return this.GetNote(NoteName.E);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000EC98 File Offset: 0x0000CE98
		public Note F
		{
			get
			{
				return this.GetNote(NoteName.F);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000ECA1 File Offset: 0x0000CEA1
		public Note FSharp
		{
			get
			{
				return this.GetNote(NoteName.FSharp);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000ECAA File Offset: 0x0000CEAA
		public Note G
		{
			get
			{
				return this.GetNote(NoteName.G);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000ECB3 File Offset: 0x0000CEB3
		public Note GSharp
		{
			get
			{
				return this.GetNote(NoteName.GSharp);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		public Note A
		{
			get
			{
				return this.GetNote(NoteName.A);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000ECC6 File Offset: 0x0000CEC6
		public Note ASharp
		{
			get
			{
				return this.GetNote(NoteName.ASharp);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public Note B
		{
			get
			{
				return this.GetNote(NoteName.B);
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public Note GetNote(NoteName noteName)
		{
			ThrowIfArgument.IsInvalidEnumValue<NoteName>("noteName", noteName);
			Note note;
			if (!this._notes.TryGetValue(noteName, out note))
			{
				throw new InvalidOperationException(string.Format("Unable to get the {0} note.", noteName));
			}
			return note;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public static Octave Get(int octaveNumber)
		{
			ThrowIfArgument.IsOutOfRange("octaveNumber", octaveNumber, Octave.MinOctaveNumber, Octave.MaxOctaveNumber, string.Format("Octave number is out of [{0}, {1}] range.", Octave.MinOctaveNumber, Octave.MaxOctaveNumber));
			Octave octave;
			if (!Octave.Cache.TryGetValue(octaveNumber, out octave))
			{
				Octave.Cache.Add(octaveNumber, octave = new Octave(octaveNumber));
			}
			return octave;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000ED7F File Offset: 0x0000CF7F
		public static bool operator ==(Octave octave1, Octave octave2)
		{
			return octave1 == octave2 || (octave1 != null && octave2 != null && octave1.Number == octave2.Number);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000ED9D File Offset: 0x0000CF9D
		public static bool operator !=(Octave octave1, Octave octave2)
		{
			return !(octave1 == octave2);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000EDA9 File Offset: 0x0000CFA9
		public override string ToString()
		{
			return string.Format("Octave {0}", this.Number);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		public override bool Equals(object obj)
		{
			return this == obj as Octave;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		public override int GetHashCode()
		{
			return this.Number.GetHashCode();
		}

		// Token: 0x040005DA RID: 1498
		private static readonly Dictionary<int, Octave> Cache = new Dictionary<int, Octave>();

		// Token: 0x040005DB RID: 1499
		private readonly Dictionary<NoteName, Note> _notes;

		// Token: 0x040005DC RID: 1500
		public const int OctaveSize = 12;

		// Token: 0x040005DD RID: 1501
		public static readonly int MinOctaveNumber = NoteUtilities.GetNoteOctave(SevenBitNumber.MinValue);

		// Token: 0x040005DE RID: 1502
		public static readonly int MaxOctaveNumber = NoteUtilities.GetNoteOctave(SevenBitNumber.MaxValue);

		// Token: 0x040005DF RID: 1503
		public static readonly Octave Middle = Octave.Get(4);
	}
}
