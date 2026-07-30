using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.VisualBasic
{
	// Token: 0x020000E2 RID: 226
	internal sealed class VBCodeGenerator : CodeCompiler
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0000F0D5 File Offset: 0x0000D2D5
		internal VBCodeGenerator()
		{
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000F0DD File Offset: 0x0000D2DD
		internal VBCodeGenerator(IDictionary<string, string> providerOptions)
		{
			this._provOptions = providerOptions;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		protected override string FileExtension
		{
			get
			{
				return ".vb";
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
		protected override string CompilerName
		{
			get
			{
				return "vbc.exe";
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000F0FA File Offset: 0x0000D2FA
		private bool IsCurrentModule
		{
			get
			{
				return base.IsCurrentClass && this.GetUserData(base.CurrentClass, "Module", false);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000F118 File Offset: 0x0000D318
		protected override string NullToken
		{
			get
			{
				return "Nothing";
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000F11F File Offset: 0x0000D31F
		private void EnsureInDoubleQuotes(ref bool fInDoubleQuotes, StringBuilder b)
		{
			if (fInDoubleQuotes)
			{
				return;
			}
			b.Append("&\"");
			fInDoubleQuotes = true;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000F135 File Offset: 0x0000D335
		private void EnsureNotInDoubleQuotes(ref bool fInDoubleQuotes, StringBuilder b)
		{
			if (!fInDoubleQuotes)
			{
				return;
			}
			b.Append('"');
			fInDoubleQuotes = false;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000F148 File Offset: 0x0000D348
		protected override string QuoteSnippetString(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length + 5);
			bool flag = true;
			Indentation indentation = new Indentation((ExposedTabStringIndentedTextWriter)base.Output, base.Indent + 1);
			stringBuilder.Append('"');
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '“')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
								stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(9)");
								break;
							case '\n':
								this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
								stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(10)");
								break;
							case '\v':
							case '\f':
								goto IL_0183;
							case '\r':
								this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
								if (i < value.Length - 1 && value[i + 1] == '\n')
								{
									stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)");
									i++;
								}
								else
								{
									stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(13)");
								}
								break;
							default:
								goto IL_0183;
							}
						}
						else
						{
							this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
							stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(0)");
						}
					}
					else
					{
						if (c != '"' && c != '“')
						{
							goto IL_0183;
						}
						goto IL_00CA;
					}
				}
				else
				{
					if (c <= '\u2028')
					{
						if (c == '”')
						{
							goto IL_00CA;
						}
						if (c != '\u2028')
						{
							goto IL_0183;
						}
					}
					else if (c != '\u2029')
					{
						if (c == '＂')
						{
							goto IL_00CA;
						}
						goto IL_0183;
					}
					this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
					VBCodeGenerator.AppendEscapedChar(stringBuilder, c);
				}
				IL_019A:
				if (i > 0 && i % 80 == 0)
				{
					if (char.IsHighSurrogate(value[i]) && i < value.Length - 1 && char.IsLowSurrogate(value[i + 1]))
					{
						stringBuilder.Append(value[++i]);
					}
					if (flag)
					{
						stringBuilder.Append('"');
					}
					flag = true;
					stringBuilder.Append("& _ ");
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(indentation.IndentationString);
					stringBuilder.Append('"');
				}
				i++;
				continue;
				IL_00CA:
				this.EnsureInDoubleQuotes(ref flag, stringBuilder);
				stringBuilder.Append(c);
				stringBuilder.Append(c);
				goto IL_019A;
				IL_0183:
				this.EnsureInDoubleQuotes(ref flag, stringBuilder);
				stringBuilder.Append(value[i]);
				goto IL_019A;
			}
			if (flag)
			{
				stringBuilder.Append('"');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000F394 File Offset: 0x0000D594
		private static void AppendEscapedChar(StringBuilder b, char value)
		{
			b.Append("&Global.Microsoft.VisualBasic.ChrW(");
			int num = (int)value;
			b.Append(num.ToString(CultureInfo.InvariantCulture));
			b.Append(")");
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000F3CE File Offset: 0x0000D5CE
		protected override void ProcessCompilerOutputLine(CompilerResults results, string line)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000F3CE File Offset: 0x0000D5CE
		protected override string CmdArgsFromParameters(CompilerParameters options)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
		protected override void OutputAttributeArgument(CodeAttributeArgument arg)
		{
			if (!string.IsNullOrEmpty(arg.Name))
			{
				this.OutputIdentifier(arg.Name);
				base.Output.Write(":=");
			}
			((ICodeGenerator)this).GenerateCodeFromExpression(arg.Value, ((ExposedTabStringIndentedTextWriter)base.Output).InnerWriter, base.Options);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000F430 File Offset: 0x0000D630
		private void OutputAttributes(CodeAttributeDeclarationCollection attributes, bool inLine)
		{
			this.OutputAttributes(attributes, inLine, null, false);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000F43C File Offset: 0x0000D63C
		private void OutputAttributes(CodeAttributeDeclarationCollection attributes, bool inLine, string prefix, bool closingLine)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			bool flag = true;
			this.GenerateAttributeDeclarationsStart(attributes);
			foreach (object obj in attributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					base.Output.Write(", ");
					if (!inLine)
					{
						this.ContinueOnNewLine("");
						base.Output.Write(' ');
					}
				}
				if (!string.IsNullOrEmpty(prefix))
				{
					base.Output.Write(prefix);
				}
				if (codeAttributeDeclaration.AttributeType != null)
				{
					base.Output.Write(this.GetTypeOutput(codeAttributeDeclaration.AttributeType));
				}
				base.Output.Write('(');
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
						base.Output.Write(", ");
					}
					this.OutputAttributeArgument(codeAttributeArgument);
				}
				base.Output.Write(')');
			}
			this.GenerateAttributeDeclarationsEnd(attributes);
			base.Output.Write(' ');
			if (!inLine)
			{
				if (closingLine)
				{
					base.Output.WriteLine();
					return;
				}
				this.ContinueOnNewLine("");
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		protected override void OutputDirection(FieldDirection dir)
		{
			if (dir == FieldDirection.In)
			{
				base.Output.Write("ByVal ");
				return;
			}
			if (dir - FieldDirection.Out > 1)
			{
				return;
			}
			base.Output.Write("ByRef ");
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000F5ED File Offset: 0x0000D7ED
		protected override void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
			base.Output.Write("CType(Nothing, " + this.GetTypeOutput(e.Type) + ")");
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000F615 File Offset: 0x0000D815
		protected override void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			base.GenerateExpression(e.Expression);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000F624 File Offset: 0x0000D824
		protected override void OutputFieldScopeModifier(MemberAttributes attributes)
		{
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Final:
				base.Output.Write("");
				return;
			case MemberAttributes.Static:
				if (!this.IsCurrentModule)
				{
					base.Output.Write("Shared ");
					return;
				}
				return;
			case MemberAttributes.Const:
				base.Output.Write("Const ");
				return;
			}
			base.Output.Write("");
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000F69C File Offset: 0x0000D89C
		protected override void OutputMemberAccessModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
			if (memberAttributes <= MemberAttributes.Family)
			{
				if (memberAttributes == MemberAttributes.Assembly)
				{
					base.Output.Write("Friend ");
					return;
				}
				if (memberAttributes == MemberAttributes.FamilyAndAssembly)
				{
					base.Output.Write("Friend ");
					return;
				}
				if (memberAttributes != MemberAttributes.Family)
				{
					return;
				}
				base.Output.Write("Protected ");
				return;
			}
			else
			{
				if (memberAttributes == MemberAttributes.FamilyOrAssembly)
				{
					base.Output.Write("Protected Friend ");
					return;
				}
				if (memberAttributes == MemberAttributes.Private)
				{
					base.Output.Write("Private ");
					return;
				}
				if (memberAttributes != MemberAttributes.Public)
				{
					return;
				}
				base.Output.Write("Public ");
				return;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000F750 File Offset: 0x0000D950
		private void OutputVTableModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				base.Output.Write("Shadows ");
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000F77C File Offset: 0x0000D97C
		protected override void OutputMemberScopeModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.ScopeMask;
			switch (memberAttributes)
			{
			case MemberAttributes.Abstract:
				base.Output.Write("MustOverride ");
				return;
			case MemberAttributes.Final:
				base.Output.Write("");
				return;
			case MemberAttributes.Static:
				if (!this.IsCurrentModule)
				{
					base.Output.Write("Shared ");
					return;
				}
				break;
			case MemberAttributes.Override:
				base.Output.Write("Overrides ");
				return;
			default:
				if (memberAttributes == MemberAttributes.Private)
				{
					base.Output.Write("Private ");
					return;
				}
				memberAttributes = attributes & MemberAttributes.AccessMask;
				if (memberAttributes == MemberAttributes.Assembly || memberAttributes == MemberAttributes.Family || memberAttributes == MemberAttributes.Public)
				{
					base.Output.Write("Overridable ");
				}
				break;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000F840 File Offset: 0x0000DA40
		protected override void OutputOperator(CodeBinaryOperatorType op)
		{
			switch (op)
			{
			case CodeBinaryOperatorType.Modulus:
				base.Output.Write("Mod");
				return;
			case CodeBinaryOperatorType.IdentityInequality:
				base.Output.Write("<>");
				return;
			case CodeBinaryOperatorType.IdentityEquality:
				base.Output.Write("Is");
				return;
			case CodeBinaryOperatorType.ValueEquality:
				base.Output.Write('=');
				return;
			case CodeBinaryOperatorType.BitwiseOr:
				base.Output.Write("Or");
				return;
			case CodeBinaryOperatorType.BitwiseAnd:
				base.Output.Write("And");
				return;
			case CodeBinaryOperatorType.BooleanOr:
				base.Output.Write("OrElse");
				return;
			case CodeBinaryOperatorType.BooleanAnd:
				base.Output.Write("AndAlso");
				return;
			}
			base.OutputOperator(op);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000F90C File Offset: 0x0000DB0C
		private void GenerateNotIsNullExpression(CodeExpression e)
		{
			base.Output.Write("(Not (");
			base.GenerateExpression(e);
			base.Output.Write(") Is ");
			base.Output.Write(this.NullToken);
			base.Output.Write(')');
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000F960 File Offset: 0x0000DB60
		protected override void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			if (e.Operator != CodeBinaryOperatorType.IdentityInequality)
			{
				base.GenerateBinaryOperatorExpression(e);
				return;
			}
			if (e.Right is CodePrimitiveExpression && ((CodePrimitiveExpression)e.Right).Value == null)
			{
				this.GenerateNotIsNullExpression(e.Left);
				return;
			}
			if (e.Left is CodePrimitiveExpression && ((CodePrimitiveExpression)e.Left).Value == null)
			{
				this.GenerateNotIsNullExpression(e.Right);
				return;
			}
			base.GenerateBinaryOperatorExpression(e);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000F9DD File Offset: 0x0000DBDD
		protected override string GetResponseFileCmdArgs(CompilerParameters options, string cmdArgs)
		{
			return "/noconfig " + base.GetResponseFileCmdArgs(options, cmdArgs);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000F9F1 File Offset: 0x0000DBF1
		protected override void OutputIdentifier(string ident)
		{
			base.Output.Write(this.CreateEscapedIdentifier(ident));
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000FA05 File Offset: 0x0000DC05
		protected override void OutputType(CodeTypeReference typeRef)
		{
			base.Output.Write(this.GetTypeOutputWithoutArrayPostFix(typeRef));
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		private void OutputTypeAttributes(CodeTypeDeclaration e)
		{
			if ((e.Attributes & MemberAttributes.New) != (MemberAttributes)0)
			{
				base.Output.Write("Shadows ");
			}
			TypeAttributes typeAttributes = e.TypeAttributes;
			if (e.IsPartial)
			{
				base.Output.Write("Partial ");
			}
			switch (typeAttributes & TypeAttributes.VisibilityMask)
			{
			case TypeAttributes.NotPublic:
			case TypeAttributes.NestedAssembly:
			case TypeAttributes.NestedFamANDAssem:
				base.Output.Write("Friend ");
				break;
			case TypeAttributes.Public:
			case TypeAttributes.NestedPublic:
				base.Output.Write("Public ");
				break;
			case TypeAttributes.NestedPrivate:
				base.Output.Write("Private ");
				break;
			case TypeAttributes.NestedFamily:
				base.Output.Write("Protected ");
				break;
			case TypeAttributes.VisibilityMask:
				base.Output.Write("Protected Friend ");
				break;
			}
			if (e.IsStruct)
			{
				base.Output.Write("Structure ");
				return;
			}
			if (e.IsEnum)
			{
				base.Output.Write("Enum ");
				return;
			}
			TypeAttributes typeAttributes2 = typeAttributes & TypeAttributes.ClassSemanticsMask;
			if (typeAttributes2 != TypeAttributes.NotPublic)
			{
				if (typeAttributes2 != TypeAttributes.ClassSemanticsMask)
				{
					return;
				}
				base.Output.Write("Interface ");
				return;
			}
			else
			{
				if (this.IsCurrentModule)
				{
					base.Output.Write("Module ");
					return;
				}
				if ((typeAttributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				{
					base.Output.Write("NotInheritable ");
				}
				if ((typeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
				{
					base.Output.Write("MustInherit ");
				}
				base.Output.Write("Class ");
				return;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000FB9D File Offset: 0x0000DD9D
		protected override void OutputTypeNamePair(CodeTypeReference typeRef, string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "__exception";
			}
			this.OutputIdentifier(name);
			this.OutputArrayPostfix(typeRef);
			base.Output.Write(" As ");
			this.OutputType(typeRef);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		private string GetArrayPostfix(CodeTypeReference typeRef)
		{
			string text = "";
			if (typeRef.ArrayElementType != null)
			{
				text = this.GetArrayPostfix(typeRef.ArrayElementType);
			}
			if (typeRef.ArrayRank > 0)
			{
				char[] array = new char[typeRef.ArrayRank + 1];
				array[0] = '(';
				array[typeRef.ArrayRank] = ')';
				for (int i = 1; i < typeRef.ArrayRank; i++)
				{
					array[i] = ',';
				}
				text = new string(array) + text;
			}
			return text;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000FC46 File Offset: 0x0000DE46
		private void OutputArrayPostfix(CodeTypeReference typeRef)
		{
			if (typeRef.ArrayRank > 0)
			{
				base.Output.Write(this.GetArrayPostfix(typeRef));
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000FC64 File Offset: 0x0000DE64
		protected override void GenerateIterationStatement(CodeIterationStatement e)
		{
			base.GenerateStatement(e.InitStatement);
			base.Output.Write("Do While ");
			base.GenerateExpression(e.TestExpression);
			base.Output.WriteLine();
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.Statements);
			base.GenerateStatement(e.IncrementStatement);
			num = base.Indent;
			base.Indent = num - 1;
			base.Output.WriteLine("Loop");
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		protected override void GeneratePrimitiveExpression(CodePrimitiveExpression e)
		{
			if (e.Value is char)
			{
				base.Output.Write("Global.Microsoft.VisualBasic.ChrW(" + ((IConvertible)e.Value).ToInt32(CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture) + ")");
				return;
			}
			if (e.Value is sbyte)
			{
				base.Output.Write("CSByte(");
				base.Output.Write(((sbyte)e.Value).ToString(CultureInfo.InvariantCulture));
				base.Output.Write(')');
				return;
			}
			if (e.Value is ushort)
			{
				base.Output.Write(((ushort)e.Value).ToString(CultureInfo.InvariantCulture));
				base.Output.Write("US");
				return;
			}
			if (e.Value is uint)
			{
				base.Output.Write(((uint)e.Value).ToString(CultureInfo.InvariantCulture));
				base.Output.Write("UI");
				return;
			}
			if (e.Value is ulong)
			{
				base.Output.Write(((ulong)e.Value).ToString(CultureInfo.InvariantCulture));
				base.Output.Write("UL");
				return;
			}
			base.GeneratePrimitiveExpression(e);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000FE5C File Offset: 0x0000E05C
		protected override void GenerateThrowExceptionStatement(CodeThrowExceptionStatement e)
		{
			base.Output.Write("Throw");
			if (e.ToThrow != null)
			{
				base.Output.Write(' ');
				base.GenerateExpression(e.ToThrow);
			}
			base.Output.WriteLine();
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000FE9C File Offset: 0x0000E09C
		protected override void GenerateArrayCreateExpression(CodeArrayCreateExpression e)
		{
			base.Output.Write("New ");
			CodeExpressionCollection initializers = e.Initializers;
			if (initializers.Count > 0)
			{
				string typeOutput = this.GetTypeOutput(e.CreateType);
				base.Output.Write(typeOutput);
				if (typeOutput.IndexOf('(') == -1)
				{
					base.Output.Write("()");
				}
				base.Output.Write(" {");
				int num = base.Indent;
				base.Indent = num + 1;
				this.OutputExpressionList(initializers);
				num = base.Indent;
				base.Indent = num - 1;
				base.Output.Write('}');
				return;
			}
			string typeOutput2 = this.GetTypeOutput(e.CreateType);
			int num2 = typeOutput2.IndexOf('(');
			if (num2 == -1)
			{
				base.Output.Write(typeOutput2);
				base.Output.Write('(');
			}
			else
			{
				base.Output.Write(typeOutput2.Substring(0, num2 + 1));
			}
			if (e.SizeExpression != null)
			{
				base.Output.Write('(');
				base.GenerateExpression(e.SizeExpression);
				base.Output.Write(") - 1");
			}
			else
			{
				base.Output.Write(e.Size - 1);
			}
			if (num2 == -1)
			{
				base.Output.Write(')');
			}
			else
			{
				base.Output.Write(typeOutput2.Substring(num2 + 1));
			}
			base.Output.Write(" {}");
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001000E File Offset: 0x0000E20E
		protected override void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e)
		{
			base.Output.Write("MyBase");
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00010020 File Offset: 0x0000E220
		protected override void GenerateCastExpression(CodeCastExpression e)
		{
			base.Output.Write("CType(");
			base.GenerateExpression(e.Expression);
			base.Output.Write(',');
			this.OutputType(e.TargetType);
			this.OutputArrayPostfix(e.TargetType);
			base.Output.Write(')');
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001007B File Offset: 0x0000E27B
		protected override void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e)
		{
			base.Output.Write("AddressOf ");
			base.GenerateExpression(e.TargetObject);
			base.Output.Write('.');
			this.OutputIdentifier(e.MethodName);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000100B2 File Offset: 0x0000E2B2
		protected override void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				base.GenerateExpression(e.TargetObject);
				base.Output.Write('.');
			}
			this.OutputIdentifier(e.FieldName);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000100E4 File Offset: 0x0000E2E4
		protected override void GenerateSingleFloatValue(float s)
		{
			if (float.IsNaN(s))
			{
				base.Output.Write("Single.NaN");
				return;
			}
			if (float.IsNegativeInfinity(s))
			{
				base.Output.Write("Single.NegativeInfinity");
				return;
			}
			if (float.IsPositiveInfinity(s))
			{
				base.Output.Write("Single.PositiveInfinity");
				return;
			}
			base.Output.Write(s.ToString(CultureInfo.InvariantCulture));
			base.Output.Write('!');
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00010160 File Offset: 0x0000E360
		protected override void GenerateDoubleValue(double d)
		{
			if (double.IsNaN(d))
			{
				base.Output.Write("Double.NaN");
				return;
			}
			if (double.IsNegativeInfinity(d))
			{
				base.Output.Write("Double.NegativeInfinity");
				return;
			}
			if (double.IsPositiveInfinity(d))
			{
				base.Output.Write("Double.PositiveInfinity");
				return;
			}
			base.Output.Write(d.ToString("R", CultureInfo.InvariantCulture));
			base.Output.Write('R');
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000101E1 File Offset: 0x0000E3E1
		protected override void GenerateDecimalValue(decimal d)
		{
			base.Output.Write(d.ToString(CultureInfo.InvariantCulture));
			base.Output.Write('D');
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00010207 File Offset: 0x0000E407
		protected override void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e)
		{
			this.OutputIdentifier(e.ParameterName);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00010215 File Offset: 0x0000E415
		protected override void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e)
		{
			this.OutputIdentifier(e.VariableName);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00010224 File Offset: 0x0000E424
		protected override void GenerateIndexerExpression(CodeIndexerExpression e)
		{
			base.GenerateExpression(e.TargetObject);
			if (e.TargetObject is CodeBaseReferenceExpression)
			{
				base.Output.Write(".Item");
			}
			base.Output.Write('(');
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
					base.Output.Write(", ");
				}
				base.GenerateExpression(codeExpression);
			}
			base.Output.Write(')');
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000102DC File Offset: 0x0000E4DC
		protected override void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e)
		{
			base.GenerateExpression(e.TargetObject);
			base.Output.Write('(');
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
					base.Output.Write(", ");
				}
				base.GenerateExpression(codeExpression);
			}
			base.Output.Write(')');
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00010374 File Offset: 0x0000E574
		protected override void GenerateSnippetExpression(CodeSnippetExpression e)
		{
			base.Output.Write(e.Value);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00010388 File Offset: 0x0000E588
		protected override void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e)
		{
			this.GenerateMethodReferenceExpression(e.Method);
			if (e.Parameters.Count > 0)
			{
				base.Output.Write('(');
				this.OutputExpressionList(e.Parameters);
				base.Output.Write(')');
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000103D8 File Offset: 0x0000E5D8
		protected override void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				base.GenerateExpression(e.TargetObject);
				base.Output.Write('.');
				base.Output.Write(e.MethodName);
			}
			else
			{
				this.OutputIdentifier(e.MethodName);
			}
			if (e.TypeArguments.Count > 0)
			{
				base.Output.Write(this.GetTypeArgumentsOutput(e.TypeArguments));
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001044C File Offset: 0x0000E64C
		protected override void GenerateEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject == null)
			{
				this.OutputIdentifier(e.EventName + "Event");
				return;
			}
			bool flag = e.TargetObject is CodeThisReferenceExpression;
			base.GenerateExpression(e.TargetObject);
			base.Output.Write('.');
			if (flag)
			{
				base.Output.Write(e.EventName + "Event");
				return;
			}
			base.Output.Write(e.EventName);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000104CE File Offset: 0x0000E6CE
		private void GenerateFormalEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject != null && !(e.TargetObject is CodeThisReferenceExpression))
			{
				base.GenerateExpression(e.TargetObject);
				base.Output.Write('.');
			}
			this.OutputIdentifier(e.EventName);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001050C File Offset: 0x0000E70C
		protected override void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e)
		{
			if (e.TargetObject != null)
			{
				if (e.TargetObject is CodeEventReferenceExpression)
				{
					base.Output.Write("RaiseEvent ");
					this.GenerateFormalEventReferenceExpression((CodeEventReferenceExpression)e.TargetObject);
				}
				else
				{
					base.GenerateExpression(e.TargetObject);
				}
			}
			if (e.Parameters.Count > 0)
			{
				base.Output.Write('(');
				this.OutputExpressionList(e.Parameters);
				base.Output.Write(')');
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00010594 File Offset: 0x0000E794
		protected override void GenerateObjectCreateExpression(CodeObjectCreateExpression e)
		{
			base.Output.Write("New ");
			this.OutputType(e.CreateType);
			base.Output.Write('(');
			this.OutputExpressionList(e.Parameters);
			base.Output.Write(')');
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000105E3 File Offset: 0x0000E7E3
		protected override void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, true);
			}
			this.OutputDirection(e.Direction);
			this.OutputTypeNamePair(e.Type, e.Name);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001061E File Offset: 0x0000E81E
		protected override void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e)
		{
			base.Output.Write("value");
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00010630 File Offset: 0x0000E830
		protected override void GenerateThisReferenceExpression(CodeThisReferenceExpression e)
		{
			base.Output.Write("Me");
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00010642 File Offset: 0x0000E842
		protected override void GenerateExpressionStatement(CodeExpressionStatement e)
		{
			base.GenerateExpression(e.Expression);
			base.Output.WriteLine();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001065B File Offset: 0x0000E85B
		private bool IsDocComment(CodeCommentStatement comment)
		{
			return comment != null && comment.Comment != null && comment.Comment.DocComment;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00010678 File Offset: 0x0000E878
		protected override void GenerateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement codeCommentStatement = (CodeCommentStatement)obj;
				if (!this.IsDocComment(codeCommentStatement))
				{
					this.GenerateCommentStatement(codeCommentStatement);
				}
			}
			foreach (object obj2 in e)
			{
				CodeCommentStatement codeCommentStatement2 = (CodeCommentStatement)obj2;
				if (this.IsDocComment(codeCommentStatement2))
				{
					this.GenerateCommentStatement(codeCommentStatement2);
				}
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00010724 File Offset: 0x0000E924
		protected override void GenerateComment(CodeComment e)
		{
			string text = (e.DocComment ? "'''" : "'");
			base.Output.Write(text);
			string text2 = e.Text;
			for (int i = 0; i < text2.Length; i++)
			{
				base.Output.Write(text2[i]);
				if (text2[i] == '\r')
				{
					if (i < text2.Length - 1 && text2[i + 1] == '\n')
					{
						base.Output.Write('\n');
						i++;
					}
					((ExposedTabStringIndentedTextWriter)base.Output).InternalOutputTabs();
					base.Output.Write(text);
				}
				else if (text2[i] == '\n')
				{
					((ExposedTabStringIndentedTextWriter)base.Output).InternalOutputTabs();
					base.Output.Write(text);
				}
				else if (text2[i] == '\u2028' || text2[i] == '\u2029' || text2[i] == '\u0085')
				{
					base.Output.Write(text);
				}
			}
			base.Output.WriteLine();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00010840 File Offset: 0x0000EA40
		protected override void GenerateMethodReturnStatement(CodeMethodReturnStatement e)
		{
			if (e.Expression != null)
			{
				base.Output.Write("Return ");
				base.GenerateExpression(e.Expression);
				base.Output.WriteLine();
				return;
			}
			base.Output.WriteLine("Return");
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00010890 File Offset: 0x0000EA90
		protected override void GenerateConditionStatement(CodeConditionStatement e)
		{
			base.Output.Write("If ");
			base.GenerateExpression(e.Condition);
			base.Output.WriteLine(" Then");
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.TrueStatements);
			num = base.Indent;
			base.Indent = num - 1;
			if (e.FalseStatements.Count > 0)
			{
				base.Output.Write("Else");
				base.Output.WriteLine();
				num = base.Indent;
				base.Indent = num + 1;
				this.GenerateVBStatements(e.FalseStatements);
				num = base.Indent;
				base.Indent = num - 1;
			}
			base.Output.WriteLine("End If");
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001095C File Offset: 0x0000EB5C
		protected override void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e)
		{
			base.Output.WriteLine("Try ");
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.TryStatements);
			num = base.Indent;
			base.Indent = num - 1;
			foreach (object obj in e.CatchClauses)
			{
				CodeCatchClause codeCatchClause = (CodeCatchClause)obj;
				base.Output.Write("Catch ");
				this.OutputTypeNamePair(codeCatchClause.CatchExceptionType, codeCatchClause.LocalName);
				base.Output.WriteLine();
				num = base.Indent;
				base.Indent = num + 1;
				this.GenerateVBStatements(codeCatchClause.Statements);
				num = base.Indent;
				base.Indent = num - 1;
			}
			CodeStatementCollection finallyStatements = e.FinallyStatements;
			if (finallyStatements.Count > 0)
			{
				base.Output.WriteLine("Finally");
				num = base.Indent;
				base.Indent = num + 1;
				this.GenerateVBStatements(finallyStatements);
				num = base.Indent;
				base.Indent = num - 1;
			}
			base.Output.WriteLine("End Try");
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		protected override void GenerateAssignStatement(CodeAssignStatement e)
		{
			base.GenerateExpression(e.Left);
			base.Output.Write(" = ");
			base.GenerateExpression(e.Right);
			base.Output.WriteLine();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		protected override void GenerateAttachEventStatement(CodeAttachEventStatement e)
		{
			base.Output.Write("AddHandler ");
			this.GenerateFormalEventReferenceExpression(e.Event);
			base.Output.Write(", ");
			base.GenerateExpression(e.Listener);
			base.Output.WriteLine();
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00010B28 File Offset: 0x0000ED28
		protected override void GenerateRemoveEventStatement(CodeRemoveEventStatement e)
		{
			base.Output.Write("RemoveHandler ");
			this.GenerateFormalEventReferenceExpression(e.Event);
			base.Output.Write(", ");
			base.GenerateExpression(e.Listener);
			base.Output.WriteLine();
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00010B78 File Offset: 0x0000ED78
		protected override void GenerateSnippetStatement(CodeSnippetStatement e)
		{
			base.Output.WriteLine(e.Value);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00010B8B File Offset: 0x0000ED8B
		protected override void GenerateGotoStatement(CodeGotoStatement e)
		{
			base.Output.Write("goto ");
			base.Output.WriteLine(e.Label);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		protected override void GenerateLabeledStatement(CodeLabeledStatement e)
		{
			int num = base.Indent;
			base.Indent = num - 1;
			base.Output.Write(e.Label);
			base.Output.WriteLine(':');
			num = base.Indent;
			base.Indent = num + 1;
			if (e.Statement != null)
			{
				base.GenerateStatement(e.Statement);
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00010C10 File Offset: 0x0000EE10
		protected override void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e)
		{
			bool flag = true;
			base.Output.Write("Dim ");
			CodeTypeReference type = e.Type;
			if (type.ArrayRank == 1 && e.InitExpression != null)
			{
				CodeArrayCreateExpression codeArrayCreateExpression = e.InitExpression as CodeArrayCreateExpression;
				if (codeArrayCreateExpression != null && codeArrayCreateExpression.Initializers.Count == 0)
				{
					flag = false;
					this.OutputIdentifier(e.Name);
					base.Output.Write('(');
					if (codeArrayCreateExpression.SizeExpression != null)
					{
						base.Output.Write('(');
						base.GenerateExpression(codeArrayCreateExpression.SizeExpression);
						base.Output.Write(") - 1");
					}
					else
					{
						base.Output.Write(codeArrayCreateExpression.Size - 1);
					}
					base.Output.Write(')');
					if (type.ArrayElementType != null)
					{
						this.OutputArrayPostfix(type.ArrayElementType);
					}
					base.Output.Write(" As ");
					this.OutputType(type);
				}
				else
				{
					this.OutputTypeNamePair(e.Type, e.Name);
				}
			}
			else
			{
				this.OutputTypeNamePair(e.Type, e.Name);
			}
			if (flag && e.InitExpression != null)
			{
				base.Output.Write(" = ");
				base.GenerateExpression(e.InitExpression);
			}
			base.Output.WriteLine();
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00010D64 File Offset: 0x0000EF64
		protected override void GenerateLinePragmaStart(CodeLinePragma e)
		{
			base.Output.WriteLine();
			base.Output.Write("#ExternalSource(\"");
			base.Output.Write(e.FileName);
			base.Output.Write("\",");
			base.Output.Write(e.LineNumber);
			base.Output.WriteLine(')');
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00010DCB File Offset: 0x0000EFCB
		protected override void GenerateLinePragmaEnd(CodeLinePragma e)
		{
			base.Output.WriteLine();
			base.Output.WriteLine("#End ExternalSource");
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00010DE8 File Offset: 0x0000EFE8
		protected override void GenerateEvent(CodeMemberEvent e, CodeTypeDeclaration c)
		{
			if (base.IsCurrentDelegate || base.IsCurrentEnum)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			string name = e.Name;
			if (e.PrivateImplementationType != null)
			{
				string text = this.GetBaseTypeOutput(e.PrivateImplementationType, false);
				text = text.Replace('.', '_');
				e.Name = text + "_" + e.Name;
			}
			this.OutputMemberAccessModifier(e.Attributes);
			base.Output.Write("Event ");
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.ImplementationTypes.Count > 0)
			{
				base.Output.Write(" Implements ");
				bool flag = true;
				using (IEnumerator enumerator = e.ImplementationTypes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
						if (flag)
						{
							flag = false;
						}
						else
						{
							base.Output.Write(" , ");
						}
						this.OutputType(codeTypeReference);
						base.Output.Write('.');
						this.OutputIdentifier(name);
					}
					goto IL_015D;
				}
			}
			if (e.PrivateImplementationType != null)
			{
				base.Output.Write(" Implements ");
				this.OutputType(e.PrivateImplementationType);
				base.Output.Write('.');
				this.OutputIdentifier(name);
			}
			IL_015D:
			base.Output.WriteLine();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00010F70 File Offset: 0x0000F170
		protected override void GenerateField(CodeMemberField e)
		{
			if (base.IsCurrentDelegate || base.IsCurrentInterface)
			{
				return;
			}
			if (base.IsCurrentEnum)
			{
				if (e.CustomAttributes.Count > 0)
				{
					this.OutputAttributes(e.CustomAttributes, false);
				}
				this.OutputIdentifier(e.Name);
				if (e.InitExpression != null)
				{
					base.Output.Write(" = ");
					base.GenerateExpression(e.InitExpression);
				}
				base.Output.WriteLine();
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			this.OutputVTableModifier(e.Attributes);
			this.OutputFieldScopeModifier(e.Attributes);
			if (this.GetUserData(e, "WithEvents", false))
			{
				base.Output.Write("WithEvents ");
			}
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.InitExpression != null)
			{
				base.Output.Write(" = ");
				base.GenerateExpression(e.InitExpression);
			}
			base.Output.WriteLine();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001108C File Offset: 0x0000F28C
		private bool MethodIsOverloaded(CodeMemberMethod e, CodeTypeDeclaration c)
		{
			if ((e.Attributes & MemberAttributes.Overloaded) != (MemberAttributes)0)
			{
				return true;
			}
			foreach (object obj in c.Members)
			{
				if (obj is CodeMemberMethod)
				{
					CodeMemberMethod codeMemberMethod = (CodeMemberMethod)obj;
					if (!(obj is CodeTypeConstructor) && !(obj is CodeConstructor) && codeMemberMethod != e && codeMemberMethod.Name.Equals(e.Name, StringComparison.OrdinalIgnoreCase) && codeMemberMethod.PrivateImplementationType == null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00011134 File Offset: 0x0000F334
		protected override void GenerateSnippetMember(CodeSnippetTypeMember e)
		{
			base.Output.Write(e.Text);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00011148 File Offset: 0x0000F348
		protected override void GenerateMethod(CodeMemberMethod e, CodeTypeDeclaration c)
		{
			if (!base.IsCurrentClass && !base.IsCurrentStruct && !base.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			string name = e.Name;
			if (e.PrivateImplementationType != null)
			{
				string text = this.GetBaseTypeOutput(e.PrivateImplementationType, false);
				text = text.Replace('.', '_');
				e.Name = text + "_" + e.Name;
			}
			if (!base.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					if (this.MethodIsOverloaded(e, c))
					{
						base.Output.Write("Overloads ");
					}
				}
				this.OutputVTableModifier(e.Attributes);
				this.OutputMemberScopeModifier(e.Attributes);
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			bool flag = false;
			if (e.ReturnType.BaseType.Length == 0 || string.Equals(e.ReturnType.BaseType, typeof(void).FullName, StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
			}
			if (flag)
			{
				base.Output.Write("Sub ");
			}
			else
			{
				base.Output.Write("Function ");
			}
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			base.Output.Write('(');
			this.OutputParameters(e.Parameters);
			base.Output.Write(')');
			if (!flag)
			{
				base.Output.Write(" As ");
				if (e.ReturnTypeCustomAttributes.Count > 0)
				{
					this.OutputAttributes(e.ReturnTypeCustomAttributes, true);
				}
				this.OutputType(e.ReturnType);
				this.OutputArrayPostfix(e.ReturnType);
			}
			if (e.ImplementationTypes.Count > 0)
			{
				base.Output.Write(" Implements ");
				bool flag2 = true;
				using (IEnumerator enumerator = e.ImplementationTypes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
						if (flag2)
						{
							flag2 = false;
						}
						else
						{
							base.Output.Write(" , ");
						}
						this.OutputType(codeTypeReference);
						base.Output.Write('.');
						this.OutputIdentifier(name);
					}
					goto IL_027B;
				}
			}
			if (e.PrivateImplementationType != null)
			{
				base.Output.Write(" Implements ");
				this.OutputType(e.PrivateImplementationType);
				base.Output.Write('.');
				this.OutputIdentifier(name);
			}
			IL_027B:
			base.Output.WriteLine();
			if (!base.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				int num = base.Indent;
				base.Indent = num + 1;
				this.GenerateVBStatements(e.Statements);
				num = base.Indent;
				base.Indent = num - 1;
				if (flag)
				{
					base.Output.WriteLine("End Sub");
				}
				else
				{
					base.Output.WriteLine("End Function");
				}
			}
			e.Name = name;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001145C File Offset: 0x0000F65C
		protected override void GenerateEntryPointMethod(CodeEntryPointMethod e, CodeTypeDeclaration c)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			base.Output.WriteLine("Public Shared Sub Main()");
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.Statements);
			num = base.Indent;
			base.Indent = num - 1;
			base.Output.WriteLine("End Sub");
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000114D0 File Offset: 0x0000F6D0
		private bool PropertyIsOverloaded(CodeMemberProperty e, CodeTypeDeclaration c)
		{
			if ((e.Attributes & MemberAttributes.Overloaded) != (MemberAttributes)0)
			{
				return true;
			}
			foreach (object obj in c.Members)
			{
				if (obj is CodeMemberProperty)
				{
					CodeMemberProperty codeMemberProperty = (CodeMemberProperty)obj;
					if (codeMemberProperty != e && codeMemberProperty.Name.Equals(e.Name, StringComparison.OrdinalIgnoreCase) && codeMemberProperty.PrivateImplementationType == null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00011568 File Offset: 0x0000F768
		protected override void GenerateProperty(CodeMemberProperty e, CodeTypeDeclaration c)
		{
			if (!base.IsCurrentClass && !base.IsCurrentStruct && !base.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			string name = e.Name;
			if (e.PrivateImplementationType != null)
			{
				string text = this.GetBaseTypeOutput(e.PrivateImplementationType, false);
				text = text.Replace('.', '_');
				e.Name = text + "_" + e.Name;
			}
			if (!base.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					if (this.PropertyIsOverloaded(e, c))
					{
						base.Output.Write("Overloads ");
					}
				}
				this.OutputVTableModifier(e.Attributes);
				this.OutputMemberScopeModifier(e.Attributes);
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			if (e.Parameters.Count > 0 && string.Equals(e.Name, "Item", StringComparison.OrdinalIgnoreCase))
			{
				base.Output.Write("Default ");
			}
			if (e.HasGet)
			{
				if (!e.HasSet)
				{
					base.Output.Write("ReadOnly ");
				}
			}
			else if (e.HasSet)
			{
				base.Output.Write("WriteOnly ");
			}
			base.Output.Write("Property ");
			this.OutputIdentifier(e.Name);
			base.Output.Write('(');
			if (e.Parameters.Count > 0)
			{
				this.OutputParameters(e.Parameters);
			}
			base.Output.Write(')');
			base.Output.Write(" As ");
			this.OutputType(e.Type);
			this.OutputArrayPostfix(e.Type);
			if (e.ImplementationTypes.Count > 0)
			{
				base.Output.Write(" Implements ");
				bool flag = true;
				using (IEnumerator enumerator = e.ImplementationTypes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
						if (flag)
						{
							flag = false;
						}
						else
						{
							base.Output.Write(" , ");
						}
						this.OutputType(codeTypeReference);
						base.Output.Write('.');
						this.OutputIdentifier(name);
					}
					goto IL_0276;
				}
			}
			if (e.PrivateImplementationType != null)
			{
				base.Output.Write(" Implements ");
				this.OutputType(e.PrivateImplementationType);
				base.Output.Write('.');
				this.OutputIdentifier(name);
			}
			IL_0276:
			base.Output.WriteLine();
			if (!c.IsInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				int num = base.Indent;
				base.Indent = num + 1;
				if (e.HasGet)
				{
					base.Output.WriteLine("Get");
					if (!base.IsCurrentInterface)
					{
						num = base.Indent;
						base.Indent = num + 1;
						this.GenerateVBStatements(e.GetStatements);
						e.Name = name;
						num = base.Indent;
						base.Indent = num - 1;
						base.Output.WriteLine("End Get");
					}
				}
				if (e.HasSet)
				{
					base.Output.WriteLine("Set");
					if (!base.IsCurrentInterface)
					{
						num = base.Indent;
						base.Indent = num + 1;
						this.GenerateVBStatements(e.SetStatements);
						num = base.Indent;
						base.Indent = num - 1;
						base.Output.WriteLine("End Set");
					}
				}
				num = base.Indent;
				base.Indent = num - 1;
				base.Output.WriteLine("End Property");
			}
			e.Name = name;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00011924 File Offset: 0x0000FB24
		protected override void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				base.GenerateExpression(e.TargetObject);
				base.Output.Write('.');
				base.Output.Write(e.PropertyName);
				return;
			}
			this.OutputIdentifier(e.PropertyName);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00011970 File Offset: 0x0000FB70
		protected override void GenerateConstructor(CodeConstructor e, CodeTypeDeclaration c)
		{
			if (!base.IsCurrentClass && !base.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			base.Output.Write("Sub New(");
			this.OutputParameters(e.Parameters);
			base.Output.WriteLine(')');
			int num = base.Indent;
			base.Indent = num + 1;
			CodeExpressionCollection baseConstructorArgs = e.BaseConstructorArgs;
			CodeExpressionCollection chainedConstructorArgs = e.ChainedConstructorArgs;
			if (chainedConstructorArgs.Count > 0)
			{
				base.Output.Write("Me.New(");
				this.OutputExpressionList(chainedConstructorArgs);
				base.Output.Write(')');
				base.Output.WriteLine();
			}
			else if (baseConstructorArgs.Count > 0)
			{
				base.Output.Write("MyBase.New(");
				this.OutputExpressionList(baseConstructorArgs);
				base.Output.Write(')');
				base.Output.WriteLine();
			}
			else if (base.IsCurrentClass)
			{
				base.Output.WriteLine("MyBase.New");
			}
			this.GenerateVBStatements(e.Statements);
			num = base.Indent;
			base.Indent = num - 1;
			base.Output.WriteLine("End Sub");
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		protected override void GenerateTypeConstructor(CodeTypeConstructor e)
		{
			if (!base.IsCurrentClass && !base.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			base.Output.WriteLine("Shared Sub New()");
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.Statements);
			num = base.Indent;
			base.Indent = num - 1;
			base.Output.WriteLine("End Sub");
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00011B39 File Offset: 0x0000FD39
		protected override void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			base.Output.Write("GetType(");
			base.Output.Write(this.GetTypeOutput(e.Type));
			base.Output.Write(')');
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00011B70 File Offset: 0x0000FD70
		protected override void GenerateTypeStart(CodeTypeDeclaration e)
		{
			if (base.IsCurrentDelegate)
			{
				if (e.CustomAttributes.Count > 0)
				{
					this.OutputAttributes(e.CustomAttributes, false);
				}
				TypeAttributes typeAttributes = e.TypeAttributes & TypeAttributes.VisibilityMask;
				if (typeAttributes != TypeAttributes.NotPublic && typeAttributes == TypeAttributes.Public)
				{
					base.Output.Write("Public ");
				}
				CodeTypeDelegate codeTypeDelegate = (CodeTypeDelegate)e;
				if (codeTypeDelegate.ReturnType.BaseType.Length > 0 && !string.Equals(codeTypeDelegate.ReturnType.BaseType, "System.Void", StringComparison.OrdinalIgnoreCase))
				{
					base.Output.Write("Delegate Function ");
				}
				else
				{
					base.Output.Write("Delegate Sub ");
				}
				this.OutputIdentifier(e.Name);
				base.Output.Write('(');
				this.OutputParameters(codeTypeDelegate.Parameters);
				base.Output.Write(')');
				if (codeTypeDelegate.ReturnType.BaseType.Length > 0 && !string.Equals(codeTypeDelegate.ReturnType.BaseType, "System.Void", StringComparison.OrdinalIgnoreCase))
				{
					base.Output.Write(" As ");
					this.OutputType(codeTypeDelegate.ReturnType);
					this.OutputArrayPostfix(codeTypeDelegate.ReturnType);
				}
				base.Output.WriteLine();
				return;
			}
			int num;
			if (e.IsEnum)
			{
				if (e.CustomAttributes.Count > 0)
				{
					this.OutputAttributes(e.CustomAttributes, false);
				}
				this.OutputTypeAttributes(e);
				this.OutputIdentifier(e.Name);
				if (e.BaseTypes.Count > 0)
				{
					base.Output.Write(" As ");
					this.OutputType(e.BaseTypes[0]);
				}
				base.Output.WriteLine();
				num = base.Indent;
				base.Indent = num + 1;
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			this.OutputTypeAttributes(e);
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			bool flag = false;
			bool flag2 = false;
			if (e.IsStruct)
			{
				flag = true;
			}
			if (e.IsInterface)
			{
				flag2 = true;
			}
			num = base.Indent;
			base.Indent = num + 1;
			foreach (object obj in e.BaseTypes)
			{
				CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
				if (!flag && (e.IsInterface || !codeTypeReference.IsInterface))
				{
					base.Output.WriteLine();
					base.Output.Write("Inherits ");
					flag = true;
				}
				else if (!flag2)
				{
					base.Output.WriteLine();
					base.Output.Write("Implements ");
					flag2 = true;
				}
				else
				{
					base.Output.Write(", ");
				}
				this.OutputType(codeTypeReference);
			}
			base.Output.WriteLine();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00011E58 File Offset: 0x00010058
		private void OutputTypeParameters(CodeTypeParameterCollection typeParameters)
		{
			if (typeParameters.Count == 0)
			{
				return;
			}
			base.Output.Write("(Of ");
			bool flag = true;
			for (int i = 0; i < typeParameters.Count; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					base.Output.Write(", ");
				}
				base.Output.Write(typeParameters[i].Name);
				this.OutputTypeParameterConstraints(typeParameters[i]);
			}
			base.Output.Write(')');
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00011EDC File Offset: 0x000100DC
		private void OutputTypeParameterConstraints(CodeTypeParameter typeParameter)
		{
			CodeTypeReferenceCollection constraints = typeParameter.Constraints;
			int num = constraints.Count;
			if (typeParameter.HasConstructorConstraint)
			{
				num++;
			}
			if (num == 0)
			{
				return;
			}
			base.Output.Write(" As ");
			if (num > 1)
			{
				base.Output.Write(" {");
			}
			bool flag = true;
			foreach (object obj in constraints)
			{
				CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					base.Output.Write(", ");
				}
				base.Output.Write(this.GetTypeOutput(codeTypeReference));
			}
			if (typeParameter.HasConstructorConstraint)
			{
				if (!flag)
				{
					base.Output.Write(", ");
				}
				base.Output.Write("New");
			}
			if (num > 1)
			{
				base.Output.Write('}');
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00011FD8 File Offset: 0x000101D8
		protected override void GenerateTypeEnd(CodeTypeDeclaration e)
		{
			if (!base.IsCurrentDelegate)
			{
				int indent = base.Indent;
				base.Indent = indent - 1;
				string text;
				if (e.IsEnum)
				{
					text = "End Enum";
				}
				else if (e.IsInterface)
				{
					text = "End Interface";
				}
				else if (e.IsStruct)
				{
					text = "End Structure";
				}
				else if (this.IsCurrentModule)
				{
					text = "End Module";
				}
				else
				{
					text = "End Class";
				}
				base.Output.WriteLine(text);
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00012050 File Offset: 0x00010250
		protected override void GenerateNamespace(CodeNamespace e)
		{
			if (this.GetUserData(e, "GenerateImports", true))
			{
				base.GenerateNamespaceImports(e);
			}
			base.Output.WriteLine();
			this.GenerateCommentStatements(e.Comments);
			this.GenerateNamespaceStart(e);
			base.GenerateTypes(e);
			this.GenerateNamespaceEnd(e);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000120A0 File Offset: 0x000102A0
		private bool AllowLateBound(CodeCompileUnit e)
		{
			object obj = e.UserData["AllowLateBound"];
			return obj == null || !(obj is bool) || (bool)obj;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000120D4 File Offset: 0x000102D4
		private bool RequireVariableDeclaration(CodeCompileUnit e)
		{
			object obj = e.UserData["RequireVariableDeclaration"];
			return obj == null || !(obj is bool) || (bool)obj;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00012108 File Offset: 0x00010308
		private bool GetUserData(CodeObject e, string property, bool defaultValue)
		{
			object obj = e.UserData[property];
			if (obj != null && obj is bool)
			{
				return (bool)obj;
			}
			return defaultValue;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00012138 File Offset: 0x00010338
		protected override void GenerateCompileUnitStart(CodeCompileUnit e)
		{
			base.GenerateCompileUnitStart(e);
			base.Output.WriteLine("'------------------------------------------------------------------------------");
			base.Output.Write("' <");
			base.Output.WriteLine("auto-generated>");
			base.Output.Write("'     ");
			base.Output.WriteLine("This code was generated by a tool.");
			base.Output.Write("'     ");
			base.Output.Write("Runtime Version:");
			base.Output.WriteLine(Environment.Version.ToString());
			base.Output.WriteLine("'");
			base.Output.Write("'     ");
			base.Output.WriteLine("Changes to this file may cause incorrect behavior and will be lost if");
			base.Output.Write("'     ");
			base.Output.WriteLine("the code is regenerated.");
			base.Output.Write("' </");
			base.Output.WriteLine("auto-generated>");
			base.Output.WriteLine("'------------------------------------------------------------------------------");
			base.Output.WriteLine();
			if (this.AllowLateBound(e))
			{
				base.Output.WriteLine("Option Strict Off");
			}
			else
			{
				base.Output.WriteLine("Option Strict On");
			}
			if (!this.RequireVariableDeclaration(e))
			{
				base.Output.WriteLine("Option Explicit Off");
			}
			else
			{
				base.Output.WriteLine("Option Explicit On");
			}
			base.Output.WriteLine();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000122C0 File Offset: 0x000104C0
		protected override void GenerateCompileUnit(CodeCompileUnit e)
		{
			this.GenerateCompileUnitStart(e);
			SortedSet<string> sortedSet = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				codeNamespace.UserData["GenerateImports"] = false;
				foreach (object obj2 in codeNamespace.Imports)
				{
					CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj2;
					sortedSet.Add(codeNamespaceImport.Namespace);
				}
			}
			foreach (string text in sortedSet)
			{
				base.Output.Write("Imports ");
				this.OutputIdentifier(text);
				base.Output.WriteLine();
			}
			if (e.AssemblyCustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.AssemblyCustomAttributes, false, "Assembly: ", true);
			}
			base.GenerateNamespaces(e);
			this.GenerateCompileUnitEnd(e);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00012418 File Offset: 0x00010618
		protected override void GenerateDirectives(CodeDirectiveCollection directives)
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

		// Token: 0x0600056D RID: 1389 RVA: 0x00012468 File Offset: 0x00010668
		private void GenerateChecksumPragma(CodeChecksumPragma checksumPragma)
		{
			base.Output.Write("#ExternalChecksum(\"");
			base.Output.Write(checksumPragma.FileName);
			base.Output.Write("\",\"");
			base.Output.Write(checksumPragma.ChecksumAlgorithmId.ToString("B", CultureInfo.InvariantCulture));
			base.Output.Write("\",\"");
			if (checksumPragma.ChecksumData != null)
			{
				foreach (byte b in checksumPragma.ChecksumData)
				{
					base.Output.Write(b.ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			base.Output.WriteLine("\")");
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00012528 File Offset: 0x00010728
		private void GenerateCodeRegionDirective(CodeRegionDirective regionDirective)
		{
			if (this.IsGeneratingStatements())
			{
				return;
			}
			if (regionDirective.RegionMode == CodeRegionMode.Start)
			{
				base.Output.Write("#Region \"");
				base.Output.Write(regionDirective.RegionText);
				base.Output.WriteLine("\"");
				return;
			}
			if (regionDirective.RegionMode == CodeRegionMode.End)
			{
				base.Output.WriteLine("#End Region");
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00012594 File Offset: 0x00010794
		protected override void GenerateNamespaceStart(CodeNamespace e)
		{
			if (!string.IsNullOrEmpty(e.Name))
			{
				base.Output.Write("Namespace ");
				string[] array = e.Name.Split(VBCodeGenerator.s_periodArray);
				this.OutputIdentifier(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					base.Output.Write('.');
					this.OutputIdentifier(array[i]);
				}
				base.Output.WriteLine();
				int indent = base.Indent;
				base.Indent = indent + 1;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00012618 File Offset: 0x00010818
		protected override void GenerateNamespaceEnd(CodeNamespace e)
		{
			if (!string.IsNullOrEmpty(e.Name))
			{
				int indent = base.Indent;
				base.Indent = indent - 1;
				base.Output.WriteLine("End Namespace");
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00012652 File Offset: 0x00010852
		protected override void GenerateNamespaceImport(CodeNamespaceImport e)
		{
			base.Output.Write("Imports ");
			this.OutputIdentifier(e.Namespace);
			base.Output.WriteLine();
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001267B File Offset: 0x0001087B
		protected override void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes)
		{
			base.Output.Write('<');
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001268A File Offset: 0x0001088A
		protected override void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes)
		{
			base.Output.Write('>');
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00012699 File Offset: 0x00010899
		public static bool IsKeyword(string value)
		{
			return FixedStringLookup.Contains(VBCodeGenerator.s_keywords, value, true);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000126A7 File Offset: 0x000108A7
		protected override bool Supports(GeneratorSupport support)
		{
			return (support & (GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties)) == support;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000126B4 File Offset: 0x000108B4
		protected override bool IsValidIdentifier(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			if (value.Length > 1023)
			{
				return false;
			}
			if (value[0] != '[' || value[value.Length - 1] != ']')
			{
				if (VBCodeGenerator.IsKeyword(value))
				{
					return false;
				}
			}
			else
			{
				value = value.Substring(1, value.Length - 2);
			}
			return (value.Length != 1 || value[0] != '_') && CodeGenerator.IsValidLanguageIndependentIdentifier(value);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001272E File Offset: 0x0001092E
		protected override string CreateValidIdentifier(string name)
		{
			if (VBCodeGenerator.IsKeyword(name))
			{
				return "_" + name;
			}
			return name;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00012745 File Offset: 0x00010945
		protected override string CreateEscapedIdentifier(string name)
		{
			if (VBCodeGenerator.IsKeyword(name))
			{
				return "[" + name + "]";
			}
			return name;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00012764 File Offset: 0x00010964
		private string GetBaseTypeOutput(CodeTypeReference typeRef, bool preferBuiltInTypes = true)
		{
			string baseType = typeRef.BaseType;
			if (preferBuiltInTypes)
			{
				if (baseType.Length == 0)
				{
					return "Void";
				}
				string text = baseType.ToLowerInvariant();
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1774064579U)
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
										return "String";
									}
								}
							}
							else if (text == "system.char")
							{
								return "Char";
							}
						}
						else if (num != 507700544U)
						{
							if (num == 574663925U)
							{
								if (text == "system.uint16")
								{
									return "UShort";
								}
							}
						}
						else if (text == "system.uint64")
						{
							return "ULong";
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
									return "Byte";
								}
							}
						}
						else if (text == "system.int32")
						{
							return "Integer";
						}
					}
					else if (num != 1487069339U)
					{
						if (num == 1774064579U)
						{
							if (text == "system.datetime")
							{
								return "Date";
							}
						}
					}
					else if (text == "system.double")
					{
						return "Double";
					}
				}
				else if (num <= 2647511797U)
				{
					if (num <= 2446023237U)
					{
						if (num != 2218649502U)
						{
							if (num == 2446023237U)
							{
								if (text == "system.decimal")
								{
									return "Decimal";
								}
							}
						}
						else if (text == "system.boolean")
						{
							return "Boolean";
						}
					}
					else if (num != 2613725868U)
					{
						if (num == 2647511797U)
						{
							if (text == "system.object")
							{
								return "Object";
							}
						}
					}
					else if (text == "system.int16")
					{
						return "Short";
					}
				}
				else if (num <= 2923133227U)
				{
					if (num != 2679997701U)
					{
						if (num == 2923133227U)
						{
							if (text == "system.uint32")
							{
								return "UInteger";
							}
						}
					}
					else if (text == "system.int64")
					{
						return "Long";
					}
				}
				else if (num != 3248684926U)
				{
					if (num == 3680803037U)
					{
						if (text == "system.sbyte")
						{
							return "SByte";
						}
					}
				}
				else if (text == "system.single")
				{
					return "Single";
				}
			}
			StringBuilder stringBuilder = new StringBuilder(baseType.Length + 10);
			if ((typeRef.Options & CodeTypeReferenceOptions.GlobalReference) != (CodeTypeReferenceOptions)0)
			{
				stringBuilder.Append("Global.");
			}
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < baseType.Length; i++)
			{
				char c = baseType[i];
				if (c != '+' && c != '.')
				{
					if (c == '`')
					{
						stringBuilder.Append(this.CreateEscapedIdentifier(baseType.Substring(num2, i - num2)));
						i++;
						int num4 = 0;
						while (i < baseType.Length && baseType[i] >= '0' && baseType[i] <= '9')
						{
							num4 = num4 * 10 + (int)(baseType[i] - '0');
							i++;
						}
						this.GetTypeArgumentsOutput(typeRef.TypeArguments, num3, num4, stringBuilder);
						num3 += num4;
						if (i < baseType.Length && (baseType[i] == '+' || baseType[i] == '.'))
						{
							stringBuilder.Append('.');
							i++;
						}
						num2 = i;
					}
				}
				else
				{
					stringBuilder.Append(this.CreateEscapedIdentifier(baseType.Substring(num2, i - num2)));
					stringBuilder.Append('.');
					i++;
					num2 = i;
				}
			}
			if (num2 < baseType.Length)
			{
				stringBuilder.Append(this.CreateEscapedIdentifier(baseType.Substring(num2)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00012BD0 File Offset: 0x00010DD0
		private string GetTypeOutputWithoutArrayPostFix(CodeTypeReference typeRef)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (typeRef.ArrayElementType != null)
			{
				typeRef = typeRef.ArrayElementType;
			}
			stringBuilder.Append(this.GetBaseTypeOutput(typeRef, true));
			return stringBuilder.ToString();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00012C0C File Offset: 0x00010E0C
		private string GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			this.GetTypeArgumentsOutput(typeArguments, 0, typeArguments.Count, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00012C3C File Offset: 0x00010E3C
		private void GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments, int start, int length, StringBuilder sb)
		{
			sb.Append("(Of ");
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
			sb.Append(')');
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00012CA4 File Offset: 0x00010EA4
		protected override string GetTypeOutput(CodeTypeReference typeRef)
		{
			string text = string.Empty;
			text += this.GetTypeOutputWithoutArrayPostFix(typeRef);
			if (typeRef.ArrayRank > 0)
			{
				text += this.GetArrayPostfix(typeRef);
			}
			return text;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00012CDD File Offset: 0x00010EDD
		protected override void ContinueOnNewLine(string st)
		{
			base.Output.Write(st);
			base.Output.WriteLine(" _");
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00012CFB File Offset: 0x00010EFB
		private bool IsGeneratingStatements()
		{
			return this._statementDepth > 0;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00012D08 File Offset: 0x00010F08
		private void GenerateVBStatements(CodeStatementCollection stms)
		{
			this._statementDepth++;
			try
			{
				base.GenerateStatements(stms);
			}
			finally
			{
				this._statementDepth--;
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00012D4C File Offset: 0x00010F4C
		protected override CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults compilerResults = new CompilerResults(options.TempFiles);
			Process process = new Process();
			string text = "";
			if (Path.DirectorySeparatorChar == '\\')
			{
				process.StartInfo.FileName = MonoToolsLocator.Mono;
				process.StartInfo.Arguments = MonoToolsLocator.VBCompiler + " " + VBCodeGenerator.BuildArgs(options, fileNames);
			}
			else
			{
				process.StartInfo.FileName = MonoToolsLocator.VBCompiler;
				process.StartInfo.Arguments = VBCodeGenerator.BuildArgs(options, fileNames);
			}
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
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
				text = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
			}
			finally
			{
				compilerResults.NativeCompilerReturnValue = process.ExitCode;
				process.Close();
			}
			bool flag = true;
			if (compilerResults.NativeCompilerReturnValue == 1)
			{
				flag = false;
				string[] array = text.Split(Environment.NewLine.ToCharArray());
				for (int i = 0; i < array.Length; i++)
				{
					CompilerError compilerError = VBCodeGenerator.CreateErrorFromString(array[i]);
					if (compilerError != null)
					{
						compilerResults.Errors.Add(compilerError);
					}
				}
			}
			if ((!flag && !compilerResults.Errors.HasErrors) || (compilerResults.NativeCompilerReturnValue != 0 && compilerResults.NativeCompilerReturnValue != 1))
			{
				flag = false;
				CompilerError compilerError2 = new CompilerError(string.Empty, 0, 0, "VBNC_CRASH", text);
				compilerResults.Errors.Add(compilerError2);
			}
			if (flag)
			{
				if (options.GenerateInMemory)
				{
					using (FileStream fileStream = File.OpenRead(options.OutputAssembly))
					{
						byte[] array2 = new byte[fileStream.Length];
						fileStream.Read(array2, 0, array2.Length);
						compilerResults.CompiledAssembly = Assembly.Load(array2, null);
						fileStream.Close();
						return compilerResults;
					}
				}
				compilerResults.CompiledAssembly = Assembly.LoadFrom(options.OutputAssembly);
				compilerResults.PathToAssembly = options.OutputAssembly;
			}
			else
			{
				compilerResults.CompiledAssembly = null;
			}
			return compilerResults;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00012FB4 File Offset: 0x000111B4
		private static string BuildArgs(CompilerParameters options, string[] fileNames)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("/quiet ");
			if (options.GenerateExecutable)
			{
				stringBuilder.Append("/target:exe ");
			}
			else
			{
				stringBuilder.Append("/target:library ");
			}
			if (options.TreatWarningsAsErrors)
			{
				stringBuilder.Append("/warnaserror ");
			}
			if (options.OutputAssembly == null || options.OutputAssembly.Length == 0)
			{
				string text = (options.GenerateExecutable ? "exe" : "dll");
				options.OutputAssembly = VBCodeGenerator.GetTempFileNameWithExtension(options.TempFiles, text, !options.GenerateInMemory);
			}
			stringBuilder.AppendFormat("/out:\"{0}\" ", options.OutputAssembly);
			bool flag = false;
			if (options.ReferencedAssemblies != null)
			{
				foreach (string text2 in options.ReferencedAssemblies)
				{
					if (string.Compare(text2, "Microsoft.VisualBasic", true, CultureInfo.InvariantCulture) == 0)
					{
						flag = true;
					}
					stringBuilder.AppendFormat("/r:\"{0}\" ", text2);
				}
			}
			if (!flag)
			{
				stringBuilder.Append("/r:\"Microsoft.VisualBasic.dll\" ");
			}
			if (options.CompilerOptions != null)
			{
				stringBuilder.Append(options.CompilerOptions);
				stringBuilder.Append(" ");
			}
			foreach (string text3 in fileNames)
			{
				stringBuilder.AppendFormat(" \"{0}\" ", text3);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00013134 File Offset: 0x00011334
		private static CompilerError CreateErrorFromString(string error_string)
		{
			CompilerError compilerError = new CompilerError();
			Match match = new Regex("^(\\s*(?<file>.*)?\\((?<line>\\d*)(,(?<column>\\d*))?\\)\\s+)?:\\s*(?<level>Error|Warning)?\\s*(?<number>.*):\\s(?<message>.*)", RegexOptions.ExplicitCapture | RegexOptions.Compiled).Match(error_string);
			if (!match.Success)
			{
				return null;
			}
			if (string.Empty != match.Result("${file}"))
			{
				compilerError.FileName = match.Result("${file}").Trim();
			}
			if (string.Empty != match.Result("${line}"))
			{
				compilerError.Line = int.Parse(match.Result("${line}"));
			}
			if (string.Empty != match.Result("${column}"))
			{
				compilerError.Column = int.Parse(match.Result("${column}"));
			}
			if (match.Result("${level}").Trim() == "Warning")
			{
				compilerError.IsWarning = true;
			}
			compilerError.ErrorNumber = match.Result("${number}");
			compilerError.ErrorText = match.Result("${message}");
			return compilerError;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00013231 File Offset: 0x00011431
		private static string GetTempFileNameWithExtension(TempFileCollection temp_files, string extension, bool keepFile)
		{
			return temp_files.AddExtension(extension, keepFile);
		}

		// Token: 0x04000BD0 RID: 3024
		private static readonly char[] s_periodArray = new char[] { '.' };

		// Token: 0x04000BD1 RID: 3025
		private const int MaxLineLength = 80;

		// Token: 0x04000BD2 RID: 3026
		private const GeneratorSupport LanguageSupport = GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties;

		// Token: 0x04000BD3 RID: 3027
		private int _statementDepth;

		// Token: 0x04000BD4 RID: 3028
		private IDictionary<string, string> _provOptions;

		// Token: 0x04000BD5 RID: 3029
		private static readonly string[][] s_keywords = new string[][]
		{
			null,
			new string[] { "as", "do", "if", "in", "is", "me", "of", "on", "or", "to" },
			new string[]
			{
				"and", "dim", "end", "for", "get", "let", "lib", "mod", "new", "not",
				"rem", "set", "sub", "try", "xor"
			},
			new string[]
			{
				"ansi", "auto", "byte", "call", "case", "cdbl", "cdec", "char", "cint", "clng",
				"cobj", "csng", "cstr", "date", "each", "else", "enum", "exit", "goto", "like",
				"long", "loop", "next", "step", "stop", "then", "true", "wend", "when", "with"
			},
			new string[]
			{
				"alias", "byref", "byval", "catch", "cbool", "cbyte", "cchar", "cdate", "class", "const",
				"ctype", "cuint", "culng", "endif", "erase", "error", "event", "false", "gosub", "isnot",
				"redim", "sbyte", "short", "throw", "ulong", "until", "using", "while"
			},
			new string[]
			{
				"csbyte", "cshort", "double", "elseif", "friend", "global", "module", "mybase", "object", "option",
				"orelse", "public", "resume", "return", "select", "shared", "single", "static", "string", "typeof",
				"ushort"
			},
			new string[]
			{
				"andalso", "boolean", "cushort", "decimal", "declare", "default", "finally", "gettype", "handles", "imports",
				"integer", "myclass", "nothing", "partial", "private", "shadows", "trycast", "unicode", "variant"
			},
			new string[]
			{
				"assembly", "continue", "delegate", "function", "inherits", "operator", "optional", "preserve", "property", "readonly",
				"synclock", "uinteger", "widening"
			},
			new string[] { "addressof", "interface", "namespace", "narrowing", "overloads", "overrides", "protected", "structure", "writeonly" },
			new string[] { "addhandler", "directcast", "implements", "paramarray", "raiseevent", "withevents" },
			new string[] { "mustinherit", "overridable" },
			new string[] { "mustoverride" },
			new string[] { "removehandler" },
			new string[] { "class_finalize", "notinheritable", "notoverridable" },
			null,
			new string[] { "class_initialize" }
		};
	}
}
