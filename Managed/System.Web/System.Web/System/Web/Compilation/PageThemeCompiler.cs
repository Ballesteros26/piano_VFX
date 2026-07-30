using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000661 RID: 1633
	internal class PageThemeCompiler : TemplateControlCompiler
	{
		// Token: 0x060045ED RID: 17901 RVA: 0x000C02F5 File Offset: 0x000BE4F5
		public PageThemeCompiler(PageThemeParser parser)
			: base(parser)
		{
			this.parser = parser;
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x000C0308 File Offset: 0x000BE508
		protected internal override void CreateMethods()
		{
			CodeMemberField codeMemberField = new CodeMemberField(typeof(HybridDictionary), "__controlSkins");
			codeMemberField.Attributes = MemberAttributes.Private;
			codeMemberField.InitExpression = new CodeObjectCreateExpression(typeof(HybridDictionary), Array.Empty<CodeExpression>());
			this.mainClass.Members.Add(codeMemberField);
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = "ControlSkins";
			codeMemberProperty.Attributes = (MemberAttributes)12292;
			codeMemberProperty.Type = new CodeTypeReference(typeof(IDictionary));
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("__controlSkins")));
			this.mainClass.Members.Add(codeMemberProperty);
			codeMemberField = new CodeMemberField(typeof(string[]), "__linkedStyleSheets");
			codeMemberField.Attributes = MemberAttributes.Private;
			codeMemberField.InitExpression = this.CreateLinkedStyleSheets();
			this.mainClass.Members.Add(codeMemberField);
			codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = "LinkedStyleSheets";
			codeMemberProperty.Attributes = (MemberAttributes)12292;
			codeMemberProperty.Type = new CodeTypeReference(typeof(string[]));
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("__linkedStyleSheets")));
			this.mainClass.Members.Add(codeMemberProperty);
			codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = "AppRelativeTemplateSourceDirectory";
			codeMemberProperty.Attributes = (MemberAttributes)12292;
			codeMemberProperty.Type = new CodeTypeReference(typeof(string));
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(VirtualPathUtility.ToAbsolute(this.parser.BaseVirtualDir))));
			this.mainClass.Members.Add(codeMemberProperty);
			ControlBuilder rootBuilder = this.parser.RootBuilder;
			if (rootBuilder.Children != null)
			{
				foreach (object obj in rootBuilder.Children)
				{
					if (obj is ControlBuilder && !(obj is CodeRenderBuilder))
					{
						ControlBuilder controlBuilder = (ControlBuilder)obj;
						this.CreateControlSkinMethod(controlBuilder);
					}
				}
			}
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x000C0540 File Offset: 0x000BE740
		private CodeExpression CreateLinkedStyleSheets()
		{
			string[] linkedStyleSheets = this.parser.LinkedStyleSheets;
			if (linkedStyleSheets == null)
			{
				return new CodePrimitiveExpression(null);
			}
			CodeExpression[] array = new CodeExpression[linkedStyleSheets.Length];
			for (int i = 0; i < linkedStyleSheets.Length; i++)
			{
				array[i] = new CodePrimitiveExpression(linkedStyleSheets[i]);
			}
			return new CodeArrayCreateExpression(typeof(string), array);
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x000C0595 File Offset: 0x000BE795
		protected override string HandleUrlProperty(string str, MemberInfo member)
		{
			if (str.StartsWith("~", StringComparison.Ordinal))
			{
				return str;
			}
			return "~/App_Themes/" + UrlUtils.Combine(Path.GetFileName(this.parser.InputFile), str);
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x000C05C8 File Offset: 0x000BE7C8
		private void CreateControlSkinMethod(ControlBuilder builder)
		{
			if (builder.ControlType == null)
			{
				return;
			}
			base.EnsureID(builder);
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "__BuildControl_" + builder.ID;
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Control), "ctrl"));
			this.mainClass.Members.Add(codeMemberMethod);
			builder.Method = codeMemberMethod;
			builder.MethodStatements = codeMemberMethod.Statements;
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(Control));
			CodeCastExpression codeCastExpression = new CodeCastExpression(builder.ControlType, new CodeVariableReferenceExpression("ctrl"));
			codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(builder.ControlType, "__ctrl"));
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = TemplateControlCompiler.ctrlVar;
			codeAssignStatement.Right = codeCastExpression;
			codeMemberMethod.Statements.Add(codeAssignStatement);
			base.CreateAssignStatementsFromAttributes(builder);
			if (builder.Children != null)
			{
				foreach (object obj in builder.Children)
				{
					if (obj is ControlBuilder)
					{
						ControlBuilder controlBuilder = (ControlBuilder)obj;
						if (!(controlBuilder.ControlType == null))
						{
							if (controlBuilder is CollectionBuilder)
							{
								PropertyInfo propertyInfo = null;
								try
								{
									propertyInfo = controlBuilder.GetType().GetProperty("Items");
								}
								catch (Exception)
								{
								}
								if (propertyInfo != null)
								{
									CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(TemplateControlCompiler.ctrlVar, controlBuilder.TagName), "Items");
									codeMemberMethod.Statements.Add(new CodeMethodInvokeExpression(codePropertyReferenceExpression, "Clear", Array.Empty<CodeExpression>()));
								}
							}
							base.CreateControlTree(controlBuilder, false, builder.ChildrenAsProperties);
							base.AddChildCall(builder, controlBuilder);
						}
					}
				}
			}
			builder.Method.Statements.Add(new CodeMethodReturnStatement(TemplateControlCompiler.ctrlVar));
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x000C07E4 File Offset: 0x000BE9E4
		protected override void AddClassAttributes()
		{
			base.AddClassAttributes();
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x000C07EC File Offset: 0x000BE9EC
		protected override void CreateStaticFields()
		{
			base.CreateStaticFields();
			ControlBuilder rootBuilder = this.parser.RootBuilder;
			if (rootBuilder.Children != null)
			{
				foreach (object obj in rootBuilder.Children)
				{
					if (!(obj is string) && !(obj is CodeRenderBuilder))
					{
						ControlBuilder controlBuilder = (ControlBuilder)obj;
						base.EnsureID(controlBuilder);
						Type controlType = controlBuilder.ControlType;
						if (!(controlType == null))
						{
							string id = controlBuilder.ID;
							string text = ((controlBuilder.Attributes != null) ? (controlBuilder.Attributes["skinid"] as string) : null);
							if (text == null)
							{
								text = "";
							}
							CodeMemberField codeMemberField = new CodeMemberField(typeof(object), "__BuildControl_" + id + "_skinKey");
							codeMemberField.Attributes = (MemberAttributes)20483;
							codeMemberField.InitExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(PageTheme)), "CreateSkinKey", new CodeExpression[]
							{
								new CodeTypeOfExpression(controlType),
								new CodePrimitiveExpression(text)
							});
							this.mainClass.Members.Add(codeMemberField);
						}
					}
				}
			}
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x000C0954 File Offset: 0x000BEB54
		protected override void CreateConstructor(CodeStatementCollection localVars, CodeStatementCollection trueStmt)
		{
			ControlBuilder rootBuilder = this.parser.RootBuilder;
			if (rootBuilder.Children != null)
			{
				foreach (object obj in rootBuilder.Children)
				{
					if (!(obj is string) && !(obj is CodeRenderBuilder))
					{
						ControlBuilder controlBuilder = (ControlBuilder)obj;
						Type controlType = controlBuilder.ControlType;
						if (!(controlType == null))
						{
							string id = controlBuilder.ID;
							if (localVars == null)
							{
								localVars = new CodeStatementCollection();
							}
							localVars.Add(new CodeAssignStatement(new CodeIndexerExpression(new CodePropertyReferenceExpression(BaseCompiler.thisRef, "__controlSkins"), new CodeExpression[]
							{
								new CodeVariableReferenceExpression("__BuildControl_" + id + "_skinKey")
							}), new CodeObjectCreateExpression(typeof(ControlSkin), new CodeExpression[]
							{
								new CodeTypeOfExpression(controlType),
								new CodeDelegateCreateExpression(new CodeTypeReference(typeof(ControlSkinDelegate)), BaseCompiler.thisRef, "__BuildControl_" + id)
							})));
						}
					}
				}
				base.CreateConstructor(localVars, trueStmt);
			}
		}

		// Token: 0x0400250D RID: 9485
		private PageThemeParser parser;
	}
}
