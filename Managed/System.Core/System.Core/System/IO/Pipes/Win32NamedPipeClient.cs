using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200004A RID: 74
	internal class Win32NamedPipeClient : Win32NamedPipe, INamedPipeClient, IPipe
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00004328 File Offset: 0x00002528
		public Win32NamedPipeClient(NamedPipeClientStream owner, SafePipeHandle safePipeHandle)
		{
			this.handle = safePipeHandle;
			this.owner = owner;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004340 File Offset: 0x00002540
		public Win32NamedPipeClient(NamedPipeClientStream owner, string serverName, string pipeName, PipeAccessRights desiredAccessRights, PipeOptions options, HandleInheritability inheritability)
		{
			Win32NamedPipeClient.<>c__DisplayClass2_0 CS$<>8__locals1 = new Win32NamedPipeClient.<>c__DisplayClass2_0();
			CS$<>8__locals1.desiredAccessRights = desiredAccessRights;
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			this.name = string.Format("\\\\{0}\\pipe\\{1}", serverName, pipeName);
			SecurityAttributes att = new SecurityAttributes(inheritability, IntPtr.Zero);
			this.is_async = (options & PipeOptions.Asynchronous) > PipeOptions.None;
			this.opener = delegate
			{
				IntPtr intPtr = Win32Marshal.CreateFile(CS$<>8__locals1.<>4__this.name, CS$<>8__locals1.desiredAccessRights, FileShare.None, ref att, 3, 0, IntPtr.Zero);
				if (intPtr == new IntPtr(-1L))
				{
					throw Win32PipeError.GetException();
				}
				return new SafePipeHandle(intPtr, true);
			};
			this.owner = owner;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000043C3 File Offset: 0x000025C3
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000043CB File Offset: 0x000025CB
		public bool IsAsync
		{
			get
			{
				return this.is_async;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000043D3 File Offset: 0x000025D3
		public void Connect()
		{
			if (this.owner.IsConnected)
			{
				throw new InvalidOperationException("The named pipe is already connected");
			}
			this.handle = this.opener();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000043FE File Offset: 0x000025FE
		public void Connect(int timeout)
		{
			if (this.owner.IsConnected)
			{
				throw new InvalidOperationException("The named pipe is already connected");
			}
			if (!Win32Marshal.WaitNamedPipe(this.name, timeout))
			{
				throw Win32PipeError.GetException();
			}
			this.Connect();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00004434 File Offset: 0x00002634
		public int NumberOfServerInstances
		{
			get
			{
				byte[] array = null;
				int num;
				int num2;
				int num3;
				int num4;
				if (!Win32Marshal.GetNamedPipeHandleState(this.Handle, out num, out num2, out num3, out num4, array, 0))
				{
					throw Win32PipeError.GetException();
				}
				return num2;
			}
		}

		// Token: 0x04000239 RID: 569
		private NamedPipeClientStream owner;

		// Token: 0x0400023A RID: 570
		private Func<SafePipeHandle> opener;

		// Token: 0x0400023B RID: 571
		private bool is_async;

		// Token: 0x0400023C RID: 572
		private string name;

		// Token: 0x0400023D RID: 573
		private SafePipeHandle handle;
	}
}
