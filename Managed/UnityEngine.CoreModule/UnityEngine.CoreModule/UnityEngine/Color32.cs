using System;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000167 RID: 359
	[UsedByNativeCode]
	[StructLayout(2)]
	public struct Color32 : IFormattable
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x0001788A File Offset: 0x00015A8A
		public Color32(byte r, byte g, byte b, byte a)
		{
			this.rgba = 0;
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000178B4 File Offset: 0x00015AB4
		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)Mathf.Round(Mathf.Clamp01(c.r) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.g) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.b) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.a) * 255f));
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00017928 File Offset: 0x00015B28
		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00017974 File Offset: 0x00015B74
		public static Color32 Lerp(Color32 a, Color32 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x000179F8 File Offset: 0x00015BF8
		public static Color32 LerpUnclamped(Color32 a, Color32 b, float t)
		{
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x17000357 RID: 855
		public byte this[int index]
		{
			get
			{
				byte b;
				switch (index)
				{
				case 0:
					b = this.r;
					break;
				case 1:
					b = this.g;
					break;
				case 2:
					b = this.b;
					break;
				case 3:
					b = this.a;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index + ")!");
				}
				return b;
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
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index + ")!");
				}
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00017B44 File Offset: 0x00015D44
		[VisibleToOtherModules]
		internal bool InternalEquals(Color32 other)
		{
			return this.rgba == other.rgba;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00017B64 File Offset: 0x00015D64
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00017B88 File Offset: 0x00015D88
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00017BAC File Offset: 0x00015DAC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r.ToString(format, formatProvider),
				this.g.ToString(format, formatProvider),
				this.b.ToString(format, formatProvider),
				this.a.ToString(format, formatProvider)
			});
		}

		// Token: 0x040005AE RID: 1454
		[Ignore(DoesNotContributeToSize = true)]
		[FieldOffset(0)]
		private int rgba;

		// Token: 0x040005AF RID: 1455
		[FieldOffset(0)]
		public byte r;

		// Token: 0x040005B0 RID: 1456
		[FieldOffset(1)]
		public byte g;

		// Token: 0x040005B1 RID: 1457
		[FieldOffset(2)]
		public byte b;

		// Token: 0x040005B2 RID: 1458
		[FieldOffset(3)]
		public byte a;
	}
}
