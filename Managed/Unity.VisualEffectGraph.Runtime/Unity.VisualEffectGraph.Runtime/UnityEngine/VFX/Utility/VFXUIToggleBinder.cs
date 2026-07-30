using System;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000020 RID: 32
	[AddComponentMenu("VFX/Property Binders/UI Toggle Binder")]
	[VFXBinder("UI/Toggle")]
	internal class VFXUIToggleBinder : VFXBinderBase
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000460E File Offset: 0x0000280E
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x0000461B File Offset: 0x0000281B
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

		// Token: 0x060000D4 RID: 212 RVA: 0x00004629 File Offset: 0x00002829
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasBool(this.m_Property);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000464C File Offset: 0x0000284C
		public override void UpdateBinding(VisualEffect component)
		{
			component.SetBool(this.m_Property, this.Target.isOn);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000466A File Offset: 0x0000286A
		public override string ToString()
		{
			return string.Format("UI Toggle : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000087 RID: 135
		[VFXPropertyBinding(new string[] { "System.Boolean" })]
		[SerializeField]
		[FormerlySerializedAs("m_Parameter")]
		protected ExposedProperty m_Property = "BoolParameter";

		// Token: 0x04000088 RID: 136
		public Toggle Target;
	}
}
