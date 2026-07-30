using System;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B1 RID: 177
	internal interface ITimeSpanConverter
	{
		// Token: 0x06000405 RID: 1029
		ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap);

		// Token: 0x06000406 RID: 1030
		long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap);
	}
}
