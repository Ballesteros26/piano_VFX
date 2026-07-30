using System;
using System.Runtime.InteropServices;

namespace System.IO.Pipes
{
	// Token: 0x0200004E RID: 78
	internal struct SecurityAttributes
	{
		// Token: 0x0600017C RID: 380 RVA: 0x000045B3 File Offset: 0x000027B3
		public SecurityAttributes(HandleInheritability inheritability, IntPtr securityDescriptor)
		{
			this.Length = Marshal.SizeOf(typeof(SecurityAttributes));
			this.SecurityDescriptor = securityDescriptor;
			this.Inheritable = inheritability == HandleInheritability.Inheritable;
		}

		// Token: 0x04000243 RID: 579
		public readonly int Length;

		// Token: 0x04000244 RID: 580
		public readonly IntPtr SecurityDescriptor;

		// Token: 0x04000245 RID: 581
		public readonly bool Inheritable;
	}
}
