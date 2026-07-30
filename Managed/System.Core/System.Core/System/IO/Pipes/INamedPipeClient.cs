using System;

namespace System.IO.Pipes
{
	// Token: 0x02000035 RID: 53
	internal interface INamedPipeClient : IPipe
	{
		// Token: 0x060000EF RID: 239
		void Connect();

		// Token: 0x060000F0 RID: 240
		void Connect(int timeout);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000F1 RID: 241
		int NumberOfServerInstances { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F2 RID: 242
		bool IsAsync { get; }
	}
}
