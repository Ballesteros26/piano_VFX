using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AB3 RID: 2739
	internal sealed class PropertyAnalysis
	{
		// Token: 0x06006356 RID: 25430 RVA: 0x00143555 File Offset: 0x00141755
		public PropertyAnalysis(string name, MethodInfo getterInfo, TraceLoggingTypeInfo typeInfo, EventFieldAttribute fieldAttribute)
		{
			this.name = name;
			this.getterInfo = getterInfo;
			this.typeInfo = typeInfo;
			this.fieldAttribute = fieldAttribute;
		}

		// Token: 0x04003181 RID: 12673
		internal readonly string name;

		// Token: 0x04003182 RID: 12674
		internal readonly MethodInfo getterInfo;

		// Token: 0x04003183 RID: 12675
		internal readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x04003184 RID: 12676
		internal readonly EventFieldAttribute fieldAttribute;
	}
}
