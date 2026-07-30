using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the XPath transform for a digital signature as defined by the W3C.</summary>
	// Token: 0x02000082 RID: 130
	public class XmlDsigXPathTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> class.</summary>
		// Token: 0x060003B4 RID: 948 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		public XmlDsigXPathTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xpath-19991116";
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000F10E File Offset: 0x0000D30E
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object; the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object return only objects of one of these types.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000F116 File Offset: 0x0000D316
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="nodeList" /> parameter is null.-or- The <paramref name="nodeList" /> parameter does not contain an <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> element. </exception>
		// Token: 0x060003B7 RID: 951 RVA: 0x0000F120 File Offset: 0x0000D320
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
			foreach (object obj in nodeList)
			{
				XmlElement xmlElement = ((XmlNode)obj) as XmlElement;
				if (xmlElement != null && xmlElement.LocalName == "XPath")
				{
					this._xpathexpr = xmlElement.InnerXml.Trim(null);
					XmlNameTable nameTable = new XmlNodeReader(xmlElement).NameTable;
					this._nsm = new XmlNamespaceManager(nameTable);
					using (IEnumerator enumerator2 = xmlElement.Attributes.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlAttribute xmlAttribute = (XmlAttribute)obj2;
							if (xmlAttribute.Prefix == "xmlns")
							{
								string text = xmlAttribute.LocalName;
								string text2 = xmlAttribute.Value;
								if (text == null)
								{
									text = xmlElement.Prefix;
									text2 = xmlElement.NamespaceURI;
								}
								this._nsm.AddNamespace(text, text2);
							}
						}
						break;
					}
				}
			}
			if (this._xpathexpr == null)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
		}

		/// <summary>Returns an XML representation of the parameters of a <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x060003B8 RID: 952 RVA: 0x0000F274 File Offset: 0x0000D474
		protected override XmlNodeList GetInnerXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement(null, "XPath", "http://www.w3.org/2000/09/xmldsig#");
			if (this._nsm != null)
			{
				foreach (object obj in this._nsm)
				{
					string text = (string)obj;
					if (!(text == "xml") && !(text == "xmlns") && text != null && text.Length > 0)
					{
						xmlElement.SetAttribute("xmlns:" + text, this._nsm.LookupNamespace(text));
					}
				}
			}
			xmlElement.InnerXml = this._xpathexpr;
			xmlDocument.AppendChild(xmlElement);
			return xmlDocument.ChildNodes;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003B9 RID: 953 RVA: 0x0000F348 File Offset: 0x0000D548
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
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000F388 File Offset: 0x0000D588
		private void LoadStreamInput(Stream stream)
		{
			XmlResolver xmlResolver = (base.ResolverSet ? this._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			XmlReader xmlReader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			this._document = new XmlDocument();
			this._document.PreserveWhitespace = true;
			this._document.Load(xmlReader);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			XmlResolver xmlResolver = (base.ResolverSet ? this._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			using (MemoryStream memoryStream = new MemoryStream(new CanonicalXml(nodeList, xmlResolver, true).GetBytes()))
			{
				this.LoadStreamInput(memoryStream);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000F44C File Offset: 0x0000D64C
		private void LoadXmlDocumentInput(XmlDocument doc)
		{
			this._document = doc;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003BD RID: 957 RVA: 0x0000F458 File Offset: 0x0000D658
		public override object GetOutput()
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			if (!string.IsNullOrEmpty(this._xpathexpr))
			{
				XPathNavigator xpathNavigator = this._document.CreateNavigator();
				XPathNodeIterator xpathNodeIterator = xpathNavigator.Select("//. | //@*");
				XPathExpression xpathExpression = xpathNavigator.Compile("boolean(" + this._xpathexpr + ")");
				xpathExpression.SetContext(this._nsm);
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
					XmlNode node = ((IHasXmlNode)xpathNavigator2).GetNode();
					if ((bool)xpathNodeIterator.Current.Evaluate(xpathExpression))
					{
						canonicalXmlNodeList.Add(node);
					}
				}
				xpathNodeIterator = xpathNavigator.Select("//namespace::*");
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator3 = xpathNodeIterator.Current;
					XmlNode node2 = ((IHasXmlNode)xpathNavigator3).GetNode();
					canonicalXmlNodeList.Add(node2);
				}
			}
			return canonicalXmlNodeList;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		// Token: 0x060003BE RID: 958 RVA: 0x0000F528 File Offset: 0x0000D728
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlNodeList) && !type.IsSubclassOf(typeof(XmlNodeList)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return (XmlNodeList)this.GetOutput();
		}

		// Token: 0x040001CF RID: 463
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x040001D0 RID: 464
		private Type[] _outputTypes = new Type[] { typeof(XmlNodeList) };

		// Token: 0x040001D1 RID: 465
		private string _xpathexpr;

		// Token: 0x040001D2 RID: 466
		private XmlDocument _document;

		// Token: 0x040001D3 RID: 467
		private XmlNamespaceManager _nsm;
	}
}
