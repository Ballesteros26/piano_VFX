using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000140 RID: 320
	internal abstract class EventCallbackFunctorBase
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00023FCB File Offset: 0x000221CB
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x00023FD3 File Offset: 0x000221D3
		public CallbackPhase phase { get; private set; }

		// Token: 0x06000921 RID: 2337 RVA: 0x00023FDC File Offset: 0x000221DC
		protected EventCallbackFunctorBase(CallbackPhase phase)
		{
			this.phase = phase;
		}

		// Token: 0x06000922 RID: 2338
		public abstract void Invoke(EventBase evt);

		// Token: 0x06000923 RID: 2339
		public abstract bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase);

		// Token: 0x06000924 RID: 2340 RVA: 0x00023FF0 File Offset: 0x000221F0
		protected bool PhaseMatches(EventBase evt)
		{
			CallbackPhase phase = this.phase;
			if (phase != CallbackPhase.TargetAndBubbleUp)
			{
				if (phase == CallbackPhase.TrickleDownAndTarget)
				{
					bool flag = evt.propagationPhase != PropagationPhase.TrickleDown && evt.propagationPhase != PropagationPhase.AtTarget;
					if (flag)
					{
						return false;
					}
				}
			}
			else
			{
				bool flag2 = evt.propagationPhase != PropagationPhase.AtTarget && evt.propagationPhase != PropagationPhase.BubbleUp;
				if (flag2)
				{
					return false;
				}
			}
			return true;
		}
	}
}
