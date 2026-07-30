using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x02000094 RID: 148
	public interface IGrid
	{
		// Token: 0x06000321 RID: 801
		IEnumerable<long> GetTimes(TempoMap tempoMap);
	}
}
