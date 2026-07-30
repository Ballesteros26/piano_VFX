using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML NcName type.</summary>
	// Token: 0x020007E1 RID: 2017
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNcName : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class.</summary>
		// Token: 0x0600510A RID: 20746 RVA: 0x00002111 File Offset: 0x00000311
		public SoapNcName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class with an XML NcName type.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML NcName type. </param>
		// Token: 0x0600510B RID: 20747 RVA: 0x0012055C File Offset: 0x0011E75C
		public SoapNcName(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML NcName type.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML NcName type.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x0600510C RID: 20748 RVA: 0x00120570 File Offset: 0x0011E770
		// (set) Token: 0x0600510D RID: 20749 RVA: 0x00120578 File Offset: 0x0011E778
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
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x0600510E RID: 20750 RVA: 0x00120581 File Offset: 0x0011E781
		public static string XsdType
		{
			get
			{
				return "NCName";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600510F RID: 20751 RVA: 0x00120588 File Offset: 0x0011E788
		public string GetXsdType()
		{
			return SoapNcName.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06005110 RID: 20752 RVA: 0x0012058F File Offset: 0x0011E78F
		public static SoapNcName Parse(string value)
		{
			return new SoapNcName(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" />.</returns>
		// Token: 0x06005111 RID: 20753 RVA: 0x00120570 File Offset: 0x0011E770
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AAB RID: 10923
		private string _value;
	}
}
