using System;
using System.Runtime.InteropServices;
using Mono.Util;

namespace System.IO.Compression
{
	// Token: 0x02000409 RID: 1033
	internal class DeflateStreamNative
	{
		// Token: 0x06001F94 RID: 8084 RVA: 0x000020EB File Offset: 0x000002EB
		private DeflateStreamNative()
		{
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0007B4BC File Offset: 0x000796BC
		public static DeflateStreamNative Create(Stream compressedStream, CompressionMode mode, bool gzip)
		{
			DeflateStreamNative deflateStreamNative = new DeflateStreamNative();
			deflateStreamNative.data = GCHandle.Alloc(deflateStreamNative);
			deflateStreamNative.feeder = ((mode == CompressionMode.Compress) ? new DeflateStreamNative.UnmanagedReadOrWrite(DeflateStreamNative.UnmanagedWrite) : new DeflateStreamNative.UnmanagedReadOrWrite(DeflateStreamNative.UnmanagedRead));
			deflateStreamNative.z_stream = DeflateStreamNative.CreateZStream(mode, gzip, deflateStreamNative.feeder, GCHandle.ToIntPtr(deflateStreamNative.data));
			if (deflateStreamNative.z_stream.IsInvalid)
			{
				deflateStreamNative.Dispose(true);
				return null;
			}
			deflateStreamNative.base_stream = compressedStream;
			return deflateStreamNative;
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x0007B53C File Offset: 0x0007973C
		~DeflateStreamNative()
		{
			this.Dispose(false);
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0007B56C File Offset: 0x0007976C
		public void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				GC.SuppressFinalize(this);
				this.io_buffer = null;
				this.z_stream.Dispose();
			}
			if (this.data.IsAllocated)
			{
				this.data.Free();
			}
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0007B5BB File Offset: 0x000797BB
		public void Flush()
		{
			DeflateStreamNative.CheckResult(DeflateStreamNative.Flush(this.z_stream), "Flush");
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0007B5D2 File Offset: 0x000797D2
		public int ReadZStream(IntPtr buffer, int length)
		{
			int num = DeflateStreamNative.ReadZStream(this.z_stream, buffer, length);
			DeflateStreamNative.CheckResult(num, "ReadInternal");
			return num;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x0007B5EC File Offset: 0x000797EC
		public void WriteZStream(IntPtr buffer, int length)
		{
			DeflateStreamNative.CheckResult(DeflateStreamNative.WriteZStream(this.z_stream, buffer, length), "WriteInternal");
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0007B608 File Offset: 0x00079808
		[MonoPInvokeCallback(typeof(DeflateStreamNative.UnmanagedReadOrWrite))]
		private static int UnmanagedRead(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStreamNative deflateStreamNative = GCHandle.FromIntPtr(data).Target as DeflateStreamNative;
			if (deflateStreamNative == null)
			{
				return -1;
			}
			return deflateStreamNative.UnmanagedRead(buffer, length);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0007B638 File Offset: 0x00079838
		private int UnmanagedRead(IntPtr buffer, int length)
		{
			if (this.io_buffer == null)
			{
				this.io_buffer = new byte[4096];
			}
			int num = Math.Min(length, this.io_buffer.Length);
			int num2 = this.base_stream.Read(this.io_buffer, 0, num);
			if (num2 > 0)
			{
				Marshal.Copy(this.io_buffer, 0, buffer, num2);
			}
			return num2;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0007B694 File Offset: 0x00079894
		[MonoPInvokeCallback(typeof(DeflateStreamNative.UnmanagedReadOrWrite))]
		private static int UnmanagedWrite(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStreamNative deflateStreamNative = GCHandle.FromIntPtr(data).Target as DeflateStreamNative;
			if (deflateStreamNative == null)
			{
				return -1;
			}
			return deflateStreamNative.UnmanagedWrite(buffer, length);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0007B6C4 File Offset: 0x000798C4
		private unsafe int UnmanagedWrite(IntPtr buffer, int length)
		{
			int num = 0;
			while (length > 0)
			{
				if (this.io_buffer == null)
				{
					this.io_buffer = new byte[4096];
				}
				int num2 = Math.Min(length, this.io_buffer.Length);
				Marshal.Copy(buffer, this.io_buffer, 0, num2);
				this.base_stream.Write(this.io_buffer, 0, num2);
				buffer = new IntPtr((void*)((byte*)buffer.ToPointer() + num2));
				length -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0007B73C File Offset: 0x0007993C
		private static void CheckResult(int result, string where)
		{
			if (result >= 0)
			{
				return;
			}
			string text;
			switch (result)
			{
			case -11:
				text = "IO error";
				goto IL_0082;
			case -10:
				text = "Invalid argument(s)";
				goto IL_0082;
			case -6:
				text = "Invalid version";
				goto IL_0082;
			case -5:
				text = "Internal error (no progress possible)";
				goto IL_0082;
			case -4:
				text = "Not enough memory";
				goto IL_0082;
			case -3:
				text = "Corrupted data";
				goto IL_0082;
			case -2:
				text = "Internal error";
				goto IL_0082;
			case -1:
				text = "Unknown error";
				goto IL_0082;
			}
			text = "Unknown error";
			IL_0082:
			throw new IOException(text + " " + where);
		}

		// Token: 0x06001FA0 RID: 8096
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern DeflateStreamNative.SafeDeflateStreamHandle CreateZStream(CompressionMode compress, bool gzip, DeflateStreamNative.UnmanagedReadOrWrite feeder, IntPtr data);

		// Token: 0x06001FA1 RID: 8097
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int CloseZStream(IntPtr stream);

		// Token: 0x06001FA2 RID: 8098
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Flush(DeflateStreamNative.SafeDeflateStreamHandle stream);

		// Token: 0x06001FA3 RID: 8099
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int ReadZStream(DeflateStreamNative.SafeDeflateStreamHandle stream, IntPtr buffer, int length);

		// Token: 0x06001FA4 RID: 8100
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int WriteZStream(DeflateStreamNative.SafeDeflateStreamHandle stream, IntPtr buffer, int length);

		// Token: 0x04001B7E RID: 7038
		private const int BufferSize = 4096;

		// Token: 0x04001B7F RID: 7039
		private DeflateStreamNative.UnmanagedReadOrWrite feeder;

		// Token: 0x04001B80 RID: 7040
		private Stream base_stream;

		// Token: 0x04001B81 RID: 7041
		private DeflateStreamNative.SafeDeflateStreamHandle z_stream;

		// Token: 0x04001B82 RID: 7042
		private GCHandle data;

		// Token: 0x04001B83 RID: 7043
		private bool disposed;

		// Token: 0x04001B84 RID: 7044
		private byte[] io_buffer;

		// Token: 0x04001B85 RID: 7045
		private const string LIBNAME = "MonoPosixHelper";

		// Token: 0x0200040A RID: 1034
		// (Invoke) Token: 0x06001FA6 RID: 8102
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int UnmanagedReadOrWrite(IntPtr buffer, int length, IntPtr data);

		// Token: 0x0200040B RID: 1035
		private sealed class SafeDeflateStreamHandle : SafeHandle
		{
			// Token: 0x1700067A RID: 1658
			// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x0007B7DC File Offset: 0x000799DC
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			// Token: 0x06001FAA RID: 8106 RVA: 0x0007B7EE File Offset: 0x000799EE
			private SafeDeflateStreamHandle()
				: base(IntPtr.Zero, true)
			{
			}

			// Token: 0x06001FAB RID: 8107 RVA: 0x0007B7FC File Offset: 0x000799FC
			protected override bool ReleaseHandle()
			{
				DeflateStreamNative.CloseZStream(this.handle);
				return true;
			}
		}
	}
}
