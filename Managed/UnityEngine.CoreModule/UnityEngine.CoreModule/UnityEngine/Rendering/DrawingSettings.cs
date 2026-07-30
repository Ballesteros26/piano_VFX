using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000366 RID: 870
	public struct DrawingSettings : IEquatable<DrawingSettings>
	{
		// Token: 0x06001DD3 RID: 7635 RVA: 0x000327B8 File Offset: 0x000309B8
		public unsafe DrawingSettings(ShaderTagId shaderPassName, SortingSettings sortingSettings)
		{
			this.m_SortingSettings = sortingSettings;
			this.m_PerObjectData = PerObjectData.None;
			this.m_Flags = DrawRendererFlags.EnableInstancing;
			this.m_OverrideMaterialInstanceId = 0;
			this.m_OverrideMaterialPassIndex = 0;
			this.m_MainLightIndex = -1;
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				*ptr2 = shaderPassName.id;
				for (int i = 1; i < DrawingSettings.maxShaderPasses; i++)
				{
					ptr2[i] = -1;
				}
			}
			this.m_PerObjectData = PerObjectData.None;
			this.m_Flags = DrawRendererFlags.EnableInstancing;
			this.m_UseSrpBatcher = 0;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x00032840 File Offset: 0x00030A40
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x00032858 File Offset: 0x00030A58
		public SortingSettings sortingSettings
		{
			get
			{
				return this.m_SortingSettings;
			}
			set
			{
				this.m_SortingSettings = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x00032864 File Offset: 0x00030A64
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x0003287C File Offset: 0x00030A7C
		public PerObjectData perObjectData
		{
			get
			{
				return this.m_PerObjectData;
			}
			set
			{
				this.m_PerObjectData = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x00032888 File Offset: 0x00030A88
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x000328A8 File Offset: 0x00030AA8
		public bool enableDynamicBatching
		{
			get
			{
				return (this.m_Flags & DrawRendererFlags.EnableDynamicBatching) > DrawRendererFlags.None;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableDynamicBatching;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableDynamicBatching;
				}
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x000328DC File Offset: 0x00030ADC
		// (set) Token: 0x06001DDB RID: 7643 RVA: 0x000328FC File Offset: 0x00030AFC
		public bool enableInstancing
		{
			get
			{
				return (this.m_Flags & DrawRendererFlags.EnableInstancing) > DrawRendererFlags.None;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableInstancing;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableInstancing;
				}
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00032930 File Offset: 0x00030B30
		// (set) Token: 0x06001DDD RID: 7645 RVA: 0x0003295D File Offset: 0x00030B5D
		public Material overrideMaterial
		{
			get
			{
				return (this.m_OverrideMaterialInstanceId != 0) ? (Object.FindObjectFromInstanceID(this.m_OverrideMaterialInstanceId) as Material) : null;
			}
			set
			{
				this.m_OverrideMaterialInstanceId = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x00032974 File Offset: 0x00030B74
		// (set) Token: 0x06001DDF RID: 7647 RVA: 0x0003298C File Offset: 0x00030B8C
		public int overrideMaterialPassIndex
		{
			get
			{
				return this.m_OverrideMaterialPassIndex;
			}
			set
			{
				this.m_OverrideMaterialPassIndex = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x00032998 File Offset: 0x00030B98
		// (set) Token: 0x06001DE1 RID: 7649 RVA: 0x000329B0 File Offset: 0x00030BB0
		public int mainLightIndex
		{
			get
			{
				return this.m_MainLightIndex;
			}
			set
			{
				this.m_MainLightIndex = value;
			}
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000329BC File Offset: 0x00030BBC
		public unsafe ShaderTagId GetShaderPassName(int index)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				return new ShaderTagId
				{
					id = ptr2[index]
				};
			}
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00032A30 File Offset: 0x00030C30
		public unsafe void SetShaderPassName(int index, ShaderTagId shaderPassName)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				ptr2[index] = shaderPassName.id;
			}
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x00032A98 File Offset: 0x00030C98
		public bool Equals(DrawingSettings other)
		{
			for (int i = 0; i < DrawingSettings.maxShaderPasses; i++)
			{
				bool flag = !this.GetShaderPassName(i).Equals(other.GetShaderPassName(i));
				if (flag)
				{
					return false;
				}
			}
			return this.m_SortingSettings.Equals(other.m_SortingSettings) && this.m_PerObjectData == other.m_PerObjectData && this.m_Flags == other.m_Flags && this.m_OverrideMaterialInstanceId == other.m_OverrideMaterialInstanceId && this.m_OverrideMaterialPassIndex == other.m_OverrideMaterialPassIndex && this.m_UseSrpBatcher == other.m_UseSrpBatcher;
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00032B40 File Offset: 0x00030D40
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DrawingSettings && this.Equals((DrawingSettings)obj);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00032B78 File Offset: 0x00030D78
		public override int GetHashCode()
		{
			int num = this.m_SortingSettings.GetHashCode();
			num = (num * 397) ^ (int)this.m_PerObjectData;
			num = (num * 397) ^ (int)this.m_Flags;
			num = (num * 397) ^ this.m_OverrideMaterialInstanceId;
			num = (num * 397) ^ this.m_OverrideMaterialPassIndex;
			return (num * 397) ^ this.m_UseSrpBatcher;
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00032BEC File Offset: 0x00030DEC
		public static bool operator ==(DrawingSettings left, DrawingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00032C08 File Offset: 0x00030E08
		public static bool operator !=(DrawingSettings left, DrawingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000A96 RID: 2710
		private const int kMaxShaderPasses = 16;

		// Token: 0x04000A97 RID: 2711
		public static readonly int maxShaderPasses = 16;

		// Token: 0x04000A98 RID: 2712
		private SortingSettings m_SortingSettings;

		// Token: 0x04000A99 RID: 2713
		[FixedBuffer(typeof(int), 16)]
		internal DrawingSettings.<shaderPassNames>e__FixedBuffer shaderPassNames;

		// Token: 0x04000A9A RID: 2714
		private PerObjectData m_PerObjectData;

		// Token: 0x04000A9B RID: 2715
		private DrawRendererFlags m_Flags;

		// Token: 0x04000A9C RID: 2716
		private int m_OverrideMaterialInstanceId;

		// Token: 0x04000A9D RID: 2717
		private int m_OverrideMaterialPassIndex;

		// Token: 0x04000A9E RID: 2718
		private int m_MainLightIndex;

		// Token: 0x04000A9F RID: 2719
		private int m_UseSrpBatcher;

		// Token: 0x02000367 RID: 871
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 64)]
		public struct <shaderPassNames>e__FixedBuffer
		{
			// Token: 0x04000AA0 RID: 2720
			public int FixedElementField;
		}
	}
}
