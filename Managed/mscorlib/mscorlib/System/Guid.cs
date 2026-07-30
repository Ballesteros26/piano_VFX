using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace System
{
	/// <summary>Represents a globally unique identifier (GUID).</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200017B RID: 379
	[ComVisible(true)]
	[Serializable]
	public struct Guid : IFormattable, IComparable, IComparable<Guid>, IEquatable<Guid>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the specified array of bytes.</summary>
		/// <param name="b">A 16-element byte array containing values with which to initialize the GUID. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="b" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="b" /> is not 16 bytes long. </exception>
		// Token: 0x06001004 RID: 4100 RVA: 0x00045674 File Offset: 0x00043874
		public Guid(byte[] b)
		{
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			if (b.Length != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Byte array for GUID must be exactly {0} bytes long.", new object[] { "16" }));
			}
			this._a = ((int)b[3] << 24) | ((int)b[2] << 16) | ((int)b[1] << 8) | (int)b[0];
			this._b = (short)(((int)b[5] << 8) | (int)b[4]);
			this._c = (short)(((int)b[7] << 8) | (int)b[6]);
			this._d = b[8];
			this._e = b[9];
			this._f = b[10];
			this._g = b[11];
			this._h = b[12];
			this._i = b[13];
			this._j = b[14];
			this._k = b[15];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the specified unsigned integers and bytes.</summary>
		/// <param name="a">The first 4 bytes of the GUID. </param>
		/// <param name="b">The next 2 bytes of the GUID. </param>
		/// <param name="c">The next 2 bytes of the GUID. </param>
		/// <param name="d">The next byte of the GUID. </param>
		/// <param name="e">The next byte of the GUID. </param>
		/// <param name="f">The next byte of the GUID. </param>
		/// <param name="g">The next byte of the GUID. </param>
		/// <param name="h">The next byte of the GUID. </param>
		/// <param name="i">The next byte of the GUID. </param>
		/// <param name="j">The next byte of the GUID. </param>
		/// <param name="k">The next byte of the GUID. </param>
		// Token: 0x06001005 RID: 4101 RVA: 0x00045740 File Offset: 0x00043940
		[CLSCompliant(false)]
		public Guid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = (int)a;
			this._b = (short)b;
			this._c = (short)c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the specified integers and byte array.</summary>
		/// <param name="a">The first 4 bytes of the GUID. </param>
		/// <param name="b">The next 2 bytes of the GUID. </param>
		/// <param name="c">The next 2 bytes of the GUID. </param>
		/// <param name="d">The remaining 8 bytes of the GUID. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="d" /> is not 8 bytes long. </exception>
		// Token: 0x06001006 RID: 4102 RVA: 0x000457A4 File Offset: 0x000439A4
		public Guid(int a, short b, short c, byte[] d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			if (d.Length != 8)
			{
				throw new ArgumentException(Environment.GetResourceString("Byte array for GUID must be exactly {0} bytes long.", new object[] { "8" }));
			}
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d[0];
			this._e = d[1];
			this._f = d[2];
			this._g = d[3];
			this._h = d[4];
			this._i = d[5];
			this._j = d[6];
			this._k = d[7];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the specified integers and bytes.</summary>
		/// <param name="a">The first 4 bytes of the GUID. </param>
		/// <param name="b">The next 2 bytes of the GUID. </param>
		/// <param name="c">The next 2 bytes of the GUID. </param>
		/// <param name="d">The next byte of the GUID. </param>
		/// <param name="e">The next byte of the GUID. </param>
		/// <param name="f">The next byte of the GUID. </param>
		/// <param name="g">The next byte of the GUID. </param>
		/// <param name="h">The next byte of the GUID. </param>
		/// <param name="i">The next byte of the GUID. </param>
		/// <param name="j">The next byte of the GUID. </param>
		/// <param name="k">The next byte of the GUID. </param>
		// Token: 0x06001007 RID: 4103 RVA: 0x0004584C File Offset: 0x00043A4C
		public Guid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the value represented by the specified string.</summary>
		/// <param name="g">A string that contains a GUID in one of the following formats ("d" represents a hexadecimal digit whose case is ignored): 32 contiguous digits: dddddddddddddddddddddddddddddddd -or- Groups of 8, 4, 4, 4, and 12 digits with hyphens between the groups. The entire GUID can optionally be enclosed in matching braces or parentheses: dddddddd-dddd-dddd-dddd-dddddddddddd -or- {dddddddd-dddd-dddd-dddd-dddddddddddd} -or- (dddddddd-dddd-dddd-dddd-dddddddddddd) -or- Groups of 8, 4, and 4 digits, and a subset of eight groups of 2 digits, with each group prefixed by "0x" or "0X", and separated by commas. The entire GUID, as well as the subset, is enclosed in matching braces: {0xdddddddd, 0xdddd, 0xdddd,{0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd}} All braces, commas, and "0x" prefixes are required. All embedded spaces are ignored. All leading zeros in a group are ignored.The digits shown in a group are the maximum number of meaningful digits that can appear in that group. You can specify from 1 to the number of digits shown for a group. The specified digits are assumed to be the low-order digits of the group. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format of <paramref name="g" /> is invalid. </exception>
		/// <exception cref="T:System.OverflowException">The format of <paramref name="g" /> is invalid. </exception>
		// Token: 0x06001008 RID: 4104 RVA: 0x000458B0 File Offset: 0x00043AB0
		public Guid(string g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			this = Guid.Empty;
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.All);
			if (Guid.TryParseGuid(g, Guid.GuidStyles.Any, ref guidResult))
			{
				this = guidResult.parsedGuid;
				return;
			}
			throw guidResult.GetGuidParseException();
		}

		/// <summary>Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid" /> structure.</summary>
		/// <returns>A structure that contains the value that was parsed.</returns>
		/// <param name="input">The GUID to convert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not in a recognized format.</exception>
		// Token: 0x06001009 RID: 4105 RVA: 0x00045908 File Offset: 0x00043B08
		public static Guid Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.AllButOverflow);
			if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref guidResult))
			{
				return guidResult.parsedGuid;
			}
			throw guidResult.GetGuidParseException();
		}

		/// <summary>Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid" /> structure. </summary>
		/// <returns>true if the parse operation was successful; otherwise, false.</returns>
		/// <param name="input">The GUID to convert.</param>
		/// <param name="result">The structure that will contain the parsed value. If the method returns true, <paramref name="result" /> contains a valid <see cref="T:System.Guid" />. If the method returns false, <paramref name="result" /> equals <see cref="F:System.Guid.Empty" />. </param>
		// Token: 0x0600100A RID: 4106 RVA: 0x00045950 File Offset: 0x00043B50
		public static bool TryParse(string input, out Guid result)
		{
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.None);
			if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref guidResult))
			{
				result = guidResult.parsedGuid;
				return true;
			}
			result = Guid.Empty;
			return false;
		}

		/// <summary>Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid" /> structure, provided that the string is in the specified format.</summary>
		/// <returns>A structure that contains the value that was parsed.</returns>
		/// <param name="input">The GUID to convert.</param>
		/// <param name="format">One of the following specifiers that indicates the exact format to use when interpreting <paramref name="input" />: "N", "D", "B", "P", or "X".</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="format" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not in a recognized format.</exception>
		// Token: 0x0600100B RID: 4107 RVA: 0x00045994 File Offset: 0x00043B94
		public static Guid ParseExact(string input, string format)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format String can be only \"D\", \"d\", \"N\", \"n\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\"."));
			}
			char c = format[0];
			Guid.GuidStyles guidStyles;
			if (c == 'D' || c == 'd')
			{
				guidStyles = Guid.GuidStyles.RequireDashes;
			}
			else if (c == 'N' || c == 'n')
			{
				guidStyles = Guid.GuidStyles.None;
			}
			else if (c == 'B' || c == 'b')
			{
				guidStyles = Guid.GuidStyles.BraceFormat;
			}
			else if (c == 'P' || c == 'p')
			{
				guidStyles = Guid.GuidStyles.ParenthesisFormat;
			}
			else
			{
				if (c != 'X' && c != 'x')
				{
					throw new FormatException(Environment.GetResourceString("Format String can be only \"D\", \"d\", \"N\", \"n\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\"."));
				}
				guidStyles = Guid.GuidStyles.HexFormat;
			}
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.AllButOverflow);
			if (Guid.TryParseGuid(input, guidStyles, ref guidResult))
			{
				return guidResult.parsedGuid;
			}
			throw guidResult.GetGuidParseException();
		}

		/// <summary>Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid" /> structure, provided that the string is in the specified format.</summary>
		/// <returns>true if the parse operation was successful; otherwise, false.</returns>
		/// <param name="input">The GUID to convert.</param>
		/// <param name="format">One of the following specifiers that indicates the exact format to use when interpreting <paramref name="input" />: "N", "D", "B", "P", or "X".</param>
		/// <param name="result">The structure that will contain the parsed value. If the method returns true, <paramref name="result" /> contains a valid <see cref="T:System.Guid" />. If the method returns false, <paramref name="result" /> equals <see cref="F:System.Guid.Empty" />.</param>
		// Token: 0x0600100C RID: 4108 RVA: 0x00045A64 File Offset: 0x00043C64
		public static bool TryParseExact(string input, string format, out Guid result)
		{
			if (format == null || format.Length != 1)
			{
				result = Guid.Empty;
				return false;
			}
			char c = format[0];
			Guid.GuidStyles guidStyles;
			if (c == 'D' || c == 'd')
			{
				guidStyles = Guid.GuidStyles.RequireDashes;
			}
			else if (c == 'N' || c == 'n')
			{
				guidStyles = Guid.GuidStyles.None;
			}
			else if (c == 'B' || c == 'b')
			{
				guidStyles = Guid.GuidStyles.BraceFormat;
			}
			else if (c == 'P' || c == 'p')
			{
				guidStyles = Guid.GuidStyles.ParenthesisFormat;
			}
			else
			{
				if (c != 'X' && c != 'x')
				{
					result = Guid.Empty;
					return false;
				}
				guidStyles = Guid.GuidStyles.HexFormat;
			}
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.None);
			if (Guid.TryParseGuid(input, guidStyles, ref guidResult))
			{
				result = guidResult.parsedGuid;
				return true;
			}
			result = Guid.Empty;
			return false;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00045B24 File Offset: 0x00043D24
		private static bool TryParseGuid(string g, Guid.GuidStyles flags, ref Guid.GuidResult result)
		{
			if (g == null)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			string text = g.Trim();
			if (text.Length == 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			bool flag = text.IndexOf('-', 0) >= 0;
			if (flag)
			{
				if ((flags & (Guid.GuidStyles.AllowDashes | Guid.GuidStyles.RequireDashes)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireDashes) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			bool flag2 = text.IndexOf('{', 0) >= 0;
			if (flag2)
			{
				if ((flags & (Guid.GuidStyles.AllowBraces | Guid.GuidStyles.RequireBraces)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireBraces) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			if (text.IndexOf('(', 0) >= 0)
			{
				if ((flags & (Guid.GuidStyles.AllowParenthesis | Guid.GuidStyles.RequireParenthesis)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireParenthesis) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			bool flag3;
			try
			{
				if (flag)
				{
					flag3 = Guid.TryParseGuidWithDashes(text, ref result);
				}
				else if (flag2)
				{
					flag3 = Guid.TryParseGuidWithHexPrefix(text, ref result);
				}
				else
				{
					flag3 = Guid.TryParseGuidWithNoStyle(text, ref result);
				}
			}
			catch (IndexOutOfRangeException ex)
			{
				result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Unrecognized Guid format.", null, null, ex);
				flag3 = false;
			}
			catch (ArgumentException ex2)
			{
				result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Unrecognized Guid format.", null, null, ex2);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00045C7C File Offset: 0x00043E7C
		private static bool TryParseGuidWithHexPrefix(string guidString, ref Guid.GuidResult result)
		{
			guidString = Guid.EatAllWhitespace(guidString);
			if (string.IsNullOrEmpty(guidString) || guidString[0] != '{')
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected {0xdddddddd, etc}.");
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{0xdddddddd, etc}");
				return false;
			}
			int num = 3;
			int num2 = guidString.IndexOf(',', num) - num;
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
				return false;
			}
			if (!Guid.StringToInt(guidString.Substring(num, num2), -1, 4096, out result.parsedGuid._a, ref result))
			{
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{0xdddddddd, 0xdddd, etc}");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.IndexOf(',', num) - num;
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
				return false;
			}
			if (!Guid.StringToShort(guidString.Substring(num, num2), -1, 4096, out result.parsedGuid._b, ref result))
			{
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{0xdddddddd, 0xdddd, 0xdddd, etc}");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.IndexOf(',', num) - num;
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
				return false;
			}
			if (!Guid.StringToShort(guidString.Substring(num, num2), -1, 4096, out result.parsedGuid._c, ref result))
			{
				return false;
			}
			if (guidString.Length <= num + num2 + 1 || guidString[num + num2 + 1] != '{')
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected {0xdddddddd, etc}.");
				return false;
			}
			num2++;
			byte[] array = new byte[8];
			for (int i = 0; i < 8; i++)
			{
				if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{... { ... 0xdd, ...}}");
					return false;
				}
				num = num + num2 + 3;
				if (i < 7)
				{
					num2 = guidString.IndexOf(',', num) - num;
					if (num2 <= 0)
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
						return false;
					}
				}
				else
				{
					num2 = guidString.IndexOf('}', num) - num;
					if (num2 <= 0)
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a brace, or the length between the previous token and the brace was zero (i.e., '0x,'etc.).");
						return false;
					}
				}
				uint num3 = (uint)Convert.ToInt32(guidString.Substring(num, num2), 16);
				if (num3 > 255U)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Value was either too large or too small for an unsigned byte.");
					return false;
				}
				array[i] = (byte)num3;
			}
			result.parsedGuid._d = array[0];
			result.parsedGuid._e = array[1];
			result.parsedGuid._f = array[2];
			result.parsedGuid._g = array[3];
			result.parsedGuid._h = array[4];
			result.parsedGuid._i = array[5];
			result.parsedGuid._j = array[6];
			result.parsedGuid._k = array[7];
			if (num + num2 + 1 >= guidString.Length || guidString[num + num2 + 1] != '}')
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find the ending brace.");
				return false;
			}
			if (num + num2 + 1 != guidString.Length - 1)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Additional non-parsable characters are at the end of the string.");
				return false;
			}
			return true;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00045F78 File Offset: 0x00044178
		private static bool TryParseGuidWithNoStyle(string guidString, ref Guid.GuidResult result)
		{
			int num = 0;
			int num2 = 0;
			if (guidString.Length != 32)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			foreach (char c in guidString)
			{
				if (c < '0' || c > '9')
				{
					char c2 = char.ToUpper(c, CultureInfo.InvariantCulture);
					if (c2 < 'A' || c2 > 'F')
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Guid string should only contain hexadecimal characters.");
						return false;
					}
				}
			}
			if (!Guid.StringToInt(guidString.Substring(num, 8), -1, 4096, out result.parsedGuid._a, ref result))
			{
				return false;
			}
			num += 8;
			if (!Guid.StringToShort(guidString.Substring(num, 4), -1, 4096, out result.parsedGuid._b, ref result))
			{
				return false;
			}
			num += 4;
			if (!Guid.StringToShort(guidString.Substring(num, 4), -1, 4096, out result.parsedGuid._c, ref result))
			{
				return false;
			}
			num += 4;
			int num3;
			if (!Guid.StringToInt(guidString.Substring(num, 4), -1, 4096, out num3, ref result))
			{
				return false;
			}
			num += 4;
			num2 = num;
			long num4;
			if (!Guid.StringToLong(guidString, ref num2, 8192, out num4, ref result))
			{
				return false;
			}
			if (num2 - num != 12)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			result.parsedGuid._d = (byte)(num3 >> 8);
			result.parsedGuid._e = (byte)num3;
			num3 = (int)(num4 >> 32);
			result.parsedGuid._f = (byte)(num3 >> 8);
			result.parsedGuid._g = (byte)num3;
			num3 = (int)num4;
			result.parsedGuid._h = (byte)(num3 >> 24);
			result.parsedGuid._i = (byte)(num3 >> 16);
			result.parsedGuid._j = (byte)(num3 >> 8);
			result.parsedGuid._k = (byte)num3;
			return true;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00046130 File Offset: 0x00044330
		private static bool TryParseGuidWithDashes(string guidString, ref Guid.GuidResult result)
		{
			int num = 0;
			int num2 = 0;
			if (guidString[0] == '{')
			{
				if (guidString.Length != 38 || guidString[37] != '}')
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
					return false;
				}
				num = 1;
			}
			else if (guidString[0] == '(')
			{
				if (guidString.Length != 38 || guidString[37] != ')')
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
					return false;
				}
				num = 1;
			}
			else if (guidString.Length != 36)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			if (guidString[8 + num] != '-' || guidString[13 + num] != '-' || guidString[18 + num] != '-' || guidString[23 + num] != '-')
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Dashes are in the wrong position for GUID parsing.");
				return false;
			}
			num2 = num;
			int num3;
			if (!Guid.StringToInt(guidString, ref num2, 8, 8192, out num3, ref result))
			{
				return false;
			}
			result.parsedGuid._a = num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			result.parsedGuid._b = (short)num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			result.parsedGuid._c = (short)num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			num2++;
			num = num2;
			long num4;
			if (!Guid.StringToLong(guidString, ref num2, 8192, out num4, ref result))
			{
				return false;
			}
			if (num2 - num != 12)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			result.parsedGuid._d = (byte)(num3 >> 8);
			result.parsedGuid._e = (byte)num3;
			num3 = (int)(num4 >> 32);
			result.parsedGuid._f = (byte)(num3 >> 8);
			result.parsedGuid._g = (byte)num3;
			num3 = (int)num4;
			result.parsedGuid._h = (byte)(num3 >> 24);
			result.parsedGuid._i = (byte)(num3 >> 16);
			result.parsedGuid._j = (byte)(num3 >> 8);
			result.parsedGuid._k = (byte)num3;
			return true;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00046339 File Offset: 0x00044539
		[SecuritySafeCritical]
		private static bool StringToShort(string str, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			return Guid.StringToShort(str, null, requiredLength, flags, out result, ref parseResult);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00046348 File Offset: 0x00044548
		[SecuritySafeCritical]
		private unsafe static bool StringToShort(string str, ref int parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			fixed (int* ptr = &parsePos)
			{
				int* ptr2 = ptr;
				return Guid.StringToShort(str, ptr2, requiredLength, flags, out result, ref parseResult);
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00046368 File Offset: 0x00044568
		[SecurityCritical]
		private unsafe static bool StringToShort(string str, int* parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			result = 0;
			int num;
			bool flag = Guid.StringToInt(str, parsePos, requiredLength, flags, out num, ref parseResult);
			result = (short)num;
			return flag;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0004638B File Offset: 0x0004458B
		[SecuritySafeCritical]
		private static bool StringToInt(string str, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			return Guid.StringToInt(str, null, requiredLength, flags, out result, ref parseResult);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0004639C File Offset: 0x0004459C
		[SecuritySafeCritical]
		private unsafe static bool StringToInt(string str, ref int parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			fixed (int* ptr = &parsePos)
			{
				int* ptr2 = ptr;
				return Guid.StringToInt(str, ptr2, requiredLength, flags, out result, ref parseResult);
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000463BC File Offset: 0x000445BC
		[SecurityCritical]
		private unsafe static bool StringToInt(string str, int* parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			result = 0;
			int num = ((parsePos == null) ? 0 : (*parsePos));
			try
			{
				result = ParseNumbers.StringToInt(str, 16, flags, parsePos);
			}
			catch (OverflowException ex)
			{
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.All)
				{
					throw;
				}
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
				{
					throw new FormatException(Environment.GetResourceString("Unrecognized Guid format."), ex);
				}
				parseResult.SetFailure(ex);
				return false;
			}
			catch (Exception ex2)
			{
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.None)
				{
					parseResult.SetFailure(ex2);
					return false;
				}
				throw;
			}
			if (requiredLength != -1 && parsePos != null && *parsePos - num != requiredLength)
			{
				parseResult.SetFailure(Guid.ParseFailureKind.Format, "Guid string should only contain hexadecimal characters.");
				return false;
			}
			return true;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00046474 File Offset: 0x00044674
		[SecuritySafeCritical]
		private static bool StringToLong(string str, int flags, out long result, ref Guid.GuidResult parseResult)
		{
			return Guid.StringToLong(str, null, flags, out result, ref parseResult);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00046484 File Offset: 0x00044684
		[SecuritySafeCritical]
		private unsafe static bool StringToLong(string str, ref int parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
		{
			fixed (int* ptr = &parsePos)
			{
				int* ptr2 = ptr;
				return Guid.StringToLong(str, ptr2, flags, out result, ref parseResult);
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000464A4 File Offset: 0x000446A4
		[SecuritySafeCritical]
		private unsafe static bool StringToLong(string str, int* parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
		{
			result = 0L;
			try
			{
				result = ParseNumbers.StringToLong(str, 16, flags, parsePos);
			}
			catch (OverflowException ex)
			{
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.All)
				{
					throw;
				}
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
				{
					throw new FormatException(Environment.GetResourceString("Unrecognized Guid format."), ex);
				}
				parseResult.SetFailure(ex);
				return false;
			}
			catch (Exception ex2)
			{
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.None)
				{
					parseResult.SetFailure(ex2);
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00046530 File Offset: 0x00044730
		private static string EatAllWhitespace(string str)
		{
			int num = 0;
			char[] array = new char[str.Length];
			foreach (char c in str)
			{
				if (!char.IsWhiteSpace(c))
				{
					array[num++] = c;
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0004657C File Offset: 0x0004477C
		private static bool IsHexPrefix(string str, int i)
		{
			return str.Length > i + 1 && str[i] == '0' && char.ToLower(str[i + 1], CultureInfo.InvariantCulture) == 'x';
		}

		/// <summary>Returns a 16-element byte array that contains the value of this instance.</summary>
		/// <returns>A 16-element byte array.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600101C RID: 4124 RVA: 0x000465B0 File Offset: 0x000447B0
		public byte[] ToByteArray()
		{
			return new byte[]
			{
				(byte)this._a,
				(byte)(this._a >> 8),
				(byte)(this._a >> 16),
				(byte)(this._a >> 24),
				(byte)this._b,
				(byte)(this._b >> 8),
				(byte)this._c,
				(byte)(this._c >> 8),
				this._d,
				this._e,
				this._f,
				this._g,
				this._h,
				this._i,
				this._j,
				this._k
			};
		}

		/// <summary>Returns a string representation of the value of this instance in registry format.</summary>
		/// <returns>The value of this <see cref="T:System.Guid" />, formatted by using the "D" format specifier as follows: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx where the value of the GUID is represented as a series of lowercase hexadecimal digits in groups of 8, 4, 4, 4, and 12 digits and separated by hyphens. An example of a return value is "382c74c3-721d-4f34-80e5-57657b6cbc27". To convert the hexadecimal digits from a through f to uppercase, call the <see cref="M:System.String.ToString" /> method on the returned string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600101D RID: 4125 RVA: 0x0004666F File Offset: 0x0004486F
		public override string ToString()
		{
			return this.ToString("D", null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600101E RID: 4126 RVA: 0x0004667D File Offset: 0x0004487D
		public override int GetHashCode()
		{
			return this._a ^ (((int)this._b << 16) | (int)((ushort)this._c)) ^ (((int)this._f << 24) | (int)this._k);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Guid" /> that has the same value as this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600101F RID: 4127 RVA: 0x000466A8 File Offset: 0x000448A8
		public override bool Equals(object o)
		{
			if (o == null || !(o is Guid))
			{
				return false;
			}
			Guid guid = (Guid)o;
			return guid._a == this._a && guid._b == this._b && guid._c == this._c && guid._d == this._d && guid._e == this._e && guid._f == this._f && guid._g == this._g && guid._h == this._h && guid._i == this._i && guid._j == this._j && guid._k == this._k;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Guid" /> object represent the same value.</summary>
		/// <returns>true if <paramref name="g" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="g">An object to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001020 RID: 4128 RVA: 0x0004677C File Offset: 0x0004497C
		public bool Equals(Guid g)
		{
			return g._a == this._a && g._b == this._b && g._c == this._c && g._d == this._d && g._e == this._e && g._f == this._f && g._g == this._g && g._h == this._h && g._i == this._i && g._j == this._j && g._k == this._k;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0004683A File Offset: 0x00044A3A
		private int GetResult(uint me, uint them)
		{
			if (me < them)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return value Description A negative integer This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is greater than <paramref name="value" />, or <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Guid" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001022 RID: 4130 RVA: 0x00046844 File Offset: 0x00044A44
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is Guid))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type GUID."));
			}
			Guid guid = (Guid)value;
			if (guid._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)guid._a);
			}
			if (guid._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)guid._b);
			}
			if (guid._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)guid._c);
			}
			if (guid._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)guid._d);
			}
			if (guid._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)guid._e);
			}
			if (guid._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)guid._f);
			}
			if (guid._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)guid._g);
			}
			if (guid._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)guid._h);
			}
			if (guid._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)guid._i);
			}
			if (guid._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)guid._j);
			}
			if (guid._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)guid._k);
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Guid" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return value Description A negative integer This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An object to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001023 RID: 4131 RVA: 0x000469E4 File Offset: 0x00044BE4
		public int CompareTo(Guid value)
		{
			if (value._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)value._a);
			}
			if (value._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)value._b);
			}
			if (value._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)value._c);
			}
			if (value._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)value._d);
			}
			if (value._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)value._e);
			}
			if (value._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)value._f);
			}
			if (value._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)value._g);
			}
			if (value._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)value._h);
			}
			if (value._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)value._i);
			}
			if (value._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)value._j);
			}
			if (value._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)value._k);
			}
			return 0;
		}

		/// <summary>Indicates whether the values of two specified <see cref="T:System.Guid" /> objects are equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are equal; otherwise, false.</returns>
		/// <param name="a">The first object to compare. </param>
		/// <param name="b">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001024 RID: 4132 RVA: 0x00046B60 File Offset: 0x00044D60
		public static bool operator ==(Guid a, Guid b)
		{
			return a._a == b._a && a._b == b._b && a._c == b._c && a._d == b._d && a._e == b._e && a._f == b._f && a._g == b._g && a._h == b._h && a._i == b._i && a._j == b._j && a._k == b._k;
		}

		/// <summary>Indicates whether the values of two specified <see cref="T:System.Guid" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, false.</returns>
		/// <param name="a">The first object to compare. </param>
		/// <param name="b">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001025 RID: 4133 RVA: 0x00046C1E File Offset: 0x00044E1E
		public static bool operator !=(Guid a, Guid b)
		{
			return !(a == b);
		}

		/// <summary>Returns a string representation of the value of this <see cref="T:System.Guid" /> instance, according to the provided format specifier.</summary>
		/// <returns>The value of this <see cref="T:System.Guid" />, represented as a series of lowercase hexadecimal digits in the specified format. </returns>
		/// <param name="format">A single format specifier that indicates how to format the value of this <see cref="T:System.Guid" />. The <paramref name="format" /> parameter can be "N", "D", "B", "P", or "X". If <paramref name="format" /> is null or an empty string (""), "D" is used. </param>
		/// <exception cref="T:System.FormatException">The value of <paramref name="format" /> is not null, an empty string (""), "N", "D", "B", "P", or "X". </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001026 RID: 4134 RVA: 0x00046C2A File Offset: 0x00044E2A
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00046C34 File Offset: 0x00044E34
		private static char HexToChar(int a)
		{
			a &= 15;
			return (char)((a > 9) ? (a - 10 + 97) : (a + 48));
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00046C4F File Offset: 0x00044E4F
		[SecurityCritical]
		private unsafe static int HexsToChars(char* guidChars, int offset, int a, int b)
		{
			return Guid.HexsToChars(guidChars, offset, a, b, false);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00046C5C File Offset: 0x00044E5C
		[SecurityCritical]
		private unsafe static int HexsToChars(char* guidChars, int offset, int a, int b, bool hex)
		{
			if (hex)
			{
				guidChars[offset++] = '0';
				guidChars[offset++] = 'x';
			}
			guidChars[offset++] = Guid.HexToChar(a >> 4);
			guidChars[offset++] = Guid.HexToChar(a);
			if (hex)
			{
				guidChars[offset++] = ',';
				guidChars[offset++] = '0';
				guidChars[offset++] = 'x';
			}
			guidChars[offset++] = Guid.HexToChar(b >> 4);
			guidChars[offset++] = Guid.HexToChar(b);
			return offset;
		}

		/// <summary>Returns a string representation of the value of this instance of the <see cref="T:System.Guid" /> class, according to the provided format specifier and culture-specific format information.</summary>
		/// <returns>The value of this <see cref="T:System.Guid" />, represented as a series of lowercase hexadecimal digits in the specified format.</returns>
		/// <param name="format">A single format specifier that indicates how to format the value of this <see cref="T:System.Guid" />. The <paramref name="format" /> parameter can be "N", "D", "B", "P", or "X". If <paramref name="format" /> is null or an empty string (""), "D" is used. </param>
		/// <param name="provider">(Reserved) An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of <paramref name="format" /> is not null, an empty string (""), "N", "D", "B", "P", or "X". </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600102A RID: 4138 RVA: 0x00046D04 File Offset: 0x00044F04
		[SecuritySafeCritical]
		public unsafe string ToString(string format, IFormatProvider provider)
		{
			if (format == null || format.Length == 0)
			{
				format = "D";
			}
			int num = 0;
			bool flag = true;
			bool flag2 = false;
			if (format.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format String can be only \"D\", \"d\", \"N\", \"n\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\"."));
			}
			char c = format[0];
			string text;
			if (c == 'D' || c == 'd')
			{
				text = string.FastAllocateString(36);
			}
			else if (c == 'N' || c == 'n')
			{
				text = string.FastAllocateString(32);
				flag = false;
			}
			else if (c == 'B' || c == 'b')
			{
				text = string.FastAllocateString(38);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					ptr[(IntPtr)(num++) * 2] = '{';
					ptr[37] = '}';
				}
			}
			else if (c == 'P' || c == 'p')
			{
				text = string.FastAllocateString(38);
				fixed (string text2 = text)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					ptr2[(IntPtr)(num++) * 2] = '(';
					ptr2[37] = ')';
				}
			}
			else
			{
				if (c != 'X' && c != 'x')
				{
					throw new FormatException(Environment.GetResourceString("Format String can be only \"D\", \"d\", \"N\", \"n\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\"."));
				}
				text = string.FastAllocateString(68);
				fixed (string text2 = text)
				{
					char* ptr3 = text2;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					ptr3[(IntPtr)(num++) * 2] = '{';
					ptr3[67] = '}';
				}
				flag = false;
				flag2 = true;
			}
			fixed (string text2 = text)
			{
				char* ptr4 = text2;
				if (ptr4 != null)
				{
					ptr4 += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (flag2)
				{
					ptr4[(IntPtr)(num++) * 2] = '0';
					ptr4[(IntPtr)(num++) * 2] = 'x';
					num = Guid.HexsToChars(ptr4, num, this._a >> 24, this._a >> 16);
					num = Guid.HexsToChars(ptr4, num, this._a >> 8, this._a);
					ptr4[(IntPtr)(num++) * 2] = ',';
					ptr4[(IntPtr)(num++) * 2] = '0';
					ptr4[(IntPtr)(num++) * 2] = 'x';
					num = Guid.HexsToChars(ptr4, num, this._b >> 8, (int)this._b);
					ptr4[(IntPtr)(num++) * 2] = ',';
					ptr4[(IntPtr)(num++) * 2] = '0';
					ptr4[(IntPtr)(num++) * 2] = 'x';
					num = Guid.HexsToChars(ptr4, num, this._c >> 8, (int)this._c);
					ptr4[(IntPtr)(num++) * 2] = ',';
					ptr4[(IntPtr)(num++) * 2] = '{';
					num = Guid.HexsToChars(ptr4, num, (int)this._d, (int)this._e, true);
					ptr4[(IntPtr)(num++) * 2] = ',';
					num = Guid.HexsToChars(ptr4, num, (int)this._f, (int)this._g, true);
					ptr4[(IntPtr)(num++) * 2] = ',';
					num = Guid.HexsToChars(ptr4, num, (int)this._h, (int)this._i, true);
					ptr4[(IntPtr)(num++) * 2] = ',';
					num = Guid.HexsToChars(ptr4, num, (int)this._j, (int)this._k, true);
					ptr4[(IntPtr)(num++) * 2] = '}';
				}
				else
				{
					num = Guid.HexsToChars(ptr4, num, this._a >> 24, this._a >> 16);
					num = Guid.HexsToChars(ptr4, num, this._a >> 8, this._a);
					if (flag)
					{
						ptr4[(IntPtr)(num++) * 2] = '-';
					}
					num = Guid.HexsToChars(ptr4, num, this._b >> 8, (int)this._b);
					if (flag)
					{
						ptr4[(IntPtr)(num++) * 2] = '-';
					}
					num = Guid.HexsToChars(ptr4, num, this._c >> 8, (int)this._c);
					if (flag)
					{
						ptr4[(IntPtr)(num++) * 2] = '-';
					}
					num = Guid.HexsToChars(ptr4, num, (int)this._d, (int)this._e);
					if (flag)
					{
						ptr4[(IntPtr)(num++) * 2] = '-';
					}
					num = Guid.HexsToChars(ptr4, num, (int)this._f, (int)this._g);
					num = Guid.HexsToChars(ptr4, num, (int)this._h, (int)this._i);
					num = Guid.HexsToChars(ptr4, num, (int)this._j, (int)this._k);
				}
			}
			return text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure.</summary>
		/// <returns>A new GUID object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600102B RID: 4139 RVA: 0x00047108 File Offset: 0x00045308
		public static Guid NewGuid()
		{
			byte[] array = new byte[16];
			object rngAccess = Guid._rngAccess;
			lock (rngAccess)
			{
				if (Guid._rng == null)
				{
					Guid._rng = RandomNumberGenerator.Create();
				}
				Guid._rng.GetBytes(array);
			}
			Guid guid = new Guid(array);
			guid._d = (guid._d & 63) | 128;
			guid._c = (short)(((long)guid._c & 4095L) | 16384L);
			return guid;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x000471A4 File Offset: 0x000453A4
		internal static byte[] FastNewGuidArray()
		{
			byte[] array = new byte[16];
			object rngAccess = Guid._rngAccess;
			lock (rngAccess)
			{
				if (Guid._rng != null)
				{
					Guid._fastRng = Guid._rng;
				}
				if (Guid._fastRng == null)
				{
					Guid._fastRng = new RNGCryptoServiceProvider();
				}
				Guid._fastRng.GetBytes(array);
			}
			array[8] = (array[8] & 63) | 128;
			array[7] = (array[7] & 15) | 64;
			return array;
		}

		/// <summary>A read-only instance of the <see cref="T:System.Guid" /> structure whose value is all zeros.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040009F7 RID: 2551
		public static readonly Guid Empty = default(Guid);

		// Token: 0x040009F8 RID: 2552
		private int _a;

		// Token: 0x040009F9 RID: 2553
		private short _b;

		// Token: 0x040009FA RID: 2554
		private short _c;

		// Token: 0x040009FB RID: 2555
		private byte _d;

		// Token: 0x040009FC RID: 2556
		private byte _e;

		// Token: 0x040009FD RID: 2557
		private byte _f;

		// Token: 0x040009FE RID: 2558
		private byte _g;

		// Token: 0x040009FF RID: 2559
		private byte _h;

		// Token: 0x04000A00 RID: 2560
		private byte _i;

		// Token: 0x04000A01 RID: 2561
		private byte _j;

		// Token: 0x04000A02 RID: 2562
		private byte _k;

		// Token: 0x04000A03 RID: 2563
		private static object _rngAccess = new object();

		// Token: 0x04000A04 RID: 2564
		private static RandomNumberGenerator _rng;

		// Token: 0x04000A05 RID: 2565
		private static RandomNumberGenerator _fastRng;

		// Token: 0x0200017C RID: 380
		[Flags]
		private enum GuidStyles
		{
			// Token: 0x04000A07 RID: 2567
			None = 0,
			// Token: 0x04000A08 RID: 2568
			AllowParenthesis = 1,
			// Token: 0x04000A09 RID: 2569
			AllowBraces = 2,
			// Token: 0x04000A0A RID: 2570
			AllowDashes = 4,
			// Token: 0x04000A0B RID: 2571
			AllowHexPrefix = 8,
			// Token: 0x04000A0C RID: 2572
			RequireParenthesis = 16,
			// Token: 0x04000A0D RID: 2573
			RequireBraces = 32,
			// Token: 0x04000A0E RID: 2574
			RequireDashes = 64,
			// Token: 0x04000A0F RID: 2575
			RequireHexPrefix = 128,
			// Token: 0x04000A10 RID: 2576
			HexFormat = 160,
			// Token: 0x04000A11 RID: 2577
			NumberFormat = 0,
			// Token: 0x04000A12 RID: 2578
			DigitFormat = 64,
			// Token: 0x04000A13 RID: 2579
			BraceFormat = 96,
			// Token: 0x04000A14 RID: 2580
			ParenthesisFormat = 80,
			// Token: 0x04000A15 RID: 2581
			Any = 15
		}

		// Token: 0x0200017D RID: 381
		private enum GuidParseThrowStyle
		{
			// Token: 0x04000A17 RID: 2583
			None,
			// Token: 0x04000A18 RID: 2584
			All,
			// Token: 0x04000A19 RID: 2585
			AllButOverflow
		}

		// Token: 0x0200017E RID: 382
		private enum ParseFailureKind
		{
			// Token: 0x04000A1B RID: 2587
			None,
			// Token: 0x04000A1C RID: 2588
			ArgumentNull,
			// Token: 0x04000A1D RID: 2589
			Format,
			// Token: 0x04000A1E RID: 2590
			FormatWithParameter,
			// Token: 0x04000A1F RID: 2591
			NativeException,
			// Token: 0x04000A20 RID: 2592
			FormatWithInnerException
		}

		// Token: 0x0200017F RID: 383
		private struct GuidResult
		{
			// Token: 0x0600102E RID: 4142 RVA: 0x00047247 File Offset: 0x00045447
			internal void Init(Guid.GuidParseThrowStyle canThrow)
			{
				this.parsedGuid = Guid.Empty;
				this.throwStyle = canThrow;
			}

			// Token: 0x0600102F RID: 4143 RVA: 0x0004725B File Offset: 0x0004545B
			internal void SetFailure(Exception nativeException)
			{
				this.m_failure = Guid.ParseFailureKind.NativeException;
				this.m_innerException = nativeException;
			}

			// Token: 0x06001030 RID: 4144 RVA: 0x0004726B File Offset: 0x0004546B
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID)
			{
				this.SetFailure(failure, failureMessageID, null, null, null);
			}

			// Token: 0x06001031 RID: 4145 RVA: 0x00047278 File Offset: 0x00045478
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, null, null);
			}

			// Token: 0x06001032 RID: 4146 RVA: 0x00047285 File Offset: 0x00045485
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName, Exception innerException)
			{
				this.m_failure = failure;
				this.m_failureMessageID = failureMessageID;
				this.m_failureMessageFormatArgument = failureMessageFormatArgument;
				this.m_failureArgumentName = failureArgumentName;
				this.m_innerException = innerException;
				if (this.throwStyle != Guid.GuidParseThrowStyle.None)
				{
					throw this.GetGuidParseException();
				}
			}

			// Token: 0x06001033 RID: 4147 RVA: 0x000472BC File Offset: 0x000454BC
			internal Exception GetGuidParseException()
			{
				switch (this.m_failure)
				{
				case Guid.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this.m_failureArgumentName, Environment.GetResourceString(this.m_failureMessageID));
				case Guid.ParseFailureKind.Format:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID));
				case Guid.ParseFailureKind.FormatWithParameter:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID, new object[] { this.m_failureMessageFormatArgument }));
				case Guid.ParseFailureKind.NativeException:
					return this.m_innerException;
				case Guid.ParseFailureKind.FormatWithInnerException:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID), this.m_innerException);
				default:
					return new FormatException(Environment.GetResourceString("Unrecognized Guid format."));
				}
			}

			// Token: 0x04000A21 RID: 2593
			internal Guid parsedGuid;

			// Token: 0x04000A22 RID: 2594
			internal Guid.GuidParseThrowStyle throwStyle;

			// Token: 0x04000A23 RID: 2595
			internal Guid.ParseFailureKind m_failure;

			// Token: 0x04000A24 RID: 2596
			internal string m_failureMessageID;

			// Token: 0x04000A25 RID: 2597
			internal object m_failureMessageFormatArgument;

			// Token: 0x04000A26 RID: 2598
			internal string m_failureArgumentName;

			// Token: 0x04000A27 RID: 2599
			internal Exception m_innerException;
		}
	}
}
