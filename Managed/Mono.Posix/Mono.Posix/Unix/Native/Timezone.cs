using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000057 RID: 87
	[Map("struct timezone")]
	public struct Timezone : IEquatable<Timezone>
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0000A743 File Offset: 0x00008943
		public override int GetHashCode()
		{
			return this.tz_minuteswest.GetHashCode();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000A750 File Offset: 0x00008950
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && ((Timezone)obj).tz_minuteswest == this.tz_minuteswest;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000A787 File Offset: 0x00008987
		public bool Equals(Timezone value)
		{
			return value.tz_minuteswest == this.tz_minuteswest;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000A797 File Offset: 0x00008997
		public static bool operator ==(Timezone lhs, Timezone rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000A7A1 File Offset: 0x000089A1
		public static bool operator !=(Timezone lhs, Timezone rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000427 RID: 1063
		public int tz_minuteswest;

		// Token: 0x04000428 RID: 1064
		private int tz_dsttime;
	}
}
