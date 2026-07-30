using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD gDay type. </summary>
	// Token: 0x020007D3 RID: 2003
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDay : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> class.</summary>
		// Token: 0x0600509D RID: 20637 RVA: 0x00002111 File Offset: 0x00000311
		public SoapDay()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x0600509E RID: 20638 RVA: 0x0011FCD8 File Offset: 0x0011DED8
		public SoapDay(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600509F RID: 20639 RVA: 0x0011FCE7 File Offset: 0x0011DEE7
		// (set) Token: 0x060050A0 RID: 20640 RVA: 0x0011FCEF File Offset: 0x0011DEEF
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
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060050A1 RID: 20641 RVA: 0x0011FCF8 File Offset: 0x0011DEF8
		public static string XsdType
		{
			get
			{
				return "gDay";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050A2 RID: 20642 RVA: 0x0011FCFF File Offset: 0x0011DEFF
		public string GetXsdType()
		{
			return SoapDay.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x060050A3 RID: 20643 RVA: 0x0011FD06 File Offset: 0x0011DF06
		public static SoapDay Parse(string value)
		{
			return new SoapDay(DateTime.ParseExact(value, SoapDay._datetimeFormats, null, DateTimeStyles.None));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay.Value" /> in the format "---dd".</returns>
		// Token: 0x060050A4 RID: 20644 RVA: 0x0011FD1A File Offset: 0x0011DF1A
		public override string ToString()
		{
			return this._value.ToString("---dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x04002A9B RID: 10907
		private static readonly string[] _datetimeFormats = new string[] { "---dd", "---ddzzz" };

		// Token: 0x04002A9C RID: 10908
		private DateTime _value;
	}
}
