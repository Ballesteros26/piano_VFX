using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Unity;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Provides a wrapper on a core XML signature object to facilitate creating XML signatures.</summary>
	// Token: 0x02000089 RID: 137
	public class SignedXml
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> class.</summary>
		// Token: 0x06000415 RID: 1045 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		public SignedXml()
		{
			this.m_signature = new Signature();
			this.m_signature.SignedInfo = new SignedInfo();
			this.hashes = new Hashtable(2);
			this._context = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> class from the specified XML document.</summary>
		/// <param name="document">The <see cref="T:System.Xml.XmlDocument" /> object to use to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.SignedXml" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="document" /> parameter is null.-or-The <paramref name="document" /> parameter contains a null <see cref="P:System.Xml.XmlDocument.DocumentElement" /> property.</exception>
		// Token: 0x06000416 RID: 1046 RVA: 0x00010E13 File Offset: 0x0000F013
		public SignedXml(XmlDocument document)
			: this()
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.envdoc = document;
			this._context = document.DocumentElement;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> class from the specified <see cref="T:System.Xml.XmlElement" /> object.</summary>
		/// <param name="elem">The <see cref="T:System.Xml.XmlElement" /> object to use to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.SignedXml" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is null. </exception>
		// Token: 0x06000417 RID: 1047 RVA: 0x00010E3C File Offset: 0x0000F03C
		public SignedXml(XmlElement elem)
			: this()
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			this.envdoc = new XmlDocument();
			this._context = elem;
			this.envdoc.LoadXml(elem.OuterXml);
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that defines the XML encryption processing rules.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that defines the XML encryption processing rules.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00010E75 File Offset: 0x0000F075
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x00010E7D File Offset: 0x0000F07D
		[ComVisible(false)]
		public EncryptedXml EncryptedXml
		{
			get
			{
				return this.encryptedXml;
			}
			set
			{
				this.encryptedXml = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00010E86 File Offset: 0x0000F086
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00010EB0 File Offset: 0x0000F0B0
		public KeyInfo KeyInfo
		{
			get
			{
				if (this.m_signature.KeyInfo == null)
				{
					this.m_signature.KeyInfo = new KeyInfo();
				}
				return this.m_signature.KeyInfo;
			}
			set
			{
				this.m_signature.KeyInfo = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Xml.Signature" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.Signature" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00010EBE File Offset: 0x0000F0BE
		public Signature Signature
		{
			get
			{
				return this.m_signature;
			}
		}

		/// <summary>Gets the length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00010EC6 File Offset: 0x0000F0C6
		public string SignatureLength
		{
			get
			{
				return this.m_signature.SignedInfo.SignatureLength;
			}
		}

		/// <summary>Gets the signature method of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The signature method of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		public string SignatureMethod
		{
			get
			{
				return this.m_signature.SignedInfo.SignatureMethod;
			}
		}

		/// <summary>Gets the signature value of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>A byte array that contains the signature value of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00010EEA File Offset: 0x0000F0EA
		public byte[] SignatureValue
		{
			get
			{
				return this.m_signature.SignatureValue;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00010EF7 File Offset: 0x0000F0F7
		public SignedInfo SignedInfo
		{
			get
			{
				return this.m_signature.SignedInfo;
			}
		}

		/// <summary>Gets or sets the asymmetric algorithm key used for signing a <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The asymmetric algorithm key used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00010F04 File Offset: 0x0000F104
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00010F0C File Offset: 0x0000F10C
		public AsymmetricAlgorithm SigningKey
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		/// <summary>Gets or sets the name of the installed key to be used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The name of the installed key to be used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00010F15 File Offset: 0x0000F115
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00010F1D File Offset: 0x0000F11D
		public string SigningKeyName
		{
			get
			{
				return this.m_strSigningKeyName;
			}
			set
			{
				this.m_strSigningKeyName = value;
			}
		}

		/// <summary>Sets the current <see cref="T:System.Xml.XmlResolver" /> object.</summary>
		/// <returns>The current <see cref="T:System.Xml.XmlResolver" /> object. The defaults is a <see cref="T:System.Xml.XmlSecureResolver" /> object.</returns>
		// Token: 0x170000F9 RID: 249
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x00010F26 File Offset: 0x0000F126
		public XmlResolver Resolver
		{
			set
			{
				this._xmlResolver = value;
				this._bResolverSet = true;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00010F36 File Offset: 0x0000F136
		internal bool ResolverSet
		{
			get
			{
				return this._bResolverSet;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object to the list of objects to be signed.</summary>
		/// <param name="dataObject">The <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object to add to the list of objects to be signed. </param>
		// Token: 0x06000427 RID: 1063 RVA: 0x00010F3E File Offset: 0x0000F13E
		public void AddObject(DataObject dataObject)
		{
			this.m_signature.AddObject(dataObject);
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Reference" /> object to the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object that describes a digest method, digest value, and transform to use for creating an XML digital signature.</summary>
		/// <param name="reference">The  <see cref="T:System.Security.Cryptography.Xml.Reference" /> object that describes a digest method, digest value, and transform to use for creating an XML digital signature.</param>
		// Token: 0x06000428 RID: 1064 RVA: 0x00010F4C File Offset: 0x0000F14C
		public void AddReference(Reference reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			this.m_signature.SignedInfo.AddReference(reference);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00010F70 File Offset: 0x0000F170
		private Stream ApplyTransform(Transform t, XmlDocument input)
		{
			if (t is XmlDsigXPathTransform || t is XmlDsigEnvelopedSignatureTransform || t is XmlDecryptionTransform)
			{
				input = (XmlDocument)input.Clone();
			}
			t.LoadInput(input);
			if (t is XmlDsigEnvelopedSignatureTransform)
			{
				return this.CanonicalizeOutput(t.GetOutput());
			}
			object output = t.GetOutput();
			if (output is Stream)
			{
				return (Stream)output;
			}
			if (output is XmlDocument)
			{
				MemoryStream memoryStream = new MemoryStream();
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				((XmlDocument)output).WriteTo(xmlTextWriter);
				xmlTextWriter.Flush();
				memoryStream.Position = 0L;
				return memoryStream;
			}
			if (output == null)
			{
				throw new NotImplementedException("This should not occur. Transform is " + t + ".");
			}
			return this.CanonicalizeOutput(output);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00011028 File Offset: 0x0000F228
		private Stream CanonicalizeOutput(object obj)
		{
			Transform c14NMethod = this.GetC14NMethod();
			c14NMethod.LoadInput(obj);
			return (Stream)c14NMethod.GetOutput();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00011044 File Offset: 0x0000F244
		private XmlDocument GetManifest(Reference r)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			if (r.Uri[0] == '#')
			{
				if (this.signatureElement != null)
				{
					XmlElement idElement = this.GetIdElement(this.signatureElement.OwnerDocument, r.Uri.Substring(1));
					if (idElement == null)
					{
						throw new CryptographicException("Manifest targeted by Reference was not found: " + r.Uri.Substring(1));
					}
					xmlDocument.AppendChild(xmlDocument.ImportNode(idElement, true));
					this.FixupNamespaceNodes(idElement, xmlDocument.DocumentElement, false);
				}
			}
			else if (this._xmlResolver != null)
			{
				Stream stream = (Stream)this._xmlResolver.GetEntity(new Uri(r.Uri), null, typeof(Stream));
				xmlDocument.Load(stream);
			}
			if (xmlDocument.FirstChild != null)
			{
				if (this.manifests == null)
				{
					this.manifests = new ArrayList();
				}
				this.manifests.Add(xmlDocument);
				return xmlDocument;
			}
			return null;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00011138 File Offset: 0x0000F338
		private void FixupNamespaceNodes(XmlElement src, XmlElement dst, bool ignoreDefault)
		{
			foreach (object obj in src.SelectNodes("namespace::*"))
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				if (!(xmlAttribute.LocalName == "xml") && (!ignoreDefault || !(xmlAttribute.LocalName == "xmlns")))
				{
					dst.SetAttributeNode(dst.OwnerDocument.ImportNode(xmlAttribute, true) as XmlAttribute);
				}
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000111D0 File Offset: 0x0000F3D0
		private byte[] GetReferenceHash(Reference r, bool check_hmac)
		{
			Stream stream = null;
			XmlDocument xmlDocument = null;
			if (r.Uri == string.Empty)
			{
				xmlDocument = this.envdoc;
			}
			else if (r.Type == "http://www.w3.org/2000/09/xmldsig#Manifest")
			{
				xmlDocument = this.GetManifest(r);
			}
			else
			{
				xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				string text = null;
				if (r.Uri.StartsWith("#xpointer"))
				{
					string text2 = string.Join("", r.Uri.Substring(9).Split(SignedXml.whitespaceChars));
					if (text2.Length < 2 || text2[0] != '(' || text2[text2.Length - 1] != ')')
					{
						text2 = string.Empty;
					}
					else
					{
						text2 = text2.Substring(1, text2.Length - 2);
					}
					if (text2 == "/")
					{
						xmlDocument = this.envdoc;
					}
					else if (text2.Length > 6 && text2.StartsWith("id(") && text2[text2.Length - 1] == ')')
					{
						text = text2.Substring(4, text2.Length - 6);
					}
				}
				else if (r.Uri[0] == '#')
				{
					text = r.Uri.Substring(1);
				}
				else if (this._xmlResolver != null)
				{
					try
					{
						Uri uri = new Uri(r.Uri);
						stream = (Stream)this._xmlResolver.GetEntity(uri, null, typeof(Stream));
					}
					catch
					{
						stream = File.OpenRead(r.Uri);
					}
				}
				if (text != null)
				{
					XmlElement xmlElement = null;
					foreach (object obj in this.m_signature.ObjectList)
					{
						DataObject dataObject = (DataObject)obj;
						if (dataObject.Id == text)
						{
							xmlElement = dataObject.GetXml();
							xmlElement.SetAttribute("xmlns", "http://www.w3.org/2000/09/xmldsig#");
							xmlDocument.AppendChild(xmlDocument.ImportNode(xmlElement, true));
							using (IEnumerator enumerator2 = xmlElement.ChildNodes.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									XmlNode xmlNode = (XmlNode)obj2;
									if (xmlNode.NodeType == XmlNodeType.Element)
									{
										this.FixupNamespaceNodes(xmlNode as XmlElement, xmlDocument.DocumentElement, true);
									}
								}
								break;
							}
						}
					}
					if (xmlElement == null && this.envdoc != null)
					{
						xmlElement = this.GetIdElement(this.envdoc, text);
						if (xmlElement != null)
						{
							xmlDocument.AppendChild(xmlDocument.ImportNode(xmlElement, true));
							this.FixupNamespaceNodes(xmlElement, xmlDocument.DocumentElement, false);
						}
					}
					if (xmlElement == null)
					{
						throw new CryptographicException(string.Format("Malformed reference object: {0}", text));
					}
				}
			}
			if (r.TransformChain.Count > 0)
			{
				using (IEnumerator enumerator = r.TransformChain.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj3 = enumerator.Current;
						Transform transform = (Transform)obj3;
						if (stream == null)
						{
							stream = this.ApplyTransform(transform, xmlDocument);
						}
						else
						{
							transform.LoadInput(stream);
							object output = transform.GetOutput();
							if (output is Stream)
							{
								stream = (Stream)output;
							}
							else
							{
								stream = this.CanonicalizeOutput(output);
							}
						}
					}
					goto IL_0383;
				}
			}
			if (stream == null)
			{
				if (r.Uri[0] != '#')
				{
					stream = new MemoryStream();
					xmlDocument.Save(stream);
				}
				else
				{
					stream = this.ApplyTransform(new XmlDsigC14NTransform(), xmlDocument);
				}
			}
			IL_0383:
			HashAlgorithm hash = this.GetHash(r.DigestMethod, check_hmac);
			if (hash != null)
			{
				return hash.ComputeHash(stream);
			}
			return null;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000115B0 File Offset: 0x0000F7B0
		private void DigestReferences()
		{
			foreach (object obj in this.m_signature.SignedInfo.References)
			{
				Reference reference = (Reference)obj;
				if (reference.DigestMethod == null)
				{
					reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
				}
				reference.DigestValue = this.GetReferenceHash(reference, false);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00011630 File Offset: 0x0000F830
		private Transform GetC14NMethod()
		{
			Transform transform = (Transform)CryptoConfig.CreateFromName(this.m_signature.SignedInfo.CanonicalizationMethod);
			if (transform == null)
			{
				throw new CryptographicException("Unknown Canonicalization Method {0}", this.m_signature.SignedInfo.CanonicalizationMethod);
			}
			return transform;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001166C File Offset: 0x0000F86C
		private Stream SignedInfoTransformed()
		{
			Transform c14NMethod = this.GetC14NMethod();
			if (this.signatureElement == null)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.LoadXml(this.m_signature.SignedInfo.GetXml().OuterXml);
				if (this.envdoc != null)
				{
					foreach (object obj in this.envdoc.DocumentElement.SelectNodes("namespace::*"))
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj;
						if (!(xmlAttribute.LocalName == "xml") && !(xmlAttribute.Prefix == xmlDocument.DocumentElement.Prefix))
						{
							xmlDocument.DocumentElement.SetAttributeNode(xmlDocument.ImportNode(xmlAttribute, true) as XmlAttribute);
						}
					}
				}
				c14NMethod.LoadInput(xmlDocument);
			}
			else
			{
				XmlElement xmlElement = this.signatureElement.GetElementsByTagName("SignedInfo", "http://www.w3.org/2000/09/xmldsig#")[0] as XmlElement;
				StringWriter stringWriter = new StringWriter();
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.WriteStartElement(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI);
				foreach (object obj2 in xmlElement.SelectNodes("namespace::*"))
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj2;
					if (xmlAttribute2.ParentNode != xmlElement && !(xmlAttribute2.LocalName == "xml") && !(xmlAttribute2.Prefix == xmlElement.Prefix))
					{
						xmlAttribute2.WriteTo(xmlTextWriter);
					}
				}
				foreach (object obj3 in xmlElement.Attributes)
				{
					((XmlNode)obj3).WriteTo(xmlTextWriter);
				}
				foreach (object obj4 in xmlElement.ChildNodes)
				{
					((XmlNode)obj4).WriteTo(xmlTextWriter);
				}
				xmlTextWriter.WriteEndElement();
				byte[] bytes = Encoding.UTF8.GetBytes(stringWriter.ToString());
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(bytes, 0, bytes.Length);
				memoryStream.Position = 0L;
				c14NMethod.LoadInput(memoryStream);
			}
			return (Stream)c14NMethod.GetOutput();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001191C File Offset: 0x0000FB1C
		private HashAlgorithm GetHash(string algorithm, bool check_hmac)
		{
			HashAlgorithm hashAlgorithm = (HashAlgorithm)this.hashes[algorithm];
			if (hashAlgorithm == null)
			{
				hashAlgorithm = HashAlgorithm.Create(algorithm);
				if (hashAlgorithm == null)
				{
					throw new CryptographicException("Unknown hash algorithm: {0}", algorithm);
				}
				this.hashes.Add(algorithm, hashAlgorithm);
			}
			else
			{
				hashAlgorithm.Initialize();
			}
			if (check_hmac && hashAlgorithm is KeyedHashAlgorithm)
			{
				return null;
			}
			return hashAlgorithm;
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies using the public key in the signature.</summary>
		/// <returns>true if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies; otherwise, false.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.AsymmetricAlgorithm.SignatureAlgorithm" /> property of the public key in the signature does not match the <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureMethod" /> property.-or- The signature description could not be created.-or The hash algorithm could not be created. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000432 RID: 1074 RVA: 0x00011977 File Offset: 0x0000FB77
		public bool CheckSignature()
		{
			return this.CheckSignatureInternal(null) != null;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00011984 File Offset: 0x0000FB84
		private bool CheckReferenceIntegrity(ArrayList referenceList)
		{
			if (referenceList == null)
			{
				return false;
			}
			foreach (object obj in referenceList)
			{
				Reference reference = (Reference)obj;
				byte[] referenceHash = this.GetReferenceHash(reference, true);
				if (!this.Compare(reference.DigestValue, referenceHash))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified key.</summary>
		/// <returns>true if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified key; otherwise, false.</returns>
		/// <param name="key">The implementation of the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> property that holds the key to be used to verify the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.AsymmetricAlgorithm.SignatureAlgorithm" /> property of the <paramref name="key" /> parameter does not match the <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureMethod" /> property.-or- The signature description could not be created.-or The hash algorithm could not be created. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000434 RID: 1076 RVA: 0x000119F8 File Offset: 0x0000FBF8
		public bool CheckSignature(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.CheckSignatureInternal(key) != null;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00011A14 File Offset: 0x0000FC14
		private AsymmetricAlgorithm CheckSignatureInternal(AsymmetricAlgorithm key)
		{
			this.pkEnumerator = null;
			if (key != null)
			{
				if (!this.CheckSignatureWithKey(key))
				{
					return null;
				}
			}
			else
			{
				if (this.Signature.KeyInfo == null)
				{
					return null;
				}
				while ((key = this.GetPublicKey()) != null && !this.CheckSignatureWithKey(key))
				{
				}
				this.pkEnumerator = null;
				if (key == null)
				{
					return null;
				}
			}
			if (!this.CheckReferenceIntegrity(this.m_signature.SignedInfo.References))
			{
				return null;
			}
			if (this.manifests != null)
			{
				for (int i = 0; i < this.manifests.Count; i++)
				{
					Manifest manifest = new Manifest((this.manifests[i] as XmlDocument).DocumentElement);
					if (!this.CheckReferenceIntegrity(manifest.References))
					{
						return null;
					}
				}
			}
			return key;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00011ACC File Offset: 0x0000FCCC
		private bool CheckSignatureWithKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				return false;
			}
			SignatureDescription signatureDescription = (SignatureDescription)CryptoConfig.CreateFromName(this.m_signature.SignedInfo.SignatureMethod);
			if (signatureDescription == null)
			{
				return false;
			}
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(signatureDescription.DeformatterAlgorithm);
			if (asymmetricSignatureDeformatter == null)
			{
				return false;
			}
			bool flag;
			try
			{
				asymmetricSignatureDeformatter.SetKey(key);
				asymmetricSignatureDeformatter.SetHashAlgorithm(signatureDescription.DigestAlgorithm);
				HashAlgorithm hash = this.GetHash(signatureDescription.DigestAlgorithm, true);
				MemoryStream memoryStream = (MemoryStream)this.SignedInfoTransformed();
				byte[] array = hash.ComputeHash(memoryStream);
				flag = asymmetricSignatureDeformatter.VerifySignature(array, this.m_signature.SignatureValue);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011B78 File Offset: 0x0000FD78
		private bool Compare(byte[] expected, byte[] actual)
		{
			bool flag = expected != null && actual != null;
			if (flag)
			{
				int num = expected.Length;
				flag = num == actual.Length;
				if (flag)
				{
					for (int i = 0; i < num; i++)
					{
						if (expected[i] != actual[i])
						{
							return false;
						}
					}
				}
			}
			return flag;
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified message authentication code (MAC) algorithm.</summary>
		/// <returns>true if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified MAC; otherwise, false.</returns>
		/// <param name="macAlg">The implementation of <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> that holds the MAC to be used to verify the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="macAlg" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.HashAlgorithm.HashSize" /> property of the specified <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object is not valid.-or- The <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property is null.-or- The cryptographic transform used to check the signature could not be created. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000438 RID: 1080 RVA: 0x00011BB8 File Offset: 0x0000FDB8
		public bool CheckSignature(KeyedHashAlgorithm macAlg)
		{
			if (macAlg == null)
			{
				throw new ArgumentNullException("macAlg");
			}
			this.pkEnumerator = null;
			Stream stream = this.SignedInfoTransformed();
			if (stream == null)
			{
				return false;
			}
			byte[] array = macAlg.ComputeHash(stream);
			if (this.m_signature.SignedInfo.SignatureLength != null)
			{
				int num = int.Parse(this.m_signature.SignedInfo.SignatureLength);
				if ((num & 7) != 0)
				{
					throw new CryptographicException("Signature length must be a multiple of 8 bits.");
				}
				num >>= 3;
				if (num != this.m_signature.SignatureValue.Length)
				{
					throw new CryptographicException("Invalid signature length.");
				}
				int num2 = Math.Max(10, array.Length / 2);
				if (num < num2)
				{
					throw new CryptographicException("HMAC signature is too small");
				}
				if (num < array.Length)
				{
					byte[] array2 = new byte[num];
					Buffer.BlockCopy(array, 0, array2, 0, num);
					array = array2;
				}
			}
			return this.Compare(this.m_signature.SignatureValue, array) && this.CheckReferenceIntegrity(this.m_signature.SignedInfo.References);
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object and, optionally, whether the certificate is valid.</summary>
		/// <returns>true if the signature is valid; otherwise, false. -or-true if the signature and certificate are valid; otherwise, false. </returns>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to use to verify the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property.</param>
		/// <param name="verifySignatureOnly">true to verify the signature only; false to verify both the signature and certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A signature description could not be created for the <paramref name="certificate" /> parameter.</exception>
		// Token: 0x06000439 RID: 1081 RVA: 0x00010A67 File Offset: 0x0000EC67
		[MonoTODO]
		[ComVisible(false)]
		public bool CheckSignature(X509Certificate2 certificate, bool verifySignatureOnly)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies using the public key in the signature.</summary>
		/// <returns>true if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies using the public key in the signature; otherwise, false.</returns>
		/// <param name="signingKey">When this method returns, contains the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the public key in the signature. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="signingKey" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.AsymmetricAlgorithm.SignatureAlgorithm" /> property of the public key in the signature does not match the <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureMethod" /> property.-or- The signature description could not be created.-or The hash algorithm could not be created. </exception>
		// Token: 0x0600043A RID: 1082 RVA: 0x00011CA9 File Offset: 0x0000FEA9
		public bool CheckSignatureReturningKey(out AsymmetricAlgorithm signingKey)
		{
			signingKey = this.CheckSignatureInternal(null);
			return signingKey != null;
		}

		/// <summary>Computes an XML digital signature.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SigningKey" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SigningKey" /> property is not a <see cref="T:System.Security.Cryptography.DSA" /> object or <see cref="T:System.Security.Cryptography.RSA" /> object.-or- The key could not be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043B RID: 1083 RVA: 0x00011CBC File Offset: 0x0000FEBC
		public void ComputeSignature()
		{
			this.DigestReferences();
			if (this.key == null)
			{
				throw new CryptographicException("Signing key is not loaded.");
			}
			if (this.SignedInfo.SignatureMethod == null)
			{
				if (this.key is DSA)
				{
					this.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
				}
				else
				{
					if (!(this.key is RSA))
					{
						throw new CryptographicException("Failed to create signing key.");
					}
					this.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
				}
			}
			SignatureDescription signatureDescription = CryptoConfig.CreateFromName(this.SignedInfo.SignatureMethod) as SignatureDescription;
			if (signatureDescription == null)
			{
				throw new CryptographicException("SignatureDescription could not be created for the signature algorithm supplied.");
			}
			HashAlgorithm hashAlgorithm = signatureDescription.CreateDigest();
			if (hashAlgorithm == null)
			{
				throw new CryptographicException("Could not create hash algorithm object.");
			}
			hashAlgorithm.ComputeHash(this.SignedInfoTransformed());
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = signatureDescription.CreateFormatter(this.key);
			this.m_signature.SignatureValue = asymmetricSignatureFormatter.CreateSignature(hashAlgorithm);
		}

		/// <summary>Computes an XML digital signature using the specified message authentication code (MAC) algorithm.</summary>
		/// <param name="macAlg">A <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object that holds the MAC to be used to compute the value of the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="macAlg" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object specified by the <paramref name="macAlg" /> parameter is not an instance of <see cref="T:System.Security.Cryptography.HMACSHA1" />.-or- The <see cref="P:System.Security.Cryptography.HashAlgorithm.HashSize" /> property of the specified <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object is not valid.-or- The cryptographic transform used to check the signature could not be created. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043C RID: 1084 RVA: 0x00011D9C File Offset: 0x0000FF9C
		public void ComputeSignature(KeyedHashAlgorithm macAlg)
		{
			if (macAlg == null)
			{
				throw new ArgumentNullException("macAlg");
			}
			string text = null;
			if (macAlg is HMACSHA1)
			{
				text = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
			}
			else if (macAlg is HMACSHA256)
			{
				text = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
			}
			else if (macAlg is HMACSHA384)
			{
				text = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";
			}
			else if (macAlg is HMACSHA512)
			{
				text = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";
			}
			else if (macAlg is HMACRIPEMD160)
			{
				text = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";
			}
			if (text == null)
			{
				throw new CryptographicException("unsupported algorithm");
			}
			this.DigestReferences();
			this.m_signature.SignedInfo.SignatureMethod = text;
			this.m_signature.SignatureValue = macAlg.ComputeHash(this.SignedInfoTransformed());
		}

		/// <summary>Returns the <see cref="T:System.Xml.XmlElement" /> object with the specified ID from the specified <see cref="T:System.Xml.XmlDocument" /> object.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlElement" /> object with the specified ID from the specified <see cref="T:System.Xml.XmlDocument" /> object, or null if it could not be found.</returns>
		/// <param name="document">The <see cref="T:System.Xml.XmlDocument" /> object to retrieve the <see cref="T:System.Xml.XmlElement" /> object from.</param>
		/// <param name="idValue">The ID of the <see cref="T:System.Xml.XmlElement" /> object to retrieve from the <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x0600043D RID: 1085 RVA: 0x00011E44 File Offset: 0x00010044
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			if (document == null || idValue == null)
			{
				return null;
			}
			XmlElement xmlElement = document.GetElementById(idValue);
			if (xmlElement == null)
			{
				xmlElement = (XmlElement)document.SelectSingleNode("//*[@Id='" + idValue + "']");
				if (xmlElement == null)
				{
					xmlElement = (XmlElement)document.SelectSingleNode("//*[@ID='" + idValue + "']");
					if (xmlElement == null)
					{
						xmlElement = (XmlElement)document.SelectSingleNode("//*[@id='" + idValue + "']");
					}
				}
			}
			return xmlElement;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00011EC0 File Offset: 0x000100C0
		internal static XmlElement DefaultGetIdElement(XmlDocument document, string idValue)
		{
			if (document == null)
			{
				return null;
			}
			try
			{
				XmlConvert.VerifyNCName(idValue);
			}
			catch
			{
				return null;
			}
			XmlElement xmlElement = document.GetElementById(idValue);
			if (xmlElement != null)
			{
				XmlDocument xmlDocument = (XmlDocument)document.CloneNode(true);
				XmlElement elementById = xmlDocument.GetElementById(idValue);
				if (elementById != null)
				{
					elementById.Attributes.RemoveAll();
					if (xmlDocument.GetElementById(idValue) != null)
					{
						throw new CryptographicException("Malformed reference element.");
					}
				}
				return xmlElement;
			}
			xmlElement = SignedXml.GetSingleReferenceTarget(document, "Id", idValue);
			if (xmlElement != null)
			{
				return xmlElement;
			}
			xmlElement = SignedXml.GetSingleReferenceTarget(document, "id", idValue);
			if (xmlElement != null)
			{
				return xmlElement;
			}
			return SignedXml.GetSingleReferenceTarget(document, "ID", idValue);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00011F6C File Offset: 0x0001016C
		private static XmlElement GetSingleReferenceTarget(XmlDocument document, string idAttributeName, string idValue)
		{
			string text = string.Concat(new string[] { "//*[@", idAttributeName, "=\"", idValue, "\"]" });
			XmlNodeList xmlNodeList = document.SelectNodes(text);
			if (xmlNodeList == null || xmlNodeList.Count == 0)
			{
				return null;
			}
			if (xmlNodeList.Count == 1)
			{
				return xmlNodeList[0] as XmlElement;
			}
			throw new CryptographicException("Malformed reference element.");
		}

		/// <summary>Returns the public key of a signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object that contains the public key of the signature, or null if the key cannot be found.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedXml.KeyInfo" /> property is null.</exception>
		// Token: 0x06000440 RID: 1088 RVA: 0x00011FDC File Offset: 0x000101DC
		protected virtual AsymmetricAlgorithm GetPublicKey()
		{
			if (this.m_signature.KeyInfo == null)
			{
				return null;
			}
			if (this.pkEnumerator == null)
			{
				this.pkEnumerator = this.m_signature.KeyInfo.GetEnumerator();
			}
			if (this._x509Enumerator != null)
			{
				if (this._x509Enumerator.MoveNext())
				{
					return new X509Certificate2(((X509Certificate)this._x509Enumerator.Current).GetRawCertData()).PublicKey.Key;
				}
				this._x509Enumerator = null;
			}
			while (this.pkEnumerator.MoveNext())
			{
				AsymmetricAlgorithm asymmetricAlgorithm = null;
				KeyInfoClause keyInfoClause = (KeyInfoClause)this.pkEnumerator.Current;
				if (keyInfoClause is DSAKeyValue)
				{
					asymmetricAlgorithm = DSA.Create();
				}
				else if (keyInfoClause is RSAKeyValue)
				{
					asymmetricAlgorithm = RSA.Create();
				}
				if (asymmetricAlgorithm != null)
				{
					asymmetricAlgorithm.FromXmlString(keyInfoClause.GetXml().InnerXml);
					return asymmetricAlgorithm;
				}
				if (keyInfoClause is KeyInfoX509Data)
				{
					this._x509Enumerator = ((KeyInfoX509Data)keyInfoClause).Certificates.GetEnumerator();
					if (this._x509Enumerator.MoveNext())
					{
						return new X509Certificate2(((X509Certificate)this._x509Enumerator.Current).GetRawCertData()).PublicKey.Key;
					}
				}
			}
			return null;
		}

		/// <summary>Returns the XML representation of a <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignedInfo" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureValue" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000441 RID: 1089 RVA: 0x00012107 File Offset: 0x00010307
		public XmlElement GetXml()
		{
			return this.m_signature.GetXml(this.envdoc);
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> state from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureValue" /> property.-or- The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignedInfo" /> property.</exception>
		// Token: 0x06000442 RID: 1090 RVA: 0x0001211C File Offset: 0x0001031C
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.signatureElement = value;
			this.m_signature.LoadXml(value);
			if (this._context == null)
			{
				this._context = value;
			}
			foreach (object obj in this.m_signature.SignedInfo.References)
			{
				foreach (object obj2 in ((Reference)obj).TransformChain)
				{
					Transform transform = (Transform)obj2;
					if (transform is XmlDecryptionTransform)
					{
						((XmlDecryptionTransform)transform).EncryptedXml = this.EncryptedXml;
					}
				}
			}
		}

		/// <summary>Gets the names of methods whose canonicalization algorithms are explicitly allowed. </summary>
		/// <returns>A collection of the names of methods that safely produce canonical XML. </returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00012218 File Offset: 0x00010418
		public Collection<string> SafeCanonicalizationMethods
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a delegate that will be called to validate the format (not the cryptographic security) of an XML signature.</summary>
		/// <returns>true if the format is acceptable; otherwise, false.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00012218 File Offset: 0x00010418
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00002FF8 File Offset: 0x000011F8
		public Func<SignedXml, bool> SignatureFormatValidator
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard namespace for XML digital signatures. This field is constant.</summary>
		// Token: 0x040001F6 RID: 502
		public const string XmlDsigNamespaceUrl = "http://www.w3.org/2000/09/xmldsig#";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard minimal canonicalization algorithm for XML digital signatures. This field is constant.</summary>
		// Token: 0x040001F7 RID: 503
		public const string XmlDsigMinimalCanonicalizationUrl = "http://www.w3.org/2000/09/xmldsig#minimal";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard canonicalization algorithm for XML digital signatures. This field is constant.</summary>
		// Token: 0x040001F8 RID: 504
		public const string XmlDsigCanonicalizationUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard canonicalization algorithm for XML digital signatures and includes comments. This field is constant.</summary>
		// Token: 0x040001F9 RID: 505
		public const string XmlDsigCanonicalizationWithCommentsUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.SHA1" /> digest method for XML digital signatures. This field is constant.</summary>
		// Token: 0x040001FA RID: 506
		public const string XmlDsigSHA1Url = "http://www.w3.org/2000/09/xmldsig#sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.DSA" /> algorithm for XML digital signatures. This field is constant.</summary>
		// Token: 0x040001FB RID: 507
		public const string XmlDsigDSAUrl = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.RSA" /> signature method for XML digital signatures. This field is constant.</summary>
		// Token: 0x040001FC RID: 508
		public const string XmlDsigRSASHA1Url = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.HMACSHA1" /> algorithm for XML digital signatures. This field is constant.</summary>
		// Token: 0x040001FD RID: 509
		public const string XmlDsigHMACSHA1Url = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		// Token: 0x040001FE RID: 510
		public const string XmlDsigSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x040001FF RID: 511
		public const string XmlDsigRSASHA256Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x04000200 RID: 512
		public const string XmlDsigSHA384Url = "http://www.w3.org/2001/04/xmldsig-more#sha384";

		// Token: 0x04000201 RID: 513
		public const string XmlDsigRSASHA384Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384";

		// Token: 0x04000202 RID: 514
		public const string XmlDsigSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x04000203 RID: 515
		public const string XmlDsigRSASHA512Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the Canonical XML transformation. This field is constant.</summary>
		// Token: 0x04000204 RID: 516
		public const string XmlDsigC14NTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the Canonical XML transformation, with comments. This field is constant.</summary>
		// Token: 0x04000205 RID: 517
		public const string XmlDsigC14NWithCommentsTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		/// <summary>Represents the Uniform Resource Identifier (URI) for exclusive XML canonicalization. This field is constant.</summary>
		// Token: 0x04000206 RID: 518
		public const string XmlDsigExcC14NTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#";

		/// <summary>Represents the Uniform Resource Identifier (URI) for exclusive XML canonicalization, with comments. This field is constant.</summary>
		// Token: 0x04000207 RID: 519
		public const string XmlDsigExcC14NWithCommentsTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the base 64 transformation. This field is constant.</summary>
		// Token: 0x04000208 RID: 520
		public const string XmlDsigBase64TransformUrl = "http://www.w3.org/2000/09/xmldsig#base64";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the XML Path Language (XPath). This field is constant.</summary>
		// Token: 0x04000209 RID: 521
		public const string XmlDsigXPathTransformUrl = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		/// <summary>Represents the Uniform Resource Identifier (URI) for XSLT transformations. This field is constant.</summary>
		// Token: 0x0400020A RID: 522
		public const string XmlDsigXsltTransformUrl = "http://www.w3.org/TR/1999/REC-xslt-19991116";

		/// <summary>Represents the Uniform Resource Identifier (URI) for enveloped signature transformation. This field is constant.</summary>
		// Token: 0x0400020B RID: 523
		public const string XmlDsigEnvelopedSignatureTransformUrl = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the XML mode decryption transformation. This field is constant.</summary>
		// Token: 0x0400020C RID: 524
		public const string XmlDecryptionTransformUrl = "http://www.w3.org/2002/07/decrypt#XML";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the license transform algorithm used to normalize XrML licenses for signatures.</summary>
		// Token: 0x0400020D RID: 525
		public const string XmlLicenseTransformUrl = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";

		// Token: 0x0400020E RID: 526
		private EncryptedXml encryptedXml;

		/// <summary>Represents the <see cref="T:System.Security.Cryptography.Xml.Signature" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object. </summary>
		// Token: 0x0400020F RID: 527
		protected Signature m_signature;

		// Token: 0x04000210 RID: 528
		private AsymmetricAlgorithm key;

		/// <summary>Represents the name of the installed key to be used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object. </summary>
		// Token: 0x04000211 RID: 529
		protected string m_strSigningKeyName;

		// Token: 0x04000212 RID: 530
		private XmlDocument envdoc;

		// Token: 0x04000213 RID: 531
		private IEnumerator pkEnumerator;

		// Token: 0x04000214 RID: 532
		private XmlElement signatureElement;

		// Token: 0x04000215 RID: 533
		private Hashtable hashes;

		// Token: 0x04000216 RID: 534
		internal XmlResolver _xmlResolver = new XmlUrlResolver();

		// Token: 0x04000217 RID: 535
		private bool _bResolverSet = true;

		// Token: 0x04000218 RID: 536
		internal XmlElement _context;

		// Token: 0x04000219 RID: 537
		private ArrayList manifests;

		// Token: 0x0400021A RID: 538
		private IEnumerator _x509Enumerator;

		// Token: 0x0400021B RID: 539
		private static readonly char[] whitespaceChars = new char[] { ' ', '\r', '\n', '\t' };
	}
}
