using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001A RID: 26
	public sealed class EventDispatcher
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003709 File Offset: 0x00001909
		internal PointerDispatchState pointerState { get; } = new PointerDispatchState();

		// Token: 0x0600007E RID: 126 RVA: 0x00003714 File Offset: 0x00001914
		internal EventDispatcher()
		{
			this.m_DispatchingStrategies = new List<IEventDispatchingStrategy>();
			this.m_DispatchingStrategies.Add(new PointerCaptureDispatchingStrategy());
			this.m_DispatchingStrategies.Add(new MouseCaptureDispatchingStrategy());
			this.m_DispatchingStrategies.Add(new KeyboardEventDispatchingStrategy());
			this.m_DispatchingStrategies.Add(new PointerEventDispatchingStrategy());
			this.m_DispatchingStrategies.Add(new MouseEventDispatchingStrategy());
			this.m_DispatchingStrategies.Add(new CommandEventDispatchingStrategy());
			this.m_DispatchingStrategies.Add(new IMGUIEventDispatchingStrategy());
			this.m_DispatchingStrategies.Add(new DefaultDispatchingStrategy());
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000037F4 File Offset: 0x000019F4
		private bool dispatchImmediately
		{
			get
			{
				return this.m_Immediate || this.m_GateCount == 0U;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000381C File Offset: 0x00001A1C
		internal void Dispatch(EventBase evt, IPanel panel, DispatchMode dispatchMode)
		{
			evt.MarkReceivedByDispatcher();
			bool flag = evt.eventTypeId == EventBase<IMGUIEvent>.TypeId();
			if (flag)
			{
				Event imguiEvent = evt.imguiEvent;
				bool flag2 = imguiEvent.rawType == EventType.Repaint;
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.dispatchImmediately || dispatchMode == DispatchMode.Immediate;
			if (flag3)
			{
				this.ProcessEvent(evt, panel);
			}
			else
			{
				evt.Acquire();
				this.m_Queue.Enqueue(new EventDispatcher.EventRecord
				{
					m_Event = evt,
					m_Panel = panel
				});
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038AC File Offset: 0x00001AAC
		internal void PushDispatcherContext()
		{
			this.ProcessEventQueue();
			this.m_DispatchContexts.Push(new EventDispatcher.DispatchContext
			{
				m_GateCount = this.m_GateCount,
				m_Queue = this.m_Queue
			});
			this.m_GateCount = 0U;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003908 File Offset: 0x00001B08
		internal void PopDispatcherContext()
		{
			Debug.Assert(this.m_GateCount == 0U, "All gates should have been opened before popping dispatch context.");
			Debug.Assert(this.m_Queue.Count == 0, "Queue should be empty when popping dispatch context.");
			EventDispatcher.k_EventQueuePool.Release(this.m_Queue);
			this.m_GateCount = this.m_DispatchContexts.Peek().m_GateCount;
			this.m_Queue = this.m_DispatchContexts.Peek().m_Queue;
			this.m_DispatchContexts.Pop();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000398C File Offset: 0x00001B8C
		internal void CloseGate()
		{
			this.m_GateCount += 1U;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000039A0 File Offset: 0x00001BA0
		internal void OpenGate()
		{
			Debug.Assert(this.m_GateCount > 0U);
			bool flag = this.m_GateCount > 0U;
			if (flag)
			{
				this.m_GateCount -= 1U;
			}
			bool flag2 = this.m_GateCount == 0U;
			if (flag2)
			{
				this.ProcessEventQueue();
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000039F0 File Offset: 0x00001BF0
		private void ProcessEventQueue()
		{
			Queue<EventDispatcher.EventRecord> queue = this.m_Queue;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
			ExitGUIException ex = null;
			try
			{
				while (queue.Count > 0)
				{
					EventDispatcher.EventRecord eventRecord = queue.Dequeue();
					EventBase @event = eventRecord.m_Event;
					IPanel panel = eventRecord.m_Panel;
					try
					{
						this.ProcessEvent(@event, panel);
					}
					catch (ExitGUIException ex2)
					{
						Debug.Assert(ex == null);
						ex = ex2;
					}
					finally
					{
						@event.Dispose();
					}
				}
			}
			finally
			{
				EventDispatcher.k_EventQueuePool.Release(queue);
			}
			bool flag = ex != null;
			if (flag)
			{
				throw ex;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003AB8 File Offset: 0x00001CB8
		private void ProcessEvent(EventBase evt, IPanel panel)
		{
			Event imguiEvent = evt.imguiEvent;
			bool flag = imguiEvent != null && imguiEvent.rawType == EventType.Used;
			using (new EventDispatcherGate(this))
			{
				evt.PreDispatch(panel);
				bool flag2 = !evt.stopDispatch && !evt.isPropagationStopped;
				if (flag2)
				{
					this.ApplyDispatchingStrategies(evt, panel, flag);
				}
				bool flag3 = evt.path != null;
				if (flag3)
				{
					foreach (VisualElement visualElement in evt.path.targetElements)
					{
						evt.target = visualElement;
						EventDispatchUtilities.ExecuteDefaultAction(evt, panel);
					}
					evt.target = evt.leafTarget;
				}
				else
				{
					EventDispatchUtilities.ExecuteDefaultAction(evt, panel);
				}
				evt.PostDispatch(panel);
				this.m_ClickDetector.ProcessEvent(evt);
				Debug.Assert(flag || evt.isPropagationStopped || imguiEvent == null || imguiEvent.rawType != EventType.Used, "Event is used but not stopped.");
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003BF4 File Offset: 0x00001DF4
		private void ApplyDispatchingStrategies(EventBase evt, IPanel panel, bool imguiEventIsInitiallyUsed)
		{
			foreach (IEventDispatchingStrategy eventDispatchingStrategy in this.m_DispatchingStrategies)
			{
				bool flag = eventDispatchingStrategy.CanDispatchEvent(evt);
				if (flag)
				{
					eventDispatchingStrategy.DispatchEvent(evt, panel);
					Debug.Assert(imguiEventIsInitiallyUsed || evt.isPropagationStopped || evt.imguiEvent == null || evt.imguiEvent.rawType != EventType.Used, "Unexpected condition: !evt.isPropagationStopped && evt.imguiEvent.rawType == EventType.Used.");
					bool flag2 = evt.stopDispatch || evt.isPropagationStopped;
					if (flag2)
					{
						break;
					}
				}
			}
		}

		// Token: 0x04000039 RID: 57
		private ClickDetector m_ClickDetector = new ClickDetector();

		// Token: 0x0400003A RID: 58
		private List<IEventDispatchingStrategy> m_DispatchingStrategies;

		// Token: 0x0400003B RID: 59
		private static readonly ObjectPool<Queue<EventDispatcher.EventRecord>> k_EventQueuePool = new ObjectPool<Queue<EventDispatcher.EventRecord>>(100);

		// Token: 0x0400003C RID: 60
		private Queue<EventDispatcher.EventRecord> m_Queue;

		// Token: 0x0400003E RID: 62
		private uint m_GateCount;

		// Token: 0x0400003F RID: 63
		private Stack<EventDispatcher.DispatchContext> m_DispatchContexts = new Stack<EventDispatcher.DispatchContext>();

		// Token: 0x04000040 RID: 64
		private bool m_Immediate = false;

		// Token: 0x0200001B RID: 27
		private struct EventRecord
		{
			// Token: 0x04000041 RID: 65
			public EventBase m_Event;

			// Token: 0x04000042 RID: 66
			public IPanel m_Panel;
		}

		// Token: 0x0200001C RID: 28
		private struct DispatchContext
		{
			// Token: 0x04000043 RID: 67
			public uint m_GateCount;

			// Token: 0x04000044 RID: 68
			public Queue<EventDispatcher.EventRecord> m_Queue;
		}
	}
}
