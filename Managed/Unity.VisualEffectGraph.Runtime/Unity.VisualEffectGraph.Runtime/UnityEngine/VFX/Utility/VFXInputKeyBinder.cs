using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000012 RID: 18
	[AddComponentMenu("VFX/Property Binders/Input Key Press Binder")]
	[VFXBinder("Input/Key")]
	internal class VFXInputKeyBinder : VFXBinderBase
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003100 File Offset: 0x00001300
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000310D File Offset: 0x0000130D
		public string KeyProperty
		{
			get
			{
				return (string)this.m_KeyProperty;
			}
			set
			{
				this.m_KeyProperty = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000311B File Offset: 0x0000131B
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003128 File Offset: 0x00001328
		public string KeySmoothProperty
		{
			get
			{
				return (string)this.m_KeySmoothProperty;
			}
			set
			{
				this.m_KeySmoothProperty = value;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003136 File Offset: 0x00001336
		public override bool IsValid(VisualEffect component)
		{
			return component.HasBool(this.m_KeyProperty) && (!this.UseKeySmooth || component.HasFloat(this.m_KeySmoothProperty));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003168 File Offset: 0x00001368
		private void Start()
		{
			if (this.UseKeySmooth)
			{
				this.m_CachedSmoothValue = (Input.GetKeyDown(this.Key) ? 1f : 0f);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003194 File Offset: 0x00001394
		public override void UpdateBinding(VisualEffect component)
		{
			bool key = Input.GetKey(this.Key);
			component.SetBool(this.m_KeyProperty, key);
			if (this.UseKeySmooth)
			{
				this.m_CachedSmoothValue += this.SmoothSpeed * Time.deltaTime * (key ? 1f : (-1f));
				this.m_CachedSmoothValue = Mathf.Clamp01(this.m_CachedSmoothValue);
				component.SetFloat(this.m_KeySmoothProperty, this.m_CachedSmoothValue);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003218 File Offset: 0x00001418
		public override string ToString()
		{
			return string.Format("Key: '{0}' -> {1}", this.m_KeySmoothProperty, this.Key.ToString());
		}

		// Token: 0x0400003A RID: 58
		[VFXPropertyBinding(new string[] { "System.Boolean" })]
		[SerializeField]
		[FormerlySerializedAs("m_KeyParameter")]
		protected ExposedProperty m_KeyProperty = "KeyDown";

		// Token: 0x0400003B RID: 59
		[VFXPropertyBinding(new string[] { "System.Single" })]
		[SerializeField]
		[FormerlySerializedAs("m_KeySmoothParameter")]
		protected ExposedProperty m_KeySmoothProperty = "KeySmooth";

		// Token: 0x0400003C RID: 60
		public KeyCode Key = KeyCode.Space;

		// Token: 0x0400003D RID: 61
		public float SmoothSpeed = 2f;

		// Token: 0x0400003E RID: 62
		public bool UseKeySmooth = true;

		// Token: 0x0400003F RID: 63
		private float m_CachedSmoothValue;
	}
}
