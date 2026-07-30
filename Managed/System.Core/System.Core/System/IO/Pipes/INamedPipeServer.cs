using System;

namespace System.IO.Pipes
{
	// Token: 0x02000036 RID: 54
	internal interface INamedPipeServer : IPipe
	{
		// Token: 0x060000F3 RID: 243
		void Disconnect();

		// Token: 0x060000F4 RID: 244
		void WaitForConnection();
	}
}
