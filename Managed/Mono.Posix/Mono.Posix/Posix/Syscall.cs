using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Posix
{
	// Token: 0x0200009A RID: 154
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.Syscall.")]
	public class Syscall
	{
		// Token: 0x060006FD RID: 1789
		[DllImport("libc", SetLastError = true)]
		public static extern int exit(int status);

		// Token: 0x060006FE RID: 1790
		[DllImport("libc", SetLastError = true)]
		public static extern int fork();

		// Token: 0x060006FF RID: 1791
		[DllImport("libc", SetLastError = true)]
		public unsafe static extern IntPtr read(int fileDescriptor, void* buf, IntPtr count);

		// Token: 0x06000700 RID: 1792
		[DllImport("libc", SetLastError = true)]
		public unsafe static extern IntPtr write(int fileDescriptor, void* buf, IntPtr count);

		// Token: 0x06000701 RID: 1793
		[DllImport("libc", EntryPoint = "open", SetLastError = true)]
		internal static extern int syscall_open(string pathname, int flags, int mode);

		// Token: 0x06000702 RID: 1794
		[DllImport("MonoPosixHelper")]
		internal static extern int map_Mono_Posix_OpenFlags(OpenFlags flags);

		// Token: 0x06000703 RID: 1795
		[DllImport("MonoPosixHelper")]
		internal static extern int map_Mono_Posix_FileMode(FileMode mode);

		// Token: 0x06000704 RID: 1796 RVA: 0x000100E4 File Offset: 0x0000E2E4
		public static int open(string pathname, OpenFlags flags)
		{
			if ((flags & OpenFlags.O_CREAT) != OpenFlags.O_RDONLY)
			{
				throw new ArgumentException("If you pass O_CREAT, you must call the method with the mode flag");
			}
			int num = Syscall.map_Mono_Posix_OpenFlags(flags);
			return Syscall.syscall_open(pathname, num, 0);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00010110 File Offset: 0x0000E310
		public static int open(string pathname, OpenFlags flags, FileMode mode)
		{
			int num = Syscall.map_Mono_Posix_OpenFlags(flags);
			int num2 = Syscall.map_Mono_Posix_FileMode(mode);
			return Syscall.syscall_open(pathname, num, num2);
		}

		// Token: 0x06000706 RID: 1798
		[DllImport("libc", SetLastError = true)]
		public static extern int close(int fileDescriptor);

		// Token: 0x06000707 RID: 1799
		[DllImport("libc", EntryPoint = "waitpid", SetLastError = true)]
		internal unsafe static extern int syscall_waitpid(int pid, int* status, int options);

		// Token: 0x06000708 RID: 1800
		[DllImport("MonoPosixHelper")]
		internal static extern int map_Mono_Posix_WaitOptions(WaitOptions wait_options);

		// Token: 0x06000709 RID: 1801 RVA: 0x00010134 File Offset: 0x0000E334
		public unsafe static int waitpid(int pid, out int status, WaitOptions options)
		{
			int num = 0;
			int num2 = Syscall.syscall_waitpid(pid, &num, Syscall.map_Mono_Posix_WaitOptions(options));
			status = num;
			return num2;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00010155 File Offset: 0x0000E355
		public static int waitpid(int pid, WaitOptions options)
		{
			return Syscall.syscall_waitpid(pid, null, Syscall.map_Mono_Posix_WaitOptions(options));
		}

		// Token: 0x0600070B RID: 1803
		[DllImport("MonoPosixHelper", EntryPoint = "wifexited")]
		public static extern int WIFEXITED(int status);

		// Token: 0x0600070C RID: 1804
		[DllImport("MonoPosixHelper", EntryPoint = "wexitstatus")]
		public static extern int WEXITSTATUS(int status);

		// Token: 0x0600070D RID: 1805
		[DllImport("MonoPosixHelper", EntryPoint = "wifsignaled")]
		public static extern int WIFSIGNALED(int status);

		// Token: 0x0600070E RID: 1806
		[DllImport("MonoPosixHelper", EntryPoint = "wtermsig")]
		public static extern int WTERMSIG(int status);

		// Token: 0x0600070F RID: 1807
		[DllImport("MonoPosixHelper", EntryPoint = "wifstopped")]
		public static extern int WIFSTOPPED(int status);

		// Token: 0x06000710 RID: 1808
		[DllImport("MonoPosixHelper", EntryPoint = "wstopsig")]
		public static extern int WSTOPSIG(int status);

		// Token: 0x06000711 RID: 1809
		[DllImport("libc", EntryPoint = "creat", SetLastError = true)]
		internal static extern int syscall_creat(string pathname, int flags);

		// Token: 0x06000712 RID: 1810 RVA: 0x00010165 File Offset: 0x0000E365
		public static int creat(string pathname, FileMode flags)
		{
			return Syscall.syscall_creat(pathname, Syscall.map_Mono_Posix_FileMode(flags));
		}

		// Token: 0x06000713 RID: 1811
		[DllImport("libc", SetLastError = true)]
		public static extern int link(string oldPath, string newPath);

		// Token: 0x06000714 RID: 1812
		[DllImport("libc", SetLastError = true)]
		public static extern int unlink(string path);

		// Token: 0x06000715 RID: 1813
		[DllImport("libc", SetLastError = true)]
		public static extern int symlink(string oldpath, string newpath);

		// Token: 0x06000716 RID: 1814
		[DllImport("libc", SetLastError = true)]
		public static extern int chdir(string path);

		// Token: 0x06000717 RID: 1815
		[DllImport("libc", EntryPoint = "chmod", SetLastError = true)]
		internal static extern int syscall_chmod(string path, int mode);

		// Token: 0x06000718 RID: 1816 RVA: 0x00010173 File Offset: 0x0000E373
		public static int chmod(string path, FileMode mode)
		{
			return Syscall.syscall_chmod(path, Syscall.map_Mono_Posix_FileMode(mode));
		}

		// Token: 0x06000719 RID: 1817
		[DllImport("libc", SetLastError = true)]
		public static extern int chown(string path, int owner, int group);

		// Token: 0x0600071A RID: 1818
		[DllImport("libc", SetLastError = true)]
		public static extern int lchown(string path, int owner, int group);

		// Token: 0x0600071B RID: 1819
		[DllImport("libc", SetLastError = true)]
		public static extern int lseek(int fileDescriptor, int offset, int whence);

		// Token: 0x0600071C RID: 1820
		[DllImport("libc", SetLastError = true)]
		public static extern int getpid();

		// Token: 0x0600071D RID: 1821
		[DllImport("libc", SetLastError = true)]
		public static extern int setuid(int uid);

		// Token: 0x0600071E RID: 1822
		[DllImport("libc", SetLastError = true)]
		public static extern int getuid();

		// Token: 0x0600071F RID: 1823
		[DllImport("libc")]
		public static extern uint alarm(uint seconds);

		// Token: 0x06000720 RID: 1824
		[DllImport("libc", SetLastError = true)]
		public static extern int pause();

		// Token: 0x06000721 RID: 1825
		[DllImport("libc", EntryPoint = "access", SetLastError = true)]
		internal static extern int syscall_access(string pathname, int mode);

		// Token: 0x06000722 RID: 1826
		[DllImport("MonoPosixHelper")]
		internal static extern int map_Mono_Posix_AccessMode(AccessMode mode);

		// Token: 0x06000723 RID: 1827 RVA: 0x00010181 File Offset: 0x0000E381
		public static int access(string pathname, AccessMode mode)
		{
			return Syscall.syscall_access(pathname, Syscall.map_Mono_Posix_AccessMode(mode));
		}

		// Token: 0x06000724 RID: 1828
		[DllImport("libc", SetLastError = true)]
		public static extern int nice(int increment);

		// Token: 0x06000725 RID: 1829
		[DllImport("libc")]
		public static extern void sync();

		// Token: 0x06000726 RID: 1830
		[DllImport("libc", SetLastError = true)]
		public static extern void kill(int pid, int sig);

		// Token: 0x06000727 RID: 1831
		[DllImport("libc", SetLastError = true)]
		public static extern int rename(string oldPath, string newPath);

		// Token: 0x06000728 RID: 1832
		[DllImport("libc", EntryPoint = "mkdir", SetLastError = true)]
		internal static extern int syscall_mkdir(string pathname, int mode);

		// Token: 0x06000729 RID: 1833 RVA: 0x0001018F File Offset: 0x0000E38F
		public static int mkdir(string pathname, FileMode mode)
		{
			return Syscall.syscall_mkdir(pathname, Syscall.map_Mono_Posix_FileMode(mode));
		}

		// Token: 0x0600072A RID: 1834
		[DllImport("libc", SetLastError = true)]
		public static extern int rmdir(string path);

		// Token: 0x0600072B RID: 1835
		[DllImport("libc", SetLastError = true)]
		public static extern int dup(int fileDescriptor);

		// Token: 0x0600072C RID: 1836
		[DllImport("libc", SetLastError = true)]
		public static extern int setgid(int gid);

		// Token: 0x0600072D RID: 1837
		[DllImport("libc", SetLastError = true)]
		public static extern int getgid();

		// Token: 0x0600072E RID: 1838
		[DllImport("libc", SetLastError = true)]
		public static extern int signal(int signum, Syscall.sighandler_t handler);

		// Token: 0x0600072F RID: 1839
		[DllImport("libc", SetLastError = true)]
		public static extern int geteuid();

		// Token: 0x06000730 RID: 1840
		[DllImport("libc", SetLastError = true)]
		public static extern int getegid();

		// Token: 0x06000731 RID: 1841
		[DllImport("libc", SetLastError = true)]
		public static extern int setpgid(int pid, int pgid);

		// Token: 0x06000732 RID: 1842
		[DllImport("libc")]
		public static extern int umask(int umask);

		// Token: 0x06000733 RID: 1843
		[DllImport("libc", SetLastError = true)]
		public static extern int chroot(string path);

		// Token: 0x06000734 RID: 1844
		[DllImport("libc", SetLastError = true)]
		public static extern int dup2(int oldFileDescriptor, int newFileDescriptor);

		// Token: 0x06000735 RID: 1845
		[DllImport("libc", SetLastError = true)]
		public static extern int getppid();

		// Token: 0x06000736 RID: 1846
		[DllImport("libc", SetLastError = true)]
		public static extern int getpgrp();

		// Token: 0x06000737 RID: 1847
		[DllImport("libc", SetLastError = true)]
		public static extern int setsid();

		// Token: 0x06000738 RID: 1848
		[DllImport("libc", SetLastError = true)]
		public static extern int setreuid(int ruid, int euid);

		// Token: 0x06000739 RID: 1849
		[DllImport("libc", SetLastError = true)]
		public static extern int setregid(int rgid, int egid);

		// Token: 0x0600073A RID: 1850
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern string helper_Mono_Posix_GetUserName(int uid);

		// Token: 0x0600073B RID: 1851
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern string helper_Mono_Posix_GetGroupName(int gid);

		// Token: 0x0600073C RID: 1852 RVA: 0x0001019D File Offset: 0x0000E39D
		public static string getusername(int uid)
		{
			return Syscall.helper_Mono_Posix_GetUserName(uid);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000101A5 File Offset: 0x0000E3A5
		public static string getgroupname(int gid)
		{
			return Syscall.helper_Mono_Posix_GetGroupName(gid);
		}

		// Token: 0x0600073E RID: 1854
		[DllImport("libc", EntryPoint = "gethostname", SetLastError = true)]
		private static extern int syscall_gethostname(byte[] p, int len);

		// Token: 0x0600073F RID: 1855 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public static string GetHostName()
		{
			byte[] array = new byte[256];
			int num = Syscall.syscall_gethostname(array, array.Length);
			if (num == -1)
			{
				return "localhost";
			}
			num = 0;
			while (num < array.Length && array[num] != 0)
			{
				num++;
			}
			return Encoding.UTF8.GetString(array, 0, num);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000101FC File Offset: 0x0000E3FC
		[CLSCompliant(false)]
		public static string gethostname()
		{
			return Syscall.GetHostName();
		}

		// Token: 0x06000741 RID: 1857
		[DllImport("libc", EntryPoint = "isatty")]
		private static extern int syscall_isatty(int desc);

		// Token: 0x06000742 RID: 1858 RVA: 0x00010203 File Offset: 0x0000E403
		public static bool isatty(int desc)
		{
			return Syscall.syscall_isatty(desc) == 1;
		}

		// Token: 0x06000743 RID: 1859
		[DllImport("MonoPosixHelper")]
		internal static extern int helper_Mono_Posix_Stat(string filename, bool dereference, out int device, out int inode, out int mode, out int nlinks, out int uid, out int gid, out int rdev, out long size, out long blksize, out long blocks, out long atime, out long mtime, out long ctime);

		// Token: 0x06000744 RID: 1860 RVA: 0x00010214 File Offset: 0x0000E414
		private static int stat2(string filename, bool dereference, out Stat stat)
		{
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			int num8;
			long num9;
			long num10;
			long num11;
			long num12;
			long num13;
			long num14;
			int num = Syscall.helper_Mono_Posix_Stat(filename, dereference, out num2, out num3, out num4, out num5, out num6, out num7, out num8, out num9, out num10, out num11, out num12, out num13, out num14);
			stat = new Stat(num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, num12, num13, num14);
			if (num != 0)
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001026D File Offset: 0x0000E46D
		public static int stat(string filename, out Stat stat)
		{
			return Syscall.stat2(filename, false, out stat);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00010277 File Offset: 0x0000E477
		public static int lstat(string filename, out Stat stat)
		{
			return Syscall.stat2(filename, true, out stat);
		}

		// Token: 0x06000747 RID: 1863
		[DllImport("libc")]
		private static extern int readlink(string path, byte[] buffer, int buflen);

		// Token: 0x06000748 RID: 1864 RVA: 0x00010284 File Offset: 0x0000E484
		public static string readlink(string path)
		{
			byte[] array = new byte[512];
			int num = Syscall.readlink(path, array, array.Length);
			if (num == -1)
			{
				return null;
			}
			char[] array2 = new char[512];
			int chars = Encoding.Default.GetChars(array, 0, num, array2, 0);
			return new string(array2, 0, chars);
		}

		// Token: 0x06000749 RID: 1865
		[DllImport("libc", EntryPoint = "strerror")]
		private static extern IntPtr _strerror(int errnum);

		// Token: 0x0600074A RID: 1866 RVA: 0x000102D0 File Offset: 0x0000E4D0
		public static string strerror(int errnum)
		{
			return Marshal.PtrToStringAnsi(Syscall._strerror(errnum));
		}

		// Token: 0x0600074B RID: 1867
		[DllImport("libc")]
		public static extern IntPtr opendir(string path);

		// Token: 0x0600074C RID: 1868
		[DllImport("libc")]
		public static extern int closedir(IntPtr dir);

		// Token: 0x0600074D RID: 1869
		[DllImport("MonoPosixHelper", EntryPoint = "helper_Mono_Posix_readdir")]
		public static extern string readdir(IntPtr dir);

		// Token: 0x020000B0 RID: 176
		// (Invoke) Token: 0x06000778 RID: 1912
		public delegate void sighandler_t(int v);
	}
}
