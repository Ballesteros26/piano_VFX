using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000018 RID: 24
	[AddComponentMenu("VFX/Property Binders/Position Binder")]
	[VFXBinder("Transform/Position")]
	internal class VFXPositionBinder : VFXBinderBase
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003BFD File Offset: 0x00001DFD
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003C0A File Offset: 0x00001E0A
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

		// Token: 0x06000094 RID: 148 RVA: 0x00003C18 File Offset: 0x00001E18
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasVector3(this.m_Property);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003C3B File Offset: 0x00001E3B
		public override void UpdateBinding(VisualEffect component)
		{
			component.SetVector3(this.m_Property, this.Target.transform.position);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003C5E File Offset: 0x00001E5E
		public override string ToString()
		{
			return string.Format("Position : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000064 RID: 100
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.Position", "UnityEngine.Vector3" })]
		[SerializeField]
		[FormerlySerializedAs("m_Parameter")]
		protected ExposedProperty m_Property = "Position";

		// Token: 0x04000065 RID: 101
		public Transform Target;
	}
}
