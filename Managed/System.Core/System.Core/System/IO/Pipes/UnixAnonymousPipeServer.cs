using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200003E RID: 62
	internal class UnixAnonymousPipeServer : UnixAnonymousPipe, IAnonymousPipeServer, IPipe
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00003DFC File Offset: 0x00001FFC
		public UnixAnonymousPipeServer(AnonymousPipeServerStream owner, PipeDirection direction, HandleInheritability inheritability, int bufferSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003E09 File Offset: 0x00002009
		public UnixAnonymousPipeServer(AnonymousPipeServerStream owner, SafePipeHandle serverHandle, SafePipeHandle clientHandle)
		{
			this.server_handle = serverHandle;
			this.client_handle = clientHandle;
			throw new NotImplementedException();
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00003E24 File Offset: 0x00002024
		public override SafePipeHandle Handle
		{
			get
			{
				return this.server_handle;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00003E2C File Offset: 0x0000202C
		public SafePipeHandle ClientHandle
		{
			get
			{
				return this.client_handle;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000227E File Offset: 0x0000047E
		public void DisposeLocalCopyOfClientHandle()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000227 RID: 551
		private SafePipeHandle server_handle;

		// Token: 0x04000228 RID: 552
		private SafePipeHandle client_handle;
	}
}
