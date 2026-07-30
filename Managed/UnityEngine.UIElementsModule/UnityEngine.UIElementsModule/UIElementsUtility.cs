using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000070 RID: 112
	internal class UIElementsUtility : IUIElementsUtility
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00009E2D File Offset: 0x0000802D
		private UIElementsUtility()
		{
			UIEventRegistration.RegisterUIElementSystem(this);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009E40 File Offset: 0x00008040
		internal static IMGUIContainer GetCurrentIMGUIContainer()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			IMGUIContainer imguicontainer;
			if (flag)
			{
				imguicontainer = UIElementsUtility.s_ContainerStack.Peek();
			}
			else
			{
				imguicontainer = null;
			}
			return imguicontainer;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009E74 File Offset: 0x00008074
		bool IUIElementsUtility.MakeCurrentIMGUIContainerDirty()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool flag2;
			if (flag)
			{
				UIElementsUtility.s_ContainerStack.Peek().MarkDirtyLayout();
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00009EB0 File Offset: 0x000080B0
		bool IUIElementsUtility.TakeCapture()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool flag3;
			if (flag)
			{
				IMGUIContainer imguicontainer = UIElementsUtility.s_ContainerStack.Peek();
				IEventHandler capturingElement = imguicontainer.panel.GetCapturingElement(PointerId.mousePointerId);
				bool flag2 = capturingElement != null && capturingElement != imguicontainer;
				if (flag2)
				{
					Debug.Log("Should not grab hot control with an active capture");
				}
				imguicontainer.CaptureMouse();
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009F20 File Offset: 0x00008120
		bool IUIElementsUtility.ReleaseCapture()
		{
			return false;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00009F34 File Offset: 0x00008134
		bool IUIElementsUtility.ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled)
		{
			Panel panel;
			bool flag = nativeEventPtr != IntPtr.Zero && UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, ref panel);
			bool flag3;
			if (flag)
			{
				bool flag2 = panel.contextType == ContextType.Editor;
				if (flag2)
				{
					UIElementsUtility.s_EventInstance.CopyFromPtr(nativeEventPtr);
					eventHandled = UIElementsUtility.DoDispatch(panel);
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00009F90 File Offset: 0x00008190
		bool IUIElementsUtility.CleanupRoots()
		{
			UIElementsUtility.s_EventInstance = null;
			UIElementsUtility.s_UIElementsCache = null;
			UIElementsUtility.s_ContainerStack = null;
			return false;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00009FB8 File Offset: 0x000081B8
		bool IUIElementsUtility.EndContainerGUIFromException(Exception exception)
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
			return false;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00009FF0 File Offset: 0x000081F0
		void IUIElementsUtility.UpdateSchedulers()
		{
			UIElementsUtility.GetAllPanels(UIElementsUtility.s_PanelsIterationList, ContextType.Editor);
			foreach (Panel panel in UIElementsUtility.s_PanelsIterationList)
			{
				panel.timerEventScheduler.UpdateScheduledEvents();
				panel.UpdateAnimations();
				panel.UpdateBindings();
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A068 File Offset: 0x00008268
		void IUIElementsUtility.RequestRepaintForPanels(Action<ScriptableObject> repaintCallback)
		{
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
				Panel value = keyValuePair.Value;
				bool flag = value.contextType != ContextType.Editor;
				if (!flag)
				{
					bool isDirty = value.isDirty;
					if (isDirty)
					{
						repaintCallback.Invoke(value.ownerObject);
					}
				}
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A0CA File Offset: 0x000082CA
		public static void RegisterCachedPanel(int instanceID, Panel panel)
		{
			UIElementsUtility.s_UIElementsCache.Add(instanceID, panel);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A0DA File Offset: 0x000082DA
		public static void RemoveCachedPanel(int instanceID)
		{
			UIElementsUtility.s_UIElementsCache.Remove(instanceID);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A0EC File Offset: 0x000082EC
		public static bool TryGetPanel(int instanceID, out Panel panel)
		{
			return UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, ref panel);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A10C File Offset: 0x0000830C
		internal static void BeginContainerGUI(GUILayoutUtility.LayoutCache cache, Event evt, IMGUIContainer container)
		{
			bool useOwnerObjectGUIState = container.useOwnerObjectGUIState;
			if (useOwnerObjectGUIState)
			{
				GUIUtility.BeginContainerFromOwner(container.elementPanel.ownerObject);
			}
			else
			{
				GUIUtility.BeginContainer(container.guiState);
			}
			UIElementsUtility.s_ContainerStack.Push(container);
			GUIUtility.s_SkinMode = (int)container.contextType;
			GUIUtility.s_OriginalID = container.elementPanel.ownerObject.GetInstanceID();
			bool flag = Event.current == null;
			if (flag)
			{
				Event.current = evt;
			}
			else
			{
				Event.current.CopyFrom(evt);
			}
			GUI.enabled = container.enabledInHierarchy;
			GUILayoutUtility.BeginContainer(cache);
			GUIUtility.ResetGlobalState();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A1B4 File Offset: 0x000083B4
		internal static void EndContainerGUI(Event evt, Rect layoutSize)
		{
			bool flag = Event.current.type == EventType.Layout && UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUILayoutUtility.LayoutFromContainer(layoutSize.width, layoutSize.height);
			}
			GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
			GUIContent.ClearStaticCache();
			bool flag2 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag2)
			{
			}
			evt.CopyFrom(Event.current);
			bool flag3 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag3)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000A250 File Offset: 0x00008450
		internal static EventBase CreateEvent(Event systemEvent)
		{
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A270 File Offset: 0x00008470
		internal static EventBase CreateEvent(Event systemEvent, EventType eventType)
		{
			switch (eventType)
			{
			case EventType.MouseDown:
			{
				bool flag = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId) != 0;
				if (flag)
				{
					return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
				}
				return PointerEventBase<PointerDownEvent>.GetPooled(systemEvent);
			}
			case EventType.MouseUp:
			{
				bool flag2 = PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, systemEvent.button);
				if (flag2)
				{
					return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
				}
				return PointerEventBase<PointerUpEvent>.GetPooled(systemEvent);
			}
			case EventType.MouseMove:
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			case EventType.MouseDrag:
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			case EventType.KeyDown:
				return KeyboardEventBase<KeyDownEvent>.GetPooled(systemEvent);
			case EventType.KeyUp:
				return KeyboardEventBase<KeyUpEvent>.GetPooled(systemEvent);
			case EventType.ScrollWheel:
				return WheelEvent.GetPooled(systemEvent);
			case EventType.DragUpdated:
				return DragUpdatedEvent.GetPooled(systemEvent);
			case EventType.DragPerform:
				return MouseEventBase<DragPerformEvent>.GetPooled(systemEvent);
			case EventType.ValidateCommand:
				return CommandEventBase<ValidateCommandEvent>.GetPooled(systemEvent);
			case EventType.ExecuteCommand:
				return CommandEventBase<ExecuteCommandEvent>.GetPooled(systemEvent);
			case EventType.DragExited:
				return DragExitedEvent.GetPooled(systemEvent);
			case EventType.ContextClick:
				return MouseEventBase<ContextClickEvent>.GetPooled(systemEvent);
			case EventType.MouseEnterWindow:
				return MouseEventBase<MouseEnterWindowEvent>.GetPooled(systemEvent);
			case EventType.MouseLeaveWindow:
				return MouseLeaveWindowEvent.GetPooled(systemEvent);
			}
			return IMGUIEvent.GetPooled(systemEvent);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A3BC File Offset: 0x000085BC
		private static bool DoDispatch(BaseVisualElementPanel panel)
		{
			bool flag = false;
			bool flag2 = UIElementsUtility.s_EventInstance.type == EventType.Repaint;
			if (flag2)
			{
				using (UIElementsUtility.s_RepaintProfilerMarker.Auto())
				{
					panel.Repaint(UIElementsUtility.s_EventInstance);
				}
				flag = panel.IMGUIContainersCount > 0;
			}
			else
			{
				panel.ValidateLayout();
				using (EventBase eventBase = UIElementsUtility.CreateEvent(UIElementsUtility.s_EventInstance))
				{
					bool flag3 = UIElementsUtility.s_EventInstance.type == EventType.Used || UIElementsUtility.s_EventInstance.type == EventType.Layout || UIElementsUtility.s_EventInstance.type == EventType.ExecuteCommand || UIElementsUtility.s_EventInstance.type == EventType.ValidateCommand;
					using (UIElementsUtility.s_EventProfilerMarker.Auto())
					{
						panel.SendEvent(eventBase, flag3 ? DispatchMode.Immediate : DispatchMode.Default);
					}
					bool isPropagationStopped = eventBase.isPropagationStopped;
					if (isPropagationStopped)
					{
						panel.visualTree.IncrementVersion(VersionChangeType.Repaint);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A500 File Offset: 0x00008700
		internal static void GetAllPanels(List<Panel> panels, ContextType contextType)
		{
			panels.Clear();
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
				bool flag = keyValuePair.Value.contextType == contextType;
				if (flag)
				{
					keyValuePair = panelsIterator.Current;
					panels.Add(keyValuePair.Value);
				}
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A560 File Offset: 0x00008760
		internal static Dictionary<int, Panel>.Enumerator GetPanelsIterator()
		{
			return UIElementsUtility.s_UIElementsCache.GetEnumerator();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000A57C File Offset: 0x0000877C
		internal static Panel FindOrCreateEditorPanel(ScriptableObject ownerObject)
		{
			Panel panel;
			bool flag = !UIElementsUtility.s_UIElementsCache.TryGetValue(ownerObject.GetInstanceID(), ref panel);
			if (flag)
			{
				panel = Panel.CreateEditorPanel(ownerObject);
				UIElementsUtility.RegisterCachedPanel(ownerObject.GetInstanceID(), panel);
			}
			else
			{
				Debug.Assert(ContextType.Editor == panel.contextType, "Panel is not an editor panel.");
			}
			return panel;
		}

		// Token: 0x04000151 RID: 337
		private static Stack<IMGUIContainer> s_ContainerStack = new Stack<IMGUIContainer>();

		// Token: 0x04000152 RID: 338
		private static Dictionary<int, Panel> s_UIElementsCache = new Dictionary<int, Panel>();

		// Token: 0x04000153 RID: 339
		private static Event s_EventInstance = new Event();

		// Token: 0x04000154 RID: 340
		internal static Color editorPlayModeTintColor = Color.white;

		// Token: 0x04000155 RID: 341
		private static UIElementsUtility s_Instance = new UIElementsUtility();

		// Token: 0x04000156 RID: 342
		private static List<Panel> s_PanelsIterationList = new List<Panel>();

		// Token: 0x04000157 RID: 343
		internal static readonly string s_RepaintProfilerMarkerName = "UIElementsUtility.DoDispatch(Repaint Event)";

		// Token: 0x04000158 RID: 344
		internal static readonly string s_EventProfilerMarkerName = "UIElementsUtility.DoDispatch(Non Repaint Event)";

		// Token: 0x04000159 RID: 345
		private static readonly ProfilerMarker s_RepaintProfilerMarker = new ProfilerMarker(UIElementsUtility.s_RepaintProfilerMarkerName);

		// Token: 0x0400015A RID: 346
		private static readonly ProfilerMarker s_EventProfilerMarker = new ProfilerMarker(UIElementsUtility.s_EventProfilerMarkerName);
	}
}
