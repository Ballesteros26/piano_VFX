using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace System.Net
{
	// Token: 0x02000490 RID: 1168
	internal static class GlobalLog
	{
		// Token: 0x06002262 RID: 8802 RVA: 0x00085F0D File Offset: 0x0008410D
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		private static BaseLoggingObject LoggingInitialize()
		{
			return new BaseLoggingObject();
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x00004240 File Offset: 0x00002440
		internal static ThreadKinds CurrentThreadKind
		{
			get
			{
				return ThreadKinds.Unknown;
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void SetThreadSource(ThreadKinds source)
		{
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, string errorMsg)
		{
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x00085F14 File Offset: 0x00084114
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, ThreadKinds allowedSources, string errorMsg)
		{
			if ((kind & ThreadKinds.SourceMask) != ThreadKinds.Unknown || (allowedSources & ThreadKinds.SourceMask) != allowedSources)
			{
				throw new InternalException();
			}
			ThreadKinds currentThreadKind = GlobalLog.CurrentThreadKind;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void AddToArray(string msg)
		{
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Ignore(object msg)
		{
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Print(string msg)
		{
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void PrintHex(string msg, object value)
		{
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Enter(string func)
		{
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Enter(string func, string parms)
		{
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x00085F38 File Offset: 0x00084138
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Assert(bool condition, string messageFormat, params object[] data)
		{
			if (!condition)
			{
				string text = string.Format(CultureInfo.InvariantCulture, messageFormat, data);
				int num = text.IndexOf('|');
				if (num != -1)
				{
					int length = text.Length;
				}
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("_FORCE_ASSERTS")]
		public static void Assert(string message)
		{
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x00085F6C File Offset: 0x0008416C
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("_FORCE_ASSERTS")]
		public static void Assert(string message, string detailMessage)
		{
			try
			{
				GlobalLog.Logobject.DumpArray(false);
			}
			finally
			{
				Debugger.Break();
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void LeaveException(string func, Exception exception)
		{
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Leave(string func)
		{
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Leave(string func, string result)
		{
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Leave(string func, int returnval)
		{
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Leave(string func, bool returnval)
		{
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void DumpArray()
		{
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer)
		{
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		public static void Dump(IntPtr buffer, int offset, int length)
		{
		}

		// Token: 0x04001F0B RID: 7947
		private static BaseLoggingObject Logobject = GlobalLog.LoggingInitialize();
	}
}
