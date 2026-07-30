using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an example implementation of the <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> interface. This class is abstract.</summary>
	// Token: 0x020007A6 RID: 1958
	public abstract class CodeGenerator : ICodeGenerator
	{
		/// <summary>Gets the code type declaration for the current class.</summary>
		/// <returns>The code type declaration for the current class.</returns>
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06003E2E RID: 15918 RVA: 0x000DB97D File Offset: 0x000D9B7D
		protected CodeTypeDeclaration CurrentClass
		{
			get
			{
				return this._currentClass;
			}
		}

		/// <summary>Gets the current class name.</summary>
		/// <returns>The current class name.</returns>
		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06003E2F RID: 15919 RVA: 0x000DB985 File Offset: 0x000D9B85
		protected string CurrentTypeName
		{
			get
			{
				if (this._currentClass == null)
				{
					return "<% unknown %>";
				}
				return this._currentClass.Name;
			}
		}

		/// <summary>Gets the current member of the class.</summary>
		/// <returns>The current member of the class.</returns>
		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x000DB9A0 File Offset: 0x000D9BA0
		protected CodeTypeMember CurrentMember
		{
			get
			{
				return this._currentMember;
			}
		}

		/// <summary>Gets the current member name.</summary>
		/// <returns>The name of the current member.</returns>
		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x000DB9A8 File Offset: 0x000D9BA8
		protected string CurrentMemberName
		{
			get
			{
				if (this._currentMember == null)
				{
					return "<% unknown %>";
				}
				return this._currentMember.Name;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is an interface.</summary>
		/// <returns>true if the current object is an interface; otherwise, false.</returns>
		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06003E32 RID: 15922 RVA: 0x000DB9C3 File Offset: 0x000D9BC3
		protected bool IsCurrentInterface
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsInterface;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a class.</summary>
		/// <returns>true if the current object is a class; otherwise, false.</returns>
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x000DB9E7 File Offset: 0x000D9BE7
		protected bool IsCurrentClass
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsClass;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a value type or struct.</summary>
		/// <returns>true if the current object is a value type or struct; otherwise, false.</returns>
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x000DBA0B File Offset: 0x000D9C0B
		protected bool IsCurrentStruct
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsStruct;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is an enumeration.</summary>
		/// <returns>true if the current object is an enumeration; otherwise, false.</returns>
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x000DBA2F File Offset: 0x000D9C2F
		protected bool IsCurrentEnum
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsEnum;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a delegate.</summary>
		/// <returns>true if the current object is a delegate; otherwise, false.</returns>
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x000DBA53 File Offset: 0x000D9C53
		protected bool IsCurrentDelegate
		{
			get
			{
				return this._currentClass != null && this._currentClass is CodeTypeDelegate;
			}
		}

		/// <summary>Gets or sets the amount of spaces to indent each indentation level.</summary>
		/// <returns>The number of spaces to indent for each indentation level.</returns>
		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x000DBA6D File Offset: 0x000D9C6D
		// (set) Token: 0x06003E38 RID: 15928 RVA: 0x000DBA7A File Offset: 0x000D9C7A
		protected int Indent
		{
			get
			{
				return this._output.Indent;
			}
			set
			{
				this._output.Indent = value;
			}
		}

		/// <summary>Gets the token that represents null.</summary>
		/// <returns>The token that represents null.</returns>
		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06003E39 RID: 15929
		protected abstract string NullToken { get; }

		/// <summary>Gets the text writer to use for output.</summary>
		/// <returns>The text writer to use for output.</returns>
		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000DBA88 File Offset: 0x000D9C88
		protected TextWriter Output
		{
			get
			{
				return this._output;
			}
		}

		/// <summary>Gets the options to be used by the code generator.</summary>
		/// <returns>An object that indicates the options for the code generator to use.</returns>
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x000DBA90 File Offset: 0x000D9C90
		protected CodeGeneratorOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x000DBA98 File Offset: 0x000D9C98
		private void GenerateType(CodeTypeDeclaration e)
		{
			this._currentClass = e;
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			this.GenerateCommentStatements(e.Comments);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			this.GenerateTypeStart(e);
			if (this.Options.VerbatimOrder)
			{
				using (IEnumerator enumerator = e.Members.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
						this.GenerateTypeMember(codeTypeMember, e);
					}
					goto IL_00CA;
				}
			}
			this.GenerateFields(e);
			this.GenerateSnippetMembers(e);
			this.GenerateTypeConstructors(e);
			this.GenerateConstructors(e);
			this.GenerateProperties(e);
			this.GenerateEvents(e);
			this.GenerateMethods(e);
			this.GenerateNestedTypes(e);
			IL_00CA:
			this._currentClass = e;
			this.GenerateTypeEnd(e);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		/// <summary>Generates code for the specified code directives.</summary>
		/// <param name="directives">The code directives to generate code for.</param>
		// Token: 0x06003E3D RID: 15933 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void GenerateDirectives(CodeDirectiveCollection directives)
		{
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x000DBBBC File Offset: 0x000D9DBC
		private void GenerateTypeMember(CodeTypeMember member, CodeTypeDeclaration declaredType)
		{
			if (this._options.BlankLinesBetweenMembers)
			{
				this.Output.WriteLine();
			}
			if (member is CodeTypeDeclaration)
			{
				((ICodeGenerator)this).GenerateCodeFromType((CodeTypeDeclaration)member, this._output.InnerWriter, this._options);
				this._currentClass = declaredType;
				return;
			}
			if (member.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(member.StartDirectives);
			}
			this.GenerateCommentStatements(member.Comments);
			if (member.LinePragma != null)
			{
				this.GenerateLinePragmaStart(member.LinePragma);
			}
			if (member is CodeMemberField)
			{
				this.GenerateField((CodeMemberField)member);
			}
			else if (member is CodeMemberProperty)
			{
				this.GenerateProperty((CodeMemberProperty)member, declaredType);
			}
			else if (member is CodeMemberMethod)
			{
				if (member is CodeConstructor)
				{
					this.GenerateConstructor((CodeConstructor)member, declaredType);
				}
				else if (member is CodeTypeConstructor)
				{
					this.GenerateTypeConstructor((CodeTypeConstructor)member);
				}
				else if (member is CodeEntryPointMethod)
				{
					this.GenerateEntryPointMethod((CodeEntryPointMethod)member, declaredType);
				}
				else
				{
					this.GenerateMethod((CodeMemberMethod)member, declaredType);
				}
			}
			else if (member is CodeMemberEvent)
			{
				this.GenerateEvent((CodeMemberEvent)member, declaredType);
			}
			else if (member is CodeSnippetTypeMember)
			{
				int indent = this.Indent;
				this.Indent = 0;
				this.GenerateSnippetMember((CodeSnippetTypeMember)member);
				this.Indent = indent;
				this.Output.WriteLine();
			}
			if (member.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(member.LinePragma);
			}
			if (member.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(member.EndDirectives);
			}
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x000DBD54 File Offset: 0x000D9F54
		private void GenerateTypeConstructors(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeTypeConstructor)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeTypeConstructor codeTypeConstructor = (CodeTypeConstructor)codeTypeMember;
					if (codeTypeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeTypeConstructor.LinePragma);
					}
					this.GenerateTypeConstructor(codeTypeConstructor);
					if (codeTypeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeTypeConstructor.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		/// <summary>Generates code for the namespaces in the specified compile unit.</summary>
		/// <param name="e">The compile unit to generate namespaces for. </param>
		// Token: 0x06003E40 RID: 15936 RVA: 0x000DBE68 File Offset: 0x000DA068
		protected void GenerateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				((ICodeGenerator)this).GenerateCodeFromNamespace(codeNamespace, this._output.InnerWriter, this._options);
			}
		}

		/// <summary>Generates code for the specified namespace and the classes it contains.</summary>
		/// <param name="e">The namespace to generate classes for. </param>
		// Token: 0x06003E41 RID: 15937 RVA: 0x000DBED4 File Offset: 0x000DA0D4
		protected void GenerateTypes(CodeNamespace e)
		{
			foreach (object obj in e.Types)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
				if (this._options.BlankLinesBetweenMembers)
				{
					this.Output.WriteLine();
				}
				((ICodeGenerator)this).GenerateCodeFromType(codeTypeDeclaration, this._output.InnerWriter, this._options);
			}
		}

		/// <summary>Gets a value indicating whether the generator provides support for the language features represented by the specified <see cref="T:System.CodeDom.Compiler.GeneratorSupport" />  object.</summary>
		/// <returns>true if the specified capabilities are supported; otherwise, false.</returns>
		/// <param name="support">The capabilities to test the generator for.</param>
		// Token: 0x06003E42 RID: 15938 RVA: 0x000DBF58 File Offset: 0x000DA158
		bool ICodeGenerator.Supports(GeneratorSupport support)
		{
			return this.Supports(support);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The type to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003E43 RID: 15939 RVA: 0x000DBF64 File Offset: 0x000DA164
		void ICodeGenerator.GenerateCodeFromType(CodeTypeDeclaration e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = o ?? new CodeGeneratorOptions();
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				this.GenerateType(e);
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and outputs it to the specified text writer.</summary>
		/// <param name="e">The expression to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003E44 RID: 15940 RVA: 0x000DBFF4 File Offset: 0x000DA1F4
		void ICodeGenerator.GenerateCodeFromExpression(CodeExpression e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = o ?? new CodeGeneratorOptions();
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				this.GenerateExpression(e);
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The CodeDOM compilation unit to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003E45 RID: 15941 RVA: 0x000DC084 File Offset: 0x000DA284
		void ICodeGenerator.GenerateCodeFromCompileUnit(CodeCompileUnit e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = o ?? new CodeGeneratorOptions();
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				if (e is CodeSnippetCompileUnit)
				{
					this.GenerateSnippetCompileUnit((CodeSnippetCompileUnit)e);
				}
				else
				{
					this.GenerateCompileUnit(e);
				}
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The namespace to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003E46 RID: 15942 RVA: 0x000DC128 File Offset: 0x000DA328
		void ICodeGenerator.GenerateCodeFromNamespace(CodeNamespace e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = o ?? new CodeGeneratorOptions();
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				this.GenerateNamespace(e);
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The statement that contains the CodeDOM elements to translate.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003E47 RID: 15943 RVA: 0x000DC1B8 File Offset: 0x000DA3B8
		void ICodeGenerator.GenerateCodeFromStatement(CodeStatement e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = o ?? new CodeGeneratorOptions();
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				this.GenerateStatement(e);
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">The class member to generate code for.</param>
		/// <param name="writer">The text writer to output code to.</param>
		/// <param name="options">The options to use when generating the code. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.CodeDom.Compiler.CodeGenerator.Output" /> property is not null.</exception>
		// Token: 0x06003E48 RID: 15944 RVA: 0x000DC248 File Offset: 0x000DA448
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			if (this._output != null)
			{
				throw new InvalidOperationException("This code generation API cannot be called while the generator is being used to generate something else.");
			}
			this._options = options ?? new CodeGeneratorOptions();
			this._output = new ExposedTabStringIndentedTextWriter(writer, this._options.IndentString);
			try
			{
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
				this._currentClass = codeTypeDeclaration;
				this.GenerateTypeMember(member, codeTypeDeclaration);
			}
			finally
			{
				this._currentClass = null;
				this._output = null;
				this._options = null;
			}
		}

		/// <summary>Gets a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The value to test.</param>
		// Token: 0x06003E49 RID: 15945 RVA: 0x000DC2CC File Offset: 0x000DA4CC
		bool ICodeGenerator.IsValidIdentifier(string value)
		{
			return this.IsValidIdentifier(value);
		}

		/// <summary>Throws an exception if the specified value is not a valid identifier.</summary>
		/// <param name="value">The identifier to validate.</param>
		// Token: 0x06003E4A RID: 15946 RVA: 0x000DC2D5 File Offset: 0x000DA4D5
		void ICodeGenerator.ValidateIdentifier(string value)
		{
			this.ValidateIdentifier(value);
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <returns>The escaped identifier for the value.</returns>
		/// <param name="value">The string to create an escaped identifier for.</param>
		// Token: 0x06003E4B RID: 15947 RVA: 0x000DC2DE File Offset: 0x000DA4DE
		string ICodeGenerator.CreateEscapedIdentifier(string value)
		{
			return this.CreateEscapedIdentifier(value);
		}

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <returns>A valid identifier for the specified value.</returns>
		/// <param name="value">The string to generate a valid identifier for.</param>
		// Token: 0x06003E4C RID: 15948 RVA: 0x000DC2E7 File Offset: 0x000DA4E7
		string ICodeGenerator.CreateValidIdentifier(string value)
		{
			return this.CreateValidIdentifier(value);
		}

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <returns>The name of the data type reference.</returns>
		/// <param name="type">The type to return.</param>
		// Token: 0x06003E4D RID: 15949 RVA: 0x000DC2F0 File Offset: 0x000DA4F0
		string ICodeGenerator.GetTypeOutput(CodeTypeReference type)
		{
			return this.GetTypeOutput(type);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x000DC2FC File Offset: 0x000DA4FC
		private void GenerateConstructors(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeConstructor)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeConstructor codeConstructor = (CodeConstructor)codeTypeMember;
					if (codeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeConstructor.LinePragma);
					}
					this.GenerateConstructor(codeConstructor, e);
					if (codeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeConstructor.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x000DC410 File Offset: 0x000DA610
		private void GenerateEvents(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeMemberEvent)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeMemberEvent codeMemberEvent = (CodeMemberEvent)codeTypeMember;
					if (codeMemberEvent.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberEvent.LinePragma);
					}
					this.GenerateEvent(codeMemberEvent, e);
					if (codeMemberEvent.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberEvent.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		/// <summary>Generates code for the specified code expression.</summary>
		/// <param name="e">The code expression to generate code for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid <see cref="T:System.CodeDom.CodeStatement" />. </exception>
		// Token: 0x06003E50 RID: 15952 RVA: 0x000DC524 File Offset: 0x000DA724
		protected void GenerateExpression(CodeExpression e)
		{
			if (e is CodeArrayCreateExpression)
			{
				this.GenerateArrayCreateExpression((CodeArrayCreateExpression)e);
				return;
			}
			if (e is CodeBaseReferenceExpression)
			{
				this.GenerateBaseReferenceExpression((CodeBaseReferenceExpression)e);
				return;
			}
			if (e is CodeBinaryOperatorExpression)
			{
				this.GenerateBinaryOperatorExpression((CodeBinaryOperatorExpression)e);
				return;
			}
			if (e is CodeCastExpression)
			{
				this.GenerateCastExpression((CodeCastExpression)e);
				return;
			}
			if (e is CodeDelegateCreateExpression)
			{
				this.GenerateDelegateCreateExpression((CodeDelegateCreateExpression)e);
				return;
			}
			if (e is CodeFieldReferenceExpression)
			{
				this.GenerateFieldReferenceExpression((CodeFieldReferenceExpression)e);
				return;
			}
			if (e is CodeArgumentReferenceExpression)
			{
				this.GenerateArgumentReferenceExpression((CodeArgumentReferenceExpression)e);
				return;
			}
			if (e is CodeVariableReferenceExpression)
			{
				this.GenerateVariableReferenceExpression((CodeVariableReferenceExpression)e);
				return;
			}
			if (e is CodeIndexerExpression)
			{
				this.GenerateIndexerExpression((CodeIndexerExpression)e);
				return;
			}
			if (e is CodeArrayIndexerExpression)
			{
				this.GenerateArrayIndexerExpression((CodeArrayIndexerExpression)e);
				return;
			}
			if (e is CodeSnippetExpression)
			{
				this.GenerateSnippetExpression((CodeSnippetExpression)e);
				return;
			}
			if (e is CodeMethodInvokeExpression)
			{
				this.GenerateMethodInvokeExpression((CodeMethodInvokeExpression)e);
				return;
			}
			if (e is CodeMethodReferenceExpression)
			{
				this.GenerateMethodReferenceExpression((CodeMethodReferenceExpression)e);
				return;
			}
			if (e is CodeEventReferenceExpression)
			{
				this.GenerateEventReferenceExpression((CodeEventReferenceExpression)e);
				return;
			}
			if (e is CodeDelegateInvokeExpression)
			{
				this.GenerateDelegateInvokeExpression((CodeDelegateInvokeExpression)e);
				return;
			}
			if (e is CodeObjectCreateExpression)
			{
				this.GenerateObjectCreateExpression((CodeObjectCreateExpression)e);
				return;
			}
			if (e is CodeParameterDeclarationExpression)
			{
				this.GenerateParameterDeclarationExpression((CodeParameterDeclarationExpression)e);
				return;
			}
			if (e is CodeDirectionExpression)
			{
				this.GenerateDirectionExpression((CodeDirectionExpression)e);
				return;
			}
			if (e is CodePrimitiveExpression)
			{
				this.GeneratePrimitiveExpression((CodePrimitiveExpression)e);
				return;
			}
			if (e is CodePropertyReferenceExpression)
			{
				this.GeneratePropertyReferenceExpression((CodePropertyReferenceExpression)e);
				return;
			}
			if (e is CodePropertySetValueReferenceExpression)
			{
				this.GeneratePropertySetValueReferenceExpression((CodePropertySetValueReferenceExpression)e);
				return;
			}
			if (e is CodeThisReferenceExpression)
			{
				this.GenerateThisReferenceExpression((CodeThisReferenceExpression)e);
				return;
			}
			if (e is CodeTypeReferenceExpression)
			{
				this.GenerateTypeReferenceExpression((CodeTypeReferenceExpression)e);
				return;
			}
			if (e is CodeTypeOfExpression)
			{
				this.GenerateTypeOfExpression((CodeTypeOfExpression)e);
				return;
			}
			if (e is CodeDefaultValueExpression)
			{
				this.GenerateDefaultValueExpression((CodeDefaultValueExpression)e);
				return;
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			throw new ArgumentException(global::SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x000DC76C File Offset: 0x000DA96C
		private void GenerateFields(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeMemberField)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeMemberField codeMemberField = (CodeMemberField)codeTypeMember;
					if (codeMemberField.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberField.LinePragma);
					}
					this.GenerateField(codeMemberField);
					if (codeMemberField.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberField.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x000DC880 File Offset: 0x000DAA80
		private void GenerateSnippetMembers(CodeTypeDeclaration e)
		{
			bool flag = false;
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeSnippetTypeMember)
				{
					flag = true;
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeSnippetTypeMember codeSnippetTypeMember = (CodeSnippetTypeMember)codeTypeMember;
					if (codeSnippetTypeMember.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeSnippetTypeMember.LinePragma);
					}
					int indent = this.Indent;
					this.Indent = 0;
					this.GenerateSnippetMember(codeSnippetTypeMember);
					this.Indent = indent;
					if (codeSnippetTypeMember.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeSnippetTypeMember.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
			if (flag)
			{
				this.Output.WriteLine();
			}
		}

		/// <summary>Outputs the code of the specified literal code fragment compile unit.</summary>
		/// <param name="e">The literal code fragment compile unit to generate code for. </param>
		// Token: 0x06003E53 RID: 15955 RVA: 0x000DC9C0 File Offset: 0x000DABC0
		protected virtual void GenerateSnippetCompileUnit(CodeSnippetCompileUnit e)
		{
			this.GenerateDirectives(e.StartDirectives);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			this.Output.WriteLine(e.Value);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000DCA2C File Offset: 0x000DAC2C
		private void GenerateMethods(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeMemberMethod && !(codeTypeMember is CodeTypeConstructor) && !(codeTypeMember is CodeConstructor))
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeMemberMethod codeMemberMethod = (CodeMemberMethod)codeTypeMember;
					if (codeMemberMethod.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberMethod.LinePragma);
					}
					if (codeTypeMember is CodeEntryPointMethod)
					{
						this.GenerateEntryPointMethod((CodeEntryPointMethod)codeTypeMember, e);
					}
					else
					{
						this.GenerateMethod(codeMemberMethod, e);
					}
					if (codeMemberMethod.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberMethod.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000DCB78 File Offset: 0x000DAD78
		private void GenerateNestedTypes(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeTypeDeclaration)
				{
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)codeTypeMember;
					((ICodeGenerator)this).GenerateCodeFromType(codeTypeDeclaration, this._output.InnerWriter, this._options);
				}
			}
		}

		/// <summary>Generates code for the specified compile unit.</summary>
		/// <param name="e">The compile unit to generate code for. </param>
		// Token: 0x06003E56 RID: 15958 RVA: 0x000DCC0C File Offset: 0x000DAE0C
		protected virtual void GenerateCompileUnit(CodeCompileUnit e)
		{
			this.GenerateCompileUnitStart(e);
			this.GenerateNamespaces(e);
			this.GenerateCompileUnitEnd(e);
		}

		/// <summary>Generates code for the specified namespace.</summary>
		/// <param name="e">The namespace to generate code for. </param>
		// Token: 0x06003E57 RID: 15959 RVA: 0x000DCC23 File Offset: 0x000DAE23
		protected virtual void GenerateNamespace(CodeNamespace e)
		{
			this.GenerateCommentStatements(e.Comments);
			this.GenerateNamespaceStart(e);
			this.GenerateNamespaceImports(e);
			this.Output.WriteLine();
			this.GenerateTypes(e);
			this.GenerateNamespaceEnd(e);
		}

		/// <summary>Generates code for the specified namespace import.</summary>
		/// <param name="e">The namespace import to generate code for. </param>
		// Token: 0x06003E58 RID: 15960 RVA: 0x000DCC58 File Offset: 0x000DAE58
		protected void GenerateNamespaceImports(CodeNamespace e)
		{
			foreach (object obj in e.Imports)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaStart(codeNamespaceImport.LinePragma);
				}
				this.GenerateNamespaceImport(codeNamespaceImport);
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaEnd(codeNamespaceImport.LinePragma);
				}
			}
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x000DCCDC File Offset: 0x000DAEDC
		private void GenerateProperties(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeMemberProperty)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeMemberProperty codeMemberProperty = (CodeMemberProperty)codeTypeMember;
					if (codeMemberProperty.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberProperty.LinePragma);
					}
					this.GenerateProperty(codeMemberProperty, e);
					if (codeMemberProperty.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberProperty.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		/// <summary>Generates code for the specified statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid <see cref="T:System.CodeDom.CodeStatement" />.</exception>
		// Token: 0x06003E5A RID: 15962 RVA: 0x000DCDF0 File Offset: 0x000DAFF0
		protected void GenerateStatement(CodeStatement e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			if (e is CodeCommentStatement)
			{
				this.GenerateCommentStatement((CodeCommentStatement)e);
			}
			else if (e is CodeMethodReturnStatement)
			{
				this.GenerateMethodReturnStatement((CodeMethodReturnStatement)e);
			}
			else if (e is CodeConditionStatement)
			{
				this.GenerateConditionStatement((CodeConditionStatement)e);
			}
			else if (e is CodeTryCatchFinallyStatement)
			{
				this.GenerateTryCatchFinallyStatement((CodeTryCatchFinallyStatement)e);
			}
			else if (e is CodeAssignStatement)
			{
				this.GenerateAssignStatement((CodeAssignStatement)e);
			}
			else if (e is CodeExpressionStatement)
			{
				this.GenerateExpressionStatement((CodeExpressionStatement)e);
			}
			else if (e is CodeIterationStatement)
			{
				this.GenerateIterationStatement((CodeIterationStatement)e);
			}
			else if (e is CodeThrowExceptionStatement)
			{
				this.GenerateThrowExceptionStatement((CodeThrowExceptionStatement)e);
			}
			else if (e is CodeSnippetStatement)
			{
				int indent = this.Indent;
				this.Indent = 0;
				this.GenerateSnippetStatement((CodeSnippetStatement)e);
				this.Indent = indent;
			}
			else if (e is CodeVariableDeclarationStatement)
			{
				this.GenerateVariableDeclarationStatement((CodeVariableDeclarationStatement)e);
			}
			else if (e is CodeAttachEventStatement)
			{
				this.GenerateAttachEventStatement((CodeAttachEventStatement)e);
			}
			else if (e is CodeRemoveEventStatement)
			{
				this.GenerateRemoveEventStatement((CodeRemoveEventStatement)e);
			}
			else if (e is CodeGotoStatement)
			{
				this.GenerateGotoStatement((CodeGotoStatement)e);
			}
			else
			{
				if (!(e is CodeLabeledStatement))
				{
					throw new ArgumentException(global::SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
				}
				this.GenerateLabeledStatement((CodeLabeledStatement)e);
			}
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		/// <summary>Generates code for the specified statement collection.</summary>
		/// <param name="stms">The statements to generate code for. </param>
		// Token: 0x06003E5B RID: 15963 RVA: 0x000DCFE0 File Offset: 0x000DB1E0
		protected void GenerateStatements(CodeStatementCollection stmts)
		{
			foreach (object obj in stmts)
			{
				CodeStatement codeStatement = (CodeStatement)obj;
				((ICodeGenerator)this).GenerateCodeFromStatement(codeStatement, this._output.InnerWriter, this._options);
			}
		}

		/// <summary>Generates code for the specified attribute declaration collection.</summary>
		/// <param name="attributes">The attributes to generate code for. </param>
		// Token: 0x06003E5C RID: 15964 RVA: 0x000DD048 File Offset: 0x000DB248
		protected virtual void OutputAttributeDeclarations(CodeAttributeDeclarationCollection attributes)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			this.GenerateAttributeDeclarationsStart(attributes);
			bool flag = true;
			foreach (object obj in attributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.ContinueOnNewLine(", ");
				}
				this.Output.Write(codeAttributeDeclaration.Name);
				this.Output.Write('(');
				bool flag2 = true;
				foreach (object obj2 in codeAttributeDeclaration.Arguments)
				{
					CodeAttributeArgument codeAttributeArgument = (CodeAttributeArgument)obj2;
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						this.Output.Write(", ");
					}
					this.OutputAttributeArgument(codeAttributeArgument);
				}
				this.Output.Write(')');
			}
			this.GenerateAttributeDeclarationsEnd(attributes);
		}

		/// <summary>Outputs an argument in an attribute block.</summary>
		/// <param name="arg">The attribute argument to generate code for. </param>
		// Token: 0x06003E5D RID: 15965 RVA: 0x000DD160 File Offset: 0x000DB360
		protected virtual void OutputAttributeArgument(CodeAttributeArgument arg)
		{
			if (!string.IsNullOrEmpty(arg.Name))
			{
				this.OutputIdentifier(arg.Name);
				this.Output.Write('=');
			}
			((ICodeGenerator)this).GenerateCodeFromExpression(arg.Value, this._output.InnerWriter, this._options);
		}

		/// <summary>Generates code for the specified <see cref="T:System.CodeDom.FieldDirection" />.</summary>
		/// <param name="dir">One of the enumeration values that indicates the attribute of the field. </param>
		// Token: 0x06003E5E RID: 15966 RVA: 0x000DD1B0 File Offset: 0x000DB3B0
		protected virtual void OutputDirection(FieldDirection dir)
		{
			switch (dir)
			{
			case FieldDirection.In:
				break;
			case FieldDirection.Out:
				this.Output.Write("out ");
				return;
			case FieldDirection.Ref:
				this.Output.Write("ref ");
				break;
			default:
				return;
			}
		}

		/// <summary>Outputs a field scope modifier that corresponds to the specified attributes.</summary>
		/// <param name="attributes">One of the enumeration values that specifies the attributes. </param>
		// Token: 0x06003E5F RID: 15967 RVA: 0x000DD1E8 File Offset: 0x000DB3E8
		protected virtual void OutputFieldScopeModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Final:
			case MemberAttributes.Override:
				break;
			case MemberAttributes.Static:
				this.Output.Write("static ");
				return;
			case MemberAttributes.Const:
				this.Output.Write("const ");
				break;
			default:
				return;
			}
		}

		/// <summary>Generates code for the specified member access modifier.</summary>
		/// <param name="attributes">One of the enumeration values that indicates the member access modifier to generate code for. </param>
		// Token: 0x06003E60 RID: 15968 RVA: 0x000DD254 File Offset: 0x000DB454
		protected virtual void OutputMemberAccessModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
			if (memberAttributes <= MemberAttributes.Family)
			{
				if (memberAttributes == MemberAttributes.Assembly)
				{
					this.Output.Write("internal ");
					return;
				}
				if (memberAttributes == MemberAttributes.FamilyAndAssembly)
				{
					this.Output.Write("internal ");
					return;
				}
				if (memberAttributes != MemberAttributes.Family)
				{
					return;
				}
				this.Output.Write("protected ");
				return;
			}
			else
			{
				if (memberAttributes == MemberAttributes.FamilyOrAssembly)
				{
					this.Output.Write("protected internal ");
					return;
				}
				if (memberAttributes == MemberAttributes.Private)
				{
					this.Output.Write("private ");
					return;
				}
				if (memberAttributes != MemberAttributes.Public)
				{
					return;
				}
				this.Output.Write("public ");
				return;
			}
		}

		/// <summary>Generates code for the specified member scope modifier.</summary>
		/// <param name="attributes">One of the enumeration values that indicates the member scope modifier to generate code for. </param>
		// Token: 0x06003E61 RID: 15969 RVA: 0x000DD308 File Offset: 0x000DB508
		protected virtual void OutputMemberScopeModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Abstract:
				this.Output.Write("abstract ");
				return;
			case MemberAttributes.Final:
				this.Output.Write("");
				return;
			case MemberAttributes.Static:
				this.Output.Write("static ");
				return;
			case MemberAttributes.Override:
				this.Output.Write("override ");
				return;
			default:
				memberAttributes = attributes & MemberAttributes.AccessMask;
				if (memberAttributes == MemberAttributes.Family || memberAttributes == MemberAttributes.Public)
				{
					this.Output.Write("virtual ");
				}
				return;
			}
		}

		/// <summary>Generates code for the specified type.</summary>
		/// <param name="typeRef">The type to generate code for. </param>
		// Token: 0x06003E62 RID: 15970
		protected abstract void OutputType(CodeTypeReference typeRef);

		/// <summary>Generates code for the specified type attributes.</summary>
		/// <param name="attributes">One of the enumeration values that indicates the type attributes to generate code for. </param>
		/// <param name="isStruct">true if the type is a struct; otherwise, false. </param>
		/// <param name="isEnum">true if the type is an enum; otherwise, false. </param>
		// Token: 0x06003E63 RID: 15971 RVA: 0x000DD3C0 File Offset: 0x000DB5C0
		protected virtual void OutputTypeAttributes(TypeAttributes attributes, bool isStruct, bool isEnum)
		{
			TypeAttributes typeAttributes = attributes & TypeAttributes.VisibilityMask;
			if (typeAttributes - TypeAttributes.Public > 1)
			{
				if (typeAttributes == TypeAttributes.NestedPrivate)
				{
					this.Output.Write("private ");
				}
			}
			else
			{
				this.Output.Write("public ");
			}
			if (isStruct)
			{
				this.Output.Write("struct ");
				return;
			}
			if (isEnum)
			{
				this.Output.Write("enum ");
				return;
			}
			typeAttributes = attributes & TypeAttributes.ClassSemanticsMask;
			if (typeAttributes == TypeAttributes.NotPublic)
			{
				if ((attributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				{
					this.Output.Write("sealed ");
				}
				if ((attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
				{
					this.Output.Write("abstract ");
				}
				this.Output.Write("class ");
				return;
			}
			if (typeAttributes != TypeAttributes.ClassSemanticsMask)
			{
				return;
			}
			this.Output.Write("interface ");
		}

		/// <summary>Generates code for the specified object type and name pair.</summary>
		/// <param name="typeRef">The type. </param>
		/// <param name="name">The name for the object. </param>
		// Token: 0x06003E64 RID: 15972 RVA: 0x000DD492 File Offset: 0x000DB692
		protected virtual void OutputTypeNamePair(CodeTypeReference typeRef, string name)
		{
			this.OutputType(typeRef);
			this.Output.Write(' ');
			this.OutputIdentifier(name);
		}

		/// <summary>Outputs the specified identifier.</summary>
		/// <param name="ident">The identifier to output. </param>
		// Token: 0x06003E65 RID: 15973 RVA: 0x000DD4AF File Offset: 0x000DB6AF
		protected virtual void OutputIdentifier(string ident)
		{
			this.Output.Write(ident);
		}

		/// <summary>Generates code for the specified expression list.</summary>
		/// <param name="expressions">The expressions to generate code for. </param>
		// Token: 0x06003E66 RID: 15974 RVA: 0x000DD4BD File Offset: 0x000DB6BD
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions)
		{
			this.OutputExpressionList(expressions, false);
		}

		/// <summary>Generates code for the specified expression list.</summary>
		/// <param name="expressions">The expressions to generate code for. </param>
		/// <param name="newlineBetweenItems">true to insert a new line after each item; otherwise, false. </param>
		// Token: 0x06003E67 RID: 15975 RVA: 0x000DD4C8 File Offset: 0x000DB6C8
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions, bool newlineBetweenItems)
		{
			bool flag = true;
			int num = this.Indent;
			this.Indent = num + 1;
			foreach (object obj in expressions)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else if (newlineBetweenItems)
				{
					this.ContinueOnNewLine(",");
				}
				else
				{
					this.Output.Write(", ");
				}
				((ICodeGenerator)this).GenerateCodeFromExpression(codeExpression, this._output.InnerWriter, this._options);
			}
			num = this.Indent;
			this.Indent = num - 1;
		}

		/// <summary>Generates code for the specified operator.</summary>
		/// <param name="op">The operator to generate code for. </param>
		// Token: 0x06003E68 RID: 15976 RVA: 0x000DD57C File Offset: 0x000DB77C
		protected virtual void OutputOperator(CodeBinaryOperatorType op)
		{
			switch (op)
			{
			case CodeBinaryOperatorType.Add:
				this.Output.Write('+');
				return;
			case CodeBinaryOperatorType.Subtract:
				this.Output.Write('-');
				return;
			case CodeBinaryOperatorType.Multiply:
				this.Output.Write('*');
				return;
			case CodeBinaryOperatorType.Divide:
				this.Output.Write('/');
				return;
			case CodeBinaryOperatorType.Modulus:
				this.Output.Write('%');
				return;
			case CodeBinaryOperatorType.Assign:
				this.Output.Write('=');
				return;
			case CodeBinaryOperatorType.IdentityInequality:
				this.Output.Write("!=");
				return;
			case CodeBinaryOperatorType.IdentityEquality:
				this.Output.Write("==");
				return;
			case CodeBinaryOperatorType.ValueEquality:
				this.Output.Write("==");
				return;
			case CodeBinaryOperatorType.BitwiseOr:
				this.Output.Write('|');
				return;
			case CodeBinaryOperatorType.BitwiseAnd:
				this.Output.Write('&');
				return;
			case CodeBinaryOperatorType.BooleanOr:
				this.Output.Write("||");
				return;
			case CodeBinaryOperatorType.BooleanAnd:
				this.Output.Write("&&");
				return;
			case CodeBinaryOperatorType.LessThan:
				this.Output.Write('<');
				return;
			case CodeBinaryOperatorType.LessThanOrEqual:
				this.Output.Write("<=");
				return;
			case CodeBinaryOperatorType.GreaterThan:
				this.Output.Write('>');
				return;
			case CodeBinaryOperatorType.GreaterThanOrEqual:
				this.Output.Write(">=");
				return;
			default:
				return;
			}
		}

		/// <summary>Generates code for the specified parameters.</summary>
		/// <param name="parameters">The parameter declaration expressions to generate code for. </param>
		// Token: 0x06003E69 RID: 15977 RVA: 0x000DD6D8 File Offset: 0x000DB8D8
		protected virtual void OutputParameters(CodeParameterDeclarationExpressionCollection parameters)
		{
			bool flag = true;
			bool flag2 = parameters.Count > 15;
			if (flag2)
			{
				this.Indent += 3;
			}
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				if (flag2)
				{
					this.ContinueOnNewLine("");
				}
				this.GenerateExpression(codeParameterDeclarationExpression);
			}
			if (flag2)
			{
				this.Indent -= 3;
			}
		}

		/// <summary>Generates code for the specified array creation expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06003E6A RID: 15978
		protected abstract void GenerateArrayCreateExpression(CodeArrayCreateExpression e);

		/// <summary>Generates code for the specified base reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeBaseReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06003E6B RID: 15979
		protected abstract void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e);

		/// <summary>Generates code for the specified binary operator expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> that indicates the expression to generate code for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null.</exception>
		// Token: 0x06003E6C RID: 15980 RVA: 0x000DD784 File Offset: 0x000DB984
		protected virtual void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			bool flag = false;
			this.Output.Write('(');
			this.GenerateExpression(e.Left);
			this.Output.Write(' ');
			if (e.Left is CodeBinaryOperatorExpression || e.Right is CodeBinaryOperatorExpression)
			{
				if (!this._inNestedBinary)
				{
					flag = true;
					this._inNestedBinary = true;
					this.Indent += 3;
				}
				this.ContinueOnNewLine("");
			}
			this.OutputOperator(e.Operator);
			this.Output.Write(' ');
			this.GenerateExpression(e.Right);
			this.Output.Write(')');
			if (flag)
			{
				this.Indent -= 3;
				this._inNestedBinary = false;
			}
		}

		/// <summary>Generates a line-continuation character and outputs the specified string on a new line.</summary>
		/// <param name="st">The string to write on the new line. </param>
		// Token: 0x06003E6D RID: 15981 RVA: 0x000DD847 File Offset: 0x000DBA47
		protected virtual void ContinueOnNewLine(string st)
		{
			this.Output.WriteLine(st);
		}

		/// <summary>Generates code for the specified cast expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCastExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06003E6E RID: 15982
		protected abstract void GenerateCastExpression(CodeCastExpression e);

		/// <summary>Generates code for the specified delegate creation expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E6F RID: 15983
		protected abstract void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e);

		/// <summary>Generates code for the specified field reference expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E70 RID: 15984
		protected abstract void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e);

		/// <summary>Generates code for the specified argument reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06003E71 RID: 15985
		protected abstract void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e);

		/// <summary>Generates code for the specified variable reference expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E72 RID: 15986
		protected abstract void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e);

		/// <summary>Generates code for the specified indexer expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E73 RID: 15987
		protected abstract void GenerateIndexerExpression(CodeIndexerExpression e);

		/// <summary>Generates code for the specified array indexer expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06003E74 RID: 15988
		protected abstract void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e);

		/// <summary>Outputs the code of the specified literal code fragment expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E75 RID: 15989
		protected abstract void GenerateSnippetExpression(CodeSnippetExpression e);

		/// <summary>Generates code for the specified method invoke expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E76 RID: 15990
		protected abstract void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e);

		/// <summary>Generates code for the specified method reference expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E77 RID: 15991
		protected abstract void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e);

		/// <summary>Generates code for the specified event reference expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E78 RID: 15992
		protected abstract void GenerateEventReferenceExpression(CodeEventReferenceExpression e);

		/// <summary>Generates code for the specified delegate invoke expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E79 RID: 15993
		protected abstract void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e);

		/// <summary>Generates code for the specified object creation expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E7A RID: 15994
		protected abstract void GenerateObjectCreateExpression(CodeObjectCreateExpression e);

		/// <summary>Generates code for the specified parameter declaration expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E7B RID: 15995 RVA: 0x000DD858 File Offset: 0x000DBA58
		protected virtual void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributeDeclarations(e.CustomAttributes);
				this.Output.Write(' ');
			}
			this.OutputDirection(e.Direction);
			this.OutputTypeNamePair(e.Type, e.Name);
		}

		/// <summary>Generates code for the specified direction expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E7C RID: 15996 RVA: 0x000DD8AA File Offset: 0x000DBAAA
		protected virtual void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			this.OutputDirection(e.Direction);
			this.GenerateExpression(e.Expression);
		}

		/// <summary>Generates code for the specified primitive expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> uses an invalid data type. Only the following data types are valid:stringcharbyteInt16Int32Int64SingleDoubleDecimal</exception>
		// Token: 0x06003E7D RID: 15997 RVA: 0x000DD8C4 File Offset: 0x000DBAC4
		protected virtual void GeneratePrimitiveExpression(CodePrimitiveExpression e)
		{
			if (e.Value == null)
			{
				this.Output.Write(this.NullToken);
				return;
			}
			if (e.Value is string)
			{
				this.Output.Write(this.QuoteSnippetString((string)e.Value));
				return;
			}
			if (e.Value is char)
			{
				this.Output.Write("'" + e.Value.ToString() + "'");
				return;
			}
			if (e.Value is byte)
			{
				this.Output.Write(((byte)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is short)
			{
				this.Output.Write(((short)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is int)
			{
				this.Output.Write(((int)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is long)
			{
				this.Output.Write(((long)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is float)
			{
				this.GenerateSingleFloatValue((float)e.Value);
				return;
			}
			if (e.Value is double)
			{
				this.GenerateDoubleValue((double)e.Value);
				return;
			}
			if (e.Value is decimal)
			{
				this.GenerateDecimalValue((decimal)e.Value);
				return;
			}
			if (!(e.Value is bool))
			{
				throw new ArgumentException(global::SR.Format("Invalid Primitive Type: {0}. Consider using CodeObjectCreateExpression.", e.Value.GetType().ToString()));
			}
			if ((bool)e.Value)
			{
				this.Output.Write("true");
				return;
			}
			this.Output.Write("false");
		}

		/// <summary>Generates code for a single-precision floating point number.</summary>
		/// <param name="s">The value to generate code for. </param>
		// Token: 0x06003E7E RID: 15998 RVA: 0x000DDAC4 File Offset: 0x000DBCC4
		protected virtual void GenerateSingleFloatValue(float s)
		{
			this.Output.Write(s.ToString("R", CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for a double-precision floating point number.</summary>
		/// <param name="d">The value to generate code for. </param>
		// Token: 0x06003E7F RID: 15999 RVA: 0x000DDAE2 File Offset: 0x000DBCE2
		protected virtual void GenerateDoubleValue(double d)
		{
			this.Output.Write(d.ToString("R", CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for the specified decimal value.</summary>
		/// <param name="d">The decimal value to generate code for. </param>
		// Token: 0x06003E80 RID: 16000 RVA: 0x000DDB00 File Offset: 0x000DBD00
		protected virtual void GenerateDecimalValue(decimal d)
		{
			this.Output.Write(d.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for the specified reference to a default value.</summary>
		/// <param name="e">The reference to generate code for.</param>
		// Token: 0x06003E81 RID: 16001 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
		}

		/// <summary>Generates code for the specified property reference expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E82 RID: 16002
		protected abstract void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e);

		/// <summary>Generates code for the specified property set value reference expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E83 RID: 16003
		protected abstract void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e);

		/// <summary>Generates code for the specified this reference expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E84 RID: 16004
		protected abstract void GenerateThisReferenceExpression(CodeThisReferenceExpression e);

		/// <summary>Generates code for the specified type reference expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E85 RID: 16005 RVA: 0x000DDB19 File Offset: 0x000DBD19
		protected virtual void GenerateTypeReferenceExpression(CodeTypeReferenceExpression e)
		{
			this.OutputType(e.Type);
		}

		/// <summary>Generates code for the specified type of expression.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E86 RID: 16006 RVA: 0x000DDB27 File Offset: 0x000DBD27
		protected virtual void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			this.Output.Write("typeof(");
			this.OutputType(e.Type);
			this.Output.Write(')');
		}

		/// <summary>Generates code for the specified expression statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E87 RID: 16007
		protected abstract void GenerateExpressionStatement(CodeExpressionStatement e);

		/// <summary>Generates code for the specified iteration statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E88 RID: 16008
		protected abstract void GenerateIterationStatement(CodeIterationStatement e);

		/// <summary>Generates code for the specified throw exception statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E89 RID: 16009
		protected abstract void GenerateThrowExceptionStatement(CodeThrowExceptionStatement e);

		/// <summary>Generates code for the specified comment statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.CodeDom.CodeCommentStatement.Comment" /> property of <paramref name="e " />is not set.</exception>
		// Token: 0x06003E8A RID: 16010 RVA: 0x000DDB52 File Offset: 0x000DBD52
		protected virtual void GenerateCommentStatement(CodeCommentStatement e)
		{
			if (e.Comment == null)
			{
				throw new ArgumentException(global::SR.Format("The 'Comment' property of the CodeCommentStatement '{0}' cannot be null.", "e"), "e");
			}
			this.GenerateComment(e.Comment);
		}

		/// <summary>Generates code for the specified comment statements.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E8B RID: 16011 RVA: 0x000DDB84 File Offset: 0x000DBD84
		protected virtual void GenerateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement codeCommentStatement = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(codeCommentStatement);
			}
		}

		/// <summary>Generates code for the specified comment.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeComment" /> to generate code for. </param>
		// Token: 0x06003E8C RID: 16012
		protected abstract void GenerateComment(CodeComment e);

		/// <summary>Generates code for the specified method return statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E8D RID: 16013
		protected abstract void GenerateMethodReturnStatement(CodeMethodReturnStatement e);

		/// <summary>Generates code for the specified conditional statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E8E RID: 16014
		protected abstract void GenerateConditionStatement(CodeConditionStatement e);

		/// <summary>Generates code for the specified try...catch...finally statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E8F RID: 16015
		protected abstract void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e);

		/// <summary>Generates code for the specified assignment statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeAssignStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x06003E90 RID: 16016
		protected abstract void GenerateAssignStatement(CodeAssignStatement e);

		/// <summary>Generates code for the specified attach event statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeAttachEventStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x06003E91 RID: 16017
		protected abstract void GenerateAttachEventStatement(CodeAttachEventStatement e);

		/// <summary>Generates code for the specified remove event statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E92 RID: 16018
		protected abstract void GenerateRemoveEventStatement(CodeRemoveEventStatement e);

		/// <summary>Generates code for the specified goto statement.</summary>
		/// <param name="e">The expression to generate code for. </param>
		// Token: 0x06003E93 RID: 16019
		protected abstract void GenerateGotoStatement(CodeGotoStatement e);

		/// <summary>Generates code for the specified labeled statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E94 RID: 16020
		protected abstract void GenerateLabeledStatement(CodeLabeledStatement e);

		/// <summary>Outputs the code of the specified literal code fragment statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E95 RID: 16021 RVA: 0x00010B78 File Offset: 0x0000ED78
		protected virtual void GenerateSnippetStatement(CodeSnippetStatement e)
		{
			this.Output.WriteLine(e.Value);
		}

		/// <summary>Generates code for the specified variable declaration statement.</summary>
		/// <param name="e">The statement to generate code for. </param>
		// Token: 0x06003E96 RID: 16022
		protected abstract void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e);

		/// <summary>Generates code for the specified line pragma start.</summary>
		/// <param name="e">The start of the line pragma to generate code for. </param>
		// Token: 0x06003E97 RID: 16023
		protected abstract void GenerateLinePragmaStart(CodeLinePragma e);

		/// <summary>Generates code for the specified line pragma end.</summary>
		/// <param name="e">The end of the line pragma to generate code for. </param>
		// Token: 0x06003E98 RID: 16024
		protected abstract void GenerateLinePragmaEnd(CodeLinePragma e);

		/// <summary>Generates code for the specified event.</summary>
		/// <param name="e">The member event to generate code for. </param>
		/// <param name="c">The type of the object that this event occurs on. </param>
		// Token: 0x06003E99 RID: 16025
		protected abstract void GenerateEvent(CodeMemberEvent e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified member field.</summary>
		/// <param name="e">The field to generate code for. </param>
		// Token: 0x06003E9A RID: 16026
		protected abstract void GenerateField(CodeMemberField e);

		/// <summary>Outputs the code of the specified literal code fragment class member.</summary>
		/// <param name="e">The member to generate code for. </param>
		// Token: 0x06003E9B RID: 16027
		protected abstract void GenerateSnippetMember(CodeSnippetTypeMember e);

		/// <summary>Generates code for the specified entry point method.</summary>
		/// <param name="e">The entry point for the code. </param>
		/// <param name="c">The code that declares the type. </param>
		// Token: 0x06003E9C RID: 16028
		protected abstract void GenerateEntryPointMethod(CodeEntryPointMethod e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified method.</summary>
		/// <param name="e">The member method to generate code for. </param>
		/// <param name="c">The type of the object that this method occurs on. </param>
		// Token: 0x06003E9D RID: 16029
		protected abstract void GenerateMethod(CodeMemberMethod e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified property.</summary>
		/// <param name="e">The property to generate code for. </param>
		/// <param name="c">The type of the object that this property occurs on. </param>
		// Token: 0x06003E9E RID: 16030
		protected abstract void GenerateProperty(CodeMemberProperty e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified constructor.</summary>
		/// <param name="e">The constructor to generate code for. </param>
		/// <param name="c">The type of the object that this constructor constructs. </param>
		// Token: 0x06003E9F RID: 16031
		protected abstract void GenerateConstructor(CodeConstructor e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified class constructor.</summary>
		/// <param name="e">The class constructor to generate code for. </param>
		// Token: 0x06003EA0 RID: 16032
		protected abstract void GenerateTypeConstructor(CodeTypeConstructor e);

		/// <summary>Generates code for the specified start of the class.</summary>
		/// <param name="e">The start of the class to generate code for. </param>
		// Token: 0x06003EA1 RID: 16033
		protected abstract void GenerateTypeStart(CodeTypeDeclaration e);

		/// <summary>Generates code for the specified end of the class.</summary>
		/// <param name="e">The end of the class to generate code for. </param>
		// Token: 0x06003EA2 RID: 16034
		protected abstract void GenerateTypeEnd(CodeTypeDeclaration e);

		/// <summary>Generates code for the start of a compile unit.</summary>
		/// <param name="e">The compile unit to generate code for. </param>
		// Token: 0x06003EA3 RID: 16035 RVA: 0x000DDBD8 File Offset: 0x000DBDD8
		protected virtual void GenerateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
		}

		/// <summary>Generates code for the end of a compile unit.</summary>
		/// <param name="e">The compile unit to generate code for. </param>
		// Token: 0x06003EA4 RID: 16036 RVA: 0x000DDBF4 File Offset: 0x000DBDF4
		protected virtual void GenerateCompileUnitEnd(CodeCompileUnit e)
		{
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		/// <summary>Generates code for the start of a namespace.</summary>
		/// <param name="e">The namespace to generate code for. </param>
		// Token: 0x06003EA5 RID: 16037
		protected abstract void GenerateNamespaceStart(CodeNamespace e);

		/// <summary>Generates code for the end of a namespace.</summary>
		/// <param name="e">The namespace to generate code for. </param>
		// Token: 0x06003EA6 RID: 16038
		protected abstract void GenerateNamespaceEnd(CodeNamespace e);

		/// <summary>Generates code for the specified namespace import.</summary>
		/// <param name="e">The namespace import to generate code for. </param>
		// Token: 0x06003EA7 RID: 16039
		protected abstract void GenerateNamespaceImport(CodeNamespaceImport e);

		/// <summary>Generates code for the specified attribute block start.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the start of the attribute block to generate code for. </param>
		// Token: 0x06003EA8 RID: 16040
		protected abstract void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes);

		/// <summary>Generates code for the specified attribute block end.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the end of the attribute block to generate code for. </param>
		// Token: 0x06003EA9 RID: 16041
		protected abstract void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes);

		/// <summary>Gets a value indicating whether the specified code generation support is provided.</summary>
		/// <returns>true if the specified code generation support is provided; otherwise, false.</returns>
		/// <param name="support">The type of code generation support to test for. </param>
		// Token: 0x06003EAA RID: 16042
		protected abstract bool Supports(GeneratorSupport support);

		/// <summary>Gets a value indicating whether the specified value is a valid identifier.</summary>
		/// <returns>true if the value is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The value to test for conflicts with valid identifiers. </param>
		// Token: 0x06003EAB RID: 16043
		protected abstract bool IsValidIdentifier(string value);

		/// <summary>Throws an exception if the specified string is not a valid identifier.</summary>
		/// <param name="value">The identifier to test for validity as an identifier. </param>
		/// <exception cref="T:System.ArgumentException">If the specified identifier is invalid or conflicts with reserved or language keywords. </exception>
		// Token: 0x06003EAC RID: 16044 RVA: 0x000DDC10 File Offset: 0x000DBE10
		protected virtual void ValidateIdentifier(string value)
		{
			if (!this.IsValidIdentifier(value))
			{
				throw new ArgumentException(global::SR.Format("Identifier '{0}' is not valid.", value));
			}
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <returns>The escaped identifier for the value.</returns>
		/// <param name="value">The string to create an escaped identifier for. </param>
		// Token: 0x06003EAD RID: 16045
		protected abstract string CreateEscapedIdentifier(string value);

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <returns>A valid identifier for the value.</returns>
		/// <param name="value">A string to create a valid identifier for. </param>
		// Token: 0x06003EAE RID: 16046
		protected abstract string CreateValidIdentifier(string value);

		/// <summary>Gets the name of the specified data type.</summary>
		/// <returns>The name of the data type reference.</returns>
		/// <param name="value">The type whose name will be returned. </param>
		// Token: 0x06003EAF RID: 16047
		protected abstract string GetTypeOutput(CodeTypeReference value);

		/// <summary>Converts the specified string by formatting it with escape codes.</summary>
		/// <returns>The converted string.</returns>
		/// <param name="value">The string to convert. </param>
		// Token: 0x06003EB0 RID: 16048
		protected abstract string QuoteSnippetString(string value);

		/// <summary>Gets a value indicating whether the specified string is a valid identifier.</summary>
		/// <returns>true if the specified string is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The string to test for validity. </param>
		// Token: 0x06003EB1 RID: 16049 RVA: 0x0001A059 File Offset: 0x00018259
		public static bool IsValidLanguageIndependentIdentifier(string value)
		{
			return CSharpHelpers.IsValidTypeNameOrIdentifier(value, false);
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000DDC2C File Offset: 0x000DBE2C
		internal static bool IsValidLanguageIndependentTypeName(string value)
		{
			return CSharpHelpers.IsValidTypeNameOrIdentifier(value, true);
		}

		/// <summary>Attempts to validate each identifier field contained in the specified <see cref="T:System.CodeDom.CodeObject" /> or <see cref="N:System.CodeDom" /> tree.</summary>
		/// <param name="e">An object to test for invalid identifiers. </param>
		/// <exception cref="T:System.ArgumentException">The specified <see cref="T:System.CodeDom.CodeObject" /> contains an invalid identifier. </exception>
		// Token: 0x06003EB3 RID: 16051 RVA: 0x000DDC35 File Offset: 0x000DBE35
		public static void ValidateIdentifiers(CodeObject e)
		{
			new CodeValidator().ValidateIdentifiers(e);
		}

		// Token: 0x04002E29 RID: 11817
		private const int ParameterMultilineThreshold = 15;

		// Token: 0x04002E2A RID: 11818
		private ExposedTabStringIndentedTextWriter _output;

		// Token: 0x04002E2B RID: 11819
		private CodeGeneratorOptions _options;

		// Token: 0x04002E2C RID: 11820
		private CodeTypeDeclaration _currentClass;

		// Token: 0x04002E2D RID: 11821
		private CodeTypeMember _currentMember;

		// Token: 0x04002E2E RID: 11822
		private bool _inNestedBinary;
	}
}
