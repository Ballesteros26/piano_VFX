using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006C RID: 108
	internal static class UIElementsRuntimeUtility
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600027D RID: 637 RVA: 0x0000966C File Offset: 0x0000786C
		// (remove) Token: 0x0600027E RID: 638 RVA: 0x000096A0 File Offset: 0x000078A0
		[field: DebuggerBrowsable(0)]
		private static event Action s_onRepaintOverlayPanels;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600027F RID: 639 RVA: 0x000096D4 File Offset: 0x000078D4
		// (remove) Token: 0x06000280 RID: 640 RVA: 0x00009700 File Offset: 0x00007900
		internal static event Action onRepaintOverlayPanels
		{
			add
			{
				bool flag = UIElementsRuntimeUtility.s_onRepaintOverlayPanels == null;
				if (flag)
				{
					UIElementsRuntimeUtility.RegisterPlayerloopCallback();
				}
				UIElementsRuntimeUtility.s_onRepaintOverlayPanels += value;
			}
			remove
			{
				UIElementsRuntimeUtility.s_onRepaintOverlayPanels -= value;
				bool flag = UIElementsRuntimeUtility.s_onRepaintOverlayPanels == null;
				if (flag)
				{
					UIElementsRuntimeUtility.UnregisterPlayerloopCallback();
				}
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000972C File Offset: 0x0000792C
		static UIElementsRuntimeUtility()
		{
			UIElementsRuntimeUtilityNative.RepaintOverlayPanelsCallback = new Action(UIElementsRuntimeUtility.RepaintOverlayPanels);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009780 File Offset: 0x00007980
		public static EventBase CreateEvent(Event systemEvent)
		{
			Debug.Assert(UIElementsRuntimeUtility.s_RuntimeDispatcher != null, "Call UIElementsRuntimeUtility.InitRuntimeEventSystem before sending any event.");
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000097B4 File Offset: 0x000079B4
		public static IPanel CreateRuntimePanel(ScriptableObject ownerObject)
		{
			return UIElementsRuntimeUtility.FindOrCreateRuntimePanel(ownerObject);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000097CC File Offset: 0x000079CC
		public static IPanel FindOrCreateRuntimePanel(ScriptableObject ownerObject)
		{
			Panel panel;
			bool flag = !UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				panel = new RuntimePanel(ownerObject, UIElementsRuntimeUtility.s_RuntimeDispatcher)
				{
					IMGUIEventInterests = new EventInterests
					{
						wantsMouseMove = true,
						wantsMouseEnterLeaveWindow = true
					}
				};
				UIElementsRuntimeUtility.RegisterCachedPanelInternal(ownerObject.GetInstanceID(), panel);
			}
			else
			{
				Debug.Assert(panel.contextType == ContextType.Player, "Panel is not a runtime panel.");
			}
			return panel;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009848 File Offset: 0x00007A48
		public static void DisposeRuntimePanel(ScriptableObject ownerObject)
		{
			Panel panel;
			bool flag = UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				panel.Dispose();
				UIElementsRuntimeUtility.RemoveCachedPanelInternal(ownerObject.GetInstanceID());
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000987C File Offset: 0x00007A7C
		public static void RegisterCachedPanel(int instanceID, IPanel panel)
		{
			UIElementsRuntimeUtility.RegisterCachedPanelInternal(instanceID, panel);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009888 File Offset: 0x00007A88
		private static void RegisterCachedPanelInternal(int instanceID, IPanel panel)
		{
			UIElementsUtility.RegisterCachedPanel(instanceID, panel as Panel);
			bool flag = !UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = true;
				UIElementsRuntimeUtility.RegisterPlayerloopCallback();
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000098BD File Offset: 0x00007ABD
		public static void RemoveCachedPanel(int instanceID)
		{
			UIElementsRuntimeUtility.RemoveCachedPanelInternal(instanceID);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000098C8 File Offset: 0x00007AC8
		private static void RemoveCachedPanelInternal(int instanceID)
		{
			UIElementsUtility.RemoveCachedPanel(instanceID);
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.panelsIteration, ContextType.Player);
			bool flag = UIElementsRuntimeUtility.panelsIteration.Count == 0;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = false;
				UIElementsRuntimeUtility.UnregisterPlayerloopCallback();
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009908 File Offset: 0x00007B08
		public static void RepaintOverlayPanels()
		{
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.panelsIteration, ContextType.Player);
			foreach (Panel panel in UIElementsRuntimeUtility.panelsIteration)
			{
				RuntimePanel runtimePanel = (RuntimePanel)panel;
				bool flag = !runtimePanel.drawToCameras && runtimePanel.targetTexture == null;
				if (flag)
				{
					using (UIElementsRuntimeUtility.s_RepaintProfilerMarker.Auto())
					{
						runtimePanel.Repaint(Event.current);
					}
				}
			}
			bool flag2 = UIElementsRuntimeUtility.s_onRepaintOverlayPanels != null;
			if (flag2)
			{
				UIElementsRuntimeUtility.s_onRepaintOverlayPanels.Invoke();
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000099D8 File Offset: 0x00007BD8
		public static void RegisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.RegisterPlayerloopCallback();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000099D8 File Offset: 0x00007BD8
		public static void UnregisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.RegisterPlayerloopCallback();
		}

		// Token: 0x04000149 RID: 329
		private static EventDispatcher s_RuntimeDispatcher = new EventDispatcher();

		// Token: 0x0400014B RID: 331
		private static bool s_RegisteredPlayerloopCallback = false;

		// Token: 0x0400014C RID: 332
		private static List<Panel> panelsIteration = new List<Panel>();

		// Token: 0x0400014D RID: 333
		internal static readonly string s_RepaintProfilerMarkerName = "UIElementsRuntimeUtility.DoDispatch(Repaint Event)";

		// Token: 0x0400014E RID: 334
		private static readonly ProfilerMarker s_RepaintProfilerMarker = new ProfilerMarker(UIElementsRuntimeUtility.s_RepaintProfilerMarkerName);
	}
}
