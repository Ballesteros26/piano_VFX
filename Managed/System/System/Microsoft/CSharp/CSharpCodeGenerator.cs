using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microsoft.CSharp
{
	// Token: 0x020000E7 RID: 231
	internal sealed class CSharpCodeGenerator : ICodeCompiler, ICodeGenerator
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x000020EB File Offset: 0x000002EB
		internal CSharpCodeGenerator()
		{
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00013AE8 File Offset: 0x00011CE8
		internal CSharpCodeGenerator(IDictionary<string, string> providerOptions)
		{
			this._provOptions = providerOptions;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00013AF7 File Offset: 0x00011CF7
		private string FileExtension
		{
			get
			{
				return ".cs";
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00013AFE File Offset: 0x00011CFE
		private string CompilerName
		{
			get
			{
				return "csc.exe";
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00013B05 File Offset: 0x00011D05
		private string CurrentTypeName
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00013B20 File Offset: 0x00011D20
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x00013B2D File Offset: 0x00011D2D
		private int Indent
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00013B3B File Offset: 0x00011D3B
		private bool IsCurrentInterface
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsInterface;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00013B5F File Offset: 0x00011D5F
		private bool IsCurrentClass
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsClass;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00013B83 File Offset: 0x00011D83
		private bool IsCurrentStruct
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsStruct;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00013BA7 File Offset: 0x00011DA7
		private bool IsCurrentEnum
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsEnum;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00013BCB File Offset: 0x00011DCB
		private bool IsCurrentDelegate
		{
			get
			{
				return this._currentClass != null && this._currentClass is CodeTypeDelegate;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00013BE5 File Offset: 0x00011DE5
		private string NullToken
		{
			get
			{
				return "null";
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00013BEC File Offset: 0x00011DEC
		private CodeGeneratorOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00013BF4 File Offset: 0x00011DF4
		private TextWriter Output
		{
			get
			{
				return this._output;
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00013BFC File Offset: 0x00011DFC
		private string QuoteSnippetStringCStyle(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length + 5);
			Indentation indentation = new Indentation(this._output, this.Indent + 1);
			stringBuilder.Append('"');
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '"')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							stringBuilder.Append("\\t");
							break;
						case '\n':
							stringBuilder.Append("\\n");
							break;
						case '\v':
						case '\f':
							goto IL_0104;
						case '\r':
							stringBuilder.Append("\\r");
							break;
						default:
							if (c != '"')
							{
								goto IL_0104;
							}
							stringBuilder.Append("\\\"");
							break;
						}
					}
					else
					{
						stringBuilder.Append("\\0");
					}
				}
				else if (c <= '\\')
				{
					if (c != '\'')
					{
						if (c != '\\')
						{
							goto IL_0104;
						}
						stringBuilder.Append("\\\\");
					}
					else
					{
						stringBuilder.Append("\\'");
					}
				}
				else
				{
					if (c != '\u2028' && c != '\u2029')
					{
						goto IL_0104;
					}
					this.AppendEscapedChar(stringBuilder, value[i]);
				}
				IL_0112:
				if (i > 0 && i % 80 == 0)
				{
					if (char.IsHighSurrogate(value[i]) && i < value.Length - 1 && char.IsLowSurrogate(value[i + 1]))
					{
						stringBuilder.Append(value[++i]);
					}
					stringBuilder.Append("\" +");
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(indentation.IndentationString);
					stringBuilder.Append('"');
				}
				i++;
				continue;
				IL_0104:
				stringBuilder.Append(value[i]);
				goto IL_0112;
			}
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00013DB0 File Offset: 0x00011FB0
		private string QuoteSnippetStringVerbatimStyle(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length + 5);
			stringBuilder.Append("@\"");
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '"')
				{
					stringBuilder.Append("\"\"");
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00013E1E File Offset: 0x0001201E
		private string QuoteSnippetString(string value)
		{
			if (value.Length < 256 || value.Length > 1500 || value.IndexOf('\0') != -1)
			{
				return this.QuoteSnippetStringCStyle(value);
			}
			return this.QuoteSnippetStringVerbatimStyle(value);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00013E53 File Offset: 0x00012053
		private void ContinueOnNewLine(string st)
		{
			this.Output.WriteLine(st);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00013E61 File Offset: 0x00012061
		private void OutputIdentifier(string ident)
		{
			this.Output.Write(this.CreateEscapedIdentifier(ident));
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00013E75 File Offset: 0x00012075
		private void OutputType(CodeTypeReference typeRef)
		{
			this.Output.Write(this.GetTypeOutput(typeRef));
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00013E8C File Offset: 0x0001208C
		private void GenerateArrayCreateExpression(CodeArrayCreateExpression e)
		{
			this.Output.Write("new ");
			CodeExpressionCollection initializers = e.Initializers;
			if (initializers.Count > 0)
			{
				this.OutputType(e.CreateType);
				if (e.CreateType.ArrayRank == 0)
				{
					this.Output.Write("[]");
				}
				this.Output.WriteLine(" {");
				int num = this.Indent;
				this.Indent = num + 1;
				this.OutputExpressionList(initializers, true);
				num = this.Indent;
				this.Indent = num - 1;
				this.Output.Write('}');
				return;
			}
			this.Output.Write(this.GetBaseTypeOutput(e.CreateType, true));
			this.Output.Write('[');
			if (e.SizeExpression != null)
			{
				this.GenerateExpression(e.SizeExpression);
			}
			else
			{
				this.Output.Write(e.Size);
			}
			this.Output.Write(']');
			int nestedArrayDepth = e.CreateType.NestedArrayDepth;
			for (int i = 0; i < nestedArrayDepth - 1; i++)
			{
				this.Output.Write("[]");
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00013FAB File Offset: 0x000121AB
		private void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e)
		{
			this.Output.Write("base");
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00013FC0 File Offset: 0x000121C0
		private void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
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

		// Token: 0x060005BC RID: 1468 RVA: 0x00014084 File Offset: 0x00012284
		private void GenerateCastExpression(CodeCastExpression e)
		{
			this.Output.Write("((");
			this.OutputType(e.TargetType);
			this.Output.Write(")(");
			this.GenerateExpression(e.Expression);
			this.Output.Write("))");
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000140DC File Offset: 0x000122DC
		public void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
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

		// Token: 0x060005BE RID: 1470 RVA: 0x00014160 File Offset: 0x00012360
		private void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
			this.Output.Write("default(");
			this.OutputType(e.Type);
			this.Output.Write(')');
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001418C File Offset: 0x0001238C
		private void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e)
		{
			this.Output.Write("new ");
			this.OutputType(e.DelegateType);
			this.Output.Write('(');
			this.GenerateExpression(e.TargetObject);
			this.Output.Write('.');
			this.OutputIdentifier(e.MethodName);
			this.Output.Write(')');
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000141F4 File Offset: 0x000123F4
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

		// Token: 0x060005C1 RID: 1473 RVA: 0x00014308 File Offset: 0x00012508
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

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001441C File Offset: 0x0001261C
		private void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.FieldName);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001444B File Offset: 0x0001264B
		private void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e)
		{
			this.OutputIdentifier(e.ParameterName);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00014459 File Offset: 0x00012659
		private void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e)
		{
			this.OutputIdentifier(e.VariableName);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00014468 File Offset: 0x00012668
		private void GenerateIndexerExpression(CodeIndexerExpression e)
		{
			this.GenerateExpression(e.TargetObject);
			this.Output.Write('[');
			bool flag = true;
			foreach (object obj in e.Indices)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				this.GenerateExpression(codeExpression);
			}
			this.Output.Write(']');
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00014500 File Offset: 0x00012700
		private void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e)
		{
			this.GenerateExpression(e.TargetObject);
			this.Output.Write('[');
			bool flag = true;
			foreach (object obj in e.Indices)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				this.GenerateExpression(codeExpression);
			}
			this.Output.Write(']');
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00014598 File Offset: 0x00012798
		private void GenerateSnippetCompileUnit(CodeSnippetCompileUnit e)
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

		// Token: 0x060005C8 RID: 1480 RVA: 0x00014604 File Offset: 0x00012804
		private void GenerateSnippetExpression(CodeSnippetExpression e)
		{
			this.Output.Write(e.Value);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00014617 File Offset: 0x00012817
		private void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e)
		{
			this.GenerateMethodReferenceExpression(e.Method);
			this.Output.Write('(');
			this.OutputExpressionList(e.Parameters);
			this.Output.Write(')');
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001464C File Offset: 0x0001284C
		private void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				if (e.TargetObject is CodeBinaryOperatorExpression)
				{
					this.Output.Write('(');
					this.GenerateExpression(e.TargetObject);
					this.Output.Write(')');
				}
				else
				{
					this.GenerateExpression(e.TargetObject);
				}
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.MethodName);
			if (e.TypeArguments.Count > 0)
			{
				this.Output.Write(this.GetTypeArgumentsOutput(e.TypeArguments));
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000146E0 File Offset: 0x000128E0
		private bool GetUserData(CodeObject e, string property, bool defaultValue)
		{
			object obj = e.UserData[property];
			if (obj != null && obj is bool)
			{
				return (bool)obj;
			}
			return defaultValue;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00014710 File Offset: 0x00012910
		private void GenerateNamespace(CodeNamespace e)
		{
			this.GenerateCommentStatements(e.Comments);
			this.GenerateNamespaceStart(e);
			if (this.GetUserData(e, "GenerateImports", true))
			{
				this.GenerateNamespaceImports(e);
			}
			this.Output.WriteLine();
			this.GenerateTypes(e);
			this.GenerateNamespaceEnd(e);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00014760 File Offset: 0x00012960
		private void GenerateStatement(CodeStatement e)
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

		// Token: 0x060005CE RID: 1486 RVA: 0x00014950 File Offset: 0x00012B50
		private void GenerateStatements(CodeStatementCollection stmts)
		{
			foreach (object obj in stmts)
			{
				CodeStatement codeStatement = (CodeStatement)obj;
				((ICodeGenerator)this).GenerateCodeFromStatement(codeStatement, this._output.InnerWriter, this._options);
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000149B8 File Offset: 0x00012BB8
		private void GenerateNamespaceImports(CodeNamespace e)
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

		// Token: 0x060005D0 RID: 1488 RVA: 0x00014A3C File Offset: 0x00012C3C
		private void GenerateEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.EventName);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00014A6B File Offset: 0x00012C6B
		private void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
			}
			this.Output.Write('(');
			this.OutputExpressionList(e.Parameters);
			this.Output.Write(')');
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00014AA8 File Offset: 0x00012CA8
		private void GenerateObjectCreateExpression(CodeObjectCreateExpression e)
		{
			this.Output.Write("new ");
			this.OutputType(e.CreateType);
			this.Output.Write('(');
			this.OutputExpressionList(e.Parameters);
			this.Output.Write(')');
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00014AF8 File Offset: 0x00012CF8
		private void GeneratePrimitiveExpression(CodePrimitiveExpression e)
		{
			if (e.Value is char)
			{
				this.GeneratePrimitiveChar((char)e.Value);
				return;
			}
			if (e.Value is sbyte)
			{
				this.Output.Write(((sbyte)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is ushort)
			{
				this.Output.Write(((ushort)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is uint)
			{
				this.Output.Write(((uint)e.Value).ToString(CultureInfo.InvariantCulture));
				this.Output.Write('u');
				return;
			}
			if (e.Value is ulong)
			{
				this.Output.Write(((ulong)e.Value).ToString(CultureInfo.InvariantCulture));
				this.Output.Write("ul");
				return;
			}
			this.GeneratePrimitiveExpressionBase(e);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00014C0C File Offset: 0x00012E0C
		private void GeneratePrimitiveExpressionBase(CodePrimitiveExpression e)
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

		// Token: 0x060005D5 RID: 1493 RVA: 0x00014E0C File Offset: 0x0001300C
		private void GeneratePrimitiveChar(char c)
		{
			this.Output.Write('\'');
			if (c > '\'')
			{
				if (c <= '\u0084')
				{
					if (c == '\\')
					{
						this.Output.Write("\\\\");
						goto IL_0143;
					}
					if (c != '\u0084')
					{
						goto IL_0125;
					}
				}
				else if (c != '\u0085' && c != '\u2028' && c != '\u2029')
				{
					goto IL_0125;
				}
				this.AppendEscapedChar(null, c);
				goto IL_0143;
			}
			if (c <= '\r')
			{
				if (c == '\0')
				{
					this.Output.Write("\\0");
					goto IL_0143;
				}
				switch (c)
				{
				case '\t':
					this.Output.Write("\\t");
					goto IL_0143;
				case '\n':
					this.Output.Write("\\n");
					goto IL_0143;
				case '\r':
					this.Output.Write("\\r");
					goto IL_0143;
				}
			}
			else
			{
				if (c == '"')
				{
					this.Output.Write("\\\"");
					goto IL_0143;
				}
				if (c == '\'')
				{
					this.Output.Write("\\'");
					goto IL_0143;
				}
			}
			IL_0125:
			if (char.IsSurrogate(c))
			{
				this.AppendEscapedChar(null, c);
			}
			else
			{
				this.Output.Write(c);
			}
			IL_0143:
			this.Output.Write('\'');
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00014F6C File Offset: 0x0001316C
		private void AppendEscapedChar(StringBuilder b, char value)
		{
			int num;
			if (b == null)
			{
				this.Output.Write("\\u");
				TextWriter output = this.Output;
				num = (int)value;
				output.Write(num.ToString("X4", CultureInfo.InvariantCulture));
				return;
			}
			b.Append("\\u");
			num = (int)value;
			b.Append(num.ToString("X4", CultureInfo.InvariantCulture));
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00014FD1 File Offset: 0x000131D1
		private void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e)
		{
			this.Output.Write("value");
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00014FE3 File Offset: 0x000131E3
		private void GenerateThisReferenceExpression(CodeThisReferenceExpression e)
		{
			this.Output.Write("this");
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00014FF5 File Offset: 0x000131F5
		private void GenerateExpressionStatement(CodeExpressionStatement e)
		{
			this.GenerateExpression(e.Expression);
			if (!this._generatingForLoop)
			{
				this.Output.WriteLine(';');
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00015018 File Offset: 0x00013218
		private void GenerateIterationStatement(CodeIterationStatement e)
		{
			this._generatingForLoop = true;
			this.Output.Write("for (");
			this.GenerateStatement(e.InitStatement);
			this.Output.Write("; ");
			this.GenerateExpression(e.TestExpression);
			this.Output.Write("; ");
			this.GenerateStatement(e.IncrementStatement);
			this.Output.Write(')');
			this.OutputStartingBrace();
			this._generatingForLoop = false;
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.Statements);
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000150D3 File Offset: 0x000132D3
		private void GenerateThrowExceptionStatement(CodeThrowExceptionStatement e)
		{
			this.Output.Write("throw");
			if (e.ToThrow != null)
			{
				this.Output.Write(' ');
				this.GenerateExpression(e.ToThrow);
			}
			this.Output.WriteLine(';');
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00015114 File Offset: 0x00013314
		private void GenerateComment(CodeComment e)
		{
			string text = (e.DocComment ? "///" : "//");
			this.Output.Write(text);
			this.Output.Write(' ');
			string text2 = e.Text;
			for (int i = 0; i < text2.Length; i++)
			{
				if (text2[i] != '\0')
				{
					this.Output.Write(text2[i]);
					if (text2[i] == '\r')
					{
						if (i < text2.Length - 1 && text2[i + 1] == '\n')
						{
							this.Output.Write('\n');
							i++;
						}
						this._output.InternalOutputTabs();
						this.Output.Write(text);
					}
					else if (text2[i] == '\n')
					{
						this._output.InternalOutputTabs();
						this.Output.Write(text);
					}
					else if (text2[i] == '\u2028' || text2[i] == '\u2029' || text2[i] == '\u0085')
					{
						this.Output.Write(text);
					}
				}
			}
			this.Output.WriteLine();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001523D File Offset: 0x0001343D
		private void GenerateCommentStatement(CodeCommentStatement e)
		{
			if (e.Comment == null)
			{
				throw new ArgumentException(global::SR.Format("The 'Comment' property of the CodeCommentStatement '{0}' cannot be null.", "e"), "e");
			}
			this.GenerateComment(e.Comment);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00015270 File Offset: 0x00013470
		private void GenerateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement codeCommentStatement = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(codeCommentStatement);
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000152C4 File Offset: 0x000134C4
		private void GenerateMethodReturnStatement(CodeMethodReturnStatement e)
		{
			this.Output.Write("return");
			if (e.Expression != null)
			{
				this.Output.Write(' ');
				this.GenerateExpression(e.Expression);
			}
			this.Output.WriteLine(';');
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00015304 File Offset: 0x00013504
		private void GenerateConditionStatement(CodeConditionStatement e)
		{
			this.Output.Write("if (");
			this.GenerateExpression(e.Condition);
			this.Output.Write(')');
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.TrueStatements);
			num = this.Indent;
			this.Indent = num - 1;
			if (e.FalseStatements.Count > 0)
			{
				this.Output.Write('}');
				if (this.Options.ElseOnClosing)
				{
					this.Output.Write(' ');
				}
				else
				{
					this.Output.WriteLine();
				}
				this.Output.Write("else");
				this.OutputStartingBrace();
				num = this.Indent;
				this.Indent = num + 1;
				this.GenerateStatements(e.FalseStatements);
				num = this.Indent;
				this.Indent = num - 1;
			}
			this.Output.WriteLine('}');
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00015400 File Offset: 0x00013600
		private void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e)
		{
			this.Output.Write("try");
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.TryStatements);
			num = this.Indent;
			this.Indent = num - 1;
			CodeCatchClauseCollection catchClauses = e.CatchClauses;
			if (catchClauses.Count > 0)
			{
				foreach (object obj in catchClauses)
				{
					CodeCatchClause codeCatchClause = (CodeCatchClause)obj;
					this.Output.Write('}');
					if (this.Options.ElseOnClosing)
					{
						this.Output.Write(' ');
					}
					else
					{
						this.Output.WriteLine();
					}
					this.Output.Write("catch (");
					this.OutputType(codeCatchClause.CatchExceptionType);
					this.Output.Write(' ');
					this.OutputIdentifier(codeCatchClause.LocalName);
					this.Output.Write(')');
					this.OutputStartingBrace();
					num = this.Indent;
					this.Indent = num + 1;
					this.GenerateStatements(codeCatchClause.Statements);
					num = this.Indent;
					this.Indent = num - 1;
				}
			}
			CodeStatementCollection finallyStatements = e.FinallyStatements;
			if (finallyStatements.Count > 0)
			{
				this.Output.Write('}');
				if (this.Options.ElseOnClosing)
				{
					this.Output.Write(' ');
				}
				else
				{
					this.Output.WriteLine();
				}
				this.Output.Write("finally");
				this.OutputStartingBrace();
				num = this.Indent;
				this.Indent = num + 1;
				this.GenerateStatements(finallyStatements);
				num = this.Indent;
				this.Indent = num - 1;
			}
			this.Output.WriteLine('}');
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000155E8 File Offset: 0x000137E8
		private void GenerateAssignStatement(CodeAssignStatement e)
		{
			this.GenerateExpression(e.Left);
			this.Output.Write(" = ");
			this.GenerateExpression(e.Right);
			if (!this._generatingForLoop)
			{
				this.Output.WriteLine(';');
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00015627 File Offset: 0x00013827
		private void GenerateAttachEventStatement(CodeAttachEventStatement e)
		{
			this.GenerateEventReferenceExpression(e.Event);
			this.Output.Write(" += ");
			this.GenerateExpression(e.Listener);
			this.Output.WriteLine(';');
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001565E File Offset: 0x0001385E
		private void GenerateRemoveEventStatement(CodeRemoveEventStatement e)
		{
			this.GenerateEventReferenceExpression(e.Event);
			this.Output.Write(" -= ");
			this.GenerateExpression(e.Listener);
			this.Output.WriteLine(';');
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00015695 File Offset: 0x00013895
		private void GenerateSnippetStatement(CodeSnippetStatement e)
		{
			this.Output.WriteLine(e.Value);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000156A8 File Offset: 0x000138A8
		private void GenerateGotoStatement(CodeGotoStatement e)
		{
			this.Output.Write("goto ");
			this.Output.Write(e.Label);
			this.Output.WriteLine(';');
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000156D8 File Offset: 0x000138D8
		private void GenerateLabeledStatement(CodeLabeledStatement e)
		{
			int num = this.Indent;
			this.Indent = num - 1;
			this.Output.Write(e.Label);
			this.Output.WriteLine(':');
			num = this.Indent;
			this.Indent = num + 1;
			if (e.Statement != null)
			{
				this.GenerateStatement(e.Statement);
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00015738 File Offset: 0x00013938
		private void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e)
		{
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.InitExpression != null)
			{
				this.Output.Write(" = ");
				this.GenerateExpression(e.InitExpression);
			}
			if (!this._generatingForLoop)
			{
				this.Output.WriteLine(';');
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00015790 File Offset: 0x00013990
		private void GenerateLinePragmaStart(CodeLinePragma e)
		{
			this.Output.WriteLine();
			this.Output.Write("#line ");
			this.Output.Write(e.LineNumber);
			this.Output.Write(" \"");
			this.Output.Write(e.FileName);
			this.Output.Write('"');
			this.Output.WriteLine();
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00015802 File Offset: 0x00013A02
		private void GenerateLinePragmaEnd(CodeLinePragma e)
		{
			this.Output.WriteLine();
			this.Output.WriteLine("#line default");
			this.Output.WriteLine("#line hidden");
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00015830 File Offset: 0x00013A30
		private void GenerateEvent(CodeMemberEvent e, CodeTypeDeclaration c)
		{
			if (this.IsCurrentDelegate || this.IsCurrentEnum)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (e.PrivateImplementationType == null)
			{
				this.OutputMemberAccessModifier(e.Attributes);
			}
			this.Output.Write("event ");
			string text = e.Name;
			if (e.PrivateImplementationType != null)
			{
				text = this.GetBaseTypeOutput(e.PrivateImplementationType, false) + "." + text;
			}
			this.OutputTypeNamePair(e.Type, text);
			this.Output.WriteLine(';');
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000158D0 File Offset: 0x00013AD0
		private void GenerateExpression(CodeExpression e)
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

		// Token: 0x060005ED RID: 1517 RVA: 0x00015B18 File Offset: 0x00013D18
		private void GenerateField(CodeMemberField e)
		{
			if (this.IsCurrentDelegate || this.IsCurrentInterface)
			{
				return;
			}
			if (this.IsCurrentEnum)
			{
				if (e.CustomAttributes.Count > 0)
				{
					this.GenerateAttributes(e.CustomAttributes);
				}
				this.OutputIdentifier(e.Name);
				if (e.InitExpression != null)
				{
					this.Output.Write(" = ");
					this.GenerateExpression(e.InitExpression);
				}
				this.Output.WriteLine(',');
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			this.OutputVTableModifier(e.Attributes);
			this.OutputFieldScopeModifier(e.Attributes);
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.InitExpression != null)
			{
				this.Output.Write(" = ");
				this.GenerateExpression(e.InitExpression);
			}
			this.Output.WriteLine(';');
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00015C17 File Offset: 0x00013E17
		private void GenerateSnippetMember(CodeSnippetTypeMember e)
		{
			this.Output.Write(e.Text);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00015C2A File Offset: 0x00013E2A
		private void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes, null, true);
			}
			this.OutputDirection(e.Direction);
			this.OutputTypeNamePair(e.Type, e.Name);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00015C68 File Offset: 0x00013E68
		private void GenerateEntryPointMethod(CodeEntryPointMethod e, CodeTypeDeclaration c)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.Output.Write("public static ");
			this.OutputType(e.ReturnType);
			this.Output.Write(" Main()");
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.Statements);
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00015CFC File Offset: 0x00013EFC
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

		// Token: 0x060005F2 RID: 1522 RVA: 0x00015E48 File Offset: 0x00014048
		private void GenerateMethod(CodeMemberMethod e, CodeTypeDeclaration c)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct && !this.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (e.ReturnTypeCustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.ReturnTypeCustomAttributes, "return: ");
			}
			if (!this.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					this.OutputVTableModifier(e.Attributes);
					this.OutputMemberScopeModifier(e.Attributes);
				}
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			this.OutputType(e.ReturnType);
			this.Output.Write(' ');
			if (e.PrivateImplementationType != null)
			{
				this.Output.Write(this.GetBaseTypeOutput(e.PrivateImplementationType, false));
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			this.Output.Write('(');
			this.OutputParameters(e.Parameters);
			this.Output.Write(')');
			this.OutputTypeParameterConstraints(e.TypeParameters);
			if (!this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.OutputStartingBrace();
				int num = this.Indent;
				this.Indent = num + 1;
				this.GenerateStatements(e.Statements);
				num = this.Indent;
				this.Indent = num - 1;
				this.Output.WriteLine('}');
				return;
			}
			this.Output.WriteLine(';');
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00015FDC File Offset: 0x000141DC
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

		// Token: 0x060005F4 RID: 1524 RVA: 0x000160F0 File Offset: 0x000142F0
		private void GenerateProperty(CodeMemberProperty e, CodeTypeDeclaration c)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct && !this.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (!this.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					this.OutputVTableModifier(e.Attributes);
					this.OutputMemberScopeModifier(e.Attributes);
				}
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			this.OutputType(e.Type);
			this.Output.Write(' ');
			if (e.PrivateImplementationType != null && !this.IsCurrentInterface)
			{
				this.Output.Write(this.GetBaseTypeOutput(e.PrivateImplementationType, false));
				this.Output.Write('.');
			}
			if (e.Parameters.Count > 0 && string.Equals(e.Name, "Item", StringComparison.OrdinalIgnoreCase))
			{
				this.Output.Write("this[");
				this.OutputParameters(e.Parameters);
				this.Output.Write(']');
			}
			else
			{
				this.OutputIdentifier(e.Name);
			}
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			if (e.HasGet)
			{
				if (this.IsCurrentInterface || (e.Attributes & MemberAttributes.ScopeMask) == MemberAttributes.Abstract)
				{
					this.Output.WriteLine("get;");
				}
				else
				{
					this.Output.Write("get");
					this.OutputStartingBrace();
					num = this.Indent;
					this.Indent = num + 1;
					this.GenerateStatements(e.GetStatements);
					num = this.Indent;
					this.Indent = num - 1;
					this.Output.WriteLine('}');
				}
			}
			if (e.HasSet)
			{
				if (this.IsCurrentInterface || (e.Attributes & MemberAttributes.ScopeMask) == MemberAttributes.Abstract)
				{
					this.Output.WriteLine("set;");
				}
				else
				{
					this.Output.Write("set");
					this.OutputStartingBrace();
					num = this.Indent;
					this.Indent = num + 1;
					this.GenerateStatements(e.SetStatements);
					num = this.Indent;
					this.Indent = num - 1;
					this.Output.WriteLine('}');
				}
			}
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00016348 File Offset: 0x00014548
		private void GenerateSingleFloatValue(float s)
		{
			if (float.IsNaN(s))
			{
				this.Output.Write("float.NaN");
				return;
			}
			if (float.IsNegativeInfinity(s))
			{
				this.Output.Write("float.NegativeInfinity");
				return;
			}
			if (float.IsPositiveInfinity(s))
			{
				this.Output.Write("float.PositiveInfinity");
				return;
			}
			this.Output.Write(s.ToString(CultureInfo.InvariantCulture));
			this.Output.Write('F');
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000163C4 File Offset: 0x000145C4
		private void GenerateDoubleValue(double d)
		{
			if (double.IsNaN(d))
			{
				this.Output.Write("double.NaN");
				return;
			}
			if (double.IsNegativeInfinity(d))
			{
				this.Output.Write("double.NegativeInfinity");
				return;
			}
			if (double.IsPositiveInfinity(d))
			{
				this.Output.Write("double.PositiveInfinity");
				return;
			}
			this.Output.Write(d.ToString("R", CultureInfo.InvariantCulture));
			this.Output.Write('D');
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00016445 File Offset: 0x00014645
		private void GenerateDecimalValue(decimal d)
		{
			this.Output.Write(d.ToString(CultureInfo.InvariantCulture));
			this.Output.Write('m');
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001646C File Offset: 0x0001466C
		private void OutputVTableModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00016498 File Offset: 0x00014698
		private void OutputMemberAccessModifier(MemberAttributes attributes)
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

		// Token: 0x060005FA RID: 1530 RVA: 0x0001654C File Offset: 0x0001474C
		private void OutputMemberScopeModifier(MemberAttributes attributes)
		{
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
			{
				MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
				if (memberAttributes == MemberAttributes.Assembly || memberAttributes == MemberAttributes.Family || memberAttributes == MemberAttributes.Public)
				{
					this.Output.Write("virtual ");
				}
				return;
			}
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000165EC File Offset: 0x000147EC
		private void OutputOperator(CodeBinaryOperatorType op)
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

		// Token: 0x060005FC RID: 1532 RVA: 0x00016748 File Offset: 0x00014948
		private void OutputFieldScopeModifier(MemberAttributes attributes)
		{
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

		// Token: 0x060005FD RID: 1533 RVA: 0x00016794 File Offset: 0x00014994
		private void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.PropertyName);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000167C4 File Offset: 0x000149C4
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

		// Token: 0x060005FF RID: 1535 RVA: 0x000168D8 File Offset: 0x00014AD8
		private void GenerateConstructor(CodeConstructor e, CodeTypeDeclaration c)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			this.OutputIdentifier(this.CurrentTypeName);
			this.Output.Write('(');
			this.OutputParameters(e.Parameters);
			this.Output.Write(')');
			CodeExpressionCollection baseConstructorArgs = e.BaseConstructorArgs;
			CodeExpressionCollection chainedConstructorArgs = e.ChainedConstructorArgs;
			int num;
			if (baseConstructorArgs.Count > 0)
			{
				this.Output.WriteLine(" : ");
				num = this.Indent;
				this.Indent = num + 1;
				num = this.Indent;
				this.Indent = num + 1;
				this.Output.Write("base(");
				this.OutputExpressionList(baseConstructorArgs);
				this.Output.Write(')');
				num = this.Indent;
				this.Indent = num - 1;
				num = this.Indent;
				this.Indent = num - 1;
			}
			if (chainedConstructorArgs.Count > 0)
			{
				this.Output.WriteLine(" : ");
				num = this.Indent;
				this.Indent = num + 1;
				num = this.Indent;
				this.Indent = num + 1;
				this.Output.Write("this(");
				this.OutputExpressionList(chainedConstructorArgs);
				this.Output.Write(')');
				num = this.Indent;
				this.Indent = num - 1;
				num = this.Indent;
				this.Indent = num - 1;
			}
			this.OutputStartingBrace();
			num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.Statements);
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00016A98 File Offset: 0x00014C98
		private void GenerateTypeConstructor(CodeTypeConstructor e)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.Output.Write("static ");
			this.Output.Write(this.CurrentTypeName);
			this.Output.Write("()");
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.Statements);
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00016B40 File Offset: 0x00014D40
		private void GenerateTypeReferenceExpression(CodeTypeReferenceExpression e)
		{
			this.OutputType(e.Type);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00016B4E File Offset: 0x00014D4E
		private void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			this.Output.Write("typeof(");
			this.OutputType(e.Type);
			this.Output.Write(')');
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00016B7C File Offset: 0x00014D7C
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

		// Token: 0x06000604 RID: 1540 RVA: 0x00016CA0 File Offset: 0x00014EA0
		private void GenerateTypes(CodeNamespace e)
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

		// Token: 0x06000605 RID: 1541 RVA: 0x00016D24 File Offset: 0x00014F24
		private void GenerateTypeStart(CodeTypeDeclaration e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (this.IsCurrentDelegate)
			{
				TypeAttributes typeAttributes = e.TypeAttributes & TypeAttributes.VisibilityMask;
				if (typeAttributes != TypeAttributes.NotPublic && typeAttributes == TypeAttributes.Public)
				{
					this.Output.Write("public ");
				}
				CodeTypeDelegate codeTypeDelegate = (CodeTypeDelegate)e;
				this.Output.Write("delegate ");
				this.OutputType(codeTypeDelegate.ReturnType);
				this.Output.Write(' ');
				this.OutputIdentifier(e.Name);
				this.Output.Write('(');
				this.OutputParameters(codeTypeDelegate.Parameters);
				this.Output.WriteLine(");");
				return;
			}
			this.OutputTypeAttributes(e);
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			bool flag = true;
			foreach (object obj in e.BaseTypes)
			{
				CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
				if (flag)
				{
					this.Output.Write(" : ");
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				this.OutputType(codeTypeReference);
			}
			this.OutputTypeParameterConstraints(e.TypeParameters);
			this.OutputStartingBrace();
			int indent = this.Indent;
			this.Indent = indent + 1;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00016E9C File Offset: 0x0001509C
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

		// Token: 0x06000607 RID: 1543 RVA: 0x00017034 File Offset: 0x00015234
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

		// Token: 0x06000608 RID: 1544 RVA: 0x00017148 File Offset: 0x00015348
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

		// Token: 0x06000609 RID: 1545 RVA: 0x00017288 File Offset: 0x00015488
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

		// Token: 0x0600060A RID: 1546 RVA: 0x0001731C File Offset: 0x0001551C
		private void GenerateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				((ICodeGenerator)this).GenerateCodeFromNamespace(codeNamespace, this._output.InnerWriter, this._options);
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00017388 File Offset: 0x00015588
		private void OutputAttributeArgument(CodeAttributeArgument arg)
		{
			if (!string.IsNullOrEmpty(arg.Name))
			{
				this.OutputIdentifier(arg.Name);
				this.Output.Write('=');
			}
			((ICodeGenerator)this).GenerateCodeFromExpression(arg.Value, this._output.InnerWriter, this._options);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000173D8 File Offset: 0x000155D8
		private void OutputDirection(FieldDirection dir)
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

		// Token: 0x0600060D RID: 1549 RVA: 0x0001740E File Offset: 0x0001560E
		private void OutputExpressionList(CodeExpressionCollection expressions)
		{
			this.OutputExpressionList(expressions, false);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00017418 File Offset: 0x00015618
		private void OutputExpressionList(CodeExpressionCollection expressions, bool newlineBetweenItems)
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

		// Token: 0x0600060F RID: 1551 RVA: 0x000174CC File Offset: 0x000156CC
		private void OutputParameters(CodeParameterDeclarationExpressionCollection parameters)
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

		// Token: 0x06000610 RID: 1552 RVA: 0x00017578 File Offset: 0x00015778
		private void OutputTypeNamePair(CodeTypeReference typeRef, string name)
		{
			this.OutputType(typeRef);
			this.Output.Write(' ');
			this.OutputIdentifier(name);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00017598 File Offset: 0x00015798
		private void OutputTypeParameters(CodeTypeParameterCollection typeParameters)
		{
			if (typeParameters.Count == 0)
			{
				return;
			}
			this.Output.Write('<');
			bool flag = true;
			for (int i = 0; i < typeParameters.Count; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				if (typeParameters[i].CustomAttributes.Count > 0)
				{
					this.GenerateAttributes(typeParameters[i].CustomAttributes, null, true);
					this.Output.Write(' ');
				}
				this.Output.Write(typeParameters[i].Name);
			}
			this.Output.Write('>');
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00017640 File Offset: 0x00015840
		private void OutputTypeParameterConstraints(CodeTypeParameterCollection typeParameters)
		{
			if (typeParameters.Count == 0)
			{
				return;
			}
			for (int i = 0; i < typeParameters.Count; i++)
			{
				this.Output.WriteLine();
				int num = this.Indent;
				this.Indent = num + 1;
				bool flag = true;
				if (typeParameters[i].Constraints.Count > 0)
				{
					foreach (object obj in typeParameters[i].Constraints)
					{
						CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
						if (flag)
						{
							this.Output.Write("where ");
							this.Output.Write(typeParameters[i].Name);
							this.Output.Write(" : ");
							flag = false;
						}
						else
						{
							this.Output.Write(", ");
						}
						this.OutputType(codeTypeReference);
					}
				}
				if (typeParameters[i].HasConstructorConstraint)
				{
					if (flag)
					{
						this.Output.Write("where ");
						this.Output.Write(typeParameters[i].Name);
						this.Output.Write(" : new()");
					}
					else
					{
						this.Output.Write(", new ()");
					}
				}
				num = this.Indent;
				this.Indent = num - 1;
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000177B0 File Offset: 0x000159B0
		private void OutputTypeAttributes(CodeTypeDeclaration e)
		{
			if ((e.Attributes & MemberAttributes.New) != (MemberAttributes)0)
			{
				this.Output.Write("new ");
			}
			TypeAttributes typeAttributes = e.TypeAttributes;
			switch (typeAttributes & TypeAttributes.VisibilityMask)
			{
			case TypeAttributes.NotPublic:
			case TypeAttributes.NestedAssembly:
			case TypeAttributes.NestedFamANDAssem:
				this.Output.Write("internal ");
				break;
			case TypeAttributes.Public:
			case TypeAttributes.NestedPublic:
				this.Output.Write("public ");
				break;
			case TypeAttributes.NestedPrivate:
				this.Output.Write("private ");
				break;
			case TypeAttributes.NestedFamily:
				this.Output.Write("protected ");
				break;
			case TypeAttributes.VisibilityMask:
				this.Output.Write("protected internal ");
				break;
			}
			if (e.IsStruct)
			{
				if (e.IsPartial)
				{
					this.Output.Write("partial ");
				}
				this.Output.Write("struct ");
				return;
			}
			if (e.IsEnum)
			{
				this.Output.Write("enum ");
				return;
			}
			TypeAttributes typeAttributes2 = typeAttributes & TypeAttributes.ClassSemanticsMask;
			if (typeAttributes2 == TypeAttributes.NotPublic)
			{
				if ((typeAttributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				{
					this.Output.Write("sealed ");
				}
				if ((typeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
				{
					this.Output.Write("abstract ");
				}
				if (e.IsPartial)
				{
					this.Output.Write("partial ");
				}
				this.Output.Write("class ");
				return;
			}
			if (typeAttributes2 != TypeAttributes.ClassSemanticsMask)
			{
				return;
			}
			if (e.IsPartial)
			{
				this.Output.Write("partial ");
			}
			this.Output.Write("interface ");
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00017948 File Offset: 0x00015B48
		private void GenerateTypeEnd(CodeTypeDeclaration e)
		{
			if (!this.IsCurrentDelegate)
			{
				int indent = this.Indent;
				this.Indent = indent - 1;
				this.Output.WriteLine('}');
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001797C File Offset: 0x00015B7C
		private void GenerateNamespaceStart(CodeNamespace e)
		{
			if (!string.IsNullOrEmpty(e.Name))
			{
				this.Output.Write("namespace ");
				string[] array = e.Name.Split(CSharpCodeGenerator.s_periodArray);
				this.OutputIdentifier(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					this.Output.Write('.');
					this.OutputIdentifier(array[i]);
				}
				this.OutputStartingBrace();
				int indent = this.Indent;
				this.Indent = indent + 1;
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000179FA File Offset: 0x00015BFA
		private void GenerateCompileUnit(CodeCompileUnit e)
		{
			this.GenerateCompileUnitStart(e);
			this.GenerateNamespaces(e);
			this.GenerateCompileUnitEnd(e);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00017A14 File Offset: 0x00015C14
		private void GenerateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			this.Output.WriteLine("//------------------------------------------------------------------------------");
			this.Output.Write("// <");
			this.Output.WriteLine("auto-generated>");
			this.Output.Write("//     ");
			this.Output.WriteLine("This code was generated by a tool.");
			this.Output.Write("//     ");
			this.Output.Write("Runtime Version:");
			this.Output.WriteLine(Environment.Version.ToString());
			this.Output.WriteLine("//");
			this.Output.Write("//     ");
			this.Output.WriteLine("Changes to this file may cause incorrect behavior and will be lost if");
			this.Output.Write("//     ");
			this.Output.WriteLine("the code is regenerated.");
			this.Output.Write("// </");
			this.Output.WriteLine("auto-generated>");
			this.Output.WriteLine("//------------------------------------------------------------------------------");
			this.Output.WriteLine();
			SortedSet<string> sortedSet = new SortedSet<string>(StringComparer.Ordinal);
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				if (string.IsNullOrEmpty(codeNamespace.Name))
				{
					codeNamespace.UserData["GenerateImports"] = false;
					foreach (object obj2 in codeNamespace.Imports)
					{
						CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj2;
						sortedSet.Add(codeNamespaceImport.Namespace);
					}
				}
			}
			foreach (string text in sortedSet)
			{
				this.Output.Write("using ");
				this.OutputIdentifier(text);
				this.Output.WriteLine(';');
			}
			if (sortedSet.Count > 0)
			{
				this.Output.WriteLine();
			}
			if (e.AssemblyCustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.AssemblyCustomAttributes, "assembly: ");
				this.Output.WriteLine();
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00017CB0 File Offset: 0x00015EB0
		private void GenerateCompileUnitEnd(CodeCompileUnit e)
		{
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00017CCC File Offset: 0x00015ECC
		private void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			this.OutputDirection(e.Direction);
			this.GenerateExpression(e.Expression);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00017CE8 File Offset: 0x00015EE8
		private void GenerateDirectives(CodeDirectiveCollection directives)
		{
			for (int i = 0; i < directives.Count; i++)
			{
				CodeDirective codeDirective = directives[i];
				if (codeDirective is CodeChecksumPragma)
				{
					this.GenerateChecksumPragma((CodeChecksumPragma)codeDirective);
				}
				else if (codeDirective is CodeRegionDirective)
				{
					this.GenerateCodeRegionDirective((CodeRegionDirective)codeDirective);
				}
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00017D38 File Offset: 0x00015F38
		private void GenerateChecksumPragma(CodeChecksumPragma checksumPragma)
		{
			this.Output.Write("#pragma checksum \"");
			this.Output.Write(checksumPragma.FileName);
			this.Output.Write("\" \"");
			this.Output.Write(checksumPragma.ChecksumAlgorithmId.ToString("B", CultureInfo.InvariantCulture));
			this.Output.Write("\" \"");
			if (checksumPragma.ChecksumData != null)
			{
				foreach (byte b in checksumPragma.ChecksumData)
				{
					this.Output.Write(b.ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			this.Output.WriteLine("\"");
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00017DF8 File Offset: 0x00015FF8
		private void GenerateCodeRegionDirective(CodeRegionDirective regionDirective)
		{
			if (regionDirective.RegionMode == CodeRegionMode.Start)
			{
				this.Output.Write("#region ");
				this.Output.WriteLine(regionDirective.RegionText);
				return;
			}
			if (regionDirective.RegionMode == CodeRegionMode.End)
			{
				this.Output.WriteLine("#endregion");
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00017E4C File Offset: 0x0001604C
		private void GenerateNamespaceEnd(CodeNamespace e)
		{
			if (!string.IsNullOrEmpty(e.Name))
			{
				int indent = this.Indent;
				this.Indent = indent - 1;
				this.Output.WriteLine('}');
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00017E83 File Offset: 0x00016083
		private void GenerateNamespaceImport(CodeNamespaceImport e)
		{
			this.Output.Write("using ");
			this.OutputIdentifier(e.Namespace);
			this.Output.WriteLine(';');
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00017EAE File Offset: 0x000160AE
		private void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes)
		{
			this.Output.Write('[');
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00017EBD File Offset: 0x000160BD
		private void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes)
		{
			this.Output.Write(']');
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00017ECC File Offset: 0x000160CC
		private void GenerateAttributes(CodeAttributeDeclarationCollection attributes)
		{
			this.GenerateAttributes(attributes, null, false);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00017ED7 File Offset: 0x000160D7
		private void GenerateAttributes(CodeAttributeDeclarationCollection attributes, string prefix)
		{
			this.GenerateAttributes(attributes, prefix, false);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00017EE4 File Offset: 0x000160E4
		private void GenerateAttributes(CodeAttributeDeclarationCollection attributes, string prefix, bool inLine)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			bool flag = false;
			foreach (object obj in attributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.Name.Equals("system.paramarrayattribute", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
				}
				else
				{
					this.GenerateAttributeDeclarationsStart(attributes);
					if (prefix != null)
					{
						this.Output.Write(prefix);
					}
					if (codeAttributeDeclaration.AttributeType != null)
					{
						this.Output.Write(this.GetTypeOutput(codeAttributeDeclaration.AttributeType));
					}
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
					this.GenerateAttributeDeclarationsEnd(attributes);
					if (inLine)
					{
						this.Output.Write(' ');
					}
					else
					{
						this.Output.WriteLine();
					}
				}
			}
			if (flag)
			{
				if (prefix != null)
				{
					this.Output.Write(prefix);
				}
				this.Output.Write("params");
				if (inLine)
				{
					this.Output.Write(' ');
					return;
				}
				this.Output.WriteLine();
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000126A7 File Offset: 0x000108A7
		public bool Supports(GeneratorSupport support)
		{
			return (support & (GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties)) == support;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00018094 File Offset: 0x00016294
		public bool IsValidIdentifier(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			if (value.Length > 512)
			{
				return false;
			}
			if (value[0] != '@')
			{
				if (CSharpHelpers.IsKeyword(value))
				{
					return false;
				}
			}
			else
			{
				value = value.Substring(1);
			}
			return CodeGenerator.IsValidLanguageIndependentIdentifier(value);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000180D3 File Offset: 0x000162D3
		public void ValidateIdentifier(string value)
		{
			if (!this.IsValidIdentifier(value))
			{
				throw new ArgumentException(global::SR.Format("Identifier '{0}' is not valid.", value));
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000180EF File Offset: 0x000162EF
		public string CreateValidIdentifier(string name)
		{
			if (CSharpHelpers.IsPrefixTwoUnderscore(name))
			{
				name = "_" + name;
			}
			while (CSharpHelpers.IsKeyword(name))
			{
				name = "_" + name;
			}
			return name;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001811E File Offset: 0x0001631E
		public string CreateEscapedIdentifier(string name)
		{
			return CSharpHelpers.CreateEscapedIdentifier(name);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00018128 File Offset: 0x00016328
		private string GetBaseTypeOutput(CodeTypeReference typeRef, bool preferBuiltInTypes = true)
		{
			string baseType = typeRef.BaseType;
			if (preferBuiltInTypes)
			{
				if (baseType.Length == 0)
				{
					return "void";
				}
				string text = baseType.ToLower(CultureInfo.InvariantCulture).Trim();
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2218649502U)
				{
					if (num <= 574663925U)
					{
						if (num <= 503664103U)
						{
							if (num != 425110298U)
							{
								if (num == 503664103U)
								{
									if (text == "system.string")
									{
										return "string";
									}
								}
							}
							else if (text == "system.char")
							{
								return "char";
							}
						}
						else if (num != 507700544U)
						{
							if (num == 574663925U)
							{
								if (text == "system.uint16")
								{
									return "ushort";
								}
							}
						}
						else if (text == "system.uint64")
						{
							return "ulong";
						}
					}
					else if (num <= 872348156U)
					{
						if (num != 801448826U)
						{
							if (num == 872348156U)
							{
								if (text == "system.byte")
								{
									return "byte";
								}
							}
						}
						else if (text == "system.int32")
						{
							return "int";
						}
					}
					else if (num != 1487069339U)
					{
						if (num == 2218649502U)
						{
							if (text == "system.boolean")
							{
								return "bool";
							}
						}
					}
					else if (text == "system.double")
					{
						return "double";
					}
				}
				else if (num <= 2679997701U)
				{
					if (num <= 2613725868U)
					{
						if (num != 2446023237U)
						{
							if (num == 2613725868U)
							{
								if (text == "system.int16")
								{
									return "short";
								}
							}
						}
						else if (text == "system.decimal")
						{
							return "decimal";
						}
					}
					else if (num != 2647511797U)
					{
						if (num == 2679997701U)
						{
							if (text == "system.int64")
							{
								return "long";
							}
						}
					}
					else if (text == "system.object")
					{
						return "object";
					}
				}
				else if (num <= 2923133227U)
				{
					if (num != 2790997960U)
					{
						if (num == 2923133227U)
						{
							if (text == "system.uint32")
							{
								return "uint";
							}
						}
					}
					else if (text == "system.void")
					{
						return "void";
					}
				}
				else if (num != 3248684926U)
				{
					if (num == 3680803037U)
					{
						if (text == "system.sbyte")
						{
							return "sbyte";
						}
					}
				}
				else if (text == "system.single")
				{
					return "float";
				}
			}
			StringBuilder stringBuilder = new StringBuilder(baseType.Length + 10);
			if ((typeRef.Options & CodeTypeReferenceOptions.GlobalReference) != (CodeTypeReferenceOptions)0)
			{
				stringBuilder.Append("global::");
			}
			string baseType2 = typeRef.BaseType;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < baseType2.Length; i++)
			{
				char c = baseType2[i];
				if (c != '+' && c != '.')
				{
					if (c == '`')
					{
						stringBuilder.Append(this.CreateEscapedIdentifier(baseType2.Substring(num2, i - num2)));
						i++;
						int num4 = 0;
						while (i < baseType2.Length && baseType2[i] >= '0' && baseType2[i] <= '9')
						{
							num4 = num4 * 10 + (int)(baseType2[i] - '0');
							i++;
						}
						this.GetTypeArgumentsOutput(typeRef.TypeArguments, num3, num4, stringBuilder);
						num3 += num4;
						if (i < baseType2.Length && (baseType2[i] == '+' || baseType2[i] == '.'))
						{
							stringBuilder.Append('.');
							i++;
						}
						num2 = i;
					}
				}
				else
				{
					stringBuilder.Append(this.CreateEscapedIdentifier(baseType2.Substring(num2, i - num2)));
					stringBuilder.Append('.');
					i++;
					num2 = i;
				}
			}
			if (num2 < baseType2.Length)
			{
				stringBuilder.Append(this.CreateEscapedIdentifier(baseType2.Substring(num2)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000185AC File Offset: 0x000167AC
		private string GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			this.GetTypeArgumentsOutput(typeArguments, 0, typeArguments.Count, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000185DC File Offset: 0x000167DC
		private void GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments, int start, int length, StringBuilder sb)
		{
			sb.Append('<');
			bool flag = true;
			for (int i = start; i < start + length; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sb.Append(", ");
				}
				if (i < typeArguments.Count)
				{
					sb.Append(this.GetTypeOutput(typeArguments[i]));
				}
			}
			sb.Append('>');
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00018640 File Offset: 0x00016840
		public string GetTypeOutput(CodeTypeReference typeRef)
		{
			string text = string.Empty;
			CodeTypeReference codeTypeReference = typeRef;
			while (codeTypeReference.ArrayElementType != null)
			{
				codeTypeReference = codeTypeReference.ArrayElementType;
			}
			text += this.GetBaseTypeOutput(codeTypeReference, true);
			while (typeRef != null && typeRef.ArrayRank > 0)
			{
				char[] array = new char[typeRef.ArrayRank + 1];
				array[0] = '[';
				array[typeRef.ArrayRank] = ']';
				for (int i = 1; i < typeRef.ArrayRank; i++)
				{
					array[i] = ',';
				}
				text += new string(array);
				typeRef = typeRef.ArrayElementType;
			}
			return text;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000186CC File Offset: 0x000168CC
		private void OutputStartingBrace()
		{
			if (this.Options.BracingStyle == "C")
			{
				this.Output.WriteLine();
				this.Output.WriteLine('{');
				return;
			}
			this.Output.WriteLine(" {");
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001871C File Offset: 0x0001691C
		CompilerResults ICodeCompiler.CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromDom(options, e);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00018760 File Offset: 0x00016960
		CompilerResults ICodeCompiler.CompileAssemblyFromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromFile(options, fileName);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000187A4 File Offset: 0x000169A4
		CompilerResults ICodeCompiler.CompileAssemblyFromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromSource(options, source);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000187E8 File Offset: 0x000169E8
		CompilerResults ICodeCompiler.CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromSourceBatch(options, sources);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001882C File Offset: 0x00016A2C
		CompilerResults ICodeCompiler.CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults compilerResults;
			try
			{
				for (int i = 0; i < fileNames.Length; i++)
				{
					File.OpenRead(fileNames[i]).Dispose();
				}
				compilerResults = this.FromFileBatch(options, fileNames);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001889C File Offset: 0x00016A9C
		CompilerResults ICodeCompiler.CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromDomBatch(options, ea);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000188E0 File Offset: 0x00016AE0
		private CompilerResults FromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return this.FromDomBatch(options, new CodeCompileUnit[] { e });
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00018901 File Offset: 0x00016B01
		private CompilerResults FromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			File.OpenRead(fileName).Dispose();
			return this.FromFileBatch(options, new string[] { fileName });
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001893B File Offset: 0x00016B3B
		private CompilerResults FromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return this.FromSourceBatch(options, new string[] { source });
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001895C File Offset: 0x00016B5C
		private CompilerResults FromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (ea == null)
			{
				throw new ArgumentNullException("ea");
			}
			string[] array = new string[ea.Length];
			for (int i = 0; i < ea.Length; i++)
			{
				if (ea[i] != null)
				{
					this.ResolveReferencedAssemblies(options, ea[i]);
					array[i] = options.TempFiles.AddExtension(i + this.FileExtension);
					using (FileStream fileStream = new FileStream(array[i], FileMode.Create, FileAccess.Write, FileShare.Read))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
						{
							((ICodeGenerator)this).GenerateCodeFromCompileUnit(ea[i], streamWriter, this.Options);
							streamWriter.Flush();
						}
					}
				}
			}
			return this.FromFileBatch(options, array);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00018A34 File Offset: 0x00016C34
		private void ResolveReferencedAssemblies(CompilerParameters options, CodeCompileUnit e)
		{
			if (e.ReferencedAssemblies.Count > 0)
			{
				foreach (string text in e.ReferencedAssemblies)
				{
					if (!options.ReferencedAssemblies.Contains(text))
					{
						options.ReferencedAssemblies.Add(text);
					}
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00018AAC File Offset: 0x00016CAC
		private CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			string[] array = new string[sources.Length];
			for (int i = 0; i < sources.Length; i++)
			{
				string text = options.TempFiles.AddExtension(i + this.FileExtension);
				using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
					{
						streamWriter.Write(sources[i]);
						streamWriter.Flush();
					}
				}
				array[i] = text;
			}
			return this.FromFileBatch(options, array);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00018B74 File Offset: 0x00016D74
		private static string JoinStringArray(string[] sa, string separator)
		{
			if (sa == null || sa.Length == 0)
			{
				return string.Empty;
			}
			if (sa.Length == 1)
			{
				return "\"" + sa[0] + "\"";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < sa.Length - 1; i++)
			{
				stringBuilder.Append('"');
				stringBuilder.Append(sa[i]);
				stringBuilder.Append('"');
				stringBuilder.Append(separator);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(sa[sa.Length - 1]);
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00018C08 File Offset: 0x00016E08
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

		// Token: 0x0600063C RID: 1596 RVA: 0x00018C98 File Offset: 0x00016E98
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

		// Token: 0x0600063D RID: 1597 RVA: 0x00018D28 File Offset: 0x00016F28
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

		// Token: 0x0600063E RID: 1598 RVA: 0x00018DCC File Offset: 0x00016FCC
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

		// Token: 0x0600063F RID: 1599 RVA: 0x00018E5C File Offset: 0x0001705C
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

		// Token: 0x06000640 RID: 1600 RVA: 0x00018EEC File Offset: 0x000170EC
		private CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults results = new CompilerResults(options.TempFiles);
			Process process = new Process();
			if (Path.DirectorySeparatorChar == '\\')
			{
				process.StartInfo.FileName = MonoToolsLocator.Mono;
				process.StartInfo.Arguments = "\"" + MonoToolsLocator.McsCSharpCompiler + "\" ";
			}
			else
			{
				process.StartInfo.FileName = MonoToolsLocator.McsCSharpCompiler;
			}
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.Arguments += CSharpCodeGenerator.BuildArgs(options, fileNames, this._provOptions);
			Mutex mcsOutMutex = new Mutex();
			process.StartInfo.EnvironmentVariables["MONO_GC_PARAMS"] = string.Empty;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs args)
			{
				if (args.Data != null)
				{
					mcsOutMutex.WaitOne();
					results.Output.Add(args.Data);
					mcsOutMutex.ReleaseMutex();
				}
			};
			process.StartInfo.StandardOutputEncoding = (process.StartInfo.StandardErrorEncoding = Encoding.UTF8);
			try
			{
				process.Start();
			}
			catch (Exception ex)
			{
				Win32Exception ex2 = ex as Win32Exception;
				if (ex2 != null)
				{
					throw new SystemException(string.Format("Error running {0}: {1}", process.StartInfo.FileName, Win32Exception.GetErrorMessage(ex2.NativeErrorCode)));
				}
				throw;
			}
			try
			{
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				results.NativeCompilerReturnValue = process.ExitCode;
			}
			finally
			{
				process.CancelErrorRead();
				process.CancelOutputRead();
				process.Close();
			}
			bool flag = true;
			foreach (string text in results.Output)
			{
				CompilerError compilerError = CSharpCodeGenerator.CreateErrorFromString(text);
				if (compilerError != null)
				{
					results.Errors.Add(compilerError);
					if (!compilerError.IsWarning)
					{
						flag = false;
					}
				}
			}
			if (results.Output.Count > 0)
			{
				results.Output.Insert(0, process.StartInfo.FileName + " " + process.StartInfo.Arguments + Environment.NewLine);
			}
			if (flag)
			{
				if (!File.Exists(options.OutputAssembly))
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text2 in results.Output)
					{
						stringBuilder.Append(text2 + Environment.NewLine);
					}
					throw new Exception("Compiler failed to produce the assembly. Output: '" + stringBuilder.ToString() + "'");
				}
				if (options.GenerateInMemory)
				{
					using (FileStream fileStream = File.OpenRead(options.OutputAssembly))
					{
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
						results.CompiledAssembly = Assembly.Load(array, null);
						fileStream.Close();
						goto IL_0351;
					}
				}
				results.PathToAssembly = options.OutputAssembly;
			}
			else
			{
				results.CompiledAssembly = null;
			}
			IL_0351:
			return results;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00019290 File Offset: 0x00017490
		private static string BuildArgs(CompilerParameters options, string[] fileNames, IDictionary<string, string> providerOptions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (options.GenerateExecutable)
			{
				stringBuilder.Append("/target:exe ");
			}
			else
			{
				stringBuilder.Append("/target:library ");
			}
			string privateBinPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
			if (privateBinPath != null && privateBinPath.Length > 0)
			{
				stringBuilder.AppendFormat("/lib:\"{0}\" ", privateBinPath);
			}
			if (options.Win32Resource != null)
			{
				stringBuilder.AppendFormat("/win32res:\"{0}\" ", options.Win32Resource);
			}
			if (options.IncludeDebugInformation)
			{
				stringBuilder.Append("/debug+ /optimize- ");
			}
			else
			{
				stringBuilder.Append("/debug- /optimize+ ");
			}
			if (options.TreatWarningsAsErrors)
			{
				stringBuilder.Append("/warnaserror ");
			}
			if (options.WarningLevel >= 0)
			{
				stringBuilder.AppendFormat("/warn:{0} ", options.WarningLevel);
			}
			if (options.OutputAssembly == null || options.OutputAssembly.Length == 0)
			{
				string text = (options.GenerateExecutable ? "exe" : "dll");
				options.OutputAssembly = CSharpCodeGenerator.GetTempFileNameWithExtension(options.TempFiles, text, !options.GenerateInMemory);
			}
			stringBuilder.AppendFormat("/out:\"{0}\" ", options.OutputAssembly);
			foreach (string text2 in options.ReferencedAssemblies)
			{
				if (text2 != null && text2.Length != 0)
				{
					stringBuilder.AppendFormat("/r:\"{0}\" ", text2);
				}
			}
			if (options.CompilerOptions != null)
			{
				stringBuilder.Append(options.CompilerOptions);
				stringBuilder.Append(" ");
			}
			foreach (string text3 in options.EmbeddedResources)
			{
				stringBuilder.AppendFormat("/resource:\"{0}\" ", text3);
			}
			foreach (string text4 in options.LinkedResources)
			{
				stringBuilder.AppendFormat("/linkresource:\"{0}\" ", text4);
			}
			if (providerOptions != null && providerOptions.Count > 0)
			{
				string text5;
				if (!providerOptions.TryGetValue("CompilerVersion", out text5))
				{
					text5 = "3.5";
				}
				if (text5.Length >= 1 && text5[0] == 'v')
				{
					text5 = text5.Substring(1);
				}
				if (!(text5 == "2.0"))
				{
					if (!(text5 == "3.5"))
					{
					}
				}
				else
				{
					stringBuilder.Append("/langversion:ISO-2 ");
				}
			}
			stringBuilder.Append("/noconfig ");
			stringBuilder.Append(" -- ");
			foreach (string text6 in fileNames)
			{
				stringBuilder.AppendFormat("\"{0}\" ", text6);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00019588 File Offset: 0x00017788
		private static CompilerError CreateErrorFromString(string error_string)
		{
			if (error_string.StartsWith("BETA"))
			{
				return null;
			}
			if (error_string == null || error_string == "")
			{
				return null;
			}
			CompilerError compilerError = new CompilerError();
			Match match = new Regex("\n\t\t\t^\n\t\t\t(\\s*(?<file>[^\\(]+)                         # filename (optional)\n\t\t\t (\\((?<line>\\d*)(,(?<column>\\d*[\\+]*))?\\))? # line+column (optional)\n\t\t\t :\\s+)?\n\t\t\t(?<level>\\w+)                               # error|warning\n\t\t\t\\s+\n\t\t\t(?<number>[^:]*\\d)                          # CS1234\n\t\t\t:\n\t\t\t\\s*\n\t\t\t(?<message>.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace).Match(error_string);
			if (match.Success)
			{
				if (string.Empty != match.Result("${file}"))
				{
					compilerError.FileName = match.Result("${file}");
				}
				if (string.Empty != match.Result("${line}"))
				{
					compilerError.Line = int.Parse(match.Result("${line}"));
				}
				if (string.Empty != match.Result("${column}"))
				{
					compilerError.Column = int.Parse(match.Result("${column}").Trim(new char[] { '+' }));
				}
				string text = match.Result("${level}");
				if (text == "warning")
				{
					compilerError.IsWarning = true;
				}
				else if (text != "error")
				{
					return null;
				}
				compilerError.ErrorNumber = match.Result("${number}");
				compilerError.ErrorText = match.Result("${message}");
				return compilerError;
			}
			match = CSharpCodeGenerator.RelatedSymbolsRegex.Match(error_string);
			if (!match.Success)
			{
				compilerError.ErrorText = error_string;
				compilerError.IsWarning = false;
				compilerError.ErrorNumber = "";
				return compilerError;
			}
			return null;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00013231 File Offset: 0x00011431
		private static string GetTempFileNameWithExtension(TempFileCollection temp_files, string extension, bool keepFile)
		{
			return temp_files.AddExtension(extension, keepFile);
		}

		// Token: 0x04000BDD RID: 3037
		private static readonly char[] s_periodArray = new char[] { '.' };

		// Token: 0x04000BDE RID: 3038
		private ExposedTabStringIndentedTextWriter _output;

		// Token: 0x04000BDF RID: 3039
		private CodeGeneratorOptions _options;

		// Token: 0x04000BE0 RID: 3040
		private CodeTypeDeclaration _currentClass;

		// Token: 0x04000BE1 RID: 3041
		private CodeTypeMember _currentMember;

		// Token: 0x04000BE2 RID: 3042
		private bool _inNestedBinary;

		// Token: 0x04000BE3 RID: 3043
		private readonly IDictionary<string, string> _provOptions;

		// Token: 0x04000BE4 RID: 3044
		private const int ParameterMultilineThreshold = 15;

		// Token: 0x04000BE5 RID: 3045
		private const int MaxLineLength = 80;

		// Token: 0x04000BE6 RID: 3046
		private const GeneratorSupport LanguageSupport = GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties;

		// Token: 0x04000BE7 RID: 3047
		private static readonly string[][] s_keywords = new string[][]
		{
			null,
			new string[] { "as", "do", "if", "in", "is" },
			new string[] { "for", "int", "new", "out", "ref", "try" },
			new string[]
			{
				"base", "bool", "byte", "case", "char", "else", "enum", "goto", "lock", "long",
				"null", "this", "true", "uint", "void"
			},
			new string[]
			{
				"break", "catch", "class", "const", "event", "false", "fixed", "float", "sbyte", "short",
				"throw", "ulong", "using", "while"
			},
			new string[]
			{
				"double", "extern", "object", "params", "public", "return", "sealed", "sizeof", "static", "string",
				"struct", "switch", "typeof", "unsafe", "ushort"
			},
			new string[] { "checked", "decimal", "default", "finally", "foreach", "private", "virtual" },
			new string[] { "abstract", "continue", "delegate", "explicit", "implicit", "internal", "operator", "override", "readonly", "volatile" },
			new string[] { "__arglist", "__makeref", "__reftype", "interface", "namespace", "protected", "unchecked" },
			new string[] { "__refvalue", "stackalloc" }
		};

		// Token: 0x04000BE8 RID: 3048
		private bool _generatingForLoop;

		// Token: 0x04000BE9 RID: 3049
		private const string ErrorRegexPattern = "\n\t\t\t^\n\t\t\t(\\s*(?<file>[^\\(]+)                         # filename (optional)\n\t\t\t (\\((?<line>\\d*)(,(?<column>\\d*[\\+]*))?\\))? # line+column (optional)\n\t\t\t :\\s+)?\n\t\t\t(?<level>\\w+)                               # error|warning\n\t\t\t\\s+\n\t\t\t(?<number>[^:]*\\d)                          # CS1234\n\t\t\t:\n\t\t\t\\s*\n\t\t\t(?<message>.*)$";

		// Token: 0x04000BEA RID: 3050
		private static readonly Regex RelatedSymbolsRegex = new Regex("\n            \\(Location\\ of\\ the\\ symbol\\ related\\ to\\ previous\\ (warning|error)\\)\n\t\t\t", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
	}
}
