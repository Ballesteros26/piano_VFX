using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200013A RID: 314
	public abstract class EventBase : IDisposable
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x00023608 File Offset: 0x00021808
		protected static long RegisterEventType()
		{
			return EventBase.s_LastTypeId += 1L;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00023628 File Offset: 0x00021828
		public virtual long eventTypeId
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0002362C File Offset: 0x0002182C
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x00023634 File Offset: 0x00021834
		public long timestamp { get; private set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0002363D File Offset: 0x0002183D
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x00023645 File Offset: 0x00021845
		internal ulong eventId { get; private set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0002364E File Offset: 0x0002184E
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00023656 File Offset: 0x00021856
		internal ulong triggerEventId { get; private set; }

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002365F File Offset: 0x0002185F
		internal void SetTriggerEventId(ulong id)
		{
			this.triggerEventId = id;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0002366A File Offset: 0x0002186A
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00023672 File Offset: 0x00021872
		internal EventBase.EventPropagation propagation { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0002367C File Offset: 0x0002187C
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x000236E4 File Offset: 0x000218E4
		internal PropagationPaths path
		{
			get
			{
				bool flag = this.m_Path == null;
				if (flag)
				{
					PropagationPaths.Type type = (this.tricklesDown ? PropagationPaths.Type.TrickleDown : PropagationPaths.Type.None);
					type |= (this.bubbles ? PropagationPaths.Type.BubbleUp : PropagationPaths.Type.None);
					this.m_Path = PropagationPaths.Build(this.leafTarget as VisualElement, type);
					EventDebugger.LogPropagationPaths(this, this.m_Path);
				}
				return this.m_Path;
			}
			set
			{
				bool flag = value != null;
				if (flag)
				{
					this.m_Path = PropagationPaths.Copy(value);
				}
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00023706 File Offset: 0x00021906
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0002370E File Offset: 0x0002190E
		private EventBase.LifeCycleStatus lifeCycleStatus { get; set; }

		// Token: 0x060008DB RID: 2267 RVA: 0x000062F3 File Offset: 0x000044F3
		[Obsolete("Override PreDispatch(IPanel panel) instead.")]
		protected virtual void PreDispatch()
		{
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00023717 File Offset: 0x00021917
		protected internal virtual void PreDispatch(IPanel panel)
		{
			this.PreDispatch();
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000062F3 File Offset: 0x000044F3
		[Obsolete("Override PostDispatch(IPanel panel) instead.")]
		protected virtual void PostDispatch()
		{
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00023721 File Offset: 0x00021921
		protected internal virtual void PostDispatch(IPanel panel)
		{
			this.PostDispatch();
			this.processed = true;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00023734 File Offset: 0x00021934
		public bool bubbles
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.Bubbles) > EventBase.EventPropagation.None;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00023754 File Offset: 0x00021954
		public bool tricklesDown
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.TricklesDown) > EventBase.EventPropagation.None;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00023771 File Offset: 0x00021971
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00023779 File Offset: 0x00021979
		internal IEventHandler leafTarget { get; private set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00023784 File Offset: 0x00021984
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0002379C File Offset: 0x0002199C
		public IEventHandler target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				this.m_Target = value;
				bool flag = this.leafTarget == null;
				if (flag)
				{
					this.leafTarget = value;
				}
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000237C8 File Offset: 0x000219C8
		internal List<IEventHandler> skipElements { get; } = new List<IEventHandler>();

		// Token: 0x060008E6 RID: 2278 RVA: 0x000237D0 File Offset: 0x000219D0
		internal bool Skip(IEventHandler h)
		{
			return this.skipElements.Contains(h);
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x000237F0 File Offset: 0x000219F0
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00023810 File Offset: 0x00021A10
		public bool isPropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagationStopped;
				}
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00023848 File Offset: 0x00021A48
		public void StopPropagation()
		{
			this.isPropagationStopped = true;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00023854 File Offset: 0x00021A54
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x00023874 File Offset: 0x00021A74
		public bool isImmediatePropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ImmediatePropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000238AC File Offset: 0x00021AAC
		public void StopImmediatePropagation()
		{
			this.isPropagationStopped = true;
			this.isImmediatePropagationStopped = true;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x000238C0 File Offset: 0x00021AC0
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x000238E0 File Offset: 0x00021AE0
		public bool isDefaultPrevented
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.DefaultPrevented) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.DefaultPrevented;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.DefaultPrevented;
				}
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00023918 File Offset: 0x00021B18
		public void PreventDefault()
		{
			bool flag = (this.propagation & EventBase.EventPropagation.Cancellable) == EventBase.EventPropagation.Cancellable;
			if (flag)
			{
				this.isDefaultPrevented = true;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0002393F File Offset: 0x00021B3F
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x00023947 File Offset: 0x00021B47
		public PropagationPhase propagationPhase { get; internal set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00023950 File Offset: 0x00021B50
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x00023968 File Offset: 0x00021B68
		public virtual IEventHandler currentTarget
		{
			get
			{
				return this.m_CurrentTarget;
			}
			internal set
			{
				this.m_CurrentTarget = value;
				bool flag = this.imguiEvent != null;
				if (flag)
				{
					VisualElement visualElement = this.currentTarget as VisualElement;
					bool flag2 = visualElement != null;
					if (flag2)
					{
						this.imguiEvent.mousePosition = visualElement.WorldToLocal(this.originalMousePosition);
					}
					else
					{
						this.imguiEvent.mousePosition = this.originalMousePosition;
					}
				}
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x000239D0 File Offset: 0x00021BD0
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x000239F0 File Offset: 0x00021BF0
		public bool dispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatching) > EventBase.LifeCycleStatus.None;
			}
			internal set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatching;
					this.dispatched = true;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatching;
				}
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00023A30 File Offset: 0x00021C30
		internal void MarkReceivedByDispatcher()
		{
			Debug.Assert(!this.dispatched, "Events cannot be dispatched more than once.");
			this.dispatched = true;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00023A50 File Offset: 0x00021C50
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00023A74 File Offset: 0x00021C74
		private bool dispatched
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatched) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatched;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatched;
				}
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00023AB4 File Offset: 0x00021CB4
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00023AD8 File Offset: 0x00021CD8
		internal bool processed
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Processed) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Processed;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Processed;
				}
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00023B18 File Offset: 0x00021D18
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00023B38 File Offset: 0x00021D38
		internal bool stopDispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.StopDispatch) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.StopDispatch;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.StopDispatch;
				}
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00023B74 File Offset: 0x00021D74
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00023B98 File Offset: 0x00021D98
		internal bool propagateToIMGUI
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagateToIMGUI) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00023BD8 File Offset: 0x00021DD8
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00023BF8 File Offset: 0x00021DF8
		private bool imguiEventIsValid
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.IMGUIEventIsValid) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00023C34 File Offset: 0x00021E34
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00023C58 File Offset: 0x00021E58
		public Event imguiEvent
		{
			get
			{
				return this.imguiEventIsValid ? this.m_ImguiEvent : null;
			}
			protected set
			{
				bool flag = this.m_ImguiEvent == null;
				if (flag)
				{
					this.m_ImguiEvent = new Event();
				}
				bool flag2 = value != null;
				if (flag2)
				{
					this.m_ImguiEvent.CopyFrom(value);
					this.imguiEventIsValid = true;
					this.originalMousePosition = value.mousePosition;
				}
				else
				{
					this.imguiEventIsValid = false;
				}
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00023CB8 File Offset: 0x00021EB8
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00023CC0 File Offset: 0x00021EC0
		public Vector2 originalMousePosition { get; private set; }

		// Token: 0x06000905 RID: 2309 RVA: 0x00023CC9 File Offset: 0x00021EC9
		protected virtual void Init()
		{
			this.LocalInit();
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00023CD4 File Offset: 0x00021ED4
		private void LocalInit()
		{
			this.timestamp = Panel.TimeSinceStartupMs();
			this.triggerEventId = 0UL;
			ulong num = EventBase.s_NextEventId;
			EventBase.s_NextEventId = num + 1UL;
			this.eventId = num;
			this.propagation = EventBase.EventPropagation.None;
			PropagationPaths path = this.m_Path;
			if (path != null)
			{
				path.Release();
			}
			this.m_Path = null;
			this.leafTarget = null;
			this.target = null;
			this.skipElements.Clear();
			this.isPropagationStopped = false;
			this.isImmediatePropagationStopped = false;
			this.isDefaultPrevented = false;
			this.propagationPhase = PropagationPhase.None;
			this.originalMousePosition = Vector2.zero;
			this.m_CurrentTarget = null;
			this.dispatch = false;
			this.stopDispatch = false;
			this.propagateToIMGUI = true;
			this.dispatched = false;
			this.processed = false;
			this.imguiEventIsValid = false;
			this.pooled = false;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00023DB4 File Offset: 0x00021FB4
		protected EventBase()
		{
			this.m_ImguiEvent = null;
			this.LocalInit();
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00023DD8 File Offset: 0x00021FD8
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x00023DF8 File Offset: 0x00021FF8
		protected bool pooled
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Pooled) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Pooled;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Pooled;
				}
			}
		}

		// Token: 0x0600090A RID: 2314
		internal abstract void Acquire();

		// Token: 0x0600090B RID: 2315
		public abstract void Dispose();

		// Token: 0x040003E7 RID: 999
		private static long s_LastTypeId = 0L;

		// Token: 0x040003E8 RID: 1000
		private static ulong s_NextEventId = 0UL;

		// Token: 0x040003ED RID: 1005
		private PropagationPaths m_Path;

		// Token: 0x040003F0 RID: 1008
		private IEventHandler m_Target;

		// Token: 0x040003F3 RID: 1011
		private IEventHandler m_CurrentTarget;

		// Token: 0x040003F4 RID: 1012
		private Event m_ImguiEvent;

		// Token: 0x0200013B RID: 315
		[Flags]
		internal enum EventPropagation
		{
			// Token: 0x040003F7 RID: 1015
			None = 0,
			// Token: 0x040003F8 RID: 1016
			Bubbles = 1,
			// Token: 0x040003F9 RID: 1017
			TricklesDown = 2,
			// Token: 0x040003FA RID: 1018
			Cancellable = 4
		}

		// Token: 0x0200013C RID: 316
		[Flags]
		private enum LifeCycleStatus
		{
			// Token: 0x040003FC RID: 1020
			None = 0,
			// Token: 0x040003FD RID: 1021
			PropagationStopped = 1,
			// Token: 0x040003FE RID: 1022
			ImmediatePropagationStopped = 2,
			// Token: 0x040003FF RID: 1023
			DefaultPrevented = 4,
			// Token: 0x04000400 RID: 1024
			Dispatching = 8,
			// Token: 0x04000401 RID: 1025
			Pooled = 16,
			// Token: 0x04000402 RID: 1026
			IMGUIEventIsValid = 32,
			// Token: 0x04000403 RID: 1027
			StopDispatch = 64,
			// Token: 0x04000404 RID: 1028
			PropagateToIMGUI = 128,
			// Token: 0x04000405 RID: 1029
			Dispatched = 512,
			// Token: 0x04000406 RID: 1030
			Processed = 1024
		}
	}
}
