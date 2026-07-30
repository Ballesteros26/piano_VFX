using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000072 RID: 114
	public abstract class UIBehaviour : MonoBehaviour
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void Awake()
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void OnEnable()
		{
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void Start()
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void OnDisable()
		{
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00019878 File Offset: 0x00017A78
		public virtual bool IsActive()
		{
			return base.isActiveAndEnabled;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void OnTransformParentChanged()
		{
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void OnDidApplyAnimationProperties()
		{
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void OnCanvasGroupChanged()
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00019880 File Offset: 0x00017A80
		public bool IsDestroyed()
		{
			return this == null;
		}
	}
}
