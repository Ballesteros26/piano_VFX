using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000055 RID: 85
	[Map]
	[CLSCompliant(false)]
	public struct Statvfs : IEquatable<Statvfs>
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000A46C File Offset: 0x0000866C
		public override int GetHashCode()
		{
			return this.f_bsize.GetHashCode() ^ this.f_frsize.GetHashCode() ^ this.f_blocks.GetHashCode() ^ this.f_bfree.GetHashCode() ^ this.f_bavail.GetHashCode() ^ this.f_files.GetHashCode() ^ this.f_ffree.GetHashCode() ^ this.f_favail.GetHashCode() ^ this.f_fsid.GetHashCode() ^ this.f_flag.GetHashCode() ^ this.f_namemax.GetHashCode();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000A504 File Offset: 0x00008704
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Statvfs statvfs = (Statvfs)obj;
			return statvfs.f_bsize == this.f_bsize && statvfs.f_frsize == this.f_frsize && statvfs.f_blocks == this.f_blocks && statvfs.f_bfree == this.f_bfree && statvfs.f_bavail == this.f_bavail && statvfs.f_files == this.f_files && statvfs.f_ffree == this.f_ffree && statvfs.f_favail == this.f_favail && statvfs.f_fsid == this.f_fsid && statvfs.f_flag == this.f_flag && statvfs.f_namemax == this.f_namemax;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000A5DC File Offset: 0x000087DC
		public bool Equals(Statvfs value)
		{
			return value.f_bsize == this.f_bsize && value.f_frsize == this.f_frsize && value.f_blocks == this.f_blocks && value.f_bfree == this.f_bfree && value.f_bavail == this.f_bavail && value.f_files == this.f_files && value.f_ffree == this.f_ffree && value.f_favail == this.f_favail && value.f_fsid == this.f_fsid && value.f_flag == this.f_flag && value.f_namemax == this.f_namemax;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000A688 File Offset: 0x00008888
		public static bool operator ==(Statvfs lhs, Statvfs rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000A692 File Offset: 0x00008892
		public static bool operator !=(Statvfs lhs, Statvfs rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400041A RID: 1050
		public ulong f_bsize;

		// Token: 0x0400041B RID: 1051
		public ulong f_frsize;

		// Token: 0x0400041C RID: 1052
		[fsblkcnt_t]
		public ulong f_blocks;

		// Token: 0x0400041D RID: 1053
		[fsblkcnt_t]
		public ulong f_bfree;

		// Token: 0x0400041E RID: 1054
		[fsblkcnt_t]
		public ulong f_bavail;

		// Token: 0x0400041F RID: 1055
		[fsfilcnt_t]
		public ulong f_files;

		// Token: 0x04000420 RID: 1056
		[fsfilcnt_t]
		public ulong f_ffree;

		// Token: 0x04000421 RID: 1057
		[fsfilcnt_t]
		public ulong f_favail;

		// Token: 0x04000422 RID: 1058
		public ulong f_fsid;

		// Token: 0x04000423 RID: 1059
		public MountFlags f_flag;

		// Token: 0x04000424 RID: 1060
		public ulong f_namemax;
	}
}
