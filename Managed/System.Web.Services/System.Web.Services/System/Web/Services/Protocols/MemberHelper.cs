using System;
using System.Reflection;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000051 RID: 81
	internal class MemberHelper
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000210F File Offset: 0x0000030F
		private MemberHelper()
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008C99 File Offset: 0x00006E99
		internal static void SetValue(MemberInfo memberInfo, object target, object value)
		{
			if (memberInfo is FieldInfo)
			{
				((FieldInfo)memberInfo).SetValue(target, value);
				return;
			}
			((PropertyInfo)memberInfo).SetValue(target, value, MemberHelper.emptyObjectArray);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008CC3 File Offset: 0x00006EC3
		internal static object GetValue(MemberInfo memberInfo, object target)
		{
			if (memberInfo is FieldInfo)
			{
				return ((FieldInfo)memberInfo).GetValue(target);
			}
			return ((PropertyInfo)memberInfo).GetValue(target, MemberHelper.emptyObjectArray);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008CEB File Offset: 0x00006EEB
		internal static bool IsStatic(MemberInfo memberInfo)
		{
			return memberInfo is FieldInfo && ((FieldInfo)memberInfo).IsStatic;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008D02 File Offset: 0x00006F02
		internal static bool CanRead(MemberInfo memberInfo)
		{
			return memberInfo is FieldInfo || ((PropertyInfo)memberInfo).CanRead;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008D19 File Offset: 0x00006F19
		internal static bool CanWrite(MemberInfo memberInfo)
		{
			return memberInfo is FieldInfo || ((PropertyInfo)memberInfo).CanWrite;
		}

		// Token: 0x0400022E RID: 558
		private static object[] emptyObjectArray = new object[0];
	}
}
