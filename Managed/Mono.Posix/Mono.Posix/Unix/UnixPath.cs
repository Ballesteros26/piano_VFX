using System;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001D RID: 29
	public sealed class UnixPath
	{
		// Token: 0x06000168 RID: 360 RVA: 0x000060B3 File Offset: 0x000042B3
		private UnixPath()
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000060BB File Offset: 0x000042BB
		public static char[] GetInvalidPathChars()
		{
			return (char[])UnixPath._InvalidPathChars.Clone();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000060CC File Offset: 0x000042CC
		public static string Combine(string path1, params string[] paths)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			if (path1.IndexOfAny(UnixPath._InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path", "path1");
			}
			int num = path1.Length;
			int num2 = -1;
			for (int i = 0; i < paths.Length; i++)
			{
				if (paths[i] == null)
				{
					throw new ArgumentNullException("paths[" + i + "]");
				}
				if (paths[i].IndexOfAny(UnixPath._InvalidPathChars) != -1)
				{
					throw new ArgumentException("Illegal characters in path", "paths[" + i + "]");
				}
				if (UnixPath.IsPathRooted(paths[i]))
				{
					num = 0;
					num2 = i;
				}
				num += paths[i].Length + 1;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			if (num2 == -1)
			{
				stringBuilder.Append(path1);
				num2 = 0;
			}
			for (int j = num2; j < paths.Length; j++)
			{
				UnixPath.Combine(stringBuilder, paths[j]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000061CC File Offset: 0x000043CC
		private static void Combine(StringBuilder path, string part)
		{
			if (path.Length > 0 && part.Length > 0)
			{
				char c = path[path.Length - 1];
				if (c != UnixPath.DirectorySeparatorChar && c != UnixPath.AltDirectorySeparatorChar && c != UnixPath.VolumeSeparatorChar)
				{
					path.Append(UnixPath.DirectorySeparatorChar);
				}
			}
			path.Append(part);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006228 File Offset: 0x00004428
		public static string GetDirectoryName(string path)
		{
			UnixPath.CheckPath(path);
			int num = path.LastIndexOf(UnixPath.DirectorySeparatorChar);
			if (num > 0)
			{
				return path.Substring(0, num);
			}
			if (num == 0)
			{
				return "/";
			}
			return "";
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006264 File Offset: 0x00004464
		public static string GetFileName(string path)
		{
			if (path == null || path.Length == 0)
			{
				return path;
			}
			int num = path.LastIndexOf(UnixPath.DirectorySeparatorChar);
			if (num >= 0)
			{
				return path.Substring(num + 1);
			}
			return path;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006299 File Offset: 0x00004499
		public static string GetFullPath(string path)
		{
			path = UnixPath._GetFullPath(path);
			return UnixPath.GetCanonicalPath(path);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000062AC File Offset: 0x000044AC
		private static string _GetFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (!UnixPath.IsPathRooted(path))
			{
				path = UnixDirectoryInfo.GetCurrentDirectory() + UnixPath.DirectorySeparatorChar.ToString() + path;
			}
			return path;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000062EC File Offset: 0x000044EC
		public static string GetCanonicalPath(string path)
		{
			string[] array;
			int num;
			UnixPath.GetPathComponents(path, out array, out num);
			string text = string.Join("/", array, 0, num);
			if (!UnixPath.IsPathRooted(path))
			{
				return text;
			}
			return "/" + text;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006328 File Offset: 0x00004528
		private static void GetPathComponents(string path, out string[] components, out int lastIndex)
		{
			string[] array = path.Split(new char[] { UnixPath.DirectorySeparatorChar });
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i] == ".") && !(array[i] == string.Empty))
				{
					if (array[i] == "..")
					{
						if (num != 0)
						{
							num--;
						}
						else
						{
							num++;
						}
					}
					else
					{
						array[num++] = array[i];
					}
				}
			}
			components = array;
			lastIndex = num;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000063A6 File Offset: 0x000045A6
		public static string GetPathRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (!UnixPath.IsPathRooted(path))
			{
				return "";
			}
			return "/";
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000063C0 File Offset: 0x000045C0
		public static string GetCompleteRealPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string[] array;
			int num;
			UnixPath.GetPathComponents(path, out array, out num);
			StringBuilder stringBuilder = new StringBuilder();
			if (array.Length != 0)
			{
				string text = (UnixPath.IsPathRooted(path) ? "/" : "");
				text += array[0];
				stringBuilder.Append(UnixPath.GetRealPath(text));
			}
			for (int i = 1; i < num; i++)
			{
				stringBuilder.Append("/").Append(array[i]);
				string realPath = UnixPath.GetRealPath(stringBuilder.ToString());
				stringBuilder.Remove(0, stringBuilder.Length);
				stringBuilder.Append(realPath);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000646C File Offset: 0x0000466C
		public static string GetRealPath(string path)
		{
			for (;;)
			{
				string text = UnixPath.ReadSymbolicLink(path);
				if (text == null)
				{
					break;
				}
				if (UnixPath.IsPathRooted(text))
				{
					path = text;
				}
				else
				{
					path = UnixPath.GetDirectoryName(path) + UnixPath.DirectorySeparatorChar.ToString() + text;
					path = UnixPath.GetCanonicalPath(path);
				}
			}
			return path;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000064B8 File Offset: 0x000046B8
		internal static string ReadSymbolicLink(string path)
		{
			string text = UnixPath.TryReadLink(path);
			if (text == null)
			{
				Errno lastError = Stdlib.GetLastError();
				if (lastError != Errno.EINVAL)
				{
					UnixMarshal.ThrowExceptionForError(lastError);
				}
			}
			return text;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000064E0 File Offset: 0x000046E0
		public static string TryReadLink(string path)
		{
			byte[] array = new byte[256];
			long num;
			for (;;)
			{
				num = Syscall.readlink(path, array);
				if (num < 0L)
				{
					break;
				}
				if (num != (long)array.Length)
				{
					goto IL_0030;
				}
				checked
				{
					array = new byte[unchecked((long)array.Length) * 2L];
				}
			}
			return null;
			IL_0030:
			return UnixEncoding.Instance.GetString(array, 0, checked((int)num));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000652C File Offset: 0x0000472C
		public static string TryReadLinkAt(int dirfd, string path)
		{
			byte[] array = new byte[256];
			long num;
			for (;;)
			{
				num = Syscall.readlinkat(dirfd, path, array);
				if (num < 0L)
				{
					break;
				}
				if (num != (long)array.Length)
				{
					goto IL_0031;
				}
				checked
				{
					array = new byte[unchecked((long)array.Length) * 2L];
				}
			}
			return null;
			IL_0031:
			return UnixEncoding.Instance.GetString(array, 0, checked((int)num));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006578 File Offset: 0x00004778
		public static string ReadLink(string path)
		{
			string text = UnixPath.TryReadLink(path);
			if (text == null)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return text;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006588 File Offset: 0x00004788
		public static string ReadLinkAt(int dirfd, string path)
		{
			string text = UnixPath.TryReadLinkAt(dirfd, path);
			if (text == null)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return text;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006599 File Offset: 0x00004799
		public static bool IsPathRooted(string path)
		{
			return path != null && path.Length != 0 && path[0] == UnixPath.DirectorySeparatorChar;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000065B8 File Offset: 0x000047B8
		internal static void CheckPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException();
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path cannot contain a zero-length string", "path");
			}
			if (path.IndexOfAny(UnixPath._InvalidPathChars) != -1)
			{
				throw new ArgumentException("Invalid characters in path.", "path");
			}
		}

		// Token: 0x0400007C RID: 124
		public static readonly char DirectorySeparatorChar = '/';

		// Token: 0x0400007D RID: 125
		public static readonly char AltDirectorySeparatorChar = '/';

		// Token: 0x0400007E RID: 126
		public static readonly char PathSeparator = ':';

		// Token: 0x0400007F RID: 127
		public static readonly char VolumeSeparatorChar = '/';

		// Token: 0x04000080 RID: 128
		private static readonly char[] _InvalidPathChars = new char[0];
	}
}
