using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000F3 RID: 243
	public sealed class MidiDeviceException : Exception
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x00019965 File Offset: 0x00017B65
		public MidiDeviceException()
		{
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001996D File Offset: 0x00017B6D
		public MidiDeviceException(string message)
			: base(message)
		{
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00019976 File Offset: 0x00017B76
		public MidiDeviceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
