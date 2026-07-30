using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AAD RID: 2733
	internal abstract class PropertyAccessor<ContainerType>
	{
		// Token: 0x06006341 RID: 25409
		public abstract void Write(TraceLoggingDataCollector collector, ref ContainerType value);

		// Token: 0x06006342 RID: 25410
		public abstract object GetData(ContainerType value);

		// Token: 0x06006343 RID: 25411 RVA: 0x001432F8 File Offset: 0x001414F8
		public static PropertyAccessor<ContainerType> Create(PropertyAnalysis property)
		{
			Type returnType = property.getterInfo.ReturnType;
			if (!Statics.IsValueType(typeof(ContainerType)))
			{
				if (returnType == typeof(int))
				{
					return new ClassPropertyWriter<ContainerType, int>(property);
				}
				if (returnType == typeof(long))
				{
					return new ClassPropertyWriter<ContainerType, long>(property);
				}
				if (returnType == typeof(string))
				{
					return new ClassPropertyWriter<ContainerType, string>(property);
				}
			}
			return new NonGenericProperytWriter<ContainerType>(property);
		}
	}
}
