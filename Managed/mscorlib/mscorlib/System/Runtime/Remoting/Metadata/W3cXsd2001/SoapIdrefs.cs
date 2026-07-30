using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML IDREFS attribute.</summary>
	// Token: 0x020007DB RID: 2011
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdrefs : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> class.</summary>
		// Token: 0x060050D8 RID: 20696 RVA: 0x00002111 File Offset: 0x00000311
		public SoapIdrefs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> class with an XML IDREFS attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML IDREFS attribute. </param>
		// Token: 0x060050D9 RID: 20697 RVA: 0x00120377 File Offset: 0x0011E577
		public SoapIdrefs(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML IDREFS attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML IDREFS attribute.</returns>
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x060050DA RID: 20698 RVA: 0x0012038B File Offset: 0x0011E58B
		// (set) Token: 0x060050DB RID: 20699 RVA: 0x00120393 File Offset: 0x0011E593
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
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x060050DC RID: 20700 RVA: 0x0012039C File Offset: 0x0011E59C
		public static string XsdType
		{
			get
			{
				return "IDREFS";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050DD RID: 20701 RVA: 0x001203A3 File Offset: 0x0011E5A3
		public string GetXsdType()
		{
			return SoapIdrefs.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060050DE RID: 20702 RVA: 0x001203AA File Offset: 0x0011E5AA
		public static SoapIdrefs Parse(string value)
		{
			return new SoapIdrefs(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs.Value" />.</returns>
		// Token: 0x060050DF RID: 20703 RVA: 0x0012038B File Offset: 0x0011E58B
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AA3 RID: 10915
		private string _value;
	}
}
