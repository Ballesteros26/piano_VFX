using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000147 RID: 327
	internal class EventCallbackRegistry
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x000245C8 File Offset: 0x000227C8
		private static EventCallbackList GetCallbackList(EventCallbackList initializer = null)
		{
			return EventCallbackRegistry.s_ListPool.Get(initializer);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x000245E5 File Offset: 0x000227E5
		private static void ReleaseCallbackList(EventCallbackList toRelease)
		{
			EventCallbackRegistry.s_ListPool.Release(toRelease);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000245F4 File Offset: 0x000227F4
		public EventCallbackRegistry()
		{
			this.m_IsInvoking = 0;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00024608 File Offset: 0x00022808
		private EventCallbackList GetCallbackListForWriting()
		{
			bool flag = this.m_IsInvoking > 0;
			EventCallbackList eventCallbackList;
			if (flag)
			{
				bool flag2 = this.m_TemporaryCallbacks == null;
				if (flag2)
				{
					bool flag3 = this.m_Callbacks != null;
					if (flag3)
					{
						this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(this.m_Callbacks);
					}
					else
					{
						this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(null);
					}
				}
				eventCallbackList = this.m_TemporaryCallbacks;
			}
			else
			{
				bool flag4 = this.m_Callbacks == null;
				if (flag4)
				{
					this.m_Callbacks = EventCallbackRegistry.GetCallbackList(null);
				}
				eventCallbackList = this.m_Callbacks;
			}
			return eventCallbackList;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00024694 File Offset: 0x00022894
		private EventCallbackList GetCallbackListForReading()
		{
			bool flag = this.m_TemporaryCallbacks != null;
			EventCallbackList eventCallbackList;
			if (flag)
			{
				eventCallbackList = this.m_TemporaryCallbacks;
			}
			else
			{
				eventCallbackList = this.m_Callbacks;
			}
			return eventCallbackList;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000246C4 File Offset: 0x000228C4
		private bool ShouldRegisterCallback(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			bool flag = callback == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventCallbackList callbackListForReading = this.GetCallbackListForReading();
				bool flag3 = callbackListForReading != null;
				flag2 = !flag3 || !callbackListForReading.Contains(eventTypeId, callback, phase);
			}
			return flag2;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00024704 File Offset: 0x00022904
		private bool UnregisterCallback(long eventTypeId, Delegate callback, TrickleDown useTrickleDown)
		{
			bool flag = callback == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventCallbackList callbackListForWriting = this.GetCallbackListForWriting();
				CallbackPhase callbackPhase = ((useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp);
				flag2 = callbackListForWriting.Remove(eventTypeId, callback, callbackPhase);
			}
			return flag2;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0002473C File Offset: 0x0002293C
		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			long num = EventBase<TEventType>.TypeId();
			CallbackPhase callbackPhase = ((useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp);
			EventCallbackList eventCallbackList = this.GetCallbackListForReading();
			bool flag2 = eventCallbackList == null || !eventCallbackList.Contains(num, callback, callbackPhase);
			if (flag2)
			{
				eventCallbackList = this.GetCallbackListForWriting();
				eventCallbackList.Add(new EventCallbackFunctor<TEventType>(callback, callbackPhase));
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000247A4 File Offset: 0x000229A4
		public void RegisterCallback<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			long num = EventBase<TEventType>.TypeId();
			CallbackPhase callbackPhase = ((useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp);
			EventCallbackList eventCallbackList = this.GetCallbackListForReading();
			bool flag2 = eventCallbackList != null;
			if (flag2)
			{
				EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = eventCallbackList.Find(num, callback, callbackPhase) as EventCallbackFunctor<TEventType, TCallbackArgs>;
				bool flag3 = eventCallbackFunctor != null;
				if (flag3)
				{
					eventCallbackFunctor.userArgs = userArgs;
					return;
				}
			}
			eventCallbackList = this.GetCallbackListForWriting();
			eventCallbackList.Add(new EventCallbackFunctor<TEventType, TCallbackArgs>(callback, userArgs, callbackPhase));
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00024824 File Offset: 0x00022A24
		public bool UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			long num = EventBase<TEventType>.TypeId();
			return this.UnregisterCallback(num, callback, useTrickleDown);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00024848 File Offset: 0x00022A48
		public bool UnregisterCallback<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			long num = EventBase<TEventType>.TypeId();
			return this.UnregisterCallback(num, callback, useTrickleDown);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0002486C File Offset: 0x00022A6C
		internal bool TryGetUserArgs<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown, out TCallbackArgs userArgs) where TEventType : EventBase<TEventType>, new()
		{
			userArgs = default(TCallbackArgs);
			bool flag = callback == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventCallbackList callbackListForReading = this.GetCallbackListForReading();
				long num = EventBase<TEventType>.TypeId();
				CallbackPhase callbackPhase = ((useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp);
				EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = callbackListForReading.Find(num, callback, callbackPhase) as EventCallbackFunctor<TEventType, TCallbackArgs>;
				bool flag3 = eventCallbackFunctor == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					userArgs = eventCallbackFunctor.userArgs;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000248D8 File Offset: 0x00022AD8
		public void InvokeCallbacks(EventBase evt)
		{
			bool flag = this.m_Callbacks == null;
			if (!flag)
			{
				this.m_IsInvoking++;
				for (int i = 0; i < this.m_Callbacks.Count; i++)
				{
					bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
					if (isImmediatePropagationStopped)
					{
						break;
					}
					this.m_Callbacks[i].Invoke(evt);
				}
				this.m_IsInvoking--;
				bool flag2 = this.m_IsInvoking == 0;
				if (flag2)
				{
					bool flag3 = this.m_TemporaryCallbacks != null;
					if (flag3)
					{
						EventCallbackRegistry.ReleaseCallbackList(this.m_Callbacks);
						this.m_Callbacks = EventCallbackRegistry.GetCallbackList(this.m_TemporaryCallbacks);
						EventCallbackRegistry.ReleaseCallbackList(this.m_TemporaryCallbacks);
						this.m_TemporaryCallbacks = null;
					}
				}
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000249A4 File Offset: 0x00022BA4
		public bool HasTrickleDownHandlers()
		{
			return this.m_Callbacks != null && this.m_Callbacks.trickleDownCallbackCount > 0;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000249D0 File Offset: 0x00022BD0
		public bool HasBubbleHandlers()
		{
			return this.m_Callbacks != null && this.m_Callbacks.bubbleUpCallbackCount > 0;
		}

		// Token: 0x0400041A RID: 1050
		private static readonly EventCallbackListPool s_ListPool = new EventCallbackListPool();

		// Token: 0x0400041B RID: 1051
		private EventCallbackList m_Callbacks;

		// Token: 0x0400041C RID: 1052
		private EventCallbackList m_TemporaryCallbacks;

		// Token: 0x0400041D RID: 1053
		private int m_IsInvoking;
	}
}
