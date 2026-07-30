using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000A7 RID: 167
	internal interface ITempoMapValuesCache
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003A1 RID: 929
		IEnumerable<TempoMapLine> InvalidateOnLines { get; }

		// Token: 0x060003A2 RID: 930
		void Invalidate(TempoMap tempoMap);
	}
}
