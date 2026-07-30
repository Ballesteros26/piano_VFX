using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Xml
{
	// Token: 0x0200008C RID: 140
	internal static class BinaryCompatibility
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00016314 File Offset: 0x00014514
		internal static bool TargetsAtLeast_Desktop_V4_5_2
		{
			get
			{
				return BinaryCompatibility._targetsAtLeast_Desktop_V4_5_2;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001631C File Offset: 0x0001451C
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool RunningOnCheck(string propertyName)
		{
			Type type;
			try
			{
				type = typeof(object).GetTypeInfo().Assembly.GetType("System.Runtime.Versioning.BinaryCompatibility", false);
			}
			catch (TypeLoadException)
			{
				return false;
			}
			if (type == null)
			{
				return false;
			}
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			return !(property == null) && (bool)property.GetValue(null);
		}

		// Token: 0x04000304 RID: 772
		private static bool _targetsAtLeast_Desktop_V4_5_2 = BinaryCompatibility.RunningOnCheck("TargetsAtLeast_Desktop_V4_5_2");
	}
}
