using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000156 RID: 342
	internal sealed class SysExEventReader : IEventReader
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x0001FB90 File Offset: 0x0001DD90
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			int num = reader.ReadVlqNumber();
			SysExEvent sysExEvent = null;
			if (currentStatusByte != 240)
			{
				if (currentStatusByte == 247)
				{
					sysExEvent = new EscapeSysExEvent();
				}
			}
			else
			{
				sysExEvent = new NormalSysExEvent();
			}
			sysExEvent.Read(reader, settings, num);
			return sysExEvent;
		}
	}
}
