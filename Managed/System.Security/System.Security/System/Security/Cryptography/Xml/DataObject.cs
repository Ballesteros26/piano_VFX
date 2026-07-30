using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the object element of an XML signature that holds data to be signed.</summary>
	// Token: 0x02000053 RID: 83
	public class DataObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> class.</summary>
		// Token: 0x060001D5 RID: 469 RVA: 0x0000709C File Offset: 0x0000529C
		public DataObject()
		{
			this._cachedXml = null;
			this._elData = new CanonicalXmlNodeList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> class with the specified identification, MIME type, encoding, and data.</summary>
		/// <param name="id">The identification to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" /> with. </param>
		/// <param name="mimeType">The MIME type of the data used to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" />. </param>
		/// <param name="encoding">The encoding of the data used to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" />. </param>
		/// <param name="data">The data to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" /> with. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="data" /> parameter is null. </exception>
		// Token: 0x060001D6 RID: 470 RVA: 0x000070B8 File Offset: 0x000052B8
		public DataObject(string id, string mimeType, string encoding, XmlElement data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._id = id;
			this._mimeType = mimeType;
			this._encoding = encoding;
			this._elData = new CanonicalXmlNodeList();
			this._elData.Add(data);
			this._cachedXml = null;
		}

		/// <summary>Gets or sets the identification of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The name of the element that contains data to be used. </returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000710F File Offset: 0x0000530F
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00007117 File Offset: 0x00005317
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the MIME type of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object. </summary>
		/// <returns>The MIME type of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object. The default is null.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007127 File Offset: 0x00005327
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000712F File Offset: 0x0000532F
		public string MimeType
		{
			get
			{
				return this._mimeType;
			}
			set
			{
				this._mimeType = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the encoding of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The type of encoding of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000713F File Offset: 0x0000533F
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00007147 File Offset: 0x00005347
		public string Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the data value of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The data of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value used to set the property is null.</exception>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007157 File Offset: 0x00005357
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00007160 File Offset: 0x00005360
		public XmlNodeList Data
		{
			get
			{
				return this._elData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._elData = new CanonicalXmlNodeList();
				foreach (object obj in value)
				{
					XmlNode xmlNode = (XmlNode)obj;
					this._elData.Add(xmlNode);
				}
				this._cachedXml = null;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000071DC File Offset: 0x000053DC
		private bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001E0 RID: 480 RVA: 0x000071E8 File Offset: 0x000053E8
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

		// Token: 0x060001E1 RID: 481 RVA: 0x00007218 File Offset: 0x00005418
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Object", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._id))
			{
				xmlElement.SetAttribute("Id", this._id);
			}
			if (!string.IsNullOrEmpty(this._mimeType))
			{
				xmlElement.SetAttribute("MimeType", this._mimeType);
			}
			if (!string.IsNullOrEmpty(this._encoding))
			{
				xmlElement.SetAttribute("Encoding", this._encoding);
			}
			if (this._elData != null)
			{
				foreach (object obj in this._elData)
				{
					XmlNode xmlNode = (XmlNode)obj;
					xmlElement.AppendChild(document.ImportNode(xmlNode, true));
				}
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> state from. </param>
		/// <exception cref="T:System.ArgumentNullException">The value from the XML element is null.</exception>
		// Token: 0x060001E2 RID: 482 RVA: 0x000072F0 File Offset: 0x000054F0
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			this._mimeType = Utils.GetAttribute(value, "MimeType", "http://www.w3.org/2000/09/xmldsig#");
			this._encoding = Utils.GetAttribute(value, "Encoding", "http://www.w3.org/2000/09/xmldsig#");
			foreach (object obj in value.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				this._elData.Add(xmlNode);
			}
			this._cachedXml = value;
		}

		// Token: 0x0400012C RID: 300
		private string _id;

		// Token: 0x0400012D RID: 301
		private string _mimeType;

		// Token: 0x0400012E RID: 302
		private string _encoding;

		// Token: 0x0400012F RID: 303
		private CanonicalXmlNodeList _elData;

		// Token: 0x04000130 RID: 304
		private XmlElement _cachedXml;
	}
}
