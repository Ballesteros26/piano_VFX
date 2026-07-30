using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000062 RID: 98
	[Map("struct cmsghdr")]
	[CLSCompliant(false)]
	public struct Cmsghdr
	{
		// Token: 0x06000422 RID: 1058
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Cmsghdr_getsize", SetLastError = true)]
		private static extern int getsize();

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000AC0D File Offset: 0x00008E0D
		public static int Size
		{
			get
			{
				return Cmsghdr.size;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000AC14 File Offset: 0x00008E14
		public unsafe static Cmsghdr ReadFromBuffer(Msghdr msgh, long cmsg)
		{
			if (msgh == null)
			{
				throw new ArgumentNullException("msgh");
			}
			if (msgh.msg_control == null || msgh.msg_controllen > (long)msgh.msg_control.Length)
			{
				throw new ArgumentException("msgh.msg_control == null || msgh.msg_controllen > msgh.msg_control.Length", "msgh");
			}
			if (cmsg < 0L || cmsg + (long)Cmsghdr.Size > msgh.msg_controllen)
			{
				throw new ArgumentException("cmsg offset pointing out of buffer", "cmsg");
			}
			byte[] array;
			byte* ptr;
			if ((array = msgh.msg_control) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			Cmsghdr cmsghdr;
			if (!NativeConvert.TryCopy((IntPtr)((void*)(ptr + cmsg)), out cmsghdr))
			{
				throw new ArgumentException("Failed to convert from native struct", "buffer");
			}
			array = null;
			if (NativeConvert.FromUnixSocketProtocol(cmsghdr.cmsg_level) == NativeConvert.FromUnixSocketProtocol(UnixSocketProtocol.SOL_SOCKET))
			{
				cmsghdr.cmsg_level = UnixSocketProtocol.SOL_SOCKET;
			}
			return cmsghdr;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		public unsafe void WriteToBuffer(Msghdr msgh, long cmsg)
		{
			if (msgh == null)
			{
				throw new ArgumentNullException("msgh");
			}
			if (msgh.msg_control == null || msgh.msg_controllen > (long)msgh.msg_control.Length)
			{
				throw new ArgumentException("msgh.msg_control == null || msgh.msg_controllen > msgh.msg_control.Length", "msgh");
			}
			if (cmsg < 0L || cmsg + (long)Cmsghdr.Size > msgh.msg_controllen)
			{
				throw new ArgumentException("cmsg offset pointing out of buffer", "cmsg");
			}
			byte[] array;
			byte* ptr;
			if ((array = msgh.msg_control) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			if (!NativeConvert.TryCopy(ref this, (IntPtr)((void*)(ptr + cmsg))))
			{
				throw new ArgumentException("Failed to convert to native struct", "buffer");
			}
			array = null;
		}

		// Token: 0x0400044E RID: 1102
		public long cmsg_len;

		// Token: 0x0400044F RID: 1103
		public UnixSocketProtocol cmsg_level;

		// Token: 0x04000450 RID: 1104
		public UnixSocketControlMessage cmsg_type;

		// Token: 0x04000451 RID: 1105
		private static readonly int size = Cmsghdr.getsize();
	}
}
