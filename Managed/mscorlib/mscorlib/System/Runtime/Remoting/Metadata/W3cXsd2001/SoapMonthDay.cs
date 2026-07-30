using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD gMonthDay type.</summary>
	// Token: 0x020007DF RID: 2015
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonthDay : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> class.</summary>
		// Token: 0x060050F9 RID: 20729 RVA: 0x00002111 File Offset: 0x00000311
		public SoapMonthDay()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x060050FA RID: 20730 RVA: 0x001204AB File Offset: 0x0011E6AB
		public SoapMonthDay(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x001204BA File Offset: 0x0011E6BA
		// (set) Token: 0x060050FC RID: 20732 RVA: 0x001204C2 File Offset: 0x0011E6C2
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
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x060050FD RID: 20733 RVA: 0x001204CB File Offset: 0x0011E6CB
		public static string XsdType
		{
			get
			{
				return "gMonthDay";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050FE RID: 20734 RVA: 0x001204D2 File Offset: 0x0011E6D2
		public string GetXsdType()
		{
			return SoapMonthDay.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x060050FF RID: 20735 RVA: 0x001204D9 File Offset: 0x0011E6D9
		public static SoapMonthDay Parse(string value)
		{
			return new SoapMonthDay(DateTime.ParseExact(value, SoapMonthDay._datetimeFormats, null, DateTimeStyles.None));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay.Value" /> in the format "'--'MM'-'dd".</returns>
		// Token: 0x06005100 RID: 20736 RVA: 0x001204ED File Offset: 0x0011E6ED
		public override string ToString()
		{
			return this._value.ToString("--MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x04002AA8 RID: 10920
		private static readonly string[] _datetimeFormats = new string[] { "--MM-dd", "--MM-ddzzz" };

		// Token: 0x04002AA9 RID: 10921
		private DateTime _value;
	}
}
