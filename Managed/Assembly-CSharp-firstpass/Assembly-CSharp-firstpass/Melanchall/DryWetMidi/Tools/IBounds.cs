using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000058 RID: 88
	public interface IBounds
	{
		// Token: 0x060001E5 RID: 485
		Tuple<long, long> GetBounds(long time, TempoMap tempoMap);
	}
}
