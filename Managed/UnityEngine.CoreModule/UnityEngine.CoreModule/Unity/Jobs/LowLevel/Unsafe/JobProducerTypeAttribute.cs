using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000043 RID: 67
	[AttributeUsage(1024)]
	public sealed class JobProducerTypeAttribute : Attribute
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000029B9 File Offset: 0x00000BB9
		public Type ProducerType { get; }

		// Token: 0x060000A3 RID: 163 RVA: 0x000029C1 File Offset: 0x00000BC1
		public JobProducerTypeAttribute(Type producerType)
		{
			this.ProducerType = producerType;
		}
	}
}
