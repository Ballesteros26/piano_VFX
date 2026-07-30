using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001C RID: 28
	public sealed class UnixMarshal
	{
		// Token: 0x06000149 RID: 329 RVA: 0x000059C2 File Offset: 0x00003BC2
		private UnixMarshal()
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000059CA File Offset: 0x00003BCA
		[CLSCompliant(false)]
		public static string GetErrorDescription(Errno errno)
		{
			return ErrorMarshal.Translate(errno);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000059D7 File Offset: 0x00003BD7
		public static IntPtr AllocHeap(long size)
		{
			if (size < 0L)
			{
				throw new ArgumentOutOfRangeException("size", "< 0");
			}
			return Stdlib.malloc((ulong)size);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000059F4 File Offset: 0x00003BF4
		public static IntPtr ReAllocHeap(IntPtr ptr, long size)
		{
			if (size < 0L)
			{
				throw new ArgumentOutOfRangeException("size", "< 0");
			}
			return Stdlib.realloc(ptr, (ulong)size);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005A12 File Offset: 0x00003C12
		public static void FreeHeap(IntPtr ptr)
		{
			Stdlib.free(ptr);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005A1C File Offset: 0x00003C1C
		public unsafe static string PtrToStringUnix(IntPtr p)
		{
			if (p == IntPtr.Zero)
			{
				return null;
			}
			int num = checked((int)Stdlib.strlen(p));
			return new string((sbyte*)(void*)p, 0, num, UnixEncoding.Instance);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005A52 File Offset: 0x00003C52
		public static string PtrToString(IntPtr p)
		{
			if (p == IntPtr.Zero)
			{
				return null;
			}
			return UnixMarshal.PtrToString(p, UnixEncoding.Instance);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005A70 File Offset: 0x00003C70
		public unsafe static string PtrToString(IntPtr p, Encoding encoding)
		{
			if (p == IntPtr.Zero)
			{
				return null;
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			int num = UnixMarshal.GetStringByteLength(p, encoding);
			string text = new string((sbyte*)(void*)p, 0, num, encoding);
			num = text.Length;
			while (num > 0 && text[num - 1] == '\0')
			{
				num--;
			}
			if (num == text.Length)
			{
				return text;
			}
			return text.Substring(0, num);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005AE0 File Offset: 0x00003CE0
		private static int GetStringByteLength(IntPtr p, Encoding encoding)
		{
			Type type = encoding.GetType();
			int num;
			if (typeof(UTF8Encoding).IsAssignableFrom(type) || typeof(UTF7Encoding).IsAssignableFrom(type) || typeof(UnixEncoding).IsAssignableFrom(type) || typeof(ASCIIEncoding).IsAssignableFrom(type))
			{
				num = checked((int)Stdlib.strlen(p));
			}
			else if (typeof(UnicodeEncoding).IsAssignableFrom(type))
			{
				num = UnixMarshal.GetInt16BufferLength(p);
			}
			else if (typeof(UTF32Encoding).IsAssignableFrom(type))
			{
				num = UnixMarshal.GetInt32BufferLength(p);
			}
			else
			{
				num = UnixMarshal.GetRandomBufferLength(p, encoding.GetMaxByteCount(1));
			}
			if (num == -1)
			{
				throw new NotSupportedException("Unable to determine native string buffer length");
			}
			return num;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005B9C File Offset: 0x00003D9C
		private static int GetInt16BufferLength(IntPtr p)
		{
			int num = 0;
			checked
			{
				while (Marshal.ReadInt16(p, unchecked(num * 2)) != 0)
				{
					num++;
				}
				return num * 2;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005BC0 File Offset: 0x00003DC0
		private static int GetInt32BufferLength(IntPtr p)
		{
			int num = 0;
			checked
			{
				while (Marshal.ReadInt32(p, unchecked(num * 4)) != 0)
				{
					num++;
				}
				return num * 4;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005BE4 File Offset: 0x00003DE4
		private static int GetRandomBufferLength(IntPtr p, int nullLength)
		{
			switch (nullLength)
			{
			case 1:
				return checked((int)Stdlib.strlen(p));
			case 2:
				return UnixMarshal.GetInt16BufferLength(p);
			case 4:
				return UnixMarshal.GetInt32BufferLength(p);
			}
			int num = 0;
			int num2 = 0;
			do
			{
				if (Marshal.ReadByte(p, num++) == 0)
				{
					num2++;
				}
				else
				{
					num2 = 0;
				}
			}
			while (num2 != nullLength);
			return num;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005C3F File Offset: 0x00003E3F
		public static string[] PtrToStringArray(IntPtr stringArray)
		{
			return UnixMarshal.PtrToStringArray(stringArray, UnixEncoding.Instance);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005C4C File Offset: 0x00003E4C
		public static string[] PtrToStringArray(IntPtr stringArray, Encoding encoding)
		{
			if (stringArray == IntPtr.Zero)
			{
				return new string[0];
			}
			return UnixMarshal.PtrToStringArray(UnixMarshal.CountStrings(stringArray), stringArray, encoding);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005C70 File Offset: 0x00003E70
		private static int CountStrings(IntPtr stringArray)
		{
			int num = 0;
			while (Marshal.ReadIntPtr(stringArray, num * IntPtr.Size) != IntPtr.Zero)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005C9F File Offset: 0x00003E9F
		public static string[] PtrToStringArray(int count, IntPtr stringArray)
		{
			return UnixMarshal.PtrToStringArray(count, stringArray, UnixEncoding.Instance);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005CB0 File Offset: 0x00003EB0
		public static string[] PtrToStringArray(int count, IntPtr stringArray, Encoding encoding)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (stringArray == IntPtr.Zero)
			{
				return new string[count];
			}
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(stringArray, i * IntPtr.Size);
				array[i] = UnixMarshal.PtrToString(intPtr, encoding);
			}
			return array;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005D1F File Offset: 0x00003F1F
		public static IntPtr StringToHeap(string s)
		{
			return UnixMarshal.StringToHeap(s, UnixEncoding.Instance);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005D2C File Offset: 0x00003F2C
		public static IntPtr StringToHeap(string s, Encoding encoding)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			return UnixMarshal.StringToHeap(s, 0, s.Length, encoding);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005D45 File Offset: 0x00003F45
		public static IntPtr StringToHeap(string s, int index, int count)
		{
			return UnixMarshal.StringToHeap(s, index, count, UnixEncoding.Instance);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005D54 File Offset: 0x00003F54
		public unsafe static IntPtr StringToHeap(string s, int index, int count, Encoding encoding)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non - negative number required.");
			}
			if (s.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("s", "Index and count must refer to a location within the string.");
			}
			int maxByteCount = encoding.GetMaxByteCount(1);
			int byteCount = encoding.GetByteCount(s);
			int num = checked(byteCount + maxByteCount);
			IntPtr intPtr = UnixMarshal.AllocHeap((long)num);
			if (intPtr == IntPtr.Zero)
			{
				throw new UnixIOException(Errno.ENOMEM);
			}
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				byte* ptr2 = (byte*)(void*)intPtr;
				int bytes;
				try
				{
					bytes = encoding.GetBytes(ptr + index, count, ptr2, num);
				}
				catch
				{
					UnixMarshal.FreeHeap(intPtr);
					throw;
				}
				if (bytes != byteCount)
				{
					UnixMarshal.FreeHeap(intPtr);
					throw new NotSupportedException("encoding.GetBytes() doesn't equal encoding.GetByteCount()!");
				}
				ptr2 += byteCount;
				for (int i = 0; i < maxByteCount; i++)
				{
					ptr2[i] = 0;
				}
			}
			return intPtr;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005E6C File Offset: 0x0000406C
		public static bool ShouldRetrySyscall(int r)
		{
			return r == -1 && Stdlib.GetLastError() == Errno.EINTR;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005E80 File Offset: 0x00004080
		[CLSCompliant(false)]
		public static bool ShouldRetrySyscall(int r, out Errno errno)
		{
			errno = (Errno)0;
			return r == -1 && (errno = Stdlib.GetLastError()) == Errno.EINTR;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005EA4 File Offset: 0x000040A4
		internal static string EscapeFormatString(string message, char[] permitted)
		{
			if (message == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder(message.Length);
			for (int i = 0; i < message.Length; i++)
			{
				char c = message[i];
				stringBuilder.Append(c);
				if (c == '%' && i + 1 < message.Length)
				{
					char c2 = message[i + 1];
					if (c2 == '%' || UnixMarshal.IsCharPresent(permitted, c2))
					{
						stringBuilder.Append(c2);
					}
					else
					{
						stringBuilder.Append('%').Append(c2);
					}
					i++;
				}
				else if (c == '%')
				{
					stringBuilder.Append('%');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005F44 File Offset: 0x00004144
		private static bool IsCharPresent(char[] array, char c)
		{
			if (array == null)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005F70 File Offset: 0x00004170
		internal static Exception CreateExceptionForError(Errno errno)
		{
			string errorDescription = UnixMarshal.GetErrorDescription(errno);
			UnixIOException ex = new UnixIOException(errno);
			if (errno <= Errno.ERANGE)
			{
				switch (errno)
				{
				case Errno.EPERM:
					goto IL_00CB;
				case Errno.ENOENT:
					return new FileNotFoundException(errorDescription, ex);
				case Errno.ESRCH:
				case Errno.EINTR:
				case Errno.E2BIG:
				case Errno.ECHILD:
				case Errno.EAGAIN:
				case Errno.ENOMEM:
					return ex;
				case Errno.EIO:
				case Errno.ENXIO:
					goto IL_00DB;
				case Errno.ENOEXEC:
					return new InvalidProgramException(errorDescription, ex);
				case Errno.EBADF:
					break;
				case Errno.EACCES:
					goto IL_00FB;
				case Errno.EFAULT:
					return new NullReferenceException(errorDescription, ex);
				default:
					switch (errno)
					{
					case Errno.ENOTDIR:
						return new DirectoryNotFoundException(errorDescription, ex);
					case Errno.EISDIR:
						goto IL_00FB;
					case Errno.EINVAL:
						break;
					case Errno.ENFILE:
					case Errno.EMFILE:
					case Errno.ENOTTY:
					case Errno.ETXTBSY:
					case Errno.EFBIG:
						return ex;
					case Errno.ENOSPC:
					case Errno.ESPIPE:
					case Errno.EROFS:
						goto IL_00DB;
					default:
						if (errno != Errno.ERANGE)
						{
							return ex;
						}
						return new ArgumentOutOfRangeException(errorDescription);
					}
					break;
				}
				return new ArgumentException(errorDescription, ex);
				IL_00FB:
				return new UnauthorizedAccessException(errorDescription, ex);
			}
			if (errno <= Errno.ENOTEMPTY)
			{
				if (errno == Errno.ENAMETOOLONG)
				{
					return new PathTooLongException(errorDescription, ex);
				}
				if (errno != Errno.ENOTEMPTY)
				{
					return ex;
				}
				goto IL_00DB;
			}
			else
			{
				if (errno == Errno.EOVERFLOW)
				{
					return new OverflowException(errorDescription, ex);
				}
				if (errno != Errno.EOPNOTSUPP)
				{
					return ex;
				}
			}
			IL_00CB:
			return new InvalidOperationException(errorDescription, ex);
			IL_00DB:
			return new IOException(errorDescription, ex);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006081 File Offset: 0x00004281
		internal static Exception CreateExceptionForLastError()
		{
			return UnixMarshal.CreateExceptionForError(Stdlib.GetLastError());
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000608D File Offset: 0x0000428D
		[CLSCompliant(false)]
		public static void ThrowExceptionForError(Errno errno)
		{
			throw UnixMarshal.CreateExceptionForError(errno);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006095 File Offset: 0x00004295
		public static void ThrowExceptionForLastError()
		{
			throw UnixMarshal.CreateExceptionForLastError();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000609C File Offset: 0x0000429C
		[CLSCompliant(false)]
		public static void ThrowExceptionForErrorIf(int retval, Errno errno)
		{
			if (retval == -1)
			{
				UnixMarshal.ThrowExceptionForError(errno);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000060A8 File Offset: 0x000042A8
		public static void ThrowExceptionForLastErrorIf(int retval)
		{
			if (retval == -1)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
		}
	}
}
