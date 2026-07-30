using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200000E RID: 14
	[AddComponentMenu("VFX/Property Binders/Enabled Binder")]
	[VFXBinder("GameObject/Enabled")]
	internal class VFXEnabledBinder : VFXBinderBase
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000029B4 File Offset: 0x00000BB4
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000029C1 File Offset: 0x00000BC1
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

		// Token: 0x0600003E RID: 62 RVA: 0x000029CF File Offset: 0x00000BCF
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasBool(this.m_Property);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029F2 File Offset: 0x00000BF2
		public override void UpdateBinding(VisualEffect component)
		{
			component.SetBool(this.m_Property, (this.check == VFXEnabledBinder.Check.ActiveInHierarchy) ? this.Target.activeInHierarchy : this.Target.activeSelf);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A25 File Offset: 0x00000C25
		public override string ToString()
		{
			return string.Format("{2} : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name, this.check);
		}

		// Token: 0x04000021 RID: 33
		public VFXEnabledBinder.Check check;

		// Token: 0x04000022 RID: 34
		[VFXPropertyBinding(new string[] { "System.Boolean" })]
		[SerializeField]
		[FormerlySerializedAs("m_Parameter")]
		protected ExposedProperty m_Property = "Enabled";

		// Token: 0x04000023 RID: 35
		public GameObject Target;

		// Token: 0x02000030 RID: 48
		public enum Check
		{
			// Token: 0x040000C1 RID: 193
			ActiveInHierarchy,
			// Token: 0x040000C2 RID: 194
			ActiveSelf
		}
	}
}
