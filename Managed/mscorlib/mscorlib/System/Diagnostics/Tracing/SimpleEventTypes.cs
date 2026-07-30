using System;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AB4 RID: 2740
	internal class SimpleEventTypes<T> : TraceLoggingEventTypes
	{
		// Token: 0x06006357 RID: 25431 RVA: 0x0014357A File Offset: 0x0014177A
		private SimpleEventTypes(TraceLoggingTypeInfo<T> typeInfo)
			: base(typeInfo.Name, typeInfo.Tags, new TraceLoggingTypeInfo[] { typeInfo })
		{
			this.typeInfo = typeInfo;
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06006358 RID: 25432 RVA: 0x0014359F File Offset: 0x0014179F
		public static SimpleEventTypes<T> Instance
		{
			get
			{
				return SimpleEventTypes<T>.instance ?? SimpleEventTypes<T>.InitInstance();
			}
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x001435B0 File Offset: 0x001417B0
		private static SimpleEventTypes<T> InitInstance()
		{
			SimpleEventTypes<T> simpleEventTypes = new SimpleEventTypes<T>(TraceLoggingTypeInfo<T>.Instance);
			Interlocked.CompareExchange<SimpleEventTypes<T>>(ref SimpleEventTypes<T>.instance, simpleEventTypes, null);
			return SimpleEventTypes<T>.instance;
		}

		// Token: 0x04003185 RID: 12677
		private static SimpleEventTypes<T> instance;

		// Token: 0x04003186 RID: 12678
		internal readonly TraceLoggingTypeInfo<T> typeInfo;
	}
}
