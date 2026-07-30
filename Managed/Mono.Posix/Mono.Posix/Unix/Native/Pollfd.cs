using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000053 RID: 83
	[Map("struct pollfd")]
	public struct Pollfd : IEquatable<Pollfd>
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x00009FBC File Offset: 0x000081BC
		public override int GetHashCode()
		{
			return this.events.GetHashCode() ^ this.revents.GetHashCode();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00009FE4 File Offset: 0x000081E4
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Pollfd pollfd = (Pollfd)obj;
			return pollfd.events == this.events && pollfd.revents == this.revents;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000A038 File Offset: 0x00008238
		public bool Equals(Pollfd value)
		{
			return value.events == this.events && value.revents == this.revents;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A058 File Offset: 0x00008258
		public static bool operator ==(Pollfd lhs, Pollfd rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000A062 File Offset: 0x00008262
		public static bool operator !=(Pollfd lhs, Pollfd rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000406 RID: 1030
		public int fd;

		// Token: 0x04000407 RID: 1031
		[CLSCompliant(false)]
		public PollEvents events;

		// Token: 0x04000408 RID: 1032
		[CLSCompliant(false)]
		public PollEvents revents;
	}
}
