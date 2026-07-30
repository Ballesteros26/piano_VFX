using System;
using System.Collections;
using System.Diagnostics;

namespace System.Web.Util
{
	// Token: 0x02000151 RID: 337
	internal class WebTrace
	{
		// Token: 0x06000F0D RID: 3853 RVA: 0x0002B03B File Offset: 0x0002923B
		[Conditional("WEBTRACE")]
		public static void PushContext(string context)
		{
			WebTrace.ctxStack.Push(context);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002B048 File Offset: 0x00029248
		[Conditional("WEBTRACE")]
		public static void PopContext()
		{
			if (WebTrace.ctxStack.Count == 0)
			{
				return;
			}
			WebTrace.ctxStack.Pop();
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0002B062 File Offset: 0x00029262
		public static string Context
		{
			get
			{
				if (WebTrace.ctxStack.Count == 0)
				{
					return "No context";
				}
				return (string)WebTrace.ctxStack.Peek();
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0002B085 File Offset: 0x00029285
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x0002B08C File Offset: 0x0002928C
		public static bool StackTrace
		{
			get
			{
				return WebTrace.trace;
			}
			set
			{
				WebTrace.trace = value;
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg)
		{
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg, object arg)
		{
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg, object arg1, object arg2)
		{
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg, params object[] args)
		{
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0002B094 File Offset: 0x00029294
		private static string Format(string msg)
		{
			if (WebTrace.trace)
			{
				return string.Format("{0}: {1}\n{2}", WebTrace.Context, msg, Environment.StackTrace);
			}
			return string.Format("{0}: {1}", WebTrace.Context, msg);
		}

		// Token: 0x04001224 RID: 4644
		private static Stack ctxStack = new Stack();

		// Token: 0x04001225 RID: 4645
		private static bool trace;
	}
}
