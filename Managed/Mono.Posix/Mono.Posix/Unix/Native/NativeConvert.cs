using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000026 RID: 38
	[CLSCompliant(false)]
	public sealed class NativeConvert
	{
		// Token: 0x0600020A RID: 522
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromRealTimeSignum")]
		private static extern int FromRealTimeSignum(int offset, out int rval);

		// Token: 0x0600020B RID: 523 RVA: 0x00007A4C File Offset: 0x00005C4C
		public static int FromRealTimeSignum(RealTimeSignum sig)
		{
			int num;
			if (NativeConvert.FromRealTimeSignum(sig.Offset, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(sig.Offset);
			}
			return num;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007A7C File Offset: 0x00005C7C
		public static RealTimeSignum ToRealTimeSignum(int offset)
		{
			return new RealTimeSignum(offset);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007A84 File Offset: 0x00005C84
		public static FilePermissions FromOctalPermissionString(string value)
		{
			return NativeConvert.ToFilePermissions(Convert.ToUInt32(value, 8));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007A94 File Offset: 0x00005C94
		public static string ToOctalPermissionString(FilePermissions value)
		{
			string text = Convert.ToString((int)(value & ~FilePermissions.S_IFMT), 8);
			return new string('0', 4 - text.Length) + text;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007AC4 File Offset: 0x00005CC4
		public static FilePermissions FromUnixPermissionString(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length != 9 && value.Length != 10)
			{
				throw new ArgumentException("value", "must contain 9 or 10 characters");
			}
			int num = 0;
			FilePermissions filePermissions = (FilePermissions)0U;
			if (value.Length == 10)
			{
				filePermissions |= NativeConvert.GetUnixPermissionDevice(value[num]);
				num++;
			}
			filePermissions |= NativeConvert.GetUnixPermissionGroup(value[num++], FilePermissions.S_IRUSR, value[num++], FilePermissions.S_IWUSR, value[num++], FilePermissions.S_IXUSR, 's', 'S', FilePermissions.S_ISUID);
			filePermissions |= NativeConvert.GetUnixPermissionGroup(value[num++], FilePermissions.S_IRGRP, value[num++], FilePermissions.S_IWGRP, value[num++], FilePermissions.S_IXGRP, 's', 'S', FilePermissions.S_ISGID);
			return filePermissions | NativeConvert.GetUnixPermissionGroup(value[num++], FilePermissions.S_IROTH, value[num++], FilePermissions.S_IWOTH, value[num++], FilePermissions.S_IXOTH, 't', 'T', FilePermissions.S_ISVTX);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007BD0 File Offset: 0x00005DD0
		private static FilePermissions GetUnixPermissionDevice(char value)
		{
			if (value <= 'd')
			{
				if (value == '-')
				{
					return FilePermissions.S_IFREG;
				}
				switch (value)
				{
				case 'b':
					return FilePermissions.S_IFBLK;
				case 'c':
					return FilePermissions.S_IFCHR;
				case 'd':
					return FilePermissions.S_IFDIR;
				}
			}
			else
			{
				if (value == 'l')
				{
					return FilePermissions.S_IFLNK;
				}
				if (value == 'p')
				{
					return FilePermissions.S_IFIFO;
				}
				if (value == 's')
				{
					return FilePermissions.S_IFSOCK;
				}
			}
			throw new ArgumentException("value", "invalid device specification: " + value.ToString());
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007C54 File Offset: 0x00005E54
		private static FilePermissions GetUnixPermissionGroup(char read, FilePermissions readb, char write, FilePermissions writeb, char exec, FilePermissions execb, char xboth, char xbitonly, FilePermissions xbit)
		{
			FilePermissions filePermissions = (FilePermissions)0U;
			if (read == 'r')
			{
				filePermissions |= readb;
			}
			if (write == 'w')
			{
				filePermissions |= writeb;
			}
			if (exec == 'x')
			{
				filePermissions |= execb;
			}
			else if (exec == xbitonly)
			{
				filePermissions |= xbit;
			}
			else if (exec == xboth)
			{
				filePermissions |= execb | xbit;
			}
			return filePermissions;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public static string ToUnixPermissionString(FilePermissions value)
		{
			char[] array = new char[] { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' };
			bool flag = true;
			FilePermissions filePermissions = value & FilePermissions.S_IFMT;
			if (filePermissions <= FilePermissions.S_IFDIR)
			{
				if (filePermissions == FilePermissions.S_IFIFO)
				{
					array[0] = 'p';
					goto IL_009E;
				}
				if (filePermissions == FilePermissions.S_IFCHR)
				{
					array[0] = 'c';
					goto IL_009E;
				}
				if (filePermissions == FilePermissions.S_IFDIR)
				{
					array[0] = 'd';
					goto IL_009E;
				}
			}
			else if (filePermissions <= FilePermissions.S_IFREG)
			{
				if (filePermissions == FilePermissions.S_IFBLK)
				{
					array[0] = 'b';
					goto IL_009E;
				}
				if (filePermissions == FilePermissions.S_IFREG)
				{
					array[0] = '-';
					goto IL_009E;
				}
			}
			else
			{
				if (filePermissions == FilePermissions.S_IFLNK)
				{
					array[0] = 'l';
					goto IL_009E;
				}
				if (filePermissions == FilePermissions.S_IFSOCK)
				{
					array[0] = 's';
					goto IL_009E;
				}
			}
			flag = false;
			IL_009E:
			NativeConvert.SetUnixPermissionGroup(value, array, 1, FilePermissions.S_IRUSR, FilePermissions.S_IWUSR, FilePermissions.S_IXUSR, 's', 'S', FilePermissions.S_ISUID);
			NativeConvert.SetUnixPermissionGroup(value, array, 4, FilePermissions.S_IRGRP, FilePermissions.S_IWGRP, FilePermissions.S_IXGRP, 's', 'S', FilePermissions.S_ISGID);
			NativeConvert.SetUnixPermissionGroup(value, array, 7, FilePermissions.S_IROTH, FilePermissions.S_IWOTH, FilePermissions.S_IXOTH, 't', 'T', FilePermissions.S_ISVTX);
			if (!flag)
			{
				return new string(array, 1, 9);
			}
			return new string(array);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007DA5 File Offset: 0x00005FA5
		private static void SetUnixPermissionGroup(FilePermissions value, char[] access, int index, FilePermissions read, FilePermissions write, FilePermissions exec, char both, char setonly, FilePermissions setxbit)
		{
			if (UnixFileSystemInfo.IsSet(value, read))
			{
				access[index] = 'r';
			}
			if (UnixFileSystemInfo.IsSet(value, write))
			{
				access[index + 1] = 'w';
			}
			access[index + 2] = NativeConvert.GetSymbolicMode(value, exec, both, setonly, setxbit);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007DDC File Offset: 0x00005FDC
		private static char GetSymbolicMode(FilePermissions value, FilePermissions xbit, char both, char setonly, FilePermissions setxbit)
		{
			bool flag = UnixFileSystemInfo.IsSet(value, xbit);
			bool flag2 = UnixFileSystemInfo.IsSet(value, setxbit);
			if (flag && flag2)
			{
				return both;
			}
			if (flag2)
			{
				return setonly;
			}
			if (flag)
			{
				return 'x';
			}
			return '-';
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007E0E File Offset: 0x0000600E
		public static DateTime ToDateTime(long time)
		{
			return NativeConvert.FromTimeT(time);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007E18 File Offset: 0x00006018
		public static DateTime ToDateTime(long time, long nanoTime)
		{
			return NativeConvert.FromTimeT(time).AddMilliseconds((double)(nanoTime / 1000L));
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007E3C File Offset: 0x0000603C
		public static long FromDateTime(DateTime time)
		{
			return NativeConvert.ToTimeT(time);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007E44 File Offset: 0x00006044
		public static DateTime FromTimeT(long time)
		{
			return NativeConvert.UnixEpoch.AddSeconds((double)time).ToLocalTime();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007E68 File Offset: 0x00006068
		public static long ToTimeT(DateTime time)
		{
			if (time.Kind == DateTimeKind.Unspecified)
			{
				throw new ArgumentException("DateTimeKind.Unspecified is not supported. Use Local or Utc times.", "time");
			}
			if (time.Kind == DateTimeKind.Local)
			{
				time = time.ToUniversalTime();
			}
			return (long)(time - NativeConvert.UnixEpoch).TotalSeconds;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007EB8 File Offset: 0x000060B8
		public static OpenFlags ToOpenFlags(FileMode mode, FileAccess access)
		{
			OpenFlags openFlags = OpenFlags.O_RDONLY;
			switch (mode)
			{
			case FileMode.CreateNew:
				openFlags = OpenFlags.O_CREAT | OpenFlags.O_EXCL;
				break;
			case FileMode.Create:
				openFlags = OpenFlags.O_CREAT | OpenFlags.O_TRUNC;
				break;
			case FileMode.Open:
				break;
			case FileMode.OpenOrCreate:
				openFlags = OpenFlags.O_CREAT;
				break;
			case FileMode.Truncate:
				openFlags = OpenFlags.O_TRUNC;
				break;
			case FileMode.Append:
				openFlags = OpenFlags.O_APPEND;
				break;
			default:
				throw new ArgumentException(Locale.GetText("Unsupported mode value"), "mode");
			}
			int num;
			if (NativeConvert.TryFromOpenFlags(OpenFlags.O_LARGEFILE, out num))
			{
				openFlags |= OpenFlags.O_LARGEFILE;
			}
			switch (access)
			{
			case FileAccess.Read:
				openFlags |= OpenFlags.O_RDONLY;
				break;
			case FileAccess.Write:
				openFlags |= OpenFlags.O_WRONLY;
				break;
			case FileAccess.ReadWrite:
				openFlags |= OpenFlags.O_RDWR;
				break;
			default:
				throw new ArgumentOutOfRangeException(Locale.GetText("Unsupported access value"), "access");
			}
			return openFlags;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007F77 File Offset: 0x00006177
		public static string ToFopenMode(FileAccess access)
		{
			switch (access)
			{
			case FileAccess.Read:
				return "rb";
			case FileAccess.Write:
				return "wb";
			case FileAccess.ReadWrite:
				return "r+b";
			default:
				throw new ArgumentOutOfRangeException("access");
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007FAC File Offset: 0x000061AC
		public static string ToFopenMode(FileMode mode)
		{
			switch (mode)
			{
			case FileMode.CreateNew:
			case FileMode.Create:
				return "w+b";
			case FileMode.Open:
			case FileMode.OpenOrCreate:
				return "r+b";
			case FileMode.Truncate:
				return "w+b";
			case FileMode.Append:
				return "a+b";
			default:
				throw new ArgumentOutOfRangeException("mode");
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008000 File Offset: 0x00006200
		public static string ToFopenMode(FileMode mode, FileAccess access)
		{
			int num = -1;
			int num2 = -1;
			switch (mode)
			{
			case FileMode.CreateNew:
				num = 0;
				break;
			case FileMode.Create:
				num = 1;
				break;
			case FileMode.Open:
				num = 2;
				break;
			case FileMode.OpenOrCreate:
				num = 3;
				break;
			case FileMode.Truncate:
				num = 4;
				break;
			case FileMode.Append:
				num = 5;
				break;
			}
			switch (access)
			{
			case FileAccess.Read:
				num2 = 0;
				break;
			case FileAccess.Write:
				num2 = 1;
				break;
			case FileAccess.ReadWrite:
				num2 = 2;
				break;
			}
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException("mode");
			}
			if (num2 == -1)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			string text = NativeConvert.fopen_modes[num][num2];
			if (text[0] != 'r' && text[0] != 'w' && text[0] != 'a')
			{
				throw new ArgumentException(text);
			}
			return text;
		}

		// Token: 0x0600021E RID: 542
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromStat")]
		private static extern int FromStat(ref Stat source, IntPtr destination);

		// Token: 0x0600021F RID: 543 RVA: 0x000080BA File Offset: 0x000062BA
		public static bool TryCopy(ref Stat source, IntPtr destination)
		{
			return NativeConvert.FromStat(ref source, destination) == 0;
		}

		// Token: 0x06000220 RID: 544
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToStat")]
		private static extern int ToStat(IntPtr source, out Stat destination);

		// Token: 0x06000221 RID: 545 RVA: 0x000080C6 File Offset: 0x000062C6
		public static bool TryCopy(IntPtr source, out Stat destination)
		{
			return NativeConvert.ToStat(source, out destination) == 0;
		}

		// Token: 0x06000222 RID: 546
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromStatvfs")]
		private static extern int FromStatvfs(ref Statvfs source, IntPtr destination);

		// Token: 0x06000223 RID: 547 RVA: 0x000080D2 File Offset: 0x000062D2
		public static bool TryCopy(ref Statvfs source, IntPtr destination)
		{
			return NativeConvert.FromStatvfs(ref source, destination) == 0;
		}

		// Token: 0x06000224 RID: 548
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToStatvfs")]
		private static extern int ToStatvfs(IntPtr source, out Statvfs destination);

		// Token: 0x06000225 RID: 549 RVA: 0x000080DE File Offset: 0x000062DE
		public static bool TryCopy(IntPtr source, out Statvfs destination)
		{
			return NativeConvert.ToStatvfs(source, out destination) == 0;
		}

		// Token: 0x06000226 RID: 550
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromInAddr")]
		private static extern int FromInAddr(ref InAddr source, IntPtr destination);

		// Token: 0x06000227 RID: 551 RVA: 0x000080EA File Offset: 0x000062EA
		public static bool TryCopy(ref InAddr source, IntPtr destination)
		{
			return NativeConvert.FromInAddr(ref source, destination) == 0;
		}

		// Token: 0x06000228 RID: 552
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToInAddr")]
		private static extern int ToInAddr(IntPtr source, out InAddr destination);

		// Token: 0x06000229 RID: 553 RVA: 0x000080F6 File Offset: 0x000062F6
		public static bool TryCopy(IntPtr source, out InAddr destination)
		{
			return NativeConvert.ToInAddr(source, out destination) == 0;
		}

		// Token: 0x0600022A RID: 554
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromIn6Addr")]
		private static extern int FromIn6Addr(ref In6Addr source, IntPtr destination);

		// Token: 0x0600022B RID: 555 RVA: 0x00008102 File Offset: 0x00006302
		public static bool TryCopy(ref In6Addr source, IntPtr destination)
		{
			return NativeConvert.FromIn6Addr(ref source, destination) == 0;
		}

		// Token: 0x0600022C RID: 556
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToIn6Addr")]
		private static extern int ToIn6Addr(IntPtr source, out In6Addr destination);

		// Token: 0x0600022D RID: 557 RVA: 0x0000810E File Offset: 0x0000630E
		public static bool TryCopy(IntPtr source, out In6Addr destination)
		{
			return NativeConvert.ToIn6Addr(source, out destination) == 0;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000811A File Offset: 0x0000631A
		public static InAddr ToInAddr(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.AddressFamily != AddressFamily.InterNetwork)
			{
				throw new ArgumentException("address", "address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork");
			}
			return new InAddr(address.GetAddressBytes());
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008150 File Offset: 0x00006350
		public static IPAddress ToIPAddress(InAddr address)
		{
			byte[] array = new byte[4];
			address.CopyTo(array, 0);
			return new IPAddress(array);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008173 File Offset: 0x00006373
		public static In6Addr ToIn6Addr(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.AddressFamily != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException("address", "address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6");
			}
			return new In6Addr(address.GetAddressBytes());
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000081A8 File Offset: 0x000063A8
		public static IPAddress ToIPAddress(In6Addr address)
		{
			byte[] array = new byte[16];
			address.CopyTo(array, 0);
			return new IPAddress(array);
		}

		// Token: 0x06000232 RID: 562
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSockaddr")]
		private unsafe static extern int FromSockaddr(_SockaddrHeader* source, IntPtr destination);

		// Token: 0x06000233 RID: 563 RVA: 0x000081CC File Offset: 0x000063CC
		public unsafe static bool TryCopy(Sockaddr source, IntPtr destination)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			byte[] dynamicData = Sockaddr.GetDynamicData(source);
			if (source.type == (SockaddrType)32769)
			{
				Marshal.Copy(dynamicData, 0, destination, (int)source.GetDynamicLength());
				return true;
			}
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(source).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] array;
				byte* ptr3;
				if ((array = dynamicData) == null || array.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &array[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(source, ptr3, false);
				return NativeConvert.FromSockaddr(Sockaddr.GetNative(&sockaddrDynamic, ptr2), destination) == 0;
			}
		}

		// Token: 0x06000234 RID: 564
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSockaddr")]
		private unsafe static extern int ToSockaddr(IntPtr source, long size, _SockaddrHeader* destination);

		// Token: 0x06000235 RID: 565 RVA: 0x00008254 File Offset: 0x00006454
		public unsafe static bool TryCopy(IntPtr source, long size, Sockaddr destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			byte[] dynamicData = Sockaddr.GetDynamicData(destination);
			fixed (SockaddrType* ptr = &Sockaddr.GetAddress(destination).type)
			{
				SockaddrType* ptr2 = ptr;
				byte[] dynamicData2;
				byte* ptr3;
				if ((dynamicData2 = Sockaddr.GetDynamicData(destination)) == null || dynamicData2.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &dynamicData2[0];
				}
				_SockaddrDynamic sockaddrDynamic = new _SockaddrDynamic(destination, ptr3, true);
				int num = NativeConvert.ToSockaddr(source, size, Sockaddr.GetNative(&sockaddrDynamic, ptr2));
				sockaddrDynamic.Update(destination);
				if (num == 0 && destination.type == (SockaddrType)32769)
				{
					Marshal.Copy(source, dynamicData, 0, (int)destination.GetDynamicLength());
				}
				return num == 0;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000082E9 File Offset: 0x000064E9
		private NativeConvert()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000082F1 File Offset: 0x000064F1
		private static void ThrowArgumentException(object value)
		{
			throw new ArgumentOutOfRangeException("value", value, Locale.GetText("Current platform doesn't support this value."));
		}

		// Token: 0x06000238 RID: 568
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromAccessModes")]
		private static extern int FromAccessModes(AccessModes value, out int rval);

		// Token: 0x06000239 RID: 569 RVA: 0x00008308 File Offset: 0x00006508
		public static bool TryFromAccessModes(AccessModes value, out int rval)
		{
			return NativeConvert.FromAccessModes(value, out rval) == 0;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008314 File Offset: 0x00006514
		public static int FromAccessModes(AccessModes value)
		{
			int num;
			if (NativeConvert.FromAccessModes(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600023B RID: 571
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToAccessModes")]
		private static extern int ToAccessModes(int value, out AccessModes rval);

		// Token: 0x0600023C RID: 572 RVA: 0x00008338 File Offset: 0x00006538
		public static bool TryToAccessModes(int value, out AccessModes rval)
		{
			return NativeConvert.ToAccessModes(value, out rval) == 0;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008344 File Offset: 0x00006544
		public static AccessModes ToAccessModes(int value)
		{
			AccessModes accessModes;
			if (NativeConvert.ToAccessModes(value, out accessModes) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return accessModes;
		}

		// Token: 0x0600023E RID: 574
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromAtFlags")]
		private static extern int FromAtFlags(AtFlags value, out int rval);

		// Token: 0x0600023F RID: 575 RVA: 0x00008368 File Offset: 0x00006568
		public static bool TryFromAtFlags(AtFlags value, out int rval)
		{
			return NativeConvert.FromAtFlags(value, out rval) == 0;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008374 File Offset: 0x00006574
		public static int FromAtFlags(AtFlags value)
		{
			int num;
			if (NativeConvert.FromAtFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000241 RID: 577
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToAtFlags")]
		private static extern int ToAtFlags(int value, out AtFlags rval);

		// Token: 0x06000242 RID: 578 RVA: 0x00008398 File Offset: 0x00006598
		public static bool TryToAtFlags(int value, out AtFlags rval)
		{
			return NativeConvert.ToAtFlags(value, out rval) == 0;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000083A4 File Offset: 0x000065A4
		public static AtFlags ToAtFlags(int value)
		{
			AtFlags atFlags;
			if (NativeConvert.ToAtFlags(value, out atFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return atFlags;
		}

		// Token: 0x06000244 RID: 580
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromCmsghdr")]
		private static extern int FromCmsghdr(ref Cmsghdr source, IntPtr destination);

		// Token: 0x06000245 RID: 581 RVA: 0x000083C8 File Offset: 0x000065C8
		public static bool TryCopy(ref Cmsghdr source, IntPtr destination)
		{
			return NativeConvert.FromCmsghdr(ref source, destination) == 0;
		}

		// Token: 0x06000246 RID: 582
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToCmsghdr")]
		private static extern int ToCmsghdr(IntPtr source, out Cmsghdr destination);

		// Token: 0x06000247 RID: 583 RVA: 0x000083D4 File Offset: 0x000065D4
		public static bool TryCopy(IntPtr source, out Cmsghdr destination)
		{
			return NativeConvert.ToCmsghdr(source, out destination) == 0;
		}

		// Token: 0x06000248 RID: 584
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromConfstrName")]
		private static extern int FromConfstrName(ConfstrName value, out int rval);

		// Token: 0x06000249 RID: 585 RVA: 0x000083E0 File Offset: 0x000065E0
		public static bool TryFromConfstrName(ConfstrName value, out int rval)
		{
			return NativeConvert.FromConfstrName(value, out rval) == 0;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000083EC File Offset: 0x000065EC
		public static int FromConfstrName(ConfstrName value)
		{
			int num;
			if (NativeConvert.FromConfstrName(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600024B RID: 587
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToConfstrName")]
		private static extern int ToConfstrName(int value, out ConfstrName rval);

		// Token: 0x0600024C RID: 588 RVA: 0x00008410 File Offset: 0x00006610
		public static bool TryToConfstrName(int value, out ConfstrName rval)
		{
			return NativeConvert.ToConfstrName(value, out rval) == 0;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000841C File Offset: 0x0000661C
		public static ConfstrName ToConfstrName(int value)
		{
			ConfstrName confstrName;
			if (NativeConvert.ToConfstrName(value, out confstrName) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return confstrName;
		}

		// Token: 0x0600024E RID: 590
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromDirectoryNotifyFlags")]
		private static extern int FromDirectoryNotifyFlags(DirectoryNotifyFlags value, out int rval);

		// Token: 0x0600024F RID: 591 RVA: 0x00008440 File Offset: 0x00006640
		public static bool TryFromDirectoryNotifyFlags(DirectoryNotifyFlags value, out int rval)
		{
			return NativeConvert.FromDirectoryNotifyFlags(value, out rval) == 0;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000844C File Offset: 0x0000664C
		public static int FromDirectoryNotifyFlags(DirectoryNotifyFlags value)
		{
			int num;
			if (NativeConvert.FromDirectoryNotifyFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000251 RID: 593
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToDirectoryNotifyFlags")]
		private static extern int ToDirectoryNotifyFlags(int value, out DirectoryNotifyFlags rval);

		// Token: 0x06000252 RID: 594 RVA: 0x00008470 File Offset: 0x00006670
		public static bool TryToDirectoryNotifyFlags(int value, out DirectoryNotifyFlags rval)
		{
			return NativeConvert.ToDirectoryNotifyFlags(value, out rval) == 0;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000847C File Offset: 0x0000667C
		public static DirectoryNotifyFlags ToDirectoryNotifyFlags(int value)
		{
			DirectoryNotifyFlags directoryNotifyFlags;
			if (NativeConvert.ToDirectoryNotifyFlags(value, out directoryNotifyFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return directoryNotifyFlags;
		}

		// Token: 0x06000254 RID: 596
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromEpollEvents")]
		private static extern int FromEpollEvents(EpollEvents value, out uint rval);

		// Token: 0x06000255 RID: 597 RVA: 0x000084A0 File Offset: 0x000066A0
		public static bool TryFromEpollEvents(EpollEvents value, out uint rval)
		{
			return NativeConvert.FromEpollEvents(value, out rval) == 0;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000084AC File Offset: 0x000066AC
		public static uint FromEpollEvents(EpollEvents value)
		{
			uint num;
			if (NativeConvert.FromEpollEvents(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000257 RID: 599
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToEpollEvents")]
		private static extern int ToEpollEvents(uint value, out EpollEvents rval);

		// Token: 0x06000258 RID: 600 RVA: 0x000084D0 File Offset: 0x000066D0
		public static bool TryToEpollEvents(uint value, out EpollEvents rval)
		{
			return NativeConvert.ToEpollEvents(value, out rval) == 0;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000084DC File Offset: 0x000066DC
		public static EpollEvents ToEpollEvents(uint value)
		{
			EpollEvents epollEvents;
			if (NativeConvert.ToEpollEvents(value, out epollEvents) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return epollEvents;
		}

		// Token: 0x0600025A RID: 602
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromEpollFlags")]
		private static extern int FromEpollFlags(EpollFlags value, out int rval);

		// Token: 0x0600025B RID: 603 RVA: 0x00008500 File Offset: 0x00006700
		public static bool TryFromEpollFlags(EpollFlags value, out int rval)
		{
			return NativeConvert.FromEpollFlags(value, out rval) == 0;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000850C File Offset: 0x0000670C
		public static int FromEpollFlags(EpollFlags value)
		{
			int num;
			if (NativeConvert.FromEpollFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600025D RID: 605
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToEpollFlags")]
		private static extern int ToEpollFlags(int value, out EpollFlags rval);

		// Token: 0x0600025E RID: 606 RVA: 0x00008530 File Offset: 0x00006730
		public static bool TryToEpollFlags(int value, out EpollFlags rval)
		{
			return NativeConvert.ToEpollFlags(value, out rval) == 0;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000853C File Offset: 0x0000673C
		public static EpollFlags ToEpollFlags(int value)
		{
			EpollFlags epollFlags;
			if (NativeConvert.ToEpollFlags(value, out epollFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return epollFlags;
		}

		// Token: 0x06000260 RID: 608
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromErrno")]
		private static extern int FromErrno(Errno value, out int rval);

		// Token: 0x06000261 RID: 609 RVA: 0x00008560 File Offset: 0x00006760
		public static bool TryFromErrno(Errno value, out int rval)
		{
			return NativeConvert.FromErrno(value, out rval) == 0;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000856C File Offset: 0x0000676C
		public static int FromErrno(Errno value)
		{
			int num;
			if (NativeConvert.FromErrno(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000263 RID: 611
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToErrno")]
		private static extern int ToErrno(int value, out Errno rval);

		// Token: 0x06000264 RID: 612 RVA: 0x00008590 File Offset: 0x00006790
		public static bool TryToErrno(int value, out Errno rval)
		{
			return NativeConvert.ToErrno(value, out rval) == 0;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000859C File Offset: 0x0000679C
		public static Errno ToErrno(int value)
		{
			Errno errno;
			if (NativeConvert.ToErrno(value, out errno) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return errno;
		}

		// Token: 0x06000266 RID: 614
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromFcntlCommand")]
		private static extern int FromFcntlCommand(FcntlCommand value, out int rval);

		// Token: 0x06000267 RID: 615 RVA: 0x000085C0 File Offset: 0x000067C0
		public static bool TryFromFcntlCommand(FcntlCommand value, out int rval)
		{
			return NativeConvert.FromFcntlCommand(value, out rval) == 0;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000085CC File Offset: 0x000067CC
		public static int FromFcntlCommand(FcntlCommand value)
		{
			int num;
			if (NativeConvert.FromFcntlCommand(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000269 RID: 617
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToFcntlCommand")]
		private static extern int ToFcntlCommand(int value, out FcntlCommand rval);

		// Token: 0x0600026A RID: 618 RVA: 0x000085F0 File Offset: 0x000067F0
		public static bool TryToFcntlCommand(int value, out FcntlCommand rval)
		{
			return NativeConvert.ToFcntlCommand(value, out rval) == 0;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000085FC File Offset: 0x000067FC
		public static FcntlCommand ToFcntlCommand(int value)
		{
			FcntlCommand fcntlCommand;
			if (NativeConvert.ToFcntlCommand(value, out fcntlCommand) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return fcntlCommand;
		}

		// Token: 0x0600026C RID: 620
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromFilePermissions")]
		private static extern int FromFilePermissions(FilePermissions value, out uint rval);

		// Token: 0x0600026D RID: 621 RVA: 0x00008620 File Offset: 0x00006820
		public static bool TryFromFilePermissions(FilePermissions value, out uint rval)
		{
			return NativeConvert.FromFilePermissions(value, out rval) == 0;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000862C File Offset: 0x0000682C
		public static uint FromFilePermissions(FilePermissions value)
		{
			uint num;
			if (NativeConvert.FromFilePermissions(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600026F RID: 623
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToFilePermissions")]
		private static extern int ToFilePermissions(uint value, out FilePermissions rval);

		// Token: 0x06000270 RID: 624 RVA: 0x00008650 File Offset: 0x00006850
		public static bool TryToFilePermissions(uint value, out FilePermissions rval)
		{
			return NativeConvert.ToFilePermissions(value, out rval) == 0;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000865C File Offset: 0x0000685C
		public static FilePermissions ToFilePermissions(uint value)
		{
			FilePermissions filePermissions;
			if (NativeConvert.ToFilePermissions(value, out filePermissions) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return filePermissions;
		}

		// Token: 0x06000272 RID: 626
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromFlock")]
		private static extern int FromFlock(ref Flock source, IntPtr destination);

		// Token: 0x06000273 RID: 627 RVA: 0x00008680 File Offset: 0x00006880
		public static bool TryCopy(ref Flock source, IntPtr destination)
		{
			return NativeConvert.FromFlock(ref source, destination) == 0;
		}

		// Token: 0x06000274 RID: 628
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToFlock")]
		private static extern int ToFlock(IntPtr source, out Flock destination);

		// Token: 0x06000275 RID: 629 RVA: 0x0000868C File Offset: 0x0000688C
		public static bool TryCopy(IntPtr source, out Flock destination)
		{
			return NativeConvert.ToFlock(source, out destination) == 0;
		}

		// Token: 0x06000276 RID: 630
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromIovec")]
		private static extern int FromIovec(ref Iovec source, IntPtr destination);

		// Token: 0x06000277 RID: 631 RVA: 0x00008698 File Offset: 0x00006898
		public static bool TryCopy(ref Iovec source, IntPtr destination)
		{
			return NativeConvert.FromIovec(ref source, destination) == 0;
		}

		// Token: 0x06000278 RID: 632
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToIovec")]
		private static extern int ToIovec(IntPtr source, out Iovec destination);

		// Token: 0x06000279 RID: 633 RVA: 0x000086A4 File Offset: 0x000068A4
		public static bool TryCopy(IntPtr source, out Iovec destination)
		{
			return NativeConvert.ToIovec(source, out destination) == 0;
		}

		// Token: 0x0600027A RID: 634
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromLinger")]
		private static extern int FromLinger(ref Linger source, IntPtr destination);

		// Token: 0x0600027B RID: 635 RVA: 0x000086B0 File Offset: 0x000068B0
		public static bool TryCopy(ref Linger source, IntPtr destination)
		{
			return NativeConvert.FromLinger(ref source, destination) == 0;
		}

		// Token: 0x0600027C RID: 636
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToLinger")]
		private static extern int ToLinger(IntPtr source, out Linger destination);

		// Token: 0x0600027D RID: 637 RVA: 0x000086BC File Offset: 0x000068BC
		public static bool TryCopy(IntPtr source, out Linger destination)
		{
			return NativeConvert.ToLinger(source, out destination) == 0;
		}

		// Token: 0x0600027E RID: 638
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromLockType")]
		private static extern int FromLockType(LockType value, out short rval);

		// Token: 0x0600027F RID: 639 RVA: 0x000086C8 File Offset: 0x000068C8
		public static bool TryFromLockType(LockType value, out short rval)
		{
			return NativeConvert.FromLockType(value, out rval) == 0;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000086D4 File Offset: 0x000068D4
		public static short FromLockType(LockType value)
		{
			short num;
			if (NativeConvert.FromLockType(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000281 RID: 641
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToLockType")]
		private static extern int ToLockType(short value, out LockType rval);

		// Token: 0x06000282 RID: 642 RVA: 0x000086F8 File Offset: 0x000068F8
		public static bool TryToLockType(short value, out LockType rval)
		{
			return NativeConvert.ToLockType(value, out rval) == 0;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008704 File Offset: 0x00006904
		public static LockType ToLockType(short value)
		{
			LockType lockType;
			if (NativeConvert.ToLockType(value, out lockType) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return lockType;
		}

		// Token: 0x06000284 RID: 644
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromLockfCommand")]
		private static extern int FromLockfCommand(LockfCommand value, out int rval);

		// Token: 0x06000285 RID: 645 RVA: 0x00008728 File Offset: 0x00006928
		public static bool TryFromLockfCommand(LockfCommand value, out int rval)
		{
			return NativeConvert.FromLockfCommand(value, out rval) == 0;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008734 File Offset: 0x00006934
		public static int FromLockfCommand(LockfCommand value)
		{
			int num;
			if (NativeConvert.FromLockfCommand(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000287 RID: 647
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToLockfCommand")]
		private static extern int ToLockfCommand(int value, out LockfCommand rval);

		// Token: 0x06000288 RID: 648 RVA: 0x00008758 File Offset: 0x00006958
		public static bool TryToLockfCommand(int value, out LockfCommand rval)
		{
			return NativeConvert.ToLockfCommand(value, out rval) == 0;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008764 File Offset: 0x00006964
		public static LockfCommand ToLockfCommand(int value)
		{
			LockfCommand lockfCommand;
			if (NativeConvert.ToLockfCommand(value, out lockfCommand) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return lockfCommand;
		}

		// Token: 0x0600028A RID: 650
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMessageFlags")]
		private static extern int FromMessageFlags(MessageFlags value, out int rval);

		// Token: 0x0600028B RID: 651 RVA: 0x00008788 File Offset: 0x00006988
		public static bool TryFromMessageFlags(MessageFlags value, out int rval)
		{
			return NativeConvert.FromMessageFlags(value, out rval) == 0;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00008794 File Offset: 0x00006994
		public static int FromMessageFlags(MessageFlags value)
		{
			int num;
			if (NativeConvert.FromMessageFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600028D RID: 653
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMessageFlags")]
		private static extern int ToMessageFlags(int value, out MessageFlags rval);

		// Token: 0x0600028E RID: 654 RVA: 0x000087B8 File Offset: 0x000069B8
		public static bool TryToMessageFlags(int value, out MessageFlags rval)
		{
			return NativeConvert.ToMessageFlags(value, out rval) == 0;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000087C4 File Offset: 0x000069C4
		public static MessageFlags ToMessageFlags(int value)
		{
			MessageFlags messageFlags;
			if (NativeConvert.ToMessageFlags(value, out messageFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return messageFlags;
		}

		// Token: 0x06000290 RID: 656
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMlockallFlags")]
		private static extern int FromMlockallFlags(MlockallFlags value, out int rval);

		// Token: 0x06000291 RID: 657 RVA: 0x000087E8 File Offset: 0x000069E8
		public static bool TryFromMlockallFlags(MlockallFlags value, out int rval)
		{
			return NativeConvert.FromMlockallFlags(value, out rval) == 0;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000087F4 File Offset: 0x000069F4
		public static int FromMlockallFlags(MlockallFlags value)
		{
			int num;
			if (NativeConvert.FromMlockallFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000293 RID: 659
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMlockallFlags")]
		private static extern int ToMlockallFlags(int value, out MlockallFlags rval);

		// Token: 0x06000294 RID: 660 RVA: 0x00008818 File Offset: 0x00006A18
		public static bool TryToMlockallFlags(int value, out MlockallFlags rval)
		{
			return NativeConvert.ToMlockallFlags(value, out rval) == 0;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008824 File Offset: 0x00006A24
		public static MlockallFlags ToMlockallFlags(int value)
		{
			MlockallFlags mlockallFlags;
			if (NativeConvert.ToMlockallFlags(value, out mlockallFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return mlockallFlags;
		}

		// Token: 0x06000296 RID: 662
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMmapFlags")]
		private static extern int FromMmapFlags(MmapFlags value, out int rval);

		// Token: 0x06000297 RID: 663 RVA: 0x00008848 File Offset: 0x00006A48
		public static bool TryFromMmapFlags(MmapFlags value, out int rval)
		{
			return NativeConvert.FromMmapFlags(value, out rval) == 0;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008854 File Offset: 0x00006A54
		public static int FromMmapFlags(MmapFlags value)
		{
			int num;
			if (NativeConvert.FromMmapFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000299 RID: 665
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMmapFlags")]
		private static extern int ToMmapFlags(int value, out MmapFlags rval);

		// Token: 0x0600029A RID: 666 RVA: 0x00008878 File Offset: 0x00006A78
		public static bool TryToMmapFlags(int value, out MmapFlags rval)
		{
			return NativeConvert.ToMmapFlags(value, out rval) == 0;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008884 File Offset: 0x00006A84
		public static MmapFlags ToMmapFlags(int value)
		{
			MmapFlags mmapFlags;
			if (NativeConvert.ToMmapFlags(value, out mmapFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return mmapFlags;
		}

		// Token: 0x0600029C RID: 668
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMmapProts")]
		private static extern int FromMmapProts(MmapProts value, out int rval);

		// Token: 0x0600029D RID: 669 RVA: 0x000088A8 File Offset: 0x00006AA8
		public static bool TryFromMmapProts(MmapProts value, out int rval)
		{
			return NativeConvert.FromMmapProts(value, out rval) == 0;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000088B4 File Offset: 0x00006AB4
		public static int FromMmapProts(MmapProts value)
		{
			int num;
			if (NativeConvert.FromMmapProts(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600029F RID: 671
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMmapProts")]
		private static extern int ToMmapProts(int value, out MmapProts rval);

		// Token: 0x060002A0 RID: 672 RVA: 0x000088D8 File Offset: 0x00006AD8
		public static bool TryToMmapProts(int value, out MmapProts rval)
		{
			return NativeConvert.ToMmapProts(value, out rval) == 0;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000088E4 File Offset: 0x00006AE4
		public static MmapProts ToMmapProts(int value)
		{
			MmapProts mmapProts;
			if (NativeConvert.ToMmapProts(value, out mmapProts) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return mmapProts;
		}

		// Token: 0x060002A2 RID: 674
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMountFlags")]
		private static extern int FromMountFlags(MountFlags value, out ulong rval);

		// Token: 0x060002A3 RID: 675 RVA: 0x00008908 File Offset: 0x00006B08
		public static bool TryFromMountFlags(MountFlags value, out ulong rval)
		{
			return NativeConvert.FromMountFlags(value, out rval) == 0;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00008914 File Offset: 0x00006B14
		public static ulong FromMountFlags(MountFlags value)
		{
			ulong num;
			if (NativeConvert.FromMountFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002A5 RID: 677
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMountFlags")]
		private static extern int ToMountFlags(ulong value, out MountFlags rval);

		// Token: 0x060002A6 RID: 678 RVA: 0x00008938 File Offset: 0x00006B38
		public static bool TryToMountFlags(ulong value, out MountFlags rval)
		{
			return NativeConvert.ToMountFlags(value, out rval) == 0;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00008944 File Offset: 0x00006B44
		public static MountFlags ToMountFlags(ulong value)
		{
			MountFlags mountFlags;
			if (NativeConvert.ToMountFlags(value, out mountFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return mountFlags;
		}

		// Token: 0x060002A8 RID: 680
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMremapFlags")]
		private static extern int FromMremapFlags(MremapFlags value, out ulong rval);

		// Token: 0x060002A9 RID: 681 RVA: 0x00008968 File Offset: 0x00006B68
		public static bool TryFromMremapFlags(MremapFlags value, out ulong rval)
		{
			return NativeConvert.FromMremapFlags(value, out rval) == 0;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00008974 File Offset: 0x00006B74
		public static ulong FromMremapFlags(MremapFlags value)
		{
			ulong num;
			if (NativeConvert.FromMremapFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002AB RID: 683
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMremapFlags")]
		private static extern int ToMremapFlags(ulong value, out MremapFlags rval);

		// Token: 0x060002AC RID: 684 RVA: 0x00008998 File Offset: 0x00006B98
		public static bool TryToMremapFlags(ulong value, out MremapFlags rval)
		{
			return NativeConvert.ToMremapFlags(value, out rval) == 0;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000089A4 File Offset: 0x00006BA4
		public static MremapFlags ToMremapFlags(ulong value)
		{
			MremapFlags mremapFlags;
			if (NativeConvert.ToMremapFlags(value, out mremapFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return mremapFlags;
		}

		// Token: 0x060002AE RID: 686
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMsyncFlags")]
		private static extern int FromMsyncFlags(MsyncFlags value, out int rval);

		// Token: 0x060002AF RID: 687 RVA: 0x000089C8 File Offset: 0x00006BC8
		public static bool TryFromMsyncFlags(MsyncFlags value, out int rval)
		{
			return NativeConvert.FromMsyncFlags(value, out rval) == 0;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000089D4 File Offset: 0x00006BD4
		public static int FromMsyncFlags(MsyncFlags value)
		{
			int num;
			if (NativeConvert.FromMsyncFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002B1 RID: 689
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMsyncFlags")]
		private static extern int ToMsyncFlags(int value, out MsyncFlags rval);

		// Token: 0x060002B2 RID: 690 RVA: 0x000089F8 File Offset: 0x00006BF8
		public static bool TryToMsyncFlags(int value, out MsyncFlags rval)
		{
			return NativeConvert.ToMsyncFlags(value, out rval) == 0;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00008A04 File Offset: 0x00006C04
		public static MsyncFlags ToMsyncFlags(int value)
		{
			MsyncFlags msyncFlags;
			if (NativeConvert.ToMsyncFlags(value, out msyncFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return msyncFlags;
		}

		// Token: 0x060002B4 RID: 692
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromOpenFlags")]
		private static extern int FromOpenFlags(OpenFlags value, out int rval);

		// Token: 0x060002B5 RID: 693 RVA: 0x00008A28 File Offset: 0x00006C28
		public static bool TryFromOpenFlags(OpenFlags value, out int rval)
		{
			return NativeConvert.FromOpenFlags(value, out rval) == 0;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008A34 File Offset: 0x00006C34
		public static int FromOpenFlags(OpenFlags value)
		{
			int num;
			if (NativeConvert.FromOpenFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002B7 RID: 695
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToOpenFlags")]
		private static extern int ToOpenFlags(int value, out OpenFlags rval);

		// Token: 0x060002B8 RID: 696 RVA: 0x00008A58 File Offset: 0x00006C58
		public static bool TryToOpenFlags(int value, out OpenFlags rval)
		{
			return NativeConvert.ToOpenFlags(value, out rval) == 0;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008A64 File Offset: 0x00006C64
		public static OpenFlags ToOpenFlags(int value)
		{
			OpenFlags openFlags;
			if (NativeConvert.ToOpenFlags(value, out openFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return openFlags;
		}

		// Token: 0x060002BA RID: 698
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPathconfName")]
		private static extern int FromPathconfName(PathconfName value, out int rval);

		// Token: 0x060002BB RID: 699 RVA: 0x00008A88 File Offset: 0x00006C88
		public static bool TryFromPathconfName(PathconfName value, out int rval)
		{
			return NativeConvert.FromPathconfName(value, out rval) == 0;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00008A94 File Offset: 0x00006C94
		public static int FromPathconfName(PathconfName value)
		{
			int num;
			if (NativeConvert.FromPathconfName(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002BD RID: 701
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPathconfName")]
		private static extern int ToPathconfName(int value, out PathconfName rval);

		// Token: 0x060002BE RID: 702 RVA: 0x00008AB8 File Offset: 0x00006CB8
		public static bool TryToPathconfName(int value, out PathconfName rval)
		{
			return NativeConvert.ToPathconfName(value, out rval) == 0;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008AC4 File Offset: 0x00006CC4
		public static PathconfName ToPathconfName(int value)
		{
			PathconfName pathconfName;
			if (NativeConvert.ToPathconfName(value, out pathconfName) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return pathconfName;
		}

		// Token: 0x060002C0 RID: 704
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPollEvents")]
		private static extern int FromPollEvents(PollEvents value, out short rval);

		// Token: 0x060002C1 RID: 705 RVA: 0x00008AE8 File Offset: 0x00006CE8
		public static bool TryFromPollEvents(PollEvents value, out short rval)
		{
			return NativeConvert.FromPollEvents(value, out rval) == 0;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public static short FromPollEvents(PollEvents value)
		{
			short num;
			if (NativeConvert.FromPollEvents(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002C3 RID: 707
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPollEvents")]
		private static extern int ToPollEvents(short value, out PollEvents rval);

		// Token: 0x060002C4 RID: 708 RVA: 0x00008B18 File Offset: 0x00006D18
		public static bool TryToPollEvents(short value, out PollEvents rval)
		{
			return NativeConvert.ToPollEvents(value, out rval) == 0;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00008B24 File Offset: 0x00006D24
		public static PollEvents ToPollEvents(short value)
		{
			PollEvents pollEvents;
			if (NativeConvert.ToPollEvents(value, out pollEvents) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return pollEvents;
		}

		// Token: 0x060002C6 RID: 710
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPollfd")]
		private static extern int FromPollfd(ref Pollfd source, IntPtr destination);

		// Token: 0x060002C7 RID: 711 RVA: 0x00008B48 File Offset: 0x00006D48
		public static bool TryCopy(ref Pollfd source, IntPtr destination)
		{
			return NativeConvert.FromPollfd(ref source, destination) == 0;
		}

		// Token: 0x060002C8 RID: 712
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPollfd")]
		private static extern int ToPollfd(IntPtr source, out Pollfd destination);

		// Token: 0x060002C9 RID: 713 RVA: 0x00008B54 File Offset: 0x00006D54
		public static bool TryCopy(IntPtr source, out Pollfd destination)
		{
			return NativeConvert.ToPollfd(source, out destination) == 0;
		}

		// Token: 0x060002CA RID: 714
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPosixFadviseAdvice")]
		private static extern int FromPosixFadviseAdvice(PosixFadviseAdvice value, out int rval);

		// Token: 0x060002CB RID: 715 RVA: 0x00008B60 File Offset: 0x00006D60
		public static bool TryFromPosixFadviseAdvice(PosixFadviseAdvice value, out int rval)
		{
			return NativeConvert.FromPosixFadviseAdvice(value, out rval) == 0;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008B6C File Offset: 0x00006D6C
		public static int FromPosixFadviseAdvice(PosixFadviseAdvice value)
		{
			int num;
			if (NativeConvert.FromPosixFadviseAdvice(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002CD RID: 717
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPosixFadviseAdvice")]
		private static extern int ToPosixFadviseAdvice(int value, out PosixFadviseAdvice rval);

		// Token: 0x060002CE RID: 718 RVA: 0x00008B90 File Offset: 0x00006D90
		public static bool TryToPosixFadviseAdvice(int value, out PosixFadviseAdvice rval)
		{
			return NativeConvert.ToPosixFadviseAdvice(value, out rval) == 0;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008B9C File Offset: 0x00006D9C
		public static PosixFadviseAdvice ToPosixFadviseAdvice(int value)
		{
			PosixFadviseAdvice posixFadviseAdvice;
			if (NativeConvert.ToPosixFadviseAdvice(value, out posixFadviseAdvice) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return posixFadviseAdvice;
		}

		// Token: 0x060002D0 RID: 720
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPosixMadviseAdvice")]
		private static extern int FromPosixMadviseAdvice(PosixMadviseAdvice value, out int rval);

		// Token: 0x060002D1 RID: 721 RVA: 0x00008BC0 File Offset: 0x00006DC0
		public static bool TryFromPosixMadviseAdvice(PosixMadviseAdvice value, out int rval)
		{
			return NativeConvert.FromPosixMadviseAdvice(value, out rval) == 0;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008BCC File Offset: 0x00006DCC
		public static int FromPosixMadviseAdvice(PosixMadviseAdvice value)
		{
			int num;
			if (NativeConvert.FromPosixMadviseAdvice(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002D3 RID: 723
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPosixMadviseAdvice")]
		private static extern int ToPosixMadviseAdvice(int value, out PosixMadviseAdvice rval);

		// Token: 0x060002D4 RID: 724 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public static bool TryToPosixMadviseAdvice(int value, out PosixMadviseAdvice rval)
		{
			return NativeConvert.ToPosixMadviseAdvice(value, out rval) == 0;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008BFC File Offset: 0x00006DFC
		public static PosixMadviseAdvice ToPosixMadviseAdvice(int value)
		{
			PosixMadviseAdvice posixMadviseAdvice;
			if (NativeConvert.ToPosixMadviseAdvice(value, out posixMadviseAdvice) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return posixMadviseAdvice;
		}

		// Token: 0x060002D6 RID: 726
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSeekFlags")]
		private static extern int FromSeekFlags(SeekFlags value, out short rval);

		// Token: 0x060002D7 RID: 727 RVA: 0x00008C20 File Offset: 0x00006E20
		public static bool TryFromSeekFlags(SeekFlags value, out short rval)
		{
			return NativeConvert.FromSeekFlags(value, out rval) == 0;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008C2C File Offset: 0x00006E2C
		public static short FromSeekFlags(SeekFlags value)
		{
			short num;
			if (NativeConvert.FromSeekFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002D9 RID: 729
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSeekFlags")]
		private static extern int ToSeekFlags(short value, out SeekFlags rval);

		// Token: 0x060002DA RID: 730 RVA: 0x00008C50 File Offset: 0x00006E50
		public static bool TryToSeekFlags(short value, out SeekFlags rval)
		{
			return NativeConvert.ToSeekFlags(value, out rval) == 0;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008C5C File Offset: 0x00006E5C
		public static SeekFlags ToSeekFlags(short value)
		{
			SeekFlags seekFlags;
			if (NativeConvert.ToSeekFlags(value, out seekFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return seekFlags;
		}

		// Token: 0x060002DC RID: 732
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromShutdownOption")]
		private static extern int FromShutdownOption(ShutdownOption value, out int rval);

		// Token: 0x060002DD RID: 733 RVA: 0x00008C80 File Offset: 0x00006E80
		public static bool TryFromShutdownOption(ShutdownOption value, out int rval)
		{
			return NativeConvert.FromShutdownOption(value, out rval) == 0;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00008C8C File Offset: 0x00006E8C
		public static int FromShutdownOption(ShutdownOption value)
		{
			int num;
			if (NativeConvert.FromShutdownOption(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002DF RID: 735
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToShutdownOption")]
		private static extern int ToShutdownOption(int value, out ShutdownOption rval);

		// Token: 0x060002E0 RID: 736 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public static bool TryToShutdownOption(int value, out ShutdownOption rval)
		{
			return NativeConvert.ToShutdownOption(value, out rval) == 0;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00008CBC File Offset: 0x00006EBC
		public static ShutdownOption ToShutdownOption(int value)
		{
			ShutdownOption shutdownOption;
			if (NativeConvert.ToShutdownOption(value, out shutdownOption) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return shutdownOption;
		}

		// Token: 0x060002E2 RID: 738
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSignum")]
		private static extern int FromSignum(Signum value, out int rval);

		// Token: 0x060002E3 RID: 739 RVA: 0x00008CE0 File Offset: 0x00006EE0
		public static bool TryFromSignum(Signum value, out int rval)
		{
			return NativeConvert.FromSignum(value, out rval) == 0;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008CEC File Offset: 0x00006EEC
		public static int FromSignum(Signum value)
		{
			int num;
			if (NativeConvert.FromSignum(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002E5 RID: 741
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSignum")]
		private static extern int ToSignum(int value, out Signum rval);

		// Token: 0x060002E6 RID: 742 RVA: 0x00008D10 File Offset: 0x00006F10
		public static bool TryToSignum(int value, out Signum rval)
		{
			return NativeConvert.ToSignum(value, out rval) == 0;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008D1C File Offset: 0x00006F1C
		public static Signum ToSignum(int value)
		{
			Signum signum;
			if (NativeConvert.ToSignum(value, out signum) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return signum;
		}

		// Token: 0x060002E8 RID: 744
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSockaddrIn")]
		private static extern int FromSockaddrIn(SockaddrIn source, IntPtr destination);

		// Token: 0x060002E9 RID: 745 RVA: 0x00008D40 File Offset: 0x00006F40
		public static bool TryCopy(SockaddrIn source, IntPtr destination)
		{
			return NativeConvert.FromSockaddrIn(source, destination) == 0;
		}

		// Token: 0x060002EA RID: 746
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSockaddrIn")]
		private static extern int ToSockaddrIn(IntPtr source, SockaddrIn destination);

		// Token: 0x060002EB RID: 747 RVA: 0x00008D4C File Offset: 0x00006F4C
		public static bool TryCopy(IntPtr source, SockaddrIn destination)
		{
			return NativeConvert.ToSockaddrIn(source, destination) == 0;
		}

		// Token: 0x060002EC RID: 748
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSockaddrIn6")]
		private static extern int FromSockaddrIn6(SockaddrIn6 source, IntPtr destination);

		// Token: 0x060002ED RID: 749 RVA: 0x00008D58 File Offset: 0x00006F58
		public static bool TryCopy(SockaddrIn6 source, IntPtr destination)
		{
			return NativeConvert.FromSockaddrIn6(source, destination) == 0;
		}

		// Token: 0x060002EE RID: 750
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSockaddrIn6")]
		private static extern int ToSockaddrIn6(IntPtr source, SockaddrIn6 destination);

		// Token: 0x060002EF RID: 751 RVA: 0x00008D64 File Offset: 0x00006F64
		public static bool TryCopy(IntPtr source, SockaddrIn6 destination)
		{
			return NativeConvert.ToSockaddrIn6(source, destination) == 0;
		}

		// Token: 0x060002F0 RID: 752
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSockaddrType")]
		private static extern int FromSockaddrType(SockaddrType value, out int rval);

		// Token: 0x060002F1 RID: 753 RVA: 0x00008D70 File Offset: 0x00006F70
		internal static bool TryFromSockaddrType(SockaddrType value, out int rval)
		{
			return NativeConvert.FromSockaddrType(value, out rval) == 0;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00008D7C File Offset: 0x00006F7C
		internal static int FromSockaddrType(SockaddrType value)
		{
			int num;
			if (NativeConvert.FromSockaddrType(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002F3 RID: 755
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSockaddrType")]
		private static extern int ToSockaddrType(int value, out SockaddrType rval);

		// Token: 0x060002F4 RID: 756 RVA: 0x00008DA0 File Offset: 0x00006FA0
		internal static bool TryToSockaddrType(int value, out SockaddrType rval)
		{
			return NativeConvert.ToSockaddrType(value, out rval) == 0;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008DAC File Offset: 0x00006FAC
		internal static SockaddrType ToSockaddrType(int value)
		{
			SockaddrType sockaddrType;
			if (NativeConvert.ToSockaddrType(value, out sockaddrType) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return sockaddrType;
		}

		// Token: 0x060002F6 RID: 758
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSysconfName")]
		private static extern int FromSysconfName(SysconfName value, out int rval);

		// Token: 0x060002F7 RID: 759 RVA: 0x00008DD0 File Offset: 0x00006FD0
		public static bool TryFromSysconfName(SysconfName value, out int rval)
		{
			return NativeConvert.FromSysconfName(value, out rval) == 0;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00008DDC File Offset: 0x00006FDC
		public static int FromSysconfName(SysconfName value)
		{
			int num;
			if (NativeConvert.FromSysconfName(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002F9 RID: 761
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSysconfName")]
		private static extern int ToSysconfName(int value, out SysconfName rval);

		// Token: 0x060002FA RID: 762 RVA: 0x00008E00 File Offset: 0x00007000
		public static bool TryToSysconfName(int value, out SysconfName rval)
		{
			return NativeConvert.ToSysconfName(value, out rval) == 0;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00008E0C File Offset: 0x0000700C
		public static SysconfName ToSysconfName(int value)
		{
			SysconfName sysconfName;
			if (NativeConvert.ToSysconfName(value, out sysconfName) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return sysconfName;
		}

		// Token: 0x060002FC RID: 764
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSyslogFacility")]
		private static extern int FromSyslogFacility(SyslogFacility value, out int rval);

		// Token: 0x060002FD RID: 765 RVA: 0x00008E30 File Offset: 0x00007030
		public static bool TryFromSyslogFacility(SyslogFacility value, out int rval)
		{
			return NativeConvert.FromSyslogFacility(value, out rval) == 0;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00008E3C File Offset: 0x0000703C
		public static int FromSyslogFacility(SyslogFacility value)
		{
			int num;
			if (NativeConvert.FromSyslogFacility(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x060002FF RID: 767
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSyslogFacility")]
		private static extern int ToSyslogFacility(int value, out SyslogFacility rval);

		// Token: 0x06000300 RID: 768 RVA: 0x00008E60 File Offset: 0x00007060
		public static bool TryToSyslogFacility(int value, out SyslogFacility rval)
		{
			return NativeConvert.ToSyslogFacility(value, out rval) == 0;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00008E6C File Offset: 0x0000706C
		public static SyslogFacility ToSyslogFacility(int value)
		{
			SyslogFacility syslogFacility;
			if (NativeConvert.ToSyslogFacility(value, out syslogFacility) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return syslogFacility;
		}

		// Token: 0x06000302 RID: 770
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSyslogLevel")]
		private static extern int FromSyslogLevel(SyslogLevel value, out int rval);

		// Token: 0x06000303 RID: 771 RVA: 0x00008E90 File Offset: 0x00007090
		public static bool TryFromSyslogLevel(SyslogLevel value, out int rval)
		{
			return NativeConvert.FromSyslogLevel(value, out rval) == 0;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00008E9C File Offset: 0x0000709C
		public static int FromSyslogLevel(SyslogLevel value)
		{
			int num;
			if (NativeConvert.FromSyslogLevel(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000305 RID: 773
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSyslogLevel")]
		private static extern int ToSyslogLevel(int value, out SyslogLevel rval);

		// Token: 0x06000306 RID: 774 RVA: 0x00008EC0 File Offset: 0x000070C0
		public static bool TryToSyslogLevel(int value, out SyslogLevel rval)
		{
			return NativeConvert.ToSyslogLevel(value, out rval) == 0;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00008ECC File Offset: 0x000070CC
		public static SyslogLevel ToSyslogLevel(int value)
		{
			SyslogLevel syslogLevel;
			if (NativeConvert.ToSyslogLevel(value, out syslogLevel) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return syslogLevel;
		}

		// Token: 0x06000308 RID: 776
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSyslogOptions")]
		private static extern int FromSyslogOptions(SyslogOptions value, out int rval);

		// Token: 0x06000309 RID: 777 RVA: 0x00008EF0 File Offset: 0x000070F0
		public static bool TryFromSyslogOptions(SyslogOptions value, out int rval)
		{
			return NativeConvert.FromSyslogOptions(value, out rval) == 0;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008EFC File Offset: 0x000070FC
		public static int FromSyslogOptions(SyslogOptions value)
		{
			int num;
			if (NativeConvert.FromSyslogOptions(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600030B RID: 779
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSyslogOptions")]
		private static extern int ToSyslogOptions(int value, out SyslogOptions rval);

		// Token: 0x0600030C RID: 780 RVA: 0x00008F20 File Offset: 0x00007120
		public static bool TryToSyslogOptions(int value, out SyslogOptions rval)
		{
			return NativeConvert.ToSyslogOptions(value, out rval) == 0;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00008F2C File Offset: 0x0000712C
		public static SyslogOptions ToSyslogOptions(int value)
		{
			SyslogOptions syslogOptions;
			if (NativeConvert.ToSyslogOptions(value, out syslogOptions) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return syslogOptions;
		}

		// Token: 0x0600030E RID: 782
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromTimespec")]
		private static extern int FromTimespec(ref Timespec source, IntPtr destination);

		// Token: 0x0600030F RID: 783 RVA: 0x00008F50 File Offset: 0x00007150
		public static bool TryCopy(ref Timespec source, IntPtr destination)
		{
			return NativeConvert.FromTimespec(ref source, destination) == 0;
		}

		// Token: 0x06000310 RID: 784
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToTimespec")]
		private static extern int ToTimespec(IntPtr source, out Timespec destination);

		// Token: 0x06000311 RID: 785 RVA: 0x00008F5C File Offset: 0x0000715C
		public static bool TryCopy(IntPtr source, out Timespec destination)
		{
			return NativeConvert.ToTimespec(source, out destination) == 0;
		}

		// Token: 0x06000312 RID: 786
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromTimeval")]
		private static extern int FromTimeval(ref Timeval source, IntPtr destination);

		// Token: 0x06000313 RID: 787 RVA: 0x00008F68 File Offset: 0x00007168
		public static bool TryCopy(ref Timeval source, IntPtr destination)
		{
			return NativeConvert.FromTimeval(ref source, destination) == 0;
		}

		// Token: 0x06000314 RID: 788
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToTimeval")]
		private static extern int ToTimeval(IntPtr source, out Timeval destination);

		// Token: 0x06000315 RID: 789 RVA: 0x00008F74 File Offset: 0x00007174
		public static bool TryCopy(IntPtr source, out Timeval destination)
		{
			return NativeConvert.ToTimeval(source, out destination) == 0;
		}

		// Token: 0x06000316 RID: 790
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromTimezone")]
		private static extern int FromTimezone(ref Timezone source, IntPtr destination);

		// Token: 0x06000317 RID: 791 RVA: 0x00008F80 File Offset: 0x00007180
		public static bool TryCopy(ref Timezone source, IntPtr destination)
		{
			return NativeConvert.FromTimezone(ref source, destination) == 0;
		}

		// Token: 0x06000318 RID: 792
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToTimezone")]
		private static extern int ToTimezone(IntPtr source, out Timezone destination);

		// Token: 0x06000319 RID: 793 RVA: 0x00008F8C File Offset: 0x0000718C
		public static bool TryCopy(IntPtr source, out Timezone destination)
		{
			return NativeConvert.ToTimezone(source, out destination) == 0;
		}

		// Token: 0x0600031A RID: 794
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromUnixAddressFamily")]
		private static extern int FromUnixAddressFamily(UnixAddressFamily value, out int rval);

		// Token: 0x0600031B RID: 795 RVA: 0x00008F98 File Offset: 0x00007198
		public static bool TryFromUnixAddressFamily(UnixAddressFamily value, out int rval)
		{
			return NativeConvert.FromUnixAddressFamily(value, out rval) == 0;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00008FA4 File Offset: 0x000071A4
		public static int FromUnixAddressFamily(UnixAddressFamily value)
		{
			int num;
			if (NativeConvert.FromUnixAddressFamily(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600031D RID: 797
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToUnixAddressFamily")]
		private static extern int ToUnixAddressFamily(int value, out UnixAddressFamily rval);

		// Token: 0x0600031E RID: 798 RVA: 0x00008FC8 File Offset: 0x000071C8
		public static bool TryToUnixAddressFamily(int value, out UnixAddressFamily rval)
		{
			return NativeConvert.ToUnixAddressFamily(value, out rval) == 0;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00008FD4 File Offset: 0x000071D4
		public static UnixAddressFamily ToUnixAddressFamily(int value)
		{
			UnixAddressFamily unixAddressFamily;
			if (NativeConvert.ToUnixAddressFamily(value, out unixAddressFamily) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return unixAddressFamily;
		}

		// Token: 0x06000320 RID: 800
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromUnixSocketControlMessage")]
		private static extern int FromUnixSocketControlMessage(UnixSocketControlMessage value, out int rval);

		// Token: 0x06000321 RID: 801 RVA: 0x00008FF8 File Offset: 0x000071F8
		public static bool TryFromUnixSocketControlMessage(UnixSocketControlMessage value, out int rval)
		{
			return NativeConvert.FromUnixSocketControlMessage(value, out rval) == 0;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00009004 File Offset: 0x00007204
		public static int FromUnixSocketControlMessage(UnixSocketControlMessage value)
		{
			int num;
			if (NativeConvert.FromUnixSocketControlMessage(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000323 RID: 803
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToUnixSocketControlMessage")]
		private static extern int ToUnixSocketControlMessage(int value, out UnixSocketControlMessage rval);

		// Token: 0x06000324 RID: 804 RVA: 0x00009028 File Offset: 0x00007228
		public static bool TryToUnixSocketControlMessage(int value, out UnixSocketControlMessage rval)
		{
			return NativeConvert.ToUnixSocketControlMessage(value, out rval) == 0;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00009034 File Offset: 0x00007234
		public static UnixSocketControlMessage ToUnixSocketControlMessage(int value)
		{
			UnixSocketControlMessage unixSocketControlMessage;
			if (NativeConvert.ToUnixSocketControlMessage(value, out unixSocketControlMessage) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return unixSocketControlMessage;
		}

		// Token: 0x06000326 RID: 806
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromUnixSocketFlags")]
		private static extern int FromUnixSocketFlags(UnixSocketFlags value, out int rval);

		// Token: 0x06000327 RID: 807 RVA: 0x00009058 File Offset: 0x00007258
		public static bool TryFromUnixSocketFlags(UnixSocketFlags value, out int rval)
		{
			return NativeConvert.FromUnixSocketFlags(value, out rval) == 0;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00009064 File Offset: 0x00007264
		public static int FromUnixSocketFlags(UnixSocketFlags value)
		{
			int num;
			if (NativeConvert.FromUnixSocketFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000329 RID: 809
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToUnixSocketFlags")]
		private static extern int ToUnixSocketFlags(int value, out UnixSocketFlags rval);

		// Token: 0x0600032A RID: 810 RVA: 0x00009088 File Offset: 0x00007288
		public static bool TryToUnixSocketFlags(int value, out UnixSocketFlags rval)
		{
			return NativeConvert.ToUnixSocketFlags(value, out rval) == 0;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00009094 File Offset: 0x00007294
		public static UnixSocketFlags ToUnixSocketFlags(int value)
		{
			UnixSocketFlags unixSocketFlags;
			if (NativeConvert.ToUnixSocketFlags(value, out unixSocketFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return unixSocketFlags;
		}

		// Token: 0x0600032C RID: 812
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromUnixSocketOptionName")]
		private static extern int FromUnixSocketOptionName(UnixSocketOptionName value, out int rval);

		// Token: 0x0600032D RID: 813 RVA: 0x000090B8 File Offset: 0x000072B8
		public static bool TryFromUnixSocketOptionName(UnixSocketOptionName value, out int rval)
		{
			return NativeConvert.FromUnixSocketOptionName(value, out rval) == 0;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000090C4 File Offset: 0x000072C4
		public static int FromUnixSocketOptionName(UnixSocketOptionName value)
		{
			int num;
			if (NativeConvert.FromUnixSocketOptionName(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600032F RID: 815
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToUnixSocketOptionName")]
		private static extern int ToUnixSocketOptionName(int value, out UnixSocketOptionName rval);

		// Token: 0x06000330 RID: 816 RVA: 0x000090E8 File Offset: 0x000072E8
		public static bool TryToUnixSocketOptionName(int value, out UnixSocketOptionName rval)
		{
			return NativeConvert.ToUnixSocketOptionName(value, out rval) == 0;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000090F4 File Offset: 0x000072F4
		public static UnixSocketOptionName ToUnixSocketOptionName(int value)
		{
			UnixSocketOptionName unixSocketOptionName;
			if (NativeConvert.ToUnixSocketOptionName(value, out unixSocketOptionName) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return unixSocketOptionName;
		}

		// Token: 0x06000332 RID: 818
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromUnixSocketProtocol")]
		private static extern int FromUnixSocketProtocol(UnixSocketProtocol value, out int rval);

		// Token: 0x06000333 RID: 819 RVA: 0x00009118 File Offset: 0x00007318
		public static bool TryFromUnixSocketProtocol(UnixSocketProtocol value, out int rval)
		{
			return NativeConvert.FromUnixSocketProtocol(value, out rval) == 0;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009124 File Offset: 0x00007324
		public static int FromUnixSocketProtocol(UnixSocketProtocol value)
		{
			int num;
			if (NativeConvert.FromUnixSocketProtocol(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000335 RID: 821
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToUnixSocketProtocol")]
		private static extern int ToUnixSocketProtocol(int value, out UnixSocketProtocol rval);

		// Token: 0x06000336 RID: 822 RVA: 0x00009148 File Offset: 0x00007348
		public static bool TryToUnixSocketProtocol(int value, out UnixSocketProtocol rval)
		{
			return NativeConvert.ToUnixSocketProtocol(value, out rval) == 0;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009154 File Offset: 0x00007354
		public static UnixSocketProtocol ToUnixSocketProtocol(int value)
		{
			UnixSocketProtocol unixSocketProtocol;
			if (NativeConvert.ToUnixSocketProtocol(value, out unixSocketProtocol) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return unixSocketProtocol;
		}

		// Token: 0x06000338 RID: 824
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromUnixSocketType")]
		private static extern int FromUnixSocketType(UnixSocketType value, out int rval);

		// Token: 0x06000339 RID: 825 RVA: 0x00009178 File Offset: 0x00007378
		public static bool TryFromUnixSocketType(UnixSocketType value, out int rval)
		{
			return NativeConvert.FromUnixSocketType(value, out rval) == 0;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009184 File Offset: 0x00007384
		public static int FromUnixSocketType(UnixSocketType value)
		{
			int num;
			if (NativeConvert.FromUnixSocketType(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600033B RID: 827
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToUnixSocketType")]
		private static extern int ToUnixSocketType(int value, out UnixSocketType rval);

		// Token: 0x0600033C RID: 828 RVA: 0x000091A8 File Offset: 0x000073A8
		public static bool TryToUnixSocketType(int value, out UnixSocketType rval)
		{
			return NativeConvert.ToUnixSocketType(value, out rval) == 0;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000091B4 File Offset: 0x000073B4
		public static UnixSocketType ToUnixSocketType(int value)
		{
			UnixSocketType unixSocketType;
			if (NativeConvert.ToUnixSocketType(value, out unixSocketType) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return unixSocketType;
		}

		// Token: 0x0600033E RID: 830
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromUtimbuf")]
		private static extern int FromUtimbuf(ref Utimbuf source, IntPtr destination);

		// Token: 0x0600033F RID: 831 RVA: 0x000091D8 File Offset: 0x000073D8
		public static bool TryCopy(ref Utimbuf source, IntPtr destination)
		{
			return NativeConvert.FromUtimbuf(ref source, destination) == 0;
		}

		// Token: 0x06000340 RID: 832
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToUtimbuf")]
		private static extern int ToUtimbuf(IntPtr source, out Utimbuf destination);

		// Token: 0x06000341 RID: 833 RVA: 0x000091E4 File Offset: 0x000073E4
		public static bool TryCopy(IntPtr source, out Utimbuf destination)
		{
			return NativeConvert.ToUtimbuf(source, out destination) == 0;
		}

		// Token: 0x06000342 RID: 834
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromWaitOptions")]
		private static extern int FromWaitOptions(WaitOptions value, out int rval);

		// Token: 0x06000343 RID: 835 RVA: 0x000091F0 File Offset: 0x000073F0
		public static bool TryFromWaitOptions(WaitOptions value, out int rval)
		{
			return NativeConvert.FromWaitOptions(value, out rval) == 0;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000091FC File Offset: 0x000073FC
		public static int FromWaitOptions(WaitOptions value)
		{
			int num;
			if (NativeConvert.FromWaitOptions(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000345 RID: 837
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToWaitOptions")]
		private static extern int ToWaitOptions(int value, out WaitOptions rval);

		// Token: 0x06000346 RID: 838 RVA: 0x00009220 File Offset: 0x00007420
		public static bool TryToWaitOptions(int value, out WaitOptions rval)
		{
			return NativeConvert.ToWaitOptions(value, out rval) == 0;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000922C File Offset: 0x0000742C
		public static WaitOptions ToWaitOptions(int value)
		{
			WaitOptions waitOptions;
			if (NativeConvert.ToWaitOptions(value, out waitOptions) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return waitOptions;
		}

		// Token: 0x06000348 RID: 840
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromXattrFlags")]
		private static extern int FromXattrFlags(XattrFlags value, out int rval);

		// Token: 0x06000349 RID: 841 RVA: 0x00009250 File Offset: 0x00007450
		public static bool TryFromXattrFlags(XattrFlags value, out int rval)
		{
			return NativeConvert.FromXattrFlags(value, out rval) == 0;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000925C File Offset: 0x0000745C
		public static int FromXattrFlags(XattrFlags value)
		{
			int num;
			if (NativeConvert.FromXattrFlags(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x0600034B RID: 843
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToXattrFlags")]
		private static extern int ToXattrFlags(int value, out XattrFlags rval);

		// Token: 0x0600034C RID: 844 RVA: 0x00009280 File Offset: 0x00007480
		public static bool TryToXattrFlags(int value, out XattrFlags rval)
		{
			return NativeConvert.ToXattrFlags(value, out rval) == 0;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000928C File Offset: 0x0000748C
		public static XattrFlags ToXattrFlags(int value)
		{
			XattrFlags xattrFlags;
			if (NativeConvert.ToXattrFlags(value, out xattrFlags) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return xattrFlags;
		}

		// Token: 0x0400009A RID: 154
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0400009B RID: 155
		public static readonly DateTime LocalUnixEpoch = new DateTime(1970, 1, 1);

		// Token: 0x0400009C RID: 156
		public static readonly TimeSpan LocalUtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.UtcNow);

		// Token: 0x0400009D RID: 157
		private static readonly string[][] fopen_modes = new string[][]
		{
			new string[] { "Can't Read+Create", "wb", "w+b" },
			new string[] { "Can't Read+Create", "wb", "w+b" },
			new string[] { "rb", "wb", "r+b" },
			new string[] { "rb", "wb", "r+b" },
			new string[] { "Cannot Truncate and Read", "wb", "w+b" },
			new string[] { "Cannot Append and Read", "ab", "a+b" }
		};

		// Token: 0x0400009E RID: 158
		private const string LIB = "MonoPosixHelper";
	}
}
