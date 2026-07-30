using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	// Token: 0x020007A9 RID: 1961
	internal sealed class CodeValidator
	{
		// Token: 0x06003EC4 RID: 16068 RVA: 0x000DDDC0 File Offset: 0x000DBFC0
		internal void ValidateIdentifiers(CodeObject e)
		{
			if (e is CodeCompileUnit)
			{
				this.ValidateCodeCompileUnit((CodeCompileUnit)e);
				return;
			}
			if (e is CodeComment)
			{
				this.ValidateComment((CodeComment)e);
				return;
			}
			if (e is CodeExpression)
			{
				this.ValidateExpression((CodeExpression)e);
				return;
			}
			if (e is CodeNamespace)
			{
				this.ValidateNamespace((CodeNamespace)e);
				return;
			}
			if (e is CodeNamespaceImport)
			{
				CodeValidator.ValidateNamespaceImport((CodeNamespaceImport)e);
				return;
			}
			if (e is CodeStatement)
			{
				this.ValidateStatement((CodeStatement)e);
				return;
			}
			if (e is CodeTypeMember)
			{
				this.ValidateTypeMember((CodeTypeMember)e);
				return;
			}
			if (e is CodeTypeReference)
			{
				CodeValidator.ValidateTypeReference((CodeTypeReference)e);
				return;
			}
			if (e is CodeDirective)
			{
				CodeValidator.ValidateCodeDirective((CodeDirective)e);
				return;
			}
			throw new ArgumentException(global::SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x000DDEA8 File Offset: 0x000DC0A8
		private void ValidateTypeMember(CodeTypeMember e)
		{
			this.ValidateCommentStatements(e.Comments);
			CodeValidator.ValidateCodeDirectives(e.StartDirectives);
			CodeValidator.ValidateCodeDirectives(e.EndDirectives);
			if (e.LinePragma != null)
			{
				this.ValidateLinePragmaStart(e.LinePragma);
			}
			if (e is CodeMemberEvent)
			{
				this.ValidateEvent((CodeMemberEvent)e);
				return;
			}
			if (e is CodeMemberField)
			{
				this.ValidateField((CodeMemberField)e);
				return;
			}
			if (e is CodeMemberMethod)
			{
				this.ValidateMemberMethod((CodeMemberMethod)e);
				return;
			}
			if (e is CodeMemberProperty)
			{
				this.ValidateProperty((CodeMemberProperty)e);
				return;
			}
			if (e is CodeSnippetTypeMember)
			{
				this.ValidateSnippetMember((CodeSnippetTypeMember)e);
				return;
			}
			if (e is CodeTypeDeclaration)
			{
				this.ValidateTypeDeclaration((CodeTypeDeclaration)e);
				return;
			}
			throw new ArgumentException(global::SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x000DDF88 File Offset: 0x000DC188
		private void ValidateCodeCompileUnit(CodeCompileUnit e)
		{
			CodeValidator.ValidateCodeDirectives(e.StartDirectives);
			CodeValidator.ValidateCodeDirectives(e.EndDirectives);
			if (e is CodeSnippetCompileUnit)
			{
				this.ValidateSnippetCompileUnit((CodeSnippetCompileUnit)e);
				return;
			}
			this.ValidateCompileUnitStart(e);
			this.ValidateNamespaces(e);
			this.ValidateCompileUnitEnd(e);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x000DDFD5 File Offset: 0x000DC1D5
		private void ValidateSnippetCompileUnit(CodeSnippetCompileUnit e)
		{
			if (e.LinePragma != null)
			{
				this.ValidateLinePragmaStart(e.LinePragma);
			}
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x000DDFEB File Offset: 0x000DC1EB
		private void ValidateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.AssemblyCustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.AssemblyCustomAttributes);
			}
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidateCompileUnitEnd(CodeCompileUnit e)
		{
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x000DE008 File Offset: 0x000DC208
		private void ValidateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				this.ValidateNamespace(codeNamespace);
			}
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x000DE064 File Offset: 0x000DC264
		private void ValidateNamespace(CodeNamespace e)
		{
			this.ValidateCommentStatements(e.Comments);
			CodeValidator.ValidateNamespaceStart(e);
			this.ValidateNamespaceImports(e);
			this.ValidateTypes(e);
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x000DE086 File Offset: 0x000DC286
		private static void ValidateNamespaceStart(CodeNamespace e)
		{
			if (!string.IsNullOrEmpty(e.Name))
			{
				CodeValidator.ValidateTypeName(e, "Name", e.Name);
			}
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x000DE0A8 File Offset: 0x000DC2A8
		private void ValidateNamespaceImports(CodeNamespace e)
		{
			foreach (object obj in e.Imports)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.LinePragma != null)
				{
					this.ValidateLinePragmaStart(codeNamespaceImport.LinePragma);
				}
				CodeValidator.ValidateNamespaceImport(codeNamespaceImport);
			}
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x000DE114 File Offset: 0x000DC314
		private static void ValidateNamespaceImport(CodeNamespaceImport e)
		{
			CodeValidator.ValidateTypeName(e, "Namespace", e.Namespace);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x000DE128 File Offset: 0x000DC328
		private void ValidateAttributes(CodeAttributeDeclarationCollection attributes)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			foreach (object obj in attributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				CodeValidator.ValidateTypeName(codeAttributeDeclaration, "Name", codeAttributeDeclaration.Name);
				CodeValidator.ValidateTypeReference(codeAttributeDeclaration.AttributeType);
				foreach (object obj2 in codeAttributeDeclaration.Arguments)
				{
					CodeAttributeArgument codeAttributeArgument = (CodeAttributeArgument)obj2;
					this.ValidateAttributeArgument(codeAttributeArgument);
				}
			}
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x000DE1E8 File Offset: 0x000DC3E8
		private void ValidateAttributeArgument(CodeAttributeArgument arg)
		{
			if (!string.IsNullOrEmpty(arg.Name))
			{
				CodeValidator.ValidateIdentifier(arg, "Name", arg.Name);
			}
			this.ValidateExpression(arg.Value);
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000DE214 File Offset: 0x000DC414
		private void ValidateTypes(CodeNamespace e)
		{
			foreach (object obj in e.Types)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
				this.ValidateTypeDeclaration(codeTypeDeclaration);
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000DE270 File Offset: 0x000DC470
		private void ValidateTypeDeclaration(CodeTypeDeclaration e)
		{
			CodeTypeDeclaration currentClass = this._currentClass;
			this._currentClass = e;
			this.ValidateTypeStart(e);
			this.ValidateTypeParameters(e.TypeParameters);
			this.ValidateTypeMembers(e);
			CodeValidator.ValidateTypeReferences(e.BaseTypes);
			this._currentClass = currentClass;
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000DE2B8 File Offset: 0x000DC4B8
		private void ValidateTypeMembers(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				this.ValidateTypeMember(codeTypeMember);
			}
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000DE314 File Offset: 0x000DC514
		private void ValidateTypeParameters(CodeTypeParameterCollection parameters)
		{
			for (int i = 0; i < parameters.Count; i++)
			{
				this.ValidateTypeParameter(parameters[i]);
			}
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000DE33F File Offset: 0x000DC53F
		private void ValidateTypeParameter(CodeTypeParameter e)
		{
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			CodeValidator.ValidateTypeReferences(e.Constraints);
			this.ValidateAttributes(e.CustomAttributes);
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000DE36C File Offset: 0x000DC56C
		private void ValidateField(CodeMemberField e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			if (!this.IsCurrentEnum)
			{
				CodeValidator.ValidateTypeReference(e.Type);
			}
			if (e.InitExpression != null)
			{
				this.ValidateExpression(e.InitExpression);
			}
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000DE3CC File Offset: 0x000DC5CC
		private void ValidateConstructor(CodeConstructor e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			this.ValidateParameters(e.Parameters);
			CodeExpressionCollection baseConstructorArgs = e.BaseConstructorArgs;
			CodeExpressionCollection chainedConstructorArgs = e.ChainedConstructorArgs;
			if (baseConstructorArgs.Count > 0)
			{
				this.ValidateExpressionList(baseConstructorArgs);
			}
			if (chainedConstructorArgs.Count > 0)
			{
				this.ValidateExpressionList(chainedConstructorArgs);
			}
			this.ValidateStatements(e.Statements);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000DE43C File Offset: 0x000DC63C
		private void ValidateProperty(CodeMemberProperty e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateTypeReference(e.Type);
			CodeValidator.ValidateTypeReferences(e.ImplementationTypes);
			if (e.PrivateImplementationType != null && !this.IsCurrentInterface)
			{
				CodeValidator.ValidateTypeReference(e.PrivateImplementationType);
			}
			if (e.Parameters.Count > 0 && string.Equals(e.Name, "Item", StringComparison.OrdinalIgnoreCase))
			{
				this.ValidateParameters(e.Parameters);
			}
			else
			{
				CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			}
			if (e.HasGet && !this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.ValidateStatements(e.GetStatements);
			}
			if (e.HasSet && !this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.ValidateStatements(e.SetStatements);
			}
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x000DE524 File Offset: 0x000DC724
		private void ValidateMemberMethod(CodeMemberMethod e)
		{
			this.ValidateCommentStatements(e.Comments);
			if (e.LinePragma != null)
			{
				this.ValidateLinePragmaStart(e.LinePragma);
			}
			this.ValidateTypeParameters(e.TypeParameters);
			CodeValidator.ValidateTypeReferences(e.ImplementationTypes);
			if (e is CodeEntryPointMethod)
			{
				this.ValidateStatements(((CodeEntryPointMethod)e).Statements);
				return;
			}
			if (e is CodeConstructor)
			{
				this.ValidateConstructor((CodeConstructor)e);
				return;
			}
			if (e is CodeTypeConstructor)
			{
				this.ValidateTypeConstructor((CodeTypeConstructor)e);
				return;
			}
			this.ValidateMethod(e);
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x000DE5B3 File Offset: 0x000DC7B3
		private void ValidateTypeConstructor(CodeTypeConstructor e)
		{
			this.ValidateStatements(e.Statements);
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x000DE5C4 File Offset: 0x000DC7C4
		private void ValidateMethod(CodeMemberMethod e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			if (e.ReturnTypeCustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.ReturnTypeCustomAttributes);
			}
			CodeValidator.ValidateTypeReference(e.ReturnType);
			if (e.PrivateImplementationType != null)
			{
				CodeValidator.ValidateTypeReference(e.PrivateImplementationType);
			}
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			this.ValidateParameters(e.Parameters);
			if (!this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.ValidateStatements(e.Statements);
			}
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidateSnippetMember(CodeSnippetTypeMember e)
		{
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x000DE660 File Offset: 0x000DC860
		private void ValidateTypeStart(CodeTypeDeclaration e)
		{
			this.ValidateCommentStatements(e.Comments);
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			if (this.IsCurrentDelegate)
			{
				CodeTypeDelegate codeTypeDelegate = (CodeTypeDelegate)e;
				CodeValidator.ValidateTypeReference(codeTypeDelegate.ReturnType);
				this.ValidateParameters(codeTypeDelegate.Parameters);
				return;
			}
			foreach (object obj in e.BaseTypes)
			{
				CodeValidator.ValidateTypeReference((CodeTypeReference)obj);
			}
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x000DE714 File Offset: 0x000DC914
		private void ValidateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement codeCommentStatement = (CodeCommentStatement)obj;
				this.ValidateCommentStatement(codeCommentStatement);
			}
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x000DE768 File Offset: 0x000DC968
		private void ValidateCommentStatement(CodeCommentStatement e)
		{
			this.ValidateComment(e.Comment);
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidateComment(CodeComment e)
		{
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x000DE778 File Offset: 0x000DC978
		private void ValidateStatement(CodeStatement e)
		{
			CodeValidator.ValidateCodeDirectives(e.StartDirectives);
			CodeValidator.ValidateCodeDirectives(e.EndDirectives);
			if (e is CodeCommentStatement)
			{
				this.ValidateCommentStatement((CodeCommentStatement)e);
				return;
			}
			if (e is CodeMethodReturnStatement)
			{
				this.ValidateMethodReturnStatement((CodeMethodReturnStatement)e);
				return;
			}
			if (e is CodeConditionStatement)
			{
				this.ValidateConditionStatement((CodeConditionStatement)e);
				return;
			}
			if (e is CodeTryCatchFinallyStatement)
			{
				this.ValidateTryCatchFinallyStatement((CodeTryCatchFinallyStatement)e);
				return;
			}
			if (e is CodeAssignStatement)
			{
				this.ValidateAssignStatement((CodeAssignStatement)e);
				return;
			}
			if (e is CodeExpressionStatement)
			{
				this.ValidateExpressionStatement((CodeExpressionStatement)e);
				return;
			}
			if (e is CodeIterationStatement)
			{
				this.ValidateIterationStatement((CodeIterationStatement)e);
				return;
			}
			if (e is CodeThrowExceptionStatement)
			{
				this.ValidateThrowExceptionStatement((CodeThrowExceptionStatement)e);
				return;
			}
			if (e is CodeSnippetStatement)
			{
				this.ValidateSnippetStatement((CodeSnippetStatement)e);
				return;
			}
			if (e is CodeVariableDeclarationStatement)
			{
				this.ValidateVariableDeclarationStatement((CodeVariableDeclarationStatement)e);
				return;
			}
			if (e is CodeAttachEventStatement)
			{
				this.ValidateAttachEventStatement((CodeAttachEventStatement)e);
				return;
			}
			if (e is CodeRemoveEventStatement)
			{
				this.ValidateRemoveEventStatement((CodeRemoveEventStatement)e);
				return;
			}
			if (e is CodeGotoStatement)
			{
				CodeValidator.ValidateGotoStatement((CodeGotoStatement)e);
				return;
			}
			if (e is CodeLabeledStatement)
			{
				this.ValidateLabeledStatement((CodeLabeledStatement)e);
				return;
			}
			throw new ArgumentException(global::SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x000DE8E0 File Offset: 0x000DCAE0
		private void ValidateStatements(CodeStatementCollection stmts)
		{
			foreach (object obj in stmts)
			{
				CodeStatement codeStatement = (CodeStatement)obj;
				this.ValidateStatement(codeStatement);
			}
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x000DE934 File Offset: 0x000DCB34
		private void ValidateExpressionStatement(CodeExpressionStatement e)
		{
			this.ValidateExpression(e.Expression);
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x000DE942 File Offset: 0x000DCB42
		private void ValidateIterationStatement(CodeIterationStatement e)
		{
			this.ValidateStatement(e.InitStatement);
			this.ValidateExpression(e.TestExpression);
			this.ValidateStatement(e.IncrementStatement);
			this.ValidateStatements(e.Statements);
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x000DE974 File Offset: 0x000DCB74
		private void ValidateThrowExceptionStatement(CodeThrowExceptionStatement e)
		{
			if (e.ToThrow != null)
			{
				this.ValidateExpression(e.ToThrow);
			}
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x000DE98A File Offset: 0x000DCB8A
		private void ValidateMethodReturnStatement(CodeMethodReturnStatement e)
		{
			if (e.Expression != null)
			{
				this.ValidateExpression(e.Expression);
			}
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x000DE9A0 File Offset: 0x000DCBA0
		private void ValidateConditionStatement(CodeConditionStatement e)
		{
			this.ValidateExpression(e.Condition);
			this.ValidateStatements(e.TrueStatements);
			if (e.FalseStatements.Count > 0)
			{
				this.ValidateStatements(e.FalseStatements);
			}
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x000DE9D4 File Offset: 0x000DCBD4
		private void ValidateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e)
		{
			this.ValidateStatements(e.TryStatements);
			CodeCatchClauseCollection catchClauses = e.CatchClauses;
			if (catchClauses.Count > 0)
			{
				foreach (object obj in catchClauses)
				{
					CodeCatchClause codeCatchClause = (CodeCatchClause)obj;
					CodeValidator.ValidateTypeReference(codeCatchClause.CatchExceptionType);
					CodeValidator.ValidateIdentifier(codeCatchClause, "LocalName", codeCatchClause.LocalName);
					this.ValidateStatements(codeCatchClause.Statements);
				}
			}
			CodeStatementCollection finallyStatements = e.FinallyStatements;
			if (finallyStatements.Count > 0)
			{
				this.ValidateStatements(finallyStatements);
			}
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x000DEA80 File Offset: 0x000DCC80
		private void ValidateAssignStatement(CodeAssignStatement e)
		{
			this.ValidateExpression(e.Left);
			this.ValidateExpression(e.Right);
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x000DEA9A File Offset: 0x000DCC9A
		private void ValidateAttachEventStatement(CodeAttachEventStatement e)
		{
			this.ValidateEventReferenceExpression(e.Event);
			this.ValidateExpression(e.Listener);
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x000DEAB4 File Offset: 0x000DCCB4
		private void ValidateRemoveEventStatement(CodeRemoveEventStatement e)
		{
			this.ValidateEventReferenceExpression(e.Event);
			this.ValidateExpression(e.Listener);
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x000DEACE File Offset: 0x000DCCCE
		private static void ValidateGotoStatement(CodeGotoStatement e)
		{
			CodeValidator.ValidateIdentifier(e, "Label", e.Label);
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x000DEAE1 File Offset: 0x000DCCE1
		private void ValidateLabeledStatement(CodeLabeledStatement e)
		{
			CodeValidator.ValidateIdentifier(e, "Label", e.Label);
			if (e.Statement != null)
			{
				this.ValidateStatement(e.Statement);
			}
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x000DEB08 File Offset: 0x000DCD08
		private void ValidateVariableDeclarationStatement(CodeVariableDeclarationStatement e)
		{
			CodeValidator.ValidateTypeReference(e.Type);
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			if (e.InitExpression != null)
			{
				this.ValidateExpression(e.InitExpression);
			}
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidateLinePragmaStart(CodeLinePragma e)
		{
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x000DEB3C File Offset: 0x000DCD3C
		private void ValidateEvent(CodeMemberEvent e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			if (e.PrivateImplementationType != null)
			{
				CodeValidator.ValidateTypeReference(e.Type);
				CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			}
			CodeValidator.ValidateTypeReferences(e.ImplementationTypes);
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x000DEB94 File Offset: 0x000DCD94
		private void ValidateParameters(CodeParameterDeclarationExpressionCollection parameters)
		{
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				this.ValidateParameterDeclarationExpression(codeParameterDeclarationExpression);
			}
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidateSnippetStatement(CodeSnippetStatement e)
		{
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000DEBE8 File Offset: 0x000DCDE8
		private void ValidateExpressionList(CodeExpressionCollection expressions)
		{
			foreach (object obj in expressions)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				this.ValidateExpression(codeExpression);
			}
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x000DEC3C File Offset: 0x000DCE3C
		private static void ValidateTypeReference(CodeTypeReference e)
		{
			CodeValidator.ValidateTypeName(e, "BaseType", e.BaseType);
			CodeValidator.ValidateArity(e);
			CodeValidator.ValidateTypeReferences(e.TypeArguments);
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x000DEC60 File Offset: 0x000DCE60
		private static void ValidateTypeReferences(CodeTypeReferenceCollection refs)
		{
			for (int i = 0; i < refs.Count; i++)
			{
				CodeValidator.ValidateTypeReference(refs[i]);
			}
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x000DEC8C File Offset: 0x000DCE8C
		private static void ValidateArity(CodeTypeReference e)
		{
			string baseType = e.BaseType;
			int num = 0;
			for (int i = 0; i < baseType.Length; i++)
			{
				if (baseType[i] == '`')
				{
					i++;
					int num2 = 0;
					while (i < baseType.Length && baseType[i] >= '0' && baseType[i] <= '9')
					{
						num2 = num2 * 10 + (int)(baseType[i] - '0');
						i++;
					}
					num += num2;
				}
			}
			if (num != e.TypeArguments.Count && e.TypeArguments.Count != 0)
			{
				throw new ArgumentException(global::SR.Format("The total arity specified in '{0}' does not match the number of TypeArguments supplied.  There were '{1}' TypeArguments supplied.", baseType, e.TypeArguments.Count));
			}
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000DED39 File Offset: 0x000DCF39
		private static void ValidateTypeName(object e, string propertyName, string typeName)
		{
			if (!CodeGenerator.IsValidLanguageIndependentTypeName(typeName))
			{
				throw new ArgumentException(global::SR.Format("The type name:\"{0}\" on the property:\"{1}\" of type:\"{2}\" is not a valid language-independent type name.", typeName, propertyName, e.GetType().FullName), "typeName");
			}
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000DED65 File Offset: 0x000DCF65
		private static void ValidateIdentifier(object e, string propertyName, string identifier)
		{
			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(identifier))
			{
				throw new ArgumentException(global::SR.Format("The identifier:\"{0}\" on the property:\"{1}\" of type:\"{2}\" is not a valid language-independent identifier name. Check to see if CodeGenerator.IsValidLanguageIndependentIdentifier allows the identifier name.", identifier, propertyName, e.GetType().FullName), "identifier");
			}
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x000DED94 File Offset: 0x000DCF94
		private void ValidateExpression(CodeExpression e)
		{
			if (e is CodeArrayCreateExpression)
			{
				this.ValidateArrayCreateExpression((CodeArrayCreateExpression)e);
				return;
			}
			if (e is CodeBaseReferenceExpression)
			{
				this.ValidateBaseReferenceExpression((CodeBaseReferenceExpression)e);
				return;
			}
			if (e is CodeBinaryOperatorExpression)
			{
				this.ValidateBinaryOperatorExpression((CodeBinaryOperatorExpression)e);
				return;
			}
			if (e is CodeCastExpression)
			{
				this.ValidateCastExpression((CodeCastExpression)e);
				return;
			}
			if (e is CodeDefaultValueExpression)
			{
				CodeValidator.ValidateDefaultValueExpression((CodeDefaultValueExpression)e);
				return;
			}
			if (e is CodeDelegateCreateExpression)
			{
				this.ValidateDelegateCreateExpression((CodeDelegateCreateExpression)e);
				return;
			}
			if (e is CodeFieldReferenceExpression)
			{
				this.ValidateFieldReferenceExpression((CodeFieldReferenceExpression)e);
				return;
			}
			if (e is CodeArgumentReferenceExpression)
			{
				CodeValidator.ValidateArgumentReferenceExpression((CodeArgumentReferenceExpression)e);
				return;
			}
			if (e is CodeVariableReferenceExpression)
			{
				CodeValidator.ValidateVariableReferenceExpression((CodeVariableReferenceExpression)e);
				return;
			}
			if (e is CodeIndexerExpression)
			{
				this.ValidateIndexerExpression((CodeIndexerExpression)e);
				return;
			}
			if (e is CodeArrayIndexerExpression)
			{
				this.ValidateArrayIndexerExpression((CodeArrayIndexerExpression)e);
				return;
			}
			if (e is CodeSnippetExpression)
			{
				this.ValidateSnippetExpression((CodeSnippetExpression)e);
				return;
			}
			if (e is CodeMethodInvokeExpression)
			{
				this.ValidateMethodInvokeExpression((CodeMethodInvokeExpression)e);
				return;
			}
			if (e is CodeMethodReferenceExpression)
			{
				this.ValidateMethodReferenceExpression((CodeMethodReferenceExpression)e);
				return;
			}
			if (e is CodeEventReferenceExpression)
			{
				this.ValidateEventReferenceExpression((CodeEventReferenceExpression)e);
				return;
			}
			if (e is CodeDelegateInvokeExpression)
			{
				this.ValidateDelegateInvokeExpression((CodeDelegateInvokeExpression)e);
				return;
			}
			if (e is CodeObjectCreateExpression)
			{
				this.ValidateObjectCreateExpression((CodeObjectCreateExpression)e);
				return;
			}
			if (e is CodeParameterDeclarationExpression)
			{
				this.ValidateParameterDeclarationExpression((CodeParameterDeclarationExpression)e);
				return;
			}
			if (e is CodeDirectionExpression)
			{
				this.ValidateDirectionExpression((CodeDirectionExpression)e);
				return;
			}
			if (e is CodePrimitiveExpression)
			{
				this.ValidatePrimitiveExpression((CodePrimitiveExpression)e);
				return;
			}
			if (e is CodePropertyReferenceExpression)
			{
				this.ValidatePropertyReferenceExpression((CodePropertyReferenceExpression)e);
				return;
			}
			if (e is CodePropertySetValueReferenceExpression)
			{
				this.ValidatePropertySetValueReferenceExpression((CodePropertySetValueReferenceExpression)e);
				return;
			}
			if (e is CodeThisReferenceExpression)
			{
				this.ValidateThisReferenceExpression((CodeThisReferenceExpression)e);
				return;
			}
			if (e is CodeTypeReferenceExpression)
			{
				CodeValidator.ValidateTypeReference(((CodeTypeReferenceExpression)e).Type);
				return;
			}
			if (e is CodeTypeOfExpression)
			{
				CodeValidator.ValidateTypeOfExpression((CodeTypeOfExpression)e);
				return;
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			throw new ArgumentException(global::SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x000DEFDC File Offset: 0x000DD1DC
		private void ValidateArrayCreateExpression(CodeArrayCreateExpression e)
		{
			CodeValidator.ValidateTypeReference(e.CreateType);
			CodeExpressionCollection initializers = e.Initializers;
			if (initializers.Count > 0)
			{
				this.ValidateExpressionList(initializers);
				return;
			}
			if (e.SizeExpression != null)
			{
				this.ValidateExpression(e.SizeExpression);
			}
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidateBaseReferenceExpression(CodeBaseReferenceExpression e)
		{
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x000DF020 File Offset: 0x000DD220
		private void ValidateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			this.ValidateExpression(e.Left);
			this.ValidateExpression(e.Right);
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x000DF03A File Offset: 0x000DD23A
		private void ValidateCastExpression(CodeCastExpression e)
		{
			CodeValidator.ValidateTypeReference(e.TargetType);
			this.ValidateExpression(e.Expression);
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x000DF053 File Offset: 0x000DD253
		private static void ValidateDefaultValueExpression(CodeDefaultValueExpression e)
		{
			CodeValidator.ValidateTypeReference(e.Type);
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x000DF060 File Offset: 0x000DD260
		private void ValidateDelegateCreateExpression(CodeDelegateCreateExpression e)
		{
			CodeValidator.ValidateTypeReference(e.DelegateType);
			this.ValidateExpression(e.TargetObject);
			CodeValidator.ValidateIdentifier(e, "MethodName", e.MethodName);
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x000DF08A File Offset: 0x000DD28A
		private void ValidateFieldReferenceExpression(CodeFieldReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "FieldName", e.FieldName);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x000DF0B1 File Offset: 0x000DD2B1
		private static void ValidateArgumentReferenceExpression(CodeArgumentReferenceExpression e)
		{
			CodeValidator.ValidateIdentifier(e, "ParameterName", e.ParameterName);
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x000DF0C4 File Offset: 0x000DD2C4
		private static void ValidateVariableReferenceExpression(CodeVariableReferenceExpression e)
		{
			CodeValidator.ValidateIdentifier(e, "VariableName", e.VariableName);
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x000DF0D8 File Offset: 0x000DD2D8
		private void ValidateIndexerExpression(CodeIndexerExpression e)
		{
			this.ValidateExpression(e.TargetObject);
			foreach (object obj in e.Indices)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				this.ValidateExpression(codeExpression);
			}
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x000DF140 File Offset: 0x000DD340
		private void ValidateArrayIndexerExpression(CodeArrayIndexerExpression e)
		{
			this.ValidateExpression(e.TargetObject);
			foreach (object obj in e.Indices)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				this.ValidateExpression(codeExpression);
			}
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidateSnippetExpression(CodeSnippetExpression e)
		{
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x000DF1A8 File Offset: 0x000DD3A8
		private void ValidateMethodInvokeExpression(CodeMethodInvokeExpression e)
		{
			this.ValidateMethodReferenceExpression(e.Method);
			this.ValidateExpressionList(e.Parameters);
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x000DF1C2 File Offset: 0x000DD3C2
		private void ValidateMethodReferenceExpression(CodeMethodReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "MethodName", e.MethodName);
			CodeValidator.ValidateTypeReferences(e.TypeArguments);
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x000DF1F4 File Offset: 0x000DD3F4
		private void ValidateEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "EventName", e.EventName);
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x000DF21B File Offset: 0x000DD41B
		private void ValidateDelegateInvokeExpression(CodeDelegateInvokeExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			this.ValidateExpressionList(e.Parameters);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x000DF23D File Offset: 0x000DD43D
		private void ValidateObjectCreateExpression(CodeObjectCreateExpression e)
		{
			CodeValidator.ValidateTypeReference(e.CreateType);
			this.ValidateExpressionList(e.Parameters);
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x000DF256 File Offset: 0x000DD456
		private void ValidateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateTypeReference(e.Type);
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x000DF28E File Offset: 0x000DD48E
		private void ValidateDirectionExpression(CodeDirectionExpression e)
		{
			this.ValidateExpression(e.Expression);
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidatePrimitiveExpression(CodePrimitiveExpression e)
		{
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x000DF29C File Offset: 0x000DD49C
		private void ValidatePropertyReferenceExpression(CodePropertyReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "PropertyName", e.PropertyName);
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidatePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e)
		{
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x000027E8 File Offset: 0x000009E8
		private void ValidateThisReferenceExpression(CodeThisReferenceExpression e)
		{
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x000DF2C3 File Offset: 0x000DD4C3
		private static void ValidateTypeOfExpression(CodeTypeOfExpression e)
		{
			CodeValidator.ValidateTypeReference(e.Type);
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x000DF2D0 File Offset: 0x000DD4D0
		private static void ValidateCodeDirectives(CodeDirectiveCollection e)
		{
			for (int i = 0; i < e.Count; i++)
			{
				CodeValidator.ValidateCodeDirective(e[i]);
			}
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000DF2FC File Offset: 0x000DD4FC
		private static void ValidateCodeDirective(CodeDirective e)
		{
			if (e is CodeChecksumPragma)
			{
				CodeValidator.ValidateChecksumPragma((CodeChecksumPragma)e);
				return;
			}
			if (e is CodeRegionDirective)
			{
				CodeValidator.ValidateRegionDirective((CodeRegionDirective)e);
				return;
			}
			throw new ArgumentException(global::SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x000DF350 File Offset: 0x000DD550
		private static void ValidateChecksumPragma(CodeChecksumPragma e)
		{
			if (e.FileName.IndexOfAny(Path.GetInvalidPathChars()) != -1)
			{
				throw new ArgumentException(global::SR.Format("The CodeChecksumPragma file name '{0}' contains invalid path characters.", e.FileName));
			}
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x000DF37B File Offset: 0x000DD57B
		private static void ValidateRegionDirective(CodeRegionDirective e)
		{
			if (e.RegionText.IndexOfAny(CodeValidator.s_newLineChars) != -1)
			{
				throw new ArgumentException(global::SR.Format("The region directive '{0}' contains invalid characters.  RegionText cannot contain any new line characters.", e.RegionText));
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x000DF3A6 File Offset: 0x000DD5A6
		private bool IsCurrentInterface
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsInterface;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x000DF3CA File Offset: 0x000DD5CA
		private bool IsCurrentEnum
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsEnum;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x000DF3EE File Offset: 0x000DD5EE
		private bool IsCurrentDelegate
		{
			get
			{
				return this._currentClass != null && this._currentClass is CodeTypeDelegate;
			}
		}

		// Token: 0x04002E30 RID: 11824
		private static readonly char[] s_newLineChars = new char[] { '\r', '\n', '\u2028', '\u2029', '\u0085' };

		// Token: 0x04002E31 RID: 11825
		private CodeTypeDeclaration _currentClass;
	}
}
