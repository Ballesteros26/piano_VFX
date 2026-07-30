using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Security
{
	// Token: 0x02000047 RID: 71
	internal class SecurityBuffer
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
		public SecurityBuffer(byte[] data, int offset, int size, SecurityBufferType tokentype)
		{
			if (offset < 0 || offset > ((data == null) ? 0 : data.Length))
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'offset' out of range.  [{0}]", new object[] { offset }), ".ctor");
			}
			if (size < 0 || size > ((data == null) ? 0 : (data.Length - offset)))
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[] { size }), ".ctor");
			}
			this.offset = ((data == null || offset < 0) ? 0 : Math.Min(offset, data.Length));
			this.size = ((data == null || size < 0) ? 0 : Math.Min(size, data.Length - this.offset));
			this.type = tokentype;
			this.token = ((size == 0) ? null : data);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000E97B File Offset: 0x0000CB7B
		public SecurityBuffer(byte[] data, SecurityBufferType tokentype)
		{
			this.size = ((data == null) ? 0 : data.Length);
			this.type = tokentype;
			this.token = ((this.size == 0) ? null : data);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		public SecurityBuffer(int size, SecurityBufferType tokentype)
		{
			if (size < 0)
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[] { size }), ".ctor");
			}
			this.size = size;
			this.type = tokentype;
			this.token = ((size == 0) ? null : new byte[size]);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000EA07 File Offset: 0x0000CC07
		public SecurityBuffer(ChannelBinding binding)
		{
			this.size = ((binding == null) ? 0 : binding.Size);
			this.type = SecurityBufferType.SECBUFFER_CHANNEL_BINDINGS;
			this.unmanagedToken = binding;
		}

		// Token: 0x040004BA RID: 1210
		public int size;

		// Token: 0x040004BB RID: 1211
		public SecurityBufferType type;

		// Token: 0x040004BC RID: 1212
		public byte[] token;

		// Token: 0x040004BD RID: 1213
		public SafeHandle unmanagedToken;

		// Token: 0x040004BE RID: 1214
		public int offset;
	}
}
