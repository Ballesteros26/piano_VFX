using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200017D RID: 381
	[NativeHeader("Editor/Src/Properties/DrivenPropertyManager.h")]
	internal class DrivenPropertyManager
	{
		// Token: 0x06001281 RID: 4737 RVA: 0x0001E761 File Offset: 0x0001C961
		[Conditional("UNITY_EDITOR")]
		public static void RegisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.RegisterPropertyPartial(driver, target, propertyPath);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0001E76D File Offset: 0x0001C96D
		[Conditional("UNITY_EDITOR")]
		public static void TryRegisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.TryRegisterPropertyPartial(driver, target, propertyPath);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0001E779 File Offset: 0x0001C979
		[Conditional("UNITY_EDITOR")]
		public static void UnregisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.UnregisterPropertyPartial(driver, target, propertyPath);
		}

		// Token: 0x06001284 RID: 4740
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[Conditional("UNITY_EDITOR")]
		[MethodImpl(4096)]
		public static extern void UnregisterProperties([NotNull] Object driver);

		// Token: 0x06001285 RID: 4741
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(4096)]
		private static extern void RegisterPropertyPartial([NotNull] Object driver, [NotNull] Object target, [NotNull] string propertyPath);

		// Token: 0x06001286 RID: 4742
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(4096)]
		private static extern void TryRegisterPropertyPartial([NotNull] Object driver, [NotNull] Object target, [NotNull] string propertyPath);

		// Token: 0x06001287 RID: 4743
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern void UnregisterPropertyPartial([NotNull] Object driver, [NotNull] Object target, [NotNull] string propertyPath);
	}
}
