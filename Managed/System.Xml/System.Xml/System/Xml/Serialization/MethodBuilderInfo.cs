using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x020002CF RID: 719
	internal class MethodBuilderInfo
	{
		// Token: 0x06001B1B RID: 6939 RVA: 0x00096A04 File Offset: 0x00094C04
		public MethodBuilderInfo(MethodBuilder methodBuilder, Type[] parameterTypes)
		{
			this.MethodBuilder = methodBuilder;
			this.ParameterTypes = parameterTypes;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00002F50 File Offset: 0x00001150
		public void Validate(Type returnType, Type[] parameterTypes, MethodAttributes attributes)
		{
		}

		// Token: 0x040015C5 RID: 5573
		public readonly MethodBuilder MethodBuilder;

		// Token: 0x040015C6 RID: 5574
		public readonly Type[] ParameterTypes;
	}
}
