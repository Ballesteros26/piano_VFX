using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006D RID: 109
	public struct RaycastResult
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00019004 File Offset: 0x00017204
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0001900C File Offset: 0x0001720C
		public GameObject gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00019015 File Offset: 0x00017215
		public bool isValid
		{
			get
			{
				return this.module != null && this.gameObject != null;
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00019034 File Offset: 0x00017234
		public void Clear()
		{
			this.gameObject = null;
			this.module = null;
			this.distance = 0f;
			this.index = 0f;
			this.depth = 0;
			this.sortingLayer = 0;
			this.sortingOrder = 0;
			this.worldNormal = Vector3.up;
			this.worldPosition = Vector3.zero;
			this.screenPosition = Vector3.zero;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000190A0 File Offset: 0x000172A0
		public override string ToString()
		{
			if (!this.isValid)
			{
				return "";
			}
			return string.Concat(new object[]
			{
				"Name: ",
				this.gameObject,
				"\nmodule: ",
				this.module,
				"\ndistance: ",
				this.distance,
				"\nindex: ",
				this.index,
				"\ndepth: ",
				this.depth,
				"\nworldNormal: ",
				this.worldNormal,
				"\nworldPosition: ",
				this.worldPosition,
				"\nscreenPosition: ",
				this.screenPosition,
				"\nmodule.sortOrderPriority: ",
				this.module.sortOrderPriority,
				"\nmodule.renderOrderPriority: ",
				this.module.renderOrderPriority,
				"\nsortingLayer: ",
				this.sortingLayer,
				"\nsortingOrder: ",
				this.sortingOrder
			});
		}

		// Token: 0x0400020D RID: 525
		private GameObject m_GameObject;

		// Token: 0x0400020E RID: 526
		public BaseRaycaster module;

		// Token: 0x0400020F RID: 527
		public float distance;

		// Token: 0x04000210 RID: 528
		public float index;

		// Token: 0x04000211 RID: 529
		public int depth;

		// Token: 0x04000212 RID: 530
		public int sortingLayer;

		// Token: 0x04000213 RID: 531
		public int sortingOrder;

		// Token: 0x04000214 RID: 532
		public Vector3 worldPosition;

		// Token: 0x04000215 RID: 533
		public Vector3 worldNormal;

		// Token: 0x04000216 RID: 534
		public Vector2 screenPosition;

		// Token: 0x04000217 RID: 535
		public int displayIndex;
	}
}
