using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000173 RID: 371
	public abstract class MidiException : Exception
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x00019965 File Offset: 0x00017B65
		internal MidiException()
		{
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001996D File Offset: 0x00017B6D
		internal MidiException(string message)
			: base(message)
		{
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00019976 File Offset: 0x00017B76
		internal MidiException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
