using System;

namespace UnityEngine
{
	// Token: 0x020001CC RID: 460
	public sealed class WaitUntil : CustomYieldInstruction
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0002193C File Offset: 0x0001FB3C
		public override bool keepWaiting
		{
			get
			{
				return !this.m_Predicate.Invoke();
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0002195C File Offset: 0x0001FB5C
		public WaitUntil(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x04000685 RID: 1669
		private Func<bool> m_Predicate;
	}
}
