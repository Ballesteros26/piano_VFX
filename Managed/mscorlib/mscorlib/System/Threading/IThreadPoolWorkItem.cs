using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000488 RID: 1160
	internal interface IThreadPoolWorkItem
	{
		// Token: 0x060036FB RID: 14075
		[SecurityCritical]
		void ExecuteWorkItem();

		// Token: 0x060036FC RID: 14076
		[SecurityCritical]
		void MarkAborted(ThreadAbortException tae);
	}
}
