using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public class UnixIOException : IOException
	{
		// Token: 0x0600012B RID: 299 RVA: 0x000055D4 File Offset: 0x000037D4
		public UnixIOException()
			: this(Marshal.GetLastWin32Error())
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000055E1 File Offset: 0x000037E1
		public UnixIOException(int errno)
			: base(UnixIOException.GetMessage(NativeConvert.ToErrno(errno)))
		{
			this.errno = errno;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000055FB File Offset: 0x000037FB
		public UnixIOException(int errno, Exception inner)
			: base(UnixIOException.GetMessage(NativeConvert.ToErrno(errno)), inner)
		{
			this.errno = errno;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005616 File Offset: 0x00003816
		public UnixIOException(Errno errno)
			: base(UnixIOException.GetMessage(errno))
		{
			this.errno = NativeConvert.FromErrno(errno);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005630 File Offset: 0x00003830
		public UnixIOException(Errno errno, Exception inner)
			: base(UnixIOException.GetMessage(errno), inner)
		{
			this.errno = NativeConvert.FromErrno(errno);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000564B File Offset: 0x0000384B
		public UnixIOException(string message)
			: base(message)
		{
			this.errno = 0;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000565B File Offset: 0x0000385B
		public UnixIOException(string message, Exception inner)
			: base(message, inner)
		{
			this.errno = 0;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000566C File Offset: 0x0000386C
		protected UnixIOException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005676 File Offset: 0x00003876
		public int NativeErrorCode
		{
			get
			{
				return this.errno;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000567E File Offset: 0x0000387E
		public Errno ErrorCode
		{
			get
			{
				return NativeConvert.ToErrno(this.errno);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000568B File Offset: 0x0000388B
		private static string GetMessage(Errno errno)
		{
			return string.Format("{0} [{1}].", UnixMarshal.GetErrorDescription(errno), errno);
		}

		// Token: 0x04000076 RID: 118
		private int errno;
	}
}
