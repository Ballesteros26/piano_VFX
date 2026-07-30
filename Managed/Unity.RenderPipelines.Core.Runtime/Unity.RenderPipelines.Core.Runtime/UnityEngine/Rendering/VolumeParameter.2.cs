using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006A RID: 106
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class VolumeParameter<T> : VolumeParameter, IEquatable<VolumeParameter<T>>
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		public virtual T value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D008 File Offset: 0x0000B208
		public VolumeParameter()
			: this(default(T), false)
		{
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D025 File Offset: 0x0000B225
		protected VolumeParameter(T value, bool overrideState)
		{
			this.m_Value = value;
			this.overrideState = overrideState;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D03B File Offset: 0x0000B23B
		internal override void Interp(VolumeParameter from, VolumeParameter to, float t)
		{
			this.Interp(from.GetValue<T>(), to.GetValue<T>(), t);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D050 File Offset: 0x0000B250
		public virtual void Interp(T from, T to, float t)
		{
			this.m_Value = ((t > 0f) ? to : from);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D064 File Offset: 0x0000B264
		public void Override(T x)
		{
			this.overrideState = true;
			this.m_Value = x;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000D074 File Offset: 0x0000B274
		public override void SetValue(VolumeParameter parameter)
		{
			this.m_Value = parameter.GetValue<T>();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D084 File Offset: 0x0000B284
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.overrideState.GetHashCode();
			if (this.value != null)
			{
				int num2 = num * 23;
				T value = this.value;
				num = num2 + value.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		public override string ToString()
		{
			return string.Format("{0} ({1})", this.value, this.overrideState);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public static bool operator ==(VolumeParameter<T> lhs, T rhs)
		{
			if (lhs != null && lhs.value != null)
			{
				T value = lhs.value;
				return value.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D12D File Offset: 0x0000B32D
		public static bool operator !=(VolumeParameter<T> lhs, T rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D139 File Offset: 0x0000B339
		public bool Equals(VolumeParameter<T> other)
		{
			return other != null && (this == other || EqualityComparer<T>.Default.Equals(this.m_Value, other.m_Value));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000D15C File Offset: 0x0000B35C
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((VolumeParameter<T>)obj)));
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		public static explicit operator T(VolumeParameter<T> prop)
		{
			return prop.m_Value;
		}

		// Token: 0x040001AE RID: 430
		[SerializeField]
		protected T m_Value;
	}
}
