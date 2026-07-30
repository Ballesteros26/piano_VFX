using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD gMonth type.</summary>
	// Token: 0x020007DE RID: 2014
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonth : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth" /> class.</summary>
		// Token: 0x060050F0 RID: 20720 RVA: 0x00002111 File Offset: 0x00000311
		public SoapMonth()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x060050F1 RID: 20721 RVA: 0x00120435 File Offset: 0x0011E635
		public SoapMonth(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060050F2 RID: 20722 RVA: 0x00120444 File Offset: 0x0011E644
		// (set) Token: 0x060050F3 RID: 20723 RVA: 0x0012044C File Offset: 0x0011E64C
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
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060050F4 RID: 20724 RVA: 0x00120455 File Offset: 0x0011E655
		public static string XsdType
		{
			get
			{
				return "gMonth";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050F5 RID: 20725 RVA: 0x0012045C File Offset: 0x0011E65C
		public string GetXsdType()
		{
			return SoapMonth.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x060050F6 RID: 20726 RVA: 0x00120463 File Offset: 0x0011E663
		public static SoapMonth Parse(string value)
		{
			return new SoapMonth(DateTime.ParseExact(value, SoapMonth._datetimeFormats, null, DateTimeStyles.None));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth.Value" /> in the format "--MM--".</returns>
		// Token: 0x060050F7 RID: 20727 RVA: 0x00120477 File Offset: 0x0011E677
		public override string ToString()
		{
			return this._value.ToString("--MM--", CultureInfo.InvariantCulture);
		}

		// Token: 0x04002AA6 RID: 10918
		private static readonly string[] _datetimeFormats = new string[] { "--MM--", "--MM--zzz" };

		// Token: 0x04002AA7 RID: 10919
		private DateTime _value;
	}
}
