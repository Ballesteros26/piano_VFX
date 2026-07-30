using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;reference&gt; element of an XML signature.</summary>
	// Token: 0x02000072 RID: 114
	public class Reference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with default properties.</summary>
		// Token: 0x060002C9 RID: 713 RVA: 0x0000A2EB File Offset: 0x000084EB
		public Reference()
		{
			this._transformChain = new TransformChain();
			this._refTarget = null;
			this._refTargetType = ReferenceTargetType.UriReference;
			this._cachedXml = null;
			this._digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with a hash value of the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> with which to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.Reference" />. </param>
		// Token: 0x060002CA RID: 714 RVA: 0x0000A31E File Offset: 0x0000851E
		public Reference(Stream stream)
		{
			this._transformChain = new TransformChain();
			this._refTarget = stream;
			this._refTargetType = ReferenceTargetType.Stream;
			this._cachedXml = null;
			this._digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with the specified <see cref="T:System.Uri" />.</summary>
		/// <param name="uri">The <see cref="T:System.Uri" /> with which to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.Reference" />. </param>
		// Token: 0x060002CB RID: 715 RVA: 0x0000A351 File Offset: 0x00008551
		public Reference(string uri)
		{
			this._transformChain = new TransformChain();
			this._refTarget = uri;
			this._uri = uri;
			this._refTargetType = ReferenceTargetType.UriReference;
			this._cachedXml = null;
			this._digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A38B File Offset: 0x0000858B
		internal Reference(XmlElement element)
		{
			this._transformChain = new TransformChain();
			this._refTarget = element;
			this._refTargetType = ReferenceTargetType.XmlElement;
			this._cachedXml = null;
			this._digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />. The default is null.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A3BE File Offset: 0x000085BE
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000A3C6 File Offset: 0x000085C6
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

		/// <summary>Gets or sets the <see cref="T:System.Uri" /> of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The <see cref="T:System.Uri" /> of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000A3CF File Offset: 0x000085CF
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000A3D7 File Offset: 0x000085D7
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the type of the object being signed.</summary>
		/// <returns>The type of the object being signed.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000A3E7 File Offset: 0x000085E7
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000A3EF File Offset: 0x000085EF
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the digest method Uniform Resource Identifier (URI) of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The digest method URI of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />. The default value is "http://www.w3.org/2000/09/xmldsig#sha1".</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000A3FF File Offset: 0x000085FF
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000A407 File Offset: 0x00008607
		public string DigestMethod
		{
			get
			{
				return this._digestMethod;
			}
			set
			{
				this._digestMethod = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the digest value of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The digest value of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000A417 File Offset: 0x00008617
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000A41F File Offset: 0x0000861F
		public byte[] DigestValue
		{
			get
			{
				return this._digestValue;
			}
			set
			{
				this._digestValue = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets the transform chain of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The transform chain of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000A42F File Offset: 0x0000862F
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000A44A File Offset: 0x0000864A
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000A45A File Offset: 0x0000865A
		internal bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000A465 File Offset: 0x00008665
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000A46D File Offset: 0x0000866D
		internal SignedXml SignedXml
		{
			get
			{
				return this._signedXml;
			}
			set
			{
				this._signedXml = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000A476 File Offset: 0x00008676
		internal ReferenceTargetType ReferenceTargetType
		{
			get
			{
				return this._refTargetType;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.Reference.DigestMethod" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.Reference.DigestValue" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002DD RID: 733 RVA: 0x0000A480 File Offset: 0x00008680
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

		// Token: 0x060002DE RID: 734 RVA: 0x0000A4B0 File Offset: 0x000086B0
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Reference", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._id))
			{
				xmlElement.SetAttribute("Id", this._id);
			}
			if (this._uri != null)
			{
				xmlElement.SetAttribute("URI", this._uri);
			}
			if (!string.IsNullOrEmpty(this._type))
			{
				xmlElement.SetAttribute("Type", this._type);
			}
			if (this.TransformChain.Count != 0)
			{
				xmlElement.AppendChild(this.TransformChain.GetXml(document, "http://www.w3.org/2000/09/xmldsig#"));
			}
			if (string.IsNullOrEmpty(this._digestMethod))
			{
				throw new CryptographicException("A DigestMethod must be specified on a Reference prior to generating XML.");
			}
			XmlElement xmlElement2 = document.CreateElement("DigestMethod", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.SetAttribute("Algorithm", this._digestMethod);
			xmlElement.AppendChild(xmlElement2);
			if (this.DigestValue == null)
			{
				if (this._hashAlgorithm.Hash == null)
				{
					throw new CryptographicException("A Reference must contain a DigestValue.");
				}
				this.DigestValue = this._hashAlgorithm.Hash;
			}
			XmlElement xmlElement3 = document.CreateElement("DigestValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(document.CreateTextNode(Convert.ToBase64String(this._digestValue)));
			xmlElement.AppendChild(xmlElement3);
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.Reference" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.Reference" /> state. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain any transforms.-or- The <paramref name="value" /> parameter contains an unknown transform. </exception>
		// Token: 0x060002DF RID: 735 RVA: 0x0000A5F0 File Offset: 0x000087F0
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			this._uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2000/09/xmldsig#");
			this._type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2000/09/xmldsig#");
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			this.TransformChain = new TransformChain();
			XmlElement xmlElement = value.SelectSingleNode("ds:Transforms", xmlNamespaceManager) as XmlElement;
			if (xmlElement != null)
			{
				XmlNodeList xmlNodeList = xmlElement.SelectNodes("ds:Transform", xmlNamespaceManager);
				if (xmlNodeList != null)
				{
					foreach (object obj in xmlNodeList)
					{
						XmlElement xmlElement2 = ((XmlNode)obj) as XmlElement;
						Transform transform = CryptoHelpers.CreateFromName(Utils.GetAttribute(xmlElement2, "Algorithm", "http://www.w3.org/2000/09/xmldsig#")) as Transform;
						if (transform == null)
						{
							throw new CryptographicException("Unknown transform has been encountered.");
						}
						this.AddTransform(transform);
						transform.LoadInnerXml(xmlElement2.ChildNodes);
						if (transform is XmlDsigEnvelopedSignatureTransform)
						{
							XmlNode xmlNode = xmlElement2.SelectSingleNode("ancestor::ds:Signature[1]", xmlNamespaceManager);
							XmlNodeList xmlNodeList2 = xmlElement2.SelectNodes("//ds:Signature", xmlNamespaceManager);
							if (xmlNodeList2 != null)
							{
								int num = 0;
								foreach (object obj2 in xmlNodeList2)
								{
									XmlNode xmlNode2 = (XmlNode)obj2;
									num++;
									if (xmlNode2 == xmlNode)
									{
										((XmlDsigEnvelopedSignatureTransform)transform).SignaturePosition = num;
										break;
									}
								}
							}
						}
					}
				}
			}
			XmlElement xmlElement3 = value.SelectSingleNode("ds:DigestMethod", xmlNamespaceManager) as XmlElement;
			if (xmlElement3 == null)
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestMethod");
			}
			this._digestMethod = Utils.GetAttribute(xmlElement3, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement4 = value.SelectSingleNode("ds:DigestValue", xmlNamespaceManager) as XmlElement;
			if (xmlElement4 == null)
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestValue");
			}
			this._digestValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlElement4.InnerText));
			this._cachedXml = value;
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to the list of transforms to be performed on the data before passing it to the digest algorithm.</summary>
		/// <param name="transform">The transform to be added to the list of transforms. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="transform" /> parameter is null.</exception>
		// Token: 0x060002E0 RID: 736 RVA: 0x0000A844 File Offset: 0x00008A44
		public void AddTransform(Transform transform)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			transform.Reference = this;
			this.TransformChain.Add(transform);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A867 File Offset: 0x00008A67
		internal void UpdateHashValue(XmlDocument document, CanonicalXmlNodeList refList)
		{
			this.DigestValue = this.CalculateHashValue(document, refList);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A878 File Offset: 0x00008A78
		internal byte[] CalculateHashValue(XmlDocument document, CanonicalXmlNodeList refList)
		{
			this._hashAlgorithm = CryptoHelpers.CreateFromName(this._digestMethod) as HashAlgorithm;
			if (this._hashAlgorithm == null)
			{
				throw new CryptographicException("Could not create hash algorithm object.");
			}
			string text = ((document == null) ? (Environment.CurrentDirectory + "\\") : document.BaseURI);
			Stream stream = null;
			WebResponse webResponse = null;
			Stream stream2 = null;
			XmlResolver xmlResolver = null;
			byte[] array = null;
			try
			{
				switch (this._refTargetType)
				{
				case ReferenceTargetType.Stream:
					xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
					stream = this.TransformChain.TransformToOctetStream((Stream)this._refTarget, xmlResolver, text);
					break;
				case ReferenceTargetType.XmlElement:
					xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
					stream = this.TransformChain.TransformToOctetStream(Utils.PreProcessElementInput((XmlElement)this._refTarget, xmlResolver, text), xmlResolver, text);
					break;
				case ReferenceTargetType.UriReference:
					if (this._uri == null)
					{
						xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						stream = this.TransformChain.TransformToOctetStream(null, xmlResolver, text);
					}
					else if (this._uri.Length == 0)
					{
						if (document == null)
						{
							throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "An XmlDocument context is required to resolve the Reference Uri {0}.", this._uri));
						}
						xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						XmlDocument xmlDocument = Utils.DiscardComments(Utils.PreProcessDocumentInput(document, xmlResolver, text));
						stream = this.TransformChain.TransformToOctetStream(xmlDocument, xmlResolver, text);
					}
					else
					{
						if (this._uri[0] != '#')
						{
							throw new CryptographicException("Unable to resolve Uri {0}.", this._uri);
						}
						bool flag = true;
						string idFromLocalUri = Utils.GetIdFromLocalUri(this._uri, out flag);
						if (idFromLocalUri == "xpointer(/)")
						{
							if (document == null)
							{
								throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "An XmlDocument context is required to resolve the Reference Uri {0}.", this._uri));
							}
							xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
							stream = this.TransformChain.TransformToOctetStream(Utils.PreProcessDocumentInput(document, xmlResolver, text), xmlResolver, text);
						}
						else
						{
							XmlElement xmlElement = this.SignedXml.GetIdElement(document, idFromLocalUri);
							if (xmlElement != null)
							{
								this._namespaces = Utils.GetPropagatedAttributes(xmlElement.ParentNode as XmlElement);
							}
							if (xmlElement == null && refList != null)
							{
								foreach (object obj in refList)
								{
									XmlElement xmlElement2 = ((XmlNode)obj) as XmlElement;
									if (xmlElement2 != null && Utils.HasAttribute(xmlElement2, "Id", "http://www.w3.org/2000/09/xmldsig#") && Utils.GetAttribute(xmlElement2, "Id", "http://www.w3.org/2000/09/xmldsig#").Equals(idFromLocalUri))
									{
										xmlElement = xmlElement2;
										if (this._signedXml._context != null)
										{
											this._namespaces = Utils.GetPropagatedAttributes(this._signedXml._context);
											break;
										}
										break;
									}
								}
							}
							if (xmlElement == null)
							{
								throw new CryptographicException("Malformed reference element.");
							}
							XmlDocument xmlDocument2 = Utils.PreProcessElementInput(xmlElement, xmlResolver, text);
							Utils.AddNamespaces(xmlDocument2.DocumentElement, this._namespaces);
							xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
							if (flag)
							{
								XmlDocument xmlDocument3 = Utils.DiscardComments(xmlDocument2);
								stream = this.TransformChain.TransformToOctetStream(xmlDocument3, xmlResolver, text);
							}
							else
							{
								stream = this.TransformChain.TransformToOctetStream(xmlDocument2, xmlResolver, text);
							}
						}
					}
					break;
				default:
					throw new CryptographicException("Unable to resolve Uri {0}.", this._uri);
				}
				stream = SignedXmlDebugLog.LogReferenceData(this, stream);
				array = this._hashAlgorithm.ComputeHash(stream);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
				if (webResponse != null)
				{
					webResponse.Close();
				}
				if (stream2 != null)
				{
					stream2.Close();
				}
			}
			return array;
		}

		// Token: 0x0400017A RID: 378
		internal const string DefaultDigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x0400017B RID: 379
		private string _id;

		// Token: 0x0400017C RID: 380
		private string _uri;

		// Token: 0x0400017D RID: 381
		private string _type;

		// Token: 0x0400017E RID: 382
		private TransformChain _transformChain;

		// Token: 0x0400017F RID: 383
		private string _digestMethod;

		// Token: 0x04000180 RID: 384
		private byte[] _digestValue;

		// Token: 0x04000181 RID: 385
		private HashAlgorithm _hashAlgorithm;

		// Token: 0x04000182 RID: 386
		private object _refTarget;

		// Token: 0x04000183 RID: 387
		private ReferenceTargetType _refTargetType;

		// Token: 0x04000184 RID: 388
		private XmlElement _cachedXml;

		// Token: 0x04000185 RID: 389
		private SignedXml _signedXml;

		// Token: 0x04000186 RID: 390
		internal CanonicalXmlNodeList _namespaces;
	}
}
