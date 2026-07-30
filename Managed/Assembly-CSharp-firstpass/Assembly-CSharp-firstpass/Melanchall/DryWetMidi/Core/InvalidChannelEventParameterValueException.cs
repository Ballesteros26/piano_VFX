using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200016E RID: 366
	public sealed class InvalidChannelEventParameterValueException : MidiException
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x000205E8 File Offset: 0x0001E7E8
		internal InvalidChannelEventParameterValueException(Type eventType, byte value)
			: base(string.Format("{0} is invalid value for parameter of channel event of {1} type.", value, eventType))
		{
			this.EventType = eventType;
			this.Value = value;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0002060F File Offset: 0x0001E80F
		public Type EventType { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00020617 File Offset: 0x0001E817
		public byte Value { get; }
	}
}
