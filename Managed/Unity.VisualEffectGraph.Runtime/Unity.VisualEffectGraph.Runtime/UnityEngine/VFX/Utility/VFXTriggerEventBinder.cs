using System;
using System.Collections.Generic;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000009 RID: 9
	[RequireComponent(typeof(Collider))]
	internal class VFXTriggerEventBinder : VFXEventBinderBase
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002578 File Offset: 0x00000778
		protected override void SetEventAttribute(object[] parameters)
		{
			Collider collider = (Collider)parameters[0];
			this.eventAttribute.SetVector3(this.positionParameter, collider.transform.position);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025AF File Offset: 0x000007AF
		private void OnTriggerEnter(Collider other)
		{
			if (this.activation != VFXTriggerEventBinder.Activation.OnEnter)
			{
				return;
			}
			if (!this.colliders.Contains(other))
			{
				return;
			}
			base.SendEventToVisualEffect(new object[] { other });
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025D9 File Offset: 0x000007D9
		private void OnTriggerExit(Collider other)
		{
			if (this.activation != VFXTriggerEventBinder.Activation.OnExit)
			{
				return;
			}
			if (!this.colliders.Contains(other))
			{
				return;
			}
			base.SendEventToVisualEffect(new object[] { other });
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002604 File Offset: 0x00000804
		private void OnTriggerStay(Collider other)
		{
			if (this.activation != VFXTriggerEventBinder.Activation.OnStay)
			{
				return;
			}
			if (!this.colliders.Contains(other))
			{
				return;
			}
			base.SendEventToVisualEffect(new object[] { other });
		}

		// Token: 0x0400000F RID: 15
		public List<Collider> colliders = new List<Collider>();

		// Token: 0x04000010 RID: 16
		public VFXTriggerEventBinder.Activation activation;

		// Token: 0x04000011 RID: 17
		private ExposedProperty positionParameter = "position";

		// Token: 0x0200002D RID: 45
		public enum Activation
		{
			// Token: 0x040000B7 RID: 183
			OnEnter,
			// Token: 0x040000B8 RID: 184
			OnExit,
			// Token: 0x040000B9 RID: 185
			OnStay
		}
	}
}
