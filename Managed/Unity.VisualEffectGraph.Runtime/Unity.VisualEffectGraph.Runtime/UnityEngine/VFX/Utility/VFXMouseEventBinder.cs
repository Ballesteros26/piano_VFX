using System;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000007 RID: 7
	[RequireComponent(typeof(Collider))]
	internal class VFXMouseEventBinder : VFXEventBinderBase
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000023C4 File Offset: 0x000005C4
		protected override void SetEventAttribute(object[] parameters)
		{
			if (this.RaycastMousePosition)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				if (base.GetComponent<Collider>().Raycast(ray, out raycastHit, 3.4028235E+38f))
				{
					this.eventAttribute.SetVector3(this.position, raycastHit.point);
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000241B File Offset: 0x0000061B
		private void OnMouseDown()
		{
			if (this.activation == VFXMouseEventBinder.Activation.OnMouseDown)
			{
				base.SendEventToVisualEffect(Array.Empty<object>());
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002431 File Offset: 0x00000631
		private void OnMouseUp()
		{
			if (this.activation == VFXMouseEventBinder.Activation.OnMouseUp)
			{
				base.SendEventToVisualEffect(Array.Empty<object>());
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002446 File Offset: 0x00000646
		private void OnMouseDrag()
		{
			if (this.activation == VFXMouseEventBinder.Activation.OnMouseDrag)
			{
				base.SendEventToVisualEffect(Array.Empty<object>());
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000245C File Offset: 0x0000065C
		private void OnMouseOver()
		{
			if (this.activation == VFXMouseEventBinder.Activation.OnMouseOver)
			{
				base.SendEventToVisualEffect(Array.Empty<object>());
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002472 File Offset: 0x00000672
		private void OnMouseEnter()
		{
			if (this.activation == VFXMouseEventBinder.Activation.OnMouseEnter)
			{
				base.SendEventToVisualEffect(Array.Empty<object>());
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002488 File Offset: 0x00000688
		private void OnMouseExit()
		{
			if (this.activation == VFXMouseEventBinder.Activation.OnMouseExit)
			{
				base.SendEventToVisualEffect(Array.Empty<object>());
			}
		}

		// Token: 0x0400000A RID: 10
		public VFXMouseEventBinder.Activation activation = VFXMouseEventBinder.Activation.OnMouseDown;

		// Token: 0x0400000B RID: 11
		private ExposedProperty position = "position";

		// Token: 0x0400000C RID: 12
		[Tooltip("Computes intersection in world space and sets it to the position EventAttribute")]
		public bool RaycastMousePosition;

		// Token: 0x0200002C RID: 44
		public enum Activation
		{
			// Token: 0x040000B0 RID: 176
			OnMouseUp,
			// Token: 0x040000B1 RID: 177
			OnMouseDown,
			// Token: 0x040000B2 RID: 178
			OnMouseEnter,
			// Token: 0x040000B3 RID: 179
			OnMouseExit,
			// Token: 0x040000B4 RID: 180
			OnMouseOver,
			// Token: 0x040000B5 RID: 181
			OnMouseDrag
		}
	}
}
