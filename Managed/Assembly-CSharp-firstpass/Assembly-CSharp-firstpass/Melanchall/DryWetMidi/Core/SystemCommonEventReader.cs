using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000157 RID: 343
	internal sealed class SystemCommonEventReader : IEventReader
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x0001FBD4 File Offset: 0x0001DDD4
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			SystemCommonEvent systemCommonEvent = null;
			switch (currentStatusByte)
			{
			case 241:
				systemCommonEvent = new MidiTimeCodeEvent();
				break;
			case 242:
				systemCommonEvent = new SongPositionPointerEvent();
				break;
			case 243:
				systemCommonEvent = new SongSelectEvent();
				break;
			case 246:
				systemCommonEvent = new TuneRequestEvent();
				break;
			}
			if (systemCommonEvent != null)
			{
				systemCommonEvent.Read(reader, settings, -1);
			}
			return systemCommonEvent;
		}
	}
}
