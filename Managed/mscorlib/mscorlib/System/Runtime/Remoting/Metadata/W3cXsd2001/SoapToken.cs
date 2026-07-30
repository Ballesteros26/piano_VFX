using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML token type.</summary>
	// Token: 0x020007EC RID: 2028
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapToken : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> class.</summary>
		// Token: 0x06005169 RID: 20841 RVA: 0x00002111 File Offset: 0x00000311
		public SoapToken()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> class with an XML token.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML token. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following: <paramref name="value" /> contains invalid characters (0xD or 0x9).<paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />.Length - 1] contains white space.<paramref name="value" /> contains any spaces. </exception>
		// Token: 0x0600516A RID: 20842 RVA: 0x00120A78 File Offset: 0x0011EC78
		public SoapToken(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML token.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML token.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following: <paramref name="value" /> contains invalid characters (0xD or 0x9).<paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />.Length - 1] contains white space.<paramref name="value" /> contains any spaces. </exception>
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x0600516B RID: 20843 RVA: 0x00120A8C File Offset: 0x0011EC8C
		// (set) Token: 0x0600516C RID: 20844 RVA: 0x00120A94 File Offset: 0x0011EC94
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
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x0600516D RID: 20845 RVA: 0x00120A9D File Offset: 0x0011EC9D
		public static string XsdType
		{
			get
			{
				return "token";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600516E RID: 20846 RVA: 0x00120AA4 File Offset: 0x0011ECA4
		public string GetXsdType()
		{
			return SoapToken.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x0600516F RID: 20847 RVA: 0x00120AAB File Offset: 0x0011ECAB
		public static SoapToken Parse(string value)
		{
			return new SoapToken(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" />.</returns>
		// Token: 0x06005170 RID: 20848 RVA: 0x00120A8C File Offset: 0x0011EC8C
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AB9 RID: 10937
		private string _value;
	}
}
