using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A9F RID: 2719
	internal sealed class EnumerableTypeInfo<IterableType, ElementType> : TraceLoggingTypeInfo<IterableType> where IterableType : IEnumerable<ElementType>
	{
		// Token: 0x060062E1 RID: 25313 RVA: 0x00142599 File Offset: 0x00140799
		public EnumerableTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x001425A8 File Offset: 0x001407A8
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x001425C4 File Offset: 0x001407C4
		public override void WriteData(TraceLoggingDataCollector collector, ref IterableType value)
		{
			int num = collector.BeginBufferedArray();
			int num2 = 0;
			if (value != null)
			{
				foreach (ElementType elementType in value)
				{
					this.elementInfo.WriteData(collector, ref elementType);
					num2++;
				}
			}
			collector.EndBufferedArray(num, num2);
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x0014263C File Offset: 0x0014083C
		public override object GetData(object value)
		{
			IterableType iterableType = (IterableType)((object)value);
			List<object> list = new List<object>();
			foreach (ElementType elementType in iterableType)
			{
				list.Add(this.elementInfo.GetData(elementType));
			}
			return list.ToArray();
		}

		// Token: 0x04003140 RID: 12608
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
