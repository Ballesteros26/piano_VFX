using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x020003DD RID: 989
	internal static class MonoIO
	{
		// Token: 0x06002E75 RID: 11893 RVA: 0x000A5EE2 File Offset: 0x000A40E2
		public static Exception GetException(MonoIOError error)
		{
			if (error == MonoIOError.ERROR_ACCESS_DENIED)
			{
				return new UnauthorizedAccessException("Access to the path is denied.");
			}
			if (error != MonoIOError.ERROR_FILE_EXISTS)
			{
				return MonoIO.GetException(string.Empty, error);
			}
			return new IOException("Cannot create a file that already exist.", -2147024816);
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000A5F18 File Offset: 0x000A4118
		public static Exception GetException(string path, MonoIOError error)
		{
			if (error <= MonoIOError.ERROR_FILE_EXISTS)
			{
				if (error <= MonoIOError.ERROR_NOT_SAME_DEVICE)
				{
					switch (error)
					{
					case MonoIOError.ERROR_FILE_NOT_FOUND:
						return new FileNotFoundException(string.Format("Could not find file \"{0}\"", path), path);
					case MonoIOError.ERROR_PATH_NOT_FOUND:
						return new DirectoryNotFoundException(string.Format("Could not find a part of the path \"{0}\"", path));
					case MonoIOError.ERROR_TOO_MANY_OPEN_FILES:
						if (MonoIO.dump_handles)
						{
							MonoIO.DumpHandles();
						}
						return new IOException("Too many open files", (int)((MonoIOError)(-2147024896) | error));
					case MonoIOError.ERROR_ACCESS_DENIED:
						return new UnauthorizedAccessException(string.Format("Access to the path \"{0}\" is denied.", path));
					case MonoIOError.ERROR_INVALID_HANDLE:
						return new IOException(string.Format("Invalid handle to path \"{0}\"", path), (int)((MonoIOError)(-2147024896) | error));
					default:
						if (error == MonoIOError.ERROR_INVALID_DRIVE)
						{
							return new DriveNotFoundException(string.Format("Could not find the drive  '{0}'. The drive might not be ready or might not be mapped.", path));
						}
						if (error == MonoIOError.ERROR_NOT_SAME_DEVICE)
						{
							return new IOException("Source and destination are not on the same device", (int)((MonoIOError)(-2147024896) | error));
						}
						break;
					}
				}
				else
				{
					switch (error)
					{
					case MonoIOError.ERROR_WRITE_FAULT:
						return new IOException(string.Format("Write fault on path {0}", path), (int)((MonoIOError)(-2147024896) | error));
					case MonoIOError.ERROR_READ_FAULT:
					case MonoIOError.ERROR_GEN_FAILURE:
						break;
					case MonoIOError.ERROR_SHARING_VIOLATION:
						return new IOException(string.Format("Sharing violation on path {0}", path), (int)((MonoIOError)(-2147024896) | error));
					case MonoIOError.ERROR_LOCK_VIOLATION:
						return new IOException(string.Format("Lock violation on path {0}", path), (int)((MonoIOError)(-2147024896) | error));
					default:
						if (error == MonoIOError.ERROR_HANDLE_DISK_FULL)
						{
							return new IOException(string.Format("Disk full. Path {0}", path), (int)((MonoIOError)(-2147024896) | error));
						}
						if (error == MonoIOError.ERROR_FILE_EXISTS)
						{
							return new IOException(string.Format("Could not create file \"{0}\". File already exists.", path), (int)((MonoIOError)(-2147024896) | error));
						}
						break;
					}
				}
			}
			else if (error <= MonoIOError.ERROR_DIR_NOT_EMPTY)
			{
				if (error == MonoIOError.ERROR_CANNOT_MAKE)
				{
					return new IOException(string.Format("Path {0} is a directory", path), (int)((MonoIOError)(-2147024896) | error));
				}
				if (error == MonoIOError.ERROR_INVALID_PARAMETER)
				{
					return new IOException(string.Format("Invalid parameter", Array.Empty<object>()), (int)((MonoIOError)(-2147024896) | error));
				}
				if (error == MonoIOError.ERROR_DIR_NOT_EMPTY)
				{
					return new IOException(string.Format("Directory {0} is not empty", path), (int)((MonoIOError)(-2147024896) | error));
				}
			}
			else
			{
				if (error == MonoIOError.ERROR_FILENAME_EXCED_RANGE)
				{
					return new PathTooLongException(string.Format("Path is too long. Path: {0}", path));
				}
				if (error == MonoIOError.ERROR_DIRECTORY)
				{
					return new IOException("The directory name is invalid", (int)((MonoIOError)(-2147024896) | error));
				}
				if (error == MonoIOError.ERROR_ENCRYPTION_FAILED)
				{
					return new IOException("Encryption failed", (int)((MonoIOError)(-2147024896) | error));
				}
			}
			return new IOException(string.Format("Win32 IO returned {0}. Path: {1}", error, path), (int)((MonoIOError)(-2147024896) | error));
		}

		// Token: 0x06002E77 RID: 11895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool CreateDirectory(char* path, out MonoIOError error);

		// Token: 0x06002E78 RID: 11896 RVA: 0x000A6184 File Offset: 0x000A4384
		public unsafe static bool CreateDirectory(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.CreateDirectory(ptr, out error);
		}

		// Token: 0x06002E79 RID: 11897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool RemoveDirectory(char* path, out MonoIOError error);

		// Token: 0x06002E7A RID: 11898 RVA: 0x000A61A8 File Offset: 0x000A43A8
		public unsafe static bool RemoveDirectory(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.RemoveDirectory(ptr, out error);
		}

		// Token: 0x06002E7B RID: 11899
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetCurrentDirectory(out MonoIOError error);

		// Token: 0x06002E7C RID: 11900
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SetCurrentDirectory(char* path, out MonoIOError error);

		// Token: 0x06002E7D RID: 11901 RVA: 0x000A61CC File Offset: 0x000A43CC
		public unsafe static bool SetCurrentDirectory(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.SetCurrentDirectory(ptr, out error);
		}

		// Token: 0x06002E7E RID: 11902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool MoveFile(char* path, char* dest, out MonoIOError error);

		// Token: 0x06002E7F RID: 11903 RVA: 0x000A61F0 File Offset: 0x000A43F0
		public unsafe static bool MoveFile(string path, string dest, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = dest;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.MoveFile(ptr, ptr2, out error);
		}

		// Token: 0x06002E80 RID: 11904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool CopyFile(char* path, char* dest, bool overwrite, out MonoIOError error);

		// Token: 0x06002E81 RID: 11905 RVA: 0x000A6228 File Offset: 0x000A4428
		public unsafe static bool CopyFile(string path, string dest, bool overwrite, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = dest;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.CopyFile(ptr, ptr2, overwrite, out error);
		}

		// Token: 0x06002E82 RID: 11906
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool DeleteFile(char* path, out MonoIOError error);

		// Token: 0x06002E83 RID: 11907 RVA: 0x000A6260 File Offset: 0x000A4460
		public unsafe static bool DeleteFile(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.DeleteFile(ptr, out error);
		}

		// Token: 0x06002E84 RID: 11908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool ReplaceFile(char* sourceFileName, char* destinationFileName, char* destinationBackupFileName, bool ignoreMetadataErrors, out MonoIOError error);

		// Token: 0x06002E85 RID: 11909 RVA: 0x000A6284 File Offset: 0x000A4484
		public unsafe static bool ReplaceFile(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors, out MonoIOError error)
		{
			char* ptr = sourceFileName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = destinationFileName;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr3 = destinationBackupFileName;
			if (ptr3 != null)
			{
				ptr3 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.ReplaceFile(ptr, ptr2, ptr3, ignoreMetadataErrors, out error);
		}

		// Token: 0x06002E86 RID: 11910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern FileAttributes GetFileAttributes(char* path, out MonoIOError error);

		// Token: 0x06002E87 RID: 11911 RVA: 0x000A62D0 File Offset: 0x000A44D0
		public unsafe static FileAttributes GetFileAttributes(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.GetFileAttributes(ptr, out error);
		}

		// Token: 0x06002E88 RID: 11912
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SetFileAttributes(char* path, FileAttributes attrs, out MonoIOError error);

		// Token: 0x06002E89 RID: 11913 RVA: 0x000A62F4 File Offset: 0x000A44F4
		public unsafe static bool SetFileAttributes(string path, FileAttributes attrs, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.SetFileAttributes(ptr, attrs, out error);
		}

		// Token: 0x06002E8A RID: 11914
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MonoFileType GetFileType(IntPtr handle, out MonoIOError error);

		// Token: 0x06002E8B RID: 11915 RVA: 0x000A631C File Offset: 0x000A451C
		public static MonoFileType GetFileType(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			MonoFileType fileType;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				fileType = MonoIO.GetFileType(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return fileType;
		}

		// Token: 0x06002E8C RID: 11916
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr FindFirstFile(char* pathWithPattern, out string fileName, out int fileAttr, out int error);

		// Token: 0x06002E8D RID: 11917 RVA: 0x000A6360 File Offset: 0x000A4560
		public unsafe static IntPtr FindFirstFile(string pathWithPattern, out string fileName, out int fileAttr, out int error)
		{
			char* ptr = pathWithPattern;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.FindFirstFile(ptr, out fileName, out fileAttr, out error);
		}

		// Token: 0x06002E8E RID: 11918
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool FindNextFile(IntPtr hnd, out string fileName, out int fileAttr, out int error);

		// Token: 0x06002E8F RID: 11919
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool FindCloseFile(IntPtr hnd);

		// Token: 0x06002E90 RID: 11920 RVA: 0x000A6386 File Offset: 0x000A4586
		public static bool Exists(string path, out MonoIOError error)
		{
			return MonoIO.GetFileAttributes(path, out error) != (FileAttributes)(-1);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000A6398 File Offset: 0x000A4598
		public static bool ExistsFile(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			return fileAttributes != (FileAttributes)(-1) && (fileAttributes & FileAttributes.Directory) == (FileAttributes)0;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000A63BC File Offset: 0x000A45BC
		public static bool ExistsDirectory(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			if (error == MonoIOError.ERROR_FILE_NOT_FOUND)
			{
				error = MonoIOError.ERROR_PATH_NOT_FOUND;
			}
			return fileAttributes != (FileAttributes)(-1) && (fileAttributes & FileAttributes.Directory) != (FileAttributes)0;
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000A63E8 File Offset: 0x000A45E8
		public static bool ExistsSymlink(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			return fileAttributes != (FileAttributes)(-1) && (fileAttributes & FileAttributes.ReparsePoint) != (FileAttributes)0;
		}

		// Token: 0x06002E94 RID: 11924
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool GetFileStat(char* path, out MonoIOStat stat, out MonoIOError error);

		// Token: 0x06002E95 RID: 11925 RVA: 0x000A6410 File Offset: 0x000A4610
		public unsafe static bool GetFileStat(string path, out MonoIOStat stat, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.GetFileStat(ptr, out stat, out error);
		}

		// Token: 0x06002E96 RID: 11926
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Open(char* filename, FileMode mode, FileAccess access, FileShare share, FileOptions options, out MonoIOError error);

		// Token: 0x06002E97 RID: 11927 RVA: 0x000A6438 File Offset: 0x000A4638
		public unsafe static IntPtr Open(string filename, FileMode mode, FileAccess access, FileShare share, FileOptions options, out MonoIOError error)
		{
			char* ptr = filename;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.Open(ptr, mode, access, share, options, out error);
		}

		// Token: 0x06002E98 RID: 11928
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Close(IntPtr handle, out MonoIOError error);

		// Token: 0x06002E99 RID: 11929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Read(IntPtr handle, byte[] dest, int dest_offset, int count, out MonoIOError error);

		// Token: 0x06002E9A RID: 11930 RVA: 0x000A6464 File Offset: 0x000A4664
		public static int Read(SafeHandle safeHandle, byte[] dest, int dest_offset, int count, out MonoIOError error)
		{
			bool flag = false;
			int num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = MonoIO.Read(safeHandle.DangerousGetHandle(), dest, dest_offset, count, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002E9B RID: 11931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Write(IntPtr handle, [In] byte[] src, int src_offset, int count, out MonoIOError error);

		// Token: 0x06002E9C RID: 11932 RVA: 0x000A64AC File Offset: 0x000A46AC
		public static int Write(SafeHandle safeHandle, byte[] src, int src_offset, int count, out MonoIOError error)
		{
			bool flag = false;
			int num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = MonoIO.Write(safeHandle.DangerousGetHandle(), src, src_offset, count, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002E9D RID: 11933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long Seek(IntPtr handle, long offset, SeekOrigin origin, out MonoIOError error);

		// Token: 0x06002E9E RID: 11934 RVA: 0x000A64F4 File Offset: 0x000A46F4
		public static long Seek(SafeHandle safeHandle, long offset, SeekOrigin origin, out MonoIOError error)
		{
			bool flag = false;
			long num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = MonoIO.Seek(safeHandle.DangerousGetHandle(), offset, origin, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002E9F RID: 11935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Flush(IntPtr handle, out MonoIOError error);

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000A6538 File Offset: 0x000A4738
		public static bool Flush(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = MonoIO.Flush(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x06002EA1 RID: 11937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetLength(IntPtr handle, out MonoIOError error);

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000A657C File Offset: 0x000A477C
		public static long GetLength(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			long length;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				length = MonoIO.GetLength(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return length;
		}

		// Token: 0x06002EA3 RID: 11939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetLength(IntPtr handle, long length, out MonoIOError error);

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000A65C0 File Offset: 0x000A47C0
		public static bool SetLength(SafeHandle safeHandle, long length, out MonoIOError error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = MonoIO.SetLength(safeHandle.DangerousGetHandle(), length, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x06002EA5 RID: 11941
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetFileTime(IntPtr handle, long creation_time, long last_access_time, long last_write_time, out MonoIOError error);

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000A6604 File Offset: 0x000A4804
		public static bool SetFileTime(SafeHandle safeHandle, long creation_time, long last_access_time, long last_write_time, out MonoIOError error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = MonoIO.SetFileTime(safeHandle.DangerousGetHandle(), creation_time, last_access_time, last_write_time, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000A664C File Offset: 0x000A484C
		public static bool SetFileTime(string path, long creation_time, long last_access_time, long last_write_time, out MonoIOError error)
		{
			return MonoIO.SetFileTime(path, 0, creation_time, last_access_time, last_write_time, DateTime.MinValue, out error);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000A665F File Offset: 0x000A485F
		public static bool SetCreationTime(string path, DateTime dateTime, out MonoIOError error)
		{
			return MonoIO.SetFileTime(path, 1, -1L, -1L, -1L, dateTime, out error);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000A6670 File Offset: 0x000A4870
		public static bool SetLastAccessTime(string path, DateTime dateTime, out MonoIOError error)
		{
			return MonoIO.SetFileTime(path, 2, -1L, -1L, -1L, dateTime, out error);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000A6681 File Offset: 0x000A4881
		public static bool SetLastWriteTime(string path, DateTime dateTime, out MonoIOError error)
		{
			return MonoIO.SetFileTime(path, 3, -1L, -1L, -1L, dateTime, out error);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000A6694 File Offset: 0x000A4894
		public static bool SetFileTime(string path, int type, long creation_time, long last_access_time, long last_write_time, DateTime dateTime, out MonoIOError error)
		{
			IntPtr intPtr = MonoIO.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, FileOptions.None, out error);
			if (intPtr == MonoIO.InvalidHandle)
			{
				return false;
			}
			switch (type)
			{
			case 1:
				creation_time = dateTime.ToFileTime();
				break;
			case 2:
				last_access_time = dateTime.ToFileTime();
				break;
			case 3:
				last_write_time = dateTime.ToFileTime();
				break;
			}
			bool flag = MonoIO.SetFileTime(new SafeFileHandle(intPtr, false), creation_time, last_access_time, last_write_time, out error);
			MonoIOError monoIOError;
			MonoIO.Close(intPtr, out monoIOError);
			return flag;
		}

		// Token: 0x06002EAC RID: 11948
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Lock(IntPtr handle, long position, long length, out MonoIOError error);

		// Token: 0x06002EAD RID: 11949 RVA: 0x000A6710 File Offset: 0x000A4910
		public static void Lock(SafeHandle safeHandle, long position, long length, out MonoIOError error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				MonoIO.Lock(safeHandle.DangerousGetHandle(), position, length, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06002EAE RID: 11950
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unlock(IntPtr handle, long position, long length, out MonoIOError error);

		// Token: 0x06002EAF RID: 11951 RVA: 0x000A6754 File Offset: 0x000A4954
		public static void Unlock(SafeHandle safeHandle, long position, long length, out MonoIOError error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				MonoIO.Unlock(safeHandle.DangerousGetHandle(), position, length, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002EB0 RID: 11952
		public static extern IntPtr ConsoleOutput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002EB1 RID: 11953
		public static extern IntPtr ConsoleInput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002EB2 RID: 11954
		public static extern IntPtr ConsoleError
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06002EB3 RID: 11955
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CreatePipe(out IntPtr read_handle, out IntPtr write_handle, out MonoIOError error);

		// Token: 0x06002EB4 RID: 11956
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DuplicateHandle(IntPtr source_process_handle, IntPtr source_handle, IntPtr target_process_handle, out IntPtr target_handle, int access, int inherit, int options, out MonoIOError error);

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002EB5 RID: 11957
		public static extern char VolumeSeparatorChar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002EB6 RID: 11958
		public static extern char DirectorySeparatorChar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002EB7 RID: 11959
		public static extern char AltDirectorySeparatorChar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002EB8 RID: 11960
		public static extern char PathSeparator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06002EB9 RID: 11961
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DumpHandles();

		// Token: 0x06002EBA RID: 11962
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RemapPath(string path, out string newPath);

		// Token: 0x04001819 RID: 6169
		public const int FileAlreadyExistsHResult = -2147024816;

		// Token: 0x0400181A RID: 6170
		public const FileAttributes InvalidFileAttributes = (FileAttributes)(-1);

		// Token: 0x0400181B RID: 6171
		public static readonly IntPtr InvalidHandle = (IntPtr)(-1L);

		// Token: 0x0400181C RID: 6172
		private static bool dump_handles = Environment.GetEnvironmentVariable("MONO_DUMP_HANDLES_ON_ERROR_TOO_MANY_OPEN_FILES") != null;
	}
}
