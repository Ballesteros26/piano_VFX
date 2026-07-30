using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200003B RID: 59
	public sealed class NotesMerger
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00008C6C File Offset: 0x00006E6C
		public IEnumerable<Note> Merge(IEnumerable<Note> notes, TempoMap tempoMap, NotesMergingSettings settings = null)
		{
			ThrowIfArgument.IsNull("notes", notes);
			settings = settings ?? new NotesMergingSettings();
			Dictionary<NoteId, NotesMerger.NoteHolder> currentNotes = new Dictionary<NoteId, NotesMerger.NoteHolder>();
			Type toleranceType = settings.Tolerance.GetType();
			foreach (Note note in from n in notes
				where n != null
				orderby n.Time
				select n)
			{
				NoteId noteId = note.GetNoteId();
				NotesMerger.NoteHolder noteHolder;
				if (!currentNotes.TryGetValue(noteId, out noteHolder))
				{
					currentNotes.Add(noteId, NotesMerger.CreateNoteHolder(note, settings));
				}
				else
				{
					long endTime = noteHolder.EndTime;
					if (LengthConverter.ConvertTo((MidiTimeSpan)Math.Max(0L, note.Time - endTime), toleranceType, endTime, tempoMap).CompareTo(settings.Tolerance) <= 0)
					{
						long num = Math.Max(note.Time + note.Length, endTime);
						noteHolder.EndTime = num;
						noteHolder.MergeVelocities(note);
					}
					else
					{
						yield return currentNotes[noteId].GetResultNote();
						currentNotes[noteId] = NotesMerger.CreateNoteHolder(note, settings);
					}
					noteId = null;
					note = null;
				}
			}
			IEnumerator<Note> enumerator = null;
			foreach (NotesMerger.NoteHolder noteHolder2 in currentNotes.Values)
			{
				yield return noteHolder2.GetResultNote();
			}
			Dictionary<NoteId, NotesMerger.NoteHolder>.ValueCollection.Enumerator enumerator2 = default(Dictionary<NoteId, NotesMerger.NoteHolder>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008C8A File Offset: 0x00006E8A
		private static NotesMerger.NoteHolder CreateNoteHolder(Note note, NotesMergingSettings settings)
		{
			return new NotesMerger.NoteHolder(note.Clone(), NotesMerger.VelocityMergers[settings.VelocityMergingPolicy](), NotesMerger.VelocityMergers[settings.OffVelocityMergingPolicy]());
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008CC4 File Offset: 0x00006EC4
		// Note: this type is marked as 'beforefieldinit'.
		static NotesMerger()
		{
			Dictionary<VelocityMergingPolicy, Func<VelocityMerger>> dictionary = new Dictionary<VelocityMergingPolicy, Func<VelocityMerger>>();
			dictionary[VelocityMergingPolicy.First] = () => new FirstVelocityMerger();
			dictionary[VelocityMergingPolicy.Last] = () => new LastVelocityMerger();
			dictionary[VelocityMergingPolicy.Min] = () => new MinVelocityMerger();
			dictionary[VelocityMergingPolicy.Max] = () => new MaxVelocityMerger();
			dictionary[VelocityMergingPolicy.Average] = () => new AverageVelocityMerger();
			NotesMerger.VelocityMergers = dictionary;
		}

		// Token: 0x040000C8 RID: 200
		private static readonly Dictionary<VelocityMergingPolicy, Func<VelocityMerger>> VelocityMergers;

		// Token: 0x0200020D RID: 525
		private sealed class NoteHolder
		{
			// Token: 0x06000CD5 RID: 3285 RVA: 0x00027E98 File Offset: 0x00026098
			public NoteHolder(Note note, VelocityMerger velocityMerger, VelocityMerger offVelocityMerger)
			{
				this._note = note;
				this._velocityMerger = velocityMerger;
				this._offVelocityMerger = offVelocityMerger;
				this._velocityMerger.Initialize(note.Velocity);
				this._offVelocityMerger.Initialize(note.OffVelocity);
				this.EndTime = this._note.Time + this._note.Length;
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00027EFF File Offset: 0x000260FF
			// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00027F07 File Offset: 0x00026107
			public long EndTime { get; set; }

			// Token: 0x06000CD8 RID: 3288 RVA: 0x00027F10 File Offset: 0x00026110
			public void MergeVelocities(Note note)
			{
				this._velocityMerger.Merge(note.Velocity);
				this._offVelocityMerger.Merge(note.OffVelocity);
			}

			// Token: 0x06000CD9 RID: 3289 RVA: 0x00027F34 File Offset: 0x00026134
			public Note GetResultNote()
			{
				this._note.Length = this.EndTime - this._note.Time;
				this._note.Velocity = this._velocityMerger.Velocity;
				this._note.OffVelocity = this._offVelocityMerger.Velocity;
				return this._note;
			}

			// Token: 0x04000C00 RID: 3072
			private readonly Note _note;

			// Token: 0x04000C01 RID: 3073
			private readonly VelocityMerger _velocityMerger;

			// Token: 0x04000C02 RID: 3074
			private readonly VelocityMerger _offVelocityMerger;
		}
	}
}
