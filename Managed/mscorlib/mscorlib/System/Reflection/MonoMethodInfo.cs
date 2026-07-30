using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000329 RID: 809
	internal struct MonoMethodInfo
	{
		// Token: 0x06002389 RID: 9097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_method_info(IntPtr handle, out MonoMethodInfo info);

		// Token: 0x0600238A RID: 9098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_method_attributes(IntPtr handle);

		// Token: 0x0600238B RID: 9099 RVA: 0x00082804 File Offset: 0x00080A04
		internal static MonoMethodInfo GetMethodInfo(IntPtr handle)
		{
			MonoMethodInfo monoMethodInfo;
			MonoMethodInfo.get_method_info(handle, out monoMethodInfo);
			return monoMethodInfo;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0008281A File Offset: 0x00080A1A
		internal static Type GetDeclaringType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).parent;
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00082827 File Offset: 0x00080A27
		internal static Type GetReturnType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).ret;
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00082834 File Offset: 0x00080A34
		internal static MethodAttributes GetAttributes(IntPtr handle)
		{
			return (MethodAttributes)MonoMethodInfo.get_method_attributes(handle);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0008283C File Offset: 0x00080A3C
		internal static CallingConventions GetCallingConvention(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).callconv;
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x00082849 File Offset: 0x00080A49
		internal static MethodImplAttributes GetMethodImplementationFlags(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).iattrs;
		}

		// Token: 0x06002391 RID: 9105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParameterInfo[] get_parameter_info(IntPtr handle, MemberInfo member);

		// Token: 0x06002392 RID: 9106 RVA: 0x00082856 File Offset: 0x00080A56
		internal static ParameterInfo[] GetParametersInfo(IntPtr handle, MemberInfo member)
		{
			return MonoMethodInfo.get_parameter_info(handle, member);
		}

		// Token: 0x06002393 RID: 9107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MarshalAsAttribute get_retval_marshal(IntPtr handle);

		// Token: 0x06002394 RID: 9108 RVA: 0x0008285F File Offset: 0x00080A5F
		internal static ParameterInfo GetReturnParameterInfo(MonoMethod method)
		{
			return ParameterInfo.New(MonoMethodInfo.GetReturnType(method.mhandle), method, MonoMethodInfo.get_retval_marshal(method.mhandle));
		}

		// Token: 0x04001347 RID: 4935
		private Type parent;

		// Token: 0x04001348 RID: 4936
		private Type ret;

		// Token: 0x04001349 RID: 4937
		internal MethodAttributes attrs;

		// Token: 0x0400134A RID: 4938
		internal MethodImplAttributes iattrs;

		// Token: 0x0400134B RID: 4939
		private CallingConventions callconv;
	}
}
