using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000046 RID: 70
	internal abstract class Win32AnonymousPipe : IPipe
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000160 RID: 352
		public abstract SafePipeHandle Handle { get; }

		// Token: 0x06000161 RID: 353 RVA: 0x0000227E File Offset: 0x0000047E
		public void WaitForPipeDrain()
		{
			throw new NotImplementedException();
		}
	}
}
