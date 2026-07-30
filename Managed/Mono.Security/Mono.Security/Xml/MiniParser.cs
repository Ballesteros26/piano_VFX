using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x02000005 RID: 5
	[CLSCompliant(false)]
	public class MiniParser
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002064 File Offset: 0x00000264
		public MiniParser()
		{
			this.twoCharBuff = new int[2];
			this.splitCData = false;
			this.Reset();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002085 File Offset: 0x00000285
		public void Reset()
		{
			this.line = 0;
			this.col = 0;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002098 File Offset: 0x00000298
		protected static bool StrEquals(string str, StringBuilder sb, int sbStart, int len)
		{
			if (len != str.Length)
			{
				return false;
			}
			for (int i = 0; i < len; i++)
			{
				if (str[i] != sb[sbStart + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D1 File Offset: 0x000002D1
		protected void FatalErr(string descr)
		{
			throw new MiniParser.XMLError(descr, this.line, this.col);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		protected static int Xlat(int charCode, int state)
		{
			int num = state * MiniParser.INPUT_RANGE;
			int num2 = Math.Min(MiniParser.tbl.Length - num, MiniParser.INPUT_RANGE);
			while (--num2 >= 0)
			{
				ushort num3 = MiniParser.tbl[num];
				if (charCode == num3 >> 12)
				{
					return (int)(num3 & 4095);
				}
				num++;
			}
			return 4095;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public void Parse(MiniParser.IReader reader, MiniParser.IHandler handler)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (handler == null)
			{
				handler = new MiniParser.HandlerAdapter();
			}
			MiniParser.AttrListImpl attrListImpl = new MiniParser.AttrListImpl();
			string text = null;
			Stack stack = new Stack();
			string text2 = null;
			this.line = 1;
			this.col = 0;
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num3 = 0;
			handler.OnStartParsing(this);
			for (;;)
			{
				this.col++;
				num = reader.Read();
				if (num == -1)
				{
					break;
				}
				int num4 = "<>/?=&'\"![ ]\t\r\n".IndexOf((char)num) & 15;
				if (num4 != 13)
				{
					if (num4 == 12)
					{
						num4 = 10;
					}
					if (num4 == 14)
					{
						this.col = 0;
						this.line++;
						num4 = 10;
					}
					int num5 = MiniParser.Xlat(num4, num2);
					num2 = num5 & 255;
					if (num != 10 || (num2 != 14 && num2 != 15))
					{
						num5 >>= 8;
						if (num2 >= 128)
						{
							if (num2 == 255)
							{
								this.FatalErr("State dispatch error.");
							}
							else
							{
								this.FatalErr(MiniParser.errors[num2 ^ 128]);
							}
						}
						switch (num5)
						{
						case 0:
							break;
						case 1:
						{
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							string text3 = null;
							if (stack.Count == 0 || text2 != (text3 = stack.Pop() as string))
							{
								if (text3 == null)
								{
									this.FatalErr("Tag stack underflow");
								}
								else
								{
									this.FatalErr(string.Format("Expected end tag '{0}' but found '{1}'", text2, text3));
								}
							}
							handler.OnEndElement(text2);
							continue;
						}
						case 2:
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							if (num != 47 && num != 62)
							{
								continue;
							}
							break;
						case 3:
							text = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							continue;
						case 4:
							if (text == null)
							{
								this.FatalErr("Internal error.");
							}
							attrListImpl.Add(text, stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							text = null;
							continue;
						case 5:
							handler.OnChars(stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							continue;
						case 6:
						{
							string text4 = "CDATA[";
							flag2 = false;
							flag3 = false;
							if (num == 45)
							{
								num = reader.Read();
								if (num != 45)
								{
									this.FatalErr("Invalid comment");
								}
								this.col++;
								flag2 = true;
								this.twoCharBuff[0] = -1;
								this.twoCharBuff[1] = -1;
								continue;
							}
							if (num != 91)
							{
								flag3 = true;
								num3 = 0;
								continue;
							}
							for (int i = 0; i < text4.Length; i++)
							{
								if (reader.Read() != (int)text4[i])
								{
									this.col += i + 1;
									break;
								}
							}
							this.col += text4.Length;
							flag = true;
							continue;
						}
						case 7:
						{
							int num6 = 0;
							num = 93;
							while (num == 93)
							{
								num = reader.Read();
								num6++;
							}
							if (num != 62)
							{
								for (int j = 0; j < num6; j++)
								{
									stringBuilder.Append(']');
								}
								stringBuilder.Append((char)num);
								num2 = 18;
							}
							else
							{
								for (int k = 0; k < num6 - 2; k++)
								{
									stringBuilder.Append(']');
								}
								flag = false;
							}
							this.col += num6;
							continue;
						}
						case 8:
							this.FatalErr(string.Format("Error {0}", num2));
							continue;
						case 9:
							continue;
						case 10:
							stringBuilder = new StringBuilder();
							if (num != 60)
							{
								goto IL_03E3;
							}
							continue;
						case 11:
							goto IL_03E3;
						case 12:
							if (flag2)
							{
								if (num == 62 && this.twoCharBuff[0] == 45 && this.twoCharBuff[1] == 45)
								{
									flag2 = false;
									num2 = 0;
									continue;
								}
								this.twoCharBuff[0] = this.twoCharBuff[1];
								this.twoCharBuff[1] = num;
								continue;
							}
							else
							{
								if (!flag3)
								{
									if (this.splitCData && stringBuilder.Length > 0 && flag)
									{
										handler.OnChars(stringBuilder.ToString());
										stringBuilder = new StringBuilder();
									}
									flag = false;
									stringBuilder.Append((char)num);
									continue;
								}
								if (num == 60 || num == 62)
								{
									num3 ^= 1;
								}
								if (num == 62 && num3 != 0)
								{
									flag3 = false;
									num2 = 0;
									continue;
								}
								continue;
							}
							break;
						case 13:
						{
							num = reader.Read();
							int num7 = this.col + 1;
							if (num == 35)
							{
								int num8 = 10;
								int num9 = 0;
								int num10 = 0;
								num = reader.Read();
								num7++;
								if (num == 120)
								{
									num = reader.Read();
									num7++;
									num8 = 16;
								}
								NumberStyles numberStyles = ((num8 == 16) ? NumberStyles.HexNumber : NumberStyles.Integer);
								for (;;)
								{
									int num11 = -1;
									if (char.IsNumber((char)num) || "abcdef".IndexOf(char.ToLower((char)num)) != -1)
									{
										try
										{
											num11 = int.Parse(new string((char)num, 1), numberStyles);
										}
										catch (FormatException)
										{
											num11 = -1;
										}
									}
									if (num11 == -1)
									{
										break;
									}
									num9 *= num8;
									num9 += num11;
									num10++;
									num = reader.Read();
									num7++;
								}
								if (num == 59 && num10 > 0)
								{
									stringBuilder.Append((char)num9);
								}
								else
								{
									this.FatalErr("Bad char ref");
								}
							}
							else
							{
								string text5 = "aglmopqstu";
								string text6 = "&'\"><";
								int num12 = 0;
								int num13 = 15;
								int num14 = 0;
								int length = stringBuilder.Length;
								for (;;)
								{
									if (num12 != 15)
									{
										num12 = text5.IndexOf((char)num) & 15;
									}
									if (num12 == 15)
									{
										this.FatalErr(MiniParser.errors[7]);
									}
									stringBuilder.Append((char)num);
									int num15 = (int)"Ｕ㾏侏ཟｸ\ue1f4⊙\ueeff\ueeffｏ"[num12];
									int num16 = (num15 >> 4) & 15;
									int num17 = num15 & 15;
									int num18 = num15 >> 12;
									int num19 = (num15 >> 8) & 15;
									num = reader.Read();
									num7++;
									num12 = 15;
									if (num16 != 15 && num == (int)text5[num16])
									{
										if (num18 < 14)
										{
											num13 = num18;
										}
										num14 = 12;
									}
									else if (num17 != 15 && num == (int)text5[num17])
									{
										if (num19 < 14)
										{
											num13 = num19;
										}
										num14 = 8;
									}
									else if (num == 59)
									{
										if (num13 != 15 && num14 != 0 && ((num15 >> num14) & 15) == 14)
										{
											break;
										}
										continue;
									}
									num12 = 0;
								}
								int num20 = num7 - this.col - 1;
								if (num20 > 0 && num20 < 5 && (MiniParser.StrEquals("amp", stringBuilder, length, num20) || MiniParser.StrEquals("apos", stringBuilder, length, num20) || MiniParser.StrEquals("quot", stringBuilder, length, num20) || MiniParser.StrEquals("lt", stringBuilder, length, num20) || MiniParser.StrEquals("gt", stringBuilder, length, num20)))
								{
									stringBuilder.Length = length;
									stringBuilder.Append(text6[num13]);
								}
								else
								{
									this.FatalErr(MiniParser.errors[7]);
								}
							}
							this.col = num7;
							continue;
						}
						default:
							this.FatalErr(string.Format("Unexpected action code - {0}.", num5));
							continue;
						}
						handler.OnStartElement(text2, attrListImpl);
						if (num != 47)
						{
							stack.Push(text2);
						}
						else
						{
							handler.OnEndElement(text2);
						}
						attrListImpl.Clear();
						continue;
						IL_03E3:
						stringBuilder.Append((char)num);
					}
				}
			}
			if (num2 != 0)
			{
				this.FatalErr("Unexpected EOF");
			}
			handler.OnEndParsing(this);
		}

		// Token: 0x04000038 RID: 56
		private static readonly int INPUT_RANGE = 13;

		// Token: 0x04000039 RID: 57
		private static readonly ushort[] tbl = new ushort[]
		{
			2305, 43264, 63616, 10368, 6272, 14464, 18560, 22656, 26752, 34944,
			39040, 47232, 30848, 2177, 10498, 6277, 14595, 18561, 22657, 26753,
			35088, 39041, 43137, 47233, 30849, 64004, 4352, 43266, 64258, 2177,
			10369, 14465, 18561, 22657, 26753, 34945, 39041, 47233, 30849, 14597,
			2307, 10499, 6403, 18691, 22787, 26883, 35075, 39171, 43267, 47363,
			30979, 63747, 64260, 8710, 4615, 41480, 2177, 14465, 18561, 22657,
			26753, 34945, 39041, 47233, 30849, 6400, 2307, 10499, 14595, 18691,
			22787, 26883, 35075, 39171, 43267, 47363, 30979, 63747, 6400, 2177,
			10369, 14465, 18561, 22657, 26753, 34945, 39041, 43137, 47233, 30849,
			63617, 2561, 23818, 11274, 7178, 15370, 19466, 27658, 35850, 39946,
			43783, 48138, 31754, 64522, 64265, 8198, 4103, 43272, 2177, 14465,
			18561, 22657, 26753, 34945, 39041, 47233, 30849, 64265, 17163, 43276,
			2178, 10370, 6274, 14466, 22658, 26754, 34946, 39042, 47234, 30850,
			2317, 23818, 11274, 7178, 15370, 19466, 27658, 35850, 39946, 44042,
			48138, 31754, 64522, 26894, 30991, 43275, 2180, 10372, 6276, 14468,
			18564, 22660, 34948, 39044, 47236, 63620, 17163, 43276, 2178, 10370,
			6274, 14466, 22658, 26754, 34946, 39042, 47234, 30850, 63618, 9474,
			35088, 2182, 6278, 14470, 18566, 22662, 26758, 39046, 43142, 47238,
			30854, 63622, 25617, 23822, 2830, 11022, 6926, 15118, 19214, 35598,
			39694, 43790, 47886, 31502, 64270, 29713, 23823, 2831, 11023, 6927,
			15119, 19215, 27407, 35599, 39695, 43791, 47887, 64271, 38418, 6400,
			1555, 9747, 13843, 17939, 22035, 26131, 34323, 42515, 46611, 30227,
			62995, 8198, 4103, 43281, 64265, 2177, 14465, 18561, 22657, 26753,
			34945, 39041, 47233, 30849, 46858, 3090, 11282, 7186, 15378, 19474,
			23570, 27666, 35858, 39954, 44050, 31762, 64530, 3091, 11283, 7187,
			15379, 19475, 23571, 27667, 35859, 39955, 44051, 48147, 31763, 64531,
			ushort.MaxValue, ushort.MaxValue
		};

		// Token: 0x0400003A RID: 58
		protected static string[] errors = new string[] { "Expected element", "Invalid character in tag", "No '='", "Invalid character entity", "Invalid attr value", "Empty tag", "No end tag", "Bad entity ref" };

		// Token: 0x0400003B RID: 59
		protected int line;

		// Token: 0x0400003C RID: 60
		protected int col;

		// Token: 0x0400003D RID: 61
		protected int[] twoCharBuff;

		// Token: 0x0400003E RID: 62
		protected bool splitCData;

		// Token: 0x020000B1 RID: 177
		public interface IReader
		{
			// Token: 0x060006A6 RID: 1702
			int Read();
		}

		// Token: 0x020000B2 RID: 178
		public interface IAttrList
		{
			// Token: 0x17000191 RID: 401
			// (get) Token: 0x060006A7 RID: 1703
			int Length { get; }

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x060006A8 RID: 1704
			bool IsEmpty { get; }

			// Token: 0x060006A9 RID: 1705
			string GetName(int i);

			// Token: 0x060006AA RID: 1706
			string GetValue(int i);

			// Token: 0x060006AB RID: 1707
			string GetValue(string name);

			// Token: 0x060006AC RID: 1708
			void ChangeValue(string name, string newValue);

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x060006AD RID: 1709
			string[] Names { get; }

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x060006AE RID: 1710
			string[] Values { get; }
		}

		// Token: 0x020000B3 RID: 179
		public interface IMutableAttrList : MiniParser.IAttrList
		{
			// Token: 0x060006AF RID: 1711
			void Clear();

			// Token: 0x060006B0 RID: 1712
			void Add(string name, string value);

			// Token: 0x060006B1 RID: 1713
			void CopyFrom(MiniParser.IAttrList attrs);

			// Token: 0x060006B2 RID: 1714
			void Remove(int i);

			// Token: 0x060006B3 RID: 1715
			void Remove(string name);
		}

		// Token: 0x020000B4 RID: 180
		public interface IHandler
		{
			// Token: 0x060006B4 RID: 1716
			void OnStartParsing(MiniParser parser);

			// Token: 0x060006B5 RID: 1717
			void OnStartElement(string name, MiniParser.IAttrList attrs);

			// Token: 0x060006B6 RID: 1718
			void OnEndElement(string name);

			// Token: 0x060006B7 RID: 1719
			void OnChars(string ch);

			// Token: 0x060006B8 RID: 1720
			void OnEndParsing(MiniParser parser);
		}

		// Token: 0x020000B5 RID: 181
		public class HandlerAdapter : MiniParser.IHandler
		{
			// Token: 0x060006BA RID: 1722 RVA: 0x0001FE0C File Offset: 0x0001E00C
			public void OnStartParsing(MiniParser parser)
			{
			}

			// Token: 0x060006BB RID: 1723 RVA: 0x0001FE0E File Offset: 0x0001E00E
			public void OnStartElement(string name, MiniParser.IAttrList attrs)
			{
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x0001FE10 File Offset: 0x0001E010
			public void OnEndElement(string name)
			{
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x0001FE12 File Offset: 0x0001E012
			public void OnChars(string ch)
			{
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x0001FE14 File Offset: 0x0001E014
			public void OnEndParsing(MiniParser parser)
			{
			}
		}

		// Token: 0x020000B6 RID: 182
		private enum CharKind : byte
		{
			// Token: 0x0400045F RID: 1119
			LEFT_BR,
			// Token: 0x04000460 RID: 1120
			RIGHT_BR,
			// Token: 0x04000461 RID: 1121
			SLASH,
			// Token: 0x04000462 RID: 1122
			PI_MARK,
			// Token: 0x04000463 RID: 1123
			EQ,
			// Token: 0x04000464 RID: 1124
			AMP,
			// Token: 0x04000465 RID: 1125
			SQUOTE,
			// Token: 0x04000466 RID: 1126
			DQUOTE,
			// Token: 0x04000467 RID: 1127
			BANG,
			// Token: 0x04000468 RID: 1128
			LEFT_SQBR,
			// Token: 0x04000469 RID: 1129
			SPACE,
			// Token: 0x0400046A RID: 1130
			RIGHT_SQBR,
			// Token: 0x0400046B RID: 1131
			TAB,
			// Token: 0x0400046C RID: 1132
			CR,
			// Token: 0x0400046D RID: 1133
			EOL,
			// Token: 0x0400046E RID: 1134
			CHARS,
			// Token: 0x0400046F RID: 1135
			UNKNOWN = 31
		}

		// Token: 0x020000B7 RID: 183
		private enum ActionCode : byte
		{
			// Token: 0x04000471 RID: 1137
			START_ELEM,
			// Token: 0x04000472 RID: 1138
			END_ELEM,
			// Token: 0x04000473 RID: 1139
			END_NAME,
			// Token: 0x04000474 RID: 1140
			SET_ATTR_NAME,
			// Token: 0x04000475 RID: 1141
			SET_ATTR_VAL,
			// Token: 0x04000476 RID: 1142
			SEND_CHARS,
			// Token: 0x04000477 RID: 1143
			START_CDATA,
			// Token: 0x04000478 RID: 1144
			END_CDATA,
			// Token: 0x04000479 RID: 1145
			ERROR,
			// Token: 0x0400047A RID: 1146
			STATE_CHANGE,
			// Token: 0x0400047B RID: 1147
			FLUSH_CHARS_STATE_CHANGE,
			// Token: 0x0400047C RID: 1148
			ACC_CHARS_STATE_CHANGE,
			// Token: 0x0400047D RID: 1149
			ACC_CDATA,
			// Token: 0x0400047E RID: 1150
			PROC_CHAR_REF,
			// Token: 0x0400047F RID: 1151
			UNKNOWN = 15
		}

		// Token: 0x020000B8 RID: 184
		public class AttrListImpl : MiniParser.IMutableAttrList, MiniParser.IAttrList
		{
			// Token: 0x060006BF RID: 1727 RVA: 0x0001FE16 File Offset: 0x0001E016
			public AttrListImpl()
				: this(0)
			{
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x0001FE1F File Offset: 0x0001E01F
			public AttrListImpl(int initialCapacity)
			{
				if (initialCapacity <= 0)
				{
					this.names = new ArrayList();
					this.values = new ArrayList();
					return;
				}
				this.names = new ArrayList(initialCapacity);
				this.values = new ArrayList(initialCapacity);
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x0001FE5A File Offset: 0x0001E05A
			public AttrListImpl(MiniParser.IAttrList attrs)
				: this((attrs != null) ? attrs.Length : 0)
			{
				if (attrs != null)
				{
					this.CopyFrom(attrs);
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001FE78 File Offset: 0x0001E078
			public int Length
			{
				get
				{
					return this.names.Count;
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001FE85 File Offset: 0x0001E085
			public bool IsEmpty
			{
				get
				{
					return this.Length != 0;
				}
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x0001FE90 File Offset: 0x0001E090
			public string GetName(int i)
			{
				string text = null;
				if (i >= 0 && i < this.Length)
				{
					text = this.names[i] as string;
				}
				return text;
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x0001FEC0 File Offset: 0x0001E0C0
			public string GetValue(int i)
			{
				string text = null;
				if (i >= 0 && i < this.Length)
				{
					text = this.values[i] as string;
				}
				return text;
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x0001FEEF File Offset: 0x0001E0EF
			public string GetValue(string name)
			{
				return this.GetValue(this.names.IndexOf(name));
			}

			// Token: 0x060006C7 RID: 1735 RVA: 0x0001FF04 File Offset: 0x0001E104
			public void ChangeValue(string name, string newValue)
			{
				int num = this.names.IndexOf(name);
				if (num >= 0 && num < this.Length)
				{
					this.values[num] = newValue;
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001FF38 File Offset: 0x0001E138
			public string[] Names
			{
				get
				{
					return this.names.ToArray(typeof(string)) as string[];
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001FF54 File Offset: 0x0001E154
			public string[] Values
			{
				get
				{
					return this.values.ToArray(typeof(string)) as string[];
				}
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x0001FF70 File Offset: 0x0001E170
			public void Clear()
			{
				this.names.Clear();
				this.values.Clear();
			}

			// Token: 0x060006CB RID: 1739 RVA: 0x0001FF88 File Offset: 0x0001E188
			public void Add(string name, string value)
			{
				this.names.Add(name);
				this.values.Add(value);
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x0001FFA4 File Offset: 0x0001E1A4
			public void Remove(int i)
			{
				if (i >= 0)
				{
					this.names.RemoveAt(i);
					this.values.RemoveAt(i);
				}
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x0001FFC2 File Offset: 0x0001E1C2
			public void Remove(string name)
			{
				this.Remove(this.names.IndexOf(name));
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x0001FFD8 File Offset: 0x0001E1D8
			public void CopyFrom(MiniParser.IAttrList attrs)
			{
				if (attrs != null && this == attrs)
				{
					this.Clear();
					int length = attrs.Length;
					for (int i = 0; i < length; i++)
					{
						this.Add(attrs.GetName(i), attrs.GetValue(i));
					}
				}
			}

			// Token: 0x04000480 RID: 1152
			protected ArrayList names;

			// Token: 0x04000481 RID: 1153
			protected ArrayList values;
		}

		// Token: 0x020000B9 RID: 185
		public class XMLError : Exception
		{
			// Token: 0x060006CF RID: 1743 RVA: 0x00020019 File Offset: 0x0001E219
			public XMLError()
				: this("Unknown")
			{
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x00020026 File Offset: 0x0001E226
			public XMLError(string descr)
				: this(descr, -1, -1)
			{
			}

			// Token: 0x060006D1 RID: 1745 RVA: 0x00020031 File Offset: 0x0001E231
			public XMLError(string descr, int line, int column)
				: base(descr)
			{
				this.descr = descr;
				this.line = line;
				this.column = column;
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0002004F File Offset: 0x0001E24F
			public int Line
			{
				get
				{
					return this.line;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00020057 File Offset: 0x0001E257
			public int Column
			{
				get
				{
					return this.column;
				}
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x0002005F File Offset: 0x0001E25F
			public override string ToString()
			{
				return string.Format("{0} @ (line = {1}, col = {2})", this.descr, this.line, this.column);
			}

			// Token: 0x04000482 RID: 1154
			protected string descr;

			// Token: 0x04000483 RID: 1155
			protected int line;

			// Token: 0x04000484 RID: 1156
			protected int column;
		}
	}
}
