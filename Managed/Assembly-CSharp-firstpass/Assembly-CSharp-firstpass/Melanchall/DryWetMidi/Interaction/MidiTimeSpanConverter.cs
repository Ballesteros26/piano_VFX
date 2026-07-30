using System;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B5 RID: 181
	internal sealed class MidiTimeSpanConverter : ITimeSpanConverter
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x000144CB File Offset: 0x000126CB
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			return (MidiTimeSpan)timeSpan;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000144D3 File Offset: 0x000126D3
		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			return ((MidiTimeSpan)timeSpan).TimeSpan;
		}
	}
}
