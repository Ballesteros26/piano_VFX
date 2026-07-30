using System;
using System.IO;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000009 RID: 9
	public sealed class FileHandleOperations
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002388 File Offset: 0x00000588
		private FileHandleOperations()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002390 File Offset: 0x00000590
		public static void AdviseFileAccessPattern(int fd, FileAccessPattern pattern, long offset, long len)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.posix_fadvise(fd, offset, len, (PosixFadviseAdvice)pattern));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023A0 File Offset: 0x000005A0
		public static void AdviseFileAccessPattern(int fd, FileAccessPattern pattern)
		{
			FileHandleOperations.AdviseFileAccessPattern(fd, pattern, 0L, 0L);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023B0 File Offset: 0x000005B0
		public static void AdviseFileAccessPattern(FileStream file, FileAccessPattern pattern, long offset, long len)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.posix_fadvise(file.Handle.ToInt32(), offset, len, (PosixFadviseAdvice)pattern));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023E6 File Offset: 0x000005E6
		public static void AdviseFileAccessPattern(FileStream file, FileAccessPattern pattern)
		{
			FileHandleOperations.AdviseFileAccessPattern(file, pattern, 0L, 0L);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023F3 File Offset: 0x000005F3
		public static void AdviseFileAccessPattern(UnixStream stream, FileAccessPattern pattern, long offset, long len)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.posix_fadvise(stream.Handle, offset, len, (PosixFadviseAdvice)pattern));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002416 File Offset: 0x00000616
		public static void AdviseFileAccessPattern(UnixStream stream, FileAccessPattern pattern)
		{
			FileHandleOperations.AdviseFileAccessPattern(stream, pattern, 0L, 0L);
		}
	}
}
