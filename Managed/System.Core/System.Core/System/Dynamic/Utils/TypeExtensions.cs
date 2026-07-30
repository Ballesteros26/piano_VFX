using System;
using System.Reflection;

namespace System.Dynamic.Utils
{
	// Token: 0x02000345 RID: 837
	internal static class TypeExtensions
	{
		// Token: 0x06001951 RID: 6481 RVA: 0x00053130 File Offset: 0x00051330
		public static MethodInfo GetAnyStaticMethodValidated(this Type type, string name, Type[] types)
		{
			MethodInfo method = type.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
			if (!method.MatchesArgumentTypes(types))
			{
				return null;
			}
			return method;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00053158 File Offset: 0x00051358
		private static bool MatchesArgumentTypes(this MethodInfo mi, Type[] argTypes)
		{
			if (mi == null)
			{
				return false;
			}
			ParameterInfo[] parametersCached = mi.GetParametersCached();
			if (parametersCached.Length != argTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < parametersCached.Length; i++)
			{
				if (!TypeUtils.AreReferenceAssignable(parametersCached[i].ParameterType, argTypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x000531A4 File Offset: 0x000513A4
		public static Type GetReturnType(this MethodBase mi)
		{
			if (!mi.IsConstructor)
			{
				return ((MethodInfo)mi).ReturnType;
			}
			return mi.DeclaringType;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000531C0 File Offset: 0x000513C0
		public static TypeCode GetTypeCode(this Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000531C8 File Offset: 0x000513C8
		internal static ParameterInfo[] GetParametersCached(this MethodBase method)
		{
			CacheDict<MethodBase, ParameterInfo[]> cacheDict = TypeExtensions.s_paramInfoCache;
			ParameterInfo[] parameters;
			if (!cacheDict.TryGetValue(method, out parameters))
			{
				parameters = method.GetParameters();
				Type declaringType = method.DeclaringType;
				if (declaringType != null && declaringType.CanCache())
				{
					cacheDict[method] = parameters;
				}
			}
			return parameters;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0005320E File Offset: 0x0005140E
		internal static bool IsByRefParameter(this ParameterInfo pi)
		{
			return pi.ParameterType.IsByRef || (pi.Attributes & ParameterAttributes.Out) == ParameterAttributes.Out;
		}

		// Token: 0x04000B54 RID: 2900
		private static readonly CacheDict<MethodBase, ParameterInfo[]> s_paramInfoCache = new CacheDict<MethodBase, ParameterInfo[]>(75);
	}
}
