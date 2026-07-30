using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000283 RID: 643
	internal static class ValidateNames
	{
		// Token: 0x0600179D RID: 6045 RVA: 0x0008B3F4 File Offset: 0x000895F4
		internal static int ParseNmtoken(string s, int offset)
		{
			int num = offset;
			while (num < s.Length && (ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0)
			{
				num++;
			}
			return num - offset;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0008B42C File Offset: 0x0008962C
		internal static int ParseNmtokenNoNamespaces(string s, int offset)
		{
			int num = offset;
			while (num < s.Length && ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0 || s[num] == ':'))
			{
				num++;
			}
			return num - offset;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0008B470 File Offset: 0x00089670
		internal static bool IsNmtokenNoNamespaces(string s)
		{
			int num = ValidateNames.ParseNmtokenNoNamespaces(s, 0);
			return num > 0 && num == s.Length;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0008B494 File Offset: 0x00089694
		internal static int ParseNameNoNamespaces(string s, int offset)
		{
			int num = offset;
			if (num < s.Length)
			{
				if ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 4) == 0 && s[num] != ':')
				{
					return 0;
				}
				num++;
				while (num < s.Length && ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0 || s[num] == ':'))
				{
					num++;
				}
			}
			return num - offset;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0008B508 File Offset: 0x00089708
		internal static bool IsNameNoNamespaces(string s)
		{
			int num = ValidateNames.ParseNameNoNamespaces(s, 0);
			return num > 0 && num == s.Length;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0008B52C File Offset: 0x0008972C
		internal static int ParseNCName(string s, int offset)
		{
			int num = offset;
			if (num < s.Length)
			{
				if ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 4) == 0)
				{
					return 0;
				}
				num++;
				while (num < s.Length && (ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0)
				{
					num++;
				}
			}
			return num - offset;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0008B588 File Offset: 0x00089788
		internal static int ParseNCName(string s)
		{
			return ValidateNames.ParseNCName(s, 0);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0008B591 File Offset: 0x00089791
		internal static string ParseNCNameThrow(string s)
		{
			ValidateNames.ParseNCNameInternal(s, true);
			return s;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0008B59C File Offset: 0x0008979C
		private static bool ParseNCNameInternal(string s, bool throwOnError)
		{
			int num = ValidateNames.ParseNCName(s, 0);
			if (num == 0 || num != s.Length)
			{
				if (throwOnError)
				{
					ValidateNames.ThrowInvalidName(s, 0, num);
				}
				return false;
			}
			return true;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0008B5CC File Offset: 0x000897CC
		internal static int ParseQName(string s, int offset, out int colonOffset)
		{
			colonOffset = 0;
			int num = ValidateNames.ParseNCName(s, offset);
			if (num != 0)
			{
				offset += num;
				if (offset < s.Length && s[offset] == ':')
				{
					int num2 = ValidateNames.ParseNCName(s, offset + 1);
					if (num2 != 0)
					{
						colonOffset = offset;
						num += num2 + 1;
					}
				}
			}
			return num;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0008B618 File Offset: 0x00089818
		internal static void ParseQNameThrow(string s, out string prefix, out string localName)
		{
			int num2;
			int num = ValidateNames.ParseQName(s, 0, out num2);
			if (num == 0 || num != s.Length)
			{
				ValidateNames.ThrowInvalidName(s, 0, num);
			}
			if (num2 != 0)
			{
				prefix = s.Substring(0, num2);
				localName = s.Substring(num2 + 1);
				return;
			}
			prefix = "";
			localName = s;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0008B668 File Offset: 0x00089868
		internal static void ParseNameTestThrow(string s, out string prefix, out string localName)
		{
			int num;
			if (s.Length != 0 && s[0] == '*')
			{
				string text;
				localName = (text = null);
				prefix = text;
				num = 1;
			}
			else
			{
				num = ValidateNames.ParseNCName(s, 0);
				if (num != 0)
				{
					localName = s.Substring(0, num);
					if (num < s.Length && s[num] == ':')
					{
						prefix = localName;
						int num2 = num + 1;
						if (num2 < s.Length && s[num2] == '*')
						{
							localName = null;
							num += 2;
						}
						else
						{
							int num3 = ValidateNames.ParseNCName(s, num2);
							if (num3 != 0)
							{
								localName = s.Substring(num2, num3);
								num += num3 + 1;
							}
						}
					}
					else
					{
						prefix = string.Empty;
					}
				}
				else
				{
					string text;
					localName = (text = null);
					prefix = text;
				}
			}
			if (num == 0 || num != s.Length)
			{
				ValidateNames.ThrowInvalidName(s, 0, num);
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0008B724 File Offset: 0x00089924
		internal static void ThrowInvalidName(string s, int offsetStartChar, int offsetBadChar)
		{
			if (offsetStartChar >= s.Length)
			{
				throw new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			if (ValidateNames.xmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !XmlCharType.Instance.IsStartNCNameSingleChar(s[offsetBadChar]))
			{
				throw new XmlException("Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
			}
			throw new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0008B798 File Offset: 0x00089998
		internal static Exception GetInvalidNameException(string s, int offsetStartChar, int offsetBadChar)
		{
			if (offsetStartChar >= s.Length)
			{
				return new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			if (ValidateNames.xmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !ValidateNames.xmlCharType.IsStartNCNameSingleChar(s[offsetBadChar]))
			{
				return new XmlException("Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
			}
			return new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0008B808 File Offset: 0x00089A08
		internal static bool StartsWithXml(string s)
		{
			return s.Length >= 3 && (s[0] == 'x' || s[0] == 'X') && (s[1] == 'm' || s[1] == 'M') && (s[2] == 'l' || s[2] == 'L');
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0008B869 File Offset: 0x00089A69
		internal static bool IsReservedNamespace(string s)
		{
			return s.Equals("http://www.w3.org/XML/1998/namespace") || s.Equals("http://www.w3.org/2000/xmlns/");
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0008B885 File Offset: 0x00089A85
		internal static void ValidateNameThrow(string prefix, string localName, string ns, XPathNodeType nodeKind, ValidateNames.Flags flags)
		{
			ValidateNames.ValidateNameInternal(prefix, localName, ns, nodeKind, flags, true);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0008B894 File Offset: 0x00089A94
		internal static bool ValidateName(string prefix, string localName, string ns, XPathNodeType nodeKind, ValidateNames.Flags flags)
		{
			return ValidateNames.ValidateNameInternal(prefix, localName, ns, nodeKind, flags, false);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0008B8A4 File Offset: 0x00089AA4
		private static bool ValidateNameInternal(string prefix, string localName, string ns, XPathNodeType nodeKind, ValidateNames.Flags flags, bool throwOnError)
		{
			if ((flags & ValidateNames.Flags.NCNames) != (ValidateNames.Flags)0)
			{
				if (prefix.Length != 0 && !ValidateNames.ParseNCNameInternal(prefix, throwOnError))
				{
					return false;
				}
				if (localName.Length != 0 && !ValidateNames.ParseNCNameInternal(localName, throwOnError))
				{
					return false;
				}
			}
			if ((flags & ValidateNames.Flags.CheckLocalName) != (ValidateNames.Flags)0)
			{
				if (nodeKind != XPathNodeType.Element)
				{
					if (nodeKind != XPathNodeType.Attribute)
					{
						if (nodeKind != XPathNodeType.ProcessingInstruction)
						{
							if (localName.Length == 0)
							{
								goto IL_00FA;
							}
							if (throwOnError)
							{
								throw new XmlException("A node of type '{0}' cannot have a name.", nodeKind.ToString());
							}
							return false;
						}
						else
						{
							if (localName.Length != 0 && (localName.Length != 3 || !ValidateNames.StartsWithXml(localName)))
							{
								goto IL_00FA;
							}
							if (throwOnError)
							{
								throw new XmlException("'{0}' is an invalid name for processing instructions.", localName);
							}
							return false;
						}
					}
					else if (ns.Length == 0 && localName.Equals("xmlns"))
					{
						if (throwOnError)
						{
							throw new XmlException("A node of type '{0}' cannot have the name '{1}'.", new string[]
							{
								nodeKind.ToString(),
								localName
							});
						}
						return false;
					}
				}
				if (localName.Length == 0)
				{
					if (throwOnError)
					{
						throw new XmlException("The local name for elements or attributes cannot be null or an empty string.", string.Empty);
					}
					return false;
				}
			}
			IL_00FA:
			if ((flags & ValidateNames.Flags.CheckPrefixMapping) != (ValidateNames.Flags)0)
			{
				if (nodeKind - XPathNodeType.Element > 2)
				{
					if (nodeKind != XPathNodeType.ProcessingInstruction)
					{
						if (prefix.Length != 0 || ns.Length != 0)
						{
							if (throwOnError)
							{
								throw new XmlException("A node of type '{0}' cannot have a name.", nodeKind.ToString());
							}
							return false;
						}
					}
					else if (prefix.Length != 0 || ns.Length != 0)
					{
						if (throwOnError)
						{
							throw new XmlException("'{0}' is an invalid name for processing instructions.", ValidateNames.CreateName(prefix, localName));
						}
						return false;
					}
				}
				else if (ns.Length == 0)
				{
					if (prefix.Length != 0)
					{
						if (throwOnError)
						{
							throw new XmlException("Cannot use a prefix with an empty namespace.", string.Empty);
						}
						return false;
					}
				}
				else if (prefix.Length == 0 && nodeKind == XPathNodeType.Attribute)
				{
					if (throwOnError)
					{
						throw new XmlException("A node of type '{0}' cannot have the name '{1}'.", new string[]
						{
							nodeKind.ToString(),
							localName
						});
					}
					return false;
				}
				else if (prefix.Equals("xml"))
				{
					if (!ns.Equals("http://www.w3.org/XML/1998/namespace"))
					{
						if (throwOnError)
						{
							throw new XmlException("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\".", string.Empty);
						}
						return false;
					}
				}
				else if (prefix.Equals("xmlns"))
				{
					if (throwOnError)
					{
						throw new XmlException("Prefix \"xmlns\" is reserved for use by XML.", string.Empty);
					}
					return false;
				}
				else if (ValidateNames.IsReservedNamespace(ns))
				{
					if (throwOnError)
					{
						throw new XmlException("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", string.Empty);
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0008BAF1 File Offset: 0x00089CF1
		private static string CreateName(string prefix, string localName)
		{
			if (prefix.Length == 0)
			{
				return localName;
			}
			return prefix + ":" + localName;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0008BB0C File Offset: 0x00089D0C
		internal static void SplitQName(string name, out string prefix, out string lname)
		{
			int num = name.IndexOf(':');
			if (-1 == num)
			{
				prefix = string.Empty;
				lname = name;
				return;
			}
			if (num == 0 || name.Length - 1 == num)
			{
				throw new ArgumentException(Res.GetString("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0')), "name");
			}
			prefix = name.Substring(0, num);
			num++;
			lname = name.Substring(num, name.Length - num);
		}

		// Token: 0x04000FD4 RID: 4052
		private static XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x02000284 RID: 644
		internal enum Flags
		{
			// Token: 0x04000FD6 RID: 4054
			NCNames = 1,
			// Token: 0x04000FD7 RID: 4055
			CheckLocalName,
			// Token: 0x04000FD8 RID: 4056
			CheckPrefixMapping = 4,
			// Token: 0x04000FD9 RID: 4057
			All = 7,
			// Token: 0x04000FDA RID: 4058
			AllExceptNCNames = 6,
			// Token: 0x04000FDB RID: 4059
			AllExceptPrefixMapping = 3
		}
	}
}
