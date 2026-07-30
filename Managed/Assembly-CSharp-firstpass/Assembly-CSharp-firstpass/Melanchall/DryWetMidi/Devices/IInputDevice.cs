using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000EA RID: 234
	public interface IInputDevice
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060005B9 RID: 1465
		// (remove) Token: 0x060005BA RID: 1466
		event EventHandler<MidiEventReceivedEventArgs> EventReceived;

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005BB RID: 1467
		bool IsListeningForEvents { get; }

		// Token: 0x060005BC RID: 1468
		void StartEventsListening();

		// Token: 0x060005BD RID: 1469
		void StopEventsListening();
	}
}
