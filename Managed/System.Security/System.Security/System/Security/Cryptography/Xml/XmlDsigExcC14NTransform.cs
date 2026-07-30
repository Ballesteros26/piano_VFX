using System;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the exclusive C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), without comments.</summary>
	// Token: 0x02000080 RID: 128
	public class XmlDsigExcC14NTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class. </summary>
		// Token: 0x060003A4 RID: 932 RVA: 0x0000ED6A File Offset: 0x0000CF6A
		public XmlDsigExcC14NTransform()
			: this(false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class specifying a value that determines whether to include comments. </summary>
		/// <param name="includeComments">true to include comments; otherwise, false.</param>
		// Token: 0x060003A5 RID: 933 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public XmlDsigExcC14NTransform(bool includeComments)
			: this(includeComments, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class specifying a list of namespace prefixes to canonicalize using the standard canonicalization algorithm. </summary>
		/// <param name="inclusiveNamespacesPrefixList">The namespace prefixes to canonicalize using the standard canonicalization algorithm.</param>
		// Token: 0x060003A6 RID: 934 RVA: 0x0000ED7E File Offset: 0x0000CF7E
		public XmlDsigExcC14NTransform(string inclusiveNamespacesPrefixList)
			: this(false, inclusiveNamespacesPrefixList)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class specifying whether to include comments, and specifying a list of namespace prefixes. </summary>
		/// <param name="includeComments">true to include comments; otherwise, false.</param>
		/// <param name="inclusiveNamespacesPrefixList">The namespace prefixes to canonicalize using the standard canonicalization algorithm.</param>
		// Token: 0x060003A7 RID: 935 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public XmlDsigExcC14NTransform(bool includeComments, string inclusiveNamespacesPrefixList)
		{
			this._includeComments = includeComments;
			this._inclusiveNamespacesPrefixList = inclusiveNamespacesPrefixList;
			base.Algorithm = (includeComments ? "http://www.w3.org/2001/10/xml-exc-c14n#WithComments" : "http://www.w3.org/2001/10/xml-exc-c14n#");
		}

		/// <summary>Gets or sets a string that contains namespace prefixes to canonicalize using the standard canonicalization algorithm. </summary>
		/// <returns>A string that contains namespace prefixes to canonicalize using the standard canonicalization algorithm.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000EE0A File Offset: 0x0000D00A
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000EE12 File Offset: 0x0000D012
		public string InclusiveNamespacesPrefixList
		{
			get
			{
				return this._inclusiveNamespacesPrefixList;
			}
			set
			{
				this._inclusiveNamespacesPrefixList = value;
			}
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000EE1B File Offset: 0x0000D01B
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object; the <see cref="Overload:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object return only objects of one of these types.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000EE23 File Offset: 0x0000D023
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</param>
		// Token: 0x060003AC RID: 940 RVA: 0x0000EE2C File Offset: 0x0000D02C
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList != null)
			{
				foreach (object obj in nodeList)
				{
					XmlElement xmlElement = ((XmlNode)obj) as XmlElement;
					if (xmlElement != null && xmlElement.LocalName.Equals("InclusiveNamespaces") && xmlElement.NamespaceURI.Equals("http://www.w3.org/2001/10/xml-exc-c14n#") && Utils.HasAttribute(xmlElement, "PrefixList", "http://www.w3.org/2000/09/xmldsig#"))
					{
						this.InclusiveNamespacesPrefixList = Utils.GetAttribute(xmlElement, "PrefixList", "http://www.w3.org/2000/09/xmldsig#");
						break;
					}
				}
			}
		}

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is not a <see cref="T:System.IO.Stream" /> object.-or-The <paramref name="obj" /> parameter is not an <see cref="T:System.Xml.XmlDocument" /> object.-or-The <paramref name="obj" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003AD RID: 941 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		public override void LoadInput(object obj)
		{
			XmlResolver xmlResolver = (base.ResolverSet ? this._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			if (obj is Stream)
			{
				this._excCanonicalXml = new ExcCanonicalXml((Stream)obj, this._includeComments, this._inclusiveNamespacesPrefixList, xmlResolver, base.BaseURI);
				return;
			}
			if (obj is XmlDocument)
			{
				this._excCanonicalXml = new ExcCanonicalXml((XmlDocument)obj, this._includeComments, this._inclusiveNamespacesPrefixList, xmlResolver);
				return;
			}
			if (obj is XmlNodeList)
			{
				this._excCanonicalXml = new ExcCanonicalXml((XmlNodeList)obj, this._includeComments, this._inclusiveNamespacesPrefixList, xmlResolver);
				return;
			}
			throw new ArgumentException("Type of input object is invalid.", "obj");
		}

		/// <summary>Returns an XML representation of the parameters of a <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x060003AE RID: 942 RVA: 0x0000EF90 File Offset: 0x0000D190
		protected override XmlNodeList GetInnerXml()
		{
			if (this.InclusiveNamespacesPrefixList == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Transform", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(base.Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", base.Algorithm);
			}
			XmlElement xmlElement2 = xmlDocument.CreateElement("InclusiveNamespaces", "http://www.w3.org/2001/10/xml-exc-c14n#");
			xmlElement2.SetAttribute("PrefixList", this.InclusiveNamespacesPrefixList);
			xmlElement.AppendChild(xmlElement2);
			return xmlElement.ChildNodes;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</returns>
		// Token: 0x060003AF RID: 943 RVA: 0x0000F00A File Offset: 0x0000D20A
		public override object GetOutput()
		{
			return new MemoryStream(this._excCanonicalXml.GetBytes());
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object as an object of the specified type.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object as an object of the specified type.</returns>
		/// <param name="type">The type of the output to return. This must be one of the types in the <see cref="P:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.OutputTypes" /> property.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.-or-The <paramref name="type" /> parameter does not derive from a <see cref="T:System.IO.Stream" /> object.</exception>
		// Token: 0x060003B0 RID: 944 RVA: 0x0000F01C File Offset: 0x0000D21C
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return new MemoryStream(this._excCanonicalXml.GetBytes());
		}

		/// <summary>Returns the digest associated with a <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <returns>The digest associated with a <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</returns>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		// Token: 0x060003B1 RID: 945 RVA: 0x0000F06D File Offset: 0x0000D26D
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return this._excCanonicalXml.GetDigestedBytes(hash);
		}

		// Token: 0x040001CA RID: 458
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		// Token: 0x040001CB RID: 459
		private Type[] _outputTypes = new Type[] { typeof(Stream) };

		// Token: 0x040001CC RID: 460
		private bool _includeComments;

		// Token: 0x040001CD RID: 461
		private string _inclusiveNamespacesPrefixList;

		// Token: 0x040001CE RID: 462
		private ExcCanonicalXml _excCanonicalXml;
	}
}
