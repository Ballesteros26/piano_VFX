using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000158 RID: 344
	internal sealed class SystemRealTimeEventReader : IEventReader
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x0001FC34 File Offset: 0x0001DE34
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			SystemRealTimeEvent systemRealTimeEvent = null;
			switch (currentStatusByte)
			{
			case 248:
				systemRealTimeEvent = new TimingClockEvent();
				break;
			case 250:
				systemRealTimeEvent = new StartEvent();
				break;
			case 251:
				systemRealTimeEvent = new ContinueEvent();
				break;
			case 252:
				systemRealTimeEvent = new StopEvent();
				break;
			case 254:
				systemRealTimeEvent = new ActiveSensingEvent();
				break;
			case 255:
				systemRealTimeEvent = new ResetEvent();
				break;
			}
			if (systemRealTimeEvent != null)
			{
				systemRealTimeEvent.Read(reader, settings, -1);
			}
			return systemRealTimeEvent;
		}
	}
}
