using System;
using UnityEngine;
using UnityEngine.VFX;

// Token: 0x02000002 RID: 2
internal class IncrementStripIndexOnStart : VFXSpawnerCallbacks
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public override void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
	{
		this.m_Index = (this.m_Index + 1U) % Math.Max(1U, vfxValues.GetUInt(IncrementStripIndexOnStart.stripMaxCountID));
		state.vfxEventAttribute.SetUint(IncrementStripIndexOnStart.stripIndexID, this.m_Index);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
	public override void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
	{
		this.m_Index = 0U;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002091 File Offset: 0x00000291
	public override void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
	{
	}

	// Token: 0x04000001 RID: 1
	private static readonly int stripMaxCountID = Shader.PropertyToID("StripMaxCount");

	// Token: 0x04000002 RID: 2
	private static readonly int stripIndexID = Shader.PropertyToID("stripIndex");

	// Token: 0x04000003 RID: 3
	private uint m_Index;

	// Token: 0x02000029 RID: 41
	public class InputProperties
	{
		// Token: 0x040000A3 RID: 163
		[Tooltip("Maximum Strip Count (Used to cycle indices)")]
		public uint StripMaxCount = 8U;
	}
}
