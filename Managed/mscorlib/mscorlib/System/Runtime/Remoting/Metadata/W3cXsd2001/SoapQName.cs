using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD QName type.</summary>
	// Token: 0x020007EA RID: 2026
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapQName : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class.</summary>
		// Token: 0x06005152 RID: 20818 RVA: 0x00002111 File Offset: 0x00000311
		public SoapQName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the local part of a qualified name.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains the local part of a qualified name. </param>
		// Token: 0x06005153 RID: 20819 RVA: 0x0012082E File Offset: 0x0011EA2E
		public SoapQName(string value)
		{
			this._name = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the namespace alias and the local part of a qualified name.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the namespace alias of a qualified name. </param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the local part of a qualified name. </param>
		// Token: 0x06005154 RID: 20820 RVA: 0x0012083D File Offset: 0x0011EA3D
		public SoapQName(string key, string name)
		{
			this._key = key;
			this._name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the namespace alias, the local part of a qualified name, and the namespace that is referenced by the alias.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the namespace alias of a qualified name. </param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the local part of a qualified name. </param>
		/// <param name="namespaceValue">A <see cref="T:System.String" /> that contains the namespace that is referenced by <paramref name="key" />. </param>
		// Token: 0x06005155 RID: 20821 RVA: 0x00120853 File Offset: 0x0011EA53
		public SoapQName(string key, string name, string namespaceValue)
		{
			this._key = key;
			this._name = name;
			this._namespace = namespaceValue;
		}

		/// <summary>Gets or sets the namespace alias of a qualified name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace alias of a qualified name.</returns>
		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06005156 RID: 20822 RVA: 0x00120870 File Offset: 0x0011EA70
		// (set) Token: 0x06005157 RID: 20823 RVA: 0x00120878 File Offset: 0x0011EA78
		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Gets or sets the name portion of a qualified name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name portion of a qualified name.</returns>
		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06005158 RID: 20824 RVA: 0x00120881 File Offset: 0x0011EA81
		// (set) Token: 0x06005159 RID: 20825 RVA: 0x00120889 File Offset: 0x0011EA89
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets or sets the namespace that is referenced by <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace that is referenced by <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.</returns>
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x0600515A RID: 20826 RVA: 0x00120892 File Offset: 0x0011EA92
		// (set) Token: 0x0600515B RID: 20827 RVA: 0x0012089A File Offset: 0x0011EA9A
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x0600515C RID: 20828 RVA: 0x001208A3 File Offset: 0x0011EAA3
		public static string XsdType
		{
			get
			{
				return "QName";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the XSD of the current SOAP type.</returns>
		// Token: 0x0600515D RID: 20829 RVA: 0x001208AA File Offset: 0x0011EAAA
		public string GetXsdType()
		{
			return SoapQName.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x0600515E RID: 20830 RVA: 0x001208B4 File Offset: 0x0011EAB4
		public static SoapQName Parse(string value)
		{
			SoapQName soapQName = new SoapQName();
			int num = value.IndexOf(':');
			if (num != -1)
			{
				soapQName.Key = value.Substring(0, num);
				soapQName.Name = value.Substring(num + 1);
			}
			else
			{
				soapQName.Name = value;
			}
			return soapQName;
		}

		/// <summary>Returns the qualified name as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> in the format " <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> : <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" /> ". If <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> is not specified, this method returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" />.</returns>
		// Token: 0x0600515F RID: 20831 RVA: 0x001208FB File Offset: 0x0011EAFB
		public override string ToString()
		{
			if (this._key == null || this._key == "")
			{
				return this._name;
			}
			return this._key + ":" + this._name;
		}

		// Token: 0x04002AB4 RID: 10932
		private string _name;

		// Token: 0x04002AB5 RID: 10933
		private string _key;

		// Token: 0x04002AB6 RID: 10934
		private string _namespace;
	}
}
