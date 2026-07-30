using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200018E RID: 398
	internal class EventDebugger
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002885A File Offset: 0x00026A5A
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x00028862 File Offset: 0x00026A62
		public IPanel panel { get; set; }

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002886C File Offset: 0x00026A6C
		public void UpdateModificationCount()
		{
			bool flag = this.panel == null;
			if (!flag)
			{
				long num = 0L;
				bool flag2 = this.m_ModificationCount.ContainsKey(this.panel);
				if (flag2)
				{
					num = this.m_ModificationCount[this.panel];
				}
				num += 1L;
				this.m_ModificationCount[this.panel] = num;
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000288CD File Offset: 0x00026ACD
		public void BeginProcessEvent(EventBase evt, IEventHandler mouseCapture)
		{
			this.AddBeginProcessEvent(evt, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000288E0 File Offset: 0x00026AE0
		public void EndProcessEvent(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.AddEndProcessEvent(evt, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000288F4 File Offset: 0x00026AF4
		public void LogCall(int cbHashCode, string cbName, EventBase evt, bool propagationHasStopped, bool immediatePropagationHasStopped, bool defaultHasBeenPrevented, long duration, IEventHandler mouseCapture)
		{
			this.AddCallObject(cbHashCode, cbName, evt, propagationHasStopped, immediatePropagationHasStopped, defaultHasBeenPrevented, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002891D File Offset: 0x00026B1D
		public void LogIMGUICall(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.AddIMGUICall(evt, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00028931 File Offset: 0x00026B31
		public void LogExecuteDefaultAction(EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
		{
			this.AddExecuteDefaultAction(evt, phase, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000062F3 File Offset: 0x000044F3
		public static void LogPropagationPaths(EventBase evt, PropagationPaths paths)
		{
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00028948 File Offset: 0x00026B48
		private void LogPropagationPathsInternal(EventBase evt, PropagationPaths paths)
		{
			PropagationPaths propagationPaths = ((paths == null) ? new PropagationPaths() : new PropagationPaths(paths));
			this.AddPropagationPaths(evt, propagationPaths);
			this.UpdateModificationCount();
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00028978 File Offset: 0x00026B78
		public List<EventDebuggerCallTrace> GetCalls(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerCallTrace> list = null;
			bool flag = this.m_EventCalledObjects.ContainsKey(panel);
			if (flag)
			{
				list = this.m_EventCalledObjects[panel];
			}
			bool flag2 = evt != null && list != null;
			if (flag2)
			{
				List<EventDebuggerCallTrace> list2 = new List<EventDebuggerCallTrace>();
				foreach (EventDebuggerCallTrace eventDebuggerCallTrace in list)
				{
					bool flag3 = eventDebuggerCallTrace.eventBase.eventId == evt.eventId;
					if (flag3)
					{
						list2.Add(eventDebuggerCallTrace);
					}
				}
				list = list2;
			}
			return list;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00028A2C File Offset: 0x00026C2C
		public List<EventDebuggerDefaultActionTrace> GetDefaultActions(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerDefaultActionTrace> list = null;
			bool flag = this.m_EventDefaultActionObjects.ContainsKey(panel);
			if (flag)
			{
				list = this.m_EventDefaultActionObjects[panel];
			}
			bool flag2 = evt != null && list != null;
			if (flag2)
			{
				List<EventDebuggerDefaultActionTrace> list2 = new List<EventDebuggerDefaultActionTrace>();
				foreach (EventDebuggerDefaultActionTrace eventDebuggerDefaultActionTrace in list)
				{
					bool flag3 = eventDebuggerDefaultActionTrace.eventBase.eventId == evt.eventId;
					if (flag3)
					{
						list2.Add(eventDebuggerDefaultActionTrace);
					}
				}
				list = list2;
			}
			return list;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00028AE0 File Offset: 0x00026CE0
		public List<EventDebuggerPathTrace> GetPropagationPaths(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerPathTrace> list = null;
			bool flag = this.m_EventPathObjects.ContainsKey(panel);
			if (flag)
			{
				list = this.m_EventPathObjects[panel];
			}
			bool flag2 = evt != null && list != null;
			if (flag2)
			{
				List<EventDebuggerPathTrace> list2 = new List<EventDebuggerPathTrace>();
				foreach (EventDebuggerPathTrace eventDebuggerPathTrace in list)
				{
					bool flag3 = eventDebuggerPathTrace.eventBase.eventId == evt.eventId;
					if (flag3)
					{
						list2.Add(eventDebuggerPathTrace);
					}
				}
				list = list2;
			}
			return list;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00028B94 File Offset: 0x00026D94
		public List<EventDebuggerTrace> GetBeginEndProcessedEvents(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerTrace> list = null;
			bool flag = this.m_EventProcessedEvents.ContainsKey(panel);
			if (flag)
			{
				list = this.m_EventProcessedEvents[panel];
			}
			bool flag2 = evt != null && list != null;
			if (flag2)
			{
				List<EventDebuggerTrace> list2 = new List<EventDebuggerTrace>();
				foreach (EventDebuggerTrace eventDebuggerTrace in list)
				{
					bool flag3 = eventDebuggerTrace.eventBase.eventId == evt.eventId;
					if (flag3)
					{
						list2.Add(eventDebuggerTrace);
					}
				}
				list = list2;
			}
			return list;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00028C48 File Offset: 0x00026E48
		public long GetModificationCount(IPanel panel)
		{
			long num = -1L;
			bool flag = panel != null && this.m_ModificationCount.ContainsKey(panel);
			if (flag)
			{
				num = this.m_ModificationCount[panel];
			}
			return num;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00028C84 File Offset: 0x00026E84
		public void ClearLogs()
		{
			this.UpdateModificationCount();
			bool flag = this.panel == null;
			if (flag)
			{
				this.m_EventCalledObjects.Clear();
				this.m_EventDefaultActionObjects.Clear();
				this.m_EventPathObjects.Clear();
				this.m_EventProcessedEvents.Clear();
				this.m_StackOfProcessedEvent.Clear();
			}
			else
			{
				this.m_EventCalledObjects.Remove(this.panel);
				this.m_EventDefaultActionObjects.Remove(this.panel);
				this.m_EventPathObjects.Remove(this.panel);
				this.m_EventProcessedEvents.Remove(this.panel);
				this.m_StackOfProcessedEvent.Remove(this.panel);
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00028D40 File Offset: 0x00026F40
		public void ReplayEvents(List<EventDebuggerEventRecord> eventBases)
		{
			bool flag = eventBases == null;
			if (!flag)
			{
				foreach (EventDebuggerEventRecord eventDebuggerEventRecord in eventBases)
				{
					Event @event = new Event
					{
						button = eventDebuggerEventRecord.button,
						clickCount = eventDebuggerEventRecord.clickCount,
						modifiers = eventDebuggerEventRecord.modifiers,
						mousePosition = eventDebuggerEventRecord.mousePosition
					};
					bool flag2 = eventDebuggerEventRecord.eventTypeId == EventBase<MouseMoveEvent>.TypeId() && eventDebuggerEventRecord.hasUnderlyingPhysicalEvent;
					if (flag2)
					{
						@event.type = EventType.MouseMove;
						this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.MouseMove), this.panel, DispatchMode.Default);
					}
					else
					{
						bool flag3 = eventDebuggerEventRecord.eventTypeId == EventBase<MouseDownEvent>.TypeId() && eventDebuggerEventRecord.hasUnderlyingPhysicalEvent;
						if (flag3)
						{
							@event.type = EventType.MouseDown;
							this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.MouseDown), this.panel, DispatchMode.Default);
						}
						else
						{
							bool flag4 = eventDebuggerEventRecord.eventTypeId == EventBase<MouseUpEvent>.TypeId() && eventDebuggerEventRecord.hasUnderlyingPhysicalEvent;
							if (flag4)
							{
								@event.type = EventType.MouseUp;
								this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.MouseUp), this.panel, DispatchMode.Default);
							}
							else
							{
								bool flag5 = eventDebuggerEventRecord.eventTypeId == EventBase<ContextClickEvent>.TypeId() && eventDebuggerEventRecord.hasUnderlyingPhysicalEvent;
								if (flag5)
								{
									@event.type = EventType.ContextClick;
									this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.ContextClick), this.panel, DispatchMode.Default);
								}
								else
								{
									bool flag6 = eventDebuggerEventRecord.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() && eventDebuggerEventRecord.hasUnderlyingPhysicalEvent;
									if (flag6)
									{
										@event.type = EventType.MouseEnterWindow;
										this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.MouseEnterWindow), this.panel, DispatchMode.Default);
									}
									else
									{
										bool flag7 = eventDebuggerEventRecord.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId() && eventDebuggerEventRecord.hasUnderlyingPhysicalEvent;
										if (flag7)
										{
											@event.type = EventType.MouseLeaveWindow;
											this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.MouseLeaveWindow), this.panel, DispatchMode.Default);
										}
										else
										{
											bool flag8 = eventDebuggerEventRecord.eventTypeId == EventBase<WheelEvent>.TypeId() && eventDebuggerEventRecord.hasUnderlyingPhysicalEvent;
											if (flag8)
											{
												@event.type = EventType.ScrollWheel;
												@event.delta = eventDebuggerEventRecord.delta;
												this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.ScrollWheel), this.panel, DispatchMode.Default);
											}
											else
											{
												bool flag9 = eventDebuggerEventRecord.eventTypeId == EventBase<KeyDownEvent>.TypeId();
												if (flag9)
												{
													@event.type = EventType.KeyDown;
													@event.character = eventDebuggerEventRecord.character;
													@event.keyCode = eventDebuggerEventRecord.keyCode;
													this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.KeyDown), this.panel, DispatchMode.Default);
												}
												else
												{
													bool flag10 = eventDebuggerEventRecord.eventTypeId == EventBase<KeyUpEvent>.TypeId();
													if (flag10)
													{
														@event.type = EventType.KeyUp;
														@event.character = eventDebuggerEventRecord.character;
														@event.keyCode = eventDebuggerEventRecord.keyCode;
														this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.KeyUp), this.panel, DispatchMode.Default);
													}
													else
													{
														bool flag11 = eventDebuggerEventRecord.eventTypeId == EventBase<DragUpdatedEvent>.TypeId();
														if (flag11)
														{
															@event.type = EventType.DragUpdated;
															this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.DragUpdated), this.panel, DispatchMode.Default);
														}
														else
														{
															bool flag12 = eventDebuggerEventRecord.eventTypeId == EventBase<DragPerformEvent>.TypeId();
															if (flag12)
															{
																@event.type = EventType.DragPerform;
																this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.DragPerform), this.panel, DispatchMode.Default);
															}
															else
															{
																bool flag13 = eventDebuggerEventRecord.eventTypeId == EventBase<DragExitedEvent>.TypeId();
																if (flag13)
																{
																	@event.type = EventType.DragExited;
																	this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.DragExited), this.panel, DispatchMode.Default);
																}
																else
																{
																	bool flag14 = eventDebuggerEventRecord.eventTypeId == EventBase<ValidateCommandEvent>.TypeId();
																	if (flag14)
																	{
																		@event.type = EventType.ValidateCommand;
																		@event.commandName = eventDebuggerEventRecord.commandName;
																		this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.ValidateCommand), this.panel, DispatchMode.Default);
																	}
																	else
																	{
																		bool flag15 = eventDebuggerEventRecord.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
																		if (flag15)
																		{
																			@event.type = EventType.ExecuteCommand;
																			@event.commandName = eventDebuggerEventRecord.commandName;
																			this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(@event, EventType.ExecuteCommand), this.panel, DispatchMode.Default);
																		}
																		else
																		{
																			bool flag16 = eventDebuggerEventRecord.eventTypeId == EventBase<IMGUIEvent>.TypeId();
																			if (flag16)
																			{
																				Debug.Log(string.Concat(new object[] { "Skipped IMGUI event (", eventDebuggerEventRecord.eventBaseName, "): ", eventDebuggerEventRecord }));
																				continue;
																			}
																			Debug.Log(string.Concat(new object[] { "Skipped event (", eventDebuggerEventRecord.eventBaseName, "): ", eventDebuggerEventRecord }));
																			continue;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					Debug.Log(string.Concat(new object[] { "Replayed event (", eventDebuggerEventRecord.eventBaseName, "): ", @event }));
				}
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000292C4 File Offset: 0x000274C4
		public Dictionary<string, long> ComputeHistogram(List<EventDebuggerEventRecord> eventBases)
		{
			bool flag = this.panel == null || !this.m_EventProcessedEvents.ContainsKey(this.panel);
			Dictionary<string, long> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				List<EventDebuggerTrace> list = this.m_EventProcessedEvents[this.panel];
				bool flag2 = list == null;
				if (flag2)
				{
					dictionary = null;
				}
				else
				{
					Dictionary<string, long> dictionary2 = new Dictionary<string, long>();
					foreach (EventDebuggerTrace eventDebuggerTrace in list)
					{
						bool flag3 = eventBases == null || eventBases.Count == 0 || eventBases.Contains(eventDebuggerTrace.eventBase);
						if (flag3)
						{
							string eventBaseName = eventDebuggerTrace.eventBase.eventBaseName;
							long num = eventDebuggerTrace.duration;
							bool flag4 = dictionary2.ContainsKey(eventBaseName);
							if (flag4)
							{
								long num2 = dictionary2[eventBaseName];
								num += num2;
							}
							dictionary2[eventBaseName] = num;
						}
					}
					dictionary = dictionary2;
				}
			}
			return dictionary;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000293D0 File Offset: 0x000275D0
		public EventDebugger()
		{
			this.m_EventCalledObjects = new Dictionary<IPanel, List<EventDebuggerCallTrace>>();
			this.m_EventDefaultActionObjects = new Dictionary<IPanel, List<EventDebuggerDefaultActionTrace>>();
			this.m_EventPathObjects = new Dictionary<IPanel, List<EventDebuggerPathTrace>>();
			this.m_StackOfProcessedEvent = new Dictionary<IPanel, Stack<EventDebuggerTrace>>();
			this.m_EventProcessedEvents = new Dictionary<IPanel, List<EventDebuggerTrace>>();
			this.m_ModificationCount = new Dictionary<IPanel, long>();
			this.m_Log = true;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00029430 File Offset: 0x00027630
		private void AddCallObject(int cbHashCode, string cbName, EventBase evt, bool propagationHasStopped, bool immediatePropagationHasStopped, bool defaultHasBeenPrevented, long duration, IEventHandler mouseCapture)
		{
			bool log = this.m_Log;
			if (log)
			{
				EventDebuggerCallTrace eventDebuggerCallTrace = new EventDebuggerCallTrace(this.panel, evt, cbHashCode, cbName, propagationHasStopped, immediatePropagationHasStopped, defaultHasBeenPrevented, duration, mouseCapture);
				bool flag = this.m_EventCalledObjects.ContainsKey(this.panel);
				List<EventDebuggerCallTrace> list;
				if (flag)
				{
					list = this.m_EventCalledObjects[this.panel];
				}
				else
				{
					list = new List<EventDebuggerCallTrace>();
					this.m_EventCalledObjects.Add(this.panel, list);
				}
				list.Add(eventDebuggerCallTrace);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000294B4 File Offset: 0x000276B4
		private void AddExecuteDefaultAction(EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
		{
			bool log = this.m_Log;
			if (log)
			{
				EventDebuggerDefaultActionTrace eventDebuggerDefaultActionTrace = new EventDebuggerDefaultActionTrace(this.panel, evt, phase, duration, mouseCapture);
				bool flag = this.m_EventDefaultActionObjects.ContainsKey(this.panel);
				List<EventDebuggerDefaultActionTrace> list;
				if (flag)
				{
					list = this.m_EventDefaultActionObjects[this.panel];
				}
				else
				{
					list = new List<EventDebuggerDefaultActionTrace>();
					this.m_EventDefaultActionObjects.Add(this.panel, list);
				}
				list.Add(eventDebuggerDefaultActionTrace);
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00029530 File Offset: 0x00027730
		private void AddPropagationPaths(EventBase evt, PropagationPaths paths)
		{
			bool log = this.m_Log;
			if (log)
			{
				EventDebuggerPathTrace eventDebuggerPathTrace = new EventDebuggerPathTrace(this.panel, evt, paths);
				bool flag = this.m_EventPathObjects.ContainsKey(this.panel);
				List<EventDebuggerPathTrace> list;
				if (flag)
				{
					list = this.m_EventPathObjects[this.panel];
				}
				else
				{
					list = new List<EventDebuggerPathTrace>();
					this.m_EventPathObjects.Add(this.panel, list);
				}
				list.Add(eventDebuggerPathTrace);
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000295A8 File Offset: 0x000277A8
		private void AddIMGUICall(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			bool log = this.m_Log;
			if (log)
			{
				EventDebuggerCallTrace eventDebuggerCallTrace = new EventDebuggerCallTrace(this.panel, evt, 0, "OnGUI", false, false, false, duration, mouseCapture);
				bool flag = this.m_EventCalledObjects.ContainsKey(this.panel);
				List<EventDebuggerCallTrace> list;
				if (flag)
				{
					list = this.m_EventCalledObjects[this.panel];
				}
				else
				{
					list = new List<EventDebuggerCallTrace>();
					this.m_EventCalledObjects.Add(this.panel, list);
				}
				list.Add(eventDebuggerCallTrace);
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00029628 File Offset: 0x00027828
		private void AddBeginProcessEvent(EventBase evt, IEventHandler mouseCapture)
		{
			EventDebuggerTrace eventDebuggerTrace = new EventDebuggerTrace(this.panel, evt, -1L, mouseCapture);
			bool flag = this.m_StackOfProcessedEvent.ContainsKey(this.panel);
			Stack<EventDebuggerTrace> stack;
			if (flag)
			{
				stack = this.m_StackOfProcessedEvent[this.panel];
			}
			else
			{
				stack = new Stack<EventDebuggerTrace>();
				this.m_StackOfProcessedEvent.Add(this.panel, stack);
			}
			bool flag2 = this.m_EventProcessedEvents.ContainsKey(this.panel);
			List<EventDebuggerTrace> list;
			if (flag2)
			{
				list = this.m_EventProcessedEvents[this.panel];
			}
			else
			{
				list = new List<EventDebuggerTrace>();
				this.m_EventProcessedEvents.Add(this.panel, list);
			}
			list.Add(eventDebuggerTrace);
			stack.Push(eventDebuggerTrace);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000296E4 File Offset: 0x000278E4
		private void AddEndProcessEvent(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			bool flag = false;
			bool flag2 = this.m_StackOfProcessedEvent.ContainsKey(this.panel);
			if (flag2)
			{
				Stack<EventDebuggerTrace> stack = this.m_StackOfProcessedEvent[this.panel];
				bool flag3 = stack.Count > 0;
				if (flag3)
				{
					EventDebuggerTrace eventDebuggerTrace = stack.Peek();
					bool flag4 = eventDebuggerTrace.eventBase.eventId == evt.eventId;
					if (flag4)
					{
						stack.Pop();
						eventDebuggerTrace.duration = duration;
						bool flag5 = eventDebuggerTrace.eventBase.target == null;
						if (flag5)
						{
							eventDebuggerTrace.eventBase.target = evt.target;
						}
						flag = true;
					}
				}
			}
			bool flag6 = !flag;
			if (flag6)
			{
				EventDebuggerTrace eventDebuggerTrace2 = new EventDebuggerTrace(this.panel, evt, duration, mouseCapture);
				bool flag7 = this.m_EventProcessedEvents.ContainsKey(this.panel);
				List<EventDebuggerTrace> list;
				if (flag7)
				{
					list = this.m_EventProcessedEvents[this.panel];
				}
				else
				{
					list = new List<EventDebuggerTrace>();
					this.m_EventProcessedEvents.Add(this.panel, list);
				}
				list.Add(eventDebuggerTrace2);
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00029800 File Offset: 0x00027A00
		public static string GetObjectDisplayName(object obj, bool withHashCode = true)
		{
			bool flag = obj == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				string text2 = obj.GetType().Name;
				bool flag2 = obj is VisualElement;
				if (flag2)
				{
					VisualElement visualElement = obj as VisualElement;
					bool flag3 = !string.IsNullOrEmpty(visualElement.name);
					if (flag3)
					{
						text2 = text2 + "#" + visualElement.name;
					}
				}
				if (withHashCode)
				{
					text2 = text2 + " (" + obj.GetHashCode().ToString("x8") + ")";
				}
				text = text2;
			}
			return text;
		}

		// Token: 0x04000477 RID: 1143
		private Dictionary<IPanel, List<EventDebuggerCallTrace>> m_EventCalledObjects;

		// Token: 0x04000478 RID: 1144
		private Dictionary<IPanel, List<EventDebuggerDefaultActionTrace>> m_EventDefaultActionObjects;

		// Token: 0x04000479 RID: 1145
		private Dictionary<IPanel, List<EventDebuggerPathTrace>> m_EventPathObjects;

		// Token: 0x0400047A RID: 1146
		private Dictionary<IPanel, List<EventDebuggerTrace>> m_EventProcessedEvents;

		// Token: 0x0400047B RID: 1147
		private Dictionary<IPanel, Stack<EventDebuggerTrace>> m_StackOfProcessedEvent;

		// Token: 0x0400047C RID: 1148
		private readonly Dictionary<IPanel, long> m_ModificationCount;

		// Token: 0x0400047D RID: 1149
		private readonly bool m_Log;
	}
}
