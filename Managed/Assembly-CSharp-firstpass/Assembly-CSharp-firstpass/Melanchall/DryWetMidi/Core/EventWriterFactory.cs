using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000168 RID: 360
	internal static class EventWriterFactory
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x000202AD File Offset: 0x0001E4AD
		internal static IEventWriter GetWriter(MidiEvent midiEvent)
		{
			if (midiEvent is MetaEvent)
			{
				return EventWriterFactory.MetaEventWriter;
			}
			if (midiEvent is ChannelEvent)
			{
				return EventWriterFactory.ChannelEventWriter;
			}
			if (midiEvent is SystemRealTimeEvent)
			{
				return EventWriterFactory.SystemRealTimeEventWriter;
			}
			if (midiEvent is SystemCommonEvent)
			{
				return EventWriterFactory.SystemCommonEventWriter;
			}
			return EventWriterFactory.SysExEventWriter;
		}

		// Token: 0x040008CE RID: 2254
		private static readonly IEventWriter MetaEventWriter = new MetaEventWriter();

		// Token: 0x040008CF RID: 2255
		private static readonly IEventWriter ChannelEventWriter = new ChannelEventWriter();

		// Token: 0x040008D0 RID: 2256
		private static readonly IEventWriter SysExEventWriter = new SysExEventWriter();

		// Token: 0x040008D1 RID: 2257
		private static readonly IEventWriter SystemRealTimeEventWriter = new SystemRealTimeEventWriter();

		// Token: 0x040008D2 RID: 2258
		private static readonly IEventWriter SystemCommonEventWriter = new SystemCommonEventWriter();
	}
}
