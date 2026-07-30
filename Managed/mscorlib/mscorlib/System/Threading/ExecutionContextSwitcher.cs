using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000472 RID: 1138
	internal struct ExecutionContextSwitcher
	{
		// Token: 0x060035D7 RID: 13783 RVA: 0x000C7090 File Offset: 0x000C5290
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			try
			{
				this.Undo();
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000C70C0 File Offset: 0x000C52C0
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void Undo()
		{
			if (this.thread == null)
			{
				return;
			}
			Thread thread = this.thread;
			ExecutionContext.Reader executionContextReader = thread.GetExecutionContextReader();
			thread.SetExecutionContext(this.outerEC, this.outerECBelongsToScope);
			this.thread = null;
			ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), this.outerEC.DangerousGetRawExecutionContext());
		}

		// Token: 0x04001CAC RID: 7340
		internal ExecutionContext.Reader outerEC;

		// Token: 0x04001CAD RID: 7341
		internal bool outerECBelongsToScope;

		// Token: 0x04001CAE RID: 7342
		internal object hecsw;

		// Token: 0x04001CAF RID: 7343
		internal Thread thread;
	}
}
