using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000060 RID: 96
	[Map]
	[CLSCompliant(false)]
	public struct InAddr : IEquatable<InAddr>
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000A91C File Offset: 0x00008B1C
		public unsafe InAddr(byte b0, byte b1, byte b2, byte b3)
		{
			this.s_addr = 0U;
			fixed (uint* ptr = &this.s_addr)
			{
				byte* ptr2 = (byte*)ptr;
				*ptr2 = b0;
				ptr2[1] = b1;
				ptr2[2] = b2;
				ptr2[3] = b3;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000A950 File Offset: 0x00008B50
		public unsafe InAddr(byte[] buffer)
		{
			if (buffer.Length != 4)
			{
				throw new ArgumentException("buffer.Length != 4", "buffer");
			}
			this.s_addr = 0U;
			fixed (uint* ptr = &this.s_addr)
			{
				uint* ptr2 = ptr;
				Marshal.Copy(buffer, 0, (IntPtr)((void*)ptr2), 4);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000A998 File Offset: 0x00008B98
		public unsafe void CopyFrom(byte[] source, int startIndex)
		{
			fixed (uint* ptr = &this.s_addr)
			{
				uint* ptr2 = ptr;
				Marshal.Copy(source, startIndex, (IntPtr)((void*)ptr2), 4);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public unsafe void CopyTo(byte[] destination, int startIndex)
		{
			fixed (uint* ptr = &this.s_addr)
			{
				Marshal.Copy((IntPtr)((void*)ptr), destination, startIndex, 4);
			}
		}

		// Token: 0x1700008D RID: 141
		public unsafe byte this[int index]
		{
			get
			{
				if (index < 0 || index >= 4)
				{
					throw new ArgumentOutOfRangeException("index", "index < 0 || index >= 4");
				}
				fixed (uint* ptr = &this.s_addr)
				{
					return ((byte*)ptr)[index];
				}
			}
			set
			{
				if (index < 0 || index >= 4)
				{
					throw new ArgumentOutOfRangeException("index", "index < 0 || index >= 4");
				}
				fixed (uint* ptr = &this.s_addr)
				{
					((byte*)ptr)[index] = value;
				}
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000AA51 File Offset: 0x00008C51
		public override string ToString()
		{
			return NativeConvert.ToIPAddress(this).ToString();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000AA63 File Offset: 0x00008C63
		public override int GetHashCode()
		{
			return this.s_addr.GetHashCode();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000AA70 File Offset: 0x00008C70
		public override bool Equals(object obj)
		{
			return obj is InAddr && this.Equals((InAddr)obj);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000AA88 File Offset: 0x00008C88
		public bool Equals(InAddr value)
		{
			return this.s_addr == value.s_addr;
		}

		// Token: 0x0400044B RID: 1099
		public uint s_addr;
	}
}
