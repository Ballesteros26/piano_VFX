using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200037A RID: 890
	[UsedByNativeCode]
	public struct ShadowDrawingSettings : IEquatable<ShadowDrawingSettings>
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x000346CC File Offset: 0x000328CC
		// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x000346E4 File Offset: 0x000328E4
		public CullingResults cullingResults
		{
			get
			{
				return this.m_CullingResults;
			}
			set
			{
				this.m_CullingResults = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x000346F0 File Offset: 0x000328F0
		// (set) Token: 0x06001EE3 RID: 7907 RVA: 0x00034708 File Offset: 0x00032908
		public int lightIndex
		{
			get
			{
				return this.m_LightIndex;
			}
			set
			{
				this.m_LightIndex = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x00034714 File Offset: 0x00032914
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x0003472F File Offset: 0x0003292F
		public bool useRenderingLayerMaskTest
		{
			get
			{
				return this.m_UseRenderingLayerMaskTest != 0;
			}
			set
			{
				this.m_UseRenderingLayerMaskTest = (value ? 1 : 0);
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x00034740 File Offset: 0x00032940
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x00034758 File Offset: 0x00032958
		public ShadowSplitData splitData
		{
			get
			{
				return this.m_SplitData;
			}
			set
			{
				this.m_SplitData = value;
			}
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00034762 File Offset: 0x00032962
		public ShadowDrawingSettings(CullingResults cullingResults, int lightIndex)
		{
			this.m_CullingResults = cullingResults;
			this.m_LightIndex = lightIndex;
			this.m_UseRenderingLayerMaskTest = 0;
			this.m_SplitData = default(ShadowSplitData);
			this.m_SplitData.shadowCascadeBlendCullingFactor = 1f;
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00034798 File Offset: 0x00032998
		public bool Equals(ShadowDrawingSettings other)
		{
			return this.m_CullingResults.Equals(other.m_CullingResults) && this.m_LightIndex == other.m_LightIndex && this.m_SplitData.Equals(other.m_SplitData) && this.m_UseRenderingLayerMaskTest.Equals(other.m_UseRenderingLayerMaskTest);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x000347F4 File Offset: 0x000329F4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowDrawingSettings && this.Equals((ShadowDrawingSettings)obj);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0003482C File Offset: 0x00032A2C
		public override int GetHashCode()
		{
			int num = this.m_CullingResults.GetHashCode();
			num = (num * 397) ^ this.m_LightIndex;
			num = (num * 397) ^ this.m_UseRenderingLayerMaskTest;
			return (num * 397) ^ this.m_SplitData.GetHashCode();
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0003488C File Offset: 0x00032A8C
		public static bool operator ==(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000348A8 File Offset: 0x00032AA8
		public static bool operator !=(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000AF5 RID: 2805
		private CullingResults m_CullingResults;

		// Token: 0x04000AF6 RID: 2806
		private int m_LightIndex;

		// Token: 0x04000AF7 RID: 2807
		private int m_UseRenderingLayerMaskTest;

		// Token: 0x04000AF8 RID: 2808
		private ShadowSplitData m_SplitData;
	}
}
