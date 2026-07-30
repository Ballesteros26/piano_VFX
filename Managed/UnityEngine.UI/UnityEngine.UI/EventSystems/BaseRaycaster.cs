using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006F RID: 111
	public abstract class BaseRaycaster : UIBehaviour
	{
		// Token: 0x060005FE RID: 1534
		public abstract void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList);

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005FF RID: 1535
		public abstract Camera eventCamera { get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00008CC2 File Offset: 0x00006EC2
		[Obsolete("Please use sortOrderPriority and renderOrderPriority", false)]
		public virtual int priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00019228 File Offset: 0x00017428
		public virtual int sortOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00019228 File Offset: 0x00017428
		public virtual int renderOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00019230 File Offset: 0x00017430
		public BaseRaycaster rootRaycaster
		{
			get
			{
				if (this.m_RootRaycaster == null)
				{
					BaseRaycaster[] componentsInParent = base.GetComponentsInParent<BaseRaycaster>();
					if (componentsInParent.Length != 0)
					{
						this.m_RootRaycaster = componentsInParent[componentsInParent.Length - 1];
					}
				}
				return this.m_RootRaycaster;
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001926C File Offset: 0x0001746C
		public override string ToString()
		{
			return string.Concat(new object[] { "Name: ", base.gameObject, "\neventCamera: ", this.eventCamera, "\nsortOrderPriority: ", this.sortOrderPriority, "\nrenderOrderPriority: ", this.renderOrderPriority });
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000192D2 File Offset: 0x000174D2
		protected override void OnEnable()
		{
			base.OnEnable();
			RaycasterManager.AddRaycaster(this);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000192E0 File Offset: 0x000174E0
		protected override void OnDisable()
		{
			RaycasterManager.RemoveRaycasters(this);
			base.OnDisable();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000192EE File Offset: 0x000174EE
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this.m_RootRaycaster = null;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000192FD File Offset: 0x000174FD
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_RootRaycaster = null;
		}

		// Token: 0x04000219 RID: 537
		private BaseRaycaster m_RootRaycaster;
	}
}
