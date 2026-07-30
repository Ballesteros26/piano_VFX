using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000178 RID: 376
	public sealed class UnexpectedRunningStatusException : MidiException
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x0002079C File Offset: 0x0001E99C
		internal UnexpectedRunningStatusException()
			: base("Unexpected running status is encountered.")
		{
		}
	}
}
