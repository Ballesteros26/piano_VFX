using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000375 RID: 885
	public struct RenderTargetBlendState : IEquatable<RenderTargetBlendState>
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x00033CE8 File Offset: 0x00031EE8
		public static RenderTargetBlendState defaultValue
		{
			get
			{
				return new RenderTargetBlendState(ColorWriteMask.All, BlendMode.One, BlendMode.Zero, BlendMode.One, BlendMode.Zero, BlendOp.Add, BlendOp.Add);
			}
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00033D08 File Offset: 0x00031F08
		public RenderTargetBlendState(ColorWriteMask writeMask = ColorWriteMask.All, BlendMode sourceColorBlendMode = BlendMode.One, BlendMode destinationColorBlendMode = BlendMode.Zero, BlendMode sourceAlphaBlendMode = BlendMode.One, BlendMode destinationAlphaBlendMode = BlendMode.Zero, BlendOp colorBlendOperation = BlendOp.Add, BlendOp alphaBlendOperation = BlendOp.Add)
		{
			this.m_WriteMask = (byte)writeMask;
			this.m_SourceColorBlendMode = (byte)sourceColorBlendMode;
			this.m_DestinationColorBlendMode = (byte)destinationColorBlendMode;
			this.m_SourceAlphaBlendMode = (byte)sourceAlphaBlendMode;
			this.m_DestinationAlphaBlendMode = (byte)destinationAlphaBlendMode;
			this.m_ColorBlendOperation = (byte)colorBlendOperation;
			this.m_AlphaBlendOperation = (byte)alphaBlendOperation;
			this.m_Padding = 0;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00033D5C File Offset: 0x00031F5C
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x00033D74 File Offset: 0x00031F74
		public ColorWriteMask writeMask
		{
			get
			{
				return (ColorWriteMask)this.m_WriteMask;
			}
			set
			{
				this.m_WriteMask = (byte)value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00033D80 File Offset: 0x00031F80
		// (set) Token: 0x06001E75 RID: 7797 RVA: 0x00033D98 File Offset: 0x00031F98
		public BlendMode sourceColorBlendMode
		{
			get
			{
				return (BlendMode)this.m_SourceColorBlendMode;
			}
			set
			{
				this.m_SourceColorBlendMode = (byte)value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x00033DA4 File Offset: 0x00031FA4
		// (set) Token: 0x06001E77 RID: 7799 RVA: 0x00033DBC File Offset: 0x00031FBC
		public BlendMode destinationColorBlendMode
		{
			get
			{
				return (BlendMode)this.m_DestinationColorBlendMode;
			}
			set
			{
				this.m_DestinationColorBlendMode = (byte)value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x00033DC8 File Offset: 0x00031FC8
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x00033DE0 File Offset: 0x00031FE0
		public BlendMode sourceAlphaBlendMode
		{
			get
			{
				return (BlendMode)this.m_SourceAlphaBlendMode;
			}
			set
			{
				this.m_SourceAlphaBlendMode = (byte)value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x00033DEC File Offset: 0x00031FEC
		// (set) Token: 0x06001E7B RID: 7803 RVA: 0x00033E04 File Offset: 0x00032004
		public BlendMode destinationAlphaBlendMode
		{
			get
			{
				return (BlendMode)this.m_DestinationAlphaBlendMode;
			}
			set
			{
				this.m_DestinationAlphaBlendMode = (byte)value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00033E10 File Offset: 0x00032010
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x00033E28 File Offset: 0x00032028
		public BlendOp colorBlendOperation
		{
			get
			{
				return (BlendOp)this.m_ColorBlendOperation;
			}
			set
			{
				this.m_ColorBlendOperation = (byte)value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00033E34 File Offset: 0x00032034
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x00033E4C File Offset: 0x0003204C
		public BlendOp alphaBlendOperation
		{
			get
			{
				return (BlendOp)this.m_AlphaBlendOperation;
			}
			set
			{
				this.m_AlphaBlendOperation = (byte)value;
			}
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00033E58 File Offset: 0x00032058
		public bool Equals(RenderTargetBlendState other)
		{
			return this.m_WriteMask == other.m_WriteMask && this.m_SourceColorBlendMode == other.m_SourceColorBlendMode && this.m_DestinationColorBlendMode == other.m_DestinationColorBlendMode && this.m_SourceAlphaBlendMode == other.m_SourceAlphaBlendMode && this.m_DestinationAlphaBlendMode == other.m_DestinationAlphaBlendMode && this.m_ColorBlendOperation == other.m_ColorBlendOperation && this.m_AlphaBlendOperation == other.m_AlphaBlendOperation;
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00033ED0 File Offset: 0x000320D0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderTargetBlendState && this.Equals((RenderTargetBlendState)obj);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00033F08 File Offset: 0x00032108
		public override int GetHashCode()
		{
			int num = this.m_WriteMask.GetHashCode();
			num = (num * 397) ^ this.m_SourceColorBlendMode.GetHashCode();
			num = (num * 397) ^ this.m_DestinationColorBlendMode.GetHashCode();
			num = (num * 397) ^ this.m_SourceAlphaBlendMode.GetHashCode();
			num = (num * 397) ^ this.m_DestinationAlphaBlendMode.GetHashCode();
			num = (num * 397) ^ this.m_ColorBlendOperation.GetHashCode();
			return (num * 397) ^ this.m_AlphaBlendOperation.GetHashCode();
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00033FA0 File Offset: 0x000321A0
		public static bool operator ==(RenderTargetBlendState left, RenderTargetBlendState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00033FBC File Offset: 0x000321BC
		public static bool operator !=(RenderTargetBlendState left, RenderTargetBlendState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000AE8 RID: 2792
		private byte m_WriteMask;

		// Token: 0x04000AE9 RID: 2793
		private byte m_SourceColorBlendMode;

		// Token: 0x04000AEA RID: 2794
		private byte m_DestinationColorBlendMode;

		// Token: 0x04000AEB RID: 2795
		private byte m_SourceAlphaBlendMode;

		// Token: 0x04000AEC RID: 2796
		private byte m_DestinationAlphaBlendMode;

		// Token: 0x04000AED RID: 2797
		private byte m_ColorBlendOperation;

		// Token: 0x04000AEE RID: 2798
		private byte m_AlphaBlendOperation;

		// Token: 0x04000AEF RID: 2799
		private byte m_Padding;
	}
}
