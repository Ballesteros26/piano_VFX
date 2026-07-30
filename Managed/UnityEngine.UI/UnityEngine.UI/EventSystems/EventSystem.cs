using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000063 RID: 99
	[AddComponentMenu("Event/Event System")]
	public class EventSystem : UIBehaviour
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0001686F File Offset: 0x00014A6F
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0001688C File Offset: 0x00014A8C
		public static EventSystem current
		{
			get
			{
				if (EventSystem.m_EventSystems.Count <= 0)
				{
					return null;
				}
				return EventSystem.m_EventSystems[0];
			}
			set
			{
				int num = EventSystem.m_EventSystems.IndexOf(value);
				if (num >= 0)
				{
					EventSystem.m_EventSystems.RemoveAt(num);
					EventSystem.m_EventSystems.Insert(0, value);
				}
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000168C0 File Offset: 0x00014AC0
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x000168C8 File Offset: 0x00014AC8
		public bool sendNavigationEvents
		{
			get
			{
				return this.m_sendNavigationEvents;
			}
			set
			{
				this.m_sendNavigationEvents = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x000168D1 File Offset: 0x00014AD1
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x000168D9 File Offset: 0x00014AD9
		public int pixelDragThreshold
		{
			get
			{
				return this.m_DragThreshold;
			}
			set
			{
				this.m_DragThreshold = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x000168E2 File Offset: 0x00014AE2
		public BaseInputModule currentInputModule
		{
			get
			{
				return this.m_CurrentInputModule;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x000168EA File Offset: 0x00014AEA
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x000168F2 File Offset: 0x00014AF2
		public GameObject firstSelectedGameObject
		{
			get
			{
				return this.m_FirstSelected;
			}
			set
			{
				this.m_FirstSelected = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000168FB File Offset: 0x00014AFB
		public GameObject currentSelectedGameObject
		{
			get
			{
				return this.m_CurrentSelected;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00013BAD File Offset: 0x00011DAD
		[Obsolete("lastSelectedGameObject is no longer supported")]
		public GameObject lastSelectedGameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00016903 File Offset: 0x00014B03
		public bool isFocused
		{
			get
			{
				return this.m_HasFocus;
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001690B File Offset: 0x00014B0B
		protected EventSystem()
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00016934 File Offset: 0x00014B34
		public void UpdateModules()
		{
			base.GetComponents<BaseInputModule>(this.m_SystemInputModules);
			for (int i = this.m_SystemInputModules.Count - 1; i >= 0; i--)
			{
				if (!this.m_SystemInputModules[i] || !this.m_SystemInputModules[i].IsActive())
				{
					this.m_SystemInputModules.RemoveAt(i);
				}
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00016997 File Offset: 0x00014B97
		public bool alreadySelecting
		{
			get
			{
				return this.m_SelectionGuard;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000169A0 File Offset: 0x00014BA0
		public void SetSelectedGameObject(GameObject selected, BaseEventData pointer)
		{
			if (this.m_SelectionGuard)
			{
				Debug.LogError("Attempting to select " + selected + "while already selecting an object.");
				return;
			}
			this.m_SelectionGuard = true;
			if (selected == this.m_CurrentSelected)
			{
				this.m_SelectionGuard = false;
				return;
			}
			ExecuteEvents.Execute<IDeselectHandler>(this.m_CurrentSelected, pointer, ExecuteEvents.deselectHandler);
			this.m_CurrentSelected = selected;
			ExecuteEvents.Execute<ISelectHandler>(this.m_CurrentSelected, pointer, ExecuteEvents.selectHandler);
			this.m_SelectionGuard = false;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00016A1A File Offset: 0x00014C1A
		private BaseEventData baseEventDataCache
		{
			get
			{
				if (this.m_DummyData == null)
				{
					this.m_DummyData = new BaseEventData(this);
				}
				return this.m_DummyData;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00016A36 File Offset: 0x00014C36
		public void SetSelectedGameObject(GameObject selected)
		{
			this.SetSelectedGameObject(selected, this.baseEventDataCache);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016A48 File Offset: 0x00014C48
		private static int RaycastComparer(RaycastResult lhs, RaycastResult rhs)
		{
			if (lhs.module != rhs.module)
			{
				Camera eventCamera = lhs.module.eventCamera;
				Camera eventCamera2 = rhs.module.eventCamera;
				if (eventCamera != null && eventCamera2 != null && eventCamera.depth != eventCamera2.depth)
				{
					if (eventCamera.depth < eventCamera2.depth)
					{
						return 1;
					}
					if (eventCamera.depth == eventCamera2.depth)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (lhs.module.sortOrderPriority != rhs.module.sortOrderPriority)
					{
						return rhs.module.sortOrderPriority.CompareTo(lhs.module.sortOrderPriority);
					}
					if (lhs.module.renderOrderPriority != rhs.module.renderOrderPriority)
					{
						return rhs.module.renderOrderPriority.CompareTo(lhs.module.renderOrderPriority);
					}
				}
			}
			if (lhs.sortingLayer != rhs.sortingLayer)
			{
				int layerValueFromID = SortingLayer.GetLayerValueFromID(rhs.sortingLayer);
				int layerValueFromID2 = SortingLayer.GetLayerValueFromID(lhs.sortingLayer);
				return layerValueFromID.CompareTo(layerValueFromID2);
			}
			if (lhs.sortingOrder != rhs.sortingOrder)
			{
				return rhs.sortingOrder.CompareTo(lhs.sortingOrder);
			}
			if (lhs.depth != rhs.depth && lhs.module.rootRaycaster == rhs.module.rootRaycaster)
			{
				return rhs.depth.CompareTo(lhs.depth);
			}
			if (lhs.distance != rhs.distance)
			{
				return lhs.distance.CompareTo(rhs.distance);
			}
			return lhs.index.CompareTo(rhs.index);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00016BF8 File Offset: 0x00014DF8
		public void RaycastAll(PointerEventData eventData, List<RaycastResult> raycastResults)
		{
			raycastResults.Clear();
			List<BaseRaycaster> raycasters = RaycasterManager.GetRaycasters();
			for (int i = 0; i < raycasters.Count; i++)
			{
				BaseRaycaster baseRaycaster = raycasters[i];
				if (!(baseRaycaster == null) && baseRaycaster.IsActive())
				{
					baseRaycaster.Raycast(eventData, raycastResults);
				}
			}
			raycastResults.Sort(EventSystem.s_RaycastComparer);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00016C4E File Offset: 0x00014E4E
		public bool IsPointerOverGameObject()
		{
			return this.IsPointerOverGameObject(-1);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00016C57 File Offset: 0x00014E57
		public bool IsPointerOverGameObject(int pointerId)
		{
			return this.m_CurrentInputModule != null && this.m_CurrentInputModule.IsPointerOverGameObject(pointerId);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00016C75 File Offset: 0x00014E75
		protected override void OnEnable()
		{
			base.OnEnable();
			EventSystem.m_EventSystems.Add(this);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00016C88 File Offset: 0x00014E88
		protected override void OnDisable()
		{
			if (this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.DeactivateModule();
				this.m_CurrentInputModule = null;
			}
			EventSystem.m_EventSystems.Remove(this);
			base.OnDisable();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00016CBC File Offset: 0x00014EBC
		private void TickModules()
		{
			for (int i = 0; i < this.m_SystemInputModules.Count; i++)
			{
				if (this.m_SystemInputModules[i] != null)
				{
					this.m_SystemInputModules[i].UpdateModule();
				}
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00016D04 File Offset: 0x00014F04
		protected virtual void OnApplicationFocus(bool hasFocus)
		{
			this.m_HasFocus = hasFocus;
			if (!this.m_HasFocus)
			{
				this.TickModules();
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00016D1C File Offset: 0x00014F1C
		protected virtual void Update()
		{
			if (EventSystem.current != this)
			{
				return;
			}
			this.TickModules();
			bool flag = false;
			int i = 0;
			while (i < this.m_SystemInputModules.Count)
			{
				BaseInputModule baseInputModule = this.m_SystemInputModules[i];
				if (baseInputModule.IsModuleSupported() && baseInputModule.ShouldActivateModule())
				{
					if (this.m_CurrentInputModule != baseInputModule)
					{
						this.ChangeEventModule(baseInputModule);
						flag = true;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			if (this.m_CurrentInputModule == null)
			{
				for (int j = 0; j < this.m_SystemInputModules.Count; j++)
				{
					BaseInputModule baseInputModule2 = this.m_SystemInputModules[j];
					if (baseInputModule2.IsModuleSupported())
					{
						this.ChangeEventModule(baseInputModule2);
						flag = true;
						break;
					}
				}
			}
			if (!flag && this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.Process();
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00016DEE File Offset: 0x00014FEE
		private void ChangeEventModule(BaseInputModule module)
		{
			if (this.m_CurrentInputModule == module)
			{
				return;
			}
			if (this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.DeactivateModule();
			}
			if (module != null)
			{
				module.ActivateModule();
			}
			this.m_CurrentInputModule = module;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00016E30 File Offset: 0x00015030
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Selected:</b>" + this.currentSelectedGameObject);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine((this.m_CurrentInputModule != null) ? this.m_CurrentInputModule.ToString() : "No module");
			return stringBuilder.ToString();
		}

		// Token: 0x040001B8 RID: 440
		private List<BaseInputModule> m_SystemInputModules = new List<BaseInputModule>();

		// Token: 0x040001B9 RID: 441
		private BaseInputModule m_CurrentInputModule;

		// Token: 0x040001BA RID: 442
		private static List<EventSystem> m_EventSystems = new List<EventSystem>();

		// Token: 0x040001BB RID: 443
		[SerializeField]
		[FormerlySerializedAs("m_Selected")]
		private GameObject m_FirstSelected;

		// Token: 0x040001BC RID: 444
		[SerializeField]
		private bool m_sendNavigationEvents = true;

		// Token: 0x040001BD RID: 445
		[SerializeField]
		private int m_DragThreshold = 10;

		// Token: 0x040001BE RID: 446
		private GameObject m_CurrentSelected;

		// Token: 0x040001BF RID: 447
		private bool m_HasFocus = true;

		// Token: 0x040001C0 RID: 448
		private bool m_SelectionGuard;

		// Token: 0x040001C1 RID: 449
		private BaseEventData m_DummyData;

		// Token: 0x040001C2 RID: 450
		private static readonly Comparison<RaycastResult> s_RaycastComparer = new Comparison<RaycastResult>(EventSystem.RaycastComparer);
	}
}
