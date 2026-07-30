using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000024 RID: 36
	public class IMGUIContainer : VisualElement, IDisposable
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000045B0 File Offset: 0x000027B0
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000045C8 File Offset: 0x000027C8
		public Action onGUIHandler
		{
			get
			{
				return this.m_OnGUIHandler;
			}
			set
			{
				bool flag = this.m_OnGUIHandler != value;
				if (flag)
				{
					this.m_OnGUIHandler = value;
					base.IncrementVersion(VersionChangeType.Layout);
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004604 File Offset: 0x00002804
		internal ObjectGUIState guiState
		{
			get
			{
				Debug.Assert(!this.useOwnerObjectGUIState);
				bool flag = this.m_ObjectGUIState == null;
				if (flag)
				{
					this.m_ObjectGUIState = new ObjectGUIState();
				}
				return this.m_ObjectGUIState;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004645 File Offset: 0x00002845
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000464D File Offset: 0x0000284D
		internal Rect lastWorldClip { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004658 File Offset: 0x00002858
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004670 File Offset: 0x00002870
		public bool cullingEnabled
		{
			get
			{
				return this.m_CullingEnabled;
			}
			set
			{
				this.m_CullingEnabled = value;
				base.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004688 File Offset: 0x00002888
		private GUILayoutUtility.LayoutCache cache
		{
			get
			{
				bool flag = this.m_Cache == null;
				if (flag)
				{
					this.m_Cache = new GUILayoutUtility.LayoutCache(-1);
				}
				return this.m_Cache;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000046BC File Offset: 0x000028BC
		private float layoutMeasuredWidth
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxWidth);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000046E4 File Offset: 0x000028E4
		private float layoutMeasuredHeight
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxHeight);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000470B File Offset: 0x0000290B
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004713 File Offset: 0x00002913
		public ContextType contextType { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000471C File Offset: 0x0000291C
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00004724 File Offset: 0x00002924
		internal bool focusOnlyIfHasFocusableControls { get; set; } = true;

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000472D File Offset: 0x0000292D
		public override bool canGrabFocus
		{
			get
			{
				return this.focusOnlyIfHasFocusableControls ? (this.hasFocusableControls && base.canGrabFocus) : base.canGrabFocus;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004750 File Offset: 0x00002950
		public IMGUIContainer()
			: this(null)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000475C File Offset: 0x0000295C
		public IMGUIContainer(Action onGUIHandler)
		{
			this.isIMGUIContainer = true;
			base.AddToClassList(IMGUIContainer.ussClassName);
			this.onGUIHandler = onGUIHandler;
			this.contextType = ContextType.Editor;
			base.focusable = true;
			base.requireMeasureFunction = true;
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004820 File Offset: 0x00002A20
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this.lastWorldClip = base.elementPanel.repaintData.currentWorldClip;
			mgc.painter.DrawImmediate(new Action(this.DoIMGUIRepaint), this.cullingEnabled);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004858 File Offset: 0x00002A58
		private void SaveGlobals()
		{
			this.m_GUIGlobals.matrix = GUI.matrix;
			this.m_GUIGlobals.color = GUI.color;
			this.m_GUIGlobals.contentColor = GUI.contentColor;
			this.m_GUIGlobals.backgroundColor = GUI.backgroundColor;
			this.m_GUIGlobals.enabled = GUI.enabled;
			this.m_GUIGlobals.changed = GUI.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				this.m_GUIGlobals.displayIndex = Event.current.displayIndex;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000048EC File Offset: 0x00002AEC
		private void RestoreGlobals()
		{
			GUI.matrix = this.m_GUIGlobals.matrix;
			GUI.color = this.m_GUIGlobals.color;
			GUI.contentColor = this.m_GUIGlobals.contentColor;
			GUI.backgroundColor = this.m_GUIGlobals.backgroundColor;
			GUI.enabled = this.m_GUIGlobals.enabled;
			GUI.changed = this.m_GUIGlobals.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				Event.current.displayIndex = this.m_GUIGlobals.displayIndex;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004984 File Offset: 0x00002B84
		private void DoOnGUI(Event evt, Matrix4x4 parentTransform, Rect clippingRect, bool isComputingLayout, Rect layoutSize, Action onGUIHandler, bool canAffectFocus = true)
		{
			bool flag = onGUIHandler == null || base.panel == null;
			if (!flag)
			{
				int num = GUIClip.Internal_GetCount();
				this.SaveGlobals();
				float layoutMeasuredWidth = this.layoutMeasuredWidth;
				float layoutMeasuredHeight = this.layoutMeasuredHeight;
				UIElementsUtility.BeginContainerGUI(this.cache, evt, this);
				GUI.color = UIElementsUtility.editorPlayModeTintColor;
				bool flag2 = Event.current.type != EventType.Layout;
				if (flag2)
				{
					bool flag3 = this.lostFocus;
					if (flag3)
					{
						bool flag4 = this.focusController != null;
						if (flag4)
						{
							bool flag5 = GUIUtility.OwnsId(GUIUtility.keyboardControl);
							if (flag5)
							{
								GUIUtility.keyboardControl = 0;
								this.focusController.imguiKeyboardControl = 0;
							}
						}
						this.lostFocus = false;
					}
					bool flag6 = this.receivedFocus;
					if (flag6)
					{
						bool flag7 = this.hasFocusableControls;
						if (flag7)
						{
							bool flag8 = this.focusChangeDirection != FocusChangeDirection.unspecified && this.focusChangeDirection != FocusChangeDirection.none;
							if (flag8)
							{
								bool flag9 = this.focusChangeDirection == VisualElementFocusChangeDirection.left;
								if (flag9)
								{
									GUIUtility.SetKeyboardControlToLastControlId();
								}
								else
								{
									bool flag10 = this.focusChangeDirection == VisualElementFocusChangeDirection.right;
									if (flag10)
									{
										GUIUtility.SetKeyboardControlToFirstControlId();
									}
								}
							}
							else
							{
								bool flag11 = GUIUtility.keyboardControl == 0;
								if (flag11)
								{
									GUIUtility.SetKeyboardControlToFirstControlId();
								}
							}
						}
						this.receivedFocus = false;
						this.focusChangeDirection = FocusChangeDirection.unspecified;
						bool flag12 = this.focusController != null;
						if (flag12)
						{
							bool flag13 = this.focusController.imguiKeyboardControl != GUIUtility.keyboardControl;
							if (flag13)
							{
								this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
							}
							this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
						}
					}
				}
				EventType type = Event.current.type;
				bool flag14 = false;
				try
				{
					using (new GUIClip.ParentClipScope(parentTransform, clippingRect))
					{
						onGUIHandler.Invoke();
					}
				}
				catch (Exception ex)
				{
					bool flag15 = type == EventType.Layout;
					if (!flag15)
					{
						throw;
					}
					flag14 = GUIUtility.IsExitGUIException(ex);
					bool flag16 = !flag14;
					if (flag16)
					{
						Debug.LogException(ex);
					}
				}
				finally
				{
					bool flag17 = Event.current.type != EventType.Layout && canAffectFocus;
					if (flag17)
					{
						int keyboardControl = GUIUtility.keyboardControl;
						int num2 = GUIUtility.CheckForTabEvent(Event.current);
						bool flag18 = this.focusController != null;
						if (flag18)
						{
							bool flag19 = num2 < 0;
							if (flag19)
							{
								Focusable leafFocusedElement = this.focusController.GetLeafFocusedElement();
								Focusable focusable = null;
								using (KeyDownEvent pooled = KeyboardEventBase<KeyDownEvent>.GetPooled('\t', KeyCode.Tab, (num2 == -1) ? EventModifiers.None : EventModifiers.Shift))
								{
									focusable = this.focusController.SwitchFocusOnEvent(pooled);
								}
								bool flag20 = leafFocusedElement == this;
								if (flag20)
								{
									bool flag21 = focusable == this;
									if (flag21)
									{
										bool flag22 = num2 == -2;
										if (flag22)
										{
											GUIUtility.SetKeyboardControlToLastControlId();
										}
										else
										{
											bool flag23 = num2 == -1;
											if (flag23)
											{
												GUIUtility.SetKeyboardControlToFirstControlId();
											}
										}
										this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
										this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									}
									else
									{
										GUIUtility.keyboardControl = 0;
										this.focusController.imguiKeyboardControl = 0;
									}
								}
							}
							else
							{
								bool flag24 = num2 > 0;
								if (flag24)
								{
									this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
								}
								else
								{
									bool flag25 = num2 == 0;
									if (flag25)
									{
										bool flag26 = type == EventType.MouseDown && !this.focusOnlyIfHasFocusableControls;
										if (flag26)
										{
											this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, true);
										}
										else
										{
											bool flag27 = keyboardControl != GUIUtility.keyboardControl || type == EventType.MouseDown;
											if (flag27)
											{
												this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
											}
											else
											{
												bool flag28 = GUIUtility.keyboardControl != this.focusController.imguiKeyboardControl;
												if (flag28)
												{
													this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
													bool flag29 = this.focusController.GetLeafFocusedElement() == this;
													if (flag29)
													{
														this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
													}
													else
													{
														this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
													}
												}
											}
										}
									}
								}
							}
						}
						this.hasFocusableControls = GUIUtility.HasFocusableControls();
					}
				}
				UIElementsUtility.EndContainerGUI(evt, layoutSize);
				this.RestoreGlobals();
				bool flag30 = !isComputingLayout && evt.type == EventType.Layout && (!Mathf.Approximately(layoutMeasuredWidth, this.layoutMeasuredWidth) || !Mathf.Approximately(layoutMeasuredHeight, this.layoutMeasuredHeight));
				if (flag30)
				{
					base.IncrementVersion(VersionChangeType.Layout);
				}
				bool flag31 = !flag14;
				if (flag31)
				{
					bool flag32 = evt.type != EventType.Ignore && evt.type != EventType.Used;
					if (flag32)
					{
						int num3 = GUIClip.Internal_GetCount();
						bool flag33 = num3 > num;
						if (flag33)
						{
							Debug.LogError("GUI Error: You are pushing more GUIClips than you are popping. Make sure they are balanced.");
						}
						else
						{
							bool flag34 = num3 < num;
							if (flag34)
							{
								Debug.LogError("GUI Error: You are popping more GUIClips than you are pushing. Make sure they are balanced.");
							}
						}
					}
				}
				while (GUIClip.Internal_GetCount() > num)
				{
					GUIClip.Internal_Pop();
				}
				bool flag35 = evt.type == EventType.Used;
				if (flag35)
				{
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004F1C File Offset: 0x0000311C
		public void MarkDirtyLayout()
		{
			this.m_RefreshCachedLayout = true;
			base.IncrementVersion(VersionChangeType.Layout);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004F30 File Offset: 0x00003130
		public override void HandleEvent(EventBase evt)
		{
			base.HandleEvent(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.propagationPhase != PropagationPhase.TrickleDown && evt.propagationPhase != PropagationPhase.AtTarget && evt.propagationPhase != PropagationPhase.BubbleUp;
				if (!flag2)
				{
					bool flag3 = evt.imguiEvent == null;
					if (!flag3)
					{
						bool isPropagationStopped = evt.isPropagationStopped;
						if (!isPropagationStopped)
						{
							bool flag4 = this.SendEventToIMGUI(evt, true, true);
							if (flag4)
							{
								evt.StopPropagation();
								evt.PreventDefault();
							}
						}
					}
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004FB8 File Offset: 0x000031B8
		private void DoIMGUIRepaint()
		{
			Matrix4x4 currentOffset = base.elementPanel.repaintData.currentOffset;
			this.m_CachedClippingRect = VisualElement.ComputeAAAlignedBound(base.worldClip, currentOffset);
			this.m_CachedTransform = currentOffset * base.worldTransform;
			this.HandleIMGUIEvent(base.elementPanel.repaintData.repaintEvent, this.m_CachedTransform, this.m_CachedClippingRect, this.onGUIHandler, true);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005028 File Offset: 0x00003228
		internal bool SendEventToIMGUI(EventBase evt, bool canAffectFocus = true, bool verifyBounds = true)
		{
			bool flag = evt is IPointerEvent;
			bool flag12;
			if (flag)
			{
				bool flag2 = evt.imguiEvent != null && evt.imguiEvent.isDirectManipulationDevice;
				if (flag2)
				{
					bool flag3 = false;
					EventType rawType = evt.imguiEvent.rawType;
					bool flag4 = evt is PointerDownEvent;
					if (flag4)
					{
						flag3 = true;
						evt.imguiEvent.type = EventType.TouchDown;
					}
					else
					{
						bool flag5 = evt is PointerUpEvent;
						if (flag5)
						{
							flag3 = true;
							evt.imguiEvent.type = EventType.TouchUp;
						}
						else
						{
							bool flag6 = evt is PointerMoveEvent && evt.imguiEvent.rawType == EventType.MouseDrag;
							if (flag6)
							{
								flag3 = true;
								evt.imguiEvent.type = EventType.TouchMove;
							}
							else
							{
								bool flag7 = evt is PointerLeaveEvent;
								if (flag7)
								{
									flag3 = true;
									evt.imguiEvent.type = EventType.TouchLeave;
								}
								else
								{
									bool flag8 = evt is PointerEnterEvent;
									if (flag8)
									{
										flag3 = true;
										evt.imguiEvent.type = EventType.TouchEnter;
									}
									else
									{
										bool flag9 = evt is PointerStationaryEvent;
										if (flag9)
										{
											flag3 = true;
											evt.imguiEvent.type = EventType.TouchStationary;
										}
									}
								}
							}
						}
					}
					bool flag10 = flag3;
					if (flag10)
					{
						bool flag11 = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
						evt.imguiEvent.type = rawType;
						return flag11;
					}
				}
				flag12 = false;
			}
			else
			{
				flag12 = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
			}
			return flag12;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005194 File Offset: 0x00003394
		private bool SendEventToIMGUIRaw(EventBase evt, bool canAffectFocus, bool verifyBounds)
		{
			bool flag = verifyBounds && !this.VerifyBounds(evt);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3;
				using (new EventDebuggerLogIMGUICall(evt))
				{
					flag3 = this.HandleIMGUIEvent(evt.imguiEvent, canAffectFocus);
				}
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000051F8 File Offset: 0x000033F8
		private bool VerifyBounds(EventBase evt)
		{
			return this.IsContainerCapturingTheMouse() || !this.IsLocalEvent(evt) || this.IsEventInsideLocalWindow(evt);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005228 File Offset: 0x00003428
		private bool IsContainerCapturingTheMouse()
		{
			IPanel panel = base.panel;
			IMGUIContainer imguicontainer;
			if (panel == null)
			{
				imguicontainer = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				imguicontainer = ((dispatcher != null) ? dispatcher.pointerState.GetCapturingElement(PointerId.mousePointerId) : null);
			}
			return this == imguicontainer;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005268 File Offset: 0x00003468
		private bool IsLocalEvent(EventBase evt)
		{
			long eventTypeId = evt.eventTypeId;
			return eventTypeId == EventBase<MouseDownEvent>.TypeId() || eventTypeId == EventBase<MouseUpEvent>.TypeId() || eventTypeId == EventBase<MouseMoveEvent>.TypeId() || eventTypeId == EventBase<PointerDownEvent>.TypeId() || eventTypeId == EventBase<PointerUpEvent>.TypeId() || eventTypeId == EventBase<PointerMoveEvent>.TypeId();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000052B4 File Offset: 0x000034B4
		private bool IsEventInsideLocalWindow(EventBase evt)
		{
			Rect currentClipRect = this.GetCurrentClipRect();
			IPointerEvent pointerEvent = evt as IPointerEvent;
			string text = ((pointerEvent != null) ? pointerEvent.pointerType : null);
			bool flag = text == PointerType.touch || text == PointerType.pen;
			return GUIUtility.HitTest(currentClipRect, evt.originalMousePosition, flag);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000530C File Offset: 0x0000350C
		private bool HandleIMGUIEvent(Event e, bool canAffectFocus)
		{
			return this.HandleIMGUIEvent(e, this.onGUIHandler, canAffectFocus);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000532C File Offset: 0x0000352C
		internal bool HandleIMGUIEvent(Event e, Action onGUIHandler, bool canAffectFocus)
		{
			IMGUIContainer.GetCurrentTransformAndClip(this, e, out this.m_CachedTransform, out this.m_CachedClippingRect);
			return this.HandleIMGUIEvent(e, this.m_CachedTransform, this.m_CachedClippingRect, onGUIHandler, canAffectFocus);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005368 File Offset: 0x00003568
		private bool HandleIMGUIEvent(Event e, Matrix4x4 worldTransform, Rect clippingRect, Action onGUIHandler, bool canAffectFocus)
		{
			bool flag = e == null || onGUIHandler == null || base.elementPanel == null || !base.elementPanel.IMGUIEventInterests.WantsEvent(e.rawType);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventType rawType = e.rawType;
				bool flag3 = rawType != EventType.Layout;
				if (flag3)
				{
					bool flag4 = this.m_RefreshCachedLayout || base.elementPanel.IMGUIEventInterests.WantsLayoutPass(e.rawType);
					if (flag4)
					{
						e.type = EventType.Layout;
						this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
						this.m_RefreshCachedLayout = false;
						e.type = rawType;
					}
					else
					{
						this.cache.ResetCursor();
					}
				}
				this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
				bool flag5 = this.newKeyboardFocusControlID > 0;
				if (flag5)
				{
					this.newKeyboardFocusControlID = 0;
					Event @event = new Event
					{
						type = EventType.ExecuteCommand,
						commandName = "NewKeyboardFocus"
					};
					this.HandleIMGUIEvent(@event, true);
				}
				bool flag6 = e.rawType == EventType.Used;
				if (flag6)
				{
					flag2 = true;
				}
				else
				{
					bool flag7 = e.rawType == EventType.MouseUp && this.HasMouseCapture();
					if (flag7)
					{
						GUIUtility.hotControl = 0;
					}
					bool flag8 = base.elementPanel == null;
					if (flag8)
					{
						GUIUtility.ExitGUI();
					}
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000054D4 File Offset: 0x000036D4
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.lostFocus = true;
					base.IncrementVersion(VersionChangeType.Repaint);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
					if (flag3)
					{
						FocusEvent focusEvent = evt as FocusEvent;
						this.receivedFocus = true;
						this.focusChangeDirection = focusEvent.direction;
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<DetachFromPanelEvent>.TypeId();
						if (flag4)
						{
							bool flag5 = base.elementPanel != null;
							if (flag5)
							{
								BaseVisualElementPanel elementPanel = base.elementPanel;
								int num = elementPanel.IMGUIContainersCount;
								elementPanel.IMGUIContainersCount = num - 1;
							}
						}
						else
						{
							bool flag6 = evt.eventTypeId == EventBase<AttachToPanelEvent>.TypeId();
							if (flag6)
							{
								bool flag7 = base.elementPanel != null;
								if (flag7)
								{
									BaseVisualElementPanel elementPanel2 = base.elementPanel;
									int num = elementPanel2.IMGUIContainersCount;
									elementPanel2.IMGUIContainersCount = num + 1;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000055C8 File Offset: 0x000037C8
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = widthMode != VisualElement.MeasureMode.Exactly || heightMode != VisualElement.MeasureMode.Exactly;
			if (flag)
			{
				Event @event = new Event
				{
					type = EventType.Layout
				};
				Rect layout = base.layout;
				if (widthMode == VisualElement.MeasureMode.Exactly)
				{
					layout.width = desiredWidth;
				}
				if (heightMode == VisualElement.MeasureMode.Exactly)
				{
					layout.height = desiredHeight;
				}
				this.DoOnGUI(@event, this.m_CachedTransform, this.m_CachedClippingRect, true, layout, this.onGUIHandler, true);
				num = this.layoutMeasuredWidth;
				num2 = this.layoutMeasuredHeight;
			}
			if (widthMode != VisualElement.MeasureMode.Exactly)
			{
				if (widthMode == VisualElement.MeasureMode.AtMost)
				{
					num = Mathf.Min(num, desiredWidth);
				}
			}
			else
			{
				num = desiredWidth;
			}
			if (heightMode != VisualElement.MeasureMode.Exactly)
			{
				if (heightMode == VisualElement.MeasureMode.AtMost)
				{
					num2 = Mathf.Min(num2, desiredHeight);
				}
			}
			else
			{
				num2 = desiredHeight;
			}
			return new Vector2(num, num2);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000056B0 File Offset: 0x000038B0
		private Rect GetCurrentClipRect()
		{
			Rect rect = this.lastWorldClip;
			bool flag = rect.width == 0f || rect.height == 0f;
			if (flag)
			{
				rect = base.worldBound;
			}
			return rect;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000056F8 File Offset: 0x000038F8
		private static void GetCurrentTransformAndClip(IMGUIContainer container, Event evt, out Matrix4x4 transform, out Rect clipRect)
		{
			clipRect = container.GetCurrentClipRect();
			transform = container.worldTransform;
			bool flag = evt.rawType == EventType.Repaint && container.elementPanel != null;
			if (flag)
			{
				transform = container.elementPanel.repaintData.currentOffset * container.worldTransform;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000575A File Offset: 0x0000395A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000576C File Offset: 0x0000396C
		protected virtual void Dispose(bool disposeManaged)
		{
			if (disposeManaged)
			{
				ObjectGUIState objectGUIState = this.m_ObjectGUIState;
				if (objectGUIState != null)
				{
					objectGUIState.Dispose();
				}
			}
		}

		// Token: 0x04000054 RID: 84
		private Action m_OnGUIHandler;

		// Token: 0x04000055 RID: 85
		private ObjectGUIState m_ObjectGUIState;

		// Token: 0x04000056 RID: 86
		internal bool useOwnerObjectGUIState;

		// Token: 0x04000058 RID: 88
		private bool m_CullingEnabled = false;

		// Token: 0x04000059 RID: 89
		private bool m_RefreshCachedLayout = true;

		// Token: 0x0400005A RID: 90
		private GUILayoutUtility.LayoutCache m_Cache = null;

		// Token: 0x0400005B RID: 91
		private Rect m_CachedClippingRect = Rect.zero;

		// Token: 0x0400005C RID: 92
		private Matrix4x4 m_CachedTransform = Matrix4x4.identity;

		// Token: 0x0400005E RID: 94
		private bool lostFocus = false;

		// Token: 0x0400005F RID: 95
		private bool receivedFocus = false;

		// Token: 0x04000060 RID: 96
		private FocusChangeDirection focusChangeDirection = FocusChangeDirection.unspecified;

		// Token: 0x04000061 RID: 97
		private bool hasFocusableControls = false;

		// Token: 0x04000062 RID: 98
		private int newKeyboardFocusControlID = 0;

		// Token: 0x04000064 RID: 100
		public static readonly string ussClassName = "unity-imgui-container";

		// Token: 0x04000065 RID: 101
		private IMGUIContainer.GUIGlobals m_GUIGlobals;

		// Token: 0x02000025 RID: 37
		public new class UxmlFactory : UxmlFactory<IMGUIContainer, IMGUIContainer.UxmlTraits>
		{
		}

		// Token: 0x02000026 RID: 38
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x060000E2 RID: 226 RVA: 0x000057A8 File Offset: 0x000039A8
			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x000057CC File Offset: 0x000039CC
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}
		}

		// Token: 0x02000028 RID: 40
		private struct GUIGlobals
		{
			// Token: 0x0400006A RID: 106
			public Matrix4x4 matrix;

			// Token: 0x0400006B RID: 107
			public Color color;

			// Token: 0x0400006C RID: 108
			public Color contentColor;

			// Token: 0x0400006D RID: 109
			public Color backgroundColor;

			// Token: 0x0400006E RID: 110
			public bool enabled;

			// Token: 0x0400006F RID: 111
			public bool changed;

			// Token: 0x04000070 RID: 112
			public int displayIndex;
		}
	}
}
