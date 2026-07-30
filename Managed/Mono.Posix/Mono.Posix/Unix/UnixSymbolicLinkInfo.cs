using System;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000022 RID: 34
	public sealed class UnixSymbolicLinkInfo : UnixFileSystemInfo
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x000072F4 File Offset: 0x000054F4
		public UnixSymbolicLinkInfo(string path)
			: base(path)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000072FD File Offset: 0x000054FD
		internal UnixSymbolicLinkInfo(string path, Stat stat)
			: base(path, stat)
		{
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007307 File Offset: 0x00005507
		public override string Name
		{
			get
			{
				return UnixPath.GetFileName(base.FullPath);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007314 File Offset: 0x00005514
		[Obsolete("Use GetContents()")]
		public UnixFileSystemInfo Contents
		{
			get
			{
				return this.GetContents();
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000731C File Offset: 0x0000551C
		public string ContentsPath
		{
			get
			{
				return UnixPath.ReadLink(base.FullPath);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007329 File Offset: 0x00005529
		public bool HasContents
		{
			get
			{
				return UnixPath.TryReadLink(base.FullPath) != null;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007339 File Offset: 0x00005539
		public UnixFileSystemInfo GetContents()
		{
			return UnixFileSystemInfo.GetFileSystemEntry(UnixPath.Combine(UnixPath.GetDirectoryName(base.FullPath), new string[] { this.ContentsPath }));
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000735F File Offset: 0x0000555F
		public void CreateSymbolicLinkTo(string path)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.symlink(path, this.FullName));
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007372 File Offset: 0x00005572
		public void CreateSymbolicLinkTo(UnixFileSystemInfo path)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.symlink(path.FullName, this.FullName));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000738A File Offset: 0x0000558A
		public override void Delete()
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.unlink(base.FullPath));
			base.Refresh();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000073A2 File Offset: 0x000055A2
		public override void SetOwner(long owner, long group)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.lchown(base.FullPath, Convert.ToUInt32(owner), Convert.ToUInt32(group)));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000073C0 File Offset: 0x000055C0
		protected override bool GetFileStatus(string path, out Stat stat)
		{
			return Syscall.lstat(path, out stat) == 0;
		}
	}
}
