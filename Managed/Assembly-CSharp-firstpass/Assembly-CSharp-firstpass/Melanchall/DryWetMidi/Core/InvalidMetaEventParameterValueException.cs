using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000170 RID: 368
	public sealed class InvalidMetaEventParameterValueException : MidiException
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x0002066B File Offset: 0x0001E86B
		internal InvalidMetaEventParameterValueException(Type eventType, string propertyName, int value)
			: base(string.Format("{0} is invalid value for the {1} property of a meta event of {2} type.", value, propertyName, eventType))
		{
			this.EventType = eventType;
			this.PropertyName = propertyName;
			this.Value = value;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0002069A File Offset: 0x0001E89A
		public Type EventType { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000206A2 File Offset: 0x0001E8A2
		public string PropertyName { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000206AA File Offset: 0x0001E8AA
		public int Value { get; }
	}
}
