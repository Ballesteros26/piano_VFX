using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
	// Token: 0x02000029 RID: 41
	public static class PhysicsSceneExtensions
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00003DDC File Offset: 0x00001FDC
		public static PhysicsScene GetPhysicsScene(this Scene scene)
		{
			bool flag = !scene.IsValid();
			if (flag)
			{
				throw new ArgumentException("Cannot get physics scene; Unity scene is invalid.", "scene");
			}
			PhysicsScene physicsScene_Internal = PhysicsSceneExtensions.GetPhysicsScene_Internal(scene);
			bool flag2 = physicsScene_Internal.IsValid();
			if (flag2)
			{
				return physicsScene_Internal;
			}
			throw new Exception("The physics scene associated with the Unity scene is invalid.");
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00003E2C File Offset: 0x0000202C
		[NativeMethod("GetPhysicsSceneFromUnityScene")]
		[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		private static PhysicsScene GetPhysicsScene_Internal(Scene scene)
		{
			PhysicsScene physicsScene;
			PhysicsSceneExtensions.GetPhysicsScene_Internal_Injected(ref scene, out physicsScene);
			return physicsScene;
		}

		// Token: 0x06000271 RID: 625
		[MethodImpl(4096)]
		private static extern void GetPhysicsScene_Internal_Injected(ref Scene scene, out PhysicsScene ret);
	}
}
