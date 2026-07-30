using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides methods for enabling and disabling events from event sources.</summary>
	// Token: 0x02000B01 RID: 2817
	public class EventListener : IDisposable
	{
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06006544 RID: 25924 RVA: 0x0014CA3C File Offset: 0x0014AC3C
		// (remove) Token: 0x06006545 RID: 25925 RVA: 0x0014CA74 File Offset: 0x0014AC74
		private event EventHandler<EventSourceCreatedEventArgs> _EventSourceCreated;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06006546 RID: 25926 RVA: 0x0014CAAC File Offset: 0x0014ACAC
		// (remove) Token: 0x06006547 RID: 25927 RVA: 0x0014CB04 File Offset: 0x0014AD04
		public event EventHandler<EventSourceCreatedEventArgs> EventSourceCreated
		{
			add
			{
				object obj = EventListener.s_EventSourceCreatedLock;
				lock (obj)
				{
					this.CallBackForExistingEventSources(false, value);
					this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Combine(this._EventSourceCreated, value);
				}
			}
			remove
			{
				object obj = EventListener.s_EventSourceCreatedLock;
				lock (obj)
				{
					this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Remove(this._EventSourceCreated, value);
				}
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06006548 RID: 25928 RVA: 0x0014CB54 File Offset: 0x0014AD54
		// (remove) Token: 0x06006549 RID: 25929 RVA: 0x0014CB8C File Offset: 0x0014AD8C
		public event EventHandler<EventWrittenEventArgs> EventWritten;

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventListener" /> class.</summary>
		// Token: 0x0600654A RID: 25930 RVA: 0x0014CBC1 File Offset: 0x0014ADC1
		public EventListener()
		{
			this.CallBackForExistingEventSources(true, delegate(object obj, EventSourceCreatedEventArgs args)
			{
				args.EventSource.AddListener(this);
			});
		}

		/// <summary>Releases the resources used by the current instance of the <see cref="T:System.Diagnostics.Tracing.EventListener" /> class.</summary>
		// Token: 0x0600654B RID: 25931 RVA: 0x0014CBDC File Offset: 0x0014ADDC
		public virtual void Dispose()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_Listeners != null)
				{
					if (this == EventListener.s_Listeners)
					{
						EventListener eventListener = EventListener.s_Listeners;
						EventListener.s_Listeners = this.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(eventListener);
					}
					else
					{
						EventListener eventListener2 = EventListener.s_Listeners;
						EventListener next;
						for (;;)
						{
							next = eventListener2.m_Next;
							if (next == null)
							{
								break;
							}
							if (next == this)
							{
								goto Block_6;
							}
							eventListener2 = next;
						}
						return;
						Block_6:
						eventListener2.m_Next = next.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(next);
					}
				}
			}
		}

		/// <summary>Enables events for the specified event source that has the specified verbosity level or lower.</summary>
		/// <param name="eventSource">The event source to enable events for.</param>
		/// <param name="level">The level of events to enable.</param>
		// Token: 0x0600654C RID: 25932 RVA: 0x0014CC74 File Offset: 0x0014AE74
		public void EnableEvents(EventSource eventSource, EventLevel level)
		{
			this.EnableEvents(eventSource, level, EventKeywords.None);
		}

		/// <summary>Enables events for the specified event source that has the specified verbosity level or lower, and matching keyword flags.</summary>
		/// <param name="eventSource">The event source to enable events for.</param>
		/// <param name="level">The level of events to enable.</param>
		/// <param name="matchAnyKeyword">The keyword flags necessary to enable the events.</param>
		// Token: 0x0600654D RID: 25933 RVA: 0x0014CC80 File Offset: 0x0014AE80
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.EnableEvents(eventSource, level, matchAnyKeyword, null);
		}

		/// <summary>Enables events for the specified event source that has the specified verbosity level or lower, matching event keyword flag, and matching arguments.</summary>
		/// <param name="eventSource">The event source to enable events for.</param>
		/// <param name="level">The level of events to enable.</param>
		/// <param name="matchAnyKeyword">The keyword flags necessary to enable the events.</param>
		/// <param name="arguments">The arguments to be matched to enable the events.</param>
		// Token: 0x0600654E RID: 25934 RVA: 0x0014CC8C File Offset: 0x0014AE8C
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, true, level, matchAnyKeyword, arguments);
		}

		/// <summary>Disables all events for the specified event source.</summary>
		/// <param name="eventSource">The event source to disable events for.</param>
		// Token: 0x0600654F RID: 25935 RVA: 0x0014CCB8 File Offset: 0x0014AEB8
		public void DisableEvents(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, false, EventLevel.LogAlways, EventKeywords.None, null);
		}

		/// <summary>Gets a small non-negative number that represents the specified event source.</summary>
		/// <returns>A small non-negative number that represents the specified event source.</returns>
		/// <param name="eventSource">The event source to find the index for.</param>
		// Token: 0x06006550 RID: 25936 RVA: 0x0014CCE2 File Offset: 0x0014AEE2
		public static int EventSourceIndex(EventSource eventSource)
		{
			return eventSource.m_id;
		}

		/// <summary>Called for all existing event sources when the event listener is created and when a new event source is attached to the listener.</summary>
		/// <param name="eventSource">The event source.</param>
		// Token: 0x06006551 RID: 25937 RVA: 0x0014CCEC File Offset: 0x0014AEEC
		protected internal virtual void OnEventSourceCreated(EventSource eventSource)
		{
			EventHandler<EventSourceCreatedEventArgs> eventSourceCreated = this._EventSourceCreated;
			if (eventSourceCreated != null)
			{
				eventSourceCreated(this, new EventSourceCreatedEventArgs
				{
					EventSource = eventSource
				});
			}
		}

		/// <summary>Called whenever an event has been written by an event source for which the event listener has enabled events.</summary>
		/// <param name="eventData">The event arguments that describe the event.</param>
		// Token: 0x06006552 RID: 25938 RVA: 0x0014CD18 File Offset: 0x0014AF18
		protected internal virtual void OnEventWritten(EventWrittenEventArgs eventData)
		{
			EventHandler<EventWrittenEventArgs> eventWritten = this.EventWritten;
			if (eventWritten != null)
			{
				eventWritten(this, eventData);
			}
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x0014CD38 File Offset: 0x0014AF38
		internal static void AddEventSource(EventSource newEventSource)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_EventSources == null)
				{
					EventListener.s_EventSources = new List<WeakReference>(2);
				}
				if (!EventListener.s_EventSourceShutdownRegistered)
				{
					EventListener.s_EventSourceShutdownRegistered = true;
					AppDomain.CurrentDomain.ProcessExit += EventListener.DisposeOnShutdown;
					AppDomain.CurrentDomain.DomainUnload += EventListener.DisposeOnShutdown;
				}
				int num = -1;
				if (EventListener.s_EventSources.Count % 64 == 63)
				{
					int num2 = EventListener.s_EventSources.Count;
					while (0 < num2)
					{
						num2--;
						WeakReference weakReference = EventListener.s_EventSources[num2];
						if (!weakReference.IsAlive)
						{
							num = num2;
							weakReference.Target = newEventSource;
							break;
						}
					}
				}
				if (num < 0)
				{
					num = EventListener.s_EventSources.Count;
					EventListener.s_EventSources.Add(new WeakReference(newEventSource));
				}
				newEventSource.m_id = num;
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					newEventSource.AddListener(next);
				}
			}
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x0014CE4C File Offset: 0x0014B04C
		private static void DisposeOnShutdown(object sender, EventArgs e)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						eventSource.Dispose();
					}
				}
			}
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x0014CED0 File Offset: 0x0014B0D0
		private static void RemoveReferencesToListenerInEventSources(EventListener listenerToRemove)
		{
			using (List<WeakReference>.Enumerator enumerator = EventListener.s_EventSources.GetEnumerator())
			{
				IL_0077:
				while (enumerator.MoveNext())
				{
					WeakReference weakReference = enumerator.Current;
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						if (eventSource.m_Dispatchers.m_Listener == listenerToRemove)
						{
							eventSource.m_Dispatchers = eventSource.m_Dispatchers.m_Next;
						}
						else
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							EventDispatcher next;
							for (;;)
							{
								next = eventDispatcher.m_Next;
								if (next == null)
								{
									goto IL_0077;
								}
								if (next.m_Listener == listenerToRemove)
								{
									break;
								}
								eventDispatcher = next;
							}
							eventDispatcher.m_Next = next.m_Next;
						}
					}
				}
			}
		}

		// Token: 0x06006556 RID: 25942 RVA: 0x0014CF80 File Offset: 0x0014B180
		[Conditional("DEBUG")]
		internal static void Validate()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				Dictionary<EventListener, bool> dictionary = new Dictionary<EventListener, bool>();
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					dictionary.Add(next, true);
				}
				int num = -1;
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					num++;
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						for (EventDispatcher eventDispatcher = eventSource.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
						{
						}
						foreach (EventListener eventListener in dictionary.Keys)
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							while (eventDispatcher.m_Listener != eventListener)
							{
								eventDispatcher = eventDispatcher.m_Next;
							}
						}
					}
				}
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06006557 RID: 25943 RVA: 0x0014D0AC File Offset: 0x0014B2AC
		internal static object EventListenersLock
		{
			get
			{
				if (EventListener.s_EventSources == null)
				{
					Interlocked.CompareExchange<List<WeakReference>>(ref EventListener.s_EventSources, new List<WeakReference>(2), null);
				}
				return EventListener.s_EventSources;
			}
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x0014D0CC File Offset: 0x0014B2CC
		private void CallBackForExistingEventSources(bool addToListenersList, EventHandler<EventSourceCreatedEventArgs> callback)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_CreatingListener)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Creating an EventListener inside a EventListener callback."));
				}
				try
				{
					EventListener.s_CreatingListener = true;
					if (addToListenersList)
					{
						this.m_Next = EventListener.s_Listeners;
						EventListener.s_Listeners = this;
					}
					WeakReference[] array = EventListener.s_EventSources.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						EventSource eventSource = array[i].Target as EventSource;
						if (eventSource != null)
						{
							callback(this, new EventSourceCreatedEventArgs
							{
								EventSource = eventSource
							});
						}
					}
				}
				finally
				{
					EventListener.s_CreatingListener = false;
				}
			}
		}

		// Token: 0x04003261 RID: 12897
		private static readonly object s_EventSourceCreatedLock = new object();

		// Token: 0x04003264 RID: 12900
		internal volatile EventListener m_Next;

		// Token: 0x04003265 RID: 12901
		internal ActivityFilter m_activityFilter;

		// Token: 0x04003266 RID: 12902
		internal static EventListener s_Listeners;

		// Token: 0x04003267 RID: 12903
		internal static List<WeakReference> s_EventSources;

		// Token: 0x04003268 RID: 12904
		private static bool s_CreatingListener = false;

		// Token: 0x04003269 RID: 12905
		private static bool s_EventSourceShutdownRegistered = false;
	}
}
