using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML IDREFS attribute.</summary>
	// Token: 0x020007DA RID: 2010
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdref : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class.</summary>
		// Token: 0x060050D0 RID: 20688 RVA: 0x00002111 File Offset: 0x00000311
		public SoapIdref()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class with an XML IDREF attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML IDREF attribute. </param>
		// Token: 0x060050D1 RID: 20689 RVA: 0x0012033C File Offset: 0x0011E53C
		public SoapIdref(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML IDREF attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML IDREF attribute.</returns>
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x060050D2 RID: 20690 RVA: 0x00120350 File Offset: 0x0011E550
		// (set) Token: 0x060050D3 RID: 20691 RVA: 0x00120358 File Offset: 0x0011E558
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
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x060050D4 RID: 20692 RVA: 0x00120361 File Offset: 0x0011E561
		public static string XsdType
		{
			get
			{
				return "IDREF";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050D5 RID: 20693 RVA: 0x00120368 File Offset: 0x0011E568
		public string GetXsdType()
		{
			return SoapIdref.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060050D6 RID: 20694 RVA: 0x0012036F File Offset: 0x0011E56F
		public static SoapIdref Parse(string value)
		{
			return new SoapIdref(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" />.</returns>
		// Token: 0x060050D7 RID: 20695 RVA: 0x00120350 File Offset: 0x0011E550
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AA2 RID: 10914
		private string _value;
	}
}
