using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents an XML digital signature or XML encryption &lt;KeyInfo&gt; element.</summary>
	// Token: 0x02000062 RID: 98
	public class KeyInfo : IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> class.</summary>
		// Token: 0x0600027C RID: 636 RVA: 0x0000999D File Offset: 0x00007B9D
		public KeyInfo()
		{
			this._keyInfoClauses = new ArrayList();
		}

		/// <summary>Gets or sets the key information identity.</summary>
		/// <returns>The key information identity.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000099B0 File Offset: 0x00007BB0
		// (set) Token: 0x0600027E RID: 638 RVA: 0x000099B8 File Offset: 0x00007BB8
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600027F RID: 639 RVA: 0x000099C4 File Offset: 0x00007BC4
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000099E8 File Offset: 0x00007BE8
		internal XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._id))
			{
				xmlElement.SetAttribute("Id", this._id);
			}
			for (int i = 0; i < this._keyInfoClauses.Count; i++)
			{
				XmlElement xml = ((KeyInfoClause)this._keyInfoClauses[i]).GetXml(xmlDocument);
				if (xml != null)
				{
					xmlElement.AppendChild(xml);
				}
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> state. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x06000281 RID: 641 RVA: 0x00009A60 File Offset: 0x00007C60
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			for (XmlNode xmlNode = value.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					string text = xmlElement.NamespaceURI + " " + xmlElement.LocalName;
					if (text == "http://www.w3.org/2000/09/xmldsig# KeyValue")
					{
						foreach (object obj in xmlElement.ChildNodes)
						{
							XmlElement xmlElement2 = ((XmlNode)obj) as XmlElement;
							if (xmlElement2 != null)
							{
								text = text + "/" + xmlElement2.LocalName;
								break;
							}
						}
					}
					KeyInfoClause keyInfoClause = (KeyInfoClause)CryptoHelpers.CreateFromName(text);
					if (keyInfoClause == null)
					{
						keyInfoClause = new KeyInfoNode();
					}
					keyInfoClause.LoadXml(xmlElement);
					this.AddClause(keyInfoClause);
				}
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00009B70 File Offset: 0x00007D70
		public int Count
		{
			get
			{
				return this._keyInfoClauses.Count;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> that represents a particular type of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> information to the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="clause">The <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object. </param>
		// Token: 0x06000283 RID: 643 RVA: 0x00009B7D File Offset: 0x00007D7D
		public void AddClause(KeyInfoClause clause)
		{
			this._keyInfoClauses.Add(clause);
		}

		/// <summary>Returns an enumerator of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000284 RID: 644 RVA: 0x00009B8C File Offset: 0x00007D8C
		public IEnumerator GetEnumerator()
		{
			return this._keyInfoClauses.GetEnumerator();
		}

		/// <summary>Returns an enumerator of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects of the specified type in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> that can be used to iterate through the collection.</returns>
		/// <param name="requestedObjectType">The type of object to enumerate. </param>
		// Token: 0x06000285 RID: 645 RVA: 0x00009B9C File Offset: 0x00007D9C
		public IEnumerator GetEnumerator(Type requestedObjectType)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._keyInfoClauses)
			{
				if (requestedObjectType.Equals(obj.GetType()))
				{
					arrayList.Add(obj);
				}
			}
			return arrayList.GetEnumerator();
		}

		// Token: 0x0400016C RID: 364
		private string _id;

		// Token: 0x0400016D RID: 365
		private ArrayList _keyInfoClauses;
	}
}
