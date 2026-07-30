using System;
using System.IO;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x02000614 RID: 1556
	internal sealed class Parser
	{
		// Token: 0x060043FF RID: 17407 RVA: 0x000EE6B0 File Offset: 0x000EC8B0
		internal SecurityElement GetTopElement()
		{
			return this._doc.GetRootElement();
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x000EE6C0 File Offset: 0x000EC8C0
		private void GetRequiredSizes(TokenizerStream stream, ref int index)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			int num = 1;
			SecurityElementType securityElementType = SecurityElementType.Regular;
			string text = null;
			bool flag5 = false;
			bool flag6 = false;
			int num2 = 0;
			for (;;)
			{
				short num3 = stream.GetNextToken();
				while (num3 != -1)
				{
					switch (num3 & 255)
					{
					case 0:
						flag4 = true;
						flag6 = false;
						num3 = stream.GetNextToken();
						if (num3 == 2)
						{
							stream.TagLastToken(17408);
							for (;;)
							{
								num3 = stream.GetNextToken();
								if (num3 != 3)
								{
									break;
								}
								stream.ThrowAwayNextString();
								stream.TagLastToken(20480);
							}
							if (num3 == -1)
							{
								goto Block_9;
							}
							if (num3 != 1)
							{
								goto Block_10;
							}
							flag4 = false;
							index++;
							flag6 = false;
							num--;
							flag = true;
							goto IL_03B9;
						}
						else if (num3 == 3)
						{
							flag3 = true;
							stream.TagLastToken(16640);
							index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
							if (securityElementType != SecurityElementType.Regular)
							{
								goto Block_12;
							}
							flag = true;
							num++;
							goto IL_03B9;
						}
						else
						{
							if (num3 == 6)
							{
								num2 = 1;
								do
								{
									num3 = stream.GetNextToken();
									switch (num3)
									{
									case 0:
										num2++;
										break;
									case 1:
										num2--;
										break;
									case 3:
										stream.ThrowAwayNextString();
										stream.TagLastToken(20480);
										break;
									}
								}
								while (num2 > 0);
								flag4 = false;
								flag6 = false;
								flag = true;
								goto IL_03B9;
							}
							if (num3 != 5)
							{
								goto IL_02B3;
							}
							num3 = stream.GetNextToken();
							if (num3 != 3)
							{
								goto Block_17;
							}
							flag3 = true;
							securityElementType = SecurityElementType.Format;
							stream.TagLastToken(16640);
							index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
							num2 = 1;
							num++;
							flag = true;
							goto IL_03B9;
						}
						break;
					case 1:
						if (flag4)
						{
							flag4 = false;
							goto IL_03C4;
						}
						goto IL_02E0;
					case 2:
						num3 = stream.GetNextToken();
						if (num3 == 1)
						{
							stream.TagLastToken(17408);
							index++;
							num--;
							flag6 = false;
							flag = true;
							goto IL_03B9;
						}
						goto IL_0329;
					case 3:
						if (flag4)
						{
							if (securityElementType == SecurityElementType.Comment)
							{
								stream.ThrowAwayNextString();
								stream.TagLastToken(20480);
								goto IL_03B9;
							}
							if (text == null)
							{
								text = stream.GetNextString();
								goto IL_03B9;
							}
							if (!flag5)
							{
								goto Block_5;
							}
							stream.TagLastToken(16896);
							index += SecurityDocument.EncodedStringSize(text) + SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
							text = null;
							flag5 = false;
							goto IL_03B9;
						}
						else
						{
							if (flag6)
							{
								stream.TagLastToken(25344);
								index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + SecurityDocument.EncodedStringSize(" ");
								goto IL_03B9;
							}
							stream.TagLastToken(17152);
							index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
							flag6 = true;
							goto IL_03B9;
						}
						break;
					case 4:
						flag5 = true;
						goto IL_03B9;
					case 5:
						if (!flag4 || securityElementType != SecurityElementType.Format || num2 != 1)
						{
							goto IL_0397;
						}
						num3 = stream.GetNextToken();
						if (num3 == 1)
						{
							stream.TagLastToken(17408);
							index++;
							num--;
							flag6 = false;
							flag = true;
							goto IL_03B9;
						}
						goto IL_037C;
					}
					goto Block_1;
					IL_03C4:
					num3 = stream.GetNextToken();
					continue;
					IL_03B9:
					if (flag)
					{
						flag = false;
						flag2 = false;
						break;
					}
					flag2 = true;
					goto IL_03C4;
				}
				if (flag2)
				{
					index++;
					num--;
					flag6 = false;
				}
				else if (num3 == -1 && (num != 1 || !flag3))
				{
					goto IL_03F5;
				}
				if (num <= 1)
				{
					return;
				}
			}
			Block_1:
			goto IL_03A8;
			Block_5:
			throw new XmlSyntaxException(this._t.LineNo);
			Block_9:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("Unexpected end of file."));
			Block_10:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("Expected > character."));
			Block_12:
			throw new XmlSyntaxException(this._t.LineNo);
			Block_17:
			throw new XmlSyntaxException(this._t.LineNo);
			IL_02B3:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("Expected / character or string."));
			IL_02E0:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("Unexpected > character."));
			IL_0329:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("Expected > character."));
			IL_037C:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("Expected > character."));
			IL_0397:
			throw new XmlSyntaxException(this._t.LineNo);
			IL_03A8:
			throw new XmlSyntaxException(this._t.LineNo);
			IL_03F5:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("Unexpected end of file."));
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x000EEAE8 File Offset: 0x000ECCE8
		private int DetermineFormat(TokenizerStream stream)
		{
			if (stream.GetNextToken() == 0 && stream.GetNextToken() == 5)
			{
				this._t.GetTokens(stream, -1, true);
				stream.GoToPosition(2);
				bool flag = false;
				bool flag2 = false;
				short num = stream.GetNextToken();
				while (num != -1 && num != 1)
				{
					if (num != 3)
					{
						if (num != 4)
						{
							throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("Unexpected end of file."));
						}
						flag = true;
					}
					else
					{
						if (flag && flag2)
						{
							this._t.ChangeFormat(Encoding.GetEncoding(stream.GetNextString()));
							return 0;
						}
						if (!flag)
						{
							if (string.Compare(stream.GetNextString(), "encoding", StringComparison.Ordinal) == 0)
							{
								flag2 = true;
							}
						}
						else
						{
							flag = false;
							flag2 = false;
							stream.ThrowAwayNextString();
						}
					}
					num = stream.GetNextToken();
				}
				return 0;
			}
			return 2;
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x000EEBAC File Offset: 0x000ECDAC
		private void ParseContents()
		{
			TokenizerStream tokenizerStream = new TokenizerStream();
			this._t.GetTokens(tokenizerStream, 2, false);
			tokenizerStream.Reset();
			int num = this.DetermineFormat(tokenizerStream);
			tokenizerStream.GoToPosition(num);
			this._t.GetTokens(tokenizerStream, -1, false);
			tokenizerStream.Reset();
			int num2 = 0;
			this.GetRequiredSizes(tokenizerStream, ref num2);
			this._doc = new SecurityDocument(num2);
			int num3 = 0;
			tokenizerStream.Reset();
			for (short num4 = tokenizerStream.GetNextFullToken(); num4 != -1; num4 = tokenizerStream.GetNextFullToken())
			{
				if ((num4 & 16384) == 16384)
				{
					short num5 = (short)((int)num4 & 65280);
					if (num5 <= 17152)
					{
						if (num5 == 16640)
						{
							this._doc.AddToken(1, ref num3);
							this._doc.AddString(tokenizerStream.GetNextString(), ref num3);
							goto IL_019D;
						}
						if (num5 == 16896)
						{
							this._doc.AddToken(2, ref num3);
							this._doc.AddString(tokenizerStream.GetNextString(), ref num3);
							this._doc.AddString(tokenizerStream.GetNextString(), ref num3);
							goto IL_019D;
						}
						if (num5 == 17152)
						{
							this._doc.AddToken(3, ref num3);
							this._doc.AddString(tokenizerStream.GetNextString(), ref num3);
							goto IL_019D;
						}
					}
					else
					{
						if (num5 == 17408)
						{
							this._doc.AddToken(4, ref num3);
							goto IL_019D;
						}
						if (num5 == 20480)
						{
							tokenizerStream.ThrowAwayNextString();
							goto IL_019D;
						}
						if (num5 == 25344)
						{
							this._doc.AppendString(" ", ref num3);
							this._doc.AppendString(tokenizerStream.GetNextString(), ref num3);
							goto IL_019D;
						}
					}
					throw new XmlSyntaxException();
				}
				IL_019D:;
			}
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x000EED64 File Offset: 0x000ECF64
		private Parser(Tokenizer t)
		{
			this._t = t;
			this._doc = null;
			try
			{
				this.ParseContents();
			}
			finally
			{
				this._t.Recycle();
			}
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000EEDAC File Offset: 0x000ECFAC
		internal Parser(string input)
			: this(new Tokenizer(input))
		{
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000EEDBA File Offset: 0x000ECFBA
		internal Parser(string input, string[] searchStrings, string[] replaceStrings)
			: this(new Tokenizer(input, searchStrings, replaceStrings))
		{
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000EEDCA File Offset: 0x000ECFCA
		internal Parser(byte[] array, Tokenizer.ByteTokenEncoding encoding)
			: this(new Tokenizer(array, encoding, 0))
		{
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x000EEDDA File Offset: 0x000ECFDA
		internal Parser(byte[] array, Tokenizer.ByteTokenEncoding encoding, int startIndex)
			: this(new Tokenizer(array, encoding, startIndex))
		{
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000EEDEA File Offset: 0x000ECFEA
		internal Parser(StreamReader input)
			: this(new Tokenizer(input))
		{
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000EEDF8 File Offset: 0x000ECFF8
		internal Parser(char[] array)
			: this(new Tokenizer(array))
		{
		}

		// Token: 0x04002217 RID: 8727
		private SecurityDocument _doc;

		// Token: 0x04002218 RID: 8728
		private Tokenizer _t;

		// Token: 0x04002219 RID: 8729
		private const short c_flag = 16384;

		// Token: 0x0400221A RID: 8730
		private const short c_elementtag = 16640;

		// Token: 0x0400221B RID: 8731
		private const short c_attributetag = 16896;

		// Token: 0x0400221C RID: 8732
		private const short c_texttag = 17152;

		// Token: 0x0400221D RID: 8733
		private const short c_additionaltexttag = 25344;

		// Token: 0x0400221E RID: 8734
		private const short c_childrentag = 17408;

		// Token: 0x0400221F RID: 8735
		private const short c_wastedstringtag = 20480;
	}
}
