using System;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001B RID: 27
	internal class ErrorMarshal
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00005904 File Offset: 0x00003B04
		static ErrorMarshal()
		{
			try
			{
				ErrorMarshal.Translate = new ErrorMarshal.ErrorTranslator(ErrorMarshal.strerror_r);
				ErrorMarshal.Translate(Errno.ERANGE);
			}
			catch (EntryPointNotFoundException)
			{
				ErrorMarshal.Translate = new ErrorMarshal.ErrorTranslator(ErrorMarshal.strerror);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005958 File Offset: 0x00003B58
		private static string strerror(Errno errno)
		{
			return Stdlib.strerror(errno);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005960 File Offset: 0x00003B60
		private static string strerror_r(Errno errno)
		{
			StringBuilder stringBuilder = new StringBuilder(16);
			int num;
			do
			{
				stringBuilder.Capacity *= 2;
				num = Syscall.strerror_r(errno, stringBuilder);
			}
			while (num == -1 && Stdlib.GetLastError() == Errno.ERANGE);
			if (num == -1)
			{
				return "** Unknown error code: " + (int)errno + "**";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400007B RID: 123
		internal static readonly ErrorMarshal.ErrorTranslator Translate;

		// Token: 0x020000A2 RID: 162
		// (Invoke) Token: 0x06000762 RID: 1890
		internal delegate string ErrorTranslator(Errno errno);
	}
}
