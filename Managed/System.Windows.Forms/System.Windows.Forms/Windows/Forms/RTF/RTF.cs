using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200002B RID: 43
	internal class RTF
	{
		// Token: 0x0600013C RID: 316 RVA: 0x0000B88C File Offset: 0x00009A8C
		public RTF(Stream stream)
		{
			this.source = new StreamReader(stream);
			this.text_buffer = new StringBuilder(1024);
			this.rtf_class = TokenClass.None;
			this.pushed_class = TokenClass.None;
			this.pushed_char = char.MaxValue;
			this.line_num = 0;
			this.line_pos = 0;
			this.prev_char = char.MaxValue;
			this.bump_line = false;
			this.font_list = null;
			this.charset_stack = null;
			this.cur_charset = new Charset();
			this.destination_callbacks = new DestinationCallback();
			this.class_callbacks = new ClassCallback();
			this.destination_callbacks[Minor.OptDest] = new DestinationDelegate(this.HandleOptDest);
			this.destination_callbacks[Minor.FontTbl] = new DestinationDelegate(this.ReadFontTbl);
			this.destination_callbacks[Minor.ColorTbl] = new DestinationDelegate(this.ReadColorTbl);
			this.destination_callbacks[Minor.StyleSheet] = new DestinationDelegate(this.ReadStyleSheet);
			this.destination_callbacks[Minor.Info] = new DestinationDelegate(this.ReadInfoGroup);
			this.destination_callbacks[Minor.Pict] = new DestinationDelegate(this.ReadPictGroup);
			this.destination_callbacks[Minor.Object] = new DestinationDelegate(this.ReadObjGroup);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		static RTF()
		{
			for (int i = 0; i < RTF.Keys.Length; i++)
			{
				RTF.key_table[RTF.Keys[i].Symbol] = RTF.Keys[i];
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000BA50 File Offset: 0x00009C50
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000BA58 File Offset: 0x00009C58
		public TokenClass TokenClass
		{
			get
			{
				return this.rtf_class;
			}
			set
			{
				this.rtf_class = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000BA64 File Offset: 0x00009C64
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000BA6C File Offset: 0x00009C6C
		public Major Major
		{
			get
			{
				return this.major;
			}
			set
			{
				this.major = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000BA78 File Offset: 0x00009C78
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000BA80 File Offset: 0x00009C80
		public Minor Minor
		{
			get
			{
				return this.minor;
			}
			set
			{
				this.minor = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000BA8C File Offset: 0x00009C8C
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000BA94 File Offset: 0x00009C94
		public int Param
		{
			get
			{
				return this.param;
			}
			set
			{
				this.param = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		public string Text
		{
			get
			{
				return this.text_buffer.ToString();
			}
			set
			{
				if (value == null)
				{
					this.text_buffer.Length = 0;
				}
				else
				{
					this.text_buffer = new StringBuilder(value);
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public string EncodedText
		{
			get
			{
				return this.encoded_text;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		// (set) Token: 0x0600014A RID: 330 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		public Picture Picture
		{
			get
			{
				return this.picture;
			}
			set
			{
				this.picture = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000BAFC File Offset: 0x00009CFC
		public Color Colors
		{
			get
			{
				return this.colors;
			}
			set
			{
				this.colors = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000BB08 File Offset: 0x00009D08
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000BB10 File Offset: 0x00009D10
		public Style Styles
		{
			get
			{
				return this.styles;
			}
			set
			{
				this.styles = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000BB1C File Offset: 0x00009D1C
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000BB24 File Offset: 0x00009D24
		public Font Fonts
		{
			get
			{
				return this.fonts;
			}
			set
			{
				this.fonts = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000BB30 File Offset: 0x00009D30
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000BB38 File Offset: 0x00009D38
		public ClassCallback ClassCallback
		{
			get
			{
				return this.class_callbacks;
			}
			set
			{
				this.class_callbacks = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000BB44 File Offset: 0x00009D44
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public DestinationCallback DestinationCallback
		{
			get
			{
				return this.destination_callbacks;
			}
			set
			{
				this.destination_callbacks = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000BB58 File Offset: 0x00009D58
		public int LineNumber
		{
			get
			{
				return this.line_num;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000BB60 File Offset: 0x00009D60
		public int LinePos
		{
			get
			{
				return this.line_pos;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000BB68 File Offset: 0x00009D68
		public void DefaultFont(string name)
		{
			Font font = new Font(this);
			font.Num = 0;
			font.Name = name;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000BB8C File Offset: 0x00009D8C
		private char GetChar()
		{
			return this.GetChar(true);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000BB98 File Offset: 0x00009D98
		private char GetChar(bool skipCrLf)
		{
			int num;
			bool flag;
			for (;;)
			{
				if ((num = this.source.Read()) != -1)
				{
					this.text_buffer.Append((char)num);
				}
				if (this.prev_char == '\uffff')
				{
					this.bump_line = true;
				}
				flag = this.bump_line;
				this.bump_line = false;
				if (!skipCrLf)
				{
					break;
				}
				if (num == 13)
				{
					this.bump_line = true;
					this.text_buffer.Length--;
				}
				else
				{
					if (num != 10)
					{
						break;
					}
					this.bump_line = true;
					if (this.prev_char == '\r')
					{
					}
					this.text_buffer.Length--;
				}
			}
			this.line_pos++;
			if (flag)
			{
				this.line_num++;
				this.line_pos = 1;
			}
			this.prev_char = (char)num;
			return (char)num;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000BC84 File Offset: 0x00009E84
		public void Read()
		{
			while (this.GetToken() != TokenClass.EOF)
			{
				this.RouteToken();
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		public void RouteToken()
		{
			if (this.CheckCM(TokenClass.Control, Major.Destination))
			{
				DestinationDelegate destinationDelegate = this.destination_callbacks[this.minor];
				if (destinationDelegate != null)
				{
					destinationDelegate(this);
				}
			}
			ClassDelegate classDelegate = this.class_callbacks[this.rtf_class];
			if (classDelegate != null)
			{
				classDelegate(this);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public void SkipGroup()
		{
			int num = 1;
			while (this.GetToken() != TokenClass.EOF)
			{
				if (this.rtf_class == TokenClass.Group)
				{
					if (this.major == Major.BeginGroup)
					{
						num++;
					}
					else if (this.major == Major.EndGroup)
					{
						num--;
						if (num < 1)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000BD54 File Offset: 0x00009F54
		public TokenClass GetToken()
		{
			if (this.pushed_class != TokenClass.None)
			{
				this.rtf_class = this.pushed_class;
				this.major = this.pushed_major;
				this.minor = this.pushed_minor;
				this.param = this.pushed_param;
				this.pushed_class = TokenClass.None;
				return this.rtf_class;
			}
			this.GetToken2();
			if (this.rtf_class == TokenClass.Text)
			{
				this.minor = (Minor)this.cur_charset[(int)this.major];
				if (this.encoding == null)
				{
					this.encoding = Encoding.GetEncoding(this.encoding_code_page);
				}
				this.encoded_text = new string(this.encoding.GetChars(new byte[] { (byte)this.major }));
			}
			if (this.cur_charset.Flags == CharsetFlags.None)
			{
				return this.rtf_class;
			}
			if (this.CheckCMM(TokenClass.Control, Major.Unicode, Minor.UnicodeAnsiCodepage))
			{
				this.encoding_code_page = this.param;
				if (this.encoding_code_page < 0 || this.encoding_code_page > 65535)
				{
					this.encoding_code_page = 1252;
				}
			}
			if ((this.cur_charset.Flags & CharsetFlags.Read) != CharsetFlags.None && this.CheckCM(TokenClass.Control, Major.CharSet))
			{
				this.cur_charset.ReadMap();
			}
			else if ((this.cur_charset.Flags & CharsetFlags.Switch) != CharsetFlags.None && this.CheckCMM(TokenClass.Control, Major.CharAttr, Minor.FontNum))
			{
				Font font = Font.GetFont(this.font_list, this.param);
				if (font != null)
				{
					if (font.Name.StartsWith("Symbol"))
					{
						this.cur_charset.ID = CharsetType.Symbol;
					}
					else
					{
						this.cur_charset.ID = CharsetType.General;
					}
				}
				else if ((this.cur_charset.Flags & CharsetFlags.Switch) != CharsetFlags.None && this.rtf_class == TokenClass.Group)
				{
					Major major = this.major;
					if (major != Major.BeginGroup)
					{
						if (major == Major.EndGroup)
						{
							this.cur_charset = (Charset)this.charset_stack.Pop();
						}
					}
					else
					{
						this.charset_stack.Push(this.cur_charset);
					}
				}
			}
			return this.rtf_class;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000BF88 File Offset: 0x0000A188
		private void GetToken2()
		{
			this.rtf_class = TokenClass.Unknown;
			this.param = -1000000;
			this.text_buffer.Length = 0;
			char c;
			if (this.pushed_char != '\uffff')
			{
				c = this.pushed_char;
				this.text_buffer.Append(c);
				this.pushed_char = char.MaxValue;
			}
			else if ((c = this.GetChar()) == '\uffff')
			{
				this.rtf_class = TokenClass.EOF;
				return;
			}
			if (c == '{')
			{
				this.rtf_class = TokenClass.Group;
				this.major = Major.BeginGroup;
				return;
			}
			if (c == '}')
			{
				this.rtf_class = TokenClass.Group;
				this.major = Major.EndGroup;
				return;
			}
			if (c != '\\')
			{
				if (c != '\t')
				{
					this.rtf_class = TokenClass.Text;
					this.major = (Major)c;
					return;
				}
				this.rtf_class = TokenClass.Control;
				this.major = Major.SpecialChar;
				this.minor = Minor.Tab;
				return;
			}
			else
			{
				if ((c = this.GetChar()) == '\uffff')
				{
					return;
				}
				if (char.IsLetter(c))
				{
					while (char.IsLetter(c))
					{
						if ((c = this.GetChar(false)) == '\uffff')
						{
							break;
						}
					}
					if (c != '\uffff')
					{
						this.text_buffer.Length--;
					}
					this.Lookup(this.text_buffer.ToString());
					if (c != '\uffff')
					{
						this.text_buffer.Append(c);
					}
					int num = 1;
					if (c == '-')
					{
						num = -1;
						c = this.GetChar();
					}
					if (c != '\uffff' && char.IsDigit(c) && this.minor != Minor.PngBlip)
					{
						this.param = 0;
						while (char.IsDigit(c))
						{
							this.param = this.param * 10 + (int)Convert.ToByte(c) - 48;
							if ((c = this.GetChar()) == '\uffff')
							{
								break;
							}
						}
						this.param *= num;
					}
					if (c != '\uffff')
					{
						if (c != ' ' && c != '\r' && c != '\n')
						{
							this.pushed_char = c;
						}
						this.text_buffer.Length--;
					}
					return;
				}
				if (c == '\'')
				{
					if ((c = this.GetChar()) == '\uffff')
					{
						return;
					}
					char @char;
					if ((@char = this.GetChar()) == '\uffff')
					{
						return;
					}
					this.rtf_class = TokenClass.Text;
					this.major = (Major)((ushort)(Convert.ToByte(c.ToString(), 16) * 16 + Convert.ToByte(@char.ToString(), 16)));
					return;
				}
				else
				{
					if (c == ':' || c == '{' || c == '}' || c == '\\')
					{
						this.rtf_class = TokenClass.Text;
						this.major = (Major)c;
						return;
					}
					this.Lookup(this.text_buffer.ToString());
					return;
				}
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000C260 File Offset: 0x0000A460
		public void SetToken(TokenClass cl, Major maj, Minor min, int par, string text)
		{
			this.rtf_class = cl;
			this.major = maj;
			this.minor = min;
			this.param = par;
			if (par == -1000000)
			{
				this.text_buffer = new StringBuilder(text);
			}
			else
			{
				this.text_buffer = new StringBuilder(text + par.ToString());
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
		public void UngetToken()
		{
			if (this.pushed_class != TokenClass.None)
			{
				throw new RTFException(this, "Cannot unget more than one token");
			}
			if (this.rtf_class == TokenClass.None)
			{
				throw new RTFException(this, "No token to unget");
			}
			this.pushed_class = this.rtf_class;
			this.pushed_major = this.major;
			this.pushed_minor = this.minor;
			this.pushed_param = this.param;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000C334 File Offset: 0x0000A534
		public TokenClass PeekToken()
		{
			this.GetToken();
			this.UngetToken();
			return this.rtf_class;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000C34C File Offset: 0x0000A54C
		public void Lookup(string token)
		{
			object obj = RTF.key_table[token.Substring(1)];
			if (obj == null)
			{
				this.rtf_class = TokenClass.Unknown;
				this.major = this.Major - 1;
				this.minor = this.Minor - 1;
				return;
			}
			KeyStruct keyStruct = (KeyStruct)obj;
			this.rtf_class = TokenClass.Control;
			this.major = keyStruct.Major;
			this.minor = keyStruct.Minor;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		public bool CheckCM(TokenClass rtf_class, Major major)
		{
			return this.rtf_class == rtf_class && this.major == major;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		public bool CheckCMM(TokenClass rtf_class, Major major, Minor minor)
		{
			return this.rtf_class == rtf_class && this.major == major && this.minor == minor;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000C40C File Offset: 0x0000A60C
		public bool CheckMM(Major major, Minor minor)
		{
			return this.major == major && this.minor == minor;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C42C File Offset: 0x0000A62C
		private void HandleOptDest(RTF rtf)
		{
			int num = 1;
			for (;;)
			{
				this.GetToken();
				if (rtf.CheckCMM(TokenClass.Control, Major.Destination, Minor.Pict))
				{
					break;
				}
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup) && --num == 0)
				{
					return;
				}
				if (rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
				{
					num++;
				}
			}
			this.ReadPictGroup(rtf);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000C48C File Offset: 0x0000A68C
		private void ReadFontTbl(RTF rtf)
		{
			int num = -1;
			Font font = null;
			for (;;)
			{
				rtf.GetToken();
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					break;
				}
				if (num < 0)
				{
					if (rtf.CheckCMM(TokenClass.Control, Major.CharAttr, Minor.FontNum))
					{
						num = 1;
					}
					else
					{
						if (!rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
						{
							goto IL_0052;
						}
						num = 0;
					}
				}
				if (num == 0)
				{
					if (!rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
					{
						goto Block_6;
					}
					rtf.GetToken();
				}
				font = new Font(rtf);
				while (rtf.rtf_class != TokenClass.EOF && !rtf.CheckCM(TokenClass.Text, (Major)59) && !rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					if (rtf.rtf_class == TokenClass.Control)
					{
						Major major = rtf.major;
						if (major != Major.FontFamily)
						{
							if (major != Major.CharAttr)
							{
								if (major == Major.FontAttr)
								{
									switch (rtf.minor)
									{
									case Minor.FontCharSet:
										font.Charset = (CharsetType)rtf.param;
										break;
									case Minor.FontPitch:
										font.Pitch = rtf.param;
										break;
									case Minor.FontCodePage:
										font.Codepage = rtf.param;
										break;
									case Minor.FTypeNil:
									case Minor.FTypeTrueType:
										font.Type = rtf.param;
										break;
									}
								}
							}
							else
							{
								Minor minor = rtf.minor;
								if (minor == Minor.FontNum)
								{
									font.Num = rtf.param;
								}
							}
						}
						else
						{
							font.Family = (int)rtf.minor;
						}
					}
					else if (rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
					{
						rtf.SkipGroup();
					}
					else if (rtf.rtf_class == TokenClass.Text)
					{
						StringBuilder stringBuilder = new StringBuilder();
						while (rtf.rtf_class != TokenClass.EOF && !rtf.CheckCM(TokenClass.Text, (Major)59) && !rtf.CheckCM(TokenClass.Group, Major.EndGroup) && !rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
						{
							stringBuilder.Append((char)rtf.major);
							rtf.GetToken();
						}
						if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
						{
							rtf.UngetToken();
						}
						font.Name = stringBuilder.ToString();
						continue;
					}
					rtf.GetToken();
				}
				if (num == 0)
				{
					rtf.GetToken();
					if (!rtf.CheckCM(TokenClass.Group, Major.EndGroup))
					{
						goto Block_22;
					}
				}
			}
			if (font == null)
			{
				throw new RTFException(rtf, "No font created");
			}
			if (font.Num == -1)
			{
				throw new RTFException(rtf, "Missing font number");
			}
			rtf.RouteToken();
			return;
			IL_0052:
			throw new RTFException(rtf, "Cannot determine format");
			Block_6:
			throw new RTFException(rtf, "missing \"{\"");
			Block_22:
			throw new RTFException(rtf, "Missing \"}\"");
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000C748 File Offset: 0x0000A948
		private void ReadColorTbl(RTF rtf)
		{
			int num = 0;
			for (;;)
			{
				rtf.GetToken();
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					break;
				}
				Color color = new Color(rtf);
				color.Num = num++;
				while (rtf.CheckCM(TokenClass.Control, Major.ColorName))
				{
					switch (rtf.minor)
					{
					case Minor.Red:
						color.Red = rtf.param;
						break;
					case Minor.Green:
						color.Green = rtf.param;
						break;
					case Minor.Blue:
						color.Blue = rtf.param;
						break;
					}
					rtf.GetToken();
				}
				if (!rtf.CheckCM(TokenClass.Text, (Major)59))
				{
					goto Block_4;
				}
			}
			rtf.RouteToken();
			return;
			Block_4:
			throw new RTFException(rtf, "Malformed color entry");
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000C814 File Offset: 0x0000AA14
		private void ReadStyleSheet(RTF rtf)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				rtf.GetToken();
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					break;
				}
				Style style = new Style(rtf);
				if (!rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
				{
					goto Block_2;
				}
				for (;;)
				{
					rtf.GetToken();
					if (rtf.rtf_class == TokenClass.EOF || rtf.CheckCM(TokenClass.Text, (Major)59))
					{
						break;
					}
					if (rtf.rtf_class == TokenClass.Control)
					{
						if (rtf.CheckMM(Major.ParAttr, Minor.StyleNum))
						{
							style.Num = rtf.param;
							style.Type = StyleType.Paragraph;
						}
						else if (rtf.CheckMM(Major.CharAttr, Minor.CharStyleNum))
						{
							style.Num = rtf.param;
							style.Type = StyleType.Character;
						}
						else if (rtf.CheckMM(Major.StyleAttr, Minor.SectStyleNum))
						{
							style.Num = rtf.param;
							style.Type = StyleType.Section;
						}
						else if (rtf.CheckMM(Major.StyleAttr, Minor.BasedOn))
						{
							style.BasedOn = rtf.param;
						}
						else if (rtf.CheckMM(Major.StyleAttr, Minor.Additive))
						{
							style.Additive = true;
						}
						else if (rtf.CheckMM(Major.StyleAttr, Minor.Next))
						{
							style.NextPar = rtf.param;
						}
						else
						{
							new StyleElement(style, rtf.rtf_class, rtf.major, rtf.minor, rtf.param, rtf.text_buffer.ToString());
						}
					}
					else if (rtf.CheckCM(TokenClass.Group, Major.BeginGroup))
					{
						rtf.SkipGroup();
					}
					else if (rtf.rtf_class == TokenClass.Text)
					{
						while (rtf.rtf_class == TokenClass.Text)
						{
							if (rtf.major == (Major)59)
							{
								rtf.UngetToken();
								break;
							}
							stringBuilder.Append((char)rtf.major);
							rtf.GetToken();
						}
						style.Name = stringBuilder.ToString();
					}
				}
				rtf.GetToken();
				if (!rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					goto Block_14;
				}
				if (style.Name == null)
				{
					goto Block_15;
				}
				if (style.Num < 0)
				{
					if (!stringBuilder.ToString().StartsWith("Normal") && !stringBuilder.ToString().StartsWith("Standard"))
					{
						goto Block_18;
					}
					style.Num = 0;
				}
				if (style.NextPar == -1)
				{
					style.NextPar = style.Num;
				}
			}
			rtf.RouteToken();
			return;
			Block_2:
			throw new RTFException(rtf, "Missing \"{\"");
			Block_14:
			throw new RTFException(rtf, "Missing EndGroup (\"}\"");
			Block_15:
			throw new RTFException(rtf, "Style must have name");
			Block_18:
			throw new RTFException(rtf, "Missing style number");
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000CABC File Offset: 0x0000ACBC
		private void ReadInfoGroup(RTF rtf)
		{
			rtf.SkipGroup();
			rtf.RouteToken();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000CACC File Offset: 0x0000ACCC
		private void ReadPictGroup(RTF rtf)
		{
			bool flag = false;
			Picture picture = new Picture();
			for (;;)
			{
				rtf.GetToken();
				if (rtf.CheckCM(TokenClass.Group, Major.EndGroup))
				{
					break;
				}
				switch (this.minor)
				{
				case Minor.WinMetafile:
					picture.ImageType = this.minor;
					flag = true;
					continue;
				case Minor.PngBlip:
					picture.ImageType = this.minor;
					flag = true;
					break;
				case Minor.PicWid:
					continue;
				case Minor.PicHt:
					continue;
				case Minor.PicGoalWid:
					picture.SetWidthFromTwips(this.param);
					continue;
				case Minor.PicGoalHt:
					picture.SetHeightFromTwips(this.param);
					continue;
				}
				if (flag && rtf.rtf_class == TokenClass.Text)
				{
					goto Block_4;
				}
			}
			goto IL_02AA;
			Block_4:
			picture.Data.Seek(0L, 0);
			char c = (char)rtf.major;
			for (;;)
			{
				while (c == '\n' || c == '\r')
				{
					c = (char)this.source.Peek();
					if (c == '}')
					{
						break;
					}
					c = (char)this.source.Read();
				}
				char c2 = (char)this.source.Peek();
				if (c2 == '}')
				{
					break;
				}
				c2 = (char)this.source.Read();
				while (c2 == '\n' || c2 == '\r')
				{
					c2 = (char)this.source.Peek();
					if (c2 == '}')
					{
						break;
					}
					c2 = (char)this.source.Read();
				}
				uint num;
				if (char.IsDigit(c))
				{
					num = (uint)(c - '0');
				}
				else if (char.IsLower(c))
				{
					num = (uint)(c - 'a' + '\n');
				}
				else if (char.IsUpper(c))
				{
					num = (uint)(c - 'A' + '\n');
				}
				else
				{
					if (c == '\n' || c == '\r')
					{
						continue;
					}
					break;
				}
				uint num2;
				if (char.IsDigit(c2))
				{
					num2 = (uint)(c2 - '0');
				}
				else if (char.IsLower(c2))
				{
					num2 = (uint)(c2 - 'a' + '\n');
				}
				else if (char.IsUpper(c2))
				{
					num2 = (uint)(c2 - 'A' + '\n');
				}
				else
				{
					if (c2 == '\n' || c2 == '\r')
					{
						continue;
					}
					break;
				}
				picture.Data.WriteByte((byte)(checked(num * 16U + num2)));
				c = (char)this.source.Peek();
				if (c == '}')
				{
					break;
				}
				c = (char)this.source.Read();
			}
			flag = false;
			IL_02AA:
			if (picture.ImageType != Minor.Undefined && !flag)
			{
				this.picture = picture;
				this.SetToken(TokenClass.Control, Major.PictAttr, picture.ImageType, 0, string.Empty);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		private void ReadObjGroup(RTF rtf)
		{
			rtf.SkipGroup();
			rtf.RouteToken();
		}

		// Token: 0x0400034C RID: 844
		internal const char EOF = '\uffff';

		// Token: 0x0400034D RID: 845
		internal const int NoParam = -1000000;

		// Token: 0x0400034E RID: 846
		internal const int DefaultEncodingCodePage = 1252;

		// Token: 0x0400034F RID: 847
		private TokenClass rtf_class;

		// Token: 0x04000350 RID: 848
		private Major major;

		// Token: 0x04000351 RID: 849
		private Minor minor;

		// Token: 0x04000352 RID: 850
		private int param;

		// Token: 0x04000353 RID: 851
		private string encoded_text;

		// Token: 0x04000354 RID: 852
		private Encoding encoding;

		// Token: 0x04000355 RID: 853
		private int encoding_code_page = 1252;

		// Token: 0x04000356 RID: 854
		private StringBuilder text_buffer;

		// Token: 0x04000357 RID: 855
		private Picture picture;

		// Token: 0x04000358 RID: 856
		private int line_num;

		// Token: 0x04000359 RID: 857
		private int line_pos;

		// Token: 0x0400035A RID: 858
		private char pushed_char;

		// Token: 0x0400035B RID: 859
		private TokenClass pushed_class;

		// Token: 0x0400035C RID: 860
		private Major pushed_major;

		// Token: 0x0400035D RID: 861
		private Minor pushed_minor;

		// Token: 0x0400035E RID: 862
		private int pushed_param;

		// Token: 0x0400035F RID: 863
		private char prev_char;

		// Token: 0x04000360 RID: 864
		private bool bump_line;

		// Token: 0x04000361 RID: 865
		private Font font_list;

		// Token: 0x04000362 RID: 866
		private Charset cur_charset;

		// Token: 0x04000363 RID: 867
		private Stack charset_stack;

		// Token: 0x04000364 RID: 868
		private Style styles;

		// Token: 0x04000365 RID: 869
		private Color colors;

		// Token: 0x04000366 RID: 870
		private Font fonts;

		// Token: 0x04000367 RID: 871
		private StreamReader source;

		// Token: 0x04000368 RID: 872
		private static Hashtable key_table = new Hashtable(RTF.Keys.Length);

		// Token: 0x04000369 RID: 873
		private static KeyStruct[] Keys = KeysInit.Init();

		// Token: 0x0400036A RID: 874
		private DestinationCallback destination_callbacks;

		// Token: 0x0400036B RID: 875
		private ClassCallback class_callbacks;
	}
}
