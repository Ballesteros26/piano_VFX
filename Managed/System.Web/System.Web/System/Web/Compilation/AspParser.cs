using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200062D RID: 1581
	internal class AspParser : ILocation
	{
		// Token: 0x1400010D RID: 269
		// (add) Token: 0x060043A9 RID: 17321 RVA: 0x000B619E File Offset: 0x000B439E
		// (remove) Token: 0x060043AA RID: 17322 RVA: 0x000B61B1 File Offset: 0x000B43B1
		public event ParseErrorHandler Error
		{
			add
			{
				this.events.AddHandler(AspParser.errorEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(AspParser.errorEvent, value);
			}
		}

		// Token: 0x1400010E RID: 270
		// (add) Token: 0x060043AB RID: 17323 RVA: 0x000B61C4 File Offset: 0x000B43C4
		// (remove) Token: 0x060043AC RID: 17324 RVA: 0x000B61D7 File Offset: 0x000B43D7
		public event TagParsedHandler TagParsed
		{
			add
			{
				this.events.AddHandler(AspParser.tagParsedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(AspParser.tagParsedEvent, value);
			}
		}

		// Token: 0x1400010F RID: 271
		// (add) Token: 0x060043AD RID: 17325 RVA: 0x000B61EA File Offset: 0x000B43EA
		// (remove) Token: 0x060043AE RID: 17326 RVA: 0x000B61FD File Offset: 0x000B43FD
		public event TextParsedHandler TextParsed
		{
			add
			{
				this.events.AddHandler(AspParser.textParsedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(AspParser.textParsedEvent, value);
			}
		}

		// Token: 0x14000110 RID: 272
		// (add) Token: 0x060043AF RID: 17327 RVA: 0x000B6210 File Offset: 0x000B4410
		// (remove) Token: 0x060043B0 RID: 17328 RVA: 0x000B6223 File Offset: 0x000B4423
		public event ParsingCompleteHandler ParsingComplete
		{
			add
			{
				this.events.AddHandler(AspParser.parsingCompleteEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(AspParser.parsingCompleteEvent, value);
			}
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000B6238 File Offset: 0x000B4438
		public AspParser(string filename, TextReader input)
		{
			this.filename = filename;
			this.fileText = input.ReadToEnd();
			this.fileReader = new StringReader(this.fileText);
			this._internalLineOffset = 0;
			this.tokenizer = new AspTokenizer(this.fileReader);
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x000B6292 File Offset: 0x000B4492
		public AspParser(string filename, TextReader input, int startLineOffset, int positionOffset, AspParser outer)
			: this(filename, input)
		{
			this._internal = true;
			this._internalLineOffset = startLineOffset;
			this._internalPositionOffset = positionOffset;
			this.outer = outer;
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x000B62BA File Offset: 0x000B44BA
		public byte[] MD5Checksum
		{
			get
			{
				if (this.checksum == null)
				{
					return new byte[0];
				}
				return this.checksum.Hash;
			}
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x060043B4 RID: 17332 RVA: 0x000B62D6 File Offset: 0x000B44D6
		public int BeginPosition
		{
			get
			{
				return this.beginPosition;
			}
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x060043B5 RID: 17333 RVA: 0x000B62DE File Offset: 0x000B44DE
		public int EndPosition
		{
			get
			{
				return this.endPosition;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x060043B6 RID: 17334 RVA: 0x000B62E6 File Offset: 0x000B44E6
		public int BeginLine
		{
			get
			{
				if (this._internal)
				{
					return this.beginLine + this._internalLineOffset;
				}
				return this.beginLine;
			}
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x060043B7 RID: 17335 RVA: 0x000B6304 File Offset: 0x000B4504
		public int BeginColumn
		{
			get
			{
				return this.beginColumn;
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x000B630C File Offset: 0x000B450C
		public int EndLine
		{
			get
			{
				if (this._internal)
				{
					return this.endLine + this._internalLineOffset;
				}
				return this.endLine;
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x000B632A File Offset: 0x000B452A
		public int EndColumn
		{
			get
			{
				return this.endColumn;
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x000B6334 File Offset: 0x000B4534
		public string FileText
		{
			get
			{
				string text = null;
				if (this._internal && this.outer != null)
				{
					text = this.outer.FileText;
				}
				if (text == null && this.fileText != null)
				{
					text = this.fileText;
				}
				return text;
			}
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x000B6374 File Offset: 0x000B4574
		public string PlainText
		{
			get
			{
				if (this.beginPosition >= this.endPosition || this.fileText == null)
				{
					return null;
				}
				string text = this.FileText;
				int num;
				int num2;
				if (this._internal && this.outer != null)
				{
					num = this.beginPosition + this._internalPositionOffset;
					num2 = this.endPosition + this._internalPositionOffset - num;
				}
				else
				{
					num = this.beginPosition;
					num2 = this.endPosition - this.beginPosition;
				}
				if (text != null)
				{
					return text.Substring(num, num2);
				}
				return null;
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x000B63F2 File Offset: 0x000B45F2
		public string Filename
		{
			get
			{
				if (this._internal && this.outer != null)
				{
					return this.outer.Filename;
				}
				return this.filename;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (set) Token: 0x060043BD RID: 17341 RVA: 0x000B6416 File Offset: 0x000B4616
		public string VerbatimID
		{
			set
			{
				this.tokenizer.Verbatim = true;
				this.verbatimID = value;
			}
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x000B642B File Offset: 0x000B462B
		private bool Eat(int expected_token)
		{
			if (this.tokenizer.get_token() != expected_token)
			{
				this.tokenizer.put_back();
				return false;
			}
			this.endLine = this.tokenizer.EndLine;
			this.endColumn = this.tokenizer.EndColumn;
			return true;
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x000B646B File Offset: 0x000B466B
		private void BeginElement()
		{
			this.beginLine = this.tokenizer.BeginLine;
			this.beginColumn = this.tokenizer.BeginColumn;
			this.beginPosition = this.tokenizer.Position - 1;
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x000B64A2 File Offset: 0x000B46A2
		private void EndElement()
		{
			this.endLine = this.tokenizer.EndLine;
			this.endColumn = this.tokenizer.EndColumn;
			this.endPosition = this.tokenizer.Position;
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x000B64D8 File Offset: 0x000B46D8
		public void Parse()
		{
			if (this.tokenizer == null)
			{
				this.OnError("AspParser not initialized properly.");
				return;
			}
			TagType tagType = TagType.Text;
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				int num;
				while ((num = this.tokenizer.get_token()) != 2097152)
				{
					this.BeginElement();
					if (this.tokenizer.Verbatim)
					{
						string text = "</" + this.verbatimID + ">";
						string verbatim = this.GetVerbatim(num, text);
						if (verbatim == null)
						{
							this.OnError("Unexpected EOF processing " + this.verbatimID);
						}
						this.tokenizer.Verbatim = false;
						this.EndElement();
						this.endPosition -= text.Length;
						this.OnTextParsed(verbatim);
						this.beginPosition = this.endPosition;
						this.endPosition += text.Length;
						this.OnTagParsed(TagType.Close, this.verbatimID, null);
					}
					else if (num == 60)
					{
						string text2;
						TagAttributes tagAttributes;
						this.GetTag(out tagType, out text2, out tagAttributes);
						this.EndElement();
						if (tagType != TagType.ServerComment)
						{
							if (tagType == TagType.Text)
							{
								this.OnTextParsed(text2);
							}
							else
							{
								this.OnTagParsed(tagType, text2, tagAttributes);
							}
						}
					}
					else if (this.tokenizer.Value.Trim().Length != 0 || tagType != TagType.Directive)
					{
						stringBuilder.Length = 0;
						do
						{
							stringBuilder.Append(this.tokenizer.Value);
							num = this.tokenizer.get_token();
						}
						while (num != 60 && num != 2097152);
						this.tokenizer.put_back();
						this.EndElement();
						this.OnTextParsed(stringBuilder.ToString());
					}
				}
			}
			finally
			{
				if (this.fileReader != null)
				{
					this.fileReader.Close();
					this.fileReader = null;
				}
				this.checksum = this.tokenizer.Checksum;
				this.tokenizer = null;
			}
			this.OnParsingComplete();
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x000B66CC File Offset: 0x000B48CC
		private bool GetInclude(string str, out string pathType, out string filename)
		{
			pathType = null;
			filename = null;
			str = str.Substring(2).Trim();
			int length = str.Length;
			int num = str.LastIndexOf('"');
			if (length < 10 || num != length - 1)
			{
				return false;
			}
			if (!StrUtils.StartsWith(str, "#include ", true))
			{
				return false;
			}
			str = str.Substring(9).Trim();
			bool flag = StrUtils.StartsWith(str, "file", true);
			if (!flag && !StrUtils.StartsWith(str, "virtual", true))
			{
				return false;
			}
			pathType = (flag ? "file" : "virtual");
			if (str.Length < pathType.Length + 3)
			{
				return false;
			}
			str = str.Substring(pathType.Length).Trim();
			if (str.Length < 3 || str[0] != '=')
			{
				return false;
			}
			int num2 = 1;
			while (num2 < str.Length && (char.IsWhiteSpace(str[num2]) || str[num2] != '"'))
			{
				num2++;
			}
			if (num2 == str.Length || num2 == num)
			{
				return false;
			}
			str = str.Substring(num2);
			if (str.Length == 2)
			{
				this.OnError("Empty file name.");
				return false;
			}
			filename = str.Trim().Substring(num2, str.Length - 2);
			return filename.LastIndexOf('"') == -1;
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x000B6814 File Offset: 0x000B4A14
		private void GetTag(out TagType tagtype, out string id, out TagAttributes attributes)
		{
			int token = this.tokenizer.get_token();
			tagtype = TagType.ServerComment;
			id = null;
			attributes = null;
			if (token <= 37)
			{
				if (token != 33)
				{
					if (token == 37)
					{
						this.GetServerTag(out tagtype, out id, out attributes);
						return;
					}
				}
				else
				{
					bool flag = this.Eat(2097157);
					if (flag)
					{
						this.tokenizer.put_back();
					}
					this.tokenizer.Verbatim = true;
					string text = (flag ? "-->" : ">");
					string verbatim = this.GetVerbatim(this.tokenizer.get_token(), text);
					this.tokenizer.Verbatim = false;
					if (verbatim == null)
					{
						this.OnError("Unfinished HTML comment/DTD");
					}
					string text2;
					string text3;
					if (flag && this.GetInclude(verbatim, out text2, out text3))
					{
						tagtype = TagType.Include;
						attributes = new TagAttributes();
						attributes.Add(text2, text3);
						return;
					}
					tagtype = TagType.Text;
					id = "<!" + verbatim + text;
					return;
				}
			}
			else
			{
				if (token == 47)
				{
					if (!this.Eat(2097153))
					{
						this.OnError("expecting TAGNAME");
					}
					id = this.tokenizer.Value;
					if (!this.Eat(62))
					{
						this.OnError("expecting '>'. Got '" + id + "'");
					}
					tagtype = TagType.Close;
					return;
				}
				if (token == 2097153)
				{
					if (this.filename == "@@inner_string@@")
					{
						tagtype = TagType.Text;
						this.tokenizer.InTag = false;
						id = "<" + this.tokenizer.Odds + this.tokenizer.Value;
						return;
					}
					id = this.tokenizer.Value;
					try
					{
						attributes = this.GetAttributes();
					}
					catch (Exception ex)
					{
						this.OnError(ex.Message);
						return;
					}
					tagtype = TagType.Tag;
					if (this.Eat(47) && this.Eat(62))
					{
						tagtype = TagType.SelfClosing;
						return;
					}
					if (this.Eat(62))
					{
						return;
					}
					if (attributes.IsRunAtServer())
					{
						this.OnError("The server tag is not well formed.");
						return;
					}
					this.tokenizer.Verbatim = true;
					attributes.Add(string.Empty, this.GetVerbatim(this.tokenizer.get_token(), ">") + ">");
					this.tokenizer.Verbatim = false;
					return;
				}
			}
			string text4;
			if ((ushort)token == 60)
			{
				string odds = this.tokenizer.Odds;
				if (odds != null && odds.Length > 0 && char.IsWhiteSpace(odds[0]))
				{
					this.tokenizer.put_back();
					text4 = odds;
				}
				else
				{
					text4 = this.tokenizer.Value;
				}
			}
			else
			{
				text4 = this.tokenizer.Value;
			}
			tagtype = TagType.Text;
			this.tokenizer.InTag = false;
			id = "<" + text4;
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x000B6ACC File Offset: 0x000B4CCC
		private TagAttributes GetAttributes()
		{
			bool flag = true;
			TagAttributes tagAttributes = new TagAttributes();
			int token;
			while ((token = this.tokenizer.get_token()) != 2097152)
			{
				if (token == 60 && this.Eat(37))
				{
					this.tokenizer.Verbatim = true;
					tagAttributes.Add(string.Empty, "<%" + this.GetVerbatim(this.tokenizer.get_token(), "%>") + "%>");
					this.tokenizer.Verbatim = false;
					this.tokenizer.InTag = true;
				}
				else
				{
					if (token != 2097153)
					{
						break;
					}
					string value = this.tokenizer.Value;
					if (this.Eat(61))
					{
						if (this.Eat(2097155))
						{
							tagAttributes.Add(value, this.tokenizer.Value);
							flag &= this.tokenizer.AlternatingQuotes;
						}
						else
						{
							if (!this.Eat(60) || !this.Eat(37))
							{
								this.OnError("expected ATTVALUE");
								return null;
							}
							this.tokenizer.Verbatim = true;
							tagAttributes.Add(value, "<%" + this.GetVerbatim(this.tokenizer.get_token(), "%>") + "%>");
							this.tokenizer.Verbatim = false;
							this.tokenizer.InTag = true;
						}
					}
					else
					{
						tagAttributes.Add(value, null);
					}
				}
			}
			this.tokenizer.put_back();
			if (tagAttributes.IsRunAtServer() && !flag)
			{
				this.OnError("The server tag is not well formed.");
				return null;
			}
			return tagAttributes;
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x000B6C58 File Offset: 0x000B4E58
		private string GetVerbatim(int token, string end)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			int num = 0;
			if (this.tokenizer.Value.Length > 1)
			{
				stringBuilder.Append(this.tokenizer.Value);
				token = this.tokenizer.get_token();
			}
			end = end.ToLower(Helpers.InvariantCulture);
			int num2 = 0;
			for (int i = 0; i < end.Length; i++)
			{
				if (end[0] == end[i])
				{
					num2++;
				}
			}
			while (token != 2097152)
			{
				if (char.ToLower((char)token, Helpers.InvariantCulture) == end[num])
				{
					if (++num >= end.Length)
					{
						break;
					}
					stringBuilder2.Append((char)token);
					token = this.tokenizer.get_token();
				}
				else
				{
					if (num > 0)
					{
						if (num2 > 1 && num == num2 && (char)token == end[0])
						{
							stringBuilder.Append((char)token);
							token = this.tokenizer.get_token();
							continue;
						}
						stringBuilder.Append(stringBuilder2.ToString());
						stringBuilder2.Remove(0, stringBuilder2.Length);
						num = 0;
					}
					stringBuilder.Append((char)token);
					token = this.tokenizer.get_token();
				}
			}
			if (token == 2097152)
			{
				this.OnError("Expecting " + end + " and got EOF.");
			}
			return this.RemoveComments(stringBuilder.ToString());
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x000B6DB8 File Offset: 0x000B4FB8
		private string RemoveComments(string text)
		{
			for (int num = text.IndexOf("<%--"); num != -1; num = text.IndexOf("<%--"))
			{
				int num2 = text.IndexOf("--%>");
				if (num2 == -1 || num2 <= num + 1)
				{
					break;
				}
				text = text.Remove(num, num2 - num + 4);
			}
			return text;
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x000B6E08 File Offset: 0x000B5008
		private void GetServerTag(out TagType tagtype, out string id, out TagAttributes attributes)
		{
			bool expectAttrValue = this.tokenizer.ExpectAttrValue;
			this.tokenizer.ExpectAttrValue = false;
			if (this.Eat(64))
			{
				this.tokenizer.ExpectAttrValue = expectAttrValue;
				tagtype = TagType.Directive;
				id = "";
				if (this.Eat(2097154))
				{
					id = this.tokenizer.Value;
				}
				attributes = this.GetAttributes();
				if (!this.Eat(37) || !this.Eat(62))
				{
					this.OnError("expecting '%>'");
				}
				return;
			}
			string text;
			if (this.Eat(2097157))
			{
				this.tokenizer.ExpectAttrValue = expectAttrValue;
				this.tokenizer.Verbatim = true;
				text = this.GetVerbatim(this.tokenizer.get_token(), "--%>");
				this.tokenizer.Verbatim = false;
				id = null;
				attributes = null;
				tagtype = TagType.ServerComment;
				return;
			}
			this.tokenizer.ExpectAttrValue = expectAttrValue;
			bool flag = this.Eat(61);
			bool flag2 = !flag && this.Eat(35);
			bool flag3 = !flag2 && !flag && this.Eat(58);
			string odds = this.tokenizer.Odds;
			this.tokenizer.Verbatim = true;
			text = this.GetVerbatim(this.tokenizer.get_token(), "%>");
			if (flag2 && odds != null && odds.Length > 0)
			{
				flag2 = false;
				text = "#" + text;
			}
			this.tokenizer.Verbatim = false;
			id = text;
			attributes = null;
			if (flag2)
			{
				tagtype = TagType.DataBinding;
				return;
			}
			if (flag)
			{
				tagtype = TagType.CodeRenderExpression;
				return;
			}
			if (flag3)
			{
				tagtype = TagType.CodeRenderEncode;
				return;
			}
			tagtype = TagType.CodeRender;
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x000B6F94 File Offset: 0x000B5194
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("AspParser {");
			if (this.filename != null && this.filename.Length > 0)
			{
				stringBuilder.AppendFormat("{0}:{1}.{2}", this.filename, this.beginLine, this.beginColumn);
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x000B6FFC File Offset: 0x000B51FC
		private void OnError(string msg)
		{
			ParseErrorHandler parseErrorHandler = this.events[AspParser.errorEvent] as ParseErrorHandler;
			if (parseErrorHandler != null)
			{
				parseErrorHandler(this, msg);
			}
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x000B702C File Offset: 0x000B522C
		private void OnTagParsed(TagType tagtype, string id, TagAttributes attributes)
		{
			TagParsedHandler tagParsedHandler = this.events[AspParser.tagParsedEvent] as TagParsedHandler;
			if (tagParsedHandler != null)
			{
				tagParsedHandler(this, tagtype, id, attributes);
			}
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x000B705C File Offset: 0x000B525C
		private void OnTextParsed(string text)
		{
			TextParsedHandler textParsedHandler = this.events[AspParser.textParsedEvent] as TextParsedHandler;
			if (textParsedHandler != null)
			{
				textParsedHandler(this, text);
			}
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x000B708C File Offset: 0x000B528C
		private void OnParsingComplete()
		{
			ParsingCompleteHandler parsingCompleteHandler = this.events[AspParser.parsingCompleteEvent] as ParsingCompleteHandler;
			if (parsingCompleteHandler != null)
			{
				parsingCompleteHandler();
			}
		}

		// Token: 0x04002425 RID: 9253
		private static readonly object errorEvent = new object();

		// Token: 0x04002426 RID: 9254
		private static readonly object tagParsedEvent = new object();

		// Token: 0x04002427 RID: 9255
		private static readonly object textParsedEvent = new object();

		// Token: 0x04002428 RID: 9256
		private static readonly object parsingCompleteEvent = new object();

		// Token: 0x04002429 RID: 9257
		private MD5 checksum;

		// Token: 0x0400242A RID: 9258
		private AspTokenizer tokenizer;

		// Token: 0x0400242B RID: 9259
		private int beginLine;

		// Token: 0x0400242C RID: 9260
		private int endLine;

		// Token: 0x0400242D RID: 9261
		private int beginColumn;

		// Token: 0x0400242E RID: 9262
		private int endColumn;

		// Token: 0x0400242F RID: 9263
		private int beginPosition;

		// Token: 0x04002430 RID: 9264
		private int endPosition;

		// Token: 0x04002431 RID: 9265
		private string filename;

		// Token: 0x04002432 RID: 9266
		private string verbatimID;

		// Token: 0x04002433 RID: 9267
		private string fileText;

		// Token: 0x04002434 RID: 9268
		private StringReader fileReader;

		// Token: 0x04002435 RID: 9269
		private bool _internal;

		// Token: 0x04002436 RID: 9270
		private int _internalLineOffset;

		// Token: 0x04002437 RID: 9271
		private int _internalPositionOffset;

		// Token: 0x04002438 RID: 9272
		private AspParser outer;

		// Token: 0x04002439 RID: 9273
		private EventHandlerList events = new EventHandlerList();
	}
}
