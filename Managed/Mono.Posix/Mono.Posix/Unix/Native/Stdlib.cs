using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x0200002D RID: 45
	public class Stdlib
	{
		// Token: 0x0600036A RID: 874
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Unix_VersionString")]
		private static extern IntPtr VersionStringPtr();

		// Token: 0x0600036B RID: 875 RVA: 0x00009718 File Offset: 0x00007918
		internal static void VersionCheck()
		{
			if (Stdlib.versionCheckPerformed)
			{
				return;
			}
			string text = "MonoProject-2015-12-1";
			string text2 = Marshal.PtrToStringAnsi(Stdlib.VersionStringPtr());
			if (text != text2)
			{
				throw new Exception(string.Concat(new string[] { "Mono.Posix assembly loaded with a different version (\"", text, "\") than MonoPosixHelper (\"", text2, "\"). You may need to reinstall Mono.Posix." }));
			}
			Stdlib.versionCheckPerformed = true;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009780 File Offset: 0x00007980
		static Stdlib()
		{
			Stdlib.VersionCheck();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000989D File Offset: 0x00007A9D
		internal Stdlib()
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000098A8 File Offset: 0x00007AA8
		public static Errno GetLastError()
		{
			int num = Marshal.GetLastWin32Error();
			if (Environment.OSVersion.Platform != PlatformID.Unix)
			{
				num = Stdlib._GetLastError();
			}
			return NativeConvert.ToErrno(num);
		}

		// Token: 0x0600036F RID: 879
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_GetLastError")]
		private static extern int _GetLastError();

		// Token: 0x06000370 RID: 880
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SetLastError")]
		private static extern void SetLastError(int error);

		// Token: 0x06000371 RID: 881 RVA: 0x000098D4 File Offset: 0x00007AD4
		protected static void SetLastError(Errno error)
		{
			Stdlib.SetLastError(NativeConvert.FromErrno(error));
		}

		// Token: 0x06000372 RID: 882
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_InvokeSignalHandler")]
		internal static extern void InvokeSignalHandler(int signum, IntPtr handler);

		// Token: 0x06000373 RID: 883
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SIG_DFL")]
		private static extern IntPtr GetDefaultSignal();

		// Token: 0x06000374 RID: 884
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SIG_ERR")]
		private static extern IntPtr GetErrorSignal();

		// Token: 0x06000375 RID: 885
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SIG_IGN")]
		private static extern IntPtr GetIgnoreSignal();

		// Token: 0x06000376 RID: 886 RVA: 0x000098E1 File Offset: 0x00007AE1
		private static void _ErrorHandler(int signum)
		{
			Console.Error.WriteLine("Error handler invoked for signum " + signum + ".  Don't do that.");
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00009902 File Offset: 0x00007B02
		private static void _DefaultHandler(int signum)
		{
			Console.Error.WriteLine("Default handler invoked for signum " + signum + ".  Don't do that.");
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009923 File Offset: 0x00007B23
		private static void _IgnoreHandler(int signum)
		{
			Console.Error.WriteLine("Ignore handler invoked for signum " + signum + ".  Don't do that.");
		}

		// Token: 0x06000379 RID: 889
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "signal", SetLastError = true)]
		private static extern IntPtr sys_signal(int signum, SignalHandler handler);

		// Token: 0x0600037A RID: 890
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "signal", SetLastError = true)]
		private static extern IntPtr sys_signal(int signum, IntPtr handler);

		// Token: 0x0600037B RID: 891 RVA: 0x00009944 File Offset: 0x00007B44
		[CLSCompliant(false)]
		[Obsolete("This is not safe; use Mono.Unix.UnixSignal for signal delivery or SetSignalAction()")]
		public static SignalHandler signal(Signum signum, SignalHandler handler)
		{
			int num = NativeConvert.FromSignum(signum);
			Delegate[] invocationList = handler.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				Marshal.Prelink(invocationList[i].Method);
			}
			IntPtr intPtr;
			if (handler == Stdlib.SIG_DFL)
			{
				intPtr = Stdlib.sys_signal(num, Stdlib._SIG_DFL);
			}
			else if (handler == Stdlib.SIG_ERR)
			{
				intPtr = Stdlib.sys_signal(num, Stdlib._SIG_ERR);
			}
			else if (handler == Stdlib.SIG_IGN)
			{
				intPtr = Stdlib.sys_signal(num, Stdlib._SIG_IGN);
			}
			else
			{
				intPtr = Stdlib.sys_signal(num, handler);
			}
			return Stdlib.TranslateHandler(intPtr);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000099DC File Offset: 0x00007BDC
		private static SignalHandler TranslateHandler(IntPtr handler)
		{
			if (handler == Stdlib._SIG_DFL)
			{
				return Stdlib.SIG_DFL;
			}
			if (handler == Stdlib._SIG_ERR)
			{
				return Stdlib.SIG_ERR;
			}
			if (handler == Stdlib._SIG_IGN)
			{
				return Stdlib.SIG_IGN;
			}
			return (SignalHandler)Marshal.GetDelegateForFunctionPointer(handler, typeof(SignalHandler));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00009A37 File Offset: 0x00007C37
		public static int SetSignalAction(Signum signal, SignalAction action)
		{
			return Stdlib.SetSignalAction(NativeConvert.FromSignum(signal), action);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00009A45 File Offset: 0x00007C45
		public static int SetSignalAction(RealTimeSignum rts, SignalAction action)
		{
			return Stdlib.SetSignalAction(NativeConvert.FromRealTimeSignum(rts), action);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00009A54 File Offset: 0x00007C54
		private static int SetSignalAction(int signum, SignalAction action)
		{
			IntPtr intPtr = IntPtr.Zero;
			switch (action)
			{
			case SignalAction.Default:
				intPtr = Stdlib._SIG_DFL;
				break;
			case SignalAction.Ignore:
				intPtr = Stdlib._SIG_IGN;
				break;
			case SignalAction.Error:
				intPtr = Stdlib._SIG_ERR;
				break;
			default:
				throw new ArgumentException("Invalid action value.", "action");
			}
			if (Stdlib.sys_signal(signum, intPtr) == Stdlib._SIG_ERR)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000380 RID: 896
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "raise")]
		private static extern int sys_raise(int sig);

		// Token: 0x06000381 RID: 897 RVA: 0x00009AB9 File Offset: 0x00007CB9
		[CLSCompliant(false)]
		public static int raise(Signum sig)
		{
			return Stdlib.sys_raise(NativeConvert.FromSignum(sig));
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00009AC6 File Offset: 0x00007CC6
		public static int raise(RealTimeSignum rts)
		{
			return Stdlib.sys_raise(NativeConvert.FromRealTimeSignum(rts));
		}

		// Token: 0x06000383 RID: 899
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib__IOFBF")]
		private static extern int GetFullyBuffered();

		// Token: 0x06000384 RID: 900
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib__IOLBF")]
		private static extern int GetLineBuffered();

		// Token: 0x06000385 RID: 901
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib__IONBF")]
		private static extern int GetNonBuffered();

		// Token: 0x06000386 RID: 902
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_BUFSIZ")]
		private static extern int GetBufferSize();

		// Token: 0x06000387 RID: 903
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_CreateFilePosition")]
		internal static extern IntPtr CreateFilePosition();

		// Token: 0x06000388 RID: 904
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_DumpFilePosition")]
		internal static extern int DumpFilePosition(StringBuilder buf, HandleRef handle, int len);

		// Token: 0x06000389 RID: 905
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_EOF")]
		private static extern int GetEOF();

		// Token: 0x0600038A RID: 906
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_FILENAME_MAX")]
		private static extern int GetFilenameMax();

		// Token: 0x0600038B RID: 907
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_FOPEN_MAX")]
		private static extern int GetFopenMax();

		// Token: 0x0600038C RID: 908
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_L_tmpnam")]
		private static extern int GetTmpnamLength();

		// Token: 0x0600038D RID: 909
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_stdin")]
		private static extern IntPtr GetStandardInput();

		// Token: 0x0600038E RID: 910
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_stdout")]
		private static extern IntPtr GetStandardOutput();

		// Token: 0x0600038F RID: 911
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_stderr")]
		private static extern IntPtr GetStandardError();

		// Token: 0x06000390 RID: 912
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_TMP_MAX")]
		private static extern int GetTmpMax();

		// Token: 0x06000391 RID: 913
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int remove([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename);

		// Token: 0x06000392 RID: 914
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int rename([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string newpath);

		// Token: 0x06000393 RID: 915
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_tmpfile", SetLastError = true)]
		public static extern IntPtr tmpfile();

		// Token: 0x06000394 RID: 916
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tmpnam", SetLastError = true)]
		private static extern IntPtr sys_tmpnam(StringBuilder s);

		// Token: 0x06000395 RID: 917 RVA: 0x00009AD4 File Offset: 0x00007CD4
		[Obsolete("Syscall.mkstemp() should be preferred.")]
		public static string tmpnam(StringBuilder s)
		{
			if (s != null && s.Capacity < Stdlib.L_tmpnam)
			{
				throw new ArgumentOutOfRangeException("s", "s.Capacity < L_tmpnam");
			}
			object obj = Stdlib.tmpnam_lock;
			string text;
			lock (obj)
			{
				text = UnixMarshal.PtrToString(Stdlib.sys_tmpnam(s));
			}
			return text;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009B3C File Offset: 0x00007D3C
		[Obsolete("Syscall.mkstemp() should be preferred.")]
		public static string tmpnam()
		{
			object obj = Stdlib.tmpnam_lock;
			string text;
			lock (obj)
			{
				text = UnixMarshal.PtrToString(Stdlib.sys_tmpnam(null));
			}
			return text;
		}

		// Token: 0x06000397 RID: 919
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fclose", SetLastError = true)]
		public static extern int fclose(IntPtr stream);

		// Token: 0x06000398 RID: 920
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fflush", SetLastError = true)]
		public static extern int fflush(IntPtr stream);

		// Token: 0x06000399 RID: 921
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fopen", SetLastError = true)]
		public static extern IntPtr fopen([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string mode);

		// Token: 0x0600039A RID: 922
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_freopen", SetLastError = true)]
		public static extern IntPtr freopen([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string mode, IntPtr stream);

		// Token: 0x0600039B RID: 923
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_setbuf", SetLastError = true)]
		public static extern int setbuf(IntPtr stream, IntPtr buf);

		// Token: 0x0600039C RID: 924 RVA: 0x00009B84 File Offset: 0x00007D84
		[CLSCompliant(false)]
		public unsafe static int setbuf(IntPtr stream, byte* buf)
		{
			return Stdlib.setbuf(stream, (IntPtr)((void*)buf));
		}

		// Token: 0x0600039D RID: 925
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_setvbuf", SetLastError = true)]
		public static extern int setvbuf(IntPtr stream, IntPtr buf, int mode, ulong size);

		// Token: 0x0600039E RID: 926 RVA: 0x00009B92 File Offset: 0x00007D92
		[CLSCompliant(false)]
		public unsafe static int setvbuf(IntPtr stream, byte* buf, int mode, ulong size)
		{
			return Stdlib.setvbuf(stream, (IntPtr)((void*)buf), mode, size);
		}

		// Token: 0x0600039F RID: 927
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fprintf")]
		private static extern int sys_fprintf(IntPtr stream, string format, string message);

		// Token: 0x060003A0 RID: 928 RVA: 0x00009BA2 File Offset: 0x00007DA2
		public static int fprintf(IntPtr stream, string message)
		{
			return Stdlib.sys_fprintf(stream, "%s", message);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00009BB0 File Offset: 0x00007DB0
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse fprintf (IntPtr, string) instead.")]
		public static int fprintf(IntPtr stream, string format, params object[] parameters)
		{
			object[] array = new object[checked(parameters.Length + 2)];
			array[0] = stream;
			array[1] = format;
			Array.Copy(parameters, 0, array, 2, parameters.Length);
			return (int)XPrintfFunctions.fprintf(array);
		}

		// Token: 0x060003A2 RID: 930
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "printf")]
		private static extern int sys_printf(string format, string message);

		// Token: 0x060003A3 RID: 931 RVA: 0x00009BF1 File Offset: 0x00007DF1
		public static int printf(string message)
		{
			return Stdlib.sys_printf("%s", message);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00009C00 File Offset: 0x00007E00
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse printf (string) instead.")]
		public static int printf(string format, params object[] parameters)
		{
			object[] array = new object[checked(parameters.Length + 1)];
			array[0] = format;
			Array.Copy(parameters, 0, array, 1, parameters.Length);
			return (int)XPrintfFunctions.printf(array);
		}

		// Token: 0x060003A5 RID: 933
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_snprintf")]
		private static extern int sys_snprintf(StringBuilder s, ulong n, string format, string message);

		// Token: 0x060003A6 RID: 934 RVA: 0x00009C38 File Offset: 0x00007E38
		[CLSCompliant(false)]
		public static int snprintf(StringBuilder s, ulong n, string message)
		{
			if (n > (ulong)((long)s.Capacity))
			{
				throw new ArgumentOutOfRangeException("n", "n must be <= s.Capacity");
			}
			return Stdlib.sys_snprintf(s, n, "%s", message);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00009C61 File Offset: 0x00007E61
		public static int snprintf(StringBuilder s, string message)
		{
			return Stdlib.sys_snprintf(s, (ulong)((long)s.Capacity), "%s", message);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00009C78 File Offset: 0x00007E78
		[CLSCompliant(false)]
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse snprintf (StringBuilder, string) instead.")]
		public static int snprintf(StringBuilder s, ulong n, string format, params object[] parameters)
		{
			if (n > (ulong)((long)s.Capacity))
			{
				throw new ArgumentOutOfRangeException("n", "n must be <= s.Capacity");
			}
			object[] array = new object[checked(parameters.Length + 3)];
			array[0] = s;
			array[1] = n;
			array[2] = format;
			Array.Copy(parameters, 0, array, 3, parameters.Length);
			return (int)XPrintfFunctions.snprintf(array);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00009CD8 File Offset: 0x00007ED8
		[CLSCompliant(false)]
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse snprintf (StringBuilder, string) instead.")]
		public static int snprintf(StringBuilder s, string format, params object[] parameters)
		{
			object[] array = new object[checked(parameters.Length + 3)];
			array[0] = s;
			array[1] = (ulong)((long)s.Capacity);
			array[2] = format;
			Array.Copy(parameters, 0, array, 3, parameters.Length);
			return (int)XPrintfFunctions.snprintf(array);
		}

		// Token: 0x060003AA RID: 938
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fgetc", SetLastError = true)]
		public static extern int fgetc(IntPtr stream);

		// Token: 0x060003AB RID: 939
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fgets", SetLastError = true)]
		private static extern IntPtr sys_fgets(StringBuilder sb, int size, IntPtr stream);

		// Token: 0x060003AC RID: 940 RVA: 0x00009D23 File Offset: 0x00007F23
		public static StringBuilder fgets(StringBuilder sb, int size, IntPtr stream)
		{
			if (Stdlib.sys_fgets(sb, size, stream) == IntPtr.Zero)
			{
				return null;
			}
			return sb;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00009D3C File Offset: 0x00007F3C
		public static StringBuilder fgets(StringBuilder sb, IntPtr stream)
		{
			return Stdlib.fgets(sb, sb.Capacity, stream);
		}

		// Token: 0x060003AE RID: 942
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fputc", SetLastError = true)]
		public static extern int fputc(int c, IntPtr stream);

		// Token: 0x060003AF RID: 943
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fputs", SetLastError = true)]
		public static extern int fputs(string s, IntPtr stream);

		// Token: 0x060003B0 RID: 944 RVA: 0x00009D4B File Offset: 0x00007F4B
		public static int getc(IntPtr stream)
		{
			return Stdlib.fgetc(stream);
		}

		// Token: 0x060003B1 RID: 945
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int getchar();

		// Token: 0x060003B2 RID: 946 RVA: 0x00009D53 File Offset: 0x00007F53
		public static int putc(int c, IntPtr stream)
		{
			return Stdlib.fputc(c, stream);
		}

		// Token: 0x060003B3 RID: 947
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int putchar(int c);

		// Token: 0x060003B4 RID: 948
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int puts(string s);

		// Token: 0x060003B5 RID: 949
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_ungetc", SetLastError = true)]
		public static extern int ungetc(int c, IntPtr stream);

		// Token: 0x060003B6 RID: 950
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fread", SetLastError = true)]
		public static extern ulong fread(IntPtr ptr, ulong size, ulong nmemb, IntPtr stream);

		// Token: 0x060003B7 RID: 951 RVA: 0x00009D5C File Offset: 0x00007F5C
		[CLSCompliant(false)]
		public unsafe static ulong fread(void* ptr, ulong size, ulong nmemb, IntPtr stream)
		{
			return Stdlib.fread((IntPtr)ptr, size, nmemb, stream);
		}

		// Token: 0x060003B8 RID: 952
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fread", SetLastError = true)]
		private static extern ulong sys_fread([Out] byte[] ptr, ulong size, ulong nmemb, IntPtr stream);

		// Token: 0x060003B9 RID: 953 RVA: 0x00009D6C File Offset: 0x00007F6C
		[CLSCompliant(false)]
		public static ulong fread(byte[] ptr, ulong size, ulong nmemb, IntPtr stream)
		{
			if (size * nmemb > (ulong)((long)ptr.Length))
			{
				throw new ArgumentOutOfRangeException("nmemb");
			}
			return Stdlib.sys_fread(ptr, size, nmemb, stream);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00009D8B File Offset: 0x00007F8B
		[CLSCompliant(false)]
		public static ulong fread(byte[] ptr, IntPtr stream)
		{
			return Stdlib.fread(ptr, 1UL, (ulong)((long)ptr.Length), stream);
		}

		// Token: 0x060003BB RID: 955
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fwrite", SetLastError = true)]
		public static extern ulong fwrite(IntPtr ptr, ulong size, ulong nmemb, IntPtr stream);

		// Token: 0x060003BC RID: 956 RVA: 0x00009D9A File Offset: 0x00007F9A
		[CLSCompliant(false)]
		public unsafe static ulong fwrite(void* ptr, ulong size, ulong nmemb, IntPtr stream)
		{
			return Stdlib.fwrite((IntPtr)ptr, size, nmemb, stream);
		}

		// Token: 0x060003BD RID: 957
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fwrite", SetLastError = true)]
		private static extern ulong sys_fwrite(byte[] ptr, ulong size, ulong nmemb, IntPtr stream);

		// Token: 0x060003BE RID: 958 RVA: 0x00009DAA File Offset: 0x00007FAA
		[CLSCompliant(false)]
		public static ulong fwrite(byte[] ptr, ulong size, ulong nmemb, IntPtr stream)
		{
			if (size * nmemb > (ulong)((long)ptr.Length))
			{
				throw new ArgumentOutOfRangeException("nmemb");
			}
			return Stdlib.sys_fwrite(ptr, size, nmemb, stream);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00009DC9 File Offset: 0x00007FC9
		[CLSCompliant(false)]
		public static ulong fwrite(byte[] ptr, IntPtr stream)
		{
			return Stdlib.fwrite(ptr, 1UL, (ulong)((long)ptr.Length), stream);
		}

		// Token: 0x060003C0 RID: 960
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fgetpos", SetLastError = true)]
		private static extern int sys_fgetpos(IntPtr stream, HandleRef pos);

		// Token: 0x060003C1 RID: 961 RVA: 0x00009DD8 File Offset: 0x00007FD8
		public static int fgetpos(IntPtr stream, FilePosition pos)
		{
			return Stdlib.sys_fgetpos(stream, pos.Handle);
		}

		// Token: 0x060003C2 RID: 962
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fseek", SetLastError = true)]
		private static extern int sys_fseek(IntPtr stream, long offset, int origin);

		// Token: 0x060003C3 RID: 963 RVA: 0x00009DE8 File Offset: 0x00007FE8
		[CLSCompliant(false)]
		public static int fseek(IntPtr stream, long offset, SeekFlags origin)
		{
			int num = (int)NativeConvert.FromSeekFlags(origin);
			return Stdlib.sys_fseek(stream, offset, num);
		}

		// Token: 0x060003C4 RID: 964
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fsetpos", SetLastError = true)]
		private static extern int sys_fsetpos(IntPtr stream, HandleRef pos);

		// Token: 0x060003C5 RID: 965 RVA: 0x00009E04 File Offset: 0x00008004
		public static int fsetpos(IntPtr stream, FilePosition pos)
		{
			return Stdlib.sys_fsetpos(stream, pos.Handle);
		}

		// Token: 0x060003C6 RID: 966
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_ftell", SetLastError = true)]
		public static extern long ftell(IntPtr stream);

		// Token: 0x060003C7 RID: 967
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_rewind", SetLastError = true)]
		public static extern int rewind(IntPtr stream);

		// Token: 0x060003C8 RID: 968
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_clearerr", SetLastError = true)]
		public static extern int clearerr(IntPtr stream);

		// Token: 0x060003C9 RID: 969
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_feof", SetLastError = true)]
		public static extern int feof(IntPtr stream);

		// Token: 0x060003CA RID: 970
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_ferror", SetLastError = true)]
		public static extern int ferror(IntPtr stream);

		// Token: 0x060003CB RID: 971
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_perror", SetLastError = true)]
		private static extern int perror(string s, int err);

		// Token: 0x060003CC RID: 972 RVA: 0x00009E12 File Offset: 0x00008012
		public static int perror(string s)
		{
			return Stdlib.perror(s, Marshal.GetLastWin32Error());
		}

		// Token: 0x060003CD RID: 973
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_EXIT_FAILURE")]
		private static extern int GetExitFailure();

		// Token: 0x060003CE RID: 974
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_EXIT_SUCCESS")]
		private static extern int GetExitSuccess();

		// Token: 0x060003CF RID: 975
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_MB_CUR_MAX")]
		private static extern int GetMbCurMax();

		// Token: 0x060003D0 RID: 976
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_RAND_MAX")]
		private static extern int GetRandMax();

		// Token: 0x060003D1 RID: 977
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern int rand();

		// Token: 0x060003D2 RID: 978
		[CLSCompliant(false)]
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern void srand(uint seed);

		// Token: 0x060003D3 RID: 979
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_calloc", SetLastError = true)]
		public static extern IntPtr calloc(ulong nmemb, ulong size);

		// Token: 0x060003D4 RID: 980
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_free")]
		public static extern void free(IntPtr ptr);

		// Token: 0x060003D5 RID: 981
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_malloc", SetLastError = true)]
		public static extern IntPtr malloc(ulong size);

		// Token: 0x060003D6 RID: 982
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_realloc", SetLastError = true)]
		public static extern IntPtr realloc(IntPtr ptr, ulong size);

		// Token: 0x060003D7 RID: 983
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern void abort();

		// Token: 0x060003D8 RID: 984
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern void exit(int status);

		// Token: 0x060003D9 RID: 985
		[CLSCompliant(false)]
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern void _Exit(int status);

		// Token: 0x060003DA RID: 986
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "getenv")]
		private static extern IntPtr sys_getenv(string name);

		// Token: 0x060003DB RID: 987 RVA: 0x00009E1F File Offset: 0x0000801F
		public static string getenv(string name)
		{
			return UnixMarshal.PtrToString(Stdlib.sys_getenv(name));
		}

		// Token: 0x060003DC RID: 988
		[CLSCompliant(false)]
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int system(string @string);

		// Token: 0x060003DD RID: 989
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "strerror", SetLastError = true)]
		private static extern IntPtr sys_strerror(int errnum);

		// Token: 0x060003DE RID: 990 RVA: 0x00009E2C File Offset: 0x0000802C
		[CLSCompliant(false)]
		public static string strerror(Errno errnum)
		{
			int num = NativeConvert.FromErrno(errnum);
			object obj = Stdlib.strerror_lock;
			string text;
			lock (obj)
			{
				text = UnixMarshal.PtrToString(Stdlib.sys_strerror(num));
			}
			return text;
		}

		// Token: 0x060003DF RID: 991
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_strlen", SetLastError = true)]
		public static extern ulong strlen(IntPtr s);

		// Token: 0x04000142 RID: 322
		internal const string LIBC = "msvcrt";

		// Token: 0x04000143 RID: 323
		internal const string MPH = "MonoPosixHelper";

		// Token: 0x04000144 RID: 324
		private static bool versionCheckPerformed = false;

		// Token: 0x04000145 RID: 325
		private static readonly IntPtr _SIG_DFL = Stdlib.GetDefaultSignal();

		// Token: 0x04000146 RID: 326
		private static readonly IntPtr _SIG_ERR = Stdlib.GetErrorSignal();

		// Token: 0x04000147 RID: 327
		private static readonly IntPtr _SIG_IGN = Stdlib.GetIgnoreSignal();

		// Token: 0x04000148 RID: 328
		[CLSCompliant(false)]
		public static readonly SignalHandler SIG_DFL = new SignalHandler(Stdlib._DefaultHandler);

		// Token: 0x04000149 RID: 329
		[CLSCompliant(false)]
		public static readonly SignalHandler SIG_ERR = new SignalHandler(Stdlib._ErrorHandler);

		// Token: 0x0400014A RID: 330
		[CLSCompliant(false)]
		public static readonly SignalHandler SIG_IGN = new SignalHandler(Stdlib._IgnoreHandler);

		// Token: 0x0400014B RID: 331
		[CLSCompliant(false)]
		public static readonly int _IOFBF = Stdlib.GetFullyBuffered();

		// Token: 0x0400014C RID: 332
		[CLSCompliant(false)]
		public static readonly int _IOLBF = Stdlib.GetLineBuffered();

		// Token: 0x0400014D RID: 333
		[CLSCompliant(false)]
		public static readonly int _IONBF = Stdlib.GetNonBuffered();

		// Token: 0x0400014E RID: 334
		[CLSCompliant(false)]
		public static readonly int BUFSIZ = Stdlib.GetBufferSize();

		// Token: 0x0400014F RID: 335
		[CLSCompliant(false)]
		public static readonly int EOF = Stdlib.GetEOF();

		// Token: 0x04000150 RID: 336
		[CLSCompliant(false)]
		public static readonly int FOPEN_MAX = Stdlib.GetFopenMax();

		// Token: 0x04000151 RID: 337
		[CLSCompliant(false)]
		public static readonly int FILENAME_MAX = Stdlib.GetFilenameMax();

		// Token: 0x04000152 RID: 338
		[CLSCompliant(false)]
		public static readonly int L_tmpnam = Stdlib.GetTmpnamLength();

		// Token: 0x04000153 RID: 339
		public static readonly IntPtr stderr = Stdlib.GetStandardError();

		// Token: 0x04000154 RID: 340
		public static readonly IntPtr stdin = Stdlib.GetStandardInput();

		// Token: 0x04000155 RID: 341
		public static readonly IntPtr stdout = Stdlib.GetStandardOutput();

		// Token: 0x04000156 RID: 342
		[CLSCompliant(false)]
		public static readonly int TMP_MAX = Stdlib.GetTmpMax();

		// Token: 0x04000157 RID: 343
		private static object tmpnam_lock = new object();

		// Token: 0x04000158 RID: 344
		[CLSCompliant(false)]
		public static readonly int EXIT_FAILURE = Stdlib.GetExitFailure();

		// Token: 0x04000159 RID: 345
		[CLSCompliant(false)]
		public static readonly int EXIT_SUCCESS = Stdlib.GetExitSuccess();

		// Token: 0x0400015A RID: 346
		[CLSCompliant(false)]
		public static readonly int MB_CUR_MAX = Stdlib.GetMbCurMax();

		// Token: 0x0400015B RID: 347
		[CLSCompliant(false)]
		public static readonly int RAND_MAX = Stdlib.GetRandMax();

		// Token: 0x0400015C RID: 348
		private static object strerror_lock = new object();
	}
}
