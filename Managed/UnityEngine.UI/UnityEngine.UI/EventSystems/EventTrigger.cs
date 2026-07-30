using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000064 RID: 100
	[AddComponentMenu("Event/Event Trigger")]
	public class EventTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00016EB0 File Offset: 0x000150B0
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00016EB8 File Offset: 0x000150B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use triggers instead (UnityUpgradable) -> triggers", true)]
		public List<EventTrigger.Entry> delegates
		{
			get
			{
				return this.triggers;
			}
			set
			{
				this.triggers = value;
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00016EC1 File Offset: 0x000150C1
		protected EventTrigger()
		{
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00016EC9 File Offset: 0x000150C9
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00016EE4 File Offset: 0x000150E4
		public List<EventTrigger.Entry> triggers
		{
			get
			{
				if (this.m_Delegates == null)
				{
					this.m_Delegates = new List<EventTrigger.Entry>();
				}
				return this.m_Delegates;
			}
			set
			{
				this.m_Delegates = value;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00016EF0 File Offset: 0x000150F0
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			int i = 0;
			int count = this.triggers.Count;
			while (i < count)
			{
				EventTrigger.Entry entry = this.triggers[i];
				if (entry.eventID == id && entry.callback != null)
				{
					entry.callback.Invoke(eventData);
				}
				i++;
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00016F3F File Offset: 0x0001513F
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00016F49 File Offset: 0x00015149
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00016F53 File Offset: 0x00015153
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drag, eventData);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00016F5D File Offset: 0x0001515D
		public virtual void OnDrop(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drop, eventData);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00016F67 File Offset: 0x00015167
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerDown, eventData);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00016F71 File Offset: 0x00015171
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerUp, eventData);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00016F7B File Offset: 0x0001517B
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00016F85 File Offset: 0x00015185
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Select, eventData);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00016F90 File Offset: 0x00015190
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Deselect, eventData);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00016F9B File Offset: 0x0001519B
		public virtual void OnScroll(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Scroll, eventData);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00016FA5 File Offset: 0x000151A5
		public virtual void OnMove(AxisEventData eventData)
		{
			this.Execute(EventTriggerType.Move, eventData);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00016FB0 File Offset: 0x000151B0
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.UpdateSelected, eventData);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00016FBA File Offset: 0x000151BA
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.InitializePotentialDrag, eventData);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00016FC5 File Offset: 0x000151C5
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.BeginDrag, eventData);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00016FD0 File Offset: 0x000151D0
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.EndDrag, eventData);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00016FDB File Offset: 0x000151DB
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Submit, eventData);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00016FE6 File Offset: 0x000151E6
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Cancel, eventData);
		}

		// Token: 0x040001C3 RID: 451
		[FormerlySerializedAs("delegates")]
		[SerializeField]
		private List<EventTrigger.Entry> m_Delegates;

		// Token: 0x020000BB RID: 187
		[Serializable]
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
		}

		// Token: 0x020000BC RID: 188
		[Serializable]
		public class Entry
		{
			// Token: 0x0400030B RID: 779
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x0400030C RID: 780
			public EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
		}
	}
}
