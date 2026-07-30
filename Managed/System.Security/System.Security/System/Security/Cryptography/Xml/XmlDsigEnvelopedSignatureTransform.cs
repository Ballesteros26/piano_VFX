using System;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the enveloped signature transform for an XML digital signature as defined by the W3C.</summary>
	// Token: 0x0200007F RID: 127
	public class XmlDsigEnvelopedSignatureTransform : Transform
	{
		// Token: 0x170000CD RID: 205
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000E83B File Offset: 0x0000CA3B
		internal int SignaturePosition
		{
			set
			{
				this._signaturePosition = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> class.</summary>
		// Token: 0x06000398 RID: 920 RVA: 0x0000E844 File Offset: 0x0000CA44
		public XmlDsigEnvelopedSignatureTransform()
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> class with comments, if specified.</summary>
		/// <param name="includeComments">true to include comments; otherwise, false. </param>
		// Token: 0x06000399 RID: 921 RVA: 0x0000E8BC File Offset: 0x0000CABC
		public XmlDsigEnvelopedSignatureTransform(bool includeComments)
		{
			this._includeComments = includeComments;
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000E93A File Offset: 0x0000CB3A
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000E942 File Offset: 0x0000CB42
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object. </param>
		// Token: 0x0600039C RID: 924 RVA: 0x00004938 File Offset: 0x00002B38
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x0600039D RID: 925 RVA: 0x00003BEE File Offset: 0x00001DEE
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The containing XML document is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600039E RID: 926 RVA: 0x0000E94C File Offset: 0x0000CB4C
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				this.LoadStreamInput((Stream)obj);
				return;
			}
			if (obj is XmlNodeList)
			{
				this.LoadXmlNodeListInput((XmlNodeList)obj);
				return;
			}
			if (obj is XmlDocument)
			{
				this.LoadXmlDocumentInput((XmlDocument)obj);
				return;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000E998 File Offset: 0x0000CB98
		private void LoadStreamInput(Stream stream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			XmlResolver xmlResolver = (base.ResolverSet ? this._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			XmlReader xmlReader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			xmlDocument.Load(xmlReader);
			this._containingDocument = xmlDocument;
			if (this._containingDocument == null)
			{
				throw new CryptographicException("An XmlDocument context is required for enveloped transforms.");
			}
			this._nsm = new XmlNamespaceManager(this._containingDocument.NameTable);
			this._nsm.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000EA30 File Offset: 0x0000CC30
		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new ArgumentNullException("nodeList");
			}
			this._containingDocument = Utils.GetOwnerDocument(nodeList);
			if (this._containingDocument == null)
			{
				throw new CryptographicException("An XmlDocument context is required for enveloped transforms.");
			}
			this._nsm = new XmlNamespaceManager(this._containingDocument.NameTable);
			this._nsm.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			this._inputNodeList = nodeList;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		private void LoadXmlDocumentInput(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}
			this._containingDocument = doc;
			this._nsm = new XmlNamespaceManager(this._containingDocument.NameTable);
			this._nsm.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The containing XML document is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003A2 RID: 930 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		public override object GetOutput()
		{
			if (this._containingDocument == null)
			{
				throw new CryptographicException("An XmlDocument context is required for enveloped transforms.");
			}
			if (this._inputNodeList != null)
			{
				if (this._signaturePosition == 0)
				{
					return this._inputNodeList;
				}
				XmlNodeList xmlNodeList = this._containingDocument.SelectNodes("//dsig:Signature", this._nsm);
				if (xmlNodeList == null)
				{
					return this._inputNodeList;
				}
				CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
				foreach (object obj in this._inputNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode != null)
					{
						if (Utils.IsXmlNamespaceNode(xmlNode) || Utils.IsNamespaceNode(xmlNode))
						{
							canonicalXmlNodeList.Add(xmlNode);
						}
						else
						{
							try
							{
								XmlNode xmlNode2 = xmlNode.SelectSingleNode("ancestor-or-self::dsig:Signature[1]", this._nsm);
								int num = 0;
								foreach (object obj2 in xmlNodeList)
								{
									XmlNode xmlNode3 = (XmlNode)obj2;
									num++;
									if (xmlNode3 == xmlNode2)
									{
										break;
									}
								}
								if (xmlNode2 == null || (xmlNode2 != null && num != this._signaturePosition))
								{
									canonicalXmlNodeList.Add(xmlNode);
								}
							}
							catch
							{
							}
						}
					}
				}
				return canonicalXmlNodeList;
			}
			else
			{
				XmlNodeList xmlNodeList2 = this._containingDocument.SelectNodes("//dsig:Signature", this._nsm);
				if (xmlNodeList2 == null)
				{
					return this._containingDocument;
				}
				if (xmlNodeList2.Count < this._signaturePosition || this._signaturePosition <= 0)
				{
					return this._containingDocument;
				}
				xmlNodeList2[this._signaturePosition - 1].ParentNode.RemoveChild(xmlNodeList2[this._signaturePosition - 1]);
				return this._containingDocument;
			}
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		// Token: 0x060003A3 RID: 931 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		public override object GetOutput(Type type)
		{
			if (type == typeof(XmlNodeList) || type.IsSubclassOf(typeof(XmlNodeList)))
			{
				if (this._inputNodeList == null)
				{
					this._inputNodeList = Utils.AllDescendantNodes(this._containingDocument, true);
				}
				return (XmlNodeList)this.GetOutput();
			}
			if (!(type == typeof(XmlDocument)) && !type.IsSubclassOf(typeof(XmlDocument)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			if (this._inputNodeList != null)
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return (XmlDocument)this.GetOutput();
		}

		// Token: 0x040001C3 RID: 451
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x040001C4 RID: 452
		private Type[] _outputTypes = new Type[]
		{
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x040001C5 RID: 453
		private XmlNodeList _inputNodeList;

		// Token: 0x040001C6 RID: 454
		private bool _includeComments;

		// Token: 0x040001C7 RID: 455
		private XmlNamespaceManager _nsm;

		// Token: 0x040001C8 RID: 456
		private XmlDocument _containingDocument;

		// Token: 0x040001C9 RID: 457
		private int _signaturePosition;
	}
}
