using System;
using Microsoft.Win32.SafeHandles;
using Mono.Unix.Native;

namespace System.IO.Pipes
{
	// Token: 0x02000044 RID: 68
	internal class UnixNamedPipeServer : UnixNamedPipe, INamedPipeServer, IPipe
	{
		// Token: 0x06000158 RID: 344 RVA: 0x0000410F File Offset: 0x0000230F
		public UnixNamedPipeServer(NamedPipeServerStream owner, SafePipeHandle safePipeHandle)
		{
			this.handle = safePipeHandle;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004120 File Offset: 0x00002320
		public UnixNamedPipeServer(NamedPipeServerStream owner, string pipeName, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeAccessRights rights, PipeOptions options, int inBufferSize, int outBufferSize, HandleInheritability inheritability)
		{
			string text = Path.Combine("/var/tmp/", pipeName);
			base.EnsureTargetFile(text);
			base.RightsToAccess(rights);
			base.ValidateOptions(options, owner.TransmissionMode);
			FileStream fileStream = new FileStream(text, FileMode.Open, base.RightsToFileAccess(rights), FileShare.ReadWrite);
			this.handle = new SafePipeHandle(fileStream.SafeFileHandle.DangerousGetHandle(), false);
			owner.Stream = fileStream;
			this.should_close_handle = true;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004193 File Offset: 0x00002393
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000419B File Offset: 0x0000239B
		public void Disconnect()
		{
			if (this.should_close_handle)
			{
				Stdlib.fclose(this.handle.DangerousGetHandle());
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00003C4C File Offset: 0x00001E4C
		public void WaitForConnection()
		{
		}

		// Token: 0x04000233 RID: 563
		private SafePipeHandle handle;

		// Token: 0x04000234 RID: 564
		private bool should_close_handle;
	}
}
