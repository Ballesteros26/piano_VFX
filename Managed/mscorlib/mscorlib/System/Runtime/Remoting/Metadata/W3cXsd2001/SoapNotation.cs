using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML NOTATION attribute type.</summary>
	// Token: 0x020007E8 RID: 2024
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNotation : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> class.</summary>
		// Token: 0x06005142 RID: 20802 RVA: 0x00002111 File Offset: 0x00000311
		public SoapNotation()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> class with an XML NOTATION attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML NOTATION attribute. </param>
		// Token: 0x06005143 RID: 20803 RVA: 0x0012078C File Offset: 0x0011E98C
		public SoapNotation(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML NOTATION attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML NOTATION attribute.</returns>
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06005144 RID: 20804 RVA: 0x0012079B File Offset: 0x0011E99B
		// (set) Token: 0x06005145 RID: 20805 RVA: 0x001207A3 File Offset: 0x0011E9A3
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
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x001207AC File Offset: 0x0011E9AC
		public static string XsdType
		{
			get
			{
				return "NOTATION";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005147 RID: 20807 RVA: 0x001207B3 File Offset: 0x0011E9B3
		public string GetXsdType()
		{
			return SoapNotation.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06005148 RID: 20808 RVA: 0x001207BA File Offset: 0x0011E9BA
		public static SoapNotation Parse(string value)
		{
			return new SoapNotation(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation.Value" />.</returns>
		// Token: 0x06005149 RID: 20809 RVA: 0x0012079B File Offset: 0x0011E99B
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AB2 RID: 10930
		private string _value;
	}
}
