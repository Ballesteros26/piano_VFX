using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x02000277 RID: 631
	[NativeHeader("Runtime/Export/SceneManager/SceneUtility.bindings.h")]
	public static class SceneUtility
	{
		// Token: 0x06001A5A RID: 6746
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		public static extern string GetScenePathByBuildIndex(int buildIndex);

		// Token: 0x06001A5B RID: 6747
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		public static extern int GetBuildIndexByScenePath(string scenePath);
	}
}
