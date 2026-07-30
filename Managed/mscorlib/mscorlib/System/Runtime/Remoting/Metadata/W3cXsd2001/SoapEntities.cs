using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML ENTITIES attribute.</summary>
	// Token: 0x020007D5 RID: 2005
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntities : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> class.</summary>
		// Token: 0x060050AA RID: 20650 RVA: 0x00002111 File Offset: 0x00000311
		public SoapEntities()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> class with an XML ENTITIES attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML ENTITIES attribute. </param>
		// Token: 0x060050AB RID: 20651 RVA: 0x001200E1 File Offset: 0x0011E2E1
		public SoapEntities(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML ENTITIES attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML ENTITIES attribute.</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x001200F5 File Offset: 0x0011E2F5
		// (set) Token: 0x060050AD RID: 20653 RVA: 0x001200FD File Offset: 0x0011E2FD
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
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x00120106 File Offset: 0x0011E306
		public static string XsdType
		{
			get
			{
				return "ENTITIES";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050AF RID: 20655 RVA: 0x0012010D File Offset: 0x0011E30D
		public string GetXsdType()
		{
			return SoapEntities.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060050B0 RID: 20656 RVA: 0x00120114 File Offset: 0x0011E314
		public static SoapEntities Parse(string value)
		{
			return new SoapEntities(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities.Value" />.</returns>
		// Token: 0x060050B1 RID: 20657 RVA: 0x001200F5 File Offset: 0x0011E2F5
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002A9D RID: 10909
		private string _value;
	}
}
