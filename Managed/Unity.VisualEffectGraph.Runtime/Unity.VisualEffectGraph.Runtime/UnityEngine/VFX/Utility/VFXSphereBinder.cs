using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200001B RID: 27
	[AddComponentMenu("VFX/Property Binders/Sphere Collider Binder")]
	[VFXBinder("Collider/Sphere")]
	internal class VFXSphereBinder : VFXBinderBase
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003FB1 File Offset: 0x000021B1
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003FBE File Offset: 0x000021BE
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

		// Token: 0x060000AC RID: 172 RVA: 0x00003FD2 File Offset: 0x000021D2
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateSubProperties();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003FE0 File Offset: 0x000021E0
		private void OnValidate()
		{
			this.UpdateSubProperties();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003FE8 File Offset: 0x000021E8
		private void UpdateSubProperties()
		{
			this.Center = this.m_Property + "_center";
			this.Radius = this.m_Property + "_radius";
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004020 File Offset: 0x00002220
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasVector3(this.Center) && component.HasFloat(this.Radius);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004058 File Offset: 0x00002258
		public override void UpdateBinding(VisualEffect component)
		{
			component.SetVector3(this.Center, this.Target.transform.position + this.Target.center);
			component.SetFloat(this.Radius, this.Target.radius * this.GetSphereColliderScale(this.Target.transform.localScale));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000040C9 File Offset: 0x000022C9
		public float GetSphereColliderScale(Vector3 scale)
		{
			return Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000040E7 File Offset: 0x000022E7
		public override string ToString()
		{
			return string.Format("Sphere : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000074 RID: 116
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.Sphere" })]
		[SerializeField]
		[FormerlySerializedAs("m_Parameter")]
		protected ExposedProperty m_Property = "Sphere";

		// Token: 0x04000075 RID: 117
		public SphereCollider Target;

		// Token: 0x04000076 RID: 118
		private ExposedProperty Center;

		// Token: 0x04000077 RID: 119
		private ExposedProperty Radius;
	}
}
