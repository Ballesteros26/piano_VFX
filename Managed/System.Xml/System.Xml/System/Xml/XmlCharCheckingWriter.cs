using System;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000D2 RID: 210
	internal class XmlCharCheckingWriter : XmlWrappingWriter
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x0001EE6D File Offset: 0x0001D06D
		internal XmlCharCheckingWriter(XmlWriter baseWriter, bool checkValues, bool checkNames, bool replaceNewLines, string newLineChars)
			: base(baseWriter)
		{
			this.checkValues = checkValues;
			this.checkNames = checkNames;
			this.replaceNewLines = replaceNewLines;
			this.newLineChars = newLineChars;
			if (checkValues)
			{
				this.xmlCharType = XmlCharType.Instance;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings xmlWriterSettings = this.writer.Settings;
				xmlWriterSettings = ((xmlWriterSettings != null) ? xmlWriterSettings.Clone() : new XmlWriterSettings());
				if (this.checkValues)
				{
					xmlWriterSettings.CheckCharacters = true;
				}
				if (this.replaceNewLines)
				{
					xmlWriterSettings.NewLineHandling = NewLineHandling.Replace;
					xmlWriterSettings.NewLineChars = this.newLineChars;
				}
				xmlWriterSettings.ReadOnly = true;
				return xmlWriterSettings;
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001EF00 File Offset: 0x0001D100
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.checkNames)
			{
				this.ValidateQName(name);
			}
			if (this.checkValues)
			{
				int num;
				if (pubid != null && (num = this.xmlCharType.IsPublicId(pubid)) >= 0)
				{
					throw XmlConvert.CreateInvalidCharException(pubid, num);
				}
				if (sysid != null)
				{
					this.CheckCharacters(sysid);
				}
				if (subset != null)
				{
					this.CheckCharacters(subset);
				}
			}
			if (this.replaceNewLines)
			{
				sysid = this.ReplaceNewLines(sysid);
				pubid = this.ReplaceNewLines(pubid);
				subset = this.ReplaceNewLines(subset);
			}
			this.writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001EF8C File Offset: 0x0001D18C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.checkNames)
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.ValidateNCName(localName);
				if (prefix != null && prefix.Length > 0)
				{
					this.ValidateNCName(prefix);
				}
			}
			this.writer.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001EFE4 File Offset: 0x0001D1E4
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.checkNames)
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.ValidateNCName(localName);
				if (prefix != null && prefix.Length > 0)
				{
					this.ValidateNCName(prefix);
				}
			}
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001F03C File Offset: 0x0001D23C
		public override void WriteCData(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
				int num;
				while ((num = text.IndexOf("]]>", StringComparison.Ordinal)) >= 0)
				{
					this.writer.WriteCData(text.Substring(0, num + 2));
					text = text.Substring(num + 2);
				}
			}
			this.writer.WriteCData(text);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001F0AB File Offset: 0x0001D2AB
		public override void WriteComment(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
					text = this.InterleaveInvalidChars(text, '-', '-');
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			this.writer.WriteComment(text);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001F0EC File Offset: 0x0001D2EC
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.checkNames)
			{
				this.ValidateNCName(name);
			}
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
					text = this.InterleaveInvalidChars(text, '?', '>');
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001F145 File Offset: 0x0001D345
		public override void WriteEntityRef(string name)
		{
			if (this.checkNames)
			{
				this.ValidateQName(name);
			}
			this.writer.WriteEntityRef(name);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001F164 File Offset: 0x0001D364
		public override void WriteWhitespace(string ws)
		{
			if (ws == null)
			{
				ws = string.Empty;
			}
			int num;
			if (this.checkNames && (num = this.xmlCharType.IsOnlyWhitespaceWithPos(ws)) != -1)
			{
				throw new ArgumentException(Res.GetString("The Whitespace or SignificantWhitespace node can contain only XML white space characters. '{0}' is not an XML white space character.", XmlException.BuildCharExceptionArgs(ws, num)));
			}
			if (this.replaceNewLines)
			{
				ws = this.ReplaceNewLines(ws);
			}
			this.writer.WriteWhitespace(ws);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		public override void WriteString(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
				}
				if (this.replaceNewLines && this.WriteState != WriteState.Attribute)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			this.writer.WriteString(text);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001F202 File Offset: 0x0001D402
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001F214 File Offset: 0x0001D414
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.checkValues)
			{
				this.CheckCharacters(buffer, index, count);
			}
			if (this.replaceNewLines && this.WriteState != WriteState.Attribute)
			{
				string text = this.ReplaceNewLines(buffer, index, count);
				if (text != null)
				{
					this.WriteString(text);
					return;
				}
			}
			this.writer.WriteChars(buffer, index, count);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001F2A5 File Offset: 0x0001D4A5
		public override void WriteNmToken(string name)
		{
			if (this.checkNames)
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				XmlConvert.VerifyNMTOKEN(name);
			}
			this.writer.WriteNmToken(name);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001F2DD File Offset: 0x0001D4DD
		public override void WriteName(string name)
		{
			if (this.checkNames)
			{
				XmlConvert.VerifyQName(name, ExceptionType.XmlException);
			}
			this.writer.WriteName(name);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001F2FB File Offset: 0x0001D4FB
		public override void WriteQualifiedName(string localName, string ns)
		{
			if (this.checkNames)
			{
				this.ValidateNCName(localName);
			}
			this.writer.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001F319 File Offset: 0x0001D519
		private void CheckCharacters(string str)
		{
			XmlConvert.VerifyCharData(str, ExceptionType.ArgumentException);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001F322 File Offset: 0x0001D522
		private void CheckCharacters(char[] data, int offset, int len)
		{
			XmlConvert.VerifyCharData(data, offset, len, ExceptionType.ArgumentException);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001F330 File Offset: 0x0001D530
		private void ValidateNCName(string ncname)
		{
			if (ncname.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			int num = ValidateNames.ParseNCName(ncname, 0);
			if (num != ncname.Length)
			{
				throw new ArgumentException(Res.GetString((num == 0) ? "Name cannot begin with the '{0}' character, hexadecimal value {1}." : "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(ncname, num)));
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001F388 File Offset: 0x0001D588
		private void ValidateQName(string name)
		{
			if (name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			int num2;
			int num = ValidateNames.ParseQName(name, 0, out num2);
			if (num != name.Length)
			{
				throw new ArgumentException(Res.GetString((num == 0 || (num2 > -1 && num == num2 + 1)) ? "Name cannot begin with the '{0}' character, hexadecimal value {1}." : "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num)));
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001F3EC File Offset: 0x0001D5EC
		private string ReplaceNewLines(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num = 0;
			int i;
			for (i = 0; i < str.Length; i++)
			{
				char c;
				if ((c = str[i]) < ' ')
				{
					if (c == '\n')
					{
						if (this.newLineChars == "\n")
						{
							goto IL_00F7;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(str.Length + 5);
						}
						stringBuilder.Append(str, num, i - num);
					}
					else
					{
						if (c != '\r')
						{
							goto IL_00F7;
						}
						if (i + 1 < str.Length && str[i + 1] == '\n')
						{
							if (this.newLineChars == "\r\n")
							{
								i++;
								goto IL_00F7;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(str.Length + 5);
							}
							stringBuilder.Append(str, num, i - num);
							i++;
						}
						else
						{
							if (this.newLineChars == "\r")
							{
								goto IL_00F7;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(str.Length + 5);
							}
							stringBuilder.Append(str, num, i - num);
						}
					}
					stringBuilder.Append(this.newLineChars);
					num = i + 1;
				}
				IL_00F7:;
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, i - num);
			return stringBuilder.ToString();
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001F518 File Offset: 0x0001D718
		private string ReplaceNewLines(char[] data, int offset, int len)
		{
			if (data == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num = offset;
			int num2 = offset + len;
			int i;
			for (i = offset; i < num2; i++)
			{
				char c;
				if ((c = data[i]) < ' ')
				{
					if (c == '\n')
					{
						if (this.newLineChars == "\n")
						{
							goto IL_00DF;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(len + 5);
						}
						stringBuilder.Append(data, num, i - num);
					}
					else
					{
						if (c != '\r')
						{
							goto IL_00DF;
						}
						if (i + 1 < num2 && data[i + 1] == '\n')
						{
							if (this.newLineChars == "\r\n")
							{
								i++;
								goto IL_00DF;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(len + 5);
							}
							stringBuilder.Append(data, num, i - num);
							i++;
						}
						else
						{
							if (this.newLineChars == "\r")
							{
								goto IL_00DF;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(len + 5);
							}
							stringBuilder.Append(data, num, i - num);
						}
					}
					stringBuilder.Append(this.newLineChars);
					num = i + 1;
				}
				IL_00DF:;
			}
			if (stringBuilder == null)
			{
				return null;
			}
			stringBuilder.Append(data, num, i - num);
			return stringBuilder.ToString();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001F628 File Offset: 0x0001D828
		private string InterleaveInvalidChars(string text, char invChar1, char invChar2)
		{
			StringBuilder stringBuilder = null;
			int num = 0;
			int i;
			for (i = 0; i < text.Length; i++)
			{
				if (text[i] == invChar2 && i > 0 && text[i - 1] == invChar1)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(text.Length + 5);
					}
					stringBuilder.Append(text, num, i - num);
					stringBuilder.Append(' ');
					num = i;
				}
			}
			if (stringBuilder != null)
			{
				stringBuilder.Append(text, num, i - num);
				if (i > 0 && text[i - 1] == invChar1)
				{
					stringBuilder.Append(' ');
				}
				return stringBuilder.ToString();
			}
			if (i != 0 && text[i - 1] == invChar1)
			{
				return text + " ";
			}
			return text;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001F6D8 File Offset: 0x0001D8D8
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			if (this.checkNames)
			{
				this.ValidateQName(name);
			}
			if (this.checkValues)
			{
				int num;
				if (pubid != null && (num = this.xmlCharType.IsPublicId(pubid)) >= 0)
				{
					throw XmlConvert.CreateInvalidCharException(pubid, num);
				}
				if (sysid != null)
				{
					this.CheckCharacters(sysid);
				}
				if (subset != null)
				{
					this.CheckCharacters(subset);
				}
			}
			if (this.replaceNewLines)
			{
				sysid = this.ReplaceNewLines(sysid);
				pubid = this.ReplaceNewLines(pubid);
				subset = this.ReplaceNewLines(subset);
			}
			return this.writer.WriteDocTypeAsync(name, pubid, sysid, subset);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001F764 File Offset: 0x0001D964
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			if (this.checkNames)
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.ValidateNCName(localName);
				if (prefix != null && prefix.Length > 0)
				{
					this.ValidateNCName(prefix);
				}
			}
			return this.writer.WriteStartElementAsync(prefix, localName, ns);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001F7BC File Offset: 0x0001D9BC
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			if (this.checkNames)
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid local name."));
				}
				this.ValidateNCName(localName);
				if (prefix != null && prefix.Length > 0)
				{
					this.ValidateNCName(prefix);
				}
			}
			return this.writer.WriteStartAttributeAsync(prefix, localName, ns);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001F814 File Offset: 0x0001DA14
		public override async Task WriteCDataAsync(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
				int i;
				while ((i = text.IndexOf("]]>", StringComparison.Ordinal)) >= 0)
				{
					await this.writer.WriteCDataAsync(text.Substring(0, i + 2)).ConfigureAwait(false);
					text = text.Substring(i + 2);
				}
			}
			await this.writer.WriteCDataAsync(text).ConfigureAwait(false);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001F861 File Offset: 0x0001DA61
		public override Task WriteCommentAsync(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
					text = this.InterleaveInvalidChars(text, '-', '-');
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			return this.writer.WriteCommentAsync(text);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			if (this.checkNames)
			{
				this.ValidateNCName(name);
			}
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
					text = this.InterleaveInvalidChars(text, '?', '>');
				}
				if (this.replaceNewLines)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			return this.writer.WriteProcessingInstructionAsync(name, text);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001F8F9 File Offset: 0x0001DAF9
		public override Task WriteEntityRefAsync(string name)
		{
			if (this.checkNames)
			{
				this.ValidateQName(name);
			}
			return this.writer.WriteEntityRefAsync(name);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001F918 File Offset: 0x0001DB18
		public override Task WriteWhitespaceAsync(string ws)
		{
			if (ws == null)
			{
				ws = string.Empty;
			}
			int num;
			if (this.checkNames && (num = this.xmlCharType.IsOnlyWhitespaceWithPos(ws)) != -1)
			{
				throw new ArgumentException(Res.GetString("The Whitespace or SignificantWhitespace node can contain only XML white space characters. '{0}' is not an XML white space character.", XmlException.BuildCharExceptionArgs(ws, num)));
			}
			if (this.replaceNewLines)
			{
				ws = this.ReplaceNewLines(ws);
			}
			return this.writer.WriteWhitespaceAsync(ws);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001F97C File Offset: 0x0001DB7C
		public override Task WriteStringAsync(string text)
		{
			if (text != null)
			{
				if (this.checkValues)
				{
					this.CheckCharacters(text);
				}
				if (this.replaceNewLines && this.WriteState != WriteState.Attribute)
				{
					text = this.ReplaceNewLines(text);
				}
			}
			return this.writer.WriteStringAsync(text);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001F9B6 File Offset: 0x0001DBB6
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			return this.writer.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001F9C8 File Offset: 0x0001DBC8
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.checkValues)
			{
				this.CheckCharacters(buffer, index, count);
			}
			if (this.replaceNewLines && this.WriteState != WriteState.Attribute)
			{
				string text = this.ReplaceNewLines(buffer, index, count);
				if (text != null)
				{
					return this.WriteStringAsync(text);
				}
			}
			return this.writer.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001FA59 File Offset: 0x0001DC59
		public override Task WriteNmTokenAsync(string name)
		{
			if (this.checkNames)
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				XmlConvert.VerifyNMTOKEN(name);
			}
			return this.writer.WriteNmTokenAsync(name);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001FA91 File Offset: 0x0001DC91
		public override Task WriteNameAsync(string name)
		{
			if (this.checkNames)
			{
				XmlConvert.VerifyQName(name, ExceptionType.XmlException);
			}
			return this.writer.WriteNameAsync(name);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001FAAF File Offset: 0x0001DCAF
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			if (this.checkNames)
			{
				this.ValidateNCName(localName);
			}
			return this.writer.WriteQualifiedNameAsync(localName, ns);
		}

		// Token: 0x0400041F RID: 1055
		private bool checkValues;

		// Token: 0x04000420 RID: 1056
		private bool checkNames;

		// Token: 0x04000421 RID: 1057
		private bool replaceNewLines;

		// Token: 0x04000422 RID: 1058
		private string newLineChars;

		// Token: 0x04000423 RID: 1059
		private XmlCharType xmlCharType;
	}
}
