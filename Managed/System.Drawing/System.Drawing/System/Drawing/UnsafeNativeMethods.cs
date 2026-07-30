using System;
using System.Internal;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing
{
	// Token: 0x0200003A RID: 58
	[SuppressUnmanagedCodeSecurity]
	internal class UnsafeNativeMethods
	{
		// Token: 0x06000105 RID: 261
		[DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "RtlMoveMemory", ExactSpelling = true, SetLastError = true)]
		public static extern void CopyMemory(HandleRef destData, HandleRef srcData, int size);

		// Token: 0x06000106 RID: 262
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetDC", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntGetDC(HandleRef hWnd);

		// Token: 0x06000107 RID: 263 RVA: 0x00004031 File Offset: 0x00002231
		public static IntPtr GetDC(HandleRef hWnd)
		{
			return global::System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntGetDC(hWnd), SafeNativeMethods.CommonHandles.HDC);
		}

		// Token: 0x06000108 RID: 264
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
		private static extern bool IntDeleteDC(HandleRef hDC);

		// Token: 0x06000109 RID: 265 RVA: 0x00004043 File Offset: 0x00002243
		public static bool DeleteDC(HandleRef hDC)
		{
			global::System.Internal.HandleCollector.Remove((IntPtr)hDC, SafeNativeMethods.CommonHandles.GDI);
			return UnsafeNativeMethods.IntDeleteDC(hDC);
		}

		// Token: 0x0600010A RID: 266
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC", ExactSpelling = true, SetLastError = true)]
		private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

		// Token: 0x0600010B RID: 267 RVA: 0x0000405C File Offset: 0x0000225C
		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
		{
			global::System.Internal.HandleCollector.Remove((IntPtr)hDC, SafeNativeMethods.CommonHandles.HDC);
			return UnsafeNativeMethods.IntReleaseDC(hWnd, hDC);
		}

		// Token: 0x0600010C RID: 268
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

		// Token: 0x0600010D RID: 269 RVA: 0x00004076 File Offset: 0x00002276
		public static IntPtr CreateCompatibleDC(HandleRef hDC)
		{
			return global::System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateCompatibleDC(hDC), SafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x0600010E RID: 270
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetStockObject(int nIndex);

		// Token: 0x0600010F RID: 271
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetSystemDefaultLCID();

		// Token: 0x06000110 RID: 272
		[DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetSystemMetrics(int nIndex);

		// Token: 0x06000111 RID: 273
		[DllImport("user32", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SystemParametersInfo(int uiAction, int uiParam, [In] [Out] NativeMethods.NONCLIENTMETRICS pvParam, int fWinIni);

		// Token: 0x06000112 RID: 274
		[DllImport("user32", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SystemParametersInfo(int uiAction, int uiParam, [In] [Out] SafeNativeMethods.LOGFONT pvParam, int fWinIni);

		// Token: 0x06000113 RID: 275
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

		// Token: 0x06000114 RID: 276
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetObjectType(HandleRef hObject);

		// Token: 0x0200003B RID: 59
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0000000C-0000-0000-C000-000000000046")]
		[ComImport]
		public interface IStream
		{
			// Token: 0x06000116 RID: 278
			int Read([In] IntPtr buf, [In] int len);

			// Token: 0x06000117 RID: 279
			int Write([In] IntPtr buf, [In] int len);

			// Token: 0x06000118 RID: 280
			[return: MarshalAs(UnmanagedType.I8)]
			long Seek([MarshalAs(UnmanagedType.I8)] [In] long dlibMove, [In] int dwOrigin);

			// Token: 0x06000119 RID: 281
			void SetSize([MarshalAs(UnmanagedType.I8)] [In] long libNewSize);

			// Token: 0x0600011A RID: 282
			[return: MarshalAs(UnmanagedType.I8)]
			long CopyTo([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.LPArray)] [Out] long[] pcbRead);

			// Token: 0x0600011B RID: 283
			void Commit([In] int grfCommitFlags);

			// Token: 0x0600011C RID: 284
			void Revert();

			// Token: 0x0600011D RID: 285
			void LockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [In] int dwLockType);

			// Token: 0x0600011E RID: 286
			void UnlockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [In] int dwLockType);

			// Token: 0x0600011F RID: 287
			void Stat([In] IntPtr pStatstg, [In] int grfStatFlag);

			// Token: 0x06000120 RID: 288
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStream Clone();
		}

		// Token: 0x0200003C RID: 60
		internal class ComStreamFromDataStream : UnsafeNativeMethods.IStream
		{
			// Token: 0x06000121 RID: 289 RVA: 0x00004088 File Offset: 0x00002288
			internal ComStreamFromDataStream(Stream dataStream)
			{
				if (dataStream == null)
				{
					throw new ArgumentNullException("dataStream");
				}
				this.dataStream = dataStream;
			}

			// Token: 0x06000122 RID: 290 RVA: 0x000040B0 File Offset: 0x000022B0
			private void ActualizeVirtualPosition()
			{
				if (this._virtualPosition == -1L)
				{
					return;
				}
				if (this._virtualPosition > this.dataStream.Length)
				{
					this.dataStream.SetLength(this._virtualPosition);
				}
				this.dataStream.Position = this._virtualPosition;
				this._virtualPosition = -1L;
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00004105 File Offset: 0x00002305
			public virtual UnsafeNativeMethods.IStream Clone()
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
				return null;
			}

			// Token: 0x06000124 RID: 292 RVA: 0x0000410D File Offset: 0x0000230D
			public virtual void Commit(int grfCommitFlags)
			{
				this.dataStream.Flush();
				this.ActualizeVirtualPosition();
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00004120 File Offset: 0x00002320
			public virtual long CopyTo(UnsafeNativeMethods.IStream pstm, long cb, long[] pcbRead)
			{
				int num = 4096;
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				long num2 = 0L;
				try
				{
					while (num2 < cb)
					{
						int num3 = num;
						if (num2 + (long)num3 > cb)
						{
							num3 = (int)(cb - num2);
						}
						int num4 = this.Read(intPtr, num3);
						if (num4 == 0)
						{
							break;
						}
						if (pstm.Write(intPtr, num4) != num4)
						{
							throw UnsafeNativeMethods.ComStreamFromDataStream.EFail("Wrote an incorrect number of bytes");
						}
						num2 += (long)num4;
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (pcbRead != null && pcbRead.Length != 0)
				{
					pcbRead[0] = num2;
				}
				return num2;
			}

			// Token: 0x06000126 RID: 294 RVA: 0x00002CE2 File Offset: 0x00000EE2
			public virtual void LockRegion(long libOffset, long cb, int dwLockType)
			{
			}

			// Token: 0x06000127 RID: 295 RVA: 0x000041B8 File Offset: 0x000023B8
			protected static ExternalException EFail(string msg)
			{
				throw new ExternalException(msg, -2147467259);
			}

			// Token: 0x06000128 RID: 296 RVA: 0x000041C5 File Offset: 0x000023C5
			protected static void NotImplemented()
			{
				throw new ExternalException(SR.Format("Not implemented.", Array.Empty<object>()), -2147467263);
			}

			// Token: 0x06000129 RID: 297 RVA: 0x000041E0 File Offset: 0x000023E0
			public virtual int Read(IntPtr buf, int length)
			{
				byte[] array = new byte[length];
				int num = this.Read(array, length);
				Marshal.Copy(array, 0, buf, length);
				return num;
			}

			// Token: 0x0600012A RID: 298 RVA: 0x00004205 File Offset: 0x00002405
			public virtual int Read(byte[] buffer, int length)
			{
				this.ActualizeVirtualPosition();
				return this.dataStream.Read(buffer, 0, length);
			}

			// Token: 0x0600012B RID: 299 RVA: 0x0000421B File Offset: 0x0000241B
			public virtual void Revert()
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
			}

			// Token: 0x0600012C RID: 300 RVA: 0x00004224 File Offset: 0x00002424
			public virtual long Seek(long offset, int origin)
			{
				long num = this._virtualPosition;
				if (this._virtualPosition == -1L)
				{
					num = this.dataStream.Position;
				}
				long length = this.dataStream.Length;
				switch (origin)
				{
				case 0:
					if (offset <= length)
					{
						this.dataStream.Position = offset;
						this._virtualPosition = -1L;
					}
					else
					{
						this._virtualPosition = offset;
					}
					break;
				case 1:
					if (offset + num <= length)
					{
						this.dataStream.Position = num + offset;
						this._virtualPosition = -1L;
					}
					else
					{
						this._virtualPosition = offset + num;
					}
					break;
				case 2:
					if (offset <= 0L)
					{
						this.dataStream.Position = length + offset;
						this._virtualPosition = -1L;
					}
					else
					{
						this._virtualPosition = length + offset;
					}
					break;
				}
				if (this._virtualPosition != -1L)
				{
					return this._virtualPosition;
				}
				return this.dataStream.Position;
			}

			// Token: 0x0600012D RID: 301 RVA: 0x000042FC File Offset: 0x000024FC
			public virtual void SetSize(long value)
			{
				this.dataStream.SetLength(value);
			}

			// Token: 0x0600012E RID: 302 RVA: 0x0000421B File Offset: 0x0000241B
			public virtual void Stat(IntPtr pstatstg, int grfStatFlag)
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
			}

			// Token: 0x0600012F RID: 303 RVA: 0x00002CE2 File Offset: 0x00000EE2
			public virtual void UnlockRegion(long libOffset, long cb, int dwLockType)
			{
			}

			// Token: 0x06000130 RID: 304 RVA: 0x0000430C File Offset: 0x0000250C
			public virtual int Write(IntPtr buf, int length)
			{
				byte[] array = new byte[length];
				Marshal.Copy(buf, array, 0, length);
				return this.Write(array, length);
			}

			// Token: 0x06000131 RID: 305 RVA: 0x00004331 File Offset: 0x00002531
			public virtual int Write(byte[] buffer, int length)
			{
				this.ActualizeVirtualPosition();
				this.dataStream.Write(buffer, 0, length);
				return length;
			}

			// Token: 0x040002BA RID: 698
			protected Stream dataStream;

			// Token: 0x040002BB RID: 699
			private long _virtualPosition = -1L;
		}
	}
}
