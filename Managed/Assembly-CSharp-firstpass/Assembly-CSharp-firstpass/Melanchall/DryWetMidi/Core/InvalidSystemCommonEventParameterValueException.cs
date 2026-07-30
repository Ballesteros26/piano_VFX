using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000172 RID: 370
	public sealed class InvalidSystemCommonEventParameterValueException : MidiException
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x000206D9 File Offset: 0x0001E8D9
		internal InvalidSystemCommonEventParameterValueException(Type eventType, string componentName, int componentValue)
			: base(string.Format("{0} is invalid value for the {1} property of a system common event of {2} type.", componentValue, componentName, eventType))
		{
			this.EventType = eventType;
			this.ComponentName = componentName;
			this.ComponentValue = componentValue;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00020708 File Offset: 0x0001E908
		public Type EventType { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00020710 File Offset: 0x0001E910
		public string ComponentName { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00020718 File Offset: 0x0001E918
		public int ComponentValue { get; }
	}
}
