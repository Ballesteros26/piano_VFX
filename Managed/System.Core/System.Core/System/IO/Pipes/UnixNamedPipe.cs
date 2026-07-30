using System;
using Microsoft.Win32.SafeHandles;
using Mono.Unix.Native;

namespace System.IO.Pipes
{
	// Token: 0x0200003F RID: 63
	internal abstract class UnixNamedPipe : IPipe
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000145 RID: 325
		public abstract SafePipeHandle Handle { get; }

		// Token: 0x06000146 RID: 326 RVA: 0x0000227E File Offset: 0x0000047E
		public void WaitForPipeDrain()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00003E34 File Offset: 0x00002034
		public void EnsureTargetFile(string name)
		{
			if (!File.Exists(name))
			{
				int num = Syscall.mknod(name, FilePermissions.S_ISUID | FilePermissions.S_ISGID | FilePermissions.S_ISVTX | FilePermissions.S_IRUSR | FilePermissions.S_IWUSR | FilePermissions.S_IXUSR | FilePermissions.S_IRGRP | FilePermissions.S_IWGRP | FilePermissions.S_IXGRP | FilePermissions.S_IROTH | FilePermissions.S_IWOTH | FilePermissions.S_IXOTH | FilePermissions.S_IFIFO, 0UL);
				if (num != 0)
				{
					throw new IOException(string.Format("Error on creating named pipe: error code {0}", num));
				}
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00003E70 File Offset: 0x00002070
		protected void ValidateOptions(PipeOptions options, PipeTransmissionMode mode)
		{
			if ((options & PipeOptions.WriteThrough) != PipeOptions.None)
			{
				throw new NotImplementedException("WriteThrough is not supported");
			}
			if ((mode & PipeTransmissionMode.Message) != PipeTransmissionMode.Byte)
			{
				throw new NotImplementedException("Message transmission mode is not supported");
			}
			if ((options & PipeOptions.Asynchronous) != PipeOptions.None)
			{
				throw new NotImplementedException("Asynchronous pipe mode is not supported");
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00003EAC File Offset: 0x000020AC
		protected string RightsToAccess(PipeAccessRights rights)
		{
			string text;
			if ((rights & PipeAccessRights.ReadData) != (PipeAccessRights)0)
			{
				if ((rights & PipeAccessRights.WriteData) != (PipeAccessRights)0)
				{
					text = "r+";
				}
				else
				{
					text = "r";
				}
			}
			else
			{
				if ((rights & PipeAccessRights.WriteData) == (PipeAccessRights)0)
				{
					throw new InvalidOperationException("The pipe must be opened to either read or write");
				}
				text = "w";
			}
			return text;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00003EEE File Offset: 0x000020EE
		protected FileAccess RightsToFileAccess(PipeAccessRights rights)
		{
			if ((rights & PipeAccessRights.ReadData) != (PipeAccessRights)0)
			{
				if ((rights & PipeAccessRights.WriteData) != (PipeAccessRights)0)
				{
					return FileAccess.ReadWrite;
				}
				return FileAccess.Read;
			}
			else
			{
				if ((rights & PipeAccessRights.WriteData) != (PipeAccessRights)0)
				{
					return FileAccess.Write;
				}
				throw new InvalidOperationException("The pipe must be opened to either read or write");
			}
		}
	}
}
