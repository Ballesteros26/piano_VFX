using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000142 RID: 322
	public sealed class DeviceNameEvent : BaseTextEvent
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x0001EDBB File Offset: 0x0001CFBB
		public DeviceNameEvent()
			: base(MidiEventType.DeviceName)
		{
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001EDC5 File Offset: 0x0001CFC5
		public DeviceNameEvent(string deviceName)
			: base(MidiEventType.DeviceName, deviceName)
		{
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001EDD0 File Offset: 0x0001CFD0
		protected override MidiEvent CloneEvent()
		{
			return new DeviceNameEvent(base.Text);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001EDDD File Offset: 0x0001CFDD
		public override string ToString()
		{
			return "Device Name (" + base.Text + ")";
		}
	}
}
