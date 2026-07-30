using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000034 RID: 52
	public sealed class ChordsSplitter : LengthedObjectsSplitter<Chord>
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00007622 File Offset: 0x00005822
		protected override Chord CloneObject(Chord obj)
		{
			return obj.Clone();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000762A File Offset: 0x0000582A
		protected override SplittedLengthedObject<Chord> SplitObject(Chord obj, long time)
		{
			return obj.Split(time);
		}
	}
}
