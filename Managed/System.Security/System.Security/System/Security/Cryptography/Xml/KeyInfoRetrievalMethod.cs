using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>References <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> objects stored at a different location when using XMLDSIG or XML encryption.</summary>
	// Token: 0x02000067 RID: 103
	public class KeyInfoRetrievalMethod : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class.</summary>
		// Token: 0x0600029F RID: 671 RVA: 0x00009C09 File Offset: 0x00007E09
		public KeyInfoRetrievalMethod()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class with the specified Uniform Resource Identifier (URI) pointing to the referenced <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="strUri">The Uniform Resource Identifier (URI) of the information to be referenced by the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" />. </param>
		// Token: 0x060002A0 RID: 672 RVA: 0x00009D8D File Offset: 0x00007F8D
		public KeyInfoRetrievalMethod(string strUri)
		{
			this._uri = strUri;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class with the specified Uniform Resource Identifier (URI) pointing to the referenced <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object and the URI that describes the type of data to retrieve.  </summary>
		/// <param name="strUri">The Uniform Resource Identifier (URI) of the information to be referenced by the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" />.</param>
		/// <param name="typeName">The URI that describes the type of data to retrieve.</param>
		// Token: 0x060002A1 RID: 673 RVA: 0x00009D9C File Offset: 0x00007F9C
		public KeyInfoRetrievalMethod(string strUri, string typeName)
		{
			this._uri = strUri;
			this._type = typeName;
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</summary>
		/// <returns>The Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00009DB2 File Offset: 0x00007FB2
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x00009DBA File Offset: 0x00007FBA
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		/// <summary>Gets or sets a Uniform Resource Identifier (URI) that describes the type of data to be retrieved.</summary>
		/// <returns>A Uniform Resource Identifier (URI) that describes the type of data to be retrieved.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00009DC3 File Offset: 0x00007FC3
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x00009DCB File Offset: 0x00007FCB
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002A6 RID: 678 RVA: 0x00009DD4 File Offset: 0x00007FD4
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009DF8 File Offset: 0x00007FF8
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("RetrievalMethod", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._uri))
			{
				xmlElement.SetAttribute("URI", this._uri);
			}
			if (!string.IsNullOrEmpty(this._type))
			{
				xmlElement.SetAttribute("Type", this._type);
			}
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object to match.</summary>
		/// <param name="value">The XML element that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060002A8 RID: 680 RVA: 0x00009E53 File Offset: 0x00008053
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2000/09/xmldsig#");
			this._type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x04000171 RID: 369
		private string _uri;

		// Token: 0x04000172 RID: 370
		private string _type;
	}
}
