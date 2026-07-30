using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AAE RID: 2734
	internal class NonGenericProperytWriter<ContainerType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x06006345 RID: 25413 RVA: 0x00143373 File Offset: 0x00141573
		public NonGenericProperytWriter(PropertyAnalysis property)
		{
			this.getterInfo = property.getterInfo;
			this.typeInfo = property.typeInfo;
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x00143394 File Offset: 0x00141594
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			object obj = ((container == null) ? null : this.getterInfo.Invoke(container, null));
			this.typeInfo.WriteObjectData(collector, obj);
		}

		// Token: 0x06006347 RID: 25415 RVA: 0x001433D6 File Offset: 0x001415D6
		public override object GetData(ContainerType container)
		{
			if (container != null)
			{
				return this.getterInfo.Invoke(container, null);
			}
			return null;
		}

		// Token: 0x0400317B RID: 12667
		private readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x0400317C RID: 12668
		private readonly MethodInfo getterInfo;
	}
}
