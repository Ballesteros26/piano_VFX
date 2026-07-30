using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001FD RID: 509
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoTypeInfo
	{
		// Token: 0x04000C57 RID: 3159
		public string full_name;

		// Token: 0x04000C58 RID: 3160
		public MonoCMethod default_ctor;
	}
}
