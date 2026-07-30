using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD negativeInteger type.</summary>
	// Token: 0x020007E2 RID: 2018
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNegativeInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> class.</summary>
		// Token: 0x06005112 RID: 20754 RVA: 0x00002111 File Offset: 0x00000311
		public SoapNegativeInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than -1. </exception>
		// Token: 0x06005113 RID: 20755 RVA: 0x00120597 File Offset: 0x0011E797
		public SoapNegativeInteger(decimal value)
		{
			if (value >= 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value);
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than -1. </exception>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06005114 RID: 20756 RVA: 0x001205CA File Offset: 0x0011E7CA
		// (set) Token: 0x06005115 RID: 20757 RVA: 0x001205D2 File Offset: 0x0011E7D2
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
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06005116 RID: 20758 RVA: 0x001205DB File Offset: 0x0011E7DB
		public static string XsdType
		{
			get
			{
				return "negativeInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005117 RID: 20759 RVA: 0x001205E2 File Offset: 0x0011E7E2
		public string GetXsdType()
		{
			return SoapNegativeInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> object that is obtained from <paramref name="value" />. </returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x06005118 RID: 20760 RVA: 0x001205E9 File Offset: 0x0011E7E9
		public static SoapNegativeInteger Parse(string value)
		{
			return new SoapNegativeInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from Value.</returns>
		// Token: 0x06005119 RID: 20761 RVA: 0x001205F6 File Offset: 0x0011E7F6
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04002AAC RID: 10924
		private decimal _value;
	}
}
