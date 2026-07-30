using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000282 RID: 642
	public static class Easing
	{
		// Token: 0x060012B8 RID: 4792 RVA: 0x000546D0 File Offset: 0x000528D0
		public static float Step(float t)
		{
			return (float)((t < 0.5f) ? 0 : 1);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x000546F0 File Offset: 0x000528F0
		public static float Linear(float t)
		{
			return t;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00054704 File Offset: 0x00052904
		public static float InSine(float t)
		{
			return Mathf.Sin(1.5707964f * (t - 1f)) + 1f;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00054730 File Offset: 0x00052930
		public static float OutSine(float t)
		{
			return Mathf.Sin(t * 1.5707964f);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00054750 File Offset: 0x00052950
		public static float InOutSine(float t)
		{
			return (Mathf.Sin(3.1415927f * (t - 0.5f)) + 1f) * 0.5f;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00054780 File Offset: 0x00052980
		public static float InQuad(float t)
		{
			return t * t;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00054798 File Offset: 0x00052998
		public static float OutQuad(float t)
		{
			return t * (2f - t);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000547B4 File Offset: 0x000529B4
		public static float InOutQuad(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float num;
			if (flag)
			{
				num = t * t * 0.5f;
			}
			else
			{
				num = -0.5f * ((t - 1f) * (t - 3f) - 1f);
			}
			return num;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00054804 File Offset: 0x00052A04
		public static float InCubic(float t)
		{
			return Easing.InPower(t, 3);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00054820 File Offset: 0x00052A20
		public static float OutCubic(float t)
		{
			return Easing.OutPower(t, 3);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0005483C File Offset: 0x00052A3C
		public static float InOutCubic(float t)
		{
			return Easing.InOutPower(t, 3);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00054858 File Offset: 0x00052A58
		public static float InPower(float t, int power)
		{
			return Mathf.Pow(t, (float)power);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00054874 File Offset: 0x00052A74
		public static float OutPower(float t, int power)
		{
			int num = ((power % 2 == 0) ? (-1) : 1);
			return (float)num * (Mathf.Pow(t - 1f, (float)power) + (float)num);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x000548A8 File Offset: 0x00052AA8
		public static float InOutPower(float t, int power)
		{
			t *= 2f;
			bool flag = t < 1f;
			float num;
			if (flag)
			{
				num = Easing.InPower(t, power) * 0.5f;
			}
			else
			{
				int num2 = ((power % 2 == 0) ? (-1) : 1);
				num = (float)num2 * 0.5f * (Mathf.Pow(t - 2f, (float)power) + (float)(num2 * 2));
			}
			return num;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00054908 File Offset: 0x00052B08
		public static float InBounce(float t)
		{
			return 1f - Easing.OutBounce(1f - t);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0005492C File Offset: 0x00052B2C
		public static float OutBounce(float t)
		{
			bool flag = t < 0.36363637f;
			float num;
			if (flag)
			{
				num = 7.5625f * t * t;
			}
			else
			{
				bool flag2 = t < 0.72727275f;
				if (flag2)
				{
					float num2;
					t = (num2 = t - 0.54545456f);
					num = 7.5625f * num2 * t + 0.75f;
				}
				else
				{
					bool flag3 = t < 0.90909094f;
					if (flag3)
					{
						float num3;
						t = (num3 = t - 0.8181818f);
						num = 7.5625f * num3 * t + 0.9375f;
					}
					else
					{
						float num4;
						t = (num4 = t - 0.95454544f);
						num = 7.5625f * num4 * t + 0.984375f;
					}
				}
			}
			return num;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000549CC File Offset: 0x00052BCC
		public static float InOutBounce(float t)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InBounce(t * 2f) * 0.5f;
			}
			else
			{
				num = Easing.OutBounce((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00054A20 File Offset: 0x00052C20
		public static float InElastic(float t)
		{
			bool flag = t == 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					float num2 = 0.3f;
					float num3 = num2 / 4f;
					float num4 = Mathf.Pow(2f, 10f * (t -= 1f));
					num = -(num4 * Mathf.Sin((t - num3) * 6.2831855f / num2));
				}
			}
			return num;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00054A9C File Offset: 0x00052C9C
		public static float OutElastic(float t)
		{
			bool flag = t == 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					float num2 = 0.3f;
					float num3 = num2 / 4f;
					num = Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - num3) * 6.2831855f / num2) + 1f;
				}
			}
			return num;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00054B10 File Offset: 0x00052D10
		public static float InOutElastic(float t)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InElastic(t * 2f) * 0.5f;
			}
			else
			{
				num = Easing.OutElastic((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00054B64 File Offset: 0x00052D64
		public static float InBack(float t)
		{
			float num = 1.70158f;
			return t * t * ((num + 1f) * t - num);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00054B8C File Offset: 0x00052D8C
		public static float OutBack(float t)
		{
			return 1f - Easing.InBack(1f - t);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00054BB0 File Offset: 0x00052DB0
		public static float InOutBack(float t)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InBack(t * 2f) * 0.5f;
			}
			else
			{
				num = Easing.OutBack((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00054C04 File Offset: 0x00052E04
		public static float InBack(float t, float s)
		{
			return t * t * ((s + 1f) * t - s);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00054C28 File Offset: 0x00052E28
		public static float OutBack(float t, float s)
		{
			return 1f - Easing.InBack(1f - t, s);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00054C50 File Offset: 0x00052E50
		public static float InOutBack(float t, float s)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InBack(t * 2f, s) * 0.5f;
			}
			else
			{
				num = Easing.OutBack((t - 0.5f) * 2f, s) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00054CA8 File Offset: 0x00052EA8
		public static float InCirc(float t)
		{
			return -(Mathf.Sqrt(1f - t * t) - 1f);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00054CD0 File Offset: 0x00052ED0
		public static float OutCirc(float t)
		{
			t -= 1f;
			return Mathf.Sqrt(1f - t * t);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00054CFC File Offset: 0x00052EFC
		public static float InOutCirc(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float num;
			if (flag)
			{
				num = -0.5f * (Mathf.Sqrt(1f - t * t) - 1f);
			}
			else
			{
				t -= 2f;
				num = 0.5f * (Mathf.Sqrt(1f - t * t) + 1f);
			}
			return num;
		}

		// Token: 0x0400098C RID: 2444
		private const float HalfPi = 1.5707964f;
	}
}
