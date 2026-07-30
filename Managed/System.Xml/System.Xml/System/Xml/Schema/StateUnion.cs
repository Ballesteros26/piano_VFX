using System;
using System.Runtime.InteropServices;

namespace System.Xml.Schema
{
	// Token: 0x02000423 RID: 1059
	[StructLayout(LayoutKind.Explicit)]
	internal struct StateUnion
	{
		// Token: 0x04001C60 RID: 7264
		[FieldOffset(0)]
		public int State;

		// Token: 0x04001C61 RID: 7265
		[FieldOffset(0)]
		public int AllElementsRequired;

		// Token: 0x04001C62 RID: 7266
		[FieldOffset(0)]
		public int CurPosIndex;

		// Token: 0x04001C63 RID: 7267
		[FieldOffset(0)]
		public int NumberOfRunningPos;
	}
}
