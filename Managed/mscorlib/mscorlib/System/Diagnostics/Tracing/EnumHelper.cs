using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A9C RID: 2716
	internal static class EnumHelper<UnderlyingType>
	{
		// Token: 0x060062D9 RID: 25305 RVA: 0x00142551 File Offset: 0x00140751
		public static UnderlyingType Cast<ValueType>(ValueType value)
		{
			return EnumHelper<UnderlyingType>.Caster<ValueType>.Instance(value);
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x00002119 File Offset: 0x00000319
		internal static UnderlyingType Identity(UnderlyingType value)
		{
			return value;
		}

		// Token: 0x0400313E RID: 12606
		private static readonly MethodInfo IdentityInfo = Statics.GetDeclaredStaticMethod(typeof(EnumHelper<UnderlyingType>), "Identity");

		// Token: 0x02000A9D RID: 2717
		// (Invoke) Token: 0x060062DD RID: 25309
		private delegate UnderlyingType Transformer<ValueType>(ValueType value);

		// Token: 0x02000A9E RID: 2718
		private static class Caster<ValueType>
		{
			// Token: 0x0400313F RID: 12607
			public static readonly EnumHelper<UnderlyingType>.Transformer<ValueType> Instance = (EnumHelper<UnderlyingType>.Transformer<ValueType>)Statics.CreateDelegate(typeof(EnumHelper<UnderlyingType>.Transformer<ValueType>), EnumHelper<UnderlyingType>.IdentityInfo);
		}
	}
}
