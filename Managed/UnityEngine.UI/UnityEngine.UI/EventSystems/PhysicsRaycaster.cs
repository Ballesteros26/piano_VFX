using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000071 RID: 113
	[AddComponentMenu("Event/Physics Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class PhysicsRaycaster : BaseRaycaster
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x0001954A File Offset: 0x0001774A
		protected PhysicsRaycaster()
		{
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001955E File Offset: 0x0001775E
		public override Camera eventCamera
		{
			get
			{
				if (this.m_EventCamera == null)
				{
					this.m_EventCamera = base.GetComponent<Camera>();
				}
				return this.m_EventCamera ?? Camera.main;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00019589 File Offset: 0x00017789
		public virtual int depth
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return 16777215;
				}
				return (int)this.eventCamera.depth;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x000195AB File Offset: 0x000177AB
		public int finalEventMask
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return -1;
				}
				return this.eventCamera.cullingMask & this.m_EventMask;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000195D4 File Offset: 0x000177D4
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x000195DC File Offset: 0x000177DC
		public LayerMask eventMask
		{
			get
			{
				return this.m_EventMask;
			}
			set
			{
				this.m_EventMask = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000195E5 File Offset: 0x000177E5
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x000195ED File Offset: 0x000177ED
		public int maxRayIntersections
		{
			get
			{
				return this.m_MaxRayIntersections;
			}
			set
			{
				this.m_MaxRayIntersections = value;
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000195F8 File Offset: 0x000177F8
		protected bool ComputeRayAndDistance(PointerEventData eventData, ref Ray ray, ref int eventDisplayIndex, ref float distanceToClipPlane)
		{
			if (this.eventCamera == null)
			{
				return false;
			}
			Vector3 vector = Display.RelativeMouseAt(eventData.position);
			if (vector != Vector3.zero)
			{
				eventDisplayIndex = (int)vector.z;
				if (eventDisplayIndex != this.eventCamera.targetDisplay)
				{
					return false;
				}
			}
			else
			{
				vector = eventData.position;
			}
			if (!this.eventCamera.pixelRect.Contains(vector))
			{
				return false;
			}
			ray = this.eventCamera.ScreenPointToRay(vector);
			float z = ray.direction.z;
			distanceToClipPlane = (Mathf.Approximately(0f, z) ? float.PositiveInfinity : Mathf.Abs((this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane) / z));
			return true;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000196C8 File Offset: 0x000178C8
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			Ray ray = default(Ray);
			int num = 0;
			float num2 = 0f;
			if (!this.ComputeRayAndDistance(eventData, ref ray, ref num, ref num2))
			{
				return;
			}
			int num3;
			if (this.m_MaxRayIntersections == 0)
			{
				if (ReflectionMethodsCache.Singleton.raycast3DAll == null)
				{
					return;
				}
				this.m_Hits = ReflectionMethodsCache.Singleton.raycast3DAll(ray, num2, this.finalEventMask);
				num3 = this.m_Hits.Length;
			}
			else
			{
				if (ReflectionMethodsCache.Singleton.getRaycastNonAlloc == null)
				{
					return;
				}
				if (this.m_LastMaxRayIntersections != this.m_MaxRayIntersections)
				{
					this.m_Hits = new RaycastHit[this.m_MaxRayIntersections];
					this.m_LastMaxRayIntersections = this.m_MaxRayIntersections;
				}
				num3 = ReflectionMethodsCache.Singleton.getRaycastNonAlloc(ray, this.m_Hits, num2, this.finalEventMask);
			}
			if (num3 != 0)
			{
				if (num3 > 1)
				{
					Array.Sort<RaycastHit>(this.m_Hits, 0, num3, PhysicsRaycaster.RaycastHitComparer.instance);
				}
				int i = 0;
				int num4 = num3;
				while (i < num4)
				{
					RaycastResult raycastResult = new RaycastResult
					{
						gameObject = this.m_Hits[i].collider.gameObject,
						module = this,
						distance = this.m_Hits[i].distance,
						worldPosition = this.m_Hits[i].point,
						worldNormal = this.m_Hits[i].normal,
						screenPosition = eventData.position,
						displayIndex = num,
						index = (float)resultAppendList.Count,
						sortingLayer = 0,
						sortingOrder = 0
					};
					resultAppendList.Add(raycastResult);
					i++;
				}
			}
		}

		// Token: 0x0400021B RID: 539
		protected const int kNoEventMaskSet = -1;

		// Token: 0x0400021C RID: 540
		protected Camera m_EventCamera;

		// Token: 0x0400021D RID: 541
		[SerializeField]
		protected LayerMask m_EventMask = -1;

		// Token: 0x0400021E RID: 542
		[SerializeField]
		protected int m_MaxRayIntersections;

		// Token: 0x0400021F RID: 543
		protected int m_LastMaxRayIntersections;

		// Token: 0x04000220 RID: 544
		private RaycastHit[] m_Hits;

		// Token: 0x020000C3 RID: 195
		private class RaycastHitComparer : IComparer<RaycastHit>
		{
			// Token: 0x060006C1 RID: 1729 RVA: 0x0001A268 File Offset: 0x00018468
			public int Compare(RaycastHit x, RaycastHit y)
			{
				return x.distance.CompareTo(y.distance);
			}

			// Token: 0x04000316 RID: 790
			public static PhysicsRaycaster.RaycastHitComparer instance = new PhysicsRaycaster.RaycastHitComparer();
		}
	}
}
