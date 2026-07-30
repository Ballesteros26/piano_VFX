using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML Name type.</summary>
	// Token: 0x020007E0 RID: 2016
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapName : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> class.</summary>
		// Token: 0x06005102 RID: 20738 RVA: 0x00002111 File Offset: 0x00000311
		public SoapName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> class with an XML Name type.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML Name type. </param>
		// Token: 0x06005103 RID: 20739 RVA: 0x00120521 File Offset: 0x0011E721
		public SoapName(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML Name type.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML Name type.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06005104 RID: 20740 RVA: 0x00120535 File Offset: 0x0011E735
		// (set) Token: 0x06005105 RID: 20741 RVA: 0x0012053D File Offset: 0x0011E73D
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
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06005106 RID: 20742 RVA: 0x00120546 File Offset: 0x0011E746
		public static string XsdType
		{
			get
			{
				return "Name";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005107 RID: 20743 RVA: 0x0012054D File Offset: 0x0011E74D
		public string GetXsdType()
		{
			return SoapName.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06005108 RID: 20744 RVA: 0x00120554 File Offset: 0x0011E754
		public static SoapName Parse(string value)
		{
			return new SoapName(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName.Value" />.</returns>
		// Token: 0x06005109 RID: 20745 RVA: 0x00120535 File Offset: 0x0011E735
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AAA RID: 10922
		private string _value;
	}
}
