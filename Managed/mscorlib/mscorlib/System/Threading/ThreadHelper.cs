using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000483 RID: 1155
	internal class ThreadHelper
	{
		// Token: 0x06003660 RID: 13920 RVA: 0x000C8057 File Offset: 0x000C6257
		internal ThreadHelper(Delegate start)
		{
			this._start = start;
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000C8066 File Offset: 0x000C6266
		internal void SetExecutionContextHelper(ExecutionContext ec)
		{
			this._executionContext = ec;
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000C8070 File Offset: 0x000C6270
		[SecurityCritical]
		private static void ThreadStart_Context(object state)
		{
			ThreadHelper threadHelper = (ThreadHelper)state;
			if (threadHelper._start is ThreadStart)
			{
				((ThreadStart)threadHelper._start)();
				return;
			}
			((ParameterizedThreadStart)threadHelper._start)(threadHelper._startArg);
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000C80B8 File Offset: 0x000C62B8
		[SecurityCritical]
		internal void ThreadStart(object obj)
		{
			this._startArg = obj;
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ParameterizedThreadStart)this._start)(obj);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000C80EC File Offset: 0x000C62EC
		[SecurityCritical]
		internal void ThreadStart()
		{
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ThreadStart)this._start)();
		}

		// Token: 0x04001CCE RID: 7374
		private Delegate _start;

		// Token: 0x04001CCF RID: 7375
		private object _startArg;

		// Token: 0x04001CD0 RID: 7376
		private ExecutionContext _executionContext;

		// Token: 0x04001CD1 RID: 7377
		[SecurityCritical]
		internal static ContextCallback _ccb = new ContextCallback(ThreadHelper.ThreadStart_Context);
	}
}
