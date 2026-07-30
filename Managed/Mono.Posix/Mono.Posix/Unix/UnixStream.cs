using System;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000021 RID: 33
	public sealed class UnixStream : Stream, IDisposable
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00006BE5 File Offset: 0x00004DE5
		public UnixStream(int fileDescriptor)
			: this(fileDescriptor, true)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public UnixStream(int fileDescriptor, bool ownsHandle)
		{
			if (-1 == fileDescriptor)
			{
				throw new ArgumentException(Locale.GetText("Invalid file descriptor"), "fileDescriptor");
			}
			this.fileDescriptor = fileDescriptor;
			this.owner = ownsHandle;
			if (Syscall.lseek(fileDescriptor, 0L, SeekFlags.SEEK_CUR) != -1L)
			{
				this.canSeek = true;
			}
			if (Syscall.read(fileDescriptor, IntPtr.Zero, 0UL) != -1L)
			{
				this.canRead = true;
			}
			if (Syscall.write(fileDescriptor, IntPtr.Zero, 0UL) != -1L)
			{
				this.canWrite = true;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006C7C File Offset: 0x00004E7C
		private void AssertNotDisposed()
		{
			if (this.fileDescriptor == -1)
			{
				throw new ObjectDisposedException("Invalid File Descriptor");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006C92 File Offset: 0x00004E92
		public int Handle
		{
			get
			{
				return this.fileDescriptor;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006C9A File Offset: 0x00004E9A
		public override bool CanRead
		{
			get
			{
				return this.canRead;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00006CA2 File Offset: 0x00004EA2
		public override bool CanSeek
		{
			get
			{
				return this.canSeek;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006CAA File Offset: 0x00004EAA
		public override bool CanWrite
		{
			get
			{
				return this.canWrite;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006CB2 File Offset: 0x00004EB2
		public override long Length
		{
			get
			{
				this.AssertNotDisposed();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("File descriptor doesn't support seeking");
				}
				this.RefreshStat();
				return this.stat.st_size;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006CDE File Offset: 0x00004EDE
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006D11 File Offset: 0x00004F11
		public override long Position
		{
			get
			{
				this.AssertNotDisposed();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				long num = Syscall.lseek(this.fileDescriptor, 0L, SeekFlags.SEEK_CUR);
				if (num == -1L)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				return num;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006D1C File Offset: 0x00004F1C
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00006D2F File Offset: 0x00004F2F
		[CLSCompliant(false)]
		public FilePermissions Protection
		{
			get
			{
				this.RefreshStat();
				return this.stat.st_mode;
			}
			set
			{
				value &= ~FilePermissions.S_IFMT;
				UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.fchmod(this.fileDescriptor, value));
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006D4B File Offset: 0x00004F4B
		public FileTypes FileType
		{
			get
			{
				return (FileTypes)(this.Protection & FilePermissions.S_IFMT);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006D59 File Offset: 0x00004F59
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00006D68 File Offset: 0x00004F68
		public FileAccessPermissions FileAccessPermissions
		{
			get
			{
				return (FileAccessPermissions)(this.Protection & FilePermissions.ACCESSPERMS);
			}
			set
			{
				int num = (int)this.Protection;
				num &= -512;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006D8F File Offset: 0x00004F8F
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public FileSpecialAttributes FileSpecialAttributes
		{
			get
			{
				return (FileSpecialAttributes)(this.Protection & (FilePermissions.S_ISUID | FilePermissions.S_ISGID | FilePermissions.S_ISVTX));
			}
			set
			{
				int num = (int)this.Protection;
				num &= -3585;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00006DC7 File Offset: 0x00004FC7
		public UnixUserInfo OwnerUser
		{
			get
			{
				this.RefreshStat();
				return new UnixUserInfo(this.stat.st_uid);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006DDF File Offset: 0x00004FDF
		public long OwnerUserId
		{
			get
			{
				this.RefreshStat();
				return (long)((ulong)this.stat.st_uid);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00006DF3 File Offset: 0x00004FF3
		public UnixGroupInfo OwnerGroup
		{
			get
			{
				this.RefreshStat();
				return new UnixGroupInfo((long)((ulong)this.stat.st_gid));
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006E0C File Offset: 0x0000500C
		public long OwnerGroupId
		{
			get
			{
				this.RefreshStat();
				return (long)((ulong)this.stat.st_gid);
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006E20 File Offset: 0x00005020
		private void RefreshStat()
		{
			this.AssertNotDisposed();
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.fstat(this.fileDescriptor, out this.stat));
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006E3E File Offset: 0x0000503E
		public void AdviseFileAccessPattern(FileAccessPattern pattern, long offset, long len)
		{
			FileHandleOperations.AdviseFileAccessPattern(this.fileDescriptor, pattern, offset, len);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006E4E File Offset: 0x0000504E
		public void AdviseFileAccessPattern(FileAccessPattern pattern)
		{
			this.AdviseFileAccessPattern(pattern, 0L, 0L);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006E5B File Offset: 0x0000505B
		public override void Flush()
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006E60 File Offset: 0x00005060
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (buffer.Length == 0)
			{
				return 0;
			}
			long num;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				do
				{
					num = Syscall.read(this.fileDescriptor, (void*)ptr2, (ulong)((long)count));
				}
				while (UnixMarshal.ShouldRetrySyscall((int)num));
			}
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return (int)num;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006ECC File Offset: 0x000050CC
		private void AssertValidBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (offset > buffer.Length - count)
			{
				throw new ArgumentException("would overrun buffer");
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006F34 File Offset: 0x00005134
		public unsafe int ReadAtOffset([In] [Out] byte[] buffer, int offset, int count, long fileOffset)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (buffer.Length == 0)
			{
				return 0;
			}
			long num;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				do
				{
					num = Syscall.pread(this.fileDescriptor, (void*)ptr2, (ulong)((long)count), fileOffset);
				}
				while (UnixMarshal.ShouldRetrySyscall((int)num));
			}
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return (int)num;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006FA0 File Offset: 0x000051A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.AssertNotDisposed();
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The File Descriptor does not support seeking");
			}
			SeekFlags seekFlags = SeekFlags.SEEK_CUR;
			switch (origin)
			{
			case SeekOrigin.Begin:
				seekFlags = SeekFlags.SEEK_SET;
				break;
			case SeekOrigin.Current:
				seekFlags = SeekFlags.SEEK_CUR;
				break;
			case SeekOrigin.End:
				seekFlags = SeekFlags.SEEK_END;
				break;
			}
			long num = Syscall.lseek(this.fileDescriptor, offset, seekFlags);
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return num;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007000 File Offset: 0x00005200
		public override void SetLength(long value)
		{
			this.AssertNotDisposed();
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", "< 0");
			}
			if (!this.CanSeek && !this.CanWrite)
			{
				throw new NotSupportedException("You can't truncating the current file descriptor");
			}
			int num;
			do
			{
				num = Syscall.ftruncate(this.fileDescriptor, value);
			}
			while (UnixMarshal.ShouldRetrySyscall(num));
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007060 File Offset: 0x00005260
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanWrite)
			{
				throw new NotSupportedException("File Descriptor does not support writing");
			}
			if (buffer.Length == 0)
			{
				return;
			}
			long num;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				do
				{
					num = Syscall.write(this.fileDescriptor, (void*)ptr2, (ulong)((long)count));
				}
				while (UnixMarshal.ShouldRetrySyscall((int)num));
			}
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000070C8 File Offset: 0x000052C8
		public unsafe void WriteAtOffset(byte[] buffer, int offset, int count, long fileOffset)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanWrite)
			{
				throw new NotSupportedException("File Descriptor does not support writing");
			}
			if (buffer.Length == 0)
			{
				return;
			}
			long num;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				do
				{
					num = Syscall.pwrite(this.fileDescriptor, (void*)ptr2, (ulong)((long)count), fileOffset);
				}
				while (UnixMarshal.ShouldRetrySyscall((int)num));
			}
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007131 File Offset: 0x00005331
		public void SendTo(UnixStream output)
		{
			this.SendTo(output, (ulong)output.Length);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007140 File Offset: 0x00005340
		[CLSCompliant(false)]
		public void SendTo(UnixStream output, ulong count)
		{
			this.SendTo(output.Handle, count);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007150 File Offset: 0x00005350
		[CLSCompliant(false)]
		public void SendTo(int out_fd, ulong count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Unable to write to the current file descriptor");
			}
			long position = this.Position;
			if (Syscall.sendfile(out_fd, this.fileDescriptor, ref position, count) == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000718F File Offset: 0x0000538F
		public void SetOwner(long user, long group)
		{
			this.AssertNotDisposed();
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.fchown(this.fileDescriptor, Convert.ToUInt32(user), Convert.ToUInt32(group)));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000071B4 File Offset: 0x000053B4
		public void SetOwner(string user, string group)
		{
			this.AssertNotDisposed();
			long userId = new UnixUserInfo(user).UserId;
			long groupId = new UnixGroupInfo(group).GroupId;
			this.SetOwner(userId, groupId);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000071E8 File Offset: 0x000053E8
		public void SetOwner(string user)
		{
			this.AssertNotDisposed();
			Passwd passwd = Syscall.getpwnam(user);
			if (passwd == null)
			{
				throw new ArgumentException(Locale.GetText("invalid username"), "user");
			}
			long num = (long)((ulong)passwd.pw_uid);
			long num2 = (long)((ulong)passwd.pw_gid);
			this.SetOwner(num, num2);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007236 File Offset: 0x00005436
		[CLSCompliant(false)]
		public long GetConfigurationValue(PathconfName name)
		{
			this.AssertNotDisposed();
			long num = Syscall.fpathconf(this.fileDescriptor, name);
			if (num == -1L && Stdlib.GetLastError() != (Errno)0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return num;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000725C File Offset: 0x0000545C
		~UnixStream()
		{
			this.Close();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007288 File Offset: 0x00005488
		public override void Close()
		{
			if (this.fileDescriptor == -1)
			{
				return;
			}
			this.Flush();
			if (!this.owner)
			{
				return;
			}
			int num;
			do
			{
				num = Syscall.close(this.fileDescriptor);
			}
			while (UnixMarshal.ShouldRetrySyscall(num));
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
			this.fileDescriptor = -1;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000072D5 File Offset: 0x000054D5
		void IDisposable.Dispose()
		{
			if (this.fileDescriptor != -1 && this.owner)
			{
				this.Close();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000087 RID: 135
		public const int InvalidFileDescriptor = -1;

		// Token: 0x04000088 RID: 136
		public const int StandardInputFileDescriptor = 0;

		// Token: 0x04000089 RID: 137
		public const int StandardOutputFileDescriptor = 1;

		// Token: 0x0400008A RID: 138
		public const int StandardErrorFileDescriptor = 2;

		// Token: 0x0400008B RID: 139
		private bool canSeek;

		// Token: 0x0400008C RID: 140
		private bool canRead;

		// Token: 0x0400008D RID: 141
		private bool canWrite;

		// Token: 0x0400008E RID: 142
		private bool owner = true;

		// Token: 0x0400008F RID: 143
		private int fileDescriptor = -1;

		// Token: 0x04000090 RID: 144
		private Stat stat;
	}
}
