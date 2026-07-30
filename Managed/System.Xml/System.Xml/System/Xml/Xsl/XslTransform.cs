using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl.XsltOld;
using System.Xml.Xsl.XsltOld.Debugger;

namespace System.Xml.Xsl
{
	/// <summary>Transforms XML data using an Extensible Stylesheet Language for Transformations (XSLT) style sheet.</summary>
	// Token: 0x020004D8 RID: 1240
	[Obsolete("This class has been deprecated. Please use System.Xml.Xsl.XslCompiledTransform instead. http://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class XslTransform
	{
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x00123F7B File Offset: 0x0012217B
		private XmlResolver _DocumentResolver
		{
			get
			{
				if (this.isDocumentResolverSet)
				{
					return this._documentResolver;
				}
				return XsltConfigSection.CreateDefaultResolver();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XslTransform" /> class.</summary>
		// Token: 0x0600328C RID: 12940 RVA: 0x000020FD File Offset: 0x000002FD
		public XslTransform()
		{
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to resolve external resources when the <see cref="Overload:System.Xml.Xsl.XslTransform.Transform" /> method is called.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlResolver" /> to use during transformation. If set to null, the XSLT document() function is not resolved.</returns>
		// Token: 0x17000ACC RID: 2764
		// (set) Token: 0x0600328D RID: 12941 RVA: 0x00123F91 File Offset: 0x00122191
		public XmlResolver XmlResolver
		{
			set
			{
				this._documentResolver = value;
				this.isDocumentResolverSet = true;
			}
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> object that contains the XSLT style sheet. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x0600328E RID: 12942 RVA: 0x00123FA1 File Offset: 0x001221A1
		public void Load(XmlReader stylesheet)
		{
			this.Load(stylesheet, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> object that contains the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Load(System.Xml.XmlReader,System.Xml.XmlResolver)" />  method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x0600328F RID: 12943 RVA: 0x00123FAF File Offset: 0x001221AF
		public void Load(XmlReader stylesheet, XmlResolver resolver)
		{
			this.Load(new XPathDocument(stylesheet, XmlSpace.Preserve), resolver);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the XSLT style sheet. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x06003290 RID: 12944 RVA: 0x00123FBF File Offset: 0x001221BF
		public void Load(IXPathNavigable stylesheet)
		{
			this.Load(stylesheet, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x06003291 RID: 12945 RVA: 0x00123FCD File Offset: 0x001221CD
		public void Load(IXPathNavigable stylesheet, XmlResolver resolver)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(stylesheet.CreateNavigator(), resolver);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the XSLT style sheet. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x06003292 RID: 12946 RVA: 0x00123FEA File Offset: 0x001221EA
		public void Load(XPathNavigator stylesheet)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(stylesheet, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x06003293 RID: 12947 RVA: 0x00124006 File Offset: 0x00122206
		public void Load(XPathNavigator stylesheet, XmlResolver resolver)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Compile(stylesheet, resolver, null);
		}

		/// <summary>Loads the XSLT style sheet specified by a URL.</summary>
		/// <param name="url">The URL that specifies the XSLT style sheet to load. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded script, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x06003294 RID: 12948 RVA: 0x00124020 File Offset: 0x00122220
		public void Load(string url)
		{
			XmlTextReaderImpl xmlTextReaderImpl = new XmlTextReaderImpl(url);
			Evidence evidence = XmlSecureResolver.CreateEvidenceForUrl(xmlTextReaderImpl.BaseURI);
			this.Compile(Compiler.LoadDocument(xmlTextReaderImpl).CreateNavigator(), XsltConfigSection.CreateDefaultResolver(), evidence);
		}

		/// <summary>Loads the XSLT style sheet specified by a URL.</summary>
		/// <param name="url">The URL that specifies the XSLT style sheet to load. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> to use to load the style sheet and any style sheet(s) referenced in xsl:import and xsl:include elements.If this is null, a default <see cref="T:System.Xml.XmlUrlResolver" /> with no user credentials is used to open the style sheet. The default <see cref="T:System.Xml.XmlUrlResolver" /> is not used to resolve any external resources in the style sheet, so xsl:import and xsl:include elements are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Load(System.String,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded script, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x06003295 RID: 12949 RVA: 0x00124058 File Offset: 0x00122258
		public void Load(string url, XmlResolver resolver)
		{
			XmlTextReaderImpl xmlTextReaderImpl = new XmlTextReaderImpl(url);
			xmlTextReaderImpl.XmlResolver = resolver;
			Evidence evidence = XmlSecureResolver.CreateEvidenceForUrl(xmlTextReaderImpl.BaseURI);
			this.Compile(Compiler.LoadDocument(xmlTextReaderImpl).CreateNavigator(), resolver, evidence);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />. This method allows you to limit the permissions of the style sheet by specifying evidence.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> set on the assembly generated for the script block in the XSLT style sheet.If this is null, script blocks are not processed, the XSLT document() function is not supported, and privileged extension objects are disallowed.The caller must have ControlEvidence permission in order to supply evidence for the script assembly. Semi-trusted callers can set this parameter to null. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The referenced style sheet requires functionality that is not allowed by the evidence provided.The caller tries to supply evidence and does not have ControlEvidence permission. </exception>
		// Token: 0x06003296 RID: 12950 RVA: 0x00124092 File Offset: 0x00122292
		public void Load(IXPathNavigable stylesheet, XmlResolver resolver, Evidence evidence)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(stylesheet.CreateNavigator(), resolver, evidence);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />. This method allows you to limit the permissions of the style sheet by specifying evidence.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> object containing the style sheet to load. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> set on the assembly generated for the script block in the XSLT style sheet.If this is null, script blocks are not processed, the XSLT document() function is not supported, and privileged extension objects are disallowed.The caller must have ControlEvidence permission in order to supply evidence for the script assembly. Semi-trusted callers can set this parameter to null. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The referenced style sheet requires functionality that is not allowed by the evidence provided.The caller tries to supply evidence and does not have ControlEvidence permission. </exception>
		// Token: 0x06003297 RID: 12951 RVA: 0x001240B0 File Offset: 0x001222B0
		public void Load(XmlReader stylesheet, XmlResolver resolver, Evidence evidence)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(new XPathDocument(stylesheet, XmlSpace.Preserve), resolver, evidence);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />. This method allows you to limit the permissions of the style sheet by specifying evidence.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object containing the style sheet to load. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> set on the assembly generated for the script block in the XSLT style sheet.If this is null, script blocks are not processed, the XSLT document() function is not supported, and privileged extension objects are disallowed.The caller must have ControlEvidence permission in order to supply evidence for the script assembly. Semi-trusted callers can set this parameter to null. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The referenced style sheet requires functionality that is not allowed by the evidence provided.The caller tries to supply evidence and does not have ControlEvidence permission. </exception>
		// Token: 0x06003298 RID: 12952 RVA: 0x001240CF File Offset: 0x001222CF
		public void Load(XPathNavigator stylesheet, XmlResolver resolver, Evidence evidence)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Compile(stylesheet, resolver, evidence);
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x001240E8 File Offset: 0x001222E8
		private void CheckCommand()
		{
			if (this._CompiledStylesheet == null)
			{
				throw new InvalidOperationException(Res.GetString("No stylesheet was loaded."));
			}
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x0600329A RID: 12954 RVA: 0x00124102 File Offset: 0x00122302
		public XmlReader Transform(XPathNavigator input, XsltArgumentList args, XmlResolver resolver)
		{
			this.CheckCommand();
			return new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).StartReader();
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x0600329B RID: 12955 RVA: 0x0012412F File Offset: 0x0012232F
		public XmlReader Transform(XPathNavigator input, XsltArgumentList args)
		{
			return this.Transform(input, args, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified args and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x0600329C RID: 12956 RVA: 0x0012413F File Offset: 0x0012233F
		public void Transform(XPathNavigator input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			this.CheckCommand();
			new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).Execute(output);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified args and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x0600329D RID: 12957 RVA: 0x0012416E File Offset: 0x0012236E
		public void Transform(XPathNavigator input, XsltArgumentList args, XmlWriter output)
		{
			this.Transform(input, args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.IO.Stream,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x0600329E RID: 12958 RVA: 0x0012417F File Offset: 0x0012237F
		public void Transform(XPathNavigator input, XsltArgumentList args, Stream output, XmlResolver resolver)
		{
			this.CheckCommand();
			new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).Execute(output);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x0600329F RID: 12959 RVA: 0x001241AE File Offset: 0x001223AE
		public void Transform(XPathNavigator input, XsltArgumentList args, Stream output)
		{
			this.Transform(input, args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.IO.TextWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060032A0 RID: 12960 RVA: 0x001241BF File Offset: 0x001223BF
		public void Transform(XPathNavigator input, XsltArgumentList args, TextWriter output, XmlResolver resolver)
		{
			this.CheckCommand();
			new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).Execute(output);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060032A1 RID: 12961 RVA: 0x001241EE File Offset: 0x001223EE
		public void Transform(XPathNavigator input, XsltArgumentList args, TextWriter output)
		{
			this.CheckCommand();
			new Processor(input, args, this._DocumentResolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).Execute(output);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.IXPathNavigable,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlResolver)" /> method completes. </param>
		// Token: 0x060032A2 RID: 12962 RVA: 0x00124221 File Offset: 0x00122421
		public XmlReader Transform(IXPathNavigable input, XsltArgumentList args, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Transform(input.CreateNavigator(), args, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		// Token: 0x060032A3 RID: 12963 RVA: 0x0012423F File Offset: 0x0012243F
		public XmlReader Transform(IXPathNavigable input, XsltArgumentList args)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Transform(input.CreateNavigator(), args, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.IXPathNavigable,System.Xml.Xsl.XsltArgumentList,System.IO.TextWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060032A4 RID: 12964 RVA: 0x00124262 File Offset: 0x00122462
		public void Transform(IXPathNavigable input, XsltArgumentList args, TextWriter output, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060032A5 RID: 12965 RVA: 0x00124282 File Offset: 0x00122482
		public void Transform(IXPathNavigable input, XsltArgumentList args, TextWriter output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Transform" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060032A6 RID: 12966 RVA: 0x001242A6 File Offset: 0x001224A6
		public void Transform(IXPathNavigable input, XsltArgumentList args, Stream output, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation.Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060032A7 RID: 12967 RVA: 0x001242C6 File Offset: 0x001224C6
		public void Transform(IXPathNavigable input, XsltArgumentList args, Stream output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.IXPathNavigable,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060032A8 RID: 12968 RVA: 0x001242EA File Offset: 0x001224EA
		public void Transform(IXPathNavigable input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060032A9 RID: 12969 RVA: 0x0012430A File Offset: 0x0012250A
		public void Transform(IXPathNavigable input, XsltArgumentList args, XmlWriter output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the input file and outputs the result to an output file.</summary>
		/// <param name="inputfile">The URL of the source document to be transformed. </param>
		/// <param name="outputfile">The URL of the output file. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Transform" /> method completes. </param>
		// Token: 0x060032AA RID: 12970 RVA: 0x00124330 File Offset: 0x00122530
		public void Transform(string inputfile, string outputfile, XmlResolver resolver)
		{
			FileStream fileStream = null;
			try
			{
				XPathDocument xpathDocument = new XPathDocument(inputfile);
				fileStream = new FileStream(outputfile, FileMode.Create, FileAccess.ReadWrite);
				this.Transform(xpathDocument, null, fileStream, resolver);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		/// <summary>Transforms the XML data in the input file and outputs the result to an output file.</summary>
		/// <param name="inputfile">The URL of the source document to be transformed. </param>
		/// <param name="outputfile">The URL of the output file. </param>
		// Token: 0x060032AB RID: 12971 RVA: 0x00124378 File Offset: 0x00122578
		public void Transform(string inputfile, string outputfile)
		{
			this.Transform(inputfile, outputfile, this._DocumentResolver);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x00124388 File Offset: 0x00122588
		private void Compile(XPathNavigator stylesheet, XmlResolver resolver, Evidence evidence)
		{
			Compiler compiler = ((this.Debugger == null) ? new Compiler() : new DbgCompiler(this.Debugger));
			NavigatorInput navigatorInput = new NavigatorInput(stylesheet);
			compiler.Compile(navigatorInput, resolver ?? XmlNullResolver.Singleton, evidence);
			this._CompiledStylesheet = compiler.CompiledStylesheet;
			this._QueryStore = compiler.QueryStore;
			this._RootAction = compiler.RootAction;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x001243ED File Offset: 0x001225ED
		internal IXsltDebugger Debugger
		{
			get
			{
				return this.debugger;
			}
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x001243F5 File Offset: 0x001225F5
		internal XslTransform(object debugger)
		{
			if (debugger != null)
			{
				this.debugger = new XslTransform.DebuggerAddapter(debugger);
			}
		}

		// Token: 0x040020EA RID: 8426
		private XmlResolver _documentResolver;

		// Token: 0x040020EB RID: 8427
		private bool isDocumentResolverSet;

		// Token: 0x040020EC RID: 8428
		private Stylesheet _CompiledStylesheet;

		// Token: 0x040020ED RID: 8429
		private List<TheQuery> _QueryStore;

		// Token: 0x040020EE RID: 8430
		private RootAction _RootAction;

		// Token: 0x040020EF RID: 8431
		private IXsltDebugger debugger;

		// Token: 0x020004D9 RID: 1241
		private class DebuggerAddapter : IXsltDebugger
		{
			// Token: 0x060032AF RID: 12975 RVA: 0x0012440C File Offset: 0x0012260C
			public DebuggerAddapter(object unknownDebugger)
			{
				this.unknownDebugger = unknownDebugger;
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
				Type type = unknownDebugger.GetType();
				this.getBltIn = type.GetMethod("GetBuiltInTemplatesUri", bindingFlags);
				this.onCompile = type.GetMethod("OnInstructionCompile", bindingFlags);
				this.onExecute = type.GetMethod("OnInstructionExecute", bindingFlags);
			}

			// Token: 0x060032B0 RID: 12976 RVA: 0x00124466 File Offset: 0x00122666
			public string GetBuiltInTemplatesUri()
			{
				if (this.getBltIn == null)
				{
					return null;
				}
				return (string)this.getBltIn.Invoke(this.unknownDebugger, new object[0]);
			}

			// Token: 0x060032B1 RID: 12977 RVA: 0x00124494 File Offset: 0x00122694
			public void OnInstructionCompile(XPathNavigator styleSheetNavigator)
			{
				if (this.onCompile != null)
				{
					this.onCompile.Invoke(this.unknownDebugger, new object[] { styleSheetNavigator });
				}
			}

			// Token: 0x060032B2 RID: 12978 RVA: 0x001244C0 File Offset: 0x001226C0
			public void OnInstructionExecute(IXsltProcessor xsltProcessor)
			{
				if (this.onExecute != null)
				{
					this.onExecute.Invoke(this.unknownDebugger, new object[] { xsltProcessor });
				}
			}

			// Token: 0x040020F0 RID: 8432
			private object unknownDebugger;

			// Token: 0x040020F1 RID: 8433
			private MethodInfo getBltIn;

			// Token: 0x040020F2 RID: 8434
			private MethodInfo onCompile;

			// Token: 0x040020F3 RID: 8435
			private MethodInfo onExecute;
		}
	}
}
