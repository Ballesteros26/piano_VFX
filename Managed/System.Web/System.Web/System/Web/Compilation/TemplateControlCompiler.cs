using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000670 RID: 1648
	internal class TemplateControlCompiler : BaseCompiler
	{
		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x0600466C RID: 18028 RVA: 0x000C23CC File Offset: 0x000C05CC
		private List<string> MasterPageContentPlaceHolders
		{
			get
			{
				if (this.masterPageContentPlaceHolders == null)
				{
					this.masterPageContentPlaceHolders = new List<string>();
				}
				return this.masterPageContentPlaceHolders;
			}
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x000C23E7 File Offset: 0x000C05E7
		public TemplateControlCompiler(TemplateControlParser parser)
			: base(parser)
		{
			this.parser = parser;
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x000C23F8 File Offset: 0x000C05F8
		protected void EnsureID(ControlBuilder builder)
		{
			string id = builder.ID;
			if (id == null || id.Trim() == string.Empty)
			{
				builder.ID = builder.GetNextID(null);
			}
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x000C2430 File Offset: 0x000C0630
		private void CreateField(ControlBuilder builder, bool check)
		{
			if (builder == null || builder.ID == null || builder.ControlType == null)
			{
				return;
			}
			if (this.partialNameOverride[builder.ID] != null)
			{
				return;
			}
			MemberAttributes memberAttributes = MemberAttributes.Family;
			this.currentLocation = builder.Location;
			if (check && this.CheckBaseFieldOrProperty(builder.ID, builder.ControlType, ref memberAttributes))
			{
				return;
			}
			CodeMemberField codeMemberField = new CodeMemberField(builder.ControlType.FullName, builder.ID);
			codeMemberField.Attributes = memberAttributes;
			codeMemberField.Type.Options |= CodeTypeReferenceOptions.GlobalReference;
			if (this.partialClass != null)
			{
				this.partialClass.Members.Add(base.AddLinePragma(codeMemberField, builder));
				return;
			}
			this.mainClass.Members.Add(base.AddLinePragma(codeMemberField, builder));
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x000C2504 File Offset: 0x000C0704
		private bool CheckBaseFieldOrProperty(string id, Type type, ref MemberAttributes ma)
		{
			FieldInfo field = this.parser.BaseType.GetField(id, TemplateControlCompiler.noCaseFlags);
			Type type2 = null;
			if (field == null || field.IsPrivate)
			{
				PropertyInfo property = this.parser.BaseType.GetProperty(id, TemplateControlCompiler.noCaseFlags);
				if (property != null && property.GetSetMethod(true) != null)
				{
					type2 = property.PropertyType;
				}
			}
			else
			{
				type2 = field.FieldType;
			}
			if (type2 == null)
			{
				return false;
			}
			if (!type2.IsAssignableFrom(type))
			{
				ma |= MemberAttributes.New;
				return false;
			}
			return true;
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x000C2598 File Offset: 0x000C0798
		private void AddParsedSubObjectStmt(ControlBuilder builder, CodeExpression expr)
		{
			if (!builder.HaveParserVariable)
			{
				CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement();
				codeVariableDeclarationStatement.Name = "__parser";
				codeVariableDeclarationStatement.Type = new CodeTypeReference(typeof(IParserAccessor));
				codeVariableDeclarationStatement.InitExpression = new CodeCastExpression(typeof(IParserAccessor), TemplateControlCompiler.ctrlVar);
				builder.MethodStatements.Add(codeVariableDeclarationStatement);
				builder.HaveParserVariable = true;
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("__parser"), "AddParsedSubObject", Array.Empty<CodeExpression>());
			codeMethodInvokeExpression.Parameters.Add(expr);
			builder.MethodStatements.Add(base.AddLinePragma(codeMethodInvokeExpression, builder));
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x000C263C File Offset: 0x000C083C
		private CodeStatement CreateControlVariable(Type type, ControlBuilder builder, CodeMemberMethod method, CodeTypeReference ctrlTypeRef)
		{
			CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(ctrlTypeRef, Array.Empty<CodeExpression>());
			object[] array = ((type != null) ? type.GetCustomAttributes(typeof(ConstructorNeedsTagAttribute), true) : null);
			if (array != null && array.Length != 0)
			{
				if (((ConstructorNeedsTagAttribute)array[0]).NeedsTag)
				{
					codeObjectCreateExpression.Parameters.Add(new CodePrimitiveExpression(builder.TagName));
				}
			}
			else if (builder is DataBindingBuilder)
			{
				codeObjectCreateExpression.Parameters.Add(new CodePrimitiveExpression(0));
				codeObjectCreateExpression.Parameters.Add(new CodePrimitiveExpression(1));
			}
			method.Statements.Add(new CodeVariableDeclarationStatement(ctrlTypeRef, "__ctrl"));
			return new CodeAssignStatement
			{
				Left = TemplateControlCompiler.ctrlVar,
				Right = codeObjectCreateExpression
			};
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x000C2708 File Offset: 0x000C0908
		private void InitMethod(ControlBuilder builder, bool isTemplate, bool childrenAsProperties)
		{
			this.currentLocation = builder.Location;
			bool flag = builder is RootBuilder;
			string text = (flag ? "Tree" : ("_" + builder.ID));
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			builder.Method = codeMemberMethod;
			builder.MethodStatements = codeMemberMethod.Statements;
			codeMemberMethod.Name = "__BuildControl" + text;
			codeMemberMethod.Attributes = (MemberAttributes)20482;
			Type controlType = builder.ControlType;
			if (flag)
			{
				this.SetCustomAttributes(codeMemberMethod);
				this.AddStatementsToInitMethodTop(builder, codeMemberMethod);
			}
			if (builder.HasAspCode)
			{
				CodeMemberMethod codeMemberMethod2 = new CodeMemberMethod();
				builder.RenderMethod = codeMemberMethod2;
				codeMemberMethod2.Name = "__Render" + text;
				codeMemberMethod2.Attributes = (MemberAttributes)20482;
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
				codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(HtmlTextWriter));
				codeParameterDeclarationExpression.Name = "__output";
				CodeParameterDeclarationExpression codeParameterDeclarationExpression2 = new CodeParameterDeclarationExpression();
				codeParameterDeclarationExpression2.Type = new CodeTypeReference(typeof(Control));
				codeParameterDeclarationExpression2.Name = "parameterContainer";
				codeMemberMethod2.Parameters.Add(codeParameterDeclarationExpression);
				codeMemberMethod2.Parameters.Add(codeParameterDeclarationExpression2);
				this.mainClass.Members.Add(codeMemberMethod2);
			}
			if (childrenAsProperties || controlType == null)
			{
				bool flag2 = true;
				string text2;
				bool flag3;
				if (builder is RootBuilder)
				{
					text2 = this.parser.ClassName;
					flag2 = false;
					flag3 = false;
				}
				else
				{
					flag3 = builder.PropertyBuilderShouldReturnValue;
					if (controlType != null && builder.IsProperty && !typeof(ITemplate).IsAssignableFrom(controlType))
					{
						text2 = controlType.FullName;
						flag2 = !controlType.IsPrimitive;
					}
					else
					{
						text2 = "System.Web.UI.Control";
					}
					this.ProcessTemplateChildren(builder);
				}
				CodeTypeReference codeTypeReference = new CodeTypeReference(text2);
				if (flag2)
				{
					codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
				}
				if (flag3)
				{
					codeMemberMethod.ReturnType = codeTypeReference;
					codeMemberMethod.Statements.Add(this.CreateControlVariable(controlType, builder, codeMemberMethod, codeTypeReference));
				}
				else
				{
					codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(text2, "__ctrl"));
				}
			}
			else
			{
				CodeTypeReference codeTypeReference2 = new CodeTypeReference(controlType.FullName);
				if (!controlType.IsPrimitive)
				{
					codeTypeReference2.Options |= CodeTypeReferenceOptions.GlobalReference;
				}
				if (typeof(Control).IsAssignableFrom(controlType))
				{
					codeMemberMethod.ReturnType = codeTypeReference2;
				}
				codeMemberMethod.Statements.Add(base.AddLinePragma(this.CreateControlVariable(controlType, builder, codeMemberMethod, codeTypeReference2), builder));
				CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression();
				codeFieldReferenceExpression.TargetObject = BaseCompiler.thisRef;
				codeFieldReferenceExpression.FieldName = builder.ID;
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = codeFieldReferenceExpression;
				codeAssignStatement.Right = TemplateControlCompiler.ctrlVar;
				codeMemberMethod.Statements.Add(base.AddLinePragma(codeAssignStatement, builder));
				if (typeof(UserControl).IsAssignableFrom(controlType))
				{
					CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
					{
						TargetObject = codeFieldReferenceExpression,
						MethodName = "InitializeAsUserControl"
					}, Array.Empty<CodeExpression>());
					codeMethodInvokeExpression.Parameters.Add(new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Page"));
					codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
				}
				if (builder.ParentTemplateBuilder is ContentBuilderInternal)
				{
					PropertyInfo propertyInfo;
					try
					{
						propertyInfo = controlType.GetProperty("TemplateControl");
					}
					catch (Exception)
					{
						propertyInfo = null;
					}
					if (propertyInfo != null && propertyInfo.CanWrite)
					{
						codeAssignStatement = new CodeAssignStatement();
						codeAssignStatement.Left = new CodePropertyReferenceExpression(TemplateControlCompiler.ctrlVar, "TemplateControl");
						codeAssignStatement.Right = BaseCompiler.thisRef;
						codeMemberMethod.Statements.Add(codeAssignStatement);
					}
				}
				if (!string.IsNullOrEmpty(builder.GetAttribute("skinid")))
				{
					this.CreateAssignStatementFromAttribute(builder, "skinid");
				}
				if (typeof(WebControl).IsAssignableFrom(controlType))
				{
					CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(TemplateControlCompiler.ctrlVar, "ApplyStyleSheetSkin", Array.Empty<CodeExpression>());
					if (typeof(Page).IsAssignableFrom(this.parser.BaseType))
					{
						codeMethodInvokeExpression2.Parameters.Add(BaseCompiler.thisRef);
					}
					else
					{
						codeMethodInvokeExpression2.Parameters.Add(new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Page"));
					}
					codeMemberMethod.Statements.Add(codeMethodInvokeExpression2);
				}
				this.ProcessTemplateChildren(builder);
				string attribute = builder.GetAttribute("id");
				if (attribute != null && attribute.Length != 0)
				{
					this.CreateAssignStatementFromAttribute(builder, "id");
				}
				if (typeof(ContentPlaceHolder).IsAssignableFrom(controlType))
				{
					List<string> list = this.MasterPageContentPlaceHolders;
					string id = builder.ID;
					if (!list.Contains(id))
					{
						list.Add(id);
					}
					string text3 = "__Template_" + id;
					CodeMemberField codeMemberField = new CodeMemberField(typeof(ITemplate), text3);
					codeMemberField.Attributes = MemberAttributes.Private;
					this.mainClass.Members.Add(codeMemberField);
					CodeFieldReferenceExpression codeFieldReferenceExpression2 = new CodeFieldReferenceExpression();
					codeFieldReferenceExpression2.TargetObject = BaseCompiler.thisRef;
					codeFieldReferenceExpression2.FieldName = text3;
					this.CreateContentPlaceHolderTemplateProperty(text3, "Template_" + id);
					CodeFieldReferenceExpression codeFieldReferenceExpression3 = new CodeFieldReferenceExpression();
					codeFieldReferenceExpression3.TargetObject = BaseCompiler.thisRef;
					codeFieldReferenceExpression3.FieldName = "ContentTemplates";
					CodeIndexerExpression codeIndexerExpression = new CodeIndexerExpression();
					codeIndexerExpression.TargetObject = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "ContentTemplates");
					codeIndexerExpression.Indices.Add(new CodePrimitiveExpression(id));
					codeAssignStatement = new CodeAssignStatement();
					codeAssignStatement.Left = codeFieldReferenceExpression2;
					codeAssignStatement.Right = new CodeCastExpression(new CodeTypeReference(typeof(ITemplate)), codeIndexerExpression);
					CodeConditionStatement codeConditionStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeFieldReferenceExpression3, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), new CodeStatement[] { codeAssignStatement });
					codeMemberMethod.Statements.Add(codeConditionStatement);
					CodeMethodInvokeExpression codeMethodInvokeExpression3 = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
					{
						TargetObject = codeFieldReferenceExpression2,
						MethodName = "InstantiateIn"
					}, new CodeExpression[] { TemplateControlCompiler.ctrlVar });
					codeConditionStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeFieldReferenceExpression2, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), new CodeStatement[]
					{
						new CodeExpressionStatement(codeMethodInvokeExpression3)
					});
					codeMemberMethod.Statements.Add(codeConditionStatement);
					builder.MethodStatements = codeConditionStatement.FalseStatements;
				}
			}
			if (flag)
			{
				this.AddStatementsToInitMethodBottom(builder, codeMemberMethod);
			}
			this.mainClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x000C2D5C File Offset: 0x000C0F5C
		private void ProcessTemplateChildren(ControlBuilder builder)
		{
			ArrayList templateChildren = builder.TemplateChildren;
			if (templateChildren != null && templateChildren.Count > 0)
			{
				foreach (object obj in templateChildren)
				{
					TemplateBuilder templateBuilder = (TemplateBuilder)obj;
					this.CreateControlTree(templateBuilder, true, false);
					if (templateBuilder.BindingDirection == BindingDirection.TwoWay)
					{
						string text = this.CreateExtractValuesMethod(templateBuilder);
						this.AddBindableTemplateInvocation(builder, templateBuilder.TagName, templateBuilder.Method.Name, text);
					}
					else
					{
						this.AddTemplateInvocation(builder, templateBuilder.TagName, templateBuilder.Method.Name);
					}
				}
			}
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x000C2E14 File Offset: 0x000C1014
		private void SetCustomAttribute(CodeMemberMethod method, UnknownAttributeDescriptor uad)
		{
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("__ctrl"), uad.Info.Name);
			codeAssignStatement.Right = this.GetExpressionFromString(uad.Value.GetType(), uad.Value.ToString(), uad.Info);
			method.Statements.Add(codeAssignStatement);
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x000C2E7C File Offset: 0x000C107C
		private void SetCustomAttributes(CodeMemberMethod method)
		{
			if (this.parser.BaseType == null)
			{
				return;
			}
			List<UnknownAttributeDescriptor> unknownMainAttributes = this.parser.UnknownMainAttributes;
			if (unknownMainAttributes == null || unknownMainAttributes.Count == 0)
			{
				return;
			}
			foreach (UnknownAttributeDescriptor unknownAttributeDescriptor in unknownMainAttributes)
			{
				this.SetCustomAttribute(method, unknownAttributeDescriptor);
			}
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x000C2EF8 File Offset: 0x000C10F8
		protected virtual void AddStatementsToInitMethodTop(ControlBuilder builder, CodeMemberMethod method)
		{
			ClientIDMode? clientIDMode = this.parser.ClientIDMode;
			if (clientIDMode != null)
			{
				CodeTypeReferenceExpression codeTypeReferenceExpression = new CodeTypeReferenceExpression(typeof(ClientIDMode));
				codeTypeReferenceExpression.Type.Options = CodeTypeReferenceOptions.GlobalReference;
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "ClientIDMode");
				codeAssignStatement.Right = new CodeFieldReferenceExpression(codeTypeReferenceExpression, clientIDMode.Value.ToString());
				method.Statements.Add(codeAssignStatement);
			}
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void AddStatementsToInitMethodBottom(ControlBuilder builder, CodeMemberMethod method)
		{
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x000C2F80 File Offset: 0x000C1180
		private void AddLiteralSubObject(ControlBuilder builder, string str)
		{
			if (!builder.HasAspCode)
			{
				CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(typeof(LiteralControl), new CodeExpression[]
				{
					new CodePrimitiveExpression(str)
				});
				this.AddParsedSubObjectStmt(builder, codeObjectCreateExpression);
				return;
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
			{
				TargetObject = new CodeArgumentReferenceExpression("__output"),
				MethodName = "Write"
			}, new CodeExpression[]
			{
				new CodePrimitiveExpression(str)
			});
			builder.RenderMethod.Statements.Add(codeMethodInvokeExpression);
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x000C3004 File Offset: 0x000C1204
		private string TrimDB(string value, bool trimTail)
		{
			string text = value.Trim();
			int num = text.Length;
			int num2 = text.IndexOf('#', 2) + 1;
			if (num2 >= num)
			{
				return string.Empty;
			}
			if (trimTail)
			{
				num -= 2;
			}
			return text.Substring(num2, num - num2).Trim();
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x000C304C File Offset: 0x000C124C
		private CodeExpression CreateEvalInvokeExpression(Regex regex, string value, bool isBind)
		{
			Match match = regex.Match(value);
			if (match.Success)
			{
				string text;
				if (isBind)
				{
					text = this.SanitizeBindCall(match);
				}
				else
				{
					text = value;
				}
				return new CodeSnippetExpression(text);
			}
			if (isBind)
			{
				throw new HttpParseException("Bind invocation wasn't formatted properly.");
			}
			return null;
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x000C3090 File Offset: 0x000C1290
		private string SanitizeBindCall(Match match)
		{
			GroupCollection groups = match.Groups;
			StringBuilder stringBuilder = new StringBuilder("Eval(\"" + groups[1] + "\"");
			Group group = groups[4];
			if (group != null)
			{
				string value = group.Value;
				if (value != null && value.Length > 0)
				{
					stringBuilder.Append(",\"" + group + "\"");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x000C3108 File Offset: 0x000C1308
		private string DataBoundProperty(ControlBuilder builder, Type type, string varName, string value)
		{
			value = this.TrimDB(value, true);
			object name = builder.Method.Name;
			object obj = "_DB_";
			int num = this.dataBoundAtts;
			this.dataBoundAtts = num + 1;
			string text = name + obj + num;
			CodeExpression codeExpression = null;
			value = value.Trim();
			bool flag = false;
			if (TemplateControlCompiler.startsWithBindRegex.Match(value).Success)
			{
				codeExpression = this.CreateEvalInvokeExpression(TemplateControlCompiler.bindRegexInValue, value, true);
				if (codeExpression != null)
				{
					flag = true;
				}
			}
			else if (StrUtils.StartsWith(value, "Eval", true))
			{
				codeExpression = this.CreateEvalInvokeExpression(TemplateControlCompiler.evalRegexInValue, value, false);
			}
			if (codeExpression == null)
			{
				codeExpression = new CodeSnippetExpression(value);
			}
			CodeMemberMethod codeMemberMethod = this.CreateDBMethod(builder, text, TemplateControlCompiler.GetContainerType(builder), builder.ControlType);
			CodeExpression codeExpression2 = new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("target"), varName);
			CodeExpression codeExpression3;
			if (type == typeof(string))
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
				CodeTypeReferenceExpression codeTypeReferenceExpression = new CodeTypeReferenceExpression(typeof(Convert));
				codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(codeTypeReferenceExpression, "ToString");
				codeMethodInvokeExpression.Parameters.Add(codeExpression);
				codeExpression3 = codeMethodInvokeExpression;
			}
			else
			{
				codeExpression3 = new CodeCastExpression(type, codeExpression);
			}
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement(codeExpression2, codeExpression3);
			if (flag)
			{
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Page"), "GetDataItem", Array.Empty<CodeExpression>()), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), new CodeStatement[] { codeAssignStatement });
				codeMemberMethod.Statements.Add(codeConditionStatement);
			}
			else
			{
				codeMemberMethod.Statements.Add(codeAssignStatement);
			}
			this.mainClass.Members.Add(codeMemberMethod);
			return codeMemberMethod.Name;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x000C32A8 File Offset: 0x000C14A8
		private void AddCodeForPropertyOrField(ControlBuilder builder, Type type, string var_name, string att, MemberInfo member, bool isDataBound, bool isExpression)
		{
			CodeMemberMethod method = builder.Method;
			bool flag = TemplateControlCompiler.IsWritablePropertyOrField(member);
			if (isDataBound && flag)
			{
				string text = this.DataBoundProperty(builder, type, var_name, att);
				this.AddEventAssign(method, builder, "DataBinding", typeof(EventHandler), text);
				return;
			}
			if (isExpression && flag)
			{
				this.AddExpressionAssign(method, builder, member, type, var_name, att);
				return;
			}
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodePropertyReferenceExpression(TemplateControlCompiler.ctrlVar, var_name);
			this.currentLocation = builder.Location;
			codeAssignStatement.Right = this.GetExpressionFromString(type, att, member);
			method.Statements.Add(base.AddLinePragma(codeAssignStatement, builder));
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x000C334C File Offset: 0x000C154C
		private void RegisterBindingInfo(ControlBuilder builder, string propName, ref string value)
		{
			string text = this.TrimDB(value, false);
			if (StrUtils.StartsWith(text, "Bind", true))
			{
				Match match = TemplateControlCompiler.bindRegex.Match(text);
				if (match.Success)
				{
					string value2 = match.Groups[1].Value;
					TemplateBuilder parentTemplateBuilder = builder.ParentTemplateBuilder;
					if (parentTemplateBuilder == null)
					{
						throw new HttpException("Bind expression not allowed in this context.");
					}
					if (parentTemplateBuilder.BindingDirection == BindingDirection.OneWay)
					{
						return;
					}
					string attribute = builder.GetAttribute("ID");
					if (string.IsNullOrEmpty(attribute))
					{
						throw new HttpException(string.Concat(new object[] { "Control of type '", builder.ControlType, "' using two-way binding on property '", propName, "' must have an ID." }));
					}
					parentTemplateBuilder.RegisterBoundProperty(builder.ControlType, propName, attribute, value2);
				}
			}
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x000C3417 File Offset: 0x000C1617
		private static bool InvariantCompareNoCase(string a, string b)
		{
			return string.Compare(a, b, true, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x000C342C File Offset: 0x000C162C
		internal static MemberInfo GetFieldOrProperty(Type type, string name)
		{
			MemberInfo memberInfo = null;
			try
			{
				memberInfo = type.GetProperty(name, TemplateControlCompiler.noCaseFlags & ~BindingFlags.NonPublic);
			}
			catch
			{
			}
			if (memberInfo != null)
			{
				return memberInfo;
			}
			try
			{
				memberInfo = type.GetField(name, TemplateControlCompiler.noCaseFlags & ~BindingFlags.NonPublic);
			}
			catch
			{
			}
			return memberInfo;
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x000C3490 File Offset: 0x000C1690
		private static bool IsWritablePropertyOrField(MemberInfo member)
		{
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.GetSetMethod(false) != null;
			}
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return !fieldInfo.IsInitOnly;
			}
			throw new ArgumentException("Argument must be of PropertyInfo or FieldInfo type", "member");
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x000C34E4 File Offset: 0x000C16E4
		private bool ProcessPropertiesAndFields(ControlBuilder builder, MemberInfo member, string id, string attValue, string prefix)
		{
			int num = id.IndexOf('-');
			bool flag = member is PropertyInfo;
			bool flag2 = BaseParser.IsDataBound(attValue);
			bool flag3 = !flag2 && BaseParser.IsExpression(attValue);
			Type type;
			if (flag)
			{
				type = ((PropertyInfo)member).PropertyType;
			}
			else
			{
				type = ((FieldInfo)member).FieldType;
			}
			if (TemplateControlCompiler.InvariantCompareNoCase(member.Name, id))
			{
				if (flag2)
				{
					this.RegisterBindingInfo(builder, member.Name, ref attValue);
				}
				if (!TemplateControlCompiler.IsWritablePropertyOrField(member))
				{
					return false;
				}
				this.AddCodeForPropertyOrField(builder, type, member.Name, attValue, member, flag2, flag3);
				return true;
			}
			else
			{
				if (num == -1)
				{
					return false;
				}
				string[] array = id.Replace('-', '.').Split(new char[] { '.' });
				int num2 = array.Length;
				if (num2 < 2 || !TemplateControlCompiler.InvariantCompareNoCase(member.Name, array[0]))
				{
					return false;
				}
				if (num2 > 2)
				{
					MemberInfo fieldOrProperty = TemplateControlCompiler.GetFieldOrProperty(type, array[1]);
					if (fieldOrProperty == null)
					{
						return false;
					}
					string text = prefix + member.Name + ".";
					string text2 = id.Substring(num + 1);
					return this.ProcessPropertiesAndFields(builder, fieldOrProperty, text2, attValue, text);
				}
				else
				{
					MemberInfo fieldOrProperty2 = TemplateControlCompiler.GetFieldOrProperty(type, array[1]);
					if (!(fieldOrProperty2 is PropertyInfo))
					{
						return false;
					}
					PropertyInfo propertyInfo = (PropertyInfo)fieldOrProperty2;
					if (!propertyInfo.CanWrite)
					{
						return false;
					}
					bool flag4 = propertyInfo.PropertyType == typeof(bool);
					if (!flag4 && attValue == null)
					{
						return false;
					}
					string text3 = attValue;
					if (attValue == null && flag4)
					{
						text3 = "true";
					}
					if (flag2)
					{
						this.RegisterBindingInfo(builder, prefix + member.Name + "." + propertyInfo.Name, ref attValue);
					}
					this.AddCodeForPropertyOrField(builder, propertyInfo.PropertyType, prefix + member.Name + "." + propertyInfo.Name, text3, propertyInfo, flag2, flag3);
					return true;
				}
			}
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x000C36B8 File Offset: 0x000C18B8
		internal CodeExpression CompileExpression(MemberInfo member, Type type, string value, bool useSetAttribute)
		{
			value = value.Substring(3, value.Length - 5).Trim();
			int num = value.IndexOf(':');
			if (num == -1)
			{
				return null;
			}
			string text = value.Substring(0, num).Trim();
			string text2 = value.Substring(num + 1).Trim();
			CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
			if (compilationSection == null)
			{
				return null;
			}
			if (compilationSection.ExpressionBuilders == null || compilationSection.ExpressionBuilders.Count == 0)
			{
				return null;
			}
			ExpressionBuilder expressionBuilder = compilationSection.ExpressionBuilders[text];
			if (expressionBuilder == null)
			{
				return null;
			}
			string type2 = expressionBuilder.Type;
			Type type3;
			try
			{
				type3 = HttpApplication.LoadType(type2, true);
			}
			catch (Exception ex)
			{
				throw new HttpException(string.Format("Failed to load expression builder type `{0}'", type2), ex);
			}
			if (!typeof(ExpressionBuilder).IsAssignableFrom(type3))
			{
				throw new HttpException(string.Format("Type {0} is not descendant from System.Web.Compilation.ExpressionBuilder", type2));
			}
			ExpressionBuilder expressionBuilder2 = null;
			ExpressionBuilderContext expressionBuilderContext;
			object obj;
			try
			{
				expressionBuilder2 = Activator.CreateInstance(type3) as ExpressionBuilder;
				expressionBuilderContext = new ExpressionBuilderContext(HttpContext.Current.Request.FilePath);
				obj = expressionBuilder2.ParseExpression(text2, type, expressionBuilderContext);
			}
			catch (Exception ex2)
			{
				throw new HttpException(string.Format("Failed to create an instance of type `{0}'", type2), ex2);
			}
			BoundPropertyEntry boundPropertyEntry = this.CreateBoundPropertyEntry(member as PropertyInfo, text, text2, useSetAttribute);
			return expressionBuilder2.GetCodeExpression(boundPropertyEntry, obj, expressionBuilderContext);
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x000C3820 File Offset: 0x000C1A20
		private void AddExpressionAssign(CodeMemberMethod method, ControlBuilder builder, MemberInfo member, Type type, string name, string value)
		{
			CodeExpression codeExpression = this.CompileExpression(member, type, value, false);
			if (codeExpression == null)
			{
				return;
			}
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodePropertyReferenceExpression(TemplateControlCompiler.ctrlVar, name);
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode != TypeCode.Empty && typeCode != TypeCode.Object && typeCode != TypeCode.DBNull)
			{
				codeAssignStatement.Right = TemplateControlCompiler.CreateConvertToCall(typeCode, codeExpression);
			}
			else
			{
				codeAssignStatement.Right = new CodeCastExpression(type, codeExpression);
			}
			builder.Method.Statements.Add(base.AddLinePragma(codeAssignStatement, builder));
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x000C38A0 File Offset: 0x000C1AA0
		internal static CodeMethodInvokeExpression CreateConvertToCall(TypeCode typeCode, CodeExpression expr)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			string text;
			switch (typeCode)
			{
			case TypeCode.Boolean:
				text = "ToBoolean";
				goto IL_00E1;
			case TypeCode.Char:
				text = "ToChar";
				goto IL_00E1;
			case TypeCode.SByte:
				text = "ToSByte";
				goto IL_00E1;
			case TypeCode.Byte:
				text = "ToByte";
				goto IL_00E1;
			case TypeCode.Int16:
				text = "ToInt16";
				goto IL_00E1;
			case TypeCode.UInt16:
				text = "ToUInt16";
				goto IL_00E1;
			case TypeCode.Int32:
				text = "ToInt32";
				goto IL_00E1;
			case TypeCode.UInt32:
				text = "ToUInt32";
				goto IL_00E1;
			case TypeCode.Int64:
				text = "ToInt64";
				goto IL_00E1;
			case TypeCode.UInt64:
				text = "ToUInt64";
				goto IL_00E1;
			case TypeCode.Single:
				text = "ToSingle";
				goto IL_00E1;
			case TypeCode.Double:
				text = "ToDouble";
				goto IL_00E1;
			case TypeCode.Decimal:
				text = "ToDecimal";
				goto IL_00E1;
			case TypeCode.DateTime:
				text = "ToDateTime";
				goto IL_00E1;
			case TypeCode.String:
				text = "ToString";
				goto IL_00E1;
			}
			throw new InvalidOperationException(string.Format("Unsupported TypeCode '{0}'", typeCode));
			IL_00E1:
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Convert))
			{
				Type = 
				{
					Options = CodeTypeReferenceOptions.GlobalReference
				}
			}, text);
			codeMethodInvokeExpression.Parameters.Add(expr);
			codeMethodInvokeExpression.Parameters.Add(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(CultureInfo)), "CurrentCulture"));
			return codeMethodInvokeExpression;
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x000C39EC File Offset: 0x000C1BEC
		private BoundPropertyEntry CreateBoundPropertyEntry(PropertyInfo pi, string prefix, string expr, bool useSetAttribute)
		{
			BoundPropertyEntry boundPropertyEntry = new BoundPropertyEntry();
			boundPropertyEntry.Expression = expr;
			boundPropertyEntry.ExpressionPrefix = prefix;
			boundPropertyEntry.Generated = false;
			if (pi != null)
			{
				boundPropertyEntry.Name = pi.Name;
				boundPropertyEntry.PropertyInfo = pi;
				boundPropertyEntry.Type = pi.PropertyType;
			}
			boundPropertyEntry.UseSetAttribute = useSetAttribute;
			return boundPropertyEntry;
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x000C3A48 File Offset: 0x000C1C48
		private bool ResourceProviderHasObject(string key)
		{
			IResourceProvider resourceProvider = HttpContext.GetResourceProvider(base.InputVirtualPath.Absolute, true);
			if (resourceProvider == null)
			{
				return false;
			}
			IResourceReader resourceReader = resourceProvider.ResourceReader;
			if (resourceReader == null)
			{
				return false;
			}
			try
			{
				IDictionaryEnumerator enumerator = resourceReader.GetEnumerator();
				if (enumerator == null)
				{
					return false;
				}
				while (enumerator.MoveNext())
				{
					string text = enumerator.Key as string;
					if (!string.IsNullOrEmpty(text) && string.Compare(key, text, StringComparison.Ordinal) == 0)
					{
						return true;
					}
				}
			}
			finally
			{
				resourceReader.Close();
			}
			return false;
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x000C3AD0 File Offset: 0x000C1CD0
		private void AssignPropertyFromResources(ControlBuilder builder, MemberInfo mi, string attvalue)
		{
			bool flag = mi.MemberType == MemberTypes.Property;
			bool flag2 = !flag && mi.MemberType == MemberTypes.Field;
			if ((!flag && !flag2) || !TemplateControlCompiler.IsWritablePropertyOrField(mi))
			{
				return;
			}
			object[] customAttributes = mi.GetCustomAttributes(typeof(LocalizableAttribute), true);
			if (customAttributes != null && customAttributes.Length != 0 && !((LocalizableAttribute)customAttributes[0]).IsLocalizable)
			{
				return;
			}
			string name = mi.Name;
			string text = attvalue + "." + name;
			if (!this.ResourceProviderHasObject(text))
			{
				return;
			}
			string text2 = this.parser.InputFile;
			string physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
			if (!StrUtils.StartsWith(text2, physicalApplicationPath))
			{
				return;
			}
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			text2 = this.parser.InputFile.Substring(physicalApplicationPath.Length - 1);
			if (appDomainAppVirtualPath != "/")
			{
				text2 = appDomainAppVirtualPath + text2;
			}
			char directorySeparatorChar = Path.DirectorySeparatorChar;
			if (directorySeparatorChar != '/')
			{
				text2 = text2.Replace(directorySeparatorChar, '/');
			}
			if (HttpContext.GetLocalResourceObject(text2, text) == null)
			{
				return;
			}
			if (!flag && !flag2)
			{
				return;
			}
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodePropertyReferenceExpression(TemplateControlCompiler.ctrlVar, name);
			codeAssignStatement.Right = ResourceExpressionBuilder.CreateGetLocalResourceObject(mi, text);
			builder.Method.Statements.Add(base.AddLinePragma(codeAssignStatement, builder));
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x000C3C28 File Offset: 0x000C1E28
		private void AssignPropertiesFromResources(ControlBuilder builder, Type controlType, string attvalue)
		{
			FieldInfo[] fields = controlType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			PropertyInfo[] properties = controlType.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			foreach (FieldInfo fieldInfo in fields)
			{
				this.AssignPropertyFromResources(builder, fieldInfo, attvalue);
			}
			foreach (PropertyInfo propertyInfo in properties)
			{
				this.AssignPropertyFromResources(builder, propertyInfo, attvalue);
			}
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x000C3C84 File Offset: 0x000C1E84
		private void AssignPropertiesFromResources(ControlBuilder builder, string attvalue)
		{
			if (attvalue == null || attvalue.Length == 0)
			{
				return;
			}
			Type controlType = builder.ControlType;
			if (controlType == null)
			{
				return;
			}
			this.AssignPropertiesFromResources(builder, controlType, attvalue);
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x000C3CB8 File Offset: 0x000C1EB8
		private void AddEventAssign(CodeMemberMethod method, ControlBuilder builder, string name, Type type, string value)
		{
			CodeEventReferenceExpression codeEventReferenceExpression = new CodeEventReferenceExpression(TemplateControlCompiler.ctrlVar, name);
			CodeDelegateCreateExpression codeDelegateCreateExpression = new CodeDelegateCreateExpression(new CodeTypeReference(type), BaseCompiler.thisRef, value);
			CodeAttachEventStatement codeAttachEventStatement = new CodeAttachEventStatement(codeEventReferenceExpression, codeDelegateCreateExpression);
			method.Statements.Add(codeAttachEventStatement);
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x000C3CF8 File Offset: 0x000C1EF8
		private void CreateAssignStatementFromAttribute(ControlBuilder builder, string id)
		{
			EventInfo[] array = null;
			Type controlType = builder.ControlType;
			string attribute = builder.GetAttribute(id);
			if (id.Length > 2 && string.Compare(id.Substring(0, 2), "ON", true, Helpers.InvariantCulture) == 0)
			{
				if (array == null)
				{
					array = controlType.GetEvents();
				}
				string text = id.Substring(2);
				foreach (EventInfo eventInfo in array)
				{
					if (TemplateControlCompiler.InvariantCompareNoCase(eventInfo.Name, text))
					{
						this.AddEventAssign(builder.Method, builder, eventInfo.Name, eventInfo.EventHandlerType, attribute);
						return;
					}
				}
			}
			if (string.Compare(id, "meta:resourcekey", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.AssignPropertiesFromResources(builder, attribute);
				return;
			}
			int num = id.IndexOf('-');
			string text2 = id;
			if (num != -1)
			{
				text2 = id.Substring(0, num);
			}
			MemberInfo fieldOrProperty = TemplateControlCompiler.GetFieldOrProperty(controlType, text2);
			if (fieldOrProperty != null && this.ProcessPropertiesAndFields(builder, fieldOrProperty, id, attribute, null))
			{
				return;
			}
			if (!typeof(IAttributeAccessor).IsAssignableFrom(controlType))
			{
				throw new ParseException(builder.Location, "Unrecognized attribute: " + id);
			}
			CodeMemberMethod method = builder.Method;
			bool flag = BaseParser.IsDataBound(attribute);
			bool flag2 = !flag && BaseParser.IsExpression(attribute);
			if (flag)
			{
				string text3 = attribute.Substring(3, attribute.Length - 5).Trim();
				CodeExpression codeExpression = null;
				if (TemplateControlCompiler.startsWithBindRegex.Match(text3).Success)
				{
					codeExpression = this.CreateEvalInvokeExpression(TemplateControlCompiler.bindRegexInValue, text3, true);
				}
				else if (StrUtils.StartsWith(text3, "Eval", true))
				{
					codeExpression = this.CreateEvalInvokeExpression(TemplateControlCompiler.evalRegexInValue, text3, false);
				}
				if (codeExpression == null && text3 != null && text3.Trim() != string.Empty)
				{
					codeExpression = new CodeSnippetExpression(text3);
				}
				this.CreateDBAttributeMethod(builder, id, codeExpression);
				return;
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeCastExpression(typeof(IAttributeAccessor), TemplateControlCompiler.ctrlVar), "SetAttribute"), Array.Empty<CodeExpression>());
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(id));
			CodeExpression codeExpression2 = null;
			if (flag2)
			{
				codeExpression2 = this.CompileExpression(null, typeof(string), attribute, true);
			}
			if (codeExpression2 == null)
			{
				codeExpression2 = new CodePrimitiveExpression(attribute);
			}
			codeMethodInvokeExpression.Parameters.Add(codeExpression2);
			method.Statements.Add(base.AddLinePragma(codeMethodInvokeExpression, builder));
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x000C3F4C File Offset: 0x000C214C
		protected void CreateAssignStatementsFromAttributes(ControlBuilder builder)
		{
			this.dataBoundAtts = 0;
			IDictionary attributes = builder.Attributes;
			if (attributes == null || attributes.Count == 0)
			{
				return;
			}
			foreach (object obj in attributes.Keys)
			{
				string text = (string)obj;
				if (!TemplateControlCompiler.InvariantCompareNoCase(text, "runat") && !TemplateControlCompiler.InvariantCompareNoCase(text, "id") && !TemplateControlCompiler.InvariantCompareNoCase(text, "skinid") && !TemplateControlCompiler.InvariantCompareNoCase(text, "meta:resourcekey"))
				{
					this.CreateAssignStatementFromAttribute(builder, text);
				}
			}
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x000C3FF4 File Offset: 0x000C21F4
		private void CreateDBAttributeMethod(ControlBuilder builder, string attr, CodeExpression code)
		{
			if (code == null)
			{
				return;
			}
			string nextID = builder.GetNextID(null);
			string text = "__DataBind_" + nextID;
			CodeMemberMethod codeMemberMethod = builder.Method;
			this.AddEventAssign(codeMemberMethod, builder, "DataBinding", typeof(EventHandler), text);
			codeMemberMethod = this.CreateDBMethod(builder, text, TemplateControlCompiler.GetContainerType(builder), builder.ControlType);
			builder.DataBindingMethod = codeMemberMethod;
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("target");
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeCastExpression(typeof(IAttributeAccessor), codeVariableReferenceExpression), "SetAttribute"), Array.Empty<CodeExpression>());
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(attr));
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression2.Method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Convert)), "ToString");
			codeMethodInvokeExpression2.Parameters.Add(code);
			codeMethodInvokeExpression.Parameters.Add(codeMethodInvokeExpression2);
			codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
			this.mainClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x000C40FC File Offset: 0x000C22FC
		private void AddRenderControl(ControlBuilder builder)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeIndexerExpression
			{
				TargetObject = new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("parameterContainer"), "Controls"),
				Indices = 
				{
					new CodePrimitiveExpression(builder.RenderIndex)
				}
			}, "RenderControl", Array.Empty<CodeExpression>());
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("__output"));
			builder.RenderMethod.Statements.Add(codeMethodInvokeExpression);
			builder.IncreaseRenderIndex();
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x000C4184 File Offset: 0x000C2384
		protected void AddChildCall(ControlBuilder parent, ControlBuilder child)
		{
			if (parent == null || child == null)
			{
				return;
			}
			CodeStatementCollection methodStatements = parent.MethodStatements;
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(BaseCompiler.thisRef, child.Method.Name), Array.Empty<CodeExpression>());
			object[] array = null;
			if (child.ControlType != null)
			{
				array = child.ControlType.GetCustomAttributes(typeof(PartialCachingAttribute), true);
			}
			if (array != null && array.Length != 0)
			{
				PartialCachingAttribute partialCachingAttribute = (PartialCachingAttribute)array[0];
				CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("System.Web.UI.StaticPartialCachingControl"), "BuildCachedControl", Array.Empty<CodeExpression>());
				CodeExpressionCollection parameters = codeMethodInvokeExpression2.Parameters;
				parameters.Add(new CodeArgumentReferenceExpression("__ctrl"));
				parameters.Add(new CodePrimitiveExpression(child.ID));
				if (partialCachingAttribute.Shared)
				{
					parameters.Add(new CodePrimitiveExpression(child.ControlType.GetHashCode().ToString()));
				}
				else
				{
					parameters.Add(new CodePrimitiveExpression(Guid.NewGuid().ToString()));
				}
				parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.Duration));
				parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.VaryByParams));
				parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.VaryByControls));
				parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.VaryByCustom));
				parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.SqlDependency));
				parameters.Add(new CodeDelegateCreateExpression(new CodeTypeReference(typeof(BuildMethod)), BaseCompiler.thisRef, child.Method.Name));
				string providerName = partialCachingAttribute.ProviderName;
				if (!string.IsNullOrEmpty(providerName) && string.Compare("AspNetInternalProvider", providerName, StringComparison.Ordinal) != 0)
				{
					parameters.Add(new CodePrimitiveExpression(providerName));
				}
				else
				{
					parameters.Add(new CodePrimitiveExpression(null));
				}
				methodStatements.Add(base.AddLinePragma(codeMethodInvokeExpression2, parent));
				if (parent.HasAspCode)
				{
					this.AddRenderControl(parent);
				}
				return;
			}
			if (!child.IsProperty && !parent.ChildrenAsProperties)
			{
				methodStatements.Add(base.AddLinePragma(codeMethodInvokeExpression, parent));
				CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(BaseCompiler.thisRef, child.ID);
				if (parent.ControlType == null || typeof(IParserAccessor).IsAssignableFrom(parent.ControlType))
				{
					this.AddParsedSubObjectStmt(parent, codeFieldReferenceExpression);
				}
				else
				{
					methodStatements.Add(base.AddLinePragma(new CodeMethodInvokeExpression(TemplateControlCompiler.ctrlVar, "Add", Array.Empty<CodeExpression>())
					{
						Parameters = { codeFieldReferenceExpression }
					}, parent));
				}
				if (parent.HasAspCode)
				{
					this.AddRenderControl(parent);
				}
				return;
			}
			if (!child.PropertyBuilderShouldReturnValue)
			{
				codeMethodInvokeExpression.Parameters.Add(new CodeFieldReferenceExpression(TemplateControlCompiler.ctrlVar, child.TagName));
				parent.MethodStatements.Add(base.AddLinePragma(codeMethodInvokeExpression, parent));
				return;
			}
			string nextLocalVariableName = parent.GetNextLocalVariableName("__ctrl");
			methodStatements.Add(new CodeVariableDeclarationStatement(child.Method.ReturnType, nextLocalVariableName));
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression(nextLocalVariableName);
			methodStatements.Add(base.AddLinePragma(new CodeAssignStatement
			{
				Left = codeVariableReferenceExpression,
				Right = codeMethodInvokeExpression
			}, parent));
			methodStatements.Add(base.AddLinePragma(new CodeAssignStatement
			{
				Left = new CodeFieldReferenceExpression(TemplateControlCompiler.ctrlVar, child.TagName),
				Right = codeVariableReferenceExpression
			}, parent));
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x000C44F8 File Offset: 0x000C26F8
		private void AddTemplateInvocation(ControlBuilder builder, string name, string methodName)
		{
			CodeExpression codeExpression = new CodePropertyReferenceExpression(TemplateControlCompiler.ctrlVar, name);
			CodeDelegateCreateExpression codeDelegateCreateExpression = new CodeDelegateCreateExpression(new CodeTypeReference(typeof(BuildTemplateMethod)), BaseCompiler.thisRef, methodName);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement(codeExpression, new CodeObjectCreateExpression(typeof(CompiledTemplateBuilder), Array.Empty<CodeExpression>())
			{
				Parameters = { codeDelegateCreateExpression }
			});
			builder.Method.Statements.Add(base.AddLinePragma(codeAssignStatement, builder));
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x000C4570 File Offset: 0x000C2770
		private void AddBindableTemplateInvocation(ControlBuilder builder, string name, string methodName, string extractMethodName)
		{
			CodeExpression codeExpression = new CodePropertyReferenceExpression(TemplateControlCompiler.ctrlVar, name);
			CodeDelegateCreateExpression codeDelegateCreateExpression = new CodeDelegateCreateExpression(new CodeTypeReference(typeof(BuildTemplateMethod)), BaseCompiler.thisRef, methodName);
			CodeDelegateCreateExpression codeDelegateCreateExpression2 = new CodeDelegateCreateExpression(new CodeTypeReference(typeof(ExtractTemplateValuesMethod)), BaseCompiler.thisRef, extractMethodName);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement(codeExpression, new CodeObjectCreateExpression(typeof(CompiledBindableTemplateBuilder), Array.Empty<CodeExpression>())
			{
				Parameters = { codeDelegateCreateExpression, codeDelegateCreateExpression2 }
			});
			builder.Method.Statements.Add(base.AddLinePragma(codeAssignStatement, builder));
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x000C4610 File Offset: 0x000C2810
		private string CreateExtractValuesMethod(TemplateBuilder builder)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "__ExtractValues_" + builder.ID;
			codeMemberMethod.Attributes = (MemberAttributes)20482;
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(IOrderedDictionary));
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
			codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(Control));
			codeParameterDeclarationExpression.Name = "__container";
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			this.mainClass.Members.Add(codeMemberMethod);
			CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression();
			codeObjectCreateExpression.CreateType = new CodeTypeReference(typeof(OrderedDictionary));
			codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(OrderedDictionary), "__table", codeObjectCreateExpression));
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("__table");
			if (builder.Bindings != null)
			{
				Hashtable hashtable = new Hashtable();
				foreach (object obj in builder.Bindings)
				{
					TemplateBinding templateBinding = (TemplateBinding)obj;
					CodeAssignStatement codeAssignStatement;
					CodeVariableReferenceExpression codeVariableReferenceExpression2;
					CodeConditionStatement codeConditionStatement;
					if (hashtable[templateBinding.ControlId] == null)
					{
						CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement(templateBinding.ControlType, templateBinding.ControlId);
						codeMemberMethod.Statements.Add(codeVariableDeclarationStatement);
						CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("__container"), "FindControl", Array.Empty<CodeExpression>());
						codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(templateBinding.ControlId));
						codeAssignStatement = new CodeAssignStatement();
						codeVariableReferenceExpression2 = new CodeVariableReferenceExpression(templateBinding.ControlId);
						codeAssignStatement.Left = codeVariableReferenceExpression2;
						codeAssignStatement.Right = new CodeCastExpression(templateBinding.ControlType, codeMethodInvokeExpression);
						codeMemberMethod.Statements.Add(codeAssignStatement);
						codeConditionStatement = new CodeConditionStatement();
						codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeVariableReferenceExpression2, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
						codeMemberMethod.Statements.Add(codeConditionStatement);
						hashtable[templateBinding.ControlId] = codeConditionStatement;
					}
					codeConditionStatement = (CodeConditionStatement)hashtable[templateBinding.ControlId];
					codeVariableReferenceExpression2 = new CodeVariableReferenceExpression(templateBinding.ControlId);
					codeAssignStatement = new CodeAssignStatement();
					codeAssignStatement.Left = new CodeIndexerExpression(codeVariableReferenceExpression, new CodeExpression[]
					{
						new CodePrimitiveExpression(templateBinding.FieldName)
					});
					codeAssignStatement.Right = new CodePropertyReferenceExpression(codeVariableReferenceExpression2, templateBinding.ControlProperty);
					codeConditionStatement.TrueStatements.Add(codeAssignStatement);
				}
			}
			codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(codeVariableReferenceExpression));
			return codeMemberMethod.Name;
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x000C48C4 File Offset: 0x000C2AC4
		private void AddContentTemplateInvocation(ContentBuilderInternal cbuilder, CodeMemberMethod method, string methodName)
		{
			CodeDelegateCreateExpression codeDelegateCreateExpression = new CodeDelegateCreateExpression(new CodeTypeReference(typeof(BuildTemplateMethod)), BaseCompiler.thisRef, methodName);
			CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(typeof(CompiledTemplateBuilder), Array.Empty<CodeExpression>());
			codeObjectCreateExpression.Parameters.Add(codeDelegateCreateExpression);
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "AddContentTemplate", Array.Empty<CodeExpression>());
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(cbuilder.ContentPlaceHolderID));
			codeMethodInvokeExpression.Parameters.Add(codeObjectCreateExpression);
			method.Statements.Add(base.AddLinePragma(codeMethodInvokeExpression, cbuilder));
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x000C495C File Offset: 0x000C2B5C
		private void AddCodeRender(ControlBuilder parent, CodeRenderBuilder cr)
		{
			if (cr.Code == null || cr.Code.Trim() == "")
			{
				return;
			}
			if (!cr.IsAssign)
			{
				CodeSnippetStatement codeSnippetStatement = new CodeSnippetStatement(cr.Code);
				parent.RenderMethod.Statements.Add(base.AddLinePragma(codeSnippetStatement, cr));
				return;
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("__output"), "Write");
			codeMethodInvokeExpression.Parameters.Add(this.GetWrappedCodeExpression(cr));
			parent.RenderMethod.Statements.Add(base.AddLinePragma(codeMethodInvokeExpression, cr));
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000C4A04 File Offset: 0x000C2C04
		private CodeExpression GetWrappedCodeExpression(CodeRenderBuilder cr)
		{
			CodeSnippetExpression codeSnippetExpression = new CodeSnippetExpression(cr.Code);
			if (cr.HtmlEncode)
			{
				return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(HttpUtility)), "HtmlEncode"), new CodeExpression[] { codeSnippetExpression });
			}
			return codeSnippetExpression;
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x000C4A4F File Offset: 0x000C2C4F
		private static Type GetContainerType(ControlBuilder builder)
		{
			return builder.BindingContainerType;
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x000C4A58 File Offset: 0x000C2C58
		private CodeMemberMethod CreateDBMethod(ControlBuilder builder, string name, Type container, Type target)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Attributes = (MemberAttributes)24578;
			codeMemberMethod.Name = name;
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "sender"));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(EventArgs), "e"));
			CodeTypeReference codeTypeReference = new CodeTypeReference(container);
			CodeTypeReference codeTypeReference2 = new CodeTypeReference(target);
			CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement();
			codeVariableDeclarationStatement.Name = "Container";
			codeVariableDeclarationStatement.Type = codeTypeReference;
			codeMemberMethod.Statements.Add(codeVariableDeclarationStatement);
			codeVariableDeclarationStatement = new CodeVariableDeclarationStatement();
			codeVariableDeclarationStatement.Name = "target";
			codeVariableDeclarationStatement.Type = codeTypeReference2;
			codeMemberMethod.Statements.Add(codeVariableDeclarationStatement);
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("target");
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = codeVariableReferenceExpression;
			codeAssignStatement.Right = new CodeCastExpression(codeTypeReference2, new CodeArgumentReferenceExpression("sender"));
			codeMemberMethod.Statements.Add(base.AddLinePragma(codeAssignStatement, builder));
			codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodeVariableReferenceExpression("Container");
			codeAssignStatement.Right = new CodeCastExpression(codeTypeReference, new CodePropertyReferenceExpression(codeVariableReferenceExpression, "BindingContainer"));
			codeMemberMethod.Statements.Add(base.AddLinePragma(codeAssignStatement, builder));
			return codeMemberMethod;
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x000C4BA0 File Offset: 0x000C2DA0
		private void AddDataBindingLiteral(ControlBuilder builder, DataBindingBuilder db)
		{
			if (db.Code == null || db.Code.Trim() == "")
			{
				return;
			}
			this.EnsureID(db);
			this.CreateField(db, false);
			string text = "__DataBind_" + db.ID;
			this.InitMethod(db, false, false);
			CodeMemberMethod codeMemberMethod = db.Method;
			this.AddEventAssign(codeMemberMethod, builder, "DataBinding", typeof(EventHandler), text);
			codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(TemplateControlCompiler.ctrlVar));
			codeMemberMethod = this.CreateDBMethod(builder, text, TemplateControlCompiler.GetContainerType(builder), typeof(DataBoundLiteralControl));
			builder.DataBindingMethod = codeMemberMethod;
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("target");
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(codeVariableReferenceExpression, "SetDataBoundString");
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(0));
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression2.Method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Convert)), "ToString");
			codeMethodInvokeExpression2.Parameters.Add(new CodeSnippetExpression(db.Code));
			codeMethodInvokeExpression.Parameters.Add(codeMethodInvokeExpression2);
			codeMemberMethod.Statements.Add(base.AddLinePragma(codeMethodInvokeExpression, builder));
			this.mainClass.Members.Add(codeMemberMethod);
			this.AddChildCall(builder, db);
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x000C4CFF File Offset: 0x000C2EFF
		private void FlushText(ControlBuilder builder, StringBuilder sb)
		{
			if (sb.Length > 0)
			{
				this.AddLiteralSubObject(builder, sb.ToString());
				sb.Length = 0;
			}
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x000C4D20 File Offset: 0x000C2F20
		protected void CreateControlTree(ControlBuilder builder, bool inTemplate, bool childrenAsProperties)
		{
			this.EnsureID(builder);
			bool isTemplate = builder.IsTemplate;
			if (!isTemplate && !inTemplate)
			{
				this.CreateField(builder, true);
			}
			else if (!isTemplate)
			{
				bool flag = false;
				bool flag2 = false;
				ControlBuilder controlBuilder = builder.ParentBuilder;
				while (controlBuilder != null)
				{
					TemplateBuilder templateBuilder = controlBuilder as TemplateBuilder;
					if (templateBuilder == null)
					{
						controlBuilder = controlBuilder.ParentBuilder;
					}
					else
					{
						if (templateBuilder.TemplateInstance == TemplateInstance.Single)
						{
							flag2 = true;
							break;
						}
						break;
					}
				}
				if (!flag2)
				{
					builder.ID = builder.GetNextID(null);
				}
				else
				{
					flag = true;
				}
				this.CreateField(builder, flag);
			}
			this.InitMethod(builder, isTemplate, childrenAsProperties);
			if (!isTemplate || builder.GetType() == typeof(RootBuilder))
			{
				this.CreateAssignStatementsFromAttributes(builder);
			}
			if (builder.Children != null && builder.Children.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in builder.Children)
				{
					if (obj is string)
					{
						stringBuilder.Append((string)obj);
					}
					else
					{
						this.FlushText(builder, stringBuilder);
						if (obj is ObjectTagBuilder)
						{
							this.ProcessObjectTag((ObjectTagBuilder)obj);
						}
						else if (obj is StringPropertyBuilder)
						{
							StringPropertyBuilder stringPropertyBuilder = obj as StringPropertyBuilder;
							if (stringPropertyBuilder.Children != null && stringPropertyBuilder.Children.Count > 0)
							{
								StringBuilder stringBuilder2 = new StringBuilder();
								foreach (object obj2 in stringPropertyBuilder.Children)
								{
									string text = (string)obj2;
									stringBuilder2.Append(text);
								}
								CodeMemberMethod method = builder.Method;
								CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
								codeAssignStatement.Left = new CodePropertyReferenceExpression(TemplateControlCompiler.ctrlVar, stringPropertyBuilder.PropertyName);
								codeAssignStatement.Right = new CodePrimitiveExpression(stringBuilder2.ToString());
								method.Statements.Add(base.AddLinePragma(codeAssignStatement, builder));
							}
						}
						else
						{
							if (obj is ContentBuilderInternal)
							{
								ContentBuilderInternal contentBuilderInternal = (ContentBuilderInternal)obj;
								this.CreateControlTree(contentBuilderInternal, false, true);
								this.AddContentTemplateInvocation(contentBuilderInternal, builder.Method, contentBuilderInternal.Method.Name);
								continue;
							}
							if (!(obj is TemplateBuilder))
							{
								if (obj is CodeRenderBuilder)
								{
									this.AddCodeRender(builder, (CodeRenderBuilder)obj);
								}
								else if (obj is DataBindingBuilder)
								{
									this.AddDataBindingLiteral(builder, (DataBindingBuilder)obj);
								}
								else
								{
									if (obj is ControlBuilder)
									{
										ControlBuilder controlBuilder2 = (ControlBuilder)obj;
										this.CreateControlTree(controlBuilder2, inTemplate, builder.ChildrenAsProperties);
										this.AddChildCall(builder, controlBuilder2);
										continue;
									}
									throw new Exception("???");
								}
							}
						}
						ControlBuilder controlBuilder3 = obj as ControlBuilder;
						controlBuilder3.ProcessGeneratedCode(base.CompileUnit, base.BaseType, base.DerivedType, controlBuilder3.Method, controlBuilder3.DataBindingMethod);
					}
				}
				this.FlushText(builder, stringBuilder);
			}
			ControlBuilder defaultPropertyBuilder = builder.DefaultPropertyBuilder;
			if (defaultPropertyBuilder != null)
			{
				this.CreateControlTree(defaultPropertyBuilder, false, true);
				this.AddChildCall(builder, defaultPropertyBuilder);
			}
			if (builder.HasAspCode)
			{
				CodeMemberMethod renderMethod = builder.RenderMethod;
				CodeMethodReferenceExpression codeMethodReferenceExpression = new CodeMethodReferenceExpression();
				codeMethodReferenceExpression.TargetObject = BaseCompiler.thisRef;
				codeMethodReferenceExpression.MethodName = renderMethod.Name;
				CodeDelegateCreateExpression codeDelegateCreateExpression = new CodeDelegateCreateExpression();
				codeDelegateCreateExpression.DelegateType = new CodeTypeReference(typeof(RenderMethod));
				codeDelegateCreateExpression.TargetObject = BaseCompiler.thisRef;
				codeDelegateCreateExpression.MethodName = renderMethod.Name;
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
				codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(TemplateControlCompiler.ctrlVar, "SetRenderMethodDelegate");
				codeMethodInvokeExpression.Parameters.Add(codeDelegateCreateExpression);
				builder.MethodStatements.Add(codeMethodInvokeExpression);
			}
			if (builder is RootBuilder && !string.IsNullOrEmpty(this.parser.MetaResourceKey))
			{
				this.AssignPropertiesFromResources(builder, this.parser.BaseType, this.parser.MetaResourceKey);
			}
			if ((!isTemplate || builder is RootBuilder) && !string.IsNullOrEmpty(builder.GetAttribute("meta:resourcekey")))
			{
				this.CreateAssignStatementFromAttribute(builder, "meta:resourcekey");
			}
			if ((childrenAsProperties && builder.PropertyBuilderShouldReturnValue) || (!childrenAsProperties && typeof(Control).IsAssignableFrom(builder.ControlType)))
			{
				builder.Method.Statements.Add(new CodeMethodReturnStatement(TemplateControlCompiler.ctrlVar));
			}
			builder.ProcessGeneratedCode(base.CompileUnit, base.BaseType, base.DerivedType, builder.Method, builder.DataBindingMethod);
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x000C51F4 File Offset: 0x000C33F4
		protected override void AddStatementsToConstructor(CodeConstructor ctor)
		{
			if (this.masterPageContentPlaceHolders == null || this.masterPageContentPlaceHolders.Count == 0)
			{
				return;
			}
			CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement();
			codeVariableDeclarationStatement.Name = "__contentPlaceHolders";
			codeVariableDeclarationStatement.Type = new CodeTypeReference(typeof(IList));
			codeVariableDeclarationStatement.InitExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "ContentPlaceHolders");
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("__contentPlaceHolders");
			CodeStatementCollection statements = ctor.Statements;
			statements.Add(codeVariableDeclarationStatement);
			foreach (string text in this.masterPageContentPlaceHolders)
			{
				statements.Add(new CodeMethodInvokeExpression(codeVariableReferenceExpression, "Add", Array.Empty<CodeExpression>())
				{
					Parameters = 
					{
						new CodePrimitiveExpression(text.ToLowerInvariant())
					}
				});
			}
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x000C52E0 File Offset: 0x000C34E0
		protected internal override void CreateMethods()
		{
			base.CreateMethods();
			this.CreateProperties();
			this.CreateControlTree(this.parser.RootBuilder, false, false);
			this.CreateFrameworkInitializeMethod();
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x000C5308 File Offset: 0x000C3508
		protected override void InitializeType()
		{
			List<string> registeredTagNames = this.parser.RegisteredTagNames;
			RootBuilder rootBuilder = this.parser.RootBuilder;
			if (rootBuilder == null || registeredTagNames == null || registeredTagNames.Count == 0)
			{
				return;
			}
			foreach (string text in registeredTagNames)
			{
				AspComponent component = rootBuilder.Foundry.GetComponent(text);
				if (component == null || component.Type == null)
				{
					throw new HttpException("Custom control '" + text + "' cannot be found.");
				}
				if (!typeof(UserControl).IsAssignableFrom(component.Type))
				{
					throw new ParseException(this.parser.Location, "Type '" + component.Type.ToString() + "' does not derive from 'System.Web.UI.UserControl'.");
				}
				base.AddReferencedAssembly(component.Type.Assembly);
			}
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x000C5408 File Offset: 0x000C3608
		private void CallBaseFrameworkInitialize(CodeMemberMethod method)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "FrameworkInitialize", Array.Empty<CodeExpression>());
			method.Statements.Add(codeMethodInvokeExpression);
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x000C5438 File Offset: 0x000C3638
		private void CallSetStringResourcePointer(CodeMemberMethod method)
		{
			CodeFieldReferenceExpression mainClassFieldReferenceExpression = base.GetMainClassFieldReferenceExpression("__stringResource");
			method.Statements.Add(new CodeMethodInvokeExpression(BaseCompiler.thisRef, "SetStringResourcePointer", new CodeExpression[]
			{
				mainClassFieldReferenceExpression,
				new CodePrimitiveExpression(0)
			}));
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x000C5484 File Offset: 0x000C3684
		private void CreateFrameworkInitializeMethod()
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "FrameworkInitialize";
			codeMemberMethod.Attributes = (MemberAttributes)12292;
			this.PrependStatementsToFrameworkInitialize(codeMemberMethod);
			this.CallBaseFrameworkInitialize(codeMemberMethod);
			this.CallSetStringResourcePointer(codeMemberMethod);
			this.AppendStatementsToFrameworkInitialize(codeMemberMethod);
			this.mainClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void PrependStatementsToFrameworkInitialize(CodeMemberMethod method)
		{
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x000C54DC File Offset: 0x000C36DC
		protected virtual void AppendStatementsToFrameworkInitialize(CodeMemberMethod method)
		{
			if (!this.parser.EnableViewState)
			{
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "EnableViewState");
				codeAssignStatement.Right = new CodePrimitiveExpression(false);
				method.Statements.Add(codeAssignStatement);
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(BaseCompiler.thisRef, "__BuildControlTree"), new CodeExpression[] { BaseCompiler.thisRef });
			method.Statements.Add(new CodeExpressionStatement(codeMethodInvokeExpression));
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x000C5564 File Offset: 0x000C3764
		protected override void AddApplicationAndSessionObjects()
		{
			foreach (object obj in GlobalAsaxCompiler.ApplicationObjects)
			{
				ObjectTagBuilder objectTagBuilder = (ObjectTagBuilder)obj;
				base.CreateFieldForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID);
				base.CreateApplicationOrSessionPropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, true, false);
			}
			foreach (object obj2 in GlobalAsaxCompiler.SessionObjects)
			{
				ObjectTagBuilder objectTagBuilder2 = (ObjectTagBuilder)obj2;
				base.CreateApplicationOrSessionPropertyForObject(objectTagBuilder2.Type, objectTagBuilder2.ObjectID, false, false);
			}
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x000C5630 File Offset: 0x000C3830
		protected override void CreateStaticFields()
		{
			base.CreateStaticFields();
			CodeMemberField codeMemberField = new CodeMemberField(typeof(object), "__stringResource");
			codeMemberField.Attributes = (MemberAttributes)20483;
			codeMemberField.InitExpression = new CodePrimitiveExpression(null);
			this.mainClass.Members.Add(codeMemberField);
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x000C5684 File Offset: 0x000C3884
		protected void ProcessObjectTag(ObjectTagBuilder tag)
		{
			string text = base.CreateFieldForObject(tag.Type, tag.ObjectID);
			base.CreatePropertyForObject(tag.Type, tag.ObjectID, text, false);
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x000C56B8 File Offset: 0x000C38B8
		private void CreateProperties()
		{
			if (!this.parser.AutoEventWireup)
			{
				this.CreateAutoEventWireup();
			}
			else
			{
				this.CreateAutoHandlers();
			}
			this.CreateApplicationInstance();
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x000C56DC File Offset: 0x000C38DC
		private void CreateApplicationInstance()
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			Type typeFromHandle = typeof(HttpApplication);
			codeMemberProperty.Type = new CodeTypeReference(typeFromHandle);
			codeMemberProperty.Name = "ApplicationInstance";
			codeMemberProperty.Attributes = (MemberAttributes)12290;
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Context");
			codePropertyReferenceExpression = new CodePropertyReferenceExpression(codePropertyReferenceExpression, "ApplicationInstance");
			CodeCastExpression codeCastExpression = new CodeCastExpression(typeFromHandle.FullName, codePropertyReferenceExpression);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeCastExpression));
			if (this.partialClass != null)
			{
				this.partialClass.Members.Add(codeMemberProperty);
				return;
			}
			this.mainClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x000C5784 File Offset: 0x000C3984
		private void CreateContentPlaceHolderTemplateProperty(string backingField, string name)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(typeof(ITemplate));
			codeMemberProperty.Name = name;
			codeMemberProperty.Attributes = MemberAttributes.Public;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(BaseCompiler.thisRef, backingField);
			codeMethodReturnStatement.Expression = codeFieldReferenceExpression;
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			codeMemberProperty.SetStatements.Add(new CodeAssignStatement(codeFieldReferenceExpression, new CodePropertySetValueReferenceExpression()));
			codeMemberProperty.CustomAttributes.Add(new CodeAttributeDeclaration("TemplateContainer", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodeTypeOfExpression(new CodeTypeReference(typeof(MasterPage))))
			}));
			CodeFieldReferenceExpression codeFieldReferenceExpression2 = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(TemplateInstance)), "Single");
			codeMemberProperty.CustomAttributes.Add(new CodeAttributeDeclaration("TemplateInstanceAttribute", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(codeFieldReferenceExpression2)
			}));
			this.mainClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x000C5884 File Offset: 0x000C3A84
		private void CreateAutoHandlers()
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(typeof(int));
			codeMemberProperty.Name = "AutoHandlers";
			codeMemberProperty.Attributes = (MemberAttributes)12292;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(this.mainClassExpr, "__autoHandlers");
			codeMethodReturnStatement.Expression = codeFieldReferenceExpression;
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			codeMemberProperty.SetStatements.Add(new CodeAssignStatement(codeFieldReferenceExpression, new CodePropertySetValueReferenceExpression()));
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Obsolete");
			codeMemberProperty.CustomAttributes.Add(codeAttributeDeclaration);
			this.mainClass.Members.Add(codeMemberProperty);
			CodeMemberField codeMemberField = new CodeMemberField(typeof(int), "__autoHandlers");
			codeMemberField.Attributes = (MemberAttributes)20483;
			this.mainClass.Members.Add(codeMemberField);
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000C5964 File Offset: 0x000C3B64
		private void CreateAutoEventWireup()
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(typeof(bool));
			codeMemberProperty.Name = "SupportAutoEvents";
			codeMemberProperty.Attributes = (MemberAttributes)12292;
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(false)));
			this.mainClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x0000207C File Offset: 0x0000027C
		protected virtual string HandleUrlProperty(string str, MemberInfo member)
		{
			return str;
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000C59D0 File Offset: 0x000C3BD0
		private TypeConverter GetConverterForMember(MemberInfo member)
		{
			TypeDescriptionProvider provider = TypeDescriptor.GetProvider(member.ReflectedType);
			if (provider == null)
			{
				return null;
			}
			ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(member.ReflectedType);
			PropertyDescriptorCollection propertyDescriptorCollection = ((typeDescriptor != null) ? typeDescriptor.GetProperties() : null);
			if (propertyDescriptorCollection == null || propertyDescriptorCollection.Count == 0)
			{
				return null;
			}
			PropertyDescriptor propertyDescriptor = propertyDescriptorCollection.Find(member.Name, false);
			if (propertyDescriptor == null)
			{
				return null;
			}
			return propertyDescriptor.Converter;
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000C5A2E File Offset: 0x000C3C2E
		private CodeExpression CreateNullableExpression(Type type, CodeExpression inst, bool nullable)
		{
			if (!nullable)
			{
				return inst;
			}
			return new CodeObjectCreateExpression(type, new CodeExpression[] { inst });
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000C5A48 File Offset: 0x000C3C48
		private bool SafeCanConvertFrom(Type type, TypeConverter cvt)
		{
			bool flag;
			try
			{
				flag = cvt.CanConvertFrom(type);
			}
			catch (NotImplementedException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x000C5A78 File Offset: 0x000C3C78
		private bool SafeCanConvertTo(Type type, TypeConverter cvt)
		{
			bool flag;
			try
			{
				flag = cvt.CanConvertTo(type);
			}
			catch (NotImplementedException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000C5AA8 File Offset: 0x000C3CA8
		private CodeExpression GetExpressionFromString(Type type, string str, MemberInfo member)
		{
			TypeConverter typeConverter = this.GetConverterForMember(member);
			if (typeConverter != null && !this.SafeCanConvertFrom(typeof(string), typeConverter))
			{
				typeConverter = null;
			}
			object obj = null;
			bool flag = false;
			if (typeConverter != null && str != null)
			{
				obj = typeConverter.ConvertFromInvariantString(str);
				if (obj != null)
				{
					type = obj.GetType();
					flag = true;
				}
			}
			bool flag2 = false;
			Type type2 = type;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				Type[] genericArguments = type.GetGenericArguments();
				type2 = type;
				type = genericArguments[0];
				flag2 = true;
			}
			if (type == typeof(string))
			{
				if (member.GetCustomAttributes(typeof(UrlPropertyAttribute), true).Length != 0)
				{
					str = this.HandleUrlProperty((flag && obj is string) ? ((string)obj) : str, member);
				}
				else if (flag)
				{
					return this.CreateNullableExpression(type2, new CodePrimitiveExpression((string)obj), flag2);
				}
				return this.CreateNullableExpression(type2, new CodePrimitiveExpression(str), flag2);
			}
			if (type == typeof(bool))
			{
				if (flag)
				{
					return this.CreateNullableExpression(type2, new CodePrimitiveExpression((bool)obj), flag2);
				}
				if (str == null || str == "" || TemplateControlCompiler.InvariantCompareNoCase(str, "true"))
				{
					return this.CreateNullableExpression(type2, new CodePrimitiveExpression(true), flag2);
				}
				if (TemplateControlCompiler.InvariantCompareNoCase(str, "false"))
				{
					return this.CreateNullableExpression(type2, new CodePrimitiveExpression(false), flag2);
				}
				if (flag2 && TemplateControlCompiler.InvariantCompareNoCase(str, "null"))
				{
					return new CodePrimitiveExpression(null);
				}
				throw new ParseException(this.currentLocation, "Value '" + str + "' is not a valid boolean.");
			}
			else
			{
				if (type == TemplateControlCompiler.monoTypeType)
				{
					type = typeof(Type);
				}
				if (str == null)
				{
					return new CodePrimitiveExpression(null);
				}
				if (type.IsPrimitive)
				{
					return this.CreateNullableExpression(type2, new CodePrimitiveExpression(Convert.ChangeType(flag ? obj : str, type, Helpers.InvariantCulture)), flag2);
				}
				if (type == typeof(string[]))
				{
					string[] array;
					if (flag)
					{
						array = (string[])obj;
					}
					else
					{
						array = str.Split(new char[] { ',' });
					}
					CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression();
					codeArrayCreateExpression.CreateType = new CodeTypeReference(typeof(string));
					foreach (string text in array)
					{
						codeArrayCreateExpression.Initializers.Add(new CodePrimitiveExpression(text.Trim()));
					}
					return this.CreateNullableExpression(type2, codeArrayCreateExpression, flag2);
				}
				if (type == typeof(Color))
				{
					Color color;
					if (!flag)
					{
						if (TemplateControlCompiler.colorConverter == null)
						{
							TemplateControlCompiler.colorConverter = TypeDescriptor.GetConverter(typeof(Color));
						}
						if (str.Trim().Length == 0)
						{
							CodeTypeReferenceExpression codeTypeReferenceExpression = new CodeTypeReferenceExpression(typeof(Color));
							return this.CreateNullableExpression(type2, new CodeFieldReferenceExpression(codeTypeReferenceExpression, "Empty"), flag2);
						}
						try
						{
							if (str.IndexOf(',') == -1)
							{
								color = (Color)TemplateControlCompiler.colorConverter.ConvertFromString(str);
							}
							else
							{
								int[] array3 = new int[4];
								array3[0] = 255;
								string[] array4 = str.Split(new char[] { ',' });
								int num = array4.Length;
								if (num < 3)
								{
									throw new Exception();
								}
								int num2 = ((num == 4) ? 0 : 1);
								for (int j = num - 1; j >= 0; j--)
								{
									array3[num2 + j] = (int)byte.Parse(array4[j]);
								}
								color = Color.FromArgb(array3[0], array3[1], array3[2], array3[3]);
							}
							goto IL_03BB;
						}
						catch (Exception ex)
						{
							if (TemplateControlCompiler.InvariantCompareNoCase("LightGrey", str))
							{
								color = Color.LightGray;
								goto IL_03BB;
							}
							throw new ParseException(this.currentLocation, "Color " + str + " is not a valid color.", ex);
						}
					}
					color = (Color)obj;
					IL_03BB:
					if (color.IsKnownColor)
					{
						CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression();
						if (color.IsSystemColor)
						{
							type = typeof(SystemColors);
						}
						codeFieldReferenceExpression.TargetObject = new CodeTypeReferenceExpression(type);
						codeFieldReferenceExpression.FieldName = color.Name;
						return this.CreateNullableExpression(type2, codeFieldReferenceExpression, flag2);
					}
					return this.CreateNullableExpression(type2, new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
					{
						TargetObject = new CodeTypeReferenceExpression(type),
						MethodName = "FromArgb"
					}, Array.Empty<CodeExpression>())
					{
						Parameters = 
						{
							new CodePrimitiveExpression(color.A),
							new CodePrimitiveExpression(color.R),
							new CodePrimitiveExpression(color.G),
							new CodePrimitiveExpression(color.B)
						}
					}, flag2);
				}
				else
				{
					TypeConverter typeConverter2 = (flag ? typeConverter : (flag2 ? TypeDescriptor.GetConverter(type) : null));
					if (typeConverter2 == null)
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(member.DeclaringType)[member.Name];
						if (propertyDescriptor != null)
						{
							typeConverter2 = propertyDescriptor.Converter;
						}
						else
						{
							MemberTypes memberType = member.MemberType;
							Type type3;
							if (memberType != MemberTypes.Field)
							{
								if (memberType != MemberTypes.Property)
								{
									type3 = null;
								}
								else
								{
									type3 = ((PropertyInfo)member).PropertyType;
								}
							}
							else
							{
								type3 = ((FieldInfo)member).FieldType;
							}
							if (type3 == null)
							{
								return null;
							}
							typeConverter2 = TypeDescriptor.GetConverter(type3);
						}
					}
					if (!flag && (typeConverter2 == null || !this.SafeCanConvertFrom(typeof(string), typeConverter2)))
					{
						Console.WriteLine(string.Concat(new object[] { "Unknown type: ", type, " value: ", str }));
						return this.CreateNullableExpression(type2, new CodePrimitiveExpression(str), flag2);
					}
					object obj2 = (flag ? obj : typeConverter2.ConvertFromInvariantString(str));
					if (this.SafeCanConvertTo(typeof(InstanceDescriptor), typeConverter2))
					{
						InstanceDescriptor instanceDescriptor = (InstanceDescriptor)typeConverter2.ConvertTo(obj2, typeof(InstanceDescriptor));
						if (flag2)
						{
							return this.CreateNullableExpression(type2, this.GenerateInstance(instanceDescriptor, true), flag2);
						}
						CodeExpression codeExpression = this.GenerateInstance(instanceDescriptor, true);
						if (type.IsPublic)
						{
							return new CodeCastExpression(type, codeExpression);
						}
						return codeExpression;
					}
					else
					{
						CodeExpression codeExpression2 = this.GenerateObjectInstance(obj2, false);
						if (codeExpression2 != null)
						{
							return this.CreateNullableExpression(type2, codeExpression2, flag2);
						}
						CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
						{
							TargetObject = new CodeTypeReferenceExpression(typeof(TypeDescriptor)),
							MethodName = "GetConverter"
						}, Array.Empty<CodeExpression>());
						CodeTypeReference codeTypeReference = new CodeTypeReference(type);
						codeMethodInvokeExpression.Parameters.Add(new CodeTypeOfExpression(codeTypeReference));
						codeMethodInvokeExpression = new CodeMethodInvokeExpression(codeMethodInvokeExpression, "ConvertFrom", Array.Empty<CodeExpression>());
						codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(str));
						if (flag2)
						{
							return this.CreateNullableExpression(type2, codeMethodInvokeExpression, flag2);
						}
						return new CodeCastExpression(type, codeMethodInvokeExpression);
					}
				}
			}
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x000C6174 File Offset: 0x000C4374
		private CodeExpression GenerateInstance(InstanceDescriptor idesc, bool throwOnError)
		{
			CodeExpression[] array = new CodeExpression[idesc.Arguments.Count];
			int num = 0;
			foreach (object obj in idesc.Arguments)
			{
				CodeExpression codeExpression = this.GenerateObjectInstance(obj, throwOnError);
				if (codeExpression == null)
				{
					return null;
				}
				array[num++] = codeExpression;
			}
			MemberTypes memberType = idesc.MemberInfo.MemberType;
			if (memberType <= MemberTypes.Field)
			{
				if (memberType == MemberTypes.Constructor)
				{
					return new CodeObjectCreateExpression(new CodeTypeReference(idesc.MemberInfo.DeclaringType), array);
				}
				if (memberType == MemberTypes.Field)
				{
					return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(idesc.MemberInfo.DeclaringType), idesc.MemberInfo.Name);
				}
			}
			else
			{
				if (memberType == MemberTypes.Method)
				{
					return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(idesc.MemberInfo.DeclaringType), idesc.MemberInfo.Name, array);
				}
				if (memberType == MemberTypes.Property)
				{
					return new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(idesc.MemberInfo.DeclaringType), idesc.MemberInfo.Name);
				}
			}
			throw new ParseException(this.currentLocation, "Invalid instance type.");
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x000C62B4 File Offset: 0x000C44B4
		private CodeExpression GenerateObjectInstance(object value, bool throwOnError)
		{
			if (value == null)
			{
				return new CodePrimitiveExpression(null);
			}
			if (value is Type)
			{
				return new CodeTypeOfExpression(new CodeTypeReference(value.ToString()));
			}
			Type type = value.GetType();
			if (type.IsPrimitive || value is string)
			{
				return new CodePrimitiveExpression(value);
			}
			if (type.IsArray)
			{
				Array array = (Array)value;
				CodeExpression[] array2 = new CodeExpression[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					CodeExpression codeExpression = this.GenerateObjectInstance(array.GetValue(i), throwOnError);
					if (codeExpression == null)
					{
						return null;
					}
					array2[i] = codeExpression;
				}
				return new CodeArrayCreateExpression(new CodeTypeReference(type), array2);
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter != null && converter.CanConvertTo(typeof(InstanceDescriptor)))
			{
				InstanceDescriptor instanceDescriptor = (InstanceDescriptor)converter.ConvertTo(value, typeof(InstanceDescriptor));
				return this.GenerateInstance(instanceDescriptor, throwOnError);
			}
			InstanceDescriptor defaultInstanceDescriptor = this.GetDefaultInstanceDescriptor(value);
			if (defaultInstanceDescriptor != null)
			{
				return this.GenerateInstance(defaultInstanceDescriptor, throwOnError);
			}
			if (throwOnError)
			{
				throw new ParseException(this.currentLocation, "Cannot generate an instance for the type: " + type);
			}
			return null;
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x000C63CC File Offset: 0x000C45CC
		private InstanceDescriptor GetDefaultInstanceDescriptor(object value)
		{
			if (!(value is Unit))
			{
				if (value is FontUnit)
				{
					FontUnit fontUnit = (FontUnit)value;
					if (fontUnit.IsEmpty)
					{
						return new InstanceDescriptor(typeof(FontUnit).GetField("Empty"), null);
					}
					FontSize type = fontUnit.Type;
					Type type2;
					object obj;
					if (type <= FontSize.AsUnit)
					{
						type2 = typeof(Unit);
						obj = fontUnit.Unit;
					}
					else
					{
						type2 = typeof(string);
						obj = fontUnit.Type.ToString();
					}
					ConstructorInfo constructor = typeof(FontUnit).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[] { type2 }, null);
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[] { obj });
					}
				}
				return null;
			}
			Unit unit = (Unit)value;
			if (unit.IsEmpty)
			{
				return new InstanceDescriptor(typeof(Unit).GetField("Empty"), null);
			}
			return new InstanceDescriptor(typeof(Unit).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(double),
				typeof(UnitType)
			}, null), new object[] { unit.Value, unit.Type });
		}

		// Token: 0x04002546 RID: 9542
		private static BindingFlags noCaseFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04002547 RID: 9543
		private static Type monoTypeType = Type.GetType("System.MonoType");

		// Token: 0x04002548 RID: 9544
		private TemplateControlParser parser;

		// Token: 0x04002549 RID: 9545
		private int dataBoundAtts;

		// Token: 0x0400254A RID: 9546
		internal ILocation currentLocation;

		// Token: 0x0400254B RID: 9547
		private static TypeConverter colorConverter;

		// Token: 0x0400254C RID: 9548
		internal static CodeVariableReferenceExpression ctrlVar = new CodeVariableReferenceExpression("__ctrl");

		// Token: 0x0400254D RID: 9549
		private List<string> masterPageContentPlaceHolders;

		// Token: 0x0400254E RID: 9550
		private static Regex startsWithBindRegex = new Regex("^Bind\\s*\\(", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400254F RID: 9551
		private static Regex bindRegex = new Regex("Bind\\s*\\(\\s*[\"']+(.*?)[\"']+((\\s*,\\s*[\"']+(.*?)[\"']+)?)\\s*\\)\\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04002550 RID: 9552
		private static Regex bindRegexInValue = new Regex("Bind\\s*\\(\\s*[\"']+(.*?)[\"']+((\\s*,\\s*[\"']+(.*?)[\"']+)?)\\s*\\)\\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04002551 RID: 9553
		private static Regex evalRegexInValue = new Regex("(.*)Eval\\s*\\(\\s*[\"']+(.*?)[\"']+((\\s*,\\s*[\"']+(.*?)[\"']+)?)\\s*\\)(.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
