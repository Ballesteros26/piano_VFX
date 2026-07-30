using System;

namespace UnityEngine
{
	// Token: 0x020001CD RID: 461
	public sealed class WaitWhile : CustomYieldInstruction
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00021970 File Offset: 0x0001FB70
		public override bool keepWaiting
		{
			get
			{
				return this.m_Predicate.Invoke();
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0002198D File Offset: 0x0001FB8D
		public WaitWhile(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x04000686 RID: 1670
		private Func<bool> m_Predicate;
	}
}
