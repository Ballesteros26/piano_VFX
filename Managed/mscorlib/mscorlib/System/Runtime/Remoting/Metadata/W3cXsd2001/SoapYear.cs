using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD gYear type. </summary>
	// Token: 0x020007ED RID: 2029
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYear : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> class.</summary>
		// Token: 0x06005171 RID: 20849 RVA: 0x00002111 File Offset: 0x00000311
		public SoapYear()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x06005172 RID: 20850 RVA: 0x00120AB3 File Offset: 0x0011ECB3
		public SoapYear(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> class with a specified <see cref="T:System.DateTime" /> object and an integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> is a positive or negative value.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		/// <param name="sign">An integer that indicates whether <paramref name="value" /> is positive. </param>
		// Token: 0x06005173 RID: 20851 RVA: 0x00120AC2 File Offset: 0x0011ECC2
		public SoapYear(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		/// <summary>Gets or sets whether the date and time of the current instance is positive or negative.</summary>
		/// <returns>An integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> is positive or negative.</returns>
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06005174 RID: 20852 RVA: 0x00120AD8 File Offset: 0x0011ECD8
		// (set) Token: 0x06005175 RID: 20853 RVA: 0x00120AE0 File Offset: 0x0011ECE0
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
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06005176 RID: 20854 RVA: 0x00120AE9 File Offset: 0x0011ECE9
		// (set) Token: 0x06005177 RID: 20855 RVA: 0x00120AF1 File Offset: 0x0011ECF1
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
		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06005178 RID: 20856 RVA: 0x00120AFA File Offset: 0x0011ECFA
		public static string XsdType
		{
			get
			{
				return "gYear";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005179 RID: 20857 RVA: 0x00120B01 File Offset: 0x0011ED01
		public string GetXsdType()
		{
			return SoapYear.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x0600517A RID: 20858 RVA: 0x00120B08 File Offset: 0x0011ED08
		public static SoapYear Parse(string value)
		{
			SoapYear soapYear = new SoapYear(DateTime.ParseExact(value, SoapYear._datetimeFormats, null, DateTimeStyles.None));
			if (value.StartsWith("-"))
			{
				soapYear.Sign = -1;
			}
			else
			{
				soapYear.Sign = 0;
			}
			return soapYear;
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> in the format "yyyy" or "-yyyy" if <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Sign" /> is negative.</returns>
		// Token: 0x0600517B RID: 20859 RVA: 0x00120B46 File Offset: 0x0011ED46
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy", CultureInfo.InvariantCulture);
		}

		// Token: 0x04002ABA RID: 10938
		private static readonly string[] _datetimeFormats = new string[] { "yyyy", "'+'yyyy", "'-'yyyy", "yyyyzzz", "'+'yyyyzzz", "'-'yyyyzzz" };

		// Token: 0x04002ABB RID: 10939
		private int _sign;

		// Token: 0x04002ABC RID: 10940
		private DateTime _value;
	}
}
