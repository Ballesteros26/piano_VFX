using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000042 RID: 66
	internal class Panel : BaseVisualElementPanel
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006580 File Offset: 0x00004780
		public override VisualElement visualTree
		{
			get
			{
				return this.m_RootContainer;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006598 File Offset: 0x00004798
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000065A0 File Offset: 0x000047A0
		public override EventDispatcher dispatcher { get; protected set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000065AC File Offset: 0x000047AC
		public TimerEventScheduler timerEventScheduler
		{
			get
			{
				TimerEventScheduler timerEventScheduler;
				if ((timerEventScheduler = this.m_Scheduler) == null)
				{
					timerEventScheduler = (this.m_Scheduler = new TimerEventScheduler());
				}
				return timerEventScheduler;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000065D8 File Offset: 0x000047D8
		internal override IScheduler scheduler
		{
			get
			{
				return this.timerEventScheduler;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000065F0 File Offset: 0x000047F0
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000065F8 File Offset: 0x000047F8
		public override ScriptableObject ownerObject { get; protected set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006601 File Offset: 0x00004801
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00006609 File Offset: 0x00004809
		public override ContextType contextType { get; protected set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006612 File Offset: 0x00004812
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000661A File Offset: 0x0000481A
		public override SavePersistentViewData saveViewData { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006623 File Offset: 0x00004823
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000662B File Offset: 0x0000482B
		public override GetViewDataDictionary getViewDataDictionary { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006634 File Offset: 0x00004834
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000663C File Offset: 0x0000483C
		public override FocusController focusController { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006645 File Offset: 0x00004845
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000664D File Offset: 0x0000484D
		public override EventInterests IMGUIEventInterests { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00006656 File Offset: 0x00004856
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000665D File Offset: 0x0000485D
		internal static LoadResourceFunction loadResourceFunc { private get; set; }

		// Token: 0x060001A9 RID: 425 RVA: 0x00006668 File Offset: 0x00004868
		internal static Object LoadResource(string pathName, Type type, float dpiScaling)
		{
			bool flag = Panel.loadResourceFunc != null;
			Object @object;
			if (flag)
			{
				@object = Panel.loadResourceFunc(pathName, type, dpiScaling);
			}
			else
			{
				@object = Resources.Load(pathName, type);
			}
			return @object;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000066A5 File Offset: 0x000048A5
		internal void Focus()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.SetFocusToLastFocusedElement();
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000066BA File Offset: 0x000048BA
		internal void Blur()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.BlurLastFocusedElement();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000066D0 File Offset: 0x000048D0
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000066E8 File Offset: 0x000048E8
		internal string name
		{
			get
			{
				return this.m_PanelName;
			}
			set
			{
				this.m_PanelName = value;
				this.CreateMarkers();
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000066FC File Offset: 0x000048FC
		private void CreateMarkers()
		{
			bool flag = !string.IsNullOrEmpty(this.m_PanelName);
			if (flag)
			{
				this.m_MarkerUpdate = new ProfilerMarker("Panel.Update." + this.m_PanelName);
				this.m_MarkerLayout = new ProfilerMarker("Panel.Layout." + this.m_PanelName);
				this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings." + this.m_PanelName);
				this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations." + this.m_PanelName);
			}
			else
			{
				this.m_MarkerUpdate = new ProfilerMarker("Panel.Update");
				this.m_MarkerLayout = new ProfilerMarker("Panel.Layout");
				this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings");
				this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000067CE File Offset: 0x000049CE
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x000067D5 File Offset: 0x000049D5
		internal static TimeMsFunction TimeSinceStartup { private get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000067DD File Offset: 0x000049DD
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000067E5 File Offset: 0x000049E5
		public override int IMGUIContainersCount { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000067EE File Offset: 0x000049EE
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x000067F6 File Offset: 0x000049F6
		public override IMGUIContainer rootIMGUIContainer { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006800 File Offset: 0x00004A00
		internal override uint version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006818 File Offset: 0x00004A18
		internal override uint repaintVersion
		{
			get
			{
				return this.m_RepaintVersion;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00006830 File Offset: 0x00004A30
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00006848 File Offset: 0x00004A48
		internal override Shader standardShader
		{
			get
			{
				return this.m_StandardShader;
			}
			set
			{
				bool flag = this.m_StandardShader != value;
				if (flag)
				{
					this.m_StandardShader = value;
					base.InvokeStandardShaderChanged();
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006878 File Offset: 0x00004A78
		internal static Panel CreateEditorPanel(ScriptableObject ownerObject)
		{
			return new Panel(ownerObject, ContextType.Editor, new EventDispatcher());
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006898 File Offset: 0x00004A98
		public Panel(ScriptableObject ownerObject, ContextType contextType, EventDispatcher dispatcher)
		{
			this.ownerObject = ownerObject;
			this.contextType = contextType;
			this.dispatcher = dispatcher;
			this.repaintData = new RepaintData();
			this.cursorManager = new CursorManager();
			base.contextualMenuManager = null;
			this.m_VisualTreeUpdater = new VisualTreeUpdater(this);
			this.m_RootContainer = new VisualElement
			{
				name = VisualElementUtils.GetUniqueName("unity-panel-container"),
				viewDataKey = "PanelContainer"
			};
			this.visualTree.SetPanel(this);
			this.focusController = new FocusController(new VisualElementFocusRing(this.visualTree, VisualElementFocusRing.DefaultFocusOrder.ChildOrder));
			this.CreateMarkers();
			base.InvokeHierarchyChanged(this.visualTree, HierarchyChangeType.Add);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006968 File Offset: 0x00004B68
		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_VisualTreeUpdater.Dispose();
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000699C File Offset: 0x00004B9C
		public static long TimeSinceStartupMs()
		{
			TimeMsFunction timeSinceStartup = Panel.TimeSinceStartup;
			return (timeSinceStartup != null) ? timeSinceStartup() : Panel.DefaultTimeSinceStartupMs();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000069C4 File Offset: 0x00004BC4
		internal static long DefaultTimeSinceStartupMs()
		{
			return (long)(Time.realtimeSinceStartup * 1000f);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000069E4 File Offset: 0x00004BE4
		internal static VisualElement PickAllWithoutValidatingLayout(VisualElement root, Vector2 point)
		{
			return Panel.PickAll(root, point, null);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006A00 File Offset: 0x00004C00
		private static VisualElement PickAll(VisualElement root, Vector2 point, List<VisualElement> picked = null)
		{
			return Panel.PerformPick(root, point, picked);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006A1C File Offset: 0x00004C1C
		private static VisualElement PerformPick(VisualElement root, Vector2 point, List<VisualElement> picked = null)
		{
			bool flag = root.resolvedStyle.display == DisplayStyle.None;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = null;
			}
			else
			{
				bool flag2 = root.pickingMode == PickingMode.Ignore && root.hierarchy.childCount == 0;
				if (flag2)
				{
					visualElement = null;
				}
				else
				{
					bool flag3 = !root.worldBoundingBox.Contains(point);
					if (flag3)
					{
						visualElement = null;
					}
					else
					{
						Vector2 vector = root.WorldToLocal(point);
						bool flag4 = root.ContainsPoint(vector);
						bool flag5 = !flag4 && root.ShouldClip();
						if (flag5)
						{
							visualElement = null;
						}
						else
						{
							VisualElement visualElement2 = null;
							int childCount = root.hierarchy.childCount;
							for (int i = childCount - 1; i >= 0; i--)
							{
								VisualElement visualElement3 = root.hierarchy[i];
								VisualElement visualElement4 = Panel.PerformPick(visualElement3, point, picked);
								bool flag6 = visualElement2 == null && visualElement4 != null && visualElement4.visible;
								if (flag6)
								{
									visualElement2 = visualElement4;
								}
							}
							bool flag7 = picked != null && root.enabledInHierarchy && root.visible && root.pickingMode == PickingMode.Position && flag4;
							if (flag7)
							{
								picked.Add(root);
							}
							bool flag8 = visualElement2 != null;
							if (flag8)
							{
								visualElement = visualElement2;
							}
							else
							{
								PickingMode pickingMode = root.pickingMode;
								if (pickingMode != PickingMode.Position)
								{
									if (pickingMode != PickingMode.Ignore)
									{
									}
								}
								else
								{
									bool flag9 = flag4 && root.enabledInHierarchy && root.visible;
									if (flag9)
									{
										return root;
									}
								}
								visualElement = null;
							}
						}
					}
				}
			}
			return visualElement;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006BAC File Offset: 0x00004DAC
		public override VisualElement PickAll(Vector2 point, List<VisualElement> picked)
		{
			this.ValidateLayout();
			bool flag = picked != null;
			if (flag)
			{
				picked.Clear();
			}
			return Panel.PickAll(this.visualTree, point, picked);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public override VisualElement Pick(Vector2 point)
		{
			this.ValidateLayout();
			Vector2 vector;
			VisualElement topElementUnderPointer = this.m_TopElementUnderPointers.GetTopElementUnderPointer(PointerId.mousePointerId, out vector);
			bool flag = (vector - point).sqrMagnitude < 0.25f;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = topElementUnderPointer;
			}
			else
			{
				visualElement = Panel.PickAll(this.visualTree, point, null);
			}
			return visualElement;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006C40 File Offset: 0x00004E40
		public override void ValidateLayout()
		{
			bool flag = !this.m_ValidatingLayout;
			if (flag)
			{
				this.m_ValidatingLayout = true;
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
				this.m_ValidatingLayout = false;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006C92 File Offset: 0x00004E92
		public override void UpdateAnimations()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Animation);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006CA2 File Offset: 0x00004EA2
		public override void UpdateBindings()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Bindings);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006CB2 File Offset: 0x00004EB2
		public override void ApplyStyles()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006CC4 File Offset: 0x00004EC4
		private void UpdateForRepaint()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.ViewData);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Repaint);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006D14 File Offset: 0x00004F14
		public override void Repaint(Event e)
		{
			bool flag = this.contextType == ContextType.Editor;
			if (flag)
			{
				Debug.Assert(GUIClip.Internal_GetCount() == 0, "UIElement is not compatible with IMGUI GUIClips, only GUIClip.ParentClipScope");
			}
			this.m_RepaintVersion = this.version;
			bool flag2 = this.contextType == ContextType.Editor;
			if (flag2)
			{
				base.pixelsPerPoint = GUIUtility.pixelsPerPoint;
			}
			this.repaintData.repaintEvent = e;
			using (this.m_MarkerUpdate.Auto())
			{
				this.UpdateForRepaint();
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006DAC File Offset: 0x00004FAC
		internal override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			this.m_Version += 1U;
			this.m_VisualTreeUpdater.OnVersionChanged(ve, versionChangeType);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006DCB File Offset: 0x00004FCB
		internal override void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase)
		{
			this.m_VisualTreeUpdater.SetUpdater(updater, phase);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006DDC File Offset: 0x00004FDC
		internal override IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase)
		{
			return this.m_VisualTreeUpdater.GetUpdater(phase);
		}

		// Token: 0x040000B4 RID: 180
		private VisualElement m_RootContainer;

		// Token: 0x040000B5 RID: 181
		private VisualTreeUpdater m_VisualTreeUpdater;

		// Token: 0x040000B6 RID: 182
		private string m_PanelName;

		// Token: 0x040000B7 RID: 183
		private uint m_Version = 0U;

		// Token: 0x040000B8 RID: 184
		private uint m_RepaintVersion = 0U;

		// Token: 0x040000B9 RID: 185
		internal static Action BeforeUpdaterChange;

		// Token: 0x040000BA RID: 186
		internal static Action AfterUpdaterChange;

		// Token: 0x040000BB RID: 187
		private ProfilerMarker m_MarkerUpdate;

		// Token: 0x040000BC RID: 188
		private ProfilerMarker m_MarkerLayout;

		// Token: 0x040000BD RID: 189
		private ProfilerMarker m_MarkerBindings;

		// Token: 0x040000BE RID: 190
		private ProfilerMarker m_MarkerAnimations;

		// Token: 0x040000BF RID: 191
		private static ProfilerMarker s_MarkerPickAll = new ProfilerMarker("Panel.PickAll");

		// Token: 0x040000C1 RID: 193
		private TimerEventScheduler m_Scheduler;

		// Token: 0x040000CC RID: 204
		private Shader m_StandardShader;

		// Token: 0x040000CD RID: 205
		private bool m_ValidatingLayout = false;
	}
}
