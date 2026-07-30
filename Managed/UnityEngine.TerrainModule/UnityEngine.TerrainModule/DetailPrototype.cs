using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[UsedByNativeCode]
	[StructLayout(0)]
	public sealed class DetailPrototype
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002460 File Offset: 0x00000660
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002478 File Offset: 0x00000678
		public GameObject prototype
		{
			get
			{
				return this.m_Prototype;
			}
			set
			{
				this.m_Prototype = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002484 File Offset: 0x00000684
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000249C File Offset: 0x0000069C
		public Texture2D prototypeTexture
		{
			get
			{
				return this.m_PrototypeTexture;
			}
			set
			{
				this.m_PrototypeTexture = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000024A8 File Offset: 0x000006A8
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000024C0 File Offset: 0x000006C0
		public float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				this.m_MinWidth = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000024CC File Offset: 0x000006CC
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000024E4 File Offset: 0x000006E4
		public float maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
			set
			{
				this.m_MaxWidth = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000024F0 File Offset: 0x000006F0
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002508 File Offset: 0x00000708
		public float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				this.m_MinHeight = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002514 File Offset: 0x00000714
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000252C File Offset: 0x0000072C
		public float maxHeight
		{
			get
			{
				return this.m_MaxHeight;
			}
			set
			{
				this.m_MaxHeight = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002538 File Offset: 0x00000738
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002550 File Offset: 0x00000750
		public float noiseSpread
		{
			get
			{
				return this.m_NoiseSpread;
			}
			set
			{
				this.m_NoiseSpread = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000255C File Offset: 0x0000075C
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002574 File Offset: 0x00000774
		public float bendFactor
		{
			get
			{
				return this.m_BendFactor;
			}
			set
			{
				this.m_BendFactor = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002580 File Offset: 0x00000780
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002598 File Offset: 0x00000798
		public Color healthyColor
		{
			get
			{
				return this.m_HealthyColor;
			}
			set
			{
				this.m_HealthyColor = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000025BC File Offset: 0x000007BC
		public Color dryColor
		{
			get
			{
				return this.m_DryColor;
			}
			set
			{
				this.m_DryColor = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000025C8 File Offset: 0x000007C8
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000025E0 File Offset: 0x000007E0
		public DetailRenderMode renderMode
		{
			get
			{
				return (DetailRenderMode)this.m_RenderMode;
			}
			set
			{
				this.m_RenderMode = (int)value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000025EC File Offset: 0x000007EC
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002607 File Offset: 0x00000807
		public bool usePrototypeMesh
		{
			get
			{
				return this.m_UsePrototypeMesh != 0;
			}
			set
			{
				this.m_UsePrototypeMesh = (value ? 1 : 0);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002618 File Offset: 0x00000818
		public DetailPrototype()
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000026CC File Offset: 0x000008CC
		public DetailPrototype(DetailPrototype other)
		{
			this.m_Prototype = other.m_Prototype;
			this.m_PrototypeTexture = other.m_PrototypeTexture;
			this.m_HealthyColor = other.m_HealthyColor;
			this.m_DryColor = other.m_DryColor;
			this.m_MinWidth = other.m_MinWidth;
			this.m_MaxWidth = other.m_MaxWidth;
			this.m_MinHeight = other.m_MinHeight;
			this.m_MaxHeight = other.m_MaxHeight;
			this.m_NoiseSpread = other.m_NoiseSpread;
			this.m_BendFactor = other.m_BendFactor;
			this.m_RenderMode = other.m_RenderMode;
			this.m_UsePrototypeMesh = other.m_UsePrototypeMesh;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002810 File Offset: 0x00000A10
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DetailPrototype);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002830 File Offset: 0x00000A30
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002848 File Offset: 0x00000A48
		private bool Equals(DetailPrototype other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = other == this;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = base.GetType() != other.GetType();
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						bool flag5 = this.m_Prototype == other.m_Prototype && this.m_PrototypeTexture == other.m_PrototypeTexture && this.m_HealthyColor == other.m_HealthyColor && this.m_DryColor == other.m_DryColor && this.m_MinWidth == other.m_MinWidth && this.m_MaxWidth == other.m_MaxWidth && this.m_MinHeight == other.m_MinHeight && this.m_MaxHeight == other.m_MaxHeight && this.m_NoiseSpread == other.m_NoiseSpread && this.m_BendFactor == other.m_BendFactor && this.m_RenderMode == other.m_RenderMode && this.m_UsePrototypeMesh == other.m_UsePrototypeMesh;
						flag2 = flag5;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0400001F RID: 31
		internal GameObject m_Prototype = null;

		// Token: 0x04000020 RID: 32
		internal Texture2D m_PrototypeTexture = null;

		// Token: 0x04000021 RID: 33
		internal Color m_HealthyColor = new Color(0.2627451f, 0.9764706f, 0.16470589f, 1f);

		// Token: 0x04000022 RID: 34
		internal Color m_DryColor = new Color(0.8039216f, 0.7372549f, 0.101960786f, 1f);

		// Token: 0x04000023 RID: 35
		internal float m_MinWidth = 1f;

		// Token: 0x04000024 RID: 36
		internal float m_MaxWidth = 2f;

		// Token: 0x04000025 RID: 37
		internal float m_MinHeight = 1f;

		// Token: 0x04000026 RID: 38
		internal float m_MaxHeight = 2f;

		// Token: 0x04000027 RID: 39
		internal float m_NoiseSpread = 0.1f;

		// Token: 0x04000028 RID: 40
		internal float m_BendFactor = 0.1f;

		// Token: 0x04000029 RID: 41
		internal int m_RenderMode = 2;

		// Token: 0x0400002A RID: 42
		internal int m_UsePrototypeMesh = 0;
	}
}
