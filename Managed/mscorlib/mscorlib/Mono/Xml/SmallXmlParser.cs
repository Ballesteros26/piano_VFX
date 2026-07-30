using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x02000027 RID: 39
	internal class SmallXmlParser
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000041F8 File Offset: 0x000023F8
		private Exception Error(string msg)
		{
			return new SmallXmlParserException(msg, this.line, this.column);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000420C File Offset: 0x0000240C
		private Exception UnexpectedEndError()
		{
			string[] array = new string[this.elementNames.Count];
			this.elementNames.CopyTo(array, 0);
			return this.Error(string.Format("Unexpected end of stream. Element stack content is {0}", string.Join(",", array)));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004254 File Offset: 0x00002454
		private bool IsNameChar(char c, bool start)
		{
			if (c <= '.')
			{
				if (c == '-' || c == '.')
				{
					return !start;
				}
			}
			else if (c == ':' || c == '_')
			{
				return true;
			}
			if (c > 'Ā')
			{
				if (c == 'ՙ' || c == 'ۥ' || c == 'ۦ')
				{
					return true;
				}
				if ('ʻ' <= c && c <= 'ˁ')
				{
					return true;
				}
			}
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				return true;
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.DecimalDigitNumber:
				return !start;
			default:
				return false;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000042F6 File Offset: 0x000024F6
		private bool IsWhitespace(int c)
		{
			return c - 9 <= 1 || c == 13 || c == 32;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000430C File Offset: 0x0000250C
		public void SkipWhitespaces()
		{
			this.SkipWhitespaces(false);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004315 File Offset: 0x00002515
		private void HandleWhitespaces()
		{
			while (this.IsWhitespace(this.Peek()))
			{
				this.buffer.Append((char)this.Read());
			}
			if (this.Peek() != 60 && this.Peek() >= 0)
			{
				this.isWhitespace = false;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004354 File Offset: 0x00002554
		public void SkipWhitespaces(bool expected)
		{
			for (;;)
			{
				int num = this.Peek();
				if (num - 9 > 1 && num != 13 && num != 32)
				{
					break;
				}
				this.Read();
				if (expected)
				{
					expected = false;
				}
			}
			if (expected)
			{
				throw this.Error("Whitespace is expected.");
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004397 File Offset: 0x00002597
		private int Peek()
		{
			return this.reader.Peek();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000043A4 File Offset: 0x000025A4
		private int Read()
		{
			int num = this.reader.Read();
			if (num == 10)
			{
				this.resetColumn = true;
			}
			if (this.resetColumn)
			{
				this.line++;
				this.resetColumn = false;
				this.column = 1;
				return num;
			}
			this.column++;
			return num;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000043FC File Offset: 0x000025FC
		public void Expect(int c)
		{
			int num = this.Read();
			if (num < 0)
			{
				throw this.UnexpectedEndError();
			}
			if (num != c)
			{
				throw this.Error(string.Format("Expected '{0}' but got {1}", (char)c, (char)num));
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004440 File Offset: 0x00002640
		private string ReadUntil(char until, bool handleReferences)
		{
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == until)
				{
					string text = this.buffer.ToString();
					this.buffer.Length = 0;
					return text;
				}
				if (handleReferences && c == '&')
				{
					this.ReadReference();
				}
				else
				{
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000044A0 File Offset: 0x000026A0
		public string ReadName()
		{
			int num = 0;
			if (this.Peek() < 0 || !this.IsNameChar((char)this.Peek(), true))
			{
				throw this.Error("XML name start character is expected.");
			}
			for (int i = this.Peek(); i >= 0; i = this.Peek())
			{
				char c = (char)i;
				if (!this.IsNameChar(c, false))
				{
					break;
				}
				if (num == this.nameBuffer.Length)
				{
					char[] array = new char[num * 2];
					Array.Copy(this.nameBuffer, array, num);
					this.nameBuffer = array;
				}
				this.nameBuffer[num++] = c;
				this.Read();
			}
			if (num == 0)
			{
				throw this.Error("Valid XML name is expected.");
			}
			return new string(this.nameBuffer, 0, num);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004550 File Offset: 0x00002750
		public void Parse(TextReader input, SmallXmlParser.IContentHandler handler)
		{
			this.reader = input;
			this.handler = handler;
			handler.OnStartParsing(this);
			while (this.Peek() >= 0)
			{
				this.ReadContent();
			}
			this.HandleBufferedContent();
			if (this.elementNames.Count > 0)
			{
				throw this.Error(string.Format("Insufficient close tag: {0}", this.elementNames.Peek()));
			}
			handler.OnEndParsing(this);
			this.Cleanup();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000045C0 File Offset: 0x000027C0
		private void Cleanup()
		{
			this.line = 1;
			this.column = 0;
			this.handler = null;
			this.reader = null;
			this.elementNames.Clear();
			this.xmlSpaces.Clear();
			this.attributes.Clear();
			this.buffer.Length = 0;
			this.xmlSpace = null;
			this.isWhitespace = false;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004624 File Offset: 0x00002824
		public void ReadContent()
		{
			if (this.IsWhitespace(this.Peek()))
			{
				if (this.buffer.Length == 0)
				{
					this.isWhitespace = true;
				}
				this.HandleWhitespaces();
			}
			if (this.Peek() != 60)
			{
				this.ReadCharacters();
				return;
			}
			this.Read();
			int num = this.Peek();
			if (num != 33)
			{
				if (num != 47)
				{
					string text;
					if (num != 63)
					{
						this.HandleBufferedContent();
						text = this.ReadName();
						while (this.Peek() != 62 && this.Peek() != 47)
						{
							this.ReadAttribute(this.attributes);
						}
						this.handler.OnStartElement(text, this.attributes);
						this.attributes.Clear();
						this.SkipWhitespaces();
						if (this.Peek() == 47)
						{
							this.Read();
							this.handler.OnEndElement(text);
						}
						else
						{
							this.elementNames.Push(text);
							this.xmlSpaces.Push(this.xmlSpace);
						}
						this.Expect(62);
						return;
					}
					this.HandleBufferedContent();
					this.Read();
					text = this.ReadName();
					this.SkipWhitespaces();
					string text2 = string.Empty;
					if (this.Peek() != 63)
					{
						for (;;)
						{
							text2 += this.ReadUntil('?', false);
							if (this.Peek() == 62)
							{
								break;
							}
							text2 += "?";
						}
					}
					this.handler.OnProcessingInstruction(text, text2);
					this.Expect(62);
					return;
				}
				else
				{
					this.HandleBufferedContent();
					if (this.elementNames.Count == 0)
					{
						throw this.UnexpectedEndError();
					}
					this.Read();
					string text = this.ReadName();
					this.SkipWhitespaces();
					string text3 = (string)this.elementNames.Pop();
					this.xmlSpaces.Pop();
					if (this.xmlSpaces.Count > 0)
					{
						this.xmlSpace = (string)this.xmlSpaces.Peek();
					}
					else
					{
						this.xmlSpace = null;
					}
					if (text != text3)
					{
						throw this.Error(string.Format("End tag mismatch: expected {0} but found {1}", text3, text));
					}
					this.handler.OnEndElement(text);
					this.Expect(62);
					return;
				}
			}
			else
			{
				this.Read();
				if (this.Peek() == 91)
				{
					this.Read();
					if (this.ReadName() != "CDATA")
					{
						throw this.Error("Invalid declaration markup");
					}
					this.Expect(91);
					this.ReadCDATASection();
					return;
				}
				else
				{
					if (this.Peek() == 45)
					{
						this.ReadComment();
						return;
					}
					if (this.ReadName() != "DOCTYPE")
					{
						throw this.Error("Invalid declaration markup.");
					}
					throw this.Error("This parser does not support document type.");
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000048BC File Offset: 0x00002ABC
		private void HandleBufferedContent()
		{
			if (this.buffer.Length == 0)
			{
				return;
			}
			if (this.isWhitespace)
			{
				this.handler.OnIgnorableWhitespace(this.buffer.ToString());
			}
			else
			{
				this.handler.OnChars(this.buffer.ToString());
			}
			this.buffer.Length = 0;
			this.isWhitespace = false;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004920 File Offset: 0x00002B20
		private void ReadCharacters()
		{
			this.isWhitespace = false;
			for (;;)
			{
				int num = this.Peek();
				if (num == -1)
				{
					break;
				}
				if (num != 38)
				{
					if (num == 60)
					{
						return;
					}
					this.buffer.Append((char)this.Read());
				}
				else
				{
					this.Read();
					this.ReadReference();
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004970 File Offset: 0x00002B70
		private void ReadReference()
		{
			if (this.Peek() == 35)
			{
				this.Read();
				this.ReadCharacterReference();
				return;
			}
			string text = this.ReadName();
			this.Expect(59);
			if (text == "amp")
			{
				this.buffer.Append('&');
				return;
			}
			if (text == "quot")
			{
				this.buffer.Append('"');
				return;
			}
			if (text == "apos")
			{
				this.buffer.Append('\'');
				return;
			}
			if (text == "lt")
			{
				this.buffer.Append('<');
				return;
			}
			if (!(text == "gt"))
			{
				throw this.Error("General non-predefined entity reference is not supported in this parser.");
			}
			this.buffer.Append('>');
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004A40 File Offset: 0x00002C40
		private int ReadCharacterReference()
		{
			int num = 0;
			if (this.Peek() == 120)
			{
				this.Read();
				for (int i = this.Peek(); i >= 0; i = this.Peek())
				{
					if (48 <= i && i <= 57)
					{
						num <<= 4 + i - 48;
					}
					else if (65 <= i && i <= 70)
					{
						num <<= 4 + i - 65 + 10;
					}
					else
					{
						if (97 > i || i > 102)
						{
							break;
						}
						num <<= 4 + i - 97 + 10;
					}
					this.Read();
				}
			}
			else
			{
				int num2 = this.Peek();
				while (num2 >= 0 && 48 <= num2 && num2 <= 57)
				{
					num <<= 4 + num2 - 48;
					this.Read();
					num2 = this.Peek();
				}
			}
			return num;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004AFC File Offset: 0x00002CFC
		private void ReadAttribute(SmallXmlParser.AttrListImpl a)
		{
			this.SkipWhitespaces(true);
			if (this.Peek() == 47 || this.Peek() == 62)
			{
				return;
			}
			string text = this.ReadName();
			this.SkipWhitespaces();
			this.Expect(61);
			this.SkipWhitespaces();
			int num = this.Read();
			string text2;
			if (num != 34)
			{
				if (num != 39)
				{
					throw this.Error("Invalid attribute value markup.");
				}
				text2 = this.ReadUntil('\'', true);
			}
			else
			{
				text2 = this.ReadUntil('"', true);
			}
			if (text == "xml:space")
			{
				this.xmlSpace = text2;
			}
			a.Add(text, text2);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004B94 File Offset: 0x00002D94
		private void ReadCDATASection()
		{
			int num = 0;
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == ']')
				{
					num++;
				}
				else
				{
					if (c == '>' && num > 1)
					{
						for (int i = num; i > 2; i--)
						{
							this.buffer.Append(']');
						}
						return;
					}
					for (int j = 0; j < num; j++)
					{
						this.buffer.Append(']');
					}
					num = 0;
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004C14 File Offset: 0x00002E14
		private void ReadComment()
		{
			this.Expect(45);
			this.Expect(45);
			while (this.Read() != 45 || this.Read() != 45)
			{
			}
			if (this.Read() != 62)
			{
				throw this.Error("'--' is not allowed inside comment markup.");
			}
		}

		// Token: 0x040003BA RID: 954
		private SmallXmlParser.IContentHandler handler;

		// Token: 0x040003BB RID: 955
		private TextReader reader;

		// Token: 0x040003BC RID: 956
		private Stack elementNames = new Stack();

		// Token: 0x040003BD RID: 957
		private Stack xmlSpaces = new Stack();

		// Token: 0x040003BE RID: 958
		private string xmlSpace;

		// Token: 0x040003BF RID: 959
		private StringBuilder buffer = new StringBuilder(200);

		// Token: 0x040003C0 RID: 960
		private char[] nameBuffer = new char[30];

		// Token: 0x040003C1 RID: 961
		private bool isWhitespace;

		// Token: 0x040003C2 RID: 962
		private SmallXmlParser.AttrListImpl attributes = new SmallXmlParser.AttrListImpl();

		// Token: 0x040003C3 RID: 963
		private int line = 1;

		// Token: 0x040003C4 RID: 964
		private int column;

		// Token: 0x040003C5 RID: 965
		private bool resetColumn;

		// Token: 0x02000028 RID: 40
		public interface IContentHandler
		{
			// Token: 0x060000D4 RID: 212
			void OnStartParsing(SmallXmlParser parser);

			// Token: 0x060000D5 RID: 213
			void OnEndParsing(SmallXmlParser parser);

			// Token: 0x060000D6 RID: 214
			void OnStartElement(string name, SmallXmlParser.IAttrList attrs);

			// Token: 0x060000D7 RID: 215
			void OnEndElement(string name);

			// Token: 0x060000D8 RID: 216
			void OnProcessingInstruction(string name, string text);

			// Token: 0x060000D9 RID: 217
			void OnChars(string text);

			// Token: 0x060000DA RID: 218
			void OnIgnorableWhitespace(string text);
		}

		// Token: 0x02000029 RID: 41
		public interface IAttrList
		{
			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000DB RID: 219
			int Length { get; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000DC RID: 220
			bool IsEmpty { get; }

			// Token: 0x060000DD RID: 221
			string GetName(int i);

			// Token: 0x060000DE RID: 222
			string GetValue(int i);

			// Token: 0x060000DF RID: 223
			string GetValue(string name);

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000E0 RID: 224
			string[] Names { get; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000E1 RID: 225
			string[] Values { get; }
		}

		// Token: 0x0200002A RID: 42
		private class AttrListImpl : SmallXmlParser.IAttrList
		{
			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004C50 File Offset: 0x00002E50
			public int Length
			{
				get
				{
					return this.attrNames.Count;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004C5D File Offset: 0x00002E5D
			public bool IsEmpty
			{
				get
				{
					return this.attrNames.Count == 0;
				}
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x00004C6D File Offset: 0x00002E6D
			public string GetName(int i)
			{
				return this.attrNames[i];
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x00004C7B File Offset: 0x00002E7B
			public string GetValue(int i)
			{
				return this.attrValues[i];
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00004C8C File Offset: 0x00002E8C
			public string GetValue(string name)
			{
				for (int i = 0; i < this.attrNames.Count; i++)
				{
					if (this.attrNames[i] == name)
					{
						return this.attrValues[i];
					}
				}
				return null;
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004CD1 File Offset: 0x00002ED1
			public string[] Names
			{
				get
				{
					return this.attrNames.ToArray();
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004CDE File Offset: 0x00002EDE
			public string[] Values
			{
				get
				{
					return this.attrValues.ToArray();
				}
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x00004CEB File Offset: 0x00002EEB
			internal void Clear()
			{
				this.attrNames.Clear();
				this.attrValues.Clear();
			}

			// Token: 0x060000EA RID: 234 RVA: 0x00004D03 File Offset: 0x00002F03
			internal void Add(string name, string value)
			{
				this.attrNames.Add(name);
				this.attrValues.Add(value);
			}

			// Token: 0x040003C6 RID: 966
			private List<string> attrNames = new List<string>();

			// Token: 0x040003C7 RID: 967
			private List<string> attrValues = new List<string>();
		}
	}
}
