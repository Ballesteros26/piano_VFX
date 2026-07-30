using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000332 RID: 818
	internal struct MonoPropertyInfo
	{
		// Token: 0x0600241A RID: 9242
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void get_property_info(MonoProperty prop, ref MonoPropertyInfo info, PInfo req_info);

		// Token: 0x0600241B RID: 9243
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] GetTypeModifiers(MonoProperty prop, bool optional);

		// Token: 0x0600241C RID: 9244
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object get_default_value(MonoProperty prop);

		// Token: 0x04001352 RID: 4946
		public Type parent;

		// Token: 0x04001353 RID: 4947
		public Type declaring_type;

		// Token: 0x04001354 RID: 4948
		public string name;

		// Token: 0x04001355 RID: 4949
		public MethodInfo get_method;

		// Token: 0x04001356 RID: 4950
		public MethodInfo set_method;

		// Token: 0x04001357 RID: 4951
		public PropertyAttributes attrs;
	}
}
