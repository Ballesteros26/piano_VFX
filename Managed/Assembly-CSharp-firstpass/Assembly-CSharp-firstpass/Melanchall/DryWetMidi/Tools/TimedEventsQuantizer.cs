using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000051 RID: 81
	public class TimedEventsQuantizer : Quantizer<TimedEvent, TimedEventsQuantizingSettings>
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00009698 File Offset: 0x00007898
		public void Quantize(IEnumerable<TimedEvent> objects, IGrid grid, TempoMap tempoMap, TimedEventsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			base.QuantizeInternal(objects, grid, tempoMap, settings);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000096C6 File Offset: 0x000078C6
		protected sealed override long GetObjectTime(TimedEvent obj, TimedEventsQuantizingSettings settings)
		{
			return obj.Time;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000096CE File Offset: 0x000078CE
		protected sealed override void SetObjectTime(TimedEvent obj, long time, TimedEventsQuantizingSettings settings)
		{
			obj.Time = time;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000096D7 File Offset: 0x000078D7
		protected override TimeProcessingInstruction OnObjectQuantizing(TimedEvent obj, QuantizedTime quantizedTime, IGrid grid, TempoMap tempoMap, TimedEventsQuantizingSettings settings)
		{
			return new TimeProcessingInstruction(quantizedTime.NewTime);
		}
	}
}
