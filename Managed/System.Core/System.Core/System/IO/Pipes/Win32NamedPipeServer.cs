using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200004D RID: 77
	internal class Win32NamedPipeServer : Win32NamedPipe, INamedPipeServer, IPipe
	{
		// Token: 0x06000177 RID: 375 RVA: 0x000044BA File Offset: 0x000026BA
		public Win32NamedPipeServer(NamedPipeServerStream owner, SafePipeHandle safePipeHandle)
		{
			this.handle = safePipeHandle;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000044CC File Offset: 0x000026CC
		public unsafe Win32NamedPipeServer(NamedPipeServerStream owner, string pipeName, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeAccessRights rights, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability)
		{
			string text = string.Format("\\\\.\\pipe\\{0}", pipeName);
			uint num = (uint)(rights | (PipeAccessRights)options);
			int num2 = 0;
			if ((owner.TransmissionMode & PipeTransmissionMode.Message) != PipeTransmissionMode.Byte)
			{
				num2 |= 4;
			}
			if ((options & PipeOptions.Asynchronous) != PipeOptions.None)
			{
				num2 |= 1;
			}
			byte[] array = null;
			if (pipeSecurity != null)
			{
				array = pipeSecurity.GetSecurityDescriptorBinaryForm();
			}
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			SecurityAttributes securityAttributes = new SecurityAttributes(inheritability, (IntPtr)((void*)ptr));
			IntPtr intPtr = Win32Marshal.CreateNamedPipe(text, num, num2, maxNumberOfServerInstances, outBufferSize, inBufferSize, 0, ref securityAttributes, IntPtr.Zero);
			if (intPtr == new IntPtr(-1L))
			{
				throw Win32PipeError.GetException();
			}
			this.handle = new SafePipeHandle(intPtr, true);
			array2 = null;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00004583 File Offset: 0x00002783
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000458B File Offset: 0x0000278B
		public void Disconnect()
		{
			Win32Marshal.DisconnectNamedPipe(this.Handle);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004599 File Offset: 0x00002799
		public void WaitForConnection()
		{
			if (!Win32Marshal.ConnectNamedPipe(this.Handle, IntPtr.Zero))
			{
				throw Win32PipeError.GetException();
			}
		}

		// Token: 0x04000242 RID: 578
		private SafePipeHandle handle;
	}
}
