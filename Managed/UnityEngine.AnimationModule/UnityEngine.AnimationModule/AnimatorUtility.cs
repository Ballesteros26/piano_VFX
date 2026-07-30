using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Animation/OptimizeTransformHierarchy.h")]
	public class AnimatorUtility
	{
		// Token: 0x060001FD RID: 509
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void OptimizeTransformHierarchy(GameObject go, string[] exposedTransforms);

		// Token: 0x060001FE RID: 510
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void DeoptimizeTransformHierarchy(GameObject go);
	}
}
