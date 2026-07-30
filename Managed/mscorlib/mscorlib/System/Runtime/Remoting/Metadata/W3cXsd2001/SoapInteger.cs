using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD integer type.</summary>
	// Token: 0x020007DC RID: 2012
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class.</summary>
		// Token: 0x060050E0 RID: 20704 RVA: 0x00002111 File Offset: 0x00000311
		public SoapInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		// Token: 0x060050E1 RID: 20705 RVA: 0x001203B2 File Offset: 0x0011E5B2
		public SoapInteger(decimal value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060050E2 RID: 20706 RVA: 0x001203C1 File Offset: 0x0011E5C1
		// (set) Token: 0x060050E3 RID: 20707 RVA: 0x001203C9 File Offset: 0x0011E5C9
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
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060050E4 RID: 20708 RVA: 0x001203D2 File Offset: 0x0011E5D2
		public static string XsdType
		{
			get
			{
				return "integer";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050E5 RID: 20709 RVA: 0x001203D9 File Offset: 0x0011E5D9
		public string GetXsdType()
		{
			return SoapInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x060050E6 RID: 20710 RVA: 0x001203E0 File Offset: 0x0011E5E0
		public static SoapInteger Parse(string value)
		{
			return new SoapInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger.Value" />.</returns>
		// Token: 0x060050E7 RID: 20711 RVA: 0x001203ED File Offset: 0x0011E5ED
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04002AA4 RID: 10916
		private decimal _value;
	}
}
