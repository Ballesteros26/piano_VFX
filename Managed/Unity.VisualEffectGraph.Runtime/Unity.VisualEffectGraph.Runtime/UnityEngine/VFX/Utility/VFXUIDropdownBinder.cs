using System;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200001E RID: 30
	[AddComponentMenu("VFX/Property Binders/UI Dropdown Binder")]
	[VFXBinder("UI/Dropdown")]
	internal class VFXUIDropdownBinder : VFXBinderBase
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000044C2 File Offset: 0x000026C2
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000044CF File Offset: 0x000026CF
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

		// Token: 0x060000C8 RID: 200 RVA: 0x000044DD File Offset: 0x000026DD
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasInt(this.m_Property);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004500 File Offset: 0x00002700
		public override void UpdateBinding(VisualEffect component)
		{
			component.SetInt(this.m_Property, this.Target.value);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000451E File Offset: 0x0000271E
		public override string ToString()
		{
			return string.Format("UI Dropdown : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000083 RID: 131
		[VFXPropertyBinding(new string[] { "System.Int32" })]
		[SerializeField]
		[FormerlySerializedAs("m_Parameter")]
		protected ExposedProperty m_Property = "IntParameter";

		// Token: 0x04000084 RID: 132
		public Dropdown Target;
	}
}
