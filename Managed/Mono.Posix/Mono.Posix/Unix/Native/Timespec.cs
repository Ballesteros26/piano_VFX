using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000059 RID: 89
	[Map("struct timespec")]
	public struct Timespec : IEquatable<Timespec>
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000A853 File Offset: 0x00008A53
		public override int GetHashCode()
		{
			return this.tv_sec.GetHashCode() ^ this.tv_nsec.GetHashCode();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000A86C File Offset: 0x00008A6C
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Timespec timespec = (Timespec)obj;
			return timespec.tv_sec == this.tv_sec && timespec.tv_nsec == this.tv_nsec;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		public bool Equals(Timespec value)
		{
			return value.tv_sec == this.tv_sec && value.tv_nsec == this.tv_nsec;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		public static bool operator ==(Timespec lhs, Timespec rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000A8EA File Offset: 0x00008AEA
		public static bool operator !=(Timespec lhs, Timespec rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400042B RID: 1067
		[time_t]
		public long tv_sec;

		// Token: 0x0400042C RID: 1068
		public long tv_nsec;
	}
}
