using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000044 RID: 68
	public static class PointerCaptureHelper
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00006F60 File Offset: 0x00005160
		private static PointerDispatchState GetStateFor(IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			PointerDispatchState pointerDispatchState;
			if (visualElement == null)
			{
				pointerDispatchState = null;
			}
			else
			{
				IPanel panel = visualElement.panel;
				if (panel == null)
				{
					pointerDispatchState = null;
				}
				else
				{
					EventDispatcher dispatcher = panel.dispatcher;
					pointerDispatchState = ((dispatcher != null) ? dispatcher.pointerState : null);
				}
			}
			return pointerDispatchState;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006FA0 File Offset: 0x000051A0
		public static bool HasPointerCapture(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			return stateFor != null && stateFor.HasPointerCapture(handler, pointerId);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006FC6 File Offset: 0x000051C6
		public static void CapturePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			if (stateFor != null)
			{
				stateFor.CapturePointer(handler, pointerId);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006FDD File Offset: 0x000051DD
		public static void ReleasePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			if (stateFor != null)
			{
				stateFor.ReleasePointer(handler, pointerId);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006FF4 File Offset: 0x000051F4
		public static IEventHandler GetCapturingElement(this IPanel panel, int pointerId)
		{
			IEventHandler eventHandler;
			if (panel == null)
			{
				eventHandler = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				eventHandler = ((dispatcher != null) ? dispatcher.pointerState.GetCapturingElement(pointerId) : null);
			}
			return eventHandler;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007024 File Offset: 0x00005224
		public static void ReleasePointer(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ReleasePointer(pointerId);
				}
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007044 File Offset: 0x00005244
		internal static void ActivateCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ActivateCompatibilityMouseEvents(pointerId);
				}
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007064 File Offset: 0x00005264
		internal static void PreventCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.PreventCompatibilityMouseEvents(pointerId);
				}
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007084 File Offset: 0x00005284
		internal static bool ShouldSendCompatibilityMouseEvents(this IPanel panel, IPointerEvent evt)
		{
			Nullable<bool> nullable;
			if (panel == null)
			{
				nullable = default(bool?);
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				nullable = ((dispatcher != null) ? new bool?(dispatcher.pointerState.ShouldSendCompatibilityMouseEvents(evt)) : default(bool?));
			}
			return nullable ?? true;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000070DD File Offset: 0x000052DD
		internal static void ProcessPointerCapture(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ProcessPointerCapture(pointerId);
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000070FD File Offset: 0x000052FD
		internal static void ResetPointerDispatchState(this IPanel panel)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.Reset();
				}
			}
		}
	}
}
