using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides a base class for <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementations. This class is abstract. </summary>
	// Token: 0x020007A4 RID: 1956
	public abstract class CodeDomProvider : Component
	{
		// Token: 0x06003E06 RID: 15878 RVA: 0x000DB600 File Offset: 0x000D9800
		static CodeDomProvider()
		{
			CodeDomProvider.AddCompilerInfo(new CompilerInfo(new CompilerParameters
			{
				WarningLevel = 4
			}, typeof(CSharpCodeProvider).FullName)
			{
				_compilerLanguages = new string[] { "c#", "cs", "csharp" },
				_compilerExtensions = new string[] { ".cs", "cs" }
			});
			CodeDomProvider.AddCompilerInfo(new CompilerInfo(new CompilerParameters
			{
				WarningLevel = 4
			}, typeof(VBCodeProvider).FullName)
			{
				_compilerLanguages = new string[] { "vb", "vbs", "visualbasic", "vbscript" },
				_compilerExtensions = new string[] { ".vb", "vb" }
			});
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000DB70C File Offset: 0x000D990C
		private static void AddCompilerInfo(CompilerInfo compilerInfo)
		{
			foreach (string text in compilerInfo._compilerLanguages)
			{
				CodeDomProvider.s_compilerLanguages[text] = compilerInfo;
			}
			foreach (string text2 in compilerInfo._compilerExtensions)
			{
				CodeDomProvider.s_compilerExtensions[text2] = compilerInfo;
			}
			CodeDomProvider.s_allCompilerInfo.Add(compilerInfo);
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the specified language and provider options.</summary>
		/// <returns>A CodeDOM provider that is implemented for the specified language name and options.</returns>
		/// <param name="language">The language name.</param>
		/// <param name="providerOptions">A collection of provider options from the configuration file.</param>
		// Token: 0x06003E08 RID: 15880 RVA: 0x000DB76E File Offset: 0x000D996E
		public static CodeDomProvider CreateProvider(string language, IDictionary<string, string> providerOptions)
		{
			return CodeDomProvider.GetCompilerInfo(language).CreateProvider(providerOptions);
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the specified language.</summary>
		/// <returns>A CodeDOM provider that is implemented for the specified language name.</returns>
		/// <param name="language">The language name. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="language" /> does not have a configured provider on this computer. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="language" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003E09 RID: 15881 RVA: 0x000DB77C File Offset: 0x000D997C
		public static CodeDomProvider CreateProvider(string language)
		{
			return CodeDomProvider.GetCompilerInfo(language).CreateProvider();
		}

		/// <summary>Returns a language name associated with the specified file name extension, as configured in the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> compiler configuration section.</summary>
		/// <returns>A language name associated with the file name extension, as configured in the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> compiler configuration settings.</returns>
		/// <param name="extension">A file name extension. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <paramref name="extension" /> does not have a configured language provider on this computer. </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="extension" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003E0A RID: 15882 RVA: 0x000DB789 File Offset: 0x000D9989
		public static string GetLanguageFromExtension(string extension)
		{
			CompilerInfo compilerInfoForExtensionNoThrow = CodeDomProvider.GetCompilerInfoForExtensionNoThrow(extension);
			if (compilerInfoForExtensionNoThrow == null)
			{
				throw new CodeDomProvider.ConfigurationErrorsException("There is no CodeDom provider defined for the language.");
			}
			return compilerInfoForExtensionNoThrow._compilerLanguages[0];
		}

		/// <summary>Tests whether a language has a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation configured on the computer.</summary>
		/// <returns>true if a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation is configured for the specified language; otherwise, false.</returns>
		/// <param name="language">The language name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="language" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003E0B RID: 15883 RVA: 0x000DB7A6 File Offset: 0x000D99A6
		public static bool IsDefinedLanguage(string language)
		{
			return CodeDomProvider.GetCompilerInfoForLanguageNoThrow(language) != null;
		}

		/// <summary>Tests whether a file name extension has an associated <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation configured on the computer.</summary>
		/// <returns>true if a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation is configured for the specified file name extension; otherwise, false.</returns>
		/// <param name="extension">A file name extension. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="extension" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003E0C RID: 15884 RVA: 0x000DB7B1 File Offset: 0x000D99B1
		public static bool IsDefinedExtension(string extension)
		{
			return CodeDomProvider.GetCompilerInfoForExtensionNoThrow(extension) != null;
		}

		/// <summary>Returns the language provider and compiler configuration settings for the specified language.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> object populated with settings of the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</returns>
		/// <param name="language">A language name. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <paramref name="language" /> does not have a configured provider on this computer. </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="language" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003E0D RID: 15885 RVA: 0x000DB7BC File Offset: 0x000D99BC
		public static CompilerInfo GetCompilerInfo(string language)
		{
			CompilerInfo compilerInfoForLanguageNoThrow = CodeDomProvider.GetCompilerInfoForLanguageNoThrow(language);
			if (compilerInfoForLanguageNoThrow == null)
			{
				throw new CodeDomProvider.ConfigurationErrorsException("There is no CodeDom provider defined for the language.");
			}
			return compilerInfoForLanguageNoThrow;
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000DB7D4 File Offset: 0x000D99D4
		private static CompilerInfo GetCompilerInfoForLanguageNoThrow(string language)
		{
			if (language == null)
			{
				throw new ArgumentNullException("language");
			}
			CompilerInfo compilerInfo;
			CodeDomProvider.s_compilerLanguages.TryGetValue(language.Trim(), out compilerInfo);
			return compilerInfo;
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000DB804 File Offset: 0x000D9A04
		private static CompilerInfo GetCompilerInfoForExtensionNoThrow(string extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			CompilerInfo compilerInfo;
			CodeDomProvider.s_compilerExtensions.TryGetValue(extension.Trim(), out compilerInfo);
			return compilerInfo;
		}

		/// <summary>Returns the language provider and compiler configuration settings for this computer.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> representing the settings of all configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementations.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003E10 RID: 15888 RVA: 0x000DB833 File Offset: 0x000D9A33
		public static CompilerInfo[] GetAllCompilerInfo()
		{
			return CodeDomProvider.s_allCompilerInfo.ToArray();
		}

		/// <summary>Gets the default file name extension to use for source code files in the current language.</summary>
		/// <returns>A file name extension corresponding to the extension of the source files of the current language. The base implementation always returns <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06003E11 RID: 15889 RVA: 0x000BE716 File Offset: 0x000BC916
		public virtual string FileExtension
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets a language features identifier.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.LanguageOptions" /> that indicates special features of the language.</returns>
		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x00004240 File Offset: 0x00002440
		public virtual LanguageOptions LanguageOptions
		{
			get
			{
				return LanguageOptions.None;
			}
		}

		/// <summary>When overridden in a derived class, creates a new code generator.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06003E13 RID: 15891
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeGenerator CreateGenerator();

		/// <summary>When overridden in a derived class, creates a new code generator using the specified <see cref="T:System.IO.TextWriter" /> for output.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> to use to output. </param>
		// Token: 0x06003E14 RID: 15892 RVA: 0x000DB83F File Offset: 0x000D9A3F
		public virtual ICodeGenerator CreateGenerator(TextWriter output)
		{
			return this.CreateGenerator();
		}

		/// <summary>When overridden in a derived class, creates a new code generator using the specified file name for output.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		/// <param name="fileName">The file name to output to. </param>
		// Token: 0x06003E15 RID: 15893 RVA: 0x000DB83F File Offset: 0x000D9A3F
		public virtual ICodeGenerator CreateGenerator(string fileName)
		{
			return this.CreateGenerator();
		}

		/// <summary>When overridden in a derived class, creates a new code compiler. </summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> that can be used for compilation of <see cref="N:System.CodeDom" /> based source code representations. </returns>
		// Token: 0x06003E16 RID: 15894
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeCompiler CreateCompiler();

		/// <summary>When overridden in a derived class, creates a new code parser.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeParser" /> that can be used to parse source code. The base implementation always returns null.</returns>
		// Token: 0x06003E17 RID: 15895 RVA: 0x00009E57 File Offset: 0x00008057
		[Obsolete("Callers should not use the ICodeParser interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public virtual ICodeParser CreateParser()
		{
			return null;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified data type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type, or null if a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type cannot be found.</returns>
		/// <param name="type">The type of object to retrieve a type converter for. </param>
		// Token: 0x06003E18 RID: 15896 RVA: 0x000DB847 File Offset: 0x000D9A47
		public virtual TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		/// <summary>Compiles an assembly based on the <see cref="N:System.CodeDom" /> trees contained in the specified array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects, using the specified compiler settings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of the compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the settings for the compilation.</param>
		/// <param name="compilationUnits">An array of type <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the code to compile.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E19 RID: 15897 RVA: 0x000DB84F File Offset: 0x000D9A4F
		public virtual CompilerResults CompileAssemblyFromDom(CompilerParameters options, params CodeCompileUnit[] compilationUnits)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromDomBatch(options, compilationUnits);
		}

		/// <summary>Compiles an assembly from the source code contained in the specified files, using the specified compiler settings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the settings for the compilation. </param>
		/// <param name="fileNames">An array of the names of the files to compile. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E1A RID: 15898 RVA: 0x000DB85E File Offset: 0x000D9A5E
		public virtual CompilerResults CompileAssemblyFromFile(CompilerParameters options, params string[] fileNames)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromFileBatch(options, fileNames);
		}

		/// <summary>Compiles an assembly from the specified array of strings containing source code, using the specified compiler settings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler settings for this compilation. </param>
		/// <param name="sources">An array of source code strings to compile. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E1B RID: 15899 RVA: 0x000DB86D File Offset: 0x000D9A6D
		public virtual CompilerResults CompileAssemblyFromSource(CompilerParameters options, params string[] sources)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromSourceBatch(options, sources);
		}

		/// <summary>Returns a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The value to verify as a valid identifier.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E1C RID: 15900 RVA: 0x000DB87C File Offset: 0x000D9A7C
		public virtual bool IsValidIdentifier(string value)
		{
			return this.CreateGeneratorHelper().IsValidIdentifier(value);
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <returns>The escaped identifier for the value.</returns>
		/// <param name="value">The string for which to create an escaped identifier.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E1D RID: 15901 RVA: 0x000DB88A File Offset: 0x000D9A8A
		public virtual string CreateEscapedIdentifier(string value)
		{
			return this.CreateGeneratorHelper().CreateEscapedIdentifier(value);
		}

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <returns>A valid identifier for the specified value.</returns>
		/// <param name="value">The string for which to generate a valid identifier.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E1E RID: 15902 RVA: 0x000DB898 File Offset: 0x000D9A98
		public virtual string CreateValidIdentifier(string value)
		{
			return this.CreateGeneratorHelper().CreateValidIdentifier(value);
		}

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <returns>A text representation of the specified type, formatted for the language in which code is generated by this code generator. In Visual Basic, for example, passing in a <see cref="T:System.CodeDom.CodeTypeReference" /> for the <see cref="T:System.Int32" /> type will return "Integer".</returns>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type to return.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E1F RID: 15903 RVA: 0x000DB8A6 File Offset: 0x000D9AA6
		public virtual string GetTypeOutput(CodeTypeReference type)
		{
			return this.CreateGeneratorHelper().GetTypeOutput(type);
		}

		/// <summary>Returns a value indicating whether the specified code generation support is provided.</summary>
		/// <returns>true if the specified code generation support is provided; otherwise, false.</returns>
		/// <param name="generatorSupport">A <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object that indicates the type of code generation support to verify.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E20 RID: 15904 RVA: 0x000DB8B4 File Offset: 0x000D9AB4
		public virtual bool Supports(GeneratorSupport generatorSupport)
		{
			return this.CreateGeneratorHelper().Supports(generatorSupport);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> object that indicates the expression for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E21 RID: 15905 RVA: 0x000DB8C2 File Offset: 0x000D9AC2
		public virtual void GenerateCodeFromExpression(CodeExpression expression, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromExpression(expression, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="statement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the CodeDOM elements for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E22 RID: 15906 RVA: 0x000DB8D2 File Offset: 0x000D9AD2
		public virtual void GenerateCodeFromStatement(CodeStatement statement, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromStatement(statement, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> object that indicates the namespace for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E23 RID: 15907 RVA: 0x000DB8E2 File Offset: 0x000D9AE2
		public virtual void GenerateCodeFromNamespace(CodeNamespace codeNamespace, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromNamespace(codeNamespace, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="compileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which the output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E24 RID: 15908 RVA: 0x000DB8F2 File Offset: 0x000D9AF2
		public virtual void GenerateCodeFromCompileUnit(CodeCompileUnit compileUnit, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromCompileUnit(compileUnit, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="codeType">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object that indicates the type for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E25 RID: 15909 RVA: 0x000DB902 File Offset: 0x000D9B02
		public virtual void GenerateCodeFromType(CodeTypeDeclaration codeType, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromType(codeType, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) member declaration and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> object that indicates the member for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">This method is not overridden in a derived class.</exception>
		// Token: 0x06003E26 RID: 15910 RVA: 0x000DB912 File Offset: 0x000D9B12
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			throw new NotImplementedException("This CodeDomProvider does not support this method.");
		}

		/// <summary>Compiles the code read from the specified text stream into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> that contains a representation of the parsed code.</returns>
		/// <param name="codeStream">A <see cref="T:System.IO.TextReader" /> object that is used to read the code to be parsed. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003E27 RID: 15911 RVA: 0x000DB91E File Offset: 0x000D9B1E
		public virtual CodeCompileUnit Parse(TextReader codeStream)
		{
			return this.CreateParserHelper().Parse(codeStream);
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000DB92C File Offset: 0x000D9B2C
		private ICodeCompiler CreateCompilerHelper()
		{
			ICodeCompiler codeCompiler = this.CreateCompiler();
			if (codeCompiler == null)
			{
				throw new NotImplementedException("This CodeDomProvider does not support this method.");
			}
			return codeCompiler;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000DB942 File Offset: 0x000D9B42
		private ICodeGenerator CreateGeneratorHelper()
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw new NotImplementedException("This CodeDomProvider does not support this method.");
			}
			return codeGenerator;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x000DB958 File Offset: 0x000D9B58
		private ICodeParser CreateParserHelper()
		{
			ICodeParser codeParser = this.CreateParser();
			if (codeParser == null)
			{
				throw new NotImplementedException("This CodeDomProvider does not support this method.");
			}
			return codeParser;
		}

		// Token: 0x04002E26 RID: 11814
		private static readonly Dictionary<string, CompilerInfo> s_compilerLanguages = new Dictionary<string, CompilerInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002E27 RID: 11815
		private static readonly Dictionary<string, CompilerInfo> s_compilerExtensions = new Dictionary<string, CompilerInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002E28 RID: 11816
		private static readonly List<CompilerInfo> s_allCompilerInfo = new List<CompilerInfo>();

		// Token: 0x020007A5 RID: 1957
		private sealed class ConfigurationErrorsException : SystemException
		{
			// Token: 0x06003E2C RID: 15916 RVA: 0x00039BD8 File Offset: 0x00037DD8
			public ConfigurationErrorsException(string message)
				: base(message)
			{
			}

			// Token: 0x06003E2D RID: 15917 RVA: 0x000DB96E File Offset: 0x000D9B6E
			public ConfigurationErrorsException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				throw new PlatformNotSupportedException();
			}
		}
	}
}
