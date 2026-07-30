using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Melanchall.DryWetMidi.Common;
using Microsoft.Win32.SafeHandles;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200019B RID: 411
	internal static class FileUtilities
	{
		// Token: 0x060009F0 RID: 2544
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x060009F1 RID: 2545 RVA: 0x00021E34 File Offset: 0x00020034
		internal static FileStream OpenFileForRead(string filePath)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("filePath", filePath, "File path");
			FileStream fileStream;
			try
			{
				fileStream = File.OpenRead(filePath);
			}
			catch (PathTooLongException)
			{
				fileStream = new FileStream(FileUtilities.GetFileHandle(filePath, 2147483648U, 3U), FileAccess.Read);
			}
			return fileStream;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00021E84 File Offset: 0x00020084
		internal static FileStream OpenFileForWrite(string filePath, bool overwriteFile)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("filePath", filePath, "File path");
			FileStream fileStream;
			try
			{
				fileStream = File.Open(filePath, overwriteFile ? FileMode.Create : FileMode.CreateNew);
			}
			catch (PathTooLongException)
			{
				fileStream = new FileStream(FileUtilities.GetFileHandle(filePath, 1073741824U, overwriteFile ? 2U : 1U), FileAccess.Write);
			}
			return fileStream;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00021EE0 File Offset: 0x000200E0
		private static SafeFileHandle GetFileHandle(string filePath, uint fileAccess, uint creationDisposition)
		{
			SafeFileHandle safeFileHandle = FileUtilities.CreateFile("\\\\?\\" + filePath, fileAccess, 0U, IntPtr.Zero, creationDisposition, 0U, IntPtr.Zero);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (safeFileHandle.IsInvalid)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return safeFileHandle;
		}

		// Token: 0x0400094D RID: 2381
		private const uint GENERIC_READ = 2147483648U;

		// Token: 0x0400094E RID: 2382
		private const uint GENERIC_WRITE = 1073741824U;

		// Token: 0x0400094F RID: 2383
		private const uint CREATE_NEW = 1U;

		// Token: 0x04000950 RID: 2384
		private const uint CREATE_ALWAYS = 2U;

		// Token: 0x04000951 RID: 2385
		private const uint OPEN_EXISTING = 3U;

		// Token: 0x04000952 RID: 2386
		private const uint FILE_SHARE_NONE = 0U;
	}
}
