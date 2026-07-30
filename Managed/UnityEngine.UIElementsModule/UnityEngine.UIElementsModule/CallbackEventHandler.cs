using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000149 RID: 329
	public abstract class CallbackEventHandler : IEventHandler
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x00024A08 File Offset: 0x00022C08
		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown);
			GlobalCallbackRegistry.RegisterListeners<TEventType>(this, callback, useTrickleDown);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00024A48 File Offset: 0x00022C48
		public void RegisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TUserArgsType userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType, TUserArgsType>(callback, userArgs, useTrickleDown);
			GlobalCallbackRegistry.RegisterListeners<TEventType>(this, callback, useTrickleDown);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00024A88 File Offset: 0x00022C88
		public void UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry != null;
			if (flag)
			{
				this.m_CallbackRegistry.UnregisterCallback<TEventType>(callback, useTrickleDown);
			}
			GlobalCallbackRegistry.UnregisterListeners<TEventType>(this, callback);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00024ABC File Offset: 0x00022CBC
		public void UnregisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry != null;
			if (flag)
			{
				this.m_CallbackRegistry.UnregisterCallback<TEventType, TUserArgsType>(callback, useTrickleDown);
			}
			GlobalCallbackRegistry.UnregisterListeners<TEventType>(this, callback);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00024AF0 File Offset: 0x00022CF0
		internal bool TryGetUserArgs<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown, out TCallbackArgs userData) where TEventType : EventBase<TEventType>, new()
		{
			userData = default(TCallbackArgs);
			bool flag = this.m_CallbackRegistry != null;
			return flag && this.m_CallbackRegistry.TryGetUserArgs<TEventType, TCallbackArgs>(callback, useTrickleDown, out userData);
		}

		// Token: 0x06000958 RID: 2392
		public abstract void SendEvent(EventBase e);

		// Token: 0x06000959 RID: 2393 RVA: 0x00024B29 File Offset: 0x00022D29
		internal void HandleEventAtTargetPhase(EventBase evt)
		{
			evt.propagationPhase = PropagationPhase.AtTarget;
			this.HandleEvent(evt);
			evt.propagationPhase = PropagationPhase.DefaultActionAtTarget;
			this.HandleEvent(evt);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00024B4C File Offset: 0x00022D4C
		public virtual void HandleEvent(EventBase evt)
		{
			bool flag = evt == null;
			if (!flag)
			{
				switch (evt.propagationPhase)
				{
				case PropagationPhase.TrickleDown:
				case PropagationPhase.AtTarget:
				case PropagationPhase.BubbleUp:
				{
					bool flag2 = !evt.isPropagationStopped;
					if (flag2)
					{
						EventCallbackRegistry callbackRegistry = this.m_CallbackRegistry;
						if (callbackRegistry != null)
						{
							callbackRegistry.InvokeCallbacks(evt);
						}
					}
					break;
				}
				case PropagationPhase.DefaultAction:
				{
					bool flag3 = !evt.isDefaultPrevented;
					if (flag3)
					{
						using (new EventDebuggerLogExecuteDefaultAction(evt))
						{
							this.ExecuteDefaultAction(evt);
						}
					}
					break;
				}
				case PropagationPhase.DefaultActionAtTarget:
				{
					bool flag4 = !evt.isDefaultPrevented;
					if (flag4)
					{
						using (new EventDebuggerLogExecuteDefaultAction(evt))
						{
							this.ExecuteDefaultActionAtTarget(evt);
						}
					}
					break;
				}
				}
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00024C3C File Offset: 0x00022E3C
		public bool HasTrickleDownHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasTrickleDownHandlers();
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00024C64 File Offset: 0x00022E64
		public bool HasBubbleUpHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasBubbleHandlers();
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000062F3 File Offset: 0x000044F3
		protected virtual void ExecuteDefaultActionAtTarget(EventBase evt)
		{
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000062F3 File Offset: 0x000044F3
		protected virtual void ExecuteDefaultAction(EventBase evt)
		{
		}

		// Token: 0x0400041E RID: 1054
		private EventCallbackRegistry m_CallbackRegistry;
	}
}
