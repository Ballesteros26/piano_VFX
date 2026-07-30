using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Web.SessionState;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000660 RID: 1632
	internal class PageCompiler : TemplateControlCompiler
	{
		// Token: 0x060045D6 RID: 17878 RVA: 0x000BF165 File Offset: 0x000BD365
		public PageCompiler(PageParser pageParser)
			: base(pageParser)
		{
			this.pageParser = pageParser;
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x000BF178 File Offset: 0x000BD378
		protected override void CreateStaticFields()
		{
			base.CreateStaticFields();
			CodeMemberField codeMemberField = new CodeMemberField(typeof(object), "__fileDependencies");
			codeMemberField.Attributes = (MemberAttributes)20483;
			codeMemberField.InitExpression = new CodePrimitiveExpression(null);
			this.mainClass.Members.Add(codeMemberField);
			if (this.pageParser.OutputCache)
			{
				codeMemberField = new CodeMemberField(typeof(OutputCacheParameters), "__outputCacheSettings");
				codeMemberField.Attributes = (MemberAttributes)20483;
				codeMemberField.InitExpression = new CodePrimitiveExpression(null);
				this.mainClass.Members.Add(codeMemberField);
			}
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x000BF214 File Offset: 0x000BD414
		protected override void CreateConstructor(CodeStatementCollection localVars, CodeStatementCollection trueStmt)
		{
			MainDirectiveAttribute<string> masterPageFile = this.pageParser.MasterPageFile;
			if (masterPageFile != null && !masterPageFile.IsExpression)
			{
				BuildManager.GetCompiledType(masterPageFile.Value);
			}
			MainDirectiveAttribute<string> clientTarget = this.pageParser.ClientTarget;
			if (clientTarget != null)
			{
				CodeExpression codeExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "ClientTarget");
				CodeExpression codeExpression2 = null;
				if (clientTarget.IsExpression)
				{
					PropertyInfo propertyInfo = TemplateControlCompiler.GetFieldOrProperty(typeof(Page), "ClientTarget") as PropertyInfo;
					if (propertyInfo != null)
					{
						codeExpression2 = base.CompileExpression(propertyInfo, propertyInfo.PropertyType, clientTarget.UnparsedValue, false);
					}
				}
				if (codeExpression2 == null)
				{
					codeExpression2 = new CodePrimitiveExpression(clientTarget.Value);
				}
				if (localVars == null)
				{
					localVars = new CodeStatementCollection();
				}
				localVars.Add(new CodeAssignStatement(codeExpression, codeExpression2));
			}
			List<string> dependencies = this.pageParser.Dependencies;
			int num = ((dependencies != null) ? dependencies.Count : 0);
			if (num > 0)
			{
				if (localVars == null)
				{
					localVars = new CodeStatementCollection();
				}
				if (trueStmt == null)
				{
					trueStmt = new CodeStatementCollection();
				}
				localVars.Add(new CodeVariableDeclarationStatement(typeof(string[]), "dependencies"));
				CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("dependencies");
				trueStmt.Add(new CodeAssignStatement(codeVariableReferenceExpression, new CodeArrayCreateExpression(typeof(string), num)));
				CodeAssignStatement codeAssignStatement;
				for (int i = 0; i < num; i++)
				{
					object obj = dependencies[i];
					codeAssignStatement = new CodeAssignStatement(new CodeArrayIndexerExpression(codeVariableReferenceExpression, new CodeExpression[]
					{
						new CodePrimitiveExpression(i)
					}), new CodePrimitiveExpression(obj));
					trueStmt.Add(codeAssignStatement);
				}
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "GetWrappedFileDependencies", new CodeExpression[] { codeVariableReferenceExpression });
				codeAssignStatement = new CodeAssignStatement(base.GetMainClassFieldReferenceExpression("__fileDependencies"), codeMethodInvokeExpression);
				trueStmt.Add(codeAssignStatement);
			}
			base.CreateConstructor(localVars, trueStmt);
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x000BF3E4 File Offset: 0x000BD5E4
		protected override void AddInterfaces()
		{
			base.AddInterfaces();
			if (this.pageParser.EnableSessionState)
			{
				CodeTypeReference codeTypeReference = new CodeTypeReference(typeof(IRequiresSessionState));
				if (this.partialClass != null)
				{
					this.partialClass.BaseTypes.Add(codeTypeReference);
				}
				else
				{
					this.mainClass.BaseTypes.Add(codeTypeReference);
				}
			}
			if (this.pageParser.ReadOnlySessionState)
			{
				CodeTypeReference codeTypeReference = new CodeTypeReference(typeof(IReadOnlySessionState));
				if (this.partialClass != null)
				{
					this.partialClass.BaseTypes.Add(codeTypeReference);
				}
				else
				{
					this.mainClass.BaseTypes.Add(codeTypeReference);
				}
			}
			if (this.pageParser.Async)
			{
				this.mainClass.BaseTypes.Add(new CodeTypeReference(typeof(IHttpAsyncHandler)));
			}
			this.mainClass.BaseTypes.Add(new CodeTypeReference(typeof(IHttpHandler)));
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x000BF4DC File Offset: 0x000BD6DC
		private void CreateGetTypeHashCode()
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.ReturnType = PageCompiler.intRef;
			codeMemberMethod.Name = "GetTypeHashCode";
			codeMemberMethod.Attributes = (MemberAttributes)24580;
			Random random = new Random(this.pageParser.InputFile.GetHashCode());
			codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(random.Next())));
			this.mainClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x000BF55C File Offset: 0x000BD75C
		private static CodeExpression GetExpressionForValueAndType(object value, Type valueType)
		{
			if (valueType == typeof(TimeSpan))
			{
				return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(TimeSpan)), "Parse"), new CodeExpression[]
				{
					new CodePrimitiveExpression(((TimeSpan)value).ToString())
				});
			}
			throw new HttpException(string.Format("Unable to create assign expression for type '{0}'.", valueType));
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x000BF5CC File Offset: 0x000BD7CC
		private static CodeAssignStatement CreatePropertyAssign(CodeExpression owner, string name, CodeExpression rhs)
		{
			return new CodeAssignStatement(new CodePropertyReferenceExpression(owner, name), rhs);
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x000BF5DC File Offset: 0x000BD7DC
		private static CodeAssignStatement CreatePropertyAssign(CodeExpression owner, string name, object value)
		{
			CodeExpression codeExpression;
			if (value == null || value is string)
			{
				codeExpression = new CodePrimitiveExpression(value);
			}
			else
			{
				Type type = value.GetType();
				if (type.IsPrimitive)
				{
					codeExpression = new CodePrimitiveExpression(value);
				}
				else
				{
					codeExpression = PageCompiler.GetExpressionForValueAndType(value, type);
				}
			}
			return PageCompiler.CreatePropertyAssign(owner, name, codeExpression);
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x000BF625 File Offset: 0x000BD825
		private static CodeAssignStatement CreatePropertyAssign(string name, object value)
		{
			return PageCompiler.CreatePropertyAssign(BaseCompiler.thisRef, name, value);
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x000BF634 File Offset: 0x000BD834
		private void AssignPropertyWithExpression<T>(CodeMemberMethod method, string name, MainDirectiveAttribute<T> value, ILocation location)
		{
			if (value == null)
			{
				return;
			}
			CodeExpression codeExpression = null;
			if (value.IsExpression)
			{
				PropertyInfo propertyInfo = TemplateControlCompiler.GetFieldOrProperty(typeof(Page), name) as PropertyInfo;
				if (propertyInfo != null)
				{
					codeExpression = base.CompileExpression(propertyInfo, propertyInfo.PropertyType, value.UnparsedValue, false);
				}
			}
			CodeAssignStatement codeAssignStatement;
			if (codeExpression != null)
			{
				codeAssignStatement = PageCompiler.CreatePropertyAssign(BaseCompiler.thisRef, name, codeExpression);
			}
			else
			{
				codeAssignStatement = PageCompiler.CreatePropertyAssign(name, value.Value);
			}
			method.Statements.Add(base.AddLinePragma(codeAssignStatement, location));
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x000BF6BC File Offset: 0x000BD8BC
		private void AddStatementsFromDirective(ControlBuilder builder, CodeMemberMethod method, ILocation location)
		{
			this.AssignPropertyWithExpression<string>(method, "ResponseEncoding", this.pageParser.ResponseEncoding, location);
			this.AssignPropertyWithExpression<int>(method, "CodePage", this.pageParser.CodePage, location);
			this.AssignPropertyWithExpression<int>(method, "LCID", this.pageParser.LCID, location);
			string contentType = this.pageParser.ContentType;
			if (contentType != null)
			{
				method.Statements.Add(base.AddLinePragma(PageCompiler.CreatePropertyAssign("ContentType", contentType), location));
			}
			string text = this.pageParser.Culture;
			if (text != null)
			{
				method.Statements.Add(base.AddLinePragma(PageCompiler.CreatePropertyAssign("Culture", text), location));
			}
			text = this.pageParser.UICulture;
			if (text != null)
			{
				method.Statements.Add(base.AddLinePragma(PageCompiler.CreatePropertyAssign("UICulture", text), location));
			}
			string errorPage = this.pageParser.ErrorPage;
			if (errorPage != null)
			{
				method.Statements.Add(base.AddLinePragma(PageCompiler.CreatePropertyAssign("ErrorPage", errorPage), location));
			}
			if (this.pageParser.HaveTrace)
			{
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "TraceEnabled");
				codeAssignStatement.Right = new CodePrimitiveExpression(this.pageParser.Trace);
				method.Statements.Add(base.AddLinePragma(codeAssignStatement, location));
			}
			if (this.pageParser.TraceMode != TraceMode.Default)
			{
				CodeAssignStatement codeAssignStatement2 = new CodeAssignStatement();
				CodeTypeReferenceExpression codeTypeReferenceExpression = new CodeTypeReferenceExpression("System.Web.TraceMode");
				codeAssignStatement2.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "TraceModeValue");
				codeAssignStatement2.Right = new CodeFieldReferenceExpression(codeTypeReferenceExpression, this.pageParser.TraceMode.ToString());
				method.Statements.Add(base.AddLinePragma(codeAssignStatement2, location));
			}
			if (this.pageParser.NotBuffer)
			{
				CodeAssignStatement codeAssignStatement3 = new CodeAssignStatement();
				codeAssignStatement3.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Buffer");
				codeAssignStatement3.Right = new CodePrimitiveExpression(false);
				method.Statements.Add(base.AddLinePragma(codeAssignStatement3, location));
			}
			if (!this.pageParser.EnableEventValidation)
			{
				CodeAssignStatement codeAssignStatement4 = new CodeAssignStatement();
				CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "EnableEventValidation");
				codeAssignStatement4.Left = codePropertyReferenceExpression;
				codeAssignStatement4.Right = new CodePrimitiveExpression(this.pageParser.EnableEventValidation);
				method.Statements.Add(base.AddLinePragma(codeAssignStatement4, location));
			}
			if (this.pageParser.MaintainScrollPositionOnPostBack)
			{
				CodeAssignStatement codeAssignStatement5 = new CodeAssignStatement();
				CodePropertyReferenceExpression codePropertyReferenceExpression2 = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "MaintainScrollPositionOnPostBack");
				codeAssignStatement5.Left = codePropertyReferenceExpression2;
				codeAssignStatement5.Right = new CodePrimitiveExpression(this.pageParser.MaintainScrollPositionOnPostBack);
				method.Statements.Add(base.AddLinePragma(codeAssignStatement5, location));
			}
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x000BF99F File Offset: 0x000BDB9F
		protected override void AddStatementsToConstructor(CodeConstructor ctor)
		{
			base.AddStatementsToConstructor(ctor);
			if (this.pageParser.OutputCache)
			{
				this.OutputCacheParamsBlock(ctor);
			}
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x000BF9BC File Offset: 0x000BDBBC
		protected override void AddStatementsToInitMethodTop(ControlBuilder builder, CodeMemberMethod method)
		{
			base.AddStatementsToInitMethodTop(builder, method);
			ILocation directiveLocation = this.pageParser.DirectiveLocation;
			this.AddStatementsFromDirective(builder, method, directiveLocation);
			CodeArgumentReferenceExpression codeArgumentReferenceExpression = new CodeArgumentReferenceExpression("__ctrl");
			if (this.pageParser.EnableViewStateMacSet)
			{
				method.Statements.Add(base.AddLinePragma(PageCompiler.CreatePropertyAssign(codeArgumentReferenceExpression, "EnableViewStateMac", this.pageParser.EnableViewStateMacSet), directiveLocation));
			}
			this.AssignPropertyWithExpression<string>(method, "Title", this.pageParser.Title, directiveLocation);
			this.AssignPropertyWithExpression<string>(method, "MasterPageFile", this.pageParser.MasterPageFile, directiveLocation);
			this.AssignPropertyWithExpression<string>(method, "Theme", this.pageParser.Theme, directiveLocation);
			if (this.pageParser.StyleSheetTheme != null)
			{
				method.Statements.Add(base.AddLinePragma(PageCompiler.CreatePropertyAssign(codeArgumentReferenceExpression, "StyleSheetTheme", this.pageParser.StyleSheetTheme), directiveLocation));
			}
			if (this.pageParser.Async)
			{
				method.Statements.Add(base.AddLinePragma(PageCompiler.CreatePropertyAssign(codeArgumentReferenceExpression, "AsyncMode", this.pageParser.Async), directiveLocation));
			}
			if (this.pageParser.AsyncTimeout != -1)
			{
				method.Statements.Add(base.AddLinePragma(PageCompiler.CreatePropertyAssign(codeArgumentReferenceExpression, "AsyncTimeout", TimeSpan.FromSeconds((double)this.pageParser.AsyncTimeout)), directiveLocation));
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "InitializeCulture", Array.Empty<CodeExpression>());
			method.Statements.Add(base.AddLinePragma(new CodeExpressionStatement(codeMethodInvokeExpression), directiveLocation));
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x000BFB58 File Offset: 0x000BDD58
		protected override void AddStatementsToInitMethodBottom(ControlBuilder builder, CodeMemberMethod method)
		{
			ILocation directiveLocation = this.pageParser.DirectiveLocation;
			this.AssignPropertyWithExpression<string>(method, "MetaDescription", this.pageParser.MetaDescription, directiveLocation);
			this.AssignPropertyWithExpression<string>(method, "MetaKeywords", this.pageParser.MetaKeywords, directiveLocation);
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x000BFBA1 File Offset: 0x000BDDA1
		protected override void PrependStatementsToFrameworkInitialize(CodeMemberMethod method)
		{
			base.PrependStatementsToFrameworkInitialize(method);
			if (this.pageParser.StyleSheetTheme != null)
			{
				method.Statements.Add(PageCompiler.CreatePropertyAssign("StyleSheetTheme", this.pageParser.StyleSheetTheme));
			}
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x000BFBD8 File Offset: 0x000BDDD8
		protected override void AppendStatementsToFrameworkInitialize(CodeMemberMethod method)
		{
			base.AppendStatementsToFrameworkInitialize(method);
			List<string> dependencies = this.pageParser.Dependencies;
			if (((dependencies != null) ? dependencies.Count : 0) > 0)
			{
				CodeFieldReferenceExpression mainClassFieldReferenceExpression = base.GetMainClassFieldReferenceExpression("__fileDependencies");
				method.Statements.Add(new CodeMethodInvokeExpression(BaseCompiler.thisRef, "AddWrappedFileDependencies", new CodeExpression[] { mainClassFieldReferenceExpression }));
			}
			if (this.pageParser.OutputCache)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(BaseCompiler.thisRef, "InitOutputCache"), new CodeExpression[] { base.GetMainClassFieldReferenceExpression("__outputCacheSettings") });
				method.Statements.Add(codeMethodInvokeExpression);
			}
			if (this.pageParser.ValidateRequest)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
				CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Request");
				codeMethodInvokeExpression2.Method = new CodeMethodReferenceExpression(codePropertyReferenceExpression, "ValidateInput");
				method.Statements.Add(codeMethodInvokeExpression2);
			}
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x000BFCC0 File Offset: 0x000BDEC0
		private CodeAssignStatement AssignOutputCacheParameter(CodeVariableReferenceExpression variable, string propName, object value)
		{
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodeFieldReferenceExpression(variable, propName);
			if (value is OutputCacheLocation)
			{
				codeAssignStatement.Right = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(new CodeTypeReference(typeof(OutputCacheLocation), CodeTypeReferenceOptions.GlobalReference)), value.ToString());
			}
			else
			{
				codeAssignStatement.Right = new CodePrimitiveExpression(value);
			}
			return codeAssignStatement;
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x000BFD20 File Offset: 0x000BDF20
		private void OutputCacheParamsBlock(CodeMemberMethod method)
		{
			List<CodeStatement> list = new List<CodeStatement>();
			CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement(typeof(OutputCacheParameters), "outputCacheSettings");
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("outputCacheSettings");
			list.Add(codeVariableDeclarationStatement);
			list.Add(new CodeAssignStatement(codeVariableReferenceExpression, new CodeObjectCreateExpression(typeof(OutputCacheParameters), new CodeExpression[0])));
			TemplateParser.OutputCacheParsedParams outputCacheParsedParameters = this.pageParser.OutputCacheParsedParameters;
			if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.CacheProfile) != (TemplateParser.OutputCacheParsedParams)0)
			{
				list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "CacheProfile", this.pageParser.OutputCacheCacheProfile));
			}
			list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "Duration", this.pageParser.OutputCacheDuration));
			if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.Location) != (TemplateParser.OutputCacheParsedParams)0)
			{
				list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "Location", this.pageParser.OutputCacheLocation));
			}
			if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.NoStore) != (TemplateParser.OutputCacheParsedParams)0)
			{
				list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "NoStore", this.pageParser.OutputCacheNoStore));
			}
			if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.SqlDependency) != (TemplateParser.OutputCacheParsedParams)0)
			{
				list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "SqlDependency", this.pageParser.OutputCacheSqlDependency));
			}
			if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.VaryByContentEncodings) != (TemplateParser.OutputCacheParsedParams)0)
			{
				list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByContentEncoding", this.pageParser.OutputCacheVaryByContentEncodings));
			}
			if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.VaryByControl) != (TemplateParser.OutputCacheParsedParams)0)
			{
				list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByControl", this.pageParser.OutputCacheVaryByControls));
			}
			if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.VaryByCustom) != (TemplateParser.OutputCacheParsedParams)0)
			{
				list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByCustom", this.pageParser.OutputCacheVaryByCustom));
			}
			if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.VaryByHeader) != (TemplateParser.OutputCacheParsedParams)0)
			{
				list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByHeader", this.pageParser.OutputCacheVaryByHeader));
			}
			list.Add(this.AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByParam", this.pageParser.OutputCacheVaryByParam));
			CodeFieldReferenceExpression mainClassFieldReferenceExpression = base.GetMainClassFieldReferenceExpression("__outputCacheSettings");
			list.Add(new CodeAssignStatement(mainClassFieldReferenceExpression, codeVariableReferenceExpression));
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(mainClassFieldReferenceExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)), list.ToArray());
			method.Statements.Add(codeConditionStatement);
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x000BFF28 File Offset: 0x000BE128
		private void CreateStronglyTypedProperty(Type type, string name)
		{
			if (type == null)
			{
				return;
			}
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = name;
			codeMemberProperty.Type = new CodeTypeReference(type);
			codeMemberProperty.Attributes = (MemberAttributes)24592;
			CodeExpression codeExpression = new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), name);
			codeExpression = new CodeCastExpression(type, codeExpression);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeExpression));
			if (this.partialClass != null)
			{
				this.partialClass.Members.Add(codeMemberProperty);
			}
			else
			{
				this.mainClass.Members.Add(codeMemberProperty);
			}
			base.AddReferencedAssembly(type.Assembly);
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x000BFFC4 File Offset: 0x000BE1C4
		protected internal override void CreateMethods()
		{
			base.CreateMethods();
			base.CreateProfileProperty();
			this.CreateStronglyTypedProperty(this.pageParser.MasterType, "Master");
			this.CreateStronglyTypedProperty(this.pageParser.PreviousPageType, "PreviousPage");
			this.CreateGetTypeHashCode();
			if (this.pageParser.Async)
			{
				this.CreateAsyncMethods();
			}
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x000C0024 File Offset: 0x000BE224
		private void CreateAsyncMethods()
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(IAsyncResult));
			codeMemberMethod.Name = "BeginProcessRequest";
			codeMemberMethod.Attributes = MemberAttributes.Public;
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
			codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(HttpContext));
			codeParameterDeclarationExpression.Name = "context";
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
			codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(AsyncCallback));
			codeParameterDeclarationExpression.Name = "cb";
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
			codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(object));
			codeParameterDeclarationExpression.Name = "data";
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "AsyncPageBeginProcessRequest", Array.Empty<CodeExpression>());
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("context"));
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("cb"));
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("data"));
			codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(codeMethodInvokeExpression));
			this.mainClass.Members.Add(codeMemberMethod);
			codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
			codeMemberMethod.Name = "EndProcessRequest";
			codeMemberMethod.Attributes = MemberAttributes.Public;
			codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
			codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(IAsyncResult));
			codeParameterDeclarationExpression.Name = "ar";
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			codeMethodInvokeExpression = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "AsyncPageEndProcessRequest", Array.Empty<CodeExpression>());
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("ar"));
			codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
			this.mainClass.Members.Add(codeMemberMethod);
			codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
			codeMemberMethod.Name = "ProcessRequest";
			codeMemberMethod.Attributes = (MemberAttributes)24580;
			codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
			codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(HttpContext));
			codeParameterDeclarationExpression.Name = "context";
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "ProcessRequest", Array.Empty<CodeExpression>());
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("context"));
			codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
			this.mainClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x000C02D2 File Offset: 0x000BE4D2
		public static Type CompilePageType(PageParser pageParser)
		{
			return new PageCompiler(pageParser).GetCompiledType();
		}

		// Token: 0x0400250B RID: 9483
		private PageParser pageParser;

		// Token: 0x0400250C RID: 9484
		private static CodeTypeReference intRef = new CodeTypeReference(typeof(int));
	}
}
