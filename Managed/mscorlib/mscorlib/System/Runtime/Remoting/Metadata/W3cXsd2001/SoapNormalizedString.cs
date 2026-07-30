using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML normalizedString type.</summary>
	// Token: 0x020007E7 RID: 2023
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNormalizedString : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> class.</summary>
		// Token: 0x0600513A RID: 20794 RVA: 0x00002111 File Offset: 0x00000311
		public SoapNormalizedString()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> class with a normalized string.</summary>
		/// <param name="value">A <see cref="T:System.String" /> object that contains a normalized string. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> contains invalid characters (0xD, 0xA, or 0x9). </exception>
		// Token: 0x0600513B RID: 20795 RVA: 0x00120751 File Offset: 0x0011E951
		public SoapNormalizedString(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets a normalized string.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains a normalized string.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> contains invalid characters (0xD, 0xA, or 0x9). </exception>
		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x0600513C RID: 20796 RVA: 0x00120765 File Offset: 0x0011E965
		// (set) Token: 0x0600513D RID: 20797 RVA: 0x0012076D File Offset: 0x0011E96D
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
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x0600513E RID: 20798 RVA: 0x00120776 File Offset: 0x0011E976
		public static string XsdType
		{
			get
			{
				return "normalizedString";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600513F RID: 20799 RVA: 0x0012077D File Offset: 0x0011E97D
		public string GetXsdType()
		{
			return SoapNormalizedString.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> object obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> contains invalid characters (0xD, 0xA, or 0x9). </exception>
		// Token: 0x06005140 RID: 20800 RVA: 0x00120784 File Offset: 0x0011E984
		public static SoapNormalizedString Parse(string value)
		{
			return new SoapNormalizedString(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> in the format "&lt;![CDATA[" + <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> + "]]&gt;".</returns>
		// Token: 0x06005141 RID: 20801 RVA: 0x00120765 File Offset: 0x0011E965
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002AB1 RID: 10929
		private string _value;
	}
}
