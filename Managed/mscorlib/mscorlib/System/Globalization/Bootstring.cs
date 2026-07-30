using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000446 RID: 1094
	internal class Bootstring
	{
		// Token: 0x06003480 RID: 13440 RVA: 0x000C29E8 File Offset: 0x000C0BE8
		public Bootstring(char delimiter, int baseNum, int tmin, int tmax, int skew, int damp, int initialBias, int initialN)
		{
			this.delimiter = delimiter;
			this.base_num = baseNum;
			this.tmin = tmin;
			this.tmax = tmax;
			this.skew = skew;
			this.damp = damp;
			this.initial_bias = initialBias;
			this.initial_n = initialN;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000C2A38 File Offset: 0x000C0C38
		public string Encode(string s, int offset)
		{
			int num = this.initial_n;
			int num2 = 0;
			int num3 = this.initial_bias;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < '\u0080')
				{
					stringBuilder.Append(s[i]);
				}
			}
			int length;
			int j = (length = stringBuilder.Length);
			if (length > 0)
			{
				stringBuilder.Append(this.delimiter);
			}
			while (j < s.Length)
			{
				int num4 = int.MaxValue;
				for (int k = 0; k < s.Length; k++)
				{
					if ((int)s[k] >= num && (int)s[k] < num4)
					{
						num4 = (int)s[k];
					}
				}
				checked
				{
					num2 += (num4 - num) * (j + 1);
					num = num4;
					foreach (char c in s)
					{
						if ((int)c < num || c < '\u0080')
						{
							num2++;
						}
						unchecked
						{
							if ((int)c == num)
							{
								int num5 = num2;
								int num6 = this.base_num;
								for (;;)
								{
									int num7 = ((num6 <= num3 + this.tmin) ? this.tmin : ((num6 >= num3 + this.tmax) ? this.tmax : (num6 - num3)));
									if (num5 < num7)
									{
										break;
									}
									stringBuilder.Append(this.EncodeDigit(num7 + (num5 - num7) % (this.base_num - num7)));
									num5 = (num5 - num7) / (this.base_num - num7);
									num6 += this.base_num;
								}
								stringBuilder.Append(this.EncodeDigit(num5));
								num3 = this.Adapt(num2, j + 1, j == length);
								num2 = 0;
								j++;
							}
						}
					}
				}
				num2++;
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000C2C07 File Offset: 0x000C0E07
		private char EncodeDigit(int d)
		{
			return (char)((d < 26) ? (d + 97) : (d - 26 + 48));
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000C2C1C File Offset: 0x000C0E1C
		private int DecodeDigit(char c)
		{
			if (c - '0' < '\n')
			{
				return (int)(c - '\u0016');
			}
			if (c - 'A' < '\u001a')
			{
				return (int)(c - 'A');
			}
			if (c - 'a' >= '\u001a')
			{
				return this.base_num;
			}
			return (int)(c - 'a');
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000C2C4C File Offset: 0x000C0E4C
		private int Adapt(int delta, int numPoints, bool firstTime)
		{
			if (firstTime)
			{
				delta /= this.damp;
			}
			else
			{
				delta /= 2;
			}
			delta += delta / numPoints;
			int num = 0;
			while (delta > (this.base_num - this.tmin) * this.tmax / 2)
			{
				delta /= this.base_num - this.tmin;
				num += this.base_num;
			}
			return num + (this.base_num - this.tmin + 1) * delta / (delta + this.skew);
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000C2CC8 File Offset: 0x000C0EC8
		public string Decode(string s, int offset)
		{
			int num = this.initial_n;
			int num2 = 0;
			int num3 = this.initial_bias;
			int num4 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == this.delimiter)
				{
					num4 = i;
				}
			}
			if (num4 < 0)
			{
				return s;
			}
			stringBuilder.Append(s, 0, num4);
			int j = ((num4 > 0) ? (num4 + 1) : 0);
			while (j < s.Length)
			{
				int num5 = num2;
				int num6 = 1;
				int num7 = this.base_num;
				for (;;)
				{
					int num8 = this.DecodeDigit(s[j++]);
					num2 += num8 * num6;
					int num9 = ((num7 <= num3 + this.tmin) ? this.tmin : ((num7 >= num3 + this.tmax) ? this.tmax : (num7 - num3)));
					if (num8 < num9)
					{
						break;
					}
					num6 *= this.base_num - num9;
					num7 += this.base_num;
				}
				num3 = this.Adapt(num2 - num5, stringBuilder.Length + 1, num5 == 0);
				num += num2 / (stringBuilder.Length + 1);
				num2 %= stringBuilder.Length + 1;
				if (num < 128)
				{
					throw new ArgumentException(string.Format("Invalid Bootstring decode result, at {0}", offset + j));
				}
				stringBuilder.Insert(num2, (char)num);
				num2++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001C0D RID: 7181
		private readonly char delimiter;

		// Token: 0x04001C0E RID: 7182
		private readonly int base_num;

		// Token: 0x04001C0F RID: 7183
		private readonly int tmin;

		// Token: 0x04001C10 RID: 7184
		private readonly int tmax;

		// Token: 0x04001C11 RID: 7185
		private readonly int skew;

		// Token: 0x04001C12 RID: 7186
		private readonly int damp;

		// Token: 0x04001C13 RID: 7187
		private readonly int initial_bias;

		// Token: 0x04001C14 RID: 7188
		private readonly int initial_n;
	}
}
