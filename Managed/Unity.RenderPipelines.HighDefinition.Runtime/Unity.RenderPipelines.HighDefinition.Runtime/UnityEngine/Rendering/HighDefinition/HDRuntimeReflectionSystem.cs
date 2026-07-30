using System;
using System.Reflection;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200007D RID: 125
	internal class HDRuntimeReflectionSystem : ScriptableRuntimeReflectionSystem
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x0002C31C File Offset: 0x0002A51C
		[RuntimeInitializeOnLoadMethod]
		private static void Initialize()
		{
			if (GraphicsSettings.currentRenderPipeline is HDRenderPipelineAsset)
			{
				ScriptableRuntimeReflectionSystemSettings.system = HDRuntimeReflectionSystem.k_instance;
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0002C334 File Offset: 0x0002A534
		public override bool TickRealtimeProbes()
		{
			HDRuntimeReflectionSystem.BuiltinUpdate.Invoke(null, new object[0]);
			return base.TickRealtimeProbes();
		}

		// Token: 0x04000531 RID: 1329
		private static MethodInfo BuiltinUpdate = Type.GetType("UnityEngine.Experimental.Rendering.BuiltinRuntimeReflectionSystem,UnityEngine").GetMethod("BuiltinUpdate", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000532 RID: 1330
		private static HDRuntimeReflectionSystem k_instance = new HDRuntimeReflectionSystem();
	}
}
