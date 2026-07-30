using System;
using System.Security;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000384 RID: 900
	internal static class __Error
	{
		// Token: 0x060029BB RID: 10683 RVA: 0x00093FF9 File Offset: 0x000921F9
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(Environment.GetResourceString("Unable to read beyond the end of the stream."));
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x0009400A File Offset: 0x0009220A
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed file."));
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x0009401C File Offset: 0x0009221C
		internal static void StreamIsClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed Stream."));
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x0009402E File Offset: 0x0009222E
		internal static void MemoryStreamNotExpandable()
		{
			throw new NotSupportedException(Environment.GetResourceString("Memory stream is not expandable."));
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0009403F File Offset: 0x0009223F
		internal static void ReaderClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot read from a closed TextReader."));
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00094051 File Offset: 0x00092251
		internal static void ReadNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00094062 File Offset: 0x00092262
		internal static void SeekNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00094073 File Offset: 0x00092273
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(Environment.GetResourceString("IAsyncResult object did not come from the corresponding async method on this type."));
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00094084 File Offset: 0x00092284
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(Environment.GetResourceString("EndRead can only be called once for each asynchronous operation."));
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00094095 File Offset: 0x00092295
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(Environment.GetResourceString("EndWrite can only be called once for each asynchronous operation."));
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000940A8 File Offset: 0x000922A8
		[SecurityCritical]
		internal static string GetDisplayablePath(string path, bool isInvalidPath)
		{
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			if (path.Length < 2)
			{
				return path;
			}
			if (PathInternal.IsPartiallyQualified(path) && !isInvalidPath)
			{
				return path;
			}
			bool flag = false;
			try
			{
				if (!isInvalidPath)
				{
					flag = true;
				}
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			if (!flag)
			{
				if (Path.IsDirectorySeparator(path[path.Length - 1]))
				{
					path = Environment.GetResourceString("<Path discovery permission to the specified directory was denied.>");
				}
				else
				{
					path = Path.GetFileName(path);
				}
			}
			return path;
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00094144 File Offset: 0x00092344
		[SecurityCritical]
		internal static void WinIOError(int errorCode, string maybeFullPath)
		{
			bool flag = errorCode == 123 || errorCode == 161;
			string displayablePath = __Error.GetDisplayablePath(maybeFullPath, flag);
			if (errorCode <= 80)
			{
				if (errorCode <= 15)
				{
					switch (errorCode)
					{
					case 2:
						if (displayablePath.Length == 0)
						{
							throw new FileNotFoundException(Environment.GetResourceString("Unable to find the specified file."));
						}
						throw new FileNotFoundException(Environment.GetResourceString("Could not find file '{0}'.", new object[] { displayablePath }), displayablePath);
					case 3:
						if (displayablePath.Length == 0)
						{
							throw new DirectoryNotFoundException(Environment.GetResourceString("Could not find a part of the path."));
						}
						throw new DirectoryNotFoundException(Environment.GetResourceString("Could not find a part of the path '{0}'.", new object[] { displayablePath }));
					case 4:
						break;
					case 5:
						if (displayablePath.Length == 0)
						{
							throw new UnauthorizedAccessException(Environment.GetResourceString("Access to the path is denied."));
						}
						throw new UnauthorizedAccessException(Environment.GetResourceString("Access to the path '{0}' is denied.", new object[] { displayablePath }));
					default:
						if (errorCode == 15)
						{
							throw new DriveNotFoundException(Environment.GetResourceString("Could not find the drive '{0}'. The drive might not be ready or might not be mapped.", new object[] { displayablePath }));
						}
						break;
					}
				}
				else if (errorCode != 32)
				{
					if (errorCode == 80)
					{
						if (displayablePath.Length != 0)
						{
							throw new IOException(Environment.GetResourceString("The file '{0}' already exists.", new object[] { displayablePath }), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
						}
					}
				}
				else
				{
					if (displayablePath.Length == 0)
					{
						throw new IOException(Environment.GetResourceString("The process cannot access the file because it is being used by another process."), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
					}
					throw new IOException(Environment.GetResourceString("The process cannot access the file '{0}' because it is being used by another process.", new object[] { displayablePath }), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
				}
			}
			else if (errorCode <= 183)
			{
				if (errorCode == 87)
				{
					throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
				}
				if (errorCode == 183)
				{
					if (displayablePath.Length != 0)
					{
						throw new IOException(Environment.GetResourceString("Cannot create \"{0}\" because a file or directory with the same name already exists.", new object[] { displayablePath }), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
					}
				}
			}
			else
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(Environment.GetResourceString("The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters."));
				}
				if (errorCode == 995)
				{
					throw new OperationCanceledException();
				}
			}
			throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x0009436A File Offset: 0x0009256A
		internal static void WriteNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support writing."));
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x0009437B File Offset: 0x0009257B
		internal static void WriterClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot write to a closed TextWriter."));
		}
	}
}
