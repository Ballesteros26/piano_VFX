using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD time type. </summary>
	// Token: 0x020007EB RID: 2027
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapTime : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> class.</summary>
		// Token: 0x06005160 RID: 20832 RVA: 0x00002111 File Offset: 0x00000311
		public SoapTime()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x06005161 RID: 20833 RVA: 0x00120934 File Offset: 0x0011EB34
		public SoapTime(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06005162 RID: 20834 RVA: 0x00120943 File Offset: 0x0011EB43
		// (set) Token: 0x06005163 RID: 20835 RVA: 0x0012094B File Offset: 0x0011EB4B
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06005164 RID: 20836 RVA: 0x00120954 File Offset: 0x0011EB54
		public static string XsdType
		{
			get
			{
				return "time";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005165 RID: 20837 RVA: 0x0012095B File Offset: 0x0011EB5B
		public string GetXsdType()
		{
			return SoapTime.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x06005166 RID: 20838 RVA: 0x00120962 File Offset: 0x0011EB62
		public static SoapTime Parse(string value)
		{
			return new SoapTime(DateTime.ParseExact(value, SoapTime._datetimeFormats, null, DateTimeStyles.None));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapTime.Value" /> in the format "HH:mm:ss.fffzzz".</returns>
		// Token: 0x06005167 RID: 20839 RVA: 0x00120976 File Offset: 0x0011EB76
		public override string ToString()
		{
			return this._value.ToString("HH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x04002AB7 RID: 10935
		private static readonly string[] _datetimeFormats = new string[]
		{
			"HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:sszzz", "HH:mm:ss.fzzz",
			"HH:mm:ss.ffzzz", "HH:mm:ss.fffzzz", "HH:mm:ss.ffffzzz", "HH:mm:ss.fffffzzz", "HH:mm:ss.ffffffzzz", "HH:mm:ss.fffffffzzz", "HH:mm:ssZ", "HH:mm:ss.fZ", "HH:mm:ss.ffZ", "HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ", "HH:mm:ss.fffffZ", "HH:mm:ss.ffffffZ", "HH:mm:ss.fffffffZ"
		};

		// Token: 0x04002AB8 RID: 10936
		private DateTime _value;
	}
}
