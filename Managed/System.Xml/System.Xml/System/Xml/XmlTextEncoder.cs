using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200011B RID: 283
	internal class XmlTextEncoder
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x00031276 File Offset: 0x0002F476
		internal XmlTextEncoder(TextWriter textWriter)
		{
			this.textWriter = textWriter;
			this.quoteChar = '"';
			this.xmlCharType = XmlCharType.Instance;
		}

		// Token: 0x170001D5 RID: 469
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00031298 File Offset: 0x0002F498
		internal char QuoteChar
		{
			set
			{
				this.quoteChar = value;
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000312A1 File Offset: 0x0002F4A1
		internal void StartAttribute(bool cacheAttrValue)
		{
			this.inAttribute = true;
			this.cacheAttrValue = cacheAttrValue;
			if (cacheAttrValue)
			{
				if (this.attrValue == null)
				{
					this.attrValue = new StringBuilder();
					return;
				}
				this.attrValue.Length = 0;
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000312D4 File Offset: 0x0002F4D4
		internal void EndAttribute()
		{
			if (this.cacheAttrValue)
			{
				this.attrValue.Length = 0;
			}
			this.inAttribute = false;
			this.cacheAttrValue = false;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x000312F8 File Offset: 0x0002F4F8
		internal string AttributeValue
		{
			get
			{
				if (this.cacheAttrValue)
				{
					return this.attrValue.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00031313 File Offset: 0x0002F513
		internal void WriteSurrogateChar(char lowChar, char highChar)
		{
			if (!XmlCharType.IsLowSurrogate((int)lowChar) || !XmlCharType.IsHighSurrogate((int)highChar))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
			}
			this.textWriter.Write(highChar);
			this.textWriter.Write(lowChar);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00031348 File Offset: 0x0002F548
		internal void Write(char[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (0 > offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (0 > count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > array.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(array, offset, count);
			}
			int num = offset + count;
			int num2 = offset;
			char c = '\0';
			for (;;)
			{
				int num3 = num2;
				while (num2 < num && (this.xmlCharType.charProperties[(int)(c = array[num2])] & 128) != 0)
				{
					num2++;
				}
				if (num3 < num2)
				{
					this.textWriter.Write(array, num3, num2 - num3);
				}
				if (num2 == num)
				{
					return;
				}
				if (c <= '&')
				{
					switch (c)
					{
					case '\t':
						this.textWriter.Write(c);
						break;
					case '\n':
					case '\r':
						if (this.inAttribute)
						{
							this.WriteCharEntityImpl(c);
						}
						else
						{
							this.textWriter.Write(c);
						}
						break;
					case '\v':
					case '\f':
						goto IL_01A9;
					default:
						if (c != '"')
						{
							if (c != '&')
							{
								goto IL_01A9;
							}
							this.WriteEntityRefImpl("amp");
						}
						else if (this.inAttribute && this.quoteChar == c)
						{
							this.WriteEntityRefImpl("quot");
						}
						else
						{
							this.textWriter.Write('"');
						}
						break;
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_01A9;
						}
						this.WriteEntityRefImpl("gt");
					}
					else
					{
						this.WriteEntityRefImpl("lt");
					}
				}
				else if (this.inAttribute && this.quoteChar == c)
				{
					this.WriteEntityRefImpl("apos");
				}
				else
				{
					this.textWriter.Write('\'');
				}
				IL_01ED:
				num2++;
				continue;
				IL_01A9:
				if (XmlCharType.IsHighSurrogate((int)c))
				{
					if (num2 + 1 < num)
					{
						this.WriteSurrogateChar(array[++num2], c);
						goto IL_01ED;
					}
					break;
				}
				else
				{
					if (XmlCharType.IsLowSurrogate((int)c))
					{
						goto Block_23;
					}
					this.WriteCharEntityImpl(c);
					goto IL_01ED;
				}
			}
			throw new ArgumentException(Res.GetString("The second character surrogate pair is not in the input buffer to be written."));
			Block_23:
			throw XmlConvert.CreateInvalidHighSurrogateCharException(c);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0003154C File Offset: 0x0002F74C
		internal void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			if (!XmlCharType.IsLowSurrogate((int)lowChar) || !XmlCharType.IsHighSurrogate((int)highChar))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
			}
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(highChar);
				this.attrValue.Append(lowChar);
			}
			this.textWriter.Write("&#x");
			this.textWriter.Write(num.ToString("X", NumberFormatInfo.InvariantInfo));
			this.textWriter.Write(';');
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000315D4 File Offset: 0x0002F7D4
		internal void Write(string text)
		{
			if (text == null)
			{
				return;
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(text);
			}
			int length = text.Length;
			int i = 0;
			int num = 0;
			char c = '\0';
			for (;;)
			{
				if (i >= length || (this.xmlCharType.charProperties[(int)(c = text[i])] & 128) == 0)
				{
					if (i == length)
					{
						break;
					}
					if (this.inAttribute)
					{
						if (c != '\t')
						{
							goto IL_0090;
						}
						i++;
					}
					else
					{
						if (c != '\t' && c != '\n' && c != '\r' && c != '"' && c != '\'')
						{
							goto IL_0090;
						}
						i++;
					}
				}
				else
				{
					i++;
				}
			}
			this.textWriter.Write(text);
			return;
			IL_0090:
			char[] array = new char[256];
			for (;;)
			{
				if (num < i)
				{
					this.WriteStringFragment(text, num, i - num, array);
				}
				if (i == length)
				{
					return;
				}
				if (c <= '&')
				{
					switch (c)
					{
					case '\t':
						this.textWriter.Write(c);
						break;
					case '\n':
					case '\r':
						if (this.inAttribute)
						{
							this.WriteCharEntityImpl(c);
						}
						else
						{
							this.textWriter.Write(c);
						}
						break;
					case '\v':
					case '\f':
						goto IL_01BF;
					default:
						if (c != '"')
						{
							if (c != '&')
							{
								goto IL_01BF;
							}
							this.WriteEntityRefImpl("amp");
						}
						else if (this.inAttribute && this.quoteChar == c)
						{
							this.WriteEntityRefImpl("quot");
						}
						else
						{
							this.textWriter.Write('"');
						}
						break;
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_01BF;
						}
						this.WriteEntityRefImpl("gt");
					}
					else
					{
						this.WriteEntityRefImpl("lt");
					}
				}
				else if (this.inAttribute && this.quoteChar == c)
				{
					this.WriteEntityRefImpl("apos");
				}
				else
				{
					this.textWriter.Write('\'');
				}
				IL_0205:
				i++;
				num = i;
				while (i < length)
				{
					if ((this.xmlCharType.charProperties[(int)(c = text[i])] & 128) == 0)
					{
						break;
					}
					i++;
				}
				continue;
				IL_01BF:
				if (XmlCharType.IsHighSurrogate((int)c))
				{
					if (i + 1 < length)
					{
						this.WriteSurrogateChar(text[++i], c);
						goto IL_0205;
					}
					break;
				}
				else
				{
					if (XmlCharType.IsLowSurrogate((int)c))
					{
						goto Block_27;
					}
					this.WriteCharEntityImpl(c);
					goto IL_0205;
				}
			}
			throw XmlConvert.CreateInvalidSurrogatePairException(text[i], c);
			Block_27:
			throw XmlConvert.CreateInvalidHighSurrogateCharException(c);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0003181C File Offset: 0x0002FA1C
		internal void WriteRawWithSurrogateChecking(string text)
		{
			if (text == null)
			{
				return;
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(text);
			}
			int length = text.Length;
			int num = 0;
			char c = '\0';
			char c2;
			for (;;)
			{
				if (num >= length || ((this.xmlCharType.charProperties[(int)(c = text[num])] & 16) == 0 && c >= ' '))
				{
					if (num == length)
					{
						goto IL_00A4;
					}
					if (XmlCharType.IsHighSurrogate((int)c))
					{
						if (num + 1 >= length)
						{
							goto IL_007F;
						}
						c2 = text[num + 1];
						if (!XmlCharType.IsLowSurrogate((int)c2))
						{
							break;
						}
						num += 2;
					}
					else
					{
						if (XmlCharType.IsLowSurrogate((int)c))
						{
							goto Block_9;
						}
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			throw XmlConvert.CreateInvalidSurrogatePairException(c2, c);
			IL_007F:
			throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
			Block_9:
			throw XmlConvert.CreateInvalidHighSurrogateCharException(c);
			IL_00A4:
			this.textWriter.Write(text);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000318D9 File Offset: 0x0002FAD9
		internal void WriteRaw(string value)
		{
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(value);
			}
			this.textWriter.Write(value);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000318FC File Offset: 0x0002FAFC
		internal void WriteRaw(char[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (0 > count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (0 > offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count > array.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.cacheAttrValue)
			{
				this.attrValue.Append(array, offset, count);
			}
			this.textWriter.Write(array, offset, count);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00031970 File Offset: 0x0002FB70
		internal void WriteCharEntity(char ch)
		{
			if (XmlCharType.IsSurrogate((int)ch))
			{
				throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
			}
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.cacheAttrValue)
			{
				this.attrValue.Append("&#x");
				this.attrValue.Append(text);
				this.attrValue.Append(';');
			}
			this.WriteCharEntityImpl(text);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000319E4 File Offset: 0x0002FBE4
		internal void WriteEntityRef(string name)
		{
			if (this.cacheAttrValue)
			{
				this.attrValue.Append('&');
				this.attrValue.Append(name);
				this.attrValue.Append(';');
			}
			this.WriteEntityRefImpl(name);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00002F50 File Offset: 0x00001150
		internal void Flush()
		{
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00031A20 File Offset: 0x0002FC20
		private void WriteStringFragment(string str, int offset, int count, char[] helperBuffer)
		{
			int num = helperBuffer.Length;
			while (count > 0)
			{
				int num2 = count;
				if (num2 > num)
				{
					num2 = num;
				}
				str.CopyTo(offset, helperBuffer, 0, num2);
				this.textWriter.Write(helperBuffer, 0, num2);
				offset += num2;
				count -= num2;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00031A64 File Offset: 0x0002FC64
		private void WriteCharEntityImpl(char ch)
		{
			int num = (int)ch;
			this.WriteCharEntityImpl(num.ToString("X", NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00031A8A File Offset: 0x0002FC8A
		private void WriteCharEntityImpl(string strVal)
		{
			this.textWriter.Write("&#x");
			this.textWriter.Write(strVal);
			this.textWriter.Write(';');
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00031AB5 File Offset: 0x0002FCB5
		private void WriteEntityRefImpl(string name)
		{
			this.textWriter.Write('&');
			this.textWriter.Write(name);
			this.textWriter.Write(';');
		}

		// Token: 0x04000640 RID: 1600
		private TextWriter textWriter;

		// Token: 0x04000641 RID: 1601
		private bool inAttribute;

		// Token: 0x04000642 RID: 1602
		private char quoteChar;

		// Token: 0x04000643 RID: 1603
		private StringBuilder attrValue;

		// Token: 0x04000644 RID: 1604
		private bool cacheAttrValue;

		// Token: 0x04000645 RID: 1605
		private XmlCharType xmlCharType;
	}
}
