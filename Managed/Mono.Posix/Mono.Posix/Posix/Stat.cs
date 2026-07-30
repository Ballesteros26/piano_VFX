using System;

namespace Mono.Posix
{
	// Token: 0x0200009D RID: 157
	[Obsolete("Use Mono.Unix.Native.Stat")]
	public struct Stat
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x000102E8 File Offset: 0x0000E4E8
		[Obsolete("Use Mono.Unix.Native.NativeConvert.ToDateTime")]
		public static DateTime UnixToDateTime(long unix)
		{
			return Stat.UnixEpoch.Add(TimeSpan.FromSeconds((double)unix)).ToLocalTime();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00010314 File Offset: 0x0000E514
		internal Stat(int device, int inode, int mode, int nlinks, int uid, int gid, int rdev, long size, long blksize, long blocks, long atime, long mtime, long ctime)
		{
			this.Device = device;
			this.INode = inode;
			this.Mode = (StatMode)mode;
			this.NLinks = nlinks;
			this.Uid = uid;
			this.Gid = gid;
			this.DeviceType = (long)rdev;
			this.Size = size;
			this.BlockSize = blksize;
			this.Blocks = blocks;
			if (atime != 0L)
			{
				this.ATime = Stat.UnixToDateTime(atime);
			}
			else
			{
				this.ATime = default(DateTime);
			}
			if (mtime != 0L)
			{
				this.MTime = Stat.UnixToDateTime(mtime);
			}
			else
			{
				this.MTime = default(DateTime);
			}
			if (ctime != 0L)
			{
				this.CTime = Stat.UnixToDateTime(ctime);
				return;
			}
			this.CTime = default(DateTime);
		}

		// Token: 0x04000536 RID: 1334
		[Obsolete("Use Mono.Unix.Native.Stat.st_dev")]
		public readonly int Device;

		// Token: 0x04000537 RID: 1335
		[Obsolete("Use Mono.Unix.Native.Stat.st_ino")]
		public readonly int INode;

		// Token: 0x04000538 RID: 1336
		[Obsolete("Use Mono.Unix.Native.Stat.st_mode")]
		public readonly StatMode Mode;

		// Token: 0x04000539 RID: 1337
		[Obsolete("Use Mono.Unix.Native.Stat.st_nlink")]
		public readonly int NLinks;

		// Token: 0x0400053A RID: 1338
		[Obsolete("Use Mono.Unix.Native.Stat.st_uid")]
		public readonly int Uid;

		// Token: 0x0400053B RID: 1339
		[Obsolete("Use Mono.Unix.Native.Stat.st_gid")]
		public readonly int Gid;

		// Token: 0x0400053C RID: 1340
		[Obsolete("Use Mono.Unix.Native.Stat.st_rdev")]
		public readonly long DeviceType;

		// Token: 0x0400053D RID: 1341
		[Obsolete("Use Mono.Unix.Native.Stat.st_size")]
		public readonly long Size;

		// Token: 0x0400053E RID: 1342
		[Obsolete("Use Mono.Unix.Native.Stat.st_blksize")]
		public readonly long BlockSize;

		// Token: 0x0400053F RID: 1343
		[Obsolete("Use Mono.Unix.Native.Stat.st_blocks")]
		public readonly long Blocks;

		// Token: 0x04000540 RID: 1344
		[Obsolete("Use Mono.Unix.Native.Stat.st_atime")]
		public readonly DateTime ATime;

		// Token: 0x04000541 RID: 1345
		[Obsolete("Use Mono.Unix.Native.Stat.st_mtime")]
		public readonly DateTime MTime;

		// Token: 0x04000542 RID: 1346
		[Obsolete("Use Mono.Unix.Native.Stat.st_ctime")]
		public readonly DateTime CTime;

		// Token: 0x04000543 RID: 1347
		[Obsolete("Use Mono.Unix.Native.NativeConvert.LocalUnixEpoch")]
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
	}
}
