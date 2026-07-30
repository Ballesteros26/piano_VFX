using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000359 RID: 857
	public struct BlendState : IEquatable<BlendState>
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00030C38 File Offset: 0x0002EE38
		public static BlendState defaultValue
		{
			get
			{
				return new BlendState(false, false);
			}
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00030C54 File Offset: 0x0002EE54
		public BlendState(bool separateMRTBlend = false, bool alphaToMask = false)
		{
			this.m_BlendState0 = RenderTargetBlendState.defaultValue;
			this.m_BlendState1 = RenderTargetBlendState.defaultValue;
			this.m_BlendState2 = RenderTargetBlendState.defaultValue;
			this.m_BlendState3 = RenderTargetBlendState.defaultValue;
			this.m_BlendState4 = RenderTargetBlendState.defaultValue;
			this.m_BlendState5 = RenderTargetBlendState.defaultValue;
			this.m_BlendState6 = RenderTargetBlendState.defaultValue;
			this.m_BlendState7 = RenderTargetBlendState.defaultValue;
			this.m_SeparateMRTBlendStates = Convert.ToByte(separateMRTBlend);
			this.m_AlphaToMask = Convert.ToByte(alphaToMask);
			this.m_Padding = 0;
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x00030CDC File Offset: 0x0002EEDC
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x00030CF9 File Offset: 0x0002EEF9
		public bool separateMRTBlendStates
		{
			get
			{
				return Convert.ToBoolean(this.m_SeparateMRTBlendStates);
			}
			set
			{
				this.m_SeparateMRTBlendStates = Convert.ToByte(value);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x00030D08 File Offset: 0x0002EF08
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x00030D25 File Offset: 0x0002EF25
		public bool alphaToMask
		{
			get
			{
				return Convert.ToBoolean(this.m_AlphaToMask);
			}
			set
			{
				this.m_AlphaToMask = Convert.ToByte(value);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00030D34 File Offset: 0x0002EF34
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x00030D4C File Offset: 0x0002EF4C
		public RenderTargetBlendState blendState0
		{
			get
			{
				return this.m_BlendState0;
			}
			set
			{
				this.m_BlendState0 = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x00030D58 File Offset: 0x0002EF58
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x00030D70 File Offset: 0x0002EF70
		public RenderTargetBlendState blendState1
		{
			get
			{
				return this.m_BlendState1;
			}
			set
			{
				this.m_BlendState1 = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00030D7C File Offset: 0x0002EF7C
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x00030D94 File Offset: 0x0002EF94
		public RenderTargetBlendState blendState2
		{
			get
			{
				return this.m_BlendState2;
			}
			set
			{
				this.m_BlendState2 = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00030DA0 File Offset: 0x0002EFA0
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x00030DB8 File Offset: 0x0002EFB8
		public RenderTargetBlendState blendState3
		{
			get
			{
				return this.m_BlendState3;
			}
			set
			{
				this.m_BlendState3 = value;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00030DC4 File Offset: 0x0002EFC4
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x00030DDC File Offset: 0x0002EFDC
		public RenderTargetBlendState blendState4
		{
			get
			{
				return this.m_BlendState4;
			}
			set
			{
				this.m_BlendState4 = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00030DE8 File Offset: 0x0002EFE8
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x00030E00 File Offset: 0x0002F000
		public RenderTargetBlendState blendState5
		{
			get
			{
				return this.m_BlendState5;
			}
			set
			{
				this.m_BlendState5 = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00030E0C File Offset: 0x0002F00C
		// (set) Token: 0x06001D60 RID: 7520 RVA: 0x00030E24 File Offset: 0x0002F024
		public RenderTargetBlendState blendState6
		{
			get
			{
				return this.m_BlendState6;
			}
			set
			{
				this.m_BlendState6 = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00030E30 File Offset: 0x0002F030
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x00030E48 File Offset: 0x0002F048
		public RenderTargetBlendState blendState7
		{
			get
			{
				return this.m_BlendState7;
			}
			set
			{
				this.m_BlendState7 = value;
			}
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00030E54 File Offset: 0x0002F054
		public bool Equals(BlendState other)
		{
			return this.m_BlendState0.Equals(other.m_BlendState0) && this.m_BlendState1.Equals(other.m_BlendState1) && this.m_BlendState2.Equals(other.m_BlendState2) && this.m_BlendState3.Equals(other.m_BlendState3) && this.m_BlendState4.Equals(other.m_BlendState4) && this.m_BlendState5.Equals(other.m_BlendState5) && this.m_BlendState6.Equals(other.m_BlendState6) && this.m_BlendState7.Equals(other.m_BlendState7) && this.m_SeparateMRTBlendStates == other.m_SeparateMRTBlendStates && this.m_AlphaToMask == other.m_AlphaToMask;
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x00030F24 File Offset: 0x0002F124
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is BlendState && this.Equals((BlendState)obj);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x00030F5C File Offset: 0x0002F15C
		public override int GetHashCode()
		{
			int num = this.m_BlendState0.GetHashCode();
			num = (num * 397) ^ this.m_BlendState1.GetHashCode();
			num = (num * 397) ^ this.m_BlendState2.GetHashCode();
			num = (num * 397) ^ this.m_BlendState3.GetHashCode();
			num = (num * 397) ^ this.m_BlendState4.GetHashCode();
			num = (num * 397) ^ this.m_BlendState5.GetHashCode();
			num = (num * 397) ^ this.m_BlendState6.GetHashCode();
			num = (num * 397) ^ this.m_BlendState7.GetHashCode();
			num = (num * 397) ^ this.m_SeparateMRTBlendStates.GetHashCode();
			return (num * 397) ^ this.m_AlphaToMask.GetHashCode();
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x00031060 File Offset: 0x0002F260
		public static bool operator ==(BlendState left, BlendState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0003107C File Offset: 0x0002F27C
		public static bool operator !=(BlendState left, BlendState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000A33 RID: 2611
		private RenderTargetBlendState m_BlendState0;

		// Token: 0x04000A34 RID: 2612
		private RenderTargetBlendState m_BlendState1;

		// Token: 0x04000A35 RID: 2613
		private RenderTargetBlendState m_BlendState2;

		// Token: 0x04000A36 RID: 2614
		private RenderTargetBlendState m_BlendState3;

		// Token: 0x04000A37 RID: 2615
		private RenderTargetBlendState m_BlendState4;

		// Token: 0x04000A38 RID: 2616
		private RenderTargetBlendState m_BlendState5;

		// Token: 0x04000A39 RID: 2617
		private RenderTargetBlendState m_BlendState6;

		// Token: 0x04000A3A RID: 2618
		private RenderTargetBlendState m_BlendState7;

		// Token: 0x04000A3B RID: 2619
		private byte m_SeparateMRTBlendStates;

		// Token: 0x04000A3C RID: 2620
		private byte m_AlphaToMask;

		// Token: 0x04000A3D RID: 2621
		private short m_Padding;
	}
}
