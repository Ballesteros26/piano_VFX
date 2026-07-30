using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000034 RID: 52
	internal interface IAnonymousPipeServer : IPipe
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000ED RID: 237
		SafePipeHandle ClientHandle { get; }

		// Token: 0x060000EE RID: 238
		void DisposeLocalCopyOfClientHandle();
	}
}
