using System;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000045 RID: 69
	internal class AsyncMethodData
	{
		// Token: 0x040005D6 RID: 1494
		public IntPtr Handle;

		// Token: 0x040005D7 RID: 1495
		public Delegate Method;

		// Token: 0x040005D8 RID: 1496
		public object[] Args;

		// Token: 0x040005D9 RID: 1497
		public AsyncMethodResult Result;

		// Token: 0x040005DA RID: 1498
		public ExecutionContext Context;
	}
}
