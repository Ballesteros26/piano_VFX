using System;
using System.IO;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000016 RID: 22
	public sealed class UnixFileInfo : UnixFileSystemInfo
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public UnixFileInfo(string path)
			: base(path)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004AD9 File Offset: 0x00002CD9
		internal UnixFileInfo(string path, Stat stat)
			: base(path, stat)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004AE3 File Offset: 0x00002CE3
		public override string Name
		{
			get
			{
				return UnixPath.GetFileName(base.FullPath);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public string DirectoryName
		{
			get
			{
				return UnixPath.GetDirectoryName(base.FullPath);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004AFD File Offset: 0x00002CFD
		public UnixDirectoryInfo Directory
		{
			get
			{
				return new UnixDirectoryInfo(this.DirectoryName);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004B0A File Offset: 0x00002D0A
		public override void Delete()
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.unlink(base.FullPath));
			base.Refresh();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004B24 File Offset: 0x00002D24
		public UnixStream Create()
		{
			FilePermissions filePermissions = FilePermissions.S_IRUSR | FilePermissions.S_IWUSR | FilePermissions.S_IRGRP | FilePermissions.S_IROTH;
			return this.Create(filePermissions);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004B3E File Offset: 0x00002D3E
		[CLSCompliant(false)]
		public UnixStream Create(FilePermissions mode)
		{
			int num = Syscall.creat(base.FullPath, mode);
			if (num < 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			base.Refresh();
			return new UnixStream(num);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004B60 File Offset: 0x00002D60
		public UnixStream Create(FileAccessPermissions mode)
		{
			return this.Create((FilePermissions)mode);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004B69 File Offset: 0x00002D69
		[CLSCompliant(false)]
		public UnixStream Open(OpenFlags flags)
		{
			if ((flags & OpenFlags.O_CREAT) != OpenFlags.O_RDONLY)
			{
				throw new ArgumentException("Cannot specify OpenFlags.O_CREAT without providing FilePermissions.  Use the Open(OpenFlags, FilePermissions) method instead");
			}
			int num = Syscall.open(base.FullPath, flags);
			if (num < 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return new UnixStream(num);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004B96 File Offset: 0x00002D96
		[CLSCompliant(false)]
		public UnixStream Open(OpenFlags flags, FilePermissions mode)
		{
			int num = Syscall.open(base.FullPath, flags, mode);
			if (num < 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return new UnixStream(num);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public UnixStream Open(FileMode mode)
		{
			OpenFlags openFlags = NativeConvert.ToOpenFlags(mode, FileAccess.ReadWrite);
			return this.Open(openFlags);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public UnixStream Open(FileMode mode, FileAccess access)
		{
			OpenFlags openFlags = NativeConvert.ToOpenFlags(mode, access);
			return this.Open(openFlags);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004BEC File Offset: 0x00002DEC
		[CLSCompliant(false)]
		public UnixStream Open(FileMode mode, FileAccess access, FilePermissions perms)
		{
			OpenFlags openFlags = NativeConvert.ToOpenFlags(mode, access);
			int num = Syscall.open(base.FullPath, openFlags, perms);
			if (num < 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return new UnixStream(num);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004C1C File Offset: 0x00002E1C
		public UnixStream OpenRead()
		{
			return this.Open(FileMode.Open, FileAccess.Read);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004C26 File Offset: 0x00002E26
		public UnixStream OpenWrite()
		{
			return this.Open(FileMode.OpenOrCreate, FileAccess.Write);
		}
	}
}
