using System;
using System.Web.Services.Protocols;

namespace System.Web.Services
{
	// Token: 0x02000013 RID: 19
	internal class WebServiceReflector
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000210F File Offset: 0x0000030F
		private WebServiceReflector()
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000281C File Offset: 0x00000A1C
		internal static WebServiceAttribute GetAttribute(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(WebServiceAttribute), false);
			if (customAttributes.Length == 0)
			{
				return new WebServiceAttribute();
			}
			return (WebServiceAttribute)customAttributes[0];
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000284D File Offset: 0x00000A4D
		internal static WebServiceAttribute GetAttribute(LogicalMethodInfo[] methodInfos)
		{
			if (methodInfos.Length == 0)
			{
				return new WebServiceAttribute();
			}
			return WebServiceReflector.GetAttribute(WebServiceReflector.GetMostDerivedType(methodInfos));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002864 File Offset: 0x00000A64
		internal static Type GetMostDerivedType(LogicalMethodInfo[] methodInfos)
		{
			if (methodInfos.Length == 0)
			{
				return null;
			}
			Type type = methodInfos[0].DeclaringType;
			for (int i = 1; i < methodInfos.Length; i++)
			{
				Type declaringType = methodInfos[i].DeclaringType;
				if (declaringType.IsSubclassOf(type))
				{
					type = declaringType;
				}
			}
			return type;
		}
	}
}
