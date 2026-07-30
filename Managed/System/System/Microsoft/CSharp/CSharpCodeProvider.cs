using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Microsoft.CSharp
{
	/// <summary>Provides access to instances of the C# code generator and code compiler.</summary>
	// Token: 0x020000E9 RID: 233
	public class CSharpCodeProvider : CodeDomProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.CSharp.CSharpCodeProvider" /> class. </summary>
		// Token: 0x06000647 RID: 1607 RVA: 0x00019A52 File Offset: 0x00017C52
		public CSharpCodeProvider()
		{
			this._generator = new CSharpCodeGenerator();
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.CSharp.CSharpCodeProvider" /> class by using the specified provider options. </summary>
		/// <param name="providerOptions">A <see cref="T:System.Collections.Generic.IDictionary`2" /> object that contains the provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions" /> is null.</exception>
		// Token: 0x06000648 RID: 1608 RVA: 0x00019A65 File Offset: 0x00017C65
		public CSharpCodeProvider(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				throw new ArgumentNullException("providerOptions");
			}
			this._generator = new CSharpCodeGenerator(providerOptions);
		}

		/// <summary>Gets the file name extension to use when creating source code files.</summary>
		/// <returns>The file name extension to use for generated source code files.</returns>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00019A87 File Offset: 0x00017C87
		public override string FileExtension
		{
			get
			{
				return "cs";
			}
		}

		/// <summary>Gets an instance of the C# code generator.</summary>
		/// <returns>An instance of the C# <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> implementation.</returns>
		// Token: 0x0600064A RID: 1610 RVA: 0x00019A8E File Offset: 0x00017C8E
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeGenerator CreateGenerator()
		{
			return this._generator;
		}

		/// <summary>Gets an instance of the C# code compiler.</summary>
		/// <returns>An instance of the C# <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> implementation.</returns>
		// Token: 0x0600064B RID: 1611 RVA: 0x00019A8E File Offset: 0x00017C8E
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeCompiler CreateCompiler()
		{
			return this._generator;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type of object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		/// <param name="type">The type of object to retrieve a type converter for. </param>
		// Token: 0x0600064C RID: 1612 RVA: 0x00019A96 File Offset: 0x00017C96
		public override TypeConverter GetConverter(Type type)
		{
			if (type == typeof(MemberAttributes))
			{
				return CSharpMemberAttributeConverter.Default;
			}
			if (!(type == typeof(TypeAttributes)))
			{
				return base.GetConverter(type);
			}
			return CSharpTypeAttributeConverter.Default;
		}

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> to generate code for.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="options">The <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> to use when generating the code.</param>
		// Token: 0x0600064D RID: 1613 RVA: 0x00019ACF File Offset: 0x00017CCF
		public override void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			this._generator.GenerateCodeFromMember(member, writer, options);
		}

		// Token: 0x04000BED RID: 3053
		private readonly CSharpCodeGenerator _generator;
	}
}
