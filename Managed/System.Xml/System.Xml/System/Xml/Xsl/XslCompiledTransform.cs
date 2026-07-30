using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.Xslt;

namespace System.Xml.Xsl
{
	/// <summary>Transforms XML data using an XSLT style sheet.</summary>
	// Token: 0x020004D7 RID: 1239
	public sealed class XslCompiledTransform
	{
		// Token: 0x06003261 RID: 12897 RVA: 0x00123574 File Offset: 0x00121774
		static XslCompiledTransform()
		{
			XslCompiledTransform.MemberAccessPermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
			XslCompiledTransform.ReaderSettings = new XmlReaderSettings();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XslCompiledTransform" /> class. </summary>
		// Token: 0x06003262 RID: 12898 RVA: 0x000020FD File Offset: 0x000002FD
		public XslCompiledTransform()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XslCompiledTransform" /> class with the specified debug setting. </summary>
		/// <param name="enableDebug">true to generate debug information; otherwise false. Setting this to true enables you to debug the style sheet with the Microsoft Visual Studio Debugger.</param>
		// Token: 0x06003263 RID: 12899 RVA: 0x0012359C File Offset: 0x0012179C
		public XslCompiledTransform(bool enableDebug)
		{
			this.enableDebug = enableDebug;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x001235AB File Offset: 0x001217AB
		private void Reset()
		{
			this.compilerResults = null;
			this.outputSettings = null;
			this.qil = null;
			this.command = null;
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x001235C9 File Offset: 0x001217C9
		internal CompilerErrorCollection Errors
		{
			get
			{
				if (this.compilerResults == null)
				{
					return null;
				}
				return this.compilerResults.Errors;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlWriterSettings" /> object that contains the output information derived from the xsl:output element of the style sheet.</summary>
		/// <returns>A read-only <see cref="T:System.Xml.XmlWriterSettings" /> object that contains the output information derived from the xsl:output element of the style sheet. This value can be null.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x001235E0 File Offset: 0x001217E0
		public XmlWriterSettings OutputSettings
		{
			get
			{
				return this.outputSettings;
			}
		}

		/// <summary>Gets the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> that contains the temporary files generated on disk after a successful call to the <see cref="Overload:System.Xml.Xsl.XslCompiledTransform.Load" /> method. </summary>
		/// <returns>The <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> that contains the temporary files generated on disk. This value is null if the <see cref="Overload:System.Xml.Xsl.XslCompiledTransform.Load" /> method has not been successfully called, or if debugging has not been enabled.</returns>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x001235E8 File Offset: 0x001217E8
		public TempFileCollection TemporaryFiles
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				if (this.compilerResults == null)
				{
					return null;
				}
				return this.compilerResults.TempFiles;
			}
		}

		/// <summary>Compiles the style sheet contained in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> containing the style sheet.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x06003268 RID: 12904 RVA: 0x001235FF File Offset: 0x001217FF
		public void Load(XmlReader stylesheet)
		{
			this.Reset();
			this.LoadInternal(stylesheet, XsltSettings.Default, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Compiles the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />. The <see cref="T:System.Xml.XmlResolver" /> resolves any XSLT import or include elements and the XSLT settings determine the permissions for the style sheet.</summary>
		/// <param name="stylesheet">The <see cref="T:System.Xml.XmlReader" /> containing the style sheet.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to apply to the style sheet. If this is null, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> setting is applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve any style sheets referenced in XSLT import and include elements. If this is null, external resources are not resolved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x06003269 RID: 12905 RVA: 0x00123619 File Offset: 0x00121819
		public void Load(XmlReader stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			this.LoadInternal(stylesheet, settings, stylesheetResolver);
		}

		/// <summary>Compiles the style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the style sheet.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x0600326A RID: 12906 RVA: 0x001235FF File Offset: 0x001217FF
		public void Load(IXPathNavigable stylesheet)
		{
			this.Reset();
			this.LoadInternal(stylesheet, XsltSettings.Default, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Compiles the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />. The <see cref="T:System.Xml.XmlResolver" /> resolves any XSLT import or include elements and the XSLT settings determine the permissions for the style sheet.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the style sheet.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to apply to the style sheet. If this is null, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> setting is applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve any style sheets referenced in XSLT import and include elements. If this is null, external resources are not resolved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x0600326B RID: 12907 RVA: 0x00123619 File Offset: 0x00121819
		public void Load(IXPathNavigable stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			this.LoadInternal(stylesheet, settings, stylesheetResolver);
		}

		/// <summary>Loads and compiles the style sheet located at the specified URI.</summary>
		/// <param name="stylesheetUri">The URI of the style sheet.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheetUri" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The style sheet cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="stylesheetUri" /> value includes a filename or directory that cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="stylesheetUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="stylesheetUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the style sheet.</exception>
		// Token: 0x0600326C RID: 12908 RVA: 0x0012362B File Offset: 0x0012182B
		public void Load(string stylesheetUri)
		{
			this.Reset();
			if (stylesheetUri == null)
			{
				throw new ArgumentNullException("stylesheetUri");
			}
			this.LoadInternal(stylesheetUri, XsltSettings.Default, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Loads and compiles the XSLT style sheet specified by the URI. The <see cref="T:System.Xml.XmlResolver" /> resolves any XSLT import or include elements and the XSLT settings determine the permissions for the style sheet.</summary>
		/// <param name="stylesheetUri">The URI of the style sheet.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to apply to the style sheet. If this is null, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> setting is applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the style sheet URI and any style sheets referenced in XSLT import and include elements. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheetUri" /> or <paramref name="stylesheetResolver" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The style sheet cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="stylesheetUri" /> value includes a filename or directory that cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="stylesheetUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="stylesheetUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the style sheet.</exception>
		// Token: 0x0600326D RID: 12909 RVA: 0x00123653 File Offset: 0x00121853
		public void Load(string stylesheetUri, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			if (stylesheetUri == null)
			{
				throw new ArgumentNullException("stylesheetUri");
			}
			this.LoadInternal(stylesheetUri, settings, stylesheetResolver);
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x00123674 File Offset: 0x00121874
		private CompilerResults LoadInternal(object stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			if (settings == null)
			{
				settings = XsltSettings.Default;
			}
			this.CompileXsltToQil(stylesheet, settings, stylesheetResolver);
			CompilerError firstError = this.GetFirstError();
			if (firstError != null)
			{
				throw new XslLoadException(firstError);
			}
			if (!settings.CheckOnly)
			{
				this.CompileQilToMsil(settings);
			}
			return this.compilerResults;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x001236C8 File Offset: 0x001218C8
		private void CompileXsltToQil(object stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.compilerResults = new Compiler(settings, this.enableDebug, null).Compile(stylesheet, stylesheetResolver, out this.qil);
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x001236EC File Offset: 0x001218EC
		private CompilerError GetFirstError()
		{
			foreach (object obj in this.compilerResults.Errors)
			{
				CompilerError compilerError = (CompilerError)obj;
				if (!compilerError.IsWarning)
				{
					return compilerError;
				}
			}
			return null;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x00123754 File Offset: 0x00121954
		private void CompileQilToMsil(XsltSettings settings)
		{
			this.command = new XmlILGenerator().Generate(this.qil, null);
			this.outputSettings = this.command.StaticData.DefaultWriterSettings;
			this.qil = null;
		}

		/// <summary>Compiles an XSLT style sheet to a specified type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> object containing compiler errors and warnings that indicate the results of the compilation.</returns>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> positioned at the beginning of the style sheet to be compiled.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to be applied to the style sheet. If this is null, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> will be applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> use to resolve style sheet modules referenced in xsl:import and xsl:include elements. If this is null, external resources will not be resolved.</param>
		/// <param name="debug">Setting this to true enables debugging the style sheet with a debugger.</param>
		/// <param name="typeBuilder">The <see cref="T:System.Reflection.Emit.TypeBuilder" /> used for the style sheet compilation. The provided TypeBuilder is used to generate the resulting type.</param>
		/// <param name="scriptAssemblyPath">The base path for the assemblies generated for msxsl:script elements. If only one script assembly is generated, this parameter specifies the path for that assembly. In case of multiple script assemblies, a distinctive suffix will be appended to the file name to ensure uniqueness of assembly names.</param>
		// Token: 0x06003272 RID: 12914 RVA: 0x0012378C File Offset: 0x0012198C
		public static CompilerErrorCollection CompileToType(XmlReader stylesheet, XsltSettings settings, XmlResolver stylesheetResolver, bool debug, TypeBuilder typeBuilder, string scriptAssemblyPath)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			if (typeBuilder == null)
			{
				throw new ArgumentNullException("typeBuilder");
			}
			if (settings == null)
			{
				settings = XsltSettings.Default;
			}
			if (settings.EnableScript && scriptAssemblyPath == null)
			{
				throw new ArgumentNullException("scriptAssemblyPath");
			}
			if (scriptAssemblyPath != null)
			{
				scriptAssemblyPath = Path.GetFullPath(scriptAssemblyPath);
			}
			QilExpression qilExpression;
			CompilerErrorCollection errors = new Compiler(settings, debug, scriptAssemblyPath).Compile(stylesheet, stylesheetResolver, out qilExpression).Errors;
			if (!errors.HasErrors)
			{
				if (XslCompiledTransform.GeneratedCodeCtor == null)
				{
					XslCompiledTransform.GeneratedCodeCtor = typeof(GeneratedCodeAttribute).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string)
					});
				}
				typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(XslCompiledTransform.GeneratedCodeCtor, new object[]
				{
					typeof(XslCompiledTransform).FullName,
					"4.0.0.0"
				}));
				new XmlILGenerator().Generate(qilExpression, typeBuilder);
			}
			return errors;
		}

		/// <summary>Loads the compiled style sheet that was created using the XSLT Compiler (xsltc.exe).</summary>
		/// <param name="compiledStylesheet">The name of the class that contains the compiled style sheet. This is usually the name of the style sheet. Unless otherwise specified, the xsltc.exe tool uses the name of the style sheet for the class and assembly names.</param>
		// Token: 0x06003273 RID: 12915 RVA: 0x00123894 File Offset: 0x00121A94
		public void Load(Type compiledStylesheet)
		{
			this.Reset();
			if (compiledStylesheet == null)
			{
				throw new ArgumentNullException("compiledStylesheet");
			}
			object[] customAttributes = compiledStylesheet.GetCustomAttributes(typeof(GeneratedCodeAttribute), false);
			GeneratedCodeAttribute generatedCodeAttribute = ((customAttributes.Length != 0) ? ((GeneratedCodeAttribute)customAttributes[0]) : null);
			if (generatedCodeAttribute != null && generatedCodeAttribute.Tool == typeof(XslCompiledTransform).FullName)
			{
				if (new Version("4.0.0.0").CompareTo(new Version(generatedCodeAttribute.Version)) < 0)
				{
					throw new ArgumentException(Res.GetString("Executing a stylesheet that was compiled using a later version of the framework is not supported. Stylesheet Version: {0}. Current Framework Version: {1}.", new object[] { generatedCodeAttribute.Version, "4.0.0.0" }), "compiledStylesheet");
				}
				FieldInfo field = compiledStylesheet.GetField("staticData", BindingFlags.Static | BindingFlags.NonPublic);
				FieldInfo field2 = compiledStylesheet.GetField("ebTypes", BindingFlags.Static | BindingFlags.NonPublic);
				if (field != null && field2 != null)
				{
					if (XsltConfigSection.EnableMemberAccessForXslCompiledTransform)
					{
						new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
					}
					byte[] array = field.GetValue(null) as byte[];
					if (array != null)
					{
						MethodInfo method = compiledStylesheet.GetMethod("Execute", BindingFlags.Static | BindingFlags.NonPublic);
						Type[] array2 = (Type[])field2.GetValue(null);
						this.Load(method, array, array2);
						return;
					}
				}
			}
			if (this.command == null)
			{
				throw new ArgumentException(Res.GetString("Type '{0}' is not a compiled stylesheet class.", new object[] { compiledStylesheet.FullName }), "compiledStylesheet");
			}
		}

		/// <summary>Loads a method from a style sheet compiled using the XSLTC.exe utility.</summary>
		/// <param name="executeMethod">A <see cref="T:System.Reflection.MethodInfo" /> object representing the compiler-generated <paramref name="execute" /> method of the compiled style sheet.</param>
		/// <param name="queryData">A byte array of serialized data structures in the <paramref name="staticData" /> field of the compiled style sheet as generated by the <see cref="M:System.Xml.Xsl.XslCompiledTransform.CompileToType(System.Xml.XmlReader,System.Xml.Xsl.XsltSettings,System.Xml.XmlResolver,System.Boolean,System.Reflection.Emit.TypeBuilder,System.String)" /> method.</param>
		/// <param name="earlyBoundTypes">An array of types stored in the compiler-generated <paramref name="ebTypes" /> field of the compiled style sheet.</param>
		// Token: 0x06003274 RID: 12916 RVA: 0x001239F4 File Offset: 0x00121BF4
		public void Load(MethodInfo executeMethod, byte[] queryData, Type[] earlyBoundTypes)
		{
			this.Reset();
			if (executeMethod == null)
			{
				throw new ArgumentNullException("executeMethod");
			}
			if (queryData == null)
			{
				throw new ArgumentNullException("queryData");
			}
			if (!XsltConfigSection.EnableMemberAccessForXslCompiledTransform && executeMethod.DeclaringType != null && !executeMethod.DeclaringType.IsVisible)
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			DynamicMethod dynamicMethod = executeMethod as DynamicMethod;
			Delegate @delegate = ((dynamicMethod != null) ? dynamicMethod.CreateDelegate(typeof(ExecuteDelegate)) : Delegate.CreateDelegate(typeof(ExecuteDelegate), executeMethod));
			this.command = new XmlILCommand((ExecuteDelegate)@delegate, new XmlQueryStaticData(queryData, earlyBoundTypes));
			this.outputSettings = this.command.StaticData.DefaultWriterSettings;
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06003275 RID: 12917 RVA: 0x00123AB7 File Offset: 0x00121CB7
		public void Transform(IXPathNavigable input, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, null, results, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06003276 RID: 12918 RVA: 0x00123ACE File Offset: 0x00121CCE
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, arguments, results, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.IO.TextWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.IO.TextWriter" /> to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06003277 RID: 12919 RVA: 0x00123AE8 File Offset: 0x00121CE8
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, TextWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to a stream. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional runtime arguments.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The stream to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06003278 RID: 12920 RVA: 0x00123B3C File Offset: 0x00121D3C
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, Stream results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">The <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06003279 RID: 12921 RVA: 0x00123B90 File Offset: 0x00121D90
		public void Transform(XmlReader input, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, null, results, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600327A RID: 12922 RVA: 0x00123BA7 File Offset: 0x00121DA7
		public void Transform(XmlReader input, XsltArgumentList arguments, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, arguments, results, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to a <see cref="T:System.IO.TextWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.IO.TextWriter" /> to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600327B RID: 12923 RVA: 0x00123BC0 File Offset: 0x00121DC0
		public void Transform(XmlReader input, XsltArgumentList arguments, TextWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to a stream. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The stream to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600327C RID: 12924 RVA: 0x00123C14 File Offset: 0x00121E14
		public void Transform(XmlReader input, XsltArgumentList arguments, Stream results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x0600327D RID: 12925 RVA: 0x00123C68 File Offset: 0x00121E68
		public void Transform(string inputUri, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				this.Transform(xmlReader, null, results, XsltConfigSection.CreateDefaultResolver());
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputtUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x0600327E RID: 12926 RVA: 0x00123CB4 File Offset: 0x00121EB4
		public void Transform(string inputUri, XsltArgumentList arguments, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				this.Transform(xmlReader, arguments, results, XsltConfigSection.CreateDefaultResolver());
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.IO.TextWriter" /> to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x0600327F RID: 12927 RVA: 0x00123D00 File Offset: 0x00121F00
		public void Transform(string inputUri, XsltArgumentList arguments, TextWriter results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
				{
					this.Transform(xmlReader, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
					xmlWriter.Close();
				}
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to stream. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The stream to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x06003280 RID: 12928 RVA: 0x00123D74 File Offset: 0x00121F74
		public void Transform(string inputUri, XsltArgumentList arguments, Stream results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
				{
					this.Transform(xmlReader, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
					xmlWriter.Close();
				}
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to a file.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="resultsFile">The URI of the output file.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="resultsFile" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The input document cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> or <paramref name="resultsFile" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> or <paramref name="resultsFile" /> value cannot be resolved.-or-An error occurred while processing the request</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> or <paramref name="resultsFile" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x06003281 RID: 12929 RVA: 0x00123DE8 File Offset: 0x00121FE8
		public void Transform(string inputUri, string resultsFile)
		{
			if (inputUri == null)
			{
				throw new ArgumentNullException("inputUri");
			}
			if (resultsFile == null)
			{
				throw new ArgumentNullException("resultsFile");
			}
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(resultsFile, this.OutputSettings))
				{
					this.Transform(xmlReader, null, xmlWriter, XsltConfigSection.CreateDefaultResolver());
					xmlWriter.Close();
				}
			}
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments and the XmlResolver resolves the XSLT document() function.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <param name="documentResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06003282 RID: 12930 RVA: 0x00123E70 File Offset: 0x00122070
		public void Transform(XmlReader input, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.CheckCommand();
			this.command.Execute(input, documentResolver, arguments, results);
		}

		/// <summary>Executes the transform by using the input document that is specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments and the <see cref="T:System.Xml.XmlResolver" /> resolves the XSLT document() function.</summary>
		/// <param name="input">The document to transform that is specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object.</param>
		/// <param name="arguments">Argument list as <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> by using the <see cref="T:System.Xml.XmlWriterSettings" /> object that is returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <param name="documentResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.</param>
		// Token: 0x06003283 RID: 12931 RVA: 0x00123E8F File Offset: 0x0012208F
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.CheckCommand();
			this.command.Execute(input.CreateNavigator(), documentResolver, arguments, results);
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x00123EB3 File Offset: 0x001220B3
		private static void CheckArguments(object input, object results)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x00123ED1 File Offset: 0x001220D1
		private static void CheckArguments(string inputUri, object results)
		{
			if (inputUri == null)
			{
				throw new ArgumentNullException("inputUri");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x00123EEF File Offset: 0x001220EF
		private void CheckCommand()
		{
			if (this.command == null)
			{
				throw new InvalidOperationException(Res.GetString("No stylesheet was loaded."));
			}
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x00123F09 File Offset: 0x00122109
		private QilExpression TestCompile(object stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			this.CompileXsltToQil(stylesheet, settings, stylesheetResolver);
			return this.qil;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x00123F20 File Offset: 0x00122120
		private void TestGenerate(XsltSettings settings)
		{
			this.CompileQilToMsil(settings);
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x00123F29 File Offset: 0x00122129
		private void Transform(string inputUri, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver)
		{
			this.command.Execute(inputUri, documentResolver, arguments, results);
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x00123F3C File Offset: 0x0012213C
		internal static void PrintQil(object qil, XmlWriter xw, bool printComments, bool printTypes, bool printLineInfo)
		{
			QilExpression qilExpression = (QilExpression)qil;
			QilXmlWriter.Options options = QilXmlWriter.Options.None;
			if (printComments)
			{
				options |= QilXmlWriter.Options.Annotations;
			}
			if (printTypes)
			{
				options |= QilXmlWriter.Options.TypeInfo;
			}
			if (printLineInfo)
			{
				options |= QilXmlWriter.Options.LineInfo;
			}
			new QilXmlWriter(xw, options).ToXml(qilExpression);
			xw.Flush();
		}

		// Token: 0x040020E1 RID: 8417
		private static readonly XmlReaderSettings ReaderSettings;

		// Token: 0x040020E2 RID: 8418
		private static readonly PermissionSet MemberAccessPermissionSet = new PermissionSet(PermissionState.None);

		// Token: 0x040020E3 RID: 8419
		private const string Version = "4.0.0.0";

		// Token: 0x040020E4 RID: 8420
		private bool enableDebug;

		// Token: 0x040020E5 RID: 8421
		private CompilerResults compilerResults;

		// Token: 0x040020E6 RID: 8422
		private XmlWriterSettings outputSettings;

		// Token: 0x040020E7 RID: 8423
		private QilExpression qil;

		// Token: 0x040020E8 RID: 8424
		private XmlILCommand command;

		// Token: 0x040020E9 RID: 8425
		private static volatile ConstructorInfo GeneratedCodeCtor;
	}
}
