using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML language type.</summary>
	// Token: 0x020007DD RID: 2013
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapLanguage : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> class.</summary>
		// Token: 0x060050E8 RID: 20712 RVA: 0x00002111 File Offset: 0x00000311
		public SoapLanguage()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> class with the language identifier value of language attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains the language identifier value of a language attribute. </param>
		// Token: 0x060050E9 RID: 20713 RVA: 0x001203FA File Offset: 0x0011E5FA
		public SoapLanguage(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets the language identifier of a language attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the language identifier of a language attribute.</returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060050EA RID: 20714 RVA: 0x0012040E File Offset: 0x0011E60E
		// (set) Token: 0x060050EB RID: 20715 RVA: 0x00120416 File Offset: 0x0011E616
		public string Value
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
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060050EC RID: 20716 RVA: 0x0012041F File Offset: 0x0011E61F
		public static string XsdType
		{
			get
			{
				return "language";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050ED RID: 20717 RVA: 0x00120426 File Offset: 0x0011E626
		public string GetXsdType()
		{
			return SoapLanguage.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060050EE RID: 20718 RVA: 0x0012042D File Offset: 0x0011E62D
		public static SoapLanguage Parse(string value)
		{
			return new SoapLanguage(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" />.</returns>
		// Token: 0x060050EF RID: 20719 RVA: 0x0012040E File Offset: 0x0011E60E
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AA5 RID: 10917
		private string _value;
	}
}
