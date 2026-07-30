using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Contains information about the canonicalization algorithm and signature algorithm used for the XML signature.</summary>
	// Token: 0x02000088 RID: 136
	public class SignedInfo : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> class.</summary>
		// Token: 0x06000400 RID: 1024 RVA: 0x00010A31 File Offset: 0x0000EC31
		public SignedInfo()
		{
			this.references = new ArrayList();
			this.c14nMethod = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
		}

		/// <summary>Gets or sets the canonicalization algorithm that is used before signing for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The canonicalization algorithm used before signing for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00010A4F File Offset: 0x0000EC4F
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x00010A57 File Offset: 0x0000EC57
		public string CanonicalizationMethod
		{
			get
			{
				return this.c14nMethod;
			}
			set
			{
				this.c14nMethod = value;
				this.element = null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object used for canonicalization.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.Transform" /> object used for canonicalization.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <see cref="T:System.Security.Cryptography.Xml.Transform" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00010A67 File Offset: 0x0000EC67
		[MonoTODO]
		[ComVisible(false)]
		public Transform CanonicalizationMethodObject
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of references in the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The number of references in the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00010A6E File Offset: 0x0000EC6E
		public int Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00010A75 File Offset: 0x0000EC75
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00010A7D File Offset: 0x0000EC7D
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.element = null;
				this.id = value;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00010A6E File Offset: 0x0000EC6E
		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the collection is synchronized.</summary>
		/// <returns>true if the collection is synchronized; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00010A6E File Offset: 0x0000EC6E
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a list of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> objects of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>A list of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> elements of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00010A8D File Offset: 0x0000EC8D
		public ArrayList References
		{
			get
			{
				return this.references;
			}
		}

		/// <summary>Gets or sets the length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00010A95 File Offset: 0x0000EC95
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00010A9D File Offset: 0x0000EC9D
		public string SignatureLength
		{
			get
			{
				return this.signatureLength;
			}
			set
			{
				this.element = null;
				this.signatureLength = value;
			}
		}

		/// <summary>Gets or sets the name of the algorithm used for signature generation and validation for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The name of the algorithm used for signature generation and validation for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00010AAD File Offset: 0x0000ECAD
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00010AB5 File Offset: 0x0000ECB5
		public string SignatureMethod
		{
			get
			{
				return this.signatureMethod;
			}
			set
			{
				this.element = null;
				this.signatureMethod = value;
			}
		}

		/// <summary>Gets an object to use for synchronization.</summary>
		/// <returns>An object to use for synchronization.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00010A6E File Offset: 0x0000EC6E
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Reference" /> object to the list of references to digest and sign.</summary>
		/// <param name="reference">The reference to add to the list of references. </param>
		/// <exception cref="T:System.ArgumentNullException">The reference parameter is null.</exception>
		// Token: 0x0600040F RID: 1039 RVA: 0x00010AC5 File Offset: 0x0000ECC5
		public void AddReference(Reference reference)
		{
			this.references.Add(reference);
		}

		/// <summary>Copies the elements of this instance into an <see cref="T:System.Array" /> object, starting at a specified index in the array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object that holds the collection's elements. </param>
		/// <param name="index">The beginning index in the array where the elements are copied. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06000410 RID: 1040 RVA: 0x00010A6E File Offset: 0x0000EC6E
		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an enumerator that iterates through the collection of references.</summary>
		/// <returns>An enumerator that iterates through the collection of references.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06000411 RID: 1041 RVA: 0x00010AD4 File Offset: 0x0000ECD4
		public IEnumerator GetEnumerator()
		{
			return this.references.GetEnumerator();
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> instance.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedInfo.SignatureMethod" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.SignedInfo.References" /> property is empty. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000412 RID: 1042 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		public XmlElement GetXml()
		{
			if (this.element != null)
			{
				return this.element;
			}
			if (this.signatureMethod == null)
			{
				throw new CryptographicException("SignatureMethod");
			}
			if (this.references.Count == 0)
			{
				throw new CryptographicException("References empty");
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (this.id != null)
			{
				xmlElement.SetAttribute("Id", this.id);
			}
			if (this.c14nMethod != null)
			{
				XmlElement xmlElement2 = xmlDocument.CreateElement("CanonicalizationMethod", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement2.SetAttribute("Algorithm", this.c14nMethod);
				xmlElement.AppendChild(xmlElement2);
			}
			if (this.signatureMethod != null)
			{
				XmlElement xmlElement3 = xmlDocument.CreateElement("SignatureMethod", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement3.SetAttribute("Algorithm", this.signatureMethod);
				if (this.signatureLength != null)
				{
					XmlElement xmlElement4 = xmlDocument.CreateElement("HMACOutputLength", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement4.InnerText = this.signatureLength;
					xmlElement3.AppendChild(xmlElement4);
				}
				xmlElement.AppendChild(xmlElement3);
			}
			if (this.references.Count == 0)
			{
				throw new CryptographicException("At least one Reference element is required in SignedInfo.");
			}
			foreach (object obj in this.references)
			{
				XmlNode xml = ((Reference)obj).GetXml();
				XmlNode xmlNode = xmlDocument.ImportNode(xml, true);
				xmlElement.AppendChild(xmlNode);
			}
			return xmlElement;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00010C70 File Offset: 0x0000EE70
		private string GetAttribute(XmlElement xel, string attribute)
		{
			XmlAttribute xmlAttribute = xel.Attributes[attribute];
			if (xmlAttribute == null)
			{
				return null;
			}
			return xmlAttribute.InnerText;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> state. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> element.-or- The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedInfo.CanonicalizationMethod" /> property.-or- The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedInfo.SignatureMethod" /> property.</exception>
		// Token: 0x06000414 RID: 1044 RVA: 0x00010C98 File Offset: 0x0000EE98
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "SignedInfo" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException();
			}
			this.id = this.GetAttribute(value, "Id");
			this.c14nMethod = XmlSignature.GetAttributeFromElement(value, "Algorithm", "CanonicalizationMethod");
			XmlElement childElement = XmlSignature.GetChildElement(value, "SignatureMethod", "http://www.w3.org/2000/09/xmldsig#");
			if (childElement != null)
			{
				this.signatureMethod = childElement.GetAttribute("Algorithm");
				XmlElement childElement2 = XmlSignature.GetChildElement(childElement, "HMACOutputLength", "http://www.w3.org/2000/09/xmldsig#");
				if (childElement2 != null)
				{
					this.signatureLength = childElement2.InnerText;
				}
			}
			for (int i = 0; i < value.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = value.ChildNodes[i];
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == "Reference" && xmlNode.NamespaceURI == "http://www.w3.org/2000/09/xmldsig#")
				{
					Reference reference = new Reference();
					reference.LoadXml((XmlElement)xmlNode);
					this.AddReference(reference);
				}
			}
			this.element = value;
		}

		// Token: 0x040001F0 RID: 496
		private ArrayList references;

		// Token: 0x040001F1 RID: 497
		private string c14nMethod;

		// Token: 0x040001F2 RID: 498
		private string id;

		// Token: 0x040001F3 RID: 499
		private string signatureMethod;

		// Token: 0x040001F4 RID: 500
		private string signatureLength;

		// Token: 0x040001F5 RID: 501
		private XmlElement element;
	}
}
