using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000083 RID: 131
	[AttributeUsage(4, AllowMultiple = true, Inherited = true)]
	[BaseTypeRequired(typeof(Attribute))]
	public sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00004062 File Offset: 0x00002262
		public BaseTypeRequiredAttribute([NotNull] Type baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00004074 File Offset: 0x00002274
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000407C File Offset: 0x0000227C
		[NotNull]
		public Type BaseType { get; private set; }
	}
}
