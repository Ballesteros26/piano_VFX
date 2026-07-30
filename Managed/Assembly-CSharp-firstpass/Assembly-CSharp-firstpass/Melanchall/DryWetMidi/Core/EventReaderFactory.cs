using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000153 RID: 339
	internal static class EventReaderFactory
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x0001F8DC File Offset: 0x0001DADC
		internal static IEventReader GetReader(byte statusByte, bool smfOnly)
		{
			if (statusByte == 247 || statusByte == 240)
			{
				return EventReaderFactory.SysExEventReader;
			}
			if (!smfOnly)
			{
				if (statusByte == 254 || statusByte == 251 || statusByte == 255 || statusByte == 250 || statusByte == 252 || statusByte == 248)
				{
					return EventReaderFactory.SystemRealTimeEventReader;
				}
				if (statusByte == 241 || statusByte == 242 || statusByte == 243 || statusByte == 246)
				{
					return EventReaderFactory.SystemCommonEventReader;
				}
			}
			if (statusByte == 255)
			{
				return EventReaderFactory.MetaEventReader;
			}
			return EventReaderFactory.ChannelEventReader;
		}

		// Token: 0x040008B5 RID: 2229
		private static readonly IEventReader MetaEventReader = new MetaEventReader();

		// Token: 0x040008B6 RID: 2230
		private static readonly IEventReader ChannelEventReader = new ChannelEventReader();

		// Token: 0x040008B7 RID: 2231
		private static readonly IEventReader SysExEventReader = new SysExEventReader();

		// Token: 0x040008B8 RID: 2232
		private static readonly IEventReader SystemRealTimeEventReader = new SystemRealTimeEventReader();

		// Token: 0x040008B9 RID: 2233
		private static readonly IEventReader SystemCommonEventReader = new SystemCommonEventReader();
	}
}
