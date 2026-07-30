using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200013B RID: 315
	public sealed class EventType
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x0001EADA File Offset: 0x0001CCDA
		public EventType(Type type, byte statusByte)
		{
			this.Type = type;
			this.StatusByte = statusByte;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001EAF0 File Offset: 0x0001CCF0
		public Type Type { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
		public byte StatusByte { get; }
	}
}
