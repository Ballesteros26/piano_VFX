using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000032 RID: 50
	internal interface IPipe
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000EB RID: 235
		SafePipeHandle Handle { get; }

		// Token: 0x060000EC RID: 236
		void WaitForPipeDrain();
	}
}
