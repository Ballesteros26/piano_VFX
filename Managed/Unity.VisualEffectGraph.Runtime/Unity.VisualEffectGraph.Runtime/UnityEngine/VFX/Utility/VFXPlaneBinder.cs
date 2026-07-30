using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000017 RID: 23
	[AddComponentMenu("VFX/Property Binders/Plane Binder")]
	[VFXBinder("Utility/Plane")]
	internal class VFXPlaneBinder : VFXBinderBase
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003ABF File Offset: 0x00001CBF
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003ACC File Offset: 0x00001CCC
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

		// Token: 0x0600008B RID: 139 RVA: 0x00003AE0 File Offset: 0x00001CE0
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateSubProperties();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003AEE File Offset: 0x00001CEE
		private void OnValidate()
		{
			this.UpdateSubProperties();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003AF6 File Offset: 0x00001CF6
		private void UpdateSubProperties()
		{
			this.Position = this.m_Property + "_position";
			this.Normal = this.m_Property + "_normal";
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003B2E File Offset: 0x00001D2E
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasVector3(this.Position) && component.HasVector3(this.Normal);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003B64 File Offset: 0x00001D64
		public override void UpdateBinding(VisualEffect component)
		{
			component.SetVector3(this.Position, this.Target.transform.position);
			component.SetVector3(this.Normal, this.Target.transform.up);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003BB3 File Offset: 0x00001DB3
		public override string ToString()
		{
			return string.Format("Plane : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000060 RID: 96
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.Plane" })]
		[SerializeField]
		[FormerlySerializedAs("m_Parameter")]
		protected ExposedProperty m_Property = "Plane";

		// Token: 0x04000061 RID: 97
		public Transform Target;

		// Token: 0x04000062 RID: 98
		private ExposedProperty Position;

		// Token: 0x04000063 RID: 99
		private ExposedProperty Normal;
	}
}
