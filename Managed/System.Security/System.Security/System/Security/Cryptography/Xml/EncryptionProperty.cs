using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;EncryptionProperty&gt; element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x0200005C RID: 92
	public sealed class EncryptionProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class. </summary>
		// Token: 0x06000246 RID: 582 RVA: 0x00002050 File Offset: 0x00000250
		public EncryptionProperty()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class using an <see cref="T:System.Xml.XmlElement" /> object. </summary>
		/// <param name="elementProperty">An <see cref="T:System.Xml.XmlElement" /> object to use for initialization.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elementProperty" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the <paramref name="elementProperty" /> parameter is not "EncryptionProperty". -or-The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the <paramref name="elementProperty" /> parameter is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		// Token: 0x06000247 RID: 583 RVA: 0x000091A8 File Offset: 0x000073A8
		public EncryptionProperty(XmlElement elementProperty)
		{
			if (elementProperty == null)
			{
				throw new ArgumentNullException("elementProperty");
			}
			if (elementProperty.LocalName != "EncryptionProperty" || elementProperty.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed encryption property element.");
			}
			this._elemProp = elementProperty;
			this._cachedXml = null;
		}

		/// <summary>Gets the ID of the current <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00009206 File Offset: 0x00007406
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		/// <summary>Gets the target of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>The target of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000920E File Offset: 0x0000740E
		public string Target
		{
			get
			{
				return this._target;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.XmlElement" /> object that represents an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property was set to null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the value set to the <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property is not "EncryptionProperty". -or-The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the value set to the <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00009216 File Offset: 0x00007416
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00009220 File Offset: 0x00007420
		public XmlElement PropertyElement
		{
			get
			{
				return this._elemProp;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.LocalName != "EncryptionProperty" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
				{
					throw new CryptographicException("Malformed encryption property element.");
				}
				this._elemProp = value;
				this._cachedXml = null;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00009278 File Offset: 0x00007478
		private bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600024D RID: 589 RVA: 0x00009284 File Offset: 0x00007484
		public XmlElement GetXml()
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

		// Token: 0x0600024E RID: 590 RVA: 0x000092B4 File Offset: 0x000074B4
		internal XmlElement GetXml(XmlDocument document)
		{
			return document.ImportNode(this._elemProp, true) as XmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to match.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object to parse.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the <paramref name="value" /> parameter is not "EncryptionProperty". -or-The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the <paramref name="value" /> parameter is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		// Token: 0x0600024F RID: 591 RVA: 0x000092C8 File Offset: 0x000074C8
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "EncryptionProperty" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed encryption property element.");
			}
			this._cachedXml = value;
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2001/04/xmlenc#");
			this._target = Utils.GetAttribute(value, "Target", "http://www.w3.org/2001/04/xmlenc#");
			this._elemProp = value;
		}

		// Token: 0x04000164 RID: 356
		private string _target;

		// Token: 0x04000165 RID: 357
		private string _id;

		// Token: 0x04000166 RID: 358
		private XmlElement _elemProp;

		// Token: 0x04000167 RID: 359
		private XmlElement _cachedXml;
	}
}
