using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000142 RID: 322
	internal class EventCallbackFunctor<TEventType, TCallbackArgs> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0002413D File Offset: 0x0002233D
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00024145 File Offset: 0x00022345
		internal TCallbackArgs userArgs { get; set; }

		// Token: 0x0600092A RID: 2346 RVA: 0x0002414E File Offset: 0x0002234E
		public EventCallbackFunctor(EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, CallbackPhase phase)
			: base(phase)
		{
			this.userArgs = userArgs;
			this.m_Callback = callback;
			this.m_EventTypeId = EventBase<TEventType>.TypeId();
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00024174 File Offset: 0x00022374
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
						this.m_Callback(evt as TEventType, this.userArgs);
					}
				}
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00024208 File Offset: 0x00022408
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.m_EventTypeId == eventTypeId && this.m_Callback == callback && base.phase == phase;
		}

		// Token: 0x0400040D RID: 1037
		private readonly EventCallback<TEventType, TCallbackArgs> m_Callback;

		// Token: 0x0400040E RID: 1038
		private readonly long m_EventTypeId;
	}
}
