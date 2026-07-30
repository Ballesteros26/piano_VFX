using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML NMTOKENS attribute.</summary>
	// Token: 0x020007E4 RID: 2020
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtokens : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class.</summary>
		// Token: 0x06005122 RID: 20770 RVA: 0x00002111 File Offset: 0x00000311
		public SoapNmtokens()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class with an XML NMTOKENS attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML NMTOKENS attribute. </param>
		// Token: 0x06005123 RID: 20771 RVA: 0x0012063E File Offset: 0x0011E83E
		public SoapNmtokens(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML NMTOKENS attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML NMTOKENS attribute.</returns>
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06005124 RID: 20772 RVA: 0x00120652 File Offset: 0x0011E852
		// (set) Token: 0x06005125 RID: 20773 RVA: 0x0012065A File Offset: 0x0011E85A
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
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x00120663 File Offset: 0x0011E863
		public static string XsdType
		{
			get
			{
				return "NMTOKENS";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005127 RID: 20775 RVA: 0x0012066A File Offset: 0x0011E86A
		public string GetXsdType()
		{
			return SoapNmtokens.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06005128 RID: 20776 RVA: 0x00120671 File Offset: 0x0011E871
		public static SoapNmtokens Parse(string value)
		{
			return new SoapNmtokens(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" />.</returns>
		// Token: 0x06005129 RID: 20777 RVA: 0x00120652 File Offset: 0x0011E852
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AAE RID: 10926
		private string _value;
	}
}
