using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD base64Binary type. </summary>
	// Token: 0x020007D0 RID: 2000
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapBase64Binary : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> class.</summary>
		// Token: 0x06005084 RID: 20612 RVA: 0x00002111 File Offset: 0x00000311
		public SoapBase64Binary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> class with the binary representation of a 64-bit number.</summary>
		/// <param name="value">A <see cref="T:System.Byte" /> array that contains a 64-bit number. </param>
		// Token: 0x06005085 RID: 20613 RVA: 0x0011FA75 File Offset: 0x0011DC75
		public SoapBase64Binary(byte[] value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the binary representation of a 64-bit number.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the binary representation of a 64-bit number.</returns>
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06005086 RID: 20614 RVA: 0x0011FA84 File Offset: 0x0011DC84
		// (set) Token: 0x06005087 RID: 20615 RVA: 0x0011FA8C File Offset: 0x0011DC8C
		public byte[] Value
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
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06005088 RID: 20616 RVA: 0x0011FA95 File Offset: 0x0011DC95
		public static string XsdType
		{
			get
			{
				return "base64Binary";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005089 RID: 20617 RVA: 0x0011FA9C File Offset: 0x0011DC9C
		public string GetXsdType()
		{
			return SoapBase64Binary.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following:<paramref name="value" /> is null. The length of <paramref name="value" /> is less than 4.The length of <paramref name="value" /> is not a multiple of 4. </exception>
		// Token: 0x0600508A RID: 20618 RVA: 0x0011FAA3 File Offset: 0x0011DCA3
		public static SoapBase64Binary Parse(string value)
		{
			return new SoapBase64Binary(Convert.FromBase64String(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" />.</returns>
		// Token: 0x0600508B RID: 20619 RVA: 0x0011FAB0 File Offset: 0x0011DCB0
		public override string ToString()
		{
			return Convert.ToBase64String(this._value);
		}

		// Token: 0x04002A96 RID: 10902
		private byte[] _value;
	}
}
