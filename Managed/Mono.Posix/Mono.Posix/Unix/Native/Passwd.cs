using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000066 RID: 102
	public sealed class Passwd : IEquatable<Passwd>
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x0000B1F0 File Offset: 0x000093F0
		public override int GetHashCode()
		{
			return this.pw_name.GetHashCode() ^ this.pw_passwd.GetHashCode() ^ this.pw_uid.GetHashCode() ^ this.pw_gid.GetHashCode() ^ this.pw_gecos.GetHashCode() ^ this.pw_dir.GetHashCode() ^ this.pw_dir.GetHashCode() ^ this.pw_shell.GetHashCode();
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000B25C File Offset: 0x0000945C
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Passwd passwd = (Passwd)obj;
			return this.Equals(passwd);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000B290 File Offset: 0x00009490
		public bool Equals(Passwd value)
		{
			return !(value == null) && (value.pw_uid == this.pw_uid && value.pw_gid == this.pw_gid && value.pw_name == this.pw_name && value.pw_passwd == this.pw_passwd && value.pw_gecos == this.pw_gecos && value.pw_dir == this.pw_dir) && value.pw_shell == this.pw_shell;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000B324 File Offset: 0x00009524
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", new object[] { this.pw_name, this.pw_passwd, this.pw_uid, this.pw_gid, this.pw_gecos, this.pw_dir, this.pw_shell });
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000B38A File Offset: 0x0000958A
		public static bool operator ==(Passwd lhs, Passwd rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000B393 File Offset: 0x00009593
		public static bool operator !=(Passwd lhs, Passwd rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000462 RID: 1122
		public string pw_name;

		// Token: 0x04000463 RID: 1123
		public string pw_passwd;

		// Token: 0x04000464 RID: 1124
		[CLSCompliant(false)]
		public uint pw_uid;

		// Token: 0x04000465 RID: 1125
		[CLSCompliant(false)]
		public uint pw_gid;

		// Token: 0x04000466 RID: 1126
		public string pw_gecos;

		// Token: 0x04000467 RID: 1127
		public string pw_dir;

		// Token: 0x04000468 RID: 1128
		public string pw_shell;
	}
}
