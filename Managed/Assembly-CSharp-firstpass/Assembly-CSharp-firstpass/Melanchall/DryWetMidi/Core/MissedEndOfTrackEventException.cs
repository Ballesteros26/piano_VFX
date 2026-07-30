using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000174 RID: 372
	public sealed class MissedEndOfTrackEventException : MidiException
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x00020720 File Offset: 0x0001E920
		internal MissedEndOfTrackEventException()
			: base("Track chunk doesn't end with End Of Track event.")
		{
		}
	}
}
