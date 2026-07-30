using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000035 RID: 53
	public sealed class NotesSplitter : LengthedObjectsSplitter<Note>
	{
		// Token: 0x06000140 RID: 320 RVA: 0x0000763B File Offset: 0x0000583B
		protected override Note CloneObject(Note obj)
		{
			return obj.Clone();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007643 File Offset: 0x00005843
		protected override SplittedLengthedObject<Note> SplitObject(Note obj, long time)
		{
			return obj.Split(time);
		}
	}
}
