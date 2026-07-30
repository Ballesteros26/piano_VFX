using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000141 RID: 321
	internal class EventCallbackFunctor<TEventType> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x0002405A File Offset: 0x0002225A
		public EventCallbackFunctor(EventCallback<TEventType> callback, CallbackPhase phase)
			: base(phase)
		{
			this.m_Callback = callback;
			this.m_EventTypeId = EventBase<TEventType>.TypeId();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00024078 File Offset: 0x00022278
		public override void Invoke(EventBase evt)
		{
			bool flag = evt == null;
			if (flag)
			{
				throw new ArgumentNullException("evt");
			}
			bool flag2 = evt.eventTypeId != this.m_EventTypeId;
			if (!flag2)
			{
				bool flag3 = base.PhaseMatches(evt);
				if (flag3)
				{
					using (new EventDebuggerLogCall(this.m_Callback, evt))
					{
						this.m_Callback(evt as TEventType);
					}
				}
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00024108 File Offset: 0x00022308
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.m_EventTypeId == eventTypeId && this.m_Callback == callback && base.phase == phase;
		}

		// Token: 0x0400040B RID: 1035
		private readonly EventCallback<TEventType> m_Callback;

		// Token: 0x0400040C RID: 1036
		private readonly long m_EventTypeId;
	}
}
