using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Mozilla
{
	// Token: 0x02000062 RID: 98
	internal class Stream : nsIInputStream, nsIOutputStream
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00004090 File Offset: 0x00002290
		public Stream(Stream stream)
		{
			this.back = stream;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000409F File Offset: 0x0000229F
		public Stream BaseStream
		{
			get
			{
				return this.back;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000040A7 File Offset: 0x000022A7
		public int close()
		{
			this.back.Close();
			return 0;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000040B5 File Offset: 0x000022B5
		public int flush()
		{
			this.back.Flush();
			return 0;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000040C4 File Offset: 0x000022C4
		public int write([MarshalAs(UnmanagedType.LPStr)] string str, uint count, out uint ret)
		{
			ret = count;
			if (count <= 0U)
			{
				return 0;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(str);
			this.back.Write(bytes, 0, (int)count);
			return 0;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000040F5 File Offset: 0x000022F5
		public int writeFrom([MarshalAs(UnmanagedType.Interface)] nsIInputStream aFromStream, uint aCount, out uint ret)
		{
			ret = 0U;
			return 0;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000040FB File Offset: 0x000022FB
		public int writeSegments(nsIReadSegmentFunDelegate aReader, IntPtr aClosure, uint aCount, out uint ret)
		{
			ret = 0U;
			return 0;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00004102 File Offset: 0x00002302
		public int isNonBlocking(out bool ret)
		{
			ret = false;
			return 0;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00004108 File Offset: 0x00002308
		public int available(out uint ret)
		{
			ret = 0U;
			return 0;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00004110 File Offset: 0x00002310
		public int read(HandleRef str, uint count, out uint ret)
		{
			byte[] array = new byte[count];
			ret = (uint)this.back.Read(array, 0, (int)count);
			string @string = Encoding.ASCII.GetString(array);
			Base.StringSet(str, @string);
			return 0;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00004148 File Offset: 0x00002348
		public int readSegments(nsIWriteSegmentFunDelegate aWriter, IntPtr aClosure, uint aCount, out uint ret)
		{
			ret = 0U;
			return 0;
		}

		// Token: 0x040000D4 RID: 212
		private Stream back;
	}
}
