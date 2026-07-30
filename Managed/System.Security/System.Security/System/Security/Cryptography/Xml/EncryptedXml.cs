using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the process model for implementing XML encryption.</summary>
	// Token: 0x0200005A RID: 90
	public class EncryptedXml
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class.</summary>
		// Token: 0x06000217 RID: 535 RVA: 0x00007FE4 File Offset: 0x000061E4
		public EncryptedXml()
			: this(new XmlDocument())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class using the specified XML document.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object used to initialize the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object.</param>
		// Token: 0x06000218 RID: 536 RVA: 0x00007FF1 File Offset: 0x000061F1
		public EncryptedXml(XmlDocument document)
			: this(document, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class using the specified XML document and evidence.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object used to initialize the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object.</param>
		/// <param name="evidence">An <see cref="T:System.Security.Policy.Evidence" /> object associated with the <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06000219 RID: 537 RVA: 0x00007FFC File Offset: 0x000061FC
		public EncryptedXml(XmlDocument document, Evidence evidence)
		{
			this._document = document;
			this._evidence = evidence;
			this._xmlResolver = null;
			this._padding = PaddingMode.ISO10126;
			this._mode = CipherMode.CBC;
			this._encoding = Encoding.UTF8;
			this._keyNameMapping = new Hashtable(4);
			this._xmlDsigSearchDepth = 20;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008051 File Offset: 0x00006251
		private bool IsOverXmlDsigRecursionLimit()
		{
			return this._xmlDsigSearchDepthCounter > this.XmlDSigSearchDepth;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008064 File Offset: 0x00006264
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000806C File Offset: 0x0000626C
		public int XmlDSigSearchDepth
		{
			get
			{
				return this._xmlDsigSearchDepth;
			}
			set
			{
				this._xmlDsigSearchDepth = value;
			}
		}

		/// <summary>Gets or sets the evidence of the <see cref="T:System.Xml.XmlDocument" /> object from which the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object is constructed.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008075 File Offset: 0x00006275
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000807D File Offset: 0x0000627D
		public Evidence DocumentEvidence
		{
			get
			{
				return this._evidence;
			}
			set
			{
				this._evidence = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlResolver" /> object used by the Document Object Model (DOM) to resolve external XML references.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> object.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008086 File Offset: 0x00006286
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000808E File Offset: 0x0000628E
		public XmlResolver Resolver
		{
			get
			{
				return this._xmlResolver;
			}
			set
			{
				this._xmlResolver = value;
			}
		}

		/// <summary>Gets or sets the padding mode used for XML encryption.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.PaddingMode" /> values that specifies the type of padding used for encryption.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008097 File Offset: 0x00006297
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000809F File Offset: 0x0000629F
		public PaddingMode Padding
		{
			get
			{
				return this._padding;
			}
			set
			{
				this._padding = value;
			}
		}

		/// <summary>Gets or sets the cipher mode used for XML encryption.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.CipherMode" /> values.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000080A8 File Offset: 0x000062A8
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000080B0 File Offset: 0x000062B0
		public CipherMode Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		/// <summary>Gets or sets the encoding used for XML encryption.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000080B9 File Offset: 0x000062B9
		// (set) Token: 0x06000226 RID: 550 RVA: 0x000080C1 File Offset: 0x000062C1
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
			}
		}

		/// <summary>Gets or sets the recipient of the encrypted key information.</summary>
		/// <returns>The recipient of the encrypted key information.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000080CA File Offset: 0x000062CA
		// (set) Token: 0x06000228 RID: 552 RVA: 0x000080E5 File Offset: 0x000062E5
		public string Recipient
		{
			get
			{
				if (this._recipient == null)
				{
					this._recipient = string.Empty;
				}
				return this._recipient;
			}
			set
			{
				this._recipient = value;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000080F0 File Offset: 0x000062F0
		private byte[] GetCipherValue(CipherData cipherData)
		{
			if (cipherData == null)
			{
				throw new ArgumentNullException("cipherData");
			}
			WebResponse webResponse = null;
			Stream stream = null;
			if (cipherData.CipherValue != null)
			{
				return cipherData.CipherValue;
			}
			if (cipherData.CipherReference == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			if (cipherData.CipherReference.CipherValue != null)
			{
				return cipherData.CipherReference.CipherValue;
			}
			Stream stream2;
			if (cipherData.CipherReference.Uri.Length == 0)
			{
				string text = ((this._document == null) ? null : this._document.BaseURI);
				stream2 = cipherData.CipherReference.TransformChain.TransformToOctetStream(this._document, this._xmlResolver, text);
			}
			else
			{
				if (cipherData.CipherReference.Uri[0] != '#')
				{
					throw new CryptographicException("Unable to resolve Uri {0}.", cipherData.CipherReference.Uri);
				}
				string text2 = Utils.ExtractIdFromLocalUri(cipherData.CipherReference.Uri);
				stream = new MemoryStream(this._encoding.GetBytes(this.GetIdElement(this._document, text2).OuterXml));
				string text3 = ((this._document == null) ? null : this._document.BaseURI);
				stream2 = cipherData.CipherReference.TransformChain.TransformToOctetStream(stream, this._xmlResolver, text3);
			}
			byte[] array = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Utils.Pump(stream2, memoryStream);
				array = memoryStream.ToArray();
				if (webResponse != null)
				{
					webResponse.Close();
				}
				if (stream != null)
				{
					stream.Close();
				}
				stream2.Close();
			}
			cipherData.CipherReference.CipherValue = array;
			return array;
		}

		/// <summary>Determines how to resolve internal Uniform Resource Identifier (URI) references.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that contains an ID indicating how internal Uniform Resource Identifiers (URIs) are to be resolved.</returns>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object that contains an element with an ID value.</param>
		/// <param name="idValue">A string that represents the ID value.</param>
		// Token: 0x0600022A RID: 554 RVA: 0x00008290 File Offset: 0x00006490
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return SignedXml.DefaultGetIdElement(document, idValue);
		}

		/// <summary>Retrieves the decryption initialization vector (IV) from an <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <returns>A byte array that contains the decryption initialization vector (IV).</returns>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that contains the initialization vector (IV) to retrieve.</param>
		/// <param name="symmetricAlgorithmUri">The Uniform Resource Identifier (URI) that describes the cryptographic algorithm associated with the <paramref name="encryptedData" /> value.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="encryptedData" /> parameter has an <see cref="P:System.Security.Cryptography.Xml.EncryptedType.EncryptionMethod" />  property that is null.-or-The value of the <paramref name="symmetricAlgorithmUrisymAlgUri" /> parameter is not a supported algorithm.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600022B RID: 555 RVA: 0x0000829C File Offset: 0x0000649C
		public virtual byte[] GetDecryptionIV(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (symmetricAlgorithmUri == null)
			{
				if (encryptedData.EncryptionMethod == null)
				{
					throw new CryptographicException("Symmetric algorithm is not specified.");
				}
				symmetricAlgorithmUri = encryptedData.EncryptionMethod.KeyAlgorithm;
			}
			int num;
			if (!(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#des-cbc") && !(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
			{
				if (!(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#aes128-cbc") && !(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#aes192-cbc") && !(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
				{
					throw new CryptographicException(" The specified Uri is not supported.");
				}
				num = 16;
			}
			else
			{
				num = 8;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(this.GetCipherValue(encryptedData.CipherData), 0, array, 0, array.Length);
			return array;
		}

		/// <summary>Retrieves the decryption key from the specified <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> object associated with the decryption key.</returns>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that contains the decryption key to retrieve.</param>
		/// <param name="symmetricAlgorithmUri">The size of the decryption key to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encryptedData parameter has an <see cref="P:System.Security.Cryptography.Xml.EncryptedType.EncryptionMethod" /> property that is null.-or-The encrypted key cannot be retrieved using the specified parameters.</exception>
		// Token: 0x0600022C RID: 556 RVA: 0x00008354 File Offset: 0x00006554
		public virtual SymmetricAlgorithm GetDecryptionKey(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (encryptedData.KeyInfo == null)
			{
				return null;
			}
			IEnumerator enumerator = encryptedData.KeyInfo.GetEnumerator();
			EncryptedKey encryptedKey = null;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				KeyInfoName keyInfoName = obj as KeyInfoName;
				if (keyInfoName != null)
				{
					string value = keyInfoName.Value;
					if ((SymmetricAlgorithm)this._keyNameMapping[value] != null)
					{
						return (SymmetricAlgorithm)this._keyNameMapping[value];
					}
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this._document.NameTable);
					xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
					XmlNodeList xmlNodeList = this._document.SelectNodes("//enc:EncryptedKey", xmlNamespaceManager);
					if (xmlNodeList == null)
					{
						break;
					}
					using (IEnumerator enumerator2 = xmlNodeList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlElement xmlElement = ((XmlNode)obj2) as XmlElement;
							EncryptedKey encryptedKey2 = new EncryptedKey();
							encryptedKey2.LoadXml(xmlElement);
							if (encryptedKey2.CarriedKeyName == value && encryptedKey2.Recipient == this.Recipient)
							{
								encryptedKey = encryptedKey2;
								break;
							}
						}
						break;
					}
				}
				KeyInfoRetrievalMethod keyInfoRetrievalMethod = enumerator.Current as KeyInfoRetrievalMethod;
				if (keyInfoRetrievalMethod != null)
				{
					string text = Utils.ExtractIdFromLocalUri(keyInfoRetrievalMethod.Uri);
					encryptedKey = new EncryptedKey();
					encryptedKey.LoadXml(this.GetIdElement(this._document, text));
					break;
				}
				KeyInfoEncryptedKey keyInfoEncryptedKey = enumerator.Current as KeyInfoEncryptedKey;
				if (keyInfoEncryptedKey != null)
				{
					encryptedKey = keyInfoEncryptedKey.EncryptedKey;
					break;
				}
			}
			if (encryptedKey == null)
			{
				return null;
			}
			if (symmetricAlgorithmUri == null)
			{
				if (encryptedData.EncryptionMethod == null)
				{
					throw new CryptographicException("Symmetric algorithm is not specified.");
				}
				symmetricAlgorithmUri = encryptedData.EncryptionMethod.KeyAlgorithm;
			}
			byte[] array = this.DecryptEncryptedKey(encryptedKey);
			if (array == null)
			{
				throw new CryptographicException("Unable to retrieve the decryption key.");
			}
			SymmetricAlgorithm symmetricAlgorithm = (SymmetricAlgorithm)CryptoHelpers.CreateFromName(symmetricAlgorithmUri);
			symmetricAlgorithm.Key = array;
			return symmetricAlgorithm;
		}

		/// <summary>Determines the key represented by the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> element.</summary>
		/// <returns>A byte array that contains the key.</returns>
		/// <param name="encryptedKey">The <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that contains the key to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedKey" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="encryptedKey" /> parameter is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael). </exception>
		// Token: 0x0600022D RID: 557 RVA: 0x00008548 File Offset: 0x00006748
		public virtual byte[] DecryptEncryptedKey(EncryptedKey encryptedKey)
		{
			if (encryptedKey == null)
			{
				throw new ArgumentNullException("encryptedKey");
			}
			if (encryptedKey.KeyInfo == null)
			{
				return null;
			}
			foreach (object obj in encryptedKey.KeyInfo)
			{
				KeyInfoName keyInfoName = obj as KeyInfoName;
				bool flag;
				if (keyInfoName == null)
				{
					IEnumerator enumerator;
					KeyInfoX509Data keyInfoX509Data = enumerator.Current as KeyInfoX509Data;
					if (keyInfoX509Data != null)
					{
						foreach (X509Certificate2 x509Certificate in Utils.BuildBagOfCerts(keyInfoX509Data, CertUsageType.Decryption))
						{
							using (RSA rsaprivateKey = x509Certificate.GetRSAPrivateKey())
							{
								if (rsaprivateKey != null)
								{
									flag = encryptedKey.EncryptionMethod != null && encryptedKey.EncryptionMethod.KeyAlgorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";
									return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, rsaprivateKey, flag);
								}
							}
						}
						break;
					}
					KeyInfoRetrievalMethod keyInfoRetrievalMethod = enumerator.Current as KeyInfoRetrievalMethod;
					EncryptedKey encryptedKey2;
					if (keyInfoRetrievalMethod != null)
					{
						string text = Utils.ExtractIdFromLocalUri(keyInfoRetrievalMethod.Uri);
						encryptedKey2 = new EncryptedKey();
						encryptedKey2.LoadXml(this.GetIdElement(this._document, text));
						try
						{
							this._xmlDsigSearchDepthCounter++;
							if (this.IsOverXmlDsigRecursionLimit())
							{
								throw new CryptoSignedXmlRecursionException();
							}
							return this.DecryptEncryptedKey(encryptedKey2);
						}
						finally
						{
							this._xmlDsigSearchDepthCounter--;
						}
					}
					KeyInfoEncryptedKey keyInfoEncryptedKey = enumerator.Current as KeyInfoEncryptedKey;
					if (keyInfoEncryptedKey == null)
					{
						continue;
					}
					encryptedKey2 = keyInfoEncryptedKey.EncryptedKey;
					byte[] array = this.DecryptEncryptedKey(encryptedKey2);
					if (array != null)
					{
						SymmetricAlgorithm symmetricAlgorithm = (SymmetricAlgorithm)CryptoHelpers.CreateFromName(encryptedKey.EncryptionMethod.KeyAlgorithm);
						symmetricAlgorithm.Key = array;
						return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, symmetricAlgorithm);
					}
					continue;
				}
				string value = keyInfoName.Value;
				object obj2 = this._keyNameMapping[value];
				if (obj2 == null)
				{
					break;
				}
				if (obj2 is SymmetricAlgorithm)
				{
					return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, (SymmetricAlgorithm)obj2);
				}
				flag = encryptedKey.EncryptionMethod != null && encryptedKey.EncryptionMethod.KeyAlgorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";
				return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, (RSA)obj2, flag);
			}
			return null;
		}

		/// <summary>Defines a mapping between a key name and a symmetric key or an asymmetric key.</summary>
		/// <param name="keyName">The name to map to <paramref name="keyObject" />.</param>
		/// <param name="keyObject">The symmetric key to map to <paramref name="keyName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyName" /> parameter is null.-or-The value of the <paramref name="keyObject" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="keyObject" /> parameter is not an RSA algorithm or a symmetric key. </exception>
		// Token: 0x0600022E RID: 558 RVA: 0x00008790 File Offset: 0x00006990
		public void AddKeyNameMapping(string keyName, object keyObject)
		{
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			if (keyObject == null)
			{
				throw new ArgumentNullException("keyObject");
			}
			if (!(keyObject is SymmetricAlgorithm) && !(keyObject is RSA))
			{
				throw new CryptographicException("The specified cryptographic transform is not supported.");
			}
			this._keyNameMapping.Add(keyName, keyObject);
		}

		/// <summary>Resets all key name mapping.</summary>
		// Token: 0x0600022F RID: 559 RVA: 0x000087E1 File Offset: 0x000069E1
		public void ClearKeyNameMappings()
		{
			this._keyNameMapping.Clear();
		}

		/// <summary>Encrypts the outer XML of an element using the specified X.509 certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> element that represents the encrypted XML data.</returns>
		/// <param name="inputElement">The XML element to encrypt.</param>
		/// <param name="certificate">The X.509 certificate to use for encryption.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="certificate" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The value of the <paramref name="certificate" /> parameter does not represent an RSA key algorithm.</exception>
		// Token: 0x06000230 RID: 560 RVA: 0x000087F0 File Offset: 0x000069F0
		public EncryptedData Encrypt(XmlElement inputElement, X509Certificate2 certificate)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			EncryptedData encryptedData2;
			using (RSA rsapublicKey = certificate.GetRSAPublicKey())
			{
				if (rsapublicKey == null)
				{
					throw new NotSupportedException("The certificate key algorithm is not supported.");
				}
				EncryptedData encryptedData = new EncryptedData();
				encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
				encryptedData.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
				EncryptedKey encryptedKey = new EncryptedKey();
				encryptedKey.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#rsa-1_5");
				encryptedKey.KeyInfo.AddClause(new KeyInfoX509Data(certificate));
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				encryptedKey.CipherData.CipherValue = EncryptedXml.EncryptKey(rijndaelManaged.Key, rsapublicKey, false);
				KeyInfoEncryptedKey keyInfoEncryptedKey = new KeyInfoEncryptedKey(encryptedKey);
				encryptedData.KeyInfo.AddClause(keyInfoEncryptedKey);
				encryptedData.CipherData.CipherValue = this.EncryptData(inputElement, rijndaelManaged, false);
				encryptedData2 = encryptedData;
			}
			return encryptedData2;
		}

		/// <summary>Encrypts the outer XML of an element using the specified key in the key mapping table.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that represents the encrypted XML data.</returns>
		/// <param name="inputElement">The XML element to encrypt.</param>
		/// <param name="keyName">A key name that can be found in the key mapping table.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="keyName" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="keyName" /> parameter does not match a registered key name pair.-or-The cryptographic key described by the <paramref name="keyName" /> parameter is not supported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000231 RID: 561 RVA: 0x000088D8 File Offset: 0x00006AD8
		public EncryptedData Encrypt(XmlElement inputElement, string keyName)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			object obj = null;
			if (this._keyNameMapping != null)
			{
				obj = this._keyNameMapping[keyName];
			}
			if (obj == null)
			{
				throw new CryptographicException("Unable to retrieve the encryption key.");
			}
			SymmetricAlgorithm symmetricAlgorithm = obj as SymmetricAlgorithm;
			RSA rsa = obj as RSA;
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
			encryptedData.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
			string text = null;
			if (symmetricAlgorithm == null)
			{
				text = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
			}
			else if (symmetricAlgorithm is TripleDES)
			{
				text = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";
			}
			else
			{
				if (!(symmetricAlgorithm is Rijndael) && !(symmetricAlgorithm is Aes))
				{
					throw new CryptographicException("The specified cryptographic transform is not supported.");
				}
				int keySize = symmetricAlgorithm.KeySize;
				if (keySize != 128)
				{
					if (keySize != 192)
					{
						if (keySize == 256)
						{
							text = "http://www.w3.org/2001/04/xmlenc#kw-aes256";
						}
					}
					else
					{
						text = "http://www.w3.org/2001/04/xmlenc#kw-aes192";
					}
				}
				else
				{
					text = "http://www.w3.org/2001/04/xmlenc#kw-aes128";
				}
			}
			EncryptedKey encryptedKey = new EncryptedKey();
			encryptedKey.EncryptionMethod = new EncryptionMethod(text);
			encryptedKey.KeyInfo.AddClause(new KeyInfoName(keyName));
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			encryptedKey.CipherData.CipherValue = ((symmetricAlgorithm == null) ? EncryptedXml.EncryptKey(rijndaelManaged.Key, rsa, false) : EncryptedXml.EncryptKey(rijndaelManaged.Key, symmetricAlgorithm));
			KeyInfoEncryptedKey keyInfoEncryptedKey = new KeyInfoEncryptedKey(encryptedKey);
			encryptedData.KeyInfo.AddClause(keyInfoEncryptedKey);
			encryptedData.CipherData.CipherValue = this.EncryptData(inputElement, rijndaelManaged, false);
			return encryptedData;
		}

		/// <summary>Decrypts all &lt;EncryptedData&gt; elements of the XML document that were specified during initialization of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic key used to decrypt the document was not found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000232 RID: 562 RVA: 0x00008A50 File Offset: 0x00006C50
		public void DecryptDocument()
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this._document.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNodeList xmlNodeList = this._document.SelectNodes("//enc:EncryptedData", xmlNamespaceManager);
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					XmlElement xmlElement = ((XmlNode)obj) as XmlElement;
					EncryptedData encryptedData = new EncryptedData();
					encryptedData.LoadXml(xmlElement);
					SymmetricAlgorithm decryptionKey = this.GetDecryptionKey(encryptedData, null);
					if (decryptionKey == null)
					{
						throw new CryptographicException("Unable to retrieve the decryption key.");
					}
					byte[] array = this.DecryptData(encryptedData, decryptionKey);
					this.ReplaceData(xmlElement, array);
				}
			}
		}

		/// <summary>Encrypts data in the specified byte array using the specified symmetric algorithm.</summary>
		/// <returns>A byte array of encrypted data.</returns>
		/// <param name="plaintext">The data to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric algorithm to use for encryption.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="plaintext" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could not be encrypted using the specified parameters.</exception>
		// Token: 0x06000233 RID: 563 RVA: 0x00008B1C File Offset: 0x00006D1C
		public byte[] EncryptData(byte[] plaintext, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (plaintext == null)
			{
				throw new ArgumentNullException("plaintext");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			CipherMode mode = symmetricAlgorithm.Mode;
			PaddingMode padding = symmetricAlgorithm.Padding;
			byte[] array = null;
			try
			{
				symmetricAlgorithm.Mode = this._mode;
				symmetricAlgorithm.Padding = this._padding;
				array = symmetricAlgorithm.CreateEncryptor().TransformFinalBlock(plaintext, 0, plaintext.Length);
			}
			finally
			{
				symmetricAlgorithm.Mode = mode;
				symmetricAlgorithm.Padding = padding;
			}
			byte[] array2;
			if (this._mode == CipherMode.ECB)
			{
				array2 = array;
			}
			else
			{
				byte[] iv = symmetricAlgorithm.IV;
				array2 = new byte[array.Length + iv.Length];
				Buffer.BlockCopy(iv, 0, array2, 0, iv.Length);
				Buffer.BlockCopy(array, 0, array2, iv.Length, array.Length);
			}
			return array2;
		}

		/// <summary>Encrypts the specified element or its contents using the specified symmetric algorithm.</summary>
		/// <returns>A byte array that contains the encrypted data.</returns>
		/// <param name="inputElement">The element or its contents to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric algorithm to use for encryption.</param>
		/// <param name="content">true to encrypt only the contents of the element; false to encrypt the entire element.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		// Token: 0x06000234 RID: 564 RVA: 0x00008BE4 File Offset: 0x00006DE4
		public byte[] EncryptData(XmlElement inputElement, SymmetricAlgorithm symmetricAlgorithm, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			byte[] array = (content ? this._encoding.GetBytes(inputElement.InnerXml) : this._encoding.GetBytes(inputElement.OuterXml));
			return this.EncryptData(array, symmetricAlgorithm);
		}

		/// <summary>Decrypts an &lt;EncryptedData&gt; element using the specified symmetric algorithm.</summary>
		/// <returns>A byte array that contains the raw decrypted plain text.</returns>
		/// <param name="encryptedData">The data to decrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to decrypt <paramref name="encryptedData" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000235 RID: 565 RVA: 0x00008C40 File Offset: 0x00006E40
		public byte[] DecryptData(EncryptedData encryptedData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			byte[] cipherValue = this.GetCipherValue(encryptedData.CipherData);
			CipherMode mode = symmetricAlgorithm.Mode;
			PaddingMode padding = symmetricAlgorithm.Padding;
			byte[] iv = symmetricAlgorithm.IV;
			byte[] array = null;
			if (this._mode != CipherMode.ECB)
			{
				array = this.GetDecryptionIV(encryptedData, null);
			}
			byte[] array2 = null;
			try
			{
				int num = 0;
				if (array != null)
				{
					symmetricAlgorithm.IV = array;
					num = array.Length;
				}
				symmetricAlgorithm.Mode = this._mode;
				symmetricAlgorithm.Padding = this._padding;
				array2 = symmetricAlgorithm.CreateDecryptor().TransformFinalBlock(cipherValue, num, cipherValue.Length - num);
			}
			finally
			{
				symmetricAlgorithm.Mode = mode;
				symmetricAlgorithm.Padding = padding;
				symmetricAlgorithm.IV = iv;
			}
			return array2;
		}

		/// <summary>Replaces an &lt;EncryptedData&gt; element with a specified decrypted sequence of bytes.</summary>
		/// <param name="inputElement">The &lt;EncryptedData&gt; element to replace.</param>
		/// <param name="decryptedData">The decrypted data to replace <paramref name="inputElement" /> with.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="decryptedData" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000236 RID: 566 RVA: 0x00008D14 File Offset: 0x00006F14
		public void ReplaceData(XmlElement inputElement, byte[] decryptedData)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (decryptedData == null)
			{
				throw new ArgumentNullException("decryptedData");
			}
			XmlNode parentNode = inputElement.ParentNode;
			if (parentNode.NodeType == XmlNodeType.Document)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				using (StringReader stringReader = new StringReader(this._encoding.GetString(decryptedData)))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader, Utils.GetSecureXmlReaderSettings(this._xmlResolver)))
					{
						xmlDocument.Load(xmlReader);
					}
				}
				XmlNode xmlNode = inputElement.OwnerDocument.ImportNode(xmlDocument.DocumentElement, true);
				parentNode.RemoveChild(inputElement);
				parentNode.AppendChild(xmlNode);
				return;
			}
			XmlNode xmlNode2 = parentNode.OwnerDocument.CreateElement(parentNode.Prefix, parentNode.LocalName, parentNode.NamespaceURI);
			try
			{
				parentNode.AppendChild(xmlNode2);
				xmlNode2.InnerXml = this._encoding.GetString(decryptedData);
				XmlNode xmlNode3 = xmlNode2.FirstChild;
				XmlNode nextSibling = inputElement.NextSibling;
				while (xmlNode3 != null)
				{
					XmlNode nextSibling2 = xmlNode3.NextSibling;
					parentNode.InsertBefore(xmlNode3, nextSibling);
					xmlNode3 = nextSibling2;
				}
			}
			finally
			{
				parentNode.RemoveChild(xmlNode2);
			}
			parentNode.RemoveChild(inputElement);
		}

		/// <summary>Replaces the specified element with the specified <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <param name="inputElement">The element to replace with an &lt;EncryptedData&gt; element.</param>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object to replace the <paramref name="inputElement" /> parameter with.</param>
		/// <param name="content">true to replace only the contents of the element; false to replace the entire element.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="encryptedData" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000237 RID: 567 RVA: 0x00008E68 File Offset: 0x00007068
		public static void ReplaceElement(XmlElement inputElement, EncryptedData encryptedData, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			XmlElement xml = encryptedData.GetXml(inputElement.OwnerDocument);
			if (content)
			{
				if (content)
				{
					Utils.RemoveAllChildren(inputElement);
					inputElement.AppendChild(xml);
					return;
				}
			}
			else
			{
				inputElement.ParentNode.ReplaceChild(xml, inputElement);
			}
		}

		/// <summary>Encrypts a key using a symmetric algorithm that a recipient uses to decrypt an &lt;EncryptedData&gt; element.</summary>
		/// <returns>A byte array that represents the encrypted value of the <paramref name="keyData" /> parameter.</returns>
		/// <param name="keyData">The key to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to encrypt <paramref name="keyData" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="symmetricAlgorithm" /> parameter is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael). </exception>
		// Token: 0x06000238 RID: 568 RVA: 0x00008EC4 File Offset: 0x000070C4
		public static byte[] EncryptKey(byte[] keyData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			if (symmetricAlgorithm is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapEncrypt(symmetricAlgorithm.Key, keyData);
			}
			if (symmetricAlgorithm is Rijndael || symmetricAlgorithm is Aes)
			{
				return SymmetricKeyWrap.AESKeyWrapEncrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException("The specified cryptographic transform is not supported.");
		}

		/// <summary>Encrypts the key that a recipient uses to decrypt an &lt;EncryptedData&gt; element.</summary>
		/// <returns>A byte array that represents the encrypted value of the <paramref name="keyData" /> parameter.</returns>
		/// <param name="keyData">The key to encrypt.</param>
		/// <param name="rsa">The asymmetric key used to encrypt <paramref name="keyData" />.</param>
		/// <param name="useOAEP">A value that specifies whether to use Optimal Asymmetric Encryption Padding (OAEP).</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is null.-or-The value of the <paramref name="rsa" /> parameter is null.</exception>
		// Token: 0x06000239 RID: 569 RVA: 0x00008F29 File Offset: 0x00007129
		public static byte[] EncryptKey(byte[] keyData, RSA rsa, bool useOAEP)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (useOAEP)
			{
				return new RSAOAEPKeyExchangeFormatter(rsa).CreateKeyExchange(keyData);
			}
			return new RSAPKCS1KeyExchangeFormatter(rsa).CreateKeyExchange(keyData);
		}

		/// <summary>Decrypts an &lt;EncryptedKey&gt; element using a symmetric algorithm.</summary>
		/// <returns>A byte array that contains the plain text key.</returns>
		/// <param name="keyData">An array of bytes that represents an encrypted &lt;EncryptedKey&gt; element.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to decrypt <paramref name="keyData" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="symmetricAlgorithm" /> element is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael). </exception>
		// Token: 0x0600023A RID: 570 RVA: 0x00008F64 File Offset: 0x00007164
		public static byte[] DecryptKey(byte[] keyData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			if (symmetricAlgorithm is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapDecrypt(symmetricAlgorithm.Key, keyData);
			}
			if (symmetricAlgorithm is Rijndael || symmetricAlgorithm is Aes)
			{
				return SymmetricKeyWrap.AESKeyWrapDecrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException("The specified cryptographic transform is not supported.");
		}

		/// <summary>Decrypts an &lt;EncryptedKey&gt; element using an asymmetric algorithm.</summary>
		/// <returns>A byte array that contains the plain text key.</returns>
		/// <param name="keyData">An array of bytes that represents an encrypted &lt;EncryptedKey&gt; element.</param>
		/// <param name="rsa">The asymmetric key used to decrypt <paramref name="keyData" />.</param>
		/// <param name="useOAEP">A value that specifies whether to use Optimal Asymmetric Encryption Padding (OAEP).</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is null.-or-The value of the <paramref name="rsa" /> parameter is null.</exception>
		// Token: 0x0600023B RID: 571 RVA: 0x00008FC9 File Offset: 0x000071C9
		public static byte[] DecryptKey(byte[] keyData, RSA rsa, bool useOAEP)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (useOAEP)
			{
				return new RSAOAEPKeyExchangeDeformatter(rsa).DecryptKeyExchange(keyData);
			}
			return new RSAPKCS1KeyExchangeDeformatter(rsa).DecryptKeyExchange(keyData);
		}

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for XML encryption syntax and processing. This field is constant.</summary>
		// Token: 0x04000145 RID: 325
		public const string XmlEncNamespaceUrl = "http://www.w3.org/2001/04/xmlenc#";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for an XML encryption element. This field is constant.</summary>
		// Token: 0x04000146 RID: 326
		public const string XmlEncElementUrl = "http://www.w3.org/2001/04/xmlenc#Element";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for XML encryption element content. This field is constant.</summary>
		// Token: 0x04000147 RID: 327
		public const string XmlEncElementContentUrl = "http://www.w3.org/2001/04/xmlenc#Content";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the XML encryption &lt;EncryptedKey&gt; element. This field is constant.</summary>
		// Token: 0x04000148 RID: 328
		public const string XmlEncEncryptedKeyUrl = "http://www.w3.org/2001/04/xmlenc#EncryptedKey";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the Digital Encryption Standard (DES) algorithm. This field is constant.</summary>
		// Token: 0x04000149 RID: 329
		public const string XmlEncDESUrl = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the Triple DES algorithm. This field is constant.</summary>
		// Token: 0x0400014A RID: 330
		public const string XmlEncTripleDESUrl = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 128-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		// Token: 0x0400014B RID: 331
		public const string XmlEncAES128Url = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 256-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		// Token: 0x0400014C RID: 332
		public const string XmlEncAES256Url = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 192-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		// Token: 0x0400014D RID: 333
		public const string XmlEncAES192Url = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the RSA Public Key Cryptography Standard (PKCS) Version 1.5 algorithm. This field is constant.</summary>
		// Token: 0x0400014E RID: 334
		public const string XmlEncRSA15Url = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the RSA Optimal Asymmetric Encryption Padding (OAEP) encryption algorithm. This field is constant.</summary>
		// Token: 0x0400014F RID: 335
		public const string XmlEncRSAOAEPUrl = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the TRIPLEDES key wrap algorithm. This field is constant.</summary>
		// Token: 0x04000150 RID: 336
		public const string XmlEncTripleDESKeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 128-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant. </summary>
		// Token: 0x04000151 RID: 337
		public const string XmlEncAES128KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 256-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		// Token: 0x04000152 RID: 338
		public const string XmlEncAES256KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 192-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		// Token: 0x04000153 RID: 339
		public const string XmlEncAES192KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the SHA-256 algorithm. This field is constant.</summary>
		// Token: 0x04000154 RID: 340
		public const string XmlEncSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the SHA-512 algorithm. This field is constant.</summary>
		// Token: 0x04000155 RID: 341
		public const string XmlEncSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x04000156 RID: 342
		private XmlDocument _document;

		// Token: 0x04000157 RID: 343
		private Evidence _evidence;

		// Token: 0x04000158 RID: 344
		private XmlResolver _xmlResolver;

		// Token: 0x04000159 RID: 345
		private const int _capacity = 4;

		// Token: 0x0400015A RID: 346
		private Hashtable _keyNameMapping;

		// Token: 0x0400015B RID: 347
		private PaddingMode _padding;

		// Token: 0x0400015C RID: 348
		private CipherMode _mode;

		// Token: 0x0400015D RID: 349
		private Encoding _encoding;

		// Token: 0x0400015E RID: 350
		private string _recipient;

		// Token: 0x0400015F RID: 351
		private int _xmlDsigSearchDepthCounter;

		// Token: 0x04000160 RID: 352
		private int _xmlDsigSearchDepth;
	}
}
