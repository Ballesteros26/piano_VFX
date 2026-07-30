using System;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000008 RID: 8
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Collider))]
	internal class VFXRigidBodyCollisionEventBinder : VFXEventBinderBase
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024C0 File Offset: 0x000006C0
		protected override void SetEventAttribute(object[] parameters)
		{
			ContactPoint contactPoint = (ContactPoint)parameters[0];
			this.eventAttribute.SetVector3(this.positionParameter, contactPoint.point);
			this.eventAttribute.SetVector3(this.directionParameter, contactPoint.normal);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002510 File Offset: 0x00000710
		private void OnCollisionEnter(Collision collision)
		{
			foreach (ContactPoint contactPoint in collision.contacts)
			{
				base.SendEventToVisualEffect(new object[] { contactPoint });
			}
		}

		// Token: 0x0400000D RID: 13
		private ExposedProperty positionParameter = "position";

		// Token: 0x0400000E RID: 14
		private ExposedProperty directionParameter = "velocity";
	}
}
