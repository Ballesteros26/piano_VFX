using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x020004A7 RID: 1191
	internal abstract class WebProxyDataBuilder
	{
		// Token: 0x0600230D RID: 8973 RVA: 0x00087AF2 File Offset: 0x00085CF2
		public WebProxyData Build()
		{
			this.m_Result = new WebProxyData();
			this.BuildInternal();
			return this.m_Result;
		}

		// Token: 0x0600230E RID: 8974
		protected abstract void BuildInternal();

		// Token: 0x0600230F RID: 8975 RVA: 0x00087B0C File Offset: 0x00085D0C
		protected void SetProxyAndBypassList(string addressString, string bypassListString)
		{
			if (addressString != null)
			{
				addressString = addressString.Trim();
				if (addressString != string.Empty)
				{
					if (addressString.IndexOf('=') == -1)
					{
						this.m_Result.proxyAddress = WebProxyDataBuilder.ParseProxyUri(addressString);
					}
					else
					{
						this.m_Result.proxyHostAddresses = WebProxyDataBuilder.ParseProtocolProxies(addressString);
					}
					if (bypassListString != null)
					{
						bypassListString = bypassListString.Trim();
						if (bypassListString != string.Empty)
						{
							bool flag = false;
							this.m_Result.bypassList = WebProxyDataBuilder.ParseBypassList(bypassListString, out flag);
							this.m_Result.bypassOnLocal = flag;
						}
					}
				}
			}
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x00087B9C File Offset: 0x00085D9C
		protected void SetAutoProxyUrl(string autoConfigUrl)
		{
			if (!string.IsNullOrEmpty(autoConfigUrl))
			{
				Uri uri = null;
				if (Uri.TryCreate(autoConfigUrl, UriKind.Absolute, out uri))
				{
					this.m_Result.scriptLocation = uri;
				}
			}
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00087BCA File Offset: 0x00085DCA
		protected void SetAutoDetectSettings(bool value)
		{
			this.m_Result.automaticallyDetectSettings = value;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x00087BD8 File Offset: 0x00085DD8
		private static Uri ParseProxyUri(string proxyString)
		{
			if (proxyString.IndexOf("://") == -1)
			{
				proxyString = "http://" + proxyString;
			}
			Uri uri;
			try
			{
				uri = new Uri(proxyString);
			}
			catch (UriFormatException)
			{
				bool on = Logging.On;
				throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyString);
			}
			return uri;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00087C2C File Offset: 0x00085E2C
		private static Hashtable ParseProtocolProxies(string proxyListString)
		{
			string[] array = proxyListString.Split(new char[] { ';' });
			Hashtable hashtable = new Hashtable(CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (!(text == string.Empty))
				{
					string[] array2 = text.Split(new char[] { '=' });
					if (array2.Length != 2)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					array2[0] = array2[0].Trim();
					array2[1] = array2[1].Trim();
					if (array2[0] == string.Empty || array2[1] == string.Empty)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					hashtable[array2[0]] = WebProxyDataBuilder.ParseProxyUri(array2[1]);
				}
			}
			return hashtable;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00087CF6 File Offset: 0x00085EF6
		private static FormatException CreateInvalidProxyStringException(string originalProxyString)
		{
			string @string = global::SR.GetString("The system proxy settings contain an invalid proxy server setting: '{0}'.", new object[] { originalProxyString });
			bool on = Logging.On;
			return new FormatException(@string);
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00087D18 File Offset: 0x00085F18
		private static string BypassStringEscape(string rawString)
		{
			Match match = new Regex("^(?<scheme>.*://)?(?<host>[^:]*)(?<port>:[0-9]{1,5})?$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Match(rawString);
			string text;
			string text2;
			string text3;
			if (match.Success)
			{
				text = match.Groups["scheme"].Value;
				text2 = match.Groups["host"].Value;
				text3 = match.Groups["port"].Value;
			}
			else
			{
				text = string.Empty;
				text2 = rawString;
				text3 = string.Empty;
			}
			text = WebProxyDataBuilder.ConvertRegexReservedChars(text);
			text2 = WebProxyDataBuilder.ConvertRegexReservedChars(text2);
			text3 = WebProxyDataBuilder.ConvertRegexReservedChars(text3);
			if (text == string.Empty)
			{
				text = "(?:.*://)?";
			}
			if (text3 == string.Empty)
			{
				text3 = "(?::[0-9]{1,5})?";
			}
			return string.Concat(new string[] { "^", text, text2, text3, "$" });
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00087DF8 File Offset: 0x00085FF8
		private static string ConvertRegexReservedChars(string rawString)
		{
			if (rawString.Length == 0)
			{
				return rawString;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in rawString)
			{
				if ("#$()+.?[\\^{|".IndexOf(c) != -1)
				{
					stringBuilder.Append('\\');
				}
				else if (c == '*')
				{
					stringBuilder.Append('.');
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00087E68 File Offset: 0x00086068
		private static ArrayList ParseBypassList(string bypassListString, out bool bypassOnLocal)
		{
			string[] array = bypassListString.Split(new char[] { ';' });
			bypassOnLocal = false;
			if (array.Length == 0)
			{
				return null;
			}
			ArrayList arrayList = null;
			foreach (string text in array)
			{
				if (text != null)
				{
					string text2 = text.Trim();
					if (text2.Length > 0)
					{
						if (string.Compare(text2, "<local>", StringComparison.OrdinalIgnoreCase) == 0)
						{
							bypassOnLocal = true;
						}
						else
						{
							text2 = WebProxyDataBuilder.BypassStringEscape(text2);
							if (arrayList == null)
							{
								arrayList = new ArrayList();
							}
							if (!arrayList.Contains(text2))
							{
								arrayList.Add(text2);
							}
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04001F53 RID: 8019
		private const char addressListDelimiter = ';';

		// Token: 0x04001F54 RID: 8020
		private const char addressListSchemeValueDelimiter = '=';

		// Token: 0x04001F55 RID: 8021
		private const char bypassListDelimiter = ';';

		// Token: 0x04001F56 RID: 8022
		private WebProxyData m_Result;

		// Token: 0x04001F57 RID: 8023
		private const string regexReserved = "#$()+.?[\\^{|";
	}
}
