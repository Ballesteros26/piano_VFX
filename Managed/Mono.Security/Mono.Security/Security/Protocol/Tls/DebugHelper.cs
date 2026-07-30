using System;
using System.Diagnostics;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000038 RID: 56
	internal class DebugHelper
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000EE58 File Offset: 0x0000D058
		[Conditional("DEBUG")]
		public static void Initialize()
		{
			if (!DebugHelper.isInitialized)
			{
				Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
				Debug.AutoFlush = true;
				DebugHelper.isInitialized = true;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000EE82 File Offset: 0x0000D082
		[Conditional("DEBUG")]
		public static void WriteLine(string format, params object[] args)
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000EE84 File Offset: 0x0000D084
		[Conditional("DEBUG")]
		public static void WriteLine(string message)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000EE86 File Offset: 0x0000D086
		[Conditional("DEBUG")]
		public static void WriteLine(string message, byte[] buffer)
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000EE88 File Offset: 0x0000D088
		[Conditional("DEBUG")]
		public static void WriteBuffer(byte[] buffer)
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000EE8C File Offset: 0x0000D08C
		[Conditional("DEBUG")]
		public static void WriteBuffer(byte[] buffer, int index, int length)
		{
			for (int i = index; i < length; i += 16)
			{
				int num = ((length - i >= 16) ? 16 : (length - i));
				string text = "";
				for (int j = 0; j < num; j++)
				{
					text = text + buffer[i + j].ToString("x2") + " ";
				}
			}
		}

		// Token: 0x04000166 RID: 358
		private static bool isInitialized;
	}
}
