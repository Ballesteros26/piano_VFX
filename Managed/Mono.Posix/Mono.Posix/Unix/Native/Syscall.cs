using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x02000070 RID: 112
	[CLSCompliant(false)]
	public sealed class Syscall : Stdlib
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x0000BE8F File Offset: 0x0000A08F
		private Syscall()
		{
		}

		// Token: 0x06000494 RID: 1172
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setxattr", SetLastError = true)]
		public static extern int setxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size, XattrFlags flags);

		// Token: 0x06000495 RID: 1173 RVA: 0x0000BE97 File Offset: 0x0000A097
		public static int setxattr(string path, string name, byte[] value, ulong size)
		{
			return Syscall.setxattr(path, name, value, size, XattrFlags.XATTR_AUTO);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000BEA3 File Offset: 0x0000A0A3
		public static int setxattr(string path, string name, byte[] value, XattrFlags flags)
		{
			return Syscall.setxattr(path, name, value, (ulong)((long)value.Length), flags);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		public static int setxattr(string path, string name, byte[] value)
		{
			return Syscall.setxattr(path, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x06000498 RID: 1176
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lsetxattr", SetLastError = true)]
		public static extern int lsetxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size, XattrFlags flags);

		// Token: 0x06000499 RID: 1177 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		public static int lsetxattr(string path, string name, byte[] value, ulong size)
		{
			return Syscall.lsetxattr(path, name, value, size, XattrFlags.XATTR_AUTO);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000BECC File Offset: 0x0000A0CC
		public static int lsetxattr(string path, string name, byte[] value, XattrFlags flags)
		{
			return Syscall.lsetxattr(path, name, value, (ulong)((long)value.Length), flags);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000BEDB File Offset: 0x0000A0DB
		public static int lsetxattr(string path, string name, byte[] value)
		{
			return Syscall.lsetxattr(path, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x0600049C RID: 1180
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fsetxattr", SetLastError = true)]
		public static extern int fsetxattr(int fd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size, XattrFlags flags);

		// Token: 0x0600049D RID: 1181 RVA: 0x0000BEE9 File Offset: 0x0000A0E9
		public static int fsetxattr(int fd, string name, byte[] value, ulong size)
		{
			return Syscall.fsetxattr(fd, name, value, size, XattrFlags.XATTR_AUTO);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000BEF5 File Offset: 0x0000A0F5
		public static int fsetxattr(int fd, string name, byte[] value, XattrFlags flags)
		{
			return Syscall.fsetxattr(fd, name, value, (ulong)((long)value.Length), flags);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000BF04 File Offset: 0x0000A104
		public static int fsetxattr(int fd, string name, byte[] value)
		{
			return Syscall.fsetxattr(fd, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060004A0 RID: 1184
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getxattr", SetLastError = true)]
		public static extern long getxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size);

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000BF12 File Offset: 0x0000A112
		public static long getxattr(string path, string name, byte[] value)
		{
			return Syscall.getxattr(path, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000BF20 File Offset: 0x0000A120
		public static long getxattr(string path, string name, out byte[] value)
		{
			value = null;
			long num = Syscall.getxattr(path, name, value, 0UL);
			if (num <= 0L)
			{
				return num;
			}
			value = new byte[num];
			return Syscall.getxattr(path, name, value, (ulong)num);
		}

		// Token: 0x060004A3 RID: 1187
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lgetxattr", SetLastError = true)]
		public static extern long lgetxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size);

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000BF56 File Offset: 0x0000A156
		public static long lgetxattr(string path, string name, byte[] value)
		{
			return Syscall.lgetxattr(path, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000BF64 File Offset: 0x0000A164
		public static long lgetxattr(string path, string name, out byte[] value)
		{
			value = null;
			long num = Syscall.lgetxattr(path, name, value, 0UL);
			if (num <= 0L)
			{
				return num;
			}
			value = new byte[num];
			return Syscall.lgetxattr(path, name, value, (ulong)num);
		}

		// Token: 0x060004A6 RID: 1190
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fgetxattr", SetLastError = true)]
		public static extern long fgetxattr(int fd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size);

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000BF9A File Offset: 0x0000A19A
		public static long fgetxattr(int fd, string name, byte[] value)
		{
			return Syscall.fgetxattr(fd, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		public static long fgetxattr(int fd, string name, out byte[] value)
		{
			value = null;
			long num = Syscall.fgetxattr(fd, name, value, 0UL);
			if (num <= 0L)
			{
				return num;
			}
			value = new byte[num];
			return Syscall.fgetxattr(fd, name, value, (ulong)num);
		}

		// Token: 0x060004A9 RID: 1193
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_listxattr", SetLastError = true)]
		public static extern long listxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, byte[] list, ulong size);

		// Token: 0x060004AA RID: 1194 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		public static long listxattr(string path, Encoding encoding, out string[] values)
		{
			values = null;
			long num = Syscall.listxattr(path, null, 0UL);
			if (num == 0L)
			{
				values = new string[0];
			}
			if (num <= 0L)
			{
				return (long)((int)num);
			}
			byte[] array = new byte[num];
			long num2 = Syscall.listxattr(path, array, (ulong)num);
			if (num2 < 0L)
			{
				return (long)((int)num2);
			}
			Syscall.GetValues(array, encoding, out values);
			return 0L;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000C032 File Offset: 0x0000A232
		public static long listxattr(string path, out string[] values)
		{
			return Syscall.listxattr(path, UnixEncoding.Instance, out values);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000C040 File Offset: 0x0000A240
		private static void GetValues(byte[] list, Encoding encoding, out string[] values)
		{
			int num = 0;
			for (int i = 0; i < list.Length; i++)
			{
				if (list[i] == 0)
				{
					num++;
				}
			}
			values = new string[num];
			num = 0;
			int num2 = 0;
			for (int j = 0; j < list.Length; j++)
			{
				if (list[j] == 0)
				{
					values[num++] = encoding.GetString(list, num2, j - num2);
					num2 = j + 1;
				}
			}
		}

		// Token: 0x060004AD RID: 1197
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_llistxattr", SetLastError = true)]
		public static extern long llistxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, byte[] list, ulong size);

		// Token: 0x060004AE RID: 1198 RVA: 0x0000C09C File Offset: 0x0000A29C
		public static long llistxattr(string path, Encoding encoding, out string[] values)
		{
			values = null;
			long num = Syscall.llistxattr(path, null, 0UL);
			if (num == 0L)
			{
				values = new string[0];
			}
			if (num <= 0L)
			{
				return (long)((int)num);
			}
			byte[] array = new byte[num];
			long num2 = Syscall.llistxattr(path, array, (ulong)num);
			if (num2 < 0L)
			{
				return (long)((int)num2);
			}
			Syscall.GetValues(array, encoding, out values);
			return 0L;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000C0EE File Offset: 0x0000A2EE
		public static long llistxattr(string path, out string[] values)
		{
			return Syscall.llistxattr(path, UnixEncoding.Instance, out values);
		}

		// Token: 0x060004B0 RID: 1200
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_flistxattr", SetLastError = true)]
		public static extern long flistxattr(int fd, byte[] list, ulong size);

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		public static long flistxattr(int fd, Encoding encoding, out string[] values)
		{
			values = null;
			long num = Syscall.flistxattr(fd, null, 0UL);
			if (num == 0L)
			{
				values = new string[0];
			}
			if (num <= 0L)
			{
				return (long)((int)num);
			}
			byte[] array = new byte[num];
			long num2 = Syscall.flistxattr(fd, array, (ulong)num);
			if (num2 < 0L)
			{
				return (long)((int)num2);
			}
			Syscall.GetValues(array, encoding, out values);
			return 0L;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000C14E File Offset: 0x0000A34E
		public static long flistxattr(int fd, out string[] values)
		{
			return Syscall.flistxattr(fd, UnixEncoding.Instance, out values);
		}

		// Token: 0x060004B3 RID: 1203
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_removexattr", SetLastError = true)]
		public static extern int removexattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name);

		// Token: 0x060004B4 RID: 1204
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lremovexattr", SetLastError = true)]
		public static extern int lremovexattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name);

		// Token: 0x060004B5 RID: 1205
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fremovexattr", SetLastError = true)]
		public static extern int fremovexattr(int fd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name);

		// Token: 0x060004B6 RID: 1206
		[DllImport("libc", SetLastError = true)]
		public static extern IntPtr opendir([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name);

		// Token: 0x060004B7 RID: 1207
		[DllImport("libc", SetLastError = true)]
		public static extern int closedir(IntPtr dir);

		// Token: 0x060004B8 RID: 1208
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_seekdir", SetLastError = true)]
		public static extern int seekdir(IntPtr dir, long offset);

		// Token: 0x060004B9 RID: 1209
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_telldir", SetLastError = true)]
		public static extern long telldir(IntPtr dir);

		// Token: 0x060004BA RID: 1210
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_rewinddir", SetLastError = true)]
		public static extern int rewinddir(IntPtr dir);

		// Token: 0x060004BB RID: 1211 RVA: 0x0000C15C File Offset: 0x0000A35C
		private static void CopyDirent(Dirent to, ref Syscall._Dirent from)
		{
			try
			{
				to.d_ino = from.d_ino;
				to.d_off = from.d_off;
				to.d_reclen = from.d_reclen;
				to.d_type = from.d_type;
				to.d_name = UnixMarshal.PtrToString(from.d_name);
			}
			finally
			{
				Stdlib.free(from.d_name);
				from.d_name = IntPtr.Zero;
			}
		}

		// Token: 0x060004BC RID: 1212
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_readdir", SetLastError = true)]
		private static extern int sys_readdir(IntPtr dir, out Syscall._Dirent dentry);

		// Token: 0x060004BD RID: 1213 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		public static Dirent readdir(IntPtr dir)
		{
			object obj = Syscall.readdir_lock;
			Syscall._Dirent dirent;
			int num;
			lock (obj)
			{
				num = Syscall.sys_readdir(dir, out dirent);
			}
			if (num != 0)
			{
				return null;
			}
			Dirent dirent2 = new Dirent();
			Syscall.CopyDirent(dirent2, ref dirent);
			return dirent2;
		}

		// Token: 0x060004BE RID: 1214
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_readdir_r", SetLastError = true)]
		private static extern int sys_readdir_r(IntPtr dirp, out Syscall._Dirent entry, out IntPtr result);

		// Token: 0x060004BF RID: 1215 RVA: 0x0000C228 File Offset: 0x0000A428
		public static int readdir_r(IntPtr dirp, Dirent entry, out IntPtr result)
		{
			entry.d_ino = 0UL;
			entry.d_off = 0L;
			entry.d_reclen = 0;
			entry.d_type = 0;
			entry.d_name = null;
			Syscall._Dirent dirent;
			int num = Syscall.sys_readdir_r(dirp, out dirent, out result);
			if (num == 0 && result != IntPtr.Zero)
			{
				Syscall.CopyDirent(entry, ref dirent);
			}
			return num;
		}

		// Token: 0x060004C0 RID: 1216
		[DllImport("libc", SetLastError = true)]
		public static extern int dirfd(IntPtr dir);

		// Token: 0x060004C1 RID: 1217
		[DllImport("libc", SetLastError = true)]
		public static extern IntPtr fdopendir(int fd);

		// Token: 0x060004C2 RID: 1218
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fcntl", SetLastError = true)]
		public static extern int fcntl(int fd, FcntlCommand cmd);

		// Token: 0x060004C3 RID: 1219
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fcntl_arg", SetLastError = true)]
		public static extern int fcntl(int fd, FcntlCommand cmd, long arg);

		// Token: 0x060004C4 RID: 1220
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fcntl_arg_int", SetLastError = true)]
		public static extern int fcntl(int fd, FcntlCommand cmd, int arg);

		// Token: 0x060004C5 RID: 1221
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fcntl_arg_ptr", SetLastError = true)]
		public static extern int fcntl(int fd, FcntlCommand cmd, IntPtr ptr);

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000C27C File Offset: 0x0000A47C
		public static int fcntl(int fd, FcntlCommand cmd, DirectoryNotifyFlags arg)
		{
			if (cmd != FcntlCommand.F_NOTIFY)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			long num = (long)NativeConvert.FromDirectoryNotifyFlags(arg);
			return Syscall.fcntl(fd, FcntlCommand.F_NOTIFY, num);
		}

		// Token: 0x060004C7 RID: 1223
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fcntl_lock", SetLastError = true)]
		public static extern int fcntl(int fd, FcntlCommand cmd, ref Flock @lock);

		// Token: 0x060004C8 RID: 1224
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_open", SetLastError = true)]
		public static extern int open([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, OpenFlags flags);

		// Token: 0x060004C9 RID: 1225
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_open_mode", SetLastError = true)]
		public static extern int open([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, OpenFlags flags, FilePermissions mode);

		// Token: 0x060004CA RID: 1226
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_creat", SetLastError = true)]
		public static extern int creat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, FilePermissions mode);

		// Token: 0x060004CB RID: 1227
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_posix_fadvise", SetLastError = true)]
		public static extern int posix_fadvise(int fd, long offset, long len, PosixFadviseAdvice advice);

		// Token: 0x060004CC RID: 1228
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_posix_fallocate", SetLastError = true)]
		public static extern int posix_fallocate(int fd, long offset, ulong len);

		// Token: 0x060004CD RID: 1229
		[DllImport("libc", EntryPoint = "openat", SetLastError = true)]
		private static extern int sys_openat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, int flags);

		// Token: 0x060004CE RID: 1230
		[DllImport("libc", EntryPoint = "openat", SetLastError = true)]
		private static extern int sys_openat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, int flags, uint mode);

		// Token: 0x060004CF RID: 1231 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		public static int openat(int dirfd, string pathname, OpenFlags flags)
		{
			int num = NativeConvert.FromOpenFlags(flags);
			return Syscall.sys_openat(dirfd, pathname, num);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		public static int openat(int dirfd, string pathname, OpenFlags flags, FilePermissions mode)
		{
			int num = NativeConvert.FromOpenFlags(flags);
			uint num2 = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_openat(dirfd, pathname, num, num2);
		}

		// Token: 0x060004D1 RID: 1233
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_get_at_fdcwd", SetLastError = true)]
		private static extern int get_at_fdcwd();

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		private static void CopyFstab(Fstab to, ref Syscall._Fstab from)
		{
			try
			{
				to.fs_spec = UnixMarshal.PtrToString(from.fs_spec);
				to.fs_file = UnixMarshal.PtrToString(from.fs_file);
				to.fs_vfstype = UnixMarshal.PtrToString(from.fs_vfstype);
				to.fs_mntops = UnixMarshal.PtrToString(from.fs_mntops);
				to.fs_type = UnixMarshal.PtrToString(from.fs_type);
				to.fs_freq = from.fs_freq;
				to.fs_passno = from.fs_passno;
			}
			finally
			{
				Stdlib.free(from._fs_buf_);
				from._fs_buf_ = IntPtr.Zero;
			}
		}

		// Token: 0x060004D3 RID: 1235
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_endfsent", SetLastError = true)]
		private static extern int sys_endfsent();

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000C394 File Offset: 0x0000A594
		public static int endfsent()
		{
			object obj = Syscall.fstab_lock;
			int num;
			lock (obj)
			{
				num = Syscall.sys_endfsent();
			}
			return num;
		}

		// Token: 0x060004D5 RID: 1237
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getfsent", SetLastError = true)]
		private static extern int sys_getfsent(out Syscall._Fstab fs);

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		public static Fstab getfsent()
		{
			object obj = Syscall.fstab_lock;
			Syscall._Fstab fstab;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getfsent(out fstab);
			}
			if (num != 0)
			{
				return null;
			}
			Fstab fstab2 = new Fstab();
			Syscall.CopyFstab(fstab2, ref fstab);
			return fstab2;
		}

		// Token: 0x060004D7 RID: 1239
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getfsfile", SetLastError = true)]
		private static extern int sys_getfsfile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string mount_point, out Syscall._Fstab fs);

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000C428 File Offset: 0x0000A628
		public static Fstab getfsfile(string mount_point)
		{
			object obj = Syscall.fstab_lock;
			Syscall._Fstab fstab;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getfsfile(mount_point, out fstab);
			}
			if (num != 0)
			{
				return null;
			}
			Fstab fstab2 = new Fstab();
			Syscall.CopyFstab(fstab2, ref fstab);
			return fstab2;
		}

		// Token: 0x060004D9 RID: 1241
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getfsspec", SetLastError = true)]
		private static extern int sys_getfsspec([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string special_file, out Syscall._Fstab fs);

		// Token: 0x060004DA RID: 1242 RVA: 0x0000C47C File Offset: 0x0000A67C
		public static Fstab getfsspec(string special_file)
		{
			object obj = Syscall.fstab_lock;
			Syscall._Fstab fstab;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getfsspec(special_file, out fstab);
			}
			if (num != 0)
			{
				return null;
			}
			Fstab fstab2 = new Fstab();
			Syscall.CopyFstab(fstab2, ref fstab);
			return fstab2;
		}

		// Token: 0x060004DB RID: 1243
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setfsent", SetLastError = true)]
		private static extern int sys_setfsent();

		// Token: 0x060004DC RID: 1244 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public static int setfsent()
		{
			object obj = Syscall.fstab_lock;
			int num;
			lock (obj)
			{
				num = Syscall.sys_setfsent();
			}
			return num;
		}

		// Token: 0x060004DD RID: 1245
		[DllImport("libc", EntryPoint = "getgrouplist", SetLastError = true)]
		private static extern int sys_getgrouplist(string user, uint grp, uint[] groups, ref int ngroups);

		// Token: 0x060004DE RID: 1246 RVA: 0x0000C510 File Offset: 0x0000A710
		public static Group[] getgrouplist(string username)
		{
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			if (username.Trim() == "")
			{
				throw new ArgumentException("Username cannot be empty", "username");
			}
			Passwd passwd = Syscall.getpwnam(username);
			if (passwd == null)
			{
				throw new ArgumentException(string.Format("User {0} does not exist", username), "username");
			}
			return Syscall.getgrouplist(passwd);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000C578 File Offset: 0x0000A778
		public static Group[] getgrouplist(Passwd user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			int num = 8;
			uint[] array = null;
			int num2;
			do
			{
				Array.Resize<uint>(ref array, num *= 2);
				num2 = Syscall.sys_getgrouplist(user.pw_name, user.pw_gid, array, ref num);
			}
			while (num2 == -1);
			List<Group> list = new List<Group>();
			for (int i = 0; i < num2; i++)
			{
				Group group = Syscall.getgrgid(array[i]);
				if (group != null)
				{
					list.Add(group);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060004E0 RID: 1248
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setgroups", SetLastError = true)]
		public static extern int setgroups(ulong size, uint[] list);

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000C600 File Offset: 0x0000A800
		public static int setgroups(uint[] list)
		{
			return Syscall.setgroups((ulong)((long)list.Length), list);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000C60C File Offset: 0x0000A80C
		private static void CopyGroup(Group to, ref Syscall._Group from)
		{
			try
			{
				to.gr_gid = from.gr_gid;
				to.gr_name = UnixMarshal.PtrToString(from.gr_name);
				to.gr_passwd = UnixMarshal.PtrToString(from.gr_passwd);
				to.gr_mem = UnixMarshal.PtrToStringArray(from._gr_nmem_, from.gr_mem);
			}
			finally
			{
				Stdlib.free(from.gr_mem);
				Stdlib.free(from._gr_buf_);
				from.gr_mem = IntPtr.Zero;
				from._gr_buf_ = IntPtr.Zero;
			}
		}

		// Token: 0x060004E3 RID: 1251
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrnam", SetLastError = true)]
		private static extern int sys_getgrnam(string name, out Syscall._Group group);

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		public static Group getgrnam(string name)
		{
			object obj = Syscall.grp_lock;
			Syscall._Group group;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getgrnam(name, out group);
			}
			if (num != 0)
			{
				return null;
			}
			Group group2 = new Group();
			Syscall.CopyGroup(group2, ref group);
			return group2;
		}

		// Token: 0x060004E5 RID: 1253
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrgid", SetLastError = true)]
		private static extern int sys_getgrgid(uint uid, out Syscall._Group group);

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		public static Group getgrgid(uint uid)
		{
			object obj = Syscall.grp_lock;
			Syscall._Group group;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getgrgid(uid, out group);
			}
			if (num != 0)
			{
				return null;
			}
			Group group2 = new Group();
			Syscall.CopyGroup(group2, ref group);
			return group2;
		}

		// Token: 0x060004E7 RID: 1255
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrnam_r", SetLastError = true)]
		private static extern int sys_getgrnam_r([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, out Syscall._Group grbuf, out IntPtr grbufp);

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000C748 File Offset: 0x0000A948
		public static int getgrnam_r(string name, Group grbuf, out Group grbufp)
		{
			grbufp = null;
			Syscall._Group group;
			IntPtr intPtr;
			int num = Syscall.sys_getgrnam_r(name, out group, out intPtr);
			if (num == 0 && intPtr != IntPtr.Zero)
			{
				Syscall.CopyGroup(grbuf, ref group);
				grbufp = grbuf;
			}
			return num;
		}

		// Token: 0x060004E9 RID: 1257
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrgid_r", SetLastError = true)]
		private static extern int sys_getgrgid_r(uint uid, out Syscall._Group grbuf, out IntPtr grbufp);

		// Token: 0x060004EA RID: 1258 RVA: 0x0000C780 File Offset: 0x0000A980
		public static int getgrgid_r(uint uid, Group grbuf, out Group grbufp)
		{
			grbufp = null;
			Syscall._Group group;
			IntPtr intPtr;
			int num = Syscall.sys_getgrgid_r(uid, out group, out intPtr);
			if (num == 0 && intPtr != IntPtr.Zero)
			{
				Syscall.CopyGroup(grbuf, ref group);
				grbufp = grbuf;
			}
			return num;
		}

		// Token: 0x060004EB RID: 1259
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrent", SetLastError = true)]
		private static extern int sys_getgrent(out Syscall._Group grbuf);

		// Token: 0x060004EC RID: 1260 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		public static Group getgrent()
		{
			object obj = Syscall.grp_lock;
			Syscall._Group group;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getgrent(out group);
			}
			if (num != 0)
			{
				return null;
			}
			Group group2 = new Group();
			Syscall.CopyGroup(group2, ref group);
			return group2;
		}

		// Token: 0x060004ED RID: 1261
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setgrent", SetLastError = true)]
		private static extern int sys_setgrent();

		// Token: 0x060004EE RID: 1262 RVA: 0x0000C80C File Offset: 0x0000AA0C
		public static int setgrent()
		{
			object obj = Syscall.grp_lock;
			int num;
			lock (obj)
			{
				num = Syscall.sys_setgrent();
			}
			return num;
		}

		// Token: 0x060004EF RID: 1263
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_endgrent", SetLastError = true)]
		private static extern int sys_endgrent();

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000C84C File Offset: 0x0000AA4C
		public static int endgrent()
		{
			object obj = Syscall.grp_lock;
			int num;
			lock (obj)
			{
				num = Syscall.sys_endgrent();
			}
			return num;
		}

		// Token: 0x060004F1 RID: 1265
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fgetgrent", SetLastError = true)]
		private static extern int sys_fgetgrent(IntPtr stream, out Syscall._Group grbuf);

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000C88C File Offset: 0x0000AA8C
		public static Group fgetgrent(IntPtr stream)
		{
			object obj = Syscall.grp_lock;
			Syscall._Group group;
			int num;
			lock (obj)
			{
				num = Syscall.sys_fgetgrent(stream, out group);
			}
			if (num != 0)
			{
				return null;
			}
			Group group2 = new Group();
			Syscall.CopyGroup(group2, ref group);
			return group2;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
		private static void CopyPasswd(Passwd to, ref Syscall._Passwd from)
		{
			try
			{
				to.pw_name = UnixMarshal.PtrToString(from.pw_name);
				to.pw_passwd = UnixMarshal.PtrToString(from.pw_passwd);
				to.pw_uid = from.pw_uid;
				to.pw_gid = from.pw_gid;
				to.pw_gecos = UnixMarshal.PtrToString(from.pw_gecos);
				to.pw_dir = UnixMarshal.PtrToString(from.pw_dir);
				to.pw_shell = UnixMarshal.PtrToString(from.pw_shell);
			}
			finally
			{
				Stdlib.free(from._pw_buf_);
				from._pw_buf_ = IntPtr.Zero;
			}
		}

		// Token: 0x060004F4 RID: 1268
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwnam", SetLastError = true)]
		private static extern int sys_getpwnam(string name, out Syscall._Passwd passwd);

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000C984 File Offset: 0x0000AB84
		public static Passwd getpwnam(string name)
		{
			object obj = Syscall.pwd_lock;
			Syscall._Passwd passwd;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getpwnam(name, out passwd);
			}
			if (num != 0)
			{
				return null;
			}
			Passwd passwd2 = new Passwd();
			Syscall.CopyPasswd(passwd2, ref passwd);
			return passwd2;
		}

		// Token: 0x060004F6 RID: 1270
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwuid", SetLastError = true)]
		private static extern int sys_getpwuid(uint uid, out Syscall._Passwd passwd);

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		public static Passwd getpwuid(uint uid)
		{
			object obj = Syscall.pwd_lock;
			Syscall._Passwd passwd;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getpwuid(uid, out passwd);
			}
			if (num != 0)
			{
				return null;
			}
			Passwd passwd2 = new Passwd();
			Syscall.CopyPasswd(passwd2, ref passwd);
			return passwd2;
		}

		// Token: 0x060004F8 RID: 1272
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwnam_r", SetLastError = true)]
		private static extern int sys_getpwnam_r([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, out Syscall._Passwd pwbuf, out IntPtr pwbufp);

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000CA2C File Offset: 0x0000AC2C
		public static int getpwnam_r(string name, Passwd pwbuf, out Passwd pwbufp)
		{
			pwbufp = null;
			Syscall._Passwd passwd;
			IntPtr intPtr;
			int num = Syscall.sys_getpwnam_r(name, out passwd, out intPtr);
			if (num == 0 && intPtr != IntPtr.Zero)
			{
				Syscall.CopyPasswd(pwbuf, ref passwd);
				pwbufp = pwbuf;
			}
			return num;
		}

		// Token: 0x060004FA RID: 1274
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwuid_r", SetLastError = true)]
		private static extern int sys_getpwuid_r(uint uid, out Syscall._Passwd pwbuf, out IntPtr pwbufp);

		// Token: 0x060004FB RID: 1275 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public static int getpwuid_r(uint uid, Passwd pwbuf, out Passwd pwbufp)
		{
			pwbufp = null;
			Syscall._Passwd passwd;
			IntPtr intPtr;
			int num = Syscall.sys_getpwuid_r(uid, out passwd, out intPtr);
			if (num == 0 && intPtr != IntPtr.Zero)
			{
				Syscall.CopyPasswd(pwbuf, ref passwd);
				pwbufp = pwbuf;
			}
			return num;
		}

		// Token: 0x060004FC RID: 1276
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwent", SetLastError = true)]
		private static extern int sys_getpwent(out Syscall._Passwd pwbuf);

		// Token: 0x060004FD RID: 1277 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		public static Passwd getpwent()
		{
			object obj = Syscall.pwd_lock;
			Syscall._Passwd passwd;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getpwent(out passwd);
			}
			if (num != 0)
			{
				return null;
			}
			Passwd passwd2 = new Passwd();
			Syscall.CopyPasswd(passwd2, ref passwd);
			return passwd2;
		}

		// Token: 0x060004FE RID: 1278
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setpwent", SetLastError = true)]
		private static extern int sys_setpwent();

		// Token: 0x060004FF RID: 1279 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		public static int setpwent()
		{
			object obj = Syscall.pwd_lock;
			int num;
			lock (obj)
			{
				num = Syscall.sys_setpwent();
			}
			return num;
		}

		// Token: 0x06000500 RID: 1280
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_endpwent", SetLastError = true)]
		private static extern int sys_endpwent();

		// Token: 0x06000501 RID: 1281 RVA: 0x0000CB30 File Offset: 0x0000AD30
		public static int endpwent()
		{
			object obj = Syscall.pwd_lock;
			int num;
			lock (obj)
			{
				num = Syscall.sys_endpwent();
			}
			return num;
		}

		// Token: 0x06000502 RID: 1282
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fgetpwent", SetLastError = true)]
		private static extern int sys_fgetpwent(IntPtr stream, out Syscall._Passwd pwbuf);

		// Token: 0x06000503 RID: 1283 RVA: 0x0000CB70 File Offset: 0x0000AD70
		public static Passwd fgetpwent(IntPtr stream)
		{
			object obj = Syscall.pwd_lock;
			Syscall._Passwd passwd;
			int num;
			lock (obj)
			{
				num = Syscall.sys_fgetpwent(stream, out passwd);
			}
			if (num != 0)
			{
				return null;
			}
			Passwd passwd2 = new Passwd();
			Syscall.CopyPasswd(passwd2, ref passwd);
			return passwd2;
		}

		// Token: 0x06000504 RID: 1284
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_psignal", SetLastError = true)]
		private static extern int psignal(int sig, string s);

		// Token: 0x06000505 RID: 1285 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		public static int psignal(Signum sig, string s)
		{
			return Syscall.psignal(NativeConvert.FromSignum(sig), s);
		}

		// Token: 0x06000506 RID: 1286
		[DllImport("libc", EntryPoint = "kill", SetLastError = true)]
		private static extern int sys_kill(int pid, int sig);

		// Token: 0x06000507 RID: 1287 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
		public static int kill(int pid, Signum sig)
		{
			int num = NativeConvert.FromSignum(sig);
			return Syscall.sys_kill(pid, num);
		}

		// Token: 0x06000508 RID: 1288
		[DllImport("libc", EntryPoint = "strsignal", SetLastError = true)]
		private static extern IntPtr sys_strsignal(int sig);

		// Token: 0x06000509 RID: 1289 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public static string strsignal(Signum sig)
		{
			int num = NativeConvert.FromSignum(sig);
			object obj = Syscall.signal_lock;
			string text;
			lock (obj)
			{
				text = UnixMarshal.PtrToString(Syscall.sys_strsignal(num));
			}
			return text;
		}

		// Token: 0x0600050A RID: 1290
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_L_ctermid")]
		private static extern int _L_ctermid();

		// Token: 0x0600050B RID: 1291
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_L_cuserid")]
		private static extern int _L_cuserid();

		// Token: 0x0600050C RID: 1292
		[DllImport("libc", EntryPoint = "cuserid", SetLastError = true)]
		private static extern IntPtr sys_cuserid([Out] StringBuilder @string);

		// Token: 0x0600050D RID: 1293 RVA: 0x0000CC40 File Offset: 0x0000AE40
		[Obsolete("\"Nobody knows precisely what cuserid() does... DO NOT USE cuserid().\n`string' must hold L_cuserid characters.  Use getlogin_r instead.")]
		public static string cuserid(StringBuilder @string)
		{
			if (@string.Capacity < Syscall.L_cuserid)
			{
				throw new ArgumentOutOfRangeException("string", "string.Capacity < L_cuserid");
			}
			object obj = Syscall.getlogin_lock;
			string text;
			lock (obj)
			{
				text = UnixMarshal.PtrToString(Syscall.sys_cuserid(@string));
			}
			return text;
		}

		// Token: 0x0600050E RID: 1294
		[DllImport("libc", SetLastError = true)]
		public static extern int renameat(int olddirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, int newdirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string newpath);

		// Token: 0x0600050F RID: 1295
		[DllImport("libc", SetLastError = true)]
		public static extern int mkstemp(StringBuilder template);

		// Token: 0x06000510 RID: 1296
		[DllImport("libc", EntryPoint = "mkdtemp", SetLastError = true)]
		private static extern IntPtr sys_mkdtemp(StringBuilder template);

		// Token: 0x06000511 RID: 1297 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		public static StringBuilder mkdtemp(StringBuilder template)
		{
			if (Syscall.sys_mkdtemp(template) == IntPtr.Zero)
			{
				return null;
			}
			return template;
		}

		// Token: 0x06000512 RID: 1298
		[DllImport("libc", SetLastError = true)]
		public static extern int ttyslot();

		// Token: 0x06000513 RID: 1299 RVA: 0x0000CCBB File Offset: 0x0000AEBB
		[Obsolete("This is insecure and should not be used", true)]
		public static int setkey(string key)
		{
			throw new SecurityException("crypt(3) has been broken.  Use something more secure.");
		}

		// Token: 0x06000514 RID: 1300
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_strerror_r", SetLastError = true)]
		private static extern int sys_strerror_r(int errnum, [Out] StringBuilder buf, ulong n);

		// Token: 0x06000515 RID: 1301 RVA: 0x0000CCC7 File Offset: 0x0000AEC7
		public static int strerror_r(Errno errnum, StringBuilder buf, ulong n)
		{
			return Syscall.sys_strerror_r(NativeConvert.FromErrno(errnum), buf, n);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000CCD6 File Offset: 0x0000AED6
		public static int strerror_r(Errno errnum, StringBuilder buf)
		{
			return Syscall.strerror_r(errnum, buf, (ulong)((long)buf.Capacity));
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000CCE6 File Offset: 0x0000AEE6
		public static int epoll_create(int size)
		{
			return Syscall.sys_epoll_create(size);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000CCEE File Offset: 0x0000AEEE
		public static int epoll_create(EpollFlags flags)
		{
			return Syscall.sys_epoll_create1(flags);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		public static int epoll_ctl(int epfd, EpollOp op, int fd, EpollEvents events)
		{
			EpollEvent epollEvent = default(EpollEvent);
			epollEvent.events = events;
			epollEvent.fd = fd;
			return Syscall.epoll_ctl(epfd, op, fd, ref epollEvent);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000CD27 File Offset: 0x0000AF27
		public static int epoll_wait(int epfd, EpollEvent[] events, int max_events, int timeout)
		{
			if (events.Length < max_events)
			{
				throw new ArgumentOutOfRangeException("events", "Must refer to at least 'max_events' elements.");
			}
			return Syscall.sys_epoll_wait(epfd, events, max_events, timeout);
		}

		// Token: 0x0600051B RID: 1307
		[DllImport("libc", EntryPoint = "epoll_create", SetLastError = true)]
		private static extern int sys_epoll_create(int size);

		// Token: 0x0600051C RID: 1308
		[DllImport("libc", EntryPoint = "epoll_create1", SetLastError = true)]
		private static extern int sys_epoll_create1(EpollFlags flags);

		// Token: 0x0600051D RID: 1309
		[DllImport("libc", SetLastError = true)]
		public static extern int epoll_ctl(int epfd, EpollOp op, int fd, ref EpollEvent ee);

		// Token: 0x0600051E RID: 1310
		[DllImport("libc", EntryPoint = "epoll_wait", SetLastError = true)]
		private static extern int sys_epoll_wait(int epfd, EpollEvent[] ee, int maxevents, int timeout);

		// Token: 0x0600051F RID: 1311
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_posix_madvise", SetLastError = true)]
		public static extern int posix_madvise(IntPtr addr, ulong len, PosixMadviseAdvice advice);

		// Token: 0x06000520 RID: 1312
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mmap", SetLastError = true)]
		public static extern IntPtr mmap(IntPtr start, ulong length, MmapProts prot, MmapFlags flags, int fd, long offset);

		// Token: 0x06000521 RID: 1313
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_munmap", SetLastError = true)]
		public static extern int munmap(IntPtr start, ulong length);

		// Token: 0x06000522 RID: 1314
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mprotect", SetLastError = true)]
		public static extern int mprotect(IntPtr start, ulong len, MmapProts prot);

		// Token: 0x06000523 RID: 1315
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_msync", SetLastError = true)]
		public static extern int msync(IntPtr start, ulong len, MsyncFlags flags);

		// Token: 0x06000524 RID: 1316
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mlock", SetLastError = true)]
		public static extern int mlock(IntPtr start, ulong len);

		// Token: 0x06000525 RID: 1317
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_munlock", SetLastError = true)]
		public static extern int munlock(IntPtr start, ulong len);

		// Token: 0x06000526 RID: 1318
		[DllImport("libc", EntryPoint = "mlockall", SetLastError = true)]
		private static extern int sys_mlockall(int flags);

		// Token: 0x06000527 RID: 1319 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public static int mlockall(MlockallFlags flags)
		{
			return Syscall.sys_mlockall(NativeConvert.FromMlockallFlags(flags));
		}

		// Token: 0x06000528 RID: 1320
		[DllImport("libc", SetLastError = true)]
		public static extern int munlockall();

		// Token: 0x06000529 RID: 1321
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mremap", SetLastError = true)]
		public static extern IntPtr mremap(IntPtr old_address, ulong old_size, ulong new_size, MremapFlags flags);

		// Token: 0x0600052A RID: 1322
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mincore", SetLastError = true)]
		public static extern int mincore(IntPtr start, ulong length, byte[] vec);

		// Token: 0x0600052B RID: 1323
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_remap_file_pages", SetLastError = true)]
		public static extern int remap_file_pages(IntPtr start, ulong size, MmapProts prot, long pgoff, MmapFlags flags);

		// Token: 0x0600052C RID: 1324
		[DllImport("libc", EntryPoint = "poll", SetLastError = true)]
		private static extern int sys_poll(Syscall._pollfd[] ufds, uint nfds, int timeout);

		// Token: 0x0600052D RID: 1325 RVA: 0x0000CD58 File Offset: 0x0000AF58
		public static int poll(Pollfd[] fds, uint nfds, int timeout)
		{
			if ((long)fds.Length < (long)((ulong)nfds))
			{
				throw new ArgumentOutOfRangeException("fds", "Must refer to at least `nfds' elements");
			}
			Syscall._pollfd[] array = new Syscall._pollfd[nfds];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].fd = fds[i].fd;
				array[i].events = NativeConvert.FromPollEvents(fds[i].events);
			}
			int num = Syscall.sys_poll(array, nfds, timeout);
			for (int j = 0; j < array.Length; j++)
			{
				fds[j].revents = NativeConvert.ToPollEvents(array[j].revents);
			}
			return num;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		public static int poll(Pollfd[] fds, int timeout)
		{
			return Syscall.poll(fds, (uint)fds.Length, timeout);
		}

		// Token: 0x0600052F RID: 1327
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sendfile", SetLastError = true)]
		public static extern long sendfile(int out_fd, int in_fd, ref long offset, ulong count);

		// Token: 0x06000530 RID: 1328
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_stat", SetLastError = true)]
		public static extern int stat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string file_name, out Stat buf);

		// Token: 0x06000531 RID: 1329
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fstat", SetLastError = true)]
		public static extern int fstat(int filedes, out Stat buf);

		// Token: 0x06000532 RID: 1330
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lstat", SetLastError = true)]
		public static extern int lstat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string file_name, out Stat buf);

		// Token: 0x06000533 RID: 1331
		[DllImport("libc", EntryPoint = "chmod", SetLastError = true)]
		private static extern int sys_chmod([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, uint mode);

		// Token: 0x06000534 RID: 1332 RVA: 0x0000CE08 File Offset: 0x0000B008
		public static int chmod(string path, FilePermissions mode)
		{
			uint num = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_chmod(path, num);
		}

		// Token: 0x06000535 RID: 1333
		[DllImport("libc", EntryPoint = "fchmod", SetLastError = true)]
		private static extern int sys_fchmod(int filedes, uint mode);

		// Token: 0x06000536 RID: 1334 RVA: 0x0000CE24 File Offset: 0x0000B024
		public static int fchmod(int filedes, FilePermissions mode)
		{
			uint num = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_fchmod(filedes, num);
		}

		// Token: 0x06000537 RID: 1335
		[DllImport("libc", EntryPoint = "umask", SetLastError = true)]
		private static extern uint sys_umask(uint mask);

		// Token: 0x06000538 RID: 1336 RVA: 0x0000CE3F File Offset: 0x0000B03F
		public static FilePermissions umask(FilePermissions mask)
		{
			return NativeConvert.ToFilePermissions(Syscall.sys_umask(NativeConvert.FromFilePermissions(mask)));
		}

		// Token: 0x06000539 RID: 1337
		[DllImport("libc", EntryPoint = "mkdir", SetLastError = true)]
		private static extern int sys_mkdir([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, uint mode);

		// Token: 0x0600053A RID: 1338 RVA: 0x0000CE54 File Offset: 0x0000B054
		public static int mkdir(string oldpath, FilePermissions mode)
		{
			uint num = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_mkdir(oldpath, num);
		}

		// Token: 0x0600053B RID: 1339
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mknod", SetLastError = true)]
		public static extern int mknod([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, FilePermissions mode, ulong dev);

		// Token: 0x0600053C RID: 1340
		[DllImport("libc", EntryPoint = "mkfifo", SetLastError = true)]
		private static extern int sys_mkfifo([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, uint mode);

		// Token: 0x0600053D RID: 1341 RVA: 0x0000CE70 File Offset: 0x0000B070
		public static int mkfifo(string pathname, FilePermissions mode)
		{
			uint num = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_mkfifo(pathname, num);
		}

		// Token: 0x0600053E RID: 1342
		[DllImport("libc", EntryPoint = "fchmodat", SetLastError = true)]
		private static extern int sys_fchmodat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, uint mode, int flags);

		// Token: 0x0600053F RID: 1343 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public static int fchmodat(int dirfd, string pathname, FilePermissions mode, AtFlags flags)
		{
			uint num = NativeConvert.FromFilePermissions(mode);
			int num2 = NativeConvert.FromAtFlags(flags);
			return Syscall.sys_fchmodat(dirfd, pathname, num, num2);
		}

		// Token: 0x06000540 RID: 1344
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fstatat", SetLastError = true)]
		public static extern int fstatat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string file_name, out Stat buf, AtFlags flags);

		// Token: 0x06000541 RID: 1345
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_get_utime_now", SetLastError = true)]
		private static extern long get_utime_now();

		// Token: 0x06000542 RID: 1346
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_get_utime_omit", SetLastError = true)]
		private static extern long get_utime_omit();

		// Token: 0x06000543 RID: 1347
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_futimens", SetLastError = true)]
		private static extern int sys_futimens(int fd, Timespec[] times);

		// Token: 0x06000544 RID: 1348 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		public static int futimens(int fd, Timespec[] times)
		{
			if (times != null && times.Length != 2)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			return Syscall.sys_futimens(fd, times);
		}

		// Token: 0x06000545 RID: 1349
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_utimensat", SetLastError = true)]
		private static extern int sys_utimensat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, Timespec[] times, int flags);

		// Token: 0x06000546 RID: 1350 RVA: 0x0000CECC File Offset: 0x0000B0CC
		public static int utimensat(int dirfd, string pathname, Timespec[] times, AtFlags flags)
		{
			if (times != null && times.Length != 2)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			int num = NativeConvert.FromAtFlags(flags);
			return Syscall.sys_utimensat(dirfd, pathname, times, num);
		}

		// Token: 0x06000547 RID: 1351
		[DllImport("libc", EntryPoint = "mkdirat", SetLastError = true)]
		private static extern int sys_mkdirat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, uint mode);

		// Token: 0x06000548 RID: 1352 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public static int mkdirat(int dirfd, string oldpath, FilePermissions mode)
		{
			uint num = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_mkdirat(dirfd, oldpath, num);
		}

		// Token: 0x06000549 RID: 1353
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mknodat", SetLastError = true)]
		public static extern int mknodat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, FilePermissions mode, ulong dev);

		// Token: 0x0600054A RID: 1354
		[DllImport("libc", EntryPoint = "mkfifoat", SetLastError = true)]
		private static extern int sys_mkfifoat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, uint mode);

		// Token: 0x0600054B RID: 1355 RVA: 0x0000CF18 File Offset: 0x0000B118
		public static int mkfifoat(int dirfd, string pathname, FilePermissions mode)
		{
			uint num = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_mkfifoat(dirfd, pathname, num);
		}

		// Token: 0x0600054C RID: 1356
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_statvfs", SetLastError = true)]
		public static extern int statvfs([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, out Statvfs buf);

		// Token: 0x0600054D RID: 1357
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fstatvfs", SetLastError = true)]
		public static extern int fstatvfs(int fd, out Statvfs buf);

		// Token: 0x0600054E RID: 1358
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gettimeofday", SetLastError = true)]
		public static extern int gettimeofday(out Timeval tv, out Timezone tz);

		// Token: 0x0600054F RID: 1359
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gettimeofday", SetLastError = true)]
		private static extern int gettimeofday(out Timeval tv, IntPtr ignore);

		// Token: 0x06000550 RID: 1360 RVA: 0x0000CF34 File Offset: 0x0000B134
		public static int gettimeofday(out Timeval tv)
		{
			return Syscall.gettimeofday(out tv, IntPtr.Zero);
		}

		// Token: 0x06000551 RID: 1361
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gettimeofday", SetLastError = true)]
		private static extern int gettimeofday(IntPtr ignore, out Timezone tz);

		// Token: 0x06000552 RID: 1362 RVA: 0x0000CF41 File Offset: 0x0000B141
		public static int gettimeofday(out Timezone tz)
		{
			return Syscall.gettimeofday(IntPtr.Zero, out tz);
		}

		// Token: 0x06000553 RID: 1363
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_settimeofday", SetLastError = true)]
		public static extern int settimeofday(ref Timeval tv, ref Timezone tz);

		// Token: 0x06000554 RID: 1364
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gettimeofday", SetLastError = true)]
		private static extern int settimeofday(ref Timeval tv, IntPtr ignore);

		// Token: 0x06000555 RID: 1365 RVA: 0x0000CF4E File Offset: 0x0000B14E
		public static int settimeofday(ref Timeval tv)
		{
			return Syscall.settimeofday(ref tv, IntPtr.Zero);
		}

		// Token: 0x06000556 RID: 1366
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_utimes", SetLastError = true)]
		private static extern int sys_utimes([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename, Timeval[] tvp);

		// Token: 0x06000557 RID: 1367 RVA: 0x0000CF5B File Offset: 0x0000B15B
		public static int utimes(string filename, Timeval[] tvp)
		{
			if (tvp != null && tvp.Length != 2)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			return Syscall.sys_utimes(filename, tvp);
		}

		// Token: 0x06000558 RID: 1368
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lutimes", SetLastError = true)]
		private static extern int sys_lutimes([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename, Timeval[] tvp);

		// Token: 0x06000559 RID: 1369 RVA: 0x0000CF76 File Offset: 0x0000B176
		public static int lutimes(string filename, Timeval[] tvp)
		{
			if (tvp != null && tvp.Length != 2)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			return Syscall.sys_lutimes(filename, tvp);
		}

		// Token: 0x0600055A RID: 1370
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_futimes", SetLastError = true)]
		private static extern int sys_futimes(int fd, Timeval[] tvp);

		// Token: 0x0600055B RID: 1371 RVA: 0x0000CF91 File Offset: 0x0000B191
		public static int futimes(int fd, Timeval[] tvp)
		{
			if (tvp != null && tvp.Length != 2)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			return Syscall.sys_futimes(fd, tvp);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		private static void CopyUtsname(ref Utsname to, ref Syscall._Utsname from)
		{
			try
			{
				to = new Utsname();
				to.sysname = UnixMarshal.PtrToString(from.sysname);
				to.nodename = UnixMarshal.PtrToString(from.nodename);
				to.release = UnixMarshal.PtrToString(from.release);
				to.version = UnixMarshal.PtrToString(from.version);
				to.machine = UnixMarshal.PtrToString(from.machine);
				to.domainname = UnixMarshal.PtrToString(from.domainname);
			}
			finally
			{
				Stdlib.free(from._buf_);
				from._buf_ = IntPtr.Zero;
			}
		}

		// Token: 0x0600055D RID: 1373
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_uname", SetLastError = true)]
		private static extern int sys_uname(out Syscall._Utsname buf);

		// Token: 0x0600055E RID: 1374 RVA: 0x0000D058 File Offset: 0x0000B258
		public static int uname(out Utsname buf)
		{
			Syscall._Utsname utsname;
			int num = Syscall.sys_uname(out utsname);
			buf = new Utsname();
			if (num == 0)
			{
				Syscall.CopyUtsname(ref buf, ref utsname);
			}
			return num;
		}

		// Token: 0x0600055F RID: 1375
		[DllImport("libc", SetLastError = true)]
		public static extern int wait(out int status);

		// Token: 0x06000560 RID: 1376
		[DllImport("libc", SetLastError = true)]
		private static extern int waitpid(int pid, out int status, int options);

		// Token: 0x06000561 RID: 1377 RVA: 0x0000D080 File Offset: 0x0000B280
		public static int waitpid(int pid, out int status, WaitOptions options)
		{
			int num = NativeConvert.FromWaitOptions(options);
			return Syscall.waitpid(pid, out status, num);
		}

		// Token: 0x06000562 RID: 1378
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WIFEXITED")]
		private static extern int _WIFEXITED(int status);

		// Token: 0x06000563 RID: 1379 RVA: 0x0000D09C File Offset: 0x0000B29C
		public static bool WIFEXITED(int status)
		{
			return Syscall._WIFEXITED(status) != 0;
		}

		// Token: 0x06000564 RID: 1380
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WEXITSTATUS")]
		public static extern int WEXITSTATUS(int status);

		// Token: 0x06000565 RID: 1381
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WIFSIGNALED")]
		private static extern int _WIFSIGNALED(int status);

		// Token: 0x06000566 RID: 1382 RVA: 0x0000D0A7 File Offset: 0x0000B2A7
		public static bool WIFSIGNALED(int status)
		{
			return Syscall._WIFSIGNALED(status) != 0;
		}

		// Token: 0x06000567 RID: 1383
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WTERMSIG")]
		private static extern int _WTERMSIG(int status);

		// Token: 0x06000568 RID: 1384 RVA: 0x0000D0B2 File Offset: 0x0000B2B2
		public static Signum WTERMSIG(int status)
		{
			return NativeConvert.ToSignum(Syscall._WTERMSIG(status));
		}

		// Token: 0x06000569 RID: 1385
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WIFSTOPPED")]
		private static extern int _WIFSTOPPED(int status);

		// Token: 0x0600056A RID: 1386 RVA: 0x0000D0BF File Offset: 0x0000B2BF
		public static bool WIFSTOPPED(int status)
		{
			return Syscall._WIFSTOPPED(status) != 0;
		}

		// Token: 0x0600056B RID: 1387
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WSTOPSIG")]
		private static extern int _WSTOPSIG(int status);

		// Token: 0x0600056C RID: 1388 RVA: 0x0000D0CA File Offset: 0x0000B2CA
		public static Signum WSTOPSIG(int status)
		{
			return NativeConvert.ToSignum(Syscall._WSTOPSIG(status));
		}

		// Token: 0x0600056D RID: 1389
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_openlog", SetLastError = true)]
		private static extern int sys_openlog(IntPtr ident, int option, int facility);

		// Token: 0x0600056E RID: 1390 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		public static int openlog(IntPtr ident, SyslogOptions option, SyslogFacility defaultFacility)
		{
			int num = NativeConvert.FromSyslogOptions(option);
			int num2 = NativeConvert.FromSyslogFacility(defaultFacility);
			return Syscall.sys_openlog(ident, num, num2);
		}

		// Token: 0x0600056F RID: 1391
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_syslog", SetLastError = true)]
		private static extern int sys_syslog(int priority, string message);

		// Token: 0x06000570 RID: 1392 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		public static int syslog(SyslogFacility facility, SyslogLevel level, string message)
		{
			int num = NativeConvert.FromSyslogFacility(facility);
			int num2 = NativeConvert.FromSyslogLevel(level);
			return Syscall.sys_syslog(num | num2, Syscall.GetSyslogMessage(message));
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000D123 File Offset: 0x0000B323
		public static int syslog(SyslogLevel level, string message)
		{
			return Syscall.sys_syslog(NativeConvert.FromSyslogLevel(level), Syscall.GetSyslogMessage(message));
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000D136 File Offset: 0x0000B336
		private static string GetSyslogMessage(string message)
		{
			return UnixMarshal.EscapeFormatString(message, new char[] { 'm' });
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000D14C File Offset: 0x0000B34C
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse syslog(SyslogFacility, SyslogLevel, string) instead.")]
		public static int syslog(SyslogFacility facility, SyslogLevel level, string format, params object[] parameters)
		{
			int num = NativeConvert.FromSyslogFacility(facility);
			int num2 = NativeConvert.FromSyslogLevel(level);
			object[] array = new object[checked(parameters.Length + 2)];
			array[0] = num | num2;
			array[1] = format;
			Array.Copy(parameters, 0, array, 2, parameters.Length);
			return (int)XPrintfFunctions.syslog(array);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse syslog(SyslogLevel, string) instead.")]
		public static int syslog(SyslogLevel level, string format, params object[] parameters)
		{
			int num = NativeConvert.FromSyslogLevel(level);
			object[] array = new object[checked(parameters.Length + 2)];
			array[0] = num;
			array[1] = format;
			Array.Copy(parameters, 0, array, 2, parameters.Length);
			return (int)XPrintfFunctions.syslog(array);
		}

		// Token: 0x06000575 RID: 1397
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_closelog", SetLastError = true)]
		public static extern int closelog();

		// Token: 0x06000576 RID: 1398
		[DllImport("libc", EntryPoint = "setlogmask", SetLastError = true)]
		private static extern int sys_setlogmask(int mask);

		// Token: 0x06000577 RID: 1399 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		public static int setlogmask(SyslogLevel mask)
		{
			return Syscall.sys_setlogmask(NativeConvert.FromSyslogLevel(mask));
		}

		// Token: 0x06000578 RID: 1400
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_nanosleep", SetLastError = true)]
		public static extern int nanosleep(ref Timespec req, ref Timespec rem);

		// Token: 0x06000579 RID: 1401
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_stime", SetLastError = true)]
		public static extern int stime(ref long t);

		// Token: 0x0600057A RID: 1402
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_time", SetLastError = true)]
		public static extern long time(out long t);

		// Token: 0x0600057B RID: 1403
		[DllImport("libc", EntryPoint = "access", SetLastError = true)]
		private static extern int sys_access([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, int mode);

		// Token: 0x0600057C RID: 1404 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public static int access(string pathname, AccessModes mode)
		{
			int num = NativeConvert.FromAccessModes(mode);
			return Syscall.sys_access(pathname, num);
		}

		// Token: 0x0600057D RID: 1405
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lseek", SetLastError = true)]
		private static extern long sys_lseek(int fd, long offset, int whence);

		// Token: 0x0600057E RID: 1406 RVA: 0x0000D214 File Offset: 0x0000B414
		public static long lseek(int fd, long offset, SeekFlags whence)
		{
			short num = NativeConvert.FromSeekFlags(whence);
			return Syscall.sys_lseek(fd, offset, (int)num);
		}

		// Token: 0x0600057F RID: 1407
		[DllImport("libc", SetLastError = true)]
		public static extern int close(int fd);

		// Token: 0x06000580 RID: 1408
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_read", SetLastError = true)]
		public static extern long read(int fd, IntPtr buf, ulong count);

		// Token: 0x06000581 RID: 1409 RVA: 0x0000D230 File Offset: 0x0000B430
		public unsafe static long read(int fd, void* buf, ulong count)
		{
			return Syscall.read(fd, (IntPtr)buf, count);
		}

		// Token: 0x06000582 RID: 1410
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_write", SetLastError = true)]
		public static extern long write(int fd, IntPtr buf, ulong count);

		// Token: 0x06000583 RID: 1411 RVA: 0x0000D23F File Offset: 0x0000B43F
		public unsafe static long write(int fd, void* buf, ulong count)
		{
			return Syscall.write(fd, (IntPtr)buf, count);
		}

		// Token: 0x06000584 RID: 1412
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_pread", SetLastError = true)]
		public static extern long pread(int fd, IntPtr buf, ulong count, long offset);

		// Token: 0x06000585 RID: 1413 RVA: 0x0000D24E File Offset: 0x0000B44E
		public unsafe static long pread(int fd, void* buf, ulong count, long offset)
		{
			return Syscall.pread(fd, (IntPtr)buf, count, offset);
		}

		// Token: 0x06000586 RID: 1414
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_pwrite", SetLastError = true)]
		public static extern long pwrite(int fd, IntPtr buf, ulong count, long offset);

		// Token: 0x06000587 RID: 1415 RVA: 0x0000D25E File Offset: 0x0000B45E
		public unsafe static long pwrite(int fd, void* buf, ulong count, long offset)
		{
			return Syscall.pwrite(fd, (IntPtr)buf, count, offset);
		}

		// Token: 0x06000588 RID: 1416
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_pipe", SetLastError = true)]
		public static extern int pipe(out int reading, out int writing);

		// Token: 0x06000589 RID: 1417 RVA: 0x0000D270 File Offset: 0x0000B470
		public static int pipe(int[] filedes)
		{
			if (filedes == null || filedes.Length != 2)
			{
				return -1;
			}
			int num2;
			int num3;
			int num = Syscall.pipe(out num2, out num3);
			filedes[0] = num2;
			filedes[1] = num3;
			return num;
		}

		// Token: 0x0600058A RID: 1418
		[DllImport("libc", SetLastError = true)]
		public static extern uint alarm(uint seconds);

		// Token: 0x0600058B RID: 1419
		[DllImport("libc", SetLastError = true)]
		public static extern uint sleep(uint seconds);

		// Token: 0x0600058C RID: 1420
		[DllImport("libc", SetLastError = true)]
		public static extern uint ualarm(uint usecs, uint interval);

		// Token: 0x0600058D RID: 1421
		[DllImport("libc", SetLastError = true)]
		public static extern int pause();

		// Token: 0x0600058E RID: 1422
		[DllImport("libc", SetLastError = true)]
		public static extern int chown([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, uint owner, uint group);

		// Token: 0x0600058F RID: 1423
		[DllImport("libc", SetLastError = true)]
		public static extern int fchown(int fd, uint owner, uint group);

		// Token: 0x06000590 RID: 1424
		[DllImport("libc", SetLastError = true)]
		public static extern int lchown([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, uint owner, uint group);

		// Token: 0x06000591 RID: 1425
		[DllImport("libc", SetLastError = true)]
		public static extern int chdir([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path);

		// Token: 0x06000592 RID: 1426
		[DllImport("libc", SetLastError = true)]
		public static extern int fchdir(int fd);

		// Token: 0x06000593 RID: 1427
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getcwd", SetLastError = true)]
		public static extern IntPtr getcwd([Out] StringBuilder buf, ulong size);

		// Token: 0x06000594 RID: 1428 RVA: 0x0000D299 File Offset: 0x0000B499
		public static StringBuilder getcwd(StringBuilder buf)
		{
			Syscall.getcwd(buf, (ulong)((long)buf.Capacity));
			return buf;
		}

		// Token: 0x06000595 RID: 1429
		[DllImport("libc", SetLastError = true)]
		public static extern int dup(int fd);

		// Token: 0x06000596 RID: 1430
		[DllImport("libc", SetLastError = true)]
		public static extern int dup2(int fd, int fd2);

		// Token: 0x06000597 RID: 1431
		[DllImport("libc", SetLastError = true)]
		public static extern int execve([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string[] argv, string[] envp);

		// Token: 0x06000598 RID: 1432
		[DllImport("libc", SetLastError = true)]
		public static extern int fexecve(int fd, string[] argv, string[] envp);

		// Token: 0x06000599 RID: 1433
		[DllImport("libc", SetLastError = true)]
		public static extern int execv([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string[] argv);

		// Token: 0x0600059A RID: 1434
		[DllImport("libc", SetLastError = true)]
		public static extern int execvp([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string[] argv);

		// Token: 0x0600059B RID: 1435
		[DllImport("libc", SetLastError = true)]
		public static extern int nice(int inc);

		// Token: 0x0600059C RID: 1436
		[CLSCompliant(false)]
		[DllImport("libc", SetLastError = true)]
		public static extern int _exit(int status);

		// Token: 0x0600059D RID: 1437
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fpathconf", SetLastError = true)]
		public static extern long fpathconf(int filedes, PathconfName name, Errno defaultError);

		// Token: 0x0600059E RID: 1438 RVA: 0x0000D2AA File Offset: 0x0000B4AA
		public static long fpathconf(int filedes, PathconfName name)
		{
			return Syscall.fpathconf(filedes, name, (Errno)0);
		}

		// Token: 0x0600059F RID: 1439
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_pathconf", SetLastError = true)]
		public static extern long pathconf([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, PathconfName name, Errno defaultError);

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		public static long pathconf(string path, PathconfName name)
		{
			return Syscall.pathconf(path, name, (Errno)0);
		}

		// Token: 0x060005A1 RID: 1441
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sysconf", SetLastError = true)]
		public static extern long sysconf(SysconfName name, Errno defaultError);

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000D2BE File Offset: 0x0000B4BE
		public static long sysconf(SysconfName name)
		{
			return Syscall.sysconf(name, (Errno)0);
		}

		// Token: 0x060005A3 RID: 1443
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_confstr", SetLastError = true)]
		public static extern ulong confstr(ConfstrName name, [Out] StringBuilder buf, ulong len);

		// Token: 0x060005A4 RID: 1444
		[DllImport("libc", SetLastError = true)]
		public static extern int getpid();

		// Token: 0x060005A5 RID: 1445
		[DllImport("libc", SetLastError = true)]
		public static extern int getppid();

		// Token: 0x060005A6 RID: 1446
		[DllImport("libc", SetLastError = true)]
		public static extern int setpgid(int pid, int pgid);

		// Token: 0x060005A7 RID: 1447
		[DllImport("libc", SetLastError = true)]
		public static extern int getpgid(int pid);

		// Token: 0x060005A8 RID: 1448
		[DllImport("libc", SetLastError = true)]
		public static extern int setpgrp();

		// Token: 0x060005A9 RID: 1449
		[DllImport("libc", SetLastError = true)]
		public static extern int getpgrp();

		// Token: 0x060005AA RID: 1450
		[DllImport("libc", SetLastError = true)]
		public static extern int setsid();

		// Token: 0x060005AB RID: 1451
		[DllImport("libc", SetLastError = true)]
		public static extern int getsid(int pid);

		// Token: 0x060005AC RID: 1452
		[DllImport("libc", SetLastError = true)]
		public static extern uint getuid();

		// Token: 0x060005AD RID: 1453
		[DllImport("libc", SetLastError = true)]
		public static extern uint geteuid();

		// Token: 0x060005AE RID: 1454
		[DllImport("libc", SetLastError = true)]
		public static extern uint getgid();

		// Token: 0x060005AF RID: 1455
		[DllImport("libc", SetLastError = true)]
		public static extern uint getegid();

		// Token: 0x060005B0 RID: 1456
		[DllImport("libc", SetLastError = true)]
		public static extern int getgroups(int size, uint[] list);

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000D2C7 File Offset: 0x0000B4C7
		public static int getgroups(uint[] list)
		{
			return Syscall.getgroups(list.Length, list);
		}

		// Token: 0x060005B2 RID: 1458
		[DllImport("libc", SetLastError = true)]
		public static extern int setuid(uint uid);

		// Token: 0x060005B3 RID: 1459
		[DllImport("libc", SetLastError = true)]
		public static extern int setreuid(uint ruid, uint euid);

		// Token: 0x060005B4 RID: 1460
		[DllImport("libc", SetLastError = true)]
		public static extern int setregid(uint rgid, uint egid);

		// Token: 0x060005B5 RID: 1461
		[DllImport("libc", SetLastError = true)]
		public static extern int seteuid(uint euid);

		// Token: 0x060005B6 RID: 1462
		[DllImport("libc", SetLastError = true)]
		public static extern int setegid(uint uid);

		// Token: 0x060005B7 RID: 1463
		[DllImport("libc", SetLastError = true)]
		public static extern int setgid(uint gid);

		// Token: 0x060005B8 RID: 1464
		[DllImport("libc", SetLastError = true)]
		public static extern int getresuid(out uint ruid, out uint euid, out uint suid);

		// Token: 0x060005B9 RID: 1465
		[DllImport("libc", SetLastError = true)]
		public static extern int getresgid(out uint rgid, out uint egid, out uint sgid);

		// Token: 0x060005BA RID: 1466
		[DllImport("libc", SetLastError = true)]
		public static extern int setresuid(uint ruid, uint euid, uint suid);

		// Token: 0x060005BB RID: 1467
		[DllImport("libc", SetLastError = true)]
		public static extern int setresgid(uint rgid, uint egid, uint sgid);

		// Token: 0x060005BC RID: 1468
		[DllImport("libc", EntryPoint = "ttyname", SetLastError = true)]
		private static extern IntPtr sys_ttyname(int fd);

		// Token: 0x060005BD RID: 1469 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		public static string ttyname(int fd)
		{
			object obj = Syscall.tty_lock;
			string text;
			lock (obj)
			{
				text = UnixMarshal.PtrToString(Syscall.sys_ttyname(fd));
			}
			return text;
		}

		// Token: 0x060005BE RID: 1470
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_ttyname_r", SetLastError = true)]
		public static extern int ttyname_r(int fd, [Out] StringBuilder buf, ulong buflen);

		// Token: 0x060005BF RID: 1471 RVA: 0x0000D31C File Offset: 0x0000B51C
		public static int ttyname_r(int fd, StringBuilder buf)
		{
			return Syscall.ttyname_r(fd, buf, (ulong)((long)buf.Capacity));
		}

		// Token: 0x060005C0 RID: 1472
		[DllImport("libc", EntryPoint = "isatty")]
		private static extern int sys_isatty(int fd);

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000D32C File Offset: 0x0000B52C
		public static bool isatty(int fd)
		{
			return Syscall.sys_isatty(fd) == 1;
		}

		// Token: 0x060005C2 RID: 1474
		[DllImport("libc", SetLastError = true)]
		public static extern int link([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string newpath);

		// Token: 0x060005C3 RID: 1475
		[DllImport("libc", SetLastError = true)]
		public static extern int symlink([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string newpath);

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000D338 File Offset: 0x0000B538
		private static int ReadlinkIntoStringBuilder(Syscall.DoReadlinkFun doReadlink, [Out] StringBuilder buf, ulong bufsiz)
		{
			int num;
			long num2;
			checked
			{
				num = (int)bufsiz;
				byte[] array = new byte[num];
				num2 = doReadlink(array);
				if (num2 < 0L)
				{
					return (int)num2;
				}
				buf.Length = 0;
				char[] chars = UnixEncoding.Instance.GetChars(array, 0, (int)num2);
				buf.Append(chars, 0, Math.Min(num, chars.Length));
			}
			if (num2 == (long)num)
			{
				buf.Append(new string('\0', num - buf.Length));
			}
			return buf.Length;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000D3A7 File Offset: 0x0000B5A7
		public static int readlink(string path, [Out] StringBuilder buf, ulong bufsiz)
		{
			return Syscall.ReadlinkIntoStringBuilder((byte[] target) => Syscall.readlink(path, target), buf, bufsiz);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000D3C7 File Offset: 0x0000B5C7
		public static int readlink(string path, [Out] StringBuilder buf)
		{
			return Syscall.readlink(path, buf, (ulong)((long)buf.Capacity));
		}

		// Token: 0x060005C7 RID: 1479
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_readlink", SetLastError = true)]
		private static extern long readlink([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, byte[] buf, ulong bufsiz);

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000D3D7 File Offset: 0x0000B5D7
		public static long readlink(string path, byte[] buf)
		{
			return Syscall.readlink(path, buf, (ulong)((long)buf.Length));
		}

		// Token: 0x060005C9 RID: 1481
		[DllImport("libc", SetLastError = true)]
		public static extern int unlink([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname);

		// Token: 0x060005CA RID: 1482
		[DllImport("libc", SetLastError = true)]
		public static extern int rmdir([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname);

		// Token: 0x060005CB RID: 1483
		[DllImport("libc", SetLastError = true)]
		public static extern int tcgetpgrp(int fd);

		// Token: 0x060005CC RID: 1484
		[DllImport("libc", SetLastError = true)]
		public static extern int tcsetpgrp(int fd, int pgrp);

		// Token: 0x060005CD RID: 1485
		[DllImport("libc", EntryPoint = "getlogin", SetLastError = true)]
		private static extern IntPtr sys_getlogin();

		// Token: 0x060005CE RID: 1486 RVA: 0x0000D3E4 File Offset: 0x0000B5E4
		public static string getlogin()
		{
			object obj = Syscall.getlogin_lock;
			string text;
			lock (obj)
			{
				text = UnixMarshal.PtrToString(Syscall.sys_getlogin());
			}
			return text;
		}

		// Token: 0x060005CF RID: 1487
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getlogin_r", SetLastError = true)]
		public static extern int getlogin_r([Out] StringBuilder name, ulong bufsize);

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000D42C File Offset: 0x0000B62C
		public static int getlogin_r(StringBuilder name)
		{
			return Syscall.getlogin_r(name, (ulong)((long)name.Capacity));
		}

		// Token: 0x060005D1 RID: 1489
		[DllImport("libc", SetLastError = true)]
		public static extern int setlogin(string name);

		// Token: 0x060005D2 RID: 1490
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gethostname", SetLastError = true)]
		public static extern int gethostname([Out] StringBuilder name, ulong len);

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000D43B File Offset: 0x0000B63B
		public static int gethostname(StringBuilder name)
		{
			return Syscall.gethostname(name, (ulong)((long)name.Capacity));
		}

		// Token: 0x060005D4 RID: 1492
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sethostname", SetLastError = true)]
		public static extern int sethostname(string name, ulong len);

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000D44A File Offset: 0x0000B64A
		public static int sethostname(string name)
		{
			return Syscall.sethostname(name, (ulong)((long)name.Length));
		}

		// Token: 0x060005D6 RID: 1494
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gethostid", SetLastError = true)]
		public static extern long gethostid();

		// Token: 0x060005D7 RID: 1495
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sethostid", SetLastError = true)]
		public static extern int sethostid(long hostid);

		// Token: 0x060005D8 RID: 1496
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getdomainname", SetLastError = true)]
		public static extern int getdomainname([Out] StringBuilder name, ulong len);

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000D459 File Offset: 0x0000B659
		public static int getdomainname(StringBuilder name)
		{
			return Syscall.getdomainname(name, (ulong)((long)name.Capacity));
		}

		// Token: 0x060005DA RID: 1498
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setdomainname", SetLastError = true)]
		public static extern int setdomainname(string name, ulong len);

		// Token: 0x060005DB RID: 1499 RVA: 0x0000D468 File Offset: 0x0000B668
		public static int setdomainname(string name)
		{
			return Syscall.setdomainname(name, (ulong)((long)name.Length));
		}

		// Token: 0x060005DC RID: 1500
		[DllImport("libc", SetLastError = true)]
		public static extern int vhangup();

		// Token: 0x060005DD RID: 1501
		[DllImport("libc", SetLastError = true)]
		public static extern int revoke([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string file);

		// Token: 0x060005DE RID: 1502
		[DllImport("libc", SetLastError = true)]
		public static extern int acct([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename);

		// Token: 0x060005DF RID: 1503
		[DllImport("libc", EntryPoint = "getusershell", SetLastError = true)]
		private static extern IntPtr sys_getusershell();

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000D478 File Offset: 0x0000B678
		public static string getusershell()
		{
			object obj = Syscall.usershell_lock;
			string text;
			lock (obj)
			{
				text = UnixMarshal.PtrToString(Syscall.sys_getusershell());
			}
			return text;
		}

		// Token: 0x060005E1 RID: 1505
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setusershell", SetLastError = true)]
		private static extern int sys_setusershell();

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		public static int setusershell()
		{
			object obj = Syscall.usershell_lock;
			int num;
			lock (obj)
			{
				num = Syscall.sys_setusershell();
			}
			return num;
		}

		// Token: 0x060005E3 RID: 1507
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_endusershell", SetLastError = true)]
		private static extern int sys_endusershell();

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000D500 File Offset: 0x0000B700
		public static int endusershell()
		{
			object obj = Syscall.usershell_lock;
			int num;
			lock (obj)
			{
				num = Syscall.sys_endusershell();
			}
			return num;
		}

		// Token: 0x060005E5 RID: 1509
		[DllImport("libc", SetLastError = true)]
		public static extern int chroot([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path);

		// Token: 0x060005E6 RID: 1510
		[DllImport("libc", SetLastError = true)]
		public static extern int fsync(int fd);

		// Token: 0x060005E7 RID: 1511
		[DllImport("libc", SetLastError = true)]
		public static extern int fdatasync(int fd);

		// Token: 0x060005E8 RID: 1512
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sync", SetLastError = true)]
		public static extern int sync();

		// Token: 0x060005E9 RID: 1513
		[Obsolete("Dropped in POSIX 1003.1-2001.  Use Syscall.sysconf (SysconfName._SC_PAGESIZE).")]
		[DllImport("libc", SetLastError = true)]
		public static extern int getpagesize();

		// Token: 0x060005EA RID: 1514
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_truncate", SetLastError = true)]
		public static extern int truncate([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, long length);

		// Token: 0x060005EB RID: 1515
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_ftruncate", SetLastError = true)]
		public static extern int ftruncate(int fd, long length);

		// Token: 0x060005EC RID: 1516
		[DllImport("libc", SetLastError = true)]
		public static extern int getdtablesize();

		// Token: 0x060005ED RID: 1517
		[DllImport("libc", SetLastError = true)]
		public static extern int brk(IntPtr end_data_segment);

		// Token: 0x060005EE RID: 1518
		[DllImport("libc", SetLastError = true)]
		public static extern IntPtr sbrk(IntPtr increment);

		// Token: 0x060005EF RID: 1519
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lockf", SetLastError = true)]
		public static extern int lockf(int fd, LockfCommand cmd, long len);

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000D540 File Offset: 0x0000B740
		[Obsolete("This is insecure and should not be used", true)]
		public static string crypt(string key, string salt)
		{
			throw new SecurityException("crypt(3) has been broken.  Use something more secure.");
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000D54C File Offset: 0x0000B74C
		[Obsolete("This is insecure and should not be used", true)]
		public static int encrypt(byte[] block, bool decode)
		{
			throw new SecurityException("crypt(3) has been broken.  Use something more secure.");
		}

		// Token: 0x060005F2 RID: 1522
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_swab", SetLastError = true)]
		public static extern int swab(IntPtr from, IntPtr to, long n);

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000D558 File Offset: 0x0000B758
		public unsafe static void swab(void* from, void* to, long n)
		{
			Syscall.swab((IntPtr)from, (IntPtr)to, n);
		}

		// Token: 0x060005F4 RID: 1524
		[DllImport("libc", EntryPoint = "faccessat", SetLastError = true)]
		private static extern int sys_faccessat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, int mode, int flags);

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000D570 File Offset: 0x0000B770
		public static int faccessat(int dirfd, string pathname, AccessModes mode, AtFlags flags)
		{
			int num = NativeConvert.FromAccessModes(mode);
			int num2 = NativeConvert.FromAtFlags(flags);
			return Syscall.sys_faccessat(dirfd, pathname, num, num2);
		}

		// Token: 0x060005F6 RID: 1526
		[DllImport("libc", EntryPoint = "fchownat", SetLastError = true)]
		private static extern int sys_fchownat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, uint owner, uint group, int flags);

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000D594 File Offset: 0x0000B794
		public static int fchownat(int dirfd, string pathname, uint owner, uint group, AtFlags flags)
		{
			int num = NativeConvert.FromAtFlags(flags);
			return Syscall.sys_fchownat(dirfd, pathname, owner, group, num);
		}

		// Token: 0x060005F8 RID: 1528
		[DllImport("libc", EntryPoint = "linkat", SetLastError = true)]
		private static extern int sys_linkat(int olddirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, int newdirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string newpath, int flags);

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		public static int linkat(int olddirfd, string oldpath, int newdirfd, string newpath, AtFlags flags)
		{
			int num = NativeConvert.FromAtFlags(flags);
			return Syscall.sys_linkat(olddirfd, oldpath, newdirfd, newpath, num);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000D5D3 File Offset: 0x0000B7D3
		public static int readlinkat(int dirfd, string pathname, [Out] StringBuilder buf, ulong bufsiz)
		{
			return Syscall.ReadlinkIntoStringBuilder((byte[] target) => Syscall.readlinkat(dirfd, pathname, target), buf, bufsiz);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000D5FA File Offset: 0x0000B7FA
		public static int readlinkat(int dirfd, string pathname, [Out] StringBuilder buf)
		{
			return Syscall.readlinkat(dirfd, pathname, buf, (ulong)((long)buf.Capacity));
		}

		// Token: 0x060005FC RID: 1532
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_readlinkat", SetLastError = true)]
		private static extern long readlinkat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, byte[] buf, ulong bufsiz);

		// Token: 0x060005FD RID: 1533 RVA: 0x0000D60B File Offset: 0x0000B80B
		public static long readlinkat(int dirfd, string pathname, byte[] buf)
		{
			return Syscall.readlinkat(dirfd, pathname, buf, (ulong)((long)buf.Length));
		}

		// Token: 0x060005FE RID: 1534
		[DllImport("libc", SetLastError = true)]
		public static extern int symlinkat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string newpath);

		// Token: 0x060005FF RID: 1535
		[DllImport("libc", EntryPoint = "unlinkat", SetLastError = true)]
		private static extern int sys_unlinkat(int dirfd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, int flags);

		// Token: 0x06000600 RID: 1536 RVA: 0x0000D618 File Offset: 0x0000B818
		public static int unlinkat(int dirfd, string pathname, AtFlags flags)
		{
			int num = NativeConvert.FromAtFlags(flags);
			return Syscall.sys_unlinkat(dirfd, pathname, num);
		}

		// Token: 0x06000601 RID: 1537
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_utime", SetLastError = true)]
		private static extern int sys_utime([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename, ref Utimbuf buf, int use_buf);

		// Token: 0x06000602 RID: 1538 RVA: 0x0000D634 File Offset: 0x0000B834
		public static int utime(string filename, ref Utimbuf buf)
		{
			return Syscall.sys_utime(filename, ref buf, 1);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000D640 File Offset: 0x0000B840
		public static int utime(string filename)
		{
			Utimbuf utimbuf = default(Utimbuf);
			return Syscall.sys_utime(filename, ref utimbuf, 0);
		}

		// Token: 0x06000604 RID: 1540
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_readv", SetLastError = true)]
		private static extern long sys_readv(int fd, Iovec[] iov, int iovcnt);

		// Token: 0x06000605 RID: 1541 RVA: 0x0000D65E File Offset: 0x0000B85E
		public static long readv(int fd, Iovec[] iov)
		{
			return Syscall.sys_readv(fd, iov, iov.Length);
		}

		// Token: 0x06000606 RID: 1542
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_writev", SetLastError = true)]
		private static extern long sys_writev(int fd, Iovec[] iov, int iovcnt);

		// Token: 0x06000607 RID: 1543 RVA: 0x0000D66A File Offset: 0x0000B86A
		public static long writev(int fd, Iovec[] iov)
		{
			return Syscall.sys_writev(fd, iov, iov.Length);
		}

		// Token: 0x06000608 RID: 1544
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_preadv", SetLastError = true)]
		private static extern long sys_preadv(int fd, Iovec[] iov, int iovcnt, long offset);

		// Token: 0x06000609 RID: 1545 RVA: 0x0000D676 File Offset: 0x0000B876
		public static long preadv(int fd, Iovec[] iov, long offset)
		{
			return Syscall.sys_preadv(fd, iov, iov.Length, offset);
		}

		// Token: 0x0600060A RID: 1546
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_pwritev", SetLastError = true)]
		private static extern long sys_pwritev(int fd, Iovec[] iov, int iovcnt, long offset);

		// Token: 0x0600060B RID: 1547 RVA: 0x0000D683 File Offset: 0x0000B883
		public static long pwritev(int fd, Iovec[] iov, long offset)
		{
			return Syscall.sys_pwritev(fd, iov, iov.Length, offset);
		}

		// Token: 0x0600060C RID: 1548
		[DllImport("libc")]
		public static extern uint htonl(uint hostlong);

		// Token: 0x0600060D RID: 1549
		[DllImport("libc")]
		public static extern ushort htons(ushort hostshort);

		// Token: 0x0600060E RID: 1550
		[DllImport("libc")]
		public static extern uint ntohl(uint netlong);

		// Token: 0x0600060F RID: 1551
		[DllImport("libc")]
		public static extern ushort ntohs(ushort netshort);

		// Token: 0x06000610 RID: 1552
		[DllImport("libc", EntryPoint = "socket", SetLastError = true)]
		private static extern int sys_socket(int domain, int type, int protocol);

		// Token: 0x06000611 RID: 1553 RVA: 0x0000D690 File Offset: 0x0000B890
		public static int socket(UnixAddressFamily domain, UnixSocketType type, UnixSocketFlags flags, UnixSocketProtocol protocol)
		{
			int num = NativeConvert.FromUnixAddressFamily(domain);
			int num2 = NativeConvert.FromUnixSocketType(type);
			int num3 = NativeConvert.FromUnixSocketFlags(flags);
			int num4 = ((protocol == (UnixSocketProtocol)0) ? 0 : NativeConvert.FromUnixSocketProtocol(protocol));
			return Syscall.sys_socket(num, num2 | num3, num4);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0000D6C7 File Offset: 0x0000B8C7
		public static int socket(UnixAddressFamily domain, UnixSocketType type, UnixSocketProtocol protocol)
		{
			return Syscall.socket(domain, type, (UnixSocketFlags)0, protocol);
		}

		// Token: 0x06000613 RID: 1555
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_socketpair", SetLastError = true)]
		private static extern int sys_socketpair(int domain, int type, int protocol, out int socket1, out int socket2);

		// Token: 0x06000614 RID: 1556 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		public static int socketpair(UnixAddressFamily domain, UnixSocketType type, UnixSocketFlags flags, UnixSocketProtocol protocol, out int socket1, out int socket2)
		{
			int num = NativeConvert.FromUnixAddressFamily(domain);
			int num2 = NativeConvert.FromUnixSocketType(type);
			int num3 = NativeConvert.FromUnixSocketFlags(flags);
			int num4 = ((protocol == (UnixSocketProtocol)0) ? 0 : NativeConvert.FromUnixSocketProtocol(protocol));
			return Syscall.sys_socketpair(num, num2 | num3, num4, out socket1, out socket2);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0000D70F File Offset: 0x0000B90F
		public static int socketpair(UnixAddressFamily domain, UnixSocketType type, UnixSocketProtocol protocol, out int socket1, out int socket2)
		{
			return Syscall.socketpair(domain, type, (UnixSocketFlags)0, protocol, out socket1, out socket2);
		}

		// Token: 0x06000616 RID: 1558
		[DllImport("libc", SetLastError = true)]
		public static extern int sockatmark(int socket);

		// Token: 0x06000617 RID: 1559
		[DllImport("libc", SetLastError = true)]
		public static extern int listen(int socket, int backlog);

		// Token: 0x06000618 RID: 1560
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getsockopt", SetLastError = true)]
		private unsafe static extern int sys_getsockopt(int socket, int level, int option_name, void* option_value, ref long option_len);

		// Token: 0x06000619 RID: 1561
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getsockopt_timeval", SetLastError = true)]
		private static extern int sys_getsockopt_timeval(int socket, int level, int option_name, out Timeval option_value);

		// Token: 0x0600061A RID: 1562
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getsockopt_linger", SetLastError = true)]
		private static extern int sys_getsockopt_linger(int socket, int level, int option_name, out Linger option_value);

		// Token: 0x0600061B RID: 1563 RVA: 0x0000D720 File Offset: 0x0000B920
		public unsafe static int getsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, void* option_value, ref long option_len)
		{
			int num = NativeConvert.FromUnixSocketProtocol(level);
			int num2 = NativeConvert.FromUnixSocketOptionName(option_name);
			return Syscall.sys_getsockopt(socket, num, num2, option_value, ref option_len);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000D746 File Offset: 0x0000B946
		public unsafe static int getsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, IntPtr option_value, ref long option_len)
		{
			return Syscall.getsockopt(socket, level, option_name, (void*)option_value, ref option_len);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000D758 File Offset: 0x0000B958
		public unsafe static int getsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, out int option_value)
		{
			long num = 4L;
			int num3;
			int num2 = Syscall.getsockopt(socket, level, option_name, (void*)(&num3), ref num);
			if (num2 != -1 && num != 4L)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				num2 = -1;
			}
			option_value = num3;
			return num2;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000D78C File Offset: 0x0000B98C
		public unsafe static int getsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, byte[] option_value, ref long option_len)
		{
			if (option_len > (long)((option_value == null) ? 0 : option_value.Length))
			{
				throw new ArgumentOutOfRangeException("option_len", "option_len > (option_value == null ? 0 : option_value.Length)");
			}
			byte* ptr;
			if (option_value == null || option_value.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &option_value[0];
			}
			return Syscall.getsockopt(socket, level, option_name, (void*)ptr, ref option_len);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public static int getsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, out Timeval option_value)
		{
			int num = NativeConvert.FromUnixSocketProtocol(level);
			int num2 = NativeConvert.FromUnixSocketOptionName(option_name);
			return Syscall.sys_getsockopt_timeval(socket, num, num2, out option_value);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000D800 File Offset: 0x0000BA00
		public static int getsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, out Linger option_value)
		{
			int num = NativeConvert.FromUnixSocketProtocol(level);
			int num2 = NativeConvert.FromUnixSocketOptionName(option_name);
			return Syscall.sys_getsockopt_linger(socket, num, num2, out option_value);
		}

		// Token: 0x06000621 RID: 1569
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setsockopt", SetLastError = true)]
		private unsafe static extern int sys_setsockopt(int socket, int level, int option_name, void* option_value, long option_len);

		// Token: 0x06000622 RID: 1570
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setsockopt_timeval", SetLastError = true)]
		private static extern int sys_setsockopt_timeval(int socket, int level, int option_name, ref Timeval option_value);

		// Token: 0x06000623 RID: 1571
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setsockopt_linger", SetLastError = true)]
		private static extern int sys_setsockopt_linger(int socket, int level, int option_name, ref Linger option_value);

		// Token: 0x06000624 RID: 1572 RVA: 0x0000D824 File Offset: 0x0000BA24
		public unsafe static int setsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, void* option_value, long option_len)
		{
			int num = NativeConvert.FromUnixSocketProtocol(level);
			int num2 = NativeConvert.FromUnixSocketOptionName(option_name);
			return Syscall.sys_setsockopt(socket, num, num2, option_value, option_len);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000D84A File Offset: 0x0000BA4A
		public unsafe static int setsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, IntPtr option_value, long option_len)
		{
			return Syscall.setsockopt(socket, level, option_name, (void*)option_value, option_len);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000D85C File Offset: 0x0000BA5C
		public unsafe static int setsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, int option_value)
		{
			return Syscall.setsockopt(socket, level, option_name, (void*)(&option_value), 4L);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000D86C File Offset: 0x0000BA6C
		public unsafe static int setsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, byte[] option_value, long option_len)
		{
			if (option_len > (long)((option_value == null) ? 0 : option_value.Length))
			{
				throw new ArgumentOutOfRangeException("option_len", "option_len > (option_value == null ? 0 : option_value.Length)");
			}
			byte* ptr;
			if (option_value == null || option_value.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &option_value[0];
			}
			return Syscall.setsockopt(socket, level, option_name, (void*)ptr, option_len);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0000D8BC File Offset: 0x0000BABC
		public static int setsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, Timeval option_value)
		{
			int num = NativeConvert.FromUnixSocketProtocol(level);
			int num2 = NativeConvert.FromUnixSocketOptionName(option_name);
			return Syscall.sys_setsockopt_timeval(socket, num, num2, ref option_value);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public static int setsockopt(int socket, UnixSocketProtocol level, UnixSocketOptionName option_name, Linger option_value)
		{
			int num = NativeConvert.FromUnixSocketProtocol(level);
			int num2 = NativeConvert.FromUnixSocketOptionName(option_name);
			return Syscall.sys_setsockopt_linger(socket, num, num2, ref option_value);
		}

		// Token: 0x0600062A RID: 1578
		[DllImport("libc", EntryPoint = "shutdown", SetLastError = true)]
		private static extern int sys_shutdown(int socket, int how);

		// Token: 0x0600062B RID: 1579 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public static int shutdown(int socket, ShutdownOption how)
		{
			int num = NativeConvert.FromShutdownOption(how);
			return Syscall.sys_shutdown(socket, num);
		}

		// Token: 0x0600062C RID: 1580
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_recv", SetLastError = true)]
		private unsafe static extern long sys_recv(int socket, void* buffer, ulong length, int flags);

		// Token: 0x0600062D RID: 1581 RVA: 0x0000D928 File Offset: 0x0000BB28
		public unsafe static long recv(int socket, void* buffer, ulong length, MessageFlags flags)
		{
			int num = NativeConvert.FromMessageFlags(flags);
			return Syscall.sys_recv(socket, buffer, length, num);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000D945 File Offset: 0x0000BB45
		public unsafe static long recv(int socket, IntPtr buffer, ulong length, MessageFlags flags)
		{
			return Syscall.recv(socket, (void*)buffer, length, flags);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000D958 File Offset: 0x0000BB58
		public unsafe static long recv(int socket, byte[] buffer, ulong length, MessageFlags flags)
		{
			if (length > (ulong)((buffer == null) ? 0L : ((long)buffer.Length)))
			{
				throw new ArgumentOutOfRangeException("length", "length > (buffer == null ? 0 : buffer.LongLength)");
			}
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			return Syscall.recv(socket, (void*)ptr, length, flags);
		}

		// Token: 0x06000630 RID: 1584
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_send", SetLastError = true)]
		private unsafe static extern long sys_send(int socket, void* message, ulong length, int flags);

		// Token: 0x06000631 RID: 1585 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
		public unsafe static long send(int socket, void* message, ulong length, MessageFlags flags)
		{
			int num = NativeConvert.FromMessageFlags(flags);
			return Syscall.sys_send(socket, message, length, num);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0000D9C1 File Offset: 0x0000BBC1
		public unsafe static long send(int socket, IntPtr message, ulong length, MessageFlags flags)
		{
			return Syscall.send(socket, (void*)message, length, flags);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public unsafe static long send(int socket, byte[] message, ulong length, MessageFlags flags)
		{
			if (length > (ulong)((message == null) ? 0L : ((long)message.Length)))
			{
				throw new ArgumentOutOfRangeException("length", "length > (message == null ? 0 : message.LongLength)");
			}
			byte* ptr;
			if (message == null || message.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &message[0];
			}
			return Syscall.send(socket, (void*)ptr, length, flags);
		}

		// Token: 0x06000634 RID: 1588
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_bind", SetLastError = true)]
		private unsafe static extern int sys_bind(int socket, _SockaddrHeader* address);

		// Token: 0x06000635 RID: 1589 RVA: 0x0000DA20 File Offset: 0x0000BC20
		public unsafe static int bind(int socket, Sockaddr address)
		{
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(address).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] dynamicData;
				byte* ptr3;
				if ((dynamicData = Sockaddr.GetDynamicData(address)) == null || dynamicData.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &dynamicData[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(address, ptr3, false);
				return Syscall.sys_bind(socket, Sockaddr.GetNative(&sockaddrDynamic, ptr2));
			}
		}

		// Token: 0x06000636 RID: 1590
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_connect", SetLastError = true)]
		private unsafe static extern int sys_connect(int socket, _SockaddrHeader* address);

		// Token: 0x06000637 RID: 1591 RVA: 0x0000DA74 File Offset: 0x0000BC74
		public unsafe static int connect(int socket, Sockaddr address)
		{
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(address).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] dynamicData;
				byte* ptr3;
				if ((dynamicData = Sockaddr.GetDynamicData(address)) == null || dynamicData.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &dynamicData[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(address, ptr3, false);
				return Syscall.sys_connect(socket, Sockaddr.GetNative(&sockaddrDynamic, ptr2));
			}
		}

		// Token: 0x06000638 RID: 1592
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_accept", SetLastError = true)]
		private unsafe static extern int sys_accept(int socket, _SockaddrHeader* address);

		// Token: 0x06000639 RID: 1593 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		public unsafe static int accept(int socket, Sockaddr address)
		{
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(address).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] dynamicData;
				byte* ptr3;
				if ((dynamicData = Sockaddr.GetDynamicData(address)) == null || dynamicData.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &dynamicData[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(address, ptr3, true);
				int num = Syscall.sys_accept(socket, Sockaddr.GetNative(&sockaddrDynamic, ptr2));
				sockaddrDynamic.Update(address);
				return num;
			}
		}

		// Token: 0x0600063A RID: 1594
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_accept4", SetLastError = true)]
		private unsafe static extern int sys_accept4(int socket, _SockaddrHeader* address, int flags);

		// Token: 0x0600063B RID: 1595 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public unsafe static int accept4(int socket, Sockaddr address, UnixSocketFlags flags)
		{
			int num = NativeConvert.FromUnixSocketFlags(flags);
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(address).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] dynamicData;
				byte* ptr3;
				if ((dynamicData = Sockaddr.GetDynamicData(address)) == null || dynamicData.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &dynamicData[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(address, ptr3, true);
				int num2 = Syscall.sys_accept4(socket, Sockaddr.GetNative(&sockaddrDynamic, ptr2), num);
				sockaddrDynamic.Update(address);
				return num2;
			}
		}

		// Token: 0x0600063C RID: 1596
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpeername", SetLastError = true)]
		private unsafe static extern int sys_getpeername(int socket, _SockaddrHeader* address);

		// Token: 0x0600063D RID: 1597 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public unsafe static int getpeername(int socket, Sockaddr address)
		{
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(address).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] dynamicData;
				byte* ptr3;
				if ((dynamicData = Sockaddr.GetDynamicData(address)) == null || dynamicData.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &dynamicData[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(address, ptr3, true);
				int num = Syscall.sys_getpeername(socket, Sockaddr.GetNative(&sockaddrDynamic, ptr2));
				sockaddrDynamic.Update(address);
				return num;
			}
		}

		// Token: 0x0600063E RID: 1598
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getsockname", SetLastError = true)]
		private unsafe static extern int sys_getsockname(int socket, _SockaddrHeader* address);

		// Token: 0x0600063F RID: 1599 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		public unsafe static int getsockname(int socket, Sockaddr address)
		{
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(address).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] dynamicData;
				byte* ptr3;
				if ((dynamicData = Sockaddr.GetDynamicData(address)) == null || dynamicData.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &dynamicData[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(address, ptr3, true);
				int num = Syscall.sys_getsockname(socket, Sockaddr.GetNative(&sockaddrDynamic, ptr2));
				sockaddrDynamic.Update(address);
				return num;
			}
		}

		// Token: 0x06000640 RID: 1600
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_recvfrom", SetLastError = true)]
		private unsafe static extern long sys_recvfrom(int socket, void* buffer, ulong length, int flags, _SockaddrHeader* address);

		// Token: 0x06000641 RID: 1601 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public unsafe static long recvfrom(int socket, void* buffer, ulong length, MessageFlags flags, Sockaddr address)
		{
			int num = NativeConvert.FromMessageFlags(flags);
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(address).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] dynamicData;
				byte* ptr3;
				if ((dynamicData = Sockaddr.GetDynamicData(address)) == null || dynamicData.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &dynamicData[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(address, ptr3, true);
				long num2 = Syscall.sys_recvfrom(socket, buffer, length, num, Sockaddr.GetNative(&sockaddrDynamic, ptr2));
				sockaddrDynamic.Update(address);
				return num2;
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000DCAF File Offset: 0x0000BEAF
		public unsafe static long recvfrom(int socket, IntPtr buffer, ulong length, MessageFlags flags, Sockaddr address)
		{
			return Syscall.recvfrom(socket, (void*)buffer, length, flags, address);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		public unsafe static long recvfrom(int socket, byte[] buffer, ulong length, MessageFlags flags, Sockaddr address)
		{
			if (length > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("length", "length > buffer.LongLength");
			}
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			return Syscall.recvfrom(socket, (void*)ptr, length, flags, address);
		}

		// Token: 0x06000644 RID: 1604
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sendto", SetLastError = true)]
		private unsafe static extern long sys_sendto(int socket, void* message, ulong length, int flags, _SockaddrHeader* address);

		// Token: 0x06000645 RID: 1605 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		public unsafe static long sendto(int socket, void* message, ulong length, MessageFlags flags, Sockaddr address)
		{
			int num = NativeConvert.FromMessageFlags(flags);
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(address).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] dynamicData;
				byte* ptr3;
				if ((dynamicData = Sockaddr.GetDynamicData(address)) == null || dynamicData.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &dynamicData[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(address, ptr3, false);
				return Syscall.sys_sendto(socket, message, length, num, Sockaddr.GetNative(&sockaddrDynamic, ptr2));
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000DD6E File Offset: 0x0000BF6E
		public unsafe static long sendto(int socket, IntPtr message, ulong length, MessageFlags flags, Sockaddr address)
		{
			return Syscall.sendto(socket, (void*)message, length, flags, address);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public unsafe static long sendto(int socket, byte[] message, ulong length, MessageFlags flags, Sockaddr address)
		{
			if (length > (ulong)((long)message.Length))
			{
				throw new ArgumentOutOfRangeException("length", "length > message.LongLength");
			}
			byte* ptr;
			if (message == null || message.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &message[0];
			}
			return Syscall.sendto(socket, (void*)ptr, length, flags, address);
		}

		// Token: 0x06000648 RID: 1608
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_recvmsg", SetLastError = true)]
		private unsafe static extern long sys_recvmsg(int socket, ref Syscall._Msghdr message, _SockaddrHeader* msg_name, int flags);

		// Token: 0x06000649 RID: 1609 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		public unsafe static long recvmsg(int socket, Msghdr message, MessageFlags flags)
		{
			int num = NativeConvert.FromMessageFlags(flags);
			Sockaddr msg_name = message.msg_name;
			byte[] msg_control;
			byte* ptr;
			if ((msg_control = message.msg_control) == null || msg_control.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &msg_control[0];
			}
			Iovec[] msg_iov;
			Iovec* ptr2;
			if ((msg_iov = message.msg_iov) == null || msg_iov.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &msg_iov[0];
			}
			Syscall._Msghdr msghdr = new Syscall._Msghdr(message, ptr2, ptr);
			long num2;
			fixed (SockaddrType* ptr3 = &Sockaddr.GetAddress(msg_name).type)
			{
				SockaddrType* ptr4 = ptr3;
				byte[] array;
				byte* ptr5;
				if ((array = Sockaddr.GetDynamicData(msg_name)) == null || array.Length == 0)
				{
					ptr5 = null;
				}
				else
				{
					ptr5 = &array[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(msg_name, ptr5, true);
				num2 = Syscall.sys_recvmsg(socket, ref msghdr, Sockaddr.GetNative(&sockaddrDynamic, ptr4), num);
				sockaddrDynamic.Update(msg_name);
				array = null;
			}
			msghdr.Update(message);
			return num2;
		}

		// Token: 0x0600064A RID: 1610
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sendmsg", SetLastError = true)]
		private unsafe static extern long sys_sendmsg(int socket, ref Syscall._Msghdr message, _SockaddrHeader* msg_name, int flags);

		// Token: 0x0600064B RID: 1611 RVA: 0x0000DE98 File Offset: 0x0000C098
		public unsafe static long sendmsg(int socket, Msghdr message, MessageFlags flags)
		{
			int num = NativeConvert.FromMessageFlags(flags);
			Sockaddr msg_name = message.msg_name;
			byte[] msg_control;
			byte* ptr;
			if ((msg_control = message.msg_control) == null || msg_control.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &msg_control[0];
			}
			Iovec[] msg_iov;
			Iovec* ptr2;
			if ((msg_iov = message.msg_iov) == null || msg_iov.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &msg_iov[0];
			}
			Syscall._Msghdr msghdr = new Syscall._Msghdr(message, ptr2, ptr);
			fixed (SockaddrType* ptr3 = &Sockaddr.GetAddress(msg_name).type)
			{
				SockaddrType* ptr4 = ptr3;
				byte[] dynamicData;
				byte* ptr5;
				if ((dynamicData = Sockaddr.GetDynamicData(msg_name)) == null || dynamicData.Length == 0)
				{
					ptr5 = null;
				}
				else
				{
					ptr5 = &dynamicData[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(msg_name, ptr5, false);
				return Syscall.sys_sendmsg(socket, ref msghdr, Sockaddr.GetNative(&sockaddrDynamic, ptr4), num);
			}
		}

		// Token: 0x0600064C RID: 1612
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_CMSG_FIRSTHDR", SetLastError = true)]
		private unsafe static extern long CMSG_FIRSTHDR(byte* msg_control, long msg_controllen);

		// Token: 0x0600064D RID: 1613 RVA: 0x0000DF50 File Offset: 0x0000C150
		public unsafe static long CMSG_FIRSTHDR(Msghdr msgh)
		{
			if (msgh.msg_control == null && msgh.msg_controllen != 0L)
			{
				throw new ArgumentException("msgh.msg_control == null && msgh.msg_controllen != 0", "msgh");
			}
			if (msgh.msg_control != null && msgh.msg_controllen > (long)msgh.msg_control.Length)
			{
				throw new ArgumentException("msgh.msg_controllen > msgh.msg_control.Length", "msgh");
			}
			byte[] msg_control;
			byte* ptr;
			if ((msg_control = msgh.msg_control) == null || msg_control.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &msg_control[0];
			}
			return Syscall.CMSG_FIRSTHDR(ptr, msgh.msg_controllen);
		}

		// Token: 0x0600064E RID: 1614
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_CMSG_NXTHDR", SetLastError = true)]
		private unsafe static extern long CMSG_NXTHDR(byte* msg_control, long msg_controllen, long cmsg);

		// Token: 0x0600064F RID: 1615 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
		public unsafe static long CMSG_NXTHDR(Msghdr msgh, long cmsg)
		{
			if (msgh.msg_control == null || msgh.msg_controllen > (long)msgh.msg_control.Length)
			{
				throw new ArgumentException("msgh.msg_control == null || msgh.msg_controllen > msgh.msg_control.Length", "msgh");
			}
			if (cmsg < 0L || cmsg + (long)Cmsghdr.Size > msgh.msg_controllen)
			{
				throw new ArgumentException("cmsg offset pointing out of buffer", "cmsg");
			}
			byte[] msg_control;
			byte* ptr;
			if ((msg_control = msgh.msg_control) == null || msg_control.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &msg_control[0];
			}
			return Syscall.CMSG_NXTHDR(ptr, msgh.msg_controllen, cmsg);
		}

		// Token: 0x06000650 RID: 1616
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_CMSG_DATA", SetLastError = true)]
		private unsafe static extern long CMSG_DATA(byte* msg_control, long msg_controllen, long cmsg);

		// Token: 0x06000651 RID: 1617 RVA: 0x0000E058 File Offset: 0x0000C258
		public unsafe static long CMSG_DATA(Msghdr msgh, long cmsg)
		{
			if (msgh.msg_control == null || msgh.msg_controllen > (long)msgh.msg_control.Length)
			{
				throw new ArgumentException("msgh.msg_control == null || msgh.msg_controllen > msgh.msg_control.Length", "msgh");
			}
			if (cmsg < 0L || cmsg + (long)Cmsghdr.Size > msgh.msg_controllen)
			{
				throw new ArgumentException("cmsg offset pointing out of buffer", "cmsg");
			}
			byte[] msg_control;
			byte* ptr;
			if ((msg_control = msgh.msg_control) == null || msg_control.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &msg_control[0];
			}
			return Syscall.CMSG_DATA(ptr, msgh.msg_controllen, cmsg);
		}

		// Token: 0x06000652 RID: 1618
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_CMSG_ALIGN", SetLastError = true)]
		public static extern ulong CMSG_ALIGN(ulong length);

		// Token: 0x06000653 RID: 1619
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_CMSG_SPACE", SetLastError = true)]
		public static extern ulong CMSG_SPACE(ulong length);

		// Token: 0x06000654 RID: 1620
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_CMSG_LEN", SetLastError = true)]
		public static extern ulong CMSG_LEN(ulong length);

		// Token: 0x0400048A RID: 1162
		internal new const string LIBC = "libc";

		// Token: 0x0400048B RID: 1163
		internal static object readdir_lock = new object();

		// Token: 0x0400048C RID: 1164
		public static readonly int AT_FDCWD = Syscall.get_at_fdcwd();

		// Token: 0x0400048D RID: 1165
		internal static object fstab_lock = new object();

		// Token: 0x0400048E RID: 1166
		internal static object grp_lock = new object();

		// Token: 0x0400048F RID: 1167
		internal static object pwd_lock = new object();

		// Token: 0x04000490 RID: 1168
		private static object signal_lock = new object();

		// Token: 0x04000491 RID: 1169
		public static readonly int L_ctermid = Syscall._L_ctermid();

		// Token: 0x04000492 RID: 1170
		public static readonly int L_cuserid = Syscall._L_cuserid();

		// Token: 0x04000493 RID: 1171
		internal static object getlogin_lock = new object();

		// Token: 0x04000494 RID: 1172
		public static readonly IntPtr MAP_FAILED = (IntPtr)(-1);

		// Token: 0x04000495 RID: 1173
		public static readonly long UTIME_NOW = Syscall.get_utime_now();

		// Token: 0x04000496 RID: 1174
		public static readonly long UTIME_OMIT = Syscall.get_utime_omit();

		// Token: 0x04000497 RID: 1175
		private static object tty_lock = new object();

		// Token: 0x04000498 RID: 1176
		internal static object usershell_lock = new object();

		// Token: 0x020000A6 RID: 166
		private struct _Dirent
		{
			// Token: 0x04000555 RID: 1365
			[ino_t]
			public ulong d_ino;

			// Token: 0x04000556 RID: 1366
			[off_t]
			public long d_off;

			// Token: 0x04000557 RID: 1367
			public ushort d_reclen;

			// Token: 0x04000558 RID: 1368
			public byte d_type;

			// Token: 0x04000559 RID: 1369
			public IntPtr d_name;
		}

		// Token: 0x020000A7 RID: 167
		[Map]
		private struct _Fstab
		{
			// Token: 0x0400055A RID: 1370
			public IntPtr fs_spec;

			// Token: 0x0400055B RID: 1371
			public IntPtr fs_file;

			// Token: 0x0400055C RID: 1372
			public IntPtr fs_vfstype;

			// Token: 0x0400055D RID: 1373
			public IntPtr fs_mntops;

			// Token: 0x0400055E RID: 1374
			public IntPtr fs_type;

			// Token: 0x0400055F RID: 1375
			public int fs_freq;

			// Token: 0x04000560 RID: 1376
			public int fs_passno;

			// Token: 0x04000561 RID: 1377
			public IntPtr _fs_buf_;
		}

		// Token: 0x020000A8 RID: 168
		[Map]
		private struct _Group
		{
			// Token: 0x04000562 RID: 1378
			public IntPtr gr_name;

			// Token: 0x04000563 RID: 1379
			public IntPtr gr_passwd;

			// Token: 0x04000564 RID: 1380
			[gid_t]
			public uint gr_gid;

			// Token: 0x04000565 RID: 1381
			public int _gr_nmem_;

			// Token: 0x04000566 RID: 1382
			public IntPtr gr_mem;

			// Token: 0x04000567 RID: 1383
			public IntPtr _gr_buf_;
		}

		// Token: 0x020000A9 RID: 169
		[Map]
		private struct _Passwd
		{
			// Token: 0x04000568 RID: 1384
			public IntPtr pw_name;

			// Token: 0x04000569 RID: 1385
			public IntPtr pw_passwd;

			// Token: 0x0400056A RID: 1386
			[uid_t]
			public uint pw_uid;

			// Token: 0x0400056B RID: 1387
			[gid_t]
			public uint pw_gid;

			// Token: 0x0400056C RID: 1388
			public IntPtr pw_gecos;

			// Token: 0x0400056D RID: 1389
			public IntPtr pw_dir;

			// Token: 0x0400056E RID: 1390
			public IntPtr pw_shell;

			// Token: 0x0400056F RID: 1391
			public IntPtr _pw_buf_;
		}

		// Token: 0x020000AA RID: 170
		private struct _pollfd
		{
			// Token: 0x04000570 RID: 1392
			public int fd;

			// Token: 0x04000571 RID: 1393
			public short events;

			// Token: 0x04000572 RID: 1394
			public short revents;
		}

		// Token: 0x020000AB RID: 171
		[Map]
		private struct _Utsname
		{
			// Token: 0x04000573 RID: 1395
			public IntPtr sysname;

			// Token: 0x04000574 RID: 1396
			public IntPtr nodename;

			// Token: 0x04000575 RID: 1397
			public IntPtr release;

			// Token: 0x04000576 RID: 1398
			public IntPtr version;

			// Token: 0x04000577 RID: 1399
			public IntPtr machine;

			// Token: 0x04000578 RID: 1400
			public IntPtr domainname;

			// Token: 0x04000579 RID: 1401
			public IntPtr _buf_;
		}

		// Token: 0x020000AC RID: 172
		// (Invoke) Token: 0x0600076E RID: 1902
		private delegate long DoReadlinkFun(byte[] target);

		// Token: 0x020000AD RID: 173
		private struct _Msghdr
		{
			// Token: 0x06000771 RID: 1905 RVA: 0x0001058C File Offset: 0x0000E78C
			public unsafe _Msghdr(Msghdr message, Iovec* ptr_msg_iov, byte* ptr_msg_control)
			{
				if (message.msg_iovlen > message.msg_iov.Length || message.msg_iovlen < 0)
				{
					throw new ArgumentException("message.msg_iovlen > message.msg_iov.Length || message.msg_iovlen < 0", "message");
				}
				this.msg_iov = ptr_msg_iov;
				this.msg_iovlen = message.msg_iovlen;
				if (message.msg_control == null && message.msg_controllen != 0L)
				{
					throw new ArgumentException("message.msg_control == null && message.msg_controllen != 0", "message");
				}
				if (message.msg_control != null && message.msg_controllen > (long)message.msg_control.Length)
				{
					throw new ArgumentException("message.msg_controllen > message.msg_control.Length", "message");
				}
				this.msg_control = ptr_msg_control;
				this.msg_controllen = message.msg_controllen;
				this.msg_flags = 0;
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x00010638 File Offset: 0x0000E838
			public void Update(Msghdr message)
			{
				message.msg_controllen = this.msg_controllen;
				message.msg_flags = NativeConvert.ToMessageFlags(this.msg_flags);
			}

			// Token: 0x0400057A RID: 1402
			public unsafe Iovec* msg_iov;

			// Token: 0x0400057B RID: 1403
			public int msg_iovlen;

			// Token: 0x0400057C RID: 1404
			public unsafe byte* msg_control;

			// Token: 0x0400057D RID: 1405
			public long msg_controllen;

			// Token: 0x0400057E RID: 1406
			public int msg_flags;
		}
	}
}
