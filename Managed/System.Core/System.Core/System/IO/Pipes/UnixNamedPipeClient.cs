using System;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000040 RID: 64
	internal class UnixNamedPipeClient : UnixNamedPipe, INamedPipeClient, IPipe
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00003F0F File Offset: 0x0000210F
		public UnixNamedPipeClient(NamedPipeClientStream owner, SafePipeHandle safePipeHandle)
		{
			this.owner = owner;
			this.handle = safePipeHandle;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003F28 File Offset: 0x00002128
		public UnixNamedPipeClient(NamedPipeClientStream owner, string serverName, string pipeName, PipeAccessRights desiredAccessRights, PipeOptions options, HandleInheritability inheritability)
		{
			UnixNamedPipeClient.<>c__DisplayClass1_0 CS$<>8__locals1 = new UnixNamedPipeClient.<>c__DisplayClass1_0();
			CS$<>8__locals1.desiredAccessRights = desiredAccessRights;
			CS$<>8__locals1.owner = owner;
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			this.owner = CS$<>8__locals1.owner;
			if (serverName != "." && !Dns.GetHostEntry(serverName).AddressList.Contains(IPAddress.Loopback))
			{
				throw new NotImplementedException("Unix fifo does not support remote server connection");
			}
			string name = Path.Combine("/var/tmp/", pipeName);
			base.EnsureTargetFile(name);
			base.RightsToAccess(CS$<>8__locals1.desiredAccessRights);
			base.ValidateOptions(options, CS$<>8__locals1.owner.TransmissionMode);
			this.opener = delegate
			{
				FileStream fileStream = new FileStream(name, FileMode.Open, CS$<>8__locals1.<>4__this.RightsToFileAccess(CS$<>8__locals1.desiredAccessRights), FileShare.ReadWrite);
				CS$<>8__locals1.owner.Stream = fileStream;
				CS$<>8__locals1.<>4__this.handle = new SafePipeHandle(fileStream.SafeFileHandle.DangerousGetHandle(), false);
			};
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00003FFD File Offset: 0x000021FD
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004005 File Offset: 0x00002205
		public void Connect()
		{
			if (this.owner.IsConnected)
			{
				throw new InvalidOperationException("The named pipe is already connected");
			}
			this.opener();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000402C File Offset: 0x0000222C
		public void Connect(int timeout)
		{
			AutoResetEvent waitHandle = new AutoResetEvent(false);
			this.opener.BeginInvoke(delegate(IAsyncResult result)
			{
				this.opener.EndInvoke(result);
				waitHandle.Set();
			}, null);
			if (!waitHandle.WaitOne(TimeSpan.FromMilliseconds((double)timeout)))
			{
				throw new TimeoutException();
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002285 File Offset: 0x00000485
		public bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000227E File Offset: 0x0000047E
		public int NumberOfServerInstances
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000229 RID: 553
		private NamedPipeClientStream owner;

		// Token: 0x0400022A RID: 554
		private SafePipeHandle handle;

		// Token: 0x0400022B RID: 555
		private Action opener;
	}
}
