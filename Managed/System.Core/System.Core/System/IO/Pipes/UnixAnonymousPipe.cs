using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200003C RID: 60
	internal abstract class UnixAnonymousPipe : IPipe
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600013C RID: 316
		public abstract SafePipeHandle Handle { get; }

		// Token: 0x0600013D RID: 317 RVA: 0x0000227E File Offset: 0x0000047E
		public void WaitForPipeDrain()
		{
			throw new NotImplementedException();
		}
	}
}
