using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000068 RID: 104
	[RequireComponent(typeof(EventSystem))]
	public abstract class BaseInputModule : UIBehaviour
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000175C4 File Offset: 0x000157C4
		public BaseInput input
		{
			get
			{
				if (this.m_InputOverride != null)
				{
					return this.m_InputOverride;
				}
				if (this.m_DefaultInput == null)
				{
					foreach (BaseInput baseInput in base.GetComponents<BaseInput>())
					{
						if (baseInput != null && baseInput.GetType() == typeof(BaseInput))
						{
							this.m_DefaultInput = baseInput;
							break;
						}
					}
					if (this.m_DefaultInput == null)
					{
						this.m_DefaultInput = base.gameObject.AddComponent<BaseInput>();
					}
				}
				return this.m_DefaultInput;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001765B File Offset: 0x0001585B
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00017663 File Offset: 0x00015863
		public BaseInput inputOverride
		{
			get
			{
				return this.m_InputOverride;
			}
			set
			{
				this.m_InputOverride = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001766C File Offset: 0x0001586C
		protected EventSystem eventSystem
		{
			get
			{
				return this.m_EventSystem;
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00017674 File Offset: 0x00015874
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_EventSystem = base.GetComponent<EventSystem>();
			this.m_EventSystem.UpdateModules();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00017693 File Offset: 0x00015893
		protected override void OnDisable()
		{
			this.m_EventSystem.UpdateModules();
			base.OnDisable();
		}

		// Token: 0x060005A2 RID: 1442
		public abstract void Process();

		// Token: 0x060005A3 RID: 1443 RVA: 0x000176A8 File Offset: 0x000158A8
		protected static RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
		{
			for (int i = 0; i < candidates.Count; i++)
			{
				if (!(candidates[i].gameObject == null))
				{
					return candidates[i];
				}
			}
			return default(RaycastResult);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000176EE File Offset: 0x000158EE
		protected static MoveDirection DetermineMoveDirection(float x, float y)
		{
			return BaseInputModule.DetermineMoveDirection(x, y, 0.6f);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000176FC File Offset: 0x000158FC
		protected static MoveDirection DetermineMoveDirection(float x, float y, float deadZone)
		{
			if (new Vector2(x, y).sqrMagnitude < deadZone * deadZone)
			{
				return MoveDirection.None;
			}
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x <= 0f)
				{
					return MoveDirection.Left;
				}
				return MoveDirection.Right;
			}
			else
			{
				if (y <= 0f)
				{
					return MoveDirection.Down;
				}
				return MoveDirection.Up;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00017744 File Offset: 0x00015944
		protected static GameObject FindCommonRoot(GameObject g1, GameObject g2)
		{
			if (g1 == null || g2 == null)
			{
				return null;
			}
			Transform transform = g1.transform;
			while (transform != null)
			{
				Transform transform2 = g2.transform;
				while (transform2 != null)
				{
					if (transform == transform2)
					{
						return transform.gameObject;
					}
					transform2 = transform2.parent;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000177A8 File Offset: 0x000159A8
		protected void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject newEnterTarget)
		{
			if (newEnterTarget == null || currentPointerData.pointerEnter == null)
			{
				for (int i = 0; i < currentPointerData.hovered.Count; i++)
				{
					ExecuteEvents.Execute<IPointerExitHandler>(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerExitHandler);
				}
				currentPointerData.hovered.Clear();
				if (newEnterTarget == null)
				{
					currentPointerData.pointerEnter = null;
					return;
				}
			}
			if (currentPointerData.pointerEnter == newEnterTarget && newEnterTarget)
			{
				return;
			}
			GameObject gameObject = BaseInputModule.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
			if (currentPointerData.pointerEnter != null)
			{
				Transform transform = currentPointerData.pointerEnter.transform;
				while (transform != null && (!(gameObject != null) || !(gameObject.transform == transform)))
				{
					ExecuteEvents.Execute<IPointerExitHandler>(transform.gameObject, currentPointerData, ExecuteEvents.pointerExitHandler);
					currentPointerData.hovered.Remove(transform.gameObject);
					transform = transform.parent;
				}
			}
			currentPointerData.pointerEnter = newEnterTarget;
			if (newEnterTarget != null)
			{
				Transform transform2 = newEnterTarget.transform;
				while (transform2 != null && transform2.gameObject != gameObject)
				{
					ExecuteEvents.Execute<IPointerEnterHandler>(transform2.gameObject, currentPointerData, ExecuteEvents.pointerEnterHandler);
					currentPointerData.hovered.Add(transform2.gameObject);
					transform2 = transform2.parent;
				}
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000178FC File Offset: 0x00015AFC
		protected virtual AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
		{
			if (this.m_AxisEventData == null)
			{
				this.m_AxisEventData = new AxisEventData(this.eventSystem);
			}
			this.m_AxisEventData.Reset();
			this.m_AxisEventData.moveVector = new Vector2(x, y);
			this.m_AxisEventData.moveDir = BaseInputModule.DetermineMoveDirection(x, y, moveDeadZone);
			return this.m_AxisEventData;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00017958 File Offset: 0x00015B58
		protected virtual BaseEventData GetBaseEventData()
		{
			if (this.m_BaseEventData == null)
			{
				this.m_BaseEventData = new BaseEventData(this.eventSystem);
			}
			this.m_BaseEventData.Reset();
			return this.m_BaseEventData;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00008CC2 File Offset: 0x00006EC2
		public virtual bool IsPointerOverGameObject(int pointerId)
		{
			return false;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00017984 File Offset: 0x00015B84
		public virtual bool ShouldActivateModule()
		{
			return base.enabled && base.gameObject.activeInHierarchy;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void DeactivateModule()
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void ActivateModule()
		{
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void UpdateModule()
		{
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000C577 File Offset: 0x0000A777
		public virtual bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x040001E9 RID: 489
		[NonSerialized]
		protected List<RaycastResult> m_RaycastResultCache = new List<RaycastResult>();

		// Token: 0x040001EA RID: 490
		private AxisEventData m_AxisEventData;

		// Token: 0x040001EB RID: 491
		private EventSystem m_EventSystem;

		// Token: 0x040001EC RID: 492
		private BaseEventData m_BaseEventData;

		// Token: 0x040001ED RID: 493
		protected BaseInput m_InputOverride;

		// Token: 0x040001EE RID: 494
		private BaseInput m_DefaultInput;
	}
}
