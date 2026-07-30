using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD date type.</summary>
	// Token: 0x020007D1 RID: 2001
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDate : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class.</summary>
		// Token: 0x0600508C RID: 20620 RVA: 0x00002111 File Offset: 0x00000311
		public SoapDate()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x0600508D RID: 20621 RVA: 0x0011FABD File Offset: 0x0011DCBD
		public SoapDate(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class with a specified <see cref="T:System.DateTime" /> object and an integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> is a positive or negative value.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		/// <param name="sign">An integer that indicates whether <paramref name="value" /> is positive. </param>
		// Token: 0x0600508E RID: 20622 RVA: 0x0011FACC File Offset: 0x0011DCCC
		public SoapDate(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		/// <summary>Gets or sets whether the date and time of the current instance is positive or negative.</summary>
		/// <returns>An integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> is positive or negative.</returns>
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x0600508F RID: 20623 RVA: 0x0011FAE2 File Offset: 0x0011DCE2
		// (set) Token: 0x06005090 RID: 20624 RVA: 0x0011FAEA File Offset: 0x0011DCEA
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
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06005091 RID: 20625 RVA: 0x0011FAF3 File Offset: 0x0011DCF3
		// (set) Token: 0x06005092 RID: 20626 RVA: 0x0011FAFB File Offset: 0x0011DCFB
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
		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06005093 RID: 20627 RVA: 0x0011FB04 File Offset: 0x0011DD04
		public static string XsdType
		{
			get
			{
				return "date";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005094 RID: 20628 RVA: 0x0011FB0B File Offset: 0x0011DD0B
		public string GetXsdType()
		{
			return SoapDate.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x06005095 RID: 20629 RVA: 0x0011FB14 File Offset: 0x0011DD14
		public static SoapDate Parse(string value)
		{
			SoapDate soapDate = new SoapDate(DateTime.ParseExact(value, SoapDate._datetimeFormats, null, DateTimeStyles.None));
			if (value.StartsWith("-"))
			{
				soapDate.Sign = -1;
			}
			else
			{
				soapDate.Sign = 0;
			}
			return soapDate;
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> in the format "yyyy-MM-dd" or "'-'yyyy-MM-dd" if <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Sign" /> is negative.</returns>
		// Token: 0x06005096 RID: 20630 RVA: 0x0011FB52 File Offset: 0x0011DD52
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x04002A97 RID: 10903
		private static readonly string[] _datetimeFormats = new string[] { "yyyy-MM-dd", "'+'yyyy-MM-dd", "'-'yyyy-MM-dd", "yyyy-MM-ddzzz", "'+'yyyy-MM-ddzzz", "'-'yyyy-MM-ddzzz" };

		// Token: 0x04002A98 RID: 10904
		private int _sign;

		// Token: 0x04002A99 RID: 10905
		private DateTime _value;
	}
}
