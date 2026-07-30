using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000365 RID: 869
	public struct DepthState : IEquatable<DepthState>
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x00032660 File Offset: 0x00030860
		public static DepthState defaultValue
		{
			get
			{
				return new DepthState(true, CompareFunction.Less);
			}
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00032679 File Offset: 0x00030879
		public DepthState(bool writeEnabled = true, CompareFunction compareFunction = CompareFunction.Less)
		{
			this.m_WriteEnabled = Convert.ToByte(writeEnabled);
			this.m_CompareFunction = (sbyte)compareFunction;
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x00032690 File Offset: 0x00030890
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x000326AD File Offset: 0x000308AD
		public bool writeEnabled
		{
			get
			{
				return Convert.ToBoolean(this.m_WriteEnabled);
			}
			set
			{
				this.m_WriteEnabled = Convert.ToByte(value);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x000326BC File Offset: 0x000308BC
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x000326D4 File Offset: 0x000308D4
		public CompareFunction compareFunction
		{
			get
			{
				return (CompareFunction)this.m_CompareFunction;
			}
			set
			{
				this.m_CompareFunction = (sbyte)value;
			}
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000326E0 File Offset: 0x000308E0
		public bool Equals(DepthState other)
		{
			return this.m_WriteEnabled == other.m_WriteEnabled && this.m_CompareFunction == other.m_CompareFunction;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00032714 File Offset: 0x00030914
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DepthState && this.Equals((DepthState)obj);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0003274C File Offset: 0x0003094C
		public override int GetHashCode()
		{
			return (this.m_WriteEnabled.GetHashCode() * 397) ^ this.m_CompareFunction.GetHashCode();
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0003277C File Offset: 0x0003097C
		public static bool operator ==(DepthState left, DepthState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00032798 File Offset: 0x00030998
		public static bool operator !=(DepthState left, DepthState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000A94 RID: 2708
		private byte m_WriteEnabled;

		// Token: 0x04000A95 RID: 2709
		private sbyte m_CompareFunction;
	}
}
