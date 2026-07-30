using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Encodes and decodes XML names and provides methods for converting between common language runtime types and XML Schema definition language (XSD) types. When converting data types the values returned are locale independent.</summary>
	// Token: 0x02000289 RID: 649
	public class XmlConvert
	{
		/// <summary>Converts the name to a valid XML name.</summary>
		/// <returns>Returns the name with any invalid characters replaced by an escape string.</returns>
		/// <param name="name">A name to be translated. </param>
		// Token: 0x060017D8 RID: 6104 RVA: 0x0008C27A File Offset: 0x0008A47A
		public static string EncodeName(string name)
		{
			return XmlConvert.EncodeName(name, true, false);
		}

		/// <summary>Verifies the name is valid according to the XML specification.</summary>
		/// <returns>The encoded name.</returns>
		/// <param name="name">The name to be encoded. </param>
		// Token: 0x060017D9 RID: 6105 RVA: 0x0008C284 File Offset: 0x0008A484
		public static string EncodeNmToken(string name)
		{
			return XmlConvert.EncodeName(name, false, false);
		}

		/// <summary>Converts the name to a valid XML local name.</summary>
		/// <returns>The encoded name.</returns>
		/// <param name="name">The name to be encoded. </param>
		// Token: 0x060017DA RID: 6106 RVA: 0x0008C28E File Offset: 0x0008A48E
		public static string EncodeLocalName(string name)
		{
			return XmlConvert.EncodeName(name, true, true);
		}

		/// <summary>Decodes a name. This method does the reverse of the <see cref="M:System.Xml.XmlConvert.EncodeName(System.String)" /> and <see cref="M:System.Xml.XmlConvert.EncodeLocalName(System.String)" /> methods.</summary>
		/// <returns>The decoded name.</returns>
		/// <param name="name">The name to be transformed. </param>
		// Token: 0x060017DB RID: 6107 RVA: 0x0008C298 File Offset: 0x0008A498
		public static string DecodeName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			StringBuilder stringBuilder = null;
			int length = name.Length;
			int num = 0;
			int num2 = name.IndexOf('_');
			if (num2 < 0)
			{
				return name;
			}
			if (XmlConvert.c_DecodeCharPattern == null)
			{
				XmlConvert.c_DecodeCharPattern = new Regex("_[Xx]([0-9a-fA-F]{4}|[0-9a-fA-F]{8})_");
			}
			IEnumerator enumerator = XmlConvert.c_DecodeCharPattern.Matches(name, num2).GetEnumerator();
			int num3 = -1;
			if (enumerator != null && enumerator.MoveNext())
			{
				num3 = ((Match)enumerator.Current).Index;
			}
			for (int i = 0; i < length - XmlConvert.c_EncodedCharLength + 1; i++)
			{
				if (i == num3)
				{
					if (enumerator.MoveNext())
					{
						num3 = ((Match)enumerator.Current).Index;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 20);
					}
					stringBuilder.Append(name, num, i - num);
					if (name[i + 6] != '_')
					{
						int num4 = XmlConvert.FromHex(name[i + 2]) * 268435456 + XmlConvert.FromHex(name[i + 3]) * 16777216 + XmlConvert.FromHex(name[i + 4]) * 1048576 + XmlConvert.FromHex(name[i + 5]) * 65536 + XmlConvert.FromHex(name[i + 6]) * 4096 + XmlConvert.FromHex(name[i + 7]) * 256 + XmlConvert.FromHex(name[i + 8]) * 16 + XmlConvert.FromHex(name[i + 9]);
						if (num4 >= 65536)
						{
							if (num4 <= 1114111)
							{
								num = i + XmlConvert.c_EncodedCharLength + 4;
								char c;
								char c2;
								XmlCharType.SplitSurrogateChar(num4, out c, out c2);
								stringBuilder.Append(c2);
								stringBuilder.Append(c);
							}
						}
						else
						{
							num = i + XmlConvert.c_EncodedCharLength + 4;
							stringBuilder.Append((char)num4);
						}
						i += XmlConvert.c_EncodedCharLength - 1 + 4;
					}
					else
					{
						num = i + XmlConvert.c_EncodedCharLength;
						stringBuilder.Append((char)(XmlConvert.FromHex(name[i + 2]) * 4096 + XmlConvert.FromHex(name[i + 3]) * 256 + XmlConvert.FromHex(name[i + 4]) * 16 + XmlConvert.FromHex(name[i + 5])));
						i += XmlConvert.c_EncodedCharLength - 1;
					}
				}
			}
			if (num == 0)
			{
				return name;
			}
			if (num < length)
			{
				stringBuilder.Append(name, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0008C524 File Offset: 0x0008A724
		private static string EncodeName(string name, bool first, bool local)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			StringBuilder stringBuilder = null;
			int length = name.Length;
			int num = 0;
			int i = 0;
			int num2 = name.IndexOf('_');
			IEnumerator enumerator = null;
			if (num2 >= 0)
			{
				if (XmlConvert.c_EncodeCharPattern == null)
				{
					XmlConvert.c_EncodeCharPattern = new Regex("(?<=_)[Xx]([0-9a-fA-F]{4}|[0-9a-fA-F]{8})_");
				}
				enumerator = XmlConvert.c_EncodeCharPattern.Matches(name, num2).GetEnumerator();
			}
			int num3 = -1;
			if (enumerator != null && enumerator.MoveNext())
			{
				num3 = ((Match)enumerator.Current).Index - 1;
			}
			if (first && ((!XmlConvert.xmlCharType.IsStartNCNameCharXml4e(name[0]) && (local || (!local && name[0] != ':'))) || num3 == 0))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(length + 20);
				}
				stringBuilder.Append("_x");
				if (length > 1 && XmlCharType.IsHighSurrogate((int)name[0]) && XmlCharType.IsLowSurrogate((int)name[1]))
				{
					int num4 = (int)name[0];
					stringBuilder.Append(XmlCharType.CombineSurrogateChar((int)name[1], num4).ToString("X8", CultureInfo.InvariantCulture));
					i++;
					num = 2;
				}
				else
				{
					stringBuilder.Append(((int)name[0]).ToString("X4", CultureInfo.InvariantCulture));
					num = 1;
				}
				stringBuilder.Append("_");
				i++;
				if (num3 == 0 && enumerator.MoveNext())
				{
					num3 = ((Match)enumerator.Current).Index - 1;
				}
			}
			while (i < length)
			{
				if ((local && !XmlConvert.xmlCharType.IsNCNameCharXml4e(name[i])) || (!local && !XmlConvert.xmlCharType.IsNameCharXml4e(name[i])) || num3 == i)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 20);
					}
					if (num3 == i && enumerator.MoveNext())
					{
						num3 = ((Match)enumerator.Current).Index - 1;
					}
					stringBuilder.Append(name, num, i - num);
					stringBuilder.Append("_x");
					if (length > i + 1 && XmlCharType.IsHighSurrogate((int)name[i]) && XmlCharType.IsLowSurrogate((int)name[i + 1]))
					{
						int num5 = (int)name[i];
						stringBuilder.Append(XmlCharType.CombineSurrogateChar((int)name[i + 1], num5).ToString("X8", CultureInfo.InvariantCulture));
						num = i + 2;
						i++;
					}
					else
					{
						stringBuilder.Append(((int)name[i]).ToString("X4", CultureInfo.InvariantCulture));
						num = i + 1;
					}
					stringBuilder.Append("_");
				}
				i++;
			}
			if (num == 0)
			{
				return name;
			}
			if (num < length)
			{
				stringBuilder.Append(name, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0008C7E6 File Offset: 0x0008A9E6
		private static int FromHex(char digit)
		{
			if (digit > '9')
			{
				return (int)(((digit <= 'F') ? (digit - 'A') : (digit - 'a')) + '\n');
			}
			return (int)(digit - '0');
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0008C804 File Offset: 0x0008AA04
		internal static byte[] FromBinHexString(string s)
		{
			return XmlConvert.FromBinHexString(s, true);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0008C80D File Offset: 0x0008AA0D
		internal static byte[] FromBinHexString(string s, bool allowOddCount)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return BinHexDecoder.Decode(s.ToCharArray(), allowOddCount);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0008C829 File Offset: 0x0008AA29
		internal static string ToBinHexString(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return BinHexEncoder.Encode(inArray, 0, inArray.Length);
		}

		/// <summary>Verifies that the name is a valid name according to the W3C Extended Markup Language recommendation.</summary>
		/// <returns>The name, if it is a valid XML name.</returns>
		/// <param name="name">The name to verify. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="name" /> is not a valid XML name. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or String.Empty. </exception>
		// Token: 0x060017E1 RID: 6113 RVA: 0x0008C844 File Offset: 0x0008AA44
		public static string VerifyName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentNullException("name", Res.GetString("The empty string '' is not a valid name."));
			}
			int num = ValidateNames.ParseNameNoNamespaces(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateInvalidNameCharException(name, num, ExceptionType.XmlException);
			}
			return name;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0008C898 File Offset: 0x0008AA98
		internal static Exception TryVerifyName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			int num = ValidateNames.ParseNameNoNamespaces(name, 0);
			if (num != name.Length)
			{
				return new XmlException((num == 0) ? "Name cannot begin with the '{0}' character, hexadecimal value {1}." : "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num));
			}
			return null;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0008C8EE File Offset: 0x0008AAEE
		internal static string VerifyQName(string name)
		{
			return XmlConvert.VerifyQName(name, ExceptionType.XmlException);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0008C8F8 File Offset: 0x0008AAF8
		internal static string VerifyQName(string name, ExceptionType exceptionType)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentNullException("name");
			}
			int num = -1;
			int num2 = ValidateNames.ParseQName(name, 0, out num);
			if (num2 != name.Length)
			{
				throw XmlConvert.CreateException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num2), exceptionType, 0, num2 + 1);
			}
			return name;
		}

		/// <summary>Verifies that the name is a valid NCName according to the W3C Extended Markup Language recommendation. An NCName is a name that cannot contain a colon.</summary>
		/// <returns>The name, if it is a valid NCName.</returns>
		/// <param name="name">The name to verify. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or String.Empty. </exception>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="name" /> is not a valid non-colon name. </exception>
		// Token: 0x060017E5 RID: 6117 RVA: 0x0008C948 File Offset: 0x0008AB48
		public static string VerifyNCName(string name)
		{
			return XmlConvert.VerifyNCName(name, ExceptionType.XmlException);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0008C954 File Offset: 0x0008AB54
		internal static string VerifyNCName(string name, ExceptionType exceptionType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentNullException("name", Res.GetString("The empty string '' is not a valid local name."));
			}
			int num = ValidateNames.ParseNCName(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateInvalidNameCharException(name, num, exceptionType);
			}
			return name;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0008C9A8 File Offset: 0x0008ABA8
		internal static Exception TryVerifyNCName(string name)
		{
			int num = ValidateNames.ParseNCName(name);
			if (num == 0 || num != name.Length)
			{
				return ValidateNames.GetInvalidNameException(name, 0, num);
			}
			return null;
		}

		/// <summary>Verifies that the string is a valid token according to the W3C XML Schema Part2: Datatypes recommendation.</summary>
		/// <returns>The token, if it is a valid token.</returns>
		/// <param name="token">The string value you wish to verify.</param>
		/// <exception cref="T:System.Xml.XmlException">The string value is not a valid token.</exception>
		// Token: 0x060017E8 RID: 6120 RVA: 0x0008C9D4 File Offset: 0x0008ABD4
		public static string VerifyTOKEN(string token)
		{
			if (token == null || token.Length == 0)
			{
				return token;
			}
			if (token[0] == ' ' || token[token.Length - 1] == ' ' || token.IndexOfAny(XmlConvert.crt) != -1 || token.IndexOf("  ", StringComparison.Ordinal) != -1)
			{
				throw new XmlException("line-feed (#xA) or tab (#x9) characters, leading or trailing spaces and sequences of one or more spaces (#x20) are not allowed in 'xs:token'.", token);
			}
			return token;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0008CA38 File Offset: 0x0008AC38
		internal static Exception TryVerifyTOKEN(string token)
		{
			if (token == null || token.Length == 0)
			{
				return null;
			}
			if (token[0] == ' ' || token[token.Length - 1] == ' ' || token.IndexOfAny(XmlConvert.crt) != -1 || token.IndexOf("  ", StringComparison.Ordinal) != -1)
			{
				return new XmlException("line-feed (#xA) or tab (#x9) characters, leading or trailing spaces and sequences of one or more spaces (#x20) are not allowed in 'xs:token'.", token);
			}
			return null;
		}

		/// <summary>Verifies that the string is a valid NMTOKEN according to the W3C XML Schema Part2: Datatypes recommendation</summary>
		/// <returns>The name token, if it is a valid NMTOKEN.</returns>
		/// <param name="name">The string you wish to verify.</param>
		/// <exception cref="T:System.Xml.XmlException">The string is not a valid name token.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x060017EA RID: 6122 RVA: 0x0008CA99 File Offset: 0x0008AC99
		public static string VerifyNMTOKEN(string name)
		{
			return XmlConvert.VerifyNMTOKEN(name, ExceptionType.XmlException);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0008CAA4 File Offset: 0x0008ACA4
		internal static string VerifyNMTOKEN(string name, ExceptionType exceptionType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw XmlConvert.CreateException("Invalid NmToken value '{0}'.", name, exceptionType);
			}
			int num = ValidateNames.ParseNmtokenNoNamespaces(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num), exceptionType, 0, num + 1);
			}
			return name;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0008CB00 File Offset: 0x0008AD00
		internal static Exception TryVerifyNMTOKEN(string name)
		{
			if (name == null || name.Length == 0)
			{
				return new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			int num = ValidateNames.ParseNmtokenNoNamespaces(name, 0);
			if (num != name.Length)
			{
				return new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num));
			}
			return null;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0008CB4C File Offset: 0x0008AD4C
		internal static string VerifyNormalizedString(string str)
		{
			if (str.IndexOfAny(XmlConvert.crt) != -1)
			{
				throw new XmlSchemaException("Carriage return (#xD), line feed (#xA), and tab (#x9) characters are not allowed in xs:normalizedString.", str);
			}
			return str;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0008CB69 File Offset: 0x0008AD69
		internal static Exception TryVerifyNormalizedString(string str)
		{
			if (str.IndexOfAny(XmlConvert.crt) != -1)
			{
				return new XmlSchemaException("Carriage return (#xD), line feed (#xA), and tab (#x9) characters are not allowed in xs:normalizedString.", str);
			}
			return null;
		}

		/// <summary>Returns the passed-in string if all the characters and surrogate pair characters in the string argument are valid XML characters, otherwise null. </summary>
		/// <returns>Returns the passed-in string if all the characters and surrogate-pair characters in the string argument are valid XML characters, otherwise null.</returns>
		/// <param name="content">
		///   <see cref="T:System.String" /> that contains characters to verify.</param>
		// Token: 0x060017EF RID: 6127 RVA: 0x0008CB86 File Offset: 0x0008AD86
		public static string VerifyXmlChars(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			XmlConvert.VerifyCharData(content, ExceptionType.XmlException);
			return content;
		}

		/// <summary>Returns the passed in string instance if all the characters in the string argument are valid public id characters.</summary>
		/// <returns>Returns the passed-in string if all the characters in the argument are valid public id characters.</returns>
		/// <param name="publicId">
		///   <see cref="T:System.String" /> that contains the id to validate.</param>
		// Token: 0x060017F0 RID: 6128 RVA: 0x0008CBA0 File Offset: 0x0008ADA0
		public static string VerifyPublicId(string publicId)
		{
			if (publicId == null)
			{
				throw new ArgumentNullException("publicId");
			}
			int num = XmlConvert.xmlCharType.IsPublicId(publicId);
			if (num != -1)
			{
				throw XmlConvert.CreateInvalidCharException(publicId, num, ExceptionType.XmlException);
			}
			return publicId;
		}

		/// <summary>Returns the passed-in string instance if all the characters in the string argument are valid whitespace characters. </summary>
		/// <returns>Returns the passed-in string instance if all the characters in the string argument are valid whitespace characters, otherwise null.</returns>
		/// <param name="content">
		///   <see cref="T:System.String" /> to verify.</param>
		// Token: 0x060017F1 RID: 6129 RVA: 0x0008CBD8 File Offset: 0x0008ADD8
		public static string VerifyWhitespace(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			int num = XmlConvert.xmlCharType.IsOnlyWhitespaceWithPos(content);
			if (num != -1)
			{
				throw new XmlException("The Whitespace or SignificantWhitespace node can contain only XML white space characters. '{0}' is not an XML white space character.", XmlException.BuildCharExceptionArgs(content, num), 0, num + 1);
			}
			return content;
		}

		/// <summary>Checks if the passed-in character is a valid Start Name Character type.</summary>
		/// <returns>true if the character is a valid Start Name Character type; otherwise, false. </returns>
		/// <param name="ch">The character to validate.</param>
		// Token: 0x060017F2 RID: 6130 RVA: 0x0008CC1A File Offset: 0x0008AE1A
		public static bool IsStartNCNameChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 4) > 0;
		}

		/// <summary>Checks whether the passed-in character is a valid non-colon character type.</summary>
		/// <returns>Returns true if the character is a valid non-colon character type; otherwise, false.</returns>
		/// <param name="ch">The character to verify as a non-colon character.</param>
		// Token: 0x060017F3 RID: 6131 RVA: 0x0008CC2D File Offset: 0x0008AE2D
		public static bool IsNCNameChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 8) > 0;
		}

		/// <summary>Checks if the passed-in character is a valid XML character.</summary>
		/// <returns>true if the passed in character is a valid XML character; otherwise false.</returns>
		/// <param name="ch">The character to validate.</param>
		// Token: 0x060017F4 RID: 6132 RVA: 0x0008CC40 File Offset: 0x0008AE40
		public static bool IsXmlChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 16) > 0;
		}

		/// <summary>Checks if the passed-in surrogate pair of characters is a valid XML character.</summary>
		/// <returns>true if the passed in surrogate pair of characters is a valid XML character; otherwise false.</returns>
		/// <param name="lowChar">The surrogate character to validate.</param>
		/// <param name="highChar">The surrogate character to validate.</param>
		// Token: 0x060017F5 RID: 6133 RVA: 0x0008CC54 File Offset: 0x0008AE54
		public static bool IsXmlSurrogatePair(char lowChar, char highChar)
		{
			return XmlCharType.IsHighSurrogate((int)highChar) && XmlCharType.IsLowSurrogate((int)lowChar);
		}

		/// <summary>Returns the passed-in character instance if the character in the argument is a valid public id character, otherwise null.</summary>
		/// <returns>Returns the passed-in character if the character is a valid public id character, otherwise null.</returns>
		/// <param name="ch">
		///   <see cref="T:System.Char" /> object to validate.</param>
		// Token: 0x060017F6 RID: 6134 RVA: 0x0008CC66 File Offset: 0x0008AE66
		public static bool IsPublicIdChar(char ch)
		{
			return XmlConvert.xmlCharType.IsPubidChar(ch);
		}

		/// <summary>Checks if the passed-in character is a valid XML whitespace character.</summary>
		/// <returns>true if the passed in character is a valid XML whitespace character; otherwise false.</returns>
		/// <param name="ch">The character to validate.</param>
		// Token: 0x060017F7 RID: 6135 RVA: 0x0008CC73 File Offset: 0x0008AE73
		public static bool IsWhitespaceChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 1) > 0;
		}

		/// <summary>Converts the <see cref="T:System.Boolean" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Boolean, that is, "true" or "false".</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x060017F8 RID: 6136 RVA: 0x000070C5 File Offset: 0x000052C5
		public static string ToString(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		/// <summary>Converts the <see cref="T:System.Char" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Char.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x060017F9 RID: 6137 RVA: 0x0008CC86 File Offset: 0x0008AE86
		public static string ToString(char value)
		{
			return value.ToString(null);
		}

		/// <summary>Converts the <see cref="T:System.Decimal" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Decimal.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x060017FA RID: 6138 RVA: 0x0008CC90 File Offset: 0x0008AE90
		public static string ToString(decimal value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.SByte" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the SByte.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x060017FB RID: 6139 RVA: 0x0008CC9F File Offset: 0x0008AE9F
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Int16" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Int16.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x060017FC RID: 6140 RVA: 0x0008CCAE File Offset: 0x0008AEAE
		public static string ToString(short value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Int32" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Int32.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x060017FD RID: 6141 RVA: 0x0008CCBD File Offset: 0x0008AEBD
		public static string ToString(int value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Int64" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Int64.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x060017FE RID: 6142 RVA: 0x0008CCCC File Offset: 0x0008AECC
		public static string ToString(long value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Byte" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Byte.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x060017FF RID: 6143 RVA: 0x0008CCDB File Offset: 0x0008AEDB
		public static string ToString(byte value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.UInt16" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the UInt16.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06001800 RID: 6144 RVA: 0x0008CCEA File Offset: 0x0008AEEA
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.UInt32" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the UInt32.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06001801 RID: 6145 RVA: 0x0008CCF9 File Offset: 0x0008AEF9
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.UInt64" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the UInt64.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06001802 RID: 6146 RVA: 0x0008CD08 File Offset: 0x0008AF08
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Single" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Single.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06001803 RID: 6147 RVA: 0x0008CD17 File Offset: 0x0008AF17
		public static string ToString(float value)
		{
			if (float.IsNegativeInfinity(value))
			{
				return "-INF";
			}
			if (float.IsPositiveInfinity(value))
			{
				return "INF";
			}
			if (XmlConvert.IsNegativeZero((double)value))
			{
				return "-0";
			}
			return value.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Double" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Double.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06001804 RID: 6148 RVA: 0x0008CD55 File Offset: 0x0008AF55
		public static string ToString(double value)
		{
			if (double.IsNegativeInfinity(value))
			{
				return "-INF";
			}
			if (double.IsPositiveInfinity(value))
			{
				return "INF";
			}
			if (XmlConvert.IsNegativeZero(value))
			{
				return "-0";
			}
			return value.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the TimeSpan.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06001805 RID: 6149 RVA: 0x0008CD94 File Offset: 0x0008AF94
		public static string ToString(TimeSpan value)
		{
			return new XsdDuration(value).ToString();
		}

		/// <summary>Converts the <see cref="T:System.DateTime" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the DateTime in the format yyyy-MM-ddTHH:mm:ss where 'T' is a constant literal.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06001806 RID: 6150 RVA: 0x0008CDB5 File Offset: 0x0008AFB5
		[Obsolete("Use XmlConvert.ToString() that takes in XmlDateTimeSerializationMode")]
		public static string ToString(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
		}

		/// <summary>Converts the <see cref="T:System.DateTime" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the DateTime in the specified format.</returns>
		/// <param name="value">The value to convert. </param>
		/// <param name="format">The format structure that defines how to display the converted string. Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. </param>
		// Token: 0x06001807 RID: 6151 RVA: 0x0008CDC2 File Offset: 0x0008AFC2
		public static string ToString(DateTime value, string format)
		{
			return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.DateTime" /> to a <see cref="T:System.String" /> using the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> specified.</summary>
		/// <returns>A <see cref="T:System.String" /> equivalent of the <see cref="T:System.DateTime" />.</returns>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to convert.</param>
		/// <param name="dateTimeOption">One of the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> values that specify how to treat the <see cref="T:System.DateTime" /> value.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="dateTimeOption" /> value is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> or <paramref name="dateTimeOption" /> value is null.</exception>
		// Token: 0x06001808 RID: 6152 RVA: 0x0008CDD4 File Offset: 0x0008AFD4
		public static string ToString(DateTime value, XmlDateTimeSerializationMode dateTimeOption)
		{
			switch (dateTimeOption)
			{
			case XmlDateTimeSerializationMode.Local:
				value = XmlConvert.SwitchToLocalTime(value);
				break;
			case XmlDateTimeSerializationMode.Utc:
				value = XmlConvert.SwitchToUtcTime(value);
				break;
			case XmlDateTimeSerializationMode.Unspecified:
				value = new DateTime(value.Ticks, DateTimeKind.Unspecified);
				break;
			case XmlDateTimeSerializationMode.RoundtripKind:
				break;
			default:
				throw new ArgumentException(Res.GetString("The '{0}' value for the 'dateTimeOption' parameter is not an allowed value for the 'XmlDateTimeSerializationMode' enumeration.", new object[] { dateTimeOption, "dateTimeOption" }));
			}
			XsdDateTime xsdDateTime = new XsdDateTime(value, XsdDateTimeFlags.DateTime);
			return xsdDateTime.ToString();
		}

		/// <summary>Converts the supplied <see cref="T:System.DateTimeOffset" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representation of the supplied <see cref="T:System.DateTimeOffset" />.</returns>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> to be converted.</param>
		// Token: 0x06001809 RID: 6153 RVA: 0x0008CE5C File Offset: 0x0008B05C
		public static string ToString(DateTimeOffset value)
		{
			XsdDateTime xsdDateTime = new XsdDateTime(value);
			return xsdDateTime.ToString();
		}

		/// <summary>Converts the supplied <see cref="T:System.DateTimeOffset" /> to a <see cref="T:System.String" /> in the specified format.</summary>
		/// <returns>A <see cref="T:System.String" /> representation in the specified format of the supplied <see cref="T:System.DateTimeOffset" />.</returns>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> to be converted.</param>
		/// <param name="format">The format to which <paramref name="s" /> is converted. The format parameter can be any subset of the W3C Recommendation for the XML dateTime type. (For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.)</param>
		// Token: 0x0600180A RID: 6154 RVA: 0x0008CE7E File Offset: 0x0008B07E
		public static string ToString(DateTimeOffset value, string format)
		{
			return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Guid" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Guid.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x0600180B RID: 6155 RVA: 0x0008CE8D File Offset: 0x0008B08D
		public static string ToString(Guid value)
		{
			return value.ToString();
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Boolean" /> equivalent.</summary>
		/// <returns>A Boolean value, that is, true or false.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a Boolean value. </exception>
		// Token: 0x0600180C RID: 6156 RVA: 0x0008CE9C File Offset: 0x0008B09C
		public static bool ToBoolean(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "1" || s == "true")
			{
				return true;
			}
			if (s == "0" || s == "false")
			{
				return false;
			}
			throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Boolean" }));
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0008CF0C File Offset: 0x0008B10C
		internal static Exception TryToBoolean(string s, out bool result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "0" || s == "false")
			{
				result = false;
				return null;
			}
			if (s == "1" || s == "true")
			{
				result = true;
				return null;
			}
			result = false;
			return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Boolean" }));
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Char" /> equivalent.</summary>
		/// <returns>A Char representing the single character.</returns>
		/// <param name="s">The string containing a single character to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="s" /> parameter is null. </exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter contains more than one character. </exception>
		// Token: 0x0600180E RID: 6158 RVA: 0x0008CF83 File Offset: 0x0008B183
		public static char ToChar(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(Res.GetString("String must be exactly one character long."));
			}
			return s[0];
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0008CFB3 File Offset: 0x0008B1B3
		internal static Exception TryToChar(string s, out char result)
		{
			if (!char.TryParse(s, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Char" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Decimal" /> equivalent.</summary>
		/// <returns>A Decimal equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		// Token: 0x06001810 RID: 6160 RVA: 0x0008CFE1 File Offset: 0x0008B1E1
		public static decimal ToDecimal(string s)
		{
			return decimal.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0008CFF0 File Offset: 0x0008B1F0
		internal static Exception TryToDecimal(string s, out decimal result)
		{
			if (!decimal.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Decimal" }));
			}
			return null;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0008D025 File Offset: 0x0008B225
		internal static decimal ToInteger(string s)
		{
			return decimal.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0008D033 File Offset: 0x0008B233
		internal static Exception TryToInteger(string s, out decimal result)
		{
			if (!decimal.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Integer" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.SByte" /> equivalent.</summary>
		/// <returns>An SByte equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		// Token: 0x06001814 RID: 6164 RVA: 0x0008D067 File Offset: 0x0008B267
		[CLSCompliant(false)]
		public static sbyte ToSByte(string s)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0008D075 File Offset: 0x0008B275
		internal static Exception TryToSByte(string s, out sbyte result)
		{
			if (!sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "SByte" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Int16" /> equivalent.</summary>
		/// <returns>An Int16 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		// Token: 0x06001816 RID: 6166 RVA: 0x0008D0A9 File Offset: 0x0008B2A9
		public static short ToInt16(string s)
		{
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0008D0B7 File Offset: 0x0008B2B7
		internal static Exception TryToInt16(string s, out short result)
		{
			if (!short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Int16" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Int32" /> equivalent.</summary>
		/// <returns>An Int32 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x06001818 RID: 6168 RVA: 0x0008D0EB File Offset: 0x0008B2EB
		public static int ToInt32(string s)
		{
			return int.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0008D0F9 File Offset: 0x0008B2F9
		internal static Exception TryToInt32(string s, out int result)
		{
			if (!int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Int32" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Int64" /> equivalent.</summary>
		/// <returns>An Int64 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		// Token: 0x0600181A RID: 6170 RVA: 0x0008D12D File Offset: 0x0008B32D
		public static long ToInt64(string s)
		{
			return long.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0008D13B File Offset: 0x0008B33B
		internal static Exception TryToInt64(string s, out long result)
		{
			if (!long.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Int64" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Byte" /> equivalent.</summary>
		/// <returns>A Byte equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		// Token: 0x0600181C RID: 6172 RVA: 0x0008D16F File Offset: 0x0008B36F
		public static byte ToByte(string s)
		{
			return byte.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0008D17D File Offset: 0x0008B37D
		internal static Exception TryToByte(string s, out byte result)
		{
			if (!byte.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Byte" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.UInt16" /> equivalent.</summary>
		/// <returns>A UInt16 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		// Token: 0x0600181E RID: 6174 RVA: 0x0008D1B1 File Offset: 0x0008B3B1
		[CLSCompliant(false)]
		public static ushort ToUInt16(string s)
		{
			return ushort.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0008D1BF File Offset: 0x0008B3BF
		internal static Exception TryToUInt16(string s, out ushort result)
		{
			if (!ushort.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "UInt16" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.UInt32" /> equivalent.</summary>
		/// <returns>A UInt32 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		// Token: 0x06001820 RID: 6176 RVA: 0x0008D1F3 File Offset: 0x0008B3F3
		[CLSCompliant(false)]
		public static uint ToUInt32(string s)
		{
			return uint.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0008D201 File Offset: 0x0008B401
		internal static Exception TryToUInt32(string s, out uint result)
		{
			if (!uint.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "UInt32" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.UInt64" /> equivalent.</summary>
		/// <returns>A UInt64 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		// Token: 0x06001822 RID: 6178 RVA: 0x0008D235 File Offset: 0x0008B435
		[CLSCompliant(false)]
		public static ulong ToUInt64(string s)
		{
			return ulong.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0008D243 File Offset: 0x0008B443
		internal static Exception TryToUInt64(string s, out ulong result)
		{
			if (!ulong.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "UInt64" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Single" /> equivalent.</summary>
		/// <returns>A Single equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		// Token: 0x06001824 RID: 6180 RVA: 0x0008D278 File Offset: 0x0008B478
		public static float ToSingle(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				return float.NegativeInfinity;
			}
			if (s == "INF")
			{
				return float.PositiveInfinity;
			}
			float num = float.Parse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo);
			if (num == 0f && s[0] == '-')
			{
				return -0f;
			}
			return num;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0008D2E0 File Offset: 0x0008B4E0
		internal static Exception TryToSingle(string s, out float result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				result = float.NegativeInfinity;
				return null;
			}
			if (s == "INF")
			{
				result = float.PositiveInfinity;
				return null;
			}
			if (!float.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Single" }));
			}
			if (result == 0f && s[0] == '-')
			{
				result = -0f;
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Double" /> equivalent.</summary>
		/// <returns>A Double equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		// Token: 0x06001826 RID: 6182 RVA: 0x0008D374 File Offset: 0x0008B574
		public static double ToDouble(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				return double.NegativeInfinity;
			}
			if (s == "INF")
			{
				return double.PositiveInfinity;
			}
			double num = double.Parse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo);
			if (num == 0.0 && s[0] == '-')
			{
				return -0.0;
			}
			return num;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0008D3EC File Offset: 0x0008B5EC
		internal static Exception TryToDouble(string s, out double result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				result = double.NegativeInfinity;
				return null;
			}
			if (s == "INF")
			{
				result = double.PositiveInfinity;
				return null;
			}
			if (!double.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Double" }));
			}
			if (result == 0.0 && s[0] == '-')
			{
				result = -0.0;
			}
			return null;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0008D490 File Offset: 0x0008B690
		internal static double ToXPathDouble(object o)
		{
			string text = o as string;
			if (text != null)
			{
				text = XmlConvert.TrimString(text);
				double num;
				if (text.Length != 0 && text[0] != '+' && double.TryParse(text, NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				return double.NaN;
			}
			else
			{
				if (o is double)
				{
					return (double)o;
				}
				if (!(o is bool))
				{
					try
					{
						return Convert.ToDouble(o, NumberFormatInfo.InvariantInfo);
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					catch (ArgumentNullException)
					{
					}
					return double.NaN;
				}
				if (!(bool)o)
				{
					return 0.0;
				}
				return 1.0;
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0008D55C File Offset: 0x0008B75C
		internal static string ToXPathString(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			if (value is double)
			{
				return ((double)value).ToString("R", NumberFormatInfo.InvariantInfo);
			}
			if (!(value is bool))
			{
				return Convert.ToString(value, NumberFormatInfo.InvariantInfo);
			}
			if (!(bool)value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0008D5C0 File Offset: 0x0008B7C0
		internal static double XPathRound(double value)
		{
			double num = Math.Round(value);
			if (value - num != 0.5)
			{
				return num;
			}
			return num + 1.0;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" /> equivalent.</summary>
		/// <returns>A TimeSpan equivalent of the string.</returns>
		/// <param name="s">The string to convert. The string format must conform to the W3C XML Schema Part 2: Datatypes recommendation for duration.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in correct format to represent a TimeSpan value. </exception>
		// Token: 0x0600182B RID: 6187 RVA: 0x0008D5F0 File Offset: 0x0008B7F0
		public static TimeSpan ToTimeSpan(string s)
		{
			XsdDuration xsdDuration;
			try
			{
				xsdDuration = new XsdDuration(s);
			}
			catch (Exception)
			{
				throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "TimeSpan" }));
			}
			return xsdDuration.ToTimeSpan();
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0008D640 File Offset: 0x0008B840
		internal static Exception TryToTimeSpan(string s, out TimeSpan result)
		{
			XsdDuration xsdDuration;
			Exception ex = XsdDuration.TryParse(s, out xsdDuration);
			if (ex != null)
			{
				result = TimeSpan.MinValue;
				return ex;
			}
			return xsdDuration.TryToTimeSpan(out result);
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x0008D66E File Offset: 0x0008B86E
		private static string[] AllDateTimeFormats
		{
			get
			{
				if (XmlConvert.s_allDateTimeFormats == null)
				{
					XmlConvert.CreateAllDateTimeFormats();
				}
				return XmlConvert.s_allDateTimeFormats;
			}
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0008D688 File Offset: 0x0008B888
		private static void CreateAllDateTimeFormats()
		{
			if (XmlConvert.s_allDateTimeFormats == null)
			{
				XmlConvert.s_allDateTimeFormats = new string[]
				{
					"yyyy-MM-ddTHH:mm:ss.FFFFFFFzzzzzz", "yyyy-MM-ddTHH:mm:ss.FFFFFFF", "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ", "HH:mm:ss.FFFFFFF", "HH:mm:ss.FFFFFFFZ", "HH:mm:ss.FFFFFFFzzzzzz", "yyyy-MM-dd", "yyyy-MM-ddZ", "yyyy-MM-ddzzzzzz", "yyyy-MM",
					"yyyy-MMZ", "yyyy-MMzzzzzz", "yyyy", "yyyyZ", "yyyyzzzzzz", "--MM-dd", "--MM-ddZ", "--MM-ddzzzzzz", "---dd", "---ddZ",
					"---ddzzzzzz", "--MM--", "--MM--Z", "--MM--zzzzzz"
				};
			}
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <returns>A DateTime equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is an empty string or is not in the correct format. </exception>
		// Token: 0x0600182F RID: 6191 RVA: 0x0008D77E File Offset: 0x0008B97E
		[Obsolete("Use XmlConvert.ToDateTime() that takes in XmlDateTimeSerializationMode")]
		public static DateTime ToDateTime(string s)
		{
			return XmlConvert.ToDateTime(s, XmlConvert.AllDateTimeFormats);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <returns>A DateTime equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <param name="format">The format structure to apply to the converted DateTime. Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. The string is validated against this format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is String.Empty -or- <paramref name="s" /> does not contain a date and time that corresponds to <paramref name="format" />. </exception>
		// Token: 0x06001830 RID: 6192 RVA: 0x0008D78B File Offset: 0x0008B98B
		public static DateTime ToDateTime(string s, string format)
		{
			return DateTime.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <returns>A DateTime equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <param name="formats">An array containing the format structures to apply to the converted DateTime. Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or an element of <paramref name="formats" /> is String.Empty -or- <paramref name="s" /> does not contain a date and time that corresponds to any of the elements of <paramref name="formats" />. </exception>
		// Token: 0x06001831 RID: 6193 RVA: 0x0008D79A File Offset: 0x0008B99A
		public static DateTime ToDateTime(string s, string[] formats)
		{
			return DateTime.ParseExact(s, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> using the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> specified</summary>
		/// <returns>A <see cref="T:System.DateTime" /> equivalent of the <see cref="T:System.String" />.</returns>
		/// <param name="s">The <see cref="T:System.String" /> value to convert.</param>
		/// <param name="dateTimeOption">One of the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> values that specify whether the date should be converted to local time or preserved as Coordinated Universal Time (UTC), if it is a UTC date.</param>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="s" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dateTimeOption" /> value is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is an empty string or is not in a valid format.</exception>
		// Token: 0x06001832 RID: 6194 RVA: 0x0008D7AC File Offset: 0x0008B9AC
		public static DateTime ToDateTime(string s, XmlDateTimeSerializationMode dateTimeOption)
		{
			DateTime dateTime = new XsdDateTime(s, XsdDateTimeFlags.AllXsd);
			switch (dateTimeOption)
			{
			case XmlDateTimeSerializationMode.Local:
				dateTime = XmlConvert.SwitchToLocalTime(dateTime);
				break;
			case XmlDateTimeSerializationMode.Utc:
				dateTime = XmlConvert.SwitchToUtcTime(dateTime);
				break;
			case XmlDateTimeSerializationMode.Unspecified:
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
				break;
			case XmlDateTimeSerializationMode.RoundtripKind:
				break;
			default:
				throw new ArgumentException(Res.GetString("The '{0}' value for the 'dateTimeOption' parameter is not an allowed value for the 'XmlDateTimeSerializationMode' enumeration.", new object[] { dateTimeOption, "dateTimeOption" }));
			}
			return dateTime;
		}

		/// <summary>Converts the supplied <see cref="T:System.String" /> to a <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <returns>The <see cref="T:System.DateTimeOffset" /> equivalent of the supplied string.</returns>
		/// <param name="s">The string to convert.Note   The string must conform to a subset of the W3C Recommendation for the XML dateTime type. For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The argument passed to this method is outside the range of allowable values. For information about allowable values, see <see cref="T:System.DateTimeOffset" />.</exception>
		/// <exception cref="T:System.FormatException">The argument passed to this method does not conform to a subset of the W3C Recommendations for the XML dateTime type. For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.</exception>
		// Token: 0x06001833 RID: 6195 RVA: 0x0008D82D File Offset: 0x0008BA2D
		public static DateTimeOffset ToDateTimeOffset(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return new XsdDateTime(s, XsdDateTimeFlags.AllXsd);
		}

		/// <summary>Converts the supplied <see cref="T:System.String" /> to a <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <returns>The <see cref="T:System.DateTimeOffset" /> equivalent of the supplied string.</returns>
		/// <param name="s">The string to convert.</param>
		/// <param name="format">The format from which <paramref name="s" /> is converted. The format parameter can be any subset of the W3C Recommendation for the XML dateTime type. (For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.) The string <paramref name="s" /> is validated against this format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string or is not in the specified format.</exception>
		// Token: 0x06001834 RID: 6196 RVA: 0x0008D84D File Offset: 0x0008BA4D
		public static DateTimeOffset ToDateTimeOffset(string s, string format)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return DateTimeOffset.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the supplied <see cref="T:System.String" /> to a <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <returns>The <see cref="T:System.DateTimeOffset" /> equivalent of the supplied string.</returns>
		/// <param name="s">The string to convert.</param>
		/// <param name="formats">An array of formats from which <paramref name="s" /> can be converted. Each format in <paramref name="formats" /> can be any subset of the W3C Recommendation for the XML dateTime type. (For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.) The string <paramref name="s" /> is validated against one of these formats.</param>
		// Token: 0x06001835 RID: 6197 RVA: 0x0008D86A File Offset: 0x0008BA6A
		public static DateTimeOffset ToDateTimeOffset(string s, string[] formats)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return DateTimeOffset.ParseExact(s, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Guid" /> equivalent.</summary>
		/// <returns>A Guid equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		// Token: 0x06001836 RID: 6198 RVA: 0x0008D887 File Offset: 0x0008BA87
		public static Guid ToGuid(string s)
		{
			return new Guid(s);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0008D890 File Offset: 0x0008BA90
		internal static Exception TryToGuid(string s, out Guid result)
		{
			Exception ex = null;
			result = Guid.Empty;
			try
			{
				result = new Guid(s);
			}
			catch (ArgumentException)
			{
				ex = new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Guid" }));
			}
			catch (FormatException)
			{
				ex = new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Guid" }));
			}
			return ex;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0008D920 File Offset: 0x0008BB20
		private static DateTime SwitchToLocalTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Local);
			case DateTimeKind.Utc:
				return value.ToLocalTime();
			case DateTimeKind.Local:
				return value;
			default:
				return value;
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0008D964 File Offset: 0x0008BB64
		private static DateTime SwitchToUtcTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Utc);
			case DateTimeKind.Utc:
				return value;
			case DateTimeKind.Local:
				return value.ToUniversalTime();
			default:
				return value;
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0008D9A8 File Offset: 0x0008BBA8
		internal static Uri ToUri(string s)
		{
			if (s != null && s.Length > 0)
			{
				s = XmlConvert.TrimString(s);
				if (s.Length == 0 || s.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Uri" }));
				}
			}
			Uri uri;
			if (!Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out uri))
			{
				throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Uri" }));
			}
			return uri;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0008DA30 File Offset: 0x0008BC30
		internal static Exception TryToUri(string s, out Uri result)
		{
			result = null;
			if (s != null && s.Length > 0)
			{
				s = XmlConvert.TrimString(s);
				if (s.Length == 0 || s.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Uri" }));
				}
			}
			if (!Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Uri" }));
			}
			return null;
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0008DABC File Offset: 0x0008BCBC
		internal static bool StrEqual(char[] chars, int strPos1, int strLen1, string str2)
		{
			if (strLen1 != str2.Length)
			{
				return false;
			}
			int num = 0;
			while (num < strLen1 && chars[strPos1 + num] == str2[num])
			{
				num++;
			}
			return num == strLen1;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0008DAF2 File Offset: 0x0008BCF2
		internal static string TrimString(string value)
		{
			return value.Trim(XmlConvert.WhitespaceChars);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0008DAFF File Offset: 0x0008BCFF
		internal static string TrimStringStart(string value)
		{
			return value.TrimStart(XmlConvert.WhitespaceChars);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0008DB0C File Offset: 0x0008BD0C
		internal static string TrimStringEnd(string value)
		{
			return value.TrimEnd(XmlConvert.WhitespaceChars);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0008DB19 File Offset: 0x0008BD19
		internal static string[] SplitString(string value)
		{
			return value.Split(XmlConvert.WhitespaceChars, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0008DB27 File Offset: 0x0008BD27
		internal static string[] SplitString(string value, StringSplitOptions splitStringOptions)
		{
			return value.Split(XmlConvert.WhitespaceChars, splitStringOptions);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0008DB35 File Offset: 0x0008BD35
		internal static bool IsNegativeZero(double value)
		{
			return value == 0.0 && XmlConvert.DoubleToInt64Bits(value) == XmlConvert.DoubleToInt64Bits(-0.0);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0008DB5C File Offset: 0x0008BD5C
		private unsafe static long DoubleToInt64Bits(double value)
		{
			return *(long*)(&value);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0008DB62 File Offset: 0x0008BD62
		internal static void VerifyCharData(string data, ExceptionType exceptionType)
		{
			XmlConvert.VerifyCharData(data, exceptionType, exceptionType);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0008DB6C File Offset: 0x0008BD6C
		internal static void VerifyCharData(string data, ExceptionType invCharExceptionType, ExceptionType invSurrogateExceptionType)
		{
			if (data == null || data.Length == 0)
			{
				return;
			}
			int num = 0;
			int length = data.Length;
			for (;;)
			{
				if (num >= length || (XmlConvert.xmlCharType.charProperties[(int)data[num]] & 16) == 0)
				{
					if (num == length)
					{
						break;
					}
					if (!XmlCharType.IsHighSurrogate((int)data[num]))
					{
						goto IL_0090;
					}
					if (num + 1 == length)
					{
						goto Block_5;
					}
					if (!XmlCharType.IsLowSurrogate((int)data[num + 1]))
					{
						goto IL_0075;
					}
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return;
			Block_5:
			throw XmlConvert.CreateException("The surrogate pair is invalid. Missing a low surrogate character.", invSurrogateExceptionType, 0, num + 1);
			IL_0075:
			throw XmlConvert.CreateInvalidSurrogatePairException(data[num + 1], data[num], invSurrogateExceptionType, 0, num + 1);
			IL_0090:
			throw XmlConvert.CreateInvalidCharException(data, num, invCharExceptionType);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0008DC14 File Offset: 0x0008BE14
		internal static void VerifyCharData(char[] data, int offset, int len, ExceptionType exceptionType)
		{
			if (data == null || len == 0)
			{
				return;
			}
			int num = offset;
			int num2 = offset + len;
			for (;;)
			{
				if (num >= num2 || (XmlConvert.xmlCharType.charProperties[(int)data[num]] & 16) == 0)
				{
					if (num == num2)
					{
						break;
					}
					if (!XmlCharType.IsHighSurrogate((int)data[num]))
					{
						goto IL_0078;
					}
					if (num + 1 == num2)
					{
						goto Block_5;
					}
					if (!XmlCharType.IsLowSurrogate((int)data[num + 1]))
					{
						goto IL_0063;
					}
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return;
			Block_5:
			throw XmlConvert.CreateException("The surrogate pair is invalid. Missing a low surrogate character.", exceptionType, 0, offset - num + 1);
			IL_0063:
			throw XmlConvert.CreateInvalidSurrogatePairException(data[num + 1], data[num], exceptionType, 0, offset - num + 1);
			IL_0078:
			throw XmlConvert.CreateInvalidCharException(data, len, num, exceptionType);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0008DCA4 File Offset: 0x0008BEA4
		internal static string EscapeValueForDebuggerDisplay(string value)
		{
			StringBuilder stringBuilder = null;
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c < ' ' || c == '"')
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(value.Length + 4);
					}
					if (i - num > 0)
					{
						stringBuilder.Append(value, num, i - num);
					}
					num = i + 1;
					switch (c)
					{
					case '\t':
						stringBuilder.Append("\\t");
						goto IL_00A9;
					case '\n':
						stringBuilder.Append("\\n");
						goto IL_00A9;
					case '\v':
					case '\f':
						break;
					case '\r':
						stringBuilder.Append("\\r");
						goto IL_00A9;
					default:
						if (c == '"')
						{
							stringBuilder.Append("\\\"");
							goto IL_00A9;
						}
						break;
					}
					stringBuilder.Append(c);
				}
				IL_00A9:
				i++;
			}
			if (stringBuilder == null)
			{
				return value;
			}
			if (i - num > 0)
			{
				stringBuilder.Append(value, num, i - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0008DD87 File Offset: 0x0008BF87
		internal static Exception CreateException(string res, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, exceptionType, 0, 0);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0008DD92 File Offset: 0x0008BF92
		internal static Exception CreateException(string res, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, string.Empty, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res));
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0008DDB6 File Offset: 0x0008BFB6
		internal static Exception CreateException(string res, string arg, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, arg, exceptionType, 0, 0);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0008DDC2 File Offset: 0x0008BFC2
		internal static Exception CreateException(string res, string arg, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, arg, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res, new object[] { arg }));
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0008DDED File Offset: 0x0008BFED
		internal static Exception CreateException(string res, string[] args, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, args, exceptionType, 0, 0);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0008DDF9 File Offset: 0x0008BFF9
		internal static Exception CreateException(string res, string[] args, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, args, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res, args));
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0008DE1B File Offset: 0x0008C01B
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi)
		{
			return XmlConvert.CreateInvalidSurrogatePairException(low, hi, ExceptionType.ArgumentException);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0008DE25 File Offset: 0x0008C025
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi, ExceptionType exceptionType)
		{
			return XmlConvert.CreateInvalidSurrogatePairException(low, hi, exceptionType, 0, 0);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0008DE34 File Offset: 0x0008C034
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi, ExceptionType exceptionType, int lineNo, int linePos)
		{
			string[] array = new string[2];
			int num = 0;
			uint num2 = (uint)hi;
			array[num] = num2.ToString("X", CultureInfo.InvariantCulture);
			int num3 = 1;
			num2 = (uint)low;
			array[num3] = num2.ToString("X", CultureInfo.InvariantCulture);
			string[] array2 = array;
			return XmlConvert.CreateException("The surrogate pair (0x{0}, 0x{1}) is invalid. A high surrogate character (0xD800 - 0xDBFF) must always be paired with a low surrogate character (0xDC00 - 0xDFFF).", array2, exceptionType, lineNo, linePos);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0008DE83 File Offset: 0x0008C083
		internal static Exception CreateInvalidHighSurrogateCharException(char hi)
		{
			return XmlConvert.CreateInvalidHighSurrogateCharException(hi, ExceptionType.ArgumentException);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0008DE8C File Offset: 0x0008C08C
		internal static Exception CreateInvalidHighSurrogateCharException(char hi, ExceptionType exceptionType)
		{
			return XmlConvert.CreateInvalidHighSurrogateCharException(hi, exceptionType, 0, 0);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0008DE98 File Offset: 0x0008C098
		internal static Exception CreateInvalidHighSurrogateCharException(char hi, ExceptionType exceptionType, int lineNo, int linePos)
		{
			string text = "Invalid high surrogate character (0x{0}). A high surrogate character must have a value from range (0xD800 - 0xDBFF).";
			uint num = (uint)hi;
			return XmlConvert.CreateException(text, num.ToString("X", CultureInfo.InvariantCulture), exceptionType, lineNo, linePos);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0008DEC5 File Offset: 0x0008C0C5
		internal static Exception CreateInvalidCharException(char[] data, int length, int invCharPos)
		{
			return XmlConvert.CreateInvalidCharException(data, length, invCharPos, ExceptionType.ArgumentException);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0008DED0 File Offset: 0x0008C0D0
		internal static Exception CreateInvalidCharException(char[] data, int length, int invCharPos, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(data, length, invCharPos), exceptionType, 0, invCharPos + 1);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0008DEE9 File Offset: 0x0008C0E9
		internal static Exception CreateInvalidCharException(string data, int invCharPos)
		{
			return XmlConvert.CreateInvalidCharException(data, invCharPos, ExceptionType.ArgumentException);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0008DEF3 File Offset: 0x0008C0F3
		internal static Exception CreateInvalidCharException(string data, int invCharPos, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(data, invCharPos), exceptionType, 0, invCharPos + 1);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0008DF0B File Offset: 0x0008C10B
		internal static Exception CreateInvalidCharException(char invChar, char nextChar)
		{
			return XmlConvert.CreateInvalidCharException(invChar, nextChar, ExceptionType.ArgumentException);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0008DF15 File Offset: 0x0008C115
		internal static Exception CreateInvalidCharException(char invChar, char nextChar, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(invChar, nextChar), exceptionType);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0008DF29 File Offset: 0x0008C129
		internal static Exception CreateInvalidNameCharException(string name, int index, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException((index == 0) ? "Name cannot begin with the '{0}' character, hexadecimal value {1}." : "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, index), exceptionType, 0, index + 1);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0008DF4B File Offset: 0x0008C14B
		internal static ArgumentException CreateInvalidNameArgumentException(string name, string argumentName)
		{
			if (name != null)
			{
				return new ArgumentException(Res.GetString("The empty string '' is not a valid name."), argumentName);
			}
			return new ArgumentNullException(argumentName);
		}

		// Token: 0x04000FFF RID: 4095
		private static XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04001000 RID: 4096
		internal static char[] crt = new char[] { '\n', '\r', '\t' };

		// Token: 0x04001001 RID: 4097
		private static readonly int c_EncodedCharLength = 7;

		// Token: 0x04001002 RID: 4098
		private static volatile Regex c_EncodeCharPattern;

		// Token: 0x04001003 RID: 4099
		private static volatile Regex c_DecodeCharPattern;

		// Token: 0x04001004 RID: 4100
		private static volatile string[] s_allDateTimeFormats;

		// Token: 0x04001005 RID: 4101
		internal static readonly char[] WhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
