using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000052 RID: 82
	[Map("struct flock")]
	public struct Flock : IEquatable<Flock>
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x00009E7C File Offset: 0x0000807C
		public override int GetHashCode()
		{
			return this.l_type.GetHashCode() ^ this.l_whence.GetHashCode() ^ this.l_start.GetHashCode() ^ this.l_len.GetHashCode() ^ this.l_pid.GetHashCode();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00009ED0 File Offset: 0x000080D0
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Flock flock = (Flock)obj;
			return this.l_type == flock.l_type && this.l_whence == flock.l_whence && this.l_start == flock.l_start && this.l_len == flock.l_len && this.l_pid == flock.l_pid;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00009F50 File Offset: 0x00008150
		public bool Equals(Flock value)
		{
			return this.l_type == value.l_type && this.l_whence == value.l_whence && this.l_start == value.l_start && this.l_len == value.l_len && this.l_pid == value.l_pid;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00009FA5 File Offset: 0x000081A5
		public static bool operator ==(Flock lhs, Flock rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00009FAF File Offset: 0x000081AF
		public static bool operator !=(Flock lhs, Flock rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000401 RID: 1025
		[CLSCompliant(false)]
		public LockType l_type;

		// Token: 0x04000402 RID: 1026
		[CLSCompliant(false)]
		public SeekFlags l_whence;

		// Token: 0x04000403 RID: 1027
		[off_t]
		public long l_start;

		// Token: 0x04000404 RID: 1028
		[off_t]
		public long l_len;

		// Token: 0x04000405 RID: 1029
		[pid_t]
		public int l_pid;
	}
}
