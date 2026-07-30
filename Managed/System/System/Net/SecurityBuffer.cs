using System;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000448 RID: 1096
	internal class SecurityBuffer
	{
		// Token: 0x060020B5 RID: 8373 RVA: 0x0007F2FC File Offset: 0x0007D4FC
		public SecurityBuffer(byte[] data, int offset, int size, BufferType tokentype)
		{
			this.offset = ((data == null || offset < 0) ? 0 : Math.Min(offset, data.Length));
			this.size = ((data == null || size < 0) ? 0 : Math.Min(size, data.Length - this.offset));
			this.type = tokentype;
			this.token = ((size == 0) ? null : data);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0007F35D File Offset: 0x0007D55D
		public SecurityBuffer(byte[] data, BufferType tokentype)
		{
			this.size = ((data == null) ? 0 : data.Length);
			this.type = tokentype;
			this.token = ((this.size == 0) ? null : data);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0007F38D File Offset: 0x0007D58D
		public SecurityBuffer(int size, BufferType tokentype)
		{
			this.size = size;
			this.type = tokentype;
			this.token = ((size == 0) ? null : new byte[size]);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0007F3B5 File Offset: 0x0007D5B5
		public SecurityBuffer(ChannelBinding binding)
		{
			this.size = ((binding == null) ? 0 : binding.Size);
			this.type = BufferType.ChannelBindings;
			this.unmanagedToken = binding;
		}

		// Token: 0x04001D3F RID: 7487
		public int size;

		// Token: 0x04001D40 RID: 7488
		public BufferType type;

		// Token: 0x04001D41 RID: 7489
		public byte[] token;

		// Token: 0x04001D42 RID: 7490
		public SafeHandle unmanagedToken;

		// Token: 0x04001D43 RID: 7491
		public int offset;
	}
}
