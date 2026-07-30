using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000373 RID: 883
	public struct RenderStateBlock : IEquatable<RenderStateBlock>
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x00033A4B File Offset: 0x00031C4B
		public RenderStateBlock(RenderStateMask mask)
		{
			this.m_BlendState = BlendState.defaultValue;
			this.m_RasterState = RasterState.defaultValue;
			this.m_DepthState = DepthState.defaultValue;
			this.m_StencilState = StencilState.defaultValue;
			this.m_StencilReference = 0;
			this.m_Mask = mask;
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x00033A88 File Offset: 0x00031C88
		// (set) Token: 0x06001E60 RID: 7776 RVA: 0x00033AA0 File Offset: 0x00031CA0
		public BlendState blendState
		{
			get
			{
				return this.m_BlendState;
			}
			set
			{
				this.m_BlendState = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x00033AAC File Offset: 0x00031CAC
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x00033AC4 File Offset: 0x00031CC4
		public RasterState rasterState
		{
			get
			{
				return this.m_RasterState;
			}
			set
			{
				this.m_RasterState = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x00033AD0 File Offset: 0x00031CD0
		// (set) Token: 0x06001E64 RID: 7780 RVA: 0x00033AE8 File Offset: 0x00031CE8
		public DepthState depthState
		{
			get
			{
				return this.m_DepthState;
			}
			set
			{
				this.m_DepthState = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x00033AF4 File Offset: 0x00031CF4
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x00033B0C File Offset: 0x00031D0C
		public StencilState stencilState
		{
			get
			{
				return this.m_StencilState;
			}
			set
			{
				this.m_StencilState = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x00033B18 File Offset: 0x00031D18
		// (set) Token: 0x06001E68 RID: 7784 RVA: 0x00033B30 File Offset: 0x00031D30
		public int stencilReference
		{
			get
			{
				return this.m_StencilReference;
			}
			set
			{
				this.m_StencilReference = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x00033B3C File Offset: 0x00031D3C
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x00033B54 File Offset: 0x00031D54
		public RenderStateMask mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00033B60 File Offset: 0x00031D60
		public bool Equals(RenderStateBlock other)
		{
			return this.m_BlendState.Equals(other.m_BlendState) && this.m_RasterState.Equals(other.m_RasterState) && this.m_DepthState.Equals(other.m_DepthState) && this.m_StencilState.Equals(other.m_StencilState) && this.m_StencilReference == other.m_StencilReference && this.m_Mask == other.m_Mask;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x00033BE0 File Offset: 0x00031DE0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderStateBlock && this.Equals((RenderStateBlock)obj);
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00033C18 File Offset: 0x00031E18
		public override int GetHashCode()
		{
			int num = this.m_BlendState.GetHashCode();
			num = (num * 397) ^ this.m_RasterState.GetHashCode();
			num = (num * 397) ^ this.m_DepthState.GetHashCode();
			num = (num * 397) ^ this.m_StencilState.GetHashCode();
			num = (num * 397) ^ this.m_StencilReference;
			return (num * 397) ^ (int)this.m_Mask;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00033CAC File Offset: 0x00031EAC
		public static bool operator ==(RenderStateBlock left, RenderStateBlock right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00033CC8 File Offset: 0x00031EC8
		public static bool operator !=(RenderStateBlock left, RenderStateBlock right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000ADB RID: 2779
		private BlendState m_BlendState;

		// Token: 0x04000ADC RID: 2780
		private RasterState m_RasterState;

		// Token: 0x04000ADD RID: 2781
		private DepthState m_DepthState;

		// Token: 0x04000ADE RID: 2782
		private StencilState m_StencilState;

		// Token: 0x04000ADF RID: 2783
		private int m_StencilReference;

		// Token: 0x04000AE0 RID: 2784
		private RenderStateMask m_Mask;
	}
}
