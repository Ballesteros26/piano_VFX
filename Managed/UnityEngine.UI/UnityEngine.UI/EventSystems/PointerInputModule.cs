using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000069 RID: 105
	public abstract class PointerInputModule : BaseInputModule
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x000179AE File Offset: 0x00015BAE
		protected bool GetPointerData(int id, out PointerEventData data, bool create)
		{
			if (!this.m_PointerData.TryGetValue(id, out data) && create)
			{
				data = new PointerEventData(base.eventSystem)
				{
					pointerId = id
				};
				this.m_PointerData.Add(id, data);
				return true;
			}
			return false;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000179E9 File Offset: 0x00015BE9
		protected void RemovePointerData(PointerEventData data)
		{
			this.m_PointerData.Remove(data.pointerId);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00017A00 File Offset: 0x00015C00
		protected PointerEventData GetTouchPointerEventData(Touch input, out bool pressed, out bool released)
		{
			PointerEventData pointerEventData;
			bool pointerData = this.GetPointerData(input.fingerId, out pointerEventData, true);
			pointerEventData.Reset();
			pressed = pointerData || input.phase == TouchPhase.Began;
			released = input.phase == TouchPhase.Canceled || input.phase == TouchPhase.Ended;
			if (pointerData)
			{
				pointerEventData.position = input.position;
			}
			if (pressed)
			{
				pointerEventData.delta = Vector2.zero;
			}
			else
			{
				pointerEventData.delta = input.position - pointerEventData.position;
			}
			pointerEventData.position = input.position;
			pointerEventData.button = PointerEventData.InputButton.Left;
			if (input.phase == TouchPhase.Canceled)
			{
				pointerEventData.pointerCurrentRaycast = default(RaycastResult);
			}
			else
			{
				base.eventSystem.RaycastAll(pointerEventData, this.m_RaycastResultCache);
				RaycastResult raycastResult = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
				pointerEventData.pointerCurrentRaycast = raycastResult;
				this.m_RaycastResultCache.Clear();
			}
			return pointerEventData;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00017AE8 File Offset: 0x00015CE8
		protected void CopyFromTo(PointerEventData from, PointerEventData to)
		{
			to.position = from.position;
			to.delta = from.delta;
			to.scrollDelta = from.scrollDelta;
			to.pointerCurrentRaycast = from.pointerCurrentRaycast;
			to.pointerEnter = from.pointerEnter;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00017B28 File Offset: 0x00015D28
		protected PointerEventData.FramePressState StateForMouseButton(int buttonId)
		{
			bool mouseButtonDown = base.input.GetMouseButtonDown(buttonId);
			bool mouseButtonUp = base.input.GetMouseButtonUp(buttonId);
			if (mouseButtonDown && mouseButtonUp)
			{
				return PointerEventData.FramePressState.PressedAndReleased;
			}
			if (mouseButtonDown)
			{
				return PointerEventData.FramePressState.Pressed;
			}
			if (mouseButtonUp)
			{
				return PointerEventData.FramePressState.Released;
			}
			return PointerEventData.FramePressState.NotChanged;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00017B61 File Offset: 0x00015D61
		protected virtual PointerInputModule.MouseState GetMousePointerEventData()
		{
			return this.GetMousePointerEventData(0);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00017B6C File Offset: 0x00015D6C
		protected virtual PointerInputModule.MouseState GetMousePointerEventData(int id)
		{
			PointerEventData pointerEventData;
			bool pointerData = this.GetPointerData(-1, out pointerEventData, true);
			pointerEventData.Reset();
			if (pointerData)
			{
				pointerEventData.position = base.input.mousePosition;
			}
			Vector2 mousePosition = base.input.mousePosition;
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				pointerEventData.position = new Vector2(-1f, -1f);
				pointerEventData.delta = Vector2.zero;
			}
			else
			{
				pointerEventData.delta = mousePosition - pointerEventData.position;
				pointerEventData.position = mousePosition;
			}
			pointerEventData.scrollDelta = base.input.mouseScrollDelta;
			pointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(pointerEventData, this.m_RaycastResultCache);
			RaycastResult raycastResult = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			pointerEventData.pointerCurrentRaycast = raycastResult;
			this.m_RaycastResultCache.Clear();
			PointerEventData pointerEventData2;
			this.GetPointerData(-2, out pointerEventData2, true);
			this.CopyFromTo(pointerEventData, pointerEventData2);
			pointerEventData2.button = PointerEventData.InputButton.Right;
			PointerEventData pointerEventData3;
			this.GetPointerData(-3, out pointerEventData3, true);
			this.CopyFromTo(pointerEventData, pointerEventData3);
			pointerEventData3.button = PointerEventData.InputButton.Middle;
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Left, this.StateForMouseButton(0), pointerEventData);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Right, this.StateForMouseButton(1), pointerEventData2);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, this.StateForMouseButton(2), pointerEventData3);
			return this.m_MouseState;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00017CB0 File Offset: 0x00015EB0
		protected PointerEventData GetLastPointerEventData(int id)
		{
			PointerEventData pointerEventData;
			this.GetPointerData(id, out pointerEventData, false);
			return pointerEventData;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00017CCC File Offset: 0x00015ECC
		private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			return !useDragThreshold || (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00017CF8 File Offset: 0x00015EF8
		protected virtual void ProcessMove(PointerEventData pointerEvent)
		{
			GameObject gameObject = ((Cursor.lockState == CursorLockMode.Locked) ? null : pointerEvent.pointerCurrentRaycast.gameObject);
			base.HandlePointerExitAndEnter(pointerEvent, gameObject);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00017D28 File Offset: 0x00015F28
		protected virtual void ProcessDrag(PointerEventData pointerEvent)
		{
			if (!pointerEvent.IsPointerMoving() || Cursor.lockState == CursorLockMode.Locked || pointerEvent.pointerDrag == null)
			{
				return;
			}
			if (!pointerEvent.dragging && PointerInputModule.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public override bool IsPointerOverGameObject(int pointerId)
		{
			PointerEventData lastPointerEventData = this.GetLastPointerEventData(pointerId);
			return lastPointerEventData != null && lastPointerEventData.pointerEnter != null;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00017E18 File Offset: 0x00016018
		protected void ClearSelection()
		{
			BaseEventData baseEventData = this.GetBaseEventData();
			foreach (PointerEventData pointerEventData in this.m_PointerData.Values)
			{
				base.HandlePointerExitAndEnter(pointerEventData, null);
			}
			this.m_PointerData.Clear();
			base.eventSystem.SetSelectedGameObject(null, baseEventData);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00017E90 File Offset: 0x00016090
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("<b>Pointer Input Module of type: </b>" + base.GetType());
			stringBuilder.AppendLine();
			foreach (KeyValuePair<int, PointerEventData> keyValuePair in this.m_PointerData)
			{
				if (keyValuePair.Value != null)
				{
					stringBuilder.AppendLine("<B>Pointer:</b> " + keyValuePair.Key);
					stringBuilder.AppendLine(keyValuePair.Value.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00017F38 File Offset: 0x00016138
		protected void DeselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
		{
			if (ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo) != base.eventSystem.currentSelectedGameObject)
			{
				base.eventSystem.SetSelectedGameObject(null, pointerEvent);
			}
		}

		// Token: 0x040001EF RID: 495
		public const int kMouseLeftId = -1;

		// Token: 0x040001F0 RID: 496
		public const int kMouseRightId = -2;

		// Token: 0x040001F1 RID: 497
		public const int kMouseMiddleId = -3;

		// Token: 0x040001F2 RID: 498
		public const int kFakeTouchesId = -4;

		// Token: 0x040001F3 RID: 499
		protected Dictionary<int, PointerEventData> m_PointerData = new Dictionary<int, PointerEventData>();

		// Token: 0x040001F4 RID: 500
		private readonly PointerInputModule.MouseState m_MouseState = new PointerInputModule.MouseState();

		// Token: 0x020000BF RID: 191
		protected class ButtonState
		{
			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001A0F5 File Offset: 0x000182F5
			// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0001A0FD File Offset: 0x000182FD
			public PointerInputModule.MouseButtonEventData eventData
			{
				get
				{
					return this.m_EventData;
				}
				set
				{
					this.m_EventData = value;
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001A106 File Offset: 0x00018306
			// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0001A10E File Offset: 0x0001830E
			public PointerEventData.InputButton button
			{
				get
				{
					return this.m_Button;
				}
				set
				{
					this.m_Button = value;
				}
			}

			// Token: 0x0400030E RID: 782
			private PointerEventData.InputButton m_Button;

			// Token: 0x0400030F RID: 783
			private PointerInputModule.MouseButtonEventData m_EventData;
		}

		// Token: 0x020000C0 RID: 192
		protected class MouseState
		{
			// Token: 0x060006B9 RID: 1721 RVA: 0x0001A118 File Offset: 0x00018318
			public bool AnyPressesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.PressedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060006BA RID: 1722 RVA: 0x0001A158 File Offset: 0x00018358
			public bool AnyReleasesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.ReleasedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060006BB RID: 1723 RVA: 0x0001A198 File Offset: 0x00018398
			public PointerInputModule.ButtonState GetButtonState(PointerEventData.InputButton button)
			{
				PointerInputModule.ButtonState buttonState = null;
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].button == button)
					{
						buttonState = this.m_TrackedButtons[i];
						break;
					}
				}
				if (buttonState == null)
				{
					buttonState = new PointerInputModule.ButtonState
					{
						button = button,
						eventData = new PointerInputModule.MouseButtonEventData()
					};
					this.m_TrackedButtons.Add(buttonState);
				}
				return buttonState;
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x0001A208 File Offset: 0x00018408
			public void SetButtonState(PointerEventData.InputButton button, PointerEventData.FramePressState stateForMouseButton, PointerEventData data)
			{
				PointerInputModule.ButtonState buttonState = this.GetButtonState(button);
				buttonState.eventData.buttonState = stateForMouseButton;
				buttonState.eventData.buttonData = data;
			}

			// Token: 0x04000310 RID: 784
			private List<PointerInputModule.ButtonState> m_TrackedButtons = new List<PointerInputModule.ButtonState>();
		}

		// Token: 0x020000C1 RID: 193
		public class MouseButtonEventData
		{
			// Token: 0x060006BE RID: 1726 RVA: 0x0001A23B File Offset: 0x0001843B
			public bool PressedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Pressed || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x0001A250 File Offset: 0x00018450
			public bool ReleasedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Released || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x04000311 RID: 785
			public PointerEventData.FramePressState buttonState;

			// Token: 0x04000312 RID: 786
			public PointerEventData buttonData;
		}
	}
}
