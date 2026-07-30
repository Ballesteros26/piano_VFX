using System;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200001F RID: 31
	[AddComponentMenu("VFX/Property Binders/UI Slider Binder")]
	[VFXBinder("UI/Slider")]
	internal class VFXUISliderBinder : VFXBinderBase
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004568 File Offset: 0x00002768
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004575 File Offset: 0x00002775
		public string Property
		{
			get
			{
				return (string)this.m_Property;
			}
			set
			{
				this.m_Property = value;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004583 File Offset: 0x00002783
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasFloat(this.m_Property);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000045A6 File Offset: 0x000027A6
		public override void UpdateBinding(VisualEffect component)
		{
			component.SetFloat(this.m_Property, this.Target.value);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000045C4 File Offset: 0x000027C4
		public override string ToString()
		{
			return string.Format("UI Slider : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000085 RID: 133
		[VFXPropertyBinding(new string[] { "System.Single" })]
		[SerializeField]
		[FormerlySerializedAs("m_Parameter")]
		protected ExposedProperty m_Property = "FloatParameter";

		// Token: 0x04000086 RID: 134
		public Slider Target;
	}
}
