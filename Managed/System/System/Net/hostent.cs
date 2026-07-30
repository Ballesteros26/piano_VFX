using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000459 RID: 1113
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct hostent
	{
		// Token: 0x04001DDF RID: 7647
		public IntPtr h_name;

		// Token: 0x04001DE0 RID: 7648
		public IntPtr h_aliases;

		// Token: 0x04001DE1 RID: 7649
		public short h_addrtype;

		// Token: 0x04001DE2 RID: 7650
		public short h_length;

		// Token: 0x04001DE3 RID: 7651
		public IntPtr h_addr_list;
	}
}
