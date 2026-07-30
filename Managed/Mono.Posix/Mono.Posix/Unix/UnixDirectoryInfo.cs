using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000010 RID: 16
	public sealed class UnixDirectoryInfo : UnixFileSystemInfo
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002FB0 File Offset: 0x000011B0
		public UnixDirectoryInfo(string path)
			: base(path)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002FB9 File Offset: 0x000011B9
		internal UnixDirectoryInfo(string path, Stat stat)
			: base(path, stat)
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002FC4 File Offset: 0x000011C4
		public override string Name
		{
			get
			{
				string fileName = UnixPath.GetFileName(base.FullPath);
				if (fileName == null || fileName.Length == 0)
				{
					return base.FullPath;
				}
				return fileName;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002FF0 File Offset: 0x000011F0
		public UnixDirectoryInfo Parent
		{
			get
			{
				if (base.FullPath == "/")
				{
					return this;
				}
				string directoryName = UnixPath.GetDirectoryName(base.FullPath);
				if (directoryName == "")
				{
					throw new InvalidOperationException("Do not know parent directory for path `" + base.FullPath + "'");
				}
				return new UnixDirectoryInfo(directoryName);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000304C File Offset: 0x0000124C
		public UnixDirectoryInfo Root
		{
			get
			{
				string pathRoot = UnixPath.GetPathRoot(base.FullPath);
				if (pathRoot == null)
				{
					return null;
				}
				return new UnixDirectoryInfo(pathRoot);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003070 File Offset: 0x00001270
		[CLSCompliant(false)]
		public void Create(FilePermissions mode)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.mkdir(base.FullPath, mode));
			base.Refresh();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003089 File Offset: 0x00001289
		public void Create(FileAccessPermissions mode)
		{
			this.Create((FilePermissions)mode);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003094 File Offset: 0x00001294
		public void Create()
		{
			FilePermissions filePermissions = FilePermissions.ACCESSPERMS;
			this.Create(filePermissions);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000030AE File Offset: 0x000012AE
		public override void Delete()
		{
			this.Delete(false);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030B8 File Offset: 0x000012B8
		public void Delete(bool recursive)
		{
			if (recursive)
			{
				foreach (UnixFileSystemInfo unixFileSystemInfo in this.GetFileSystemEntries())
				{
					UnixDirectoryInfo unixDirectoryInfo = unixFileSystemInfo as UnixDirectoryInfo;
					if (unixDirectoryInfo != null)
					{
						unixDirectoryInfo.Delete(true);
					}
					else
					{
						unixFileSystemInfo.Delete();
					}
				}
			}
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.rmdir(base.FullPath));
			base.Refresh();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003110 File Offset: 0x00001310
		public Dirent[] GetEntries()
		{
			IntPtr intPtr = Syscall.opendir(base.FullPath);
			if (intPtr == IntPtr.Zero)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			bool flag = false;
			Dirent[] array;
			try
			{
				Dirent[] entries = UnixDirectoryInfo.GetEntries(intPtr);
				flag = true;
				array = entries;
			}
			finally
			{
				int num = Syscall.closedir(intPtr);
				if (flag)
				{
					UnixMarshal.ThrowExceptionForLastErrorIf(num);
				}
			}
			return array;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000316C File Offset: 0x0000136C
		private static Dirent[] GetEntries(IntPtr dirp)
		{
			ArrayList arrayList = new ArrayList();
			IntPtr intPtr;
			int num;
			do
			{
				Dirent dirent = new Dirent();
				num = Syscall.readdir_r(dirp, dirent, out intPtr);
				if (num == 0 && intPtr != IntPtr.Zero && dirent.d_name != "." && dirent.d_name != "..")
				{
					arrayList.Add(dirent);
				}
			}
			while (num == 0 && intPtr != IntPtr.Zero);
			if (num != 0)
			{
				UnixMarshal.ThrowExceptionForLastErrorIf(num);
			}
			return (Dirent[])arrayList.ToArray(typeof(Dirent));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000031FC File Offset: 0x000013FC
		public Dirent[] GetEntries(Regex regex)
		{
			IntPtr intPtr = Syscall.opendir(base.FullPath);
			if (intPtr == IntPtr.Zero)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			Dirent[] entries;
			try
			{
				entries = UnixDirectoryInfo.GetEntries(intPtr, regex);
			}
			finally
			{
				UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.closedir(intPtr));
			}
			return entries;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003250 File Offset: 0x00001450
		private static Dirent[] GetEntries(IntPtr dirp, Regex regex)
		{
			ArrayList arrayList = new ArrayList();
			IntPtr intPtr;
			int num;
			do
			{
				Dirent dirent = new Dirent();
				num = Syscall.readdir_r(dirp, dirent, out intPtr);
				if (num == 0 && intPtr != IntPtr.Zero && regex.Match(dirent.d_name).Success && dirent.d_name != "." && dirent.d_name != "..")
				{
					arrayList.Add(dirent);
				}
			}
			while (num == 0 && intPtr != IntPtr.Zero);
			if (num != 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return (Dirent[])arrayList.ToArray(typeof(Dirent));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032F0 File Offset: 0x000014F0
		public Dirent[] GetEntries(string regex)
		{
			Regex regex2 = new Regex(regex);
			return this.GetEntries(regex2);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000330C File Offset: 0x0000150C
		public UnixFileSystemInfo[] GetFileSystemEntries()
		{
			Dirent[] entries = this.GetEntries();
			return this.GetFileSystemEntries(entries);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003328 File Offset: 0x00001528
		private UnixFileSystemInfo[] GetFileSystemEntries(Dirent[] dentries)
		{
			UnixFileSystemInfo[] array = new UnixFileSystemInfo[dentries.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = UnixFileSystemInfo.GetFileSystemEntry(UnixPath.Combine(base.FullPath, new string[] { dentries[num].d_name }));
			}
			return array;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003374 File Offset: 0x00001574
		public UnixFileSystemInfo[] GetFileSystemEntries(Regex regex)
		{
			Dirent[] entries = this.GetEntries(regex);
			return this.GetFileSystemEntries(entries);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003390 File Offset: 0x00001590
		public UnixFileSystemInfo[] GetFileSystemEntries(string regex)
		{
			Regex regex2 = new Regex(regex);
			return this.GetFileSystemEntries(regex2);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000033AC File Offset: 0x000015AC
		public static string GetCurrentDirectory()
		{
			StringBuilder stringBuilder = new StringBuilder(16);
			IntPtr intPtr = IntPtr.Zero;
			do
			{
				stringBuilder.Capacity *= 2;
				intPtr = Syscall.getcwd(stringBuilder, (ulong)((long)stringBuilder.Capacity));
			}
			while (intPtr == IntPtr.Zero && Stdlib.GetLastError() == Errno.ERANGE);
			if (intPtr == IntPtr.Zero)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003411 File Offset: 0x00001611
		public static void SetCurrentDirectory(string path)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.chdir(path));
		}
	}
}
