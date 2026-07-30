using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000831 RID: 2097
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncMethodBuilderAttribute : Attribute
	{
		// Token: 0x0600538D RID: 21389 RVA: 0x001259C6 File Offset: 0x00123BC6
		public AsyncMethodBuilderAttribute(Type builderType)
		{
			this.BuilderType = builderType;
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x0600538E RID: 21390 RVA: 0x001259D5 File Offset: 0x00123BD5
		public Type BuilderType { get; }
	}
}
