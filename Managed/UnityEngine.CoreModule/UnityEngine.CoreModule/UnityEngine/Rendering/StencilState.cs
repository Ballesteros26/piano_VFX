using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000381 RID: 897
	public struct StencilState : IEquatable<StencilState>
	{
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x00034FB0 File Offset: 0x000331B0
		public static StencilState defaultValue
		{
			get
			{
				return new StencilState(true, byte.MaxValue, byte.MaxValue, CompareFunction.Always, StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00034FD8 File Offset: 0x000331D8
		public StencilState(bool enabled = true, byte readMask = 255, byte writeMask = 255, CompareFunction compareFunction = CompareFunction.Always, StencilOp passOperation = StencilOp.Keep, StencilOp failOperation = StencilOp.Keep, StencilOp zFailOperation = StencilOp.Keep)
		{
			this = new StencilState(enabled, readMask, writeMask, compareFunction, passOperation, failOperation, zFailOperation, compareFunction, passOperation, failOperation, zFailOperation);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00035000 File Offset: 0x00033200
		public StencilState(bool enabled, byte readMask, byte writeMask, CompareFunction compareFunctionFront, StencilOp passOperationFront, StencilOp failOperationFront, StencilOp zFailOperationFront, CompareFunction compareFunctionBack, StencilOp passOperationBack, StencilOp failOperationBack, StencilOp zFailOperationBack)
		{
			this.m_Enabled = Convert.ToByte(enabled);
			this.m_ReadMask = readMask;
			this.m_WriteMask = writeMask;
			this.m_Padding = 0;
			this.m_CompareFunctionFront = (byte)compareFunctionFront;
			this.m_PassOperationFront = (byte)passOperationFront;
			this.m_FailOperationFront = (byte)failOperationFront;
			this.m_ZFailOperationFront = (byte)zFailOperationFront;
			this.m_CompareFunctionBack = (byte)compareFunctionBack;
			this.m_PassOperationBack = (byte)passOperationBack;
			this.m_FailOperationBack = (byte)failOperationBack;
			this.m_ZFailOperationBack = (byte)zFailOperationBack;
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x00035078 File Offset: 0x00033278
		// (set) Token: 0x06001F1B RID: 7963 RVA: 0x00035095 File Offset: 0x00033295
		public bool enabled
		{
			get
			{
				return Convert.ToBoolean(this.m_Enabled);
			}
			set
			{
				this.m_Enabled = Convert.ToByte(value);
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x000350A4 File Offset: 0x000332A4
		// (set) Token: 0x06001F1D RID: 7965 RVA: 0x000350BC File Offset: 0x000332BC
		public byte readMask
		{
			get
			{
				return this.m_ReadMask;
			}
			set
			{
				this.m_ReadMask = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x000350C8 File Offset: 0x000332C8
		// (set) Token: 0x06001F1F RID: 7967 RVA: 0x000350E0 File Offset: 0x000332E0
		public byte writeMask
		{
			get
			{
				return this.m_WriteMask;
			}
			set
			{
				this.m_WriteMask = value;
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000350EA File Offset: 0x000332EA
		public void SetCompareFunction(CompareFunction value)
		{
			this.compareFunctionFront = value;
			this.compareFunctionBack = value;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000350FD File Offset: 0x000332FD
		public void SetPassOperation(StencilOp value)
		{
			this.passOperationFront = value;
			this.passOperationBack = value;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00035110 File Offset: 0x00033310
		public void SetFailOperation(StencilOp value)
		{
			this.failOperationFront = value;
			this.failOperationBack = value;
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00035123 File Offset: 0x00033323
		public void SetZFailOperation(StencilOp value)
		{
			this.zFailOperationFront = value;
			this.zFailOperationBack = value;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x00035138 File Offset: 0x00033338
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x00035150 File Offset: 0x00033350
		public CompareFunction compareFunctionFront
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionFront;
			}
			set
			{
				this.m_CompareFunctionFront = (byte)value;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0003515C File Offset: 0x0003335C
		// (set) Token: 0x06001F27 RID: 7975 RVA: 0x00035174 File Offset: 0x00033374
		public StencilOp passOperationFront
		{
			get
			{
				return (StencilOp)this.m_PassOperationFront;
			}
			set
			{
				this.m_PassOperationFront = (byte)value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x00035180 File Offset: 0x00033380
		// (set) Token: 0x06001F29 RID: 7977 RVA: 0x00035198 File Offset: 0x00033398
		public StencilOp failOperationFront
		{
			get
			{
				return (StencilOp)this.m_FailOperationFront;
			}
			set
			{
				this.m_FailOperationFront = (byte)value;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x000351A4 File Offset: 0x000333A4
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x000351BC File Offset: 0x000333BC
		public StencilOp zFailOperationFront
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationFront;
			}
			set
			{
				this.m_ZFailOperationFront = (byte)value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x000351C8 File Offset: 0x000333C8
		// (set) Token: 0x06001F2D RID: 7981 RVA: 0x000351E0 File Offset: 0x000333E0
		public CompareFunction compareFunctionBack
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionBack;
			}
			set
			{
				this.m_CompareFunctionBack = (byte)value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x000351EC File Offset: 0x000333EC
		// (set) Token: 0x06001F2F RID: 7983 RVA: 0x00035204 File Offset: 0x00033404
		public StencilOp passOperationBack
		{
			get
			{
				return (StencilOp)this.m_PassOperationBack;
			}
			set
			{
				this.m_PassOperationBack = (byte)value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00035210 File Offset: 0x00033410
		// (set) Token: 0x06001F31 RID: 7985 RVA: 0x00035228 File Offset: 0x00033428
		public StencilOp failOperationBack
		{
			get
			{
				return (StencilOp)this.m_FailOperationBack;
			}
			set
			{
				this.m_FailOperationBack = (byte)value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x00035234 File Offset: 0x00033434
		// (set) Token: 0x06001F33 RID: 7987 RVA: 0x0003524C File Offset: 0x0003344C
		public StencilOp zFailOperationBack
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationBack;
			}
			set
			{
				this.m_ZFailOperationBack = (byte)value;
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00035258 File Offset: 0x00033458
		public bool Equals(StencilState other)
		{
			return this.m_Enabled == other.m_Enabled && this.m_ReadMask == other.m_ReadMask && this.m_WriteMask == other.m_WriteMask && this.m_CompareFunctionFront == other.m_CompareFunctionFront && this.m_PassOperationFront == other.m_PassOperationFront && this.m_FailOperationFront == other.m_FailOperationFront && this.m_ZFailOperationFront == other.m_ZFailOperationFront && this.m_CompareFunctionBack == other.m_CompareFunctionBack && this.m_PassOperationBack == other.m_PassOperationBack && this.m_FailOperationBack == other.m_FailOperationBack && this.m_ZFailOperationBack == other.m_ZFailOperationBack;
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00035310 File Offset: 0x00033510
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is StencilState && this.Equals((StencilState)obj);
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00035348 File Offset: 0x00033548
		public override int GetHashCode()
		{
			int num = this.m_Enabled.GetHashCode();
			num = (num * 397) ^ this.m_ReadMask.GetHashCode();
			num = (num * 397) ^ this.m_WriteMask.GetHashCode();
			num = (num * 397) ^ this.m_CompareFunctionFront.GetHashCode();
			num = (num * 397) ^ this.m_PassOperationFront.GetHashCode();
			num = (num * 397) ^ this.m_FailOperationFront.GetHashCode();
			num = (num * 397) ^ this.m_ZFailOperationFront.GetHashCode();
			num = (num * 397) ^ this.m_CompareFunctionBack.GetHashCode();
			num = (num * 397) ^ this.m_PassOperationBack.GetHashCode();
			num = (num * 397) ^ this.m_FailOperationBack.GetHashCode();
			return (num * 397) ^ this.m_ZFailOperationBack.GetHashCode();
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00035430 File Offset: 0x00033630
		public static bool operator ==(StencilState left, StencilState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0003544C File Offset: 0x0003364C
		public static bool operator !=(StencilState left, StencilState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000B18 RID: 2840
		private byte m_Enabled;

		// Token: 0x04000B19 RID: 2841
		private byte m_ReadMask;

		// Token: 0x04000B1A RID: 2842
		private byte m_WriteMask;

		// Token: 0x04000B1B RID: 2843
		private byte m_Padding;

		// Token: 0x04000B1C RID: 2844
		private byte m_CompareFunctionFront;

		// Token: 0x04000B1D RID: 2845
		private byte m_PassOperationFront;

		// Token: 0x04000B1E RID: 2846
		private byte m_FailOperationFront;

		// Token: 0x04000B1F RID: 2847
		private byte m_ZFailOperationFront;

		// Token: 0x04000B20 RID: 2848
		private byte m_CompareFunctionBack;

		// Token: 0x04000B21 RID: 2849
		private byte m_PassOperationBack;

		// Token: 0x04000B22 RID: 2850
		private byte m_FailOperationBack;

		// Token: 0x04000B23 RID: 2851
		private byte m_ZFailOperationBack;
	}
}
