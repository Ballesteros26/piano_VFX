using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD nonPositiveInteger type.</summary>
	// Token: 0x020007E6 RID: 2022
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonPositiveInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger" /> class.</summary>
		// Token: 0x06005132 RID: 20786 RVA: 0x00002111 File Offset: 0x00000311
		public SoapNonPositiveInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than zero. </exception>
		// Token: 0x06005133 RID: 20787 RVA: 0x001206E5 File Offset: 0x0011E8E5
		public SoapNonPositiveInteger(decimal value)
		{
			if (value > 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value);
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than zero. </exception>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06005134 RID: 20788 RVA: 0x00120718 File Offset: 0x0011E918
		// (set) Token: 0x06005135 RID: 20789 RVA: 0x00120720 File Offset: 0x0011E920
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
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06005136 RID: 20790 RVA: 0x00120729 File Offset: 0x0011E929
		public static string XsdType
		{
			get
			{
				return "nonPositiveInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005137 RID: 20791 RVA: 0x00120730 File Offset: 0x0011E930
		public string GetXsdType()
		{
			return SoapNonPositiveInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger" /> object that is obtained from <paramref name="value" /></returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06005138 RID: 20792 RVA: 0x00120737 File Offset: 0x0011E937
		public static SoapNonPositiveInteger Parse(string value)
		{
			return new SoapNonPositiveInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonPositiveInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from Value.</returns>
		// Token: 0x06005139 RID: 20793 RVA: 0x00120744 File Offset: 0x0011E944
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04002AB0 RID: 10928
		private decimal _value;
	}
}
