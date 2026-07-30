using System;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000E RID: 14
	[RequiredByNativeCode]
	[Serializable]
	public abstract class VFXSpawnerCallbacks : ScriptableObject
	{
		// Token: 0x06000071 RID: 113
		public abstract void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		// Token: 0x06000072 RID: 114
		public abstract void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		// Token: 0x06000073 RID: 115
		public abstract void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);
	}
}
