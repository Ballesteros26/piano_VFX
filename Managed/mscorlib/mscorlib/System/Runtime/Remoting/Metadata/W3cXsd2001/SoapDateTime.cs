using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Provides static methods for the serialization and deserialization of <see cref="T:System.DateTime" /> to a string that is formatted as XSD dateTime. </summary>
	// Token: 0x020007D2 RID: 2002
	[ComVisible(true)]
	public sealed class SoapDateTime
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x0011FBC5 File Offset: 0x0011DDC5
		public static string XsdType
		{
			get
			{
				return "dateTime";
			}
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following: <paramref name="value" /> is an empty string.<paramref name="value" /> is null reference.<paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x0600509A RID: 20634 RVA: 0x0011FBCC File Offset: 0x0011DDCC
		public static DateTime Parse(string value)
		{
			return DateTime.ParseExact(value, SoapDateTime._datetimeFormats, null, DateTimeStyles.None);
		}

		/// <summary>Returns the specified <see cref="T:System.DateTime" /> object as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representation of <paramref name="value" /> in the format "yyyy-MM-dd'T'HH:mm:ss.fffffffzzz".</returns>
		/// <param name="value">The <see cref="T:System.DateTime" /> object to convert. </param>
		// Token: 0x0600509B RID: 20635 RVA: 0x0011FBDB File Offset: 0x0011DDDB
		public static string ToString(DateTime value)
		{
			return value.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x04002A9A RID: 10906
		private static readonly string[] _datetimeFormats = new string[]
		{
			"yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss.f", "yyyy-MM-ddTHH:mm:ss.ff", "yyyy-MM-ddTHH:mm:ss.fff", "yyyy-MM-ddTHH:mm:ss.ffff", "yyyy-MM-ddTHH:mm:ss.fffff", "yyyy-MM-ddTHH:mm:ss.ffffff", "yyyy-MM-ddTHH:mm:ss.fffffff", "yyyy-MM-ddTHH:mm:sszzz", "yyyy-MM-ddTHH:mm:ss.fzzz",
			"yyyy-MM-ddTHH:mm:ss.ffzzz", "yyyy-MM-ddTHH:mm:ss.fffzzz", "yyyy-MM-ddTHH:mm:ss.ffffzzz", "yyyy-MM-ddTHH:mm:ss.fffffzzz", "yyyy-MM-ddTHH:mm:ss.ffffffzzz", "yyyy-MM-ddTHH:mm:ss.fffffffzzz", "yyyy-MM-ddTHH:mm:ssZ", "yyyy-MM-ddTHH:mm:ss.fZ", "yyyy-MM-ddTHH:mm:ss.ffZ", "yyyy-MM-ddTHH:mm:ss.fffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffZ", "yyyy-MM-ddTHH:mm:ss.fffffZ", "yyyy-MM-ddTHH:mm:ss.ffffffZ", "yyyy-MM-ddTHH:mm:ss.fffffffZ"
		};
	}
}
