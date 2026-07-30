using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000048 RID: 72
	internal class Win32AnonymousPipeServer : Win32AnonymousPipe, IAnonymousPipeServer, IPipe
	{
		// Token: 0x06000164 RID: 356 RVA: 0x000041EC File Offset: 0x000023EC
		public unsafe Win32AnonymousPipeServer(AnonymousPipeServerStream owner, PipeDirection direction, HandleInheritability inheritability, int bufferSize, PipeSecurity pipeSecurity)
		{
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
			IntPtr intPtr;
			IntPtr intPtr2;
			if (!Win32Marshal.CreatePipe(out intPtr, out intPtr2, ref securityAttributes, bufferSize))
			{
				throw Win32PipeError.GetException();
			}
			array2 = null;
			SafePipeHandle safePipeHandle = new SafePipeHandle(intPtr, true);
			SafePipeHandle safePipeHandle2 = new SafePipeHandle(intPtr2, true);
			if (direction == PipeDirection.Out)
			{
				this.server_handle = safePipeHandle2;
				this.client_handle = safePipeHandle;
				return;
			}
			this.server_handle = safePipeHandle;
			this.client_handle = safePipeHandle2;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004285 File Offset: 0x00002485
		public Win32AnonymousPipeServer(AnonymousPipeServerStream owner, SafePipeHandle serverHandle, SafePipeHandle clientHandle)
		{
			this.server_handle = serverHandle;
			this.client_handle = clientHandle;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000429B File Offset: 0x0000249B
		public override SafePipeHandle Handle
		{
			get
			{
				return this.server_handle;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000042A3 File Offset: 0x000024A3
		public SafePipeHandle ClientHandle
		{
			get
			{
				return this.client_handle;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000227E File Offset: 0x0000047E
		public void DisposeLocalCopyOfClientHandle()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000236 RID: 566
		private SafePipeHandle server_handle;

		// Token: 0x04000237 RID: 567
		private SafePipeHandle client_handle;
	}
}
