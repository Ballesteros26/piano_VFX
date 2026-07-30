using System;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002C7 RID: 711
	internal static class ParameterProviderExtensions
	{
		// Token: 0x0600152F RID: 5423 RVA: 0x0003F780 File Offset: 0x0003D980
		public static int IndexOf(this IParameterProvider provider, ParameterExpression parameter)
		{
			int i = 0;
			int parameterCount = provider.ParameterCount;
			while (i < parameterCount)
			{
				if (provider.GetParameter(i) == parameter)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0003F7AD File Offset: 0x0003D9AD
		public static bool Contains(this IParameterProvider provider, ParameterExpression parameter)
		{
			return provider.IndexOf(parameter) >= 0;
		}
	}
}
