using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System
{
	// Token: 0x020000F0 RID: 240
	internal static class SecurityUtils
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001AB43 File Offset: 0x00018D43
		private static ReflectionPermission MemberAccessPermission
		{
			get
			{
				if (SecurityUtils.memberAccessPermission == null)
				{
					SecurityUtils.memberAccessPermission = new ReflectionPermission(ReflectionPermissionFlag.MemberAccess);
				}
				return SecurityUtils.memberAccessPermission;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001AB62 File Offset: 0x00018D62
		private static ReflectionPermission RestrictedMemberAccessPermission
		{
			get
			{
				if (SecurityUtils.restrictedMemberAccessPermission == null)
				{
					SecurityUtils.restrictedMemberAccessPermission = new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess);
				}
				return SecurityUtils.restrictedMemberAccessPermission;
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000027E8 File Offset: 0x000009E8
		private static void DemandReflectionAccess(Type type)
		{
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000027E8 File Offset: 0x000009E8
		[SecuritySafeCritical]
		private static void DemandGrantSet(Assembly assembly)
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001AB84 File Offset: 0x00018D84
		private static bool HasReflectionPermission(Type type)
		{
			try
			{
				SecurityUtils.DemandReflectionAccess(type);
				return true;
			}
			catch (SecurityException)
			{
			}
			return false;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001ABB4 File Offset: 0x00018DB4
		internal static object SecureCreateInstance(Type type)
		{
			return SecurityUtils.SecureCreateInstance(type, null, false);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		internal static object SecureCreateInstance(Type type, object[] args, bool allowNonPublic)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			if (!type.IsVisible)
			{
				SecurityUtils.DemandReflectionAccess(type);
			}
			else if (allowNonPublic && !SecurityUtils.HasReflectionPermission(type))
			{
				allowNonPublic = false;
			}
			if (allowNonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return Activator.CreateInstance(type, bindingFlags, null, args, null);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001AC17 File Offset: 0x00018E17
		internal static object SecureCreateInstance(Type type, object[] args)
		{
			return SecurityUtils.SecureCreateInstance(type, args, false);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001AC21 File Offset: 0x00018E21
		internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic)
		{
			return SecurityUtils.SecureConstructorInvoke(type, argTypes, args, allowNonPublic, BindingFlags.Default);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001AC30 File Offset: 0x00018E30
		internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic, BindingFlags extraFlags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsVisible)
			{
				SecurityUtils.DemandReflectionAccess(type);
			}
			else if (allowNonPublic && !SecurityUtils.HasReflectionPermission(type))
			{
				allowNonPublic = false;
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | extraFlags;
			if (!allowNonPublic)
			{
				bindingFlags &= ~BindingFlags.NonPublic;
			}
			ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, argTypes, null);
			if (constructor != null)
			{
				return constructor.Invoke(args);
			}
			return null;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001AC9C File Offset: 0x00018E9C
		private static bool GenericArgumentsAreVisible(MethodInfo method)
		{
			if (method.IsGenericMethod)
			{
				Type[] genericArguments = method.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (!genericArguments[i].IsVisible)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		internal static object FieldInfoGetValue(FieldInfo field, object target)
		{
			Type declaringType = field.DeclaringType;
			if (declaringType == null)
			{
				if (!field.IsPublic)
				{
					SecurityUtils.DemandGrantSet(field.Module.Assembly);
				}
			}
			else if (!(declaringType != null) || !declaringType.IsVisible || !field.IsPublic)
			{
				SecurityUtils.DemandReflectionAccess(declaringType);
			}
			return field.GetValue(target);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001AD34 File Offset: 0x00018F34
		internal static object MethodInfoInvoke(MethodInfo method, object target, object[] args)
		{
			Type declaringType = method.DeclaringType;
			if (declaringType == null)
			{
				if (!method.IsPublic || !SecurityUtils.GenericArgumentsAreVisible(method))
				{
					SecurityUtils.DemandGrantSet(method.Module.Assembly);
				}
			}
			else if (!declaringType.IsVisible || !method.IsPublic || !SecurityUtils.GenericArgumentsAreVisible(method))
			{
				SecurityUtils.DemandReflectionAccess(declaringType);
			}
			return method.Invoke(target, args);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001AD9C File Offset: 0x00018F9C
		internal static object ConstructorInfoInvoke(ConstructorInfo ctor, object[] args)
		{
			Type declaringType = ctor.DeclaringType;
			if (declaringType != null && (!declaringType.IsVisible || !ctor.IsPublic))
			{
				SecurityUtils.DemandReflectionAccess(declaringType);
			}
			return ctor.Invoke(args);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001ADD6 File Offset: 0x00018FD6
		internal static object ArrayCreateInstance(Type type, int length)
		{
			if (!type.IsVisible)
			{
				SecurityUtils.DemandReflectionAccess(type);
			}
			return Array.CreateInstance(type, length);
		}

		// Token: 0x04000C01 RID: 3073
		private static volatile ReflectionPermission memberAccessPermission;

		// Token: 0x04000C02 RID: 3074
		private static volatile ReflectionPermission restrictedMemberAccessPermission;
	}
}
