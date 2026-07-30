using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD nonNegativeInteger type.</summary>
	// Token: 0x020007E5 RID: 2021
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonNegativeInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> class.</summary>
		// Token: 0x0600512A RID: 20778 RVA: 0x00002111 File Offset: 0x00000311
		public SoapNonNegativeInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 0. </exception>
		// Token: 0x0600512B RID: 20779 RVA: 0x00120679 File Offset: 0x0011E879
		public SoapNonNegativeInteger(decimal value)
		{
			if (value < 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value);
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 0. </exception>
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x001206AC File Offset: 0x0011E8AC
		// (set) Token: 0x0600512D RID: 20781 RVA: 0x001206B4 File Offset: 0x0011E8B4
		public decimal Value
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
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x001206BD File Offset: 0x0011E8BD
		public static string XsdType
		{
			get
			{
				return "nonNegativeInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600512F RID: 20783 RVA: 0x001206C4 File Offset: 0x0011E8C4
		public string GetXsdType()
		{
			return SoapNonNegativeInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> object that is obtained from <paramref name="value" />. </returns>
		/// <param name="value">The <see cref="T:System.String" />  to convert. </param>
		// Token: 0x06005130 RID: 20784 RVA: 0x001206CB File Offset: 0x0011E8CB
		public static SoapNonNegativeInteger Parse(string value)
		{
			return new SoapNonNegativeInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger.Value" />.</returns>
		// Token: 0x06005131 RID: 20785 RVA: 0x001206D8 File Offset: 0x0011E8D8
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04002AAF RID: 10927
		private decimal _value;
	}
}
