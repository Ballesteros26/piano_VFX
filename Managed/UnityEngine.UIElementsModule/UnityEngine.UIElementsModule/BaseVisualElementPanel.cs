using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003D RID: 61
	internal abstract class BaseVisualElementPanel : IPanel, IDisposable
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600013D RID: 317
		// (set) Token: 0x0600013E RID: 318
		public abstract EventInterests IMGUIEventInterests { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013F RID: 319
		// (set) Token: 0x06000140 RID: 320
		public abstract ScriptableObject ownerObject { get; protected set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000141 RID: 321
		// (set) Token: 0x06000142 RID: 322
		public abstract SavePersistentViewData saveViewData { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000143 RID: 323
		// (set) Token: 0x06000144 RID: 324
		public abstract GetViewDataDictionary getViewDataDictionary { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000145 RID: 325
		// (set) Token: 0x06000146 RID: 326
		public abstract int IMGUIContainersCount { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000147 RID: 327
		// (set) Token: 0x06000148 RID: 328
		public abstract FocusController focusController { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000149 RID: 329
		// (set) Token: 0x0600014A RID: 330
		public abstract IMGUIContainer rootIMGUIContainer { get; set; }

		// Token: 0x0600014B RID: 331 RVA: 0x00006010 File Offset: 0x00004210
		protected BaseVisualElementPanel()
		{
			this.yogaConfig = new YogaConfig();
			this.yogaConfig.UseWebDefaults = YogaConfig.Default.UseWebDefaults;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000606E File Offset: 0x0000426E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006080 File Offset: 0x00004280
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.ownerObject != null;
					if (flag)
					{
						UIElementsUtility.RemoveCachedPanel(this.ownerObject.GetInstanceID());
					}
				}
				this.yogaConfig = null;
				this.disposed = true;
			}
		}

		// Token: 0x0600014E RID: 334
		public abstract void Repaint(Event e);

		// Token: 0x0600014F RID: 335
		public abstract void ValidateLayout();

		// Token: 0x06000150 RID: 336
		public abstract void UpdateAnimations();

		// Token: 0x06000151 RID: 337
		public abstract void UpdateBindings();

		// Token: 0x06000152 RID: 338
		public abstract void ApplyStyles();

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000060D4 File Offset: 0x000042D4
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000060EC File Offset: 0x000042EC
		internal float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_Scale, value);
				if (flag)
				{
					this.m_Scale = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					this.yogaConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006144 File Offset: 0x00004344
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000615C File Offset: 0x0000435C
		internal float pixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_PixelsPerPoint, value);
				if (flag)
				{
					this.m_PixelsPerPoint = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					this.yogaConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000061B4 File Offset: 0x000043B4
		public float scaledPixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint * this.m_Scale;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000061D3 File Offset: 0x000043D3
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000061DB File Offset: 0x000043DB
		internal PanelClearFlags clearFlags { get; set; } = PanelClearFlags.All;

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000061E4 File Offset: 0x000043E4
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000061EC File Offset: 0x000043EC
		internal bool duringLayoutPhase { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000061F8 File Offset: 0x000043F8
		internal bool isDirty
		{
			get
			{
				return this.version != this.repaintVersion;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015D RID: 349
		internal abstract uint version { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600015E RID: 350
		internal abstract uint repaintVersion { get; }

		// Token: 0x0600015F RID: 351
		internal abstract void OnVersionChanged(VisualElement ele, VersionChangeType changeTypeFlag);

		// Token: 0x06000160 RID: 352
		internal abstract void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase);

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000621B File Offset: 0x0000441B
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00006223 File Offset: 0x00004423
		internal virtual RepaintData repaintData { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000622C File Offset: 0x0000442C
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00006234 File Offset: 0x00004434
		internal virtual ICursorManager cursorManager { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000623D File Offset: 0x0000443D
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00006245 File Offset: 0x00004445
		public ContextualMenuManager contextualMenuManager { get; internal set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000167 RID: 359
		public abstract VisualElement visualTree { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000168 RID: 360
		// (set) Token: 0x06000169 RID: 361
		public abstract EventDispatcher dispatcher { get; protected set; }

		// Token: 0x0600016A RID: 362 RVA: 0x0000624E File Offset: 0x0000444E
		internal void SendEvent(EventBase e, DispatchMode dispatchMode = DispatchMode.Default)
		{
			Debug.Assert(this.dispatcher != null);
			EventDispatcher dispatcher = this.dispatcher;
			if (dispatcher != null)
			{
				dispatcher.Dispatch(e, this, dispatchMode);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600016B RID: 363
		internal abstract IScheduler scheduler { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600016C RID: 364
		// (set) Token: 0x0600016D RID: 365
		public abstract ContextType contextType { get; protected set; }

		// Token: 0x0600016E RID: 366
		public abstract VisualElement Pick(Vector2 point);

		// Token: 0x0600016F RID: 367
		public abstract VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006275 File Offset: 0x00004475
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000627D File Offset: 0x0000447D
		internal bool disposed { get; private set; }

		// Token: 0x06000172 RID: 370
		internal abstract IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase);

		// Token: 0x06000173 RID: 371 RVA: 0x00006288 File Offset: 0x00004488
		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_TopElementUnderPointers.GetTopElementUnderPointer(pointerId);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000062A6 File Offset: 0x000044A6
		private void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, Vector2 pointerPos)
		{
			this.m_TopElementUnderPointers.SetElementUnderPointer(newElementUnderPointer, pointerId, pointerPos);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000062B8 File Offset: 0x000044B8
		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, EventBase triggerEvent)
		{
			this.m_TopElementUnderPointers.SetElementUnderPointer(newElementUnderPointer, triggerEvent);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000062C9 File Offset: 0x000044C9
		internal void CommitElementUnderPointers()
		{
			this.m_TopElementUnderPointers.CommitElementUnderPointers(this.dispatcher);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000177 RID: 375
		// (set) Token: 0x06000178 RID: 376
		internal abstract Shader standardShader { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000062E0 File Offset: 0x000044E0
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000062F3 File Offset: 0x000044F3
		internal virtual Shader standardWorldSpaceShader
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600017B RID: 379 RVA: 0x000062F8 File Offset: 0x000044F8
		// (remove) Token: 0x0600017C RID: 380 RVA: 0x00006330 File Offset: 0x00004530
		[field: DebuggerBrowsable(0)]
		internal event Action standardShaderChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600017D RID: 381 RVA: 0x00006368 File Offset: 0x00004568
		// (remove) Token: 0x0600017E RID: 382 RVA: 0x000063A0 File Offset: 0x000045A0
		[field: DebuggerBrowsable(0)]
		internal event Action standardWorldSpaceShaderChanged;

		// Token: 0x0600017F RID: 383 RVA: 0x000063D8 File Offset: 0x000045D8
		protected void InvokeStandardShaderChanged()
		{
			bool flag = this.standardShaderChanged != null;
			if (flag)
			{
				this.standardShaderChanged.Invoke();
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006400 File Offset: 0x00004600
		protected void InvokeStandardWorldSpaceShaderChanged()
		{
			bool flag = this.standardWorldSpaceShaderChanged != null;
			if (flag)
			{
				this.standardWorldSpaceShaderChanged.Invoke();
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000181 RID: 385 RVA: 0x00006428 File Offset: 0x00004628
		// (remove) Token: 0x06000182 RID: 386 RVA: 0x00006460 File Offset: 0x00004660
		[field: DebuggerBrowsable(0)]
		internal event HierarchyEvent hierarchyChanged;

		// Token: 0x06000183 RID: 387 RVA: 0x00006498 File Offset: 0x00004698
		internal void InvokeHierarchyChanged(VisualElement ve, HierarchyChangeType changeType)
		{
			bool flag = this.hierarchyChanged != null;
			if (flag)
			{
				this.hierarchyChanged(ve, changeType);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000064C4 File Offset: 0x000046C4
		internal void UpdateElementUnderPointers()
		{
			foreach (int num in PointerId.hoveringPointers)
			{
				bool flag = PointerDeviceState.GetPanel(num) != this;
				if (flag)
				{
					this.SetElementUnderPointer(null, num, new Vector2(float.MinValue, float.MinValue));
				}
				else
				{
					Vector2 pointerPosition = PointerDeviceState.GetPointerPosition(num);
					VisualElement visualElement = this.PickAll(pointerPosition, null);
					this.SetElementUnderPointer(visualElement, num, pointerPosition);
				}
			}
			this.CommitElementUnderPointers();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006560 File Offset: 0x00004760
		public void Update()
		{
			this.scheduler.UpdateScheduledEvents();
			this.ValidateLayout();
			this.UpdateBindings();
		}

		// Token: 0x040000A7 RID: 167
		private float m_Scale = 1f;

		// Token: 0x040000A8 RID: 168
		internal YogaConfig yogaConfig;

		// Token: 0x040000A9 RID: 169
		private float m_PixelsPerPoint = 1f;

		// Token: 0x040000B0 RID: 176
		internal ElementUnderPointer m_TopElementUnderPointers = new ElementUnderPointer();
	}
}
