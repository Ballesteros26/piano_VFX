using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000060 RID: 96
	public sealed class TimedEventsRandomizer : Randomizer<TimedEvent, TimedEventsRandomizingSettings>
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x00009DF9 File Offset: 0x00007FF9
		public void Randomize(IEnumerable<TimedEvent> objects, IBounds bounds, TempoMap tempoMap, TimedEventsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			base.RandomizeInternal(objects, bounds, tempoMap, settings);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000096C6 File Offset: 0x000078C6
		protected override long GetObjectTime(TimedEvent obj, TimedEventsRandomizingSettings settings)
		{
			return obj.Time;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000096CE File Offset: 0x000078CE
		protected override void SetObjectTime(TimedEvent obj, long time, TimedEventsRandomizingSettings settings)
		{
			obj.Time = time;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009E27 File Offset: 0x00008027
		protected override TimeProcessingInstruction OnObjectRandomizing(TimedEvent obj, long time, TimedEventsRandomizingSettings settings)
		{
			return new TimeProcessingInstruction(time);
		}
	}
}
