using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200004A RID: 74
	internal class RequestResponseUtils
	{
		// Token: 0x06000191 RID: 401 RVA: 0x0000210F File Offset: 0x0000030F
		private RequestResponseUtils()
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007368 File Offset: 0x00005568
		internal static Encoding GetEncoding(string contentType)
		{
			string charset = ContentType.GetCharset(contentType);
			Encoding encoding = null;
			try
			{
				if (charset != null && charset.Length > 0)
				{
					encoding = Encoding.GetEncoding(charset);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, typeof(RequestResponseUtils), "GetEncoding", ex);
				}
			}
			if (encoding != null)
			{
				return encoding;
			}
			return new ASCIIEncoding();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000073E8 File Offset: 0x000055E8
		internal static Encoding GetEncoding2(string contentType)
		{
			if (!ContentType.IsApplication(contentType))
			{
				return RequestResponseUtils.GetEncoding(contentType);
			}
			string charset = ContentType.GetCharset(contentType);
			Encoding encoding = null;
			try
			{
				if (charset != null && charset.Length > 0)
				{
					encoding = Encoding.GetEncoding(charset);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, typeof(RequestResponseUtils), "GetEncoding2", ex);
				}
			}
			return encoding;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007470 File Offset: 0x00005670
		internal static string ReadResponse(WebResponse response)
		{
			return RequestResponseUtils.ReadResponse(response, response.GetResponseStream());
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007480 File Offset: 0x00005680
		internal static string ReadResponse(WebResponse response, Stream stream)
		{
			Encoding encoding = RequestResponseUtils.GetEncoding(response.ContentType);
			if (encoding == null)
			{
				encoding = Encoding.Default;
			}
			StreamReader streamReader = new StreamReader(stream, encoding, true);
			string text;
			try
			{
				text = streamReader.ReadToEnd();
			}
			finally
			{
				stream.Close();
			}
			return text;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000074CC File Offset: 0x000056CC
		internal static Stream StreamToMemoryStream(Stream stream)
		{
			MemoryStream memoryStream = new MemoryStream(1024);
			byte[] array = new byte[1024];
			int num;
			while ((num = stream.Read(array, 0, array.Length)) != 0)
			{
				memoryStream.Write(array, 0, num);
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007512 File Offset: 0x00005712
		internal static string CreateResponseExceptionString(WebResponse response)
		{
			return RequestResponseUtils.CreateResponseExceptionString(response, response.GetResponseStream());
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007520 File Offset: 0x00005720
		internal static string CreateResponseExceptionString(WebResponse response, Stream stream)
		{
			if (response is HttpWebResponse)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)response;
				int statusCode = (int)httpWebResponse.StatusCode;
				if (statusCode >= 400 && statusCode != 500)
				{
					return Res.GetString("WebResponseKnownError", new object[] { statusCode, httpWebResponse.StatusDescription });
				}
			}
			string text = ((stream != null) ? RequestResponseUtils.ReadResponse(response, stream) : string.Empty);
			if (text.Length > 0)
			{
				text = RequestResponseUtils.HttpUtility.HtmlDecode(text);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(Res.GetString("WebResponseUnknownError"));
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("--");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(text);
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("--");
				stringBuilder.Append(".");
				return stringBuilder.ToString();
			}
			return Res.GetString("WebResponseUnknownErrorEmptyBody");
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007610 File Offset: 0x00005810
		internal static int GetBufferSize(int contentLength)
		{
			int num;
			if (contentLength == -1)
			{
				num = 8000;
			}
			else if (contentLength <= 16000)
			{
				num = contentLength;
			}
			else
			{
				num = 16000;
			}
			return num;
		}

		// Token: 0x0200004B RID: 75
		private static class HttpUtility
		{
			// Token: 0x0600019A RID: 410 RVA: 0x0000763C File Offset: 0x0000583C
			internal static string HtmlDecode(string s)
			{
				if (s == null)
				{
					return null;
				}
				if (s.IndexOf('&') < 0)
				{
					return s;
				}
				StringBuilder stringBuilder = new StringBuilder();
				StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
				RequestResponseUtils.HttpUtility.HtmlDecode(s, stringWriter);
				return stringBuilder.ToString();
			}

			// Token: 0x0600019B RID: 411 RVA: 0x00007678 File Offset: 0x00005878
			public static void HtmlDecode(string s, TextWriter output)
			{
				if (s == null)
				{
					return;
				}
				if (s.IndexOf('&') < 0)
				{
					output.Write(s);
					return;
				}
				int length = s.Length;
				int i = 0;
				while (i < length)
				{
					char c = s[i];
					if (c != '&')
					{
						goto IL_014C;
					}
					int num = s.IndexOfAny(RequestResponseUtils.HttpUtility.s_entityEndingChars, i + 1);
					if (num <= 0 || s[num] != ';')
					{
						goto IL_014C;
					}
					string text = s.Substring(i + 1, num - i - 1);
					if (text.Length > 1 && text[0] == '#')
					{
						try
						{
							if (text[1] == 'x' || text[1] == 'X')
							{
								c = (char)int.Parse(text.Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
							}
							else
							{
								c = (char)int.Parse(text.Substring(1), CultureInfo.InvariantCulture);
							}
							i = num;
							goto IL_014C;
						}
						catch (FormatException ex)
						{
							i++;
							if (Tracing.On)
							{
								Tracing.ExceptionCatch(TraceEventType.Warning, typeof(RequestResponseUtils.HttpUtility), "HtmlDecode", ex);
							}
							goto IL_014C;
						}
						catch (ArgumentException ex2)
						{
							i++;
							if (Tracing.On)
							{
								Tracing.ExceptionCatch(TraceEventType.Warning, typeof(RequestResponseUtils.HttpUtility), "HtmlDecode", ex2);
							}
							goto IL_014C;
						}
					}
					i = num;
					char c2 = RequestResponseUtils.HttpUtility.HtmlEntities.Lookup(text);
					if (c2 != '\0')
					{
						c = c2;
						goto IL_014C;
					}
					output.Write('&');
					output.Write(text);
					output.Write(';');
					IL_0153:
					i++;
					continue;
					IL_014C:
					output.Write(c);
					goto IL_0153;
				}
			}

			// Token: 0x0400021F RID: 543
			private static char[] s_entityEndingChars = new char[] { ';', '&' };

			// Token: 0x0200004C RID: 76
			private static class HtmlEntities
			{
				// Token: 0x0600019D RID: 413 RVA: 0x00007818 File Offset: 0x00005A18
				internal static char Lookup(string entity)
				{
					if (RequestResponseUtils.HttpUtility.HtmlEntities._entitiesLookupTable == null)
					{
						object lookupLockObject = RequestResponseUtils.HttpUtility.HtmlEntities._lookupLockObject;
						lock (lookupLockObject)
						{
							if (RequestResponseUtils.HttpUtility.HtmlEntities._entitiesLookupTable == null)
							{
								Hashtable hashtable = new Hashtable();
								foreach (string text in RequestResponseUtils.HttpUtility.HtmlEntities._entitiesList)
								{
									hashtable[text.Substring(2)] = text[0];
								}
								RequestResponseUtils.HttpUtility.HtmlEntities._entitiesLookupTable = hashtable;
							}
						}
					}
					object obj = RequestResponseUtils.HttpUtility.HtmlEntities._entitiesLookupTable[entity];
					if (obj != null)
					{
						return (char)obj;
					}
					return '\0';
				}

				// Token: 0x04000220 RID: 544
				private static object _lookupLockObject = new object();

				// Token: 0x04000221 RID: 545
				private static string[] _entitiesList = new string[]
				{
					"\"-quot", "&-amp", "<-lt", ">-gt", "\u00a0-nbsp", "¡-iexcl", "¢-cent", "£-pound", "¤-curren", "¥-yen",
					"¦-brvbar", "§-sect", "\u00a8-uml", "©-copy", "ª-ordf", "«-laquo", "¬-not", "\u00ad-shy", "®-reg", "\u00af-macr",
					"°-deg", "±-plusmn", "²-sup2", "³-sup3", "\u00b4-acute", "µ-micro", "¶-para", "·-middot", "\u00b8-cedil", "¹-sup1",
					"º-ordm", "»-raquo", "¼-frac14", "½-frac12", "¾-frac34", "¿-iquest", "À-Agrave", "Á-Aacute", "Â-Acirc", "Ã-Atilde",
					"Ä-Auml", "Å-Aring", "Æ-AElig", "Ç-Ccedil", "È-Egrave", "É-Eacute", "Ê-Ecirc", "Ë-Euml", "Ì-Igrave", "Í-Iacute",
					"Î-Icirc", "Ï-Iuml", "Ð-ETH", "Ñ-Ntilde", "Ò-Ograve", "Ó-Oacute", "Ô-Ocirc", "Õ-Otilde", "Ö-Ouml", "×-times",
					"Ø-Oslash", "Ù-Ugrave", "Ú-Uacute", "Û-Ucirc", "Ü-Uuml", "Ý-Yacute", "Þ-THORN", "ß-szlig", "à-agrave", "á-aacute",
					"â-acirc", "ã-atilde", "ä-auml", "å-aring", "æ-aelig", "ç-ccedil", "è-egrave", "é-eacute", "ê-ecirc", "ë-euml",
					"ì-igrave", "í-iacute", "î-icirc", "ï-iuml", "ð-eth", "ñ-ntilde", "ò-ograve", "ó-oacute", "ô-ocirc", "õ-otilde",
					"ö-ouml", "÷-divide", "ø-oslash", "ù-ugrave", "ú-uacute", "û-ucirc", "ü-uuml", "ý-yacute", "þ-thorn", "ÿ-yuml",
					"Œ-OElig", "œ-oelig", "Š-Scaron", "š-scaron", "Ÿ-Yuml", "ƒ-fnof", "ˆ-circ", "\u02dc-tilde", "Α-Alpha", "Β-Beta",
					"Γ-Gamma", "Δ-Delta", "Ε-Epsilon", "Ζ-Zeta", "Η-Eta", "Θ-Theta", "Ι-Iota", "Κ-Kappa", "Λ-Lambda", "Μ-Mu",
					"Ν-Nu", "Ξ-Xi", "Ο-Omicron", "Π-Pi", "Ρ-Rho", "Σ-Sigma", "Τ-Tau", "Υ-Upsilon", "Φ-Phi", "Χ-Chi",
					"Ψ-Psi", "Ω-Omega", "α-alpha", "β-beta", "γ-gamma", "δ-delta", "ε-epsilon", "ζ-zeta", "η-eta", "θ-theta",
					"ι-iota", "κ-kappa", "λ-lambda", "μ-mu", "ν-nu", "ξ-xi", "ο-omicron", "π-pi", "ρ-rho", "ς-sigmaf",
					"σ-sigma", "τ-tau", "υ-upsilon", "φ-phi", "χ-chi", "ψ-psi", "ω-omega", "ϑ-thetasym", "ϒ-upsih", "ϖ-piv",
					"\u2002-ensp", "\u2003-emsp", "\u2009-thinsp", "\u200c-zwnj", "\u200d-zwj", "\u200e-lrm", "\u200f-rlm", "–-ndash", "—-mdash", "‘-lsquo",
					"’-rsquo", "‚-sbquo", "“-ldquo", "”-rdquo", "„-bdquo", "†-dagger", "‡-Dagger", "•-bull", "…-hellip", "‰-permil",
					"′-prime", "″-Prime", "‹-lsaquo", "›-rsaquo", "‾-oline", "⁄-frasl", "€-euro", "ℑ-image", "℘-weierp", "ℜ-real",
					"™-trade", "ℵ-alefsym", "←-larr", "↑-uarr", "→-rarr", "↓-darr", "↔-harr", "↵-crarr", "⇐-lArr", "⇑-uArr",
					"⇒-rArr", "⇓-dArr", "⇔-hArr", "∀-forall", "∂-part", "∃-exist", "∅-empty", "∇-nabla", "∈-isin", "∉-notin",
					"∋-ni", "∏-prod", "∑-sum", "−-minus", "∗-lowast", "√-radic", "∝-prop", "∞-infin", "∠-ang", "∧-and",
					"∨-or", "∩-cap", "∪-cup", "∫-int", "∴-there4", "∼-sim", "≅-cong", "≈-asymp", "≠-ne", "≡-equiv",
					"≤-le", "≥-ge", "⊂-sub", "⊃-sup", "⊄-nsub", "⊆-sube", "⊇-supe", "⊕-oplus", "⊗-otimes", "⊥-perp",
					"⋅-sdot", "⌈-lceil", "⌉-rceil", "⌊-lfloor", "⌋-rfloor", "〈-lang", "〉-rang", "◊-loz", "♠-spades", "♣-clubs",
					"♥-hearts", "♦-diams"
				};

				// Token: 0x04000222 RID: 546
				private static volatile Hashtable _entitiesLookupTable;
			}
		}
	}
}
