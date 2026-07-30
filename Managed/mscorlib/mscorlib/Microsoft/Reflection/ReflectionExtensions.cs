using System;
using System.Reflection;

namespace Microsoft.Reflection
{
	// Token: 0x020000A1 RID: 161
	internal static class ReflectionExtensions
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		public static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001F0F8 File Offset: 0x0001D2F8
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001F100 File Offset: 0x0001D300
		public static Assembly Assembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001F108 File Offset: 0x0001D308
		public static TypeCode GetTypeCode(this Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001F110 File Offset: 0x0001D310
		public static bool ReflectionOnly(this Assembly assm)
		{
			return assm.ReflectionOnly;
		}
	}
}
