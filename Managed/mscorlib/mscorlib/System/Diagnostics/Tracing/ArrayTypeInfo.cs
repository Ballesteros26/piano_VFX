using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A97 RID: 2711
	internal sealed class ArrayTypeInfo<ElementType> : TraceLoggingTypeInfo<ElementType[]>
	{
		// Token: 0x060062BF RID: 25279 RVA: 0x00141EE8 File Offset: 0x001400E8
		public ArrayTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x00141EF7 File Offset: 0x001400F7
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x00141F14 File Offset: 0x00140114
		public override void WriteData(TraceLoggingDataCollector collector, ref ElementType[] value)
		{
			int num = collector.BeginBufferedArray();
			int num2 = 0;
			if (value != null)
			{
				num2 = value.Length;
				for (int i = 0; i < value.Length; i++)
				{
					this.elementInfo.WriteData(collector, ref value[i]);
				}
			}
			collector.EndBufferedArray(num, num2);
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x00141F60 File Offset: 0x00140160
		public override object GetData(object value)
		{
			ElementType[] array = (ElementType[])value;
			object[] array2 = new object[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.elementInfo.GetData(array[i]);
			}
			return array2;
		}

		// Token: 0x04003130 RID: 12592
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
