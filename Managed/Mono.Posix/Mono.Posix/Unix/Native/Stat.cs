using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000054 RID: 84
	public struct Stat : IEquatable<Stat>
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000A070 File Offset: 0x00008270
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x0000A0A0 File Offset: 0x000082A0
		public Timespec st_atim
		{
			get
			{
				return new Timespec
				{
					tv_sec = this.st_atime,
					tv_nsec = this.st_atime_nsec
				};
			}
			set
			{
				this.st_atime = value.tv_sec;
				this.st_atime_nsec = value.tv_nsec;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000A0BC File Offset: 0x000082BC
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000A0EC File Offset: 0x000082EC
		public Timespec st_mtim
		{
			get
			{
				return new Timespec
				{
					tv_sec = this.st_mtime,
					tv_nsec = this.st_mtime_nsec
				};
			}
			set
			{
				this.st_mtime = value.tv_sec;
				this.st_mtime_nsec = value.tv_nsec;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000A108 File Offset: 0x00008308
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000A138 File Offset: 0x00008338
		public Timespec st_ctim
		{
			get
			{
				return new Timespec
				{
					tv_sec = this.st_ctime,
					tv_nsec = this.st_ctime_nsec
				};
			}
			set
			{
				this.st_ctime = value.tv_sec;
				this.st_ctime_nsec = value.tv_nsec;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A154 File Offset: 0x00008354
		public override int GetHashCode()
		{
			return this.st_dev.GetHashCode() ^ this.st_ino.GetHashCode() ^ this.st_mode.GetHashCode() ^ this.st_nlink.GetHashCode() ^ this.st_uid.GetHashCode() ^ this.st_gid.GetHashCode() ^ this.st_rdev.GetHashCode() ^ this.st_size.GetHashCode() ^ this.st_blksize.GetHashCode() ^ this.st_blocks.GetHashCode() ^ this.st_atime.GetHashCode() ^ this.st_mtime.GetHashCode() ^ this.st_ctime.GetHashCode() ^ this.st_atime_nsec.GetHashCode() ^ this.st_mtime_nsec.GetHashCode() ^ this.st_ctime_nsec.GetHashCode();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A228 File Offset: 0x00008428
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Stat stat = (Stat)obj;
			return stat.st_dev == this.st_dev && stat.st_ino == this.st_ino && stat.st_mode == this.st_mode && stat.st_nlink == this.st_nlink && stat.st_uid == this.st_uid && stat.st_gid == this.st_gid && stat.st_rdev == this.st_rdev && stat.st_size == this.st_size && stat.st_blksize == this.st_blksize && stat.st_blocks == this.st_blocks && stat.st_atime == this.st_atime && stat.st_mtime == this.st_mtime && stat.st_ctime == this.st_ctime && stat.st_atime_nsec == this.st_atime_nsec && stat.st_mtime_nsec == this.st_mtime_nsec && stat.st_ctime_nsec == this.st_ctime_nsec;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000A354 File Offset: 0x00008554
		public bool Equals(Stat value)
		{
			return value.st_dev == this.st_dev && value.st_ino == this.st_ino && value.st_mode == this.st_mode && value.st_nlink == this.st_nlink && value.st_uid == this.st_uid && value.st_gid == this.st_gid && value.st_rdev == this.st_rdev && value.st_size == this.st_size && value.st_blksize == this.st_blksize && value.st_blocks == this.st_blocks && value.st_atime == this.st_atime && value.st_mtime == this.st_mtime && value.st_ctime == this.st_ctime && value.st_atime_nsec == this.st_atime_nsec && value.st_mtime_nsec == this.st_mtime_nsec && value.st_ctime_nsec == this.st_ctime_nsec;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000A455 File Offset: 0x00008655
		public static bool operator ==(Stat lhs, Stat rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000A45F File Offset: 0x0000865F
		public static bool operator !=(Stat lhs, Stat rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000409 RID: 1033
		[CLSCompliant(false)]
		[dev_t]
		public ulong st_dev;

		// Token: 0x0400040A RID: 1034
		[CLSCompliant(false)]
		[ino_t]
		public ulong st_ino;

		// Token: 0x0400040B RID: 1035
		[CLSCompliant(false)]
		public FilePermissions st_mode;

		// Token: 0x0400040C RID: 1036
		[NonSerialized]
		private uint _padding_;

		// Token: 0x0400040D RID: 1037
		[CLSCompliant(false)]
		[nlink_t]
		public ulong st_nlink;

		// Token: 0x0400040E RID: 1038
		[CLSCompliant(false)]
		[uid_t]
		public uint st_uid;

		// Token: 0x0400040F RID: 1039
		[CLSCompliant(false)]
		[gid_t]
		public uint st_gid;

		// Token: 0x04000410 RID: 1040
		[CLSCompliant(false)]
		[dev_t]
		public ulong st_rdev;

		// Token: 0x04000411 RID: 1041
		[off_t]
		public long st_size;

		// Token: 0x04000412 RID: 1042
		[blksize_t]
		public long st_blksize;

		// Token: 0x04000413 RID: 1043
		[blkcnt_t]
		public long st_blocks;

		// Token: 0x04000414 RID: 1044
		[time_t]
		public long st_atime;

		// Token: 0x04000415 RID: 1045
		[time_t]
		public long st_mtime;

		// Token: 0x04000416 RID: 1046
		[time_t]
		public long st_ctime;

		// Token: 0x04000417 RID: 1047
		public long st_atime_nsec;

		// Token: 0x04000418 RID: 1048
		public long st_mtime_nsec;

		// Token: 0x04000419 RID: 1049
		public long st_ctime_nsec;
	}
}
