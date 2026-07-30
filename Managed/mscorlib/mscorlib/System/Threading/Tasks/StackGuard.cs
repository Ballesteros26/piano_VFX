using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000506 RID: 1286
	internal class StackGuard
	{
		// Token: 0x06003B0E RID: 15118 RVA: 0x000D619C File Offset: 0x000D439C
		[SecuritySafeCritical]
		internal bool TryBeginInliningScope()
		{
			if (this.m_inliningDepth < 20 || this.CheckForSufficientStack())
			{
				this.m_inliningDepth++;
				return true;
			}
			return false;
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000D61C1 File Offset: 0x000D43C1
		internal void EndInliningScope()
		{
			this.m_inliningDepth--;
			if (this.m_inliningDepth < 0)
			{
				this.m_inliningDepth = 0;
			}
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x00003B29 File Offset: 0x00001D29
		[SecurityCritical]
		private bool CheckForSufficientStack()
		{
			return true;
		}

		// Token: 0x04001EDD RID: 7901
		private int m_inliningDepth;

		// Token: 0x04001EDE RID: 7902
		private const int MAX_UNCHECKED_INLINING_DEPTH = 20;
	}
}
