using System;
using System.IO;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000017 RID: 23
	public abstract class UnixFileSystemInfo
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004C30 File Offset: 0x00002E30
		protected UnixFileSystemInfo(string path)
		{
			UnixPath.CheckPath(path);
			this.originalPath = path;
			this.fullPath = UnixPath.GetFullPath(path);
			this.Refresh(true);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004C58 File Offset: 0x00002E58
		internal UnixFileSystemInfo(string path, Stat stat)
		{
			this.originalPath = path;
			this.fullPath = UnixPath.GetFullPath(path);
			this.stat = stat;
			this.valid = true;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004C81 File Offset: 0x00002E81
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004C89 File Offset: 0x00002E89
		protected string FullPath
		{
			get
			{
				return this.fullPath;
			}
			set
			{
				if (this.fullPath != value)
				{
					UnixPath.CheckPath(value);
					this.valid = false;
					this.fullPath = value;
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004CAD File Offset: 0x00002EAD
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004CB5 File Offset: 0x00002EB5
		protected string OriginalPath
		{
			get
			{
				return this.originalPath;
			}
			set
			{
				this.originalPath = value;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004CBE File Offset: 0x00002EBE
		private void AssertValid()
		{
			this.Refresh(false);
			if (!this.valid)
			{
				throw new InvalidOperationException("Path doesn't exist!");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004CDA File Offset: 0x00002EDA
		public virtual string FullName
		{
			get
			{
				return this.FullPath;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E5 RID: 229
		public abstract string Name { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004CE2 File Offset: 0x00002EE2
		public bool Exists
		{
			get
			{
				this.Refresh(true);
				return this.valid;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004CF1 File Offset: 0x00002EF1
		public long Device
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_dev);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004D09 File Offset: 0x00002F09
		public long Inode
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_ino);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004D21 File Offset: 0x00002F21
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004D34 File Offset: 0x00002F34
		[CLSCompliant(false)]
		public FilePermissions Protection
		{
			get
			{
				this.AssertValid();
				return this.stat.st_mode;
			}
			set
			{
				UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.chmod(this.FullPath, value));
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004D47 File Offset: 0x00002F47
		public FileTypes FileType
		{
			get
			{
				this.AssertValid();
				return (FileTypes)(this.stat.st_mode & FilePermissions.S_IFMT);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004D60 File Offset: 0x00002F60
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004D7C File Offset: 0x00002F7C
		public FileAccessPermissions FileAccessPermissions
		{
			get
			{
				this.AssertValid();
				return (FileAccessPermissions)(this.stat.st_mode & FilePermissions.ACCESSPERMS);
			}
			set
			{
				this.AssertValid();
				int num = (int)this.stat.st_mode;
				num &= -512;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004DAE File Offset: 0x00002FAE
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public FileSpecialAttributes FileSpecialAttributes
		{
			get
			{
				this.AssertValid();
				return (FileSpecialAttributes)(this.stat.st_mode & (FilePermissions.S_ISUID | FilePermissions.S_ISGID | FilePermissions.S_ISVTX));
			}
			set
			{
				this.AssertValid();
				int num = (int)this.stat.st_mode;
				num &= -3585;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004DFA File Offset: 0x00002FFA
		public long LinkCount
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_nlink);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004E12 File Offset: 0x00003012
		public UnixUserInfo OwnerUser
		{
			get
			{
				this.AssertValid();
				return new UnixUserInfo(this.stat.st_uid);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004E2A File Offset: 0x0000302A
		public long OwnerUserId
		{
			get
			{
				this.AssertValid();
				return (long)((ulong)this.stat.st_uid);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004E3E File Offset: 0x0000303E
		public UnixGroupInfo OwnerGroup
		{
			get
			{
				this.AssertValid();
				return new UnixGroupInfo((long)((ulong)this.stat.st_gid));
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004E57 File Offset: 0x00003057
		public long OwnerGroupId
		{
			get
			{
				this.AssertValid();
				return (long)((ulong)this.stat.st_gid);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004E6B File Offset: 0x0000306B
		public long DeviceType
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_rdev);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004E83 File Offset: 0x00003083
		public long Length
		{
			get
			{
				this.AssertValid();
				return this.stat.st_size;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004E96 File Offset: 0x00003096
		public long BlockSize
		{
			get
			{
				this.AssertValid();
				return this.stat.st_blksize;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004EA9 File Offset: 0x000030A9
		public long BlocksAllocated
		{
			get
			{
				this.AssertValid();
				return this.stat.st_blocks;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004EBC File Offset: 0x000030BC
		public DateTime LastAccessTime
		{
			get
			{
				this.AssertValid();
				return NativeConvert.ToDateTime(this.stat.st_atime, this.stat.st_atime_nsec);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004EE0 File Offset: 0x000030E0
		public DateTime LastAccessTimeUtc
		{
			get
			{
				return this.LastAccessTime.ToUniversalTime();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004EFB File Offset: 0x000030FB
		public DateTime LastWriteTime
		{
			get
			{
				this.AssertValid();
				return NativeConvert.ToDateTime(this.stat.st_mtime, this.stat.st_mtime_nsec);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004F20 File Offset: 0x00003120
		public DateTime LastWriteTimeUtc
		{
			get
			{
				return this.LastWriteTime.ToUniversalTime();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004F3B File Offset: 0x0000313B
		public DateTime LastStatusChangeTime
		{
			get
			{
				this.AssertValid();
				return NativeConvert.ToDateTime(this.stat.st_ctime, this.stat.st_ctime_nsec);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004F60 File Offset: 0x00003160
		public DateTime LastStatusChangeTimeUtc
		{
			get
			{
				return this.LastStatusChangeTime.ToUniversalTime();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004F7B File Offset: 0x0000317B
		public bool IsDirectory
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFDIR);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004F98 File Offset: 0x00003198
		public bool IsCharacterDevice
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFCHR);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004FB5 File Offset: 0x000031B5
		public bool IsBlockDevice
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFBLK);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004FD2 File Offset: 0x000031D2
		public bool IsRegularFile
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFREG);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004FEF File Offset: 0x000031EF
		public bool IsFifo
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFIFO);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000500C File Offset: 0x0000320C
		public bool IsSymbolicLink
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFLNK);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005029 File Offset: 0x00003229
		public bool IsSocket
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFSOCK);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005046 File Offset: 0x00003246
		public bool IsSetUser
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsSet(this.stat.st_mode, FilePermissions.S_ISUID);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005063 File Offset: 0x00003263
		public bool IsSetGroup
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsSet(this.stat.st_mode, FilePermissions.S_ISGID);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005080 File Offset: 0x00003280
		public bool IsSticky
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsSet(this.stat.st_mode, FilePermissions.S_ISVTX);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000509D File Offset: 0x0000329D
		internal static bool IsFileType(FilePermissions mode, FilePermissions type)
		{
			return (mode & FilePermissions.S_IFMT) == type;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000050A9 File Offset: 0x000032A9
		internal static bool IsSet(FilePermissions mode, FilePermissions type)
		{
			return (mode & type) == type;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000050B1 File Offset: 0x000032B1
		[CLSCompliant(false)]
		public bool CanAccess(AccessModes mode)
		{
			return Syscall.access(this.FullPath, mode) == 0;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000050C2 File Offset: 0x000032C2
		public UnixFileSystemInfo CreateLink(string path)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.link(this.FullName, path));
			return UnixFileSystemInfo.GetFileSystemEntry(path);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000050DB File Offset: 0x000032DB
		public UnixSymbolicLinkInfo CreateSymbolicLink(string path)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.symlink(this.FullName, path));
			return new UnixSymbolicLinkInfo(path);
		}

		// Token: 0x0600010E RID: 270
		public abstract void Delete();

		// Token: 0x0600010F RID: 271 RVA: 0x000050F4 File Offset: 0x000032F4
		[CLSCompliant(false)]
		public long GetConfigurationValue(PathconfName name)
		{
			long num = Syscall.pathconf(this.FullPath, name);
			if (num == -1L && Stdlib.GetLastError() != (Errno)0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return num;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005113 File Offset: 0x00003313
		public void Refresh()
		{
			this.Refresh(true);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000511C File Offset: 0x0000331C
		internal void Refresh(bool force)
		{
			if (this.valid && !force)
			{
				return;
			}
			this.valid = this.GetFileStatus(this.FullPath, out this.stat);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005142 File Offset: 0x00003342
		protected virtual bool GetFileStatus(string path, out Stat stat)
		{
			return Syscall.stat(path, out stat) == 0;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005150 File Offset: 0x00003350
		public void SetLength(long length)
		{
			int num;
			do
			{
				num = Syscall.truncate(this.FullPath, length);
			}
			while (UnixMarshal.ShouldRetrySyscall(num));
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005178 File Offset: 0x00003378
		public virtual void SetOwner(long owner, long group)
		{
			uint num = Convert.ToUInt32(owner);
			uint num2 = Convert.ToUInt32(group);
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.chown(this.FullPath, num, num2));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000051A8 File Offset: 0x000033A8
		public void SetOwner(string owner)
		{
			Passwd passwd = Syscall.getpwnam(owner);
			if (passwd == null)
			{
				throw new ArgumentException(Locale.GetText("invalid username"), "owner");
			}
			uint pw_uid = passwd.pw_uid;
			uint pw_gid = passwd.pw_gid;
			this.SetOwner((long)((ulong)pw_uid), (long)((ulong)pw_gid));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000051F0 File Offset: 0x000033F0
		public void SetOwner(string owner, string group)
		{
			long num = -1L;
			if (owner != null)
			{
				num = new UnixUserInfo(owner).UserId;
			}
			long num2 = -1L;
			if (group != null)
			{
				num2 = new UnixGroupInfo(group).GroupId;
			}
			this.SetOwner(num, num2);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000522C File Offset: 0x0000342C
		public void SetOwner(UnixUserInfo owner)
		{
			long num2;
			long num = (num2 = -1L);
			if (owner != null)
			{
				num2 = owner.UserId;
				num = owner.GroupId;
			}
			this.SetOwner(num2, num);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005258 File Offset: 0x00003458
		public void SetOwner(UnixUserInfo owner, UnixGroupInfo group)
		{
			long num2;
			long num = (num2 = -1L);
			if (owner != null)
			{
				num2 = owner.UserId;
			}
			if (group != null)
			{
				num = owner.GroupId;
			}
			this.SetOwner(num2, num);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005286 File Offset: 0x00003486
		public override string ToString()
		{
			return this.FullPath;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000528E File Offset: 0x0000348E
		public Stat ToStat()
		{
			this.AssertValid();
			return this.stat;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000529C File Offset: 0x0000349C
		public static UnixFileSystemInfo GetFileSystemEntry(string path)
		{
			UnixFileSystemInfo unixFileSystemInfo;
			if (UnixFileSystemInfo.TryGetFileSystemEntry(path, out unixFileSystemInfo))
			{
				return unixFileSystemInfo;
			}
			UnixMarshal.ThrowExceptionForLastError();
			throw new DirectoryNotFoundException("UnixMarshal.ThrowExceptionForLastError didn't throw?!");
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000052C4 File Offset: 0x000034C4
		public static bool TryGetFileSystemEntry(string path, out UnixFileSystemInfo entry)
		{
			Stat stat;
			if (Syscall.lstat(path, out stat) != -1)
			{
				if (UnixFileSystemInfo.IsFileType(stat.st_mode, FilePermissions.S_IFDIR))
				{
					entry = new UnixDirectoryInfo(path, stat);
				}
				else if (UnixFileSystemInfo.IsFileType(stat.st_mode, FilePermissions.S_IFLNK))
				{
					entry = new UnixSymbolicLinkInfo(path, stat);
				}
				else
				{
					entry = new UnixFileInfo(path, stat);
				}
				return true;
			}
			if (Stdlib.GetLastError() == Errno.ENOENT)
			{
				entry = new UnixFileInfo(path);
				return true;
			}
			entry = null;
			return false;
		}

		// Token: 0x0400006F RID: 111
		private Stat stat;

		// Token: 0x04000070 RID: 112
		private string fullPath;

		// Token: 0x04000071 RID: 113
		private string originalPath;

		// Token: 0x04000072 RID: 114
		private bool valid;

		// Token: 0x04000073 RID: 115
		internal const FileSpecialAttributes AllSpecialAttributes = FileSpecialAttributes.SetUserId | FileSpecialAttributes.SetGroupId | FileSpecialAttributes.Sticky;

		// Token: 0x04000074 RID: 116
		internal const FileTypes AllFileTypes = (FileTypes)61440;
	}
}
