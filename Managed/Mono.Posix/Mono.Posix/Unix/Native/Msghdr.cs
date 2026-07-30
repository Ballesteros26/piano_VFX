using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200006F RID: 111
	[CLSCompliant(false)]
	public sealed class Msghdr
	{
		// Token: 0x04000484 RID: 1156
		public Sockaddr msg_name;

		// Token: 0x04000485 RID: 1157
		public Iovec[] msg_iov;

		// Token: 0x04000486 RID: 1158
		public int msg_iovlen;

		// Token: 0x04000487 RID: 1159
		public byte[] msg_control;

		// Token: 0x04000488 RID: 1160
		public long msg_controllen;

		// Token: 0x04000489 RID: 1161
		public MessageFlags msg_flags;
	}
}
