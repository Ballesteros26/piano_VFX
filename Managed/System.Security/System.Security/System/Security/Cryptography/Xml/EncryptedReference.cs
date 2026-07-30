using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class used in XML encryption from which the <see cref="T:System.Security.Cryptography.Xml.CipherReference" />, <see cref="T:System.Security.Cryptography.Xml.KeyReference" />, and <see cref="T:System.Security.Cryptography.Xml.DataReference" /> classes derive.</summary>
	// Token: 0x02000058 RID: 88
	public abstract class EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class.</summary>
		// Token: 0x060001F5 RID: 501 RVA: 0x00007CEC File Offset: 0x00005EEC
		protected EncryptedReference()
			: this(string.Empty, new TransformChain())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">The Uniform Resource Identifier (URI) that points to the data to encrypt.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is null.</exception>
		// Token: 0x060001F6 RID: 502 RVA: 0x00007CFE File Offset: 0x00005EFE
		protected EncryptedReference(string uri)
			: this(uri, new TransformChain())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class using the specified Uniform Resource Identifier (URI) and transform chain.</summary>
		/// <param name="uri">The Uniform Resource Identifier (URI) that points to the data to encrypt.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to be done on the data to encrypt.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is null.</exception>
		// Token: 0x060001F7 RID: 503 RVA: 0x00007D0C File Offset: 0x00005F0C
		protected EncryptedReference(string uri, TransformChain transformChain)
		{
			this.TransformChain = transformChain;
			this.Uri = uri;
			this._cachedXml = null;
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>The Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedReference.Uri" /> property was set to null.</exception>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00007D29 File Offset: 0x00005F29
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00007D31 File Offset: 0x00005F31
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("A Uri attribute is required for a CipherReference element.");
				}
				this._uri = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the transform chain of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms used on the encrypted data.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007D4F File Offset: 0x00005F4F
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00007D6A File Offset: 0x00005F6A
		public TransformChain TransformChain
		{
			get
			{
				if (this._transformChain == null)
				{
					this._transformChain = new TransformChain();
				}
				return this._transformChain;
			}
			set
			{
				this._transformChain = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to the current transform chain of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <param name="transform">A <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to add to the transform chain.</param>
		// Token: 0x060001FC RID: 508 RVA: 0x00007D7A File Offset: 0x00005F7A
		public void AddTransform(Transform transform)
		{
			this.TransformChain.Add(transform);
		}

		/// <summary>Gets or sets a reference type.</summary>
		/// <returns>The reference type of the encrypted data.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00007D88 File Offset: 0x00005F88
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00007D90 File Offset: 0x00005F90
		protected string ReferenceType
		{
			get
			{
				return this._referenceType;
			}
			set
			{
				this._referenceType = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets a value that indicates whether the cache is valid.</summary>
		/// <returns>true if the cache is valid; otherwise, false.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007DA0 File Offset: 0x00005FA0
		protected internal bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Returns the XML representation of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the values of the &lt;EncryptedReference&gt; element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedReference.ReferenceType" /> property is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000200 RID: 512 RVA: 0x00007DAC File Offset: 0x00005FAC
		public virtual XmlElement GetXml()
		{
			if (this.CacheValid)
			{
				return this._cachedXml;
			}
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007DDC File Offset: 0x00005FDC
		internal XmlElement GetXml(XmlDocument document)
		{
			if (this.ReferenceType == null)
			{
				throw new CryptographicException("The Reference type must be set in an EncryptedReference object.");
			}
			XmlElement xmlElement = document.CreateElement(this.ReferenceType, "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(this._uri))
			{
				xmlElement.SetAttribute("URI", this._uri);
			}
			if (this.TransformChain.Count > 0)
			{
				xmlElement.AppendChild(this.TransformChain.GetXml(document, "http://www.w3.org/2000/09/xmldsig#"));
			}
			return xmlElement;
		}

		/// <summary>Loads an XML element into an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object that represents an XML element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x06000202 RID: 514 RVA: 0x00007E54 File Offset: 0x00006054
		public virtual void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.ReferenceType = value.LocalName;
			this.Uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2001/04/xmlenc#");
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = value.SelectSingleNode("ds:Transforms", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.TransformChain.LoadXml(xmlNode as XmlElement);
			}
			this._cachedXml = value;
		}

		// Token: 0x04000138 RID: 312
		private string _uri;

		// Token: 0x04000139 RID: 313
		private string _referenceType;

		// Token: 0x0400013A RID: 314
		private TransformChain _transformChain;

		// Token: 0x0400013B RID: 315
		internal XmlElement _cachedXml;
	}
}
