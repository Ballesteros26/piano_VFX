using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML NMTOKEN attribute.</summary>
	// Token: 0x020007E3 RID: 2019
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtoken : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> class.</summary>
		// Token: 0x0600511A RID: 20762 RVA: 0x00002111 File Offset: 0x00000311
		public SoapNmtoken()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> class with an XML NMTOKEN attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> containing an XML NMTOKEN attribute. </param>
		// Token: 0x0600511B RID: 20763 RVA: 0x00120603 File Offset: 0x0011E803
		public SoapNmtoken(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML NMTOKEN attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML NMTOKEN attribute.</returns>
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600511C RID: 20764 RVA: 0x00120617 File Offset: 0x0011E817
		// (set) Token: 0x0600511D RID: 20765 RVA: 0x0012061F File Offset: 0x0011E81F
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
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600511E RID: 20766 RVA: 0x00120628 File Offset: 0x0011E828
		public static string XsdType
		{
			get
			{
				return "NMTOKEN";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600511F RID: 20767 RVA: 0x0012062F File Offset: 0x0011E82F
		public string GetXsdType()
		{
			return SoapNmtoken.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06005120 RID: 20768 RVA: 0x00120636 File Offset: 0x0011E836
		public static SoapNmtoken Parse(string value)
		{
			return new SoapNmtoken(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken.Value" />.</returns>
		// Token: 0x06005121 RID: 20769 RVA: 0x00120617 File Offset: 0x0011E817
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AAD RID: 10925
		private string _value;
	}
}
