using System;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006A RID: 106
	[AddComponentMenu("Event/Standalone Input Module")]
	public class StandaloneInputModule : PointerInputModule
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x00017F80 File Offset: 0x00016180
		protected StandaloneInputModule()
		{
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00008CC2 File Offset: 0x00006EC2
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public StandaloneInputModule.InputMode inputMode
		{
			get
			{
				return StandaloneInputModule.InputMode.Mouse;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00017FD5 File Offset: 0x000161D5
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00017FDD File Offset: 0x000161DD
		[Obsolete("allowActivationOnMobileDevice has been deprecated. Use forceModuleActive instead (UnityUpgradable) -> forceModuleActive")]
		public bool allowActivationOnMobileDevice
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

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00017FD5 File Offset: 0x000161D5
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00017FDD File Offset: 0x000161DD
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

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00017FE6 File Offset: 0x000161E6
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00017FEE File Offset: 0x000161EE
		public float inputActionsPerSecond
		{
			get
			{
				return this.m_InputActionsPerSecond;
			}
			set
			{
				this.m_InputActionsPerSecond = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00017FF7 File Offset: 0x000161F7
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x00017FFF File Offset: 0x000161FF
		public float repeatDelay
		{
			get
			{
				return this.m_RepeatDelay;
			}
			set
			{
				this.m_RepeatDelay = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00018008 File Offset: 0x00016208
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x00018010 File Offset: 0x00016210
		public string horizontalAxis
		{
			get
			{
				return this.m_HorizontalAxis;
			}
			set
			{
				this.m_HorizontalAxis = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00018019 File Offset: 0x00016219
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00018021 File Offset: 0x00016221
		public string verticalAxis
		{
			get
			{
				return this.m_VerticalAxis;
			}
			set
			{
				this.m_VerticalAxis = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001802A File Offset: 0x0001622A
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00018032 File Offset: 0x00016232
		public string submitButton
		{
			get
			{
				return this.m_SubmitButton;
			}
			set
			{
				this.m_SubmitButton = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001803B File Offset: 0x0001623B
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x00018043 File Offset: 0x00016243
		public string cancelButton
		{
			get
			{
				return this.m_CancelButton;
			}
			set
			{
				this.m_CancelButton = value;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000C577 File Offset: 0x0000A777
		private bool ShouldIgnoreEventsOnNoFocus()
		{
			return true;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001804C File Offset: 0x0001624C
		public override void UpdateModule()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				if (this.m_InputPointerEvent != null && this.m_InputPointerEvent.pointerDrag != null && this.m_InputPointerEvent.dragging)
				{
					this.ReleaseMouse(this.m_InputPointerEvent, this.m_InputPointerEvent.pointerCurrentRaycast.gameObject);
				}
				this.m_InputPointerEvent = null;
				return;
			}
			this.m_LastMousePosition = this.m_MousePosition;
			this.m_MousePosition = base.input.mousePosition;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000180DC File Offset: 0x000162DC
		private void ReleaseMouse(PointerEventData pointerEvent, GameObject currentOverGo)
		{
			ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
			GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
			if (pointerEvent.pointerClick == eventHandler && pointerEvent.eligibleForClick)
			{
				ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerClick, pointerEvent, ExecuteEvents.pointerClickHandler);
			}
			else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				ExecuteEvents.ExecuteHierarchy<IDropHandler>(currentOverGo, pointerEvent, ExecuteEvents.dropHandler);
			}
			pointerEvent.eligibleForClick = false;
			pointerEvent.pointerPress = null;
			pointerEvent.rawPointerPress = null;
			pointerEvent.pointerClick = null;
			if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
			}
			pointerEvent.dragging = false;
			pointerEvent.pointerDrag = null;
			if (currentOverGo != pointerEvent.pointerEnter)
			{
				base.HandlePointerExitAndEnter(pointerEvent, null);
				base.HandlePointerExitAndEnter(pointerEvent, currentOverGo);
			}
			this.m_InputPointerEvent = pointerEvent;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000181C6 File Offset: 0x000163C6
		public override bool IsModuleSupported()
		{
			return this.m_ForceModuleActive || base.input.mousePresent || base.input.touchSupported;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000181EC File Offset: 0x000163EC
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			bool flag = this.m_ForceModuleActive;
			flag |= base.input.GetButtonDown(this.m_SubmitButton);
			flag |= base.input.GetButtonDown(this.m_CancelButton);
			flag |= !Mathf.Approximately(base.input.GetAxisRaw(this.m_HorizontalAxis), 0f);
			flag |= !Mathf.Approximately(base.input.GetAxisRaw(this.m_VerticalAxis), 0f);
			flag |= (this.m_MousePosition - this.m_LastMousePosition).sqrMagnitude > 0f;
			flag |= base.input.GetMouseButtonDown(0);
			if (base.input.touchCount > 0)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000182B8 File Offset: 0x000164B8
		public override void ActivateModule()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			base.ActivateModule();
			this.m_MousePosition = base.input.mousePosition;
			this.m_LastMousePosition = base.input.mousePosition;
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00018336 File Offset: 0x00016536
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00018344 File Offset: 0x00016544
		public override void Process()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			bool flag = this.SendUpdateEventToSelectedObject();
			if (!this.ProcessTouchEvents() && base.input.mousePresent)
			{
				this.ProcessMouseEvent();
			}
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= this.SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendSubmitEventToSelectedObject();
				}
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000183AC File Offset: 0x000165AC
		private bool ProcessTouchEvents()
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
			return base.input.touchCount > 0;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00018428 File Offset: 0x00016628
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
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (gameObject2 == null)
				{
					gameObject2 = eventHandler;
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
				pointerEvent.pointerClick = eventHandler;
				pointerEvent.clickTime = unscaledTime;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (released)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerClick == eventHandler2 && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerClick, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				pointerEvent.pointerClick = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
			this.m_InputPointerEvent = pointerEvent;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00018640 File Offset: 0x00016840
		protected bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			if (base.input.GetButtonDown(this.m_SubmitButton))
			{
				ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
			}
			if (base.input.GetButtonDown(this.m_CancelButton))
			{
				ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
			}
			return baseEventData.used;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000186C4 File Offset: 0x000168C4
		private Vector2 GetRawMoveVector()
		{
			Vector2 zero = Vector2.zero;
			zero.x = base.input.GetAxisRaw(this.m_HorizontalAxis);
			zero.y = base.input.GetAxisRaw(this.m_VerticalAxis);
			if (base.input.GetButtonDown(this.m_HorizontalAxis))
			{
				if (zero.x < 0f)
				{
					zero.x = -1f;
				}
				if (zero.x > 0f)
				{
					zero.x = 1f;
				}
			}
			if (base.input.GetButtonDown(this.m_VerticalAxis))
			{
				if (zero.y < 0f)
				{
					zero.y = -1f;
				}
				if (zero.y > 0f)
				{
					zero.y = 1f;
				}
			}
			return zero;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00018794 File Offset: 0x00016994
		protected bool SendMoveEventToSelectedObject()
		{
			float unscaledTime = Time.unscaledTime;
			Vector2 rawMoveVector = this.GetRawMoveVector();
			if (Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f))
			{
				this.m_ConsecutiveMoveCount = 0;
				return false;
			}
			bool flag = Vector2.Dot(rawMoveVector, this.m_LastMoveVector) > 0f;
			if (flag && this.m_ConsecutiveMoveCount == 1)
			{
				if (unscaledTime <= this.m_PrevActionTime + this.m_RepeatDelay)
				{
					return false;
				}
			}
			else if (unscaledTime <= this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond)
			{
				return false;
			}
			AxisEventData axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
			if (axisEventData.moveDir != MoveDirection.None)
			{
				ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
				if (!flag)
				{
					this.m_ConsecutiveMoveCount = 0;
				}
				this.m_ConsecutiveMoveCount++;
				this.m_PrevActionTime = unscaledTime;
				this.m_LastMoveVector = rawMoveVector;
			}
			else
			{
				this.m_ConsecutiveMoveCount = 0;
			}
			return axisEventData.used;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00018892 File Offset: 0x00016A92
		protected void ProcessMouseEvent()
		{
			this.ProcessMouseEvent(0);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00008CC2 File Offset: 0x00006EC2
		[Obsolete("This method is no longer checked, overriding it with return true does nothing!")]
		protected virtual bool ForceAutoSelect()
		{
			return false;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001889C File Offset: 0x00016A9C
		protected void ProcessMouseEvent(int id)
		{
			PointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(id);
			PointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
			this.m_CurrentFocusedGameObject = eventData.buttonData.pointerCurrentRaycast.gameObject;
			this.ProcessMousePress(eventData);
			this.ProcessMove(eventData.buttonData);
			this.ProcessDrag(eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);
			if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject), eventData.buttonData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00018990 File Offset: 0x00016B90
		protected bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
			return baseEventData.used;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000189D8 File Offset: 0x00016BD8
		protected void ProcessMousePress(PointerInputModule.MouseButtonEventData data)
		{
			PointerEventData buttonData = data.buttonData;
			GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
			if (data.PressedThisFrame())
			{
				buttonData.eligibleForClick = true;
				buttonData.delta = Vector2.zero;
				buttonData.dragging = false;
				buttonData.useDragThreshold = true;
				buttonData.pressPosition = buttonData.position;
				buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, buttonData);
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (gameObject2 == null)
				{
					gameObject2 = eventHandler;
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == buttonData.lastPress)
				{
					if (unscaledTime - buttonData.clickTime < 0.3f)
					{
						PointerEventData pointerEventData = buttonData;
						int num = pointerEventData.clickCount + 1;
						pointerEventData.clickCount = num;
					}
					else
					{
						buttonData.clickCount = 1;
					}
					buttonData.clickTime = unscaledTime;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.pointerPress = gameObject2;
				buttonData.rawPointerPress = gameObject;
				buttonData.pointerClick = eventHandler;
				buttonData.clickTime = unscaledTime;
				buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (buttonData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
				}
				this.m_InputPointerEvent = buttonData;
			}
			if (data.ReleasedThisFrame())
			{
				this.ReleaseMouse(buttonData, gameObject);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00018B19 File Offset: 0x00016D19
		protected GameObject GetCurrentFocusedGameObject()
		{
			return this.m_CurrentFocusedGameObject;
		}

		// Token: 0x040001F5 RID: 501
		private float m_PrevActionTime;

		// Token: 0x040001F6 RID: 502
		private Vector2 m_LastMoveVector;

		// Token: 0x040001F7 RID: 503
		private int m_ConsecutiveMoveCount;

		// Token: 0x040001F8 RID: 504
		private Vector2 m_LastMousePosition;

		// Token: 0x040001F9 RID: 505
		private Vector2 m_MousePosition;

		// Token: 0x040001FA RID: 506
		private GameObject m_CurrentFocusedGameObject;

		// Token: 0x040001FB RID: 507
		private PointerEventData m_InputPointerEvent;

		// Token: 0x040001FC RID: 508
		[SerializeField]
		private string m_HorizontalAxis = "Horizontal";

		// Token: 0x040001FD RID: 509
		[SerializeField]
		private string m_VerticalAxis = "Vertical";

		// Token: 0x040001FE RID: 510
		[SerializeField]
		private string m_SubmitButton = "Submit";

		// Token: 0x040001FF RID: 511
		[SerializeField]
		private string m_CancelButton = "Cancel";

		// Token: 0x04000200 RID: 512
		[SerializeField]
		private float m_InputActionsPerSecond = 10f;

		// Token: 0x04000201 RID: 513
		[SerializeField]
		private float m_RepeatDelay = 0.5f;

		// Token: 0x04000202 RID: 514
		[SerializeField]
		[FormerlySerializedAs("m_AllowActivationOnMobileDevice")]
		private bool m_ForceModuleActive;

		// Token: 0x020000C2 RID: 194
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public enum InputMode
		{
			// Token: 0x04000314 RID: 788
			Mouse,
			// Token: 0x04000315 RID: 789
			Buttons
		}
	}
}
