using System;
using UnityEngine;
using UnityEngine.VFX;

namespace UnityEditor.VFX
{
	// Token: 0x02000027 RID: 39
	internal class SetSpawnTime : VFXSpawnerCallbacks
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00002091 File Offset: 0x00000291
		public sealed override void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004B21 File Offset: 0x00002D21
		public sealed override void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
		{
			state.vfxEventAttribute.SetFloat(SetSpawnTime.spawnTimeID, state.totalTime);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002091 File Offset: 0x00000291
		public sealed override void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
		{
		}

		// Token: 0x0400009A RID: 154
		private static readonly int spawnTimeID = Shader.PropertyToID("spawnTime");
	}
}
