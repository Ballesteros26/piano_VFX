using System;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200000B RID: 11
	internal abstract class VFXEventBinderBase : MonoBehaviour
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002687 File Offset: 0x00000887
		private void OnValidate()
		{
			if (this.target != null)
			{
				this.eventAttribute = this.target.CreateVFXEventAttribute();
				return;
			}
			this.eventAttribute = null;
		}

		// Token: 0x06000029 RID: 41
		protected abstract void SetEventAttribute(object[] parameters = null);

		// Token: 0x0600002A RID: 42 RVA: 0x000026B0 File Offset: 0x000008B0
		protected void SendEventToVisualEffect(params object[] parameters)
		{
			if (this.target != null)
			{
				this.SetEventAttribute(parameters);
				this.target.SendEvent(this.EventName, this.eventAttribute);
			}
		}

		// Token: 0x04000013 RID: 19
		[SerializeField]
		protected VisualEffect target;

		// Token: 0x04000014 RID: 20
		public string EventName = "Event";

		// Token: 0x04000015 RID: 21
		[SerializeField]
		[HideInInspector]
		protected VFXEventAttribute eventAttribute;
	}
}
