using System;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200000E RID: 14
	public class StdioFileStream : Stream
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002486 File Offset: 0x00000686
		public StdioFileStream(IntPtr fileStream)
			: this(fileStream, true)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002490 File Offset: 0x00000690
		public StdioFileStream(IntPtr fileStream, bool ownsHandle)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			this.InitStream(fileStream, ownsHandle);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024B2 File Offset: 0x000006B2
		public StdioFileStream(IntPtr fileStream, FileAccess access)
			: this(fileStream, access, true)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024BD File Offset: 0x000006BD
		public StdioFileStream(IntPtr fileStream, FileAccess access, bool ownsHandle)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			this.InitStream(fileStream, ownsHandle);
			this.InitCanReadWrite(access);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024E6 File Offset: 0x000006E6
		public StdioFileStream(string path)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.InitStream(StdioFileStream.Fopen(path, "rb"), true);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002520 File Offset: 0x00000720
		public StdioFileStream(string path, string mode)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.InitStream(StdioFileStream.Fopen(path, mode), true);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002556 File Offset: 0x00000756
		public StdioFileStream(string path, FileMode mode)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.InitStream(StdioFileStream.Fopen(path, StdioFileStream.ToFopenMode(path, mode)), true);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002594 File Offset: 0x00000794
		public StdioFileStream(string path, FileAccess access)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.InitStream(StdioFileStream.Fopen(path, StdioFileStream.ToFopenMode(path, access)), true);
			this.InitCanReadWrite(access);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025E4 File Offset: 0x000007E4
		public StdioFileStream(string path, FileMode mode, FileAccess access)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.InitStream(StdioFileStream.Fopen(path, StdioFileStream.ToFopenMode(path, mode, access)), true);
			this.InitCanReadWrite(access);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002634 File Offset: 0x00000834
		private static IntPtr Fopen(string path, string mode)
		{
			if (path.Length == 0)
			{
				throw new ArgumentException("path");
			}
			if (mode == null)
			{
				throw new ArgumentNullException("mode");
			}
			IntPtr intPtr = Stdlib.fopen(path, mode);
			if (intPtr == IntPtr.Zero)
			{
				throw new DirectoryNotFoundException("path", UnixMarshal.CreateExceptionForLastError());
			}
			return intPtr;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002688 File Offset: 0x00000888
		private void InitStream(IntPtr fileStream, bool ownsHandle)
		{
			if (StdioFileStream.InvalidFileStream == fileStream)
			{
				throw new ArgumentException(Locale.GetText("Invalid file stream"), "fileStream");
			}
			this.file = fileStream;
			this.owner = ownsHandle;
			try
			{
				if ((long)Stdlib.fseek(this.file, 0L, SeekFlags.SEEK_CUR) != -1L)
				{
					this.canSeek = true;
				}
				Stdlib.fread(IntPtr.Zero, 0UL, 0UL, this.file);
				if (Stdlib.ferror(this.file) == 0)
				{
					this.canRead = true;
				}
				Stdlib.fwrite(IntPtr.Zero, 0UL, 0UL, this.file);
				if (Stdlib.ferror(this.file) == 0)
				{
					this.canWrite = true;
				}
				Stdlib.clearerr(this.file);
			}
			catch (Exception)
			{
				throw new ArgumentException(Locale.GetText("Invalid file stream"), "fileStream");
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000276C File Offset: 0x0000096C
		private void InitCanReadWrite(FileAccess access)
		{
			this.canRead = this.canRead && (access == FileAccess.Read || access == FileAccess.ReadWrite);
			this.canWrite = this.canWrite && (access == FileAccess.Write || access == FileAccess.ReadWrite);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027A6 File Offset: 0x000009A6
		private static string ToFopenMode(string file, FileMode mode)
		{
			string text = NativeConvert.ToFopenMode(mode);
			StdioFileStream.AssertFileMode(file, mode);
			return text;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027B6 File Offset: 0x000009B6
		private static string ToFopenMode(string file, FileAccess access)
		{
			return NativeConvert.ToFopenMode(access);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027C0 File Offset: 0x000009C0
		private static string ToFopenMode(string file, FileMode mode, FileAccess access)
		{
			string text = NativeConvert.ToFopenMode(mode, access);
			bool flag = StdioFileStream.AssertFileMode(file, mode);
			if (mode == FileMode.OpenOrCreate && access == FileAccess.Read && !flag)
			{
				text = "w+b";
			}
			return text;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027F0 File Offset: 0x000009F0
		private static bool AssertFileMode(string file, FileMode mode)
		{
			bool flag = StdioFileStream.FileExists(file);
			if (mode == FileMode.CreateNew && flag)
			{
				throw new IOException("File exists and FileMode.CreateNew specified");
			}
			if ((mode == FileMode.Open || mode == FileMode.Truncate) && !flag)
			{
				throw new FileNotFoundException("File doesn't exist and FileMode.Open specified", file);
			}
			return flag;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002830 File Offset: 0x00000A30
		private static bool FileExists(string file)
		{
			IntPtr intPtr = Stdlib.fopen(file, "r");
			bool flag = intPtr != IntPtr.Zero;
			if (intPtr != IntPtr.Zero)
			{
				Stdlib.fclose(intPtr);
			}
			return flag;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002868 File Offset: 0x00000A68
		private void AssertNotDisposed()
		{
			if (this.file == StdioFileStream.InvalidFileStream)
			{
				throw new ObjectDisposedException("Invalid File Stream");
			}
			GC.KeepAlive(this);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000288D File Offset: 0x00000A8D
		public IntPtr Handle
		{
			get
			{
				this.AssertNotDisposed();
				GC.KeepAlive(this);
				return this.file;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000028A1 File Offset: 0x00000AA1
		public override bool CanRead
		{
			get
			{
				return this.canRead;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000028A9 File Offset: 0x00000AA9
		public override bool CanSeek
		{
			get
			{
				return this.canSeek;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000028B1 File Offset: 0x00000AB1
		public override bool CanWrite
		{
			get
			{
				return this.canWrite;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000028BC File Offset: 0x00000ABC
		public override long Length
		{
			get
			{
				this.AssertNotDisposed();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("File Stream doesn't support seeking");
				}
				long num = Stdlib.ftell(this.file);
				if (num == -1L)
				{
					throw new NotSupportedException("Unable to obtain current file position");
				}
				UnixMarshal.ThrowExceptionForLastErrorIf(Stdlib.fseek(this.file, 0L, SeekFlags.SEEK_END));
				long num2 = Stdlib.ftell(this.file);
				if (num2 == -1L)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				UnixMarshal.ThrowExceptionForLastErrorIf(Stdlib.fseek(this.file, num, SeekFlags.SEEK_SET));
				GC.KeepAlive(this);
				return num2;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000293E File Offset: 0x00000B3E
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002974 File Offset: 0x00000B74
		public override long Position
		{
			get
			{
				this.AssertNotDisposed();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				long num = Stdlib.ftell(this.file);
				if (num == -1L)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				GC.KeepAlive(this);
				return num;
			}
			set
			{
				this.AssertNotDisposed();
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002985 File Offset: 0x00000B85
		public void SaveFilePosition(FilePosition pos)
		{
			this.AssertNotDisposed();
			UnixMarshal.ThrowExceptionForLastErrorIf(Stdlib.fgetpos(this.file, pos));
			GC.KeepAlive(this);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void RestoreFilePosition(FilePosition pos)
		{
			this.AssertNotDisposed();
			if (pos == null)
			{
				throw new ArgumentNullException("value");
			}
			UnixMarshal.ThrowExceptionForLastErrorIf(Stdlib.fsetpos(this.file, pos));
			GC.KeepAlive(this);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029D7 File Offset: 0x00000BD7
		public override void Flush()
		{
			this.AssertNotDisposed();
			if (Stdlib.fflush(this.file) != 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029F8 File Offset: 0x00000BF8
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			ulong num;
			fixed (byte* ptr = &buffer[offset])
			{
				num = Stdlib.fread((void*)ptr, 1UL, (ulong)((long)count), this.file);
			}
			if (num != (ulong)((long)count) && Stdlib.ferror(this.file) != 0)
			{
				throw new IOException();
			}
			GC.KeepAlive(this);
			return (int)num;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A64 File Offset: 0x00000C64
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

		// Token: 0x06000045 RID: 69 RVA: 0x00002ACB File Offset: 0x00000CCB
		public void Rewind()
		{
			this.AssertNotDisposed();
			Stdlib.rewind(this.file);
			GC.KeepAlive(this);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.AssertNotDisposed();
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The File Stream does not support seeking");
			}
			SeekFlags seekFlags;
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
			default:
				throw new ArgumentException("origin");
			}
			if (Stdlib.fseek(this.file, offset, seekFlags) != 0)
			{
				throw new IOException("Unable to seek", UnixMarshal.CreateExceptionForLastError());
			}
			long num = Stdlib.ftell(this.file);
			if (num == -1L)
			{
				throw new IOException("Unable to get current file position", UnixMarshal.CreateExceptionForLastError());
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B80 File Offset: 0x00000D80
		public override void SetLength(long value)
		{
			throw new NotSupportedException("ANSI C doesn't provide a way to truncate a file");
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B8C File Offset: 0x00000D8C
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanWrite)
			{
				throw new NotSupportedException("File Stream does not support writing");
			}
			long num;
			fixed (byte* ptr = &buffer[offset])
			{
				num = (long)Stdlib.fwrite((void*)ptr, 1UL, (ulong)((long)count), this.file);
			}
			if (num != (long)count)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BE8 File Offset: 0x00000DE8
		~StdioFileStream()
		{
			this.Close();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C14 File Offset: 0x00000E14
		public override void Close()
		{
			if (this.file == StdioFileStream.InvalidFileStream)
			{
				return;
			}
			if (this.owner)
			{
				if (Stdlib.fclose(this.file) != 0)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
			}
			else
			{
				this.Flush();
			}
			this.file = StdioFileStream.InvalidFileStream;
			this.canRead = false;
			this.canSeek = false;
			this.canWrite = false;
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x04000054 RID: 84
		public static readonly IntPtr InvalidFileStream = IntPtr.Zero;

		// Token: 0x04000055 RID: 85
		public static readonly IntPtr StandardInput = Stdlib.stdin;

		// Token: 0x04000056 RID: 86
		public static readonly IntPtr StandardOutput = Stdlib.stdout;

		// Token: 0x04000057 RID: 87
		public static readonly IntPtr StandardError = Stdlib.stderr;

		// Token: 0x04000058 RID: 88
		private bool canSeek;

		// Token: 0x04000059 RID: 89
		private bool canRead;

		// Token: 0x0400005A RID: 90
		private bool canWrite;

		// Token: 0x0400005B RID: 91
		private bool owner;

		// Token: 0x0400005C RID: 92
		private IntPtr file;
	}
}
