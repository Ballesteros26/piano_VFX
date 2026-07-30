using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000047 RID: 71
	internal class Win32AnonymousPipeClient : Win32AnonymousPipe, IAnonymousPipeClient, IPipe
	{
		// Token: 0x06000162 RID: 354 RVA: 0x000041D4 File Offset: 0x000023D4
		public Win32AnonymousPipeClient(AnonymousPipeClientStream owner, SafePipeHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000041E3 File Offset: 0x000023E3
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x04000235 RID: 565
		private SafePipeHandle handle;
	}
}
