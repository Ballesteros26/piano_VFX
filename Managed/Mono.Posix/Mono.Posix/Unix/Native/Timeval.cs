using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000056 RID: 86
	[Map("struct timeval")]
	public struct Timeval : IEquatable<Timeval>
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000A69F File Offset: 0x0000889F
		public override int GetHashCode()
		{
			return this.tv_sec.GetHashCode() ^ this.tv_usec.GetHashCode();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000A6B8 File Offset: 0x000088B8
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Timeval timeval = (Timeval)obj;
			return timeval.tv_sec == this.tv_sec && timeval.tv_usec == this.tv_usec;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000A70C File Offset: 0x0000890C
		public bool Equals(Timeval value)
		{
			return value.tv_sec == this.tv_sec && value.tv_usec == this.tv_usec;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A72C File Offset: 0x0000892C
		public static bool operator ==(Timeval lhs, Timeval rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A736 File Offset: 0x00008936
		public static bool operator !=(Timeval lhs, Timeval rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000425 RID: 1061
		[time_t]
		public long tv_sec;

		// Token: 0x04000426 RID: 1062
		[suseconds_t]
		public long tv_usec;
	}
}
