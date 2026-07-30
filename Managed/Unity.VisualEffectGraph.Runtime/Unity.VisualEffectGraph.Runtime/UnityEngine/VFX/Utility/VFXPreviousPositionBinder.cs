using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000019 RID: 25
	[AddComponentMenu("VFX/Property Binders/Previous Position Binder")]
	[VFXBinder("Transform/Position (Previous)")]
	internal class VFXPreviousPositionBinder : VFXBinderBase
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003CA8 File Offset: 0x00001EA8
		protected override void OnEnable()
		{
			base.OnEnable();
			this.oldPosition = this.Target.position;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003CC1 File Offset: 0x00001EC1
		public override bool IsValid(VisualEffect component)
		{
			return component.HasVector3(this.m_Property);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public override void UpdateBinding(VisualEffect component)
		{
			component.SetVector3(this.m_Property, this.oldPosition);
			this.oldPosition = this.Target.position;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003CFE File Offset: 0x00001EFE
		public override string ToString()
		{
			return string.Format("Previous Position : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000066 RID: 102
		[VFXPropertyBinding(new string[] { "UnityEngine.Vector3" })]
		[FormerlySerializedAs("m_Parameter")]
		public ExposedProperty m_Property = "PreviousPosition";

		// Token: 0x04000067 RID: 103
		public Transform Target;

		// Token: 0x04000068 RID: 104
		private Vector3 oldPosition;
	}
}
