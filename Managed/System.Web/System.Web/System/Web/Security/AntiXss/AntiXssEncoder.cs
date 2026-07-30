using System;
using System.Text;
using System.Web.Util;
using Unity;

namespace System.Web.Security.AntiXss
{
	/// <summary>Encodes a string for use in HTML, XML, CSS, and URL strings.</summary>
	// Token: 0x020006ED RID: 1773
	public class AntiXssEncoder : HttpEncoder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.AntiXss.AntiXssEncoder" /> class.</summary>
		// Token: 0x06004AFB RID: 19195 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public AntiXssEncoder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Encodes the specified string for use in cascading style sheets (CSS).</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004AFC RID: 19196 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string CssEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use as text in HTML markup and optionally specifies whether to use HTML 4.0 named entities.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="useNamedEntities">true to use HTML 4.0 named entities for certain character encodings; false to encode by using only &amp;#DECIMAL; notation.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004AFD RID: 19197 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string HtmlEncode(string input, bool useNamedEntities)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in form submissions whose MIME type is "application/x-www-form-urlencoded".</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		// Token: 0x06004AFE RID: 19198 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string HtmlFormUrlEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in form submissions whose MIME type is "application/x-www-form-urlencoded" by using the specified code page.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="codePage">The code page to use to encode the <paramref name="input" /> string.</param>
		// Token: 0x06004AFF RID: 19199 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string HtmlFormUrlEncode(string input, int codePage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for form submissions whose MIME type is "application/x-www-form-urlencoded" by using the specified character encoding type.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="inputEncoding">The input encoding type.</param>
		// Token: 0x06004B00 RID: 19200 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string HtmlFormUrlEncode(string input, Encoding inputEncoding)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Marks characters from the specified Unicode code charts as safe.</summary>
		/// <param name="lowerCodeCharts">The combination of lower code charts to mark as safe.</param>
		/// <param name="lowerMidCodeCharts">The combination of lower-middle code charts to mark as safe.</param>
		/// <param name="midCodeCharts">The combination of middle code charts to mark as safe.</param>
		/// <param name="upperMidCodeCharts">The combination of upper-middle code charts to mark as safe.</param>
		/// <param name="upperCodeCharts">The combination of upper code charts to mark as safe.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.AntiXss.AntiXssEncoder.MarkAsSafe(System.Web.Security.AntiXss.LowerCodeCharts,System.Web.Security.AntiXss.LowerMidCodeCharts,System.Web.Security.AntiXss.MidCodeCharts,System.Web.Security.AntiXss.UpperMidCodeCharts,System.Web.Security.AntiXss.UpperCodeCharts)" /> method was called outside the Application_Start method in the Global.asax file.</exception>
		// Token: 0x06004B01 RID: 19201 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void MarkAsSafe(LowerCodeCharts lowerCodeCharts, LowerMidCodeCharts lowerMidCodeCharts, MidCodeCharts midCodeCharts, UpperMidCodeCharts upperMidCodeCharts, UpperCodeCharts upperCodeCharts)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Encodes the specified string for use in a URL.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		// Token: 0x06004B02 RID: 19202 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string UrlEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in a URL by using the specified code page.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="codePage">The code page to use to encode the <paramref name="input" /> string.</param>
		// Token: 0x06004B03 RID: 19203 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string UrlEncode(string input, int codePage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in a URL by using the specified character encoding type.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="inputEncoding">The input encoding type.</param>
		// Token: 0x06004B04 RID: 19204 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string UrlEncode(string input, Encoding inputEncoding)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in XML attributes.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004B05 RID: 19205 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string XmlAttributeEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in XML attributes.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004B06 RID: 19206 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string XmlEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
