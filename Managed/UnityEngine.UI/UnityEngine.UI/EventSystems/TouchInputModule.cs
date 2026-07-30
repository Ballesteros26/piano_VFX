using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006B RID: 107
	[Obsolete("TouchInputModule is no longer required as Touch input is now handled in StandaloneInputModule.")]
	[AddComponentMenu("Event/Touch Input Module")]
	public class TouchInputModule : PointerInputModule
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x00018B21 File Offset: 0x00016D21
		protected TouchInputModule()
		{
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00018B29 File Offset: 0x00016D29
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x00018B31 File Offset: 0x00016D31
		[Obsolete("allowActivationOnStandalone has been deprecated. Use forceModuleActive instead (UnityUpgradable) -> forceModuleActive")]
		public bool allowActivationOnStandalone
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00018B29 File Offset: 0x00016D29
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x00018B31 File Offset: 0x00016D31
		public bool forceModuleActive
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00018B3C File Offset: 0x00016D3C
		public override void UpdateModule()
		{
			if (!base.eventSystem.isFocused)
			{
				if (this.m_InputPointerEvent != null && this.m_InputPointerEvent.pointerDrag != null && this.m_InputPointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(this.m_InputPointerEvent.pointerDrag, this.m_InputPointerEvent, ExecuteEvents.endDragHandler);
				}
				this.m_InputPointerEvent = null;
			}
			this.m_LastMousePosition = this.m_MousePosition;
			this.m_MousePosition = base.input.mousePosition;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00018BBE File Offset: 0x00016DBE
		public override bool IsModuleSupported()
		{
			return this.forceModuleActive || base.input.touchSupported;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00018BD8 File Offset: 0x00016DD8
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			if (this.m_ForceModuleActive)
			{
				return true;
			}
			if (this.UseFakeInput())
			{
				return base.input.GetMouseButtonDown(0) | ((this.m_MousePosition - this.m_LastMousePosition).sqrMagnitude > 0f);
			}
			return base.input.touchCount > 0;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00018C3D File Offset: 0x00016E3D
		private bool UseFakeInput()
		{
			return !base.input.touchSupported;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00018C4D File Offset: 0x00016E4D
		public override void Process()
		{
			if (this.UseFakeInput())
			{
				this.FakeTouches();
				return;
			}
			this.ProcessTouchEvents();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00018C64 File Offset: 0x00016E64
		private void FakeTouches()
		{
			PointerInputModule.MouseButtonEventData eventData = this.GetMousePointerEventData(0).GetButtonState(PointerEventData.InputButton.Left).eventData;
			if (eventData.PressedThisFrame())
			{
				eventData.buttonData.delta = Vector2.zero;
			}
			this.ProcessTouchPress(eventData.buttonData, eventData.PressedThisFrame(), eventData.ReleasedThisFrame());
			if (base.input.GetMouseButton(0))
			{
				this.ProcessMove(eventData.buttonData);
				this.ProcessDrag(eventData.buttonData);
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00018CDC File Offset: 0x00016EDC
		private void ProcessTouchEvents()
		{
			for (int i = 0; i < base.input.touchCount; i++)
			{
				Touch touch = base.input.GetTouch(i);
				if (touch.type != TouchType.Indirect)
				{
					bool flag;
					bool flag2;
					PointerEventData touchPointerEventData = base.GetTouchPointerEventData(touch, out flag, out flag2);
					this.ProcessTouchPress(touchPointerEventData, flag, flag2);
					if (!flag2)
					{
						this.ProcessMove(touchPointerEventData);
						this.ProcessDrag(touchPointerEventData);
					}
					else
					{
						base.RemovePointerData(touchPointerEventData);
					}
				}
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00018D4C File Offset: 0x00016F4C
		protected void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
		{
			GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			if (pressed)
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, pointerEvent);
				if (pointerEvent.pointerEnter != gameObject)
				{
					base.HandlePointerExitAndEnter(pointerEvent, gameObject);
					pointerEvent.pointerEnter = gameObject;
				}
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == pointerEvent.lastPress)
				{
					if (unscaledTime - pointerEvent.clickTime < 0.3f)
					{
						int num = pointerEvent.clickCount + 1;
						pointerEvent.clickCount = num;
					}
					else
					{
						pointerEvent.clickCount = 1;
					}
					pointerEvent.clickTime = unscaledTime;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}
				pointerEvent.pointerPress = gameObject2;
				pointerEvent.rawPointerPress = gameObject;
				pointerEvent.clickTime = unscaledTime;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
				this.m_InputPointerEvent = pointerEvent;
			}
			if (released)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerPress == eventHandler && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
				this.m_InputPointerEvent = pointerEvent;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00018336 File Offset: 0x00016536
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00018F58 File Offset: 0x00017158
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(this.UseFakeInput() ? "Input: Faked" : "Input: Touch");
			if (this.UseFakeInput())
			{
				PointerEventData lastPointerEventData = base.GetLastPointerEventData(-1);
				if (lastPointerEventData != null)
				{
					stringBuilder.AppendLine(lastPointerEventData.ToString());
				}
			}
			else
			{
				foreach (KeyValuePair<int, PointerEventData> keyValuePair in this.m_PointerData)
				{
					stringBuilder.AppendLine(keyValuePair.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000203 RID: 515
		private Vector2 m_LastMousePosition;

		// Token: 0x04000204 RID: 516
		private Vector2 m_MousePosition;

		// Token: 0x04000205 RID: 517
		private PointerEventData m_InputPointerEvent;

		// Token: 0x04000206 RID: 518
		[SerializeField]
		[FormerlySerializedAs("m_AllowActivationOnStandalone")]
		private bool m_ForceModuleActive;
	}
}
