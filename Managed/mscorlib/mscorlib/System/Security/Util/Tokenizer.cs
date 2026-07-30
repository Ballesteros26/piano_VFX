using System;
using System.IO;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x02000615 RID: 1557
	internal sealed class Tokenizer
	{
		// Token: 0x0600440A RID: 17418 RVA: 0x000EEE08 File Offset: 0x000ED008
		internal void BasicInitialization()
		{
			this.LineNo = 1;
			this._inProcessingTag = 0;
			this._inSavedCharacter = -1;
			this._inIndex = 0;
			this._inSize = 0;
			this._inNestedSize = 0;
			this._inNestedIndex = 0;
			this._inTokenSource = Tokenizer.TokenSource.Other;
			this._maker = SharedStatics.GetSharedStringMaker();
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x000EEE58 File Offset: 0x000ED058
		public void Recycle()
		{
			SharedStatics.ReleaseSharedStringMaker(ref this._maker);
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x000EEE65 File Offset: 0x000ED065
		internal Tokenizer(string input)
		{
			this.BasicInitialization();
			this._inString = input;
			this._inSize = input.Length;
			this._inTokenSource = Tokenizer.TokenSource.String;
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000EEE8D File Offset: 0x000ED08D
		internal Tokenizer(string input, string[] searchStrings, string[] replaceStrings)
		{
			this.BasicInitialization();
			this._inString = input;
			this._inSize = this._inString.Length;
			this._inTokenSource = Tokenizer.TokenSource.NestedStrings;
			this._searchStrings = searchStrings;
			this._replaceStrings = replaceStrings;
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x000EEEC8 File Offset: 0x000ED0C8
		internal Tokenizer(byte[] array, Tokenizer.ByteTokenEncoding encoding, int startIndex)
		{
			this.BasicInitialization();
			this._inBytes = array;
			this._inSize = array.Length;
			this._inIndex = startIndex;
			switch (encoding)
			{
			case Tokenizer.ByteTokenEncoding.UnicodeTokens:
				this._inTokenSource = Tokenizer.TokenSource.UnicodeByteArray;
				return;
			case Tokenizer.ByteTokenEncoding.UTF8Tokens:
				this._inTokenSource = Tokenizer.TokenSource.UTF8ByteArray;
				return;
			case Tokenizer.ByteTokenEncoding.ByteTokens:
				this._inTokenSource = Tokenizer.TokenSource.ASCIIByteArray;
				return;
			default:
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { (int)encoding }));
			}
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x000EEF42 File Offset: 0x000ED142
		internal Tokenizer(char[] array)
		{
			this.BasicInitialization();
			this._inChars = array;
			this._inSize = array.Length;
			this._inTokenSource = Tokenizer.TokenSource.CharArray;
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x000EEF67 File Offset: 0x000ED167
		internal Tokenizer(StreamReader input)
		{
			this.BasicInitialization();
			this._inTokenReader = new Tokenizer.StreamTokenReader(input);
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x000EEF84 File Offset: 0x000ED184
		internal void ChangeFormat(Encoding encoding)
		{
			if (encoding == null)
			{
				return;
			}
			Tokenizer.TokenSource tokenSource = this._inTokenSource;
			if (tokenSource > Tokenizer.TokenSource.ASCIIByteArray)
			{
				if (tokenSource - Tokenizer.TokenSource.CharArray <= 2)
				{
					return;
				}
			}
			else
			{
				if (encoding == Encoding.Unicode)
				{
					this._inTokenSource = Tokenizer.TokenSource.UnicodeByteArray;
					return;
				}
				if (encoding == Encoding.UTF8)
				{
					this._inTokenSource = Tokenizer.TokenSource.UTF8ByteArray;
					return;
				}
				if (encoding == Encoding.ASCII)
				{
					this._inTokenSource = Tokenizer.TokenSource.ASCIIByteArray;
					return;
				}
			}
			tokenSource = this._inTokenSource;
			Stream stream;
			if (tokenSource > Tokenizer.TokenSource.ASCIIByteArray)
			{
				if (tokenSource - Tokenizer.TokenSource.CharArray <= 2)
				{
					return;
				}
				Tokenizer.StreamTokenReader streamTokenReader = this._inTokenReader as Tokenizer.StreamTokenReader;
				if (streamTokenReader == null)
				{
					return;
				}
				stream = streamTokenReader._in.BaseStream;
				string text = new string(' ', streamTokenReader.NumCharEncountered);
				stream.Position = (long)streamTokenReader._in.CurrentEncoding.GetByteCount(text);
			}
			else
			{
				stream = new MemoryStream(this._inBytes, this._inIndex, this._inSize - this._inIndex);
			}
			this._inTokenReader = new Tokenizer.StreamTokenReader(new StreamReader(stream, encoding));
			this._inTokenSource = Tokenizer.TokenSource.Other;
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x000EF06C File Offset: 0x000ED26C
		internal void GetTokens(TokenizerStream stream, int maxNum, bool endAfterKet)
		{
			while (maxNum == -1 || stream.GetTokenCount() < maxNum)
			{
				int num = 0;
				bool flag = false;
				bool flag2 = false;
				Tokenizer.StringMaker maker = this._maker;
				maker._outStringBuilder = null;
				maker._outIndex = 0;
				int num2;
				for (;;)
				{
					if (this._inSavedCharacter != -1)
					{
						num2 = this._inSavedCharacter;
						this._inSavedCharacter = -1;
					}
					else
					{
						switch (this._inTokenSource)
						{
						case Tokenizer.TokenSource.UnicodeByteArray:
							if (this._inIndex + 1 >= this._inSize)
							{
								goto Block_3;
							}
							num2 = ((int)this._inBytes[this._inIndex + 1] << 8) + (int)this._inBytes[this._inIndex];
							this._inIndex += 2;
							break;
						case Tokenizer.TokenSource.UTF8ByteArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_4;
							}
							byte[] inBytes = this._inBytes;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inBytes[num3];
							if ((num2 & 128) != 0)
							{
								switch ((num2 & 240) >> 4)
								{
								case 8:
								case 9:
								case 10:
								case 11:
									goto IL_012D;
								case 12:
								case 13:
									num2 &= 31;
									num = 2;
									break;
								case 14:
									num2 &= 15;
									num = 3;
									break;
								case 15:
									goto IL_014B;
								}
								if (this._inIndex >= this._inSize)
								{
									goto Block_7;
								}
								byte[] inBytes2 = this._inBytes;
								num3 = this._inIndex;
								this._inIndex = num3 + 1;
								byte b = inBytes2[num3];
								if ((b & 192) != 128)
								{
									goto Block_8;
								}
								num2 = (num2 << 6) | (int)(b & 63);
								if (num != 2)
								{
									if (this._inIndex >= this._inSize)
									{
										goto Block_10;
									}
									byte[] inBytes3 = this._inBytes;
									num3 = this._inIndex;
									this._inIndex = num3 + 1;
									b = inBytes3[num3];
									if ((b & 192) != 128)
									{
										goto Block_11;
									}
									num2 = (num2 << 6) | (int)(b & 63);
								}
							}
							break;
						}
						case Tokenizer.TokenSource.ASCIIByteArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_12;
							}
							byte[] inBytes4 = this._inBytes;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inBytes4[num3];
							break;
						}
						case Tokenizer.TokenSource.CharArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_13;
							}
							char[] inChars = this._inChars;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inChars[num3];
							break;
						}
						case Tokenizer.TokenSource.String:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_14;
							}
							string inString = this._inString;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = (int)inString[num3];
							break;
						}
						case Tokenizer.TokenSource.NestedStrings:
						{
							int num3;
							if (this._inNestedSize != 0)
							{
								if (this._inNestedIndex < this._inNestedSize)
								{
									string inNestedString = this._inNestedString;
									num3 = this._inNestedIndex;
									this._inNestedIndex = num3 + 1;
									num2 = (int)inNestedString[num3];
									break;
								}
								this._inNestedSize = 0;
							}
							if (this._inIndex >= this._inSize)
							{
								goto Block_17;
							}
							string inString2 = this._inString;
							num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = (int)inString2[num3];
							if (num2 == 123)
							{
								for (int i = 0; i < this._searchStrings.Length; i++)
								{
									if (string.Compare(this._searchStrings[i], 0, this._inString, this._inIndex - 1, this._searchStrings[i].Length, StringComparison.Ordinal) == 0)
									{
										this._inNestedString = this._replaceStrings[i];
										this._inNestedSize = this._inNestedString.Length;
										this._inNestedIndex = 1;
										num2 = (int)this._inNestedString[0];
										this._inIndex += this._searchStrings[i].Length - 1;
										break;
									}
								}
							}
							break;
						}
						default:
							num2 = this._inTokenReader.Read();
							if (num2 == -1)
							{
								goto Block_21;
							}
							break;
						}
					}
					if (!flag)
					{
						if (num2 <= 34)
						{
							switch (num2)
							{
							case 9:
							case 13:
								continue;
							case 10:
								this.LineNo++;
								continue;
							case 11:
							case 12:
								break;
							default:
								switch (num2)
								{
								case 32:
									continue;
								case 33:
									if (this._inProcessingTag != 0)
									{
										goto Block_32;
									}
									break;
								case 34:
									flag = true;
									flag2 = true;
									continue;
								}
								break;
							}
						}
						else if (num2 != 45)
						{
							if (num2 != 47)
							{
								switch (num2)
								{
								case 60:
									goto IL_048A;
								case 61:
									goto IL_04C0;
								case 62:
									goto IL_04A4;
								case 63:
									if (this._inProcessingTag != 0)
									{
										goto Block_31;
									}
									break;
								}
							}
							else if (this._inProcessingTag != 0)
							{
								goto Block_30;
							}
						}
						else if (this._inProcessingTag != 0)
						{
							goto Block_33;
						}
					}
					else if (num2 <= 34)
					{
						switch (num2)
						{
						case 9:
						case 13:
							break;
						case 10:
							this.LineNo++;
							if (!flag2)
							{
								goto Block_46;
							}
							goto IL_062F;
						case 11:
						case 12:
							goto IL_062F;
						default:
							if (num2 != 32)
							{
								if (num2 != 34)
								{
									goto IL_062F;
								}
								if (flag2)
								{
									goto Block_44;
								}
								goto IL_062F;
							}
							break;
						}
						if (!flag2)
						{
							goto Block_45;
						}
					}
					else
					{
						if (num2 != 47)
						{
							if (num2 != 60)
							{
								if (num2 - 61 > 1)
								{
									goto IL_062F;
								}
							}
							else
							{
								if (!flag2)
								{
									goto Block_41;
								}
								goto IL_062F;
							}
						}
						if (!flag2 && this._inProcessingTag != 0)
						{
							goto Block_43;
						}
					}
					IL_062F:
					flag = true;
					if (maker._outIndex < 512)
					{
						char[] outChars = maker._outChars;
						Tokenizer.StringMaker stringMaker = maker;
						int num3 = stringMaker._outIndex;
						stringMaker._outIndex = num3 + 1;
						outChars[num3] = (ushort)num2;
					}
					else
					{
						if (maker._outStringBuilder == null)
						{
							maker._outStringBuilder = new StringBuilder();
						}
						maker._outStringBuilder.Append(maker._outChars, 0, 512);
						maker._outChars[0] = (char)num2;
						maker._outIndex = 1;
					}
				}
				IL_048A:
				this._inProcessingTag++;
				stream.AddToken(0);
				continue;
				Block_3:
				stream.AddToken(-1);
				return;
				IL_04A4:
				this._inProcessingTag--;
				stream.AddToken(1);
				if (endAfterKet)
				{
					return;
				}
				continue;
				IL_04C0:
				stream.AddToken(4);
				continue;
				Block_30:
				stream.AddToken(2);
				continue;
				Block_31:
				stream.AddToken(5);
				continue;
				Block_32:
				stream.AddToken(6);
				continue;
				Block_33:
				stream.AddToken(7);
				continue;
				Block_41:
				this._inSavedCharacter = num2;
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_43:
				this._inSavedCharacter = num2;
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_44:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_45:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_46:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_4:
				stream.AddToken(-1);
				return;
				IL_012D:
				throw new XmlSyntaxException(this.LineNo);
				IL_014B:
				throw new XmlSyntaxException(this.LineNo);
				Block_7:
				throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("Unexpected end of file."));
				Block_8:
				throw new XmlSyntaxException(this.LineNo);
				Block_10:
				throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("Unexpected end of file."));
				Block_11:
				throw new XmlSyntaxException(this.LineNo);
				Block_12:
				stream.AddToken(-1);
				return;
				Block_13:
				stream.AddToken(-1);
				return;
				Block_14:
				stream.AddToken(-1);
				return;
				Block_17:
				stream.AddToken(-1);
				return;
				Block_21:
				stream.AddToken(-1);
				return;
			}
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x000EF736 File Offset: 0x000ED936
		private string GetStringToken()
		{
			return this._maker.MakeString();
		}

		// Token: 0x04002220 RID: 8736
		internal const byte bra = 0;

		// Token: 0x04002221 RID: 8737
		internal const byte ket = 1;

		// Token: 0x04002222 RID: 8738
		internal const byte slash = 2;

		// Token: 0x04002223 RID: 8739
		internal const byte cstr = 3;

		// Token: 0x04002224 RID: 8740
		internal const byte equals = 4;

		// Token: 0x04002225 RID: 8741
		internal const byte quest = 5;

		// Token: 0x04002226 RID: 8742
		internal const byte bang = 6;

		// Token: 0x04002227 RID: 8743
		internal const byte dash = 7;

		// Token: 0x04002228 RID: 8744
		internal const int intOpenBracket = 60;

		// Token: 0x04002229 RID: 8745
		internal const int intCloseBracket = 62;

		// Token: 0x0400222A RID: 8746
		internal const int intSlash = 47;

		// Token: 0x0400222B RID: 8747
		internal const int intEquals = 61;

		// Token: 0x0400222C RID: 8748
		internal const int intQuote = 34;

		// Token: 0x0400222D RID: 8749
		internal const int intQuest = 63;

		// Token: 0x0400222E RID: 8750
		internal const int intBang = 33;

		// Token: 0x0400222F RID: 8751
		internal const int intDash = 45;

		// Token: 0x04002230 RID: 8752
		internal const int intTab = 9;

		// Token: 0x04002231 RID: 8753
		internal const int intCR = 13;

		// Token: 0x04002232 RID: 8754
		internal const int intLF = 10;

		// Token: 0x04002233 RID: 8755
		internal const int intSpace = 32;

		// Token: 0x04002234 RID: 8756
		public int LineNo;

		// Token: 0x04002235 RID: 8757
		private int _inProcessingTag;

		// Token: 0x04002236 RID: 8758
		private byte[] _inBytes;

		// Token: 0x04002237 RID: 8759
		private char[] _inChars;

		// Token: 0x04002238 RID: 8760
		private string _inString;

		// Token: 0x04002239 RID: 8761
		private int _inIndex;

		// Token: 0x0400223A RID: 8762
		private int _inSize;

		// Token: 0x0400223B RID: 8763
		private int _inSavedCharacter;

		// Token: 0x0400223C RID: 8764
		private Tokenizer.TokenSource _inTokenSource;

		// Token: 0x0400223D RID: 8765
		private Tokenizer.ITokenReader _inTokenReader;

		// Token: 0x0400223E RID: 8766
		private Tokenizer.StringMaker _maker;

		// Token: 0x0400223F RID: 8767
		private string[] _searchStrings;

		// Token: 0x04002240 RID: 8768
		private string[] _replaceStrings;

		// Token: 0x04002241 RID: 8769
		private int _inNestedIndex;

		// Token: 0x04002242 RID: 8770
		private int _inNestedSize;

		// Token: 0x04002243 RID: 8771
		private string _inNestedString;

		// Token: 0x02000616 RID: 1558
		private enum TokenSource
		{
			// Token: 0x04002245 RID: 8773
			UnicodeByteArray,
			// Token: 0x04002246 RID: 8774
			UTF8ByteArray,
			// Token: 0x04002247 RID: 8775
			ASCIIByteArray,
			// Token: 0x04002248 RID: 8776
			CharArray,
			// Token: 0x04002249 RID: 8777
			String,
			// Token: 0x0400224A RID: 8778
			NestedStrings,
			// Token: 0x0400224B RID: 8779
			Other
		}

		// Token: 0x02000617 RID: 1559
		internal enum ByteTokenEncoding
		{
			// Token: 0x0400224D RID: 8781
			UnicodeTokens,
			// Token: 0x0400224E RID: 8782
			UTF8Tokens,
			// Token: 0x0400224F RID: 8783
			ByteTokens
		}

		// Token: 0x02000618 RID: 1560
		[Serializable]
		internal sealed class StringMaker
		{
			// Token: 0x06004414 RID: 17428 RVA: 0x000EF744 File Offset: 0x000ED944
			private static uint HashString(string str)
			{
				uint num = 0U;
				int length = str.Length;
				for (int i = 0; i < length; i++)
				{
					num = (num << 3) ^ (uint)str[i] ^ (num >> 29);
				}
				return num;
			}

			// Token: 0x06004415 RID: 17429 RVA: 0x000EF778 File Offset: 0x000ED978
			private static uint HashCharArray(char[] a, int l)
			{
				uint num = 0U;
				for (int i = 0; i < l; i++)
				{
					num = (num << 3) ^ (uint)a[i] ^ (num >> 29);
				}
				return num;
			}

			// Token: 0x06004416 RID: 17430 RVA: 0x000EF7A1 File Offset: 0x000ED9A1
			public StringMaker()
			{
				this.cStringsMax = 2048U;
				this.cStringsUsed = 0U;
				this.aStrings = new string[this.cStringsMax];
				this._outChars = new char[512];
			}

			// Token: 0x06004417 RID: 17431 RVA: 0x000EF7DC File Offset: 0x000ED9DC
			private bool CompareStringAndChars(string str, char[] a, int l)
			{
				if (str.Length != l)
				{
					return false;
				}
				for (int i = 0; i < l; i++)
				{
					if (a[i] != str[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06004418 RID: 17432 RVA: 0x000EF810 File Offset: 0x000EDA10
			public string MakeString()
			{
				char[] outChars = this._outChars;
				int outIndex = this._outIndex;
				if (this._outStringBuilder != null)
				{
					this._outStringBuilder.Append(this._outChars, 0, this._outIndex);
					return this._outStringBuilder.ToString();
				}
				uint num3;
				if (this.cStringsUsed > this.cStringsMax / 4U * 3U)
				{
					uint num = this.cStringsMax * 2U;
					string[] array = new string[num];
					int num2 = 0;
					while ((long)num2 < (long)((ulong)this.cStringsMax))
					{
						if (this.aStrings[num2] != null)
						{
							num3 = Tokenizer.StringMaker.HashString(this.aStrings[num2]) % num;
							while (array[(int)num3] != null)
							{
								if ((num3 += 1U) >= num)
								{
									num3 = 0U;
								}
							}
							array[(int)num3] = this.aStrings[num2];
						}
						num2++;
					}
					this.cStringsMax = num;
					this.aStrings = array;
				}
				num3 = Tokenizer.StringMaker.HashCharArray(outChars, outIndex) % this.cStringsMax;
				string text;
				while ((text = this.aStrings[(int)num3]) != null)
				{
					if (this.CompareStringAndChars(text, outChars, outIndex))
					{
						return text;
					}
					if ((num3 += 1U) >= this.cStringsMax)
					{
						num3 = 0U;
					}
				}
				text = new string(outChars, 0, outIndex);
				this.aStrings[(int)num3] = text;
				this.cStringsUsed += 1U;
				return text;
			}

			// Token: 0x04002250 RID: 8784
			private string[] aStrings;

			// Token: 0x04002251 RID: 8785
			private uint cStringsMax;

			// Token: 0x04002252 RID: 8786
			private uint cStringsUsed;

			// Token: 0x04002253 RID: 8787
			public StringBuilder _outStringBuilder;

			// Token: 0x04002254 RID: 8788
			public char[] _outChars;

			// Token: 0x04002255 RID: 8789
			public int _outIndex;

			// Token: 0x04002256 RID: 8790
			public const int outMaxSize = 512;
		}

		// Token: 0x02000619 RID: 1561
		internal interface ITokenReader
		{
			// Token: 0x06004419 RID: 17433
			int Read();
		}

		// Token: 0x0200061A RID: 1562
		internal class StreamTokenReader : Tokenizer.ITokenReader
		{
			// Token: 0x0600441A RID: 17434 RVA: 0x000EF93B File Offset: 0x000EDB3B
			internal StreamTokenReader(StreamReader input)
			{
				this._in = input;
				this._numCharRead = 0;
			}

			// Token: 0x0600441B RID: 17435 RVA: 0x000EF951 File Offset: 0x000EDB51
			public virtual int Read()
			{
				int num = this._in.Read();
				if (num != -1)
				{
					this._numCharRead++;
				}
				return num;
			}

			// Token: 0x17000B61 RID: 2913
			// (get) Token: 0x0600441C RID: 17436 RVA: 0x000EF970 File Offset: 0x000EDB70
			internal int NumCharEncountered
			{
				get
				{
					return this._numCharRead;
				}
			}

			// Token: 0x04002257 RID: 8791
			internal StreamReader _in;

			// Token: 0x04002258 RID: 8792
			internal int _numCharRead;
		}
	}
}
