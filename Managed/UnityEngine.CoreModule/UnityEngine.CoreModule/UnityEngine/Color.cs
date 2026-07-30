using System;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000166 RID: 358
	[NativeClass("ColorRGBAf")]
	[NativeHeader("Runtime/Math/Color.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Color : IEquatable<Color>, IFormattable
	{
		// Token: 0x06001033 RID: 4147 RVA: 0x00016B7B File Offset: 0x00014D7B
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00016B9B File Offset: 0x00014D9B
		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = 1f;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00016BE4 File Offset: 0x00014DE4
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00016C08 File Offset: 0x00014E08
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F3";
			}
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r.ToString(format, formatProvider),
				this.g.ToString(format, formatProvider),
				this.b.ToString(format, formatProvider),
				this.a.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00016C7C File Offset: 0x00014E7C
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00016CA8 File Offset: 0x00014EA8
		public override bool Equals(object other)
		{
			bool flag = !(other is Color);
			return !flag && this.Equals((Color)other);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00016CDC File Offset: 0x00014EDC
		public bool Equals(Color other)
		{
			return this.r.Equals(other.r) && this.g.Equals(other.g) && this.b.Equals(other.b) && this.a.Equals(other.a);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00016D3C File Offset: 0x00014F3C
		public static Color operator +(Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00016D88 File Offset: 0x00014F88
		public static Color operator -(Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00016DD4 File Offset: 0x00014FD4
		public static Color operator *(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00016E20 File Offset: 0x00015020
		public static Color operator *(Color a, float b)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00016E58 File Offset: 0x00015058
		public static Color operator *(float b, Color a)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00016E90 File Offset: 0x00015090
		public static Color operator /(Color a, float b)
		{
			return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00016EC8 File Offset: 0x000150C8
		public static bool operator ==(Color lhs, Color rhs)
		{
			return lhs == rhs;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00016EEC File Offset: 0x000150EC
		public static bool operator !=(Color lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00016F08 File Offset: 0x00015108
		public static Color Lerp(Color a, Color b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00016F80 File Offset: 0x00015180
		public static Color LerpUnclamped(Color a, Color b, float t)
		{
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00016FF0 File Offset: 0x000151F0
		internal Color RGBMultiplied(float multiplier)
		{
			return new Color(this.r * multiplier, this.g * multiplier, this.b * multiplier, this.a);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00017028 File Offset: 0x00015228
		internal Color AlphaMultiplied(float multiplier)
		{
			return new Color(this.r, this.g, this.b, this.a * multiplier);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0001705C File Offset: 0x0001525C
		internal Color RGBMultiplied(Color multiplier)
		{
			return new Color(this.r * multiplier.r, this.g * multiplier.g, this.b * multiplier.b, this.a);
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x000170A0 File Offset: 0x000152A0
		public static Color red
		{
			get
			{
				return new Color(1f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x000170CC File Offset: 0x000152CC
		public static Color green
		{
			get
			{
				return new Color(0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x000170F8 File Offset: 0x000152F8
		public static Color blue
		{
			get
			{
				return new Color(0f, 0f, 1f, 1f);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00017124 File Offset: 0x00015324
		public static Color white
		{
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00017150 File Offset: 0x00015350
		public static Color black
		{
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0001717C File Offset: 0x0001537C
		public static Color yellow
		{
			get
			{
				return new Color(1f, 0.92156863f, 0.015686275f, 1f);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x000171A8 File Offset: 0x000153A8
		public static Color cyan
		{
			get
			{
				return new Color(0f, 1f, 1f, 1f);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000171D4 File Offset: 0x000153D4
		public static Color magenta
		{
			get
			{
				return new Color(1f, 0f, 1f, 1f);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00017200 File Offset: 0x00015400
		public static Color gray
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0001722C File Offset: 0x0001542C
		public static Color grey
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00017258 File Offset: 0x00015458
		public static Color clear
		{
			get
			{
				return new Color(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00017284 File Offset: 0x00015484
		public float grayscale
		{
			get
			{
				return 0.299f * this.r + 0.587f * this.g + 0.114f * this.b;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x000172BC File Offset: 0x000154BC
		public Color linear
		{
			get
			{
				return new Color(Mathf.GammaToLinearSpace(this.r), Mathf.GammaToLinearSpace(this.g), Mathf.GammaToLinearSpace(this.b), this.a);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x000172FC File Offset: 0x000154FC
		public Color gamma
		{
			get
			{
				return new Color(Mathf.LinearToGammaSpace(this.r), Mathf.LinearToGammaSpace(this.g), Mathf.LinearToGammaSpace(this.b), this.a);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0001733C File Offset: 0x0001553C
		public float maxColorComponent
		{
			get
			{
				return Mathf.Max(Mathf.Max(this.r, this.g), this.b);
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0001736C File Offset: 0x0001556C
		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.r, c.g, c.b, c.a);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0001739C File Offset: 0x0001559C
		public static implicit operator Color(Vector4 v)
		{
			return new Color(v.x, v.y, v.z, v.w);
		}

		// Token: 0x17000356 RID: 854
		public float this[int index]
		{
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.r;
					break;
				case 1:
					num = this.g;
					break;
				case 2:
					num = this.b;
					break;
				case 3:
					num = this.a;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color index(" + index + ")!");
				}
				return num;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.r = value;
					break;
				case 1:
					this.g = value;
					break;
				case 2:
					this.b = value;
					break;
				case 3:
					this.a = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color index(" + index + ")!");
				}
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0001749C File Offset: 0x0001569C
		public static void RGBToHSV(Color rgbColor, out float H, out float S, out float V)
		{
			bool flag = rgbColor.b > rgbColor.g && rgbColor.b > rgbColor.r;
			if (flag)
			{
				Color.RGBToHSVHelper(4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
			}
			else
			{
				bool flag2 = rgbColor.g > rgbColor.r;
				if (flag2)
				{
					Color.RGBToHSVHelper(2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
				}
				else
				{
					Color.RGBToHSVHelper(0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
				}
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00017544 File Offset: 0x00015744
		private static void RGBToHSVHelper(float offset, float dominantcolor, float colorone, float colortwo, out float H, out float S, out float V)
		{
			V = dominantcolor;
			bool flag = V != 0f;
			if (flag)
			{
				bool flag2 = colorone > colortwo;
				float num;
				if (flag2)
				{
					num = colortwo;
				}
				else
				{
					num = colorone;
				}
				float num2 = V - num;
				bool flag3 = num2 != 0f;
				if (flag3)
				{
					S = num2 / V;
					H = offset + (colorone - colortwo) / num2;
				}
				else
				{
					S = 0f;
					H = offset + (colorone - colortwo);
				}
				H /= 6f;
				bool flag4 = H < 0f;
				if (flag4)
				{
					H += 1f;
				}
			}
			else
			{
				S = 0f;
				H = 0f;
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x000175F8 File Offset: 0x000157F8
		public static Color HSVToRGB(float H, float S, float V)
		{
			return Color.HSVToRGB(H, S, V, true);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00017614 File Offset: 0x00015814
		public static Color HSVToRGB(float H, float S, float V, bool hdr)
		{
			Color white = Color.white;
			bool flag = S == 0f;
			if (flag)
			{
				white.r = V;
				white.g = V;
				white.b = V;
			}
			else
			{
				bool flag2 = V == 0f;
				if (flag2)
				{
					white.r = 0f;
					white.g = 0f;
					white.b = 0f;
				}
				else
				{
					white.r = 0f;
					white.g = 0f;
					white.b = 0f;
					float num = H * 6f;
					int num2 = (int)Mathf.Floor(num);
					float num3 = num - (float)num2;
					float num4 = V * (1f - S);
					float num5 = V * (1f - S * num3);
					float num6 = V * (1f - S * (1f - num3));
					switch (num2)
					{
					case -1:
						white.r = V;
						white.g = num4;
						white.b = num5;
						break;
					case 0:
						white.r = V;
						white.g = num6;
						white.b = num4;
						break;
					case 1:
						white.r = num5;
						white.g = V;
						white.b = num4;
						break;
					case 2:
						white.r = num4;
						white.g = V;
						white.b = num6;
						break;
					case 3:
						white.r = num4;
						white.g = num5;
						white.b = V;
						break;
					case 4:
						white.r = num6;
						white.g = num4;
						white.b = V;
						break;
					case 5:
						white.r = V;
						white.g = num4;
						white.b = num5;
						break;
					case 6:
						white.r = V;
						white.g = num6;
						white.b = num4;
						break;
					}
					bool flag3 = !hdr;
					if (flag3)
					{
						white.r = Mathf.Clamp(white.r, 0f, 1f);
						white.g = Mathf.Clamp(white.g, 0f, 1f);
						white.b = Mathf.Clamp(white.b, 0f, 1f);
					}
				}
			}
			return white;
		}

		// Token: 0x040005AA RID: 1450
		public float r;

		// Token: 0x040005AB RID: 1451
		public float g;

		// Token: 0x040005AC RID: 1452
		public float b;

		// Token: 0x040005AD RID: 1453
		public float a;
	}
}
