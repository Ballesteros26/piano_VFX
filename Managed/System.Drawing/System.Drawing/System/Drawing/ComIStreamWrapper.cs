using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Drawing
{
	// Token: 0x0200005C RID: 92
	internal sealed class ComIStreamWrapper : IStream
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00007C2B File Offset: 0x00005E2B
		internal ComIStreamWrapper(Stream stream)
		{
			this.baseStream = stream;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00007C44 File Offset: 0x00005E44
		private void SetSizeToPosition()
		{
			if (this.position != -1L)
			{
				if (this.position > this.baseStream.Length)
				{
					this.baseStream.SetLength(this.position);
				}
				this.baseStream.Position = this.position;
				this.position = -1L;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00007C98 File Offset: 0x00005E98
		public void Read(byte[] pv, int cb, IntPtr pcbRead)
		{
			int num = 0;
			if (cb != 0)
			{
				this.SetSizeToPosition();
				num = this.baseStream.Read(pv, 0, cb);
			}
			if (pcbRead != IntPtr.Zero)
			{
				Marshal.WriteInt32(pcbRead, num);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00007CD3 File Offset: 0x00005ED3
		public void Write(byte[] pv, int cb, IntPtr pcbWritten)
		{
			if (cb != 0)
			{
				this.SetSizeToPosition();
				this.baseStream.Write(pv, 0, cb);
			}
			if (pcbWritten != IntPtr.Zero)
			{
				Marshal.WriteInt32(pcbWritten, cb);
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00007D00 File Offset: 0x00005F00
		public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
		{
			long length = this.baseStream.Length;
			long num;
			switch (dwOrigin)
			{
			case 0:
				num = dlibMove;
				break;
			case 1:
				if (this.position == -1L)
				{
					num = this.baseStream.Position + dlibMove;
				}
				else
				{
					num = this.position + dlibMove;
				}
				break;
			case 2:
				num = length + dlibMove;
				break;
			default:
				throw new ExternalException(null, -2147287039);
			}
			if (num > length)
			{
				this.position = num;
			}
			else
			{
				this.baseStream.Position = num;
				this.position = -1L;
			}
			if (plibNewPosition != IntPtr.Zero)
			{
				Marshal.WriteInt64(plibNewPosition, num);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00007D9F File Offset: 0x00005F9F
		public void SetSize(long libNewSize)
		{
			this.baseStream.SetLength(libNewSize);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00007DB0 File Offset: 0x00005FB0
		public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
		{
			long num = 0L;
			if (cb != 0L)
			{
				int num2;
				if (cb < 4096L)
				{
					num2 = (int)cb;
				}
				else
				{
					num2 = 4096;
				}
				byte[] array = new byte[num2];
				this.SetSizeToPosition();
				int num3;
				while ((num3 = this.baseStream.Read(array, 0, num2)) != 0)
				{
					pstm.Write(array, num3, IntPtr.Zero);
					num += (long)num3;
					if (num >= cb)
					{
						break;
					}
					if (cb - num < 4096L)
					{
						num2 = (int)(cb - num);
					}
				}
			}
			if (pcbRead != IntPtr.Zero)
			{
				Marshal.WriteInt64(pcbRead, num);
			}
			if (pcbWritten != IntPtr.Zero)
			{
				Marshal.WriteInt64(pcbWritten, num);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00007E48 File Offset: 0x00006048
		public void Commit(int grfCommitFlags)
		{
			this.baseStream.Flush();
			this.SetSizeToPosition();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00007E5B File Offset: 0x0000605B
		public void Revert()
		{
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00007E5B File Offset: 0x0000605B
		public void LockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00007E5B File Offset: 0x0000605B
		public void UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00007E68 File Offset: 0x00006068
		public void Stat(out global::System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
		{
			pstatstg = default(global::System.Runtime.InteropServices.ComTypes.STATSTG);
			pstatstg.cbSize = this.baseStream.Length;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00007E82 File Offset: 0x00006082
		public void Clone(out IStream ppstm)
		{
			ppstm = null;
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x0400037E RID: 894
		private const int STG_E_INVALIDFUNCTION = -2147287039;

		// Token: 0x0400037F RID: 895
		private readonly Stream baseStream;

		// Token: 0x04000380 RID: 896
		private long position = -1L;
	}
}
