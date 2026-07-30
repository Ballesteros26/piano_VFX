using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Specifies the order of XML Digital Signature and XML Encryption operations when both are performed on the same document.</summary>
	// Token: 0x0200007B RID: 123
	public class XmlDecryptionTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> class. </summary>
		// Token: 0x06000370 RID: 880 RVA: 0x0000DC80 File Offset: 0x0000BE80
		public XmlDecryptionTransform()
		{
			base.Algorithm = "http://www.w3.org/2002/07/decrypt#XML";
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000DCDD File Offset: 0x0000BEDD
		private ArrayList ExceptUris
		{
			get
			{
				if (this._arrayListUri == null)
				{
					this._arrayListUri = new ArrayList();
				}
				return this._arrayListUri;
			}
		}

		/// <summary>Determines whether the ID attribute of an <see cref="T:System.Xml.XmlElement" /> object matches a specified value.</summary>
		/// <returns>true if the ID attribute of the <paramref name="inputElement" /> parameter matches the <paramref name="idValue" /> parameter; otherwise, false. </returns>
		/// <param name="inputElement">An <see cref="T:System.Xml.XmlElement" /> object with an ID attribute to compare with <paramref name="idValue" />.</param>
		/// <param name="idValue">The value to compare with the ID attribute of <paramref name="inputElement" />.</param>
		// Token: 0x06000372 RID: 882 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		protected virtual bool IsTargetElement(XmlElement inputElement, string idValue)
		{
			return inputElement != null && (inputElement.GetAttribute("Id") == idValue || inputElement.GetAttribute("id") == idValue || inputElement.GetAttribute("ID") == idValue);
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that contains information about the keys necessary to decrypt an XML document.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that contains information about the keys necessary to decrypt an XML document.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000DD48 File Offset: 0x0000BF48
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000DDAD File Offset: 0x0000BFAD
		public EncryptedXml EncryptedXml
		{
			get
			{
				if (this._exml != null)
				{
					return this._exml;
				}
				Reference reference = base.Reference;
				SignedXml signedXml = ((reference == null) ? base.SignedXml : reference.SignedXml);
				if (signedXml == null || signedXml.EncryptedXml == null)
				{
					this._exml = new EncryptedXml(this._containingDocument);
				}
				else
				{
					this._exml = signedXml.EncryptedXml;
				}
				return this._exml;
			}
			set
			{
				this._exml = value;
			}
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000DDB6 File Offset: 0x0000BFB6
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000DDBE File Offset: 0x0000BFBE
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Adds a Uniform Resource Identifier (URI) to exclude from processing.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) to exclude from processing</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is null.</exception>
		// Token: 0x06000377 RID: 887 RVA: 0x0000DDC6 File Offset: 0x0000BFC6
		public void AddExceptUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.ExceptUris.Add(uri);
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="nodeList" /> parameter is null.-or-The Uniform Resource Identifier (URI) value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> was not found.-or-The length of the URI value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> is 0. -or-The first character of the URI value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> is not '#'.  </exception>
		// Token: 0x06000378 RID: 888 RVA: 0x0000DDE4 File Offset: 0x0000BFE4
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
			this.ExceptUris.Clear();
			foreach (object obj in nodeList)
			{
				XmlElement xmlElement = ((XmlNode)obj) as XmlElement;
				if (xmlElement != null && xmlElement.LocalName == "Except" && xmlElement.NamespaceURI == "http://www.w3.org/2002/07/decrypt#")
				{
					string attribute = Utils.GetAttribute(xmlElement, "URI", "http://www.w3.org/2002/07/decrypt#");
					if (attribute == null || attribute.Length == 0 || attribute[0] != '#')
					{
						throw new CryptographicException("A Uri attribute is required for a CipherReference element.");
					}
					string text = Utils.ExtractIdFromLocalUri(attribute);
					this.ExceptUris.Add(text);
				}
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x06000379 RID: 889 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		protected override XmlNodeList GetInnerXml()
		{
			if (this.ExceptUris.Count == 0)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Transform", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(base.Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", base.Algorithm);
			}
			foreach (object obj in this.ExceptUris)
			{
				string text = (string)obj;
				XmlElement xmlElement2 = xmlDocument.CreateElement("Except", "http://www.w3.org/2002/07/decrypt#");
				xmlElement2.SetAttribute("URI", text);
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement.ChildNodes;
		}

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600037A RID: 890 RVA: 0x0000DF8C File Offset: 0x0000C18C
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				this.LoadStreamInput((Stream)obj);
				return;
			}
			if (obj is XmlDocument)
			{
				this.LoadXmlDocumentInput((XmlDocument)obj);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		private void LoadStreamInput(Stream stream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			XmlResolver xmlResolver = (base.ResolverSet ? this._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			XmlReader xmlReader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			xmlDocument.Load(xmlReader);
			this._containingDocument = xmlDocument;
			this._nsm = new XmlNamespaceManager(this._containingDocument.NameTable);
			this._nsm.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this._encryptedDataList = xmlDocument.SelectNodes("//enc:EncryptedData", this._nsm);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000E054 File Offset: 0x0000C254
		private void LoadXmlDocumentInput(XmlDocument document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this._containingDocument = document;
			this._nsm = new XmlNamespaceManager(document.NameTable);
			this._nsm.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this._encryptedDataList = document.SelectNodes("//enc:EncryptedData", this._nsm);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		private void ReplaceEncryptedData(XmlElement encryptedDataElement, byte[] decrypted)
		{
			XmlNode parentNode = encryptedDataElement.ParentNode;
			if (parentNode.NodeType == XmlNodeType.Document)
			{
				parentNode.InnerXml = this.EncryptedXml.Encoding.GetString(decrypted);
				return;
			}
			this.EncryptedXml.ReplaceData(encryptedDataElement, decrypted);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		private bool ProcessEncryptedDataItem(XmlElement encryptedDataElement)
		{
			if (this.ExceptUris.Count > 0)
			{
				for (int i = 0; i < this.ExceptUris.Count; i++)
				{
					if (this.IsTargetElement(encryptedDataElement, (string)this.ExceptUris[i]))
					{
						return false;
					}
				}
			}
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.LoadXml(encryptedDataElement);
			SymmetricAlgorithm decryptionKey = this.EncryptedXml.GetDecryptionKey(encryptedData, null);
			if (decryptionKey == null)
			{
				throw new CryptographicException("Unable to retrieve the decryption key.");
			}
			byte[] array = this.EncryptedXml.DecryptData(encryptedData, decryptionKey);
			this.ReplaceEncryptedData(encryptedDataElement, array);
			return true;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000E188 File Offset: 0x0000C388
		private void ProcessElementRecursively(XmlNodeList encryptedDatas)
		{
			if (encryptedDatas == null || encryptedDatas.Count == 0)
			{
				return;
			}
			Queue queue = new Queue();
			foreach (object obj in encryptedDatas)
			{
				XmlNode xmlNode = (XmlNode)obj;
				queue.Enqueue(xmlNode);
			}
			for (XmlNode xmlNode2 = queue.Dequeue() as XmlNode; xmlNode2 != null; xmlNode2 = queue.Dequeue() as XmlNode)
			{
				XmlElement xmlElement = xmlNode2 as XmlElement;
				if (xmlElement != null && xmlElement.LocalName == "EncryptedData" && xmlElement.NamespaceURI == "http://www.w3.org/2001/04/xmlenc#")
				{
					XmlNode nextSibling = xmlElement.NextSibling;
					XmlNode parentNode = xmlElement.ParentNode;
					if (this.ProcessEncryptedDataItem(xmlElement))
					{
						XmlNode xmlNode3 = parentNode.FirstChild;
						while (xmlNode3 != null && xmlNode3.NextSibling != nextSibling)
						{
							xmlNode3 = xmlNode3.NextSibling;
						}
						if (xmlNode3 != null)
						{
							XmlNodeList xmlNodeList = xmlNode3.SelectNodes("//enc:EncryptedData", this._nsm);
							if (xmlNodeList.Count > 0)
							{
								foreach (object obj2 in xmlNodeList)
								{
									XmlNode xmlNode4 = (XmlNode)obj2;
									queue.Enqueue(xmlNode4);
								}
							}
						}
					}
				}
				if (queue.Count == 0)
				{
					break;
				}
			}
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A decryption key could not be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000380 RID: 896 RVA: 0x0000E304 File Offset: 0x0000C504
		public override object GetOutput()
		{
			if (this._encryptedDataList != null)
			{
				this.ProcessElementRecursively(this._encryptedDataList);
			}
			Utils.AddNamespaces(this._containingDocument.DocumentElement, base.PropagatedNamespaces);
			return this._containingDocument;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		// Token: 0x06000381 RID: 897 RVA: 0x0000E336 File Offset: 0x0000C536
		public override object GetOutput(Type type)
		{
			if (type == typeof(XmlDocument))
			{
				return (XmlDocument)this.GetOutput();
			}
			throw new ArgumentException("The input type was invalid for this transform.", "type");
		}

		// Token: 0x040001B4 RID: 436
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument)
		};

		// Token: 0x040001B5 RID: 437
		private Type[] _outputTypes = new Type[] { typeof(XmlDocument) };

		// Token: 0x040001B6 RID: 438
		private XmlNodeList _encryptedDataList;

		// Token: 0x040001B7 RID: 439
		private ArrayList _arrayListUri;

		// Token: 0x040001B8 RID: 440
		private EncryptedXml _exml;

		// Token: 0x040001B9 RID: 441
		private XmlDocument _containingDocument;

		// Token: 0x040001BA RID: 442
		private XmlNamespaceManager _nsm;

		// Token: 0x040001BB RID: 443
		private const string XmlDecryptionTransformNamespaceUrl = "http://www.w3.org/2002/07/decrypt#";
	}
}
