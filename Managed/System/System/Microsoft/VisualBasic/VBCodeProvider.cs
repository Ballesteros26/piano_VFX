using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Microsoft.VisualBasic
{
	/// <summary>Provides access to instances of the Visual Basic code generator and code compiler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E3 RID: 227
	public class VBCodeProvider : CodeDomProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class. </summary>
		// Token: 0x06000586 RID: 1414 RVA: 0x00013832 File Offset: 0x00011A32
		public VBCodeProvider()
		{
			this._generator = new VBCodeGenerator();
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class by using the specified provider options. </summary>
		/// <param name="providerOptions">A <see cref="T:System.Collections.Generic.IDictionary`2" /> object that contains the provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions" /> is null.</exception>
		// Token: 0x06000587 RID: 1415 RVA: 0x00013845 File Offset: 0x00011A45
		public VBCodeProvider(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				throw new ArgumentNullException("providerOptions");
			}
			this._generator = new VBCodeGenerator(providerOptions);
		}

		/// <summary>Gets the file name extension to use when creating source code files.</summary>
		/// <returns>The file name extension to use for generated source code files.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00013867 File Offset: 0x00011A67
		public override string FileExtension
		{
			get
			{
				return "vb";
			}
		}

		/// <summary>Gets a language features identifier.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.LanguageOptions" /> that indicates special features of the language.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000027E2 File Offset: 0x000009E2
		public override LanguageOptions LanguageOptions
		{
			get
			{
				return LanguageOptions.CaseInsensitive;
			}
		}

		/// <summary>Gets an instance of the Visual Basic code generator.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600058A RID: 1418 RVA: 0x0001386E File Offset: 0x00011A6E
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeGenerator CreateGenerator()
		{
			return this._generator;
		}

		/// <summary>Gets an instance of the Visual Basic code compiler.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600058B RID: 1419 RVA: 0x0001386E File Offset: 0x00011A6E
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeCompiler CreateCompiler()
		{
			return this._generator;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type of object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		/// <param name="type">The type of object to retrieve a type converter for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600058C RID: 1420 RVA: 0x00013876 File Offset: 0x00011A76
		public override TypeConverter GetConverter(Type type)
		{
			if (type == typeof(MemberAttributes))
			{
				return VBMemberAttributeConverter.Default;
			}
			if (!(type == typeof(TypeAttributes)))
			{
				return base.GetConverter(type);
			}
			return VBTypeAttributeConverter.Default;
		}

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> to generate code for.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="options">The <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> to use when generating the code.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600058D RID: 1421 RVA: 0x000138AF File Offset: 0x00011AAF
		public override void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			this._generator.GenerateCodeFromMember(member, writer, options);
		}

		// Token: 0x04000BD6 RID: 3030
		private VBCodeGenerator _generator;
	}
}
