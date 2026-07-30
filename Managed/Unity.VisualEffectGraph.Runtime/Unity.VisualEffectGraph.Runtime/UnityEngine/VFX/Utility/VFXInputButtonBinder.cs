using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000011 RID: 17
	[AddComponentMenu("VFX/Property Binders/Input Button Binder")]
	[VFXBinder("Input/Button")]
	internal class VFXInputButtonBinder : VFXBinderBase
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002F79 File Offset: 0x00001179
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002F86 File Offset: 0x00001186
		public string ButtonProperty
		{
			get
			{
				return (string)this.m_ButtonProperty;
			}
			set
			{
				this.m_ButtonProperty = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002F94 File Offset: 0x00001194
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002FA1 File Offset: 0x000011A1
		public string ButtonSmoothProperty
		{
			get
			{
				return (string)this.m_ButtonSmoothProperty;
			}
			set
			{
				this.m_ButtonSmoothProperty = value;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002FAF File Offset: 0x000011AF
		public override bool IsValid(VisualEffect component)
		{
			return component.HasBool(this.m_ButtonProperty) && (!this.UseButtonSmooth || component.HasFloat(this.m_ButtonSmoothProperty));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FE1 File Offset: 0x000011E1
		private void Start()
		{
			if (this.UseButtonSmooth)
			{
				this.m_CachedSmoothValue = (Input.GetButton(this.ButtonName) ? 1f : 0f);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000300C File Offset: 0x0000120C
		public override void UpdateBinding(VisualEffect component)
		{
			bool button = Input.GetButton(this.ButtonName);
			component.SetBool(this.m_ButtonProperty, button);
			if (this.UseButtonSmooth)
			{
				this.m_CachedSmoothValue += this.SmoothSpeed * Time.deltaTime * (button ? 1f : (-1f));
				this.m_CachedSmoothValue = Mathf.Clamp01(this.m_CachedSmoothValue);
				component.SetFloat(this.m_ButtonSmoothProperty, this.m_CachedSmoothValue);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003090 File Offset: 0x00001290
		public override string ToString()
		{
			return string.Format("Input Button: '{0}' -> {1}", this.m_ButtonSmoothProperty, this.ButtonName.ToString());
		}

		// Token: 0x04000034 RID: 52
		[VFXPropertyBinding(new string[] { "System.Boolean" })]
		[SerializeField]
		[FormerlySerializedAs("m_ButtonParameter")]
		protected ExposedProperty m_ButtonProperty = "ButtonDown";

		// Token: 0x04000035 RID: 53
		[VFXPropertyBinding(new string[] { "System.Single" })]
		[SerializeField]
		[FormerlySerializedAs("m_ButtonSmoothParameter")]
		protected ExposedProperty m_ButtonSmoothProperty = "KeySmooth";

		// Token: 0x04000036 RID: 54
		public string ButtonName = "Action";

		// Token: 0x04000037 RID: 55
		public float SmoothSpeed = 2f;

		// Token: 0x04000038 RID: 56
		public bool UseButtonSmooth = true;

		// Token: 0x04000039 RID: 57
		private float m_CachedSmoothValue;
	}
}
