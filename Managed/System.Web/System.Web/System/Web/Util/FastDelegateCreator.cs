using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x02000119 RID: 281
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	internal static class FastDelegateCreator<TDelegate> where TDelegate : class
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x000262DF File Offset: 0x000244DF
		internal static TDelegate BindTo(object obj, IntPtr method)
		{
			return FastDelegateCreator<TDelegate>._factory(obj, method);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000262F0 File Offset: 0x000244F0
		internal static TDelegate BindTo(object obj, MethodInfo method)
		{
			return FastDelegateCreator<TDelegate>.BindTo(obj, method.MethodHandle.GetFunctionPointer());
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00026314 File Offset: 0x00024514
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static Func<object, IntPtr, TDelegate> GetFactory()
		{
			ConstructorInfo constructor = typeof(TDelegate).GetConstructor(new Type[]
			{
				typeof(object),
				typeof(IntPtr)
			});
			DynamicMethod dynamicMethod = new DynamicMethod("FastCreateDelegate_" + typeof(TDelegate).Name, typeof(TDelegate), new Type[]
			{
				typeof(object),
				typeof(IntPtr)
			}, typeof(FastDelegateCreator<TDelegate>), true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Newobj, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return (Func<object, IntPtr, TDelegate>)dynamicMethod.CreateDelegate(typeof(Func<object, IntPtr, TDelegate>));
		}

		// Token: 0x040011B2 RID: 4530
		private static readonly Func<object, IntPtr, TDelegate> _factory = FastDelegateCreator<TDelegate>.GetFactory();
	}
}
