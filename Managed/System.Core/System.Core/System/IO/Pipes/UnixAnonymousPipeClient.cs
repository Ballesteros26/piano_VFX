using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200003D RID: 61
	internal class UnixAnonymousPipeClient : UnixAnonymousPipe, IAnonymousPipeClient, IPipe
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00003DE5 File Offset: 0x00001FE5
		public UnixAnonymousPipeClient(AnonymousPipeClientStream owner, SafePipeHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x04000226 RID: 550
		private SafePipeHandle handle;
	}
}
