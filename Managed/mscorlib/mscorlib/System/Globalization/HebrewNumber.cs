using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000417 RID: 1047
	internal class HebrewNumber
	{
		// Token: 0x060031C1 RID: 12737 RVA: 0x00002111 File Offset: 0x00000311
		private HebrewNumber()
		{
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000B3648 File Offset: 0x000B1848
		internal static string ToString(int Number)
		{
			char c = '\0';
			StringBuilder stringBuilder = new StringBuilder();
			if (Number > 5000)
			{
				Number -= 5000;
			}
			int num = Number / 100;
			if (num > 0)
			{
				Number -= num * 100;
				for (int i = 0; i < num / 4; i++)
				{
					stringBuilder.Append('ת');
				}
				int num2 = num % 4;
				if (num2 > 0)
				{
					stringBuilder.Append((char)(1510 + num2));
				}
			}
			int num3 = Number / 10;
			Number %= 10;
			switch (num3)
			{
			case 0:
				c = '\0';
				break;
			case 1:
				c = 'י';
				break;
			case 2:
				c = 'כ';
				break;
			case 3:
				c = 'ל';
				break;
			case 4:
				c = 'מ';
				break;
			case 5:
				c = 'נ';
				break;
			case 6:
				c = 'ס';
				break;
			case 7:
				c = 'ע';
				break;
			case 8:
				c = 'פ';
				break;
			case 9:
				c = 'צ';
				break;
			}
			char c2 = (char)((Number > 0) ? (1488 + Number - 1) : 0);
			if (c2 == 'ה' && c == 'י')
			{
				c2 = 'ו';
				c = 'ט';
			}
			if (c2 == 'ו' && c == 'י')
			{
				c2 = 'ז';
				c = 'ט';
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
			if (c2 != '\0')
			{
				stringBuilder.Append(c2);
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Insert(stringBuilder.Length - 1, '"');
			}
			else
			{
				stringBuilder.Append('\'');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000B37D4 File Offset: 0x000B19D4
		internal static HebrewNumberParsingState ParseByChar(char ch, ref HebrewNumberParsingContext context)
		{
			HebrewNumber.HebrewToken hebrewToken;
			if (ch == '\'')
			{
				hebrewToken = HebrewNumber.HebrewToken.SingleQuote;
			}
			else if (ch == '"')
			{
				hebrewToken = HebrewNumber.HebrewToken.DoubleQuote;
			}
			else
			{
				int num = (int)(ch - 'א');
				if (num < 0 || num >= HebrewNumber.HebrewValues.Length)
				{
					return HebrewNumberParsingState.NotHebrewDigit;
				}
				hebrewToken = HebrewNumber.HebrewValues[num].token;
				if (hebrewToken == HebrewNumber.HebrewToken.Invalid)
				{
					return HebrewNumberParsingState.NotHebrewDigit;
				}
				context.result += HebrewNumber.HebrewValues[num].value;
			}
			context.state = HebrewNumber.NumberPasingState[(int)context.state][(int)hebrewToken];
			if (context.state == HebrewNumber.HS._err)
			{
				return HebrewNumberParsingState.InvalidHebrewNumber;
			}
			if (context.state == HebrewNumber.HS.END)
			{
				return HebrewNumberParsingState.FoundEndOfHebrewNumber;
			}
			return HebrewNumberParsingState.ContinueParsing;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000B3863 File Offset: 0x000B1A63
		internal static bool IsDigit(char ch)
		{
			if (ch >= 'א' && ch <= HebrewNumber.maxHebrewNumberCh)
			{
				return HebrewNumber.HebrewValues[(int)(ch - 'א')].value >= 0;
			}
			return ch == '\'' || ch == '"';
		}

		// Token: 0x04001A52 RID: 6738
		private static HebrewNumber.HebrewValue[] HebrewValues = new HebrewNumber.HebrewValue[]
		{
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 2),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 3),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 4),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 5),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 6),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 7),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 8),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit9, 9),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 10),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 20),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 30),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 40),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 50),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 60),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 70),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 80),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 90),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit100, 100),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 200),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 300),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit400, 400)
		};

		// Token: 0x04001A53 RID: 6739
		private const int minHebrewNumberCh = 1488;

		// Token: 0x04001A54 RID: 6740
		private static char maxHebrewNumberCh = (char)(1488 + HebrewNumber.HebrewValues.Length - 1);

		// Token: 0x04001A55 RID: 6741
		private static readonly HebrewNumber.HS[][] NumberPasingState = new HebrewNumber.HS[][]
		{
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS.S400,
				HebrewNumber.HS.X00,
				HebrewNumber.HS.X00,
				HebrewNumber.HS.X0,
				HebrewNumber.HS.X,
				HebrewNumber.HS.X,
				HebrewNumber.HS.X,
				HebrewNumber.HS.S9,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS.S400_400,
				HebrewNumber.HS.S400_X00,
				HebrewNumber.HS.S400_X00,
				HebrewNumber.HS.S400_X0,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.X00_S9,
				HebrewNumber.HS.END,
				HebrewNumber.HS.S400_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.S400_400_100,
				HebrewNumber.HS.S400_X0,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.X00_S9,
				HebrewNumber.HS._err,
				HebrewNumber.HS.S400_400_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.S400_X00_X0,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.X00_S9,
				HebrewNumber.HS._err,
				HebrewNumber.HS.X00_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.X0_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.X0_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.END,
				HebrewNumber.HS._err
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.END,
				HebrewNumber.HS.X0_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.S400_X0,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.X00_S9,
				HebrewNumber.HS.END,
				HebrewNumber.HS.X00_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.S400_X00_X0,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.X00_S9,
				HebrewNumber.HS._err,
				HebrewNumber.HS.X00_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.END,
				HebrewNumber.HS.S9_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.S9_DQ
			},
			new HebrewNumber.HS[]
			{
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS.END,
				HebrewNumber.HS.END,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err,
				HebrewNumber.HS._err
			}
		};

		// Token: 0x02000418 RID: 1048
		private enum HebrewToken
		{
			// Token: 0x04001A57 RID: 6743
			Invalid = -1,
			// Token: 0x04001A58 RID: 6744
			Digit400,
			// Token: 0x04001A59 RID: 6745
			Digit200_300,
			// Token: 0x04001A5A RID: 6746
			Digit100,
			// Token: 0x04001A5B RID: 6747
			Digit10,
			// Token: 0x04001A5C RID: 6748
			Digit1,
			// Token: 0x04001A5D RID: 6749
			Digit6_7,
			// Token: 0x04001A5E RID: 6750
			Digit7,
			// Token: 0x04001A5F RID: 6751
			Digit9,
			// Token: 0x04001A60 RID: 6752
			SingleQuote,
			// Token: 0x04001A61 RID: 6753
			DoubleQuote
		}

		// Token: 0x02000419 RID: 1049
		private class HebrewValue
		{
			// Token: 0x060031C6 RID: 12742 RVA: 0x000B3B7A File Offset: 0x000B1D7A
			internal HebrewValue(HebrewNumber.HebrewToken token, int value)
			{
				this.token = token;
				this.value = value;
			}

			// Token: 0x04001A62 RID: 6754
			internal HebrewNumber.HebrewToken token;

			// Token: 0x04001A63 RID: 6755
			internal int value;
		}

		// Token: 0x0200041A RID: 1050
		internal enum HS
		{
			// Token: 0x04001A65 RID: 6757
			_err = -1,
			// Token: 0x04001A66 RID: 6758
			Start,
			// Token: 0x04001A67 RID: 6759
			S400,
			// Token: 0x04001A68 RID: 6760
			S400_400,
			// Token: 0x04001A69 RID: 6761
			S400_X00,
			// Token: 0x04001A6A RID: 6762
			S400_X0,
			// Token: 0x04001A6B RID: 6763
			X00_DQ,
			// Token: 0x04001A6C RID: 6764
			S400_X00_X0,
			// Token: 0x04001A6D RID: 6765
			X0_DQ,
			// Token: 0x04001A6E RID: 6766
			X,
			// Token: 0x04001A6F RID: 6767
			X0,
			// Token: 0x04001A70 RID: 6768
			X00,
			// Token: 0x04001A71 RID: 6769
			S400_DQ,
			// Token: 0x04001A72 RID: 6770
			S400_400_DQ,
			// Token: 0x04001A73 RID: 6771
			S400_400_100,
			// Token: 0x04001A74 RID: 6772
			S9,
			// Token: 0x04001A75 RID: 6773
			X00_S9,
			// Token: 0x04001A76 RID: 6774
			S9_DQ,
			// Token: 0x04001A77 RID: 6775
			END = 100
		}
	}
}
