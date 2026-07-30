using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200001D RID: 29
	[AddComponentMenu("VFX/Property Binders/Transform Binder")]
	[VFXBinder("Transform/Transform")]
	internal class VFXTransformBinder : VFXBinderBase
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000431B File Offset: 0x0000251B
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004328 File Offset: 0x00002528
		public string Property
		{
			get
			{
				return (string)this.m_Property;
			}
			set
			{
				this.m_Property = value;
				this.UpdateSubProperties();
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000433C File Offset: 0x0000253C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateSubProperties();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000434A File Offset: 0x0000254A
		private void OnValidate()
		{
			this.UpdateSubProperties();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004354 File Offset: 0x00002554
		private void UpdateSubProperties()
		{
			this.Position = this.m_Property + "_position";
			this.Angles = this.m_Property + "_angles";
			this.Scale = this.m_Property + "_scale";
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000043B4 File Offset: 0x000025B4
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasVector3(this.Position) && component.HasVector3(this.Angles) && component.HasVector3(this.Scale);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004408 File Offset: 0x00002608
		public override void UpdateBinding(VisualEffect component)
		{
			component.SetVector3(this.Position, this.Target.transform.position);
			component.SetVector3(this.Angles, this.Target.transform.eulerAngles);
			component.SetVector3(this.Scale, this.Target.transform.localScale);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004478 File Offset: 0x00002678
		public override string ToString()
		{
			return string.Format("Transform : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x0400007E RID: 126
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.Transform" })]
		[SerializeField]
		[FormerlySerializedAs("m_Parameter")]
		protected ExposedProperty m_Property = "Transform";

		// Token: 0x0400007F RID: 127
		public Transform Target;

		// Token: 0x04000080 RID: 128
		private ExposedProperty Position;

		// Token: 0x04000081 RID: 129
		private ExposedProperty Angles;

		// Token: 0x04000082 RID: 130
		private ExposedProperty Scale;
	}
}
