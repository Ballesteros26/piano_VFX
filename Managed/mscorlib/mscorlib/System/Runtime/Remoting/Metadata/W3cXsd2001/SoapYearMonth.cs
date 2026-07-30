using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD gYearMonth type. </summary>
	// Token: 0x020007EE RID: 2030
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYearMonth : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> class.</summary>
		// Token: 0x0600517D RID: 20861 RVA: 0x00002111 File Offset: 0x00000311
		public SoapYearMonth()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x0600517E RID: 20862 RVA: 0x00120BB9 File Offset: 0x0011EDB9
		public SoapYearMonth(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> class with a specified <see cref="T:System.DateTime" /> object and an integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Value" /> is a positive or negative value.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		/// <param name="sign">An integer that indicates whether <paramref name="value" /> is positive. </param>
		// Token: 0x0600517F RID: 20863 RVA: 0x00120BC8 File Offset: 0x0011EDC8
		public SoapYearMonth(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		/// <summary>Gets or sets whether the date and time of the current instance is positive or negative.</summary>
		/// <returns>An integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Value" /> is positive or negative.</returns>
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06005180 RID: 20864 RVA: 0x00120BDE File Offset: 0x0011EDDE
		// (set) Token: 0x06005181 RID: 20865 RVA: 0x00120BE6 File Offset: 0x0011EDE6
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06005182 RID: 20866 RVA: 0x00120BEF File Offset: 0x0011EDEF
		// (set) Token: 0x06005183 RID: 20867 RVA: 0x00120BF7 File Offset: 0x0011EDF7
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
		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06005184 RID: 20868 RVA: 0x00120C00 File Offset: 0x0011EE00
		public static string XsdType
		{
			get
			{
				return "gYearMonth";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005185 RID: 20869 RVA: 0x00120C07 File Offset: 0x0011EE07
		public string GetXsdType()
		{
			return SoapYearMonth.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x06005186 RID: 20870 RVA: 0x00120C10 File Offset: 0x0011EE10
		public static SoapYearMonth Parse(string value)
		{
			SoapYearMonth soapYearMonth = new SoapYearMonth(DateTime.ParseExact(value, SoapYearMonth._datetimeFormats, null, DateTimeStyles.None));
			if (value.StartsWith("-"))
			{
				soapYearMonth.Sign = -1;
			}
			else
			{
				soapYearMonth.Sign = 0;
			}
			return soapYearMonth;
		}

		/// <summary>Returns a <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Value" /> in the format "yyyy-MM" or "'-'yyyy-MM" if <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Sign" /> is negative.</returns>
		// Token: 0x06005187 RID: 20871 RVA: 0x00120C4E File Offset: 0x0011EE4E
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy-MM", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy-MM", CultureInfo.InvariantCulture);
		}

		// Token: 0x04002ABD RID: 10941
		private static readonly string[] _datetimeFormats = new string[] { "yyyy-MM", "'+'yyyy-MM", "'-'yyyy-MM", "yyyy-MMzzz", "'+'yyyy-MMzzz", "'-'yyyy-MMzzz" };

		// Token: 0x04002ABE RID: 10942
		private int _sign;

		// Token: 0x04002ABF RID: 10943
		private DateTime _value;
	}
}
