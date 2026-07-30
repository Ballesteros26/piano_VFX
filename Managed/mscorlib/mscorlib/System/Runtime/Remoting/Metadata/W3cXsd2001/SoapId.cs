using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML ID attribute.</summary>
	// Token: 0x020007D9 RID: 2009
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapId : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> class.</summary>
		// Token: 0x060050C8 RID: 20680 RVA: 0x00002111 File Offset: 0x00000311
		public SoapId()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> class with an XML ID attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML ID attribute. </param>
		// Token: 0x060050C9 RID: 20681 RVA: 0x00120301 File Offset: 0x0011E501
		public SoapId(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML ID attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML ID attribute.</returns>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x060050CA RID: 20682 RVA: 0x00120315 File Offset: 0x0011E515
		// (set) Token: 0x060050CB RID: 20683 RVA: 0x0012031D File Offset: 0x0011E51D
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
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x060050CC RID: 20684 RVA: 0x00120326 File Offset: 0x0011E526
		public static string XsdType
		{
			get
			{
				return "ID";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050CD RID: 20685 RVA: 0x0012032D File Offset: 0x0011E52D
		public string GetXsdType()
		{
			return SoapId.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060050CE RID: 20686 RVA: 0x00120334 File Offset: 0x0011E534
		public static SoapId Parse(string value)
		{
			return new SoapId(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId.Value" />.</returns>
		// Token: 0x060050CF RID: 20687 RVA: 0x00120315 File Offset: 0x0011E515
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AA1 RID: 10913
		private string _value;
	}
}
