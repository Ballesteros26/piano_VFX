using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x0200036B RID: 875
	internal class XmlCustomFormatter
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x000DC20C File Offset: 0x000DA40C
		private static DateTimeSerializationSection.DateTimeSerializationMode Mode
		{
			get
			{
				if (XmlCustomFormatter.mode == DateTimeSerializationSection.DateTimeSerializationMode.Default)
				{
					DateTimeSerializationSection dateTimeSerializationSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.DateTimeSerializationSectionPath) as DateTimeSerializationSection;
					if (dateTimeSerializationSection != null)
					{
						XmlCustomFormatter.mode = dateTimeSerializationSection.Mode;
					}
					else
					{
						XmlCustomFormatter.mode = DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip;
					}
				}
				return XmlCustomFormatter.mode;
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000020FD File Offset: 0x000002FD
		private XmlCustomFormatter()
		{
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000DC24C File Offset: 0x000DA44C
		internal static string FromDefaultValue(object value, string formatter)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			if (type == typeof(DateTime))
			{
				if (formatter == "DateTime")
				{
					return XmlCustomFormatter.FromDateTime((DateTime)value);
				}
				if (formatter == "Date")
				{
					return XmlCustomFormatter.FromDate((DateTime)value);
				}
				if (formatter == "Time")
				{
					return XmlCustomFormatter.FromTime((DateTime)value);
				}
			}
			else if (type == typeof(string))
			{
				if (formatter == "XmlName")
				{
					return XmlCustomFormatter.FromXmlName((string)value);
				}
				if (formatter == "XmlNCName")
				{
					return XmlCustomFormatter.FromXmlNCName((string)value);
				}
				if (formatter == "XmlNmToken")
				{
					return XmlCustomFormatter.FromXmlNmToken((string)value);
				}
				if (formatter == "XmlNmTokens")
				{
					return XmlCustomFormatter.FromXmlNmTokens((string)value);
				}
			}
			throw new Exception(Res.GetString("The default value type, {0}, is unsupported.", new object[] { type.FullName }));
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000DC359 File Offset: 0x000DA559
		internal static string FromDate(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-dd");
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000DC368 File Offset: 0x000DA568
		internal static string FromTime(DateTime value)
		{
			if (!LocalAppContextSwitches.IgnoreKindInUtcTimeSerialization && value.Kind == DateTimeKind.Utc)
			{
				return XmlConvert.ToString(DateTime.MinValue + value.TimeOfDay, "HH:mm:ss.fffffffZ");
			}
			return XmlConvert.ToString(DateTime.MinValue + value.TimeOfDay, "HH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000DC3BD File Offset: 0x000DA5BD
		internal static string FromDateTime(DateTime value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
			}
			return XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000DC3DA File Offset: 0x000DA5DA
		internal static string FromChar(char value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000DC3E2 File Offset: 0x000DA5E2
		internal static string FromXmlName(string name)
		{
			return XmlConvert.EncodeName(name);
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000DC3EA File Offset: 0x000DA5EA
		internal static string FromXmlNCName(string ncName)
		{
			return XmlConvert.EncodeLocalName(ncName);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000DC3F2 File Offset: 0x000DA5F2
		internal static string FromXmlNmToken(string nmToken)
		{
			return XmlConvert.EncodeNmToken(nmToken);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000DC3FC File Offset: 0x000DA5FC
		internal static string FromXmlNmTokens(string nmTokens)
		{
			if (nmTokens == null)
			{
				return null;
			}
			if (nmTokens.IndexOf(' ') < 0)
			{
				return XmlCustomFormatter.FromXmlNmToken(nmTokens);
			}
			string[] array = nmTokens.Split(new char[] { ' ' });
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(XmlCustomFormatter.FromXmlNmToken(array[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000DC468 File Offset: 0x000DA668
		internal static void WriteArrayBase64(XmlWriter writer, byte[] inData, int start, int count)
		{
			if (inData == null || count == 0)
			{
				return;
			}
			writer.WriteBase64(inData, start, count);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000DC47A File Offset: 0x000DA67A
		internal static string FromByteArrayHex(byte[] value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.Length == 0)
			{
				return "";
			}
			return XmlConvert.ToBinHexString(value);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000DC494 File Offset: 0x000DA694
		internal static string FromEnum(long val, string[] vals, long[] ids, string typeName)
		{
			long num = val;
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = -1;
			for (int i = 0; i < ids.Length; i++)
			{
				if (ids[i] == 0L)
				{
					num2 = i;
				}
				else
				{
					if (val == 0L)
					{
						break;
					}
					if ((ids[i] & num) == ids[i])
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(" ");
						}
						stringBuilder.Append(vals[i]);
						val &= ~ids[i];
					}
				}
			}
			if (val != 0L)
			{
				throw new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[]
				{
					num,
					(typeName == null) ? "enum" : typeName
				}));
			}
			if (stringBuilder.Length == 0 && num2 >= 0)
			{
				stringBuilder.Append(vals[num2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000DC544 File Offset: 0x000DA744
		internal static object ToDefaultValue(string value, string formatter)
		{
			if (formatter == "DateTime")
			{
				return XmlCustomFormatter.ToDateTime(value);
			}
			if (formatter == "Date")
			{
				return XmlCustomFormatter.ToDate(value);
			}
			if (formatter == "Time")
			{
				return XmlCustomFormatter.ToTime(value);
			}
			if (formatter == "XmlName")
			{
				return XmlCustomFormatter.ToXmlName(value);
			}
			if (formatter == "XmlNCName")
			{
				return XmlCustomFormatter.ToXmlNCName(value);
			}
			if (formatter == "XmlNmToken")
			{
				return XmlCustomFormatter.ToXmlNmToken(value);
			}
			if (formatter == "XmlNmTokens")
			{
				return XmlCustomFormatter.ToXmlNmTokens(value);
			}
			throw new Exception(Res.GetString("The formatter {0} cannot be used for default values.", new object[] { formatter }));
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000DC605 File Offset: 0x000DA805
		internal static DateTime ToDateTime(string value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateTimeFormats);
			}
			return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000DC622 File Offset: 0x000DA822
		internal static DateTime ToDateTime(string value, string[] formats)
		{
			return XmlConvert.ToDateTime(value, formats);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000DC62B File Offset: 0x000DA82B
		internal static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateFormats);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000DC638 File Offset: 0x000DA838
		internal static DateTime ToTime(string value)
		{
			if (!LocalAppContextSwitches.IgnoreKindInUtcTimeSerialization)
			{
				return DateTime.ParseExact(value, XmlCustomFormatter.allTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.RoundtripKind);
			}
			return DateTime.ParseExact(value, XmlCustomFormatter.allTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.NoCurrentDateDefault);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000DC669 File Offset: 0x000DA869
		internal static char ToChar(string value)
		{
			return (char)XmlConvert.ToUInt16(value);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000DC671 File Offset: 0x000DA871
		internal static string ToXmlName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000DC671 File Offset: 0x000DA871
		internal static string ToXmlNCName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000DC671 File Offset: 0x000DA871
		internal static string ToXmlNmToken(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000DC671 File Offset: 0x000DA871
		internal static string ToXmlNmTokens(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000DC67E File Offset: 0x000DA87E
		internal static byte[] ToByteArrayBase64(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				return new byte[0];
			}
			return Convert.FromBase64String(value);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000DC6A2 File Offset: 0x000DA8A2
		internal static byte[] ToByteArrayHex(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			return XmlConvert.FromBinHexString(value);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000DC6B8 File Offset: 0x000DA8B8
		internal static long ToEnum(string val, Hashtable vals, string typeName, bool validate)
		{
			long num = 0L;
			string[] array = val.Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				object obj = vals[array[i]];
				if (obj != null)
				{
					num |= (long)obj;
				}
				else if (validate && array[i].Length > 0)
				{
					throw new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[]
					{
						array[i],
						typeName
					}));
				}
			}
			return num;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000DC725 File Offset: 0x000DA925
		private static string CollapseWhitespace(string value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Trim();
		}

		// Token: 0x04001873 RID: 6259
		private static DateTimeSerializationSection.DateTimeSerializationMode mode;

		// Token: 0x04001874 RID: 6260
		private static string[] allDateTimeFormats = new string[]
		{
			"yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz", "yyyy", "---dd", "---ddZ", "---ddzzzzzz", "--MM-dd", "--MM-ddZ", "--MM-ddzzzzzz", "--MM--", "--MM--Z",
			"--MM--zzzzzz", "yyyy-MM", "yyyy-MMZ", "yyyy-MMzzzzzz", "yyyyzzzzzz", "yyyy-MM-dd", "yyyy-MM-ddZ", "yyyy-MM-ddzzzzzz", "HH:mm:ss", "HH:mm:ss.f",
			"HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:ssZ", "HH:mm:ss.fZ", "HH:mm:ss.ffZ", "HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ", "HH:mm:ss.fffffZ", "HH:mm:ss.ffffffZ", "HH:mm:ss.fffffffZ", "HH:mm:sszzzzzz", "HH:mm:ss.fzzzzzz", "HH:mm:ss.ffzzzzzz", "HH:mm:ss.fffzzzzzz", "HH:mm:ss.ffffzzzzzz", "HH:mm:ss.fffffzzzzzz",
			"HH:mm:ss.ffffffzzzzzz", "HH:mm:ss.fffffffzzzzzz", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss.f", "yyyy-MM-ddTHH:mm:ss.ff", "yyyy-MM-ddTHH:mm:ss.fff", "yyyy-MM-ddTHH:mm:ss.ffff", "yyyy-MM-ddTHH:mm:ss.fffff", "yyyy-MM-ddTHH:mm:ss.ffffff", "yyyy-MM-ddTHH:mm:ss.fffffff",
			"yyyy-MM-ddTHH:mm:ssZ", "yyyy-MM-ddTHH:mm:ss.fZ", "yyyy-MM-ddTHH:mm:ss.ffZ", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.ffffZ", "yyyy-MM-ddTHH:mm:ss.fffffZ", "yyyy-MM-ddTHH:mm:ss.ffffffZ", "yyyy-MM-ddTHH:mm:ss.fffffffZ", "yyyy-MM-ddTHH:mm:sszzzzzz", "yyyy-MM-ddTHH:mm:ss.fzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffffffzzzzzz"
		};

		// Token: 0x04001875 RID: 6261
		private static string[] allDateFormats = new string[]
		{
			"yyyy-MM-ddzzzzzz", "yyyy-MM-dd", "yyyy-MM-ddZ", "yyyy", "---dd", "---ddZ", "---ddzzzzzz", "--MM-dd", "--MM-ddZ", "--MM-ddzzzzzz",
			"--MM--", "--MM--Z", "--MM--zzzzzz", "yyyy-MM", "yyyy-MMZ", "yyyy-MMzzzzzz", "yyyyzzzzzz"
		};

		// Token: 0x04001876 RID: 6262
		private static string[] allTimeFormats = new string[]
		{
			"HH:mm:ss.fffffffzzzzzz", "HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:ssZ",
			"HH:mm:ss.fZ", "HH:mm:ss.ffZ", "HH:mm:ss.fffZ", "HH:mm:ss.ffffZ", "HH:mm:ss.fffffZ", "HH:mm:ss.ffffffZ", "HH:mm:ss.fffffffZ", "HH:mm:sszzzzzz", "HH:mm:ss.fzzzzzz", "HH:mm:ss.ffzzzzzz",
			"HH:mm:ss.fffzzzzzz", "HH:mm:ss.ffffzzzzzz", "HH:mm:ss.fffffzzzzzz", "HH:mm:ss.ffffffzzzzzz"
		};
	}
}
