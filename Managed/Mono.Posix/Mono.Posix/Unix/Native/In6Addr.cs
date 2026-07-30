using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000061 RID: 97
	[Map]
	public struct In6Addr : IEquatable<In6Addr>
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000AA98 File Offset: 0x00008C98
		public unsafe In6Addr(byte[] buffer)
		{
			if (buffer.Length != 16)
			{
				throw new ArgumentException("buffer.Length != 16", "buffer");
			}
			this.addr0 = (this.addr1 = 0UL);
			fixed (ulong* ptr = &this.addr0)
			{
				ulong* ptr2 = ptr;
				Marshal.Copy(buffer, 0, (IntPtr)((void*)ptr2), 16);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000AAEC File Offset: 0x00008CEC
		public unsafe void CopyFrom(byte[] source, int startIndex)
		{
			fixed (ulong* ptr = &this.addr0)
			{
				ulong* ptr2 = ptr;
				Marshal.Copy(source, startIndex, (IntPtr)((void*)ptr2), 16);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000AB18 File Offset: 0x00008D18
		public unsafe void CopyTo(byte[] destination, int startIndex)
		{
			fixed (ulong* ptr = &this.addr0)
			{
				Marshal.Copy((IntPtr)((void*)ptr), destination, startIndex, 16);
			}
		}

		// Token: 0x1700008E RID: 142
		public unsafe byte this[int index]
		{
			get
			{
				if (index < 0 || index >= 16)
				{
					throw new ArgumentOutOfRangeException("index", "index < 0 || index >= 16");
				}
				fixed (ulong* ptr = &this.addr0)
				{
					return ((byte*)ptr)[index];
				}
			}
			set
			{
				if (index < 0 || index >= 16)
				{
					throw new ArgumentOutOfRangeException("index", "index < 0 || index >= 16");
				}
				fixed (ulong* ptr = &this.addr0)
				{
					((byte*)ptr)[index] = value;
				}
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000ABAA File Offset: 0x00008DAA
		public override string ToString()
		{
			return NativeConvert.ToIPAddress(this).ToString();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000ABBC File Offset: 0x00008DBC
		public override int GetHashCode()
		{
			return this.addr0.GetHashCode() ^ this.addr1.GetHashCode();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000ABD5 File Offset: 0x00008DD5
		public override bool Equals(object obj)
		{
			return obj is In6Addr && this.Equals((In6Addr)obj);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000ABED File Offset: 0x00008DED
		public bool Equals(In6Addr value)
		{
			return this.addr0 == value.addr0 && this.addr1 == value.addr1;
		}

		// Token: 0x0400044C RID: 1100
		private ulong addr0;

		// Token: 0x0400044D RID: 1101
		private ulong addr1;
	}
}
