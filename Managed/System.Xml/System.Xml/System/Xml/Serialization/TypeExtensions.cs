using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200031A RID: 794
	internal static class TypeExtensions
	{
		// Token: 0x06001DD3 RID: 7635 RVA: 0x000A4410 File Offset: 0x000A2610
		public static bool TryConvertTo(this Type targetType, object data, out object returnValue)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			returnValue = null;
			if (data == null)
			{
				return !targetType.IsValueType;
			}
			Type type = data.GetType();
			if (targetType == type || targetType.IsAssignableFrom(type))
			{
				returnValue = data;
				return true;
			}
			foreach (MethodInfo methodInfo in targetType.GetMethods(BindingFlags.Static | BindingFlags.Public))
			{
				if (methodInfo.Name == "op_Implicit" && methodInfo.ReturnType != null && targetType.IsAssignableFrom(methodInfo.ReturnType))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters != null && parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(type))
					{
						returnValue = methodInfo.Invoke(null, new object[] { data });
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040016B7 RID: 5815
		private const string ImplicitCastOperatorName = "op_Implicit";
	}
}
