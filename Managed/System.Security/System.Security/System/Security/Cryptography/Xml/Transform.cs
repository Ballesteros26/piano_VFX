using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class from which all &lt;Transform&gt; elements that can be used in an XML digital signature derive.</summary>
	// Token: 0x02000078 RID: 120
	public abstract class Transform
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C18A File Offset: 0x0000A38A
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000C192 File Offset: 0x0000A392
		internal string BaseURI
		{
			get
			{
				return this._baseUri;
			}
			set
			{
				this._baseUri = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C19B File Offset: 0x0000A39B
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000C1A3 File Offset: 0x0000A3A3
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		internal Reference Reference
		{
			get
			{
				return this._reference;
			}
			set
			{
				this._reference = value;
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) that identifies the algorithm performed by the current transform.</summary>
		/// <returns>The URI that identifies the algorithm performed by the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000C1BD File Offset: 0x0000A3BD
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000C1C5 File Offset: 0x0000A3C5
		public string Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				this._algorithm = value;
			}
		}

		/// <summary>Sets the current <see cref="T:System.Xml.XmlResolver" /> object.</summary>
		/// <returns>The current <see cref="T:System.Xml.XmlResolver" /> object. This property defaults to an <see cref="T:System.Xml.XmlSecureResolver" /> object.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000C1DE File Offset: 0x0000A3DE
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000C1CE File Offset: 0x0000A3CE
		public XmlResolver Resolver
		{
			internal get
			{
				return this._xmlResolver;
			}
			set
			{
				this._xmlResolver = value;
				this._bResolverSet = true;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000C1E6 File Offset: 0x0000A3E6
		internal bool ResolverSet
		{
			get
			{
				return this._bResolverSet;
			}
		}

		/// <summary>When overridden in a derived class, gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000327 RID: 807
		public abstract Type[] InputTypes { get; }

		/// <summary>When overridden in a derived class, gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000328 RID: 808
		public abstract Type[] OutputTypes { get; }

		// Token: 0x06000329 RID: 809 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
		internal bool AcceptsType(Type inputType)
		{
			if (this.InputTypes != null)
			{
				for (int i = 0; i < this.InputTypes.Length; i++)
				{
					if (inputType == this.InputTypes[i] || inputType.IsSubclassOf(this.InputTypes[i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Returns the XML representation of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>The XML representation of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600032A RID: 810 RVA: 0x0000C23C File Offset: 0x0000A43C
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C25D File Offset: 0x0000A45D
		internal XmlElement GetXml(XmlDocument document)
		{
			return this.GetXml(document, "Transform");
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C26C File Offset: 0x0000A46C
		internal XmlElement GetXml(XmlDocument document, string name)
		{
			XmlElement xmlElement = document.CreateElement(name, "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this.Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", this.Algorithm);
			}
			XmlNodeList innerXml = this.GetInnerXml();
			if (innerXml != null)
			{
				foreach (object obj in innerXml)
				{
					XmlNode xmlNode = (XmlNode)obj;
					xmlElement.AppendChild(document.ImportNode(xmlNode, true));
				}
			}
			return xmlElement;
		}

		/// <summary>When overridden in a derived class, parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object. </param>
		// Token: 0x0600032D RID: 813
		public abstract void LoadInnerXml(XmlNodeList nodeList);

		/// <summary>When overridden in a derived class, returns an XML representation of the parameters of the <see cref="T:System.Security.Cryptography.Xml.Transform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x0600032E RID: 814
		protected abstract XmlNodeList GetInnerXml();

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object. </param>
		// Token: 0x0600032F RID: 815
		public abstract void LoadInput(object obj);

		/// <summary>When overridden in a derived class, returns the output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		// Token: 0x06000330 RID: 816
		public abstract object GetOutput();

		/// <summary>When overridden in a derived class, returns the output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object of the specified type.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object as an object of the specified type.</returns>
		/// <param name="type">The type of the output to return. This must be one of the types in the <see cref="P:System.Security.Cryptography.Xml.Transform.OutputTypes" /> property. </param>
		// Token: 0x06000331 RID: 817
		public abstract object GetOutput(Type type);

		/// <summary>When overridden in a derived class, returns the digest associated with a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object. </summary>
		/// <returns>The digest associated with a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		// Token: 0x06000332 RID: 818 RVA: 0x0000C304 File Offset: 0x0000A504
		public virtual byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return hash.ComputeHash((Stream)this.GetOutput(typeof(Stream)));
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.XmlElement" /> object that represents the document context under which the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object is running. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the document context under which the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object is running.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000C324 File Offset: 0x0000A524
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000C364 File Offset: 0x0000A564
		public XmlElement Context
		{
			get
			{
				if (this._context != null)
				{
					return this._context;
				}
				Reference reference = this.Reference;
				SignedXml signedXml = ((reference == null) ? this.SignedXml : reference.SignedXml);
				if (signedXml == null)
				{
					return null;
				}
				return signedXml._context;
			}
			set
			{
				this._context = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Collections.Hashtable" /> object that contains the namespaces that are propagated into the signature. </summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> object that contains the namespaces that are propagated into the signature.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.Transform.PropagatedNamespaces" /> property was set to null.</exception>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000C370 File Offset: 0x0000A570
		public Hashtable PropagatedNamespaces
		{
			get
			{
				if (this._propagatedNamespaces != null)
				{
					return this._propagatedNamespaces;
				}
				Reference reference = this.Reference;
				SignedXml signedXml = ((reference == null) ? this.SignedXml : reference.SignedXml);
				if (reference != null && (reference.ReferenceTargetType != ReferenceTargetType.UriReference || string.IsNullOrEmpty(reference.Uri) || reference.Uri[0] != '#'))
				{
					this._propagatedNamespaces = new Hashtable(0);
					return this._propagatedNamespaces;
				}
				CanonicalXmlNodeList canonicalXmlNodeList = null;
				if (reference != null)
				{
					canonicalXmlNodeList = reference._namespaces;
				}
				else if (((signedXml != null) ? signedXml._context : null) != null)
				{
					canonicalXmlNodeList = Utils.GetPropagatedAttributes(signedXml._context);
				}
				if (canonicalXmlNodeList == null)
				{
					this._propagatedNamespaces = new Hashtable(0);
					return this._propagatedNamespaces;
				}
				this._propagatedNamespaces = new Hashtable(canonicalXmlNodeList.Count);
				foreach (object obj in canonicalXmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string text = ((xmlNode.Prefix.Length > 0) ? (xmlNode.Prefix + ":" + xmlNode.LocalName) : xmlNode.LocalName);
					if (!this._propagatedNamespaces.Contains(text))
					{
						this._propagatedNamespaces.Add(text, xmlNode.Value);
					}
				}
				return this._propagatedNamespaces;
			}
		}

		// Token: 0x040001A7 RID: 423
		private string _algorithm;

		// Token: 0x040001A8 RID: 424
		private string _baseUri;

		// Token: 0x040001A9 RID: 425
		internal XmlResolver _xmlResolver;

		// Token: 0x040001AA RID: 426
		private bool _bResolverSet;

		// Token: 0x040001AB RID: 427
		private SignedXml _signedXml;

		// Token: 0x040001AC RID: 428
		private Reference _reference;

		// Token: 0x040001AD RID: 429
		private Hashtable _propagatedNamespaces;

		// Token: 0x040001AE RID: 430
		private XmlElement _context;
	}
}
