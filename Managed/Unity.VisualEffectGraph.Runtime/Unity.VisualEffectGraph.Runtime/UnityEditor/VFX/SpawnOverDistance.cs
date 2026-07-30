using System;
using UnityEngine;
using UnityEngine.VFX;

namespace UnityEditor.VFX
{
	// Token: 0x02000028 RID: 40
	internal class SpawnOverDistance : VFXSpawnerCallbacks
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004B4A File Offset: 0x00002D4A
		public sealed override void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
		{
			this.m_OldPosition = vfxValues.GetVector3(SpawnOverDistance.positionPropertyId);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004B60 File Offset: 0x00002D60
		public sealed override void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
		{
			this.cachedSqrThreshold = vfxValues.GetFloat(SpawnOverDistance.velocityThresholdPropertyId);
			this.cachedSqrThreshold *= this.cachedSqrThreshold;
			this.cachedRatePerSqrUnit = vfxValues.GetFloat(SpawnOverDistance.ratePerUnitPropertyId);
			this.cachedRatePerSqrUnit *= this.cachedRatePerSqrUnit;
			if (!state.playing || state.deltaTime == 0f)
			{
				return;
			}
			Vector3 vector = vfxValues.GetVector3(SpawnOverDistance.positionPropertyId);
			float num = Vector3.SqrMagnitude(this.m_OldPosition - vector);
			if (num < this.cachedSqrThreshold * state.deltaTime)
			{
				state.spawnCount += num * this.cachedRatePerSqrUnit;
				state.vfxEventAttribute.SetVector3(SpawnOverDistance.oldPositionAttributeId, this.m_OldPosition);
				state.vfxEventAttribute.SetVector3(SpawnOverDistance.positionAttributeId, vector);
			}
			this.m_OldPosition = vector;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00002091 File Offset: 0x00000291
		public sealed override void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
		{
		}

		// Token: 0x0400009B RID: 155
		private Vector3 m_OldPosition;

		// Token: 0x0400009C RID: 156
		private static readonly int positionPropertyId = Shader.PropertyToID("Position");

		// Token: 0x0400009D RID: 157
		private static readonly int ratePerUnitPropertyId = Shader.PropertyToID("RatePerUnit");

		// Token: 0x0400009E RID: 158
		private static readonly int velocityThresholdPropertyId = Shader.PropertyToID("VelocityThreshold");

		// Token: 0x0400009F RID: 159
		private static readonly int positionAttributeId = Shader.PropertyToID("position");

		// Token: 0x040000A0 RID: 160
		private static readonly int oldPositionAttributeId = Shader.PropertyToID("oldPosition");

		// Token: 0x040000A1 RID: 161
		private float cachedSqrThreshold;

		// Token: 0x040000A2 RID: 162
		private float cachedRatePerSqrUnit;

		// Token: 0x02000036 RID: 54
		public class InputProperties
		{
			// Token: 0x040000D5 RID: 213
			public Vector3 Position = Vector3.zero;

			// Token: 0x040000D6 RID: 214
			public float RatePerUnit = 10f;

			// Token: 0x040000D7 RID: 215
			public float VelocityThreshold = 50f;
		}
	}
}
