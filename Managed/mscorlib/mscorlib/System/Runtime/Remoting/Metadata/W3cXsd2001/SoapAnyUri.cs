using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD anyURI type.</summary>
	// Token: 0x020007CF RID: 1999
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapAnyUri : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> class.</summary>
		// Token: 0x0600507C RID: 20604 RVA: 0x00002111 File Offset: 0x00000311
		public SoapAnyUri()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> class with the specified URI.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains a URI. </param>
		// Token: 0x0600507D RID: 20605 RVA: 0x0011FA3F File Offset: 0x0011DC3F
		public SoapAnyUri(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets a URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains a URI.</returns>
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x0600507E RID: 20606 RVA: 0x0011FA4E File Offset: 0x0011DC4E
		// (set) Token: 0x0600507F RID: 20607 RVA: 0x0011FA56 File Offset: 0x0011DC56
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
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06005080 RID: 20608 RVA: 0x0011FA5F File Offset: 0x0011DC5F
		public static string XsdType
		{
			get
			{
				return "anyUri";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005081 RID: 20609 RVA: 0x0011FA66 File Offset: 0x0011DC66
		public string GetXsdType()
		{
			return SoapAnyUri.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x06005082 RID: 20610 RVA: 0x0011FA6D File Offset: 0x0011DC6D
		public static SoapAnyUri Parse(string value)
		{
			return new SoapAnyUri(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" />.</returns>
		// Token: 0x06005083 RID: 20611 RVA: 0x0011FA4E File Offset: 0x0011DC4E
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002A95 RID: 10901
		private string _value;
	}
}
