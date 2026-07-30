using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public class HDPhysicalCamera
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0003F7A3 File Offset: 0x0003D9A3
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0003F7AB File Offset: 0x0003D9AB
		public int iso
		{
			get
			{
				return this.m_Iso;
			}
			set
			{
				this.m_Iso = Mathf.Max(value, 1);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0003F7BA File Offset: 0x0003D9BA
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x0003F7C2 File Offset: 0x0003D9C2
		public float shutterSpeed
		{
			get
			{
				return this.m_ShutterSpeed;
			}
			set
			{
				this.m_ShutterSpeed = Mathf.Max(value, 0f);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0003F7D5 File Offset: 0x0003D9D5
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x0003F7DD File Offset: 0x0003D9DD
		public float aperture
		{
			get
			{
				return this.m_Aperture;
			}
			set
			{
				this.m_Aperture = Mathf.Clamp(value, 1f, 32f);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0003F7F5 File Offset: 0x0003D9F5
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0003F7FD File Offset: 0x0003D9FD
		public int bladeCount
		{
			get
			{
				return this.m_BladeCount;
			}
			set
			{
				this.m_BladeCount = Mathf.Clamp(value, 3, 11);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0003F80E File Offset: 0x0003DA0E
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0003F816 File Offset: 0x0003DA16
		public Vector2 curvature
		{
			get
			{
				return this.m_Curvature;
			}
			set
			{
				this.m_Curvature.x = Mathf.Max(value.x, 1f);
				this.m_Curvature.y = Mathf.Min(value.y, 32f);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0003F84E File Offset: 0x0003DA4E
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0003F856 File Offset: 0x0003DA56
		public float barrelClipping
		{
			get
			{
				return this.m_BarrelClipping;
			}
			set
			{
				this.m_BarrelClipping = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0003F864 File Offset: 0x0003DA64
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0003F86C File Offset: 0x0003DA6C
		public float anamorphism
		{
			get
			{
				return this.m_Anamorphism;
			}
			set
			{
				this.m_Anamorphism = Mathf.Clamp(value, -1f, 1f);
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0003F884 File Offset: 0x0003DA84
		public void CopyTo(HDPhysicalCamera c)
		{
			c.iso = this.iso;
			c.shutterSpeed = this.shutterSpeed;
			c.aperture = this.aperture;
			c.bladeCount = this.bladeCount;
			c.curvature = this.curvature;
			c.barrelClipping = this.barrelClipping;
			c.anamorphism = this.anamorphism;
		}

		// Token: 0x0400087E RID: 2174
		public const float kMinAperture = 1f;

		// Token: 0x0400087F RID: 2175
		public const float kMaxAperture = 32f;

		// Token: 0x04000880 RID: 2176
		public const int kMinBladeCount = 3;

		// Token: 0x04000881 RID: 2177
		public const int kMaxBladeCount = 11;

		// Token: 0x04000882 RID: 2178
		[SerializeField]
		[Min(1f)]
		private int m_Iso = 200;

		// Token: 0x04000883 RID: 2179
		[SerializeField]
		[Min(0f)]
		private float m_ShutterSpeed = 0.005f;

		// Token: 0x04000884 RID: 2180
		[SerializeField]
		[Range(1f, 32f)]
		private float m_Aperture = 16f;

		// Token: 0x04000885 RID: 2181
		[SerializeField]
		[Range(3f, 11f)]
		private int m_BladeCount = 5;

		// Token: 0x04000886 RID: 2182
		[SerializeField]
		private Vector2 m_Curvature = new Vector2(2f, 11f);

		// Token: 0x04000887 RID: 2183
		[SerializeField]
		[Range(0f, 1f)]
		private float m_BarrelClipping = 0.25f;

		// Token: 0x04000888 RID: 2184
		[SerializeField]
		[Range(-1f, 1f)]
		private float m_Anamorphism;
	}
}
