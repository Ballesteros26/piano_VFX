using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x0200068C RID: 1676
	internal struct sockaddr_dl
	{
		// Token: 0x060034A2 RID: 13474 RVA: 0x000C3848 File Offset: 0x000C1A48
		internal void Read(IntPtr ptr)
		{
			this.sdl_len = Marshal.ReadByte(ptr, 0);
			this.sdl_family = Marshal.ReadByte(ptr, 1);
			this.sdl_index = (ushort)Marshal.ReadInt16(ptr, 2);
			this.sdl_type = Marshal.ReadByte(ptr, 4);
			this.sdl_nlen = Marshal.ReadByte(ptr, 5);
			this.sdl_alen = Marshal.ReadByte(ptr, 6);
			this.sdl_slen = Marshal.ReadByte(ptr, 7);
			this.sdl_data = new byte[Math.Max(12, (int)(this.sdl_len - 8))];
			Marshal.Copy(new IntPtr(ptr.ToInt64() + 8L), this.sdl_data, 0, this.sdl_data.Length);
		}

		// Token: 0x04002A33 RID: 10803
		public byte sdl_len;

		// Token: 0x04002A34 RID: 10804
		public byte sdl_family;

		// Token: 0x04002A35 RID: 10805
		public ushort sdl_index;

		// Token: 0x04002A36 RID: 10806
		public byte sdl_type;

		// Token: 0x04002A37 RID: 10807
		public byte sdl_nlen;

		// Token: 0x04002A38 RID: 10808
		public byte sdl_alen;

		// Token: 0x04002A39 RID: 10809
		public byte sdl_slen;

		// Token: 0x04002A3A RID: 10810
		public byte[] sdl_data;
	}
}
