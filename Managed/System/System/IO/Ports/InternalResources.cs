using System;

namespace System.IO.Ports
{
	// Token: 0x020003EE RID: 1006
	internal static class InternalResources
	{
		// Token: 0x06001E73 RID: 7795 RVA: 0x000793BF File Offset: 0x000775BF
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(global::SR.GetString("Unable to read beyond the end of the stream."));
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000793D0 File Offset: 0x000775D0
		internal static string GetMessage(int errorCode)
		{
			return global::SR.GetString("Unknown Error '{0}'.", new object[] { errorCode });
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000793EB File Offset: 0x000775EB
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, global::SR.GetString("The port is closed."));
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000793FD File Offset: 0x000775FD
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(global::SR.GetString("IAsyncResult object did not come from the corresponding async method on this type."));
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0007940E File Offset: 0x0007760E
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(global::SR.GetString("EndRead can only be called once for each asynchronous operation."));
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0007941F File Offset: 0x0007761F
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(global::SR.GetString("EndWrite can only be called once for each asynchronous operation."));
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00079430 File Offset: 0x00077630
		internal static void WinIOError(int errorCode, string str)
		{
			if (errorCode <= 5)
			{
				if (errorCode - 2 > 1)
				{
					if (errorCode == 5)
					{
						if (str.Length == 0)
						{
							throw new UnauthorizedAccessException(global::SR.GetString("Access to the port is denied."));
						}
						throw new UnauthorizedAccessException(global::SR.GetString("Access to the port '{0}' is denied.", new object[] { str }));
					}
				}
				else
				{
					if (str.Length == 0)
					{
						throw new IOException(global::SR.GetString("The specified port does not exist."));
					}
					throw new IOException(global::SR.GetString("The port '{0}' does not exist.", new object[] { str }));
				}
			}
			else if (errorCode != 32)
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(global::SR.GetString("The specified port name is too long.  The port name must be less than 260 characters."));
				}
			}
			else
			{
				if (str.Length == 0)
				{
					throw new IOException(global::SR.GetString("The process cannot access the port because it is being used by another process."));
				}
				throw new IOException(global::SR.GetString("The process cannot access the port '{0}' because it is being used by another process.", new object[] { str }));
			}
			throw new IOException(InternalResources.GetMessage(errorCode), InternalResources.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0007951C File Offset: 0x0007771C
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}
	}
}
