using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000015 RID: 21
	[AddComponentMenu("VFX/Property Binders/Light Binder")]
	[VFXBinder("Utility/Light")]
	internal class VFXLightBinder : VFXBinderBase
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003716 File Offset: 0x00001916
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003723 File Offset: 0x00001923
		public string ColorProperty
		{
			get
			{
				return (string)this.m_ColorProperty;
			}
			set
			{
				this.m_ColorProperty = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003731 File Offset: 0x00001931
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003723 File Offset: 0x00001923
		public string BrightnessProperty
		{
			get
			{
				return (string)this.m_BrightnessProperty;
			}
			set
			{
				this.m_ColorProperty = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000373E File Offset: 0x0000193E
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000374B File Offset: 0x0000194B
		public string RadiusProperty
		{
			get
			{
				return (string)this.m_RadiusProperty;
			}
			set
			{
				this.m_RadiusProperty = value;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000375C File Offset: 0x0000195C
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && (!this.BindColor || component.HasVector4(this.ColorProperty)) && (!this.BindBrightness || component.HasFloat(this.BrightnessProperty)) && (!this.BindRadius || component.HasFloat(this.RadiusProperty));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000037BC File Offset: 0x000019BC
		public override void UpdateBinding(VisualEffect component)
		{
			if (this.BindColor)
			{
				component.SetVector4(this.ColorProperty, this.Target.color);
			}
			if (this.BindBrightness)
			{
				component.SetFloat(this.BrightnessProperty, this.Target.intensity);
			}
			if (this.BindRadius)
			{
				component.SetFloat(this.RadiusProperty, this.Target.range);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000382B File Offset: 0x00001A2B
		public override string ToString()
		{
			return string.Format("Light : '{0}' -> {1}", this.m_ColorProperty, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000053 RID: 83
		[VFXPropertyBinding(new string[] { "UnityEngine.Color" })]
		[SerializeField]
		[FormerlySerializedAs("m_ColorParameter")]
		protected ExposedProperty m_ColorProperty = "Color";

		// Token: 0x04000054 RID: 84
		[VFXPropertyBinding(new string[] { "System.Single" })]
		[SerializeField]
		[FormerlySerializedAs("m_BrightnessParameter")]
		protected ExposedProperty m_BrightnessProperty = "Brightness";

		// Token: 0x04000055 RID: 85
		[VFXPropertyBinding(new string[] { "System.Single" })]
		[SerializeField]
		[FormerlySerializedAs("m_RadiusParameter")]
		protected ExposedProperty m_RadiusProperty = "Radius";

		// Token: 0x04000056 RID: 86
		public Light Target;

		// Token: 0x04000057 RID: 87
		public bool BindColor = true;

		// Token: 0x04000058 RID: 88
		public bool BindBrightness;

		// Token: 0x04000059 RID: 89
		public bool BindRadius;
	}
}
