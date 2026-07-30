using System;
using System.Collections;
using System.IO;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000012 RID: 18
	public sealed class UnixDriveInfo
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003420 File Offset: 0x00001620
		public UnixDriveInfo(string mountPoint)
		{
			if (mountPoint == null)
			{
				throw new ArgumentNullException("mountPoint");
			}
			Fstab fstab = Syscall.getfsfile(mountPoint);
			if (fstab != null)
			{
				this.FromFstab(fstab);
				return;
			}
			this.mount_point = mountPoint;
			this.block_device = "";
			this.fstype = "Unknown";
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003476 File Offset: 0x00001676
		private void FromFstab(Fstab fstab)
		{
			this.fstype = fstab.fs_vfstype;
			this.mount_point = fstab.fs_file;
			this.block_device = fstab.fs_spec;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000349C File Offset: 0x0000169C
		public static UnixDriveInfo GetForSpecialFile(string specialFile)
		{
			if (specialFile == null)
			{
				throw new ArgumentNullException("specialFile");
			}
			Fstab fstab = Syscall.getfsspec(specialFile);
			if (fstab == null)
			{
				throw new ArgumentException("specialFile isn't valid: " + specialFile);
			}
			return new UnixDriveInfo(fstab);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000034D1 File Offset: 0x000016D1
		private UnixDriveInfo(Fstab fstab)
		{
			this.FromFstab(fstab);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000034E0 File Offset: 0x000016E0
		public long AvailableFreeSpace
		{
			get
			{
				this.Refresh();
				return Convert.ToInt64(this.stat.f_bavail * this.stat.f_frsize);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003504 File Offset: 0x00001704
		public string DriveFormat
		{
			get
			{
				return this.fstype;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000350C File Offset: 0x0000170C
		public UnixDriveType DriveType
		{
			get
			{
				return UnixDriveType.Unknown;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003510 File Offset: 0x00001710
		public bool IsReady
		{
			get
			{
				bool flag = this.Refresh(false);
				if (this.mount_point == "/" || !flag)
				{
					return flag;
				}
				Statvfs statvfs;
				return Syscall.statvfs(this.RootDirectory.Parent.FullName, out statvfs) == 0 && statvfs.f_fsid != this.stat.f_fsid;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000356D File Offset: 0x0000176D
		public string Name
		{
			get
			{
				return this.mount_point;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003575 File Offset: 0x00001775
		public UnixDirectoryInfo RootDirectory
		{
			get
			{
				return new UnixDirectoryInfo(this.mount_point);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003582 File Offset: 0x00001782
		public long TotalFreeSpace
		{
			get
			{
				this.Refresh();
				return (long)(this.stat.f_bfree * this.stat.f_frsize);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000035A1 File Offset: 0x000017A1
		public long TotalSize
		{
			get
			{
				this.Refresh();
				return (long)(this.stat.f_frsize * this.stat.f_blocks);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000035C0 File Offset: 0x000017C0
		public string VolumeLabel
		{
			get
			{
				return this.block_device;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000035C8 File Offset: 0x000017C8
		public long MaximumFilenameLength
		{
			get
			{
				this.Refresh();
				return Convert.ToInt64(this.stat.f_namemax);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000035E0 File Offset: 0x000017E0
		public static UnixDriveInfo[] GetDrives()
		{
			ArrayList arrayList = new ArrayList();
			object fstab_lock = Syscall.fstab_lock;
			lock (fstab_lock)
			{
				if (Syscall.setfsent() != 1)
				{
					throw new IOException("Error calling setfsent(3)", new UnixIOException());
				}
				try
				{
					Fstab fstab;
					while ((fstab = Syscall.getfsent()) != null)
					{
						if (fstab.fs_file != null && fstab.fs_file.StartsWith("/"))
						{
							arrayList.Add(new UnixDriveInfo(fstab));
						}
					}
				}
				finally
				{
					Syscall.endfsent();
				}
			}
			return (UnixDriveInfo[])arrayList.ToArray(typeof(UnixDriveInfo));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003698 File Offset: 0x00001898
		public override string ToString()
		{
			return this.VolumeLabel;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000036A0 File Offset: 0x000018A0
		private void Refresh()
		{
			this.Refresh(true);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036AC File Offset: 0x000018AC
		private bool Refresh(bool throwException)
		{
			int num = Syscall.statvfs(this.mount_point, out this.stat);
			if (num == -1 && throwException)
			{
				Errno lastError = Stdlib.GetLastError();
				throw new InvalidOperationException(UnixMarshal.GetErrorDescription(lastError), new UnixIOException(lastError));
			}
			return num != -1;
		}

		// Token: 0x04000068 RID: 104
		private Statvfs stat;

		// Token: 0x04000069 RID: 105
		private string fstype;

		// Token: 0x0400006A RID: 106
		private string mount_point;

		// Token: 0x0400006B RID: 107
		private string block_device;
	}
}
