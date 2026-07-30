using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000063 RID: 99
	public sealed class Dirent : IEquatable<Dirent>
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000AD99 File Offset: 0x00008F99
		public override int GetHashCode()
		{
			return this.d_ino.GetHashCode() ^ this.d_off.GetHashCode() ^ this.d_reclen.GetHashCode() ^ this.d_type.GetHashCode() ^ this.d_name.GetHashCode();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Dirent dirent = (Dirent)obj;
			return this.Equals(dirent);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000AE0C File Offset: 0x0000900C
		public bool Equals(Dirent value)
		{
			return !(value == null) && (value.d_ino == this.d_ino && value.d_off == this.d_off && value.d_reclen == this.d_reclen && value.d_type == this.d_type) && value.d_name == this.d_name;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000AE6F File Offset: 0x0000906F
		public override string ToString()
		{
			return this.d_name;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000AE77 File Offset: 0x00009077
		public static bool operator ==(Dirent lhs, Dirent rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000AE80 File Offset: 0x00009080
		public static bool operator !=(Dirent lhs, Dirent rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000452 RID: 1106
		[CLSCompliant(false)]
		public ulong d_ino;

		// Token: 0x04000453 RID: 1107
		public long d_off;

		// Token: 0x04000454 RID: 1108
		[CLSCompliant(false)]
		public ushort d_reclen;

		// Token: 0x04000455 RID: 1109
		public byte d_type;

		// Token: 0x04000456 RID: 1110
		public string d_name;
	}
}
