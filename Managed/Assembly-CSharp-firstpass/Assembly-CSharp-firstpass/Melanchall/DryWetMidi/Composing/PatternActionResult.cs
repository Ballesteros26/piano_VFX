using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001B5 RID: 437
	internal sealed class PatternActionResult
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x00004D6C File Offset: 0x00002F6C
		public PatternActionResult()
		{
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00022F79 File Offset: 0x00021179
		public PatternActionResult(long? time)
			: this(time, null, null)
		{
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00022F84 File Offset: 0x00021184
		public PatternActionResult(long? time, IEnumerable<Note> notes)
			: this(time, notes, null)
		{
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00022F8F File Offset: 0x0002118F
		public PatternActionResult(long? time, IEnumerable<TimedEvent> events)
			: this(time, null, events)
		{
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00022F9A File Offset: 0x0002119A
		public PatternActionResult(long? time, IEnumerable<Note> notes, IEnumerable<TimedEvent> events)
		{
			this.Time = time;
			this.Notes = notes;
			this.Events = events;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00022FB7 File Offset: 0x000211B7
		public long? Time { get; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00022FBF File Offset: 0x000211BF
		public IEnumerable<Note> Notes { get; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00022FC7 File Offset: 0x000211C7
		public IEnumerable<TimedEvent> Events { get; }

		// Token: 0x04000988 RID: 2440
		public static readonly PatternActionResult DoNothing = new PatternActionResult();
	}
}
