using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Linq
{
	// Token: 0x020000AE RID: 174
	internal static class TypeHelper
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x0000FE08 File Offset: 0x0000E008
		internal static Type FindGenericType(Type definition, Type type)
		{
			bool? flag = null;
			while (type != null && type != typeof(object))
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == definition)
				{
					return type;
				}
				if (flag == null)
				{
					flag = new bool?(definition.IsInterface);
				}
				if (flag.GetValueOrDefault())
				{
					foreach (Type type2 in type.GetInterfaces())
					{
						Type type3 = TypeHelper.FindGenericType(definition, type2);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		internal static IEnumerable<MethodInfo> GetStaticMethods(this Type type)
		{
			return from m in type.GetRuntimeMethods()
				where m.IsStatic
				select m;
		}
	}
}
