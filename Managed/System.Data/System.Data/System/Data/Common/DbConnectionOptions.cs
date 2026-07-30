using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
	// Token: 0x0200031D RID: 797
	internal class DbConnectionOptions
	{
		// Token: 0x0600245B RID: 9307 RVA: 0x000A5C9B File Offset: 0x000A3E9B
		public string UsersConnectionString(bool hidePassword)
		{
			return this.UsersConnectionString(hidePassword, false);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000A5CA8 File Offset: 0x000A3EA8
		private string UsersConnectionString(bool hidePassword, bool forceHidePassword)
		{
			string usersConnectionString = this._usersConnectionString;
			if (this._hasPasswordKeyword && (forceHidePassword || (hidePassword && !this.HasPersistablePassword)))
			{
				this.ReplacePasswordPwd(out usersConnectionString, false);
			}
			return usersConnectionString ?? string.Empty;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x000A5CE6 File Offset: 0x000A3EE6
		internal bool HasPersistablePassword
		{
			get
			{
				return !this._hasPasswordKeyword || this.ConvertValueToBoolean("persist security info", false);
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000A5D00 File Offset: 0x000A3F00
		public bool ConvertValueToBoolean(string keyName, bool defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text))
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertValueToBooleanInternal(keyName, text);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000A5D28 File Offset: 0x000A3F28
		internal static bool ConvertValueToBooleanInternal(string keyName, string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string text = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "true") || DbConnectionOptions.CompareInsensitiveInvariant(text, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "false") || DbConnectionOptions.CompareInsensitiveInvariant(text, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue(keyName);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000A5DB2 File Offset: 0x000A3FB2
		private static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(strvalue, strconst) == 0;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000A5DC4 File Offset: 0x000A3FC4
		[Conditional("DEBUG")]
		[Conditional("DEBUG")]
		private static void DebugTraceKeyValuePair(string keyname, string keyvalue, Dictionary<string, string> synonyms)
		{
			string text = ((synonyms != null) ? synonyms[keyname] : keyname);
			if ("password" != text && "pwd" != text)
			{
				if (keyvalue != null)
				{
					DataCommonEventSource.Log.Trace<string, string>("<comm.DbConnectionOptions|INFO|ADV> KeyName='{0}', KeyValue='{1}'", keyname, keyvalue);
					return;
				}
				DataCommonEventSource.Log.Trace<string>("<comm.DbConnectionOptions|INFO|ADV> KeyName='{0}'", keyname);
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000A5E20 File Offset: 0x000A4020
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000A5E60 File Offset: 0x000A4060
		private static string GetKeyValue(StringBuilder buffer, bool trimWhitespace)
		{
			int num = buffer.Length;
			int i = 0;
			if (trimWhitespace)
			{
				while (i < num)
				{
					if (!char.IsWhiteSpace(buffer[i]))
					{
						break;
					}
					i++;
				}
				while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
				{
					num--;
				}
			}
			return buffer.ToString(i, num - i);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000A5EB8 File Offset: 0x000A40B8
		internal static int GetKeyValuePair(string connectionString, int currentPosition, StringBuilder buffer, bool useOdbcRules, out string keyname, out string keyvalue)
		{
			int num = currentPosition;
			buffer.Length = 0;
			keyname = null;
			keyvalue = null;
			char c = '\0';
			DbConnectionOptions.ParserState parserState = DbConnectionOptions.ParserState.NothingYet;
			int length = connectionString.Length;
			while (currentPosition < length)
			{
				c = connectionString[currentPosition];
				switch (parserState)
				{
				case DbConnectionOptions.ParserState.NothingYet:
					if (';' != c && !char.IsWhiteSpace(c))
					{
						if (c == '\0')
						{
							parserState = DbConnectionOptions.ParserState.NullTermination;
						}
						else
						{
							if (char.IsControl(c))
							{
								throw ADP.ConnectionStringSyntax(num);
							}
							num = currentPosition;
							if ('=' != c)
							{
								parserState = DbConnectionOptions.ParserState.Key;
								goto IL_0248;
							}
							parserState = DbConnectionOptions.ParserState.KeyEqual;
						}
					}
					break;
				case DbConnectionOptions.ParserState.Key:
					if ('=' == c)
					{
						parserState = DbConnectionOptions.ParserState.KeyEqual;
					}
					else
					{
						if (!char.IsWhiteSpace(c) && char.IsControl(c))
						{
							throw ADP.ConnectionStringSyntax(num);
						}
						goto IL_0248;
					}
					break;
				case DbConnectionOptions.ParserState.KeyEqual:
					if (!useOdbcRules && '=' == c)
					{
						parserState = DbConnectionOptions.ParserState.Key;
						goto IL_0248;
					}
					keyname = DbConnectionOptions.GetKeyName(buffer);
					if (string.IsNullOrEmpty(keyname))
					{
						throw ADP.ConnectionStringSyntax(num);
					}
					buffer.Length = 0;
					parserState = DbConnectionOptions.ParserState.KeyEnd;
					goto IL_0107;
				case DbConnectionOptions.ParserState.KeyEnd:
					goto IL_0107;
				case DbConnectionOptions.ParserState.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_0248;
					}
					if (char.IsControl(c))
					{
						goto IL_025C;
					}
					if (';' == c)
					{
						goto IL_025C;
					}
					goto IL_0248;
				case DbConnectionOptions.ParserState.DoubleQuoteValue:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(num);
						}
						goto IL_0248;
					}
					break;
				case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_0248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0212;
				case DbConnectionOptions.ParserState.SingleQuoteValue:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(num);
						}
						goto IL_0248;
					}
					break;
				case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_0248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0212;
				case DbConnectionOptions.ParserState.BraceQuoteValue:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValueQuote;
						goto IL_0248;
					}
					if (c == '\0')
					{
						throw ADP.ConnectionStringSyntax(num);
					}
					goto IL_0248;
				case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_0248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0212;
				case DbConnectionOptions.ParserState.QuotedValueEnd:
					goto IL_0212;
				case DbConnectionOptions.ParserState.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw ADP.ConnectionStringSyntax(currentPosition);
					}
					break;
				default:
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState1);
				}
				IL_0250:
				currentPosition++;
				continue;
				IL_0107:
				if (char.IsWhiteSpace(c))
				{
					goto IL_0250;
				}
				if (useOdbcRules)
				{
					if ('{' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_0248;
					}
				}
				else
				{
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_0250;
					}
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_0250;
					}
				}
				if (';' == c || c == '\0')
				{
					break;
				}
				if (char.IsControl(c))
				{
					throw ADP.ConnectionStringSyntax(num);
				}
				parserState = DbConnectionOptions.ParserState.UnquotedValue;
				goto IL_0248;
				IL_0212:
				if (char.IsWhiteSpace(c))
				{
					goto IL_0250;
				}
				if (';' == c)
				{
					break;
				}
				if (c == '\0')
				{
					parserState = DbConnectionOptions.ParserState.NullTermination;
					goto IL_0250;
				}
				throw ADP.ConnectionStringSyntax(num);
				IL_0248:
				buffer.Append(c);
				goto IL_0250;
			}
			IL_025C:
			switch (parserState)
			{
			case DbConnectionOptions.ParserState.NothingYet:
			case DbConnectionOptions.ParserState.KeyEnd:
			case DbConnectionOptions.ParserState.NullTermination:
				break;
			case DbConnectionOptions.ParserState.Key:
			case DbConnectionOptions.ParserState.DoubleQuoteValue:
			case DbConnectionOptions.ParserState.SingleQuoteValue:
			case DbConnectionOptions.ParserState.BraceQuoteValue:
				throw ADP.ConnectionStringSyntax(num);
			case DbConnectionOptions.ParserState.KeyEqual:
				keyname = DbConnectionOptions.GetKeyName(buffer);
				if (string.IsNullOrEmpty(keyname))
				{
					throw ADP.ConnectionStringSyntax(num);
				}
				break;
			case DbConnectionOptions.ParserState.UnquotedValue:
			{
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, true);
				char c2 = keyvalue[keyvalue.Length - 1];
				if (!useOdbcRules && ('\'' == c2 || '"' == c2))
				{
					throw ADP.ConnectionStringSyntax(num);
				}
				break;
			}
			case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
			case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
			case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
			case DbConnectionOptions.ParserState.QuotedValueEnd:
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
				break;
			default:
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState2);
			}
			if (';' == c && currentPosition < connectionString.Length)
			{
				currentPosition++;
			}
			return currentPosition;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000A61DC File Offset: 0x000A43DC
		private static bool IsValueValidInternal(string keyvalue)
		{
			return keyvalue == null || -1 == keyvalue.IndexOf('\0');
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000A61ED File Offset: 0x000A43ED
		private static bool IsKeyNameValid(string keyname)
		{
			return keyname != null && (0 < keyname.Length && ';' != keyname[0] && !char.IsWhiteSpace(keyname[0])) && -1 == keyname.IndexOf('\0');
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000A6224 File Offset: 0x000A4424
		private static NameValuePair ParseInternal(Dictionary<string, string> parsetable, string connectionString, bool buildChain, Dictionary<string, string> synonyms, bool firstKey)
		{
			StringBuilder stringBuilder = new StringBuilder();
			NameValuePair nameValuePair = null;
			NameValuePair nameValuePair2 = null;
			int i = 0;
			int length = connectionString.Length;
			while (i < length)
			{
				int num = i;
				string text;
				string text2;
				i = DbConnectionOptions.GetKeyValuePair(connectionString, num, stringBuilder, firstKey, out text, out text2);
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				string text4;
				string text3 = ((synonyms != null) ? (synonyms.TryGetValue(text, out text4) ? text4 : null) : text);
				if (!DbConnectionOptions.IsKeyNameValid(text3))
				{
					throw ADP.KeywordNotSupported(text);
				}
				if (!firstKey || !parsetable.ContainsKey(text3))
				{
					parsetable[text3] = text2;
				}
				if (nameValuePair != null)
				{
					nameValuePair = (nameValuePair.Next = new NameValuePair(text3, text2, i - num));
				}
				else if (buildChain)
				{
					nameValuePair = (nameValuePair2 = new NameValuePair(text3, text2, i - num));
				}
			}
			return nameValuePair2;
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000A62E4 File Offset: 0x000A44E4
		internal NameValuePair ReplacePasswordPwd(out string constr, bool fakePassword)
		{
			int num = 0;
			NameValuePair nameValuePair = null;
			NameValuePair nameValuePair2 = null;
			NameValuePair nameValuePair3 = null;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair4 = this._keyChain; nameValuePair4 != null; nameValuePair4 = nameValuePair4.Next)
			{
				if ("password" != nameValuePair4.Name && "pwd" != nameValuePair4.Name)
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair4.Length);
					if (fakePassword)
					{
						nameValuePair3 = new NameValuePair(nameValuePair4.Name, nameValuePair4.Value, nameValuePair4.Length);
					}
				}
				else if (fakePassword)
				{
					stringBuilder.Append(nameValuePair4.Name).Append("=*;");
					nameValuePair3 = new NameValuePair(nameValuePair4.Name, "*", nameValuePair4.Name.Length + "=*;".Length);
				}
				if (fakePassword)
				{
					if (nameValuePair2 != null)
					{
						nameValuePair2 = (nameValuePair2.Next = nameValuePair3);
					}
					else
					{
						nameValuePair = (nameValuePair2 = nameValuePair3);
					}
				}
				num += nameValuePair4.Length;
			}
			constr = stringBuilder.ToString();
			return nameValuePair;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000A63F8 File Offset: 0x000A45F8
		public DbConnectionOptions(string connectionString, Dictionary<string, string> synonyms, bool useOdbcRules)
		{
			this._useOdbcRules = useOdbcRules;
			this._parsetable = new Dictionary<string, string>();
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this._keyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, this._useOdbcRules);
				this._hasPasswordKeyword = this._parsetable.ContainsKey("password") || this._parsetable.ContainsKey("pwd");
				this._hasUserIdKeyword = this._parsetable.ContainsKey("user id") || this._parsetable.ContainsKey("uid");
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x000A64B1 File Offset: 0x000A46B1
		internal Dictionary<string, string> Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x1700062B RID: 1579
		public string this[string keyword]
		{
			get
			{
				return this._parsetable[keyword];
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000A64C8 File Offset: 0x000A46C8
		internal static void AppendKeyValuePairBuilder(StringBuilder builder, string keyName, string keyValue, bool useOdbcRules)
		{
			ADP.CheckArgumentNull(builder, "builder");
			ADP.CheckArgumentLength(keyName, "keyName");
			if (keyName == null || !DbConnectionOptions.s_connectionStringValidKeyRegex.IsMatch(keyName))
			{
				throw ADP.InvalidKeyname(keyName);
			}
			if (keyValue != null && !DbConnectionOptions.IsValueValidInternal(keyValue))
			{
				throw ADP.InvalidValue(keyName);
			}
			if (0 < builder.Length && ';' != builder[builder.Length - 1])
			{
				builder.Append(';');
			}
			if (useOdbcRules)
			{
				builder.Append(keyName);
			}
			else
			{
				builder.Append(keyName.Replace("=", "=="));
			}
			builder.Append('=');
			if (keyValue != null)
			{
				if (useOdbcRules)
				{
					if (0 < keyValue.Length && ('{' == keyValue[0] || 0 <= keyValue.IndexOf(';') || string.Compare("Driver", keyName, StringComparison.OrdinalIgnoreCase) == 0) && !DbConnectionOptions.s_connectionStringQuoteOdbcValueRegex.IsMatch(keyValue))
					{
						builder.Append('{').Append(keyValue.Replace("}", "}}")).Append('}');
						return;
					}
					builder.Append(keyValue);
					return;
				}
				else
				{
					if (DbConnectionOptions.s_connectionStringQuoteValueRegex.IsMatch(keyValue))
					{
						builder.Append(keyValue);
						return;
					}
					if (-1 != keyValue.IndexOf('"') && -1 == keyValue.IndexOf('\''))
					{
						builder.Append('\'');
						builder.Append(keyValue);
						builder.Append('\'');
						return;
					}
					builder.Append('"');
					builder.Append(keyValue.Replace("\"", "\"\""));
					builder.Append('"');
				}
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000A6647 File Offset: 0x000A4847
		protected internal virtual string Expand()
		{
			return this._usersConnectionString;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000A6650 File Offset: 0x000A4850
		internal string ExpandKeyword(string keyword, string replacementValue)
		{
			bool flag = false;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair = this._keyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
			{
				if (nameValuePair.Name == keyword && nameValuePair.Value == this[keyword])
				{
					DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, nameValuePair.Name, replacementValue, this._useOdbcRules);
					stringBuilder.Append(';');
					flag = true;
				}
				else
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair.Length);
				}
				num += nameValuePair.Length;
			}
			if (!flag)
			{
				DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, keyword, replacementValue, this._useOdbcRules);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000A66FB File Offset: 0x000A48FB
		internal static void ValidateKeyValuePair(string keyword, string value)
		{
			if (keyword == null || !DbConnectionOptions.s_connectionStringValidKeyRegex.IsMatch(keyword))
			{
				throw ADP.InvalidKeyname(keyword);
			}
			if (value != null && !DbConnectionOptions.s_connectionStringValidValueRegex.IsMatch(value))
			{
				throw ADP.InvalidValue(keyword);
			}
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000A672C File Offset: 0x000A492C
		public DbConnectionOptions(string connectionString, Dictionary<string, string> synonyms)
		{
			this._parsetable = new Dictionary<string, string>();
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this._keyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, false);
				this._hasPasswordKeyword = this._parsetable.ContainsKey("password") || this._parsetable.ContainsKey("pwd");
			}
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000A67AE File Offset: 0x000A49AE
		protected DbConnectionOptions(DbConnectionOptions connectionOptions)
		{
			this._usersConnectionString = connectionOptions._usersConnectionString;
			this._hasPasswordKeyword = connectionOptions._hasPasswordKeyword;
			this._parsetable = connectionOptions._parsetable;
			this._keyChain = connectionOptions._keyChain;
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x000A67E6 File Offset: 0x000A49E6
		public bool IsEmpty
		{
			get
			{
				return this._keyChain == null;
			}
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000A67F1 File Offset: 0x000A49F1
		internal bool TryGetParsetableValue(string key, out string value)
		{
			return this._parsetable.TryGetValue(key, out value);
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000A6800 File Offset: 0x000A4A00
		public bool ConvertValueToIntegratedSecurity()
		{
			string text;
			return this._parsetable.TryGetValue("integrated security", out text) && text != null && this.ConvertValueToIntegratedSecurityInternal(text);
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000A6830 File Offset: 0x000A4A30
		internal bool ConvertValueToIntegratedSecurityInternal(string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string text = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(text, "true") || DbConnectionOptions.CompareInsensitiveInvariant(text, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "false") || DbConnectionOptions.CompareInsensitiveInvariant(text, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue("integrated security");
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000A68D8 File Offset: 0x000A4AD8
		public int ConvertValueToInt32(string keyName, int defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text) || text == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertToInt32Internal(keyName, text);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000A6904 File Offset: 0x000A4B04
		internal static int ConvertToInt32Internal(string keyname, string stringValue)
		{
			int num;
			try
			{
				num = int.Parse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, ex2);
			}
			return num;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000A6950 File Offset: 0x000A4B50
		public string ConvertValueToString(string keyName, string defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text) || text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000A6973 File Offset: 0x000A4B73
		public bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000A6984 File Offset: 0x000A4B84
		internal static string ExpandDataDirectory(string keyword, string value, ref string datadir)
		{
			string text = null;
			if (value != null && value.StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				string text2 = datadir;
				if (text2 == null)
				{
					object data = AppDomain.CurrentDomain.GetData("DataDirectory");
					text2 = data as string;
					if (data != null && text2 == null)
					{
						throw ADP.InvalidDataDirectory();
					}
					if (string.IsNullOrEmpty(text2))
					{
						text2 = AppDomain.CurrentDomain.BaseDirectory;
					}
					if (text2 == null)
					{
						text2 = "";
					}
					datadir = text2;
				}
				int length = "|datadirectory|".Length;
				bool flag = 0 < text2.Length && text2[text2.Length - 1] == '\\';
				bool flag2 = length < value.Length && value[length] == '\\';
				if (!flag && !flag2)
				{
					text = text2 + "\\" + value.Substring(length);
				}
				else if (flag && flag2)
				{
					text = text2 + value.Substring(length + 1);
				}
				else
				{
					text = text2 + value.Substring(length);
				}
				if (!ADP.GetFullPath(text).StartsWith(text2, StringComparison.Ordinal))
				{
					throw ADP.InvalidConnectionOptionValue(keyword);
				}
			}
			return text;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000A6A94 File Offset: 0x000A4C94
		internal string ExpandDataDirectories(ref string filename, ref int position)
		{
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			string text = null;
			int num = 0;
			bool flag = false;
			string text2;
			for (NameValuePair nameValuePair = this._keyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
			{
				text2 = nameValuePair.Value;
				if (this._useOdbcRules)
				{
					string text3 = nameValuePair.Name;
					if (!(text3 == "driver") && !(text3 == "pwd") && !(text3 == "uid"))
					{
						text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
					}
				}
				else
				{
					string text3 = nameValuePair.Name;
					uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text3);
					if (num2 <= 2781420622U)
					{
						if (num2 <= 1433271620U)
						{
							if (num2 != 910909208U)
							{
								if (num2 == 1433271620U)
								{
									if (text3 == "pwd")
									{
										goto IL_01AB;
									}
								}
							}
							else if (text3 == "password")
							{
								goto IL_01AB;
							}
						}
						else if (num2 != 1556604621U)
						{
							if (num2 == 2781420622U)
							{
								if (text3 == "data provider")
								{
									goto IL_01AB;
								}
							}
						}
						else if (text3 == "uid")
						{
							goto IL_01AB;
						}
					}
					else if (num2 <= 3082861500U)
					{
						if (num2 != 2906666283U)
						{
							if (num2 == 3082861500U)
							{
								if (text3 == "provider")
								{
									goto IL_01AB;
								}
							}
						}
						else if (text3 == "user id")
						{
							goto IL_01AB;
						}
					}
					else if (num2 != 4008387664U)
					{
						if (num2 == 4015305829U)
						{
							if (text3 == "extended properties")
							{
								goto IL_01AB;
							}
						}
					}
					else if (text3 == "remote provider")
					{
						goto IL_01AB;
					}
					text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
				}
				IL_01AB:
				if (text2 == null)
				{
					text2 = nameValuePair.Value;
				}
				if (this._useOdbcRules || "file name" != nameValuePair.Name)
				{
					if (text2 != nameValuePair.Value)
					{
						flag = true;
						DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, nameValuePair.Name, text2, this._useOdbcRules);
						stringBuilder.Append(';');
					}
					else
					{
						stringBuilder.Append(this._usersConnectionString, num, nameValuePair.Length);
					}
				}
				else
				{
					flag = true;
					filename = text2;
					position = stringBuilder.Length;
				}
				num += nameValuePair.Length;
			}
			if (flag)
			{
				text2 = stringBuilder.ToString();
			}
			else
			{
				text2 = null;
			}
			return text2;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x000A6CF4 File Offset: 0x000A4EF4
		internal bool HasBlankPassword
		{
			get
			{
				if (this.ConvertValueToIntegratedSecurity())
				{
					return false;
				}
				if (this._parsetable.ContainsKey("password"))
				{
					return ADP.IsEmpty(this._parsetable["password"]);
				}
				if (this._parsetable.ContainsKey("pwd"))
				{
					return ADP.IsEmpty(this._parsetable["pwd"]);
				}
				return (this._parsetable.ContainsKey("user id") && !ADP.IsEmpty(this._parsetable["user id"])) || (this._parsetable.ContainsKey("uid") && !ADP.IsEmpty(this._parsetable["uid"]));
			}
		}

		// Token: 0x040017A8 RID: 6056
		private const string ConnectionStringValidKeyPattern = "^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$";

		// Token: 0x040017A9 RID: 6057
		private const string ConnectionStringValidValuePattern = "^[^\0]*$";

		// Token: 0x040017AA RID: 6058
		private const string ConnectionStringQuoteValuePattern = "^[^\"'=;\\s\\p{Cc}]*$";

		// Token: 0x040017AB RID: 6059
		private const string ConnectionStringQuoteOdbcValuePattern = "^\\{([^\\}\0]|\\}\\})*\\}$";

		// Token: 0x040017AC RID: 6060
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x040017AD RID: 6061
		private static readonly Regex s_connectionStringValidKeyRegex = new Regex("^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$", RegexOptions.Compiled);

		// Token: 0x040017AE RID: 6062
		private static readonly Regex s_connectionStringValidValueRegex = new Regex("^[^\0]*$", RegexOptions.Compiled);

		// Token: 0x040017AF RID: 6063
		private static readonly Regex s_connectionStringQuoteValueRegex = new Regex("^[^\"'=;\\s\\p{Cc}]*$", RegexOptions.Compiled);

		// Token: 0x040017B0 RID: 6064
		private static readonly Regex s_connectionStringQuoteOdbcValueRegex = new Regex("^\\{([^\\}\0]|\\}\\})*\\}$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x040017B1 RID: 6065
		private readonly string _usersConnectionString;

		// Token: 0x040017B2 RID: 6066
		private readonly Dictionary<string, string> _parsetable;

		// Token: 0x040017B3 RID: 6067
		internal readonly NameValuePair _keyChain;

		// Token: 0x040017B4 RID: 6068
		internal readonly bool _hasPasswordKeyword;

		// Token: 0x040017B5 RID: 6069
		internal readonly bool _useOdbcRules;

		// Token: 0x040017B6 RID: 6070
		internal readonly bool _hasUserIdKeyword;

		// Token: 0x0200031E RID: 798
		private static class KEY
		{
			// Token: 0x040017B7 RID: 6071
			internal const string Integrated_Security = "integrated security";

			// Token: 0x040017B8 RID: 6072
			internal const string Password = "password";

			// Token: 0x040017B9 RID: 6073
			internal const string Persist_Security_Info = "persist security info";

			// Token: 0x040017BA RID: 6074
			internal const string User_ID = "user id";
		}

		// Token: 0x0200031F RID: 799
		private static class SYNONYM
		{
			// Token: 0x040017BB RID: 6075
			internal const string Pwd = "pwd";

			// Token: 0x040017BC RID: 6076
			internal const string UID = "uid";
		}

		// Token: 0x02000320 RID: 800
		private enum ParserState
		{
			// Token: 0x040017BE RID: 6078
			NothingYet = 1,
			// Token: 0x040017BF RID: 6079
			Key,
			// Token: 0x040017C0 RID: 6080
			KeyEqual,
			// Token: 0x040017C1 RID: 6081
			KeyEnd,
			// Token: 0x040017C2 RID: 6082
			UnquotedValue,
			// Token: 0x040017C3 RID: 6083
			DoubleQuoteValue,
			// Token: 0x040017C4 RID: 6084
			DoubleQuoteValueQuote,
			// Token: 0x040017C5 RID: 6085
			SingleQuoteValue,
			// Token: 0x040017C6 RID: 6086
			SingleQuoteValueQuote,
			// Token: 0x040017C7 RID: 6087
			BraceQuoteValue,
			// Token: 0x040017C8 RID: 6088
			BraceQuoteValueQuote,
			// Token: 0x040017C9 RID: 6089
			QuotedValueEnd,
			// Token: 0x040017CA RID: 6090
			NullTermination
		}
	}
}
